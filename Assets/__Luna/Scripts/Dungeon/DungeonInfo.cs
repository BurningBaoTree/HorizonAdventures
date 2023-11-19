using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonInfo : MonoBehaviour
{
    private int width;
    public int Width => width;

    private int height;
    public int Height => height;

    private void Awake()
    {
        Transform tileMap = transform.GetChild(0);
        Tilemap background = tileMap.GetComponent<Tilemap>();

        width = background.size.x;
        height = background.size.y;
    }
}
