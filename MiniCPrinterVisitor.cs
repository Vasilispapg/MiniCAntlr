using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Tree;

namespace MiniC
{
    class MiniCPrinterVisitor : MiniCBaseVisitor<int>
    {
        public int flag_func = 0; //gia na jerw an einai function na min emfanisw ws filo to onoma tis
        // 0->false
        // 1->func_name
        // 2->parameters
        StreamWriter m_file = new StreamWriter("test.dot");
        private Stack<string> m_parentsLabel = new Stack<string>();
        private static int ms_serialCounter = 0;

        public override int VisitNumber(MiniCParser.NumberContext context)
        {
            //edw omws mpainei giati 
            string label = "Number_" + ms_serialCounter++ + " num=" + context.NUM().Symbol.Text;
            m_file.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);
            return base.VisitNumber(context);
        }

        public override int VisitOperators(MiniCParser.OperatorsContext context)
        {
            string label="";
            switch (context.op.Type)
            {
                case MiniCLexer.OR:
                    label = "Or";
                    break;
                case MiniCLexer.AND:
                    label = "And";
                    break;
                case MiniCLexer.EQUAL:
                    label = "Equal";
                    break;
                case MiniCLexer.GT:
                    label = "Gt";
                    break;
                case MiniCLexer.GTE:
                    label = "Gte";
                    break;
                case MiniCLexer.LT:
                    label = "Lt";
                    break;
                case MiniCLexer.LTE:
                    label = "Lte";
                    break;
                case MiniCLexer.NEQUAL:
                    label = "Nequal";
                    break;
                default: break;
            }

            label += "Operator_"+ ms_serialCounter++;
            m_file.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);
            m_parentsLabel.Push(label);
            base.VisitOperators(context);
            m_parentsLabel.Pop();
            return 0;
        }

        public override int VisitAdd_sub(MiniCParser.Add_subContext context)
        {
            String label = "";
            switch (context.op.Type)
            {
                case MiniCLexer.PLUS:
                    label = "Addition_" + ms_serialCounter++;
                    break;
                case MiniCLexer.MINUS:
                    label = "Substraction_" + ms_serialCounter++;
                    break;
            }
            m_file.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);
            m_parentsLabel.Push(label);
            base.VisitAdd_sub(context);
            m_parentsLabel.Pop();
            return 0;
        }

        public override int VisitMult_div(MiniCParser.Mult_divContext context)
        {
            String label = "";
            switch (context.op.Type)
            {
                case MiniCLexer.MULT:
                    label = "Multiplication_" + ms_serialCounter++;
                    break;
                case MiniCLexer.DIV:
                    label = "Division_" + ms_serialCounter++;
                    break;

            }
            m_file.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);
            m_parentsLabel.Push(label);
            base.VisitMult_div(context);
            m_parentsLabel.Pop();
            return 0;
        }

        public override int VisitAssignment(MiniCParser.AssignmentContext context)
        {
            string label = "Assignment_" + ms_serialCounter++;

            // print Edge
            m_file.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);

            //m_parentsLabel.Peek() -> ekei poy einai i stiva

            m_parentsLabel.Push(label);
            base.VisitAssignment(context); /*episkepsi sta paidia toy komvou*/
            m_parentsLabel.Pop(); /*meta aferoyme tin etiketa apo tin stiva gia ta paidia poy tha pane meta*/
            return 0;
        }

        public override int VisitParenthesis(MiniCParser.ParenthesisContext context)
        {
            string label = "Parenthesis_" + ms_serialCounter++;
            m_file.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);

            m_parentsLabel.Push(label);
            base.VisitParenthesis(context); /*episkepsi sta paidia toy komvou*/
            m_parentsLabel.Pop();
            return 0;
        }

        public override int VisitNotOperator(MiniCParser.NotOperatorContext context)
        {
            
            string label = "NotOperator_" + ms_serialCounter++;
            // Print edge
            m_file.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);
            m_parentsLabel.Push(label);
            base.VisitNotOperator(context);
            m_parentsLabel.Pop();
            return 0;
        }

        public override int VisitPlusplusOperator(MiniCParser.PlusplusOperatorContext context)
        {
            string label = "PlusPlusOperator_" + ms_serialCounter++;
            // Print edge
            m_file.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);
            m_parentsLabel.Push(label);
            base.VisitPlusplusOperator(context);
            m_parentsLabel.Pop();
            return 0;
        }

        public override int VisitCompileUnit(MiniCParser.CompileUnitContext context)
        {
            //Create name
            string label = "CompileUnit_" + ms_serialCounter++;

            //Print Prologue
            m_file.WriteLine("digraph G{");
            m_parentsLabel.Push(label);

            //Visit Childs
            base.VisitCompileUnit(context);

            //pop the last child
            m_parentsLabel.Pop();

            //print epilogue and close file
            m_file.WriteLine("}");
            m_file.Close();

            //Generate files
            ProcessStartInfo start = new ProcessStartInfo();
            start.Arguments = "-Tgif " +
                              Path.GetFileName("test.dot") + " -o " +
                              Path.GetFileNameWithoutExtension("test") + ".gif";
            start.FileName = "dot";
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();
                exitCode = proc.ExitCode;
            }

            return 0;
        }

        public override int VisitWhile_st(MiniCParser.While_stContext context)
        {
            string label = "While_Statement_" + ms_serialCounter++;
            m_file.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);
            m_parentsLabel.Push(label);
            base.VisitWhile_st(context);
            m_parentsLabel.Pop();
            return 0;
        }

        public override int VisitIf_st(MiniCParser.If_stContext context)
        {
            string label = "If_Statement_" + ms_serialCounter++;
            m_file.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);
            m_parentsLabel.Push(label);
            base.VisitIf_st(context);
            m_parentsLabel.Pop();
            return 0;
        }

        public override int VisitFunc_deffinition_st(MiniCParser.Func_deffinition_stContext context)
        {
            string label = "Name="+context.VARIABLE().Symbol.Text+" Function_Deffinition_" + ms_serialCounter++;
            m_file.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);
            m_parentsLabel.Push(label);

            flag_func = 1;//gia na jerw oti mpika se synartisi -> xrisimopoieite stin VisitTerminal

            base.VisitFunc_deffinition_st(context);
            m_parentsLabel.Pop();
            return 0;
        }

        public override int VisitFunc_call_st(MiniCParser.Func_call_stContext context)
        {
            string label = "Name=" + context.VARIABLE().Symbol.Text + " Function_Call_" + ms_serialCounter++;
            m_file.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);
            m_parentsLabel.Push(label);

            flag_func = 1; //gia na jerw oti mpika se synartisi -> xrisimopoieite stin VisitTerminal

            base.VisitFunc_call_st(context);
            m_parentsLabel.Pop();
            return 0;
        }
        public override int VisitTerminal(ITerminalNode node)
        {
            /*
              ayto tha xrisimopioisoume gia ta termatika
              gia to num -> mpainei sto VisitNumber, ektelei oti prepei gia to fyllo,
              mpainei sto terminal den kanei tipota ara paei sto return
              kai meta feygei ara etsi exw mono 1 fyllo, giati den ektypwnw to terminal toy
               */
            string label = "";
            switch (node.Symbol.Type)
            {
                case MiniCLexer.VARIABLE:
                    // Print edge
                    switch (flag_func)
                    {
                        case 0:
                            label = "Variable_" + ms_serialCounter++ + " name=" + node.Symbol.Text;
                            m_file.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);
                            break;
                        case 1:
                            flag_func = 2; //an vreis func meta einai parametri mexri na vreis to RP -> ')'
                            break;
                        case 2:
                            label = "Parameter_" + ms_serialCounter++ + " name=" + node.Symbol.Text;
                            m_file.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);
                            break;
                    }
                    break;
                case MiniCLexer.RP:
                    flag_func = 0; // telos synartisis -> ola variables twra
                    break;
                default: break;
            }
            return base.VisitTerminal(node);
        }
    }
}
