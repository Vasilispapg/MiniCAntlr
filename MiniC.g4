grammar MiniC;

/*
 * Parser Rules
 */

compileUnit:	(statement|func_deffinition_st)+ EOF
	;

	statement:	(expr SEMICOLON)+
	|	func_call_st
	|	if_st
	|	while_st
	|	for_st
	|	dowhile_st
	|	print_st
	|	SEMICOLON+
	|	breakreturn_st
	;

	print_st: PRINT LP prints? RP SEMICOLON
		;

	prints:(expr (COMMA)?)+
		  ;

	for_st: FOR LP op1=expr? SEMICOLON op2=expr? SEMICOLON op3=expr? RP compound_st
		;

	dowhile_st: DO compound_st WHILE LP expr RP SEMICOLON
		;

	breakreturn_st: BREAK SEMICOLON #Break
	|	RETURN expr SEMICOLON	#Return
	;

	while_st:	WHILE LP expr RP compound_st
			;

	if_st:	IF LP expr RP compound_st (ELSE compound_st)?
		 ;

	compound_st : 	LBRACKET statement* RBRACKET #Compound_statement
		;
	
	func_deffinition_st:	TYPE FUNCTION VARIABLE? LP fargs? RP compound_st
		;

	func_call_st:	VARIABLE LP fargs? RP SEMICOLON
		;

	fargs : (VARIABLE (COMMA)?)+
	  ;

	


	expr: last								#last_expr		
		|	expr PLUSPLUS					#PlusplusOperator
		|	NOT expr						#NotOperator
		|	LP expr RP						#Parenthesis
		|	expr op=(MULT|DIV)expr			#Mult_div
		|	expr op=(PLUS|MINUS)expr		#Add_sub
		|	expr op=(LT|GT|LTE|GTE) expr	#Operators
		|	expr op=(EQUAL|NEQUAL) expr		#EqualNotOperator
		|	expr AND expr					#AndOperator
		|	expr OR expr					#OrOperator	
		|	VARIABLE ASSIGN expr			#Assignment
		;

last : NUM	#Number
	   | VARIABLE #Variable
	   ;

/*
 * Lexer Rules
 */

IF: 'if';
WHILE: 'while';
FOR: 'for';
BREAK: 'break';
ELSE:'else';
DO:'do';
RETURN:'return';
FUNCTION:'function';
PRINT:'print';
		
AND:'&&';	
OR:'||';	
NOT:'!';		
LT:'<';		
GT:'>';		
GTE:'>=';	
LTE:'<=';	
EQUAL:'==';		
NEQUAL:'!=';	

DIV: '/' ;
MULT : '*';
PLUS : '+';
PLUSPLUS:'++';
MINUS : '-';

LBRACKET:'{';
RBRACKET:'}';
LP: '(';
RP: ')';
ASSIGN: '=';
SEMICOLON: ';';
COMMA:',';

TYPE:'int'|'float'|'char'|'string';

STRING : ('"'(~[\n\"]|('\\\n')|('\\'.))*'"' | ('\''(~[\n\']|('\\\n')|('\\'.))*'\''));
COMMENT : '/*'(.|'\n')*?'*\\' | '\\'.*;

NUM: ([1-9][0-9]*|[0]);
VARIABLE:[a-zA-Z][a-zA-Z0-9]*;
WS
	:	[ \r\t\n] -> skip
	;
