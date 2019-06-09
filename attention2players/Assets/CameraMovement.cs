using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
	public Transform playerLeft;
	public Vector3 offset;
	void Start()
	{
		print("Camera start");
	}
	void Update()
	{
		if(GameObject.Find("Player Left")!=null && GameObject.Find("Player Right")!=null)
		{
			transform.position = playerLeft.position + offset;
		}
	}
}
