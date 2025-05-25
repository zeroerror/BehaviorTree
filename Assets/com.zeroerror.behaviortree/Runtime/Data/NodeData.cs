using UnityEngine;

namespace com.zeroerror.behaviortree.Runtime
{
    [System.Serializable]
    public abstract class NodeData
    {
        public string nodeName;    // 节点类型名或自定义名. （放在第一位是因为编辑器会自动获取展示
        [HideInInspector]
        public string guid;      // 唯一标识符
        [HideInInspector]
        public Vector2 position; // 节点在编辑器中的位置
        [HideInInspector]
        public string viewType;

        public NodeData()
        {
            this.nodeName = this.GetType().Name; // 默认使用类名作为节点名称
        }

        public abstract NodeData Clone(); // 克隆节点数据
        public virtual void CopyFrom(NodeData data)
        {
            this.nodeName = data.nodeName;
            this.guid = data.guid;
            this.position = data.position;
            this.viewType = data.viewType;
        }

        public virtual void InitGUID()
        {
            if (string.IsNullOrEmpty(this.guid))
            {
                this.guid = System.Guid.NewGuid().ToString();
            }
        }

        public abstract Node ToNode();
    }
    // delegate 用于根据 guid 获取节点数据的方法
    public delegate NodeData GetNodeDataByGuid(string guid);
}