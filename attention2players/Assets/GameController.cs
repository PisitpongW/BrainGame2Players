using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public CanvasGroup endCanvas;
	public Text endText, playerText, showText, buttonText;
	public static int round;

	void Start()
	{
		print("Control start");

		round = 1;
		endCanvas.alpha = 0;
		endCanvas.interactable = false;
		endCanvas.blocksRaycasts = false;
	}
	void Update()
	{
		
		if(GameObject.Find("Player Left") == null)
		{
			endCanvas.alpha = 1;
			endCanvas.interactable = true;
			endCanvas.blocksRaycasts = true;
			endText.text = ">>GAME OVER<<";
			playerText.text = "PLAYER 2";
			showText.text = "W I N";
			if(round == 1)
			{
				buttonText.text = "ROUND 2";
				round = 1;
			}
			else if(round == 2)
			{
				buttonText.text = "PLAY AGAIN";
				round = 1;
			}
		}
		else if(GameObject.Find("Player Right") == null)
		{
			endCanvas.alpha = 1;
			endCanvas.interactable = true;
			endCanvas.blocksRaycasts = true;
			endText.text = ">>GAME OVER<<";
			playerText.text = "PLAYER 1";
			showText.text = "W I N";

			if(round == 1)
			{
				buttonText.text = "ROUND 2";
				round = 2;
			}
			else if(round == 2)
			{
				buttonText.text = "PLAY AGAIN";
				round = 1;
			}
		}
	}
}
