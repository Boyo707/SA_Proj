using System;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Dungeon/Actor/Character")]
public class CharacterData : ActorData
{
    public CharacterStats characterStats;
    public AnimationCurve levelCurve;
    public AttackData normalAttack;
    public AttackData duoAttack;
    public AnimatorController defaultAnimation;

    public Character CreateCharacter()
    {
        return new Character(this);
    }
}

[Serializable]
public class CharacterStats
{
    public int maxSpecial;
    public int currentSpecial;
    public int experiencePoints;
    public int level = 1;

    public CharacterStats(int pMaxSpecial, int pCurrentSpecial, int pXP, int pLevel)
    {
        maxSpecial = pMaxSpecial;
        currentSpecial = pCurrentSpecial;
        experiencePoints = pXP;
        level = pLevel;
    }

    public CharacterStats Clone()
    {
        CharacterStats other = new CharacterStats(maxSpecial, currentSpecial, experiencePoints, level);

        return other;
    }
}

[Serializable]
public class Character: Actor
{
    public CharacterStats characterStats;

    private AnimationCurve levelCurve;
    public AnimationCurve LevelCurve => levelCurve;

    private AttackData normalAttack;
    public AttackData NormalAttack => normalAttack;
    private AttackData duoAttack;
    public AttackData DuoAttack => duoAttack;

    private AnimatorController defaultAnimation;
    public AnimatorController DefaultAnimation => defaultAnimation;

    public Character(CharacterData data)
    {
        name = data.name;
        baseStats = data.baseStats;
        characterStats = data.characterStats;
        levelCurve = data.levelCurve;
        normalAttack = data.normalAttack;
        duoAttack = data.duoAttack;
        defaultAnimation = data.defaultAnimation;
    }

    public Attack GetNormalAttack()
    {
        return normalAttack.GetAttack();
    }

    public Attack GetDuoAttack()
    {
        return duoAttack.GetAttack();
    }
}
