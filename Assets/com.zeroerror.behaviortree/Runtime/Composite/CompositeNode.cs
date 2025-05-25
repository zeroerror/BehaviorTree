using System.Collections.Generic;

namespace com.zeroerror.behaviortree.Runtime
{
    public abstract class CompositeNode : Node
    {
        public List<Node> holds { get; private set; } = new List<Node>();

        public override void InjectContext(object context)
        {
            base.InjectContext(context);
            foreach (var n in holds)
            {
                n.InjectContext(context);
            }
        }

        public void AddHold(Node node)
        {
            holds.Add(node);
        }
    }
}