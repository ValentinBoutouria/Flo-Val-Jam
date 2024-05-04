using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Pour utiliser TextMeshPro

public class timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Référence à l'objet TextMeshProUGUI pour afficher le timer
    private float startTime; // Temps de départ
    private bool isRunning = false; // Indique si le timer est en cours d'exécution
    public float timeElapsed; // Temps écoulé
    private finNiveau _finNiveau;

    void OnEnable()
    {
        _finNiveau = GetComponent<finNiveau>();
        // Démarre le timer lorsque le script est activé
        startTime = Time.time;
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            // Calcule le temps écoulé et l'affiche
            timeElapsed = Time.time - startTime;
            timerText.text = "Temps écoulé : " + timeElapsed.ToString("F2") + " secondes";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(_finNiveau.isEndable)
        {

        // Arrête le timer lorsque un objet entre dans le trigger
        isRunning = false;
        this.transform.parent.gameObject.SetActive(false);
        }
    }
}
