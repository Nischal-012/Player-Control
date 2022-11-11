using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Swimming : MonoBehaviour
{
    public string waterTag = "Water";
    public PlayerController playerController;

    public float colliderRadius = .5f;
    public float colliderHeight = .5f;
    public float heightOffset = .3f;

    public GameObject impactEffect;
    public GameObject waterRingEffect;
    public float waterRingFrequencyIdle = .8f;
    public float waterRingFrequencySwim = .15f;
    public GameObject waterDrops;
    public float waterDropsYOffset = 1.6f;

    public UnityEvent OnEnterWater;
    public UnityEvent OnExitWater;
    public UnityEvent OnAboveWater;

    public GameObject water;
    public bool isSwimming;
    public bool inTheWater;

    protected float waterHeightLevel;
    protected float timer;
    protected float originalMoveSpeed;
    protected float originalRotationSpeed;
    protected float waterRingSpawnFrequency;
    protected bool triggerSwimState;
    protected bool triggerAboveWater;

    protected virtual void SwimmingBehaviour()
    {
        if (water)
        {
            waterHeightLevel = water.transform.position.y;
        }

        if (isSwimming)
        {
                if (!triggerSwimState)
                {
                    EnterSwimState();               
                }                          
            else
            {
                ExitSwimState();                                                            
            }
        }
        else
        {
            ExitSwimState();
        }
    }
    public void OnActionEnter(Collider other)
    {
        if (other.gameObject.CompareTag(waterTag))
        {
            inTheWater = true;
            water = other.gameObject;
            waterHeightLevel = other.transform.position.y;
            originalMoveSpeed = playerController.speed;
            var newPos = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
            Instantiate(impactEffect, newPos, playerController.transform.rotation); 
            
        }
    }

    public void OnActionExit(Collider other)
    {
        if (other.gameObject.CompareTag(waterTag))
        {

            if (other.gameObject == water)
            {
                water = null;
                inTheWater = false;
                isSwimming = false;
                ExitSwimState();
                if (waterDrops)
                {
                    var newPos = new Vector3(transform.position.x, transform.position.y + waterDropsYOffset, transform.position.z);
                    GameObject myWaterDrops = Instantiate(waterDrops, newPos, playerController.transform.rotation);
                    myWaterDrops.transform.parent = transform;
                }
            }

        }
    }

    protected virtual void EnterSwimState()
    {
        triggerAboveWater = false;
        triggerSwimState = true;
        OnEnterWater.Invoke();
        ResetPlayerValues();
        playerController.anim.SetBool("isSwimming", true);
        playerController.anim.CrossFadeInFixedTime("Swimming", 0.25f);
    }
    protected virtual void ExitSwimState()
    {
        if (!triggerSwimState)
        {
            return;
        }

        triggerSwimState = false;
        OnExitWater.Invoke();
        playerController.speed = originalMoveSpeed;
        playerController.anim.SetBool("isSwimming", false);
    }
    protected virtual void WaterRingEffect()
    {
        if (playerController.moving)
        {
            waterRingSpawnFrequency = waterRingFrequencySwim;
        }
        else
        {
            waterRingSpawnFrequency = waterRingFrequencyIdle;
        }

        timer += Time.deltaTime;
        if (timer >= waterRingSpawnFrequency)
        {
            var newPos = new Vector3(transform.position.x, waterHeightLevel, transform.position.z);
            Instantiate(waterRingEffect, newPos, playerController.transform.rotation);
            timer = 0f;
        }
    }

    protected virtual void ResetPlayerValues()
    {
        playerController.anim.SetFloat("Speed", 0);
    }
}
