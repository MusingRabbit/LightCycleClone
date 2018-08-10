using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone.DataStructures
{
    public class QuadTree<T>
    {
        private QuadNode<T> _root;

        public QuadTree()
        {
        }

        public void SetRoot(T value)
        {
            _root = new QuadNode<T>(value);
        }

        public void Push(int depth, int idx, T value)
        {
            
        }

        public void AddNode(QuadNode<T> node)
        {
            if (_root == null)
            {
                _root = node;
            }
            else
            {

            }
        }
    }
}
