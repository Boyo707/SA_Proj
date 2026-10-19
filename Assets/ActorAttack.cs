using UnityEngine;

public class ActorAttack : MonoBehaviour
{
    private GameObject sparkObject;

    private CharacterOld characterData;
    private Animator playerAnimator;

    private EnemyOld enemyData;
    private Animator enemyAnimator;

    private string clipName;

    public bool hasAttacked = false;
    public bool hasHit = false;
    public bool hasMissed = false;
    private int attackCombo = 0;

    private bool isInitialized = false;
    private bool attackStarted = false;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private float time = 0;
    private bool sparkActive = false;

    public void InitializeAttack(GameObject pSparkObject, CharacterOld pCharData, Animator pAnimator)
    {
        sparkObject = pSparkObject;
        playerAnimator = pAnimator;
        characterData = pCharData;

        isInitialized = true;
    }
    public void InitializeAttack(GameObject pSparkObject, GameObject pEnemy, Transform pPlayerTrans, bool pSecondary)
    {
        sparkObject = pSparkObject;

        playerAnimator = pPlayerTrans.GetComponent<Animator>();
        characterData = pPlayerTrans.GetComponent<PlayerActor>().CharacterData;

        enemyAnimator = pEnemy.GetComponent<Animator>();
        enemyAnimator.SetBool("Attack2", pSecondary);
        enemyData = pEnemy.GetComponent<EnemyActor>().EnemyData;

        startPosition = transform.position;
        Vector3 playerPos = pPlayerTrans.position;
        endPosition = new Vector3(playerPos.x + 1.5f, playerPos.y, playerPos.z);

        isInitialized = true;
    }

    private void Update()
    {
        if (isInitialized)
        {
            Attack();
        }
    }

    

    private void Attack()
    {
        if (enemyAnimator == null)
        {
            clipName = playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            if (clipName == "BattleIdle")
            {
                if (attackStarted)
                {
                    OldBattleController.Instance.EndCurrentTurn();
                    Destroy(this);
                }
                attackCombo = 0;
            }
            else
            {
                attackStarted = true;
            }

            //if attack window opened
            if (sparkObject.activeSelf)
            {
                sparkActive = true;
            }
            //if spark bool is active and attack window closes
            else if (sparkActive && !sparkObject.activeSelf)
            {
                hasAttacked = false;
                hasHit = false;
                sparkActive = false;
            }

            if (Input.GetKeyDown(KeyCode.K) && hasAttacked == false)
            {
                hasAttacked = true;
                if (sparkObject.activeSelf == true)
                {
                    hasHit = true;
                    attackCombo++;
                    OldBattleController.Instance.GetEnemyHit(attackCombo * 0.33f, characterData);
                }
                else if (sparkObject.activeSelf == false)
                {
                    attackCombo = 0; //for testing
                }
            }

            playerAnimator.SetBool("canContinue", hasHit);
        }
        else
        {
            clipName = enemyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            if(clipName == "BattleIdle")
            {
                if (attackStarted)
                {
                    if (LerpGameObject(endPosition, startPosition))
                    {
                        OldBattleController.Instance.EndCurrentTurn();
                        Destroy(this);
                    }
                }
                else
                {
                    if (LerpGameObject(startPosition, endPosition))
                    {
                        enemyAnimator.SetTrigger("HasMoved");
                    }
                }
            }
            else
            {
                //reset lerp time
                if (!attackStarted) time = 0;
                attackStarted = true;
            }

            //if block window opened
            if (sparkObject.activeSelf)
            {
                sparkActive = true;
            }
            //if spark bool is active and block window closes
            else if(sparkActive && !sparkObject.activeSelf)
            {
                //if the attack was not blocked
                if (!hasHit)
                {
                    hasMissed = true;
                }
                playerAnimator.SetBool("Blocked", false);
                hasAttacked = false;
                hasHit = false;
                sparkActive = false;
            }

            //if the button has been pressed - if the player has not blocked yet
            if (Input.GetKeyDown(KeyCode.K) && !hasAttacked)
            {
                //the player has attempted to block
                hasAttacked = true;

                //if block window is open
                if (sparkObject.activeSelf)
                {
                    //hasBlocked
                    playerAnimator.SetBool("Blocked", true);
                    hasHit = true;
                }
            }

            //if player missed the block
            if (hasMissed)
            {
                hasMissed = false;
                OldBattleController.Instance.GetActorHit(characterData, enemyData.Damage);
            }
        }
    }

    private bool LerpGameObject(Vector3 startPos, Vector3 endPos)
    {
        //set target Pos
        time += Time.deltaTime;
        transform.position = Vector3.Lerp(startPos, endPos, time * 1.2f);

        if(startPos.x > endPos.x)
        {
            if (transform.position.x == endPos.x)
            {
                transform.position = endPos;

                return true;
            }
            return false;
        }
        else
        {
            if(transform.position.x == endPos.x)
            {
                transform.position = endPos;
                return true;
            }
            return false;
        }
    }

    public void StopAttack()
    {
        hasAttacked = true;
        hasHit = false;
        hasMissed = false;
    }
}
