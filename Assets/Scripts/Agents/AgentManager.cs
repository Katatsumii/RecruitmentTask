using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Agents
{
    public class AgentManager : MonoBehaviour, IAgentService
    {
        [Header("Object pooling")]
        [SerializeField] int poolStartSize;
        [SerializeField] Agent poolPrefab;
        List<Agent> agentsPool = new();
        List<Agent> currentlyUsedAgents = new();

        public event UnityAction<int> OnAgentAmountChanged = delegate {};
        public event UnityAction<string> OnAgentReachedDestination = delegate { };
        

        ITickService iTickService;
        int currentTickrate;
        

        void Awake()
        {
            ServiceLocator.RegisterService<IAgentService>(this);
            CreateInitialPool();
        }

        void Start()
        {
            iTickService = ServiceLocator.GetService<ITickService>();
            iTickService.OnTickRateSet += SetTickRate;
        }

        void OnDestroy()
        {
            iTickService.OnTickRateSet -= SetTickRate;
            foreach (var agent in currentlyUsedAgents)
            {
                agent.OnAgentPathCompleted -= AgentPathCompleted;
            }
        }

        void RefreshAgentsAmount()
        {
            OnAgentAmountChanged.Invoke(currentlyUsedAgents.Count);
        }

        void SetTickRate(int tickRate)
        {
            currentTickrate = tickRate;
            foreach (var agent in currentlyUsedAgents)
            {
                agent.SetTickRateForAgent(currentTickrate);
            }
        }


        #region IAgentService

        void IAgentService.RequestAgentSpawn()
        {
            var agent = GetAgentFromPool();
            agent.gameObject.SetActive(true);
            agent.InitAgent();
            agent.OnAgentPathCompleted += AgentPathCompleted;
            currentlyUsedAgents.Add(agent);
            RefreshAgentsAmount();

        }

        void IAgentService.RequestRandomAgentDisabled()
        {
            if (currentlyUsedAgents.Count == 0) return;
            
            int i = Random.Range(0, currentlyUsedAgents.Count - 1);
            
            var agentToDisable = currentlyUsedAgents[i];
            agentToDisable.gameObject.SetActive(false);
            agentToDisable.OnAgentPathCompleted -= AgentPathCompleted;
            currentlyUsedAgents.Remove(agentToDisable);
            
            RefreshAgentsAmount();
        }

        void IAgentService.RequestAllAgentsDisabled()
        {
            for (int i = currentlyUsedAgents.Count - 1; i >= 0; i--)
            {
                currentlyUsedAgents[i].gameObject.SetActive(false);
                currentlyUsedAgents[i].OnAgentPathCompleted -= AgentPathCompleted;
                currentlyUsedAgents.RemoveAt(i);
            }
            
            RefreshAgentsAmount();
        }

        #endregion

        #region ObjectPooling

        Agent GetAgentFromPool()
        {
            foreach (var agent in agentsPool)
            {
                if (!agent.gameObject.activeSelf)
                    return agent;
            }

            var createdAgent = Instantiate(poolPrefab, transform);
            agentsPool.Add(createdAgent);
            return createdAgent;
        }

        void CreateInitialPool()
        {
            for (int i = 0; i < poolStartSize; i++)
            {
                var createdAgent = Instantiate(poolPrefab, transform);
                agentsPool.Add(createdAgent);
            }
        }

        void AgentPathCompleted(string agentID)
        {
            OnAgentReachedDestination.Invoke(agentID);
        }

        #endregion
    }
}