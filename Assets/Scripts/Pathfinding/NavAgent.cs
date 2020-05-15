using com.leothelegion.Serializables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace com.leothelegion.Nav
{
    public class NavAgent: MonoBehaviour
    {
        [SerializeField]
        bool useSharedPathing = true;
        [SerializeField]
        bool useCheapPathfindingWhenPossible = true;

        static NodeComparer nodeSorter = new NodeComparer();

        Queue<Node> nodePool = new Queue<Node>();

        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        List<Node> path_node = new List<Node>();

        static Dictionary<(Vector2Int, Vector2Int), List<Vector2>> calculatedPaths = new Dictionary<(Vector2Int, Vector2Int), List<Vector2>>();

        public List<Vector2> FindPath(Vector2Int start, Vector2Int goal)
        {
            if (useSharedPathing)
            {
                if (calculatedPaths.ContainsKey((start, goal)))
                {
                    return calculatedPaths[(start, goal)];
                }
            }

            if (!NavMap.GetPoints().ContainsKey(start))
                return new List<Vector2>();

            if (!NavMap.GetPoints().ContainsKey(goal))
                return new List<Vector2>();

            if (useCheapPathfindingWhenPossible)
            {
                var cheapPath = UseCheapSearch(start, goal);
                if (cheapPath != null)
                    if (cheapPath.Count > 0)
                        return cheapPath;
            }

            var AStarpath =  AStarSearch(start, goal);

            if (useSharedPathing)
                calculatedPaths.Add((start, goal), AStarpath);

            return AStarpath;
        }

        private static List<Vector2> UseCheapSearch(Vector2Int start, Vector2Int goal)
        {
            bool foundCheap = true;

            Vector2Int dir = goal - start;

            float rad = Mathf.Atan2(dir.y, dir.x);

            for (int i = 0; i < dir.magnitude; i++)
            {
                int xa = (int)(i * Mathf.Cos(rad));
                int ya = (int)(i * Mathf.Sin(rad));

                Vector2Int vec = new Vector2Int(xa, ya) + start;

                if (!NavMap.GetPoints()[vec])
                {
                    foundCheap = false;
                    return null;
                }
            }

            if (foundCheap)
            {
                return new List<Vector2>() { goal };
            }
            else
                return null;
        }

        private List<Vector2> AStarSearch(Vector2Int start, Vector2Int goal)
        {
            List<Vector2> path = new List<Vector2>();

            Node current = CreateNewNode(start, null, 0, getDistance(start, goal));//new Node(start, null, 0, getDistance(start, goal));
            openList.Add(current);

            //Debug.Log (start + "awdd"+ goal);
            while (openList.Count > 0)
            {
                openList.Sort(nodeSorter);
                current = openList[0];
                if (current.pos.Equals(goal))
                {
                    //print(openList.Count + closedList.Count - recycle);

                    while (current.parent != null)
                    {
                        path_node.Add(current);
                        current = current.parent;
                    }
                    BackupToPool(openList);
                    BackupToPool(closedList);

                    openList.Clear();
                    closedList.Clear();

                    path.Clear();
                    foreach (var node in path_node)
                    {
                        path.Add(node.pos);
                    }

                    path_node.Clear();
                    path.Reverse();

                    return path;
                }
                openList.Remove(current);
                closedList.Add(current);
                for (int i = 0; i < 9; i++)
                {
                    if (i == 4)
                        continue;
                    int x = (int)current.pos.x;
                    int y = (int)current.pos.y;
                    int xi = (i % 3) - 1;
                    int yi = (i / 3) - 1;
                    //filter start

                    //if(xi == 1 && yi == 1)
                    //  continue;

                    //if (xi == -1 && yi == -1)
                    //   continue;

                    //if (xi == 1 && yi == -1)
                    //  continue;

                    //if (xi == -1 && yi == 1)
                    // continue;

                    Vector2Int pointinQuestion = new Vector2Int(x + xi, y + yi);

                    if (!NavMap.GetPoints().ContainsKey(pointinQuestion))//costs too much
                        continue;

                    bool at = NavMap.GetPoints()[new Vector2Int(x + xi, y + yi)];

                    if (at == false)
                        continue;


                    //filter end
                    Vector2 a = new Vector2(x + xi, y + yi);
                    double gCost = current.gCost + (getDistance(current.pos, a) == 1 ? 1 : 0.95);
                    double hCost = getDistance(a, goal);
                    Node node = CreateNewNode(a, current, gCost, hCost);//new Node(a, current, gCost, hCost);
                    if (vecInList(closedList, a) && gCost >= node.gCost)
                    {
                        continue;
                    }

                    if (!vecInList(openList, a) || gCost < node.gCost)
                    {
                        openList.Add(node);
                    }

                }
            }
            BackupToPool(closedList);
            closedList.Clear();
            return null;
        }

        /// 
        /// Utils
        /// 

        Node CreateNewNode(Vector2 pos, Node parent, double gCost, double hCost)
        {
            if (nodePool.Count > 0)
            {
                Node recycleNode = nodePool.Dequeue();
                recycleNode.pos = pos;
                recycleNode.parent = parent;
                recycleNode.gCost = gCost;
                recycleNode.hCost = hCost;
                recycleNode.fCost = gCost + hCost;
                return recycleNode;
            }

            //print("Creating new node");
            return new Node(pos, parent, gCost, hCost);
        }

        double getDistance(Vector2 tile, Vector2 goal)
        {
            double tiebreaker = (1.0 + 1 / 10000);
            return Manhattan_distance(tile, goal) * tiebreaker;
        }

        public double D = 0.95;  // lowest cost between adjacent squares

        double Manhattan_distance(Vector2 node, Vector2 goal)
        {
            //4 directions
            double dx = Mathf.Abs(node.x - goal.x);
            double dy = Mathf.Abs(node.y - goal.y);
            return D * (dx + dy);
        }

        static bool vecInList(List<Node> list, Vector2 vector)
        {
            foreach (Node n in list)
            {
                if (n.pos.Equals(vector))
                    return true;
            }
            return false;
        }

        void BackupToPool(List<Node> nodelist)
        {
            //print("backingUp Node");
            foreach (var node in nodelist)
            {
                nodePool.Enqueue(node);
            }
        }

        private class Node
        {
            public Vector2 pos;
            public Node parent;
            public double fCost, gCost, hCost;

            public Node(Vector2 pos, Node parent, double gCost, double hCost)
            {
                this.pos = pos;
                this.parent = parent;
                this.gCost = gCost;
                this.hCost = hCost;
                this.fCost = this.gCost + this.hCost;
            }
        }

        private class NodeComparer : IComparer<Node>
        {
            public int Compare(Node n0, Node n1)
            {
                if (n1.fCost < n0.fCost)
                    return +1;
                if (n1.fCost > n0.fCost)
                    return -1;
                return 0;
            }
        }
    }
}
