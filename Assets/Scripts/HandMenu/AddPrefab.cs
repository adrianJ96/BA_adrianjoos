using UnityEngine;

public class AddPrefab : MonoBehaviour
{
    // Reference to the prefab to be assigned  
    public OVRSpatialAnchor anchorPrefab;
    public GameObject previewPrefab;

    // Public function to assign the prefab to the PrefabPlacer script  
    public void AssignAnchorPrefab(PrefabPlacer prefabPlacer)
    {
        if (prefabPlacer != null)
        {
            // Assign the anchorPrefab from this script to the PrefabPlacer script  
            prefabPlacer.anchorPrefab = anchorPrefab;
            prefabPlacer.previewPrefab = previewPrefab;
            Debug.Log("Anchor prefab successfully assigned to PrefabPlacer.");
        }
        else
        {
            Debug.LogError("PrefabPlacer reference is null. Please assign a valid PrefabPlacer.");
        }
    }
}