using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tp : MonoBehaviour
{
    public GameObject otherPrefab; // R�f�rence � l'autre prefab

    void OnTriggerEnter(Collider other)
    {
        // Lorsqu'un objet entre dans le collider, changez sa position � celle de l'autre prefab
        other.transform.position = otherPrefab.transform.position;
    }
}
