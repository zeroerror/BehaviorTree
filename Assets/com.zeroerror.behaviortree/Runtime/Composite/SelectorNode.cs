namespace com.zeroerror.behaviortree.Runtime
{
    public class SelectorNode : CompositeNode
    {
        public override string Name => "Selector";

        protected override void _Reset()
        {
            foreach (var child in holds)
                child.Reset();
        }

        protected override NodeStatus _Tick(float dt)
        {
            for (int i = 0; i < holds.Count; i++)
            {
                var status = holds[i].Tick(dt);
                if (status == NodeStatus.Success || status == NodeStatus.Running)
                {
                    for (int j = i + 1; j < holds.Count; j++) holds[j].Reset();
                    return status;
                }
            }
            return NodeStatus.Failure;
        }
    }
}