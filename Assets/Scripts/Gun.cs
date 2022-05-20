using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    public ParticleSystem ShootingSystem;
    public Transform camPos;
    public float range = 20f;
    public float impactForce = 150f;
    public int fireRate = 10;
    private float nextTimeToFire = 0f; 
    private Animator Animator;

    private void Awake()
    {
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
        }
    }
}