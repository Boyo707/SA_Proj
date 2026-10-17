using System;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Dungeon/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public CharacterStats baseStats;
    public AnimationCurve levelCurve;
    public AttackData attackData;
    public AnimatorController defaultAnimation;

    public Character CreateCharacter()
    {
        return new Character(this);
    }
}

[Serializable]
public class CharacterStats
{
    public int maxHealth;
    public int currentHP;
    public int damage;
    public int maxSpecial;
    public int currentSpecial;
    public int experiencePoints;
    public int level = 1;

    public CharacterStats(int pMaxHealth, int pCurrentHp, int pDamage, int pMaxSpecial, int pCurrentSpecial, int pXP, int pLevel)
    {
        maxHealth = pMaxHealth;
        currentHP = pCurrentHp;
        damage = pDamage;
        maxSpecial = pMaxSpecial;
        currentSpecial = pCurrentSpecial;
        experiencePoints = pXP;
        level = pLevel;
    }

    public CharacterStats Clone()
    {
        CharacterStats other = new CharacterStats(maxHealth, currentHP, damage,
            maxSpecial, currentSpecial, experiencePoints, level);

        return other;
    }
}

[Serializable]
public class Character
{
    private string name;
    public string Name => name;

    public CharacterStats stats;

    private AnimationCurve levelCurve;
    public AnimationCurve LevelCurve => levelCurve;

    private AttackData attackData;
    public AttackData AttackData => attackData;

    private AnimatorController defaultAnimation;
    public AnimatorController DefaultAnimation => defaultAnimation;

    public Character(CharacterData data)
    {
        name = data.name;
        stats = data.baseStats;
        levelCurve = data.levelCurve;
        attackData = data.attackData;
        defaultAnimation = data.defaultAnimation;
    }
}
