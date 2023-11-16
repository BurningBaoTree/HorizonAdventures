using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum ItemCode
{
    GoldCoin = 0,
    SilverCoin,
    CaperCoin,
    Woods,
    Rocks,
    MonsterCore
}

public class SlotCellManager : MonoBehaviour
{
    GridLayoutGroup GL;

    /// <summary>
    /// 셀 프리펩
    /// </summary>
    public GameObject Cell;

    /// <summary>
    /// 가방 크기
    /// </summary>
    public Vector2Int BagSize;

    /// <summary>
    /// 총 셀 개수
    /// </summary>
    public int totalCellCount;

    /// <summary>
    /// 슬롯 리스트
    /// </summary>
    public List<SlotCellData> CellDatas = new List<SlotCellData>();

    private void Awake()
    {
        GL = GetComponent<GridLayoutGroup>();
        MakeBag();
    }

    /// <summary>
    /// 배낭 생성 함수
    /// </summary>
    void MakeBag()
    {
        //현재 잔류하는 셀들 없애고
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        //가방 사이즈 다시 조절해서
        GL.constraintCount = BagSize.x;
        totalCellCount = BagSize.x * BagSize.y;

        //셀 제작
        for (int i = 0; i < totalCellCount; i++)
        {
            GameObject Cellobject = Instantiate(Cell);
            Cellobject.name = $"{i}_Cell";
            Cellobject.transform.SetParent(this.transform, false);
            SlotCellData CellDataCom = Cellobject.GetComponent<SlotCellData>();
            CellDataCom.MadeNum = i;
            CellDatas.Add(CellDataCom);
        }
    }

}
