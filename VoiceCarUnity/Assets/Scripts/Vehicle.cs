using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {

    public float speed = 0f;
    public float decelerationFactor = 100f;
    public float maxSpeed = 7500f;

    public SpriteRenderer[] wheelSprites;
    public GameObject[] bodies;     //antrieb - raketenantrieb, segel
    public GameObject[] engines;    //rumpf - auto, raketendüsen
    public GameObject[] tractions;  //fahrgestell - boot, räder
    private WheelJoint2D[] wheels;

    private int currentBody = 0;
    private int currentEngine = 0;
    private int currentTraction = 0;

    void Start() {
        wheels = GetComponents<WheelJoint2D>();
        SetupVehicle();
    }

    void Update() {
        speed = Mathf.Clamp (speed, 0f, maxSpeed);

        if (speed > 0f) {
            speed -= Time.deltaTime * decelerationFactor;
        }
    }
        
    public void AddSpeed(float newSpeed) {
        float newAdaptedSpeed = newSpeed * 5000;    //adapt small speed-range to usable amount
        if (newAdaptedSpeed > speed) {
            speed = newAdaptedSpeed;
        }
        SetSpeed(speed);
    }

    public void StopVehicle() {
        SetSpeed(0f);
    }

    private void SetSpeed(float newSpeed) {
        foreach (WheelJoint2D wheel in wheels) {
            JointMotor2D motor = wheel.motor;
            motor.motorSpeed = -newSpeed;   //wheels need negative speed to move right
            wheel.motor = motor;            //motor needs to be extracted and then re-assigned
        }
    }
           
    public void CycleBodies() {
        currentBody = IncreaseValueAndWraparound(currentBody, bodies.Length);
        ActivateBody(currentBody);
    }
        
    public void CycleEngines() {
        currentEngine = IncreaseValueAndWraparound(currentEngine, engines.Length);
        ActivateEngine(currentEngine);
    }
        
    public void CycleTractions() {
        currentTraction = IncreaseValueAndWraparound(currentTraction, tractions.Length);
        ActivateTraction(currentTraction);
    }

    private void SetupVehicle() {
        ActivateBody(0);
        ActivateEngine(0);
        ActivateTraction(0);
    }
        
    private void ActivateBody(int index) {
        for (int i = 0; i < bodies.Length; i++) {
            bodies[i].SetActive(i == index ? true : false);
        }
    }

    private void ActivateEngine(int index) {
        for (int i = 0; i < engines.Length; i++) {
            engines[i].SetActive(i == index ? true : false);
        }
    }

    private void ActivateTraction(int index) {
        //actual wheels in slot 0 do not get disabled, only made invisible
        for (int i = 0; i < tractions.Length; i++) {
            if (i != 0) {
                tractions[i].SetActive(i == index ? true : false);
            }
        }
        SetWheelVisibility(index == 0 ? true : false);
    }

    private void SetWheelVisibility(bool state) {
        foreach(SpriteRenderer wheel in wheelSprites) {
            wheel.enabled = state;
        }
    }

    private int IncreaseValueAndWraparound(int value, int arrayLength) {
        return value + 1 > arrayLength - 1 ? 0 : value + 1;
    }
}
