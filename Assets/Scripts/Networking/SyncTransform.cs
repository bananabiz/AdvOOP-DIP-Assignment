using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Networking
{
    public class SyncTransform : NetworkBehaviour
    {
        public float lerpSpeed = 10f;

        ////threshold for when to send commands
        //public float positionThreshold = 0.5f;
        //public float rotationThreshold = 5.0f;

        ////record the previous position & rotation that was sent to the server
        //private Vector3 lastPosition;
        //private Quaternion lastRotation;

        [SyncVar] Vector3 syncPosition;
        [SyncVar] Quaternion syncRotation;
        
        // Update is called once per frame
        void Update()
        {
            if(isLocalPlayer)  //if current instance is local player
            {
                Cmd_SendPosition(transform.position);
                Cmd_SendRotation(transform.rotation);
            }
            else  //if current instance is not local player
            {
                transform.position = Vector3.Lerp(transform.position, syncPosition, lerpSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, syncRotation, lerpSpeed * Time.deltaTime);
            }
        }

        [Command] void Cmd_SendPosition(Vector3 position)
        {
            syncPosition = position;
        }

        [Command] void Cmd_SendRotation(Quaternion rotation)
        {
            syncRotation = rotation;
        }
    }
}