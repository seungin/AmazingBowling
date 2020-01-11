using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public LayerMask propLayerMask;
	public ParticleSystem explosionParticle;
	public AudioSource explosionAudio;

	public float explosionForce = 1000f;
	public float explosionDamage = 100f;
	public float explosionRadius = 20f;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Plane")
		{
			Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, propLayerMask);

			for (int i = 0; i < colliders.Length; ++i)
			{
				Rigidbody body = colliders[i].GetComponent<Rigidbody>();
				body.AddExplosionForce(explosionForce, transform.position, explosionRadius);

				Prop prop = colliders[i].GetComponent<Prop>();
				float damage = calculateDamage(prop.transform.position);
				prop.TakeDamage(damage);
			}

			explosionParticle.transform.parent = null;

			explosionParticle.Play();
			explosionAudio.Play();

			Destroy(explosionParticle.gameObject, explosionParticle.main.duration);
			Destroy(gameObject);
		}
	}

	private float calculateDamage(Vector3 targetPosition)
	{
		float distance = (transform.position - targetPosition).magnitude;
		float damageRate = (explosionRadius - distance) / explosionRadius;
		damageRate = Mathf.Max(damageRate, 0);
		return damageRate * explosionDamage;
	}
}
