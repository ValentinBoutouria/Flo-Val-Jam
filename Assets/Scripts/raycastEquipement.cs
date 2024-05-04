using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class raycastEquipement : MonoBehaviour
{
    public Camera cam; // Référence à la caméra
    public TextMeshProUGUI textMeshPro; // Référence au TextMeshPro
    public Equipement equipement; // Référence au script Equipement

    private string currentTag;
    private string currentName;

    void Update()
    {
        // Lance un raycast à partir de la caméra
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.forward * hit.distance, Color.red);

            // Vérifie si l'objet touché a le tag "epee" ou d'autres tags spécifiques
            if (hit.collider.CompareTag("epee") /* || hit.collider.CompareTag("autreTag") */)
            {
                // Stocke le tag et le nom de l'objet actuellement visé
                currentTag = hit.collider.tag;
                currentName = hit.collider.name;

                // Vérifie si l'épée est déjà équipée
                if (equipement.EquippedStuff.ContainsKey(currentTag) && equipement.EquippedStuff[currentTag].ContainsKey(currentName))
                {
                    // Si l'épée est déjà équipée, affiche un message pour la déséquiper
                    textMeshPro.text = "Appuyez sur E pour déséquiper " + currentTag + " " + currentName;
                }
                else
                {
                    // Si l'épée n'est pas équipée, affiche un message pour l'équiper
                    textMeshPro.text = "Appuyez sur E pour équiper " + currentTag + " " + currentName;
                }
            }
            else
            {
                // Si l'objet touché n'a pas le tag "epee" ou d'autres tags spécifiques, efface le texte et réinitialise le tag et le nom actuels
                textMeshPro.text = "";
                currentTag = null;
                currentName = null;
            }
        }
        else
        {
            // Si le raycast ne touche pas d'objet, efface le texte et réinitialise le tag et le nom actuels
            textMeshPro.text = "";
            currentTag = null;
            currentName = null;
        }

        // Si le joueur appuie sur E, équipe ou déséquipe l'épée
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(currentTag!=null)
            {
                if(currentName!=null)
                {
                    Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAA");
                    if (equipement.EquippedStuff.ContainsKey(currentTag) && equipement.EquippedStuff[currentTag].ContainsKey(currentName))
                    {
                        equipement.EquippedStuff[currentTag].Remove(currentName);
                    }
                    else
                    {
                        equipement.EquipItem(currentTag, currentName);
                    }
                }

            }

        }
    }
 
}