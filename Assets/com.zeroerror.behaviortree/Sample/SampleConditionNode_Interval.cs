using com.zeroerror.behaviortree.Runtime;

namespace com.zeroerror.behaviortree.Sample
{
    public class SampleConditionNode_Interval : Node
    {
        private float time;
        public override string Name => "条件节点 - 间隔时间";

        private readonly float interval;
        public SampleConditionNode_Interval(float interval)
        {
            this.interval = interval;
        }

        protected override NodeStatus _Tick(float dt)
        {
            time += dt;
            if (time >= interval)
            {
                var context = _context as SampleContext;
                context.frame += 1;
                time -= interval;
                return NodeStatus.Success;
            }
            return NodeStatus.Failure;
        }
    }
}