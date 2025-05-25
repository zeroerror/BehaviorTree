using com.zeroerror.behaviortree.Runtime;
using UnityEngine;
namespace com.zeroerror.behaviortree.Sample
{
    [RequireComponent(typeof(BehaviorTreeComponent))]
    public class Sample : MonoBehaviour
    {
        private SampleContext context;
        private BehaviorTreeComponent behaviorTreeComponent;
        private bool hasInited = false;

        private void Awake()
        {
            behaviorTreeComponent = GetComponent<BehaviorTreeComponent>();
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            if (!hasInited && behaviorTreeComponent?.tree != null)
            {
                this.context = new SampleContext();
                behaviorTreeComponent.tree.entryNode.InjectContext(context);
            }
        }
    }
}