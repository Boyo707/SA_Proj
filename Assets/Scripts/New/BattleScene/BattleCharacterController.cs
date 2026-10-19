using UnityEngine;

public class BattleCharacterController : MonoBehaviour
{
    [SerializeField] private CharacterData currentCharacter;
    [SerializeField] private CharacterAttack attack;
    [SerializeField] private Animator animator;

    private Character character;

    public void Start()
    {
        character = currentCharacter.CreateCharacter();
    }

    private void OnEnable()
    {
        BattleObserver.onAttack += PerformAttack;
        BattleObserver.onTurnStart += CharacterSelected;
        BattleObserver.onTurnEnd += ResetCharacter;
    }

    private void OnDisable()
    {
        BattleObserver.onAttack -= PerformAttack;
        BattleObserver.onTurnEnd -= ResetCharacter;
    }

    public void PerformAttack(Actor actorData, Attack attackData)
    {
        if (actorData is Character charData)
        {
            if (charData.Name != character.Name) return;
            attack.enabled = true;
            attack.ActivateAttack(character, attackData);
        }
    }

    public void ResetCharacter()
    {
        animator.runtimeAnimatorController = character.DefaultAnimation;
        attack.enabled = false;
        animator.SetBool("IsSelecting", false);
    }

    public void CharacterSelected(Actor actorData, Transform trans)
    {
        if(actorData is Character charData) 
        { 
            if (charData.Name != character.Name) return;

            animator.SetBool("IsSelecting", true);
        }
    }
}
