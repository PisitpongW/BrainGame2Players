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
	public Renderer rnLeftPlayer, rnRightPlayer;
	public static Vector3 ltran, lVelocity, lAngular, rtran, rVelocity, rAngular, mtran, mVelocity, mAngular;
	public static int phase, con = 0;
	private MidballMovement midballMovement;

	public Material orangeMat, cyanMat;

	void Start()
	{
		print("Control start");

		GameObject checkGameObject = GameObject.FindGameObjectWithTag ("Midball");
        if (checkGameObject != null)
        {
            midballMovement = checkGameObject.GetComponent <MidballMovement>();
        }
        if (midballMovement == null)
        {
            Debug.Log("Cannot find 'ReadUDP' script");
        }
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
		if(isPlaying==true && (GameObject.Find("Player Left")==null||GameObject.Find("Player Right")==null||TimeController.isTimeout==true) && con==0 && phase!=0)
		{
			con = 1;
			StartCoroutine(HoldPlay());
		}
	}
	public IEnumerator HoldPlay()
	{
		if(TimeController.isTimeout == false) 
			yield return new WaitForSeconds(2f);
		EnableEndCanvas();
		isPlaying = false;

		if(TimeController.isTimeout == true)
			timeoutShow();
		else	
			gameoverShow();

		if(phase == 1)
		{
			print("Go Round 2");
			buttonText.text = "ROUND 2";

			midballMovement.con1 = 99f;
			midballMovement.con2 = 99f;

			rnLeftPlayer.material = cyanMat;
			rnRightPlayer.material = orangeMat;

			phase = 2;
		}
		else if(phase == 2)
		{
			print("Go Round 1");
			buttonText.text = "PLAY AGAIN";

			midballMovement.con1 = 99f;
			midballMovement.con2 = 99f;

			rnLeftPlayer.material = orangeMat;
			rnRightPlayer.material = cyanMat;

			phase = 3;
		}
		con = 0;
	}

	void gameoverShow()
	{
		print("Game over");
		endText.text = ">>GAME OVER<<";
		if(GameObject.Find("Player Left") == null) playerText.text = "PLAYER 2";
		else if(GameObject.Find("Player Right") == null) playerText.text = "PLAYER 1";
		showText.text = "W I N";
	}

	void timeoutShow()
	{
		print("Time out");
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
		TimeController.isTimeout = false;
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
