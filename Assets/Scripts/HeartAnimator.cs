using DG.Tweening;
using UnityEngine;

public class HeartAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMoveY(500, Random.Range(2.0f, 4.0f))
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}