using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public enum AttackType
{
    normal,
    duo
}

[CreateAssetMenu(fileName = "NormalAttack", menuName = "Scriptable Objects/Attack/NormalAttack")]
public class AttackData : ScriptableObject
{

    public string attackName;
    public AttackType attacktype;
    public AnimatorController attackAnimController;
    public List<Vector2Int> damageList;
}

/*public class NormalAttackData : AttackData
{

}

[CreateAssetMenu(fileName = "SpecialAttack", menuName = "Scriptable Objects/Attack/SpecialAttack")]
public class SpecialAttackData : AttackData
{
    public int specialCost;
}

[CreateAssetMenu(fileName = "EnemyAttack", menuName = "Scriptable Objects/Attack/EnemyAttack")]
public class EnemyAttackData : AttackData
{

}*/