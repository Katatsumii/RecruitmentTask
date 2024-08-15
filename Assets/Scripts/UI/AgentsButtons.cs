using Core;
using UnityEngine;

namespace UI
{
    public class AgentsButtons : MonoBehaviour
    {
        IAgentService iAgentService;

        public void SpawnAgent()
        {
            iAgentService.RequestAgentSpawn();
        }

        public void DisableRandomAgent()
        {
            iAgentService.RequestRandomAgentDisabled();
        }

        public void DisableAllAgents()
        {
            iAgentService.RequestAllAgentsDisabled();
        }
    }
}
