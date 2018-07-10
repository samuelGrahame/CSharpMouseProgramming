using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpMouseProgramming.CodeViewerObjects
{
    public class CodeBlock : Conponent
    {
        public List<Block> Blocks = new List<Block>();
        public Dictionary<string, Size> cacheMeasure = new Dictionary<string, Size>();

        public override void OnRender(Graphics g)
        {
            int length = Blocks.Count;
            float y = this.Y;
            float x = this.X + 3;
            int level = 0;
            Size spaceSize = Size.Empty;
            if (length > 0)
            {
                spaceSize = cacheMeasure.ContainsKey("    ") ? cacheMeasure["    "] :
                       (cacheMeasure["    "] = TextRenderer.MeasureText("    ", Blocks[0].Font));
            }
            bool prevNewline = false;
            for (int i = 0; i < length; i++)
            {
                var current = Blocks[i];

                string source = current.IsUseVariable ? current.Variable.Name : current.Source;
                source = source.Replace("\t", "    ");
                
                if (source == " ; ")
                {
                    var size = cacheMeasure.ContainsKey(source) ? cacheMeasure[source] :
                        (cacheMeasure[source] = TextRenderer.MeasureText(source, current.Font));

                    TextRenderer.DrawText(g, source, current.Font,
                        new Rectangle((int)x, (int)y, size.Width, size.Height), current.Color, current.Backcolor);

                    y += size.Height + 3;

                    x = this.X + 3 + (spaceSize.Width * level);

                    prevNewline = true;
                }
                else if (source == " { " || source == " } ")
                {      
                    if(source == " } ")
                        level--;
                    if (level < 0)
                        level = 0;

                    var size = cacheMeasure.ContainsKey(source) ? cacheMeasure[source] :
                        (cacheMeasure[source] = TextRenderer.MeasureText(source, current.Font));
                    if(!prevNewline)
                    {
                        y += size.Height + 3;

                        x = this.X + 3 + (spaceSize.Width * level);
                    }
                    else
                    {
                        x = this.X + 3 + (spaceSize.Width * level);
                    }
                    
                    TextRenderer.DrawText(g, source, current.Font,
                        new Rectangle((int)x, (int)y, size.Width, size.Height), current.Color, current.Backcolor);

                    y += size.Height + 3;
                    prevNewline = true;
                    if (source == " { ")
                        level++;

                    x = this.X + 3 + (spaceSize.Width * level);
                }
                else
                {
                    prevNewline = false;
                    if (source.Contains("\r\n"))
                    {
                        var lines = source.Split(new char[] { '\r', '\n' });
                        string firstLine = lines[0];

                        var size = cacheMeasure.ContainsKey(firstLine) ? cacheMeasure[firstLine] :
                        (cacheMeasure[firstLine] = TextRenderer.MeasureText(firstLine, current.Font));

                        TextRenderer.DrawText(g, firstLine, current.Font,
                            new Rectangle((int)x, (int)y, size.Width, size.Height), current.Color, current.Backcolor);

                        x += size.Width;

                        for (int j = 1; j < lines.Length; j++)
                        {
                            var item = lines[j];

                            y += size.Height + 3;

                            x = this.X + 3 + (spaceSize.Width * level);

                            size = cacheMeasure.ContainsKey(item) ? cacheMeasure[item] :
                                (cacheMeasure[item] = TextRenderer.MeasureText(item, current.Font));

                            TextRenderer.DrawText(g, item, current.Font,
                                new Rectangle((int)x, (int)y, size.Width, size.Height), current.Color, current.Backcolor);

                            x += size.Width;
                        }                     
                    }
                    else
                    {
                        var size = cacheMeasure.ContainsKey(source) ? cacheMeasure[source] :
                        (cacheMeasure[source] = TextRenderer.MeasureText(source, current.Font));

                        TextRenderer.DrawText(g, source, current.Font,
                            new Rectangle((int)x, (int)y, size.Width, size.Height), current.Color, current.Backcolor);

                        x += size.Width;
                    }
                    
                }                    
            }

            base.OnRender(g);
        }
    }

    public class Block
    {
        public string Source;
        public Color Backcolor;
        public Color Color;
        public CodeViewer code;
        public string Name;
        public string Type;



        public bool IsVariable;
        public bool IsUseVariable;
        public Block Variable;

        public Font Font => code.Font;

        public Block(CodeViewer _code)
        {
            code = _code;
        }
    }
}
