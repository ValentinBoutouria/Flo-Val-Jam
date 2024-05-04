using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationObjCube : MonoBehaviour
{
    private Vector3 originalScale;
    private float scaleFactor = 1.5f;

    void Start()
    {
        originalScale = transform.localScale;
        StartCoroutine(AnimateObject());
    }

    IEnumerator AnimateObject()
    {
        Vector3 randomDirection = Random.insideUnitSphere;
        float randomTime = Random.Range(3f, 6f);

        while (true)
        {
            // Fait tourner l'objet dans la direction aléatoire pendant le temps aléatoire
            float elapsedTime = 0f;
            while (elapsedTime < randomTime)
            {
                transform.Rotate(randomDirection * Time.deltaTime * 30f);
                elapsedTime += Time.deltaTime;

                // Modifie l'échelle de l'objet
                float scale = originalScale.x * (1f + Mathf.PingPong(elapsedTime / 4f, scaleFactor - 1f));
                transform.localScale = new Vector3(scale, scale, scale);

                yield return null;
            }

            // Réinitialise l'échelle de l'objet
            transform.localScale = originalScale;

            // Génère une nouvelle direction et un nouveau temps aléatoires pour la prochaine rotation
            Vector3 newDirection = Random.insideUnitSphere;
            float newTime = Random.Range(3f, 6f);

            // Lisse la transition vers la nouvelle direction et le nouveau temps
            float transitionTime = 1f;
            elapsedTime = 0f;
            while (elapsedTime < transitionTime)
            {
                randomDirection = Vector3.Lerp(randomDirection, newDirection, elapsedTime / transitionTime);
                randomTime = Mathf.Lerp(randomTime, newTime, elapsedTime / transitionTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Affecte la nouvelle direction et le nouveau temps
            randomDirection = newDirection;
            randomTime = newTime;
        }
    }
}
