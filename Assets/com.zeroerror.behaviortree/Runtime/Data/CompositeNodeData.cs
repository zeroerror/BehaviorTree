using System.Collections.Generic;

namespace com.zeroerror.behaviortree.Runtime
{
    public abstract class CompositeNodeData : NodeData
    {
        public List<string> holdGuids = new List<string>();

        public override void CopyFrom(NodeData data)
        {
            base.CopyFrom(data);
            if (data is CompositeNodeData compositeData)
            {
                this.holdGuids.Clear();
                this.holdGuids.AddRange(compositeData.holdGuids);
            }
        }
    }
}