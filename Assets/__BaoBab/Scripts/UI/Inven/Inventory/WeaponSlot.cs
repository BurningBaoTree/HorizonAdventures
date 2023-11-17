using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour , UIInventoryController
{
    public Image weaponImage;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI LeftBullet;

    private void Awake()
    {
        weaponImage = transform.GetChild(0).GetComponent<Image>();
        NameText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        LeftBullet = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// 무기 슬롯 동기화
    /// </summary>
    /// <param name="imageWP"></param>
    /// <param name="NameWP"></param>
    /// <param name="NowState"></param>
    /// <param name="MaxState"></param>
    public void initializeWeaponSlot(Sprite imageWP, string NameWP, int NowState, int MaxState)
    {
        if (imageWP == null)
        {
            weaponImage.sprite = null;
            weaponImage.color = Color.clear;
        }
        else
        {
            weaponImage.sprite = imageWP;
            weaponImage.color = Color.white;
        }

        if(NameWP == null)
        {
            NameText.color = Color.clear;
        }
        else
        {
            NameText.color = Color.white;
        }

        if (MaxState == 0)
        {
            LeftBullet.color = Color.clear;
        }
        else
        {
            LeftBullet.color = Color.white;
            NameText.text = NameWP;
            LeftBullet.text = $"{NowState: 000} / {MaxState: 000}";
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("클릭");
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("드래그");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
}
