using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCube : MonoBehaviour
{
    public int destroyDelay = 5;
    public Material[] matGlass;
    private int cubeValue = 10;
    private Renderer rend;

	// Use this for initialization
	void Start ()
    {
        Destroy(this.gameObject, destroyDelay);

        Score.totalScore += cubeValue;

        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = matGlass[0];
        StartCoroutine("CrackTexture");
    }
	
	// Update is called once per frame
	void Update ()
    {
       
	}

    IEnumerator CrackTexture()  //change to crack material
    {
        yield return new WaitForSeconds(destroyDelay * 0.5f);
        rend.sharedMaterial = matGlass[1];
        yield return new WaitForSeconds(destroyDelay * 0.3f);
        rend.sharedMaterial = matGlass[2];
    }
}
