using PP.InventorySystem;
using System.Collections;
using TMPro;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private Transform itemLayoutGroup;

    [SerializeField] private ActionWheel actionWheel;

    [SerializeField] private int itemIndex = 0;

    private PlayerActor playerActor;

    private bool canBeUsed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        InstantiateInventory();
        itemLayoutGroup.GetChild(0).GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (canBeUsed)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ChangeSelection(-1);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                ChangeSelection(1);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                CloseInventory();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                StartCoroutine(UseItem());
            }
        }
    }

    public IEnumerator UseItem()
    {

        if (Inventory.Instance.Items[itemIndex].Id.Contains("hp"))
        {
            playerActor.CharAnimator.SetTrigger("hasChosen");

            yield return new WaitForEndOfFrame();

            Debug.Log(playerActor.CharAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

            yield return new WaitForSeconds(playerActor.CharAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

            Debug.Log("Ah");
            OldBattleController.Instance.GetPlayerHeal(playerActor.CharacterData, Inventory.Instance.Items[itemIndex].Healing);
            OldBattleController.Instance.EndCurrentTurn();
            itemLayoutGroup.gameObject.SetActive(false);
            canBeUsed = false;
        }
    }

    public void OpenInventory(PlayerActor actor)
    {
        itemLayoutGroup.gameObject.SetActive(true);
        canBeUsed = true;
        playerActor = actor;
    }

    public void CloseInventory()
    {
        itemLayoutGroup.gameObject.SetActive(false);
        canBeUsed = false;
        actionWheel.ToggleActions(true);
    }

    private void ChangeSelection(int add)
    {
        itemLayoutGroup.GetChild(itemIndex).GetChild(0).gameObject.SetActive(false);
        itemIndex += add;
        if(itemIndex > itemLayoutGroup.childCount - 1)
        {
            itemIndex = 0;
        }
        else if(itemIndex < 0)
        {
            itemIndex = itemLayoutGroup.childCount - 1;
        }
        itemLayoutGroup.GetChild(itemIndex).GetChild(0).gameObject.SetActive(true);
    }

    private void InstantiateInventory()
    {
        for (int i = 0; i < itemLayoutGroup.childCount; i++)
        {
            Destroy(itemLayoutGroup.GetChild(i));
        }

        for (int i = 0; i < Inventory.Instance.Items.Length; i++)
        {
            Instantiate(itemPanel, itemLayoutGroup).
                GetComponentInChildren<TMP_Text>().text = Inventory.Instance.Items[i].ItemName;
        }
    }

    
}
