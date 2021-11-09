using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleCalc;

namespace MiniC
{
    public class MiniCType : CNodeType<MiniCType.NodeType>
    {
        public enum NodeType
        {
            NT_NA, NT_COMPILEUNIT, NT_ASSIGNMENT, NT_ADDITION, NT_LASTEXPR,
            NT_SUBSTRACTION, NT_MULTIPLICATION, NT_DIVISION, NT_NUMBER,
            NT_VARIABLE, NT_EQUAL, NT_NEQUAL, NT_AND, NT_OR, NT_LT, NT_LTE,
            NT_GT, NT_GTE, NT_PARAMETERS, NT_FUNCTION_DEF, NT_FUNCTION_CALL, NT_COUMPOUNTSTATEMENT,
            NT_IFSTATEMENT, NT_WHILESTATEMENT, NT_RETURN, NT_BREAK, NT_PLUSPLUS
        }

        public MiniCType(NodeType nodeType) : base(nodeType)
        {
        }

        public override NodeType Map(NodeType type)
        {
            return (NodeType)type;
        }
        public override NodeType Map(int type)
        {
            return (NodeType)type;
        }
        public override NodeType NA()
        {
            return NodeType.NT_NA;
        }
        public override NodeType Default()
        {
            return NodeType.NT_NA;
        }
    }
    public abstract class MiniCASTElement : ASTElement
    {
        private MiniCType m_nodeType;

        protected MiniCASTElement(int context, MiniCType.NodeType Type) : base(context)
        {
            /*
             context gia ta paidia
             kai typos kovmou
             */
            m_nodeType = new MiniCType(Type); //arxikopoiisi type
        }


    }
    public class CCompileUnit : MiniCASTElement
    {
        public const int CT_BODY = 0;
        public CCompileUnit() : base(1, MiniCType.NodeType.NT_COMPILEUNIT)
        {
        }
    }
    public class CAssignment : MiniCASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CAssignment() : base(2, MiniCType.NodeType.NT_ASSIGNMENT)
        {
        }
    }
    public class CIfstatement : MiniCASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CIfstatement() : base(2, MiniCType.NodeType.NT_IFSTATEMENT)
        {
        }
    }
    public class CWhilestatement : MiniCASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CWhilestatement() : base(2, MiniCType.NodeType.NT_WHILESTATEMENT)
        {
        }
    }
    public class CReturn : MiniCASTElement
    {
        public const int CT_EXPR = 0;
        public CReturn() : base(1, MiniCType.NodeType.NT_RETURN)
        {
        }
    }
    public class CBreak : MiniCASTElement
    {
        public CBreak() : base(0, MiniCType.NodeType.NT_BREAK)
        {
        }
    }
    public class CPlusplus : MiniCASTElement
    {
        public const int CT_LEFT = 0;
        public CPlusplus() : base(1, MiniCType.NodeType.NT_PLUSPLUS)
        {
        }
    }
    public class CFunctiondef : MiniCASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;//TODO edw thelei ena fix to context
        public CFunctiondef() : base(2, MiniCType.NodeType.NT_FUNCTION_DEF)
        {
        }
    }
    public class CFunctioncall : MiniCASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;//TODO edw thelei ena fix to context
        public CFunctioncall() : base(2, MiniCType.NodeType.NT_FUNCTION_CALL)
        {
        }
    }
    public class CAddition : MiniCASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CAddition() : base(2, MiniCType.NodeType.NT_ADDITION)
        {
        }
    }
    public class CSubstraction : MiniCASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CSubstraction() : base(2, MiniCType.NodeType.NT_SUBSTRACTION)
        {
        }
    }
    public class CMultiplication : MiniCASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CMultiplication() : base(2, MiniCType.NodeType.NT_MULTIPLICATION)
        {
        }
    }
    public class CDivision : MiniCASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CDivision() : base(2, MiniCType.NodeType.NT_DIVISION)
        {
        }
    }
    public class CNUMBER : MiniCASTElement
    {
        public CNUMBER() : base(0, MiniCType.NodeType.NT_NUMBER)
        {
        }
    }
    public class CVARIABLE : MiniCASTElement
    {
        public CVARIABLE() : base(0, MiniCType.NodeType.NT_VARIABLE)
        {
        }
    }
    public class CLASTExpr : MiniCASTElement
    {
        public CLASTExpr() : base(0, MiniCType.NodeType.NT_LASTEXPR)
        {
        }
    }
    public class CEqual : MiniCASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CEqual() : base(2, MiniCType.NodeType.NT_EQUAL)
        {
        }
    }
    public class CNequal : MiniCASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CNequal() : base(2, MiniCType.NodeType.NT_NEQUAL)
        {
        }
    }
    public class CAnd : MiniCASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CAnd() : base(2, MiniCType.NodeType.NT_AND)
        {
        }
    }
    public class COr : MiniCASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public COr() : base(2, MiniCType.NodeType.NT_OR)
        {
        }
    }
    public class CLte : MiniCASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CLte() : base(2, MiniCType.NodeType.NT_LTE)
        {
        }
    }
    public class CLt : MiniCASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CLt() : base(2, MiniCType.NodeType.NT_LT)
        {
        }
    }
    public class CGt : MiniCASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CGt() : base(2, MiniCType.NodeType.NT_GT)
        {
        }
    }
    public class CGte : MiniCASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CGte() : base(2, MiniCType.NodeType.NT_GTE)
        {
        }
    }

}
