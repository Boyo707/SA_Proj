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

public class Enemy : Actor
{
    private int moneyDrop;
    public int MoneyDrop => moneyDrop;

    private int xpDrop;
    public int XpDrop => xpDrop;
    public Enemy(EnemyData data)
    {
        name = data.name;
        baseStats = data.baseStats;
        moneyDrop = data.moneyDropAmount;
        xpDrop = data.xpDropAmount;
    }
}