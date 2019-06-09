using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollision : MonoBehaviour
{
	public GameObject explosion;
	void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Wall" || other.tag=="Player" || other.tag=="Midball")
		{
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
