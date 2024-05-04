using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class finNiveau : MonoBehaviour
{
    public float time;
    public float Besttime;
    public TextMeshProUGUI BesttimerText;
    public Equipement eq;
    public float objectiveTime = 10f;
    public Dictionary<string, Dictionary<string, int>> GameStuff;
    public Transform lobbySpawn;
    private GameObject ListeMob;
    public bool isEndable;
    private tempsLevel tempsLevel;

    // Start is called before the first frame update
    void Start()
    {
        tempsLevel = GameObject.FindWithTag("levelTime").GetComponent<tempsLevel>();

        isEndable = false;
        eq = GameObject.Find("GestionEquipement").GetComponent<Equipement>();
        GameStuff = eq.GameStuff;
        ListeMob = GameObject.FindGameObjectWithTag("ListeMob");
    }

    // Update is called once per frame
    void Update()
    {
        // Par défaut, on suppose que tous les enfants sont désactivés
        isEndable = true;

        // Parcourt tous les enfants de ListeMob
        foreach (Transform child in ListeMob.transform)
        {
            // Si un enfant est activé, isEndable devient false et on arrête la boucle
            if (child.gameObject.activeSelf)
            {
                isEndable = false;
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isEndable==true)
            {
                other.transform.position = lobbySpawn.position;
                time = GetComponent<timer>().timeElapsed;
               
                if(Besttime > time) 
                {
                    Besttime=time;
                    BesttimerText.text = "Best Time : " + time.ToString("F2") + "s";
                }
                tirageObjet();
            }
        }
    }

    private void tirageObjet()
    {
        // Selectionne une cle aleatoire dans GameStuff
        List<string> keys = new List<string>(GameStuff.Keys);
        string randomKey = keys[Random.Range(0, keys.Count)];

        // Obtient le sous-dictionnaire pour la cle selectionnee
        Dictionary<string, int> subDict = GameStuff[randomKey];

        // Calcule le nombre d'etoiles en fonction du temps
        string etoiles;
        if (time <= tempsLevel.tempsMax3Etoiles)
        {
            etoiles = "3 etoiles";
        }
        else if (time <= tempsLevel.tempsMax2Etoiles)
        {
            etoiles = "2 etoiles";
        }
        else if (time <= tempsLevel.tempsMax1Etoile)
        {
            etoiles = "1 etoile";
        }
        else
        {
            etoiles = "0 etoiles";
        }

        // Definit les probabilites pour chaque etoile
        Dictionary<string, float> probabilites = new Dictionary<string, float>();
        switch (etoiles)
        {
            case "0 etoiles":
                probabilites = new Dictionary<string, float> { { "Legendaire", 0f }, { "Rare", 0.1f }, { "Commune", 0.9f } };
                break;
            case "1 etoile":
                probabilites = new Dictionary<string, float> { { "Legendaire", 0f }, { "Rare", 0.25f }, { "Commune", 0.75f } };
                break;
            case "2 etoiles":
                probabilites = new Dictionary<string, float> { { "Legendaire", 0.05f }, { "Rare", 0.35f }, { "Commune", 0.6f } };
                break;
            case "3 etoiles":
                probabilites = new Dictionary<string, float> { { "Legendaire", 0.1f }, { "Rare", 0.45f }, { "Commune", 0.45f } };
                break;
        }

        // Selectionne une cle dans le sous-dictionnaire en fonction des probabilites
        string selectedKey = SelectKeyBasedOnProbability(probabilites);

        // Verifie si la cle selectionnee existe dans le sous-dictionnaire
        if (subDict.ContainsKey(selectedKey))
        {
            // Ajoute l'item a AvailableStuff
            eq.UnlockItem(randomKey, selectedKey);
        }
        else
        {
            Debug.Log("La cle " + selectedKey + " n'existe pas dans le sous-dictionnaire pour la cle " + randomKey);
        }
    }

    private string SelectKeyBasedOnProbability(Dictionary<string, float> probabilities)
    {
        float total = 0;
        foreach (float value in probabilities.Values)
        {
            total += value;
        }

        float randomPoint = Random.value * total;

        foreach (KeyValuePair<string, float> entry in probabilities)
        {
            if (randomPoint < entry.Value)
            {
                return entry.Key;
            }
            else
            {
                randomPoint -= entry.Value;
            }
        }
        return probabilities.Keys.Last();
    }
}
