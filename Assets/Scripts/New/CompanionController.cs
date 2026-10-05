using PP.Player;
using UnityEditor.Rendering;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    [SerializeField] private CompanionFollow follow;
    [SerializeField] private AnimationHandler animationHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        follow.MoveCompanion();
    }

    private void FixedUpdate()
    {
        animationHandler.AnimateWalkingDirection(follow.MoveDirection, follow.FaceDirection);
    }
}
