using TMPro;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    [SerializeField] private GameObject positiveHit;
    [SerializeField] private GameObject negativeHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        BattleObserver.onHit += DamageVFX;
    }

    private void OnDisable()
    {
        BattleObserver.onHit -= DamageVFX;
    }

    public void DamageVFX(Actor data, int damage)
    {
        Vector3 damagePos = data.transform.position + Vector3.up * 3;
        if (data is Character charData)
        {
            //needs damage int and transform
            GameObject obj = Instantiate(negativeHit);
            obj.GetComponentInChildren<TMP_Text>().text = $"{damage}";
        }
        else if(data is Enemy enemyData)
        {
            GameObject obj = Instantiate(positiveHit);
            obj.GetComponentInChildren<TMP_Text>().text = $"{damage}";
        }
    }

    public void NegativeDamage()
    {

    }


}
