using System;
using UnityEngine;
[CreateAssetMenu(fileName = "CharacterData", menuName = "Dungeon/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public CharacterStats baseStats;
    public AnimationCurve levelCurve;
    public AttackData attackData;

    public Character CreateCharacter()
    {
        return new Character(characterName, baseStats, levelCurve, attackData);
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
    public string name;
    public CharacterStats stats;
    public AnimationCurve levelCurve;
    public AttackData attackData;

    public Character(string pName, CharacterStats pStats, AnimationCurve pLevelCurve, AttackData pAttackData)
    {
        name = pName;
        stats = pStats;
        levelCurve = pLevelCurve;
        attackData = pAttackData;
    }
}
