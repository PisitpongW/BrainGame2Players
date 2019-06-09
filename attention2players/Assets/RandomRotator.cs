using UnityEngine;

public class RandomRotator : MonoBehaviour 
{
	public Rigidbody rb;
	public Vector3 av;
	public float tumble;
	private void Start()
	{
		av = rb.angularVelocity;
		av.z = tumble;
		rb.angularVelocity = av;
	}
}
