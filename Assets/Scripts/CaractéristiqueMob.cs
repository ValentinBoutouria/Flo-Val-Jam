using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaractéristiqueMob : MonoBehaviour
{
    public float PV=5;
    public float Degats=20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ControleMort();
    }
    void ControleMort()
    {
        if (PV<=0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
