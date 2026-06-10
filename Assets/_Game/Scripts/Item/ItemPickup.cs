using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] Item item;

    public string GetItemName() => item != null ? item.itemName : "";

    public void Interact()
    {
        if (item == null) return;

        if (item.isFlashlight)
        {
            Inventory.Instance.HasFlashlight = true;
            UIManager.Instance.ShowDialogue(new string[] {
                "E키를 눌러 손전등을 킬 수 있을 것 같다.",
                "전원 이외에도 버튼이 있다. R키를 눌러 버튼을 누를 수 있을 것 같다."
            });
        }
        else
            Inventory.Instance.AddItem(item);

            SFXManager.Instance.PlaySFX("item_get");

        gameObject.SetActive(false);
    }
}
