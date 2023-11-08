using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuserEdit : MonoBehaviour
{
    public Texture2D cursorTexture; // 사용할 커서 텍스처
    public Vector2 cursorHotspot; // 커서의 핫스팟 위치

    void Start()
    {
        // 커서 텍스처와 핫스팟을 설정하여 마우스 커서를 변경
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }
}
