using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    public Texture2D crosshair;
    private CursorMode cursorMode = CursorMode.Auto;

        void Start()
    {
        Cursor.visible = true;
        Cursor.SetCursor(crosshair, Vector2.zero, cursorMode);
    }

}
