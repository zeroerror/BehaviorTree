using com.zeroerror.behaviortree.Runtime;

namespace com.zeroerror.behaviortree.Sample
{
    public class LogActionNode : ActionNode
    {
        public string message;

        public LogActionNode(string message)
        {
            this.message = message;
        }

        protected override NodeStatus _Tick(float dt)
        {
            UnityEngine.Debug.Log(message);
            return NodeStatus.Success;
        }
    }
}