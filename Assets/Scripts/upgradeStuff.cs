using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class upgradeStuff : MonoBehaviour
{
    public Caractéristique caractéristique; // Référence au script Caractéristique
    private raycastEquipement raycastEquipement; // Référence au script raycastEquipement
    public TextMeshProUGUI textMeshPro; // Référence au TextMeshPro
    public TextMeshProUGUI infoTextMeshPro; // Référence à un autre TextMeshPro

    private Equipement equipement; // Référence au script Equipement

    private string lastTag; // Dernier tag touché par le raycast
    private string lastName; // Dernier nom touché par le raycast

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

        // Crée une liste de tâches à effectuer après l'énumération
        List<Action> tasks = new List<Action>();

        // Parcourt chaque entité dans AvailableStuff
        foreach (var equipmentType in equipement.AvailableStuff)
        {
            foreach (var item in equipmentType.Value)
            {
                // Vérifie si l'objet concerné est touché par le raycast
                if (raycastEquipement.currentTag == equipmentType.Key && raycastEquipement.currentName == item.Key)
                {
                    // Vérifie si l'entité est présente dans nombrePossede_necessaire et si le premier membre du tuple est supérieur ou égal au second
                    if (equipement.nombrePossede_necessaire[equipmentType.Key][item.Key].Item1 >= equipement.nombrePossede_necessaire[equipmentType.Key][item.Key].Item2)
                    {
                        // Vérifie si le montant en gold est supérieur au coût d'amélioration
                        if (caractéristique.gold >= equipement.coutUpgrade[equipmentType.Key][item.Key])
                        {
                            // Affiche le message d'amélioration possible
                            textMeshPro.text = "Amélioration possible : dépensez " + equipement.coutUpgrade[equipmentType.Key][item.Key] + " en appuyant sur A";

                            // Ajoute la tâche à la liste
                            tasks.Add(() => CheckForUpgrade(equipmentType.Key, item.Key));
                        }
                        else
                        {
                            // Affiche le message d'amélioration disponible
                            textMeshPro.text = "Amélioration disponible pour " + equipement.coutUpgrade[equipmentType.Key][item.Key];
                        }
                    }
                    else
                    {
                        // Affiche le message d'équipement nécessaire
                        textMeshPro.text = "Vous devez avoir cet équipement en " + equipement.nombrePossede_necessaire[equipmentType.Key][item.Key].Item2 + " pour pouvoir l'améliorer, et cette action vous coûtera " + equipement.coutUpgrade[equipmentType.Key][item.Key] + " gold";
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

        // Exécute les tâches
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
        // Vérifie si le joueur appuie sur la touche A
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Incrémente de 2 la valeur de l'item dans AvailableStuff et GameStuff
            equipement.AvailableStuff[equipmentType][itemName] += 2;
            equipement.GameStuff[equipmentType][itemName] += 2;

            // Vérifie si les clés existent dans EquippedStuff avant d'incrémenter la valeur de l'item
            if (equipement.EquippedStuff.ContainsKey(equipmentType) && equipement.EquippedStuff[equipmentType].ContainsKey(itemName))
            {
                equipement.EquippedStuff[equipmentType][itemName] += 2;
            }

            // Retire le coût d'amélioration de la variable gold du joueur
            caractéristique.gold -= equipement.coutUpgrade[equipmentType][itemName];

            // Réduit à un la première valeur du tuple de l'item dans nombrePossede_necessaire
            var tuple = equipement.nombrePossede_necessaire[equipmentType][itemName];
            equipement.nombrePossede_necessaire[equipmentType][itemName] = (tuple.Item1-2, tuple.Item2);
        }
    }
}
