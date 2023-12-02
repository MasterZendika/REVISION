using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class TractorAgent : Agent
{
    public Transform cosechadoraTransform; // La transformación de la cosechadora
    public float moveSpeed = 5f; // Velocidad de movimiento hacia adelante y hacia atrás
    public float turnSpeed = 200f; // Velocidad de giro
    public float stopDistance = 5f; // La distancia a la cual el tractor se detendrá

    private CarController carController;
    private Rigidbody rBody; // Rigidbody del tractor
    private Vector3 startingLocalPosition; // Posición local inicial del tractor

    public override void Initialize()
    {
        rBody = GetComponent<Rigidbody>();
        carController = GetComponent<CarController>(); // Asegúrate de que CarController está en el mismo GameObject
        startingLocalPosition = transform.localPosition;
    }

    public override void OnEpisodeBegin()
    {
        // Resetear la posición y la velocidad del tractor
        rBody.velocity = Vector3.zero;
        rBody.angularVelocity = Vector3.zero;
        transform.localPosition = startingLocalPosition; // Restablecer la posición local inicial
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Observar la dirección hacia la cosechadora
        Vector3 directionToCosechadora = (cosechadoraTransform.position - transform.position).normalized;
        sensor.AddObservation(directionToCosechadora);

        // Observar la distancia a la cosechadora
        float distanceToCosechadora = Vector3.Distance(cosechadoraTransform.position, transform.position);
        sensor.AddObservation(distanceToCosechadora);

        // Observar la velocidad actual del tractor (opcional)
        sensor.AddObservation(rBody.velocity.magnitude);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Interpretar las acciones para mover el tractor
        float moveSignal = actionBuffers.ContinuousActions[0]; // Asumiendo que esta es la aceleración
        float turnSignal = actionBuffers.ContinuousActions[1]; // Asumiendo que esta es la dirección

        // Calcula la distancia a la cosechadora
        float distanceToCosechadora = Vector3.Distance(cosechadoraTransform.position, transform.position);

        // Si está demasiado cerca, no se mueve o incluso podría retroceder
        if (distanceToCosechadora < stopDistance)
        {
            moveSignal = 0; // Se detiene
            // moveSignal = -1f; // Para retroceder si es necesario
        }

        // Envía las señales al CarController
        carController.SetInputs(turnSignal, moveSignal, false); // Puedes añadir la lógica de frenado aquí si es necesario
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Implementar control manual para pruebas
        var continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Vertical"); // W y S o Flechas arriba y abajo
        continuousActions[1] = Input.GetAxis("Horizontal"); // A y D o Flechas izquierda y derecha
    }
}
