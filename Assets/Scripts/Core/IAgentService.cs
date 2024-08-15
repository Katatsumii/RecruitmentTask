using UnityEngine.Events;

namespace Core
{
    public interface IAgentService
    {
        event UnityAction OnAgentSpawned;
        event UnityAction OnAgentDisabled;
        void RequestAgentSpawn();
        void RequestAgentDisable();
    }
}
