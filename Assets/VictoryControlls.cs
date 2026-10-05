using System.Collections;
using TMPro;
using UnityEngine;

public class VictoryControlls : OldBattleObserver
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private TMP_Text totalRewardsText;
    [SerializeField] private TMP_Text[] characterRewards = new TMP_Text[2];
    [SerializeField] private GameObject[] LevelUpText = new GameObject[2];

    private Enemy enemyData;
    private CharacterOld[] characterData = new CharacterOld[2];

    protected override void GetEnemyDeath(CharacterOld pCharData, int damage)
    {
        victoryPanel.SetActive(true);
        for (int i = 0; i < characterData.Length; i++)
        {
            characterData[i] = GameManagerOld.Instance.characterDatas[i];
        }
        enemyData = GameManagerOld.Instance.currentEnemy;
        StartCoroutine(Testing());
    }

    private IEnumerator Testing()
    {
        totalRewardsText.text = $"Coins:\n" +
            $"XP:";

        characterRewards[0].text = $"{characterData[0].Name}";
        characterRewards[1].text = $"{characterData[1].Name}";

        yield return new WaitForSeconds(1);

        totalRewardsText.text = $"Coins: {enemyData.Money}\n" +
            $"XP:";

        yield return new WaitForSeconds(0.5f);

        totalRewardsText.text = $"Coins: {enemyData.Money}\n" +
            $"XP: {enemyData.XP}";

        yield return new WaitForSeconds(0.5f);

        characterRewards[0].text = $"{characterData[0].Name}\n" +
            $"\n" +
            $"Level: {characterData[0].CharacterStats.level}\n" +
            $"XP: {characterData[0].CharacterStats.experiencePoints}";

        characterRewards[1].text = $"{characterData[1].Name}\n" +
            $"\n" +
            $"Level: {characterData[1].CharacterStats.level}\n" +
            $"XP: {characterData[1].CharacterStats.experiencePoints}";

        yield return new WaitForSeconds(0.5f);

        characterRewards[0].text = $"{characterData[0].Name}\n" +
            $"\n" +
            $"Level: {characterData[0].CharacterStats.level}\n" +
            $"XP: {characterData[0].CharacterStats.experiencePoints} + {enemyData.XP / 2}";

        characterRewards[1].text = $"{characterData[1].Name}\n" +
            $"\n" +
            $"Level: {characterData[1].CharacterStats.level}\n" +
            $"XP: {characterData[1].CharacterStats.experiencePoints} + {enemyData.XP / 2}";

        yield return new WaitForSeconds(0.5f);

        characterData[0].CharacterStats.experiencePoints += enemyData.XP / 2;
        characterData[1].CharacterStats.experiencePoints += enemyData.XP / 2;

        characterRewards[0].text = $"{characterData[0].Name}\n" +
            $"\n" +
            $"Level: {characterData[0].CharacterStats.level}\n" +
            $"XP: {characterData[0].CharacterStats.experiencePoints}";

        characterRewards[1].text = $"{characterData[1].Name}\n" +
            $"\n" +
            $"Level: {characterData[1].CharacterStats.level}\n" +
            $"XP: {characterData[1].CharacterStats.experiencePoints}";
        levelup();

        characterRewards[0].text = $"{characterData[0].Name}\n" +
            $"\n" +
            $"Level: {characterData[0].CharacterStats.level}\n" +
            $"XP: {characterData[0].CharacterStats.experiencePoints}";

        characterRewards[1].text = $"{characterData[1].Name}\n" +
            $"\n" +
            $"Level: {characterData[1].CharacterStats.level}\n" +
            $"XP: {characterData[1].CharacterStats.experiencePoints}";

    }

    private void levelup()
    {
        //Debug.Log(c2data.levelCurve.Evaluate());
        //the level should add the xp or set the value
        //xp should be reset once it reaches a new level

        bool hasFullyLeveled = false;

        //if new level is higher then current level, level up and remove key time from expierence points
        for (int i = 0; i < 2; i++)
        {
            Keyframe[] keys = characterData[i].LevelCurve.keys;

            while (!hasFullyLeveled)
            {
                float currentXP = characterData[i].CharacterStats.experiencePoints;
                float nextLevelXP = keys[characterData[i].CharacterStats.level + 1].time;
                if (currentXP > nextLevelXP)
                {
                    characterData[i].CharacterStats.experiencePoints -= (int)nextLevelXP;
                    characterData[i].CharacterStats.level++;
                    LevelUpText[i].SetActive(true);
                }
                else
                {
                    hasFullyLeveled = true;
                }
            }
            hasFullyLeveled = false;
        }
    }
}
