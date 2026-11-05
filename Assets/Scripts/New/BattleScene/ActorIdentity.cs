using UnityEngine;

public class ActorIdentity : MonoBehaviour
{
    //[SerializeField] private ActorData actorData;
    private Actor actor = null;
    public Actor MyActor => actor;

    private void Awake()
    {
        /*if (actorData is CharacterData characterData)
        {
            actor = characterData.CreateCharacter();
        }
        else if (actorData is EnemyData enemyData) 
        {
            actor = enemyData.CreateEnemy();
        }
        Debug.Log("awakASs");*/

    }

    public void AssignActor(Actor assignedActor)
    {
        actor = assignedActor;
        Debug.Log("Assigned");
    }
}
