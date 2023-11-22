using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    public GameObject BaseItemOBJ;
    public List<Vector2> CellCenters = new List<Vector2>();
    public List<InvItemOBJ> AllItmes = new List<InvItemOBJ>();

    TempSlot temp;

    private void Start()
    {
        temp = InventoryInfo.Inst.temp;
    }

    /// <summary>
    /// 아이템을 가방에 생성하는 함수
    /// </summary>
    public void PutItemInTheBag(ItemData tem)
    {
        GameObject obj = Instantiate(BaseItemOBJ);
        InvItemOBJ invItemOBJ = obj.GetComponent<InvItemOBJ>();
        obj.transform.position = calculateCenterPos();
        invItemOBJ.MakeItemInfo(tem,temp.countInt);
        AllItmes.Add(invItemOBJ);
        CellCenters.Clear();
    }

    /// <summary>
    /// 중심 위치 계산하는 함수
    /// </summary>
    /// <returns></returns>
    Vector2 calculateCenterPos()
    {
        float x = 0;
        float y = 0;
        foreach (Vector2 cell in CellCenters)
        {
            x += cell.x;
            y += cell.y;
        }
        Vector2 valx = Vector2.right * (x / CellCenters.Count);
        Vector2 valy = Vector2.up * (y / CellCenters.Count);
        return valx + valy;
    }
}
