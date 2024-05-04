using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private GameObject target;
    public GameObject Projectile;
    public GameObject spawnpoint;

    public float compteurShoot = 1f;
    public float cooldownShoot = 1f;

    public float ProjectileDMG = 10f;

    public float DistanceActivation;
    public float speedBullet=1f;
    public bool IsShooting = false;

    private Rigidbody rb;
    private Vector3 distanceVector;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("cat");
        
    }

    // Update is called once per frame
    void Update()
    {
        ControleisShooting();
        Shooting();
        ControleCompteurShoot();


    }
    float ControleDistance()
    {
        float distance = 0;
        distance = Vector3.Distance(this.transform.position, target.transform.position);
        distanceVector=this.transform.position-target.transform.position;
        return distance;
    }
    void ControleisShooting()
    {
        if(ControleDistance() <= DistanceActivation)
        {
            if (!IsShooting)
            {
                IsShooting=true;
            }
        }
        else
        {
            if (IsShooting)
            {
                IsShooting = false;
            }
        }


    }
    void Shooting()
    {
        if(IsShooting) 
        { 
            this.transform.LookAt(target.transform.position);
            if (compteurShoot>=cooldownShoot)
            {
            GameObject projectiletemp=Instantiate(Projectile,spawnpoint.transform.position,spawnpoint.transform.rotation,this.transform);
            rb = projectiletemp.GetComponent<Rigidbody>();
            rb.AddForce(-distanceVector * speedBullet, ForceMode.Impulse);
            compteurShoot = 0;
                Destroy(projectiletemp, 5f);
            }
        }
    }
    void ControleCompteurShoot()
    {
        if (compteurShoot < cooldownShoot)
        {
            compteurShoot += Time.deltaTime;
        }

    }
}
