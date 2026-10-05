using PP.Player;
using UnityEngine;

/// <summary>
/// A state to idle for a period of time(idleTime).
/// </summary>
public class IdleState:State
{
    private float idleTime;
    private float startTime;

    private PlayerDetection playerDetection;

    public IdleState(float pIdleTime, PlayerDetection pPlayerDetection)
    {
        idleTime = pIdleTime;
        playerDetection = pPlayerDetection;

        stateName = "Idle";
    }

    public override void Enter()
    {
        base.Enter();
        startTime = Time.time;
    }

    //--------Helper Conditions for Transitions to decide whether to transition to the next state

    public bool IdleTimeOver()
    {
        return Time.time > startTime + idleTime;
    }

    public bool IsInView()
    {
        return playerDetection.InView();
    }
}
