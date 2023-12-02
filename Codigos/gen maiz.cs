using UnityEngine;

public class CornFieldGenerator : MonoBehaviour
{
    public GameObject cornPrefab; // Prefab de la planta de maíz
    public float spacing = 1.0f; // Espaciado entre plantas

    private void Start()
    {
        // Calcular filas y columnas para ajustarse a un área de 200x200
        int rows = Mathf.FloorToInt(200 / spacing);
        int columns = Mathf.FloorToInt(200 / spacing);

        GenerateField(rows, columns);
    }

    private void GenerateField(int rows, int columns)
    {
        Vector3 startPosition = new Vector3(0, -1, 0); // Punto de inicio en (0, 0)

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 position = new Vector3(startPosition.x + i * spacing, startPosition.y, startPosition.z + j * spacing);
                Instantiate(cornPrefab, position, Quaternion.identity);
            }
        }
    }
}
