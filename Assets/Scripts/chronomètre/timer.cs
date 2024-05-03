using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Pour utiliser TextMeshPro

public class timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Référence à l'objet TextMeshProUGUI pour afficher le timer
    private float startTime; // Temps de départ
    private bool isRunning = false; // Indique si le timer est en cours d'exécution

    void OnEnable()
    {
        // Démarre le timer lorsque le script est activé
        startTime = Time.time;
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            // Calcule le temps écoulé et l'affiche
            float timeElapsed = Time.time - startTime;
            timerText.text = "Temps écoulé : " + timeElapsed.ToString("F2") + " secondes";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Arrête le timer lorsque un objet entre dans le trigger
        isRunning = false;
    }
}
