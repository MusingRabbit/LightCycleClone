using LightCycleClone.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone.DataStructures
{
    public class EvalNode : Node<EvalData>
    {
        public EvalNode()
            :base()
        {

        }

        public EvalNode(EvalData data) 
            : base(data)
        {
        }

        public EvalNode(EvalData data, Node<EvalData> parentNode) 
            : base(data, parentNode)
        {
        }
    }
}
