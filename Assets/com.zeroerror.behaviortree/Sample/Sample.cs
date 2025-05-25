using com.zeroerror.behaviortree.Runtime;
using UnityEngine;
namespace com.zeroerror.behaviortree.Sample
{
    public class Sample : MonoBehaviour
    {
        private BehaviorTree behaviorTree;
        private SampleContext context;
        public BehaviorTreeAsset behaviorTreeAsset;

        private void Awake()
        {
            // 创建上下文
            this.context = new SampleContext();
            // 创建行为树
            this.behaviorTree = behaviorTreeAsset.ToTree();
            this.behaviorTree.entryNode.InjectContext(this.context);
        }

        private void Update()
        {
            var dt = Time.deltaTime;
            this.behaviorTree.Tick(dt);
        }
    }
}