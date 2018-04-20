using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinIfCollide : MonoBehaviour
{
    public GameObject winPanel;
    public AudioSource winSound;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            winPanel.SetActive(true);
            winSound.Play(); 
            Time.timeScale = 0;
        }
    }

    private void Update()
    {
        if (Time.timeScale == 0)  //load current scene if player win and right click 
        {
            if (Input.GetButtonDown("Fire2"))
            {
                winPanel.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
