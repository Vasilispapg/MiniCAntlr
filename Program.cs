using System;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace MiniC
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader aStream = new StreamReader(args[0]);
            AntlrInputStream alAntlrInputStream = new AntlrInputStream(aStream);
            MiniCLexer lexer = new MiniCLexer(alAntlrInputStream);
            CommonTokenStream tokens = new CommonTokenStream(lexer);

            MiniCParser parser = new MiniCParser(tokens);
            IParseTree tree = parser.compileUnit();  //root node
            Console.WriteLine(tree.ToStringTree());

            MiniCPrinterVisitor miniCPrinter = new MiniCPrinterVisitor();
            miniCPrinter.Visit(tree);

            ASTGenerator astGenerator = new ASTGenerator();
            astGenerator.Visit(tree);

            MiniCASTBaseVisitor<int> dummyVisitor = new MiniCASTBaseVisitor<int>();
            dummyVisitor.Visit(astGenerator.MRoot);

            ASTPrinterVisitor astPrinterVisitor = new ASTPrinterVisitor("ast.dot");
            astPrinterVisitor.Visit(astGenerator.MRoot);


            //C
            MiniC2CGeneration cGeneration = new MiniC2CGeneration();
            cGeneration.Visit(astGenerator.MRoot);
            String cFileName = Path.GetFileNameWithoutExtension(args[0]);

            StreamWriter mir = new StreamWriter("mir.dot");
            cGeneration.MTranslatedFile.PrintStructure(mir);

            StreamWriter outCFile = new StreamWriter(cFileName + ".c");
            cGeneration.MTranslatedFile.EmmitToFile(outCFile);
            outCFile.Close();

            //Javascript
            MiniC2Javascript jvsGeneration = new MiniC2Javascript();
            jvsGeneration.Visit(astGenerator.MRoot);
            StreamWriter outJSFile = new StreamWriter(cFileName + ".js");
            jvsGeneration.MTranslatedFile.EmmitToFile(outJSFile);
            outJSFile.Close();
        }
    }
}
