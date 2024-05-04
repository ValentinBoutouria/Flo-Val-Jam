using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipement : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, int>> GameStuff;
    public Dictionary<string, Dictionary<string, int>> AvailableStuff;
    public Dictionary<string, Dictionary<string, int>> EquippedStuff;


    // Start is called before the first frame update
    void Start()
    {
        GameStuff = new Dictionary<string, Dictionary<string, int>>();
        AvailableStuff = new Dictionary<string, Dictionary<string, int>>();
        EquippedStuff = new Dictionary<string, Dictionary<string, int>>();


        // Créer un nouveau dictionnaire pour un type d'équipement
        Dictionary<string, int> armuresDict = new Dictionary<string, int>();
        armuresDict.Add("commune", 10);
        Dictionary<string, int> epeesDict = new Dictionary<string, int>();
        epeesDict.Add("commune", 10);

        AvailableStuff.Add("epee", epeesDict);

        // Ajouter le type d'équipement au dictionnaire principal
        GameStuff.Add("Armures", armuresDict);
        GameStuff.Add("epee", epeesDict);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Méthode pour débloquer un item
    public void UnlockItem(string equipmentType, string itemName)
    {
        // Vérifie si l'item existe dans GameStuff
        if (GameStuff.ContainsKey(equipmentType) && GameStuff[equipmentType].ContainsKey(itemName))
        {
            // Ajoute l'item à AvailableStuff
            AvailableStuff[equipmentType].Add(itemName, GameStuff[equipmentType][itemName]);
        }
    }

    // Méthode pour équiper un item
    public void EquipItem(string equipmentType, string itemName)
    {
        // Vérifie si l'item existe dans GameStuff et AvailableStuff
        if (AvailableStuff.ContainsKey(equipmentType) && AvailableStuff[equipmentType].ContainsKey(itemName))
        {
            // Si l'item est déjà équipé, le retire
            if (EquippedStuff.ContainsKey(equipmentType))
            {
                EquippedStuff[equipmentType].Remove(itemName);
            }

            // Ajoute l'item à EquippedStuff
            EquippedStuff[equipmentType] = new Dictionary<string, int>();
            EquippedStuff[equipmentType].Add(itemName, AvailableStuff[equipmentType][itemName]);
        }
    }
}
