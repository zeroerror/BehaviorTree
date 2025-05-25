using UnityEngine;

namespace com.zeroerror.behaviortree.EditorTool
{
    public abstract class ActionNodeView : NodeView
    {
        protected override Color bgColor => new Color(40, 82, 68, 255) / 255.0f; // 背景颜色
        public NodeView childView; // 子节点视图
    }
}