using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

namespace Networking
{
    public class SyncTransform : NetworkBehaviour
    {
        public float lerpSpeed = 10f;

        [SyncVar] Vector3 syncPosition;
        [SyncVar] Quaternion syncRotation;
        
        // Update is called once per frame
        void Update()
        {
            if(isLocalPlayer)
            {
                Cmd_SendPosition(transform.position);
                Cmd_SendRotation(transform.rotation);
            }
            else
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