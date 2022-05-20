using UnityEngine;
using UnityEngine.InputSystem;

public class HeadBob : MonoBehaviour
{
    private Inputs playerInput;
    private Inputs.OnFootActions onFoot;

    public float walkingBobbingSpeed = 14f;
    public float bobbingAmount = 0.05f;
    public CharacterController controller;

    float defaultPosY = 0;
    float timer = 0;

    Vector2 idleVector = new Vector2(0,0);

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake() 
    {
        playerInput = new Inputs(); 
        onFoot = playerInput.OnFoot; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(controller.isGrounded);
        
    }
}