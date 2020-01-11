using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
	public ParticleSystem explosionPaticle;
	public float hp = 100f;
	private float fullHp;
	private Material material;

	private void Start()
	{
		material = GetComponent<MeshRenderer>().material;
		fullHp = hp;
	}

	public void TakeDamage(float damage)
	{
		hp -= damage;
		Debug.Log("HP: " + hp);

		applyDamageColor();

		if (hp <= 0)
		{
			ParticleSystem instance = Instantiate(explosionPaticle);
			AudioSource audio = instance.GetComponent<AudioSource>();

			instance.Play();
			audio.Play();

			Destroy(instance.gameObject, instance.main.duration);

			gameObject.SetActive(false);
		}
	}

	private void applyDamageColor()
	{
		Color hpColor = Color.green;
		float hpRate = hp / fullHp;

		if (hpRate > 0.5f)
		{
			hpColor.r = -2f * hpRate + 2f;
			hpColor.g = 1f;
			hpColor.b = 0f;
		}
		else
		{
			hpColor.r = 1f;
			hpColor.g = 2f * hpRate;
			hpColor.b = 0f;
		}

		material.color = hpColor;
	}
}
