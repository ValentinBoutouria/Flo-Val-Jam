using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balancierScie : MonoBehaviour
{
    public float amplitude = 90; // Amplitude du mouvement de balancier en degrés
    public float vitesse = 1f; // Vitesse du mouvement de balancier

    private Transform childTransform; // Transform du premier enfant

    // Start is called before the first frame update
    void Start()
    {
        // Obtient le Transform du premier enfant
        if (transform.childCount > 0)
        {
            childTransform = transform.GetChild(0);
        }
        transform.Rotate(-amplitude / 2, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Vérifie si childTransform a été défini
        if (childTransform != null)
        {
            // Calcule l'angle du mouvement de balancier
            float angle = amplitude * Mathf.Sin(Time.time * vitesse);

            // Fait tourner le GameObject autour de l'axe X du premier enfant
            transform.RotateAround(childTransform.position, childTransform.right, angle * Time.deltaTime);
        }
    }
}
