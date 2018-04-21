using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Networking
{
    public class NetworkShoot : NetworkBehaviour
    {
        public float fireRate = 0.5f;  //amount of bullets that can be fired per second
        public float range = 100f;  //range that the bullet can travel
        public LayerMask mask;  //layermask of which layer to hit
        public GameObject bullet;
        public float bulletSpeed = 5;
        public Transform spawnBullet;

        private float fireFactor = 0f;  //timer for the fireRate
        private Camera mainCamera;  //reference the camera child
         
        // Use this for initialization
        void Start()
        {
            mainCamera = GetComponentInChildren<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isLocalPlayer)
            {
                HandleInput(); 
            }
        }

        //act as message that gets sent to all other clients that tells them which client got shot using it’s id
        [Command] void Cmd_PlayerShot(string _id)
        {
            Debug.Log("Player " + this.name + " has been shot!");  
        }

        [Command] void CmdShoot()
        {
            GameObject bulletIns = Instantiate(bullet, spawnBullet);
            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;

            // Spawn the bullet on the Clients
            NetworkServer.Spawn(bulletIns);

            Destroy(bulletIns, 2f);
        }

        void HandleInput()
        {
            fireFactor += Time.deltaTime;

            if (fireFactor >= fireRate)
            {
                if (Input.GetMouseButton(0))
                {
                    CmdShoot();
                    fireFactor = 0;
                }
            }
        }

    }
}
