using Meta.XR.MRUtilityKit;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPlacer : MonoBehaviour
{
    public OVRSpatialAnchor anchorPrefab; // Prefab for spatial anchors  
    public GameObject previewPrefab; // Prefab for the preview object  
    private Canvas canvas; // Optional: Canvas reference for UI  
    private List<OVRSpatialAnchor> anchors = new List<OVRSpatialAnchor>(); // List of spatial anchors  
    private OVRSpatialAnchor lastCreatedAnchor; // Reference to the last created anchor  
    private AnchorLoader anchorLoader; // Component for loading anchors  
    private bool isInitialized; // Flag to check initialization status  

    public OVRHand leftHand; // Reference to the left hand  
    public OVRHand rightHand; // Reference to the right hand  

    private GameObject previewInstance; // Instance of the preview prefab  
    private bool isPreviewing = false; // Flag to track if preview is active  
    private Vector3 lastStableHitPoint; // Store the last stable hit position  
    private Vector3 lastStableHitNormal; // Store the last stable hit normal  

    // Property to set or get initialization  
    public bool Initialized
    {
        get => isInitialized;
        set => isInitialized = value;
    }

    private void Awake()
    {
        anchorLoader = GetComponent<AnchorLoader>();
    }

    private void Update()
    {
        if (!isInitialized) return;

        HandleHandTrackingInput();
    }

    private bool wasPinching = false; // Tracks the previous pinch state  

    private void HandleHandTrackingInput()
    {
        // Check if the user is pinching with their right hand  
        bool isPinching = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);

        // Get the hand's position and direction for raycasting  
        Vector3 handPosition = rightHand.PointerPose.position;
        Vector3 handDirection = rightHand.PointerPose.forward;

        // Define a layer mask to ignore "Props" but specifically check for "UI"  
        int ignoreLayerMask = ~LayerMask.GetMask("Props"); // Exclude "Props" layer  

        Ray ray = new Ray(handPosition, handDirection);
        RaycastHit hit;

        // Perform the raycast while ignoring specified layers  
        Debug.DrawLine(handPosition, handPosition + handDirection * 10f, Color.blue);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ignoreLayerMask))
        {
            Debug.DrawLine(handPosition, hit.point, Color.yellow);

            // Check if the hit object is on the "UI" layer  
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                // Cancel any active preview if interacting with UI  
                if (isPreviewing)
                {
                    CancelPreview();
                }
                return; // Exit early to prevent further logic execution  
            }

            // Stabilize the hit point and normal to avoid jitter  
            lastStableHitPoint = hit.point;
            lastStableHitNormal = hit.normal;

            // Start the preview if it's not already active  
            if (isPinching && !wasPinching)
            {
                if (!isPreviewing)
                {
                    StartPreview(lastStableHitPoint, lastStableHitNormal);
                }
            }

            // Update the preview's position and rotation while pinching  
            if (isPreviewing)
            {
                UpdatePreview(lastStableHitPoint, lastStableHitNormal);
            }
        }
        else
        {
            // Cancel the preview if the raycast does not hit a surface  
            if (isPreviewing)
            {
                CancelPreview();
            }
        }

        // Place the anchor when the pinch ends  
        if (!isPinching && wasPinching)
        {
            if (isPreviewing)
            {
                PlaceAnchor(previewInstance.transform.position, previewInstance.transform.up);
            }
        }

        // Update the previous pinch state  
        wasPinching = isPinching;
    }

    private void StartPreview(Vector3 position, Vector3 normal)
    {
        if (previewPrefab == null) return;

        Quaternion rotation = CalculatePerpendicularRotation(normal);
        previewInstance = Instantiate(previewPrefab, position, rotation);
        isPreviewing = true;
    }

    private void UpdatePreview(Vector3 position, Vector3 normal)
    {
        if (previewInstance == null) return;

        Quaternion rotation = CalculatePerpendicularRotation(normal);
        previewInstance.transform.position = position;
        previewInstance.transform.rotation = rotation;
    }

    private void CancelPreview()
    {
        if (previewInstance != null)
        {
            Destroy(previewInstance);
            previewInstance = null;
        }
        isPreviewing = false;
    }

    private void PlaceAnchor(Vector3 position, Vector3 normal)
    {
        if (previewInstance != null)
        {
            Destroy(previewInstance);
            previewInstance = null;
        }
        isPreviewing = false;

        Quaternion rotation = CalculatePerpendicularRotation(normal);
        CreateSpatialAnchor(position, rotation);
    }

    private void CreateSpatialAnchor(Vector3 position, Quaternion surfaceRotation)
    {
        if (anchorPrefab == null)
        {
            Debug.LogWarning("anchorPrefab is null. Cannot create spatial anchor.");
            return;
        }

        OVRSpatialAnchor newAnchor = Instantiate(anchorPrefab, position, surfaceRotation);
        anchors.Add(newAnchor);
        lastCreatedAnchor = newAnchor;

        anchorPrefab = null; // Reset prefab reference  
        previewPrefab = null;
    }

    private Quaternion CalculatePerpendicularRotation(Vector3 surfaceNormal)
    {
        Vector3 right = Vector3.Cross(Vector3.up, surfaceNormal);
        if (right == Vector3.zero)
        {
            right = Vector3.Cross(Vector3.forward, surfaceNormal);
        }
        Vector3 forward = Vector3.Cross(surfaceNormal, right);
        return Quaternion.LookRotation(forward, surfaceNormal);
    }
}