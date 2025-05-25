using UnityEditor;
using UnityEngine;

namespace com.zeroerror.behaviortree.EditorTool
{
    public abstract class DecoratorNodeView : NodeView
    {
        protected virtual float ContentHeight => 50;
        protected override Color bgColor => new Color(0.5f, 0.5f, 0.5f, 0.5f); // 背景颜色
        public NodeView childView; // 子节点视图

        public override void Draw()
        {
            this._rect = childView.rect;
            this._rect.height += ContentHeight;
            this._rect.x -= 10;
            this._rect.y -= ContentHeight;
            EditorGUI.DrawRect(this._rect, this.bgColor);

            GUILayout.BeginArea(this._rect);
            GUILayout.Label(title, EditorStyles.boldLabel);
            _Draw();
            GUILayout.EndArea();
        }

        protected override void _Draw()
        {
        }
    }
}