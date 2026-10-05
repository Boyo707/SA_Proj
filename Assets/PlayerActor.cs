using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : OldBattleObserver
{
    [SerializeField] private GameObject selectionWheel;
    [SerializeField] private Transform wheelPosTransform;
    [SerializeField] private bool isMale;
    public bool isDead = false;

    [SerializeField] private List<RuntimeAnimatorController> animations;
    //0 = selection
    //1 = basicAttack

    [SerializeField] private Animator animator;

    public Animator CharAnimator => animator; 

    [SerializeField] private GameObject sparkObject;

    private ActionWheel actionWheel;

    private CharacterOld characterData;
    public CharacterOld CharacterData => characterData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isMale)
        {
            characterData = GameManagerOld.Instance.characterDatas[0];
        }
        else
        {
            characterData = GameManagerOld.Instance.characterDatas[1];
        }

        actionWheel = selectionWheel.GetComponent<ActionWheel>();
    }

    public override void StartTurn()
    {
        animator.runtimeAnimatorController = animations[0];

        actionWheel.currentActor = this;
        selectionWheel.transform.position = wheelPosTransform.position;
        actionWheel.ToggleActions(true);
    }

    public override void EndTurn()
    {
        animator.runtimeAnimatorController = animations[0];
        actionWheel.ResetPosition();
        OldBattleController.Instance.NextTurn();
    }

    protected override void GetPlayerHit(CharacterOld pCharacter, int pDamage)
    {
        if (pCharacter == characterData)
        {
            characterData.CharacterStats.currentHP -= pDamage;

            if (characterData.CharacterStats.currentHP <= 0)
            {
                Debug.Log(pCharacter.Name);
                characterData.CharacterStats.currentHP = 0;
                OldBattleController.Instance.GetActorDeath(pCharacter);
            }
            else
            {
                animator.SetTrigger("Hurt");
            }
        }
    }

    protected override void GetPlayerDeath(CharacterOld pCharacter)
    {
        if (characterData == pCharacter)
        {
            isDead = true;
            animator.SetBool("Death", true);
        }
    }

    protected override void GetEnemyDeath(CharacterOld pCharData, int damage)
    {
        if(TryGetComponent<ActorAttack>(out ActorAttack attack))
        {
            attack.StopAttack();
        }
    }
    
    public void Attack()
    {
        actionWheel.ResetPosition();
        animator.runtimeAnimatorController = animations[1];
        gameObject.AddComponent<ActorAttack>().InitializeAttack(sparkObject, characterData, animator);
    }

    protected override void GetPlayerHeal(CharacterOld pCharacter, int heal)
    {
        if(characterData == pCharacter)
        {
            int newHealth = characterData.CharacterStats.currentHP + heal;
            characterData.CharacterStats.currentHP = Mathf.Clamp(newHealth, 0, characterData.CharacterStats.maxHealth);
        }
    }

}
