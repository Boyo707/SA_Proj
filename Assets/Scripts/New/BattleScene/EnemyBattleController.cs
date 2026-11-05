using UnityEngine;

public class EnemyBattleController : MonoBehaviour
{
    [SerializeField] private EnemyData currentCharacter;
    [SerializeField] private EnemyAttack attack;
    [SerializeField] private Animator animator;

    //Choose an attack.
    //perform an attack.
    //the momment where the player usually attacks is now a block.
    //blocking at the right moment blocks the attack.
    //the attack of the enemy continues no matter what.
}
