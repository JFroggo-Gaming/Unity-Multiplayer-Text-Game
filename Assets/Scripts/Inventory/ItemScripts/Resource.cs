using UnityEngine;
[CreateAssetMenu(fileName = "Resource", menuName = "Item/Resource")]
public class Resource : Item
{
    public resourceType type;

    public override void Use()
    {
        base.Use();

        //Use Resource

        //Use the following line if you want to destroy this type of item after use
        // Inventory.instance.RemoveItem(this, 1);
    }

    public enum resourceType { Wood, Herb, Ore }
}

