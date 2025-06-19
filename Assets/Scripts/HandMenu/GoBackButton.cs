using UnityEngine;
using UnityEngine.SceneManagement; // Required to manage scenes
using UnityEngine.UI; // Required for Button interaction

public class GoBackButton : MonoBehaviour
{
    [SerializeField] private Button goBackButton; // Reference to the button
    [SerializeField] private string sceneToLoad; // Name of the scene to load

    void Start()
    {
        if (goBackButton != null)
        {
            // Add listener to the button
            goBackButton.onClick.AddListener(OnGoBackButtonClick);
        }
        else
        {
            Debug.LogError("Go Back Button is not assigned in the Inspector!");
        }
    }

    // Method to handle button click
    private void OnGoBackButtonClick()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is not set!");
        }
    }
}
