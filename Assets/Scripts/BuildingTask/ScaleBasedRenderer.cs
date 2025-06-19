using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBasedRenderer : MonoBehaviour
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

    private Renderer[] childRenderers; 
    private Material[] originalMaterials; 
    private Vector3 initialScale; 
    private Vector3 targetScale; 
    private Vector3 lowerThresholdScale; 
    private bool usingTargetMaterial = false;

    void Start()
    {
        // Get the Renderer of the GameObject 
        childRenderers = GetComponentsInChildren<Renderer>();

        if (childRenderers.Length == 0)
        {
            Debug.LogError("Keine Renderer gefunden. Bitte stellen Sie sicher, dass dieses GameObject oder seine Kinder Renderer haben.");
            enabled = false;
            return;
        }

        // Save the original material
        originalMaterials = new Material[childRenderers.Length];
        for (int i = 0; i < childRenderers.Length; i++)
        {
            originalMaterials[i] = childRenderers[i].material;
        }

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

                AssignMaterialToOriginal();
            }
        }
        else if (enableDebugLogs)
        {
            Debug.Log($"Current Scale: {currentScale} is between thresholds. No material change.");
        }
    }

    private void AssignMaterial(Material material)
    {
        if (material != null)
        {
            foreach (var renderer in childRenderers)
            {
                renderer.material = material;
            }
            usingTargetMaterial = true;
            if (enableDebugLogs)
            {
                Debug.Log("Target Material assigned.");
            }
        }
    }

    private void AssignMaterialToOriginal()
    {
        for (int i = 0; i < childRenderers.Length; i++)
        {
            childRenderers[i].material = originalMaterials[i];
        }
        usingTargetMaterial = false;
        if (enableDebugLogs)
        {
            Debug.Log("Original Material restored.");
        }
    }
}
