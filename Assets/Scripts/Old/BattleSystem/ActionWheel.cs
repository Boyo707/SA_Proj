using System.Collections;
using UnityEngine;

public class ActionWheel : MonoBehaviour
{
    [SerializeField] public PlayerActor currentActor;
    [SerializeField] public Transform actionsParent;
    private Transform[] wheelOptions;

    [SerializeField] private Transform[] optionPositions;

    [SerializeField] private InventoryDisplay inventoryDisplay;

    private int wheelIndex = 0;

    private bool isAxisInUse = false;

    private bool canBeUsed = true;

    private void Start()
    {
        wheelOptions = new Transform[actionsParent.childCount];
        for (int i = 0; i < actionsParent.childCount; i++)
        {
            wheelOptions[i] = actionsParent.GetChild(i).transform;
        }

        ChangePosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (canBeUsed)
        {
            if (Input.GetAxisRaw("Horizontal") < 0 && !isAxisInUse)
            {
                wheelIndex++;
                if (wheelIndex > optionPositions.Length - 1)
                {
                    wheelIndex = 0;
                }
                ChangePosition();

                isAxisInUse = true;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0 && !isAxisInUse)
            {
                wheelIndex--;
                if (wheelIndex < 0)
                {
                    wheelIndex = optionPositions.Length - 1;
                }
                ChangePosition();

                isAxisInUse = true;
            }
            else if (Input.GetAxisRaw("Horizontal") == 0)
            {
                isAxisInUse = false;
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                UseAction();
            }
           //test
        }
    }

    private void UseAction()
    {
        
        switch (wheelOptions[wheelIndex].name)
        {
            case "Attack":
                StartCoroutine(AttackAction(currentActor));
                break;
            case "Run":
                break;
            case "Item":
                ItemAction();
                break;

        }
    }

    public void ResetPosition()
    {
        wheelIndex = 0;
        ChangePosition();
    }
    public void ToggleActions(bool toggle)
    {
        actionsParent.gameObject.SetActive(toggle);
        canBeUsed = toggle;
    }
    private void ChangePosition()
    {
        int thisIndex = wheelIndex;
        for (int i = 0; i < optionPositions.Length; i++)
        {
            //Debug.Log(optionPositions.Length);
            wheelOptions[thisIndex].position = optionPositions[i].position;
            thisIndex++;
            if(thisIndex > optionPositions.Length - 1)
            {
                thisIndex = 0;
            }
        }
    }

    private IEnumerator AttackAction(PlayerActor pActor)
    {
        ToggleActions(false);
        currentActor.CharAnimator.SetTrigger("hasChosen");

        yield return new WaitForEndOfFrame();

        Debug.Log(currentActor.CharAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

        yield return new WaitForSeconds(currentActor.CharAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

        pActor.Attack();
    }

    private void RunAction()
    {
        //Attempt to run
    }

    private void ItemAction()
    {
        ToggleActions(false);
        inventoryDisplay.OpenInventory(currentActor);
    }

}
