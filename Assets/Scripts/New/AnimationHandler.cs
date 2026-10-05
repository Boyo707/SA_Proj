using UnityEngine;

namespace PP.Player
{
    public class AnimationHandler : MonoBehaviour
    {
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        private Vector3 faceDir;
        private Vector3 moveDir;

        public Vector3 CurrentFaceDirection => faceDir;
        public Vector3 CurrentMoveDirection => moveDir;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void AnimateWalkingDirection(Vector3 moveDirection, Vector3 facedirection)
        {
            faceDir = GetStraightAngle(facedirection);
            moveDir = GetStraightAngle(moveDirection);

            Vector2 animDirection = moveDir + faceDir;
           

            animator.SetFloat("XDir", animDirection.x);

            spriteRenderer.flipX = animDirection.x < 0;

            animator.SetFloat("YDir", animDirection.y);
        }

        public void AnimateWalkingDirection(Vector3 moveDirection)
        {
            faceDir = GetStraightAngle(moveDirection);
            moveDir = GetStraightAngle(moveDirection);

            Vector2 animDirection = moveDir + faceDir;


            animator.SetFloat("XDir", animDirection.x);

            spriteRenderer.flipX = animDirection.x < 0;

            animator.SetFloat("YDir", animDirection.y);
        }

        public void AnimateFacingDirection(Vector3 facedirection)
        {
            faceDir = GetStraightAngle(facedirection);

            Vector2 animDirection =  faceDir;

            animator.SetFloat("XDir", animDirection.x);

            spriteRenderer.flipX = animDirection.x < 0;

            animator.SetFloat("YDir", animDirection.y);
        }

        public void AnimateFacingDirection()
        {
            Vector2 animDirection = faceDir;

            animator.SetFloat("XDir", animDirection.x);

            spriteRenderer.flipX = animDirection.x < 0;

            animator.SetFloat("YDir", animDirection.y);
        }

        private Vector3 GetStraightAngle(Vector3 inputVector)
        {
            if (inputVector == Vector3.zero) return Vector3.zero;

            float x = Mathf.Abs(inputVector.x);
            float y = Mathf.Abs(inputVector.y);
            float z = Mathf.Abs(inputVector.z);

            // Controleer of X de dominante as is
            if (x >= y && x >= z)
            {
                return new Vector3(inputVector.x, 0f, 0f).normalized;
            }
            // Controleer of Y de dominante as is
            else if (y >= x && y >= z)
            {
                return new Vector3(0f, inputVector.y, 0f).normalized;
            }
            // Anders is Z de dominante as
            else
            {
                return new Vector3(0f, 0f, inputVector.z).normalized;
            }
        }
    }
}
