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
	public static bool isTimeout = false;
    public Text timeCountText;

	public CanvasGroup startCanvas;
	public CanvasGroup endCanvas;
	public Text endText, playerText, showText;
	public GameObject leftPlayer, rightPlayer;
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
				isTimeout = true;
			}
		}
	}
}
