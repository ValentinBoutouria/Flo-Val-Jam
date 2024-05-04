using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class upgradeStuff : MonoBehaviour
{
    public Caract�ristique caract�ristique; // R�f�rence au script Caract�ristique
    private raycastEquipement raycastEquipement; // R�f�rence au script raycastEquipement
    public TextMeshProUGUI textMeshPro; // R�f�rence au TextMeshPro
    public TextMeshProUGUI infoTextMeshPro; // R�f�rence � un autre TextMeshPro

    private Equipement equipement; // R�f�rence au script Equipement

    private string lastTag; // Dernier tag touch� par le raycast
    private string lastName; // Dernier nom touch� par le raycast

    private void Start()
    {
        equipement = GetComponent<Equipement>();
        raycastEquipement = GetComponent<raycastEquipement>();

        // Centre le texte dans le TextMeshPro
        textMeshPro.alignment = TextAlignmentOptions.Center;
    }

    void Update()
    {
        // ... (reste du code)

        // Cr�e une liste de t�ches � effectuer apr�s l'�num�ration
        List<Action> tasks = new List<Action>();

        // Parcourt chaque entit� dans AvailableStuff
        foreach (var equipmentType in equipement.AvailableStuff)
        {
            foreach (var item in equipmentType.Value)
            {
                // V�rifie si l'objet concern� est touch� par le raycast
                if (raycastEquipement.currentTag == equipmentType.Key && raycastEquipement.currentName == item.Key)
                {
                    // V�rifie si l'entit� est pr�sente dans nombrePossede_necessaire et si le premier membre du tuple est sup�rieur ou �gal au second
                    if (equipement.nombrePossede_necessaire[equipmentType.Key][item.Key].Item1 >= equipement.nombrePossede_necessaire[equipmentType.Key][item.Key].Item2)
                    {
                        // V�rifie si le montant en gold est sup�rieur au co�t d'am�lioration
                        if (caract�ristique.gold >= equipement.coutUpgrade[equipmentType.Key][item.Key])
                        {
                            // Affiche le message d'am�lioration possible
                            textMeshPro.text = "Am�lioration possible : d�pensez " + equipement.coutUpgrade[equipmentType.Key][item.Key] + " en appuyant sur A";

                            // Ajoute la t�che � la liste
                            tasks.Add(() => CheckForUpgrade(equipmentType.Key, item.Key));
                        }
                        else
                        {
                            // Affiche le message d'am�lioration disponible
                            textMeshPro.text = "Am�lioration disponible pour " + equipement.coutUpgrade[equipmentType.Key][item.Key];
                        }
                    }
                    else
                    {
                        // Affiche le message d'�quipement n�cessaire
                        textMeshPro.text = "Vous devez avoir cet �quipement en " + equipement.nombrePossede_necessaire[equipmentType.Key][item.Key].Item2 + " pour pouvoir l'am�liorer, et cette action vous co�tera " + equipement.coutUpgrade[equipmentType.Key][item.Key] + " gold";
                    }
                }
                else
                {
                    Debug.Log("BBBBBBBBBBBBBBBB");
                    // Efface le texte du TextMeshPro
                    textMeshPro.text = "";
                }
            }
        }

        // Ex�cute les t�ches
        foreach (var task in tasks)
        {
            task();
        }

        string infoText = "";
        foreach (var equipmentType in equipement.nombrePossede_necessaire)
        {
            foreach (var item in equipmentType.Value)
            {
                infoText += equipmentType.Key + "-" + item.Key + " : " + item.Value.Item1 + " / " + item.Value.Item2;
            }
        }

        // Affiche les informations dans le TextMeshPro
        infoTextMeshPro.text = infoText;
    }

    void CheckForUpgrade(string equipmentType, string itemName)
    {
        // V�rifie si le joueur appuie sur la touche A
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Incr�mente de 2 la valeur de l'item dans AvailableStuff et GameStuff
            equipement.AvailableStuff[equipmentType][itemName] += 2;
            equipement.GameStuff[equipmentType][itemName] += 2;

            // V�rifie si les cl�s existent dans EquippedStuff avant d'incr�menter la valeur de l'item
            if (equipement.EquippedStuff.ContainsKey(equipmentType) && equipement.EquippedStuff[equipmentType].ContainsKey(itemName))
            {
                equipement.EquippedStuff[equipmentType][itemName] += 2;
            }

            // Retire le co�t d'am�lioration de la variable gold du joueur
            caract�ristique.gold -= equipement.coutUpgrade[equipmentType][itemName];

            // R�duit � un la premi�re valeur du tuple de l'item dans nombrePossede_necessaire
            var tuple = equipement.nombrePossede_necessaire[equipmentType][itemName];
            equipement.nombrePossede_necessaire[equipmentType][itemName] = (tuple.Item1-2, tuple.Item2);
        }
    }
}
