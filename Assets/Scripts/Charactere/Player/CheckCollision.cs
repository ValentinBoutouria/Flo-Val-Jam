using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    public Caract�ristique caract;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        // Faites quelque chose en r�ponse � la collision
        caract.isGrounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        
        caract.isGrounded = false;
    }
}
