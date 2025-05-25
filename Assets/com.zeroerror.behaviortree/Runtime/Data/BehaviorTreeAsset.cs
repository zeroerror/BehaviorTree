using System.Collections.Generic;
using UnityEngine;

namespace com.zeroerror.behaviortree.Runtime
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "BehaviorTree/TreeAsset")]
    public class BehaviorTreeAsset : ScriptableObject
    {
        [SerializeReference]
        public NodeData entryNodeData;
        [SerializeReference]
        public List<NodeData> nodeDatas = new List<NodeData>();
        public List<ConnectionData> connections = new List<ConnectionData>();

        public BehaviorTree ToTree()
        {
            var nodeDict = new Dictionary<string, Node>();
            var nodeList = new List<Node>();
            foreach (var nodeData in nodeDatas)
            {
                var node = nodeData.ToNode();
                node.SetGuid(nodeData.guid);
                nodeList.Add(node);
                nodeDict[nodeData.guid] = node;
            }

            foreach (var connection in connections)
            {
                if (nodeDict.TryGetValue(connection.fromNodeId, out var fromNode) &&
                    nodeDict.TryGetValue(connection.toNodeId, out var toNode))
                {
                    if (!connection.isHold)
                    {
                        fromNode.SetChild(toNode);
                    }
                    else
                    {
                        if (fromNode is CompositeNode compositeNode)
                        {
                            compositeNode.AddHold(toNode);
                        }
                        else if (fromNode is DecoratorNode decoratorNode)
                        {
                            decoratorNode.SetHold(toNode);
                        }
                    }
                }
            }

            var entryNode = nodeDict[entryNodeData.guid];
            return new BehaviorTree(entryNode);
        }
    }

}