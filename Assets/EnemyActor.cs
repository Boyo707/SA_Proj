using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class EnemyActor :  OldBattleObserver
{
    [SerializeField] private List<GameObject> players = new List<GameObject>();
    [SerializeField] private GameObject sparkObject;
    [SerializeField] private Animator animator;

    [SerializeField] private List<RuntimeAnimatorController> animations;
    //Idle = 0
    //Attack = 1

    private Enemy enemyData;
    public Enemy EnemyData => enemyData;

    private void Start()
    {
        enemyData = GameManagerOld.Instance.currentEnemy;
    }


    public override void StartTurn()
    {

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetComponent<PlayerActor>().isDead)
            {
                players.Remove(players[i]);
            }
            Debug.Log(players.Count);

        }

        //choose player target randomly
        //range from 0 - 2 or else it wont give 1 as result
        int playerTargetIndex = Random.Range(0, players.Count);
        bool secondaryAttack = Random.Range(0, 2) == 0;

        gameObject.AddComponent<ActorAttack>().
            InitializeAttack(sparkObject, gameObject, players[playerTargetIndex].transform, secondaryAttack);
    }

    public override void EndTurn()
    {    
        OldBattleController.Instance.NextTurn();
    }

    protected override void GetEnemyHit(CharacterOld charData, int damage) 
    {
        enemyData.currentHP -= damage;
        Debug.Log(enemyData.currentHP);

        if (enemyData.currentHP <= 0)
        {
            enemyData.currentHP = 0;
            Debug.Log("IM DEAD");
            OldBattleController.Instance.GetEnemyDeath(charData, damage);
        }
    }
    protected override void GetEnemyDeath(CharacterOld pCharData, int damage)
    {
        if (TryGetComponent<ActorAttack>(out ActorAttack attack))
        {
            attack.StopAttack();
        }

    }


}
