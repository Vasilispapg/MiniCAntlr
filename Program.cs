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

            MiniCASTBaseVisiton<int> dummyVisitor = new MiniCASTBaseVisiton<int>();
            dummyVisitor.Visit(astGenerator.MRoot);

            ASTPrinterVisitor astPrinterVisitor = new ASTPrinterVisitor("ast.dot");
            astPrinterVisitor.Visit(astGenerator.MRoot);
        }
    }
}
