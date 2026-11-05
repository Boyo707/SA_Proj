using System;
using UnityEngine;

public class BattleObserver : MonoBehaviour
{
    public static Action onBattleStart;

    public static Action<Actor> onTurnStart;
    public static Action<Actor> onTurnEnd;

    public static Action<Actor, Attack, Actor> onAttack;
    public static Action onAttackFail;

    public static Action<Actor, int> onHit; //needs to get receiver info.
    public static Action<Actor, int> onHeal;

    public static Action onWin;
    public static Action onDefeat;
}
