using UnityEngine;
using TMPro;

namespace UI
{
    public class AgentsAmountLabel : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] TextMeshProUGUI agentsLabel;

        void SetAgentsNumber(int agentsCount)
        {
            agentsLabel.text = agentsCount.ToString();
        }

    }
}
