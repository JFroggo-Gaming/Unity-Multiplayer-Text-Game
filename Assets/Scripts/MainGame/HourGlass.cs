using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourGlass : MonoBehaviour
{

private ItemDrop itemDrop;

private GameUI gameUI;

private StoryBlocks storyBlocks;

private VotingConditions votingConditions;

public Animator HourGlassAnim;  // The animation of HourGlass. 


public Coroutine Timer; // The reference to the Coroutine. In order to start and stop it properly(the Coroutine) it has to be keep in this format
                     // This Coroutine is triggered each time "IntPlayersAreReady" value is equal to 3(the buttons for voting becomes visible for players
                     // and they have x seconds determinated by the Coroutine to take their actions)


public  IEnumerator HourGlassTimer () {
 //1.      This Coroutine is triggered each time the option buttons shows up(after every player presses "shoe" icon)
        // If the players doesn't vote before the timer runs down, the Coroutine checks how many votes were cast(if any)
        // and based on that, displays adequate UI elements and moves the players to other story blocks using
        // "Invoke(DisplayOption)"  
                  

 //2.    Also checks the player's progress and when he is on the right text block, changes the UI accordingly.
         // If the player is in the sewers in the text, 
         // the text block with the sewers has the appropriate ID and a new UI for the player will be displayed based on it

//////////////////////////////////////////// - 2 - ///////////////////////////////////////////
// UI for levels
if(storyBlocks.currentBlock.ThisBlockID == 21) // The Sewers Level 
{
   gameUI.LavaLevel_Background.SetActive(false);
   gameUI.LavaLevel_Content.SetActive(false);
   gameUI.SideEntrance_Background.SetActive(false);
   gameUI.SideEntrance_Content.SetActive(false);
   gameUI.SewersLevel_Background.SetActive(true);
   gameUI.SewersLevel_Content.SetActive(true);
  
}
if(storyBlocks.currentBlock.ThisBlockID == 19) // Side Entrance Level
{
   gameUI.LavaLevel_Background.SetActive(false);
   gameUI.LavaLevel_Content.SetActive(false);
   gameUI.SewersLevel_Background.SetActive(false);
   gameUI.SewersLevel_Content.SetActive(false);
   gameUI.SideEntrance_Background.SetActive(true);
   gameUI.SideEntrance_Content.SetActive(true);
}

//////////////////////////////////////////////////////////////////////////////////////////
// Switching to less/more options to choose from (turning off buttons)
// Main entrance is too dangerous, two options, then unlock the button again : BLOCK 20
if(storyBlocks.currentBlock.ThisBlockID ==20)
{
     gameUI.option3.interactable = false;
}

if(storyBlocks.currentBlock.ThisBlockID ==21)
{
     gameUI.option3.interactable = true;
}
if(storyBlocks.currentBlock.ThisBlockID ==19)
{
     gameUI.option3.interactable = true;
}

// The Side Entrance, two stairs(up and down) : BLOCK 22
if(storyBlocks.currentBlock.ThisBlockID ==22)
{
     gameUI.option3.interactable = false;
}
if(storyBlocks.currentBlock.ThisBlockID ==31)
{
     gameUI.option3.interactable = true;
}
if(storyBlocks.currentBlock.ThisBlockID ==32)
{
     gameUI.option3.interactable = true;
}

// The game demo is finished, only two ways to pick from : BLOCK 30/39
if(storyBlocks.currentBlock.ThisBlockID ==30)
{
     gameUI.option3.interactable = false;
}
if(storyBlocks.currentBlock.ThisBlockID ==39)
{
     gameUI.option3.interactable = false;
}
if(storyBlocks.currentBlock.ThisBlockID ==0)
{
     gameUI.option3.interactable = true;
}
if(storyBlocks.currentBlock.ThisBlockID ==19)
{
     gameUI.option3.interactable = true;
}
if(storyBlocks.currentBlock.ThisBlockID ==21)
{
     gameUI.option3.interactable = true;
}

//////////////////////////////////////////// - 1 - ///////////////////////////////////////////
// Players have 30 seconds to vote. If they do not, based on their votes (or lack thereof), the Coroutine will take appropriate action
    yield return new WaitForSeconds(30);
     itemDrop.NumberGenForItems(); // Generate item find chances
     itemDrop.NumberGenForDecision(); // Generate a random number so you can make random decisions if needed
     if(votingConditions.Option1Votes == 0 && votingConditions.Option2Votes == 0 && votingConditions.Option3Votes == 0) // What happanes, when there are no votes at all.
     {  
    
        HourGlassVotingConditions();
        HourGlassVariablesReset();
         gameUI.HourGlassTimeOverZero.SetActive(true);   // The Panel informing about votes (In this case, that no one voted)
         gameUI.PanelWithOptionButtons.SetActive(false); // Options buttons are again invisible
        yield return new WaitForSeconds(5);      // After 5 seconds, the panel becomes invisible again.
         gameUI.HourGlassTimeOverZero.SetActive(false);  
        itemDrop.RandomNumber = 0;                        // Random number is set back to 0 after each times it is used.
    }        

    if(votingConditions.Option1Votes == 1 && votingConditions.Option2Votes == 1 && votingConditions.Option3Votes == 0)
     {  
    
        HourGlassVotingConditions();
        HourGlassVariablesReset();
         gameUI.RandomDecisionPanel.SetActive(true);
         gameUI.PanelWithOptionButtons.SetActive(false);
        yield return new WaitForSeconds(5);
         gameUI.RandomDecisionPanel.SetActive(false);
        itemDrop.RandomNumber = 0;
    }                                                                
    
    if(votingConditions.Option1Votes == 1 && votingConditions.Option2Votes == 0 && votingConditions.Option3Votes == 1)
     {  
    
        HourGlassVotingConditions();
        HourGlassVariablesReset();
         gameUI.RandomDecisionPanel.SetActive(true);
         gameUI.PanelWithOptionButtons.SetActive(false);
        yield return new WaitForSeconds(5);
         gameUI.RandomDecisionPanel.SetActive(false);
        itemDrop.RandomNumber = 0;
    }                        

    if(votingConditions.Option1Votes == 0 && votingConditions.Option2Votes == 1 && votingConditions.Option3Votes == 1)
     {  
     
        HourGlassVotingConditions();
        HourGlassVariablesReset();
         gameUI.RandomDecisionPanel.SetActive(true);
         gameUI.PanelWithOptionButtons.SetActive(false);
        yield return new WaitForSeconds(5);
         gameUI.RandomDecisionPanel.SetActive(false);
        itemDrop.RandomNumber = 0;
    }                        
     
    if(votingConditions.Option1Votes == 1 && votingConditions.Option2Votes == 0 && votingConditions.Option3Votes == 0){

    HourGlassVariablesReset();
    Invoke("DisplayOption1",5);
     gameUI.HourGlassTimeOver1OV1.SetActive(true);
     gameUI.PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
     gameUI.HourGlassTimeOver1OV1.SetActive(false);
     }

    if(votingConditions.Option2Votes == 1 && votingConditions.Option1Votes == 0 && votingConditions.Option3Votes == 0){

    HourGlassVariablesReset();
    Invoke("DisplayOption2",5);
     gameUI.HourGlassTimeOver2OV1.SetActive(true);
     gameUI.PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
     gameUI.HourGlassTimeOver2OV1.SetActive(false);
    }

    if(votingConditions.Option3Votes == 1 && votingConditions.Option1Votes == 0 && votingConditions.Option2Votes == 0){

    HourGlassVariablesReset();
    Invoke("DisplayOption3",5);
     gameUI.HourGlassTimeOver3OV1.SetActive(true);
     gameUI.PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
     gameUI.HourGlassTimeOver3OV1.SetActive(false);
    }

    if(votingConditions.Option1Votes == 2 && votingConditions.Option2Votes ==1) 
    {

    HourGlassVariablesReset();
    Invoke("DisplayOption1",5);
     gameUI.HourGlassTimeOver1OV2.SetActive(true);
     gameUI.PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
     gameUI.HourGlassTimeOver1OV2.SetActive(false);
            
    }
    if(votingConditions.Option1Votes == 2 && votingConditions.Option3Votes ==1) 
    {

    HourGlassVariablesReset();        
    Invoke("DisplayOption1",5);
     gameUI.HourGlassTimeOver1OV2.SetActive(true);
     gameUI.PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
     gameUI.HourGlassTimeOver1OV2.SetActive(false);
    }

    if(votingConditions.Option2Votes == 2 && votingConditions.Option1Votes ==1) 
    {

    HourGlassVariablesReset();
    Invoke("DisplayOption2",5);
     gameUI.HourGlassTimeOver2OV2.SetActive(true);
     gameUI.PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
     gameUI.HourGlassTimeOver2OV2.SetActive(false);

    }
    if(votingConditions.Option2Votes == 2 && votingConditions.Option3Votes ==1) 
    {

    HourGlassVariablesReset();
    Invoke("DisplayOption2",5);
     gameUI.HourGlassTimeOver2OV2.SetActive(true);
     gameUI.PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
     gameUI.HourGlassTimeOver2OV2.SetActive(false);
    }

    if(votingConditions.Option3Votes == 2 && votingConditions.Option1Votes ==1) 
    {

    HourGlassVariablesReset();
    Invoke("DisplayOption3",5);
     gameUI.HourGlassTimeOver3OV2.SetActive(true);
     gameUI.PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
     gameUI.HourGlassTimeOver3OV2.SetActive(false);
    }

    if(votingConditions.Option3Votes == 2 && votingConditions.Option2Votes ==1) 
    {

    HourGlassVariablesReset();
    Invoke("DisplayOption3",5);
     gameUI.HourGlassTimeOver3OV2.SetActive(true);
     gameUI.PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
     gameUI.HourGlassTimeOver3OV2.SetActive(false);
    }

    if(votingConditions.Option1Votes == 2){
    
    HourGlassVariablesReset();
    Invoke("DisplayOption1",5);
     gameUI.HourGlassTimeOver1OV2.SetActive(true);
     gameUI.PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
     gameUI.HourGlassTimeOver1OV2.SetActive(false);
     }

    if(votingConditions.Option2Votes == 2){

    HourGlassVariablesReset();
    Invoke("DisplayOption2",5);
     gameUI.HourGlassTimeOver2OV2.SetActive(true);
     gameUI.PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
     gameUI.HourGlassTimeOver2OV2.SetActive(false);
    }

    if(votingConditions.Option3Votes == 2){
    
    HourGlassVariablesReset();
    Invoke("DisplayOption3",5);
     gameUI.HourGlassTimeOver3OV2.SetActive(true);
     gameUI.PanelWithOptionButtons.SetActive(false);
    yield return new WaitForSeconds(5);
     gameUI.HourGlassTimeOver3OV2.SetActive(false);
    }

    HourGlassAnim.SetTrigger("HourGlassTriggerBack"); // After X seconds, the HourGlass animation is set back to defualt using this trigger in its Animator.
 }
private void HourGlassVariablesReset()
{   
    Invoke("CloseYesNoPanels", 0);
    Invoke("ClosingUIPanels", 0);
    Invoke("ShoeButtonSetActive", 0);       // Make "shoe" icon clickable again
    votingConditions.Option1Votes = 0;                       // Set votes value back to 0
    votingConditions.Option2Votes = 0;
    votingConditions.Option3Votes = 0;
     gameUI.option1.interactable = true;            // Options button are interactable again.
     gameUI.option2.interactable = true;
     gameUI.option3.interactable = true;
}
private void HourGlassVotingConditions()
{
      if(itemDrop.RandomNumber==0)                   // Depending of the outcome of Randomnumber (0-2)
        {                                   // a different option will be display for the players.
            Invoke("DisplayOption1",5);
            
        }
        if(itemDrop.RandomNumber==1)
        {
            Invoke("DisplayOption2",5);
           
        }
        if(itemDrop.RandomNumber==2)
        {
            Invoke("DisplayOption3",5);
            
        }
}

}
