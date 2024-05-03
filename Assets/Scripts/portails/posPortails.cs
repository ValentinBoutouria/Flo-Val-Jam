using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class posPortails : MonoBehaviour
{
    public GameObject bleu; // Préfab 1
    public GameObject jaune; // Préfab 2

    public Vector3 positionPrefab1; // Position pour le préfab 1
    public Vector3 positionPrefab2; // Position pour le préfab 2

    public Quaternion rotationPrefab1; // Rotation pour le préfab 1
    public Quaternion rotationPrefab2; // Rotation pour le préfab 2

    // Start is called before the first frame update
    void Start()
    {
        if (bleu != null) // Vérifie si le préfab 1 est défini
        {
            bleu.transform.position = positionPrefab1;
            bleu.transform.rotation = rotationPrefab1; // Crée le préfab 1 à la position et rotation spécifiées
        }

        if (jaune != null) // Vérifie si le préfab 2 est défini
        {
            jaune.transform.position = positionPrefab2;
            jaune.transform.rotation = rotationPrefab2; // Crée le préfab 2 à la position et rotation spécifiées
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
