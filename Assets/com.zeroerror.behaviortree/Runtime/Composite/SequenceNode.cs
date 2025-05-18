namespace com.zeroerror.behaviortree.Runtime
{
    public class SequenceNode : CompositeNode
    {
        public override string Name => "Sequence";

        protected override void _Reset()
        {
            foreach (var child in children)
                child.Reset();
        }

        protected override NodeStatus _Tick(float dt)
        {
            for (int i = 0; i < children.Count; i++)
            {
                var status = children[i].Tick(dt);
                if (status == NodeStatus.Failure || status == NodeStatus.Running)
                {
                    for (int j = i + 1; j < children.Count; j++) children[j].Reset();
                    return status;
                }
            }
            return NodeStatus.Success;
        }
    }
}