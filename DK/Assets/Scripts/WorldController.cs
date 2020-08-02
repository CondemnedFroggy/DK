using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

[ExecuteInEditMode]
public class WorldController : MonoBehaviour
{
    public bool complete = false;

    public int Level = 1;
    public int gridX = 5;
    public int gridY = 5;
    public int gridSize = 25;
    public GameObject tilePrefab;
    public GameObject floorPrefab;
    
    private List<string> levelData;

    Vector2 grid;
       
    void Awake()
    {
        levelData = new List<string>();
        ReadLevelData(Level);

        if (!complete)
        {
            GenerateTiles();

            FillTilesWithWalls();

            complete = true;
        }
    }

    // put data from the level file into a string
    [MenuItem("Tools/Read file")]
    void ReadLevelData(int level)
    {
        string path = "Assets/LevelData/Level" + level + ".txt";

        StreamReader reader = new StreamReader(path);

        string line;
        int temp = 0;
        bool set = false;

        do
        {
            line = reader.ReadLine();

            if (line != null)
            {
                Debug.Log(line);

                if (!set)
                {
                    gridX = line.Length;

                    set = true;
                }

                levelData.Add(line);

                temp++;
            }

        } while (line != null && temp < 100);
                       
        reader.Close();

        gridSize = gridX * gridY;

        Debug.Log(gridX + gridY + gridSize);

        grid = new Vector2(gridX, gridY);
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
}