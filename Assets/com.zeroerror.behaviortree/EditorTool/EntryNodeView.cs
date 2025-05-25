using com.zeroerror.behaviortree.Runtime;
using UnityEngine;

namespace com.zeroerror.behaviortree.EditorTool
{
    // 行为树入口节点视图
    public class EntryNodeView : NodeView
    {
        protected override Color bgColor => new Color(129, 172, 63, 255) / 255.0f; // 背景颜色
        public NodeView childView; // 子节点视图

        protected override NodeData _InitNode()
        {
            var nodeData = new EntryNodeData();
            return nodeData;
        }

        protected override void _Draw()
        {

        }
    }
}