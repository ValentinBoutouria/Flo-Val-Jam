using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class raycastEquipement : MonoBehaviour
{
    public Camera cam; // R�f�rence � la cam�ra
    public TextMeshProUGUI textMeshPro; // R�f�rence au TextMeshPro
    public Equipement equipement; // R�f�rence au script Equipement

    private string currentTag;
    private string currentName;

    void Update()
    {
        // Lance un raycast � partir de la cam�ra
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.forward * hit.distance, Color.red);

            // V�rifie si l'objet touch� a le tag "epee" ou d'autres tags sp�cifiques
            if (hit.collider.CompareTag("epee") /* || hit.collider.CompareTag("autreTag") */)
            {
                // Stocke le tag et le nom de l'objet actuellement vis�
                currentTag = hit.collider.tag;
                currentName = hit.collider.name;

                // V�rifie si l'�p�e est d�j� �quip�e
                if (equipement.EquippedStuff.ContainsKey(currentTag) && equipement.EquippedStuff[currentTag].ContainsKey(currentName))
                {
                    // Si l'�p�e est d�j� �quip�e, affiche un message pour la d�s�quiper
                    textMeshPro.text = "Appuyez sur E pour d�s�quiper " + currentTag + " " + currentName;
                }
                else
                {
                    // Si l'�p�e n'est pas �quip�e, affiche un message pour l'�quiper
                    textMeshPro.text = "Appuyez sur E pour �quiper " + currentTag + " " + currentName;
                }
            }
            else
            {
                // Si l'objet touch� n'a pas le tag "epee" ou d'autres tags sp�cifiques, efface le texte et r�initialise le tag et le nom actuels
                textMeshPro.text = "";
                currentTag = null;
                currentName = null;
            }
        }
        else
        {
            // Si le raycast ne touche pas d'objet, efface le texte et r�initialise le tag et le nom actuels
            textMeshPro.text = "";
            currentTag = null;
            currentName = null;
        }

        // Si le joueur appuie sur E, �quipe ou d�s�quipe l'�p�e
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