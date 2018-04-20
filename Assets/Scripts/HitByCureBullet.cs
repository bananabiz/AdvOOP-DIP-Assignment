using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HitByCureBullet : MonoBehaviour
{
    public Material[] mat;  //assign different materials

    private int cureValue = 50;
    private Renderer rend;
    private NavMeshAgent nav;
    private float navSpeed;
    //private Color colorStart = Color.red;
    //private Color colorEnd = Color.green;
    //private float colorDuration = 1.0f;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = mat[0];

        nav = GetComponent<NavMeshAgent>();
        navSpeed = nav.speed * 0.3f;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet1")
            rend.sharedMaterial = mat[2];

        if (other.gameObject.tag == "Cure")
        {
            //rend.material.color = Color.Lerp(colorStart, colorEnd, colorDuration); //should be placed in Update()
            
            StartCoroutine("CureStatus"); 
            //rend.sharedMaterial = mat[1];
            //change object's material once hit by Cure bullet and untag it
            gameObject.tag = "Untagged";
            
            Destroy(other.gameObject);

            Score.totalScore += cureValue;
            Score.cureScore++;
        }
    }

    IEnumerator CureStatus()  //change enemy color to cured status
    {
        yield return new WaitForSeconds(0.1f);
        rend.material.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        rend.material.color = Color.green;
        yield return new WaitForSeconds(0.15f);
        rend.material.color = Color.cyan;
        yield return new WaitForSeconds(0.15f);
        rend.material.color = Color.blue;
        yield return new WaitForSeconds(0.15f);
        rend.material.color = Color.grey;
        yield return new WaitForSeconds(0.15f);
        rend.material.color = Color.black;
        yield return new WaitForSeconds(0.15f);
        rend.material.color = Color.yellow;
        yield return new WaitForSeconds(0.1f); 
        rend.sharedMaterial = mat[1];
    }

    private void Update()
    {
        if (gameObject.tag == "Untagged")
            nav.speed = navSpeed;  //slow down movement 
    }
}