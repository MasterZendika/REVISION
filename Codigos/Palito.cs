using UnityEngine;

public class RotateMechanism : MonoBehaviour
{
    private bool isRotated = false;
    private Quaternion originalLocalRotation;
    private Quaternion targetLocalRotation;
    private bool isRotating = false;
    private float rotationSpeed = 2.0f; // Velocidad de rotación, ajustable

    void Start()
    {
        // Guarda la rotación local original
        originalLocalRotation = transform.localRotation;
        // Calcula la rotación local objetivo (90 grados en el eje local que elijas)
        targetLocalRotation = Quaternion.Euler(0, 90, 0) * originalLocalRotation; // Ajusta los valores Euler según el eje de rotación deseado
    }

    void Update()
    {
        // Detecta cuando se presiona el botón
        if (Input.GetKeyDown(KeyCode.P)) // Reemplaza 'YourButton' con la tecla que quieras
        {
            isRotating = true;
        }

        if (isRotating)
        {
            PerformRotation();
        }
    }

    private void PerformRotation()
    {
        // Determina la rotación objetivo actual basada en si el objeto ya está rotado o no
        Quaternion currentTarget = isRotated ? originalLocalRotation : targetLocalRotation;

        // Rota el objeto suavemente hacia la rotación objetivo en el espacio local
        transform.localRotation = Quaternion.Slerp(transform.localRotation, currentTarget, Time.deltaTime * rotationSpeed);

        // Verifica si el objeto ha alcanzado la rotación objetivo
        if (Quaternion.Angle(transform.localRotation, currentTarget) < 0.5f)
        {
            transform.localRotation = currentTarget; // Asegúrate de que la rotación se ajuste exactamente al objetivo
            isRotating = false;
            isRotated = !isRotated;
        }
    }
}
