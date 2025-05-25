namespace com.zeroerror.behaviortree.Runtime
{
    public abstract class DecoratorNode : Node
    {
        public Node child;

        public DecoratorNode() { }

        public override void InjectContext(object context)
        {
            child.InjectContext(context);
        }

        public override void AddChild(Node child)
        {
            this.child = child;
        }
    }
}