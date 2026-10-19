using System;
using UnityEngine;

public class ActorData : ScriptableObject
{
    public string characterName;
    public ActorBaseStats baseStats;
}

[Serializable]
public class ActorBaseStats
{
    public int maxHealth;
    public int currentHP;
    public int damage;
}

public class Actor
{
    protected string name;
    public string Name => name;

    public ActorBaseStats baseStats;
}