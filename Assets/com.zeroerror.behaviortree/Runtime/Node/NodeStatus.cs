namespace com.zeroerror.behaviortree.Runtime
{
    public enum NodeStatus
    {
        None,       // 节点未执行
        Running,    // 节点正在执行
        Success,    // 节点执行成功
        Failure,    // 节点执行失败
        Aborted     // 节点被中止
    }
}