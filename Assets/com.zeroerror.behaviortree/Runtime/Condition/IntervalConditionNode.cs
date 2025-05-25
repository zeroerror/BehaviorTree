namespace com.zeroerror.behaviortree.Runtime
{
    public class IntervalConditionNode : ConditionNode
    {
        private float time;
        public override string Name => "条件节点 - 间隔时间";

        public float interval;

        public IntervalConditionNode(float interval)
        {
            this.interval = interval;
        }

        protected override NodeStatus _Tick(float dt)
        {
            time += dt;
            if (time >= interval)
            {
                time -= interval;
                return NodeStatus.Success;
            }
            return NodeStatus.Failure;
        }
    }
}