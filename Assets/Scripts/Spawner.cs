using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float dis = 100;
    public GameObject enemyPrefab;
    public int sec = 15;
    public bool isSpawned = false;
    public Transform spawnLineStart;

    private LineRenderer spawnLine;

    // Use this for initialization
    void Start ()
    {
        spawnLine = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        Vector3 targetPosition = target.transform.position;
        float distance = Vector3.Distance(transform.position, targetPosition);

        //spawn enemy once player's position is within range
        if (distance < dis && isSpawned == false)
        {
            StartCoroutine(Wait(sec));
        }
    }

    IEnumerator Wait(int seconds)
    {
        isSpawned = true;
        //shoot laser to the ground
        spawnLine.enabled = true;
        spawnLine.SetPosition(0, spawnLineStart.position);
        spawnLine.SetPosition(1, transform.position + new Vector3(0, -2.5f, 0));
        yield return new WaitForSeconds(1);
        //spawn enemy in 1 second
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        spawnLine.enabled = false;
        Debug.Log("go");
        //delay spawn time
        yield return new WaitForSeconds(seconds);
        isSpawned = false;
    }
}
