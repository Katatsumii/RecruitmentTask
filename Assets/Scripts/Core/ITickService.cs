using UnityEngine.Events;

namespace Core
{
    public interface ITickService
    {
        event UnityAction<int> OnTickRateSet;
        void SetTickRate(int tickRate);
    }
}