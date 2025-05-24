using com.zeroerror.behaviortree.Runtime;
using UnityEditor;
using UnityEngine;

namespace com.zeroerror.behaviortree.EditorTool
{
    public abstract class NodeView
    {
        protected Color _bgColor = new Color(0.2f, 0.2f, 0.2f, 1);

        protected Rect _rect = new Rect(0, 0, 200, 100);
        public Rect rect => _rect;

        public string title;
        public NodeData nodeData; // 逻辑节点

        public NodeView()
        {
            this.nodeData = this._InitNode();
            this.nodeData.viewType = this.GetType().AssemblyQualifiedName;
            this.title = nodeData.nodeName;
        }

        protected abstract NodeData _InitNode();

        public virtual void Drag(Vector2 delta)
        {
            this._rect.position += delta;
        }

        public virtual void SetPosition(Vector2 pos)
        {
            this._rect.position = pos;
        }

        // 派生类重写以自定义显示内容
        public void Draw()
        {
            GUI.color = this._bgColor;
            GUI.Box(rect, "");
            GUI.color = Color.white;
            GUILayout.BeginArea(rect);
            GUILayout.Label(title, EditorStyles.boldLabel);
            _Draw();
            GUILayout.EndArea();
        }
        protected abstract void _Draw();

        public void ImportData(NodeData data)
        {
            if (data != null)
            {
                this.nodeData = data;
                this.title = data.nodeName;
                this._rect.position = data.position;
            }
        }

        public NodeData ExportData()
        {
            var nodeData = this.nodeData.Clone();
            nodeData.position = rect.position;
            return nodeData;
        }
    }
}