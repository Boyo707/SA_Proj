using UnityEngine;

public class CharacterBattleAnimation : MonoBehaviour
{
    [SerializeField] CharacterData currentCharacter;
    private Character character;

    [SerializeField] Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        character = currentCharacter.CreateCharacter();
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
    public void CharacterSelected(Actor actorData, Transform trans)
    {
        if (actorData is Character charData)
        {
            if (charData.Name != character.Name) return;

            animator.SetBool("IsSelecting", true);
        }
    }

    public void ResetCharacter()
    {
        animator.runtimeAnimatorController = character.DefaultAnimation;
        animator.SetBool("IsSelecting", false);
    }

    private void ReceiveDamage()
    {

    }
}
