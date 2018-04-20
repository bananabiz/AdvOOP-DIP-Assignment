using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    public class Player : MonoBehaviour
    {
        public float movementSpeed = 10f;
        public float lookSpeed = 30f;
        public float jumpHeight = 4f;
        public Camera cam;

        private bool isGrounded = false;
        private Rigidbody rigid;
        private float yaw, pitch;

        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody>();
        }
        
        public void Move(float h, float v)
        {
            Vector3 position = rigid.position;
            
            position += transform.forward * v * movementSpeed * Time.deltaTime;
            position += transform.right * h * movementSpeed * Time.deltaTime;

            rigid.MovePosition(position);
        }

        public void Look(float h, float v)
        {
            yaw += h * lookSpeed * Time.deltaTime;
            pitch += v * lookSpeed * Time.deltaTime;

            transform.eulerAngles = new Vector3(0, yaw, 0);
            cam.transform.localEulerAngles = new Vector3(-pitch, 0, 0);
        }

        public void Jump()
        {
            if(isGrounded)
            {
                rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        void OnCollisionEnter(Collision col)
        {
            isGrounded = true;
        }
    }
}