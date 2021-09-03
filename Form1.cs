using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinaryCalculator
{
    enum Operation { 
        AND,
        OR,
        XOR,
        NOT,
        NONE
    }
    public partial class Calculator : Form
    {
        private Operation currentOperation;
        private String number;
        private String operator1;
        private String operator2;

        public Calculator()
        {
            number = "";
            operator1 = "";
            operator2 = "";
            currentOperation = Operation.NONE;
            InitializeComponent();
        }

        private void button0_Click(object sender, EventArgs e)
        {
            if (number.Count() < 8) {
                number += "0";
                textBox.Text += "0";
            }         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (number.Count() < 8)
            {
                number += "1";
                textBox.Text += "1";
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBox.Text = "";
            textOperator1.Text = "";
            textOperation.Text = "";
            number = "";
            textBoxResult.Text = "";
            currentOperation = Operation.NONE;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if(textBox.Text.Length > 0)
            {
                if(textBox.Text[textBox.Text.Length - 1] == ' ')
                {
                    while(textBox.Text[textBox.Text.Length - 1] != '0' && textBox.Text[textBox.Text.Length - 1] != '1')
                    {
                        textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1, 1);
                    }
                }
                else
                {
                    textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1, 1);
                    number = number.Remove(number.Length - 1, 1);
                }               
            }           
        }

        private void buttonAND_Click(object sender, EventArgs e)
        {
            currentOperation = Operation.AND;
            if (textBoxResult.Text.Count() == 0)
            {
                textBox.Text += " AND ";
                number = "";
            }
            else
            {
                textBox.Text = textBoxResult.Text;
                textBox.Text += " AND ";
                number = "";
                textBoxResult.Text = "";
            }           
        }

        private void buttonOR_Click(object sender, EventArgs e)
        {
            currentOperation = Operation.OR;
            if (textBoxResult.Text.Count() == 0)
            {
                textBox.Text += " OR ";
                number = "";
                
            }
            else
            {
                textBox.Text = textBoxResult.Text;
                textBox.Text += " OR ";
                number = "";
                textBoxResult.Text = "";
            }            
        }

        private void buttonXOR_Click(object sender, EventArgs e)
        {
            currentOperation = Operation.XOR;
            if (textBoxResult.Text.Count() == 0)
            {
                textBox.Text += " XOR ";
                number = "";

            }
            else
            {
                textBox.Text = textBoxResult.Text;
                textBox.Text += " XOR ";
                number = "";
                textBoxResult.Text = "";
            }
        }

        private void buttonNOT_Click(object sender, EventArgs e)
        {
            currentOperation = Operation.NOT;
            if (textBoxResult.Text.Count() == 0)
            {
                textBox.Text += " NOT ";
                number = "";

            }
            else
            {
                textBox.Text = textBoxResult.Text;
                textBox.Text += " NOT ";
                number = "";
                textBoxResult.Text = "";
            }
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            String num = "";
            foreach(char c in textBox.Text) {
                if (c == '1' || c == '0')
                {
                    num += c;
                }else if(c == ' ' && num.Count() > 0)
                {
                    textOperator1.Text = num;
                    operator1 = num;
                    num = "";
                }
            }
            operator2 = num;
            textOperation.Text = num;
            solveCurrentOperation();
        }

        private void solveCurrentOperation() {
            setEqualLengths();
            switch (currentOperation) {
                case Operation.AND:
                    solveAND();
                    break;
                case Operation.OR:
                    solveOR();
                    break;
                case Operation.XOR:
                    solveXOR();
                    break;
                case Operation.NOT:
                    solveNOT();
                    break;
            }
        }

        private void setEqualLengths() {
            String diff = "";

            for(int i = 0; i < Math.Abs(operator1.Count() - operator2.Count()); i++)
            {
                diff += "0";
            }

            if(operator1.Count() < operator2.Count())
            {
                operator1 = operator1.Insert(0, diff);
            }else if(operator2.Count() < operator1.Count())
            {
                operator2 = operator2.Insert(0, diff);
            }
        }

        private void solveAND() {
            for(int i = 0; i < operator1.Count(); i++)
            {
                if(operator1[i] == '1' && operator2[i] == '1')
                {
                    textBoxResult.Text += '1';
                }
                else
                {
                    textBoxResult.Text += '0';
                }
            }
        }

        private void solveOR()
        {
            for (int i = 0; i < operator1.Count(); i++)
            {
                if (operator1[i] == '1' || operator2[i] == '1')
                {
                    textBoxResult.Text += '1';
                }
                else
                {
                    textBoxResult.Text += '0';
                }
            }
        }

        private void solveXOR()
        {
            for (int i = 0; i < operator1.Count(); i++)
            {
                if ((operator1[i] == '1' || operator2[i] == '1') && operator1[i] != operator2[i])
                {
                    textBoxResult.Text += '1';
                }
                else
                {
                    textBoxResult.Text += '0';
                }
            }
        }

        private void solveNOT()
        {
            foreach(char bit in operator1)
            {
                if(bit == '1')
                {
                    textBoxResult.Text += '0';
                }
                else if(bit == '0')
                {
                    textBoxResult.Text += '1';
                }
            }
        }        
    }
}
