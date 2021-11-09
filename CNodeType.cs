using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalc
{
    public abstract class CNodeType<T>
    {
        private T m_nodeType;

        protected CNodeType(T nodeType)
        {
            m_nodeType = nodeType;
        }

        public abstract T Map(int type);
        public abstract T Map(T type);
        public abstract T NA();
        public abstract T Default();

    }
}
