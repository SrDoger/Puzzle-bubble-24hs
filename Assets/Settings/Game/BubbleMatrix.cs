using UnityEngine;
using System.Collections.Generic;

public class BubbleMatrix : MonoBehaviour
{
    public int rows = 10; // Number of rows in the grid
    public int columns = 10; // Number of columns in the grid
    public float cellSize = 1f; // Size of each cell in the grid
    public List<GameObject> objectList = new List<GameObject>(); // List of possible objects to place in the grid
    private GameObject[,] grid; // 2D array representing the grid

    void Start()
    {
        grid = new GameObject[rows, columns]; // Initialize the grid array
        CreateGrid(); // Create the grid on start
    }

    void CreateGrid()
    {
        Vector3 gridPosition = transform.position; // Starting position of the grid

        // Iterate over the grid dimensions and instantiate objects
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                int randomObjectIndex = Random.Range(0, objectList.Count); // Randomly select an object from the list
                Vector3 position = new Vector3(column * cellSize + gridPosition.x, row * cellSize + gridPosition.y, 0); // Calculate position

                // Instantiate object at calculated position
                GameObject newObject = Instantiate(objectList[randomObjectIndex], position, Quaternion.identity);
                newObject.transform.parent = transform; // Make the new object a child of the BubbleMatrix object

                // Store the object in the grid
                grid[row, column] = newObject;
            }
        }
    }
}
