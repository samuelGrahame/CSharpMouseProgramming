using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMouseProgramming.CodeViewerObjects
{
    public abstract class Conponent
    {
        public float X;
        public float Y;

        public float Width;
        public float Height;  


        
        public virtual void OnRender(Graphics g)
        {

        }
    }
}
