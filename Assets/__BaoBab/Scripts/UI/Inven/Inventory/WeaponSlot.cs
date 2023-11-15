using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
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
    public void initializeWeaponSlot(Sprite imageWP, string NameWP, int NowState, int MaxState )
    {
        weaponImage.sprite = imageWP;
        NameText.text = NameWP;
        LeftBullet.text = $"{NowState: 000} / {MaxState: 000}";
    }
}
