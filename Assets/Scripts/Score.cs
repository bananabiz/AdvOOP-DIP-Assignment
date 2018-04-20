using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int totalScore;
    public static int cureScore;
    public static int captureScore;
    public Text totalScoreText, cureScoreText, captureScoreText;
    public float scoreSpeed = 10f;

    private float displayScore;

	// Use this for initialization
	void Awake ()
    {
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        displayScore = Mathf.Lerp(displayScore, totalScore, scoreSpeed * Time.deltaTime);
        totalScoreText.text = Mathf.Round(displayScore).ToString();

        cureScoreText.text = cureScore.ToString();
        captureScoreText.text = captureScore.ToString();
	}
}
