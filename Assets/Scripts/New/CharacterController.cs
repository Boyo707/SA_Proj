using UnityEngine;
namespace PP.Player
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private CharacterMovement movement;
        [SerializeField] private CharacterInteraction interaction;
        [SerializeField] private AnimationHandler animationhandler;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(interaction.Key))
            {
                interaction.Interact(movement.FaceDirection);
            }
        }

        private void FixedUpdate()
        {
            movement.MoveCharacter((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"));
            animationhandler.AnimateWalkingDirection(movement.MoveDirection, movement.FaceDirection);
        }
    }
}
