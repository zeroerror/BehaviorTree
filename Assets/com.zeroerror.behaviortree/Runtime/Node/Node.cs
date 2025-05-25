namespace com.zeroerror.behaviortree.Runtime
{
    public abstract class Node
    {
        /// <summary> 节点名称 </summary>
        public abstract string Name { get; }
        /// <summary> 节点重置时 </summary>
        protected virtual void _Reset() { }
        /// <summary> 节点激活时 </summary>        
        protected virtual void _OnEnter() { }
        /// <summary> 节点结束时 </summary>        
        protected virtual void _OnExit() { }
        /// <summary> 执行节点逻辑 </summary>   
        protected abstract NodeStatus _Tick(float dt);

        /// <summary> 当前节点状态 </summary>        
        public virtual NodeStatus Status => _status;
        private NodeStatus _status = NodeStatus.None;

        /// <summary> 注入上下文 </summary>
        public virtual void InjectContext(object context)
        {
            this._context = context;
            this._child?.InjectContext(context);
        }
        protected object _context { get; private set; }

        /// <summary> 更新 </summary>        
        public NodeStatus Tick(float dt)
        {
            var curStatus = _Tick(dt);
            if (curStatus == this._status)
            {
                if (curStatus == NodeStatus.Success) this._child?.Tick(dt);
                return curStatus;
            }

            var fromStatus = this._status;
            var toStatus = curStatus;
            this._status = curStatus;

            if (fromStatus == NodeStatus.Running && toStatus != NodeStatus.Running)
            {
                _OnExit();
            }
            else if (fromStatus != NodeStatus.Running && toStatus == NodeStatus.Running)
            {
                _OnEnter();
            }

            if (toStatus == NodeStatus.Success) this._child?.Tick(dt);

            return curStatus;
        }

        /// <summary> 重置节点状态 </summary>        
        public void Reset()
        {
            if (_status == NodeStatus.Running)
            {
                _OnExit();
            }
            _Reset();
            _status = NodeStatus.None;
        }

        private Node _child;
        public void SetChild(Node child) => this._child = child;
    }
}

