using System;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
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

    //private Queue<Actor> order = new();
    [SerializeField]private List<Actor> turnOrder = new();

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

        //if i ever wanna have more then one enemy, instantiate and then get comp

        enemyData = enemy;
        enemyData.transform = enemyObj.transform;
        enemyData.transform.GetComponent<ActorIdentity>().AssignActor(enemy);

       // enemyData.animator = enemyObj.GetComponent<Animator>();

        //order.Enqueue(maleData);
        //order.Enqueue(femaleData);
        //order.Enqueue(enemyData);

        turnOrder.Add(maleData);
        turnOrder.Add(femaleData);
        turnOrder.Add(enemyData);

        currentActor = turnOrder[0];
        BattleObserver.onTurnStart?.Invoke(currentActor);
        //NextTurn();
    }
    public void NextTurn()
    {
        Actor previousActor = turnOrder[0]; 
        turnOrder.Remove(previousActor);

        currentActor = turnOrder[0];
        BattleObserver.onTurnStart?.Invoke(currentActor);

        turnOrder.Add(previousActor);
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

    public void ActorDied(Actor deadActor)
    {
        turnOrder.Remove(deadActor);                //Remove actor from queue. - Or skip actor once its his turn...
        BattleObserver.onDeath?.Invoke(deadActor);  //play death VFX and animation        

        //Check if the dead actor is an enemy.
        if (deadActor is Enemy enemyData)
        {
            Debug.Log("Man im dead");
            BattleObserver.onWin?.Invoke();//start the onWin event.

            //The gamemanager or some other script will remove the dead enemy from the overworld
        }

        //Check if the dead actor is a player
        if (deadActor is Character characterData)
        {
            if (hasAllPlayersDied())                //check if there are any players left alive.
            {
                BattleObserver.onDefeat?.Invoke();  //start the onDefeat event.
                //The gamemanager or some other script will put you back somewhere,
                //maybe the player gets kicked back to the starting point. with a health loss
            }
        }
    }

    public void SetCurrentActor(Actor newCurrentActor)
    {
        //currentActor = newCurrentActor;
    }

    public void SetTargetActor(Actor newTargetActor)
    {
        //targetActor = newTargetActor;
    }

    private bool hasAllPlayersDied()
    {
        for (int i = 0; i < turnOrder.Count; i++)
        {
            if (turnOrder[i] is Character characterData)
            {
                return false;
            }
        }
        return true;
    }
}
