using UnityEngine;
public interface IInteraction
{
    void OnInteraction();
}
public class CharacterInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange = 2;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private KeyCode interactionKey;

    public KeyCode Key => interactionKey;

    public void Interact(Vector3 faceDirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, faceDirection, interactionRange, interactionLayer);
        if (hit)
        {
            hit.transform.GetComponent<IInteraction>().OnInteraction();
        }
    }
}
