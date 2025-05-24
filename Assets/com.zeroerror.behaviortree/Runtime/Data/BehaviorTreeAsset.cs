using System.Collections.Generic;
using UnityEngine;

namespace com.zeroerror.behaviortree.Runtime
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "BehaviorTree/TreeAsset")]
    public class BehaviorTreeAsset : ScriptableObject
    {
        [SerializeReference]
        public List<NodeData> nodes = new List<NodeData>();
        public List<ConnectionData> connections = new List<ConnectionData>();
    }

}