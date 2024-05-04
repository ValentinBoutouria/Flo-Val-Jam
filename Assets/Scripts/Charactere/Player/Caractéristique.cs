using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Caractéristique : MonoBehaviour

{
    public int gold = 0;
    public float speed = 5f;
    public float walkingSpeed = 5f;
    public float runningSpeed = 10f;
    public float dashSpeed = 20f;
    public float Slowspeed = 1f;
    public float jumpForce = 2f;

    
    public float Degatscat = 2f;

    public float compteurDeg = 1f;
    public float cooldownDeg = 1f;
    public float DegatsPiege = 2f;

    public float compteurDash = 1f;
    public float cooldownDash = 1f;
    public float DureeDash = 1f;
    public float DureeDashing = 1f;

    public float PVcat = 100f;
    private float PVcatMax = 100f;

    public bool isGrounded;
    public bool isSit;
    public bool isWalking;
    public bool isRunning;
    public bool isDashing;
    public bool isSlow;
    public bool Dashable;

    public Animator animator;

    public Image cdDash;
    public Image Pv;

    public Material CatMat;
    public Material DashMat;

    public TextMeshProUGUI textPV;
    public TextMeshProUGUI textGold;

    public GameObject Cam;

    private Renderer renderer;


    Rigidbody rb;

   // private Rigidbody rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderer = GetComponentInChildren<Renderer>();
        Cursor.visible = false;


    }

    // Update is called once per frame
    void Update()
    {
        DeplacementWalk();
        DeplacementRun();

        ControleWalk();
        ControleSpeed();
        ControleDash();
        ControleJump();
        ControleCompteurDash();
        ControleMat();
        ControleCompteurDegat();
        ControlePV();
        //ControleCompteurDegat();

        UIDash();
        UIPV();
        UIGold();

        DureeDashingIncrem();





    }
    void DeplacementWalk() 
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculer la direction de déplacement
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput)*speed*Time.deltaTime;

        // Vérifier si le personnage est en mouvement
        if (movement != Vector3.zero)
        {
            // Calculer la rotation pour faire face à la direction de déplacement
            //Quaternion targetRotation = Quaternion.LookRotation(movement);

            // Interpoler doucement la rotation actuelle vers la rotation ciblée
            //Armaturechat.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Appliquer le mouvement
        transform.Translate(movement);
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * speed * 10);
        if (Input.GetMouseButton(1))
        {
            Cam.transform.Rotate(Vector3.right * Input.GetAxis("Mouse Y") * Time.deltaTime * speed*5);

        }
        


    }
    void DeplacementRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("IsRunning", true);
            isRunning = true;

            animator.SetBool("IsWalking", false);
            isWalking = false;

            //speed = runningSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("IsRunning", false);
            isRunning = false;
            
            //speed = walkingSpeed;

        }

    }
    void ControleSpeed() 
    {
        if (isWalking && !isRunning && !isDashing && !isSlow) { speed = walkingSpeed; }
        if (!isWalking && isRunning && !isDashing && !isSlow) { speed = runningSpeed; }
        if (isDashing) { speed = dashSpeed; }
        if (isSlow) { speed = Slowspeed; }


    }
    void ControleWalk()
    {
        if (isRunning==false && (Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical")!=0)) //si on ne cours pas et si on se déplace quand même
        {
            animator.SetBool("IsWalking", true);
            isWalking = true;
            //speed = walkingSpeed;
            
        }
        else
        {
            animator.SetBool("IsWalking", false);
            isWalking=false;

            animator.SetBool("IsSit", true);
            isSit = true;
        }

    }

    void ControleJump()
    {
        // Gestion du saut
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
  
        }

    }
    void ControleDash()
    {
        if (Dashable)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isGrounded)
            {
                DureeDashing = 0;
                compteurDash = 0;
                isDashing = true;  
            }         
        }
    }
    void DureeDashingIncrem()
    {
        if(DureeDashing<DureeDash)
        {
            DureeDashing += Time.deltaTime;
        }
        else
        {
            isDashing = false;
        }
    }
    void ControleCompteurDash()
    {
        if (!isDashing && compteurDash<cooldownDash)//on dash pas et le cooldown est pas ok
        {
            Dashable = false; //donc on peut pas dash
            compteurDash += Time.deltaTime;
        }
        else
        {
            Dashable = true;
        }   
    }
    
    void ControleCompteurDegat()
    {
        if (compteurDeg<cooldownDeg)
        { 
            compteurDeg += Time.deltaTime;
        }
       
    }
    
    void ControleMat()
    {
        if (isDashing)
        {
            if(renderer.material!=DashMat)
            {
                renderer.material = DashMat;
            }
        }
        else {
            if (renderer.material != CatMat)
            {
                renderer.material = CatMat;
            }
        }
    }
    void UIDash()
    {
        cdDash.fillAmount = compteurDash/cooldownDash;

    }
    void UIPV()
    {
        Pv.fillAmount = PVcat/PVcatMax;

    }
    void UIGold()
    {
        textGold.text = "Gold : " + gold;
    }
    void ControlePV()
    {
        textPV.text = "" + PVcat;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Mob"))
        {
            CaractéristiqueMob caracmobtemp=other.GetComponent<CaractéristiqueMob>();
                if(isDashing)
                {
                    caracmobtemp.PV -= Degatscat;  
                }
                else
                {
                    PVcat -= caracmobtemp.Degats;
                }
        }
        if(other.CompareTag("Trampoline"))
        {
            
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce*2, ForceMode.Impulse);
        }
        if(other.CompareTag("Slow"))
        {
            isSlow=true;
            animator.SetBool("IsSlow", true);
        }
        if(other.CompareTag("Restore"))
        {
            isSlow = false;
            animator.SetBool("IsSlow", false);
        }
        if(other.CompareTag("Piege"))
        {
            
            if(compteurDeg >= cooldownDeg)
            {
                PVcat -= DegatsPiege;
                compteurDeg = 0;
            }
        }
        if(other.CompareTag("Projectile"))
        {
            if(compteurDeg>=cooldownDeg)
            {
                Debug.Log("Ouch");
                Shoot scriptShoottemp=other.GetComponentInParent<Shoot>();
                PVcat -= scriptShoottemp.ProjectileDMG;
                compteurDeg = 0;

            }
            Destroy(other.gameObject);
        }

    }

}
