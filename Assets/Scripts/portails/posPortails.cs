using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class posPortails : MonoBehaviour
{
    public GameObject bleu; // Pr�fab 1
    public GameObject jaune; // Pr�fab 2

    public Vector3 positionPrefab1; // Position pour le pr�fab 1
    public Vector3 positionPrefab2; // Position pour le pr�fab 2

    public Vector3 rotationEulerPrefab1; // Rotation pour le pr�fab 1
    public Vector3 rotationEulerPrefab2; // Rotation pour le pr�fab 2

    // Start is called before the first frame update
    void Start()
    {
        if (bleu != null) // V�rifie si le pr�fab 1 est d�fini
        {
            bleu.transform.localPosition = positionPrefab1;
            bleu.transform.rotation = Quaternion.Euler(rotationEulerPrefab1); // Cr�e le pr�fab 1 � la position et rotation sp�cifi�es
        }

        if (jaune != null) // V�rifie si le pr�fab 2 est d�fini
        {
            jaune.transform.localPosition = positionPrefab2;
            jaune.transform.rotation = Quaternion.Euler(rotationEulerPrefab2); // Cr�e le pr�fab 2 � la position et rotation sp�cifi�es
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
