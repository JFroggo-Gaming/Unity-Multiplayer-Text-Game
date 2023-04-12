using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// IN THIS SCRIPT: Inventory Slot Handler that shows the player one item and it's quantity based on the Inventory Script
public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //The item on the slot, if it's null the slot is considered empty
    Item item;

    // Each slots shows the icon and quantity of that item, the following are the references to those on the UI
    public Image itemImage;
    public Text quantity;

    // The remove Button is where the player clicks to remove the item in this slot
    public Button removeButton;


    // The following function is called everytime an item is added or removed from the inventory
    public void UpdateSlot(Item itemInSlot, int quantityInSlot)
    {
        item = itemInSlot;

        // If the item is null or the quantity 0 the slot is considered empty

        if (itemInSlot != null && quantityInSlot !=0)
        {
            // Slot has item: Enable the icon and Remove Button

            removeButton.enabled = true;
            itemImage.enabled = true; 
            
            itemImage.sprite = itemInSlot.itemIcon;

            //If the quantity on the slot is equal to one there is no necessity of enabling the quantity UI text
            if (quantityInSlot > 1)
            {
               
                quantity.enabled = true;
                quantity.text = quantityInSlot.ToString();
            }
            else
            {
                quantity.enabled = false;
                
            }

        }
        else
        {
            // Slot Empy: Disable the Icon, quantity and Remove Button
            
            removeButton.enabled = false;
            itemImage.enabled = false;
            quantity.enabled = false;
        }
    }

    // Called if the player mouses over this slot
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Tells the UI that shows the information of an item to appear and show it
        GetComponentInParent<ItemInfoUpdate>().UpdateInfoPanel(item);
    }

    // Called if the player take the mouse out of the slot borders
    public void OnPointerExit(PointerEventData eventData)
    {
        // Calls the function that sets the panel inactive
        GetComponentInParent<ItemInfoUpdate>().ClosePanel();
    }

    // Called when the player pressed the slot of the item(You can call it in other ways)
    public void UseItem()
    {
        //Checks if there is an item in the slot
        if (item != null)
        {
            // Use the item by calling the function of that specific item
             
            item.Use();
        }
    }

    // Called when the player presses the Remove Button corresponding to this slot
    public void RemoveItem()
    {
        // Removes item from the Inventory Script and consequently updates the UI(This occurs inside of RemoveItem())
        // Currently removing one piece on stackable objects
        Inventory.instance.RemoveItem(Inventory.instance.itemList[Inventory.instance.itemList.IndexOf(item)], 1);
    }
}
