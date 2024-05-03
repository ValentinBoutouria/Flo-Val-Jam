using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caractéristique : MonoBehaviour

{
    public float speed = 5f;
    public float jumpForce = 2f;
    public bool isGrounded;

    

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        DeplacementPersoSimple();
        Jump();


    }
    void DeplacementPersoSimple() {
        // Déplacement horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(verticalInput, 0f, 0f) * speed * Time.deltaTime;
        transform.Translate(movement);

        
    }

    void Jump()
    {
        // Gestion du saut
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  
        }

    }
    
}
