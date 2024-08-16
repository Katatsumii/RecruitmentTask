using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

namespace Agents
{
    public class Agent : MonoBehaviour
    {
        [SerializeField] AIPath aiPath;

        string id;
        

        IAstarAI ai;
        public void InitAgent()
        {
            //materialize 
            SetAgentGuid();
            NewPath();
        }

        void Awake()
        {
            ai = GetComponent<IAstarAI>();
        }

        void NewPath()
        {
            Vector3 newDestination = PickRandomPoint();
            if (IsDestinationCorrect(newDestination))
            {
                ai.destination = newDestination;
                ai.SearchPath();
            }
            else
                NewPath();
        }

        bool IsDestinationCorrect(Vector3 destination)
        {
            var node1 = AstarPath.active.GetNearest(transform.position, NNConstraint.Default).node;
            var node2 = AstarPath.active.GetNearest(destination, NNConstraint.Default).node;

            return PathUtilities.IsPathPossible(node1, node2) && node2.Walkable;
        }

        Vector3 PickRandomPoint()
        {
            GraphNode randomNode;
            var grid = AstarPath.active.data.gridGraph;
            randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
            return (Vector3)randomNode.position;
        }

        void SetAgentGuid()
        {
            id = Guid.NewGuid().ToString();
        }

        public void SetTickRateForAgent(int tickRate)
        {
            ai.maxSpeed = tickRate;
        }
    }
}
