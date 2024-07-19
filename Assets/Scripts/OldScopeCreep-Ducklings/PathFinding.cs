using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinding : MonoBehaviour
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public Tilemap tilemap;
    public Tilemap obstacles;
    public Tilemap prev;

    List<PathNode> openList; //= new List<PathNode>();
    List<PathNode> closedList; //= new List<PathNode>();

    public Tile tilePrev;

    Dictionary<Vector3Int, PathNode> allNodes = new Dictionary<Vector3Int, PathNode>();

    private Vector3 mousePos;
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(mousePos, 1);
    }

    private void Start()
    {
        tilemap.CompressBounds();
        Debug.Log(tilemap.size);
        Debug.Log(tilemap.origin);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveBaby.ResetPath();
            mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3Int mouseTilePos = tilemap.WorldToCell(worldPosition);// - new Vector3Int(tilemap.origin.x, tilemap.origin.y,0);
            mousePos = worldPosition;

            //prev.SetTile(mouseTilePos, tilePrev);
            Debug.Log(mouseTilePos);
            Debug.DrawLine(mousePos, new Vector3(0, 0, 0), Color.white, 2.5f);

           //List<PathNode> path =  FindPath(new Vector3Int(5,5,0),mouseTilePos);
           List<PathNode> path =  FindPath( tilemap.WorldToCell(MoveBaby.GetBabyPos()),mouseTilePos);

            if (path != null)
            {
                List<Vector3> pathForBaby = new List<Vector3>();
                for (int i = 0; i < path.Count -1; i++)
                {
                    pathForBaby.Add(tilemap.CellToWorld(path[i + 1].pos) + (new Vector3(tilemap.cellSize.x / 2, tilemap.cellSize.y / 2, 0)));
                    Debug.DrawLine(tilemap.CellToWorld(path[i].pos), tilemap.CellToWorld(path[i+1].pos), Color.green, 10);
                }
                MoveBaby.path = pathForBaby;
            }else
            {
                Debug.Log("No possible path!");
            }
        }
    }

    public List<PathNode> FindPath(Vector3Int startPos, Vector3Int endPos)
    {
        Debug.Log("startPos: " + startPos + ", endPos" + endPos);
        PathNode startNode = new PathNode() { pos = startPos };
        PathNode endNode = new PathNode() { pos = endPos };

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();
        allNodes = new Dictionary<Vector3Int, PathNode>();

       // for (int x = 0; x < tilemap.size.x; x++)
        for (int x = tilemap.origin.x; x < (tilemap.origin.x + tilemap.size.x); x++)
         //   for (int y = 0; y < tilemap.size.y; y++)
            for (int y = tilemap.origin.y; y < (tilemap.size.y + tilemap.origin.y); y++)
            {
                Vector3Int newPos = new Vector3Int(x, y, 0);
                PathNode pathNode = new PathNode() {pos = newPos};
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;

                //Debug.Log(obstacles.GetTile(newPos)); 
                pathNode.isWalkable = (obstacles.GetTile(newPos) == null);

                if(!allNodes.ContainsKey(newPos))
                    allNodes.Add(newPos, pathNode);
            }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while(openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if(currentNode.pos == endNode.pos)
            {
                //Reached final node
                return CalculatePath(allNodes[endNode.pos]);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeightbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;
                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if(tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        return null;
    }

    //Return a list of all possible neighbours around node (doesn't care if they are blocked at the moment)
    private List<PathNode> GetNeightbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();
        if (currentNode.pos.x - 1 >= (0 + tilemap.origin.x))
        {
            //Left
            neighbourList.Add(GetNode(new Vector3Int(currentNode.pos.x - 1, currentNode.pos.y, 0)));
            //Left Down
            if (currentNode.pos.y - 1 >= (0 + tilemap.origin.y) && IsTileWalkable(currentNode.pos - new Vector3Int(1, 0, 0)) && IsTileWalkable(currentNode.pos - new Vector3Int(0, 1, 0))) neighbourList.Add(GetNode(new Vector3Int(currentNode.pos.x - 1, currentNode.pos.y - 1, 0)));
            //Left up
            if (currentNode.pos.y + 1 < (tilemap.size.y + tilemap.origin.y) && IsTileWalkable(currentNode.pos-new Vector3Int(1,0,0)) && IsTileWalkable(currentNode.pos + new Vector3Int(0, 1, 0))) neighbourList.Add(GetNode(new Vector3Int(currentNode.pos.x - 1, currentNode.pos.y + 1, 0)));
        }
        if (currentNode.pos.x + 1 < (tilemap.size.x + tilemap.origin.x))
        {
            //Right
            neighbourList.Add(GetNode(new Vector3Int(currentNode.pos.x + 1, currentNode.pos.y, 0)));
            //Right Down
            if (currentNode.pos.y - 1 >= (0 + tilemap.origin.y) && IsTileWalkable(currentNode.pos + new Vector3Int(1, 0, 0)) && IsTileWalkable(currentNode.pos - new Vector3Int(0, 1, 0))) neighbourList.Add(GetNode(new Vector3Int(currentNode.pos.x + 1, currentNode.pos.y - 1, 0)));
            //Right Up
            if (currentNode.pos.y + 1 < (tilemap.size.y + tilemap.origin.y) && IsTileWalkable(currentNode.pos + new Vector3Int(1, 0, 0)) && IsTileWalkable(currentNode.pos + new Vector3Int(0, 1, 0))) neighbourList.Add(GetNode(new Vector3Int(currentNode.pos.x + 1, currentNode.pos.y + 1, 0)));
        }
        //Down
        if (currentNode.pos.y - 1 >= (0 + tilemap.origin.y)) neighbourList.Add(GetNode(new Vector3Int(currentNode.pos.x, currentNode.pos.y - 1, 0)));
        //Up
        if (currentNode.pos.y + 1 < (tilemap.size.y + tilemap.origin.y)) neighbourList.Add(GetNode(new Vector3Int(currentNode.pos.x, currentNode.pos.y + 1, 0)));

        return neighbourList;
    }

    private bool IsTileWalkable(Vector3Int pos)
    {
        return obstacles.GetTile(pos) == null;
    }

    private PathNode GetNode(Vector3Int pos)
    {
        return allNodes[pos];
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();

        path.Add(endNode);
        PathNode currentNode = endNode;

        while(currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.pos.x - b.pos.x);
        int yDistance = Mathf.Abs(a.pos.y - b.pos.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];

        for (int i = 0; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost< lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }

    public class PathNode
    {
        public Vector3Int pos;

        public int gCost = 0; //walking cost from start node
        public int hCost = 0; //heuristic cost to reach end node
        public int fCost = 0; //G + H

        public PathNode cameFromNode = null;

        public bool isWalkable = true;

        internal void CalculateFCost()
        {
            fCost = gCost + hCost;
        }
    }

}







//tilemap.CellToWorld
//prev.SetTile(path[i].pos, tilePrev);
//Debug.Log("pos is: " + path[i].pos);
//Debug.DrawLine(new Vector3(path[i].pos.x, path[i].pos.y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].pos.x, path[i+1].pos.y) * 10f + Vector3.one * 5f, Color.green, 10);





/*
public float accelerationTime;
public float maxSpeed;
private Vector2 movement;
private float timeLeft;
public Rigidbody2D rb;

void Update()
{
    timeLeft -= Time.deltaTime;
    if (timeLeft <= 0)
    {
        movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        timeLeft += accelerationTime;
    }
}

void FixedUpdate()
{
    rb.AddForce(movement * maxSpeed);
}*/