using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public GameObject cam;

    private Inputs playerInput;
    private Inputs.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;

    public float walkingBobbingSpeed = 14f;
    public float bobbingAmount = 0.05f;
    public CharacterController controller;

    float defaultPosY = 0.573f;
    float timer = 0;

    Vector2 idleVector = new Vector2(0,0);

    private void Awake() 
    {
        Cursor.lockState = CursorLockMode.Locked;

        var mousePos = Input. mousePosition;
        mousePos. x -= Screen. width/2;
        mousePos. y -= Screen. height/2;

        playerInput = new Inputs(); 
        onFoot = playerInput.OnFoot; 

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Courch.performed += ctx => motor.Courch();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        //onFoot.Shoot.performed += ctx => motor.Shoot();  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());

        Debug.Log(onFoot.Movement.ReadValue<Vector2>());
        if (controller.isGrounded == true && onFoot.Movement.ReadValue<Vector2>() != idleVector)
        {
            //Player is moving
            timer += Time.deltaTime * walkingBobbingSpeed;
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, cam.transform.localPosition.z);
        }
        else
        {
            //Idle
            timer = 0;
            cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, Mathf.Lerp(cam.transform.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), cam.transform.localPosition.z);
        }    
    }

    private void LateUpdate() 
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable() 
    {
        onFoot.Enable();    
    }

    private void OnDisable() 
    {
        onFoot.Disable();    
    }
}
