using DG.Tweening;
using UnityEngine;

public class HeartAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();

        transform.DOLocalMoveY((canvasRectTransform.rect.height / 2) + 72, Random.Range(2.0f, 4.0f))
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}