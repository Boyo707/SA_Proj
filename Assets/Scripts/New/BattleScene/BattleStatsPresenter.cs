using TMPro;
using UnityEngine;

public class BattleStatsPresenter : MonoBehaviour
{
    [SerializeField] private TMP_Text statText;

    public void UpdateStats(Actor data)
    {
        if (data is Character charData)
        {
            statText.text = $"Health: {charData.baseStats.currentHP}\n" +
            $"Special: {charData.characterStats.currentSpecial}";
        }
    }
}
