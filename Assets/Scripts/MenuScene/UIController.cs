using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject rightCanvas;

    // Load scene by name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Show or hide second canvas
    public void ToggleRightCanvas()
    {
        if (rightCanvas != null)
        {
            rightCanvas.SetActive(!rightCanvas.activeSelf); // Wechselt zwischen Aktiv und Inaktiv
        }
    }
}