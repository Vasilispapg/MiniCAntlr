using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniC;

namespace MiniC
{

    public class ASTElementChildrenEnumerator : IEnumerator<ASTElement>
    {
        private int m_currentContext;
        private int m_currentChildIndex;
        private ASTElement m_currentChild;//to paidi
        private ASTElement m_currentNode; //aytos poy diatrexoyme

        public ASTElement Current => m_currentChild;

        public ASTElementChildrenEnumerator(ASTElement mCurrentNode)
        {
            m_currentNode = mCurrentNode;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            m_currentContext++;
            if (m_currentChildIndex == m_currentNode.getContextChildrenNumber(m_currentContext))
            {
                //we found the last child and move to next node
                if (m_currentContext+1 == m_currentNode.getContextNumber())
                {
                    return false; //we reach the end
                }
                else
                {
                    m_currentContext++;
                    while (m_currentNode.getContextChildrenNumber(m_currentContext) == 0
                           && m_currentNode.getContextNumber() > m_currentContext)
                    {
                        //oso einai adeia kai oso den eimaste sto
                        m_currentContext++;
                    }

                    if (m_currentContext == m_currentNode.getContextNumber())
                    {
                        return false; //we reach the end
                    }
                    m_currentChildIndex = 0;
                    m_currentChild = m_currentNode.GetChild(m_currentContext, m_currentChildIndex);
                    return true;

                }
            }

            //eimaste kapoy endiamesa sta paidia
            m_currentChildIndex++;
            m_currentChildIndex = 0;
            m_currentChild = m_currentNode.GetChild(m_currentContext, m_currentChildIndex);
            return true;
            //sto telos epistrefei false alliws true
        }

        public void Reset()//sto stoixeio prin to prwto deiktis sto -1
        {
            //init
            m_currentChildIndex = -1;
            m_currentContext = -1;
            m_currentChild = null;
            //den deixnei tipota stin arxi
        }

        object IEnumerator.Current => Current;

    }

    public enum NodeType
    {
        NT_NA, NT_COMPILEUNIT, NT_ASSIGNMENT, NT_ADDITION, NT_LASTEXPR,
        NT_SUBSTRACTION, NT_MULTIPLICATION, NT_DIVISION, NT_NUMBER,
        NT_VARIABLE, NT_EQUAL, NT_NEQUAL, NT_AND, NT_OR, NT_LT, NT_LTE,
        NT_GT, NT_GTE, NT_FUNCTION_DEF, NT_FUNCTION_CALL, NT_COUMPOUNTSTATEMENT,
        NT_IFSTATEMENT, NT_WHILESTATEMENT, NT_RETURN, NT_BREAK, NT_PLUSPLUS, NT_FORSTATEMENT,
        NT_DOWHILESTATEMENT, NT_NOTOPERATOR,NT_STATEMENT,NT_PRINT
    }

    public abstract class ASTElement : IEnumerable<ASTElement>
    {
        private List<ASTElement>[] m_children = null;
        private ASTElement m_parent;
        private int m_serial;
        private string m_name;
        private static int m_serialCounter = 0;
        private NodeType m_type;

        public NodeType Type
        {
            get => m_type;
        }

        public int getContextChildrenNumber(int context)
        {
            if (m_children != null && m_children.Length > context)
            {
                return m_children[context].Count;
            }
            else if (m_children != null && m_children.Length <= context)
            {
                throw new IndexOutOfRangeException("Index out of context's array range for the current node!\n");
            }
            else
            {
                return 0;
            }
        }
        public int getContextNumber()
        {
            return m_children != null ? m_children.Length : 0;
        }

        public ASTElement MParent
        {
            get => m_parent;
            set => m_parent = value;
        }

        public virtual string MName => m_name;

        protected ASTElement(int context, NodeType type)
        {
            m_type = type;
            m_serial = m_serialCounter++;
            m_name = GenerateNodeName();
            if (context != 0)
            {
                m_children = new List<ASTElement>[context];
                for (int i = 0; i < context; i++)
                {
                    m_children[i] = new List<ASTElement>();
                }
            }
        }

        public IEnumerable<ASTElement> GetChildren(int context)
        {
            return m_children[context];
        }


        public void AddChild(ASTElement child, int contextIndex)
        {
            m_children[contextIndex].Add(child);
            //theloyme se poio child eimaste kai se poio context to vazoyme
        }

        public ASTElement GetChild(int context,int index)
        {
            return m_children[context][index];
        }

        //epistrefw apo methodo iterator, ara mporei na xrisimopoiithei se mia foreach
        public IEnumerable<ASTElement> GetContextChildren(int context)
        {
            foreach (ASTElement c in m_children[context])//returns a list
            {
                yield return c;//returns iterator
            }
        }

        public virtual string GenerateNodeName()
        {
            return Enum.GetName(typeof(NodeType), Type)+ "_" + m_serial;
        }

        //gia tin foreach na ylopoiei ienumurable inteface ^^
        //ftiaxnei kai ena collection taytoxrona twra
        public IEnumerator<ASTElement> GetEnumerator()
        {
            return new ASTElementChildrenEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract T Accept<T>(ASTBaseVisitor<T> visitor);

    }
}
