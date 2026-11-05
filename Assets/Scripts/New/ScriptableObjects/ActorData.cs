using System;
using UnityEngine;
using UnityEditor.Animations;

public class ActorData : ScriptableObject
{
    public string characterName;
    public ActorBaseStats baseStats;
    public AnimatorController defaultBattleAnimation;
}

[Serializable]
public class ActorBaseStats
{
    public int maxHealth;
    public int currentHP;
    public int damage;

    public ActorBaseStats(int maxHealth, int currentHP, int damage)
    {
        this.maxHealth = maxHealth;
        this.currentHP = currentHP;
        this.damage = damage;
    }

    public ActorBaseStats Clone()
    {
        ActorBaseStats other = new ActorBaseStats(maxHealth, currentHP, damage);

        return other;
    }
}

[Serializable]
public class Actor
{
    [SerializeField] protected string name;
    public string Name => name;

    public ActorBaseStats baseStats;

    public Transform transform;

    protected AnimatorController defaultBattleAnimation;
    public AnimatorController DefaultBattleAnimation => defaultBattleAnimation;
}