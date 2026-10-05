using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Image characterImage;
    [SerializeField] private PlayerActor playerActor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = playerActor.CharacterData.CharacterStats.currentHP.ToString();
    }

}
