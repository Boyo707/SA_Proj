using UnityEngine;

namespace PP.Player
{
    public enum PlayerDirection
    {
        down,
        up,
        left,
        right
    }
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        private PlayerDirection currentDirection;
        private Vector2 direction = Vector2.up;
        private Vector3 moveDirection = Vector3.zero;

        private Rigidbody2D rb;

        //public PlayerDirection CurrentDirection => currentDirection;
        public Vector3 FaceDirection => direction;
        public Vector3 MoveDirection => moveDirection;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void MoveCharacter(int horizontal, int vertical)
        {
            rb.linearVelocity = new Vector2(horizontal, vertical).normalized * moveSpeed;

            if (horizontal != 0 && vertical != 0)
            {
                return;
            }
            else if (horizontal != 0)
            {
                if (horizontal > 0)
                {
                    currentDirection = PlayerDirection.right;
                    direction = Vector2.right;
                }
                else
                {
                    currentDirection = PlayerDirection.left;
                    direction = Vector2.left;
                }
                moveDirection = direction;
            }
            else if (vertical != 0)
            {
                if (vertical > 0)
                {
                    currentDirection = PlayerDirection.up;
                    direction = Vector2.up;
                }
                else
                {
                    currentDirection = PlayerDirection.down;
                    direction = Vector2.down;
                }
                moveDirection = direction;
            }
            else if (horizontal == 0 && vertical == 0)
            {
                moveDirection = Vector2.zero;
            }
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, direction);
        }
    }
}
