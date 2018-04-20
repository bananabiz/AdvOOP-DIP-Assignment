using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

namespace Networking
{
    [RequireComponent(typeof(Player))]
    public class NetworkUser : NetworkBehaviour
    {
        private Player player;
        private float moveH, moveV;

        // Use this for initialization
        void Awake()
        {
            player = GetComponent<Player>();
        }

        void Start()
        {
            if(!isLocalPlayer)
            {
                player.cam.enabled = false;
                // disable audio listener
            }
        }

        void FixedUpdate()
        {
            if(isLocalPlayer)
            {
                player.Move(moveH, moveV);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isLocalPlayer)
            {
                moveH = Input.GetAxis("Horizontal");
                moveV = Input.GetAxis("Vertical");
                float lookH = Input.GetAxis("Mouse X");
                float lookV = Input.GetAxis("Mouse Y");
                player.Look(lookH, lookV);
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    player.Jump();
                }
            }
        }
    }
}