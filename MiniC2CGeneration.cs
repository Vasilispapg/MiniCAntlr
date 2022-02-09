using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace MiniC
{
    public class MiniC2CGeneration : MiniCASTBaseVisitor<CodeContainer>
    {
        private CCFile m_translatedFile;
        private Stack<(CEmmitableCodeContainer, CEmmitableCodeContainer, int)> m_parentsInfo = new Stack<(CEmmitableCodeContainer, CEmmitableCodeContainer, int)>();

        public CCFile MTranslatedFile => m_translatedFile;

        public MiniC2CGeneration()
        {
        }

        public override CodeContainer VisitCompileUnit(CCompileUnit node)
        {
            m_translatedFile = new CCFile(true);

            m_translatedFile.AddCode("#include <stdio.h>\n", CCFile.mc_PREPROCESSOR);
            m_translatedFile.AddCode("#include <stdlib.h>\n", CCFile.mc_PREPROCESSOR);

            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple =
                (m_translatedFile.MainDefinition.GetChild(CCFunctionDefinition.mc_BODY, 0),
                    m_translatedFile.MainDefinition,
                    CCFunctionDefinition.mc_BODY);
            m_parentsInfo.Push(infoTuple);
           
            VisitContextChildren(node, CCompileUnit.CT_STATEMENTS);
            m_parentsInfo.Pop();

            return null;
        }

        public override CodeContainer VisitCompoundStatement(CCompoundStatement node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();
            CCompoundContainer rep;
          
            if (!(infoTuple.Item1 is CCompoundContainer rep1))
            {
                rep = new CCompoundContainer(infoTuple.Item1);
                infoTuple.Item1.AddCode(rep, infoTuple.Item3);
            }
            else
            {
                rep = rep1;
            }
            // Translation.
            infoTuple = (rep, m_translatedFile.MainDefinition, CCompoundContainer.mc_COMPOUNDSTATEMENT_BODY);
            m_parentsInfo.Push(infoTuple);
            foreach (var varChild in node.GetContextChildren(CCompoundContainer.mc_COMPOUNDSTATEMENT_BODY))
            {
                Visit(varChild);
            }
            m_parentsInfo.Pop();
            return null;
        }

        public override CodeContainer VisitIf(CIfstatement node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CIfContainer rep = new CIfContainer(infoTuple.Item1);
            infoTuple.Item1.AddCode(rep, infoTuple.Item3);//connect with parent

            // Translation
            infoTuple = (rep, m_translatedFile.MainDefinition, CIfstatement.CT_EXPR);
            m_parentsInfo.Push(infoTuple);
            rep.AddCode(Visit(node.GetChild(CIfstatement.CT_EXPR, 0)), CIfContainer.mc_IF_CONDITION);
            m_parentsInfo.Pop();

            if (node.GetChildren(CIfstatement.CT_IFBODY).Count() > 0)
            {
                infoTuple = (rep.GetChild(CIfstatement.CT_IFBODY), m_translatedFile.MainDefinition,
                    CCompoundContainer.mc_COMPOUNDSTATEMENT_BODY);
                m_parentsInfo.Push(infoTuple);
                foreach (var VARIABLE in node.GetContextChildren(CIfstatement.CT_IFBODY))
                {
                    Visit(VARIABLE);
                }
                
                m_parentsInfo.Pop();
            }

            if (node.GetChildren(CIfstatement.CT_ELSEBODY).Count() > 0)//an yparxei else
            {
                infoTuple = (rep.GetChild(CIfstatement.CT_ELSEBODY), m_translatedFile.MainDefinition, CCompoundContainer.mc_COMPOUNDSTATEMENT_BODY);
                m_parentsInfo.Push(infoTuple);
                foreach (var VARIABLE in node.GetContextChildren(CIfstatement.CT_ELSEBODY))
                {
                    Visit(VARIABLE);
                }
                m_parentsInfo.Pop();
            }

            return null;
        }

        public override CodeContainer VisitFuncCall(CFunctioncall node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CFunctionCallContainer rep = new CFunctionCallContainer(infoTuple.Item1);
            infoTuple.Item1.AddCode(rep, infoTuple.Item3);

            // Translation
            infoTuple = (rep, m_translatedFile.MainDefinition, CFunctioncall.CT_NAME);
            m_parentsInfo.Push(infoTuple);
            rep.AddCode(Visit(node.GetChild(CFunctioncall.CT_NAME, 0)), CFunctioncall.CT_NAME);
            m_parentsInfo.Pop();

            if (node.GetChildren(CFunctioncall.CT_ARGS).Count() > 0)
            {
                infoTuple = (rep, m_translatedFile.MainDefinition, CFunctioncall.CT_ARGS);
                m_parentsInfo.Push(infoTuple);
                foreach (var varChild in node.GetContextChildren(CFunctionCallContainer.mc_ARGS))
                {
                    rep.AddCode(Visit(varChild), CFunctioncall.CT_ARGS);
                }

                m_parentsInfo.Pop();

            }

            return null;
        }

        public override CodeContainer VisitPrint_st(CPrint node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();
            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, infoTuple.Item1);

            infoTuple.Item1.AddCode(rep, infoTuple.Item3); //enwsi toy patera me to paidi

            // Translation.
            rep.AddCode("printf(\"");

            m_parentsInfo.Push(infoTuple);
            m_parentsInfo.Pop();
            int i = 0;
            foreach (var VARIABLE in node.GetContextChildren(CPrint.CT_EXPR))
            {
                var var=Visit(VARIABLE);
                rep.AddCode(var+"=%2f ");
            }
            rep.AddCode("\"");
            foreach (var VARIABLE in node.GetContextChildren(CPrint.CT_EXPR))
            {
                rep.AddCode(",");
                rep.AddCode(Visit(VARIABLE));
                i++;
            }
           
            rep.AddCode(");");
            rep.AddNewLine();
            return rep;
        }

        public override CodeContainer VisitWhile(CWhilestatement node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CWhileContainer rep = new CWhileContainer(infoTuple.Item1);
            infoTuple.Item1.AddCode(rep, infoTuple.Item3);

            // Translation
            infoTuple = (rep, m_translatedFile.MainDefinition, CWhilestatement.CT_EXPR);
            m_parentsInfo.Push(infoTuple);
            CodeContainer condition = Visit(node.GetChild(CWhilestatement.CT_EXPR,0));
            rep.AddCode(condition, CWhilestatement.CT_EXPR);
            m_parentsInfo.Pop();

            infoTuple = (rep.GetChild(CWhilestatement.CT_COMPOUNTSTATEMENT), m_translatedFile.MainDefinition, CCompoundContainer.mc_COMPOUNDSTATEMENT_BODY);
            m_parentsInfo.Push(infoTuple);
            foreach (var VARIABLE in node.GetContextChildren(CWhilestatement.CT_COMPOUNTSTATEMENT))
            {
                Visit(VARIABLE);
            }
            m_parentsInfo.Pop();
            return null;
        }

        public override CodeContainer VisitDoWhile(CDowhile node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CDoWhileContainer rep = new CDoWhileContainer(infoTuple.Item1);
            infoTuple.Item1.AddCode(rep, infoTuple.Item3);

            // Translation
            infoTuple = (rep.GetChild(CDowhile.CT_COMPSTATEMENT), m_translatedFile.MainDefinition, CCompoundContainer.mc_COMPOUNDSTATEMENT_BODY);
            m_parentsInfo.Push(infoTuple);
            foreach (var VARIABLE in node.GetContextChildren(CDowhile.CT_COMPSTATEMENT))
            {
                Visit(VARIABLE);
            }
            m_parentsInfo.Pop();

            infoTuple = (rep, m_translatedFile.MainDefinition, CDowhile.CT_EXPR);
            m_parentsInfo.Push(infoTuple);
            CodeContainer condition = Visit(node.GetChild(CDowhile.CT_EXPR, 0));
            rep.AddCode(condition, CDowhile.CT_EXPR);
            m_parentsInfo.Pop();
            return null;
        }

        public override CodeContainer VisitReturn(CReturn node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, infoTuple.Item1);
            infoTuple.Item1.AddCode(rep, infoTuple.Item3);

            // Translation.
            rep.AddCode("return ");
            rep.AddCode(Visit(node.GetChild(CReturn.CT_EXPR,0)));
            rep.AddCode(";");
            rep.AddNewLine();
            return rep;
        }

        public override CodeContainer VisitFuncDef(CFunctiondef node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CFunctionCallContainer rep = new CFunctionCallContainer(infoTuple.Item1);
            infoTuple.Item1.AddCode(rep, infoTuple.Item3);

            // Translation
            infoTuple = (rep, m_translatedFile.MainDefinition, CFunctiondef.CT_NAME);
            m_parentsInfo.Push(infoTuple);
            rep.AddCode(Visit(node.GetChild(CFunctiondef.CT_NAME, 0)), CFunctiondef.CT_NAME);
            m_parentsInfo.Pop();
            if (node.GetChildren(CFunctiondef.CT_ARGS).Count() > 0)
            {
                
                infoTuple = (rep, m_translatedFile.MainDefinition, CFunctiondef.CT_ARGS);
                m_parentsInfo.Push(infoTuple);
                rep.AddCode(Visit(node.GetChild(CFunctiondef.CT_ARGS, 1)), CFunctiondef.CT_ARGS);
                m_parentsInfo.Pop();
            }

            infoTuple = (rep, m_translatedFile.MainDefinition, CFunctiondef.CT_COMPSTATEMENT);
            m_parentsInfo.Push(infoTuple);
            rep.AddCode(Visit(node.GetChild(CFunctiondef.CT_COMPSTATEMENT, 0)), CFunctiondef.CT_COMPSTATEMENT);
            m_parentsInfo.Pop();

            return null;
        }

        public override CodeContainer VisitFor(CFor node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CForContainer rep = new CForContainer(infoTuple.Item1);
            infoTuple.Item1.AddCode(rep, infoTuple.Item3);

            // Translation
            if (node.GetChildren(CFor.CT_INIT).Count() > 0)
            {
                infoTuple = (rep, m_translatedFile.MainDefinition, CForContainer.mc_START);
                m_parentsInfo.Push(infoTuple);
                CodeContainer start = Visit(node.GetChild(CFor.CT_INIT, 0));
                rep.AddCode(start, CFor.CT_INIT);
                m_parentsInfo.Pop();
            }

            if (node.GetChildren(CFor.CT_FINAL).Count() > 0)
            {
                infoTuple = (rep, m_translatedFile.MainDefinition, CForContainer.mc_FINALIZATION);
                m_parentsInfo.Push(infoTuple);
                CodeContainer fin = Visit(node.GetChild(CFor.CT_FINAL, 0));
                rep.AddCode(fin, CFor.CT_FINAL);
                m_parentsInfo.Pop();
            }

            if (node.GetChildren(CFor.CT_STEP).Count() > 0)
            {
                infoTuple = (rep, m_translatedFile.MainDefinition, CForContainer.mc_STEP);
                m_parentsInfo.Push(infoTuple);
                CodeContainer step = Visit(node.GetChild(CFor.CT_STEP, 0));
                rep.AddCode(step, CFor.CT_STEP);
                m_parentsInfo.Pop();
            }

            if (node.GetChildren(CFor.CT_BODY).Count() > 0)
            {
                infoTuple = (rep.GetChild(CFor.CT_BODY), m_translatedFile.MainDefinition,
                    CCompoundContainer.mc_COMPOUNDSTATEMENT_BODY);
                m_parentsInfo.Push(infoTuple);
                foreach (var varChild in node.GetContextChildren(CForContainer.mc_BODY))
                {
                    Visit(varChild);
                }
                m_parentsInfo.Pop();
            }

            return null;
        }

        // Expressions
        public override CodeContainer VisitPlusPlus(CPlusplus node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, infoTuple.Item1);

            rep.AddCode(Visit(node.GetChild(CPlusplus.CT_LEFT, 0)));
            rep.AddCode("++");
            return rep;
        }

        public override CodeContainer VisitNot(CNot node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, infoTuple.Item1);

            rep.AddCode("!");
            rep.AddCode(Visit(node.GetChild(CNot.CT_RIGHT, 0)));
            return rep;
        }

        public override CodeContainer VisitAddition(CAddition node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, infoTuple.Item1);

            // Translation.
            rep.AddCode(Visit(node.GetChild(CAddition.CT_LEFT, 0)));
            rep.AddCode(" + ");
            rep.AddCode(Visit(node.GetChild(CAddition.CT_RIGHT, 0)));
            return rep;
        }

        public override CodeContainer VisitSub(CSubstraction node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, infoTuple.Item1);

            // Translation.
            rep.AddCode(Visit(node.GetChild(CSubstraction.CT_LEFT, 0)));
            rep.AddCode(" - ");
            rep.AddCode(Visit(node.GetChild(CSubstraction.CT_RIGHT, 0)));
            return rep;
        }

        public override CodeContainer VisitMult(CMultiplication node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, infoTuple.Item1);

            // Translation.
            rep.AddCode(Visit(node.GetChild(CMultiplication.CT_LEFT, 0)));
            rep.AddCode(" * ");
            rep.AddCode(Visit(node.GetChild(CMultiplication.CT_RIGHT, 0)));
            return rep;
        }

        public override CodeContainer VisitDiv(CDivision node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, infoTuple.Item1);

            // Translation.
            rep.AddCode(Visit(node.GetChild(CDivision.CT_LEFT, 0)));
            rep.AddCode(" / ");
            rep.AddCode(Visit(node.GetChild(CDivision.CT_RIGHT, 0)));
            return rep;
        }

        public override CodeContainer VisitGt(CGt node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, infoTuple.Item1);
            rep.AddCode(Visit(node.GetChild(CGt.CT_LEFT,0)));
            rep.AddCode(" > ");
            rep.AddCode(Visit(node.GetChild(CGt.CT_RIGHT,0)));

            return rep;
        }

        public override CodeContainer VisitGte(CGte node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, infoTuple.Item1);

            rep.AddCode(Visit(node.GetChild(CGte.CT_LEFT,0)));
            rep.AddCode(" >= ");
            rep.AddCode(Visit(node.GetChild(CGte.CT_RIGHT,0)));

            return rep;
        }

        public override CodeContainer VisitLt(CLt node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, infoTuple.Item1);

            rep.AddCode(Visit(node.GetChild(CLt.CT_LEFT,0)));
            rep.AddCode(" < ");
            rep.AddCode(Visit(node.GetChild(CLt.CT_RIGHT,0)));

            return rep;
        }

        public override CodeContainer VisitLte(CLte node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, infoTuple.Item1);

            rep.AddCode(Visit(node.GetChild(CLte.CT_LEFT,0)));
            rep.AddCode(" <= ");
            rep.AddCode(Visit(node.GetChild(CLte.CT_RIGHT,0)));

            return rep;
        }

        public override CodeContainer VisitEqual(CEqual node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, infoTuple.Item1);

            rep.AddCode(Visit(node.GetChild(CEqual.CT_LEFT,0)));
            rep.AddCode(" == ");
            rep.AddCode(Visit(node.GetChild(CEqual.CT_RIGHT,0)));

            return rep;
        }

        public override CodeContainer VisitNequal(CNequal node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, infoTuple.Item1);

            rep.AddCode(Visit(node.GetChild(CNequal.CT_LEFT,0)));
            rep.AddCode(" != ");
            rep.AddCode(Visit(node.GetChild(CNequal.CT_RIGHT,0)));

            return rep;
        }

        public override CodeContainer VisitAnd(CAnd node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, infoTuple.Item1);

            rep.AddCode(Visit(node.GetChild(CAnd.CT_LEFT,0)));
            rep.AddCode(" && ");
            rep.AddCode(Visit(node.GetChild(CAnd.CT_RIGHT,0)));

            return rep;
        }

        public override CodeContainer VisitOr(COr node)
        {
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, infoTuple.Item1);

            rep.AddCode(Visit(node.GetChild(COr.CT_LEFT,0)));
            rep.AddCode(" || ");
            rep.AddCode(Visit(node.GetChild(COr.CT_RIGHT,0)));

            return rep;
        }

        public override CodeContainer VisitAssignment(CAssignment node)
        {
            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, null);
            (CEmmitableCodeContainer, CEmmitableCodeContainer, int) infoTuple = m_parentsInfo.Peek();

            CCFunctionDefinition fun = infoTuple.Item2 as CCFunctionDefinition;

            infoTuple.Item1.AddCode(rep, infoTuple.Item3); //enwsi toy patera me to paidi

            // Translation.
            CVARIABLE id = node.GetChild(CAssignment.CT_LEFT, 0) as CVARIABLE;
            fun.DeclareVariable(id.MName1, false);
            rep.AddCode(id.MName1);
            rep.AddCode("=");
            rep.AddCode(Visit(node.GetChild(CAssignment.CT_RIGHT, 0)));
            rep.AddCode(";");
            rep.AddNewLine();
            return rep;
        }

        public override CodeContainer VisitVARIABLE(CVARIABLE node)
        {
            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, null);

            // Translation.
            rep.AddCode(node.MName1);
            return rep;
        }

    }
}
