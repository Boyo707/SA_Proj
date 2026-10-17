using System;
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

    [Header("Attack Properties")]
    public float attackDistance = 3;
    public List<DamageProperties> damageList;
    public float reactionTime = 0.2f;

    [Header("Aproach Properties")]
    public AnimationClip aproachClip;
    public float aproachTime = 0.7f;
    public AnimationCurve aproachCurve;

    [Header("Return Properties")]
    public List<AnimationClip> returnClips;
    public float returnTime = 0.7f;
    public AnimationCurve returnCurve;


    public Attack GetAttack()
    {
        return new Attack(this);
    }
}

[Serializable]
public struct DamageProperties
{
    public AnimationClip attackClip;
    public int minDamage;
    public int maxDamage;
}

[Serializable]
public class Attack
{
    private string name;
    public string Name => name;

    private AttackType type;
    public AttackType Type => type;

    private AnimatorController animationController;
    public AnimatorController AnimationController => animationController;

    private float attackDistance = 3;
    public float AttackDistance => attackDistance;

    private List<DamageProperties> damageList;
    public List<DamageProperties> DamageList => damageList;

    private float reactionTime = 0.2f;
    public float ReactionTime => reactionTime;

    private AnimationClip aproachClip;
    public AnimationClip AproachClip => aproachClip;

    private float aproachTime;
    public float AproachTime => aproachTime;

    private AnimationCurve aproachCurve;
    public AnimationCurve AproachCurve => aproachCurve;

    private List<AnimationClip> returnClips = new();
    public List<AnimationClip> ReturnClips => returnClips;

    private float returnTime;
    public float ReturnTime => returnTime;

    private AnimationCurve returnCurve;
    public AnimationCurve ReturnCurve => returnCurve;

    public Attack(AttackData data)
    {
        name = data.attackName;
        type = data.attacktype;
        animationController = data.attackAnimController;

        attackDistance = data.attackDistance;
        damageList = data.damageList;
        reactionTime = data.reactionTime;

        aproachClip = data.aproachClip;
        aproachTime = data.aproachTime;
        aproachCurve = data.aproachCurve;

        returnClips = data.returnClips;
        returnTime = data.returnTime;
        returnCurve = data.returnCurve;
    }

    public bool IsAproachClip(AnimationClip clip)
    {
        return aproachClip == clip;
    }
    public bool IsReturnClip(AnimationClip clip)
    {
        return returnClips.Count != 0 && returnClips.Contains(clip);
    }

    public bool HasAttack(AnimationClip clip)
    {
        foreach(DamageProperties c in damageList)
        {
            if(c.attackClip == clip)
            {
                return true;
            }
        }
        return false;
    }
}