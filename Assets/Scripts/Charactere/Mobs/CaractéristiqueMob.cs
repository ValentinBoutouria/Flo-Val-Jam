using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaractéristiqueMob : MonoBehaviour
{
    public float PV=5;
    public float PVMax = 5;
    public float Degats=20;
    public Image Pv;
    public TextMeshProUGUI textPV;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ControleMort();
        UiPV();
    }
    void ControleMort()
    {
        if (PV<=0)
        {
            this.gameObject.SetActive(false);
        }
    }
    void UiPV()
    {
        textPV.text = "" + PV;
        Pv.fillAmount = PV / PVMax;
    }
}
