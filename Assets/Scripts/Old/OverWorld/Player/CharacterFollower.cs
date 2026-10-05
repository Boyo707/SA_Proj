using UnityEngine;

namespace PP.Player
{
    public class CharacterFollower : MonoBehaviour
    {
        [Header("Required Components")]
        [SerializeField] private Transform playerTransform;
        [SerializeField] private PlayerMovementOld playerMovement;

        [Header("Follower Settings")]
        [SerializeField] private float followSpeed;
        [SerializeField] private float followDistance;
        [SerializeField] private float minDistance;


        private Vector2 velocity;

        // Update is called once per frame
        void Update()
        {
            if (Vector2.Distance(transform.position, playerTransform.position) > minDistance)
            {
                Vector2 newPosition = playerTransform.position - playerMovement.PlayerDirection * followDistance;
                transform.position = Vector2.SmoothDamp(transform.position, newPosition, ref velocity, Time.deltaTime, followSpeed);
            }
        }
    }
}
