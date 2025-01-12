using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlinePolygonObject : MonoBehaviour
{
    public PolygonCollider2D polygonObject;
    void Awake()
    {
        DrawPolygonCollider(polygonObject);
    }

    public void DrawPolygonCollider(PolygonCollider2D collider)
    {
        LineRenderer _lr = gameObject.AddComponent<LineRenderer>();
        _lr.sortingOrder = 800;
        _lr.startWidth = 2f;
        _lr.endWidth = 2f;
        _lr.useWorldSpace = true;
        _lr.startColor = Color.green;
        _lr.endColor = Color.green;

        _lr.positionCount = collider.points.Length + 1;
        for (int i = 0; i < collider.points.Length; i++)
        {
            _lr.SetPosition(i, new Vector3(collider.points[i].x, collider.points[i].y));
        }
        _lr.SetPosition(collider.points.Length, new Vector3(collider.points[0].x, collider.points[0].y));
    }
}
