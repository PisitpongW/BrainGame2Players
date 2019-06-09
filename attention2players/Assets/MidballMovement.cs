using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidballMovement : MonoBehaviour 
{
	public Rigidbody rb;
	public Rigidbody playerLeft;
	public Vector3 a, ltran, mtran;
	public float centerDist; // will be change to position from UDP
	public float v, j;
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
			a.z = 0f;
			rb.velocity = a;
			if(Input.GetKey("right"))
			{
				a.x = v;
				rb.velocity = a;
			}
			if(Input.GetKey("left"))
			{
				a.x = -v;
				rb.velocity = a;
			}

			rb.transform.position = mtran;
		}
	}
}
