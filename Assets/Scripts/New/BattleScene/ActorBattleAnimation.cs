using UnityEngine;

[RequireComponent(typeof(ActorIdentity))]
[RequireComponent(typeof(Animator))]
public class ActorBattleAnimation : MonoBehaviour
{

    private Actor actor;
    private Animator animator;
    private void Start()
    {
        actor = GetComponent<ActorIdentity>().MyActor;
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        BattleObserver.onTurnStart += CharacterSelected;
        BattleObserver.onTurnEnd += ResetCharacter;
    }
    private void OnDisable()
    {
        BattleObserver.onTurnStart -= CharacterSelected;
        BattleObserver.onTurnEnd -= ResetCharacter;
    }
    public void CharacterSelected(Actor actorData)
    {
        if (actor == actorData)
        {
            if (actorData is Character charData)
            {
                animator.SetBool("IsSelecting", true);
            }
        }
    }

    public void ResetCharacter(Actor actor)
    {
        animator.runtimeAnimatorController = actor.DefaultBattleAnimation;
        animator.SetBool("IsSelecting", false);
    }

    private void ReceiveDamage()
    {

    }
}
