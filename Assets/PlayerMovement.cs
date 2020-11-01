using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera cam;
    public CharacterController controller;
    public GameObject target; //New Variable
    public float speed = 1.0f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    private float verticalVelocity;

    public Transform groundCheck; 
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Debug.DrawLine(ray.origin, hit.point);


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }
        
        //Jumping 
        
        //controller.Move(velocity * Time.deltaTime); // delta_y= gt^2

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            UnityEngine.Debug.Log("pass");
        }


        //Simple Movement 
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x * speed
            + transform.forward * z * speed
            + transform.up * velocity.y;

        controller.Move(move * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

       
        if (Input.GetButtonDown("Fire1"))
        {
            transform.RotateAround(target.transform.position, Vector3.left , 20 * Time.deltaTime);
            UnityEngine.Debug.Log("FIRE");
        }
    }
}