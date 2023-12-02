using UnityEngine;

public class CornStalkBend : MonoBehaviour
{
    public GameObject stalkPrefab;
    public float bendSpeed = 1.0f;
    private Quaternion originalRotation;
    private bool isBending = false;

    void Start()
    {
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (!isBending)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * bendSpeed);
        }
        isBending = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Cosechadora"))
        {
            // Obtén la referencia a la cosechadora que está interactuando con la planta
            CornHarvester harvester = other.GetComponent<CornHarvester>();
            if (harvester != null)
            {
                harvester.HarvestPlant(GetComponent<CornPlant>());
                HarvestPlant();
            }
        }
        else
        {
            BendTowardsObject(other);
        }
    }

    void BendTowardsObject(Collider other)
    {
        Vector3 direction = other.transform.position - transform.position;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * bendSpeed);
        isBending = true;
    }

    void HarvestPlant()
    {
        // Instancia el prefab del tallo en la misma posición y rotación que la planta de maíz
        if (stalkPrefab != null)
        {
            Instantiate(stalkPrefab, transform.position, transform.rotation);
            Debug.Log("Mazorca cosechada");
        }
        else
        {
            Debug.Log("Prefab del tallo no asignado");
        }

        // Destruye la planta de maíz
        Destroy(gameObject);
    }
}
