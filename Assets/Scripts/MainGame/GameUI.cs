using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GameUI : MonoBehaviour
{


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

public void ClosingUIPanels(){
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

public void CloseYesNoPanels()
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

}
