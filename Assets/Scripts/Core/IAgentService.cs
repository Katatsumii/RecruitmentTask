using UnityEngine.Events;

namespace Core
{
    public interface IAgentService
    {
        event UnityAction<int> OnAgentAmountChanged;
        event UnityAction<string> OnAgentReachedDestination;
        void RequestAgentSpawn();
        void RequestRandomAgentDisabled();
        void RequestAllAgentsDisabled();
    }
}
