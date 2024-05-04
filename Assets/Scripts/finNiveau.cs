using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finNiveau : MonoBehaviour
{
    public float time;
    public Equipement eq;
    public float objectiveTime = 10f;
    public Dictionary<string, Dictionary<string, int>> GameStuff;
    public Transform lobbySpawn;

    // Start is called before the first frame update
    void Start()
    {
        eq = GameObject.Find("GestionEquipement").GetComponent<Equipement>();
        GameStuff = eq.GameStuff;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Fin du niveau");
            other.transform.position = lobbySpawn.position;
            time = GetComponent<timer>().timeElapsed;
            tirageObjet();
        }
    }

    private void tirageObjet()
    {
        // S�lectionne une cl� al�atoire dans GameStuff
        List<string> keys = new List<string>(GameStuff.Keys);
        string randomKey = keys[Random.Range(0, keys.Count)];

        // Obtient le sous-dictionnaire pour la cl� s�lectionn�e
        Dictionary<string, int> subDict = GameStuff[randomKey];

        // Calcule la probabilit� en fonction du temps
        float prob = Mathf.Clamp((objectiveTime - time) / objectiveTime, 0f, 1f);

        // S�lectionne une cl� dans le sous-dictionnaire en fonction de la probabilit�
        string selectedKey;
        float randomValue = Random.Range(0f, 1f);
        if (randomValue < prob / 3f)
        {
            // Probabilit� la plus faible : "legendaire"
            selectedKey = "legendaire";
        }
        else if (randomValue < prob * 2f / 3f)
        {
            // Probabilit� moyenne : "rare"
            selectedKey = "rare";
        }
        else
        {
            // Probabilit� la plus �lev�e : "commune"
            selectedKey = "commune";
        }

        // V�rifie si la cl� s�lectionn�e existe dans le sous-dictionnaire
        if (subDict.ContainsKey(selectedKey))
        {
            // Affiche la cl� et la valeur s�lectionn�es
            Debug.Log("Cl� s�lectionn�e: " + randomKey + ", Sous-cl�: " + selectedKey + ", Valeur: " + subDict[selectedKey]);

            // Ajoute l'item � AvailableStuff
            eq.UnlockItem(randomKey, selectedKey);
        }
        else
        {
            Debug.Log("La cl� " + selectedKey + " n'existe pas dans le sous-dictionnaire pour la cl� " + randomKey);
        }
    }
}
