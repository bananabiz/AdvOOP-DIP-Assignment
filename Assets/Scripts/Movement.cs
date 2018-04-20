using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Character Set Up 
//Character Movement
[AddComponentMenu("Character Set Up / Character Movement")]
//This script requires the component Character controller
[RequireComponent(typeof(CharacterController))]

public class Movement : MonoBehaviour 
{
    #region Variables
    [Header("Characters MoveDirection")]
    //vector3 called moveDirection
    //we will use this to apply movement in worldspace
    public Vector3 moveDir = Vector3.zero;

    private CharacterController charC;

    [Header("Character Variables")]
    //public float variables jumpSpeed, speed, gravity
    public float jumpSpeed = 8.0f;
    public float speed = 6.0f;
    public float gravity = 20f;
    #endregion

    #region Start
    void Start()
    {
        //charc is on this game object we need to get the character controller that is attached to it
        charC = this.GetComponent<CharacterController>(); 
    }
    #endregion

    #region Update
    void Update()
    {
        //if our character is grounded
        if (charC.isGrounded)
        {
            //we are able to move in game scene meaning
            //Input Manager(https://docs.unity3d.com/Manual/class-InputManager.html)
            //Input(https://docs.unity3d.com/ScriptReference/Input.html)
            //moveDir is equal to a new vector3 that is affected by Input.Get Axis.. Horizontal, 0, Vertical
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            //moveDir is transformed in the direction of our moveDir
            moveDir = transform.TransformDirection(moveDir);
            //our moveDir is then multiplied by our speed
            moveDir *= speed;

            //we can also jump if we are grounded so
            //in the input button for jump is pressed then
            if (Input.GetButton("Jump"))
            {
                //our moveDir.y is equal to our jump speed
                moveDir.y = jumpSpeed;
            }
        }
        //regardless of if we are grounded or not the players moveDir.y is always affected by gravity timesed my time.deltaTime to normalize it
        moveDir.y -= gravity * Time.deltaTime;
        //we then tell the character Controller that it is moving in a direction timesed Time.deltaTime
        charC.Move(moveDir * Time.deltaTime);

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    #endregion
}










