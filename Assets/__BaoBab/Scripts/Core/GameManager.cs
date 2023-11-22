using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player_Move playerMove;
    public Player_Move PlayerMove
    { 
        get
        {
            if (!playerMove)
            {
                playerMove = FindObjectOfType<Player_Move>();
            }

            return playerMove;
        }
    }

    Player_Equiped playerEquipted;
    public Player_Equiped PlayerEquiped
    {
        get
        {
            if (!playerEquipted)
            {
                playerEquipted = FindObjectOfType<Player_Equiped>();
            }

            return playerEquipted;
        }
    }

    Player_State playerState;
    public Player_State PlayerState
    {
        get
        {
            if (!playerState)
            {
                playerState = FindObjectOfType<Player_State>();
            }

            return playerState;
        }
    }

    ItemDataManager itemDataManager;
    public ItemDataManager ItemData => itemDataManager;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        itemDataManager = GetComponent<ItemDataManager>();
    }

    protected override void OnInitialize()
    {
    }
}
