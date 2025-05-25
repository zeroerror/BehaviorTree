namespace com.zeroerror.behaviortree.Runtime
{
    public class DelayNode : DecoratorNode
    {
        public override string Name => "延迟节点";

        public readonly float delayTime;
        private float elapsedTime;

        public DelayNode(float delayTime) : base()
        {
            this.delayTime = delayTime;
            this.elapsedTime = 0f;
        }

        protected override NodeStatus _Tick(float dt)
        {
            elapsedTime += dt;
            if (elapsedTime >= delayTime)
            {
                this.hold?.Tick(dt);
                return NodeStatus.Success;
            }
            return NodeStatus.Running;
        }
    }
}