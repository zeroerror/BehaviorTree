using UnityEngine;

namespace com.zeroerror.behaviortree.EditorTool
{
    public abstract class CompositeNodeView : NodeView
    {
        protected override Color bgColor => new Color(0.3f, 0.3f, 0.3f, 1f);

        protected override void _Draw()
        {
        }
    }
}