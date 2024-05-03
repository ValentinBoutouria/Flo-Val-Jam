using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tp : MonoBehaviour
{
    public GameObject otherPrefab; // R�f�rence � l'autre prefab

    void OnTriggerEnter(Collider other)
    {
        // Calculer l'�cart entre le collider other et le collider de l'objet
        Vector3 offset = other.transform.position - this.transform.position;
        offset.y = 0; // Ignorer la hauteur
        // Ajouter cet �cart � la position de l'autre prefab
        other.transform.position = otherPrefab.transform.position + offset*-2;

    }
}
