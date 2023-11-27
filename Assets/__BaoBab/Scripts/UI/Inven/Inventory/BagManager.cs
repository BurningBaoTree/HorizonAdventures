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
