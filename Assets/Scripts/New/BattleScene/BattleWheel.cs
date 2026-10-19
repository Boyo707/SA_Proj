using System;
using System.Collections;
using UnityEngine;

public class BattleWheel : MonoBehaviour
{
    [SerializeField] private ActionData[] actions;
    [SerializeField] private Transform[] wheelPositions = new Transform[4];
    [SerializeField] private float spinTime;

    [SerializeField] private float wheelHeight = 0.5f;

    private bool isMoving = false;


    private Character currentCharacter;
    public enum IconType
    {
        NormalAttack,
        DuoAttack,
        Bag,
        Run
    }

    [Serializable]
    private struct ActionData
    {
        public IconType type;
        public Transform transform;
        [HideInInspector] public int posIndex;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].posIndex = i;
            actions[i].transform.position = wheelPositions[i].position;
        }
    }

    private void OnEnable()
    {
        BattleObserver.onTurnStart += InstantiateWheel;
    }

    private void OnDisable()
    {
        BattleObserver.onTurnStart -= InstantiateWheel;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && !isMoving)
        {
            StartCoroutine(MoveIcons(true));
        }
        if (Input.GetKeyDown(KeyCode.A) && !isMoving)
        {
            StartCoroutine(MoveIcons(false));
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SelectAction();
        }
    }

    private IEnumerator MoveIcons(bool isRight)
    {
        isMoving = true;
        float elapsedTime = 0;
        float percentage = 0;
        Vector3[] startingPos = new Vector3[4];

        for (int i = 0; i < startingPos.Length; i++)
        {
            startingPos[i] = actions[i].transform.position;
        }

        do
        {
            elapsedTime += Time.deltaTime;
            percentage = elapsedTime / spinTime;

            for (int i = 0; i < wheelPositions.Length; i++)
            {
                int currentIndex = actions[i].posIndex;
                int nextIndex = 0;
                if (isRight)
                {
                    nextIndex = currentIndex + 1;
                    if (nextIndex > 3)
                    {
                        nextIndex = 0;
                    }
                }
                else
                {
                    nextIndex = currentIndex - 1;
                    if (nextIndex < 0)
                    {
                        nextIndex = 3;
                    }
                }
                    actions[i].transform.position = Vector3.Lerp(startingPos[i], wheelPositions[nextIndex].position, percentage);
            }
            yield return null;
        }
        while (percentage < 1);

        for (int i = 0; i < wheelPositions.Length; i++)
        {
            int nextIndex = actions[i].posIndex + 1;
            if (isRight)
            {
                nextIndex = actions[i].posIndex + 1;
                if (nextIndex > 3)
                {
                    nextIndex = 0;
                }
            }
            else
            {
                nextIndex = actions[i].posIndex - 1;
                if (nextIndex < 0)
                {
                    nextIndex = 3;
                }
            }

            actions[i].transform.position = wheelPositions[nextIndex].position;
            actions[i].posIndex = nextIndex;
        }

        isMoving = false;
    }

    private void SelectAction()
    {
        for (int i = 0; i < actions.Length; i++) 
        {
            //if this is the first pos
            if (actions[i].posIndex == 0)
            {
                //get action type
                switch (actions[i].type)
                {
                    case IconType.NormalAttack:
                        Debug.Log("Normal Attack Selected");

                        BattleObserver.onAttack?.Invoke(currentCharacter, currentCharacter.GetNormalAttack());
                        DisableWheel();
                        break;
                    case IconType.DuoAttack:
                        Debug.Log("Duo Attack Selected");


                        DisableWheel();
                        break;
                    case IconType.Bag:
                        Debug.Log("Bag Selected");
                        break;
                    case IconType.Run:
                        Debug.Log("Run Selected");

                        DisableWheel();
                        break;
                }
            }
        }
    }

    private void DisableWheel()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void InstantiateWheel(Actor data, Transform characterTrans)
    {
        if (data is Character characterData)
        {
            currentCharacter = characterData;

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            transform.position = characterTrans.position + Vector3.up * wheelHeight;

            ResetWheel();
        }
    }

    private void ResetWheel()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].transform.position = wheelPositions[i].position;
            actions[i].posIndex = i;
        }
    }
}
