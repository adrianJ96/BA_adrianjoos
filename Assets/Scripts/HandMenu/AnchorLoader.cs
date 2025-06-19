using System;
using TMPro;
using UnityEngine;

/// <summary>  
/// Handles loading and managing spatial anchors.  
/// </summary>  
public class AnchorLoader : MonoBehaviour
{
    private OVRSpatialAnchor anchorPrefab; // The prefab for spatial anchors  
    private PrefabPlacer spatialAnchorManager; // Manager for spatial anchors  
    private Action<OVRSpatialAnchor.UnboundAnchor, bool> _onLoadAnchor; // Callback for when an anchor is localized  

    // Unity's Awake method, called when the script instance is being loaded  
    private void Awake()
    {
        // Get the SpatialAnchorManager component from the GameObject  
        spatialAnchorManager = GetComponent<PrefabPlacer>();

        // Assign the anchor prefab from the manager  
        anchorPrefab = spatialAnchorManager.anchorPrefab;

        // Assign the OnLocalized callback to the local variable  
        _onLoadAnchor = OnLocalized;
    }

    /// <summary>  
    /// Loads all available spatial anchors.  
    /// </summary>  
    public void LoadAllAnchors()
    {
        // Call Load with default options to load all anchors  
        Load(new OVRSpatialAnchor.LoadOptions
        {
            Timeout = 0,
            StorageLocation = OVRSpace.StorageLocation.Local,
        });
    }

    /// <summary>  
    /// Loads spatial anchors based on the provided options.  
    /// </summary>  
    /// <param name="options">Options for loading spatial anchors.</param>  
    private void Load(OVRSpatialAnchor.LoadOptions options)
    {
        OVRSpatialAnchor.LoadUnboundAnchors(options, anchors =>
        {
            // If no anchors are found, return early  
            if (anchors == null)
            {
                return;
            }

            // Iterate through all the anchors  
            foreach (var anchor in anchors)
            {
                // If the anchor is already localized, invoke the callback directly  
                if (anchor.Localized)
                {
                    _onLoadAnchor(anchor, true);
                }
                // If the anchor is not localizing, attempt to localize it  
                else if (!anchor.Localizing)
                {
                    anchor.Localize(_onLoadAnchor);
                }
            }
        });
    }

    /// <summary>  
    /// Callback invoked when an anchor has been localized.  
    /// </summary>  
    /// <param name="unboundAnchor">The unbound anchor that was localized.</param>  
    /// <param name="success">Indicates if the localization was successful.</param>  
    private void OnLocalized(OVRSpatialAnchor.UnboundAnchor unboundAnchor, bool success)
    {
        // If localization failed, return early  
        if (!success) return;

        // Get the pose of the unbound anchor  
        var pose = unboundAnchor.Pose;

        // Instantiate the spatial anchor prefab at the pose's position and rotation  
        var spatialAnchor = Instantiate(anchorPrefab, pose.position, pose.rotation);

        // Bind the unbound anchor to the instantiated spatial anchor  
        unboundAnchor.BindTo(spatialAnchor);

        // Try to get the OVRSpatialAnchor component from the instantiated anchor  
        if (spatialAnchor.TryGetComponent<OVRSpatialAnchor>(out var anchor))
        {
            // Retrieve TextMeshPro components for displaying status information  
            var statusText = spatialAnchor.GetComponentInChildren<TextMeshProUGUI>();

            // Update the displayed text with the load status  
            statusText.text = "Loaded from Device";
        }
    }
}