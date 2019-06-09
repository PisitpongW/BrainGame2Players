using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour 
{
	public GameObject midball;
	private AudioSource ad;
	public AudioClip forestAudio;
	public AudioClip winterAudio;
	public AudioClip desertAudio;
	public AudioClip oceanAudio;
	public static int state, con;
	void Start()
	{
		ad = gameObject.GetComponent<AudioSource>();
		state = 0;
		con = 0;
	}
	void Update()
	{
		if(GameObject.Find("Midball")!=null)
		{
			if(midball.transform.position.x>=0 && midball.transform.position.x<75 && state!=1)
			{
				ad.clip = forestAudio;
				ad.Play();
				print("Play Forest");
				state = 1;
			}
			if(midball.transform.position.x>=75 && midball.transform.position.x<150 && state!=2)
			{
				ad.clip = winterAudio;
				ad.Play();
				print("Play Winter");
				state = 2;
			}
			if(midball.transform.position.x>=150 && midball.transform.position.x<225 && state!=3)
			{
				ad.clip = desertAudio;
				ad.Play();
				print("Play Desert");
				state = 3;
			}
			if(midball.transform.position.x>=225 && midball.transform.position.x<=300 && state!=4)
			{
				ad.clip = oceanAudio;
				ad.Play();
				print("Play Ocean");
				state = 4;
			}
		}
	}
}
