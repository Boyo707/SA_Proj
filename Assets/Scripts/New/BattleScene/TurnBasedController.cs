using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedController : MonoBehaviour
{
    //eventually all of these will be picked from the gamemanager
    [SerializeField] private Transform maleTrans;
    [SerializeField] private Transform femaleTrans;
    [SerializeField] private Transform enemyTrans;

    private Queue<TurnInfo> order = new();


    TurnInfo currentActor;
    public struct TurnInfo
    {
        public Transform transform;
        public Actor actor;

        public TurnInfo(Actor actorData, Transform trans)
        {
            transform = trans;
            actor = actorData;
        }
    }

    IEnumerator Start()
    {
        order.Enqueue(new TurnInfo(GameManager.Instance.maleData, maleTrans));
        order.Enqueue(new TurnInfo(GameManager.Instance.femaleData, femaleTrans));
        order.Enqueue(new TurnInfo(GameManager.Instance.battleEnemyData, enemyTrans));

        yield return new WaitForEndOfFrame();

        NextCharacter();
    }

    private void OnEnable()
    {
        BattleObserver.onTurnEnd += NextCharacter;
    }

    private void OnDisable()
    {
        BattleObserver.onTurnEnd -= NextCharacter;
    }

    public void NextCharacter()
    {
        currentActor = order.Dequeue();
        BattleObserver.onTurnStart?.Invoke(currentActor.actor, currentActor.transform);
        order.Enqueue(currentActor);
    }

    public void OnWin()
    {

    }
    
}
