using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meta.XR.MRUtilityKit;
using Meta.XR.MRUtilityKit.SceneDecorator;



public class RoomManager : MonoBehaviour
{
    // Assign these buttons in the Inspector
    public Button deleteAllPropsButton;
    

    private void Start()
    {
        // Attach button click listeners
        if (deleteAllPropsButton != null)
            deleteAllPropsButton.onClick.AddListener(DeleteAllProps);

        
    }

    /// <summary>
    /// Deletes all GameObjects in the scene that have the layer named "Props".
    /// </summary>
    private void DeleteAllProps()
    {
        int propsLayer = LayerMask.NameToLayer("Props");

        if (propsLayer == -1)
        {
            Debug.LogError("Layer 'Props' does not exist in the project settings.");
            return;
        }

        // Find all objects in the scene
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == propsLayer)
            {
                Destroy(obj);
            }
        }

        Debug.Log("All objects with the 'Props' layer have been destroyed.");
    }

    public void RequestSpaceSetupManual()
    {
        _ = OVRScene.RequestSpaceSetup();
        _ = MRUK.Instance.LoadSceneFromDevice(false);
    }

}
