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
    public Text timeCountText;

	public CanvasGroup startCanvas;
	public CanvasGroup endCanvas;
	public Text endText, playerText, showText;

	public Rigidbody midBall;

    void Start()
    {
		print("timeController start");
		if(timeSet > 0) isTimeSet = true;
    }

	void Update()
	{
		if(isTimeSet == true && GameController.isPlaying == true) // Game running
		{
			timeCountText.text = (timeSet-(Time.time-timeStart)).ToString("00");
			if(timeSet-(Time.time-timeStart) < 0)
			{
				timeOut();
			}
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
		GameController.isPlaying = false;
	}
}
