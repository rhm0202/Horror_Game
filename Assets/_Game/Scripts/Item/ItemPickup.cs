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

            SFXManager.Instance.PlaySFX("item_get");

        gameObject.SetActive(false);
    }
}
