using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;
    public float Speed = 12.0f;
    public Vector3 Velocity;
    public float Gravity = 9.81f;

    public Transform GroundCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;
    private bool IsGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (IsGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }
        
        float MovementX = Input.GetAxis("Horizontal");
        float MovementZ = Input.GetAxis("Vertical");

        Vector3 Movement = transform.right * MovementX + transform.forward * MovementZ;

        Controller.Move(Movement * Speed * Time.deltaTime);

        Velocity.y -= Gravity * Time.deltaTime;
        Controller.Move(Velocity * Time.deltaTime);
    }
}
