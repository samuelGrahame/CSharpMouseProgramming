using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpMouseProgramming.CodeViewerObjects
{
    public class CodeViewer : Control
    {
        public List<Conponent> Conponents = new List<Conponent>();
        private bool inUpdate;
        public float WorldX;
        public float WorldY;

        public void BeginUpdate()
        {
            inUpdate = true;
        }

        public void EndUpdate()
        {
            inUpdate = false;
        }

        public CodeViewer()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            var g = e.Graphics;

            g.TranslateTransform(-WorldX, -WorldY);

            var length = Conponents.Count;
            for (int i = 0; i < length; i++)
            {
                Conponents[i].OnRender(g);
            }            
            base.OnPaint(e);
        }

        public void AddConponent<T>(T obj) where T : Conponent
        { 
            Conponents.Add(obj);
            RequestRefresh();
        }

        public void RequestRefresh()
        {
            if (inUpdate)
                return;

            this.Refresh();
        }

        public void AddConponent<T>() where T : Conponent, new()
        {
            Conponents.Add(new T());
            RequestRefresh();
        }

        public void RemoveConponent<T>() where T : Conponent
        {
            for (int i = Conponents.Count - 1; i >= 0; i--)
            {
                if(Conponents[i] is T)
                {
                    Conponents.RemoveAt(i);
                    RequestRefresh();
                    return;
                }
            }
        }

        public void RemoveConponent<T>(T obj) where T : Conponent
        {
            Conponents.Remove(obj);
            RequestRefresh();
        }
    }
}
