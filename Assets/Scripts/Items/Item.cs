using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    //public CircleCollider2D cc2d = new CircleCollider2D();
    //public TextMesh tm = new TextMesh();

    public virtual void Use()
    {


        Debug.Log("Using " + name);
    }


    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
