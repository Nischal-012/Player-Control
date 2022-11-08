using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water123 : MonoBehaviour
{
    public Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "WaterDetector")
            anim.SetBool("isSwimming", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "WaterDetector")
            anim.SetBool("isSwimming", false);
    }
}
