using UnityEngine;

public class SkySphereController : MonoBehaviour
{
    public Material[] skyMaterials;

    public Texture[] skyTextures;

    private Renderer sphereRenderer;

    private int currentIndex = 0;

    private bool useMaterials = true;

    void Start()
    {
        sphereRenderer = GetComponent<Renderer>();

        if (sphereRenderer == null)
        {
            Debug.LogError("Renderer missing!");
            return;
        }

        AssignCurrentSky();
    }

    // Option to switch between materials and textures
    public void ToggleMode()
    {
        useMaterials = !useMaterials;
        currentIndex = 0;
        AssignCurrentSky();

        Debug.Log($"Mode switched to: {(useMaterials ? "Materialien" : "Texturen")}");
    }

    // Methodes to switch in two different directions
    public void CycleSky()
    {
        if (useMaterials && skyMaterials.Length > 0)
        {
            currentIndex = (currentIndex + 1) % skyMaterials.Length;
        }
        else if (!useMaterials && skyTextures.Length > 0)
        {
            currentIndex = (currentIndex + 1) % skyTextures.Length;
        }

        AssignCurrentSky();
    }

    public void CycleSkyBackward()
    {
        if (useMaterials && skyMaterials.Length > 0)
        {
            currentIndex = (currentIndex - 1 + skyMaterials.Length) % skyMaterials.Length;
        }
        else if (!useMaterials && skyTextures.Length > 0)
        {
            currentIndex = (currentIndex - 1 + skyTextures.Length) % skyTextures.Length;
        }

        AssignCurrentSky();
    }

    // Assign the current sky to the sphere
    private void AssignCurrentSky()
    {
        if (useMaterials)
        {
            if (skyMaterials.Length > 0)
            {
                sphereRenderer.material = skyMaterials[currentIndex];
                Debug.Log($"Material gewechselt zu: {skyMaterials[currentIndex].name}");
            }
        }
        else
        {
            if (skyTextures.Length > 0)
            {
                sphereRenderer.material.mainTexture = skyTextures[currentIndex];
                Debug.Log($"Textur gewechselt zu: {skyTextures[currentIndex].name}");
            }
        }
    }
}
