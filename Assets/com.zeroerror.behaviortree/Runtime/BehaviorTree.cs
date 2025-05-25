namespace com.zeroerror.behaviortree.Runtime
{
    public class BehaviorTree
    {
        public Node entryNode { get; private set; }

        public BehaviorTree(Node entryNode)
        {
            this.entryNode = entryNode;
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