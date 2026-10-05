using PP.Player;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{

    private Transform[] patrolPositions;
    private PlayerDetection playerDetection;
    private PathFinder pathFinder;
    private AnimationHandler animationHandler;
    private float moveSpeed;

    private int positionIndex = 0;

    private int randomNum;

    public PatrolState(Transform[] pPatrolPositions, PlayerDetection pPlayerDetection, PathFinder pPathFinder, AnimationHandler pAnimationHandler, float pMoveSpeed)
    {
        patrolPositions = pPatrolPositions;
        playerDetection = pPlayerDetection;
        pathFinder = pPathFinder;
        animationHandler = pAnimationHandler;
        moveSpeed = pMoveSpeed;

        positionIndex = pPatrolPositions.Length;
        
        stateName = "Patrol";
    }

    public override void Enter()
    {
        base.Enter();

        randomNum = Random.Range(0, 2); 

        if (positionIndex >= patrolPositions.Length)
        {
            positionIndex = 0;
        }
        pathFinder.MoveSpeed = moveSpeed;
    }

    public override void Step()
    {
        pathFinder.GoToDestination(patrolPositions[positionIndex]);
        animationHandler.AnimateWalkingDirection(pathFinder.MoveDirection());
        playerDetection.SetDetectionDir(pathFinder.MoveDirection().normalized);
        base.Step();
    }

    public override void Exit()
    {
        base.Exit();
        positionIndex++;
        pathFinder.StopPathFinding();
        animationHandler.AnimateFacingDirection();
        playerDetection.SetDetectionDir(animationHandler.CurrentFaceDirection);
    }

    private bool ReachedDestination()
    {
        return pathFinder.HasReachedDestination(patrolPositions[positionIndex].position);

    }

    public bool NextStateIsIdle()
    {
        return ReachedDestination() && randomNum == 0;
    }

    public bool NextStateIsSurverying()
    {
        return ReachedDestination() && randomNum == 1;
    }

    public bool IsInView()
    {
        return playerDetection.InView();
    }
}
