using UnityEngine;

public class WheelRotator : MonoBehaviour
{
    public WheelCollider frontLeftWheelCollider;
    public GameObject frontLeftWheelMesh;

    public WheelCollider frontRightWheelCollider;
    public GameObject frontRightWheelMesh;

    public WheelCollider rearLeftWheelCollider;
    public GameObject rearLeftWheelMesh;

    public WheelCollider rearRightWheelCollider;
    public GameObject rearRightWheelMesh;

    void Update()
    {
        UpdateWheel(frontLeftWheelCollider, frontLeftWheelMesh);
        UpdateWheel(frontRightWheelCollider, frontRightWheelMesh);
        UpdateWheel(rearLeftWheelCollider, rearLeftWheelMesh);
        UpdateWheel(rearRightWheelCollider, rearRightWheelMesh);
    }

    void UpdateWheel(WheelCollider collider, GameObject mesh)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        mesh.transform.position = position;
        mesh.transform.rotation = rotation;
    }
}
