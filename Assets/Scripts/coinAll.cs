using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinAll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Calcule l'angle de rotation pour un tour en 6 secondes
        float rotationSpeed = 360f / 6f; // 60 degrés par seconde

        // Fait tourner la pièce sur elle-même selon son axe Y
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
    void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet qui est entré dans le trigger a le tag "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            // Obtient une référence au script Caracteristique de l'objet
            Caractéristique caracteristique = other.gameObject.GetComponent<Caractéristique>();

            // Incrémente la variable gold
            caracteristique.gold += 1;

            // Lance l'animation et détruit la pièce
            StartCoroutine(AnimateAndDestroy());
        }
    }
    IEnumerator AnimateAndDestroy()
    {
        // Double la taille de la pièce
        transform.localScale *= 2f;

        // Réduit la pièce à 0 plus lentement
        float elapsedTime = 0f;
        float duration = 1f; // Durée de l'animation
        Vector3 originalScale = transform.localScale;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Détruit la pièce
        Destroy(gameObject);
    }
}
