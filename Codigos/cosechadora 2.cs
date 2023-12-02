using System;
using UnityEngine;

public class CosechadoraController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle;

    // Settings
    [SerializeField] private float motorForce, maxSteerAngle;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Axle Transforms
    [SerializeField] private Transform frontAxleTransform;
    [SerializeField] private Transform rearAxleTransform;

    private void FixedUpdate() {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateAxles();
    }

    private void GetInput() {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        verticalInput = Input.GetAxis("Vertical");
    }

    private void HandleMotor() {
        // Apply motor force to the rear axle for movement
        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;
    }

    private void HandleSteering() {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateAxles() {
        UpdateSingleAxle(frontLeftWheelCollider, frontAxleTransform);
        UpdateSingleAxle(frontRightWheelCollider, frontAxleTransform);
        UpdateSingleAxle(rearLeftWheelCollider, rearAxleTransform);
        UpdateSingleAxle(rearRightWheelCollider, rearAxleTransform);
    }

    private void UpdateSingleAxle(WheelCollider wheelCollider, Transform axleTransform) {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        axleTransform.rotation = rot;
        // Aquí, ajusta solo la rotación del eje, la posición se mantiene constante
    }
}
