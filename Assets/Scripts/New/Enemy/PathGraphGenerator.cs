using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathGraphGenerator : MonoBehaviour
{
    [SerializeField] private Tilemap pathFindingMap;

    private RectInt dungeonSize;
    private List<Vector3> activeTiles = new();
    private Graph<Vector3> graph = new();

    public Graph<Vector3> DungeonGraph => graph;

    private void Start()
    {
        GetActiveTiles();
        GenerateGraph();
    }

    private void GetActiveTiles()
    {
        pathFindingMap.CompressBounds();

        BoundsInt bounds = pathFindingMap.cellBounds;

        dungeonSize = new RectInt(bounds.x, bounds.y, bounds.size.x, bounds.size.y);

        TileBase[] allTiles = pathFindingMap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Vector3 worldPos = pathFindingMap.GetCellCenterWorld(new Vector3Int(x + bounds.x, y + bounds.y, 0));
                    activeTiles.Add(worldPos);
                }
            }
        }
    }

    #region GeneratingGraph
    void GenerateGraph()
    {
        graph.Clear();

        for (int x = dungeonSize.xMin; x < dungeonSize.xMax; x++)
        {
            for (int y = dungeonSize.yMin; y < dungeonSize.yMax; y++)
            {
                //looks at the currentposition with some offset
                Vector3 currentPos = new Vector3(x + .5f, y + .5f, 0);

                //Straight Direction
                TryConnectNeighbor(x + 1, y, currentPos);    //Right
                TryConnectNeighbor(x - 1, y, currentPos);    //left
                TryConnectNeighbor(x, y + 1, currentPos);    //Up
                TryConnectNeighbor(x, y - 1, currentPos);    //Down

                //Diagonal Directions
                TryConnectNeighbor(x + 1, y + 1, currentPos); //Top right
                TryConnectNeighbor(x - 1, y + 1, currentPos); //Top left
                TryConnectNeighbor(x + 1, y - 1, currentPos); //Bottm right
                TryConnectNeighbor(x - 1, y - 1, currentPos); //Bottom left
            }
        }
        Debug.Log("Finished creating player walkable Path");
    }
    private void TryConnectNeighbor(int nx, int ny, Vector3 currentPos)
    {
        //if the x and y posiition si withing the dungeonsize
        if (nx >= dungeonSize.xMin && nx < dungeonSize.xMax &&
            ny >= dungeonSize.yMin && ny < dungeonSize.yMax)
        {
            Vector3 neighborPos = new Vector3(nx + .5f, ny + .5f, 0);
            //Debug.Log("CHECKING" + currentPos + " :  " + neighborPos);
            //checks if this potential position and the currentposition is found inside the floors position
            if (activeTiles.Contains(neighborPos) && activeTiles.Contains(currentPos))
            {
                //Adds an edge between the currentposition and the newly found neighbour position
                graph.AddEdge(currentPos, neighborPos);
            }
        }
    }
    #endregion
}
