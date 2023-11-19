using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// 스테이터스 종류 enum
/// </summary>
public enum Statetus
{
    Str = 0,
    Mov
}

/// <summary>
/// 스탯 정보 코드
/// </summary>
public class StatusInfo : MonoBehaviour
{
    /// <summary>
    /// 이 항목의 스탯
    /// </summary>
    public Statetus stat;

    public int statetus;

    public string stateExplonation;

    TextMeshProUGUI stateText;

    private void Awake()
    {
        stateText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// 스테이터스 인터페이스 내부 사항을 바꾸는 함수
    /// </summary>
    /// <param name="str"></param>
    /// <param name="state"></param>
    public void SetState(Statetus str, int state)
    {
        if(this.stat == str)
        {
            this.statetus = state;
            stateText.text = $"{str} : {statetus}";
        }
    }
}