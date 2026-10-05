using UnityEngine;
using PP.Player;
using System.Collections.Generic;

/// <summary>
/// A finite state machine that controls the enemy behaviour
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float idleTime = 2f;
    [SerializeField] private float surveryTime = 6f;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chaseSpeed;

    [SerializeField] private PlayerDetection playerDetection;
    [SerializeField] private AnimationHandler animationHandler;
    [SerializeField] private PathFinder pathFinding;
    [SerializeField] private GameObject patrolPositionsParent;

    [SerializeReference]
    private State currentState;

    private IdleState idleState;
    private PatrolState patrolState;
    private SpottingState spottingState;
    private SurveyingState surveyingState;
    private ChaseState chaseState;
    
    
    List<Transform> patrolPositions = new();

    void Start()
    {
        patrolPositionsParent.transform.parent = null;
        patrolPositions.AddRange(patrolPositionsParent.GetComponentsInChildren<Transform>());
        patrolPositions.RemoveAt(0);
        

        idleState = new IdleState(idleTime, playerDetection);
        patrolState = new PatrolState(patrolPositions.ToArray(), playerDetection, pathFinding, animationHandler, patrolSpeed);
        spottingState = new SpottingState(transform, target, animationHandler, playerDetection);
        surveyingState = new SurveyingState(animationHandler, playerDetection, surveryTime);
        chaseState = new ChaseState(target, transform, playerDetection, pathFinding, animationHandler, chaseSpeed);

        idleState.transitions.Add(new Transition(idleState.IdleTimeOver, patrolState));
        idleState.transitions.Add(new Transition(idleState.IsInView, spottingState));

        patrolState.transitions.Add(new Transition(patrolState.NextStateIsIdle, idleState));
        patrolState.transitions.Add(new Transition(patrolState.NextStateIsSurverying, surveyingState));
        patrolState.transitions.Add(new Transition(patrolState.IsInView, spottingState));

        spottingState.transitions.Add(new Transition(spottingState.IsOutOfView, surveyingState));
        spottingState.transitions.Add(new Transition(spottingState.HasSpottedPlayer, chaseState));

        surveyingState.transitions.Add(new Transition(surveyingState.SurveyTimeOver, patrolState));
        surveyingState.transitions.Add(new Transition(surveyingState.IsInView, spottingState));

        chaseState.transitions.Add(new Transition(chaseState.IsOutOfRange, surveyingState));


        //idleState.onEnter += () => { animator.SetBool("Idle", true); };
        //idleState.onExit += () => { animator.SetBool("Idle", false); };
        // moveToState.onEnter += () => { animator.SetBool("Chase", true); };
        //moveToState.onExit += () => { animator.SetBool("Chase", false); };
        //alignToState.onEnter += () => { animator.SetBool("Aim", true); };
        //alignToState.onExit += () => { animator.SetBool("Aim", false); };

        //Default state is idleState.
        currentState = idleState;
    }

    //In each update, step the current state to let it process, then check if a condition is met
    //for one of the current state's transitions, if so transition to next state.
    void Update()
    {
        currentState.Step();
        if (currentState.NextState() != null)
        {
            //Cache the next state, because after currentState.Exit, calling
            //currentState.NextState again might return null because of change
            //of context.
            State nextState = currentState.NextState();
            currentState.Exit();
            currentState = nextState;
            //stateText.text = currentState.stateName;
            currentState.Enter();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //start transition to battle
    }
}
