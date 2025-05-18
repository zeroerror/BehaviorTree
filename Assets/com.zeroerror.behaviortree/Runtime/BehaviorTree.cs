namespace com.zeroerror.behaviortree.Runtime
{
    public class BehaviorTree
    {
        public Node rootNode { get; private set; }

        public BehaviorTree(Node rootNode)
        {
            this.rootNode = rootNode;
        }

        public NodeStatus Tick(float dt)
        {
            return rootNode?.Tick(dt) ?? NodeStatus.Failure;
        }

        public void Reset()
        {
            rootNode?.Reset();
        }
    }
}