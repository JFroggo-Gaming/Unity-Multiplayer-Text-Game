using UnityEngine;
using UnityEngine.UI;
using TMPro;

// IN THIS SCRIPT: Simple UI that shows the desription of the item that the player is currently with the mouse over
public class ItemInfoUpdate : MonoBehaviour
{
   
    // infoPanel is the parent object of the Information Panel
    public GameObject infoPanel;

    // The following UI components can be modified in some form that conveys you
    // Curently the Information Panel will show the name and icon of the item selected
    // The Description is 
    public TMP_Text  nameText;

    public TMP_Text  desciptionText;
    public Image icon;

    public void UpdateInfoPanel(Item itemInfo)
    {
        if (itemInfo != null)
        {
            // Set the Information Panel Active
            infoPanel.SetActive(true);

            //Change the name of the Item Selected in the UI
            nameText.text = itemInfo.itemName;
            desciptionText.text = itemInfo.desciptionText;
            icon.sprite = itemInfo.itemIcon;
        }
        else
        {
            // If there is no item in the slot, it sets the panel off
            infoPanel.SetActive(false);
        }
    }

    // Set the Information Panel Inactive (this is triggered if the player takes the mouse from over any item in the UI)
    public void ClosePanel()
    {
       
        infoPanel.SetActive(false);
    }
}
