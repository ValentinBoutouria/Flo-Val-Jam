using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class raycastEquipement : MonoBehaviour
{
    public Camera cam; // Référence à la caméra
    public TextMeshProUGUI textMeshPro; // Référence au TextMeshPro
    public Equipement equipement; // Référence au script Equipement

    void Update()
    {
        // Lance un raycast à partir de la caméra
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            // Vérifie si l'objet touché a le tag "epee" ou d'autres tags spécifiques
            if (hit.collider.CompareTag("epee") /* || hit.collider.CompareTag("autreTag") */)
            {
                // Affiche "equiper" suivi du tag et du nom de l'objet dans le TextMeshPro
                textMeshPro.text = "Equiper " + hit.collider.tag + " " + hit.collider.name;

                // Ajoute l'élément correspondant à EquippedStuff depuis GameStuff
                equipement.EquipItem(hit.collider.tag, hit.collider.name);
            }
        }
    }
}