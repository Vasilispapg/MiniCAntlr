using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Tree;

namespace MiniC
{
    public class ASTGenerator  : MiniCBaseVisitor<int>
    {
        private ASTElement m_root;

        public ASTElement MRoot => m_root;

        private Stack<(ASTElement, int)> m_contextData = new Stack<(ASTElement, int)>();

        public override int VisitReturn(MiniCParser.ReturnContext context)
        {
            CReturn node = new CReturn();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CReturn.CT_EXPR));
            Visit(context.expr());
            m_contextData.Pop();

            return 0;
        }

        public override int VisitBreak(MiniCParser.BreakContext context)
        {
            CBreak node = new CBreak();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            return 0;
        }

        public override int VisitOrOperator(MiniCParser.OrOperatorContext context)
        {
            COr node = new COr();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, COr.CT_LEFT));
            Visit(context.expr(0));
            m_contextData.Pop();

            m_contextData.Push((node, COr.CT_RIGHT));
            Visit(context.expr(1));
            m_contextData.Pop();

            return 0;
        }

        public override int VisitAssignment(MiniCParser.AssignmentContext context)
        {
            CAssignment node = new CAssignment();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CAssignment.CT_LEFT));
            Visit(context.VARIABLE());
            m_contextData.Pop();

            m_contextData.Push((node, CAssignment.CT_RIGHT));
            Visit(context.expr());
            m_contextData.Pop();

            return 0;
        }

        public override int VisitAndOperator(MiniCParser.AndOperatorContext context)
        {
            CAnd node = new CAnd();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CAnd.CT_LEFT));
            Visit(context.expr(0));
            m_contextData.Pop();

            m_contextData.Push((node, CAnd.CT_RIGHT));
            Visit(context.expr(1));
            m_contextData.Pop();

            return 0;
        }

        public override int VisitAdd_sub(MiniCParser.Add_subContext context)
        {
            (ASTElement, int) parent_data;
            switch (context.op.Type)
            {
                case MiniCLexer.PLUS:
                    CAddition plusNode = new CAddition();
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(plusNode, parent_data.Item2);
                    plusNode.MParent = parent_data.Item1;

                    m_contextData.Push((plusNode, CAddition.CT_LEFT));
                    Visit(context.expr(0));
                    m_contextData.Pop();

                    m_contextData.Push((plusNode, CAddition.CT_RIGHT));
                    Visit(context.expr(1));
                    m_contextData.Pop();

                    break;
                case MiniCLexer.MINUS:
                    CSubstraction minusNode = new CSubstraction();
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(minusNode, parent_data.Item2);

                    m_contextData.Push((minusNode, CSubstraction.CT_LEFT));
                    Visit(context.expr(0));
                    m_contextData.Pop();

                    m_contextData.Push((minusNode, CSubstraction.CT_RIGHT));
                    Visit(context.expr(1));
                    m_contextData.Pop();

                    break;
            }
            return 0;
        }

        public override int VisitNotOperator(MiniCParser.NotOperatorContext context)
        {
            CNot node = new CNot();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CNot.CT_RIGHT));
            Visit(context.expr());
            m_contextData.Pop();

            return 0;
        }

        public override int VisitMult_div(MiniCParser.Mult_divContext context)
        {
            (ASTElement, int) parent_data;
            switch (context.op.Type)
            {
                case MiniCLexer.MULT:
                    CMultiplication multNode = new CMultiplication();
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(multNode, parent_data.Item2);
                    multNode.MParent = parent_data.Item1;

                    m_contextData.Push((multNode, CMultiplication.CT_LEFT));
                    Visit(context.expr(0));
                    m_contextData.Pop();

                    m_contextData.Push((multNode, CMultiplication.CT_RIGHT));
                    Visit(context.expr(1));
                    m_contextData.Pop();

                    break;
                case MiniCLexer.DIV:
                    CDivision divNode = new CDivision();
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(divNode, parent_data.Item2);

                    m_contextData.Push((divNode, CDivision.CT_LEFT));
                    Visit(context.expr(0));
                    m_contextData.Pop();

                    m_contextData.Push((divNode, CDivision.CT_RIGHT));
                    Visit(context.expr(1));
                    m_contextData.Pop();

                    break;
            }
            return 0;
        }

        public override int VisitPlusplusOperator(MiniCParser.PlusplusOperatorContext context)
        {
            CPlusplus node = new CPlusplus();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CPlusplus.CT_LEFT));
            Visit(context.expr());
            m_contextData.Pop();

            return 0;
        }

        public override int VisitEqualNotOperator(MiniCParser.EqualNotOperatorContext context)
        {
            (ASTElement, int) parent_data;
            switch (context.op.Type)
            {
                case MiniCLexer.EQUAL:
                    CEqual eqNode = new CEqual();
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(eqNode, parent_data.Item2);
                    eqNode.MParent = parent_data.Item1;

                    m_contextData.Push((eqNode,CEqual.CT_LEFT));
                    Visit(context.expr(0));
                    m_contextData.Pop();

                    m_contextData.Push((eqNode, CEqual.CT_RIGHT));
                    Visit(context.expr(1));
                    m_contextData.Pop();

                    break;
               case MiniCLexer.NEQUAL:
                    CNequal neqNode = new CNequal();
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(neqNode, parent_data.Item2);
                    neqNode.MParent = parent_data.Item1;

                    m_contextData.Push((neqNode, CNequal.CT_LEFT));
                    Visit(context.expr(0));
                    m_contextData.Pop();

                    m_contextData.Push((neqNode, CNequal.CT_RIGHT));
                    Visit(context.expr(1));
                    m_contextData.Pop();

                    break;
            }
            return 0;
        }

        public override int VisitCompileUnit(MiniCParser.CompileUnitContext context)
        {
            /*  eimaste sto compile unit me 2 context
                tha paei se ola ta paidia kai ta paidia tha kollisoyn 
                sto compile unit, prepei na pas sta swsta context
                poios prepei na jerei poy tha mpoyn ta paidia
                ta paidia i o pateras; -> o pateras ta paidia
                thelei na steilei pliroforia poios einai o pateras
                kai poy prepei na mpei to paidi
            */

            CCompileUnit cNode = new CCompileUnit();//root
            m_root = cNode;
            //Connect current node to parent
            //CompileUnit doesnt have a parent - Nothing to do!

            (ASTElement, int) parent_data = (m_root,CCompileUnit.CT_STATEMENTS); //Thelei egkatastasi nugget ValueTuple
            //Tuple = anonymus struct
            m_contextData.Push(parent_data);
           
            foreach (var child in context.statement())//me ayto tha trexoyme ta paidia
            {
                Visit(child);
            }
            m_contextData.Pop();

            parent_data = (m_root, CCompileUnit.CT_FUNCDEF);
            m_contextData.Push(parent_data);
            foreach (var child in context.func_deffinition_st())
            {
                Visit(child);
            }
            m_contextData.Pop();

            return 0;
        }

        public override int VisitFor_st(MiniCParser.For_stContext context)
        {
            CFor node = new CFor();
            (ASTElement, int) parent_data = m_contextData.Peek();

            parent_data.Item1.AddChild(node, parent_data.Item2);

            node.MParent = parent_data.Item1;

            m_contextData.Push((node,CFor.CT_INIT));
            Visit(context.expr(0));//init
            m_contextData.Pop();

            m_contextData.Push((node, CFor.CT_FINAL));
            Visit(context.expr(1));//final
            m_contextData.Pop();

            m_contextData.Push((node, CFor.CT_STEP));
            Visit(context.expr(2));//step
            m_contextData.Pop();

            m_contextData.Push((node, CFor.CT_BODY));
            Visit(context.compound_st());//body
            m_contextData.Pop();

            return 0;
        }

        public override int VisitDowhile_st(MiniCParser.Dowhile_stContext context)
        {
            CDowhile node = new CDowhile();
            (ASTElement, int) parent_data = m_contextData.Peek();

            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node,CDowhile.CT_COMPSTATEMENT));
            Visit(context.compound_st());//compount
            m_contextData.Pop();

            m_contextData.Push((node, CDowhile.CT_EXPR));
            Visit(context.expr());//expr
            m_contextData.Pop();
            return 0;
        }

        public override int VisitWhile_st(MiniCParser.While_stContext context)
        {
            CWhilestatement node = new CWhilestatement();
            (ASTElement, int) parent_data = m_contextData.Peek();

            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node,CWhilestatement.CT_EXPR));
            Visit(context.expr());
            m_contextData.Pop();

            m_contextData.Push((node, CWhilestatement.CT_COMPOUNTSTATEMENT));
            Visit(context.compound_st());
            m_contextData.Pop();

            return 0;
        }

        public override int VisitIf_st(MiniCParser.If_stContext context)
        {
            CIfstatement node = new CIfstatement();
            (ASTElement, int) parent_data = m_contextData.Peek();

            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node,CIfstatement.CT_EXPR));
            Visit(context.expr());//expr
            m_contextData.Pop();

            m_contextData.Push((node, CIfstatement.CT_IFBODY));
            Visit(context.compound_st(0));//if
            m_contextData.Pop();

            if (context.compound_st(1) != null)
            {
                m_contextData.Push((node, CIfstatement.CT_ELSEBODY));
                Visit(context.compound_st(1));//else
                m_contextData.Pop();
            }

            return 0;
        }

        public override int VisitFunc_deffinition_st(MiniCParser.Func_deffinition_stContext context)
        {
            CFunctiondef node = new CFunctiondef();//root
            //Connect current node to parent
            //Funf_Def doesnt have a parent - Nothing to do!

            (ASTElement, int) parent_data = m_contextData.Peek(); //pairnei oli tin pliroforia

            parent_data.Item1.AddChild(node, parent_data.Item2); //prwto stoixeio

            node.MParent = parent_data.Item1;

            m_contextData.Push((node,CFunctiondef.CT_NAME));
            Visit(context.VARIABLE());//variable
            m_contextData.Pop();

            m_contextData.Push((node, CFunctiondef.CT_ARGS));
            Visit(context.fargs());//fargs
            m_contextData.Pop();

            m_contextData.Push((node, CFunctiondef.CT_COMPSTATEMENT));
            Visit(context.compound_st());//compound
            m_contextData.Pop();

            return 0;
        }

        public override int VisitFunc_call_st(MiniCParser.Func_call_stContext context)
        {
            CFunctioncall node = new CFunctioncall();//root
            //Connect current node to parent
            //Funf_Def doesnt have a parent - Nothing to do!

            (ASTElement, int) parent_data = m_contextData.Peek(); //pairnei oli tin pliroforia

            parent_data.Item1.AddChild(node, parent_data.Item2);

            node.MParent = parent_data.Item1;

            m_contextData.Push((node,CFunctioncall.CT_NAME));
            Visit(context.VARIABLE());//variable
            m_contextData.Pop();

            m_contextData.Push((node, CFunctioncall.CT_ARGS));
            Visit(context.fargs());//fargs
            m_contextData.Pop();

            return 0;
        }

        public override int VisitTerminal(ITerminalNode node)
        {
            (ASTElement, int) parent_data;
            switch (node.Symbol.Type)
            {
                case MiniCLexer.VARIABLE:
                    CVARIABLE varNode = new CVARIABLE(node.Symbol.Text);
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(varNode,parent_data.Item2);
                    varNode.MParent = parent_data.Item1;
                    break;
                case MiniCLexer.NUM:
                    CVARIABLE numNode = new CVARIABLE(node.Symbol.Text);
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(numNode, parent_data.Item2);
                    numNode.MParent = parent_data.Item1;
                    break;
            }
            return 0;
        }

        public override int VisitOperators(MiniCParser.OperatorsContext context)
        {
            (ASTElement, int) parent_data;
            switch (context.op.Type)
            {
                case MiniCLexer.GTE:
                    CGte nodegte = new CGte();
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(nodegte, parent_data.Item2);
                    nodegte.MParent = parent_data.Item1;

                    m_contextData.Push((nodegte, CGte.CT_LEFT));
                    Visit(context.expr(0));
                    m_contextData.Pop();

                    m_contextData.Push((nodegte, CGte.CT_RIGHT));
                    Visit(context.expr(1));
                    m_contextData.Pop();
                    break;
                case MiniCLexer.GT:
                    CGt nodegt = new CGt();
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(nodegt, parent_data.Item2);
                    nodegt.MParent = parent_data.Item1;

                    m_contextData.Push((nodegt, CGt.CT_LEFT));
                    Visit(context.expr(0));
                    m_contextData.Pop();

                    m_contextData.Push((nodegt, CGt.CT_RIGHT));
                    Visit(context.expr(1));
                    m_contextData.Pop();
                    break;
                case MiniCLexer.LT:
                    CLt nodelt = new CLt();
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(nodelt, parent_data.Item2);
                    nodelt.MParent = parent_data.Item1;

                    m_contextData.Push((nodelt, CLt.CT_LEFT));
                    Visit(context.expr(0));
                    m_contextData.Pop();

                    m_contextData.Push((nodelt, CLt.CT_RIGHT));
                    Visit(context.expr(1));
                    m_contextData.Pop();
                    break;
                case MiniCLexer.LTE:
                    CLte nodelte = new CLte();
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(nodelte, parent_data.Item2);
                    nodelte.MParent = parent_data.Item1;

                    m_contextData.Push((nodelte, CLte.CT_LEFT));
                    Visit(context.expr(0));
                    m_contextData.Pop();

                    m_contextData.Push((nodelte, CLte.CT_RIGHT));
                    Visit(context.expr(1));
                    m_contextData.Pop();
                    break;
            }

            return 0;
        }
    }
}
