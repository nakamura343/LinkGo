using LinkGo.Base.Pool;
using System.Collections.Generic;

/// <summary>
/// 说明：用于BetterLinkedList，链表节点
/// </summary>
namespace LinkGo.Base.Collections
{
    public sealed class BetterLinkedListNodeData<T>
    {
        public BetterLinkedListNodeData(BetterLinkedListNode<T> holder, T value)
        {
            Holder = holder;
            Value = value;
        }
        
        public T Value { get; set; }

        public BetterLinkedListNode<T> Holder { get; set; }
    }

    public sealed class BetterLinkedListNode<T>
    {
        static ObjectPool<BetterLinkedListNode<T>> s_cachePool = new ObjectPool<BetterLinkedListNode<T>>(8);

        LinkedListNode<BetterLinkedListNodeData<T>> mNode;

        public BetterLinkedListNode()
        {
        }

        static public BetterLinkedListNode<T> Get()
        {
            BetterLinkedListNode<T> node = s_cachePool.New();
            if (node != null)
            {
                node.List = default(BetterLinkedList<T>);
                node.Value = default(T);
            }
            return node;
        }

        public void Release()
        {
            s_cachePool.Store(this);
        }
        
        public void InitInfo(BetterLinkedList<T> list, T value)
        {
            List = list;
            Value = value;
        }

        public BetterLinkedList<T> List
        {
            get;
            set;
        }

        public BetterLinkedListNode<T> Next
        {
            get
            {
                if (mNode != null && mNode.Next != null && mNode.Next.Value != null)
                {
                    return mNode.Next.Value.Holder;
                }
                return null;
            }
        }

        public BetterLinkedListNode<T> Previous
        {
            get
            {
                if (mNode != null && mNode.Previous != null && mNode.Previous.Value != null)
                {
                    return mNode.Previous.Value.Holder;
                }
                return null;
            }
        }

        public T Value
        {
            get
            {
                return (mNode == null || mNode.Value == null) ? default(T) : mNode.Value.Value;
            }
            set
            {
                if (mNode == null)
                {
                    BetterLinkedListNodeData<T> data = new BetterLinkedListNodeData<T>(this, value);
                    mNode = new LinkedListNode<BetterLinkedListNodeData<T>>(data);
                }

                if (mNode != null && mNode.Value != null)
                {
                    mNode.Value.Value = value;
                }
            }
        }

        public LinkedListNode<BetterLinkedListNodeData<T>> Node
        {
            get
            {
                return mNode;
            }
        }
    }
}