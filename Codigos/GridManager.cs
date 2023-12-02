using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int gridSize = 10; // 10x10 grid for a 200x200 plane
    public float cellSize = 20.0f; // Each cell is 20x20 units

    // Converts world position to grid index
    public Vector2Int GetGridIndex(Vector3 worldPosition)
    {
        int xIndex = Mathf.FloorToInt(worldPosition.x / cellSize);
        int zIndex = Mathf.FloorToInt(worldPosition.z / cellSize);
        return new Vector2Int(xIndex, zIndex);
    }

    // Check if the given index is within the grid bounds
    public bool IsWithinGrid(Vector2Int gridIndex)
    {
        return gridIndex.x >= 0 && gridIndex.x < gridSize &&
               gridIndex.y >= 0 && gridIndex.y < gridSize;
    }

    // Get world position for the center of a cell given its grid index
    public Vector3 GetCellCenterWorldPosition(Vector2Int gridIndex)
    {
        return new Vector3(gridIndex.x * cellSize + cellSize / 2,
                           0, // Assuming the plane is at y = 0
                           gridIndex.y * cellSize + cellSize / 2);
    }
}
