using UnityEngine;

public class BattleCharacterController : MonoBehaviour
{
    [SerializeField] private CharacterData currentCharacter;
    [SerializeField] private CharacterAttack attack;

    private Character character;

    public void Start()
    {
        character = currentCharacter.CreateCharacter();
    }

    public void PerformAttack()
    {
        //HEY OBSERVER IM STARTING IT ISH
        attack.enabled = true;
        attack.ActivateAttack(character);
    }
}
