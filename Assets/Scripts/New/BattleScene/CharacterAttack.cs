using System.Collections;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] CharacterData currentCharacter;
    private Character character;

    private Attack currentAttack;
    
    [SerializeField] private Animator animator;
    [SerializeField] private Transform enemy;
    
    private Vector3 attackPosition;
    private Vector3 startPosition;

    private AnimationClip previousClip;
    private AnimationClip currentClip;

    private bool hasAttacked = false;
    private bool isLerping = false;

    private bool isEnding = false;
    private bool isAttacking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        character = currentCharacter.CreateCharacter();
    }

    private void OnEnable()
    {
        BattleObserver.onAttack += ActivateAttack;
    }

    private void OnDisable()
    {
        BattleObserver.onAttack -= ActivateAttack;
    }

    public void ActivateAttack(Actor data, Attack attackData)
    {
        if (data.Name == character.Name)
        {
            currentAttack = attackData;
            animator.runtimeAnimatorController = currentAttack.AnimationController;

            startPosition = transform.position;
            attackPosition = enemy.position + Vector3.left * currentAttack.AttackDistance;

            currentClip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
            previousClip = currentClip;

            isAttacking = true;
        }
    }

    private void Update()
    {
        if (!isAttacking) return;

        currentClip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;

        if (isEnding)
        {
            if (AnimationHasReachedEnd())
            {
                EndAttack();
            }
            return;
        }

        LerpCheck();

        AttackCheck();

        FailCheck();

        previousClip = currentClip;
    }

    private void LerpCheck()
    {
        if (currentAttack.IsAproachClip(currentClip))
        {
            if (!isLerping)
            {
                StartCoroutine(LerpMove(startPosition, attackPosition, false));
            }
        }
        else if (currentAttack.IsReturnClip(currentClip))
        {
            if (!isLerping)
            {
                StartCoroutine(LerpMove(attackPosition, startPosition, true));
            }
        }
        else
        {
            isLerping = false;
        }
    }

    public IEnumerator LerpMove(Vector3 startPos, Vector3 endPos, bool isReturning)
    {
        isLerping = true;
        float elapsedTime = 0;
        float lerpTime = 0;
        while (lerpTime <= 1)
        {
            lerpTime = elapsedTime / currentAttack.AproachTime;
            transform.position = Vector3.Lerp(startPos, endPos, currentAttack.AproachCurve.Evaluate(lerpTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (isReturning)
        {
            isEnding = true;
        }
    }

    private void AttackCheck()
    {
        //Input Check
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Check Click");
            if (currentAttack.HasAttack(previousClip))
            {
                Debug.Log("Is clip");
                float currentClipTime = Mathf.Lerp(0, currentClip.length, animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

                float hitTime = currentClip.length - currentClipTime;
                if (hitTime > currentAttack.ReactionTime)
                {
                    animator.SetTrigger("Fail");
                }
                else if (hitTime < currentAttack.ReactionTime)
                {
                    hasAttacked = true;
                }
            }
        }
    }

    private void FailCheck()
    {
        //Fail Check
        if (currentClip != previousClip && currentAttack.HasAttack(previousClip))
        {
            if (hasAttacked == false)
            {
                Debug.Log(currentClip.name);
                Debug.Log(previousClip.name);
                Debug.Log("Failed");
                animator.SetTrigger("Fail");
            }
            else
            {
                Debug.Log("ClickFail");
                hasAttacked = false;
            }
        }
    }

    
    public bool AnimationHasReachedEnd()
    {
        return Mathf.Lerp(0, currentClip.length, animator.GetCurrentAnimatorStateInfo(0).normalizedTime) == currentClip.length;
    }

    public void EndAttack()
    {
        isAttacking = false;
        isEnding = false;
        BattleObserver.onTurnEnd?.Invoke();
    }
    
}
