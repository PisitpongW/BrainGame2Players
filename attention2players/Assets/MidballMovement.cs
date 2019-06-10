using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidballMovement : MonoBehaviour 
{
	public Rigidbody rb;
	public Rigidbody playerLeft;
	public Vector3 a, b, ltran, mtran;
	public float centerDist; // will be change to position from UDP
	public float v, j, tumble;
	public int con = 0;
	void Start()
	{
		print("Midball start");
	}
	void Update () 
	{
		if(GameObject.Find("Player Left")!=null && GameObject.Find("Player Right")!=null)
		{
			ltran = playerLeft.transform.position;
			mtran = rb.transform.position;

			// Midball controller
			//mtran.x = ltran.x + centerDist;
			a = rb.velocity;
			b = rb.angularVelocity;
			a.z = 0f;
			rb.velocity = a;
			if(Input.GetKey("right"))
			{
				a.x = v;
				b.z = -tumble;
				rb.velocity = a;
				rb.angularVelocity = b;
			}
			if(Input.GetKey("left"))
			{
				a.x = -v;
				b.z = tumble;
				rb.velocity = a;
				rb.angularVelocity = b;
			}

			rb.transform.position = mtran;
		}
	}
}
