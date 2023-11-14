using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTone<GameManager>
{
    Player_Move playerMove;
    public Player_Move PlayerMove => playerMove;

    Player_Equiped playerEquipted;
    public Player_Equiped PlayerEquiped => playerEquipted;

    Player_State playerState;
    public Player_State PlayerState => playerState;

    private void Awake()
    {
        playerMove = FindObjectOfType<Player_Move>();   
        playerEquipted = FindObjectOfType<Player_Equiped>();
        playerState = FindObjectOfType<Player_State>();
    }
}
