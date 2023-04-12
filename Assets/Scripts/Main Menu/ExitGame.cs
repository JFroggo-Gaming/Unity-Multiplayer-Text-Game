using UnityEngine;

public class ExitGame : MonoBehaviour
{
// IN THIS SCRIPT: This script handles UI elements and makes sure that the exit panel doesn't overlap or collide with others
// It also contains the function to close the game
// This script is separeted for LobbyScene, as it contains more UI elements to handle
    [SerializeField] private GameObject EnterAddress = null;
    [SerializeField] private GameObject ConnectionInfoPanel = null;
    [SerializeField] private GameObject IncorrectAddressPanel = null;
    [SerializeField] private GameObject ExitPanel = null;
    [SerializeField] private GameObject OptionsPanel = null;
    [SerializeField] private GameObject LobbyParent = null;
    [SerializeField] private GameObject LobbyPanelLeft = null;
    [SerializeField] private GameObject LobbyPanelRight = null; 

    private void ExitGameUI()
    {
        ExitPanel.SetActive(true);

        EnterAddress.SetActive(false);

        if(EnterAddress.activeSelf)
        {
            EnterAddress.SetActive(false);
        }
     
        OptionsPanel.SetActive(false);

        if(OptionsPanel.activeSelf)
        {
            OptionsPanel.SetActive(false);
        }
        
        if (LobbyParent.activeSelf)
        {
            LobbyPanelRight.SetActive(false);
        }
    
        if(ConnectionInfoPanel.activeSelf){
        ConnectionInfoPanel.SetActive(false);
        }
        if(IncorrectAddressPanel.activeSelf)
        {
        IncorrectAddressPanel.SetActive(false);
        }
    
    }
  
    private void ExitButton() // When a player press the X button to close the UI, bring back and display everything according to the code below.
                              // (bringing back the lobby panel, if the exit button is pressed in lobby)
    {  
       if(LobbyPanelLeft.activeSelf && !LobbyPanelRight.activeSelf){
        LobbyPanelRight.SetActive(true);
       }
    }
    private void QuiteTheGame()
    {
        Application.Quit();
    }
}
