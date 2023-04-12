using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Pings : NetworkBehaviour
{
// IN THIS SCRIT: Describes how to communicate with other players using arrows.
// Displays the appropriate UI based on arrow direction and which player is pressing it (1-3)
#region UI Elements
///////////////////////////////////////////////////////////////////////////////
// Three blue Arrows above the inventory
    [SerializeField] public Button LeftArrow;
    [SerializeField] public Button RightArrow;
    [SerializeField] public Button MiddleArrow;

///////////////////////////////////////////////////////////////////////////////
// Player's pings on the left. P1Middle -> Red player(middle) etc.
    [SerializeField] public GameObject P1Middle;
    [SerializeField] public GameObject P1Left;
    [SerializeField] public GameObject P1Right;
    [SerializeField] public GameObject P2Middle;
    [SerializeField] public GameObject P2Left;
    [SerializeField] public GameObject P2Right;    
    [SerializeField] public GameObject P3Middle;
    [SerializeField] public GameObject P3Left;
    [SerializeField] public GameObject P3Right;

///////////////////////////////////////////////////////////////////////////////
#endregion

#region Coroutines
// The Coroutines in order to display what option a player wants to vote for. Thanks to the Coroutines the UI elements are visible for few seconds.
    private IEnumerator P1LeftScript () {       // In this example Player One(the host) wants to vote for left button
    P1Left.SetActive(true);                     // SetActive for the panel with this information      // Disable other Arrows for X seconds(unable to click them)   
    yield return new WaitForSeconds(3);
    P1Left.SetActive(false);                    // After x seconds, the panel becomes invisible again
     
 }
    private IEnumerator P1RightScript () {    // Player One(RED) wants to go right etc.
    P1Right.SetActive(true);                                         
    yield return new WaitForSeconds(3);
    P1Right.SetActive(false);
     
 }
    private IEnumerator P1MiddleScript () {    // P1- M
    P1Middle.SetActive(true);     ;                                      
    yield return new WaitForSeconds(3);
    P1Middle.SetActive(false);
     
 }
    private IEnumerator P2LeftScript () {    // P2 - L
    P2Left.SetActive(true);                                       
    yield return new WaitForSeconds(3);
    P2Left.SetActive(false);
     
 }
    private IEnumerator P2RightScript () {    // P2 - R
    P2Right.SetActive(true);                                         
    yield return new WaitForSeconds(3);
    P2Right.SetActive(false);
     
 }
    private IEnumerator P2MiddleScript () {    // P2 - M
    P2Middle.SetActive(true);                                          
    yield return new WaitForSeconds(3);
    P2Middle.SetActive(false);
     
 }
    private IEnumerator P3LeftScript () {    // P3 - L
    P3Left.SetActive(true);                                        
    yield return new WaitForSeconds(3);
    P3Left.SetActive(false);
     
 }
    private IEnumerator P3RightScript () {    // P3 - R
    P3Right.SetActive(true);                                          
    yield return new WaitForSeconds(3);
    P3Right.SetActive(false);
     
 }
    private IEnumerator P3MiddleScript () {    // P3 - M
    P3Middle.SetActive(true);                                         
    yield return new WaitForSeconds(3);
    P3Middle.SetActive(false);
     
 }
/////////////////////////////////////////////////////////////////////////
// Disable all arrows for x seconds
    private IEnumerator DisableAllArrows () {    // P3 - M
    LeftArrow.interactable = false;
    RightArrow.interactable = false;
    MiddleArrow.interactable = false;                                        
    yield return new WaitForSeconds(3);
    LeftArrow.interactable = true;
    RightArrow.interactable = true;
    MiddleArrow.interactable = true;
     
 }

/////////////////////////////////////////////////////////////////////////
#endregion

#region WhoIsThisPlayer
// recognizing which player presses the button, so that he has the proper UI asssigned to him
    public void LeftSendInfo() // Left Arrow check what player presses it
    {   
        StartCoroutine(DisableAllArrows());
        List<MyNetworkPlayer> players = ((MyNetworkManager)NetworkManager.singleton).players; // Reference to list of all players from MyNetworkManager
        foreach (var player in players)                                                       
        {
            if(player.hasAuthority)         // If a player has AUthority over his networkobject, send CMD to the server
                                            // It is done in order to recognise which player pressed the button.
                                            // In this case, we will recognise what player have pressed the "Arrow" button.
                                            // We do it, by sending the player object via CMD and then based on player list
                                            // from MyNetworkManager, finding the matching player object
            {
                WhatPlayerAmILeft(player);  
            }
        } 
      
    }

    public void MiddleSendInfo()
    {   
        StartCoroutine(DisableAllArrows());
        List<MyNetworkPlayer> players = ((MyNetworkManager)NetworkManager.singleton).players; // Middle Arrow check what player presses it
        foreach (var player in players)
        {
            if(player.hasAuthority)
            {
                WhatPlayerAmIMiddle(player);  
            }
        } 
    }

    public void RightSendInfo()
    {   
        StartCoroutine(DisableAllArrows());
        List<MyNetworkPlayer> players = ((MyNetworkManager)NetworkManager.singleton).players; // Right Arrow check what player presses it
        foreach (var player in players)
        {
            if(player.hasAuthority)
            {
                WhatPlayerAmIRight(player);  
            }
        } 
    }
#endregion
    
#region RPC/Command

[Command(requiresAuthority = false)]  // Checking what player presses the left Arrow                              
    private void WhatPlayerAmILeft(MyNetworkPlayer player)                  // i == 0 host, i == 1 player1, i == 2 player2. Change the conditions inside the if statement,         
    {                                                                       // to define what heppens when different player pressed the "Arrow" icon.
        List<MyNetworkPlayer> players = ((MyNetworkManager)NetworkManager.singleton).players;  // Reference to list of all players from MyNetworkManager
                                            
        for (int i = 0; i < players.Count; i++)                         // Repeat that for every player in player's list.     
        {
            
            if(players[i].connectionToClient == player.connectionToClient)  // Having a player reference 
                                                                            // we can compare players[i].connectionToClient == player.connectionToClient to find the player
                                                              // who clicked the button and thanks to that we can add more players to our game and recognise them.
            {   
                if(i == 0)                            
        {                                               
             RpcP1LeftRED();
             return;
        }
        else if (i == 1)
        {
            CmdP2LeftGREEN();
            return;
           
        }
        else if (i == 2)
        {
            CmdP3LeftYELLOW();
            return;
           
        }
            }
        }
    }

    [Command(requiresAuthority = false)]   // Checking what player presses the right Arrow    
    private void WhatPlayerAmIRight(MyNetworkPlayer player)   
    {
        List<MyNetworkPlayer> players = ((MyNetworkManager)NetworkManager.singleton).players;  
                                            
        for (int i = 0; i < players.Count; i++)    
        {
            
            if(players[i].connectionToClient == player.connectionToClient) 
            {   
                if(i == 0)                            
        {                                               
             RpcP1RightRED();
             return;
        }
        else if (i == 1)
        {
           CmdP2RightGREEN();
            return;
           
        }
        else if (i == 2)
        {
            CmdP3RightYELLOW();
            return;
           
        }
            }
        }
    }

    [Command(requiresAuthority = false)]   // Checking what player presses the middle Arrow    
    private void WhatPlayerAmIMiddle(MyNetworkPlayer player)   
    {
        List<MyNetworkPlayer> players = ((MyNetworkManager)NetworkManager.singleton).players;  
                                            
        for (int i = 0; i < players.Count; i++)    
        {
            
            if(players[i].connectionToClient == player.connectionToClient) 
            {   
                if(i == 0)                            
        {                                               
             RpcP1MiddleRED();
             return;
        }
        else if (i == 1)
        {
            CmdP2MiddleGREEN();
            return;
           
        }
        else if (i == 2)
        {
            CmdP3MiddleYELLOW();
            return;
           
        }
            }
        }
    }
    [Command(requiresAuthority = false)]      // If we are a client, not the host, we execute the CMD first
    private void  CmdP2LeftGREEN() {          // to avoid authority issues. Then a client moves to RPC execution.
        RpcP2LeftGREEN();
    }                                         // The RPC triggers UI Panels and make them visible for other players.

    [Command(requiresAuthority = false)]       
    private void  CmdP3LeftYELLOW() {      
         RpcP3LeftYELLOW();
    }

    [Command(requiresAuthority = false)]       
    private void  CmdP2RightGREEN() {  
        RpcP2RightGREEN();
    }        

    [Command(requiresAuthority = false)]       
    private void  CmdP3RightYELLOW() {      
         RpcP3RightYELLOW();
    }

    [Command(requiresAuthority = false)]       
    private void  CmdP2MiddleGREEN() {  
        RpcP2MiddleGREEN();
    }        

    [Command(requiresAuthority = false)]       
    private void  CmdP3MiddleYELLOW() {      
         RpcP3MiddleYELLOW();
    }

    [ClientRpc]
    private void RpcP1LeftRED()
    {
    StartCoroutine(P1LeftScript());
    }

    [ClientRpc]
    public void RpcP1RightRED()
    {
    StartCoroutine(P1RightScript());
    }

    [ClientRpc]
    private void RpcP1MiddleRED()
    {
    StartCoroutine(P1MiddleScript());
    }

    [ClientRpc]
    private void RpcP2LeftGREEN()
    {
    StartCoroutine(P2LeftScript());
    }

    [ClientRpc]
    private void RpcP2RightGREEN()
    {
    StartCoroutine(P2RightScript());
    }

    [ClientRpc]
    private void RpcP2MiddleGREEN()
    {
     StartCoroutine(P2MiddleScript());
    }

    [ClientRpc]
    private void RpcP3LeftYELLOW()
    {
    StartCoroutine(P3LeftScript());
    }

    [ClientRpc]
    private void RpcP3RightYELLOW()
    {
     StartCoroutine(P3RightScript());
    }

    [ClientRpc]
    private void RpcP3MiddleYELLOW()
    {
     StartCoroutine(P3MiddleScript());
    }

#endregion

}
