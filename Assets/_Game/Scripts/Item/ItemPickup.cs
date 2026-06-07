using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] Item item;

    public string GetItemName() => item != null ? item.itemName : "";

    public void Interact()
    {
        if (item == null) return;

        if (item.isFlashlight)
            Inventory.Instance.HasFlashlight = true;
        else
            Inventory.Instance.AddItem(item);

        gameObject.SetActive(false);
    }
}
