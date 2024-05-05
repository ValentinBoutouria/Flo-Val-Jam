using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Equipement : MonoBehaviour
{
    public Dictionary<string, Dictionary<string, int>> GameStuff;
    public Dictionary<string, Dictionary<string, int>> AvailableStuff;
    public Dictionary<string, Dictionary<string, int>> EquippedStuff;
    public Dictionary<string, Dictionary<string, (int, int)>> nombrePossede_necessaire;
    public Dictionary<string, Dictionary<string, int>> coutUpgrade;
    public GameObject shelf;
    public List<Texture> icones = new List<Texture>();
    public GameObject emptyUI;

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
        if (AvailableStuff[equipmentType].ContainsKey(itemName))
        {
            // Si l'item est déjà équipé, le retire
            if (EquippedStuff.ContainsKey(equipmentType))
            {
                EquippedStuff[equipmentType].Remove(itemName);
            }

            // Ajoute l'item à EquippedStuff
            EquippedStuff[equipmentType] = new Dictionary<string, int>();
            EquippedStuff[equipmentType].Add(itemName, AvailableStuff[equipmentType][itemName]);
            int index = selectIndex(equipmentType, itemName);
            emptyUI.transform.GetChild(0).Find(equipmentType).GetComponent<RawImage>().texture = icones[index];
            emptyUI.transform.GetChild(1).Find(equipmentType).GetComponent<TMP_Text>().text = equipmentType + "\n" + itemName + " :\n" + GameStuff[equipmentType][itemName];
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
            { "commune", 10},
            { "rare", 30 },
            { "legendaire", 60 }
        };

        Dictionary<string, int> epeesDict = new Dictionary<string, int>
        {
            { "commune", 10 },
            { "rare", 30 },
            { "legendaire", 60 }
        };
        Dictionary<string, int> bootsDict = new Dictionary<string, int>
        {
            { "commune", 3 },
            { "rare", 6 },
            { "legendaire", 9 }
        };


        // Ajouter le type d'équipement au dictionnaire principal
        GameStuff.Add("Armures", armuresDict);
        GameStuff.Add("epee", epeesDict);
        GameStuff.Add("Boots", bootsDict);

        AvailableStuff.Add("Armures", new Dictionary<string, int>());
        AvailableStuff.Add("epee", new Dictionary<string, int>());
        AvailableStuff.Add("Boots", new Dictionary<string, int>());
        EquippedStuff.Add("Armures", new Dictionary<string, int>());
        EquippedStuff.Add("epee", new Dictionary<string, int>());
        EquippedStuff.Add("Boots", new Dictionary<string, int>());

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

    private int selectIndex(string equipmentType, string itemName)
    {
        int index = 0;
        switch (equipmentType)
        {
            case "Armures":
                index = 3;
                break;
            case "epee":
                index = 6;
                break;
            case "Boots":
                index = 0;
                break;
        }
        switch (itemName)
        {
            case "commune":
                index += 0;
                break;
            case "rare":
                index += 1;
                break;
            case "legendaire":
                index += 2;
                break;
        }
        return index;
    }
}
