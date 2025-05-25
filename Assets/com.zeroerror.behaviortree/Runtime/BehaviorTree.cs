using System.Collections.Generic;
using UnityEngine;


namespace com.zeroerror.behaviortree.Runtime
{
    public class BehaviorTree
    {
        public Node entryNode { get; private set; }
        public readonly Dictionary<string, Node> nodeDict = new Dictionary<string, Node>();

        public BehaviorTree(Node entryNode)
        {
            this.entryNode = entryNode;
            entryNode.ForeachChildren((node) =>
            {
                var guid = node.guid;
                if (nodeDict.ContainsKey(guid))
                {
                    Debug.LogError($"BehaviorTree: Duplicate node guid {guid}");
                }
                else
                {
                    nodeDict.Add(guid, node);
                }
            });
        }

        public NodeStatus Tick(float dt)
        {
            return entryNode?.Tick(dt) ?? NodeStatus.Failure;
        }

        public void Reset()
        {
            entryNode?.Reset();
        }
    }
}