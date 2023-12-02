using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnGrid : MonoBehaviour
{
    private GridManager gridManager;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>(); // Encuentra el GridManager en la escena
    }

    void Update()
    {
        Vector2Int gridIndex = gridManager.GetGridIndex(transform.position);
        if (gridManager.IsWithinGrid(gridIndex))
        {
            // El objeto está dentro de los límites de la cuadrícula
            Debug.Log($"El objeto está en la celda de la cuadrícula: {gridIndex}");
        }
    }
}
