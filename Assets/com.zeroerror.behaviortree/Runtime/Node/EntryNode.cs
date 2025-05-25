namespace com.zeroerror.behaviortree.Runtime
{
    public class EntryNode : Node
    {
        public override string Name => "入口节点";

        public EntryNode() : base()
        {
        }

        protected override NodeStatus _Tick(float dt)
        {
            return NodeStatus.Success;
        }
    }
}