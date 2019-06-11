using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidballMovement : MonoBehaviour 
{
	public Rigidbody rb;
	public Rigidbody playerLeft;
	public Vector3 a, b, ltran, mtran;
	private ReadUDP readUDP;
	public float centerDist, centerVelo, centerDiff, data1, data2, factor1, factor2;
	public float v, j, tumble;
	public int con = 0;
	void Start()
	{
		print("Midball start");
		GameObject checkGameObject = GameObject.FindGameObjectWithTag ("UDPReceiver");
        if (checkGameObject != null)
        {
            readUDP = checkGameObject.GetComponent <ReadUDP>();
        }
        if (readUDP == null)
        {
            Debug.Log("Cannot find 'ReadUDP' script");
        }
	}
	void Update () 
	{
		if(GameObject.Find("Player Left")!=null && GameObject.Find("Player Right")!=null)
		{
			//Movement1();

			//Movement2();

			MidballMove();
		}
	}
	void Movement1()
	{
		ltran = playerLeft.transform.position;
		mtran = rb.transform.position;

		data1 = readUDP.data1float;	// Ref data1 from UDP
		data2 = readUDP.data2float; // Ref data2 from UDP
		centerDiff = data1 - data2; // Different power

		// Position setting
		centerDist = (centerDiff)*factor1;  // *** Power scaling with using range ***

		mtran.x = ltran.x + centerDist; // Set distance from Left Player
		rb.transform.position = mtran;

		// Rotation setting
		b = rb.angularVelocity;
		if(centerDiff > 0) {b.z = -tumble; rb.angularVelocity = b;}
		else if(centerDiff < 0) {b.z = tumble; rb.angularVelocity = b;}
		else {b.z = 0; rb.angularVelocity = b;}
	}
	void Movement2()
	{
		data1 = readUDP.data1float;	// Ref data1 from UDP
		data2 = readUDP.data2float; // Ref data2 from UDP
		centerDiff = data1 - data2; // Different power

		// Velocity setting
		a = rb.velocity;
		a.z = 0f;
		float centerVelo = (centerDiff)*factor2; // *** Power scaling with using range ***
		a.x = centerVelo; // Set velocity of Midball
		rb.velocity = a;

		// Rotation setting
		b = rb.angularVelocity;
		if(centerDiff > 0) {b.z = -tumble; rb.angularVelocity = b;}
		else if(centerDiff < 0) {b.z = tumble; rb.angularVelocity = b;}
		else {b.z = 0; rb.angularVelocity = b;}
	}
	void MidballMove()
	{
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
	}
}
