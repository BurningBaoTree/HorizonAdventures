using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player_Move playerMove;
    public Player_Move PlayerMove => playerMove;

    Player_Equiped playerEquipted;
    public Player_Equiped PlayerEquiped => playerEquipted;

    Player_State playerState;
    public Player_State PlayerState => playerState;

    protected override void OnInitialize()
    {
        playerMove = FindObjectOfType<Player_Move>();
        playerEquipted = playerMove.GetComponent<Player_Equiped>();
        playerState = playerMove.GetComponent<Player_State>();
    }
}
