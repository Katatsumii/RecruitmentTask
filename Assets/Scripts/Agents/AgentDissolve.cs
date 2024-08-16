using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Agents
{
    public class AgentDissolve : MonoBehaviour
    {
        [Header("Components")] [SerializeField]
        Renderer renderer;

        [SerializeField] GameObject dissolveParticles;
        [SerializeField] GameObject materializeParticles;

        [Header("Values")] [SerializeField] float dissolveTime = 1f;

        MaterialPropertyBlock materialPropertyBlock;

        readonly float dissolvedValue = 2f;
        readonly float materializedValue = 0f;
        readonly int dissolveID = Shader.PropertyToID("_Dissolve");


        public void MaterializeAgent(UnityAction onMaterializeEnd)
        {
            materializeParticles.SetActive(true);
            materialPropertyBlock = new MaterialPropertyBlock();

            DOTween.To(() => dissolvedValue,
                    x =>
                    {
                        materialPropertyBlock.SetFloat(dissolveID, x);
                        renderer.SetPropertyBlock(materialPropertyBlock);
                    },
                    materializedValue,
                    dissolveTime)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    materializeParticles.SetActive(false);
                    onMaterializeEnd.Invoke();
                });
        }


        public void DissolveAgent(UnityAction onDissolveEnd)
        {
            dissolveParticles.SetActive(true);

            DOTween.To(() => materializedValue,
                    x =>
                    {
                        materialPropertyBlock.SetFloat(dissolveID, x);
                        renderer.SetPropertyBlock(materialPropertyBlock);
                    },
                    dissolvedValue,
                    dissolveTime)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    dissolveParticles.SetActive(false);
                    onDissolveEnd.Invoke();
                });
        }
    }
}