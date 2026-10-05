using PP.Player;
using Unity.VisualScripting;
using UnityEngine;

public class SurveyingState : State
{

    private AnimationHandler animationHandler;
    private PlayerDetection playerDetection;
    private float surveyTime;
    private float rotateTime;

    private float startTime;
    private int rotateBleh = 0;

    private Vector3 faceDirection = Vector3.left;
    public SurveyingState(AnimationHandler pAnimationHandler, PlayerDetection pPlayerDetection, float pSurveyTime)
    {
        animationHandler = pAnimationHandler;
        playerDetection = pPlayerDetection;
        surveyTime = pSurveyTime;

        stateName = "Surveying";
    }

    public override void Enter()
    {
        base.Enter();

        startTime = Time.time;
        rotateTime = surveyTime / 5;

        faceDirection = animationHandler.CurrentFaceDirection;
        playerDetection.SetDetectionDir(faceDirection);
        Debug.Log("Enter");
    }

    //--------Helper Conditions for Transitions to decide whether to transition to the next state

    public override void Step()
    {
        float currentTime = Time.time - startTime;
        float bleh = Mathf.Floor(currentTime / rotateTime);
        if(bleh > rotateBleh)
        {
            rotateBleh = (int)bleh;
            faceDirection = new Vector3(faceDirection.y, -faceDirection.x, 0);
            animationHandler.AnimateFacingDirection(faceDirection);
            playerDetection.SetDetectionDir(faceDirection);
        }

        //walk to to location, reach location, switch target, repeat.

        base.Step();
    }

    public override void Exit()
    {
        base.Exit();

        rotateBleh = 0;
    }

    public bool SurveyTimeOver()
    {
        return Time.time > startTime + surveyTime;
    }

    public bool IsInView()
    {
        return playerDetection.InView();
    }
}
