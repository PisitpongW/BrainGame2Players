using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public CanvasGroup startCanvas;
	public CanvasGroup endCanvas;
	public static bool isPlaying = false;
	public Text endText, playerText, showText, buttonText;
	public Rigidbody rbLeftPlayer, rbRightPlayer, rbMidBall;
	public GameObject leftPlayer, rightPlayer, midBall;
	public static Vector3 ltran, lVelocity, lAngular, rtran, rVelocity, rAngular, mtran, mVelocity, mAngular;
	public static int phase, con = 0;

	void Start()
	{
		print("Control start");
		phase = 1;

		ltran = leftPlayer.transform.position;
		rtran = rightPlayer.transform.position;
		mtran = midBall.transform.position;

		lVelocity = rbLeftPlayer.velocity;
		rVelocity = rbRightPlayer.velocity;
		mVelocity = rbMidBall.velocity;
		lAngular = rbLeftPlayer.angularVelocity;
		rAngular = rbRightPlayer.angularVelocity;
		mAngular = rbMidBall.angularVelocity;

		DisableStartCanvas();
		DisableEndCanvas();
	}
	void Update()
	{
		CanvasCheck();
		GamePlayCheck();
		GameOverCheck();
	}

	void CanvasCheck()
	{
		if(phase == 1 && isPlaying == false)
		{
			EnableStartCanvas();
			DisableEndCanvas();
		}
		else if(phase == 2 && isPlaying == false)
		{
			EnableEndCanvas();
			DisableStartCanvas();
		}
	}

	void GamePlayCheck()
	{
		if(isPlaying == false && Input.anyKey) // Press any key to stat the game
		{
			print("Press key to start");
			TimeController.timeStart = Time.time;
			leftPlayer.SetActive(true);
			rightPlayer.SetActive(true);
			midBall.SetActive(true);
			leftPlayer.transform.position = ltran;
			rightPlayer.transform.position = rtran;
			midBall.transform.position = mtran;
			rbLeftPlayer.velocity = lVelocity;
			rbRightPlayer.velocity = rVelocity;
			rbMidBall.velocity = mVelocity;
			rbLeftPlayer.angularVelocity = lAngular;
			rbRightPlayer.angularVelocity = rAngular;
			//rbMidBall.angularVelocity = mAngular;

			if(phase == 1)
			{
				print("Play round 1");
				DisableStartCanvas();
				isPlaying = true;
			}
			else if(phase == 2)
			{
				print("Play round 2");
				DisableEndCanvas();
				isPlaying = true;
			}
			else if(phase == 3)
			{
				phase = 0;
				endText.text = " ";
				playerText.text = "PLEASE WAIT";
				showText.text = ". . .";
				print("Back to start");
				isPlaying = true;
				StartCoroutine(HoldStart());
			}
		}
	}
	public IEnumerator HoldStart()
	{
		leftPlayer.SetActive(false);
		rightPlayer.SetActive(false);
		midBall.SetActive(false);
		yield return new WaitForSeconds(3f);
		EnableStartCanvas();
		isPlaying = false;
		phase = 1;
	}
	
	void GameOverCheck()
	{
		if(isPlaying == true && (GameObject.Find("Player Left") == null || GameObject.Find("Player Right") == null) && con == 0 && phase != 0)
		{
			print("Game end");
			con = 1;
			StartCoroutine(HoldPlay());
		}
	}
	public IEnumerator HoldPlay()
	{
		yield return new WaitForSeconds(2f);
		EnableEndCanvas();
		isPlaying = false;
		endText.text = ">>GAME OVER<<";

		if(GameObject.Find("Player Left") == null) playerText.text = "PLAYER 2";
		else if(GameObject.Find("Player Right") == null) playerText.text = "PLAYER 1";

		showText.text = "W I N";
		if(phase == 1)
		{
			print("Go Round 2");
			buttonText.text = "ROUND 2";
			phase = 2;
		}
		else if(phase == 2)
		{
			print("Go Round 1");
			buttonText.text = "PLAY AGAIN";
			phase = 3;
		}
		con = 0;
	}
	
	void EnableStartCanvas()
	{
		startCanvas.alpha = 1;
		startCanvas.interactable = true;
		startCanvas.blocksRaycasts = true;
	}
	void DisableStartCanvas()
	{
		startCanvas.alpha = 0;
		startCanvas.interactable = false;
		startCanvas.blocksRaycasts = false;
	}
	void EnableEndCanvas()
	{
		endCanvas.alpha = 1;
		endCanvas.interactable = true;
		endCanvas.blocksRaycasts = true;
	}
	void DisableEndCanvas()
	{
		endCanvas.alpha = 0;
		endCanvas.interactable = false;
		endCanvas.blocksRaycasts = false;
	}
}
