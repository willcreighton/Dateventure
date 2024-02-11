using DG.Tweening;
using UnityEngine;

public class HeartAttachable : MonoBehaviour
{
    // Smoothly animate the Heart Prefab
    void Start()
    {
        // Find Canvas and its RectTransform
        Canvas canvas = FindObjectOfType<Canvas>();
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();

        // Define animation parameters
        float offset = 72.0f;
        float endPos = (canvasRectTransform.rect.height / 2) + offset;
        float minRandomBounds = 2.0f;
        float maxRandomBounds = 4.0f;

        // Play the animation, destroying the Heart on completion
        transform.DOLocalMoveY(endPos, Random.Range(minRandomBounds, maxRandomBounds))
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}