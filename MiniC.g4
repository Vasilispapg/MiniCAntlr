grammar MiniC;

/*
 * Parser Rules
 */

compileUnit:	statement+
	;

	statement:	(expr SEMICOLON)+
	|	func_deffinition_st
	|	func_call_st
	|	if_st
	|	while_st
	|	for_st
	|	dowhile_st
	|	SEMICOLON+
	|	breakreturn_st
	;

	for_st: FOR LP expr? SEMICOLON expr? SEMICOLON expr? RP compound_st
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

	compound_st : 	LBRACKET statement* RBRACKET
		;

	func_deffinition_st:	FUNCTION VARIABLE? LP parameters RP compound_st
		;

	func_call_st:	VARIABLE LP parameters RP SEMICOLON
					;

	parameters: VARIABLE (COMMA VARIABLE)*
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

STRING : ('"'(~[\n\"]|('\\\n')|('\\'.))*'"' | ('\''(~[\n\']|('\\\n')|('\\'.))*'\''));
COMMENT : '/*'(.|'\n')*?'*\\' | '\\'.*;

NUM: [1-9][0-9]*;
VARIABLE:[a-zA-Z][a-zA-Z0-9]*;
WS
	:	[ \r\t\n] -> skip
	;
