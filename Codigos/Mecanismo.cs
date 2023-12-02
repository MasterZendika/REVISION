using UnityEngine;

public class HarvesterMechanism : MonoBehaviour
{
    public float rotationSpeed = 90.0f; // Grados por segundo

    void Update()
    {
        // Rotar el GameObject alrededor de su propio eje X
        transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0, Space.Self);
    }
}
