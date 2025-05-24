using com.zeroerror.behaviortree.Runtime;
using UnityEngine;
namespace com.zeroerror.behaviortree.Sample
{
    public class Sample : MonoBehaviour
    {
        private SampleBehaviorTree behaviorTree;
        private SampleContext context;

        private void Awake()
        {
            // 创建顺序节点
            var sequenceNode = new SequenceNode();
            // 创建条件节点、行为节点 并添加到顺序节点
            var conditionNode = new IntervalConditionNode(1f);
            var actionNode = new LogActionNode();
            sequenceNode.AddChild(conditionNode);
            sequenceNode.AddChild(actionNode);
            // 创建装饰器节点 (3s延迟) 装饰顺序节点
            var delayNode = new DelayNode(sequenceNode, 3f);
            // 创建上下文
            this.context = new SampleContext();
            // 创建行为树
            this.behaviorTree = new SampleBehaviorTree(delayNode, this.context);
        }

        private void Update()
        {
            var dt = Time.deltaTime;
            this.behaviorTree.Tick(dt);
        }
    }
}