using UnityEngine;

public class Options_GameScene : MonoBehaviour
{
// IN THIS SCRIPT: This script handles UI elements and makes sure that the options panel doesn't overlap or collide with others
// This script is separeted for GameScene, as it contains less UI elements to handle    
    [SerializeField] private GameObject ExitPanel = null;
    [SerializeField] private GameObject OptionsPanel = null;

    private void Options() // Execute when "OPTIONS" button in left top corner is pressed
    {
        OptionsPanel.SetActive(true);

        ExitPanel.SetActive(false);

        if(ExitPanel.activeSelf)
        {
        ExitPanel.SetActive(false);
        }
    }
}

 