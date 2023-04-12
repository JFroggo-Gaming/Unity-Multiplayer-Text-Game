
using UnityEngine;
[CreateAssetMenu(fileName = "Jewelry", menuName = "Item/Jewelry")]
public class Jewelry : Item
{
    public JewelryType type;
    public int JewelryHP;

    public override void Use()
    {
        base.Use();

        //Equip Action
        //The Equip Action is avaible in the "Inventory & Equip System - Drag & Drop", an extension of this package that can be purchased from the Unity asset Store in the following link:
        // https://assetstore.unity.com/packages/slug/209478

        //Use the following line if you want to destroy this type of item after use
        // Inventory.instance.RemoveItem(this, 1);
    }

    public enum JewelryType { Ring, Necklace,}
}
