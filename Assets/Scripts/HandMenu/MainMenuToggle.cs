using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuToggle : MonoBehaviour
{
    [Header("References")]
    public GameObject MainMenu;
    public Image CollapseArrowImage;
    public TMP_Text ToggleLabel;

    [Header("Settings")]
    public string CollapseMenuText = "Collapse Menu";
    public string ExpandMenuText = "Expand Menu";

    private bool isMenuCollapsed = false; // Tracks the current state of the menu

    void Start()
    {
        // Initialize default states
        MainMenu.SetActive(true);
        CollapseArrowImage.rectTransform.rotation = Quaternion.Euler(0, 0, 180);
        ToggleLabel.text = CollapseMenuText;
        isMenuCollapsed = false;
    }

    public void ToggleMenu()
    {
        // Toggle the menu's active state
        isMenuCollapsed = !isMenuCollapsed;
        MainMenu.SetActive(!isMenuCollapsed);

        // Update the arrow rotation
        CollapseArrowImage.rectTransform.rotation = isMenuCollapsed
            ? Quaternion.Euler(0, 0, 0) // Expand
            : Quaternion.Euler(0, 0, 180); // Collapse

        // Update the label text
        ToggleLabel.text = isMenuCollapsed ? ExpandMenuText : CollapseMenuText;
    }
}
