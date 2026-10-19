using System;
using UnityEngine;

public class BattleObserver : MonoBehaviour
{
    public static Action<int> onHit; //needs to get receiver info.
    public static Action onHeal;

    public static Action<Actor, Attack> onAttack;
    public static Action onTurnEnd;
    public static Action<Actor, Transform> onTurnStart;

    public static Action onWin;
    public static Action onDefeat;

    public static Action onAttackFail;
}
