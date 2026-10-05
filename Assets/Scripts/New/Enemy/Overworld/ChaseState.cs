using PP.Player;
using UnityEngine;

public class ChaseState : State
{
    private Transform target;
    private Transform self;
    private PlayerDetection playerDetection;
    private PathFinder pathFinder;
    private AnimationHandler animationHandler;
    private float moveSpeed;

    public ChaseState(Transform pTarget, Transform pSelf, PlayerDetection pPlayerDetection, PathFinder pPathFinder, AnimationHandler pAnimationHandler, float pMoveSpeed)
    {
        target = pTarget;
        self = pSelf;
        playerDetection = pPlayerDetection;
        pathFinder = pPathFinder;
        animationHandler = pAnimationHandler;
        moveSpeed = pMoveSpeed;

        stateName = "Patrol";
    }

    public override void Enter()
    {
        base.Enter();

        pathFinder.MoveSpeed = moveSpeed;
    }

    //--------Helper Conditions for Transitions to decide whether to transition to the next state

    public override void Step()
    {
        //walk to to location, reach location, switch target, repeat.
        pathFinder.GoToDestination(target);
        animationHandler.AnimateWalkingDirection(target.position - self.position);
        playerDetection.SetDetectionDir(pathFinder.MoveDirection().normalized);
        base.Step();
    }

    public override void Exit()
    {
        base.Exit();
        pathFinder.StopPathFinding();
        animationHandler.AnimateFacingDirection();
        playerDetection.SetDetectionDir(animationHandler.CurrentFaceDirection);
    }

    public bool IsOutOfRange()
    {
        return !playerDetection.InRange();
    }
}
