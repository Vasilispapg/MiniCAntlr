grammar MiniC;

/*
 * Parser Rules
 */

compileUnit:	statement
	| compound_st statement
	;

statement:
	|	(expr SEMICOLON)+
	|	FUNCTION LP expr* RP compound_st
	|	IF LP expr+ RP compound_st
	|	WHILE LP expr+ RP compound_st
	|	SEMICOLON+
	|	RETURN expr SEMICOLON
	;

	compound_st : 	LBRACKET statement* RBRACKET
	|		LBRACKET  RBRACKET
	;

expr: NUM							#Number
	|   VARIABLE					#Variable
	|	expr op=(MULT|DIV)expr		#Mult_div
	|	expr op=(PLUS|MINUS)expr	#Add_sub
	|	VARIABLE ASSIGN expr		#Assignment
	|	LP expr RP					#Parenthesis
	|	expr AND expr	#AndOperator
	|	expr OR expr	#OrOperator
	|	expr LT expr	#LtOperator
	|	expr GT expr	#GtOperator
	|	expr LTE expr	#LteOperator
	|	expr GTE expr	#GteOperator
	|	NOT expr		#NotOperator
	|	expr EQUAL expr	#EqualOperator
	|	expr NEQUAL expr #NequalOperator
	|	expr PLUSPLUS #PlusplusOperator
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

UNION:'union';
UNIQUE:'unique';		
JOIN:'join';	
SETXOR:'setxor';	
SETDIFF:'setdiff';		
ISMEMBER:'ismember';		

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
