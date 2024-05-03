using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Pour utiliser TextMeshPro

public class timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // R�f�rence � l'objet TextMeshProUGUI pour afficher le timer
    private float startTime; // Temps de d�part
    private bool isRunning = false; // Indique si le timer est en cours d'ex�cution

    void OnEnable()
    {
        // D�marre le timer lorsque le script est activ�
        startTime = Time.time;
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            // Calcule le temps �coul� et l'affiche
            float timeElapsed = Time.time - startTime;
            timerText.text = "Temps �coul� : " + timeElapsed.ToString("F2") + " secondes";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Arr�te le timer lorsque un objet entre dans le trigger
        isRunning = false;
    }
}
