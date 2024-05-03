using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caractéristique : MonoBehaviour

{
    public float walkingSpeed = 5f;
    public float speed = 5f;
    public float runningSpeed = 10f;
    public float jumpForce = 2f;
    public float life = 100f;
    public bool isGrounded;
    public bool isWalking;
    public bool isRunning;
    public bool isDashing;



    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        DeplacementWalk();
        DeplacementRun();
        Jump();



    }
    void DeplacementWalk() {
        // Déplacement horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
        transform.Translate(movement);

        
    }
    void DeplacementRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning= true;
            isWalking = false;
            speed = runningSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning= false;
            isWalking= true;
            speed = walkingSpeed;

        }

    }
    void ControleWalk()
    {

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
