using System.Collections.Generic;
using UnityEngine;

public class LaserDestroyer : MonoBehaviour
{
    public float timeTilDestroy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Destroy(gameObject, timeTilDestroy); 
    }
}
