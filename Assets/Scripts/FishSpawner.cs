using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FishSpawner : MonoBehaviour
{

	public Vector3 spawnArea;
	public int poolSize;
	public List<Transform> Waypoints = new List<Transform>();
	public Color gizmoColor;
	public int ActiveCount;
	public int InactiveCount;
	[SerializeField] private ObjectPool<GameObject> pool;
	[SerializeField] private GameObject[] fishPrefab;
	private GameObject objectToSpawn;

	private void Start()
	{
		pool = new ObjectPool<GameObject>(() =>
		{
			GameObject fish;
			fish = Instantiate(fishPrefab[Random.Range(0, 4)]);
			fish.transform.parent = transform;
			return fish;

		}, fish =>
		{
			fish.gameObject.SetActive(true);
		}, fish =>
		{
			fish.gameObject.SetActive(false);
		}, fish =>
		{
			Destroy(fish.gameObject);
		});

	}
	private void Update()
	{
		if (pool.CountAll < poolSize)
		{
			SpawnFromPool();
		}
		else
			return;

	}
	public void SpawnFromPool()
	{
		Quaternion randomRotation = Quaternion.Euler(Random.Range(-20, 20), Random.Range(0, 360), 0);

		objectToSpawn = pool.Get();
		objectToSpawn.transform.position = RandomPos();
		objectToSpawn.transform.rotation = randomRotation;
		InactiveCount = pool.CountInactive;
		ActiveCount = pool.CountActive;
	}

	public Vector3 RandomPos()
	{
		Vector3 randomPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y), Random.Range(-spawnArea.z, spawnArea.z));
		return randomPosition = transform.TransformPoint(randomPosition * 0.5f);
	}

	public Vector3 RandomWayPoint()
	{
		int randomWP = Random.Range(0, (Waypoints.Count - 1));
		Vector3 randomWayPoint = Waypoints[randomWP].transform.position;
		return randomWayPoint;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = gizmoColor;
		Gizmos.DrawCube(transform.position, spawnArea);
	}

}
