using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    [RequireComponent(typeof(Player))]
    public class LocalUser : MonoBehaviour
    {
        private Player player;
        private float moveH, moveV;

        // Use this for initialization
        void Awake()
        {
            player = GetComponent<Player>();
        }

        void FixedUpdate()
        {
            player.Move(moveH, moveV);
        }

        // Update is called once per frame
        void Update()
        {
            moveH = Input.GetAxis("Horizontal");
            moveV = Input.GetAxis("Vertical");

            float lookH = Input.GetAxis("Mouse X");
            float lookV = Input.GetAxis("Mouse Y");
            player.Look(lookH, lookV);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.Jump();
            }
        }
    }
}