using com.zeroerror.behaviortree.Runtime;
using UnityEditor;
using UnityEngine;

namespace com.zeroerror.behaviortree.EditorTool
{
    [NodeMenu("条件节点/间隔时间")]
    public class IntervalConditionNodeView : NodeView
    {
        protected override void _Draw()
        {
            GUILayout.Label("间隔时间（秒）", EditorStyles.label);
            var n = this.nodeData as IntervalConditionNodeData;
            float newInterval = EditorGUILayout.FloatField(n.interval);
            if (newInterval != n.interval)
            {
                n.interval = newInterval;
            }
        }

        protected override NodeData _InitNode()
        {
            var nodeData = new IntervalConditionNodeData();
            return nodeData;
        }
    }
}