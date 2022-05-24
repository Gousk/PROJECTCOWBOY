using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    public ParticleSystem ShootingSystem;
    public GameObject impactEffectWall;
    public GameObject impactEffectWood;
    public GameObject impactEffectBlood;
    public AudioSource shootingSound;
    public Transform camPos;
    public float range = 100f;
    public float impactForce = 150f;
    public int fireRate = 10;
    private float nextTimeToFire = 0f; 
    private Animator Animator;

    public float shootingShakeSpeed = 14f;
    public float shakeAmount = 0.05f;
    float defaultPosX = 0.197f;
    float defaultPosZ = 0.638f;
    float timer = 0;

    private void Awake()
    {
        //shootingSound = GetComponent<AudioSource>();
        Animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Animator.SetBool("IsShooting", true);
            ShootingSystem.Play();
            
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Animator.SetBool("IsShooting", false);
            ShootingSystem.Stop();
        }  
    }

    private void FixedUpdate() 
    {
        if (Animator.GetBool("IsShooting") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
            shootingSound.Play();
        } 

        if (Animator.GetBool("IsShooting"))
        {
            //Player is shooting
            timer += Time.deltaTime * shootingShakeSpeed;
            //transform.localPosition = new Vector3(defaultPosX + Mathf.Sin(timer)* shakeAmount, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, defaultPosZ - Mathf.Sin(timer)* shakeAmount);
        }
        else
        {
            //Idle
            timer = 0;
            //transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, defaultPosX, Time.deltaTime * shootingShakeSpeed), transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Lerp(transform.localPosition.z, defaultPosZ, Time.deltaTime * shootingShakeSpeed));
        }   
    }

    private void Fire()
    {
        Debug.Log("fire");
        RaycastHit hit;
        if (Physics.Raycast(camPos.position, camPos.forward, out hit, range))
        {
            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            Quaternion impactRotation = Quaternion.LookRotation(hit.normal);

            if (hit.transform.tag == "Wood")
            {
                GameObject impact = Instantiate(impactEffectWood, hit.point, impactRotation);
                Destroy(impact, 5f);
            }
            if (hit.transform.tag == "Brick")
            {  
                GameObject impact = Instantiate(impactEffectWall, hit.point, impactRotation);
                Destroy(impact, 5f);
            }
            if (hit.transform.tag == "Enemy")
            {  
                GameObject impact = Instantiate(impactEffectBlood, hit.point, impactRotation);
                Destroy(impact, 0.5f);
            }
        }
    }
}