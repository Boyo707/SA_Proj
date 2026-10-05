using UnityEngine;
namespace PP.Player
{
    public class PlayerMovementOld : MonoBehaviour
    {
        [SerializeField] private float playerSpeed;
        private Rigidbody2D rB;

        public Vector3 PlayerDirection;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rB = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (rB.linearVelocity != Vector2.zero)
            {
                PlayerDirection = rB.linearVelocity.normalized;
            }
            MovePlayer(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        private void MovePlayer(float horizontalInput, float verticalInput)
        {
            rB.linearVelocity = new Vector2(horizontalInput, verticalInput).normalized * playerSpeed;
        }

        private void OnEnable()
        {
            EventBus<onExitOverworldEventData>.OnEventPublished += GivePosition;
            EventBus<OnEnterOverworldEventData>.OnEventPublished += AssignPosition;
        }

        private void OnDisable()
        {
            EventBus<onExitOverworldEventData>.OnEventPublished -= GivePosition;
            EventBus<OnEnterOverworldEventData>.OnEventPublished -= AssignPosition;
        }

        public void AssignPosition(EventData eventData)
        {
            OnEnterOverworldEventData loadOverWorldEventData = (OnEnterOverworldEventData)eventData;

            transform.position = GameManagerOld.Instance.overWorldPosition;
        }

        public void GivePosition(EventData eventData)
        {
            onExitOverworldEventData loadBattleEventData = (onExitOverworldEventData)eventData;

            GameManagerOld.Instance.overWorldPosition = transform.position ;
        }
    }
}
