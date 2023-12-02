using UnityEngine;

public class HarvesterSteering : MonoBehaviour
{
    public float motorForce = 1000f;
    public float maxSteerAngle = 45f;
    public float maxSpeed = 20f;

    private Rigidbody rb;

    public WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider, rearRightWheelCollider;
    public Transform frontLeftWheelTransform, frontRightWheelTransform;
    public Transform rearLeftWheelTransform, rearRightWheelTransform;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.9f, 0); // Ajustar el centro de masa para mejorar la estabilidad
    }

    // Este método ahora toma entradas del script del agente
    public void SetInput(float forwardAmount, float turnAmount)
    {
        float motor = forwardAmount * motorForce;
        float steering = turnAmount * maxSteerAngle;

        frontLeftWheelCollider.motorTorque = motor;
        frontRightWheelCollider.motorTorque = motor;
        rearLeftWheelCollider.motorTorque = motor;
        rearRightWheelCollider.motorTorque = motor;

        frontLeftWheelCollider.steerAngle = steering;
        frontRightWheelCollider.steerAngle = steering;
        rearLeftWheelCollider.steerAngle = -steering / 2; // Dirección opuesta para mejor maniobrabilidad
        rearRightWheelCollider.steerAngle = -steering / 2;

        UpdateWheels();
    }

    private void UpdateWheels()
    {
        UpdateWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheel(WheelCollider collider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}
