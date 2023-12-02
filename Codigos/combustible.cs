using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con la UI de Unity

public class FuelSystem : MonoBehaviour
{
     [SerializeField]
    private CornHarvester harvester; // Asegúrate de que esta línea está presente
    [SerializeField]
    private Text fuelLevelText; // Referencia al texto de UI que muestra el nivel de combustible

    [SerializeField]
    private float fuelLevel = 100f; // Nivel de combustible inicial
    [SerializeField]
    

      private Vector3 lastPosition; // Para almacenar la última posición

          private bool IsMoving()
    {
        // Comprueba si la posición actual es diferente de la última posición
        return transform.position != lastPosition;
    }

    private void Start()
    {
        // Asegúrate de que la referencia al Text no sea nula
        if (fuelLevelText == null)
        {
            Debug.LogError("No se ha asignado el componente Text al sistema de combustible.");
        }
          lastPosition = transform.position; // Inicializa la última posición
    }

    private void Update()
    {
        // Verifica si hay movimiento
        if (IsMoving())
        {
            AdjustFuelConsumption();
        }
        UpdateFuelLevelUI();
        lastPosition = transform.position; // Actualiza la última posición
    }
private void AdjustFuelConsumption()
{
    // Calcular la distancia recorrida desde el último frame
    float distanceMoved = Vector3.Distance(transform.position, lastPosition);

    // Consumo de combustible por unidad de distancia
    float fuelConsumptionPerUnitDistance = 0.01f; // Ajusta este valor según sea necesario

    // Calcular el consumo de combustible basado en la distancia
    float consumption = distanceMoved * fuelConsumptionPerUnitDistance;

    // Actualizar el nivel de combustible
    fuelLevel -= consumption;

    // Comprobar si el combustible se ha agotado
    if (fuelLevel <= 0)
    {
        Debug.Log("Se ha agotado el combustible.");
        // Lógica adicional cuando se acaba el combustible
    }

    // Actualizar la última posición
    lastPosition = transform.position;
}

       private void UpdateFuelLevelUI()
    {
        // Asegúrate de que el texto de UI está asignado antes de intentar actualizarlo
        if (fuelLevelText != null)
        {
            fuelLevelText.text = $"Fuel Level: {fuelLevel:F2}"; // Actualiza el texto de UI con el nivel de combustible, formateado a dos decimales
        }
    }
}

