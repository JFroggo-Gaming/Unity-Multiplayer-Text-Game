using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class GameScript : NetworkBehaviour
{// This script describes the main mechanics of the game, such as voting, player recognition, the structure of the entire text game, etc.      \
 // Treat it as the main script describing the game
    private GameUI gameUI;
    private HourGlass hourGlass;

    private StoryBlocks storyBlocks;

    private GameScript gameScript;

    

     void OnEnable()
    {     
          
          gameScript.storyBlocks.DisplayBlock(storyBlocks.storyBlocksList[0]); // When we enable the gameobject that this script is on, we want to display the first story
                                        // block with our story.
                                        // Change the zero to another number to display a different story block
    }   

#region Variables/References
// all Variables used in the game. There are intigers counting the votes of players,
//  numbers responsible for random decisions, etc.
    
    
    
    private int IntPlayersAreReady; // This intiger checks how many players pressed "shoe" button. When each of them do so, it increases its value by 1. When its value is equal to 3,
                                    // That means, that everyone is familiar with the game text, and the game set its value back to 0
    
    
    
#endregion


#region ShoeIcon=PlayerIsReady
    // Recognize what player is pressing on the shoe icon, display the corresponding icon according to what kind of player it is
    // This allows other players to see who is ready to vote and see their avatar (icon) at the bottom of the game panel
    public void PlayersUnderstoodText()
    {
        gameUI.HowToPlayInfo_Panel.SetActive(false); // Disable tutorial page information
        gameUI.ShoeButton.interactable = false;      // Once this button is clicked, set it back to interactable = false
        List<MyNetworkPlayer> players = ((MyNetworkManager)NetworkManager.singleton).players;  // Reference to list of all players from MyNetworkManager
        foreach (var player in players)
        {
            if(player.hasAuthority)         // I a player has AUthority over his networkobject, send CMD to the server
                                            // It is done in order to recognise which player pressed the button.
                                            // In this case, we will recognise what player have pressed "shoe" icon.
                                            // We do it, by sending the player object via CMD and then based on player list
                                            // from MyNetworkManager, finding the matching player object
            {
                CmdIUnderstand(player);  
            }
        } 
        
    }
 [Command(requiresAuthority = false)]   
  private void CmdIUnderstand(MyNetworkPlayer player)                 // i == 0 the host, i == 1 player1, i == 2 player2. Change the conditions inside the if statement,                         
    {                                                                 // to define what heppens when different player presses "shoe" icon.
        List<MyNetworkPlayer> players = ((MyNetworkManager)NetworkManager.singleton).players;  // Reference to the list of all players from MyNetworkManager
                                            
 
        for (int i = 0; i < players.Count; i++)                                        // Repeat that for every player in player's list.
        {
            
            if(players[i].connectionToClient == player.connectionToClient)             // Having a player reference 
            {                                                                          // we can compare players[i].connectionToClient == player.connectionToClient to find the player
                if(i == 0)                                                             // who clicked the button and thanks to that we can add more players to our game and recognise them.
        {    
             GetComponent<AddItemToInventory>().NumberGenSettingWhatItemDrops(); // the host generates a random number: this set what kind of items can be find each time                      
             RpcIUnderstandRED();                                                // players find an item. The random number is generated only by host(1 person).
                                                                                 // Otherwise, when someone would find an item
             return;                                                             // and would like to put it to the shared inventory, everyone would receive a different item.
        }
        else if (i == 1)
        {
            CmdIUnderstandYELLOW();
            return;
           
        }
        else if (i == 2)
        {
            CmdIUnderstandGREEN();
            return;
           
        }
            }
        }
    }
[Command(requiresAuthority = false)]       
    private void CmdIUnderstandYELLOW() {  
        RpcIUnderstandYELLOW();
    }        
[Command(requiresAuthority = false)]       
    private void CmdIUnderstandGREEN() {      
         RpcIUnderstandGREEN();
    }

[ClientRpc]                             // If we are recognized as a red player(the host), we perform this method
private void RpcIUnderstandRED(){
    gameUI.RedPlayer_Icon.SetActive(true);     // Show the red icon -> a visual representation of host and first player
    IntPlayersAreReady++;               // Every time a player clicks on "shoe" button the int "IntPlayerAreReady" increases its value by 1. When it hits 3
                                        // that means that all the players have understood game text and option buttoncs can be visible for them and they can start voting.
    if(IntPlayersAreReady == 3)
    {   
        hourGlass.Timer = StartCoroutine(hourGlass.HourGlassTimer());       // Start the Coroutine of HourGlass
        hourGlass.HourGlassAnim.SetTrigger("HourGlassTrigger");   // Start the HourGlass Animation(it takes 30sec to start and finish the sequence)
        gameUI.PanelWithOptionButtons.SetActive(true);         // Make UI Buttons visible
        IntPlayersAreReady = 0;                         // Set the intiger back to 0 after every player pressed the button.
        Invoke("ClosingUIPanels", 0);                   // Close all unnecessary UI elements

    }
}
[ClientRpc]
private void RpcIUnderstandYELLOW(){                    // Show the yellow icon -> a visual representation of second player
     gameUI.YellowPlayer_Icon.SetActive(true);
    IntPlayersAreReady++;
      if(IntPlayersAreReady == 3)
    {   
        hourGlass.Timer = StartCoroutine(hourGlass.HourGlassTimer());
        hourGlass.HourGlassAnim.SetTrigger("HourGlassTrigger");
         gameUI.PanelWithOptionButtons.SetActive(true); 
        IntPlayersAreReady = 0;                    
        Invoke("ClosingUIPanels", 0);  
    }
}
[ClientRpc]
private void RpcIUnderstandGREEN(){
     gameUI.BluePlayer_Icon.SetActive(true);                    // show the green icon -> a visual representation of third player
    IntPlayersAreReady++;
      if(IntPlayersAreReady == 3)
    {   
        hourGlass.Timer = StartCoroutine(hourGlass.HourGlassTimer());
        hourGlass.HourGlassAnim.SetTrigger("HourGlassTrigger");
         gameUI.PanelWithOptionButtons.SetActive(true); 
        IntPlayersAreReady = 0;                    
        Invoke("ClosingUIPanels", 0);  
    }
}
private void ShoeButtonSetActive()       // The method that makes the "shoe" icon be interactable again. 
                                         // It is written in this format, because later on in the code
{                                        // It is invoked with some delay.
     if( gameUI.ShoeButton.interactable == false)
    {
          gameUI.ShoeButton.interactable = true;
    }
}
#endregion


}



