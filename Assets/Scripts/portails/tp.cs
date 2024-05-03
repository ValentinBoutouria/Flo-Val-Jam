using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tp : MonoBehaviour
{
    public GameObject otherPrefab; // Référence à l'autre prefab

    void OnTriggerEnter(Collider other)
    {
        // Lorsqu'un objet entre dans le collider, changez sa position à celle de l'autre prefab
        other.transform.position = otherPrefab.transform.position;
    }
}
