using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomPositionLVL7 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Génère une position aléatoire
        float x = Random.Range(-7f, 7f);
        float y = 0.5f;
        float z = Random.Range(1.5f, 15.7f);
        Vector3 randomPosition = new Vector3(x, y, z);

        // Applique la position aléatoire au GameObject
        transform.localPosition = randomPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
