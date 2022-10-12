using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileSafeArea : MonoBehaviour
{
    // this script takes the safe area of the device into account when determining canvas size
    // Unity has an automatic function to do this for android but this script will be needed for iOS (that might change in the future)

    RectTransform rectTransform;
    Rect safeArea;

    Vector2 minAnchor;
    Vector2 maxAnchor;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        safeArea = Screen.safeArea;
        minAnchor = safeArea.position;
        maxAnchor = minAnchor + safeArea.size;

        // minimum anchor
        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;

        // maximum anchor
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        // assign min/max anchor points to the rect transform
        rectTransform.anchorMin = minAnchor;
        rectTransform.anchorMax = maxAnchor;
    }
}
