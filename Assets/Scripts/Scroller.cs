using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] RawImage pattern;
    [SerializeField] float speed;

    void Update()
    {
        pattern.uvRect = new Rect(
            pattern.uvRect.position + new Vector2(speed, speed) * Time.deltaTime,
            pattern.uvRect.size);
    }
}