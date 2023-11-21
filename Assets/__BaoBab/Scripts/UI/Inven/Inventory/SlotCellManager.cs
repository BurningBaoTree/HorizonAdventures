using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 슬롯 셀들을 관리하는 코드
/// </summary>
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
    public List<SlotCellData> compairedCell = new List<SlotCellData>();

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
        for (int i = 0; i < transform.childCount; i++)
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

    /// <summary>
    /// 모든 셀을 비교하여 cellcount보다 숫자보다 StackMode의 숫자가 작을 경우 인벤토리에 넣지 못한것으로 반단해서 반환
    /// </summary>
    /// <param name="cellcount">아이템의 부피</param>
    /// <returns></returns>
    public bool ACCCanStack(int cellcount)
    {
        compairedCell.Clear();
        int count = 0;
        foreach (SlotCellData cell in CellDatas)
        {
            if (cell.tryToSet)
            {
                compairedCell.Add(cell);
                count++;
            }
        }
        bool result;
        if (count == cellcount)
        {
            result = true;
            foreach (SlotCellData cell in compairedCell)
            {
                cell.IsSet = true;
                InventoryInfo.Inst.BagParent.CellCenters.Add(cell.transform.position);
            }
        }
        else
        {
            result = false;
        }
        return result;
    }
}
