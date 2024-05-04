using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationScie : MonoBehaviour
{
    public float rotationSpeed = 1000f; // Vitesse de rotation en degrés par seconde

    // Update is called once per frame
    void Update()
    {
        // Fait tourner l'objet sur son axe X très rapidement
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
