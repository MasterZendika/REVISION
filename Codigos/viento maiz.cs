using UnityEngine;

public class WindEffect : MonoBehaviour
{
    public float maxAngle = 10f; // Máximo ángulo de inclinación
    public float speed = 2f; // Velocidad del movimiento

    private float angle; // Ángulo actual de inclinación
    private float originalY; // Posición Y original para la rotación

    void Start()
    {
        originalY = transform.localEulerAngles.y;
    }

    void Update()
    {
        // Simular el movimiento del viento
        angle = maxAngle * Mathf.Sin(Time.time * speed);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, originalY, angle);
    }
}
