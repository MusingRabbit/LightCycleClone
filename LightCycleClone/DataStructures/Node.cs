using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone.DataStructures
{
    public class Node<T> : IDisposable
    {
        private T _data;
        private List<Node<T>> _children;
        private Node<T> _parent;

        public List<Node<T>> Children
        {
            get
            {
                return _children;
            }
        }

        public T Data
        {
            get
            {
                return _data;
            }
        }

        public Node<T> Parent
        {
            get
            {
                return _parent;
            }
        }

        public Node()
        {
            _children = new List<Node<T>>(4);
        }

        public Node(T data)
            :this()
        {
            _data = data;
        }

        public Node(T data, Node<T> parentNode)
            : this(data)
        {
            _parent = parentNode;
        }

        public void Push(T data)
        {
            var newNode = new Node<T>(data, this);
            _children.Add(newNode);
        }

        public void AddChild(Node<T> value)
        {
            value._parent = this;
            _children.Add(value);
        }

        public List<Node<T>> Query(Func<T, bool> func)
        {
            var result = new List<Node<T>>();

            if (func.Invoke(_data))
            {
                result.Add(this);
            }

            foreach (var child in _children)
            {
                result.AddRange(child.Query(func));
            }

            return result;
        }

        public List<Node<T>> GetAll()
        {
            var result = new List<Node<T>>();

            result.AddRange(_children);

            foreach (var child in _children)
            {
                result.AddRange(child.GetAll());
            }

            return result;
        }

        public void Dispose()
        {
            foreach (var child in _children)
            {
                child.Dispose();
            }

            _children.Clear();
            _data = default(T);
        }
    }
}
