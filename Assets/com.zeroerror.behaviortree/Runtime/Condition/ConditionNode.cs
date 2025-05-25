namespace com.zeroerror.behaviortree.Runtime
{
    public abstract class ConditionNode : Node
    {
        public override string Name => "条件节点";
        public Node child;

        public override void AddChild(Node child)
        {
            this.child = child;
        }

        protected override NodeStatus _Tick(float dt)
        {
            var status = this.M_Tick(dt);
            if (status == NodeStatus.Success)
            {
                child?.Tick(dt);
            }
            return status;
        }
        protected abstract NodeStatus M_Tick(float dt);
    }
}