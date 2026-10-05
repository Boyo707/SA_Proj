using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private Tilemap pathFindingMap;
    [SerializeField] private float speed = 5f;
    [SerializeField] private PathGraphGenerator pathGraphGenerator;

    public float MoveSpeed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    private Graph<Vector3> graph = new();

    private Vector3 startNode;
    private Vector3 endNode;

    private List<Vector3> path = new List<Vector3>();
    HashSet<Vector3> discovered = new HashSet<Vector3>();

    private bool isMoving = false;
    private Vector3 moveDir = Vector3.zero;
    private Vector3 currentDestination = Vector3.zero;

    private void Start()
    {
        graph = pathGraphGenerator.DungeonGraph;
    }

    public List<Vector3> CalculatePath(Vector3 from, Vector3 to)
    {
        Vector3 playerPosition = from;

        startNode = GetClosestNodeToPosition(playerPosition);

        endNode = GetClosestNodeToPosition(to);

        path = AStar(startNode, endNode);
        return path;
    }
    private Vector3 GetClosestNodeToPosition(Vector3 position)
    {
        
        //Lowers the position to 0 for more accurate calculations
        
        List<Vector3> nodes = graph.GetNodes();

        for (int i = 0; i < nodes.Count; i++)
        {
            if (Vector3.Distance(nodes[i], position) <= 0.6f)
            {
                return nodes[i];
            }
        }

        //if it has not found one then go back to the starting position
        return startNode;
    }

    List<Vector3> AStar(Vector3 start, Vector3 end)
    {
        discovered.Clear();

        Vector3 v = start;

        List<(Vector3 node, float priority)> priorityQueue = new List<(Vector3, float)>();

        Dictionary<Vector3, float> cost = new();

        Dictionary<Vector3, Vector3> parent = new();

        priorityQueue.Add((v, 0));
        discovered.Add(v);

        cost[v] = 0;

        while (priorityQueue.Count > 0)
        {
            //sorts the Queue by priority
            priorityQueue = priorityQueue.OrderByDescending(node => node.priority).ToList();
            
            v = priorityQueue[priorityQueue.Count - 1].node;
            priorityQueue.RemoveAt(priorityQueue.Count - 1);
            discovered.Add(v);

            //if the current node is the end node then construct the path towards the node.
            if (v == end)
            {
                return ReconstructPath(parent, start, end);
            }

            foreach (Vector3 w in graph.GetNeighbors(v))
            {
                float newCost = cost[v] + Cost(v, w);

                if (!cost.ContainsKey(w) || newCost < cost[w])
                {
                    cost[w] = newCost;

                    parent[w] = v;

                    priorityQueue.Add((w, newCost + Heuristic(w, end)));
                }
            }
        }

        return new List<Vector3>();
    }

    public float Cost(Vector3 from, Vector3 to)
    {
        return Vector3.Distance(from, to);
    }
    public float Heuristic(Vector3 from, Vector3 to)
    {
        return Vector3.Distance(from, to);
    }
    List<Vector3> ReconstructPath(Dictionary<Vector3, Vector3> parentMap, Vector3 start, Vector3 end)
    {
        List<Vector3> path = new List<Vector3>();

        Vector3 currentNode = end;

        while (currentNode != start)
        {
            path.Add(currentNode);
            currentNode = parentMap[currentNode];
        }

        path.Add(start);

        //reverse the path so it goes from the start to the end and not the reverse.
        path.Reverse();
        return path;
    }


    public void GoToDestination(Transform destinationTrans)
    {
        //stops any coroutines before it.

        if (!isMoving && !HasReachedDestination(destinationTrans.position))
        {
            StartCoroutine(FollowPathCoroutine(CalculatePath(transform.position, destinationTrans.position), destinationTrans));
        }
    }

    public void StopPathFinding()
    {
        StopAllCoroutines();
        isMoving = false;
    }

    public bool HasReachedDestination(Vector3 destination)
    {
        return GetClosestNodeToPosition(destination) == transform.position;
    }

    public Vector3 MoveDirection()
    {
        return moveDir;
    }

    IEnumerator FollowPathCoroutine(List<Vector3> path, Transform destinationTrans)
    {
        isMoving = true;



        //if the path is empty or it does not have anything in the list then do not create the path
        Debug.Log("Following");
        if (path == null || path.Count == 0)
        {
            Debug.Log("No path found");
            yield break;
        }
        for (int i = 0; i < path.Count; i++)
        {
            //pushes the target up 1 to compensate player size
            Vector3 target = path[i];
            moveDir = target - transform.position;

            while (Vector3.Distance(transform.position, target) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
                yield return null;
            }
            TrackTarget(destinationTrans);
        }
        transform.position = path.Last();
        isMoving = false;
    }

    private void TrackTarget(Transform target)
    {
        Vector3 newDestination = pathFindingMap.GetCellCenterWorld(pathFindingMap.WorldToCell(target.position));
        if (currentDestination != newDestination)
        {
            currentDestination = newDestination;
            StopPathFinding();
            GoToDestination(target);
        }
    }
}
