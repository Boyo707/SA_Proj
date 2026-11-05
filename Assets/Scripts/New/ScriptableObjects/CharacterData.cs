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

   

    public Character(CharacterData data)
    {
        name = data.name;
        baseStats = data.baseStats.Clone();
        characterStats = data.characterStats.Clone();
        levelCurve = data.levelCurve;
        normalAttack = data.normalAttack;
        duoAttack = data.duoAttack;
        defaultBattleAnimation = data.defaultBattleAnimation;
    }

    public Attack GetNormalAttack()
    {
        return normalAttack.GetAttack();
    }

    public Attack GetDuoAttack()
    {
        return duoAttack.GetAttack();
    }

    public void DepleteSpecial(int special)
    {
        characterStats.currentSpecial = Mathf.Clamp(characterStats.currentSpecial - special, 0, characterStats.maxSpecial);
    }

    public void IncreaseSpecial(int special)
    {
        characterStats.currentSpecial = Mathf.Clamp(characterStats.currentSpecial + special, 0, characterStats.maxSpecial);
    }
}
