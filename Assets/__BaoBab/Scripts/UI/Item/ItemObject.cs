using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 오브젝트 코드
/// </summary>
public class ItemObject : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    ItemData itemData = null;
    public ItemData ItemDatas => itemData;
    private void OnEnable()
    {
        spriteRenderer.sprite = itemData.itemIcon;
    }
}
