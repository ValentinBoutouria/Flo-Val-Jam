using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class posPortails : MonoBehaviour
{
    public GameObject bleu; // Pr�fab 1
    public GameObject jaune; // Pr�fab 2

    public Vector3 positionPrefab1; // Position pour le pr�fab 1
    public Vector3 positionPrefab2; // Position pour le pr�fab 2

    public Quaternion rotationPrefab1; // Rotation pour le pr�fab 1
    public Quaternion rotationPrefab2; // Rotation pour le pr�fab 2

    // Start is called before the first frame update
    void Start()
    {
        if (bleu != null) // V�rifie si le pr�fab 1 est d�fini
        {
            bleu.transform.position = positionPrefab1;
            bleu.transform.rotation = rotationPrefab1; // Cr�e le pr�fab 1 � la position et rotation sp�cifi�es
        }

        if (jaune != null) // V�rifie si le pr�fab 2 est d�fini
        {
            jaune.transform.position = positionPrefab2;
            jaune.transform.rotation = rotationPrefab2; // Cr�e le pr�fab 2 � la position et rotation sp�cifi�es
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
