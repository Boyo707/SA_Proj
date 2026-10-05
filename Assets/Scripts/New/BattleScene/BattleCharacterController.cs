using UnityEngine;

public class BattleCharacterController : MonoBehaviour
{
    [SerializeField] private CharacterData currentCharacter;
    [SerializeField] private Animator animator;

    private Character character;

    public void Start()
    {
        character = currentCharacter.CreateCharacter();
    }

    public void NormalAttack()
    {
        
    }
}
