using Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class TimeButtons : MonoBehaviour, ITickService
    {
        [Header("Values")] [SerializeField] int minTickrate;
        [SerializeField] int maxTickrate;

        [Header("UI")] [SerializeField] Button addTickRateButton;
        [SerializeField] Button lowerTickRateButton;
        [SerializeField] Image pauseButtonIcon;
        [SerializeField] Sprite pauseSprite;
        [SerializeField] Sprite playSprite;

        int currentTickRate = 1;
        bool paused = false;

        public event UnityAction<int> OnTickRateSet = delegate { };

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
            if (currentTickRate > maxTickrate)
            {
                currentTickRate = maxTickrate;
                return;
            }

            SetTickRate(currentTickRate);
        }

        public void LowerTickrateButton()
        {
            currentTickRate--;

            if (currentTickRate < minTickrate)
            {
                currentTickRate = minTickrate;
                return;
            }

            SetTickRate(currentTickRate);
        }

        public void PauseButton()
        {
            if (!paused)
            {
                SetTickRate(0);
                addTickRateButton.interactable = false;
                lowerTickRateButton.interactable = false;
                pauseButtonIcon.sprite = playSprite;
            }
            else
            {
                SetTickRate(currentTickRate);
                addTickRateButton.interactable = true;
                lowerTickRateButton.interactable = true;
                pauseButtonIcon.sprite = pauseSprite;
            }

            paused = !paused;
        }
    }
}