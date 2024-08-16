using TMPro;
using UnityEngine;

namespace UI
{
    public class AgentsLogText : MonoBehaviour
    {
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] TextMeshProUGUI logText;

        public CanvasGroup CanvasGroup => canvasGroup;


        public void SetLogText(string id)
        {
            logText.text = "Agent " + "<b><color=yellow>" + id + "</b></color=yellow>" + " arrived.";
        }
    }
}
