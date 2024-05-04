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
        // Durée de l'animation
        float growDuration = 0.6f; // Durée de l'agrandissement
        float shrinkDuration = 0.4f; // Durée de la réduction

        // Agrandit la pièce progressivement
        float elapsedTime = 0f;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 2f;
        while (elapsedTime < growDuration)
        {
            float t = elapsedTime / growDuration;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Réduit la pièce à 0 progressivement
        elapsedTime = 0f;
        originalScale = transform.localScale;
        while (elapsedTime < shrinkDuration)
        {
            float t = elapsedTime / shrinkDuration;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Détruit la pièce
        Destroy(gameObject);
    }
}
