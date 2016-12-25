using ConsoleDraw.Inputs.Base;
using System;
using ConsoleDraw.Windows.Base;

namespace ConsoleDraw.Inputs
{
    public class Button : Input
    {
        private String Text;
        public ConsoleColor BackgroundColour = ConsoleColor.Gray;
        private ConsoleColor TextColour = ConsoleColor.Black;

        private ConsoleColor SelectedBackgroundColour = ConsoleColor.DarkGray;
        private ConsoleColor SelectedTextColour = ConsoleColor.White;

        private bool Selected = false;

        public Action Action;

        public Button(int x, int y, String text, Window win, String iD) : base(x, y, 1, text.Length + 2, iD, win)
        {
            Text = text;
            Selectable = true;
        }

        public override void Select()
        {
            if (!Selected)
            {
                Selected = true;
                Draw();
            }
        }

        public override void Unselect()
        {
            if (Selected)
            {
                Selected = false;
                Draw();
            }
        }

        public override void Enter()
        {
            if (Action != null) //If an action has been set
                Action();
        }

        public override void Draw()
        {
            if(Selected)
                WindowManager.WirteText('['+Text+']', Xpostion, Ypostion, SelectedTextColour, SelectedBackgroundColour);
            else
                WindowManager.WirteText('[' + Text + ']', Xpostion, Ypostion, TextColour, BackgroundColour);  
        }
        
        public override void CursorMoveDown()
        {
        }

        public override void CursorMoveRight()
        {

        }

        public override void CursorMoveLeft()
        {
        }

        public override void CursorMoveUp()
        {
        }
    }
}
