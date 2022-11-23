using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using StarterAssets;

public class GettingInOut : MonoBehaviour
{
    [SerializeField] GameObject human;
    [SerializeField] GameObject car;
    [SerializeField] CarController carEngine;
    [SerializeField] CarUserControl carController;
    [SerializeField] Collider waterCol1;
    [SerializeField] Collider waterCol2;
    [SerializeField] Collider waterCol3;
    [SerializeField] Collider waterCol4;
    public ThirdPersonController thirdPersonController;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
		{
            if (thirdPersonController.onCar) GetOutOfCar();
            else if(Vector3.Distance(car.transform.position, human.transform.position) < 3f)
                GetIntoCar();
        }

    }
    private void GetIntoCar()
    {
        carEngine.enabled = true;
        carController.enabled = true;
        thirdPersonController.onCar = true;
		thirdPersonController.carFollowCamera.SetActive(true);
		thirdPersonController.playerFollowCamera.SetActive(false) ;
        human.SetActive(false);
        waterCol1.isTrigger = true;
        waterCol2.isTrigger = true;
        waterCol3.isTrigger = true;
        waterCol4.isTrigger = true;
    }
    private void GetOutOfCar()
    {
        human.SetActive(true);
        thirdPersonController.playerFollowCamera.SetActive(true);
        thirdPersonController.onCar = false;
        human.transform.position = car.transform.position + car.transform.TransformDirection(Vector3.left);
        carEngine.Move(0, 0, 1, 1);
        carController.enabled = false;
        thirdPersonController.carFollowCamera.SetActive(false);
        waterCol1.isTrigger = false;
        waterCol2.isTrigger = false;
        waterCol3.isTrigger = false;
        waterCol4.isTrigger = false;
    }

}
