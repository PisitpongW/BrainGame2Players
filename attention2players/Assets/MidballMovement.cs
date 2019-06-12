using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidballMovement : MonoBehaviour 
{
	public Rigidbody rb;
	public Rigidbody playerLeft;
	public Vector3 a, b, ltran, mtran, newForce;
	private ReadUDP readUDP;
	public float centerDist, dataDiff, data1, data2;
	public float v, j, tumble;
	public float con1 = 99f, con2 = 99f;
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
		if(GameController.isPlaying == true && GameObject.Find("Player Left")!=null && GameObject.Find("Player Right")!=null)
		{
			Movement();
			//MidballMove();
		}
	}
	void Movement()
	{
		if(GameController.phase == 1)
		{
			data1 = readUDP.data1float;
			data2 = readUDP.data2float;
		}
		else if(GameController.phase == 2)
		{
			data1 = readUDP.data2float;
			data2 = readUDP.data1float;
		}
		dataDiff = data1 - data2;

		// Velocity setting
		a = rb.velocity;
		a.z = 0f;
		if(data1 != con1 || data2 != con2)
		{
			if(dataDiff > 0)
				a.x = dataDiff + readUDP.vFactor;
			else if(dataDiff < 0)
				a.x = dataDiff -readUDP.vFactor;
			else
				a.x = dataDiff;
		}
		newForce = new Vector3(dataDiff * readUDP.aFactor, 0f, 0f);
		rb.AddForce(newForce, ForceMode.Acceleration);
		rb.velocity = a;

		// Rotation setting
		b = rb.angularVelocity;
		if(dataDiff > 0) {b.z = -tumble; rb.angularVelocity = b;}
		else if(dataDiff < 0) {b.z = tumble; rb.angularVelocity = b;}
		else {b.z = 0; rb.angularVelocity = b;}

		con1 = data1;
		con2 = data2;
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
