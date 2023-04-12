using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;
public class LobbyMenu : MonoBehaviour

{// IN THIS SCRIPT: The "start game" button is only active for the lobby host
 // Changing the UI in the lobby when people are connected / not connected yet 
 // 
   [SerializeField] private GameObject lobbyUI = null;
   [SerializeField] private Button startGameButton = null;

   // That is the array that will store player's names of maximum of 3 players
   [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[3];
   [SerializeField] private Image[] OldLobbyImage = new Image[3];
   [SerializeField] private Image[] NewLobbyImage = new Image[3];
   
   private void Start()
   {    
       // As a client when you connect, we want to say handle client connected
       MyNetworkManager.ClientOnConnected += HandleClientConnected;
       // When the game starts we assign the authority to a player
       MyNetworkPlayer.AuthorityOnPartyOwnerStateUpdated += AuthorityHandlePartyOwnerStateUpdated;
       // As we set the players name and set the events for that, we have to
       // hook into that and actually use it in the user interface
       MyNetworkPlayer.ClientOnInfoUpdated += ClientHandleInfoUpdated;
   }
    private void OnDestroy() {

        MyNetworkManager.ClientOnConnected -= HandleClientConnected;
        MyNetworkPlayer.AuthorityOnPartyOwnerStateUpdated -= AuthorityHandlePartyOwnerStateUpdated;
        MyNetworkPlayer.ClientOnInfoUpdated -= ClientHandleInfoUpdated;
    }
    private void HandleClientConnected()
    {   // Set the lobby on (turning on the UI)
        lobbyUI.SetActive(true);
    }
    // Here is the method to handle player authority written in the start
    private void ClientHandleInfoUpdated()
    {   // We need a list of our players and we can get that from network manager
        List<MyNetworkPlayer> players = ((MyNetworkManager)NetworkManager.singleton).players;
        // If no one is connected - > Waiting for player
        // If there is new person in the lobby -> replace the old ". . ." with his displayed name
        for (int i = 0 ; i < players.Count; i++)
        {   OldLobbyImage[i].enabled = false;       // Here depending on if we are connected or not, we set an array of old images(quesiton marks)
                                                    // to be invisible, and we want them to be replace by newImages when someone join.
            NewLobbyImage[i].enabled = true;

            playerNameTexts[i].text = players[i].GetDisplayName();
        }
        // If someone leave the lobby, we have to update their name back to ". . ."
        for (int i = players.Count; i < playerNameTexts.Length; i++)

        {   
            OldLobbyImage[i].enabled = true;            
                                                        // Change the images back again and set the player name to ". . ."
            NewLobbyImage[i].enabled = false;
            playerNameTexts[i].text = ". . .";
        }
        // We can help the host to know when they can start the game, we can actually disable
        // the button if they are no enough players
        startGameButton.interactable = players.Count >= 3; // If there are at least three players
                                                           // the start button should be enabled to press

    }
    private void AuthorityHandlePartyOwnerStateUpdated(bool state) // If we are the host, the start game button will be active for us
    {
       startGameButton.gameObject.SetActive(state);
    }
   
    // What happens, we the host press "Start Game" button
    // "Grab" the connection and start the game.
    public void StartGame()
    {
        NetworkClient.connection.identity.GetComponent<MyNetworkPlayer>().CmdStartGame();
    }
  

  public void LeaveLobby()  // Here we can type what do we want to do when we disconnect from the game as a host or client
    {                   
        if(NetworkServer.active)
        {  
             MyNetworkManager.singleton.StopHost(); // When you have the lobby and you stop hosting, other players will be disconnected                          
        }
        if(NetworkServer.active && NetworkClient.isConnected) // When we stop hosting as a client
        {
            MyNetworkManager.singleton.StopClient();
        }

    }

}
