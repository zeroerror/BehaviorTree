namespace com.zeroerror.behaviortree.Runtime
{
    [System.Serializable]
    public class ConnectionData
    {
        public string fromNodeId; // 起始节点的唯一标识符
        public string toNodeId;   // 目标节点的唯一标识符
    }
}