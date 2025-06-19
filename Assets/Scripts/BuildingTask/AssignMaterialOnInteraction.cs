using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignMaterialOnInteraction : MonoBehaviour
{
    [SerializeField]
    public Renderer targetRenderer; 
    [SerializeField]
    public Material materialToAssign; 
    private Material originalMaterial;

    // Method to assign the material
    public void AssignMaterial()
    {
        if (targetRenderer == null || materialToAssign == null)
        {
            Debug.LogWarning("Target Renderer or Material not assigned!");
            return;
        }

        originalMaterial = targetRenderer.material;

        targetRenderer.material = materialToAssign;
        Debug.Log($"Material '{materialToAssign.name}' assigned to '{targetRenderer.gameObject.name}'");
    }

    // Method to reset the material
    public void ResetMaterial()
    {
        if (targetRenderer == null || originalMaterial == null)
        {
            Debug.LogWarning("Target Renderer or original material not assigned!");
            return;
        }

        targetRenderer.material = originalMaterial;
        Debug.Log($"Material reset to origin of '{targetRenderer.gameObject.name}'");
    }
}
