using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject AK47;
    public GameObject AUG;

    float timeElapsed;
    float lerpDuration = 3;

    Vector3 startPos = new Vector3 (0.197f, -0.707f, 0.458f);
    Vector3 goalPos = new Vector3(0.197f, -0.302f, 0.638f);
    Quaternion startRot = new Quaternion(-82.614f, -251.7f, 72.15f, 0f);
    Quaternion goalRot = new Quaternion(4.803f, -187.035f, 0f, 0f);

    void Update()
    {  
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AK47.SetActive(true); 
            AUG.SetActive(false);
            // if (timeElapsed < lerpDuration)
            // {
            //     AK47.transform.localPosition = Vector3.Lerp(startPos, goalPos, timeElapsed / lerpDuration);
            //     timeElapsed += Time.deltaTime;
            // }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AK47.SetActive(false); 
            AUG.SetActive(true); 
        }   
    }
}
