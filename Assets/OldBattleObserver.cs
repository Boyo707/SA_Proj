using UnityEngine;

public abstract class OldBattleObserver : MonoBehaviour
{
    [SerializeField]
    protected OldBattleController battleController;

    protected void OnEnable()
    {
        battleController.onEnemyHit += GetEnemyHit;
        battleController.onEnemyDeath += GetEnemyDeath;
        battleController.onPlayerHit += GetPlayerHit;
        battleController.onPlayerDeath += GetPlayerDeath;
        battleController.onPlayerHeal += GetPlayerHeal;
    }

    protected void OnDisable()
    {
        battleController.onEnemyHit -= GetEnemyHit;
        battleController.onEnemyDeath -= GetEnemyDeath;
        battleController.onPlayerHit -= GetPlayerHit;
        battleController.onPlayerDeath -= GetPlayerDeath;
        battleController.onPlayerHeal -= GetPlayerHeal;

    }

    public virtual void StartTurn() { }
    public virtual void EndTurn() { }

    protected virtual void GetEnemyHit(CharacterOld pCharData, int damage) { }
    protected virtual void GetEnemyDeath(CharacterOld pCharData, int damage) { }
    protected virtual void GetPlayerHit(CharacterOld pCharacter, int pDamage) { }
    protected virtual void GetPlayerDeath(CharacterOld pCharacter) { }
    protected virtual void GetPlayerHeal(CharacterOld pCharacter, int damage) { }
}
