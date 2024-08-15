using UnityEngine.Events;

namespace Core
{
    public interface ITickService
    {
        event UnityAction OnTickRateSet;
        void SetTickRate();
    }
}
