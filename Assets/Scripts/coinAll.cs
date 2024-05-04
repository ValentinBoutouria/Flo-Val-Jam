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
        float rotationSpeed = 360f / 6f; // 60 degr�s par seconde

        // Fait tourner la pi�ce sur elle-m�me selon son axe Y
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
    void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet qui est entr� dans le trigger a le tag "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            // Obtient une r�f�rence au script Caracteristique de l'objet
            Caract�ristique caracteristique = other.gameObject.GetComponent<Caract�ristique>();

            // Incr�mente la variable gold
            caracteristique.gold += 1;

            // Lance l'animation et d�truit la pi�ce
            StartCoroutine(AnimateAndDestroy());
        }
    }
    IEnumerator AnimateAndDestroy()
    {
        // Dur�e de l'animation
        float growDuration = 0.6f; // Dur�e de l'agrandissement
        float shrinkDuration = 0.4f; // Dur�e de la r�duction

        // Agrandit la pi�ce progressivement
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

        // R�duit la pi�ce � 0 progressivement
        elapsedTime = 0f;
        originalScale = transform.localScale;
        while (elapsedTime < shrinkDuration)
        {
            float t = elapsedTime / shrinkDuration;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // D�truit la pi�ce
        Destroy(gameObject);
    }
}
