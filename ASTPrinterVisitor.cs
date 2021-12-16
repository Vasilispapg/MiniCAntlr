using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniC
{
    class ASTPrinterVisitor : MiniCASTBaseVisiton<int>
    {
        private StreamWriter m_dotFile;
        private ASTElement m_root;
        private String m_dotFilename;
        private static int ms_clusterSerial;

        public ASTPrinterVisitor(string mDotFilename)
        {
            m_dotFilename = mDotFilename;
            m_root = null;
            m_dotFile = null;
            ms_clusterSerial = 0;
        }

        public override int VisitCompileUnit(CCompileUnit node)
        {
            //Open dotFile
            m_dotFile = new StreamWriter(m_dotFilename);

            m_dotFile.WriteLine("digraph G{");

            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine("node [style=filled,color=white];");
            m_dotFile.WriteLine("style=filled;\ncolor=lightgrey;");

            //Generate contexts
            ExtractSubgraphs(node, CCompileUnit.CT_STATEMENTS, CCompileUnit.msc_contextNames);
            ExtractSubgraphs(node, CCompileUnit.CT_FUNCDEF, CCompileUnit.msc_contextNames);

            //Visit contexts
            base.VisitCompileUnit(node);

            //Close dotFile
            m_dotFile.WriteLine("}");
            m_dotFile.Close();

            //Call graphviz to print tree
            //Prepare the process dot to run
            ProcessStartInfo start = new ProcessStartInfo();
            //Enter, in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-Tgif " +
                              Path.GetFileName("ast.dot") + " -o " +
                              Path.GetFileNameWithoutExtension("ast") + ".gif";
            //Enter the executable to run , including the complete path
            start.FileName = "dot";
            //Do you want to show the console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            //Run the external process and wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                //Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }

            return 0;
        }

        public void ExtractSubgraphs(ASTElement node,int context,string[] contextNames)
        {
            /*
            - vgazei tin gkri perioxi kai toys komvoys mesa
            - pairnei ton komvo san eisodo kai to context
            - o cluster thelei ai arithmo
            */
            m_dotFile.WriteLine($"subgraph cluster{ms_clusterSerial++}{{");
            m_dotFile.WriteLine("node [style=filled,color=white];");
            m_dotFile.WriteLine("style=filled;\ncolor=lightgrey;");
            foreach (ASTElement c in node.GetContextChildren(context))
            {
                m_dotFile.Write($"{c.MName};");
            }
            m_dotFile.WriteLine($"label=\"{contextNames[context]}\";\n}}");
        }

        public override int VisitAssignment(CAssignment node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CAssignment.CT_LEFT, CAssignment.msc_contextNames);
            ExtractSubgraphs(node, CAssignment.CT_RIGHT, CAssignment.msc_contextNames);

            base.VisitAssignment(node);
            return 0;
        }

        public override int VisitIf(CIfstatement node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CIfstatement.CT_EXPR, CIfstatement.msc_contextNames);
            ExtractSubgraphs(node, CIfstatement.CT_IFBODY, CIfstatement.msc_contextNames);
            ExtractSubgraphs(node, CIfstatement.CT_ELSEBODY, CIfstatement.msc_contextNames);

            base.VisitIf(node);
            return 0;
        }

        public override int VisitWhile(CWhilestatement node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CWhilestatement.CT_EXPR, CWhilestatement.msc_contextNames);
            ExtractSubgraphs(node, CWhilestatement.CT_COMPOUNTSTATEMENT, CWhilestatement.msc_contextNames);

            base.VisitWhile(node);
            return 0;
        }

        public override int VisitReturn(CReturn node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CReturn.CT_EXPR, CReturn.msc_contextNames);

            base.VisitReturn(node);
            return 0;
        }

        public override int VisitBreak(CBreak node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            return 0;
        }

        public override int VisitPlusPlus(CPlusplus node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CPlusplus.CT_LEFT, CPlusplus.msc_contextNames);

            base.VisitPlusPlus(node);
            return 0;
        }

        public override int VisitFuncDef(CFunctiondef node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CFunctiondef.CT_NAME, CFunctiondef.msc_contextNames);
            ExtractSubgraphs(node, CFunctiondef.CT_ARGS, CFunctiondef.msc_contextNames);
            ExtractSubgraphs(node, CFunctiondef.CT_COMPSTATEMENT, CFunctiondef.msc_contextNames);

            base.VisitFuncDef(node);
            return 0;
        }

        public override int VisitFuncCall(CFunctioncall node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CFunctioncall.CT_NAME, CFunctioncall.msc_contextNames);
            ExtractSubgraphs(node, CFunctioncall.CT_ARGS, CFunctioncall.msc_contextNames);

            base.VisitFuncCall(node);
            return 0;
        }

        public override int VisitAddition(CAddition node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CAddition.CT_LEFT, CAddition.msc_contextNames);
            ExtractSubgraphs(node, CAddition.CT_RIGHT, CAddition.msc_contextNames);

            base.VisitAddition(node);
            return 0;
        }

        public override int VisitSub(CSubstraction node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CSubstraction.CT_LEFT, CSubstraction.msc_contextNames);
            ExtractSubgraphs(node, CSubstraction.CT_RIGHT, CSubstraction.msc_contextNames);

            base.VisitSub(node);
            return 0;
        }

        public override int VisitMult(CMultiplication node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CMultiplication.CT_LEFT, CMultiplication.msc_contextNames);
            ExtractSubgraphs(node, CMultiplication.CT_RIGHT, CMultiplication.msc_contextNames);

            base.VisitMult(node);
            return 0;
        }

        public override int VisitDiv(CDivision node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CDivision.CT_LEFT, CDivision.msc_contextNames);
            ExtractSubgraphs(node, CDivision.CT_RIGHT, CDivision.msc_contextNames);

            base.VisitDiv(node);
            return 0;
        }

        public override int VisitEqual(CEqual node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CEqual.CT_LEFT, CEqual.msc_contextNames);
            ExtractSubgraphs(node, CEqual.CT_RIGHT, CEqual.msc_contextNames);

            base.VisitEqual(node);
            return 0;
        }

        public override int VisitVARIABLE(CVARIABLE node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            return 0;
        }

        public override int VisitNequal(CNequal node)
        {
            //MParent == null
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CNequal.CT_LEFT, CNequal.msc_contextNames);
            ExtractSubgraphs(node, CNequal.CT_RIGHT, CNequal.msc_contextNames);

            base.VisitNequal(node);
            return 0;
        }

        public override int VisitAnd(CAnd node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CAnd.CT_LEFT, CAnd.msc_contextNames);
            ExtractSubgraphs(node, CAnd.CT_RIGHT, CAnd.msc_contextNames);

            base.VisitAnd(node);
            return 0;
        }

        public override int VisitOr(COr node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, COr.CT_LEFT, COr.msc_contextNames);
            ExtractSubgraphs(node, COr.CT_RIGHT, COr.msc_contextNames);

            base.VisitOr(node);
            return 0;
        }

        public override int VisitLte(CLte node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CLte.CT_LEFT, CLte.msc_contextNames);
            ExtractSubgraphs(node, CLte.CT_RIGHT, CLte.msc_contextNames);

            base.VisitLte(node);
            return 0;
        }

        public override int VisitLt(CLt node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CLt.CT_LEFT, CLt.msc_contextNames);
            ExtractSubgraphs(node, CLt.CT_RIGHT, CLt.msc_contextNames);

            base.VisitLt(node);
            return 0;
        }

        public override int VisitGte(CGte node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CGte.CT_LEFT, CGte.msc_contextNames);
            ExtractSubgraphs(node, CGte.CT_RIGHT, CGte.msc_contextNames);

            base.VisitGte(node);
            return 0;
        }

        public override int VisitGt(CGt node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CGt.CT_LEFT, CGt.msc_contextNames);
            ExtractSubgraphs(node, CGt.CT_RIGHT, CGt.msc_contextNames);

            base.VisitGt(node);
            return 0;
        }

        public override int VisitNot(CNot node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CNot.CT_RIGHT, CNot.msc_contextNames);

            base.VisitNot(node);
            return 0;
        }

        public override int VisitFor(CFor node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CFor.CT_INIT, CFor.msc_contextNames);
            ExtractSubgraphs(node, CFor.CT_FINAL, CFor.msc_contextNames);
            ExtractSubgraphs(node, CFor.CT_STEP, CFor.msc_contextNames);
            ExtractSubgraphs(node, CFor.CT_BODY, CFor.msc_contextNames);

            base.VisitFor(node);
            return 0;
        }

        public override int VisitDoWhile(CDowhile node)
        {
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            ExtractSubgraphs(node, CDowhile.CT_EXPR, CDowhile.msc_contextNames);
            ExtractSubgraphs(node, CDowhile.CT_COMPSTATEMENT, CDowhile.msc_contextNames);

            base.VisitDoWhile(node);
            return 0;
        }
    }
}
