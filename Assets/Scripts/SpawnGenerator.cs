using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
	public GameObject[] propPrefabs;
	public int count = 100;

	private BoxCollider area;
	private List<GameObject> props = new List<GameObject>();

	private void Start()
	{
		area = GetComponent<BoxCollider>();

		for (int i = 0; i < count; ++i)
		{
			Spawn();
		}

		area.enabled = false;
	}

	private void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			Reset();
		}
	}

	public void Spawn()
	{
		int selection = Random.Range(0, propPrefabs.Length);
		GameObject selectedPrefab = propPrefabs[selection];
		Vector3 spawnPosition = GetRandomPosition();

		GameObject instance = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

		props.Add(instance);
	}

	public void Reset()
	{
		for (int i = 0; i < props.Count; ++i)
		{
			props[i].GetComponent<MeshRenderer>().material.color = Color.green;
			props[i].transform.position = GetRandomPosition();
			props[i].SetActive(true);
		}
	}

	private Vector3 GetRandomPosition()
	{
		Vector3 center = transform.position;
		Vector3 size = area.size;
		float x = center.x + Random.Range(-size.x/2f, size.x/2f);
		float y = center.y + Random.Range(-size.y/2f, size.y/2f);
		float z = center.z + Random.Range(-size.z/2f, size.z/2f);
		return new Vector3(x, y, z);
	}
}
