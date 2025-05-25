namespace com.zeroerror.behaviortree.Runtime
{
    public abstract class DecoratorNode : Node
    {
        protected Node hold;
        public DecoratorNode() { }

        public void SetHold(Node node)
        {
            this.hold = node;
        }
    }
}