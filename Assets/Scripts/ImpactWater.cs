using UnityEngine;

public class ImpactWater : MonoBehaviour
{
	public GameObject particleImpact;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Water")
			particleImpact.GetComponent<ParticleSystem>().Play();
	}
}
