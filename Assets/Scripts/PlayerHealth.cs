using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth = 100;
    public float maxHealth = 100;
    public float damage = 10;
    public Image hpBar;
    public GameObject redPanel;
    public GameObject losePanel;
    public AudioSource hurtSound;

    private bool isHurt = false;

    void Awake()
    {
        hpBar = GameObject.Find("HP_Image").GetComponent<Image>();
        redPanel = GameObject.Find("RedPanel");
        losePanel = GameObject.Find("LosePanel");
        hurtSound = GameObject.Find("HurtSound").GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start ()
    {
        Time.timeScale = 1;
        losePanel.SetActive(false);
        maxHealth = 100;
        //reset health to full on game load
        currentHealth = maxHealth;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isHurt)
        {
            redPanel.SetActive(false);
        }
        else
        {
            redPanel.SetActive(true);
            isHurt = false;
        }

        if (hpBar.fillAmount <= 0)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0;

            if (Input.GetButtonDown("Fire2"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Score.totalScore = 0;
                Score.cureScore = 0;
                Score.captureScore = 0;
            }
        }
        if (currentHealth <= 40)
        {
            hpBar.color = new Color(255, 0, 0);
        }
	}

    void OnCollisionEnter(Collision enemy)
    {
        
        if (enemy.gameObject.tag == "Enemy")
        {
            isHurt = true;
            currentHealth -= damage;
            hpBar.fillAmount = currentHealth / maxHealth;
            hurtSound.Play(); 
        }
    }
}
