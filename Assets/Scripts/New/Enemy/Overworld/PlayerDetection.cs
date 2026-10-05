using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private float range = 2;
    [SerializeField] private float radius = 45;
    [SerializeField] private float detectionTime = 2;
    [SerializeField] private Transform target;

    private Vector2 faceDir = Vector2.left;

    private float startTime = 0;

    private float dot;

    private float dotRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 rightVec = Quaternion.Euler(0, 0, radius) * Vector3.up;
        dotRange = Vector3.Dot(rightVec, Vector3.up);
    }

    

    public bool InRange()
    {
        Vector3 targetDir = target.position - transform.position;
        return targetDir.magnitude < range;
    }

    public bool InVision()
    {
        Vector3 targetDir = (target.position - transform.position);
        dot = Vector3.Dot(faceDir, targetDir.normalized);
        return dot > dotRange;
    }

    public bool InView()
    {
        if (InVision() && InRange())
        {
            if (startTime == 0)
            {
                startTime = Time.time;
            }
            Debug.Log("Hm?");
            return true;
        }
        startTime = 0;
        return false;
    }

    public bool IsDetected()
    {
        if(Time.time > startTime + detectionTime)
        {
            Debug.Log("THERE YOU ASRE");
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetDetectionDir(Vector3 dir)
    {
        faceDir = dir.normalized;
    }

    private void OnDrawGizmos()
    {
        
        Vector2 rightVec = Quaternion.Euler(0, 0, radius) * faceDir;
        Vector2 leftVec = Quaternion.Euler(0, 0, -radius) * faceDir;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, leftVec * range);
        Gizmos.DrawRay(transform.position, rightVec * range);
        Gizmos.DrawRay(transform.position, faceDir * range);
    }
}
