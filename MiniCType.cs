using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniC
{

    public class CCompileUnit : ASTElement
    {
        public static readonly string[] msc_contextNames = { "statementContext", "functionDefinitionContext" };
        public const int CT_STATEMENTS = 0,CT_FUNCDEF=1;
        public CCompileUnit() : base(2, NodeType.NT_COMPILEUNIT)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            //kalei tin visit toy trexonta komvou
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitCompileUnit(this);//dineis to trexonta komvo gia na epejergastei
        }
    }
    public class CAssignment : ASTElement
    {
        public static readonly string[] msc_contextNames = { "leftContext", "rightContext" };

        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CAssignment() : base(2, NodeType.NT_ASSIGNMENT)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitAssignment(this);//dineis to trexonta komvo gia na epejergastei
        }
    }
    public class CIfstatement : ASTElement
    {
        public static readonly string[] msc_contextNames = { "exprContext", "ifbodyContext","elsebodyContext" };
        public const int CT_EXPR = 0, CT_IFBODY= 1, CT_ELSEBODY= 2;
        public CIfstatement() : base(3, NodeType.NT_IFSTATEMENT)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitIf(this);//dineis to trexonta komvo gia na epejergastei
        }
    }
    public class CWhilestatement : ASTElement
    {
        public static readonly string[] msc_contextNames = { "exprContext", "compoundStContext" };
        public const int CT_EXPR = 0, CT_COMPOUNTSTATEMENT = 1;
        public CWhilestatement() : base(2, NodeType.NT_WHILESTATEMENT)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitWhile(this);//dineis to trexonta komvo gia na epejergastei
        }
    }
    public class CReturn : ASTElement
    {
        public static readonly string[] msc_contextNames = { "exprContext"};

        public const int CT_EXPR = 0;
        public CReturn() : base(1, NodeType.NT_RETURN)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitReturn(this);//dineis to trexonta komvo gia na epejergastei
        }
    }

    public class CBreak : ASTElement
    {
        public CBreak() : base(0, NodeType.NT_BREAK)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitBreak(this);//dineis to trexonta komvo gia na epejergastei
        }
    }
    public class CPlusplus : ASTElement
    {
        public static readonly string[] msc_contextNames = { "leftContext"};
        public const int CT_LEFT = 0;
        public CPlusplus() : base(1, NodeType.NT_PLUSPLUS)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitPlusPlus(this);//dineis to trexonta komvo gia na epejergastei
        }
    }
    public class CFunctiondef : ASTElement
    {
        public static readonly string[] msc_contextNames = { "nameContext", "argsContext","compoundStContext" };
        public const int CT_NAME = 0, CT_ARGS = 1,CT_COMPSTATEMENT=2;//TODO edw thelei ena fix to context
        public CFunctiondef() : base(3, NodeType.NT_FUNCTION_DEF)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitFuncDef(this);//dineis to trexonta komvo gia na epejergastei
        }
    }
    public class CFunctioncall : ASTElement
    {
        public static readonly string[] msc_contextNames = { "nameContext", "argsContext" };
        public const int CT_NAME=0,CT_ARGS = 1;
        public CFunctioncall() : base(2, NodeType.NT_FUNCTION_CALL)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitFuncCall(this);//dineis to trexonta komvo gia na epejergastei
        }
    }
    public class CAddition : ASTElement
    {
        public static readonly string[] msc_contextNames = { "leftContext", "rightContext" };
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CAddition() : base(2, NodeType.NT_ADDITION)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitAddition(this);//dineis to trexonta komvo gia na epejergastei
        }
    }
    public class CSubstraction : ASTElement
    {
        public static readonly string[] msc_contextNames = { "leftContext", "rightContext" };
        public const int CT_LEFT = 0, CT_RIGHT = 1;

        public CSubstraction() : base(2, NodeType.NT_SUBSTRACTION)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitSub(this); //dineis to trexonta komvo gia na epejergastei        }
        }
    }
    public class CMultiplication : ASTElement
    {
        public static readonly string[] msc_contextNames = { "leftContext", "rightContext" };
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CMultiplication() : base(2, NodeType.NT_MULTIPLICATION)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitMult(this);//dineis to trexonta komvo gia na epejergastei
        }
    }
    public class CDivision : ASTElement
    {
        public static readonly string[] msc_contextNames = { "leftContext", "rightContext" };
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CDivision() : base(2, NodeType.NT_DIVISION)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitDiv(this);//dineis to trexonta komvo gia na epejergastei
        }
    }
    public class CVARIABLE : ASTElement
    {
        private string m_name;

        public string MName1 => m_name;

        public CVARIABLE(string name) : base(0, NodeType.NT_VARIABLE)
        {
            m_name = name;
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitVARIABLE(this);//dineis to trexonta komvo gia na epejergastei
        }
    }
    public class CEqual : ASTElement
    {
        public static readonly string[] msc_contextNames = { "leftContext", "rightContext" };
        public const int CT_LEFT = 0, CT_RIGHT = 1;

        public CEqual() : base(2, NodeType.NT_EQUAL)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitEqual(this); //dineis to trexonta komvo gia na epejergastei        }
        }
    }
    public class CNequal : ASTElement
    {
        public static readonly string[] msc_contextNames = { "leftContext", "rightContext" };
        public const int CT_LEFT = 0, CT_RIGHT = 1;

        public CNequal() : base(2, NodeType.NT_NEQUAL)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitNequal(this); //dineis to trexonta komvo gia na epejergastei        }
        }
    }
    public class CAnd : ASTElement
    {
        public static readonly string[] msc_contextNames = { "leftContext", "rightContext" };
        public const int CT_LEFT = 0, CT_RIGHT = 1;

        public CAnd() : base(2, NodeType.NT_AND)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitAnd(this); //dineis to trexonta komvo gia na epejergastei        }
        }
    }
    public class COr : ASTElement
    {
        public static readonly string[] msc_contextNames = { "leftContext", "rightContext" };
        public const int CT_LEFT = 0, CT_RIGHT = 1;

        public COr() : base(2, NodeType.NT_OR)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitOr(this); //dineis to trexonta komvo gia na epejergastei        }
        }
    }
    public class CLte : ASTElement
    {
        public static readonly string[] msc_contextNames = { "leftContext", "rightContext" };
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CLte() : base(2, NodeType.NT_LTE)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitLte(this);//dineis to trexonta komvo gia na epejergastei
        }
    }
    public class CLt : ASTElement
    {
        public static readonly string[] msc_contextNames = { "leftContext", "rightContext" };
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CLt() : base(2, NodeType.NT_LT)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitLt(this);//dineis to trexonta komvo gia na epejergastei
        }
    }
    public class CGt : ASTElement
    {
        public static readonly string[] msc_contextNames = { "leftContext", "rightContext" };
        public const int CT_LEFT = 0, CT_RIGHT = 1;
        public CGt() : base(2, NodeType.NT_GT)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitGt(this);//dineis to trexonta komvo gia na epejergastei
        }
    }
    public class CGte : ASTElement
    {
        public static readonly string[] msc_contextNames = { "leftContext", "rightContext" };
        public const int CT_LEFT = 0, CT_RIGHT = 1;

        public CGte() : base(2, NodeType.NT_GTE)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitGte(this); //dineis to trexonta komvo gia na epejergastei        }
        }
    }
    public class CNot : ASTElement
    {
        public static readonly string[] msc_contextNames = { "rightContext" };
        public const int CT_RIGHT = 0;
        public CNot() : base(1, NodeType.NT_NOTOPERATOR)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitNot(this);//dineis to trexonta komvo gia na epejergastei
        }
    }
    public class CFor : ASTElement
    {
        public static readonly string[] msc_contextNames = { "initContext", "finalContext","stepContext","bodyContext" };
        public const int CT_INIT = 0, CT_FINAL = 1,CT_STEP=2,CT_BODY=3;
        public CFor() : base(4, NodeType.NT_FORSTATEMENT)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitFor(this);//dineis to trexonta komvo gia na epejergastei
        }
    }
    public class CDowhile : ASTElement
    {
        public static readonly string[] msc_contextNames = { "exprContext", "compoundStContext" };
        public const int CT_EXPR = 1, CT_COMPSTATEMENT = 0;

        public CDowhile() : base(2, NodeType.NT_DOWHILESTATEMENT)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            MiniCASTBaseVisiton<T> MiniCVisitor = visitor as MiniCASTBaseVisiton<T>;
            return MiniCVisitor.VisitDoWhile(this); //dineis to trexonta komvo gia na epejergastei        }
        }
    }
}
