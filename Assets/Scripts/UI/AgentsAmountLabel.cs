using Core;
using UnityEngine;
using TMPro;

namespace UI
{
    public class AgentsAmountLabel : MonoBehaviour
    {
        [Header("UI")] [SerializeField] TextMeshProUGUI agentsLabel;

        IAgentService iAgentService;

        void Start()
        {
            iAgentService = ServiceLocator.GetService<IAgentService>();
            iAgentService.OnAgentAmountChanged += SetAgentsNumber;
        }

        void OnDestroy()
        {
            iAgentService.OnAgentAmountChanged -= SetAgentsNumber;
        }

        void SetAgentsNumber(int agentsCount)
        {
            agentsLabel.text = agentsCount.ToString();
        }
    }
}