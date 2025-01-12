using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTriggerEvent : MonoBehaviour
{
    MainScript mainScript;

    private void Awake()
    {
        mainScript = FindObjectOfType<MainScript>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        mainScript.setCamDistanceNear();
    }
}
