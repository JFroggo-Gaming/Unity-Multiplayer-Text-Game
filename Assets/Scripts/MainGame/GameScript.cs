using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameScript : NetworkBehaviour
{// This script describes the main mechanics of the game, such as voting, player recognition, the structure of the entire text game, etc.      \
 // Treat it as the main script describing the game
     void OnEnable()
    {   
          DisplayBlock(storyBlocks[0]); // When we enable the gameobject that this script is on, we want to display the first story
                                        // block with our story.
                                        // Change the zero to another number to display a different story block
    }   

#region Panels/Buttons/GameObjects
// In this region there are UI objects included in the game scene.
//////////////////////////////////////////////////////////////////////////////////
    public TMP_Text mainText;       // The main text of our game and one of the building blocks of StoryBlock
    public TMP_Text RecentOption;   // This is "special" button, which displays all the information of the player's last decision, or about the decision
                                    // which has been taken based on RandomNumber generator output(apart from being a reminder for the players what was the last option, this
                                    // also tells them what's the result of a randomness in options taking.)
    public Button option1;          // One of three buttons(option1, option2, option3), that players votes on. 
    public Button option2;          // Each of them stores text element relevant to current text block.
    public Button option3;   
    public Button ShoeButton;       //"Shoe" icon, which should be clicked, when a player is familiar with the text, and wants to show his readiness to others

 ///////////////////////////////////////////////////////////////////////////////
     // UI of individual levels in the game. It changes depending on the player's progress in the game (where the current story block is located)
    [SerializeField] public GameObject LavaLevel_Background = null;
    [SerializeField] public GameObject LavaLevel_Content = null;
    [SerializeField] public GameObject SewersLevel_Background = null;
    [SerializeField] public GameObject SewersLevel_Content = null;
    [SerializeField] public GameObject SideEntrance_Background = null;
    [SerializeField] public GameObject SideEntrance_Content = null;

//////////////////////////////////////////////////////////////////////////////////////////
    // Player's swords icons on the botton of the main text field
    [SerializeField]public GameObject RedPlayer_Icon= null;                 // A visual representation of PLAYER 1(RED SWORD), becomes visible when a player clicks a "shoe" icon
    [SerializeField]public GameObject YellowPlayer_Icon = null;             // A visual representation of PLAYER 2(YELLOW SWORD), becomes visible when a player clicks a "shoe" icon 
    [SerializeField]public GameObject BluePlayer_Icon = null;               // A visual representation of PLAYER 3(GREEN SWORD), becomes visible when a player clicks a "shoe" icon

//////////////////////////////////////////////////////////////////////////////
    // Player icons when they vote by clicking on the individual option buttons (1-3)
    [SerializeField]public GameObject Red_PlayerVote1_Icon = null;          // PLAYER 1: Red icon above button nr1
    [SerializeField]public GameObject Red_PlayerVote2_Icon = null;
    [SerializeField]public GameObject Red_PlayerVote3_Icon = null;
    [SerializeField]public GameObject Blue_PlayerVote1_Icon = null;         // PLAYER 2: Blue icon above button nr1
    [SerializeField]public GameObject Blue_PlayerVote2_Icon = null;
    [SerializeField]public GameObject Blue_PlayerVote3_Icon = null;
    [SerializeField]public GameObject Yellow_PlayerVote1_Icon = null;       // PLAYER 3: Yellow icon above button nr1     
    [SerializeField]public GameObject Yellow_PlayerVote2_Icon = null;
    [SerializeField]public GameObject Yellow_PlayerVote3_Icon = null;

//////////////////////////////////////////////////////////////////////////////
    // Elements related to option buttons
    [SerializeField]public GameObject PanelWithOptionButtons = null;        // The panel, which includes all 3 button elements
    [SerializeField]public GameObject OptionButton1YesNo_Panel = null;      // One of 3 panels, that pops out to confirm player's vote(YES/NO)
    [SerializeField]public GameObject OptionButton2YesNo_Panel = null;
    [SerializeField]public GameObject OptionButton3YesNo_Panel = null;
    /////////////////////////////////////////////////////////////////////////////
    [SerializeField]public GameObject RandomDecisionPanel = null;           // The panel, which informs a player about a random decision(When the votes are 1-1-1)
    [SerializeField]public GameObject RandomDecisionOption1Panel = null;    // The panel telling that the majority has voted on option1(when the votes are 2-x-x)
    [SerializeField]public GameObject RandomDecisionOption2Panel = null;    // The panel telling that the majority has voted on option2(when the votes are x-2-x)
    [SerializeField]public GameObject RandomDecisionOption3Panel = null;    // The panel telling that the majority has voted on option3(when the votes are x-x-2)

///////////////////////////////////////////////////////////////////////////////
    // When the hourglass timer is over, based on player's votes, one of these panels pops out
    [SerializeField]public GameObject HourGlassTimeOver1OV1 = null;          // There was only one vote for option1  
    [SerializeField]public GameObject HourGlassTimeOver1OV2 = null;          // There were two votes for option2
    [SerializeField]public GameObject HourGlassTimeOver2OV1 = null;          // There was only one vote for option2
    [SerializeField]public GameObject HourGlassTimeOver2OV2 = null;          // There were two votes for option2
    [SerializeField]public GameObject HourGlassTimeOver3OV1 = null;          // There was only one vote for option3
    [SerializeField]public GameObject HourGlassTimeOver3OV2 = null;          // There were two votes for option3
    [SerializeField]public GameObject HourGlassTimeOverZero = null;          // No one has voted, the decision will be made randomly!

///////////////////////////////////////////////////////////////////////////////////////
    [SerializeField]public GameObject NotificationAboutItem = null;         // The star, which appears on the button right corner next to "shoe" icon,
                                                                            // when a player finds an item
    [SerializeField]public GameObject HowToPlayInfo_Panel = null;           // UI element with short tips for the player (displayed at the beginning of the game)

//////////////////////////////////////////////////////////////////////////////////////////

private void ClosingUIPanels(){
//////////////////////////////////////////////////////////////////////////////////////////
// Many times in this script you need to close interface elements so that they don't collide with each other.
//  Most of them are collected in one function.

    // Closing player icons on main game text panel
        if(RedPlayer_Icon.activeSelf)                   // Red player
        {                                                                             
            RedPlayer_Icon.SetActive(false);               
        }
         if(BluePlayer_Icon.activeSelf)                 // Blue player
        {
            BluePlayer_Icon.SetActive(false);      
        }
        if(YellowPlayer_Icon.activeSelf)                // Yellow player
        {
            YellowPlayer_Icon.SetActive(false);
        }

//////////////////////////////////////////////////////////////////////////////////////////
    // Closing player icons above option button number 1
        if(Red_PlayerVote1_Icon.activeSelf)              // Red player
        {                                                                                                
            Red_PlayerVote1_Icon.SetActive(false);               
        }
        if(Blue_PlayerVote1_Icon.activeSelf)             // Blue player
        {
            Blue_PlayerVote1_Icon.SetActive(false);      
        }
        if(Yellow_PlayerVote1_Icon.activeSelf)           // Yellow player
        {
            Yellow_PlayerVote1_Icon.SetActive(false);
        }

//////////////////////////////////////////////////////////////////////////////////////////
    // Closing player icons above option button number 2
        if(Red_PlayerVote2_Icon.activeSelf)              // Red player
        {                                                   
            Red_PlayerVote2_Icon.SetActive(false);
        }
         if(Blue_PlayerVote2_Icon.activeSelf)            // Blue player 
        {
            Blue_PlayerVote2_Icon.SetActive(false);
        }
        if(Yellow_PlayerVote2_Icon.activeSelf)           // Yellow player
        {
            Yellow_PlayerVote2_Icon.SetActive(false);                            
        }

//////////////////////////////////////////////////////////////////////////////////////////
    // Closing player icons above option button number 3
        if(Red_PlayerVote3_Icon.activeSelf)              // Red player
        {                                                   
            Red_PlayerVote3_Icon.SetActive(false);
        }
        if(Blue_PlayerVote3_Icon.activeSelf)             // Blue player
        {
            Blue_PlayerVote3_Icon.SetActive(false);
        }
       if(Yellow_PlayerVote3_Icon.activeSelf)            // Yellow player
       {
        Yellow_PlayerVote3_Icon.SetActive(false);                                                      
       }

//////////////////////////////////////////////////////////////////////////////////////////

}

private void CloseYesNoPanels()
{// When ve press "YES" when we vote, we want other panels to become invisible. 
 // this has to be done many times in the code, that is why this function is created
    
    // Closing the panels (YES/NO)
       if(OptionButton1YesNo_Panel.activeSelf)           // Button 1   
       {
        OptionButton1YesNo_Panel.SetActive(false);
       }
        if(OptionButton2YesNo_Panel.activeSelf)          // Button 2
       {
        OptionButton2YesNo_Panel.SetActive(false);       
       }
        if(OptionButton3YesNo_Panel.activeSelf)          // Button 3
       {
        OptionButton3YesNo_Panel.SetActive(false);
       }

}
// These Coroutines are created in order to display UI panels with some delay. For example, "RandomDecisionOption1" method include "RandomDecisionOption1Panel", which
// inform the players, that majority of votes are for option 1(2-1-0 or 2-0-1).
public IEnumerator RandomDecisionOption1 () {    
                                           
     RandomDecisionOption1Panel.SetActive(true);                                       
     yield return new WaitForSeconds(5);
     RandomDecisionOption1Panel.SetActive(false);
 }
public IEnumerator RandomDecisionOption2() {    
     RandomDecisionOption2Panel.SetActive(true);                                      
     yield return new WaitForSeconds(5);
     RandomDecisionOption2Panel.SetActive(false);
 }
public IEnumerator RandomDecisionOption3() {    
     RandomDecisionOption3Panel.SetActive(true);                                       
     yield return new WaitForSeconds(5);
     RandomDecisionOption3Panel.SetActive(false);
 }
public IEnumerator RandomPanel () {    
     RandomDecisionPanel.SetActive(true);                                        
     yield return new WaitForSeconds(5);
     RandomDecisionPanel.SetActive(false);
     PanelWithOptionButtons.SetActive(false);
 }
#endregion

#region Variables/References
// all Variables used in the game. There are intigers counting the votes of players,
//  numbers responsible for random decisions, etc.
    private int Option1Votes;  // There are 3 int(Option1Votes, Option2Votes, Option3Votes). Each of them represents a vote casted by a player.
                               // Based on how many votoes are on particular option, this value can be increase each time by 1 and reach the maximum value of 3.
                               // Each time voting ends, based on how high is this intiger(how many votes a particular option got) different condition is triggered.
    
    private int Option2Votes;
     
    private int Option3Votes;
    
    private int RandomNumber;      // This is the variable, which is synchronized betweeen the host and a client 
                                   // It is generated every time HourGlass Coroutine starts(when every player press shoe icon showing, that they are ready to play)
                                   // Based on its value, if the players face a situation when the votes ae not equal (eg. 1-1-1)
                                   // It allows the game to randomize the choise based on random outcome of this number and take a random decision.

    private int RandomNumberChanceToDropAnItem; // This random intiger is generated by every player after voting is finished. It determinates a CHANCE to find an item by each player
    private int IntPlayersAreReady; // This intiger checks how many players pressed "shoe" button. When each of them do so, it increases its value by 1. When its value is equal to 3,
                                    // That means, that everyone is familiar with the game text, and the game set its value back to 0
    
    public Animator HourGlassAnim;  // The animation of HourGlass. 
    Coroutine Timer; // The reference to the Coroutine. In order to start and stop it properly(the Coroutine) it has to be keep in this format
                     // This Coroutine is triggered each time "IntPlayersAreReady" value is equal to 3(the buttons for voting becomes visible for players
                     // and they have x seconds determinated by the Coroutine to take their actions)
    StoryBlock currentBlock; // The reference to the built story block
                             // In this format, we can use, among others, the first component of such a block, 
                             // which is its ID and track the progress of our game
#endregion

#region HourGlass Coroutine
// The Corutine associated with the Hourglass
private  IEnumerator HourGlassTimer () {
 //1.      This Coroutine is triggered each time the option buttons shows up(after every player presses "shoe" icon)
        // If the players doesn't vote before the timer runs down, the Coroutine checks how many votes were cast(if any)
        // and based on that, displays adequate UI elements and moves the players to other story blocks using
        // "Invoke(DisplayOption)"  
                  

 //2.    Also checks the player's progress and when he is on the right text block, changes the UI accordingly.
         // If the player is in the sewers in the text, 
         // the text block with the sewers has the appropriate ID and a new UI for the player will be displayed based on it

//////////////////////////////////////////// - 2 - ///////////////////////////////////////////
// UI for levels
if(currentBlock.ThisBlockID == 21) // The Sewers Level 
{
  LavaLevel_Background.SetActive(false);
  LavaLevel_Content.SetActive(false);
  SideEntrance_Background.SetActive(false);
  SideEntrance_Background.SetActive(false);
  SewersLevel_Background.SetActive(true);
  SewersLevel_Content.SetActive(true);
  
}
if(currentBlock.ThisBlockID == 19) // Side Entrance Level
{
  LavaLevel_Background.SetActive(false);
  LavaLevel_Content.SetActive(false);
  SewersLevel_Background.SetActive(false);
  SewersLevel_Content.SetActive(false);
  SideEntrance_Background.SetActive(true);
  SideEntrance_Background.SetActive(true);
}

//////////////////////////////////////////////////////////////////////////////////////////
// Switching to less/more options to choose from (turning off buttons)
// Main entrance is too dangerous, two options, then unlock the button again : BLOCK 20
if(currentBlock.ThisBlockID ==20)
{
    option3.interactable = false;
}

if(currentBlock.ThisBlockID ==21)
{
    option3.interactable = true;
}
if(currentBlock.ThisBlockID ==19)
{
    option3.interactable = true;
}

// The Side Entrance, two stairs(up and down) : BLOCK 22
if(currentBlock.ThisBlockID ==22)
{
    option3.interactable = false;
}
if(currentBlock.ThisBlockID ==31)
{
    option3.interactable = true;
}
if(currentBlock.ThisBlockID ==32)
{
    option3.interactable = true;
}

// The game demo is finished, only two ways to pick from : BLOCK 30/39
if(currentBlock.ThisBlockID ==30)
{
    option3.interactable = false;
}
if(currentBlock.ThisBlockID ==39)
{
    option3.interactable = false;
}
if(currentBlock.ThisBlockID ==0)
{
    option3.interactable = true;
}
if(currentBlock.ThisBlockID ==19)
{
    option3.interactable = true;
}
if(currentBlock.ThisBlockID ==21)
{
    option3.interactable = true;
}

//////////////////////////////////////////// - 1 - ///////////////////////////////////////////
// Players have 30 seconds to vote. If they do not, based on their votes (or lack thereof), the Coroutine will take appropriate action
    yield return new WaitForSeconds(30);
     NumberGenForItems(); // Generate item find chances
     NumberGenForDecision(); // Generate a random number so you can make random decisions if needed
     if(Option1Votes == 0 && Option2Votes == 0 && Option3Votes == 0) // What happanes, when there are no votes at all.
     {  
    
        HourGlassVotingConditions();
        HourGlassVariablesReset();
        HourGlassTimeOverZero.SetActive(true);   // The Panel informing about votes (In this case, that no one voted)
        PanelWithOptionButtons.SetActive(false); // Options buttons are again invisible
        yield return new WaitForSeconds(5);      // After 5 seconds, the panel becomes invisible again.
        HourGlassTimeOverZero.SetActive(false);  
        RandomNumber = 0;                        // Random number is set back to 0 after each times it is used.
    }        

    if(Option1Votes == 1 && Option2Votes == 1 && Option3Votes == 0)
     {  
    
        HourGlassVotingConditions();
        HourGlassVariablesReset();
        RandomDecisionPanel.SetActive(true);
        PanelWithOptionButtons.SetActive(false);
        yield return new WaitForSeconds(5);
        RandomDecisionPanel.SetActive(false);
        RandomNumber = 0;
    }                                                                
    
    if(Option1Votes == 1 && Option2Votes == 0 && Option3Votes == 1)
     {  
    
        HourGlassVotingConditions();
        HourGlassVariablesReset();
        RandomDecisionPanel.SetActive(true);
        PanelWithOptionButtons.SetActive(false);
        yield return new WaitForSeconds(5);
        RandomDecisionPanel.SetActive(false);
        RandomNumber = 0;
    }                        

    if(Option1Votes == 0 && Option2Votes == 1 && Option3Votes == 1)
     {  
     
        HourGlassVotingConditions();
        HourGlassVariablesReset();
        RandomDecisionPanel.SetActive(true);
        PanelWithOptionButtons.SetActive(false);
        yield return new WaitForSeconds(5);
        RandomDecisionPanel.SetActive(false);
        RandomNumber = 0;
    }                        
     
    if(Option1Votes == 1 && Option2Votes == 0 && Option3Votes == 0){

    HourGlassVariablesReset();
    Invoke("DisplayOption1",5);
    HourGlassTimeOver1OV1.SetActive(true);
    PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
    HourGlassTimeOver1OV1.SetActive(false);
     }

    if(Option2Votes == 1 && Option1Votes == 0 && Option3Votes == 0){

    HourGlassVariablesReset();
    Invoke("DisplayOption2",5);
    HourGlassTimeOver2OV1.SetActive(true);
    PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
    HourGlassTimeOver2OV1.SetActive(false);
    }

    if(Option3Votes == 1 && Option1Votes == 0 && Option2Votes == 0){

    HourGlassVariablesReset();
    Invoke("DisplayOption3",5);
    HourGlassTimeOver3OV1.SetActive(true);
    PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
    HourGlassTimeOver3OV1.SetActive(false);
    }

    if(Option1Votes == 2 && Option2Votes ==1) 
    {

    HourGlassVariablesReset();
    Invoke("DisplayOption1",5);
    HourGlassTimeOver1OV2.SetActive(true);
    PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
    HourGlassTimeOver1OV2.SetActive(false);
            
    }
    if(Option1Votes == 2 && Option3Votes ==1) 
    {

    HourGlassVariablesReset();        
    Invoke("DisplayOption1",5);
    HourGlassTimeOver1OV2.SetActive(true);
    PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
    HourGlassTimeOver1OV2.SetActive(false);
    }

    if(Option2Votes == 2 && Option1Votes ==1) 
    {

    HourGlassVariablesReset();
    Invoke("DisplayOption2",5);
    HourGlassTimeOver2OV2.SetActive(true);
    PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
    HourGlassTimeOver2OV2.SetActive(false);

    }
    if(Option2Votes == 2 && Option3Votes ==1) 
    {

    HourGlassVariablesReset();
    Invoke("DisplayOption2",5);
    HourGlassTimeOver2OV2.SetActive(true);
    PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
    HourGlassTimeOver2OV2.SetActive(false);
    }

    if(Option3Votes == 2 && Option1Votes ==1) 
    {

    HourGlassVariablesReset();
    Invoke("DisplayOption3",5);
    HourGlassTimeOver3OV2.SetActive(true);
    PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
    HourGlassTimeOver3OV2.SetActive(false);
    }

    if(Option3Votes == 2 && Option2Votes ==1) 
    {

    HourGlassVariablesReset();
    Invoke("DisplayOption3",5);
    HourGlassTimeOver3OV2.SetActive(true);
    PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
    HourGlassTimeOver3OV2.SetActive(false);
    }

    if(Option1Votes == 2){
    
    HourGlassVariablesReset();
    Invoke("DisplayOption1",5);
    HourGlassTimeOver1OV2.SetActive(true);
    PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
    HourGlassTimeOver1OV2.SetActive(false);
     }

    if(Option2Votes == 2){

    HourGlassVariablesReset();
    Invoke("DisplayOption2",5);
    HourGlassTimeOver2OV2.SetActive(true);
    PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
    HourGlassTimeOver2OV2.SetActive(false);
    }

    if(Option3Votes == 2){
    
    HourGlassVariablesReset();
    Invoke("DisplayOption3",5);
    HourGlassTimeOver3OV2.SetActive(true);
    PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
    HourGlassTimeOver3OV2.SetActive(false);
    }

    HourGlassAnim.SetTrigger("HourGlassTriggerBack"); // After X seconds, the HourGlass animation is set back to defualt using this trigger in its Animator.
 }
private void HourGlassVariablesReset()
{   
    Invoke("CloseYesNoPanels", 0);
    Invoke("ClosingUIPanels", 0);
    Invoke("ShoeButtonSetActive", 5);       // Make "shoe" icon clickable again
    Option1Votes = 0;                       // Set votes value back to 0
    Option2Votes = 0;
    Option3Votes = 0;
    option1.interactable = true;            // Options button are interactable again.
    option2.interactable = true;
    option3.interactable = true;
}
private void HourGlassVotingConditions()
{
      if(RandomNumber==0)                   // Depending of the outcome of Randomnumber (0-2)
        {                                   // a different option will be display for the players.
            Invoke("DisplayOption1",5);
            
        }
        if(RandomNumber==1)
        {
            Invoke("DisplayOption2",5);
           
        }
        if(RandomNumber==2)
        {
            Invoke("DisplayOption3",5);
            
        }
}
#endregion

#region ItemDropChance/AddToInventory
// This region describes how random numbers are generated to take decisions,
//  the chance of finding an item, and adding items to your inventory

//////////////////////////////////////////////////////// RANDOM NUMBER GENERATOR ////////////////////////////////////////////////////////
private void NumberGenForDecision(){ 
{       
                RandomNumber = Random.Range(0,3);    // This method generates a random number, which is used in order to choose one of 3 options.
                                                     // It is synchronized via RPC with other players, so they will have
                                                     // the same number in their game and display the same option as every other player.

                if(isServer)                         // If we are the host, send the number directly via RPC
                {
                RpcSetRandomNumber(RandomNumber);             
                }
                if(!isServer)                        // If we are the client, use the command function first to avoid authority issues,
                                                     // Then in this CMD use the same RPC as above
                {
                CmdSetRandomNumber();
                }                              
}                                           
}
[Command(requiresAuthority = false)]       
private void CmdSetRandomNumber() {      
         RpcSetRandomNumber(RandomNumber);
}

[ClientRpc]                         
private void RpcSetRandomNumber (int NewRandomNumber) // Replace the old intiger with a new one and send this iformation to other players
{                                                       
    RandomNumber = NewRandomNumber;
}

//////////////////////////////////////////////////////// RANDOM CHANCE TO FIND AN ITEM ////////////////////////////////////////////////////////
private void NumberGenForItems(){                                                       // This random number is not set via RPC to everyone, otherwise everyone would have a panel
                                                                                        // with a drop. Not SynC this value means that only one person at the time finds an item and
                                                                                        // everyone generates a unique Random Number!(everyone )

                RandomNumberChanceToDropAnItem = Random.Range(0,3);                     // Set the number to be a random value from 0 to 2.
                RandomNumberGeneratorForItems(RandomNumberChanceToDropAnItem);          // Increase the range = items are not found that often
                //Debug.Log("Numer itemka to: " + RandomNumberChanceToDropAnItem);          
 }  

private void RandomNumberGeneratorForItems(int NewRandomNumberForItems)                  // Replace the old int, with a brand new generated one
{
    RandomNumberChanceToDropAnItem = NewRandomNumberForItems;
}
//////////////////////////////////////////////////////// ADD AN ITEM TO INVENTORY ////////////////////////////////////////////////////////

private void NotificationButtonClick()                       // When you press the "star" button(which is the symbol of a found item),
                                                            // You add your item to an inventory
{
     NotificationAboutItem.SetActive(false);                // Once the star is pressed, it becomes invisible again
    if(isServer)                                            // If we are the host, we use RPC method place an item in other player's inventory
    {
        RpcAddRandomItem();
    }
    if(!isServer)
    {
        CmdAddRandomItem();                                 // If we are a client, we do the same, but firstly we use CMD to avoid authority issues.
    }
}
 [ClientRpc]
private void RpcAddRandomItem()
{
    GetComponent<AddItemToInventory>().AddItem();           // We take AddItemToInventory script(it has to be attatched on the same gameobject as this script to be visible)                                                           //
}                                                           // And from this cript we call AddItem method

[Command(requiresAuthority = false)]       
private void CmdAddRandomItem() {      
         RpcAddRandomItem();
    }

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

#region GameStory
// This region describes the structure of the text block and the game, how it is displayed, and how to move between blocks.
 [System.Serializable]
public class StoryBlock {
    // This is visible in the inspector and thanks to that, a developer can see what text is included in each of the blocks.
    [TextArea]
    public string story;
    [Header("This Block ID")]
    public int ThisBlockID;

    [Header("Button 1")]
    public string option1Text;
    public int option1BlockId;
    [Header("Button 2")]
    public string option2Text;
    public int option2BlockId;
     [Header("Button 3")]
    public string option3Text;
    public int option3BlockId;
    [Header("LAST OPTION")]

    public string option4Text;
     
    public StoryBlock(int ThisBlockID, string story, string option1Text = "", string option2Text = "",string option3Text = "", string option4Text = "",
      
        int option1BlockId = -1, int option2BlockId = -1, int option3BlockId = -1) {
        
        this.ThisBlockID = ThisBlockID;
        this.story = story;
        this.option1Text = option1Text;
        this.option2Text = option2Text;
        this.option3Text = option3Text;
        this.option4Text = option4Text;
        this.option1BlockId = option1BlockId;
        this.option2BlockId = option2BlockId;
        this.option3BlockId = option3BlockId;
    }
// The decscription of the method above is really important to understand. 
// It is build out from few values: 1 intiger, 5 strings and 3 intigers.

// The 4 strings are for the text displayed on the buttons, and the 5th one is for the main text of the game
// They are declared as a block together with an assigned ID, because each block have got a different piece of text written on it.
// Eg. ID: 1 -> YourOption1, YourOption2, YourOption3, YourLastDecision, MainGameText
//     ID;2 -> DifferentOption1, DifferentOption2, DifferentOption3, DifferentLastDecision, NowGameStoryIsDifferent

// There are also some intigers. The first contains the ID of the current block (used to identify the block)
// the rest of them is assgined to a different option buttons(they are there to move from block to block)
// Based on that ID, every buttons moves a player to a different ID of the story block. Basically, an intiger is a pointer to different storyblock.

}

   public StoryBlock[] storyBlocks = {
     // This is the actual list of story blocks
                                        // Storyblock is build like this: Int, String, String, String, String, String, Int, Int, Int
                                        // The first int: ID of the block
                                        // The First string: Main text of the game
                                        // 3 strings: 3 option buttons
                                        // 4th string: previous choosen option
                                        // Three intigers: each one assigned to coresponding option buttos. If a line finishes with numbers eg. 5,6,7 ->
                                        // that means that option1 moves to block nr5, option2 moves to block nr 6, option3 moves to block nr 7.
////////////////////////////////////////////////////////////////////// LAVA LEVEL ///////////////////////////////////////////////////////////////////////////////
    /* 0 */     new StoryBlock(0, "It's been a few days since you've been on the road. Crossing this rocky terrain was not very easy. Despite the difficult journey, you look at the faces of your companions and you see that they are as motivated as you. But is it really worth it? After all, no one has ever seen the fortress, and it is known that mages like to keep their own secrets...","March on", "March on", "March on",". . .", 1, 1 ,1),
    /* 1*/      new StoryBlock(1, "The heat is in the air, and the color of the lava, despite the height you are at, seems to be just as fiery as when you started your journey at the bottom...", "Take a deep breath", "Take a look around","Take a look in your bag","March on", 2, 3, 4),
    /* 2*/      new StoryBlock(2, "The heat cuts into the nostrils and makes it impossible to inhale fully. \"Nothing unusual\" you think, \"A man would not be able to live in this land...\"","March on", "March on","March on","Take a deep breath", 5, 5, 5), // 2
    /* 3*/      new StoryBlock(3, "You look up and you see starry skies and stars that you had no idea existed. There are new eruptions on the horizon, are they these volcanoes? Perhaps. At least the mountain you are climbing looks safe...", "March on", "March on", "March on","Take a look around", 5, 5, 5), // 3
    /* 4*/      new StoryBlock(4, "You look in your bag and smile to your nose. A magic battle always full of water, a gift from an old friend attracts memories... Without it the whole trip would be much more difficult, especially in this unknown land...", "March on", "March on", "March on","Take a look in your bag",  5, 5, 5),  // 4
    /* 5*/      new StoryBlock(5, "You finally reach the top of the mountain. You are struck by the ubiquitous silence. Why is everyone so silent? You, and your companions are looking around. Ubiquitous lava and volcanoes do not herald anything good. But the fortress is here somewhere, you are sure of it with all of yourself.", "Pull out the binoculars", "Ask your companions about further plans", "Look at the road behind you","March on", 6, 7, 8), // 5
    /* 6*/      new StoryBlock(6, "You take out your binoculars, looking at the horizon. Everything looks similar to the west side of the mountain. Were you wandering in vain? Hold on! You see a point on the horizon. How could you have missed that before? There is only one road leading to the middle of the lake of fire, and at its end there is ... the fortress!", "Tell your comrades what you see", "Tell your comrades what you see", "Tell your comrades what you see","Pull out the binoculars",  10, 10, 10), //6
    /* 7*/      new StoryBlock(7, "They all look exhausted and stunned by the size of the lake of fire. Is it really a lake? Or are the mountains here all drenched in lava? Nobody knows, you and your companions are the first to get here. The others are curious about a certain point on the horizon; they suggest you take out your binoculars.", "Pull out the binoculars", "Pull out the binoculars", "Pull out the binoculars","Ask your companions about further plans", 6, 6, 6),         // 7
    /* 8*/      new StoryBlock(8, "You turn back and look proudly at the distance traveled. Stone road leading through the gorge and not a half a trace of life. Did your skills get you this far, or just luck?", "Pull out the binoculars", "Ask your companions about further plans", "Do nothing","Look at the road behind you", 6, 7, 9),       // 8
    /* 9*/      new StoryBlock(9, "Everyone looks at you with a little surprise on their face, not believing that you are not curious what is around after such a long walk. After all, everyone was hoping that what they were looking for might be on the other side of the moutain. They insist on you to get your binoculars out.", "Pull out the binoculars", "Pull out the binoculars", "Pull out the binoculars","Do nothing", 6, 6, 6), //9
    /* 10*/     new StoryBlock(10, "You put your eyes to the binoculars and watch. A mixture of excitement and hope is strongly felt. Everyone hopes to see what you're getting at. Suddenly you scream \"The fortress!\". Everyone thinks: \"Is he serious?\". No doubt, he is! Everyone wants to see it with their own eyes. While handing each other binoculars, they comment on more and more new observations. But the fortress is too far away. You have to get closer and take a closer look. After all, we're supposed to go in there unnoticed, right?", "Take another look at the fortress", "Talk of your companions", "Go towards the fortress","Tell your comrades what you see",11, 12, 13), // 10
    /* 11*/     new StoryBlock(11, "You grab your binoculars and look again... yes... obviously it must be the fortress! At least you think so. You wonder how it happened. Risking your life based on what? Urban legends? But now you have no doubts. From this distance you can only see the outline of the two towers and the fortress itself... To see more you should definitely come closer...", "Go towards the fortress", "Go towards the fortress", "Go towards the fortress","Take another look at the fortress", 13, 13, 13), // 11
    /* 12*/     new StoryBlock(12, "Despite your good motivation, you had no idea if the entire expedition would work. A group of excellent thieves, specialists in their profession. But here? Nobody has holed up that far. But now you can see that motivation has been revived. Another goal looms on the horizon, there is only hope that the entrance to the fort will remain open ...", "Go towards the fortress", "Go towards the fortress", "Go towards the fortress","Talk of your companions",  13, 13, 13), // 12
    /* 13*/     new StoryBlock(13, "Who would have thought that going downhill can sometimes be more difficult than going uphill? The sharp rocks present do not make the task any easier. Despite the excitement, you calm each other down and stay calm. If anything should happen, you can be sure you can rely on yourself, but it's always better to be careful ...", "March on", "March on", "March on","Go towards the fortress",  14, 14, 14),        // 13
    /* 14*/     new StoryBlock(14, "You have been marching through a narrow canyon for a few hours, the rocks are close enough that you touch them with your hand with fascination. They don't look like any material you've ever seen in your life. It resembles a mixture of ash and granite. The path sometimes leads over the canyon, so every now and then you make sure that you are going in the right direction...", "March on", "March on", "March on","March on", 15, 15, 15), // 14
    /* 15*/     new StoryBlock(15, "You reach the next hill, it seems to be a good enough place to check the fortress again...", "Look at the towers", "Look at the main entrance", "Look at the bridge","March on", 16, 17, 18), // 15                                                                                                                                                                                                    // 15
    /* 16*/     new StoryBlock(16, "The powerfully built towers glisten with black soot. At the top of them, you see something disturbing... sculptures? Perhaps... you observe the sculptures for a few seconds with concern. They look like strange birds, maybe gargoyles? You've heard legends about them. Perhaps it's just sculptures. Although at this point you need to be careful...The towers are standing on the sides of the right entrance", "Look at the main entrance", "Look at the bridge", "Decide to go this way","Look at the towers", 17, 18, 19), // 16               
    /* 17*/     new StoryBlock(17, "You look at the entrance. It is located in the middle of two towers. It looks abandoned and dusty. At its end you can see an old metal gate, most likely the entrance to the fort.", "Look at the towers", "Look at the bridge", "Decide to go this way","Look at the main entrance",  16, 18, 20),      // 17                                                                                                                                       // 17
    /* 18*/     new StoryBlock(18, "A small bridge located right in front of the main entrance. Ironically, your attention is drawn to the entrance to the sewers. It looks big enough for a human to enter...", "Look at the towers", "Look at the main entrance", "Decide to go this way","Look at the bridge",  16, 17, 21),                                   //18
    /* 19*/     new StoryBlock(19, "After deliberation, your team decides to go through the side entrace ", "Carefuly go forward", "Carefuly go forward", "Carefuly go forward","Go to Side entrance", 22, 22, 22),    // 19  
    /* 20*/     new StoryBlock(20, "After discussion, your team comes to the conclusion that it is too dangerous to go through the main entrance. Well, there are still two more options...", "Choose to go through the sewers", "Decide to go through the side entrance", ". . .","Decide to go this way", 21, 19, 0),   // 20
    /* 21*/     new StoryBlock(21, "After deliberation, your team decides to go through the sewers", "Carefuly go forward", "Carefuly go forward", "Carefuly go forward","Go to the sewers", 23, 23, 23),              //21
    /* 22*/     new StoryBlock(22, "You approach what appears to be a side tower of a fortress and break down the door. The stairs in the Tower lead up to the walls and down... Exactly, where? Someone may see you on the walls, but you will see a large part of the fortress, but at the bottom there may be a passage to the center of the fortress..." ,"Go upstairs", "Go downstairs" , ". . .", "Cerfuly go forward", 31, 32, 22),                              // 22
////////////////////////////////////////////////////////////////////// THE SEWERS LEVEL ///////////////////////////////////////////////////////////////////////////////
    /* 23*/     new StoryBlock(23, "As you enter the sewers, it slowly gets darker and darker. You are disoriented by the lack of a light source. The wall is your only reference point. It's hard to tell how long you've been walking in such darkness. Suddenly - like a flash of eyes - you see something crawling towards you. ", "Start screaming like a little girl", "Start singing an old tavern chant", "Stand like a pole and do nothing","Carefuly go forward", 24, 25, 26),        //23
    /* 24*/     new StoryBlock(24, "The shape that was approaching you was...a Goblin! Screams do not impress him much, after all, he has seen and heard many things in these sewers. The goblin says he'll show you a faster way to the castle... in exchange for a gold ring!", "Give him the ring", "Don't trust the goblin", "Tell the goblin, that you dont have the ring","Start screaming like a little girl", 27, 23, 23),   //24                                                       //24
    /* 25*/     new StoryBlock(25, "The shape that was approaching you was...a Goblin! The goblin doesn't seem to understand what's going on (you can't blame him - he's never been to the tavern after all). The goblin says he'll show you a faster way to the castle... in exchange for a gold ring!", " Give him the ring", "Don't trust the goblin", "Tell the goblin, that you dont have the ring","Start singing an old tavern chant", 27, 23, 23),   //25
    /* 26*/     new StoryBlock(26, "The shape that was approaching you was...a Goblin! Despite the lack of reaction on your part to his presence - it does not prevent him from making you an offer. The goblin says he'll show you a faster way to the castle... in exchange for a gold ring!", "Give him the ring", "Don't trust the goblin", "Tell the goblin, that you dont have the ring","Stand like a pole and do nothing", 27, 23, 23),   //26
    /* 27*/     new StoryBlock(27, "You pull a shimmering ring out of your pocket. The goblin struggles to keep his balance at the sight of the ring (that's right, he doesn't crawl anymore). It tells you that the fastest way to the fortress is through the northern entrance. Considering that you have only seen one long corridor so far, this answer seems very puzzling. After all, you thank the goblin and move on.", "March on", "March on", "March on"," ", 30, 30, 30),       //27                                                                                                                                                //27
    /* 28*/     new StoryBlock(28, "The Goblin looked at you in surprise - after all, it would be very profitable for the two of you to do so. You see him slowly retreat into a hole in the floor and disappear...", "March on", "March on", "March on"," ", 30, 30, 30),
    /* 29*/     new StoryBlock(29, "The Goblin is watching you intently... You see him walking slowly, sniffing the leg of your pants, only to quickly retreat and disappear somewhere in a hole in the floor...", "March on", "March on", "March on"," ", 30, 30, 30),
    /* 30*/     new StoryBlock(30, "This is where the story ends... written by the author. Because certainly not the one experienced by our heroes! If you want to see what the alternative entrance looks like, vote for this option at the bottom! Thank you player for choosing to play this demo!", "(Come back to the lava moutains)", "(Go visit The Side Entrance)", ". . .","March on", 0, 29, 30),
////////////////////////////////////////////////////////////////////// SIDE ENTRANCE LEVEL ///////////////////////////////////////////////////////////////////////////////
    /* 31*/     new StoryBlock(31, "You go up the stairs. There are many more steps than you thought. Upstairs you are greeted by a creaking door that opens without any problem. You are on the walls on the east side of the fortress. The main square seems deserted, you can see the training equipment left on it. Your attention is drawn to the tallest of the fortress towers - perhaps this is where you should go? But for now what you need to find the any entrance...", "Look at the main square", "Look to the right, beyond the fortress", "Take a look at the tallest tower","Go upstairs", 32, 33, 34), // 31
    /* 32*/     new StoryBlock(32, "On the square there are a lot of training puppets and a lot of crates under cover. You have a feeling that there is a weapon in them. In the middle of the square on a small hill there is a strip. It has long looked unused. Strange...", "Continue walking on the walls...", "Continue walking on the walls...", "Continue walking on the walls...","Look at the main square", 39, 39, 39), // 32
    /* 33*/     new StoryBlock(33, "You look around in amazement at the fortress built on the rock and surrounded by a lake of lava. You wonder about the process and effort that must have gone into creating it. But are you sure? You don't know if the fortress belonged to the mages from the beginning. On the horizon there are ravines made of characteristic rock with the consistency of dust and granite, you see smoke and fire...", "Continue walking on the walls...", "Continue walking on the walls...", "Continue walking on the walls...","Look to the right, beyond the fortress", 39, 39, 39), // 33
    /* 34*/     new StoryBlock(34, "The tallest of the towers is the most characteristic. Not because it is the highest. At the top there are matte red finishes and unique decorative stained glass windows. It definitely looks like it's hiding something interesting inside. But first you have to find the entrance to it...", "Continue walking on the walls...", "Continue walking on the walls...", "Continue walking on the walls...","Take a look at the tallest tower", 39, 39, 39), // 34
    /* 35*/     new StoryBlock(35, "The descent down seems exceptionally deep. Twilight quickly turns into complete darkness. You pull one of the torches off the wall and light up your way down. After some time, you reach a room that seems to be sleeping quarters, as indicated by the evenly distributed bunks against the wall.", "Look around the room", "Take a look at the beds", "Go under the armor stand","Go downstairs", 36, 37, 38), // 35
    /* 36*/     new StoryBlock(36, "You look around the room and your attention is caught by a painting hanging on the wall. Quite an unusual addition in such a place ... But what is it? You look closely and see the inscription \"232\" engraved in the corner. What does this mean?", "Leave the room", "Leave the room", "Leave the room","Look around the room", 39, 39, 39),
    /* 37*/     new StoryBlock(37, "You look at equally made beds. Nothing out of the ordinary. However, your intuition tells you to check what is under the pillow in each of the beds. Nothing... nothing... nothing... wait...there is something! Under one of the pillows you find a key!", "Leave the room", "Leave the room", "Leave the room","Take a look at the beds", 39, 39, 39),
    /* 38*/     new StoryBlock(38, "You're looking at the armor stand. It doesn't look new, but it doesn't look too worn either. Your attention is drawn to the strangely cut right arm of the armor. It seems that whoever was wearing it must have been attacked from the right...", "Leave the room", "Leave the room", "Leave the room","Go under the armor stand", 39, 39, 39),  
    /* 39*/     new StoryBlock(39, "This is where the story ends... written by the author. Because certainly not the one experienced by our heroes! If you want to see what the alternative entrance looks like, vote for this option at the bottom! Thank you player for choosing to play this demo!", "(Come back to the lava moutains)", "(Go visit the Sewers)", ". . .","Carefuly go forward", 0, 21, 39),                                                                      //30
    //* x */     new StoryBlock(x, " ", " ", " ", " "," ", x, x, x),  
    };

        private void DisplayBlock(StoryBlock block) {   // This method describes what will be display "inside" my UI objects
                                                        // It takes whatever is currently asigned in storyblock element and connect it to
                                                        // corresponding UI objects, in order to display that for the player
                                                       
        mainText.text = block.story;                    
        option1.GetComponentInChildren<TMP_Text>().text = block.option1Text;
        option2.GetComponentInChildren<TMP_Text>().text = block.option2Text;
        option3.GetComponentInChildren<TMP_Text>().text = block.option3Text;
        RecentOption.GetComponentInChildren<TMP_Text>().text = block.option4Text; // Display last option text
        currentBlock = block;
       
    }
    // Option Button nr1(left)
    public void DisplayOption1(){
        DisplayBlock(storyBlocks[currentBlock.option1BlockId]);
        if(RandomNumberChanceToDropAnItem == 2)                     // Each time this method is called, also check if someone
        {                                                           // have their RandomNumber equal to 2, which means that they found an item.
        NotificationAboutItem.SetActive(true);                      // If yes, open the notification panel for them
         RandomNumberChanceToDropAnItem= 0;
        }  
    }
    // Option Button nr2(middle)
    public void DisplayOption2(){
        DisplayBlock(storyBlocks[currentBlock.option2BlockId]);
        if(RandomNumberChanceToDropAnItem == 2)
        {
        NotificationAboutItem.SetActive(true);
         RandomNumberChanceToDropAnItem= 0;
        } 
    }
    // Option Button nr3(right)
    public void DisplayOption3(){
        DisplayBlock(storyBlocks[currentBlock.option3BlockId]); 
        if(RandomNumberChanceToDropAnItem == 2)
        {
        NotificationAboutItem.SetActive(true);
         RandomNumberChanceToDropAnItem= 0;
        }
    
     }
#endregion

#region ShoeIcon=PlayerIsReady
    // Recognize what player is pressing on the shoe icon, display the corresponding icon according to what kind of player it is
    // This allows other players to see who is ready to vote and see their avatar (icon) at the bottom of the game panel
    public void PlayersUnderstoodText()
    {
        HowToPlayInfo_Panel.SetActive(false); // Disable tutorial page information
        ShoeButton.interactable = false;      // Once this button is clicked, set it back to interactable = false
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
    RedPlayer_Icon.SetActive(true);     // Show the red icon -> a visual representation of host and first player
    IntPlayersAreReady++;               // Every time a player clicks on "shoe" button the int "IntPlayerAreReady" increases its value by 1. When it hits 3
                                        // that means that all the players have understood game text and option buttoncs can be visible for them and they can start voting.
    if(IntPlayersAreReady == 3)
    {   
        Timer = StartCoroutine(HourGlassTimer());       // Start the Coroutine of HourGlass
        HourGlassAnim.SetTrigger("HourGlassTrigger");   // Start the HourGlass Animation(it takes 30sec to start and finish the sequence)
        PanelWithOptionButtons.SetActive(true);         // Make UI Buttons visible
        IntPlayersAreReady = 0;                         // Set the intiger back to 0 after every player pressed the button.
        Invoke("ClosingUIPanels", 0);                   // Close all unnecessary UI elements

    }
}
[ClientRpc]
private void RpcIUnderstandYELLOW(){                    // Show the yellow icon -> a visual representation of second player
    YellowPlayer_Icon.SetActive(true);
    IntPlayersAreReady++;
      if(IntPlayersAreReady == 3)
    {   
        Timer = StartCoroutine(HourGlassTimer());
        HourGlassAnim.SetTrigger("HourGlassTrigger");
        PanelWithOptionButtons.SetActive(true); 
        IntPlayersAreReady = 0;                    
        Invoke("ClosingUIPanels", 0);  
    }
}
[ClientRpc]
private void RpcIUnderstandGREEN(){
    BluePlayer_Icon.SetActive(true);                    // show the green icon -> a visual representation of third player
    IntPlayersAreReady++;
      if(IntPlayersAreReady == 3)
    {   
        Timer = StartCoroutine(HourGlassTimer());
        HourGlassAnim.SetTrigger("HourGlassTrigger");
        PanelWithOptionButtons.SetActive(true); 
        IntPlayersAreReady = 0;                    
        Invoke("ClosingUIPanels", 0);  
    }
}
private void ShoeButtonSetActive()       // The method that makes the "shoe" icon be interactable again. 
                                         // It is written in this format, because later on in the code
{                                        // It is invoked with some delay.
     if(ShoeButton.interactable == false)
    {
         ShoeButton.interactable = true;
    }
}
#endregion

#region VotingConditions/Methods
// These methods below includes what happens with the game, depending on what are the votes.
// This method is executed when all 3 players cast their votes. If at least one of them doesn't vote
// before HourGlass timer is finished, HourGlass Coroutine conditions will be executed instead of these ones below.
private void Option1V1Option2V1Option3V1()// There was one vote for each option
{                
    if(RandomNumber== 0)                  // If the RandomNumber is 0, behave like option1 button is pressed
    {  
    StartCoroutine(RandomPanel());        // Display the UI panel for x seconds
    Invoke("DisplayOption1", 5);          // Invoke in this context means "Display with a delay of 5 seconds" the method in the brackets.
    RandomNumber =0;                      // if Random number is equal to 0 we want to display a story block with an ID behind option1 button.
    }
    if(RandomNumber== 1)
    {    
    StartCoroutine(RandomPanel());
    Invoke("DisplayOption2", 5);
    RandomNumber =0; 
    }
    if(RandomNumber == 2)
    {    
    StartCoroutine(RandomPanel());
    Invoke("DisplayOption3", 5);
    RandomNumber =0;                       
    }
    ResetVotesAndButtonsUI();
}
private void Option1V2Option2V1()         // Option1 > Option 2
{       
        Invoke("DisplayOption1", 5);
        ResetVotesAndButtonsUI();
        PanelWithOptionButtons.SetActive(false);
        StartCoroutine(RandomDecisionOption1());
}
private void Option1V2Option3V1()        // Option 1 > Option 3
{       
        Invoke("DisplayOption1", 5);
        ResetVotesAndButtonsUI();
        PanelWithOptionButtons.SetActive(false);
        StartCoroutine(RandomDecisionOption1());
}
private void Option2V2Option1V1()        // Option2 > Option 1
{       
        Invoke("DisplayOption2", 5);
        ResetVotesAndButtonsUI();
        PanelWithOptionButtons.SetActive(false);  
        StartCoroutine(RandomDecisionOption2());
}
private void Option2V2Option3V1()       // Option 2 > Option 3
{
        Invoke("DisplayOption2", 5);
        ResetVotesAndButtonsUI();
        PanelWithOptionButtons.SetActive(false); 
        StartCoroutine(RandomDecisionOption2());
}
private void Option3V2Option1V1()       // Option 3 > Option 1
{       
        Invoke("DisplayOption3", 5);
        ResetVotesAndButtonsUI();
        PanelWithOptionButtons.SetActive(false); 
        StartCoroutine(RandomDecisionOption3());
}
private void Option3V2Option2V1()       // Option 3 > Option 2
{
        Invoke("DisplayOption3", 5);
        ResetVotesAndButtonsUI();
        PanelWithOptionButtons.SetActive(false); 
        StopCoroutine(Timer);
}

private void Option1V3()                // Option 1 Votes 3
{
        Invoke("DisplayOption1", 0);
        ResetVotesAndButtonsUI();
        PanelWithOptionButtons.SetActive(false);

}
private void Option2V3()                // Option 2 Votes 3
{       
        Invoke("DisplayOption2", 0);
        ResetVotesAndButtonsUI();
        PanelWithOptionButtons.SetActive(false); 
}
private void Option3V3()                // Option 1 Votes 3
{    
        Invoke("DisplayOption3", 0);
        ResetVotesAndButtonsUI();
        PanelWithOptionButtons.SetActive(false);
}
private void ResetVotesAndButtonsUI()
{   
    HourGlassAnim.SetTrigger("HourGlassTriggerBack");   // Stopping hour glass animation
    option1.interactable = true;                        // we want to make our option button clickable again and set the back to = true
                                                        // so that the players can press them again.
    option2.interactable = true;
    option3.interactable = true;
    Option1Votes =0;                                    // Set our votes back to 0
    Option2Votes =0;
    Option3Votes =0;
    StopCoroutine(Timer);                               // Stop HourGlass Coroutine
    NumberGenForDecision();                             // Generate a RandomNumber to randomize the next decision.
    NumberGenForItems();                                // Generate  new RandomNumberChanceToDropAnItem, basically a drop chance for an item each time the voting is finished
    Invoke("ShoeButtonSetActive", 5);                   // We can click shoe icon again
    Invoke("ClosingUIPanels", 0);                       // Close other UI Panels
    Invoke("CloseYesNoPanels", 0);
}
#endregion

#region OPTION1 BUTTON
 public void OptionCheck1()                         // These methods are similar for each option button.
                                                    // I will include the description here, it doesn't change much for the other buttons below(other regions with buttons conditions)
                                                    // This function is triggered when a player clicks the first button on the left(option1)
 {                                                  // This checks if other panels are active and if yes, it close them. 
                                                    // But mainly, it opens up "YES NO" panels
     OptionButton1YesNo_Panel.SetActive(true);
     if(OptionButton2YesNo_Panel.activeSelf)
        {
            OptionButton2YesNo_Panel.SetActive(false);
        }
         if(OptionButton3YesNo_Panel.activeSelf)
        {
            OptionButton3YesNo_Panel.SetActive(false);
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
        option1.interactable = false; // When we confirm our vote by pressing "YES", we disable other option buttons
        option2.interactable = false;
        option3.interactable = false; 
        CloseYesNoPanels();

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
    if(OptionButton1YesNo_Panel.activeSelf)
        {
            OptionButton1YesNo_Panel.SetActive(false);  
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
            Yellow_PlayerVote1_Icon.SetActive(true);   
        }

         if(Option1Votes == 2)            
        {
            Yellow_PlayerVote1_Icon.SetActive(true);   
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
            Red_PlayerVote1_Icon.SetActive(true);   
        }
         if(Option1Votes == 2)            
        {
            Red_PlayerVote1_Icon.SetActive(true);   
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
            Blue_PlayerVote1_Icon.SetActive(true);   
        }
         if(Option1Votes == 2)            
        {
            Blue_PlayerVote1_Icon.SetActive(true);   
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
     OptionButton2YesNo_Panel.SetActive(true);
     if(OptionButton1YesNo_Panel.activeSelf)
        {
            OptionButton1YesNo_Panel.SetActive(false);
        }
         if(OptionButton3YesNo_Panel.activeSelf)
        {
            OptionButton3YesNo_Panel.SetActive(false);
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
        option1.interactable = false; // When we confirm our vote by pressing "YES", we disable other option buttons
        option2.interactable = false;
        option3.interactable = false; 
        CloseYesNoPanels();
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
            Yellow_PlayerVote2_Icon.SetActive(true);   
        }
         if(Option2Votes == 2)            
        {
            Yellow_PlayerVote2_Icon.SetActive(true);   
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
            Red_PlayerVote2_Icon.SetActive(true);   
        }
         if(Option2Votes == 2)            
        {
            Red_PlayerVote2_Icon.SetActive(true);   
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
            Blue_PlayerVote2_Icon.SetActive(true);   
        }
         if(Option2Votes == 2)            
        {
            Blue_PlayerVote2_Icon.SetActive(true);   
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
  OptionButton3YesNo_Panel.SetActive(true);
     if(OptionButton2YesNo_Panel.activeSelf)
        {
            OptionButton2YesNo_Panel.SetActive(false);
        }
         if(OptionButton1YesNo_Panel.activeSelf)
        {
            OptionButton1YesNo_Panel.SetActive(false);
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
        option1.interactable = false; // When we confirm our vote by pressing "YES", we disable other option buttons
        option2.interactable = false;
        option3.interactable = false; 
        CloseYesNoPanels();
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
            Red_PlayerVote3_Icon.SetActive(true);   
        }
         if(Option3Votes == 2)            
        {
            Red_PlayerVote3_Icon.SetActive(true);   
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
            Blue_PlayerVote3_Icon.SetActive(true);   
        }
         if(Option3Votes == 2)            
        {
            Blue_PlayerVote3_Icon.SetActive(true);   
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
            Yellow_PlayerVote3_Icon.SetActive(true);   
        }
         if(Option3Votes == 2)            
        {
            Yellow_PlayerVote3_Icon.SetActive(true);   
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



