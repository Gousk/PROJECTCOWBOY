using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {

    // Use this for initialization
    [SerializeField] CharacterController cc;
    float nextTimeToStep = 0f;
    float stepRate = 2f;
    bool isWalking;
 
 // Update is called once per frame
    void Update ()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D)) && cc.isGrounded == true)
        { 
            if (Time.time >= nextTimeToStep)
            {
                nextTimeToStep = Time.time + 1f / stepRate;
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
