using UnityEngine;

public class EnemyOverworldController : MonoBehaviour
{
    [SerializeField] private GameObject visuals;
    [SerializeField] private GameObject functionality;
    [SerializeField]private EnemyOld enemy;

    public Vector2 roomKey;
    public EnemyOld Enemy => enemy;

    public void Start()
    {
        //enemy.currentPosition = transform.position;
        //gameObject.SetActive(false);
    }
    private void Update()
    {
        visuals.SetActive(enemy.isVisible);
        functionality.SetActive(enemy.isVisible);
    }

    public void InstantiateEnemy(EnemyOld pEnemy)
    {
        enemy = pEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManagerOld.Instance.currentEnemy = enemy;
        GameManagerOld.Instance.OnExitOveworld();
    }
}
