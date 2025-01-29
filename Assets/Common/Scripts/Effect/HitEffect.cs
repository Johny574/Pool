using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public void AnimationFinished()
    {
        Destroy(gameObject);
    }
}
