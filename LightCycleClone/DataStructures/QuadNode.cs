using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone.DataStructures
{
    public class QuadNode<T>
    {
        private T _data;
        private QuadNode<T>[] _nodes;

        public QuadNode<T>[] Children
        {
            get
            {
                return _nodes;
            }
        }

        public T Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        public QuadNode(T data)
        {
            _nodes = new QuadNode<T>[4];
            _data = data;
        }




    }
}
