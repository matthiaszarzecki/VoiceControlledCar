using UnityEngine; 
using System.Collections; 

public class WheelJointCarMovement : MonoBehaviour { 
    
    WheelJoint2D[] wheelJoints; 
    JointMotor2D motorBack;  
    float dir = 0f; 
    float torqueDir = 0f;
    float maxFwdSpeed = -5000;
    float maxBwdSpeed = 2000f;
    float accelerationRate = 500;
    float decelerationRate = -100;
    float brakeSpeed = 2500f;
    float gravity = 9.81f;
    float slope = 0;
    public Transform rearWheel;
    public Transform frontWheel;

    void Start () { 
        wheelJoints = gameObject.GetComponents<WheelJoint2D>(); 
        motorBack = wheelJoints[0].motor; 
    }  
        
    void FixedUpdate() {
        //add ability to rotate the car around its axis
        torqueDir = Input.GetAxis("Horizontal"); 
        if(torqueDir!=0){ 
            //rigidbody2D.AddTorque(3*Mathf.PI*torqueDir, ForceMode2D.Force);
        } 
        else{
            //rigidbody2D.AddTorque(0);
        }
            
        slope = transform.localEulerAngles.z;
        if(slope>=180)
            slope = slope - 360;
        dir = Input.GetAxis("Horizontal"); 

        if (dir!=0)
            motorBack.motorSpeed = Mathf.Clamp(motorBack.motorSpeed -(dir*accelerationRate - gravity*Mathf.Sin((slope * Mathf.PI)/180)*80 )*Time.deltaTime, maxFwdSpeed, maxBwdSpeed);

        if ((dir==0 && motorBack.motorSpeed < 0 ) ||(dir==0 && motorBack.motorSpeed==0 && slope < 0)){
            motorBack.motorSpeed = Mathf.Clamp(motorBack.motorSpeed - (decelerationRate - gravity*Mathf.Sin((slope * Mathf.PI)/180)*80)*Time.deltaTime, maxFwdSpeed, 0);
        } else if ((dir==0 && motorBack.motorSpeed > 0 )||(dir==0 && motorBack.motorSpeed==0 && slope > 0)){
            motorBack.motorSpeed = Mathf.Clamp(motorBack.motorSpeed -(-decelerationRate - gravity*Mathf.Sin((slope * Mathf.PI)/180)*80)*Time.deltaTime, 0, maxBwdSpeed);
        }
            
        if (Input.GetKey(KeyCode.Space) && motorBack.motorSpeed > 0){
            motorBack.motorSpeed = Mathf.Clamp(motorBack.motorSpeed - brakeSpeed*Time.deltaTime, 0, maxBwdSpeed); 
        }
        else if(Input.GetKey(KeyCode.Space) && motorBack.motorSpeed < 0){ 
            motorBack.motorSpeed = Mathf.Clamp(motorBack.motorSpeed + brakeSpeed*Time.deltaTime, maxFwdSpeed, 0);
        }

        wheelJoints[0].motor = motorBack; 
    }
}