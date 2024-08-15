using UnityEngine.Events;

namespace Core
{
    public interface IAgentService
    {
        event UnityAction<int> OnAgentAmountChanged;
        void RequestAgentSpawn();
        void RequestRandomAgentDisabled();
        void RequestAllAgentsDisabled();
    }
}
