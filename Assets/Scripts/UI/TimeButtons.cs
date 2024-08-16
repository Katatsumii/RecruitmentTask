using System;
using Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class TimeButtons : MonoBehaviour, ITickService
    {
        [Header("Values")]
        [SerializeField] int minTickrate;
        [SerializeField] int maxTickrate;
        int currentTickRate;
        bool paused = false;
        
        public event UnityAction<int> OnTickRateSet = delegate {};

        void Awake()
        {
            ServiceLocator.RegisterService<ITickService>(this);
        }

        public void SetTickRate(int tickRate)
        {
            OnTickRateSet.Invoke(tickRate);
        }

        public void HigherTickrateButton()
        {
            currentTickRate++;
            SetTickRate(currentTickRate);
        }

        public void LowerTickrateButton()
        {
            currentTickRate--;
            SetTickRate(currentTickRate);
        }

        public void PauseButton()
        {
            if (!paused)
                SetTickRate(0);
            else
                SetTickRate(currentTickRate);

            paused = !paused;
        }
    }
}
