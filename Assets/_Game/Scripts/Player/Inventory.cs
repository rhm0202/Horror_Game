using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] GameObject flashlightObject;

    bool _hasFlashlight = false;
    public bool HasFlashlight
    {
        get => _hasFlashlight;
        set
        {
            _hasFlashlight = value;
            if (flashlightObject != null)
                flashlightObject.SetActive(value);
        }
    }
    readonly List<Item> items = new List<Item>();
    public Item SelectedItem { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddItem(Item item) => items.Add(item);
    public bool HasItem(Item item) => items.Contains(item);
    public void SelectItem(Item item) => SelectedItem = item;
    public void ClearSelection() => SelectedItem = null;
}
