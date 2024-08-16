using System;
using Core;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TimeLabel : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI label;

        ITickService iTickService;
        void Start()
        {
            iTickService = ServiceLocator.GetService<ITickService>();
            iTickService.OnTickRateSet += UpdateTimeLabel;
        }

        void OnDestroy()
        {
            iTickService.OnTickRateSet -= UpdateTimeLabel;
        }

        void UpdateTimeLabel(int tickRate)
        {
            label.text = tickRate.ToString();
        }
    }
}
