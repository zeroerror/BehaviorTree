namespace com.zeroerror.behaviortree.Runtime
{
    public abstract class DecoratorNode : Node
    {
        public readonly Node child;

        public DecoratorNode(Node child)
        {
            this.child = child;
        }

        public override void InjectContext(object context)
        {
            child.InjectContext(context);
        }
    }
}