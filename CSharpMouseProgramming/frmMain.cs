using CSharpMouseProgramming.CodeViewerObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpMouseProgramming
{
    public partial class frmMain : Form
    {
        public CodeBlock codeBlock = new CodeBlock();
        public Dictionary<string, int> VarCounts = new Dictionary<string, int>();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            codeViewer1.AddConponent(codeBlock);
        }

        private void stringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddVariable("string");
        }

        private string MakeFirstUpper(string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
        }

        public void AddVariable(string type)
        {
            if (!VarCounts.ContainsKey(type))
                VarCounts[type] = 1;
            string name = $"my{MakeFirstUpper(type)}" + (VarCounts[type]++);

            codeBlock.Blocks.Add(new Block(codeViewer1)
            {
                Backcolor = Color.White,
                Source = $"{type} {name}",
                Color = Color.Blue,
                Name = name,
                Type = type,
                IsVariable = true
            });
            codeViewer1.RequestRefresh();
        }

        public void AddKeyword(string word)
        {
            codeBlock.Blocks.Add(new Block(codeViewer1)
            {
                Backcolor = Color.White,
                Source = word,
                Color = Color.Blue
            });
            codeViewer1.RequestRefresh();
        }

        public void AddString(string literal)
        {
            literal = literal.Replace("\"", "\\\"");

            codeBlock.Blocks.Add(new Block(codeViewer1)
            {
                Backcolor = Color.White,
                Source = "\"" + literal + "\"",
                Color = Color.FromArgb(163, 21, 21)
            });
            codeViewer1.RequestRefresh();
        }

        private void semiColonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSymbol(";");
        }

        public void AddSymbol(string symbol)
        {
            codeBlock.Blocks.Add(new Block(codeViewer1)
            {
                Backcolor = Color.White,
                Source = $" {symbol} ",
                Color = Color.Black
            });
            codeViewer1.RequestRefresh();
        }

        public void AddUseVariable(Block block)
        {
            codeBlock.Blocks.Add(new Block(codeViewer1)
            {
                Backcolor = Color.White,
                Source = null,
                IsUseVariable = true,
                Variable = block,
                Color = Color.FromArgb(43, 145, 175)
            });
            codeViewer1.RequestRefresh();
        }

        private void setToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSymbol("=");
        }

        private void equalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSymbol("==");
        }

        private void plusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSymbol("+");
        }

        private void minusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSymbol("-");
        }

        private void integerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddVariable("int");
        }

        private void longToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddVariable("long");
        }

        private void singleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddVariable("float");
        }

        private void doubleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddVariable("double");
        }

        private void decimalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddVariable("decimal");
        }

        private void dateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            AddVariable("DateTime");
        }

        private void stringToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (frmInput input = new frmInput())
            {
                if (input.ShowDialog() == DialogResult.OK)
                    AddString(input.textBox1.Text);

            }
        }

        private void mulitLineStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmBigInput input = new frmBigInput())
            {
                if (input.ShowDialog() == DialogResult.OK)
                {
                    string literal = input.textBox1.Text;

                    literal = literal.Replace("\"", "\\\"");

                    codeBlock.Blocks.Add(new Block(codeViewer1)
                    {
                        Backcolor = Color.White,
                        Source = "@\"" + literal + "\"",
                        Color = Color.FromArgb(163, 21, 21)
                    });
                    codeViewer1.RequestRefresh();
                }
                    

            }
        }

        private void ifToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddKeyword("if");
            AddSymbol("(");
        }

        private void whileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddKeyword("while");
            AddSymbol("(");
        }

        private void forToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddKeyword("for");
            AddSymbol("(");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSymbol("(");
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSymbol(")");
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddSymbol("[");
        }

        private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddSymbol("]");
        }

        private void openToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AddSymbol("{");
        }

        private void closeToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AddSymbol("}");
        }

        private void variablesToolStripMenuItem1_DropDownOpening(object sender, EventArgs e)
        {
            variablesToolStripMenuItem1.DropDownItems.Clear();

            foreach (var item in codeBlock.Blocks)
            {
                var current = item;
                if(item.IsVariable)
                {
                    variablesToolStripMenuItem1.DropDownItems.Add(new ToolStripMenuItem($"{item.Type} {item.Name}", null, (s, ev) => {
                        AddUseVariable(current);
                    }));
                }
            }


        }

        private void smallerThenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSymbol("<");
        }

        private void largerThenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSymbol(">");
        }

        private void smallerAndEqualThenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSymbol("<=");
        }

        private void largerAndEqualThenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSymbol(">=");
        }
    }
}
