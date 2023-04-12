using System.Collections;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
// The default server address is localhost. This script describes how we connect to game lobby
    [SerializeField] private GameObject landingPagepanel = null;
    [SerializeField] private GameObject LobbyOpen = null;
    [SerializeField] private TMP_InputField addressInput = null;
    [SerializeField] private Button joinButton = null;
    [SerializeField] private GameObject IncorrectPanel = null;

    private void OnEnable()
    {
        
        MyNetworkManager.ClientOnConnected += HandleClientConnected;    // We subscribe to methods depending on if we are connected to the server or not
                                                                        // depending on that (ClientOnDisconnected or ClientOnConnected we execute different methods)
        MyNetworkManager.ClientOnDisconnected += HandleClientDisconnected;
    }
    private void OnDisable()
    {
        MyNetworkManager.ClientOnConnected -= HandleClientConnected;
        MyNetworkManager.ClientOnDisconnected -= HandleClientDisconnected;
    }
    public void Join()  // When we press the "attach" button, the address we entered is checked.
                    
    {

        string address = addressInput.text;
        
        if(NetworkManager.singleton.networkAddress != address) // If not, we will receive a message about the wrong address
        {
            
         StartCoroutine(InvalidAddressInfo());

        }
        
        if(NetworkManager.singleton.networkAddress == address) // The default address is localhost. If the address entered is correct, we will connect to the lobby.
        {
            
         NetworkManager.singleton.StartClient();

        }
    
        joinButton.interactable = true;
       
    }


public  IEnumerator InvalidAddressInfo () {    
     IncorrectPanel.SetActive(true);                                      
     yield return new WaitForSeconds(5);
     IncorrectPanel.SetActive(false);
 }
private void HandleClientConnected() // If we are connected, display and close the appropriate interface elements
{   
    
    IncorrectPanel.SetActive(false);
    LobbyOpen.SetActive(true);
    joinButton.interactable = true;
    gameObject.SetActive(false);
    landingPagepanel.SetActive(false);
}
private void HandleClientDisconnected()
{
     joinButton.interactable = false;
}

}
