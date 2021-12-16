using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniC
{
    public abstract class ASTBaseVisitor <T>
    {
        public virtual T Visit(ASTElement node)
        {
            return node.Accept(this); //to default gyrnaei tin deafult timi toy komvoy gia int->0 gia float->0.
        }

        public virtual T VisitChildren(ASTElement node)
        {
            for (int i = 0; i < node.getContextNumber(); i++)
            {
                foreach (ASTElement child in node.GetChildren(i))
                {
                    Visit(child);
                }
            }
            return default(T);
        }
    }

    public class MiniCASTBaseVisiton<T> : ASTBaseVisitor<T>
    {
        public virtual T VisitCompileUnit(CCompileUnit node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitAssignment(CAssignment node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitIf(CIfstatement node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitWhile(CWhilestatement node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitReturn(CReturn node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitBreak(CBreak node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitPlusPlus(CPlusplus node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitFuncDef(CFunctiondef node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitFuncCall(CFunctioncall node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitAddition(CAddition node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitSub(CSubstraction node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitMult(CMultiplication node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitDiv(CDivision node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitVARIABLE(CVARIABLE node)
        {
            return default(T);
        }
        public virtual T VisitEqual(CEqual node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitNequal(CNequal node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitAnd(CAnd node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitOr(COr node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitLte(CLte node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitLt(CLt node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitGte(CGte node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitGt(CGt node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitNot(CNot node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitFor(CFor node)
        {
            return VisitChildren(node);
        }
        public virtual T VisitDoWhile(CDowhile node)
        {
            return VisitChildren(node);
        }
    }
}
