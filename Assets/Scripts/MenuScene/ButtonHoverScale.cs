using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform buttonTransform;
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f); // Goal scale
    private Vector3 originalScale;

    void Start()
    {
        // Save the original scale
        if (buttonTransform == null)
        {
            buttonTransform = GetComponent<RectTransform>();
        }
        originalScale = buttonTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Set the goal scale
        buttonTransform.localScale = hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Reset the scale
        buttonTransform.localScale = originalScale;
    }
}
