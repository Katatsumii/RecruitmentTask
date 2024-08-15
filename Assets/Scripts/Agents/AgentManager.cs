using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.Events;

namespace Agents
{
    public class AgentManager : MonoBehaviour, IAgentService, ITickService
    {
        [Header("Object pooling")] [SerializeField]
        int poolStartSize;

        [SerializeField] Agent poolPrefab;
        List<Agent> agentsPool = new();
        List<Agent> currentlyUsedAgents = new();

        public event UnityAction OnAgentSpawned = delegate {};
        public event UnityAction OnAgentDisabled = delegate {};
        public event UnityAction OnTickRateSet = delegate {};

        void Awake()
        {
            CreateInitialPool();
        }


        #region IAgentService

        void IAgentService.RequestAgentSpawn()
        {
            Agent agent = GetAgentFromPool();
            agent.gameObject.SetActive(true);
            currentlyUsedAgents.Add(agent);
            OnAgentSpawned.Invoke();
        }

        void IAgentService.RequestAgentDisable()
        {
            if (currentlyUsedAgents.Count == 0) return;
            
            int i = Random.Range(0, currentlyUsedAgents.Count - 1);
            Agent agentToDisable = currentlyUsedAgents[i];
            agentToDisable.gameObject.SetActive(false);
            currentlyUsedAgents.Remove(agentToDisable);
            OnAgentDisabled.Invoke();
        }

        #endregion

        #region ITickService

        void ITickService.SetTickRate()
        {
            
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

        #endregion
    }
}