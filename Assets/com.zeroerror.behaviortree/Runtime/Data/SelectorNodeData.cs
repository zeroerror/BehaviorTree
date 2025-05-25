using System.Collections.Generic;

namespace com.zeroerror.behaviortree.Runtime
{
    public class SelectorNodeData : CompositeNodeData
    {
        public SelectorNodeData() : base()
        {
            this.nodeName = "选择节点";
        }

        public override NodeData Clone()
        {
            var nodeData = new SelectorNodeData();
            nodeData.CopyFrom(this);
            return nodeData;
        }

        public override Node ToNode()
        {
            throw new System.NotImplementedException();
        }
    }
}