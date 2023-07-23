using System.Collections.Generic;
using Mirror;

public class VotingConditions : NetworkBehaviour
{
    public int Option1Votes;  // There are 3 int(Option1Votes, Option2Votes, Option3Votes). Each of them represents a vote casted by a player.
                               // Based on how many votoes are on particular option, this value can be increase each time by 1 and reach the maximum value of 3.
                               // Each time voting ends, based on how high is this intiger(how many votes a particular option got) different condition is triggered.
    
    public int Option2Votes;
    
    public int Option3Votes;

    private int IntPlayersAreReady; // This intiger checks how many players pressed "shoe" button. When each of them do so, it increases its value by 1. When its value is equal to 3,
                                    // That means, that everyone is familiar with the game text, and the game set its value back to 0

    private ItemDrop itemDrop;

    private GameUI gameUI;

    private HourGlass hourGlass;

// These methods below includes what happens with the game, depending on what are the votes.
// This method is executed when all 3 players cast their votes. If at least one of them doesn't vote
// before HourGlass timer is finished, HourGlass Coroutine conditions will be executed instead of these ones below.
private void Option1V1Option2V1Option3V1()// There was one vote for each option
{                
    if(itemDrop.RandomNumber== 0)                  // If the RandomNumber is 0, behave like option1 button is pressed
    {  
    StartCoroutine( gameUI.RandomPanel());        // Display the UI panel for x seconds
    Invoke("DisplayOption1", 5);          // Invoke in this context means "Display with a delay of 5 seconds" the method in the brackets.
    itemDrop.RandomNumber =0;                      // if Random number is equal to 0 we want to display a story block with an ID behind option1 button.
    }
    if(itemDrop.RandomNumber== 1)
    {    
    StartCoroutine( gameUI.RandomPanel());
    Invoke("DisplayOption2", 5);
    itemDrop.RandomNumber =0; 
    }
    if(itemDrop.RandomNumber == 2)
    {    
    StartCoroutine( gameUI.RandomPanel());
    Invoke("DisplayOption3", 5);
    itemDrop.RandomNumber =0;                       
    }
    ResetVotesAndButtonsUI();
}
private void Option1V2Option2V1()         // Option1 > Option 2
{       
        Invoke("DisplayOption1", 5);
        ResetVotesAndButtonsUI();
         gameUI.PanelWithOptionButtons.SetActive(false);
        StartCoroutine( gameUI.RandomDecisionOption1());
}
private void Option1V2Option3V1()        // Option 1 > Option 3
{       
        Invoke("DisplayOption1", 5);
        ResetVotesAndButtonsUI();
         gameUI.PanelWithOptionButtons.SetActive(false);
        StartCoroutine( gameUI.RandomDecisionOption1());
}
private void Option2V2Option1V1()        // Option2 > Option 1
{       
        Invoke("DisplayOption2", 5);
        ResetVotesAndButtonsUI();
         gameUI.PanelWithOptionButtons.SetActive(false);  
        StartCoroutine( gameUI.RandomDecisionOption2());
}
private void Option2V2Option3V1()       // Option 2 > Option 3
{
        Invoke("DisplayOption2", 5);
        ResetVotesAndButtonsUI();
         gameUI.PanelWithOptionButtons.SetActive(false); 
        StartCoroutine( gameUI.RandomDecisionOption2());
}
private void Option3V2Option1V1()       // Option 3 > Option 1
{       
        Invoke("DisplayOption3", 5);
        ResetVotesAndButtonsUI();
         gameUI.PanelWithOptionButtons.SetActive(false); 
        StartCoroutine( gameUI.RandomDecisionOption3());
}
private void Option3V2Option2V1()       // Option 3 > Option 2
{
        Invoke("DisplayOption3", 5);
        ResetVotesAndButtonsUI();
         gameUI.PanelWithOptionButtons.SetActive(false); 
        StartCoroutine( gameUI.RandomDecisionOption3());
}

private void Option1V3()                // Option 1 Votes 3
{
        Invoke("DisplayOption1", 0);
        ResetVotesAndButtonsUI();
         gameUI.PanelWithOptionButtons.SetActive(false);

}
private void Option2V3()                // Option 2 Votes 3
{       
        Invoke("DisplayOption2", 0);
        ResetVotesAndButtonsUI();
         gameUI.PanelWithOptionButtons.SetActive(false); 
}
private void Option3V3()                // Option 1 Votes 3
{    
        Invoke("DisplayOption3", 0);
        ResetVotesAndButtonsUI();
         gameUI.PanelWithOptionButtons.SetActive(false);
}
private void ResetVotesAndButtonsUI()
{   
    hourGlass.HourGlassAnim.SetTrigger("HourGlassTriggerBack");   // Stopping hour glass animation
     gameUI.option1.interactable = true;                        // we want to make our option button clickable again and set the back to = true
                                                        // so that the players can press them again.
     gameUI.option2.interactable = true;
     gameUI.option3.interactable = true;
    Option1Votes =0;                                    // Set our votes back to 0
    Option2Votes =0;
    Option3Votes =0;
    StopCoroutine(hourGlass.Timer);                               // Stop HourGlass Coroutine
    itemDrop.NumberGenForDecision();                             // Generate a RandomNumber to randomize the next decision.
    itemDrop.NumberGenForItems();                                // Generate  new RandomNumberChanceToDropAnItem, basically a drop chance for an item each time the voting is finished
    Invoke("ShoeButtonSetActive", 0);                   // We can click shoe icon again
    Invoke("ClosingUIPanels", 0);                       // Close other UI Panels
    Invoke("CloseYesNoPanels", 0);
}

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

#region  OPTION1 BUTTON
 public void OptionCheck1()                         // These methods are similar for each option button.
                                                    // I will include the description here, it doesn't change much for the other buttons below(other regions with buttons conditions)
                                                    // This function is triggered when a player clicks the first button on the left(option1)
 {                                                  // This checks if other panels are active and if yes, it close them. 
                                                    // But mainly, it opens up "YES NO" panels
      gameUI.OptionButton1YesNo_Panel.SetActive(true);
     if( gameUI.OptionButton2YesNo_Panel.activeSelf)
        {
             gameUI.OptionButton2YesNo_Panel.SetActive(false);
        }
         if( gameUI.OptionButton3YesNo_Panel.activeSelf)
        {
             gameUI.OptionButton3YesNo_Panel.SetActive(false);
        }
 }

public void Option1YesVote()                         // If a player clicks "YES" it means that he's decided on casting a vote for a particular option
                                                     // Other option buttons won't be available for him to vote on for now
                                                     // That's why they're set to = false
                                                            
{    
        List<MyNetworkPlayer> players = ((MyNetworkManager)NetworkManager.singleton).players;  // take the list of all players from
                                                                                               // MyNetworkManager
        foreach (var player in players)
        {
            if(player.hasAuthority)
            {
                CmdSendPlayerID1(player);   // I a player has AUthority over his networkobject, send CMD to the server
                                            // It is done in order to recognise which player pressed the button.
                                            // In this case, we will recognise what player have pressed button number 1
                                            // We do it, by sending the player object via CMD and then based on player list
                                            // from MyNetworkManager, finding the matching player object
            }
        }
         gameUI.option1.interactable = false; // When we confirm our vote by pressing "YES", we disable other option buttons
         gameUI.option2.interactable = false;
         gameUI.option3.interactable = false; 
         gameUI.CloseYesNoPanels();

}

[Command(requiresAuthority = false)]                    // This CMD is executed every time a player clicks on a button
                                                        // It allows the game to recognise who is the player
    private void CmdSendPlayerID1(MyNetworkPlayer player)    // We are passing player's reference from MyNetworkManager as a parameter
    {
        List<MyNetworkPlayer> players = ((MyNetworkManager)NetworkManager.singleton).players;  
                                                   // Because MyNetwrokManager is other script, we have to use this reference
        for (int i = 0; i < players.Count; i++)    // This loop will be executed as many times as large is our number of players
        {
            
            if(players[i].connectionToClient == player.connectionToClient)  // Compare player's connection from the List of Players 
                                                                            // to the player's just passed paramter via CMD
            {   
                if(i == 0)                              // if this will be equal to 0, that means it's the host and our first player
        {                                               // The following numbers refer to the joining players
                                                        // Thanks to that we can execute different RPC by different players
             RpcOption1PlayerRED();
             return;
        }
        else if (i == 1)
        {
            CmdOption1PlayerYELLOW();
            return;
           
        }
        else if (i == 2)
        {
            CmdOption1PlayerBLUE();
            return;
           
        }
            }
        }                                               
    }
              
public void Option1NoVote()                      // If a player press "NO" in (YES/NO) panel, close the whole (YES/NO) panel
{   
    if( gameUI.OptionButton1YesNo_Panel.activeSelf)
        {
             gameUI.OptionButton1YesNo_Panel.SetActive(false);  
        }  
}

[Command(requiresAuthority = false)]            // These two CMDs are executed by players in order to non-directly execute RPCs(avoid authority issues)
    private void CmdOption1PlayerYELLOW() {     // Both of them are used for OPTION BUTTONS
        RpcOption1PlayerYELLOW();
    }        
[Command(requiresAuthority = false)]       
    private void CmdOption1PlayerBLUE() {      
         RpcOption1PlayerBLUE();
    }      
         
[ClientRpc]
 private void RpcOption1PlayerYELLOW(){      
        Option1Votes++;                     // After pressing "Yes" which, eventually, leads to this (depending what player are we)RPC, we are increasing the intiger
                                            // of votes by 1. These are the votes casted on our first option
                                            // (button on the left). The RPC synchronize this information, so
                                            // the other members in the game know about the current value of this intiger
                                            // (they know how many votes are on particular option and they see who voted)
        
         if(Option1Votes == 1)              // If the votes on this option are greater than 0, we will have our UI object(different for each player)
                                            // popping out on the screen, telling other players who just casted this vote.
        {
             gameUI.Yellow_PlayerVote1_Icon.SetActive(true);   
        }

         if(Option1Votes == 2)            
        {
             gameUI.Yellow_PlayerVote1_Icon.SetActive(true);   
        }

         if(Option1Votes == 3)                 // If there are 3 votes on option1 (the button on the bottom left), execute this method.
        {                                      // Basically below there are only conditions, checking how many votes were casted on what option
            Option1V3();                       // and based on that, they are calling different methods.
        }
        if(Option1Votes == 1 && Option2Votes ==1 && Option3Votes == 1) 
        {
            Option1V1Option2V1Option3V1();
        }
        if(Option1Votes == 2 && Option2Votes ==1) 
        {
            Option1V2Option2V1();
            
        }
        if(Option1Votes == 2 && Option3Votes ==1) 
        {
            Option1V2Option3V1();
            
        }
        if(Option2Votes == 2 && Option1Votes ==1) 
        {
            Option2V2Option1V1();
            
        }
        if(Option2Votes == 2 && Option3Votes ==1) 
        {
          Option2V2Option3V1();
          
        }
        if(Option3Votes == 2 && Option1Votes ==1) 
        {
           Option3V2Option1V1();
           
        }
        if(Option3Votes == 2 && Option2Votes ==1) 
        {
           Option3V2Option2V1();
           
        }
    }                                                       
[ClientRpc]
private void RpcOption1PlayerRED(){            
        Option1Votes++; 
         if(Option1Votes == 1) 
        {
             gameUI.Red_PlayerVote1_Icon.SetActive(true);   
        }
         if(Option1Votes == 2)            
        {
             gameUI.Red_PlayerVote1_Icon.SetActive(true);   
        }
         if(Option1Votes == 3)                 
        {
            Option1V3();
        }
        if(Option1Votes == 1 && Option2Votes ==1 && Option3Votes == 1)
        {
             Option1V1Option2V1Option3V1();                            
        }

        if(Option1Votes == 2 && Option2Votes ==1) 
        {
            Option1V2Option2V1();
            
        }
        if(Option1Votes == 2 && Option3Votes ==1) 
        {
            Option1V2Option3V1();
            
        }
        if(Option2Votes == 2 && Option1Votes ==1) 
        {
            Option2V2Option1V1();
            
        }
        if(Option3Votes == 2 && Option1Votes ==1) 
        {
           Option3V2Option1V1();
           
        }
}
[ClientRpc]
private void RpcOption1PlayerBLUE(){      
        Option1Votes++; 
         if(Option1Votes == 1) 
        {
             gameUI.Blue_PlayerVote1_Icon.SetActive(true);   
        }
         if(Option1Votes == 2)            
        {
             gameUI.Blue_PlayerVote1_Icon.SetActive(true);   
        }
         if(Option1Votes == 3)                 
        {
            Option1V3();
        }
        if(Option1Votes == 1 && Option2Votes ==1 && Option3Votes == 1)
        {
             Option1V1Option2V1Option3V1();                            
        }

        if(Option1Votes == 2 && Option2Votes ==1) 
        {
            Option1V2Option2V1();
            
        }
        if(Option1Votes == 2 && Option3Votes ==1) 
        {
            Option1V2Option3V1();
            
        }
        if(Option2Votes == 2 && Option1Votes ==1) 
        {
            Option2V2Option1V1();
            
        }
        if(Option3Votes == 2 && Option1Votes ==1) 
        {
           Option3V2Option1V1();
           
        }
       
}
#endregion

#region OPTION2 BUTTON
 public void OptionCheck2()
{      
      gameUI.OptionButton2YesNo_Panel.SetActive(true);
     if( gameUI.OptionButton1YesNo_Panel.activeSelf)
        {
             gameUI.OptionButton1YesNo_Panel.SetActive(false);
        }
         if( gameUI.OptionButton3YesNo_Panel.activeSelf)
        {
             gameUI.OptionButton3YesNo_Panel.SetActive(false);
        }
}
public void Option2YesVote()
{
        List<MyNetworkPlayer> players = ((MyNetworkManager)NetworkManager.singleton).players;
        foreach (var player in players)
        {
            if(player.hasAuthority)
            {
                CmdSendPlayerID2(player); 
            }
        }
         gameUI.option1.interactable = false; // When we confirm our vote by pressing "YES", we disable other option buttons
         gameUI.option2.interactable = false;
         gameUI.option3.interactable = false; 
         gameUI.CloseYesNoPanels();
}
[Command(requiresAuthority = false)]   
    private void CmdSendPlayerID2(MyNetworkPlayer player)
    {
        List<MyNetworkPlayer> players = ((MyNetworkManager)NetworkManager.singleton).players;  
        for (int i = 0; i < players.Count; i++)  
        {
            
            if(players[i].connectionToClient == player.connectionToClient)  
                                                                     
            {   
                if(i == 0)                         
        {                                              
             RpcOption2PlayerRED();
             return;
        }
        else if (i == 1)
        {
            CmdOption2PlayerYELLOW();
            return;
           
        }
        else if (i == 2)
        {
            CmdOption2PlayerBLUE();
            return;
           
        }
            }
        }
    }

[Command(requiresAuthority = false)]       
    private void CmdOption2PlayerYELLOW() 
    {      
     RpcOption2PlayerYELLOW();
    }      
  
[Command(requiresAuthority = false)]       
    private void CmdOption2PlayerBLUE() 
    {      
     RpcOption2PlayerBLUE();
    }      

[ClientRpc]
private void RpcOption2PlayerYELLOW(){      
        Option2Votes++;                     
         if(Option2Votes == 1) 
        {
             gameUI.Yellow_PlayerVote2_Icon.SetActive(true);   
        }
         if(Option2Votes == 2)            
        {
             gameUI.Yellow_PlayerVote2_Icon.SetActive(true);   
        }
         if(Option2Votes == 3)                 
        {
            Option2V3();
        }
        if(Option1Votes == 1 && Option2Votes ==1 && Option3Votes == 1)
        {
             Option1V1Option2V1Option3V1();                            
        }

        if(Option1Votes == 2 && Option2Votes ==1) 
        {
            Option1V2Option2V1();
            
        }
        
        if(Option2Votes == 2 && Option1Votes ==1) 
        {
            Option2V2Option1V1();
            
        }
        if(Option2Votes == 2 && Option3Votes ==1) 
        {
          Option2V2Option3V1();
          
        }
        
        if(Option3Votes == 2 && Option2Votes ==1) 
        {
           Option3V2Option2V1();
           
        }
}
[ClientRpc]
private void RpcOption2PlayerRED(){
      Option2Votes++;     
      if(Option2Votes == 1) 
        {
             gameUI.Red_PlayerVote2_Icon.SetActive(true);   
        }
         if(Option2Votes == 2)            
        {
             gameUI.Red_PlayerVote2_Icon.SetActive(true);   
        }
         if(Option2Votes == 3)                 
        {
            Option2V3();
        }
        if(Option1Votes == 1 && Option2Votes ==1 && Option3Votes == 1)
        {
             Option1V1Option2V1Option3V1();                            
        }

        if(Option1Votes == 2 && Option2Votes ==1) 
        {
            Option1V2Option2V1();
            
        }
        
        if(Option2Votes == 2 && Option1Votes ==1) 
        {
            Option2V2Option1V1();
            
        }
        if(Option2Votes == 2 && Option3Votes ==1) 
        {
          Option2V2Option3V1();
          
        }
        
        if(Option3Votes == 2 && Option2Votes ==1) 
        {
           Option3V2Option2V1();
           
        }
    }     
[ClientRpc]
private void RpcOption2PlayerBLUE(){      
        Option2Votes++;                      
         if(Option2Votes == 1) 
        {
             gameUI.Blue_PlayerVote2_Icon.SetActive(true);   
        }
         if(Option2Votes == 2)            
        {
             gameUI.Blue_PlayerVote2_Icon.SetActive(true);   
        }
         if(Option2Votes == 3)                 
        {
            Option2V3();
        }
        if(Option1Votes == 1 && Option2Votes ==1 && Option3Votes == 1)
        {
             Option1V1Option2V1Option3V1();                            
        }

        if(Option1Votes == 2 && Option2Votes ==1) 
        {
            Option1V2Option2V1();
            
        }
        
        if(Option2Votes == 2 && Option1Votes ==1) 
        {
            Option2V2Option1V1();
            
        }
        if(Option2Votes == 2 && Option3Votes ==1) 
        {
          Option2V2Option3V1();
          
        }

        if(Option3Votes == 2 && Option2Votes ==1) 
        {
           Option3V2Option2V1();
           
        }
}
#endregion

#region OPTION3 BUTTON
public void OptionCheck3()
 {  
   gameUI.OptionButton3YesNo_Panel.SetActive(true);
     if( gameUI.OptionButton2YesNo_Panel.activeSelf)
        {
             gameUI.OptionButton2YesNo_Panel.SetActive(false);
        }
         if( gameUI.OptionButton1YesNo_Panel.activeSelf)
        {
             gameUI.OptionButton1YesNo_Panel.SetActive(false);
        }
 }
public void Option3YesVote()
{   
     List<MyNetworkPlayer> players = ((MyNetworkManager)NetworkManager.singleton).players;
        foreach (var player in players)
        {
            if(player.hasAuthority)
            {
                CmdSendPlayerID3(player); 
            }
        }
         gameUI.option1.interactable = false; // When we confirm our vote by pressing "YES", we disable other option buttons
         gameUI.option2.interactable = false;
         gameUI.option3.interactable = false; 
         gameUI.CloseYesNoPanels();
}
[Command(requiresAuthority = false)]   
    private void CmdSendPlayerID3(MyNetworkPlayer player)
    {
       List<MyNetworkPlayer> players = ((MyNetworkManager)NetworkManager.singleton).players;  
        for (int i = 0; i < players.Count; i++)  
        {
            
            if(players[i].connectionToClient == player.connectionToClient)  
                                                                     
            {   
                if(i == 0)                         
        {                                              
             RpcOption3PlayerRED();
             return;
        }
        else if (i == 1)
        {
            CmdOption3PlayerYELLOW();
            return;
           
        }
        else if (i == 2)
        {
            CmdOption3PlayerBLUE();
            return;
           
        }
            }
        }
    }
[Command(requiresAuthority = false)]       
    private void CmdOption3PlayerYELLOW()
    {      
     RpcOption3PlayerYELLOW();
    }      
[Command(requiresAuthority = false)]       
    private void CmdOption3PlayerBLUE()
    {      
    RpcOption3PlayerBLUE();
    }
[ClientRpc]
private void RpcOption3PlayerRED(){      
        Option3Votes++;                     
         if(Option3Votes == 1) 
        {
             gameUI.Red_PlayerVote3_Icon.SetActive(true);   
        }
         if(Option3Votes == 2)            
        {
             gameUI.Red_PlayerVote3_Icon.SetActive(true);   
        }
         if(Option3Votes == 3)                 
        {
            Option3V3();
        }
        if(Option1Votes == 1 && Option2Votes ==1 && Option3Votes == 1)
        {
            Option1V1Option2V1Option3V1();
                                                      
        }

        if(Option1Votes == 2 && Option3Votes ==1) 
        {
            Option1V2Option3V1();
            
        }
       
        if(Option2Votes == 2 && Option3Votes ==1) 
        {
          Option2V2Option3V1();
          
        }
        if(Option3Votes == 2 && Option1Votes ==1) 
        {
           Option3V2Option1V1();
           
        }
        if(Option3Votes == 2 && Option2Votes ==1) 
        {
           Option3V2Option2V1();
           
        }
}
[ClientRpc]
private void RpcOption3PlayerBLUE(){                         
        Option3Votes++;                     
         if(Option3Votes == 1) 
        {
             gameUI.Blue_PlayerVote3_Icon.SetActive(true);   
        }
         if(Option3Votes == 2)            
        {
            gameUI. Blue_PlayerVote3_Icon.SetActive(true);   
        }
         if(Option3Votes == 3)                 
        {
            Option3V3();
        }
        if(Option1Votes == 1 && Option2Votes ==1 && Option3Votes == 1)
        {
            Option1V1Option2V1Option3V1();
                                                      
        }

        if(Option1Votes == 2 && Option3Votes ==1) 
        {
            Option1V2Option3V1();
            
        }
       
        if(Option2Votes == 2 && Option3Votes ==1) 
        {
          Option2V2Option3V1();
          
        }
        if(Option3Votes == 2 && Option1Votes ==1) 
        {
           Option3V2Option1V1();
           
        }
        if(Option3Votes == 2 && Option2Votes ==1) 
        {
           Option3V2Option2V1();
           
        }
}
[ClientRpc]
private void RpcOption3PlayerYELLOW(){      
        Option3Votes++;                     
         if(Option3Votes == 1) 
        {
             gameUI.Yellow_PlayerVote3_Icon.SetActive(true);   
        }
         if(Option3Votes == 2)            
        {
             gameUI.Yellow_PlayerVote3_Icon.SetActive(true);   
        }
         if(Option3Votes == 3)                 
        {
            Option3V3();
        }
        if(Option1Votes == 1 && Option2Votes ==1 && Option3Votes == 1)
        {
            Option1V1Option2V1Option3V1();
                                                     
        }

        if(Option1Votes == 2 && Option3Votes ==1) 
        {
            Option1V2Option3V1();
        }
       
        if(Option2Votes == 2 && Option3Votes ==1) 
        {
          Option2V2Option3V1();
          
        }
        if(Option3Votes == 2 && Option1Votes ==1) 
        {
           Option3V2Option1V1();
           
        }
        if(Option3Votes == 2 && Option2Votes ==1) 
        {
           Option3V2Option2V1();
           
        }
}
#endregion
}
