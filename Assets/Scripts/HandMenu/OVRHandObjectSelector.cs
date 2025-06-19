using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OVRHandObjectSelector : MonoBehaviour
{
    public OVRHand ovrHand;
    public LineRenderer rayLineRenderer;
    public LayerMask interactableLayer;
    public TMP_Text currentGameObjectNameText;
    public Button deleteButton;
    public Slider rotationSliderX;
    public Slider rotationSliderY;
    public Slider rotationSliderZ;
    public TMP_Text sliderXValueText;
    public TMP_Text sliderYValueText;
    public TMP_Text sliderZValueText;
    public Material highlightMaterial;

    private GameObject selectedObject;
    private Renderer selectedObjectRenderer;
    private Material originalMaterial;
    private bool isRayAlwaysVisible = false; // Flag to control ray visibility mode

    private void Start()
    {
        if (deleteButton != null)
            deleteButton.onClick.AddListener(DeleteSelectedObject);

        SetupRotationSlider(rotationSliderX, sliderXValueText);
        SetupRotationSlider(rotationSliderY, sliderYValueText);
        SetupRotationSlider(rotationSliderZ, sliderZValueText);

        if (rayLineRenderer != null)
        {
            rayLineRenderer.enabled = false;
        }
    }

    private void Update()
    {
        HandleRaycastSelection();
    }

    private void HandleRaycastSelection()
    {
        // Create a ray from the hand's pinch point
        Ray ray = new Ray(ovrHand.PointerPose.position, ovrHand.PointerPose.forward);
        RaycastHit hit;

        // Check if the ray hits an object in the interactable layer
        if (Physics.Raycast(ray, out hit, 10, interactableLayer))
        {
            // Draw the line renderer when hitting an object
            rayLineRenderer.enabled = true;
            rayLineRenderer.SetPosition(0, ray.origin);
            rayLineRenderer.SetPosition(1, hit.point);

            // Check if the user is pinching
            if (ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index))
            {
                if (selectedObject != hit.collider.gameObject)
                {
                    DeselectObject();
                    SelectObject(hit.collider.gameObject);
                }
            }
        }
        else
        {
            // Control ray visibility based on the toggle state
            rayLineRenderer.enabled = isRayAlwaysVisible;
            if (isRayAlwaysVisible)
            {
                rayLineRenderer.SetPosition(0, ray.origin);
                rayLineRenderer.SetPosition(1, ray.origin + ray.direction * 10);
            }
        }
    }

    private void SelectObject(GameObject obj)
    {
        selectedObject = obj;
        selectedObjectRenderer = selectedObject.GetComponent<Renderer>();
        if (selectedObjectRenderer != null)
        {
            originalMaterial = selectedObjectRenderer.material;
            selectedObjectRenderer.material = highlightMaterial;
        }
        if (currentGameObjectNameText != null)
        {
            currentGameObjectNameText.text = selectedObject.name;
        }

        // Set the sliders to reflect the child's current rotation
        if (selectedObject.transform.childCount > 0)
        {
            Transform firstChild = selectedObject.transform.GetChild(0);
            Vector3 currentRotation = firstChild.eulerAngles;
            rotationSliderX.value = currentRotation.x;
            rotationSliderY.value = currentRotation.y;
            rotationSliderZ.value = currentRotation.z;
            UpdateSliderText(sliderXValueText, currentRotation.x);
            UpdateSliderText(sliderYValueText, currentRotation.y);
            UpdateSliderText(sliderZValueText, currentRotation.z);
        }

        // Activate the "Selected" child object within the second child
        Transform secondChild = selectedObject.transform.GetChild(0);
        Transform selectedChild = secondChild.Find("Selected");
        if (selectedChild != null)
        {
            selectedChild.gameObject.SetActive(true);
        }
    }

    private void DeselectObject()
    {
        if (selectedObjectRenderer != null && originalMaterial != null)
        {
            selectedObjectRenderer.material = originalMaterial;
        }

        // Deactivate the child object named "Selected"
        if (selectedObject != null)
        {
            Transform secondChild = selectedObject.transform.GetChild(0);
            Transform selectedChild = secondChild.Find("Selected");
            if (selectedChild != null)
            {
                selectedChild.gameObject.SetActive(false);
            }
        }

        selectedObject = null;
        selectedObjectRenderer = null;
        originalMaterial = null;
        if (currentGameObjectNameText != null)
        {
            currentGameObjectNameText.text = "Kein Element";
        }
    }

    private void DeleteSelectedObject()
    {
        if (selectedObject != null)
        {
            Destroy(selectedObject);
            DeselectObject();
        }
    }

    private void SetupRotationSlider(Slider slider, TMP_Text valueText)
    {
        if (slider != null)
        {
            slider.minValue = 0;
            slider.maxValue = 360;
            slider.onValueChanged.AddListener((value) => OnRotationSliderChanged(slider, value, valueText));
        }
    }

    private void OnRotationSliderChanged(Slider slider, float value, TMP_Text valueText)
    {
        if (selectedObject != null && selectedObject.transform.childCount > 0)
        {
            Transform firstChild = selectedObject.transform.GetChild(0);
            Vector3 currentRotation = firstChild.eulerAngles;

            // Update the rotation based on the slider that was changed
            if (slider == rotationSliderX)
                currentRotation.x = value;
            else if (slider == rotationSliderY)
                currentRotation.y = value;
            else if (slider == rotationSliderZ)
                currentRotation.z = value;

            firstChild.rotation = Quaternion.Euler(currentRotation);
        }

        UpdateSliderText(valueText, value);
    }

    private void UpdateSliderText(TMP_Text text, float value)
    {
        if (text != null)
        {
            text.text = value.ToString("F1") + "°"; // Show value with one decimal and a degree symbol
        }
    }

    // Function to toggle ray visibility mode
    public void ToggleRayVisibility()
    {
        isRayAlwaysVisible = !isRayAlwaysVisible;
    }
}
