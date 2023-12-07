using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public GameObject BaseItemOBJ;

    /// <summary>
    /// 
    /// </summary>
    public List<SlotCellData> CellCenters = new List<SlotCellData>();

    /// <summary>
    /// 
    /// </summary>
    public List<InvItemOBJ> AllItmes = new List<InvItemOBJ>();

    /// <summary>
    /// 
    /// </summary>
    TempSlot temp;

    private void Start()
    {
        temp = InventoryInfo.Inst.temp;
    }

    /// <summary>
    /// Temp에서 받아서 가방에 아이템 넣는 함수
    /// </summary>
    public void PutItemInTheBag(ItemData tem)
    {
        //오브젝트 생성(프리펩)
        GameObject obj = Instantiate(BaseItemOBJ);
        //아이템 컴포넌트 받아오기
        InvItemOBJ invItemOBJ = obj.GetComponent<InvItemOBJ>();
        //부모지정
        obj.transform.SetParent(transform, false);
        //위치는 이 코드가 가진 가운데 정렬용 리스트에서 가운데값
        obj.transform.position = calculateCenterPos();
        //아이템에 정보를 입력한다.
        invItemOBJ.MakeItemInfo(tem, temp.countInt);
        //아이템 관리용 리스트에 해당 아이템 저장
        AllItmes.Add(invItemOBJ);
        //아이템의 셀리스트에 지금 현재 아이템이 가지게 될 셀들의 정보를 입력시킨다.
        for (int i = 0; i < CellCenters.Count; i++)
        {
            invItemOBJ.cellOnIt.Add(CellCenters[i]);
        }
        //가운데 정렬용 리스트 초기화
        CellCenters.Clear();
    }
    public void PutItemInTheBag(SubWeaponBase tem)
    {
        GameObject obj = Instantiate(BaseItemOBJ);
        InvItemOBJ invItemOBJ = obj.GetComponent<InvItemOBJ>();
        obj.transform.SetParent(transform, false);
        obj.transform.position = calculateCenterPos();
        invItemOBJ.MakeItemInfo(tem, temp.countInt);
        AllItmes.Add(invItemOBJ);
        for (int i = 0; i < CellCenters.Count; i++)
        {
            invItemOBJ.cellOnIt.Add(CellCenters[i]);
        }
        CellCenters.Clear();
    }
    public void PutItemInTheBag(EquiptBase tem)
    {
        GameObject obj = Instantiate(BaseItemOBJ);
        InvItemOBJ invItemOBJ = obj.GetComponent<InvItemOBJ>();
        obj.transform.SetParent(transform, false);
        obj.transform.position = calculateCenterPos();
        invItemOBJ.MakeItemInfo(tem, temp.countInt);
        AllItmes.Add(invItemOBJ);
        for (int i = 0; i < CellCenters.Count; i++)
        {
            invItemOBJ.cellOnIt.Add(CellCenters[i]);
        }
        CellCenters.Clear();
    }

    /// <summary>
    /// 아이템을 생성과 동시에 적당한 위치에 배치하는 함수
    /// </summary>
    public void MakeAndPutInTheBag()
    {

    }

    /// <summary>
    /// 중심 위치 계산하는 함수
    /// </summary>
    /// <returns></returns>
    Vector2 calculateCenterPos()
    {
        float x = 0;
        float y = 0;
        foreach (SlotCellData cell in CellCenters)
        {
            x += cell.transform.position.x;
            y += cell.transform.position.y;
        }
        Vector2 valx = Vector2.right * (x / CellCenters.Count);
        Vector2 valy = Vector2.up * (y / CellCenters.Count);
        return valx + valy;
    }
}
