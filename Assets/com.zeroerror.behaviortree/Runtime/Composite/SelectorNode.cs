namespace com.zeroerror.behaviortree.Runtime
{
    public class SelectorNode : CompositeNode
    {
        public override string Name => "Selector";

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
                if (status == NodeStatus.Success || status == NodeStatus.Running)
                {
                    for (int j = i + 1; j < children.Count; j++) children[j].Reset();
                    return status;
                }
            }
            return NodeStatus.Failure;
        }
    }
}