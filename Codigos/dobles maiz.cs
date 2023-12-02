using UnityEngine;

public class CornStalk : MonoBehaviour
{
    public float bendAngle = 30f; // Ángulo máximo al que se doblará la planta
    public float bendSpeed = 5f; // Velocidad a la que se dobla
    public float recoveryTime = 2f; // Tiempo antes de "romperse" o ser cosechada

    private Quaternion originalRotation;
    private Quaternion targetRotation;
    private bool isBending = false;

    void Start()
    {
        originalRotation = transform.rotation;
        targetRotation = originalRotation;
    }

    void Update()
    {
        if (isBending)
        {
            // Suavemente dobla la planta hacia la rotación objetivo
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * bendSpeed);
        }
        else
        {
            // Regresar a la rotación original si no está doblada
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * bendSpeed);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 direction = collision.transform.position - transform.position;
            direction.y = 0; // Ignorar la componente Y para rotación en el plano XZ
            targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 0, bendAngle);
            isBending = true;

            // Esperar un tiempo antes de cosechar
            Invoke("Harvest", recoveryTime);
        }
    }

    private void Harvest()
    {
        isBending = false; // Detener la inclinación
        // Aquí la lógica para "romper" o cosechar la planta
        Debug.Log("Mazorca cosechada!");
    }
}
