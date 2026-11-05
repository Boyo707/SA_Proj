using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    [SerializeField] GameObject maleObj;
    [SerializeField] GameObject femaleObj;
    [SerializeField] GameObject enemyObj;

    Character maleData;
    Character femaleData;
    Enemy enemyData;

    Actor currentActor;

    private Queue<Actor> order = new();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        { 
            Destroy(gameObject);
        }
    }

    public void InitializeBattle(Character male, Character female, Enemy enemy)
    {
        maleData = male;
        maleData.transform = maleObj.transform;
        maleData.transform.GetComponent<ActorIdentity>().AssignActor(male);
        //maleData.animator = maleObj.GetComponent<Animator>();

        femaleData = female;
        femaleData.transform = femaleObj.transform;
        femaleData.transform.GetComponent<ActorIdentity>().AssignActor(female);

        //femaleData.animator = femaleObj.GetComponent<Animator>();

        enemyData = enemy;
        enemyData.transform = enemyObj.transform;
        enemyData.transform.GetComponent<ActorIdentity>().AssignActor(enemy);

       // enemyData.animator = enemyObj.GetComponent<Animator>();

        order.Enqueue(maleData);
        order.Enqueue(femaleData);
        order.Enqueue(enemyData);

        NextTurn();
    }
    public void NextTurn()
    {
        currentActor = order.Dequeue();
        BattleObserver.onTurnStart?.Invoke(currentActor);
        order.Enqueue(currentActor);
    }

    public void EndTurn()
    {
        BattleObserver.onTurnEnd?.Invoke(currentActor);
        NextTurn();
    }

    public void Attack(Attack attack)
    {
        BattleObserver.onAttack?.Invoke(currentActor, attack, enemyData);
    }
    public void Attack(Attack attack, Actor targetActor)
    {
        BattleObserver.onAttack?.Invoke(currentActor, attack, targetActor);
    }

    public void DamageActor(Actor targetActor, int damage)
    {
        BattleObserver.onHit?.Invoke(targetActor, damage);
    }

    public void HealActor(Actor targetActor, int heal)
    {
        BattleObserver.onHeal?.Invoke(targetActor, heal);
    }

    public void SetCurrentActor(Actor newCurrentActor)
    {
        //currentActor = newCurrentActor;
    }

    public void SetTargetActor(Actor newTargetActor)
    {
        //targetActor = newTargetActor;
    }
}
