using com.zeroerror.behaviortree.Runtime;
using UnityEditor;
using UnityEngine;

namespace com.zeroerror.behaviortree.EditorTool
{
    public abstract class NodeView
    {
        public NodeData nodeData; // 逻辑节点

        protected BehaviorTreeWindow window; // 关联的行为树窗口
        public void InjectWindow(BehaviorTreeWindow window) => this.window = window;
        protected virtual Color contentColor => new Color(1, 1, 1, 1); // 内容颜色
        protected virtual Color bgColor => new Color(35, 35, 35, 255) / 255.0f; // 背景颜色
        protected virtual Texture2D bgTexture => AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/com.zeroerror.behaviortree/EditorTool/Textures/NodeBackground.png");
        protected Rect _rect = new Rect(0, 0, 200, 100);
        public Rect rect => _rect;
        public string title;

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
        public virtual void Draw()
        {
            EditorGUI.BeginChangeCheck();

            var contentColor = GUI.contentColor;
            GUI.contentColor = this.contentColor;

            // 画背景
            if (bgTexture != null)
            {
                GUI.DrawTexture(rect, bgTexture, ScaleMode.StretchToFill);
            }
            else
            {
                EditorGUI.DrawRect(rect, this.bgColor);
            }

            GUILayout.BeginArea(rect);
            GUILayout.Label(title, EditorStyles.boldLabel);
            _Draw();
            GUILayout.EndArea();

            GUI.contentColor = contentColor;

            if (EditorGUI.EndChangeCheck())
            {
                window.RecordUndoSnapshot(); // 通知主窗口记录照
            }
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