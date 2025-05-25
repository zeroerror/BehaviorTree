using System.Collections.Generic;

namespace com.zeroerror.behaviortree.Runtime
{
    public abstract class CompositeNodeData : NodeData
    {
        public List<string> childGuids = new List<string>();

        public CompositeNodeData() : base()
        {
            this.nodeName = "复合节点";
        }

        public override void CopyFrom(NodeData data)
        {
            base.CopyFrom(data);
            var compositeData = data as CompositeNodeData;
            if (compositeData != null)
            {
                this.childGuids.Clear();
                this.childGuids.AddRange(compositeData.childGuids);
            }
        }
    }
}