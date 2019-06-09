using UnityEngine;

public class PlayerLeftMovement : MonoBehaviour 
{
	public Rigidbody rb;
	public Vector3 a, b;
	public float v, j;
	void Start()
	{
		print("Player Left start");
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
