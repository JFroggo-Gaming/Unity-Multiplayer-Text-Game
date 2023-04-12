using UnityEngine;
using Mirror;

public class MainMenu : MonoBehaviour
{
// IN THIS SCRIPT: The logic of what happens when, as a player, we click the "Host Lobby" button in the main menu.
// We want to close unnecessary UI elements and start hosting the game
    [SerializeField] private GameObject MainLandingPage_Panel = null;
    [SerializeField] private GameObject LobbyPage = null;
    [SerializeField] private GameObject EnterAddress = null;
    [SerializeField] private GameObject OptionsPanel = null;
    [SerializeField] private GameObject ExitPanel = null;

    public void HostLobby()
    {   
        LobbyPage.SetActive(true);

        MainLandingPage_Panel.SetActive(false);

           if(EnterAddress.activeSelf)
        {
            EnterAddress.SetActive(false);
        }
      
        OptionsPanel.SetActive(false);

           if(OptionsPanel.activeSelf)
        {
            OptionsPanel.SetActive(false);
        }
      
         ExitPanel.SetActive(false);

           if(ExitPanel.activeSelf)
        {
            ExitPanel.SetActive(false);
        }
        NetworkManager.singleton.StartHost();
    }

}
