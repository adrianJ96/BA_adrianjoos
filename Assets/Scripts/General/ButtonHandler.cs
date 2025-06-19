using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject objectToShow;
    
    private void Start()
    {
        if (objectToShow == null)
        {
            objectToShow = FindInactiveObject("BuildingTask_1") ?? FindInactiveObject("BuildingTask_2");

            if (objectToShow == null)
                Debug.LogWarning("[ButtonHandler] GameObject could not be found!", this);
            else
                Debug.Log("[ButtonHandler] GameObject found: " + objectToShow.name, this);
        }
    }

    // Set object to active
    public void ShowObject()
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
            Debug.Log("[ButtonHandler] Object set to active: " + objectToShow.name, this);
        }
        else
        {
            Debug.LogError("[ButtonHandler] objectToShow is null!", this);
        }
    }

    // Search for inactive object by name
    private GameObject FindInactiveObject(string name)
    {
        Transform[] allObjects = Resources.FindObjectsOfTypeAll<Transform>(); // All objects, even inactive ones
        foreach (Transform obj in allObjects)
        {
            if (obj.name == name)
                return obj.gameObject;
        }
        return null;
    }
}
