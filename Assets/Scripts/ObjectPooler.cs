/*using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooler : MonoBehaviour
{
	[System.Serializable]
	public class Pool
	{

		public GameObject[] prefab;
		public int size;
	}

	#region Singleton
	public ObjectPool<GameObject>();
	public static ObjectPooler Instance;
	void Awake()
	{
		Instance = this;
	}

	#endregion

	public Pool pool;
	private List<GameObject> objectPool = new List<GameObject>();
	void Start()
	{
		for (int i = 0; i < pool.size; i++)
		{
			GameObject obj = Instantiate(pool.prefab[Random.Range(0, 4)]);
			obj.SetActive(false);
			obj.transform.parent = transform;
			objectPool.Add(obj);
		}
	}

	public GameObject SpawnFromPool(int amount, Vector3 position, Quaternion rotation)
	{
		GameObject objectToSpawn = objectPool[i];
		objectToSpawn.SetActive(true);
		objectToSpawn.transform.position = position;
		objectToSpawn.transform.rotation = rotation;
		return objectToSpawn;

	}
}
*/