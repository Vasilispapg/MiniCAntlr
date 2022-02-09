using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniC
{
    public class C2JVS_IfContainer : CComboContainer
    {
        public const int mc_IF_CONDITION = 0, mc_TRUE_BODY = 1, mc_FALSE_BODY = 2;
        public readonly String[] mc_contextNames = { "IF_CONDITION", "TRUE_BODY", "FALSE_BODY" };

        public C2JVS_IfContainer(CEmmitableCodeContainer parent) : base(CodeContainerType.CT_IFCONTAINER, parent, 3)
        {
            AddCode(new CCompoundJvSContainer(this), mc_TRUE_BODY);
            AddCode(new CCompoundJvSContainer(this), mc_FALSE_BODY);
        }

        public override CodeContainer AssemblyCodeContainer()
        {
            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, MParent);
            rep.AddCode("if (");
            rep.AddCode(GetChild(mc_IF_CONDITION, 0).AssemblyCodeContainer());
            rep.AddCode(")");
            rep.AddCode(GetChild(mc_TRUE_BODY, 0).AssemblyCodeContainer());

            CComboContainer child = GetChild(mc_FALSE_BODY, 0) as CComboContainer;
            if (child.GetContextChildren(CCompoundJvSContainer.mc_COMPOUNDSTATEMENT_BODY).Length > 0)
            {
                rep.AddCode("else");
                rep.AddCode(GetChild(mc_FALSE_BODY, 0).AssemblyCodeContainer());
            }
            return rep;
        }

        public override void PrintStructure(StreamWriter m_ostream)
        {
            ExtractSubgraphs(m_ostream, mc_IF_CONDITION, mc_contextNames[mc_IF_CONDITION]);
            ExtractSubgraphs(m_ostream, mc_TRUE_BODY, mc_contextNames[mc_TRUE_BODY]);
            ExtractSubgraphs(m_ostream, mc_FALSE_BODY, mc_contextNames[mc_FALSE_BODY]);
            foreach (List<CEmmitableCodeContainer> cEmmitableCodeContainers in m_repository)
            {
                foreach (CEmmitableCodeContainer codeContainer in cEmmitableCodeContainers)
                {
                    codeContainer.PrintStructure(m_ostream);
                }
            }
            m_ostream.WriteLine("\"{0}\"->\"{1}\"", MParent.MLabel, MLabel);
        }
    }

    public class C2JVS_ForContainer : CComboContainer
    {
        public const int mc_START = 0, mc_FINALIZATION = 1, mc_STEP = 2, mc_BODY = 3;
        public readonly String[] mc_contextNames = { "START", "FINALIZATION", "STEP", "BODY" };

        public C2JVS_ForContainer(CEmmitableCodeContainer parent) : base(CodeContainerType.CT_FORCONTAINER, parent, 4)
        {
            AddCode(new CCompoundJvSContainer(this), mc_BODY);
        }

        public override CodeContainer AssemblyCodeContainer()
        {
            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, MParent);
            rep.AddCode("for (");

            if (!(GetChild(mc_START, 0) == null))
            {
                String start = GetChild(mc_START, 0).AssemblyCodeContainer().ToString();
                if (start.Contains("=")) //an einai assignment prepei na svisw to ; kai asci:13 kai \n
                    start = start.Remove(start.Length - 3, 3);
                rep.AddCode(start);
            }

            rep.AddCode("; ");
            if (!(GetChild(mc_FINALIZATION, 0) == null))
            {
                rep.AddCode(GetChild(mc_FINALIZATION, 0).AssemblyCodeContainer());
            }

            rep.AddCode("; ");
            if (!(GetChild(mc_STEP, 0) == null))
            {
                rep.AddCode(GetChild(mc_STEP, 0).AssemblyCodeContainer());
            }

            rep.AddCode(")");
            if (!(GetChild(mc_BODY, 0) == null))
            {
                rep.AddCode(GetChild(mc_BODY, 0).AssemblyCodeContainer());
            }

            return rep;
        }

        public override void PrintStructure(StreamWriter m_ostream)
        {
            ExtractSubgraphs(m_ostream, mc_START, mc_contextNames[mc_START]);
            ExtractSubgraphs(m_ostream, mc_FINALIZATION, mc_contextNames[mc_FINALIZATION]);
            ExtractSubgraphs(m_ostream, mc_STEP, mc_contextNames[mc_STEP]);
            ExtractSubgraphs(m_ostream, mc_BODY, mc_contextNames[mc_BODY]);
            foreach (List<CEmmitableCodeContainer> cEmmitableCodeContainers in m_repository)
            {
                foreach (CEmmitableCodeContainer codeContainer in cEmmitableCodeContainers)
                {
                    codeContainer.PrintStructure(m_ostream);
                }
            }
            m_ostream.WriteLine("\"{0}\"->\"{1}\"", MParent.MLabel, MLabel);
        }
    }

    public class C2JVS_File : CComboContainer
    {
        public const int mc_PREPROCESSOR = 0, mc_FUNCTION_DEFINITION = 1, mc_GLOBALVARS = 2, mc_FUNCTION_DECLARATIONS = 3;
        public readonly string[] mc_contextNames = { "PREPROCESSOR", "FUNCTION_DEFINITION", "GLOBALVARS", "FUNCTION_DECLARATIONS" };

        private HashSet<string> m_globalVarSymbolTable = new HashSet<string>();
        private HashSet<string> m_FunctionsSymbolTable = new HashSet<string>();

        private CMainFunc m_mainDefinition = null;
        public CMainFunc MainDefinition => m_mainDefinition;

        public C2JVS_File(bool withStartUpFunction) : base(CodeContainerType.CT_FILE, null, 4)
        {
            if (withStartUpFunction)
            {
                m_mainDefinition = new CMainFunc(this);
                AddCode(m_mainDefinition, mc_FUNCTION_DEFINITION);
            }
        }

        public void DeclareGlobalVariable(string varname)
        {
            CodeContainer rep;
            if (!m_globalVarSymbolTable.Contains(varname))
            {
                m_globalVarSymbolTable.Add(varname);
                rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, this);
                rep.AddCode("var " + varname + ";\n", mc_GLOBALVARS);
                AddCode(rep, mc_GLOBALVARS);
            }
        }

        public void DeclareFunction(string funname, string funheader)
        {
            CodeContainer rep;
            if (!m_FunctionsSymbolTable.Contains(funname))
            {
                rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, this);
                m_globalVarSymbolTable.Add(funname);
                rep.AddCode(funheader + ";\n", mc_FUNCTION_DECLARATIONS);
                AddCode(rep, mc_FUNCTION_DECLARATIONS);
            }
        }

        public override CodeContainer AssemblyCodeContainer()
        {
            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, null);

            rep.AddCode(AssemblyContext(mc_PREPROCESSOR));
            rep.AddCode(AssemblyContext(mc_FUNCTION_DECLARATIONS));
            rep.AddCode(AssemblyContext(mc_GLOBALVARS));
            rep.AddCode(AssemblyContext(mc_FUNCTION_DEFINITION));
            return rep;
        }

        public override void PrintStructure(StreamWriter m_ostream)
        {

            m_ostream.WriteLine("digraph {");

            ExtractSubgraphs(m_ostream, mc_PREPROCESSOR, mc_contextNames[mc_PREPROCESSOR]);
            ExtractSubgraphs(m_ostream, mc_FUNCTION_DECLARATIONS, mc_contextNames[mc_FUNCTION_DECLARATIONS]);
            ExtractSubgraphs(m_ostream, mc_GLOBALVARS, mc_contextNames[mc_GLOBALVARS]);
            ExtractSubgraphs(m_ostream, mc_FUNCTION_DEFINITION, mc_contextNames[mc_FUNCTION_DEFINITION]);

            foreach (List<CEmmitableCodeContainer> cEmmitableCodeContainers in m_repository)
            {
                foreach (CEmmitableCodeContainer codeContainer in cEmmitableCodeContainers)
                {
                    codeContainer.PrintStructure(m_ostream);
                }
            }

            m_ostream.WriteLine("}");
            m_ostream.Close();

            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-Tgif mirjvs.dot " + " -o" + " mirjvs.gif";
            // Enter the executable to run, including the complete path
            start.FileName = "dot";
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }
        }
    }

    public class CCompoundJvSContainer : CComboContainer
    {
        public const int mc_COMPOUNDSTATEMENT_DECLARATIONS = 0, mc_COMPOUNDSTATEMENT_BODY = 1;
        public readonly String[] mc_contextNames = { "COMPOUNDSTATEMENT_DECLARATIONS", "COMPOUNDSTATEMENT_BODY" };

        public CCompoundJvSContainer(CEmmitableCodeContainer parent) : base(CodeContainerType.CT_COMPOUNDCONTAINER, parent, 2)
        { }

        public override CodeContainer AssemblyCodeContainer()
        {
            //edw einai gia ta container tis javascript
            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, MParent);
            rep.AddCode("{");
            rep.EnterScope();
            //rep.AddCode("//  ***** Local declarations *****");
            //rep.AddNewLine();
            rep.AddCode(AssemblyContext(mc_COMPOUNDSTATEMENT_DECLARATIONS));
            //rep.AddCode("//  ***** Code Body *****");

            rep.AddNewLine();
            rep.AddCode(AssemblyContext(mc_COMPOUNDSTATEMENT_BODY));
            rep.LeaveScope();
            rep.AddCode("}");
            return rep;
        }

        public override void PrintStructure(StreamWriter m_ostream)
        {
            ExtractSubgraphs(m_ostream, mc_COMPOUNDSTATEMENT_BODY, mc_contextNames[mc_COMPOUNDSTATEMENT_BODY]);
            ExtractSubgraphs(m_ostream, mc_COMPOUNDSTATEMENT_DECLARATIONS, mc_contextNames[mc_COMPOUNDSTATEMENT_DECLARATIONS]);
            foreach (List<CEmmitableCodeContainer> cEmmitableCodeContainers in m_repository)
            {
                foreach (CEmmitableCodeContainer codeContainer in cEmmitableCodeContainers)
                {
                    codeContainer.PrintStructure(m_ostream);
                }
            }
            m_ostream.WriteLine("\"{0}\"->\"{1}\"", MParent.MLabel, MLabel);
        }
    }

    public class CMainFunc : C2JVS_FuncDefinition
    {
        CCompoundJvSContainer m_compound;

        public CMainFunc(CComboContainer parent) : base(parent)
        {
            m_compound = new CCompoundJvSContainer(this);
            string mainheader = "function start()";
            AddCode(mainheader, C2JVS_FuncDefinition.mc_HEADER);
            AddCode(m_compound, C2JVS_FuncDefinition.mc_BODY);
        }
    }

    public class C2JVS_FuncDefinition : CComboContainer
    {
        public const int mc_HEADER = 0, mc_BODY = 1;
        public readonly String[] mc_contextNames = { "HEADER", "BODY" };

        private HashSet<string> m_localSymbolTable = new HashSet<string>();
        private C2JVS_File m_file;

        public C2JVS_FuncDefinition(CEmmitableCodeContainer parent) : base(CodeContainerType.CT_FUNCTION_DEFINITION, parent, 3)
        {
            m_file = parent as C2JVS_File;
        }

        public virtual void DeclareVariable(string varname, bool isread)
        {
            CodeContainer rep;
            if (!m_localSymbolTable.Contains(varname))
            {
                if (isread)
                {
                    m_file.DeclareGlobalVariable(varname);
                }
                else
                {
                    rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, this);
                    m_localSymbolTable.Add(varname);

                    rep.AddCode("var " + varname + ";\n");

                    CEmmitableCodeContainer compoundst = GetChild(C2JVS_FuncDefinition.mc_BODY);
                    compoundst.AddCode(rep, CCompoundJvSContainer.mc_COMPOUNDSTATEMENT_DECLARATIONS);
                }
            }
        }

        public void AddVariableToLocalSymbolTable(string varname)
        {
            if (!m_localSymbolTable.Contains(varname))
            {
                m_localSymbolTable.Add(varname);
            }
        }

        public override CodeContainer AssemblyCodeContainer()
        {
            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, this);
            // 1. Emmit Header
            rep.AddCode(AssemblyContext(mc_HEADER));
            // 2. Emmit Body
            rep.AddCode(AssemblyContext(mc_BODY));
            rep.AddNewLine();

            return rep;
        }

        public override void PrintStructure(StreamWriter m_ostream)
        {
            ExtractSubgraphs(m_ostream, mc_HEADER, mc_contextNames[mc_HEADER]);
            ExtractSubgraphs(m_ostream, mc_BODY, mc_contextNames[mc_BODY]);

            foreach (List<CEmmitableCodeContainer> cEmmitableCodeContainers in m_repository)
            {
                foreach (CEmmitableCodeContainer codeContainer in cEmmitableCodeContainers)
                {
                    codeContainer.PrintStructure(m_ostream);
                }
            }
            m_ostream.WriteLine("\"{0}\"->\"{1}\"", MParent.MLabel, MLabel);
        }
    }

    public class CJVS_FunctionCallContainer : CComboContainer
    {
        public const int mc_NAME = 0, mc_ARGS = 1;
        public readonly String[] mc_contextNames = { "NAME", "ARGS" };

        public CJVS_FunctionCallContainer(CEmmitableCodeContainer parent) : base(CodeContainerType.CT_FUNCTION_CALL, parent, 2)
        { }

        public override CodeContainer AssemblyCodeContainer()
        {
            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, MParent);
            rep.AddCode(GetChild(mc_NAME, 0).AssemblyCodeContainer());
            rep.AddCode("(");
            int i = 0;
            foreach (var VARIABLE in GetContextChildren(mc_ARGS))
            {
                if (i > 0)
                {
                    rep.AddCode(",");
                }
                rep.AddCode(VARIABLE.AssemblyCodeContainer());
                i++;
            }

            rep.AddCode(");");
            return rep;
        }

        public override void PrintStructure(StreamWriter m_ostream)
        {
            ExtractSubgraphs(m_ostream, mc_NAME, mc_contextNames[mc_NAME]);
            ExtractSubgraphs(m_ostream, mc_ARGS, mc_contextNames[mc_ARGS]);
            foreach (List<CEmmitableCodeContainer> cEmmitableCodeContainers in m_repository)
            {
                foreach (CEmmitableCodeContainer codeContainer in cEmmitableCodeContainers)
                {
                    codeContainer.PrintStructure(m_ostream);
                }
            }
            m_ostream.WriteLine("\"{0}\"->\"{1}\"", MParent.MLabel, MLabel);
        }
    }

    public class CJVS_FunctionDefContainer : CComboContainer
    {
        public const int mc_NAME = 0, mc_ARGS = 1, mc_COMPSTATEMENT = 2;
        public readonly String[] mc_contextNames = { "NAME", "ARGS", "CT_COMPSTATEMENT" };

        public CJVS_FunctionDefContainer(CEmmitableCodeContainer parent) : base(CodeContainerType.CT_FUNCTION_DEFINITION, parent, 3)
        {
            AddCode(new CCompoundContainer(this), mc_NAME);
        }

        public override CodeContainer AssemblyCodeContainer()
        {
            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, MParent);
            rep.AddCode("function ");
            rep.AddCode(GetChild(mc_NAME, 0).AssemblyCodeContainer());
            rep.AddCode("(");
            rep.AddCode(GetChild(mc_ARGS, 0).AssemblyCodeContainer());
            rep.AddCode(")");
            rep.AddNewLine();
            rep.AddCode("{");
            rep.AddNewLine();
            rep.AddCode(GetChild(mc_COMPSTATEMENT, 1).AssemblyCodeContainer());
            rep.AddNewLine();
            rep.AddCode("}");
            return rep;
        }

        public override void PrintStructure(StreamWriter m_ostream)
        {
            ExtractSubgraphs(m_ostream, mc_NAME, mc_contextNames[mc_NAME]);
            ExtractSubgraphs(m_ostream, mc_ARGS, mc_contextNames[mc_ARGS]);
            ExtractSubgraphs(m_ostream, mc_ARGS, mc_contextNames[mc_COMPSTATEMENT]);
            foreach (List<CEmmitableCodeContainer> cEmmitableCodeContainers in m_repository)
            {
                foreach (CEmmitableCodeContainer codeContainer in cEmmitableCodeContainers)
                {
                    codeContainer.PrintStructure(m_ostream);
                }
            }
            m_ostream.WriteLine("\"{0}\"->\"{1}\"", MParent.MLabel, MLabel);
        }
    }

    public class C2JVS_WhileContainer : CComboContainer
    {
        public const int mc_CONDITION = 0, mc_BODY = 1;
        public readonly String[] mc_contextNames = { "CONDITION", "BODY" };

        public C2JVS_WhileContainer(CEmmitableCodeContainer parent) : base(CodeContainerType.CT_WHILECONTAINER, parent, 2)
        {
            AddCode(new CCompoundJvSContainer(this), mc_BODY);
        }

        public override CodeContainer AssemblyCodeContainer()
        {
            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, MParent);
            rep.AddCode("while (");
            rep.AddCode(GetChild(mc_CONDITION, 0).AssemblyCodeContainer());
            rep.AddCode(")");
            rep.AddCode(GetChild(mc_BODY, 0).AssemblyCodeContainer());
            return rep;
        }

        public override void PrintStructure(StreamWriter m_ostream)
        {
            ExtractSubgraphs(m_ostream, mc_CONDITION, mc_contextNames[mc_CONDITION]);
            ExtractSubgraphs(m_ostream, mc_BODY, mc_contextNames[mc_BODY]);
            foreach (List<CEmmitableCodeContainer> cEmmitableCodeContainers in m_repository)
            {
                foreach (CEmmitableCodeContainer codeContainer in cEmmitableCodeContainers)
                {
                    codeContainer.PrintStructure(m_ostream);
                }
            }
            m_ostream.WriteLine("\"{0}\"->\"{1}\"", MParent.MLabel, MLabel);
        }
    }

    public class C2JVS_DoWhileContainer : CComboContainer
    {
        public const int mc_BODY = 0, mc_CONDITION = 1;
        public readonly String[] mc_contextNames = { "BODY", "CONDITION" };

        public C2JVS_DoWhileContainer(CEmmitableCodeContainer parent) : base(CodeContainerType.CT_DOWHILECONTAINER, parent, 2)
        {
            AddCode(new CCompoundJvSContainer(this), mc_BODY);
        }

        public override CodeContainer AssemblyCodeContainer()
        {
            CodeContainer rep = new CodeContainer(CodeContainerType.CT_CODEREPOSITORY, MParent);
            rep.AddCode("do");
            rep.AddCode(GetChild(mc_BODY, 0).AssemblyCodeContainer());
            rep.AddCode("while (");
            rep.AddCode(GetChild(mc_CONDITION, 0).AssemblyCodeContainer());
            rep.AddCode(");");
            return rep;
        }

        public override void PrintStructure(StreamWriter m_ostream)
        {
            ExtractSubgraphs(m_ostream, mc_BODY, mc_contextNames[mc_BODY]);
            ExtractSubgraphs(m_ostream, mc_CONDITION, mc_contextNames[mc_CONDITION]);
            foreach (List<CEmmitableCodeContainer> cEmmitableCodeContainers in m_repository)
            {
                foreach (CEmmitableCodeContainer codeContainer in cEmmitableCodeContainers)
                {
                    codeContainer.PrintStructure(m_ostream);
                }
            }
            m_ostream.WriteLine("\"{0}\"->\"{1}\"", MParent.MLabel, MLabel);
        }
    }


}
