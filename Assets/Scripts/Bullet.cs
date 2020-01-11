using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public ParticleSystem explosionParticle;
	public AudioSource explosionAudio;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Plane")
		{
			explosionParticle.transform.parent = null;

			explosionParticle.Play();
			explosionAudio.Play();

			Destroy(explosionParticle.gameObject, explosionParticle.main.duration);
			Destroy(gameObject);
		}
	}
}
