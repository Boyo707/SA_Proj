using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class OldEnemyData : ScriptableObject
{
    public string enemyName;

    public int XP;
    public int money;
    public int damage;
    public int maxHP;

    [Range(1, 100)] public int spawnChance;

    public AnimatorController animatorController; // animation for idle, attack, hurt and defeat
    //I need something for the timing of attacks
    //maybe a prefab that contains an animation and script for the action timings

    public SpriteLibraryAsset overworldSprites;
    public SpriteLibraryAsset battleSprites;

    public EnemyOld CreateEnemy()
    {
        return new EnemyOld(enemyName, XP, money, damage, maxHP, spawnChance, animatorController, overworldSprites, battleSprites);
    }
}

[Serializable]
public class EnemyOld
{
    public string Name => name;
    private string name;
    public int XP => xp;
    private int xp;
    public int Money => money;
    private int money;
    public int Damage => damage;
    private int damage;
    public int MaxHP => maxHP;
    private int maxHP;
    public int currentHP;
    public int SpawnChance => spawnChance;
    private int spawnChance;

    public Vector3 currentPosition;
    public bool isAlive = true;
    public bool isVisible = true;

    public AnimatorController AnimatorController => animatorController;
    private AnimatorController animatorController;

    public SpriteLibraryAsset OverworldSprites => overworldSprites;
    private SpriteLibraryAsset overworldSprites;

    public SpriteLibraryAsset BattleSprites => battleSprites;
    private SpriteLibraryAsset battleSprites;

    public EnemyOld(string pName, int pXP, int pMoney, int pDamage, int pMaxHP, int pSpawnChance,
        AnimatorController pAnimatorController, SpriteLibraryAsset pOverworldSprite, SpriteLibraryAsset pBattleSprites)
    {
        name = pName;
        xp = pXP;
        money = pMoney;
        damage = pDamage;
        maxHP = pMaxHP;
        currentHP = pMaxHP;
        spawnChance = pSpawnChance;
        animatorController = pAnimatorController;
        overworldSprites = pOverworldSprite;
        battleSprites = pBattleSprites;
    }

    public EnemyOld Clone()
    {
        EnemyOld other = new EnemyOld(name, xp, money, damage, maxHP, 
            spawnChance, animatorController, overworldSprites, battleSprites); 

        return other;
    }
}
