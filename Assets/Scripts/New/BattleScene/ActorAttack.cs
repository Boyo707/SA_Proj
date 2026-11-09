using System.Collections;
using UnityEngine;
[RequireComponent(typeof(ActorIdentity))]
[RequireComponent(typeof(Animator))]

public class ActorAttack : MonoBehaviour
{
    private Attack currentAttack;

    private Actor attackingActor;
    private Actor targetActor;

    private Animator animator;
        
    private Vector3 attackPosition;
    private Vector3 startPosition;

    private AnimationClip previousClip;
    private AnimationClip currentClip;

    private bool hasAttacked = false;
    private bool isLerping = false;

    private bool isEnding = false;
    private bool isAttacking = false;


    private Actor actor;
    private void Start()
    {
        actor = GetComponent<ActorIdentity>().MyActor;
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        BattleObserver.onAttack += ActivateAttack;
    }

    private void OnDisable()
    {
        BattleObserver.onAttack -= ActivateAttack;
    }

    public void ActivateAttack(Actor actorData, Attack attackData, Actor enemyData)
    {
        if (actor == actorData)
        {
            attackingActor = actorData;
            currentAttack = attackData;
            targetActor = enemyData;

            animator.runtimeAnimatorController = currentAttack.AnimationController;

            startPosition = attackingActor.transform.position;
            attackPosition = targetActor.transform.position + Vector3.left * currentAttack.AttackDistance;

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
            attackingActor.transform.position = Vector3.Lerp(startPos, endPos, currentAttack.AproachCurve.Evaluate(lerpTime));
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
            //Debug.Log("Check Click");
            if (currentAttack.HasAttack(previousClip))
            {
                //Debug.Log("Is clip");
                float currentClipTime = Mathf.Lerp(0, currentClip.length, animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

                float hitTime = currentClip.length - currentClipTime;
                if (hitTime > currentAttack.ReactionTime)
                {
                    animator.SetTrigger("Fail");
                }
                else if (hitTime < currentAttack.ReactionTime)
                {
                    hasAttacked = true;
                    animator.SetTrigger("Hit");
                    BattleManager.Instance.DamageActor(targetActor, attackingActor.baseStats.damage);
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
                //Debug.Log(currentClip.name);
                //Debug.Log(previousClip.name);
                //Debug.Log("Failed");
                animator.SetTrigger("Fail");
            }
            else
            {
                //Debug.Log("ClickFail");
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
        BattleManager.Instance.EndTurn();
    }
    
}
