using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipement : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, int>> GameStuff;
    public Dictionary<string, Dictionary<string, int>> AvailableStuff;
    public Dictionary<string, Dictionary<string, int>> EquippedStuff;
    public GameObject shelf;

    // Start is called before the first frame update
    void Start()
    {
        GameStuff = new Dictionary<string, Dictionary<string, int>>();
        AvailableStuff = new Dictionary<string, Dictionary<string, int>>();
        EquippedStuff = new Dictionary<string, Dictionary<string, int>>();


        // Cr�er un nouveau dictionnaire pour un type d'�quipement
        Dictionary<string, int> armuresDict = new Dictionary<string, int>();
        armuresDict.Add("commune", 10);
        armuresDict.Add("rare", 20);
        armuresDict.Add("legendaire", 30);

        Dictionary<string, int> epeesDict = new Dictionary<string, int>();
        epeesDict.Add("commune", 10);
        epeesDict.Add("rare", 20);
        epeesDict.Add("legendaire", 30);


        // Ajouter le type d'�quipement au dictionnaire principal
        GameStuff.Add("Armures", armuresDict);
        GameStuff.Add("epee", epeesDict);
        AvailableStuff.Add("Armures", new Dictionary<string, int>());
        AvailableStuff.Add("epee", new Dictionary<string, int>());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // M�thode pour d�bloquer un item
    public void UnlockItem(string equipmentType, string itemName)
    {
        // V�rifie si l'item existe dans GameStuff
        if (GameStuff.ContainsKey(equipmentType) && GameStuff[equipmentType].ContainsKey(itemName))
        {
            // Ajoute l'item � AvailableStuff
            AvailableStuff[equipmentType].Add(itemName, GameStuff[equipmentType][itemName]);
            // Parcourt tous les enfants de shelf
            for (int i = 0; i < shelf.transform.childCount; i++)
            {
                GameObject child = shelf.transform.GetChild(i).gameObject;

                // V�rifie si le tag et le nom du fils correspondent � equipmentType et itemName
                Debug.Log(equipmentType + itemName + "     -----------    " + child.tag + child.name);
                if (child.tag == equipmentType && child.name == itemName)
                {
                    // R�active le fils
                    child.SetActive(true);
                }
            }
        }
    }

    // M�thode pour �quiper un item
    public void EquipItem(string equipmentType, string itemName)
    {
        // V�rifie si l'item existe dans GameStuff et AvailableStuff
        if (AvailableStuff.ContainsKey(equipmentType) && AvailableStuff[equipmentType].ContainsKey(itemName))
        {
            // Si l'item est d�j� �quip�, le retire
            if (EquippedStuff.ContainsKey(equipmentType))
            {
                EquippedStuff[equipmentType].Remove(itemName);
            }

            // Ajoute l'item � EquippedStuff
            EquippedStuff[equipmentType] = new Dictionary<string, int>();
            EquippedStuff[equipmentType].Add(itemName, AvailableStuff[equipmentType][itemName]);

        }
    }
}
