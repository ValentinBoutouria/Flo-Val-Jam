using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipement : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, int>> GameStuff;
    public Dictionary<string, Dictionary<string, int>> AvailableStuff;
    public Dictionary<string, Dictionary<string, int>> EquippedStuff;
    public Dictionary<string, Dictionary<string, (int, int)>> nombrePossede_necessaire;
    public Dictionary<string, Dictionary<string, int>> coutUpgrade;
    public GameObject shelf;

    // Start is called before the first frame update
    void Start()
    {
        initDict();
        initUpgrade();
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
            // Vérifie si AvailableStuff ne contient pas déjà l'item
            if (!AvailableStuff[equipmentType].ContainsKey(itemName))
            {
                // Ajoute l'item à AvailableStuff
                AvailableStuff[equipmentType].Add(itemName, GameStuff[equipmentType][itemName]);
            }

            // Parcourt tous les enfants de shelf
            for (int i = 0; i < shelf.transform.childCount; i++)
            {
                GameObject child = shelf.transform.GetChild(i).gameObject;

                // Vérifie si le tag et le nom du fils correspondent à equipmentType et itemName
                if (child.tag == equipmentType && child.name == itemName)
                {
                    // Réactive le fils
                    child.SetActive(true);
                }
            }

            // Incrémente le premier élément du tuple nombrePossede_necessaire[equipmentType][itemName] de 1
            var tuple = nombrePossede_necessaire[equipmentType][itemName];
            nombrePossede_necessaire[equipmentType][itemName] = (tuple.Item1 + 1, tuple.Item2);
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


    private void initDict()
    {
        GameStuff = new Dictionary<string, Dictionary<string, int>>();
        AvailableStuff = new Dictionary<string, Dictionary<string, int>>();
        EquippedStuff = new Dictionary<string, Dictionary<string, int>>();


        // Créer un nouveau dictionnaire pour un type d'équipement
        Dictionary<string, int> armuresDict = new Dictionary<string, int>
        {
            { "commune", 10 },
            { "rare", 20 },
            { "legendaire", 30 }
        };

        Dictionary<string, int> epeesDict = new Dictionary<string, int>
        {
            { "commune", 10 },
            { "rare", 20 },
            { "legendaire", 30 }
        };


        // Ajouter le type d'équipement au dictionnaire principal
        GameStuff.Add("Armures", armuresDict);
        GameStuff.Add("epee", epeesDict);
        AvailableStuff.Add("Armures", new Dictionary<string, int>());
        AvailableStuff.Add("epee", new Dictionary<string, int>());
        EquippedStuff.Add("Armures", new Dictionary<string, int>());
        EquippedStuff.Add("epee", new Dictionary<string, int>());

    }
    private void initUpgrade()
    {
        // Initialise nombrePossede et coutUpgrade
        nombrePossede_necessaire = new Dictionary<string, Dictionary<string, (int, int)>>();
        coutUpgrade = new Dictionary<string, Dictionary<string, int>>();
        foreach (var equipmentType in GameStuff)
        {
            nombrePossede_necessaire[equipmentType.Key] = new Dictionary<string, (int, int)>();
            coutUpgrade[equipmentType.Key] = new Dictionary<string, int>();
            foreach (var item in equipmentType.Value)
            {
                nombrePossede_necessaire[equipmentType.Key][item.Key] = (0,3);

                // Initialise le coût d'amélioration en fonction de la rareté de l'item
                if (item.Key == "commune")
                {
                    coutUpgrade[equipmentType.Key][item.Key] = 2;
                }
                else if (item.Key == "rare")
                {
                    coutUpgrade[equipmentType.Key][item.Key] = 5;
                }
                else if (item.Key == "legendaire")
                {
                    coutUpgrade[equipmentType.Key][item.Key] = 10;
                }
            }
        }
    }
}
