using System;
using ConsoleDraw.Windows.Base;

namespace ConsoleDraw.Inputs.Base
{
    public class Input : AInput
    {
        public Input(int xPostion, int yPostion, int height, int width, String iD, Window win)
        {
            ID = iD;

            Xpostion = xPostion;
            Ypostion = yPostion;

            Height = height;
            Width = width;
            windows = win;
        }

        public override void AddLetter(Char letter) { }
        public override void BackSpace() { }
        public override void CursorMoveLeft() { }
        public override void CursorMoveRight() { }
        public override void CursorMoveUp() { }
        public override void CursorMoveDown() { }
        public override void CursorToStart() { }
        public override void CursorToEnd() { }
        public override void Enter() { }
        
        public override void Unselect() { }
        public override void Select() { }
        public override void Draw() { }
    }
}
