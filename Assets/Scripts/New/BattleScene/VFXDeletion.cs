using UnityEngine;

public class VFXDeletion : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Update is called once per frame
    void Update()
    {
        AnimationClip currentClip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        float currentClipTime = Mathf.Lerp(0, currentClip.length, animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        if (currentClipTime == currentClip.length)
        {
            Destroy(gameObject);
        }
    }
}
