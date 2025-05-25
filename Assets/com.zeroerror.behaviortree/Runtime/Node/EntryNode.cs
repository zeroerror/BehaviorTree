namespace com.zeroerror.behaviortree.Runtime
{
    public class EntryNode : Node
    {
        public override string Name => "入口节点";

        public Node child;

        public EntryNode() : base()
        {
        }

        protected override NodeStatus _Tick(float dt)
        {
            this.child.Tick(dt);
            return NodeStatus.Running;
        }

        public override void AddChild(Node child)
        {
            this.child = child;
        }
    }
}