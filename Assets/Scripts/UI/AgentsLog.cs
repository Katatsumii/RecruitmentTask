using System;
using System.Collections.Generic;
using Core;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class AgentsLog : MonoBehaviour
    {
        [Header("Pool")]
        [SerializeField, Range(0, 50)] int poolStartSize;
        [SerializeField] AgentsLogText logTextPrefab;
        List<AgentsLogText> logTextPool = new();

        

        [Header("Text fade values")]
        [SerializeField] float fadeDelay;
        [SerializeField] float fadeTime;

        Dictionary<AgentsLogText, Tween> currentTweens = new();
        
        IAgentService iAgentService;

        void Awake()
        {
            CreateInitialPool();
        }

        void Start()
        {
            iAgentService = ServiceLocator.GetService<IAgentService>();
            iAgentService.OnAgentReachedDestination += ShowLogMessage;
        }

        void OnDestroy()
        {
            iAgentService.OnAgentReachedDestination -= ShowLogMessage;
        }

        void ShowLogMessage(string id)
        {
            var consoleText = GetTextFromPool();
            consoleText.gameObject.SetActive(true);
            consoleText.transform.SetAsLastSibling();
            consoleText.SetLogText(id);
            Tween fadeTween = consoleText.CanvasGroup.DOFade(0, fadeTime)
                .SetDelay(fadeDelay).OnComplete(() =>
                {
                    consoleText.gameObject.SetActive(false);
                    consoleText.CanvasGroup.alpha = 1;
                    currentTweens.Remove(consoleText);
                });
            
            currentTweens.Add(consoleText, fadeTween);
        }

        AgentsLogText GetTextFromPool()
        {
            foreach (var text in logTextPool)
            {
                if (!text.gameObject.activeSelf)
                    return text;
            }

            AgentsLogText textToReUse = logTextPool[0];
            
            logTextPool.Remove(textToReUse);
            logTextPool.Add(textToReUse); //to change index of element to last one
            
            if (currentTweens.ContainsKey(textToReUse))
            {
                currentTweens[textToReUse].Kill();
                currentTweens.Remove(textToReUse);
                textToReUse.CanvasGroup.alpha = 1f;
            }

            return textToReUse;
        }
        
        void CreateInitialPool()
        {
            for (int i = 0; i < poolStartSize; i++)
            {
                var createdLogText = Instantiate(logTextPrefab, transform);
                createdLogText.gameObject.SetActive(false);
                logTextPool.Add(createdLogText);
            }
        }
    }
}
