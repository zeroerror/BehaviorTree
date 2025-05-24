using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;
using com.zeroerror.behaviortree.Runtime;

namespace com.zeroerror.behaviortree.EditorTool
{

    public class BehaviorTreeWindow : EditorWindow
    {
        private List<NodeView> nodeViews = new List<NodeView>();
        private List<ConnectionData> connections = new List<ConnectionData>();
        private Vector2 offset;
        private Vector2 drag;
        private NodeView selectedNode = null;

        [MenuItem("Tools/Behavior Tree Editor")]
        public static void OpenWindow()
        {
            BehaviorTreeWindow window = GetWindow<BehaviorTreeWindow>();
            window.titleContent = new GUIContent("Behavior Tree Editor");
        }

        private void OnGUI()
        {
            DrawGrid(20, 0.2f, Color.gray);
            DrawGrid(100, 0.4f, Color.gray);

            DrawNodes();
            DrawConnections();

            ProcessEvents(Event.current);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("保存行为树"))
                SaveTree();
            if (GUILayout.Button("加载行为树"))
                LoadTree();
            GUILayout.EndHorizontal();

            if (GUI.changed) Repaint();
        }

        private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
        {
            int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
            int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

            Handles.BeginGUI();
            Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

            offset += drag * 0.5f;
            Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

            for (int i = 0; i < widthDivs; i++)
                Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset,
                                 new Vector3(gridSpacing * i, position.height, 0f) + newOffset);

            for (int j = 0; j < heightDivs; j++)
                Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset,
                                 new Vector3(position.width, gridSpacing * j, 0f) + newOffset);

            Handles.color = Color.white;
            Handles.EndGUI();
        }

        private void DrawNodes()
        {
            foreach (var node in nodeViews)
            {
                node.Draw();
            }
        }

        private void DrawConnections()
        {
            // 示例：画贝塞尔曲线连接
            foreach (var conn in connections)
            {
                NodeView from = nodeViews.Find(n => n.nodeData.guid == conn.fromNodeId);
                NodeView to = nodeViews.Find(n => n.nodeData.guid == conn.toNodeId);
                if (from != null && to != null)
                {
                    Handles.DrawBezier(
                        from.rect.center, to.rect.center,
                        from.rect.center + Vector2.right * 50,
                        to.rect.center + Vector2.left * 50,
                        Color.white, null, 2f
                    );
                }
            }
        }

        private void ProcessEvents(Event e)
        {
            drag = Vector2.zero;

            if (e.type == EventType.ContextClick)
            {
                Vector2 mousePos = e.mousePosition;
                selectedNode = null;
                foreach (var node in nodeViews)
                {
                    if (node.rect.Contains(mousePos))
                    {
                        selectedNode = node;
                        break;
                    }
                }

                GenericMenu menu = new GenericMenu();
                if (selectedNode != null)
                {
                    menu.AddItem(new GUIContent("删除节点"), false, () => DeleteNode(selectedNode));
                }
                else
                {
                    foreach (var (menuName, nodeViewType) in NodeViewRegistry.GetAllNodeViewsWithMenu())
                    {
                        menu.AddItem(new GUIContent("创建/" + menuName), false, () =>
                        {
                            var nodeView = (NodeView)Activator.CreateInstance(nodeViewType);
                            nodeView.nodeData.InitGUID();
                            nodeView.SetPosition(e.mousePosition);
                            nodeViews.Add(nodeView);
                        });
                    }
                }
                menu.ShowAsContext();
                e.Use();
            }

            switch (e.type)
            {
                case EventType.MouseDrag:
                    if (e.button == 0)
                    {
                        OnDrag(e.delta);
                    }
                    break;
            }
        }

        private void OnDrag(Vector2 delta)
        {
            drag = delta;
            foreach (var node in nodeViews)
            {
                node.Drag(delta);
            }
            GUI.changed = true;
        }

        private void DeleteNode(NodeView node)
        {
            nodeViews.Remove(node);
            // 同步移除相关连接
            connections.RemoveAll(c => c.fromNodeId == node.nodeData.guid || c.toNodeId == node.nodeData.guid);
        }

        private List<ConnectionData> BuildConnectionData()
        {
            // 如果你的connections已经是ConnectionData列表，这里直接return connections;
            // 如果用的是自定义Connection类，需要转换为ConnectionData
            return new List<ConnectionData>(connections);
        }

        private void SaveTree()
        {
            string path = EditorUtility.SaveFilePanelInProject("保存行为树", "BehaviorTreeAsset", "asset", "保存行为树到Asset");
            if (string.IsNullOrEmpty(path)) return;

            BehaviorTreeAsset asset = ScriptableObject.CreateInstance<BehaviorTreeAsset>();
            foreach (var node in nodeViews)
            {
                var nodeData = node.ExportData();
                asset.nodes.Add(nodeData.Clone());
            }
            asset.connections = BuildConnectionData();

            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("行为树已保存: " + path);
        }

        private void LoadTree()
        {
            string path = EditorUtility.OpenFilePanel("加载行为树", "Assets", "asset");
            if (string.IsNullOrEmpty(path)) return;

            if (path.StartsWith(Application.dataPath))
                path = "Assets" + path.Substring(Application.dataPath.Length);

            var asset = AssetDatabase.LoadAssetAtPath<BehaviorTreeAsset>(path);
            if (asset == null)
            {
                Debug.LogError("未找到行为树Asset: " + path);
                return;
            }

            nodeViews.Clear();
            foreach (var nodeData in asset.nodes)
            {
                var viewType = Type.GetType(nodeData.viewType);
                var nodeView = (NodeView)Activator.CreateInstance(viewType);
                nodeView.ImportData(nodeData.Clone());
                nodeViews.Add(nodeView);
            }
            connections = new List<ConnectionData>(asset.connections);
            Debug.Log("行为树已加载: " + path);
        }
    }
}