using PP.Player;
using UnityEngine;

public class SpottingState : State
{
    private Transform self;
    private Transform target;
    private AnimationHandler animHandler;
    private PlayerDetection playerDetection;

    public SpottingState(Transform pSelf, Transform pTarget, AnimationHandler pAnimationHandler, PlayerDetection pPlayerDetection)
    {
        self = pSelf;
        target = pTarget;
        animHandler = pAnimationHandler;
        playerDetection = pPlayerDetection;

        stateName = "Spotting";
    }

    public override void Enter()
    {
        base.Enter();
    }

    //--------Helper Conditions for Transitions to decide whether to transition to the next state

    public override void Step()
    {
        Vector3 direction = target.position - self.position;
        animHandler.AnimateFacingDirection(direction);
        playerDetection.SetDetectionDir(direction);
        base.Step();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public bool IsOutOfView()
    {
        return !playerDetection.InView();
    }

    public bool HasSpottedPlayer()
    {
        return playerDetection.IsDetected();
    }
}
