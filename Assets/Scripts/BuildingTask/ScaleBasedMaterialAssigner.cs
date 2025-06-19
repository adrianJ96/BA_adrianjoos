using UnityEngine;

public class ScaleBasedMaterialAssigner : MonoBehaviour
{
    [Header("Target Scale Factors")]
    public float scaleFactorX = 2.0f; 
    public float scaleFactorY = 2.0f; 
    public float scaleFactorZ = 2.0f; 

    [Header("Return Thresholds")]
    public float returnThresholdX = 0.2f; 
    public float returnThresholdY = 0.2f; 
    public float returnThresholdZ = 0.2f; 

    [Header("Material Settings")]
    public Material targetMaterial; 

    [Header("Optional Settings")]
    public bool checkContinuously = true; 

    [Header("Debug Settings")]
    public bool enableDebugLogs = true; 

    private Renderer objectRenderer;
    private Material originalMaterial; 
    private Vector3 initialScale; 
    private Vector3 targetScale; 
    private Vector3 lowerThresholdScale; 
    private bool usingTargetMaterial = false;

    void Start()
    {
        // Get the Renderer of the GameObject 
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer == null)
        {
            Debug.LogError("No Renderer found.");
            enabled = false;
            return;
        }

        // Save the original material 
        originalMaterial = objectRenderer.material;

        // Calculate the target scale
        initialScale = transform.localScale;
        targetScale = new Vector3(
            initialScale.x * scaleFactorX,
            initialScale.y * scaleFactorY,
            initialScale.z * scaleFactorZ
        );

        // Calcuate the lower threshold scale
        lowerThresholdScale = new Vector3(
            targetScale.x - returnThresholdX,
            targetScale.y - returnThresholdY,
            targetScale.z - returnThresholdZ
        );

        if (enableDebugLogs)
        {
            Debug.Log($"Initial Scale: {initialScale}, Target Scale: {targetScale}, Lower Threshold Scale: {lowerThresholdScale}");
        }

        CheckAndAssignMaterial();
    }

    void Update()
    {
        // Check the scale continuously
        if (checkContinuously)
        {
            CheckAndAssignMaterial();
        }
    }

    private void CheckAndAssignMaterial()
    {
        Vector3 currentScale = transform.localScale;

        bool xAboveTarget = currentScale.x >= targetScale.x;
        bool yAboveTarget = currentScale.y >= targetScale.y;
        bool zAboveTarget = currentScale.z >= targetScale.z;

        bool xBelowThreshold = currentScale.x <= lowerThresholdScale.x;
        bool yBelowThreshold = currentScale.y <= lowerThresholdScale.y;
        bool zBelowThreshold = currentScale.z <= lowerThresholdScale.z;

        // Assign target material
        if (xAboveTarget && yAboveTarget && zAboveTarget && !usingTargetMaterial)
        {
            if (enableDebugLogs)
            {
                Debug.Log($"Current Scale: {currentScale} has reached or exceeded Target Scale: {targetScale}");
            }

            AssignMaterial(targetMaterial);
        }
        // Reset to original material
        else if (xBelowThreshold || yBelowThreshold || zBelowThreshold)
        {
            bool resetMaterial = true;

            if (!xBelowThreshold && scaleFactorX == 1.0f) resetMaterial = false;
            if (!yBelowThreshold && scaleFactorY == 1.0f) resetMaterial = false;
            if (!zBelowThreshold && scaleFactorZ == 1.0f) resetMaterial = false;

            if (resetMaterial && usingTargetMaterial)
            {
                if (enableDebugLogs)
                {
                    Debug.Log($"Current Scale: {currentScale} has fallen below Lower Threshold Scale: {lowerThresholdScale}");
                }

                AssignMaterial(originalMaterial);
            }
        }
        else if (enableDebugLogs)
        {
            Debug.Log($"Current Scale: {currentScale} is between thresholds. No material change.");
        }
    }

    private void AssignMaterial(Material material)
    {
        if (material != null && objectRenderer != null)
        {
            objectRenderer.material = material;
            usingTargetMaterial = (material == targetMaterial);
            if (enableDebugLogs)
            {
                Debug.Log(usingTargetMaterial ? "Target Material assigned." : "Original Material restored.");
            }
        }
        else
        {
            Debug.LogError("Material or Renderer missing.");
        }
    }
}
