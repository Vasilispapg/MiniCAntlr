//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Vasilis\source\repos\SimpleCalc\MiniC\MiniC.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace MiniC {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="MiniCParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface IMiniCVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by the <c>OrOperator</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOrOperator([NotNull] MiniCParser.OrOperatorContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>NequalOperator</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNequalOperator([NotNull] MiniCParser.NequalOperatorContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Variable</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariable([NotNull] MiniCParser.VariableContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>AndOperator</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAndOperator([NotNull] MiniCParser.AndOperatorContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Add_sub</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAdd_sub([NotNull] MiniCParser.Add_subContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>GteOperator</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGteOperator([NotNull] MiniCParser.GteOperatorContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Mult_div</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMult_div([NotNull] MiniCParser.Mult_divContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>EqualOperator</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEqualOperator([NotNull] MiniCParser.EqualOperatorContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Assignment</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignment([NotNull] MiniCParser.AssignmentContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>LteOperator</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLteOperator([NotNull] MiniCParser.LteOperatorContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Parenthesis</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenthesis([NotNull] MiniCParser.ParenthesisContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>Number</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumber([NotNull] MiniCParser.NumberContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>GtOperator</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGtOperator([NotNull] MiniCParser.GtOperatorContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>LtOperator</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLtOperator([NotNull] MiniCParser.LtOperatorContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>NotOperator</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNotOperator([NotNull] MiniCParser.NotOperatorContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>PlusplusOperator</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPlusplusOperator([NotNull] MiniCParser.PlusplusOperatorContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MiniCParser.compileUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCompileUnit([NotNull] MiniCParser.CompileUnitContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MiniCParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStatement([NotNull] MiniCParser.StatementContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MiniCParser.compound_st"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCompound_st([NotNull] MiniCParser.Compound_stContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MiniCParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpr([NotNull] MiniCParser.ExprContext context);
}
} // namespace MiniC
