using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    protected string attackName;
    protected AnimatorController attackAnimController;
    protected List<int> damageList;
}
