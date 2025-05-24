namespace com.zeroerror.behaviortree.Runtime
{
    [System.Serializable]
    public abstract class DecoratorNodeData : NodeData
    {
        public string childGuid; // 保存被装饰节点的唯一ID

        public override void CopyFrom(NodeData data)
        {
            base.CopyFrom(data);
            var decoratorData = data as DecoratorNodeData;
            if (decoratorData != null)
                this.childGuid = decoratorData.childGuid;
        }
    }
}