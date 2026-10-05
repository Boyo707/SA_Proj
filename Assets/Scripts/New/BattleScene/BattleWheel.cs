using System;
using System.Collections;
using UnityEngine;

public class BattleWheel : MonoBehaviour
{
    [SerializeField] private IconData[] icons;
    [SerializeField] private Transform[] wheelPositions = new Transform[4];
    [SerializeField] private float spinTime;

    [SerializeField] private CharacterData currentCharacter;
    private Character bleh;

    private bool isMoving = false;

    public enum IconType
    {
        NormalAttack,
        DuoAttack,
        Bag,
        Run
    }

    [Serializable]
    public struct IconData
    {
        public IconType type;
        public Transform transform;
        [HideInInspector] public int posIndex;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].posIndex = i;
            icons[i].transform.position = wheelPositions[i].position;
        }
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
            startingPos[i] = icons[i].transform.position;
        }

        do
        {
            elapsedTime += Time.deltaTime;
            percentage = elapsedTime / spinTime;

            for (int i = 0; i < wheelPositions.Length; i++)
            {
                int currentIndex = icons[i].posIndex;
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
                    icons[i].transform.position = Vector3.Lerp(startingPos[i], wheelPositions[nextIndex].position, percentage);
            }
            yield return null;
        }
        while (percentage < 1);

        for (int i = 0; i < wheelPositions.Length; i++)
        {
            int nextIndex = icons[i].posIndex + 1;
            if (isRight)
            {
                nextIndex = icons[i].posIndex + 1;
                if (nextIndex > 3)
                {
                    nextIndex = 0;
                }
            }
            else
            {
                nextIndex = icons[i].posIndex - 1;
                if (nextIndex < 0)
                {
                    nextIndex = 3;
                }
            }

            icons[i].transform.position = wheelPositions[nextIndex].position;
            icons[i].posIndex = nextIndex;
        }

        isMoving = false;
    }

    private void SelectAction()
    {
        for (int i = 0; i < icons.Length; i++) 
        {
            if (icons[i].posIndex == 0)
            {
                switch (icons[i].type)
                {
                    case IconType.NormalAttack:
                        Debug.Log("Normal Attack Selected");
                        break;
                    case IconType.DuoAttack:
                        Debug.Log("Duo Attack Selected");
                        break;
                    case IconType.Bag:
                        Debug.Log("Bag Selected");
                        break;
                    case IconType.Run:
                        Debug.Log("Run Selected");
                        break;
                }
            }
        }
    }

    
}
