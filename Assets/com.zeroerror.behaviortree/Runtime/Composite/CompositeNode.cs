using System.Collections.Generic;

namespace com.zeroerror.behaviortree.Runtime
{
    public abstract class CompositeNode : Node
    {
        protected List<Node> children = new List<Node>();
        public override void AddChild(Node child) => children.Add(child);

        public Node child;

        public override void InjectContext(object context)
        {
            foreach (var child in children)
            {
                child.InjectContext(context);
            }
        }
    }
}