using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject rockPrefab; // Prefab de la roca
    public int numberOfRocks = 400; // Número de piedras para generar
    public float maxSize = 0.2f; // Tamaño máximo en metros (20 cm)

    void Start()
    {
        for (int i = 0; i < numberOfRocks; i++)
        {
            SpawnRock();
        }
    }

    void SpawnRock()
    {
        // Asumiendo que el terreno es plano y se extiende en el rango -50 a 50 en X y Z
        Vector3 position = new Vector3(Random.Range(0f, 200f), 0, Random.Range(0f, 200f));
        float size = Random.Range(0.05f, maxSize); // Tamaño aleatorio, mínimo 5 cm, máximo 20 cm

        GameObject rock = Instantiate(rockPrefab, position, Quaternion.identity);
        rock.transform.localScale = new Vector3(size, size, size); // Escala la piedra a un tamaño aleatorio
    }
 }