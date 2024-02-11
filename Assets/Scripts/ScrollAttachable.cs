using UnityEngine;
using UnityEngine.UI;

public class ScrollAttachable : MonoBehaviour
{
    // Define the pattern and speed for the scrolling effect
    [SerializeField] RawImage pattern;
    [SerializeField] float scrollSpeed;

    // Perform the scrolling effect each frame
    void Update()
    {
        // Calculate the offset based on scrollSpeed and deltaTime
        Vector2 offset = new Vector2(scrollSpeed, scrollSpeed) * Time.deltaTime;

        // Update the pattern's uvRect to achieve scrolling effect
        pattern.uvRect = new Rect(
            pattern.uvRect.position + offset,
            pattern.uvRect.size);
    }
}