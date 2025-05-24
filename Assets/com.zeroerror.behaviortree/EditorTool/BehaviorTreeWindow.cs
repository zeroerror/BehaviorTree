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
        private Vector2 offset;
        private Vector2 drag;
        private NodeView selectedNodeView = null;

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

            ProcessAllEvents(Event.current);

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

        private void ProcessAllEvents(Event e)
        {
            drag = Vector2.zero;

            if (e.type == EventType.ContextClick)
            {
                Vector2 mousePos = e.mousePosition;
                selectedNodeView = null;
                foreach (var node in nodeViews)
                {
                    if (node.rect.Contains(mousePos))
                    {
                        selectedNodeView = node;
                        break;
                    }
                }

                GenericMenu menu = new GenericMenu();
                if (selectedNodeView != null)
                {
                    menu.AddItem(new GUIContent("删除节点"), false, () => DeleteNode(selectedNodeView));
                    menu.AddItem(new GUIContent("创建连线"), false, () =>
                    {
                        connectMode = ConnectMode.SelectingTo;
                        connectFromNode = selectedNodeView;
                    });

                    // 显示所有装饰节点 todo
                    menu.AddItem(new GUIContent("添加装饰节点/Delay"), false, () =>
                    {
                        var decoratorView = new DelayNodeView();
                        decoratorView.nodeData.InitGUID();
                        decoratorView.SetPosition(e.mousePosition);
                        var decoratorNodeData = decoratorView.nodeData as DecoratorNodeData;
                        decoratorNodeData.childGuid = selectedNodeView.nodeData.guid; // 连接到被装饰节点
                        decoratorView.childView = selectedNodeView; // 连接到被装饰节点
                        // 插入到selectedNodeView之前 
                        int index = nodeViews.IndexOf(selectedNodeView);
                        if (index >= 0) nodeViews.Insert(index, decoratorView);
                        // 更新所有父节点：指向decorator.guid，而不是B
                        foreach (var conn in connections)
                        {
                            if (conn.toNodeId == selectedNodeView.nodeData.guid)
                                conn.toNodeId = decoratorView.nodeData.guid;
                        }
                        // 如果B是根节点，记得把根节点指向decorator
                        // ...
                    });

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

            this._ProcessConnectEvents(e);
            this._ProcessDragEvents(e);
        }

        private void DeleteNode(NodeView view)
        {
            // 1. 先找到所有以view为childView的装饰节点
            List<NodeView> decoratorToDelete = new List<NodeView>();
            foreach (var node in nodeViews)
            {
                if (node is DecoratorNodeView decorator && decorator.childView == view)
                {
                    decoratorToDelete.Add(decorator);
                }
            }
            // 2. 递归删除所有相关装饰节点
            foreach (var decorator in decoratorToDelete)
            {
                DeleteNode(decorator);
            }
            // 3. 移除自身和所有连接
            nodeViews.Remove(view);
            connections.RemoveAll(c => c.fromNodeId == view.nodeData.guid || c.toNodeId == view.nodeData.guid);
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

            // 刷新 decoratorView 的 childView
            foreach (var nodeView in nodeViews)
            {
                if (nodeView is DecoratorNodeView decoratorView)
                {
                    var childNode = nodeViews.Find(n =>
                    {
                        var decoratorData = decoratorView.nodeData as DecoratorNodeData;
                        return n.nodeData.guid == decoratorData.childGuid;
                    });
                    if (childNode != null)
                    {
                        decoratorView.childView = childNode;
                    }
                }
            }

            connections = new List<ConnectionData>(asset.connections);
            Debug.Log("行为树已加载: " + path);
        }

        public NodeView GetNodeViewByGuid(string guid)
        {
            foreach (var node in nodeViews)
            {
                if (node.nodeData.guid == guid)
                    return node;
            }
            return null;
        }

        #region [Connect]

        private List<ConnectionData> connections = new List<ConnectionData>();
        // 用于记录连线创建状态
        private enum ConnectMode { None, SelectingFrom, SelectingTo }
        private ConnectMode connectMode = ConnectMode.None;
        private NodeView connectFromNode = null;

        private List<ConnectionData> BuildConnectionData()
        {
            // 如果你的connections已经是ConnectionData列表，这里直接return connections;
            // 如果用的是自定义Connection类，需要转换为ConnectionData
            return new List<ConnectionData>(connections);
        }

        private void _ProcessConnectEvents(Event e)
        {
            // 鼠标拖动时画临时贝塞尔线
            if (connectMode == ConnectMode.SelectingTo && connectFromNode != null)
            {
                Handles.DrawBezier(
                    connectFromNode.rect.center,
                    Event.current.mousePosition,
                    connectFromNode.rect.center + Vector2.right * 50,
                    Event.current.mousePosition + Vector2.left * 50,
                    Color.yellow, null, 2f
                );
                GUI.changed = true;
            }

            // 鼠标点击目标节点时完成连线
            if (connectMode == ConnectMode.SelectingTo && Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                Vector2 mousePos = Event.current.mousePosition;
                NodeView toNode = null;
                foreach (var node in nodeViews)
                {
                    if (node.rect.Contains(mousePos) && node != connectFromNode)
                    {
                        toNode = node;
                        break;
                    }
                }
                if (toNode != null)
                {
                    // 创建连线
                    connections.Add(new ConnectionData
                    {
                        fromNodeId = connectFromNode.nodeData.guid,
                        toNodeId = toNode.nodeData.guid
                    });
                    connectMode = ConnectMode.None;
                    connectFromNode = null;
                    Event.current.Use();
                    GUI.changed = true;
                }
                else if (!IsMouseOverAnyNode(mousePos))
                {
                    // 点击空白区，取消连线
                    connectMode = ConnectMode.None;
                    connectFromNode = null;
                    Event.current.Use();
                    GUI.changed = true;
                }
            }

            // 支持ESC键取消连线
            if (connectMode == ConnectMode.SelectingTo && Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Escape)
            {
                connectMode = ConnectMode.None;
                connectFromNode = null;
                Event.current.Use();
            }
        }

        private bool IsMouseOverAnyNode(Vector2 mousePos)
        {
            foreach (var node in nodeViews)
            {
                if (node.rect.Contains(mousePos))
                    return true;
            }
            return false;
        }

        private void DrawConnections()
        {
            foreach (var conn in connections)
            {
                NodeView from = nodeViews.Find(n => n.nodeData.guid == conn.fromNodeId);
                NodeView to = nodeViews.Find(n => n.nodeData.guid == conn.toNodeId);
                if (from != null && to != null)
                {
                    Vector2 fromCenter = from.rect.center;
                    Vector2 toCenter = to.rect.center;
                    Vector2 dir = (toCenter - fromCenter).normalized;

                    // 计算起点/终点：矩形中心往目标方向投射到边上
                    Vector2 fromEdge = GetRectEdgePoint(from.rect, dir);
                    Vector2 toEdge = GetRectEdgePoint(to.rect, -dir);

                    Handles.color = Color.white;
                    Handles.DrawLine(fromEdge, toEdge);
                    Handles.color = Color.white; // 恢复颜色（可选）
                    DrawArrow(toEdge, dir, 16, 22, Color.white);
                }
            }
        }

        // 计算rect边缘最近点（direction为起点指向终点的方向）
        private Vector2 GetRectEdgePoint(Rect rect, Vector2 direction)
        {
            Vector2 center = rect.center;
            float hw = rect.width / 2f;
            float hh = rect.height / 2f;
            float dx = direction.x;
            float dy = direction.y;

            // 计算与哪个边的交点最近
            float px = dx == 0 ? 0 : hw / Mathf.Abs(dx);
            float py = dy == 0 ? 0 : hh / Mathf.Abs(dy);
            float t = Mathf.Min(px, py);

            return center + direction * t;
        }

        // 在point点，沿dir方向画一个箭头
        private void DrawArrow(Vector2 point, Vector2 dir, float arrowLength, float arrowAngle, Color color)
        {
            Vector2 right = Quaternion.Euler(0, 0, arrowAngle) * -dir * arrowLength;
            Vector2 left = Quaternion.Euler(0, 0, -arrowAngle) * -dir * arrowLength;

            Handles.color = color;
            Handles.DrawAAPolyLine(3f, point, point + right);
            Handles.DrawAAPolyLine(3f, point, point + left);
            Handles.color = Color.white;
        }

        #endregion

        #region [Drag]

        private NodeView draggingNode = null;
        private Vector2 dragOffset = Vector2.zero;

        private void _ProcessDragEvents(Event e)
        {
            drag = Vector2.zero;
            Vector2 mousePos = e.mousePosition;

            // 节点拖拽
            if (e.type == EventType.MouseDown && e.button == 0)
            {
                foreach (var node in nodeViews)
                {
                    if (node.rect.Contains(mousePos))
                    {
                        draggingNode = node;
                        dragOffset = mousePos - node.rect.position;
                        GUI.FocusControl(null); // 防止控件被选中
                        e.Use();
                        break;
                    }
                }
            }

            if (e.type == EventType.MouseDrag && e.button == 0 && draggingNode != null)
            {
                draggingNode.SetPosition(mousePos - dragOffset);
                GUI.changed = true;
                e.Use();
            }

            if (e.type == EventType.MouseUp && e.button == 0 && draggingNode != null)
            {
                draggingNode = null;
                e.Use();
            }
        }

        #endregion
    }
}