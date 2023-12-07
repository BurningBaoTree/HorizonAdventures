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
    public bool ACCCanStack(ItemData tem)
    {
        //비교슬롯을 비워준 다음
        compairedCell.Clear();
        //비교용 숫자도 0으로 초기화
        int count = 0;
        //셀 데이터의 모든 셀들이 저장모드일때 해당 셀을 비교셀에 저장 그리고 카운트값 증강
        foreach (SlotCellData cell in CellDatas)
        {
            if (cell.tryToSet)
            {
                compairedCell.Add(cell);
                count++;
            }
        }
        //결과용 bool값
        bool result;
        //카운트값이 슬롯사이즈와 같다면
        if (count == tem.slotSize)
        {
            //결과는 참
            result = true;
            //비교 셀의 모든 저장값을 참으로 변환하고 셀의 가운데를 정하는 리스트에 셀을 입력
            foreach (SlotCellData cell in compairedCell)
            {
                cell.IsSet = true;
                InventoryInfo.Inst.BagParent.CellCenters.Add(cell);
            }
            //아이템을 가방에 추가한다.
            InventoryInfo.Inst.BagParent.PutItemInTheBag(tem);
        }
        //카운트값이 슬롯사이즈와 같지 않다면
        else
        {
            //결과는 거짓
            result = false;
        }
        //반환
        return result;
    }
    public bool ACCCanStack(EquiptBase tem)
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

        if (count == tem.temData.slotSize)
        {
            result = true;
            foreach (SlotCellData cell in compairedCell)
            {
                cell.IsSet = true;
                InventoryInfo.Inst.BagParent.CellCenters.Add(cell);
            }
            InventoryInfo.Inst.BagParent.PutItemInTheBag(tem);
        }
        else
        {
            result = false;
        }
        return result;
    }
    public bool ACCCanStack(SubWeaponBase tem)
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

        if (count == tem.temData.slotSize)
        {
            result = true;
            foreach (SlotCellData cell in compairedCell)
            {
                cell.IsSet = true;
                InventoryInfo.Inst.BagParent.CellCenters.Add(cell);
            }
            InventoryInfo.Inst.BagParent.PutItemInTheBag(tem);
        }
        else
        {
            result = false;
        }
        return result;
    }

    /// <summary>
    /// 아이템이 인벤토리로 들어오는 코드
    /// </summary>
    /// <param name="tem"></param>
    public void addItmeToPossablePos(ItemData tem)
    {
        bool result = true;
        TempSlot tmepslot = InventoryInfo.Inst.temp;
        foreach (SlotCellData cell in CellDatas)
        {
            //셀이 셋 상태가 아닐때
            if (!cell.isSet)
            {
                //세로 반복
                for (int j = 0; j < tmepslot.TextSizeList[(int)tem.size].y; j++)
                {
                    //가로 반복
                    for (int i = 0; i < tmepslot.TextSizeList[(int)tem.size].x; i++)
                    {
                        if (CellDatas[cell.MadeNum + i + (j * BagSize.x)].isSet)
                        {
                            result = false;
                        }
                        else
                        {
                            InventoryInfo.Inst.BagParent.CellCenters.Add(CellDatas[cell.MadeNum + i + (j * BagSize.x)]);
                        }
                    }
                }
                //배치가 가능할때
                if(result)
                {
                    InventoryInfo.Inst.BagParent.PutItemInTheBag(tem);
                    foreach(SlotCellData cells in InventoryInfo.Inst.BagParent.CellCenters)
                    {
                        cells.isSet = true;
                    }
                    break;
                }
                else
                {
                    InventoryInfo.Inst.BagParent.CellCenters.Clear();
                }
            }
        }
    }
}
