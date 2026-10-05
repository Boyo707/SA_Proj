using UnityEngine;

public class VisualEffectController : OldBattleObserver
{
    [SerializeField] private GameObject damageVFX;

    protected override void GetEnemyHit(CharacterOld charData, int damage)
    {
        GameObject VFXObj = Instantiate(damageVFX, transform.position, Quaternion.identity);
        VFXObj.GetComponent<VFX>().SetVFX(damage.ToString(), Color.yellow);
    }
    protected override void GetEnemyDeath(CharacterOld pCharData, int damage) 
    { 
        
    }
    protected override void GetPlayerDeath(CharacterOld pCharacter) 
    {
        
    }
    protected override void GetPlayerHit(CharacterOld pCharacter, int pDamage) 
    {
        GameObject VFXObj = Instantiate(damageVFX, transform.position, Quaternion.identity);
        VFXObj.GetComponent<VFX>().SetVFX(pDamage.ToString(), Color.red);
    }
    protected override void GetPlayerHeal(CharacterOld pCharacter, int heal)
    {
        GameObject VFXObj = Instantiate(damageVFX, transform.position, Quaternion.identity);
        VFXObj.GetComponent<VFX>().SetVFX(heal.ToString(), Color.green);
    }
}
