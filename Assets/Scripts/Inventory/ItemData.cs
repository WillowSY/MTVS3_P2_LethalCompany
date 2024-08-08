using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public Sprite icon;
    public GameObject itemPrefab;
    public int weight;
    public int price;
}
