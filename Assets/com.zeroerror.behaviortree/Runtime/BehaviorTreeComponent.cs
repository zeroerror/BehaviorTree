using UnityEngine;

namespace com.zeroerror.behaviortree.Runtime
{
    public class BehaviorTreeComponent : MonoBehaviour
    {
        [Header("行为树配置")]
        public BehaviorTreeAsset asset;

        public BehaviorTree tree;

        public void Update()
        {
            if (tree != null)
            {
                tree.Tick(Time.deltaTime);
                return;
            }

            tree = asset?.ToTree();
        }
    }
}