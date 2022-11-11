using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public float thrusterStrength;
    public float thrusterDistance;
    public Transform[] thrusters;
    public Rigidbody rb;

    private void Awake()
    {
        rb = rb.GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        foreach (Transform thruster in thrusters)
        {
            Vector3 downwardForce;
            float distancePercentage;
            if (Physics.Raycast(thruster.position, thruster.up * -1, out hit, thrusterDistance))
            {
                distancePercentage = 1 - (hit.distance / thrusterDistance);
                downwardForce = transform.up * thrusterStrength * distancePercentage;
                downwardForce = downwardForce * Time.deltaTime * GetComponent<Rigidbody>().mass;

                GetComponent<Rigidbody>().AddForceAtPosition(downwardForce, thruster.position);
            }

        }

    }
}