using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteraction
{
    [SerializeField] private GameObject roomShadow;
    public void OnInteraction()
    {
        roomShadow.SetActive(false);
        gameObject.SetActive(false);
    }
}
