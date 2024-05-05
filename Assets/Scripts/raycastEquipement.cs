using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class raycastEquipement : MonoBehaviour
{
    public Camera cam; // R�f�rence � la cam�ra
    public TextMeshProUGUI textMeshPro; // R�f�rence au TextMeshPro
    public Equipement equipement; // R�f�rence au script Equipement

    public string currentTag;
    public string currentName;
    public Texture noIcone;
    public GameObject emptyUI;

    private void Start()
    {
        emptyUI = GetComponent<Equipement>().emptyUI;
    }
    void Update()
    {
        // Lance un raycast � partir de la cam�ra
        RaycastHit hit;
        Vector3 playerPosition = cam.transform.parent.parent.position;

        if (Physics.Raycast(playerPosition, this.transform.parent.forward, out hit))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.forward * hit.distance, Color.red);

            // V�rifie si l'objet touch� a le tag "epee" ou d'autres tags sp�cifiques
            if (hit.collider.CompareTag("epee")  || hit.collider.CompareTag("Armures") || hit.collider.CompareTag("Boots"))
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
                    if (equipement.EquippedStuff.ContainsKey(currentTag) && equipement.EquippedStuff[currentTag].ContainsKey(currentName))
                    {
                        equipement.EquippedStuff[currentTag].Remove(currentName);
                        emptyUI.transform.GetChild(0).Find(currentTag).GetComponent<RawImage>().texture = noIcone;
                        emptyUI.transform.GetChild(1).Find(currentTag).GetComponent<TMP_Text>().text = "";
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