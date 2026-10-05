using UnityEngine;

public class CompanionFollow : MonoBehaviour
{
    [SerializeField] private Transform leaderTrans;
    [SerializeField] private float minFollowDistance;

    private Vector3 moveDir;
    private Vector3 faceDir;

    private float time;
    private const float timeMargin = 0.1f;

    public Vector3 MoveDirection => moveDir;
    public Vector3 FaceDirection => faceDir;

    private void Start()
    {
        time = timeMargin;
    }
    public void MoveCompanion()
    {
        Vector3 direction = leaderTrans.position - transform.position;
        faceDir = direction.normalized;
        moveDir = direction;

        if (direction.magnitude - 0.01f >= minFollowDistance)
        {
            time = timeMargin;
            float diff = direction.magnitude - minFollowDistance;
            transform.position += direction.normalized * diff;
        }
        else
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                moveDir = Vector3.zero;
            }
        }
    }
}
