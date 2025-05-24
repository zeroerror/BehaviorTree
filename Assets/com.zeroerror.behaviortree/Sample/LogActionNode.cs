using com.zeroerror.behaviortree.Runtime;

namespace com.zeroerror.behaviortree.Sample
{
    public class LogActionNode : Node
    {
        public override string Name => "行为节点 - 日志打印";

        protected override NodeStatus _Tick(float dt)
        {
            // 打印日志
            var sampleContext = (SampleContext)this._context;
            UnityEngine.Debug.Log($"行为节点 - 当前帧:{sampleContext.frame}");
            // 返回成功状态
            return NodeStatus.Success;
        }
    }
}