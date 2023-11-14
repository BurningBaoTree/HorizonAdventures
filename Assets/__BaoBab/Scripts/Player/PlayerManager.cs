using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 임시 게임 매니저를 대체하는 플레이어 매니저
/// </summary>
public class PlayerManager : MonoBehaviour
{
    static PlayerManager instance;
    public static PlayerManager Inst => instance;

    Player_Move playerMove;
    public Player_Move PlayerMove => playerMove;

    Player_Equiped playerEquipted;


    private void OnEnable()
    {
        
    }
}
