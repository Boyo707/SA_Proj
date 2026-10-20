using UnityEngine;

public class ActorHeath : MonoBehaviour
{
    [SerializeField] ActorData currentCharacter;
    private Character character;
    private Enemy enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        if(currentCharacter is CharacterData data)
        {
            character = data.CreateCharacter();
        }
        else if(currentCharacter is EnemyData enemyData)
        {
            enemy = enemyData.CreateEnemy();
        }
    }

    private void ReceiveDamage(Actor data)
    {
        if(data.Name == currentCharacter.characterName)
        {

        }
    }
}
