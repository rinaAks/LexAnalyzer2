using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static lexAnalyzerForms.Lexer;
using static lexAnalyzerForms.ParserClass.StateParser;

namespace lexAnalyzerForms
{
    public class ParserClass
    {
        public class StateParser
        {
            public StateState state;
            public LexemType type;
            public enum StateState
            {
                None = 0,
                F,
                I,
                M,
                A,
                N,
                Q,
                S,
                U,
                T,
                V,
                L,
                H,
                C,
                Ъ,
                K,
                D,
                O,
                J,
                B,
                W,
                END
            };

            //не нужно
            public enum aa
            {
                //DEFAULT = -3,
                EMPTY,
                //ERROR = -1,
                INTEGER,
                DECIMAL,
                PLUS,
                MINUS,
                MULTIPLY,
                DIVIDE,
                LPAREN,
                RPAREN,
                SEMICOLON,
                COMMA,
                LBRACE,
                RBRACE,
                LSQUARE,
                RSQUARE,
                NOT,
                LESS,
                GREATER,
                LESS_OR_EQUAL,
                GREATER_OR_EQUAL,
                EQUAL,
                NOT_EQUALS,
                ASSIGN,
                INT_DECLARE,
                FLOAT_DECLARE,
                ARRAY_DECLARE,
                INPUT,
                OUTPUT,
                IF,
                ELSE,
                WHILE,
                AND,
                OR,
                NAME,

                END
            }
        };


        public class Parser
        {
            public List<StateParser> magasin = new List<StateParser>();
            StateParser parsForAdding;
            public Parser()
            {
                magasin.Add(new StateParser());
                magasin[0].state = StateState.F;
            }

            //public List<string> Stroka = new List<string> { "int", "a", ":=", "5", ";", "\n", "a", "=", "a", "+", "5", ";", "END" };


            int i = 0;
            public Lexem Lex()
            {
                return Form1.myStorage[i];
            }

            public void Ops()
            {
                Lexem lex = Lex();
                while (lex.Type != LexemType.END && magasin[0].state != StateState.END)
                {
                    i++;

                    StateParser stateMagas = magasin[i];
                    magasin.RemoveAt(0);

                    if (stateMagas.type != LexemType.END)
                    {
                        //вызываем функцию pair
                        //в pair передавать нулевое состояние магазина 
                        //и тип лексемы, чтобы понимать, какая комбинация - пара
                        Pair(magasin[0].state, lex.Type);
                    }
                    //
                }
            }

            public void Pair(StateState stateLex, LexemType type)
            {
                List<StateParser> stateParser = new List<StateParser>();
                if (stateLex == StateState.F && type == LexemType.NAME)
                {
                    stateParser.EnsureCapacity(6);
                    stateParser[0].type = LexemType.NAME;
                    stateParser[1].state = StateState.H;
                    stateParser[2].type = LexemType.ASSIGN;
                    stateParser[3].state = StateState.S;
                    stateParser[4].type = LexemType.SEMICOLON;
                    stateParser[5].state = StateState.Q;
                }
                if (stateLex == StateState.F && type == LexemType.INT_DECLARE)
                {
                    stateParser.EnsureCapacity(3);
                    stateParser[0].type = LexemType.INT_DECLARE;
                    stateParser[1].state = StateState.I;
                    stateParser[2].state = StateState.F;
                }
                if (stateLex == StateState.F && type == LexemType.DECIMAL_DECLARE)
                {
                    stateParser.EnsureCapacity(3);
                    stateParser[0].type = LexemType.DECIMAL_DECLARE;
                    stateParser[1].state = StateState.I;
                    stateParser[2].state = StateState.F;
                }
                if (stateLex == StateState.F && type == LexemType.INPUT)
                {
                    stateParser.EnsureCapacity(6);
                    stateParser[0].type = LexemType.INPUT;
                    stateParser[1].type = LexemType.LPAREN;
                    stateParser[2].type = LexemType.NAME;
                    stateParser[3].state = StateState.H;
                    stateParser[4].type = LexemType.RPAREN;
                    stateParser[5].type = LexemType.SEMICOLON;
                    stateParser[6].state = StateState.Q;
                }

                magasin.AddRange(stateParser);
            }

            public string printMagasin()
            {
                string text = "MAGASIN\n";
                foreach (StateParser s in magasin) 
                {
                    text += "type: " + s.type.ToString() + '\n';
                    text += "state: " + s.state.ToString() + '\n';
                }

                return text;
            }
        }
    }
}
