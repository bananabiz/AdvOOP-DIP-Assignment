using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun1 : MonoBehaviour
{

    public GameObject bullet;
    public GameObject platform;
    public GameObject laserStart;
    public float fireRate = .25f;
    public float weaponRange = 80f;
    //public float buildRange = 30f;
    public GameObject shotHint;
    public Image weaponReloadBar;
    public GameObject aim;
    public AudioSource gunSound, buildSound, lockSound;

    private Vector3 start;
    private Vector3 end;
    private LineRenderer laserLine;
    //private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.08f);
    private WaitForSeconds shotHintDuration = new WaitForSeconds(1.1f);
    private float nextFire;
    private int lockTarget;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        //gunAudio = GetComponent<AudioSource>();
        //fpsCam = GetComponentInParent<Camera>();
    }
    
    void FixedUpdate()
    {
        RaycastHit other;
        if (Physics.Raycast(transform.position, transform.forward, out other))
        {
            // calculate distance between raycast hit point and player
            float distance = Vector3.Distance(other.transform.position, transform.position);

            // update position of laser
            start = laserStart.transform.position;
            end = other.point;

            if (other.transform.tag == "Enemy")  //activate aim animation if raycast hit on enemy
            {
                lockTarget++;   
                aim.SetActive(true);
                StartCoroutine("LockSound");
            }
            else
            {
                aim.SetActive(false);
                lockTarget = 0;
            }

            // shoot bullet under shoot rate
            //if (Input.GetButton("Fire1") && other.transform.tag == "Enemy" && Time.time > nextFire && weaponReloadBar.fillAmount >= 1)
            if (Input.GetButton("Fire1") && other.transform.tag == "Enemy" && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                gunSound.Play();
                // shoot bullet if within shoot range
                if (distance <= weaponRange)
                {
                    StartCoroutine(ShotEffect());
                    //shoot bullet facing target
                    Instantiate(bullet, other.point + new Vector3(0, 0.5f, -0.1f), Quaternion.LookRotation(other.transform.position - transform.position));
                    //print(other.transform.name);
                    
                    weaponReloadBar.fillAmount = 0;
                }
                // show hint if out of shoot range
                if (distance > weaponRange)
                {
                    StartCoroutine(ShotHintEffect());
                }
            }
            
            // build platform in front of player under conditions
            if (Input.GetButtonDown("Fire2") && (other.transform.tag == "Environment" || other.transform.tag == "Platform"))
            {
                Instantiate(platform, transform.position + (transform.forward * 3), Quaternion.identity);
                buildSound.Play();
            }

            weaponReloadBar.fillAmount += Time.deltaTime / fireRate;
            if (weaponReloadBar.fillAmount >= 1)
            {
                weaponReloadBar.fillAmount = 1;
            }
        }
    }
    
    private IEnumerator ShotEffect()
    {
        //gunAudio.Play();
        laserLine.enabled = true;

        // render laser between gun and raycast hit point
        laserLine.SetPosition(0, start);
        laserLine.SetPosition(1, end);

        yield return shotDuration;

        laserLine.enabled = false;
    }

    private IEnumerator ShotHintEffect()
    {
        shotHint.SetActive(true);
        yield return shotHintDuration;
        shotHint.SetActive(false);
    }

    IEnumerator LockSound()  // slow down lock target sound
    {
        if (lockTarget == 1)  //play lock sound one time only when raycast keep hitting same object
        {
            lockSound.Play();
            yield return new WaitForSeconds(0.1f);
            lockSound.Play();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
