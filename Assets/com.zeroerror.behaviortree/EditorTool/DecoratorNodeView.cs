using UnityEditor;
using UnityEngine;

namespace com.zeroerror.behaviortree.EditorTool
{
    public abstract class DecoratorNodeView : NodeView
    {
        protected virtual float ContentHeight => 50;
        protected override Color bgColor => new Color(0.5f, 0.5f, 0.5f, 0.5f); // 背景颜色
    }
}