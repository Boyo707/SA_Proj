using UnityEngine;
using System;
using UnityEditor.Rendering;

public interface IInteractable
{
    void OnInteraction();
}

namespace PP.Player {
    public class PlayerInteractionOld : MonoBehaviour
    {
        [SerializeField] private float interactionRange;
        [SerializeField] private LayerMask interactionLayer;
        [SerializeField] private PlayerMovementOld playerMovement;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, playerMovement.PlayerDirection, interactionRange, interactionLayer); 
                if (hit.collider != null)
                {
                    hit.transform.GetComponent<IInteractable>().OnInteraction();
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (playerMovement != null && playerMovement.PlayerDirection != Vector3.zero)
            {
                Gizmos.DrawRay(transform.position, playerMovement.PlayerDirection * interactionRange);
            }
            else
            {
                Gizmos.DrawRay(transform.position, Vector3.right * interactionRange);
            }
        }
    }
}
