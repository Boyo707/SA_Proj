using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Dungeon/Actor/Enemy")]

public class EnemyData : ActorData
{
    public int moneyDropAmount;
    public int xpDropAmount;
    public Enemy CreateEnemy()
    {
        return new Enemy(this);
    }
}

[Serializable]
public class Enemy : Actor
{
    private int moneyDrop;
    public int MoneyDrop => moneyDrop;

    private int xpDrop;
    public int XpDrop => xpDrop;
    public Enemy(EnemyData data)
    {
        name = data.name;
        baseStats = data.baseStats.Clone();
        moneyDrop = data.moneyDropAmount;
        xpDrop = data.xpDropAmount;
    }
}