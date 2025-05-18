using com.zeroerror.behaviortree.Runtime;

namespace com.zeroerror.behaviortree.Sample
{
    public class SampleBehaviorTree : BehaviorTree
    {
        public SampleContext sampleContext { get; private set; }

        public SampleBehaviorTree(Node rootNode, SampleContext sampleContext) : base(rootNode)
        {
            this.sampleContext = sampleContext;
            rootNode.InjectContext(sampleContext);
        }
    }
}
