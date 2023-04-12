using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
// This is the main web script. Here is a list of our players, 
// How they join/disconnect from the game, adding new players to the player list      
    public static event Action ClientOnConnected;
    public static event Action ClientOnDisconnected;
    private bool isGameInProgress = false; // This variable is created to prevent the players from joining the game, when it's in progress
    public List<MyNetworkPlayer> players { get; } = new List<MyNetworkPlayer>(); // The list of our players
                                                                                 // We use it in the GameScript to recognise our players

    #region Serwer
    public override void OnServerConnect(NetworkConnection conn  )
    {   
        if (!isGameInProgress)  { return; } // Prevent from joining when the game is in progress
        base.OnServerConnect (conn);
        conn.Disconnect();
    }
    // What heppens when a client disconnects from the server.
    public override void OnServerDisconnect(NetworkConnection conn)
    { 
        MyNetworkPlayer player =  conn.identity.GetComponent<MyNetworkPlayer>(); // When we disconnect, let's refer to our list of players
        players.Remove(player);                                                  // ... and remove that player from the list
        base.OnServerDisconnect(conn);
        
    }
    public override void OnStopServer()
    // What happens when we stop running the server(hosting the game)
    {    
        players.Clear();    // Clear all the players from our player list
        isGameInProgress = false; // Game is no longer marked as "In Progress"
    }
    public void StartGame()
    {   
        if(players.Count < 3){return;} // In order to start the game, we have to have 3 players in our player list
        isGameInProgress = true;
        ServerChangeScene("GameScene"); // If there are 3 players and the host press "Start Game" button
                                        // Change the scane to "GameScene"
                         
    }
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
    base.OnServerAddPlayer(conn);
    //reference to network player
    MyNetworkPlayer player =  conn.identity.GetComponent<MyNetworkPlayer>();

    players.Add(player);
    // here I assign player's name base on joining order
    player.SetDisplayName($"Player {players.Count}");
    // Debug.Log("Player conn ID: " + conn.connectionId);
    //here we dclare who will be a party owner
    // if there is only one player he will be a party owner
    player.SetPartyOwner(players.Count == 1);                              
    // Debug.Log($"There are now {numPlayers} players");
    
    }

    //When a client stops, we shold also clear the player's list
    public override void OnStopClient()
    {   
        players.Clear();
    }

    #endregion

    #region Client
   [System.Obsolete]
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        ClientOnConnected?.Invoke();
        
    }
    [System.Obsolete]
    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        
        ClientOnDisconnected?.Invoke();
    }
    #endregion
}
