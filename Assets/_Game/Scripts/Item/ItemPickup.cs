using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] Item item;

    public void Pickup()
    {
        if (item == null) return;

        if (item.isFlashlight)
            Inventory.Instance.HasFlashlight = true;
        else
            Inventory.Instance.AddItem(item);

        gameObject.SetActive(false);
    }
}
