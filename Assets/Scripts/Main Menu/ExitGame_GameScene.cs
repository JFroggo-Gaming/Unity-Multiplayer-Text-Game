using UnityEngine;

public class ExitGame_GameScene : MonoBehaviour
{
// IN THIS SCRIPT: This script handles UI elements and makes sure that the exit panel doesn't overlap or collide with others
// It also contains the function to close the game
// This script is separeted for GameScene, as this scene contains less UI elements to handle
    [SerializeField] private GameObject ExitPanel = null;
    [SerializeField] private GameObject OptionsPanel = null;

    private void ExitGamePanel()
    {
        ExitPanel.SetActive(true);
           if(OptionsPanel.activeSelf)
        {
            OptionsPanel.SetActive(false);
        }
    }
    private void QuiteTheGame()
    {
        Application.Quit();
    }
}
