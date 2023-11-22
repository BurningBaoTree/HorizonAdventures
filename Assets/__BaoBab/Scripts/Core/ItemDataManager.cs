using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    MainWeapon,
    SubWeapon,
    Item
}

public enum ItemCode
{
    GoldCoin = 0,
    SilverCoin,
    CaperCoin,
    Woods,
    Rocks,
    MonsterCore,
    Assult,
    Sashimi,
    HandGun,
    WoodStick,
    IronSword,
    Shotgun,
}

public enum ItemSize
{
    size1x1 = 0,
    size1x2,
    size1x3,
    size2x3,
    size3x3,
    size4x2,
    size4x4,
    size5x2
}



public class ItemDataManager : MonoBehaviour
{
    /// <summary>
    /// 모든 아이템 종류에 대한 배열
    /// </summary>
    public ItemData[] itemDatas = null;

    /// <summary>
    /// 아이템 종류별 접근을 위한 인덱서
    /// </summary>
    /// <param name="code">접근할 아이템의 코드</param>
    /// <returns>아이템 데이터</returns>
    public ItemData this[ItemCode code] => itemDatas[(int)code];

    /// <summary>
    /// 아이템 종류별 접근을 위한 인덱서(테스트용)
    /// </summary>
    /// <param name="index">접근할 아이템의 인덱스</param>
    /// <returns></returns>
    public ItemData this[int index] => itemDatas[index];

    /// <summary>
    /// 지금 존재하는 아이템 종류의 모든 갯수
    /// </summary>
    public int length => itemDatas.Length;
}
