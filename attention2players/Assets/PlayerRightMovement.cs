using UnityEngine;

public class PlayerRightMovement : MonoBehaviour 
{
	public Rigidbody rb;
	public Rigidbody playerLeft;
	public Vector3 a, b, ltran, rtran;
	public float centerDist;
	public float v, j;
	void Start()
	{
		print("Player Right start");
	}
	void Update () 
	{
		if(GameObject.Find("Player Left")!=null && GameObject.Find("Player Right")!=null)
		{
			Physics.gravity = new Vector3(0, -15f, 0);
			a = rb.velocity;
			b = rb.angularVelocity;
			a.z = 0f;
			b.x = 0f;
			b.y = 0f;
			rb.velocity = a;
			rb.angularVelocity = b;
			ltran = playerLeft.transform.position;
			rtran = rb.transform.position;
			rtran.x = ltran.x + 2*centerDist;
			rb.transform.position = rtran;
			if(Input.GetKey("d"))
			{
				a.x = v;
				rb.velocity = a;
			}
			if(Input.GetKey("a"))
			{
				a.x = -v;
				rb.velocity = a;
			}
		}
	}
}
