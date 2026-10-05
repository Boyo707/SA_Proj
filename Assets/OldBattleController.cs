using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class OldBattleController : MonoBehaviour
{
    [SerializeField] public PlayerActor[] playerActors  = new PlayerActor[2];
    [SerializeField] public List<EnemyActor> enemyActors;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField, HideInInspector] private List<OldBattleObserver> battleActors;
    public static OldBattleController Instance;

    public event Action onStartTurn;
    public event Action onEndTurn;
    public event Action<CharacterOld, int> onEnemyHit;
    public event Action<CharacterOld, int> onEnemyDeath;
    public event Action<CharacterOld, int> onPlayerHit;
    public event Action<CharacterOld> onPlayerDeath;
    public event Action<CharacterOld, int> onPlayerHeal;

    private int turnIndex;

    private bool gameEnded = false;
    private int actorDeaths = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    IEnumerator Start()
    {
        battleActors.Add(playerActors[0]);
        battleActors.Add(playerActors[1]);
        for (int i = 0; i < enemyActors.Count; i++)
        {
            battleActors.Add(enemyActors[i]);
        }

        yield return new WaitForSeconds(0.1f);

        battleActors[0].StartTurn();
    }

    public void EndCurrentTurn()
    {
        battleActors[turnIndex].EndTurn();
    }

    public void NextTurn()
    {
        
        turnIndex++;
        if(turnIndex > battleActors.Count - 1)
        {
            turnIndex = 0;
        }
        if (!gameEnded)
        {
            if (battleActors[turnIndex] != null)
            {
                battleActors[turnIndex].StartTurn();
            }
            else
            {
                NextTurn();
            }
        }
    }

    public void GetEnemyHit(float pDamagePercentage, CharacterOld pCharData) 
    {
        int fullDamage = pCharData.CharacterStats.damage + pCharData.currentWeapon.Damage;
        int finalDamage = Mathf.RoundToInt(fullDamage * pDamagePercentage);
        //damage the enemy
        onEnemyHit?.Invoke(pCharData, finalDamage);
    }

    public void GetEnemyDeath(CharacterOld pChar, int pDamage)
    {
        gameEnded = true;
        onEnemyDeath?.Invoke(pChar, pDamage);
    }

    public void GetActorHit(CharacterOld pCharacter, int pDamage)
    {
        onPlayerHit?.Invoke(pCharacter, pDamage);
    }

    public void GetActorDeath(CharacterOld pCharacter)
    {
        Debug.Log("Pre character name: " + pCharacter.Name);
        Debug.Log("1/4");
        for (int i = 0; i < battleActors.Count; i++)
        {
            Debug.Log("2/4");
            if (battleActors[i] != null)
            {
                Debug.Log("3/4");
                if (battleActors[i].TryGetComponent(out PlayerActor playerActor) && playerActor.CharacterData == pCharacter)
                {
                    Debug.Log("4/4");
                    actorDeaths++;
                    battleActors[i] = null;
                    if(actorDeaths == 2)
                    {
                        gameEnded = true;
                        gameOverPanel.SetActive(true);
                    }
                }
            }
        }
        Debug.Log("Post character name: " + pCharacter.Name);
        onPlayerDeath?.Invoke(pCharacter);
    }

    public void GetPlayerHeal(CharacterOld pCharacter, int pDamage)
    {
        onPlayerHeal?.Invoke(pCharacter, pDamage);
    }
}
