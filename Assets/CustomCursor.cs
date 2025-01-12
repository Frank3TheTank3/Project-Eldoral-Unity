using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D cursorStart;
    public Texture2D cursorEnter;
    public Texture2D cursorExit;
    void Start()
    {
        Cursor.SetCursor(cursorStart, Vector2.zero, CursorMode.ForceSoftware);
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursorEnter, Vector2.zero, CursorMode.ForceSoftware);
    }
    void OnMouseExit()
    {
        Cursor.SetCursor(cursorExit, Vector2.zero, CursorMode.ForceSoftware);
    }
}
