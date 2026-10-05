using PP.InventorySystem;
using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]

public class CharacterDataOld : ScriptableObject
{
    public string characterName;
    public CharacterStatsOld characterStats;
    public ItemData currentWeapon;
    public SpriteLibraryAsset overworldSprites;
    public SpriteLibraryAsset battleSprites;
    public AnimatorController characterAnimatorController;

    public AnimationCurve levelCurve;

    public CharacterOld CreateCharacter()
    {
        return new CharacterOld(characterName, characterStats, currentWeapon, overworldSprites, 
            battleSprites, characterAnimatorController, levelCurve);
    }
}

[Serializable]
public class CharacterStatsOld
{
    public int maxHealth;
    [HideInInspector] public int currentHP;
    public int damage;
    public int maxSpecial;
    public int experiencePoints;
    public int level = 1;

    public CharacterStatsOld(int pMaxHealth, int pCurrentHp, int pDamage, int pMaxSpecial, int pXP, int pLevel)
    {
        maxHealth = pMaxHealth;
        currentHP = pCurrentHp;
        damage = pDamage;
        maxSpecial = pMaxSpecial;
        experiencePoints = pXP;
        level= pLevel;
    }

    public CharacterStatsOld Clone()
    {
        CharacterStatsOld other = new CharacterStatsOld(maxHealth, currentHP, damage, 
            maxSpecial, experiencePoints, level);

        return other;
    }
}

[Serializable]
public class CharacterOld
{
    private string characterName;
    public string Name => characterName;

    private CharacterStatsOld characterStats;
    public CharacterStatsOld CharacterStats => characterStats;
    public ItemData currentWeapon;

    private SpriteLibraryAsset overworldSprites;
    public SpriteLibraryAsset OverworldSprites => overworldSprites;

    private SpriteLibraryAsset battleSprites;
    public SpriteLibraryAsset BattleSprites => battleSprites;

    private AnimatorController characterAnimatorController;
    public AnimatorController AnimatorController => characterAnimatorController;

    private AnimationCurve levelCurve;
    public AnimationCurve LevelCurve => levelCurve;

    public CharacterOld(string pCharacterName, CharacterStatsOld pStats, ItemData pWeapon, SpriteLibraryAsset pOverworldSprites,
        SpriteLibraryAsset pBattleSprites, AnimatorController pAnimator, AnimationCurve pLevelCurve)
    {
        characterName = pCharacterName;
        characterStats = pStats.Clone();
        pStats.currentHP = pStats.maxHealth;
        currentWeapon = pWeapon;
        overworldSprites = pOverworldSprites;
        battleSprites = pBattleSprites;
        characterAnimatorController = pAnimator;
        levelCurve = pLevelCurve;
    }
}
