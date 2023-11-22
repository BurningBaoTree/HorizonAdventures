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

    ItemDataManager itemDataManager;
    public ItemDataManager ItemData => itemDataManager;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        itemDataManager = GetComponent<ItemDataManager>();
    }

    protected override void OnInitialize()
    {
        playerMove = FindObjectOfType<Player_Move>();
        playerEquipted = playerMove.GetComponent<Player_Equiped>();
        playerState = playerMove.GetComponent<Player_State>();
    }
}
