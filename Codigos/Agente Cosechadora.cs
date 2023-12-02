using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Collections.Generic;

public class CornHarvesterAgent : Agent
{
    Rigidbody rBody;
    public float moveSpeed = 5f;
    public float turnSpeed = 200f;
    public Vector3 startPosition = new Vector3(10f, 3.0f, -10f);
    private HashSet<Vector2Int> visitedCells;
    private GridManager gridManager;

    [SerializeField] private Transform targetTransform;
    private HarvesterSteering HarvesterSteering;

    public override void Initialize()
    {
        rBody = GetComponent<Rigidbody>();
        HarvesterSteering = GetComponent<HarvesterSteering>();
        gridManager = FindObjectOfType<GridManager>();
        visitedCells = new HashSet<Vector2Int>();
    }

    public override void OnEpisodeBegin()
    {
        transform.position = startPosition;
        rBody.angularVelocity = Vector3.zero;
        rBody.velocity = Vector3.zero;
        visitedCells.Clear();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform.position);
    }

    private Vector3 CalculateDesiredDirection()
    {
        if (DeberíaGirar())
        {
            Vector3 currentDirection = transform.forward;
            Vector3 newDirection = Quaternion.Euler(0, 90, 0) * currentDirection;
            return newDirection.normalized;
        }
        else
        {
            return transform.forward;
        }
    }

    private bool DeberíaGirar()
    {
        float boundaryX = 190f; // Ajusta según el tamaño de tu cuadrícula
        float boundaryZ = 190f; // Ajusta según el tamaño de tu cuadrícula
        return Mathf.Abs(transform.position.x) > boundaryX || Mathf.Abs(transform.position.z) > boundaryZ;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float forwardAmount = actions.DiscreteActions[0] == 1 ? 1f : actions.DiscreteActions[0] == 2 ? -1f : 0f;
        float turnAmount = actions.DiscreteActions[1] == 1 ? 1f : actions.DiscreteActions[1] == 2 ? -1f : 0f;

        Vector3 desiredDirection = CalculateDesiredDirection();
        if (Vector3.Dot(transform.forward, desiredDirection) > 0.9)
        {
            AddReward(0.5f);
        }

        HarvesterSteering.SetInput(forwardAmount, turnAmount);

        Vector2Int currentCell = gridManager.GetGridIndex(transform.position);
        if (!visitedCells.Add(currentCell))
        {
            AddReward(-10.0f);
        }
        else
        {
            AddReward(100.0f);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int forwardAction = 0;
        if (Input.GetKey(KeyCode.UpArrow)) forwardAction = 1;
        if (Input.GetKey(KeyCode.DownArrow)) forwardAction = 2;

        int turnAction = 0;
        if (Input.GetKey(KeyCode.RightArrow)) turnAction = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) turnAction = 2;

        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = forwardAction;
        discreteActions[1] = turnAction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Valla"))
        {
            AddReward(-10.0f);
            EndEpisode();
        }
        else if (collision.gameObject.CompareTag("Maiz"))
        {
            AddReward(0.1f);
        }
    }
}
