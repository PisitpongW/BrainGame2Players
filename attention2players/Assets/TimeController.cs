using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TimeController : MonoBehaviour
{
    public static float timeStart;
    public float timeSet;
	public static bool isTimeSet = false;
	public static bool isPlaying = false;
    public Text timeCountText;

	public CanvasGroup startCanvas;
	public CanvasGroup endCanvas;
	public Text endText, playerText, showText;

	public Rigidbody midBall;

    void Start()
    {
		print("timeController start");
		if(timeSet > 0) isTimeSet = true;

		startCanvas.alpha = 1;
		startCanvas.interactable = true;
		startCanvas.blocksRaycasts = true;
    }

	void Update()
	{
		if(isTimeSet && isPlaying)
		{
			timeCountText.text = (timeSet-(Time.time-timeStart)).ToString("00");
			if(timeSet-(Time.time-timeStart) < 0)
			{
				timeOut();
			}
		}
		if(isPlaying == false && Input.anyKey)
		{
			print("Press key to start");
			isPlaying = true;
			timeStart = Time.time;
			startCanvas.alpha = 0;
			startCanvas.interactable = false;
			startCanvas.blocksRaycasts = false;
		}
	}

	public void timeOut()
	{
		print("timeOut func");
		endText.text = ">>TIME OUT<<";
		if(GameObject.Find("Midball") != null)
		{
			if(midBall.transform.position.x < 150)
			{
				playerText.text = "PLAYER 2";
				showText.text = "W I N";
			}
			else if(midBall.transform.position.x > 150)
			{
				playerText.text = "PLAYER 1";
				showText.text = "W I N";
			}
			else
			{
				playerText.text = "DRAW";
				showText.text = "---";
			}
		}
		endCanvas.alpha = 1;
		endCanvas.interactable = true;
		endCanvas.blocksRaycasts = true;
		isPlaying = false;
	}
}
