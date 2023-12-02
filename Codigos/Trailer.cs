using UnityEngine;

public class TrailerConnector : MonoBehaviour
{
    public GameObject trailer; // Referencia al remolque
    public GameObject hitchPoint; // Punto de enganche en el tractor

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == trailer)
        {
            ConnectTrailer();
        }
    }

private void ConnectTrailer()
{
    HingeJoint joint = gameObject.AddComponent<HingeJoint>();
    joint.connectedBody = trailer.GetComponent<Rigidbody>();
    joint.anchor = hitchPoint.transform.localPosition;
    joint.axis = Vector3.up; // Ajusta esto según la orientación deseada
}

    public void DisconnectTrailer()
    {
        FixedJoint joint = GetComponent<FixedJoint>();
        if (joint != null)
        {
            Destroy(joint);
        }
    }
}
