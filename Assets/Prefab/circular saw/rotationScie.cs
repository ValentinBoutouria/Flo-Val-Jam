using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationScie : MonoBehaviour
{
    public float rotationSpeed = 1000f; // Vitesse de rotation en degr�s par seconde

    // Update is called once per frame
    void Update()
    {
        // Fait tourner l'objet sur son axe X tr�s rapidement
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
