using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    public float Day = 1.0f;
    float Speed = 1.0f;
    // Start is called before the first frame update
    
    void Start()
    {
        //Time.timeScale = 20.0f;
        Speed = 360.0f / Day;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Speed * Time.deltaTime);
    }
}
