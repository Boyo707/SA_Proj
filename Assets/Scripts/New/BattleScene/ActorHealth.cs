using UnityEngine;

[RequireComponent(typeof(ActorIdentity))]
public class ActorHealth : MonoBehaviour
{
    [SerializeField] private BattleStatsPresenter statsPresenter;

    private Actor actor;

    private void Start()
    {
        Debug.Log("getting actor");
        actor = GetComponent<ActorIdentity>().MyActor;
        statsPresenter?.UpdateStats(actor);
    }

    private void OnEnable()
    {
        BattleObserver.onHit += Damage;
        BattleObserver.onHeal += Heal;
    }

    private void OnDisable()
    {
        BattleObserver.onHit -= Damage;
        BattleObserver.onHeal -= Heal;
    }

    private void Damage(Actor actorData, int damageAmount)
    {
        if (actor == actorData)
        {
            Debug.Log("Hello im the enemy taking damage");

            actorData.baseStats.currentHP = Mathf.Clamp(actorData.baseStats.currentHP - damageAmount, 0, actorData.baseStats.maxHealth);

            if (actorData.baseStats.currentHP == 0)
            {
                BattleManager.Instance.ActorDied(actor);
            }
            statsPresenter?.UpdateStats(actorData);
        }
    }

    private void Heal(Actor actorData, int healAmount)
    {
        if (actor == actorData)
        {
            actorData.baseStats.currentHP = Mathf.Clamp(actorData.baseStats.currentHP + healAmount, 0, actorData.baseStats.maxHealth);

            statsPresenter?.UpdateStats(actorData);
        }
    }
}
