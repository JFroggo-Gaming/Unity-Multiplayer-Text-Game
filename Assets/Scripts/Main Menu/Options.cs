using UnityEngine;

public class Options : MonoBehaviour
{
// IN THIS SCRIPT: This script handles UI elements and makes sure that the options panel doesn't overlap or collide with others
// This script is separeted for LobbyScene, as it contains more UI elements to handle
    [SerializeField] private GameObject LobbyParent = null;
    [SerializeField] private GameObject LobbyPanelLeft = null;
    [SerializeField] private GameObject LobbyPanelRight = null;
    [SerializeField] private GameObject EnterAddress = null; 
    [SerializeField] private GameObject ConnectionInfoPanel = null;
    [SerializeField] private GameObject IncorrectAddressPanel = null;
    [SerializeField] private GameObject ExitPanel = null;
    [SerializeField] private GameObject OptionsPanel = null;

    private void OptionsUI() // Execute this, when "OPTIONS" button in LobbyScene is pressed
    {
      
        OptionsPanel.SetActive(true);

        EnterAddress.SetActive(false);

        if(EnterAddress.activeSelf)
        {
            EnterAddress.SetActive(false);
        }
        ExitPanel.SetActive(false);

        if(ExitPanel.activeSelf)
        {
            ExitPanel.SetActive(false);
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
    private void ExitButton()
    {  
       if(LobbyPanelLeft.activeSelf && !LobbyPanelRight.activeSelf){
        LobbyPanelRight.SetActive(true);
       }
    }
}

 