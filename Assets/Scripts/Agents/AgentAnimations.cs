using UnityEngine;

namespace Agents
{
    public class AgentAnimations : MonoBehaviour
    {
        [SerializeField] Animator animator;

        readonly int speedID = Animator.StringToHash("Speed");
        readonly int motionSpeed = Animator.StringToHash("MotionSpeed");
    
        float minAnimationSpeed = 1.5f;
        float maxAnimationSpeed = 3f;
        float minSpeed = 1;
        float maxSpeed = 10;
    

        public void SetSpeed(float speed)
        {
            float animationSpeed = GetMappedValue(speed);
            animator.SetFloat(speedID, animationSpeed);
            animator.SetFloat(motionSpeed, animationSpeed);
        }
    
        float GetMappedValue(float currentSpeed)
        {
            return minAnimationSpeed + (currentSpeed - minSpeed) * (maxAnimationSpeed - minAnimationSpeed) / (maxSpeed - minSpeed);
        }
    }
}
