using System.Collections.Generic;

namespace com.zeroerror.behaviortree.Runtime
{
    public abstract class CompositeNodeData : NodeData
    {
        public List<string> childGuids = new List<string>();

        public override void CopyFrom(NodeData data)
        {
            base.CopyFrom(data);
            if (data is CompositeNodeData compositeData)
            {
                this.childGuids.Clear();
                this.childGuids.AddRange(compositeData.childGuids);
            }
        }
    }
}