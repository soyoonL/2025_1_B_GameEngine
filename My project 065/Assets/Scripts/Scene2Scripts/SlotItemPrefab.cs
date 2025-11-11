using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotItemPrefab : MonoBehaviour
{
    public Image itemImage;
    public Text itemText;
    public Text itemCountText;

    [Header("아이템 아이콘 등록")]
    public Sprite dirtSprite;
    public Sprite grassSprite;
    public Sprite waterSprite;

    public GameObject slotPrefab;
    public Transform slotParent;

    public void ItemSetting(Sprite itemSprite, string txt, int count)
    {
        itemImage.sprite = itemSprite;
        itemText.text = txt;
        itemCountText.text = count.ToString();
    }

    public void UpdateInventory(Inventory myInven)
    {
        foreach(Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }
        foreach (var item in myInven.items)
        {
            GameObject newSlot = Instantiate(slotPrefab, slotParent);
            SlotItemPrefab slot = newSlot.GetComponent<SlotItemPrefab>();   
            switch (item.Key)
            {
                case BlockType.Dirt:
                    slot.ItemSetting(dirtSprite, "Dirt", item.Value);
                    break;
                case BlockType.Grass:
                    slot.ItemSetting(grassSprite, "Grass", item.Value);
                    break;
                case BlockType.Water:
                    slot.ItemSetting(waterSprite, "Water", item.Value);
                    break;
            }
        }
    }
}
