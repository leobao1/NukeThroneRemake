using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    public Texture2D crosshair;
    private CursorMode cursorMode = CursorMode.Auto;

    void Start() {
        Cursor.visible = true;
        Vector2 cursorHotspot = new Vector2(crosshair.width / 2, crosshair.height / 2);
        Cursor.SetCursor(crosshair, cursorHotspot, cursorMode);
    }

}
