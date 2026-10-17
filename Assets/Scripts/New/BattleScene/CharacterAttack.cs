using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterAttack : MonoBehaviour
{
    private Character characterData;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.enabled = false;
    }

    public void ActivateAttack(Character data)
    {
        characterData = data; 
        currentAttack = data.AttackData.GetAttack();
        animator.runtimeAnimatorController = currentAttack.AnimationController;

        startPosition = transform.position;
        attackPosition = enemy.position + Vector3.left * currentAttack.AttackDistance;

        currentClip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        previousClip = currentClip;
    }

    // Update is called once per frame
    void Update()
    {
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

    private void AttackCheck()
    {
        //Input Check
        if (Input.GetMouseButtonDown(0) && currentAttack.HasAttack(previousClip))
        {
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

        //Fail Check
        if (currentClip != previousClip && currentAttack.HasAttack(previousClip))
        {
            if (hasAttacked == false)
            {
                animator.SetTrigger("Fail");
            }
            else
            {
                hasAttacked = false;
            }
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
    public bool AnimationHasReachedEnd()
    {
        return Mathf.Lerp(0, currentClip.length, animator.GetCurrentAnimatorStateInfo(0).normalizedTime) == currentClip.length;
    }

    public void EndAttack()
    {
        //HEY OBSERVER IM ENDING IT
        isEnding = false;
        animator.runtimeAnimatorController = characterData.DefaultAnimation;
        this.enabled = false;
    }
    
}
