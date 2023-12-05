using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 오브젝트 코드
/// </summary>
public class ItemObject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public ItemData itemData = null;
    public ItemData ItemData
    {
        get => itemData;
        set
        {
            if (itemData == null)    // 팩토리에서 한번 설정가능하도록
            {
                itemData = value;
            }
        }
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        if (ItemData.type == ItemType.Item)
        {
            spriteRenderer.sprite = ItemData.itemIcon;
        }
    }
}
