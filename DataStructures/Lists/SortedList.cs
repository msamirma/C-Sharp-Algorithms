namespace DataStructures.Lists
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
        
    internal interface INodeFinder<T>
    where T : IComparable<T>
    {
        SortedListNode<T> FindNode(SortedListNode<T> head, T data, ref int level);

        void AddNode(SortedListNode<T> parent, T data, ref int level);
    }

    internal interface INodeTraversal<out T>
        where T : IComparable<T>
    {
        T[] Nodes { get; }
    }

    public class SortedList<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private readonly object locker = new object();

        private SortedListNode<T> head;

        private int count;

        private int level;
        
        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var items = new PreOrderTraversal<T>(this.head, this.count).Nodes;

            return ((IEnumerable<T>)items).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new PreOrderTraversal<T>(this.head, this.count).Nodes.GetEnumerator();
        }

        public void Add(T data)
        {
            lock (this.locker)
            {
                if (this.head == null)
                {
                    this.head = new SortedListNode<T>(data);
                    this.count++;
                }
                else
                {
                    this.InternalAdd(data);
                    this.count++;   

                    if (this.count == 2)
                    {                        
                        var temp = this.head.RightNode;
                        temp.ParentNode = null;
                        temp.LeftNode = this.head;
                        this.head.ParentNode = temp;
                        this.head.RightNode = null;
                        this.head = temp;
                    }
                    
                    if (this.count > 2)
                    {                                                                      
                        var comparison = this.head.Data.CompareTo(data);
                        if (comparison > 0)
                        {
                            if (this.head.HasLeftNode)
                            {                                                               
                                var temp = this.head.LeftNode;
                                this.head.ParentNode = temp;
                                temp.ParentNode = null;

                                var tempPointer = temp;
                                while (tempPointer.HasRightNode)
                                {
                                    tempPointer = tempPointer.RightNode;
                                }

                                this.head.LeftNode = null;
                                tempPointer.RightNode = this.head;
                                this.head = temp;
                            }                                                           
                        }
                        else if (comparison < 0)
                        {
                            if (this.head.HasRightNode)
                            {                                
                                var temp = this.head.RightNode;
                                this.head.ParentNode = temp;
                                temp.ParentNode = null;

                                var tempPointer = temp;
                                while (tempPointer.HasLeftNode)
                                {
                                    tempPointer = tempPointer.LeftNode;
                                }

                                this.head.RightNode = null;
                                tempPointer.LeftNode = this.head;
                                this.head = temp;                                
                            }                                                       
                        }
                    }
                }                
            }            
        }

        public void AddRange(T[] items)
        {
            // todo impl this
        }
        
        public T Get(T data)
        {
            return default(T);
        }

        public void Remove(T data)
        {
            // todo: implement remove
            this.count--;
        }
        
        private void InternalAdd(T data)
        {
            new InorderNodeFinder<T>().AddNode(this.head, data, ref this.level);            
        }    
    }

    public class SortedListNode<T> : IComparable<T>
        where T : IComparable<T>
    {
        private readonly List<T> internalData = new List<T>();

        private SortedListNode<T> leftNode;

        private SortedListNode<T> rightNode;

        public SortedListNode<T> ParentNode;        

        public SortedListNode(T data)
        {
            this.internalData.Add(data);            
            this.leftNode = null;
            this.rightNode = null;
        }
       
        public bool HasLeftNode
        {
            get
            {
                return this.leftNode != null;
            }
        }

        public SortedListNode<T> LeftNode
        {
            get
            {
                return this.leftNode;
            }
            set
            {
                this.leftNode = value;
            }
        }

        public SortedListNode<T> RightNode
        {
            get
            {
                return this.rightNode;
            }
            set
            {
                this.rightNode = value;
            }
        }

        public bool HasRightNode
        {
            get
            {
                return this.rightNode != null;
            }
        }

        public T Data
        {
            get
            {
                if (this.internalData != null)
                {
                    return this.internalData.First();
                }

                return default(T);
            }
            set
            {               
                this.internalData.Add(value);
            }
        }

        public T[] Datas
        {
            get
            {
                return this.internalData.ToArray();                
            }   
        }

        public int CompareTo(T other)
        {
            if (other == null)
            {
                return -1;
            }

            return other.CompareTo(this.Data);
        }
    }

    internal class InorderNodeFinder<T> : INodeFinder<T>
        where T : IComparable<T>
    {
        public SortedListNode<T> FindNode(SortedListNode<T> head, T data, ref int level)
        {        
            // todo: use itiration vs recursion

            if (head == null)
            {
                level++;
                return new SortedListNode<T>(data);
            }

            var comparison = head.Data.CompareTo(data);
            if (comparison < 0)
            {
                // go right
                if (head.HasRightNode)
                {
                    level++;
                    return FindNode(head.RightNode, data, ref level);
                }                
            }
            else if (comparison > 0)
            {
                // go left
                if (head.HasLeftNode)
                {
                    level++;
                    return FindNode(head.LeftNode, data, ref level);
                }                
            }
            
            return head;                          
        }

        public void AddNode(SortedListNode<T> parent, T data, ref int level)
        {
            // todo: rename local head var
            var head = this.FindNode(parent, data, ref level);
            var comparison = head.Data.CompareTo(data);
            var newNode = new SortedListNode<T>(data);
            
            if (comparison > 0)
            {                
                head.LeftNode = newNode;
                newNode.ParentNode = head;
            }
            else if (comparison < 0)
            {
                head.RightNode = newNode;
                newNode.ParentNode = head;
            }
            else
            {
                head.Data = data;
            }
        }
    }

    internal class PreOrderTraversal<T> : INodeTraversal<T>
        where T : IComparable<T>
    {
        private readonly T[] nodes;
        private readonly SortedListNode<T> head;
        private readonly object locker = new object();
        private int index;
       
        public PreOrderTraversal(SortedListNode<T> head, int count)
        {
            this.head = head;
            this.nodes = new T[count];
        }

        public T[] Nodes
        {
            get
            {
                this.Traverse(this.head);
                return this.nodes;
            }
        }

        private void Traverse(SortedListNode<T> currNode)
        {
            if (currNode == null)
            {
                return;
            }        
            
            lock (this.locker)
            {
                if (currNode.HasLeftNode)
                {
                    this.Traverse(currNode.LeftNode);
                }
                
                foreach (var data in currNode.Datas)
                {
                    this.nodes[this.index] = data;
                    this.index++;
                }                

                if (currNode.HasRightNode)
                {                    
                    this.Traverse(currNode.RightNode);
                }                
            }
        }        
    }

    #region Not Yet Implemented (Pre & Post Order Traversal)

    internal class InOrderNodeFinder<T> : INodeFinder<T>
        where T : IComparable<T>
    {
        public SortedListNode<T> FindNode(SortedListNode<T> head, T data, ref int level)
        {
            throw new NotImplementedException();
        }

        public void AddNode(SortedListNode<T> parent, T data, ref int level)
        {
        }
    }

    internal class PostOrderNodeFinder<T> : INodeFinder<T>
        where T : IComparable<T>
    {
        public SortedListNode<T> FindNode(SortedListNode<T> head, T data, ref int level)
        {
            throw new NotImplementedException();
        }

        public void AddNode(SortedListNode<T> parent, T data, ref int level)
        {
        }
    }

    #endregion
}
