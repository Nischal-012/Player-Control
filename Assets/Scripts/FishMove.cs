using UnityEngine;

public class FishMove : MonoBehaviour
{
	private FishSpawner fishSpawner;

	private bool hasWayPoint = false;

	private Vector3 currentWayPoint;
	private Vector3 LastWayPoint;
	private Animator fishAnimator;
	private float speed;

	private Collider col;


	void Start()
	{
		fishSpawner = transform.parent.GetComponentInParent<FishSpawner>();
		fishAnimator = GetComponent<Animator>();
		SetUpFish();
	}
	void SetUpFish()
	{
		col = transform.GetComponent<Collider>();
	}

	private void Update()
	{
		if (!hasWayPoint)
		{
			hasWayPoint = GetWayPoint();
		}
		else
		{
			RotateFish(currentWayPoint, speed);
			transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, speed * Time.deltaTime);
			CollideFish();
		}

		if (transform.position == currentWayPoint)
			hasWayPoint = false;

	}

	void CollideFish()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, transform.localScale.z))
		{
			if (hit.collider == col | hit.collider.tag == "waypoint") return;
			int randomNum = Random.Range(1, 100);
			if (randomNum < 40)
				hasWayPoint = false;

		}
	}

	Vector3 GetRandomWayPoint(bool isRandom)
	{
		if (isRandom)
			return fishSpawner.RandomPos();
		else
			return fishSpawner.RandomWayPoint();
	}

	bool GetWayPoint()
	{
		currentWayPoint = fishSpawner.RandomWayPoint();

		if (LastWayPoint == currentWayPoint)
		{
			currentWayPoint = GetRandomWayPoint(true);
			return false;
		}
		else
		{
			LastWayPoint = currentWayPoint;
			speed = Random.Range(1f, 7f);
			fishAnimator.speed = speed;
			return true;
		}
	}

	void RotateFish(Vector3 waypoint, float currentSpeed)
	{
		float turnSpeed = currentSpeed * Random.Range(1f, 3f);

		Vector3 LookAt = waypoint - this.transform.position;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookAt), turnSpeed * Time.deltaTime);
	}


}
