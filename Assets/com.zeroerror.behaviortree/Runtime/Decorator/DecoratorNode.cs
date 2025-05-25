namespace com.zeroerror.behaviortree.Runtime
{
    public abstract class DecoratorNode : Node
    {
        public Node hold { get; private set; }
        public DecoratorNode() { }

        public void SetHold(Node node)
        {
            this.hold = node;
        }
    }
}