using UnityEngine;

[ExecuteInEditMode]
public class WorldController : MonoBehaviour
{
    public bool complete = false;

    public int gridX = 5;
    public int gridY = 5;
    public int gridSize = 25;
    public GameObject tilePrefab;
    public GameObject floorPrefab;

    Vector2 grid;
       
    void Awake()
    {
        gridSize = gridX * gridY;

        grid = new Vector2(gridX, gridY);

        if (!complete)
        {
            GenerateTiles();

            FillTilesWithWalls();

            complete = true;
        }
    }

    void GenerateTiles()
    {
        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                GameObject clone = Instantiate(tilePrefab, new Vector3(x * 10.0f, 0.0f, y * 10.0f), Quaternion.identity, transform);
                clone.name = "Tile (" + x + ", " + y + ")";
            }
        }
    }

    void FillTilesWithWalls()
    {
        foreach (Transform gridTile in transform)
        {
            Transform floorTile = gridTile.transform.Find("FloorTile");
            
            GameObject floor = Instantiate(floorPrefab, floorTile.position, Quaternion.identity, floorTile);
        }
    }

    void Update()
    {

    }
}
