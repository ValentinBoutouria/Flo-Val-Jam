using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using Unity.VisualScripting;
using UnityEditor.TestTools.CodeCoverage;

public class Caractéristique : MonoBehaviour

{
    public int gold = 0;
    public float speed = 5f;
    public float walkingSpeed = 50f;
    public float runningSpeed = 100f;
    public float dashSpeed = 200f;
    public float Slowspeed = 10f;
    public float jumpForce = 2f;

    
    public float Degatscat = 2f;
    public float dgtEquipement = 0f;

    public float compteurDeg = 1f;
    public float cooldownDeg = 1f;
    public float DegatsPiege = 2f;

    public float compteurDash = 1f;
    public float cooldownDash = 1f;
    public float DureeDash = 1f;
    public float DureeDashing = 1f;

    public float PVcat = 1000f;
    private float PVcatMax = 1000f;

    public float armure = 0f;

    public bool isGrounded;
    public bool isSit;
    public bool isWalking;
    public bool isRunning;
    public bool isDashing;
    public bool isSlow;
    public bool isDead;
    public bool Dashable;

    public Animator animator;

    public Image cdDash;
    public Image Pv;

    public Material CatMat;
    public Material DashMat;

    public TextMeshProUGUI textPV;
    public TextMeshProUGUI textStuff;
    public TextMeshProUGUI textGold;

    public GameObject Cam;

    public GameObject LvlListe;

    private Renderer renderer;


    Rigidbody rb;

    // private Rigidbody rb;

    Equipement equipement;
    Vector2 rotation = Vector2.zero;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    public Transform playerCameraParent;

    private int bonusSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        renderer = GetComponentInChildren<Renderer>();
        Cursor.visible = false;
        equipement = GameObject.Find("GestionEquipement").GetComponent<Equipement>();

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in equipement.EquippedStuff["Boots"])
        {
            bonusSpeed = 0;
            bonusSpeed += equipement.GameStuff["Boots"][item.Key];
        }
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
        ControleChute();
        ControleMort();
        //ControleCompteurDegat();

        UIDash();
        UIPV();
        UIGold();
        UIStuff();

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
        //transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * speed * 10);
        //Cam.transform.Rotate(Vector3.right * Input.GetAxis("Mouse Y") * Time.deltaTime * speed*2);
        rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
        rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        transform.eulerAngles = new Vector2(0, rotation.y);
        if (Input.GetMouseButton(1))
        {

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
        if (isWalking && !isRunning && !isDashing && !isSlow) { speed = walkingSpeed + bonusSpeed; }
        if (!isWalking && isRunning && !isDashing && !isSlow) { speed = runningSpeed + bonusSpeed; }
        if (isDashing) { speed = dashSpeed + bonusSpeed; }
        if (isSlow) { speed = Slowspeed + bonusSpeed/2; }


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
            if (Input.GetMouseButtonDown(1))
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

    void UIStuff()
    {
        armure = 0;
        dgtEquipement = 0;
        foreach (var item in equipement.EquippedStuff["Armures"])
        {
            armure += equipement.GameStuff["Armures"][item.Key];
        }
        foreach (var item in equipement.EquippedStuff["epee"])
        {
            dgtEquipement += equipement.GameStuff["epee"][item.Key];
        }
        textStuff.text = "Armure : " + armure + "\n Bonus dégats : " + dgtEquipement;
    }
    void ControlePV()
    {
        textPV.text = "" + PVcat;
    }
    void ControleChute()
    {
        if(this.transform.position.y < -25) 
        {
            PVcat = 0;
        }
    }
    void ControleMort()
    {
        if(PVcat<=0)
        {
            if(isDead==false) 
            {
                isDead=true;
            }
        }
        else
        {
            if (isDead == true)
            {
                isDead = false;
            }

        }
        if(isDead == true) 
        {
            foreach (Transform child in LvlListe.transform)
            {
                child.gameObject.SetActive(false);
            }
           // LvlListe.gameObject.SetActive(false);
            this.transform.position= Vector3.zero;
            PVcat = PVcatMax;
            isDead = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Mob"))
        {
            CaractéristiqueMob caracmobtemp=other.GetComponent<CaractéristiqueMob>();
                if(isDashing)
                {
                    caracmobtemp.PV -= Degatscat+dgtEquipement;  
                }
                else
                {
                    PVcat -= caracmobtemp.Degats;
                }
        }
        if(other.CompareTag("Trampoline"))
        {
            
            GetComponent<Rigidbody>().AddForce(Vector3.up * other.GetComponent<trampolineCarac>().force, ForceMode.Impulse);
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
                PVcat -= DegatsPiege-armure;
                compteurDeg = 0;
            }
        }
        if(other.CompareTag("Projectile"))
        {
            if(compteurDeg>=cooldownDeg)
            {
                //Debug.Log("Ouch");
                Shoot scriptShoottemp=other.GetComponentInParent<Shoot>();
                PVcat -= scriptShoottemp.ProjectileDMG;
                compteurDeg = 0;

            }
            Destroy(other.gameObject);
        }
        if(other.CompareTag("Teleporteur"))
            {
            LvlListe.gameObject.SetActive(true);
            TPVal Tpniveautemp= other.GetComponent<TPVal>();
            Tpniveautemp.lvl.gameObject.SetActive(true);
            this.transform.position=Tpniveautemp.SpawnPoint.transform.position;
            this.transform.rotation=Tpniveautemp.SpawnPoint.transform.rotation;
        }

    }

}
