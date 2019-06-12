using UnityEngine;

public class PlayerRightMovement : MonoBehaviour 
{
	public Rigidbody rb;
	public Rigidbody playerLeft;
	public Vector3 a, b, ltran, rtran;
	private ReadUDP readUDP;
	public float centerDist;
	public float v, j;
	public float data1, data2, dataDiff;
	void Start()
	{
		print("Player Right start");
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
			Physics.gravity = new Vector3(0, -15f, 0);

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

			a = rb.velocity;
			b = rb.angularVelocity;

			a.z = 0f;
			b.x = 0f;
			b.y = 0f;
			if(dataDiff > 0)
				a.x = dataDiff + readUDP.vFactor;
			else if(dataDiff < 0)
				a.x = dataDiff -readUDP.vFactor;
			else
				a.x = dataDiff;

			ltran = playerLeft.transform.position;
			rtran = rb.transform.position;
			rtran.x = ltran.x + 2*centerDist;
			rb.transform.position = rtran;

			rb.velocity = a;
			rb.angularVelocity = b;
		}
	}
}
