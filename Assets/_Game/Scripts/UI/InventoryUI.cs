using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Image[] iconSlots = new Image[6];

    void Start()
    {
        Inventory.OnInventoryChanged += Refresh;
        Refresh();
    }

    void OnDestroy()
    {
        Inventory.OnInventoryChanged -= Refresh;
    }

    void Refresh()
    {
        var items = Inventory.Instance != null ? Inventory.Instance.GetItems() : new List<Item>();

        for (int i = 0; i < iconSlots.Length; i++)
        {
            if (iconSlots[i] == null) continue;

            if (i < items.Count && items[i].icon != null)
            {
                iconSlots[i].sprite = items[i].icon;
                iconSlots[i].enabled = true;
            }
            else
            {
                iconSlots[i].sprite = null;
                iconSlots[i].enabled = false;
            }
        }
    }
}
