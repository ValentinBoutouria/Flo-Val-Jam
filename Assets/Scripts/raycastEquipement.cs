using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class raycastEquipement : MonoBehaviour
{
    public Camera cam; // R�f�rence � la cam�ra
    public TextMeshProUGUI textMeshPro; // R�f�rence au TextMeshPro
    public Equipement equipement; // R�f�rence au script Equipement

    void Update()
    {
        // Lance un raycast � partir de la cam�ra
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            // V�rifie si l'objet touch� a le tag "epee" ou d'autres tags sp�cifiques
            if (hit.collider.CompareTag("epee") /* || hit.collider.CompareTag("autreTag") */)
            {
                // Affiche "equiper" suivi du tag et du nom de l'objet dans le TextMeshPro
                textMeshPro.text = "Equiper " + hit.collider.tag + " " + hit.collider.name;

                // Ajoute l'�l�ment correspondant � EquippedStuff depuis GameStuff
                equipement.EquipItem(hit.collider.tag, hit.collider.name);
            }
        }
    }
}