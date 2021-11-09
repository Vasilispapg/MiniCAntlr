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
using IErrorNode = Antlr4.Runtime.Tree.IErrorNode;
using ITerminalNode = Antlr4.Runtime.Tree.ITerminalNode;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

/// <summary>
/// This class provides an empty implementation of <see cref="IMiniCListener"/>,
/// which can be extended to create a listener which only needs to handle a subset
/// of the available methods.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public partial class MiniCBaseListener : IMiniCListener {
	/// <summary>
	/// Enter a parse tree produced by the <c>Variable</c>
	/// labeled alternative in <see cref="MiniCParser.last"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterVariable([NotNull] MiniCParser.VariableContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Variable</c>
	/// labeled alternative in <see cref="MiniCParser.last"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitVariable([NotNull] MiniCParser.VariableContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>Number</c>
	/// labeled alternative in <see cref="MiniCParser.last"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterNumber([NotNull] MiniCParser.NumberContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Number</c>
	/// labeled alternative in <see cref="MiniCParser.last"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitNumber([NotNull] MiniCParser.NumberContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>Return</c>
	/// labeled alternative in <see cref="MiniCParser.breakreturn_st"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterReturn([NotNull] MiniCParser.ReturnContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Return</c>
	/// labeled alternative in <see cref="MiniCParser.breakreturn_st"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitReturn([NotNull] MiniCParser.ReturnContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>Break</c>
	/// labeled alternative in <see cref="MiniCParser.breakreturn_st"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBreak([NotNull] MiniCParser.BreakContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Break</c>
	/// labeled alternative in <see cref="MiniCParser.breakreturn_st"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBreak([NotNull] MiniCParser.BreakContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>Assignment</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAssignment([NotNull] MiniCParser.AssignmentContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Assignment</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAssignment([NotNull] MiniCParser.AssignmentContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>Parenthesis</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterParenthesis([NotNull] MiniCParser.ParenthesisContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Parenthesis</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitParenthesis([NotNull] MiniCParser.ParenthesisContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>Operators</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterOperators([NotNull] MiniCParser.OperatorsContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Operators</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitOperators([NotNull] MiniCParser.OperatorsContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>last_expr</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLast_expr([NotNull] MiniCParser.Last_exprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>last_expr</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLast_expr([NotNull] MiniCParser.Last_exprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>Add_sub</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterAdd_sub([NotNull] MiniCParser.Add_subContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Add_sub</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitAdd_sub([NotNull] MiniCParser.Add_subContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>NotOperator</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterNotOperator([NotNull] MiniCParser.NotOperatorContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>NotOperator</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitNotOperator([NotNull] MiniCParser.NotOperatorContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>Mult_div</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterMult_div([NotNull] MiniCParser.Mult_divContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>Mult_div</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitMult_div([NotNull] MiniCParser.Mult_divContext context) { }

	/// <summary>
	/// Enter a parse tree produced by the <c>PlusplusOperator</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterPlusplusOperator([NotNull] MiniCParser.PlusplusOperatorContext context) { }
	/// <summary>
	/// Exit a parse tree produced by the <c>PlusplusOperator</c>
	/// labeled alternative in <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitPlusplusOperator([NotNull] MiniCParser.PlusplusOperatorContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="MiniCParser.compileUnit"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCompileUnit([NotNull] MiniCParser.CompileUnitContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="MiniCParser.compileUnit"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCompileUnit([NotNull] MiniCParser.CompileUnitContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="MiniCParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterStatement([NotNull] MiniCParser.StatementContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="MiniCParser.statement"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitStatement([NotNull] MiniCParser.StatementContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="MiniCParser.breakreturn_st"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterBreakreturn_st([NotNull] MiniCParser.Breakreturn_stContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="MiniCParser.breakreturn_st"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitBreakreturn_st([NotNull] MiniCParser.Breakreturn_stContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="MiniCParser.while_st"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterWhile_st([NotNull] MiniCParser.While_stContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="MiniCParser.while_st"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitWhile_st([NotNull] MiniCParser.While_stContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="MiniCParser.if_st"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterIf_st([NotNull] MiniCParser.If_stContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="MiniCParser.if_st"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitIf_st([NotNull] MiniCParser.If_stContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="MiniCParser.compound_st"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterCompound_st([NotNull] MiniCParser.Compound_stContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="MiniCParser.compound_st"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitCompound_st([NotNull] MiniCParser.Compound_stContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="MiniCParser.func_deffinition_st"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFunc_deffinition_st([NotNull] MiniCParser.Func_deffinition_stContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="MiniCParser.func_deffinition_st"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFunc_deffinition_st([NotNull] MiniCParser.Func_deffinition_stContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="MiniCParser.func_call_st"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterFunc_call_st([NotNull] MiniCParser.Func_call_stContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="MiniCParser.func_call_st"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitFunc_call_st([NotNull] MiniCParser.Func_call_stContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="MiniCParser.parameters"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterParameters([NotNull] MiniCParser.ParametersContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="MiniCParser.parameters"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitParameters([NotNull] MiniCParser.ParametersContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterExpr([NotNull] MiniCParser.ExprContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="MiniCParser.expr"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitExpr([NotNull] MiniCParser.ExprContext context) { }

	/// <summary>
	/// Enter a parse tree produced by <see cref="MiniCParser.last"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void EnterLast([NotNull] MiniCParser.LastContext context) { }
	/// <summary>
	/// Exit a parse tree produced by <see cref="MiniCParser.last"/>.
	/// <para>The default implementation does nothing.</para>
	/// </summary>
	/// <param name="context">The parse tree.</param>
	public virtual void ExitLast([NotNull] MiniCParser.LastContext context) { }

	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void EnterEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void ExitEveryRule([NotNull] ParserRuleContext context) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitTerminal([NotNull] ITerminalNode node) { }
	/// <inheritdoc/>
	/// <remarks>The default implementation does nothing.</remarks>
	public virtual void VisitErrorNode([NotNull] IErrorNode node) { }
}
} // namespace MiniC
