using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector3 playerPosition;
    public Vector3 companionPosition;

    public Character[] charactersData = new Character[2];

    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
