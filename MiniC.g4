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
	|	SEMICOLON+
	|	breakreturn_st
	;

	breakreturn_st: 	BREAK SEMICOLON #Break
	|	RETURN expr SEMICOLON	#Return
	;

	while_st:	WHILE LP expr RP compound_st
			;
	if_st:	IF LP expr RP compound_st
		  ;

	compound_st : 	LBRACKET statement* RBRACKET
		;

	func_deffinition_st:	FUNCTION VARIABLE? LP parameters RP compound_st
		;

	func_call_st:	VARIABLE LP parameters RP SEMICOLON
					;

	parameters: VARIABLE*
		|	parameters COMMA VARIABLE
		;

	expr: last #last_expr					
		|	expr op=(MULT|DIV)expr		#Mult_div
		|	expr op=(PLUS|MINUS)expr	#Add_sub
		|	VARIABLE ASSIGN expr		#Assignment
		|	LP expr RP					#Parenthesis
		|	expr op=(AND|OR|LT|GT|LTE|GTE|EQUAL|NEQUAL) expr	#Operators
		|	NOT expr		#NotOperator
		|	expr PLUSPLUS #PlusplusOperator
		;

last : NUM	#Number
	   | VARIABLE #Variable
	   ;

/*
 * Lexer Rules
 */

IF: 'if';
WHILE: 'while';
BREAK: 'break';
ELSE:'else';
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

NUM: [1-9][0-9]*;
VARIABLE:[a-zA-Z][a-zA-Z0-9]*;
WS
	:	[ \r\t\n] -> skip
	;
