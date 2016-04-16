namespace PolarisOS.HAL.KeyBoard.Layout
{
    using System.Collections.Generic;
    using Cosmos.HAL;

    /// <summary>
    /// The QWERTY-Implementation.
    /// </summary>
    public sealed class QWERTY : KeyLayoutBase
    {
        protected override void InitKeys()
        {
            _keys = new List<KeyMapping>();


            #region Keys
            AddKey(16u, 'q', ConsoleKeyEx.Q);
            AddKey(1048576u, 'Q', ConsoleKeyEx.Q);
            AddKey(17u, 'w', ConsoleKeyEx.W);
            AddKey(1114112u, 'W', ConsoleKeyEx.W);
            AddKey(18u, 'e', ConsoleKeyEx.E);
            AddKey(1179648u, 'E', ConsoleKeyEx.E);
            AddKey(19u, 'r', ConsoleKeyEx.R);
            AddKey(1245184u, 'R', ConsoleKeyEx.R);
            AddKey(20u, 't', ConsoleKeyEx.T);
            AddKey(1310720u, 'T', ConsoleKeyEx.T);
            AddKey(21u, 'y', ConsoleKeyEx.Y);
            AddKey(1376256u, 'Y', ConsoleKeyEx.Y);
            AddKey(22u, 'u', ConsoleKeyEx.U);
            AddKey(1441792u, 'U', ConsoleKeyEx.U);
            AddKey(23u, 'i', ConsoleKeyEx.I);
            AddKey(1507328u, 'I', ConsoleKeyEx.I);
            AddKey(24u, 'o', ConsoleKeyEx.O);
            AddKey(1572864u, 'O', ConsoleKeyEx.O);
            AddKey(25u, 'p', ConsoleKeyEx.P);
            AddKey(1638400u, 'P', ConsoleKeyEx.P);
            AddKey(30u, 'a', ConsoleKeyEx.A);
            AddKey(1966080u, 'A', ConsoleKeyEx.A);
            AddKey(31u, 's', ConsoleKeyEx.S);
            AddKey(2031616u, 'S', ConsoleKeyEx.S);
            AddKey(32u, 'd', ConsoleKeyEx.D);
            AddKey(2097152u, 'D', ConsoleKeyEx.D);
            AddKey(33u, 'f', ConsoleKeyEx.F);
            AddKey(2162688u, 'F', ConsoleKeyEx.F);
            AddKey(34u, 'g', ConsoleKeyEx.G);
            AddKey(2228224u, 'G', ConsoleKeyEx.G);
            AddKey(35u, 'h', ConsoleKeyEx.H);
            AddKey(2293760u, 'H', ConsoleKeyEx.H);
            AddKey(36u, 'j', ConsoleKeyEx.J);
            AddKey(2359296u, 'J', ConsoleKeyEx.J);
            AddKey(37u, 'k', ConsoleKeyEx.K);
            AddKey(2424832u, 'K', ConsoleKeyEx.K);
            AddKey(38u, 'l', ConsoleKeyEx.L);
            AddKey(2490368u, 'L', ConsoleKeyEx.L);
            AddKey(44u, 'z', ConsoleKeyEx.Z);
            AddKey(2883584u, 'Z', ConsoleKeyEx.Z);
            AddKey(45u, 'x', ConsoleKeyEx.X);
            AddKey(2949120u, 'X', ConsoleKeyEx.X);
            AddKey(46u, 'c', ConsoleKeyEx.C);
            AddKey(3014656u, 'C', ConsoleKeyEx.C);
            AddKey(47u, 'v', ConsoleKeyEx.V);
            AddKey(3080192u, 'V', ConsoleKeyEx.V);
            AddKey(48u, 'b', ConsoleKeyEx.B);
            AddKey(3145728u, 'B', ConsoleKeyEx.B);
            AddKey(49u, 'n', ConsoleKeyEx.N);
            AddKey(3211264u, 'N', ConsoleKeyEx.N);
            AddKey(50u, 'm', ConsoleKeyEx.M);
            AddKey(3276800u, 'M', ConsoleKeyEx.M);
            AddKey(41u, '`', ConsoleKeyEx.NoName);
            AddKey(2686976u, '~', ConsoleKeyEx.NoName);
            AddKey(2u, '1', ConsoleKeyEx.D1);
            AddKey(131072u, '!', ConsoleKeyEx.D1);
            AddKey(3u, '2', ConsoleKeyEx.D2);
            AddKey(196608u, '@', ConsoleKeyEx.D2);
            AddKey(4u, '3', ConsoleKeyEx.D3);
            AddKey(262144u, '#', ConsoleKeyEx.D3);
            AddKey(5u, '4', ConsoleKeyEx.D4);
            AddKey(327680u, '$', ConsoleKeyEx.D5);
            AddKey(6u, '5', ConsoleKeyEx.D5);
            AddKey(393216u, '%', ConsoleKeyEx.D5);
            AddKey(7u, '6', ConsoleKeyEx.D6);
            AddKey(458752u, '^', ConsoleKeyEx.D6);
            AddKey(8u, '7', ConsoleKeyEx.D7);
            AddKey(524288u, '&', ConsoleKeyEx.D7);
            AddKey(9u, '8', ConsoleKeyEx.D8);
            AddKey(589824u, '*', ConsoleKeyEx.D8);
            AddKey(10u, '9', ConsoleKeyEx.D9);
            AddKey(655360u, '(', ConsoleKeyEx.D9);
            AddKey(11u, '0', ConsoleKeyEx.D0);
            AddKey(720896u, ')', ConsoleKeyEx.D0);
            AddKeyWithShift(14u, '\b', ConsoleKeyEx.Backspace);
            AddKeyWithShift(15u, '\t', ConsoleKeyEx.Tab);
            AddKeyWithShift(28u, '\n', ConsoleKeyEx.Enter);
            AddKeyWithShift(57u, ' ', ConsoleKeyEx.Spacebar);
            AddKeyWithShift(75u, '\0', ConsoleKeyEx.LeftArrow);
            AddKeyWithShift(72u, '\0', ConsoleKeyEx.UpArrow);
            AddKeyWithShift(77u, '\0', ConsoleKeyEx.RightArrow);
            AddKeyWithShift(80u, '\0', ConsoleKeyEx.DownArrow);
            AddKeyWithShift(82u, ConsoleKeyEx.Insert);
            AddKeyWithShift(71u, ConsoleKeyEx.Home);
            AddKeyWithShift(73u, ConsoleKeyEx.PageUp);
            AddKeyWithShift(83u, ConsoleKeyEx.Delete);
            AddKeyWithShift(79u, ConsoleKeyEx.End);
            AddKeyWithShift(81u, ConsoleKeyEx.PageDown);
            AddKeyWithShift(55u, ConsoleKeyEx.PrintScreen);
            AddKeyWithShift(59u, ConsoleKeyEx.F1);
            AddKeyWithShift(60u, ConsoleKeyEx.F2);
            AddKeyWithShift(61u, ConsoleKeyEx.F3);
            AddKeyWithShift(62u, ConsoleKeyEx.F4);
            AddKeyWithShift(63u, ConsoleKeyEx.F5);
            AddKeyWithShift(64u, ConsoleKeyEx.F6);
            AddKeyWithShift(65u, ConsoleKeyEx.F7);
            AddKeyWithShift(66u, ConsoleKeyEx.F8);
            AddKeyWithShift(67u, ConsoleKeyEx.F9);
            AddKeyWithShift(68u, ConsoleKeyEx.F10);
            AddKeyWithShift(87u, ConsoleKeyEx.F11);
            AddKeyWithShift(88u, ConsoleKeyEx.F12);
            AddKeyWithShift(1u, ConsoleKeyEx.Escape);
            AddKey(39u, ';', ConsoleKeyEx.NoName);
            AddKey(2555904u, ':', ConsoleKeyEx.NoName);
            AddKey(40u, '\'', ConsoleKeyEx.NoName);
            AddKey(2621440u, '"', ConsoleKeyEx.NoName);
            AddKey(43u, '\\', ConsoleKeyEx.NoName);
            AddKey(2818048u, '|', ConsoleKeyEx.NoName);
            AddKey(26u, '[', ConsoleKeyEx.NoName);
            AddKey(1703936u, '{', ConsoleKeyEx.NoName);
            AddKey(27u, ']', ConsoleKeyEx.NoName);
            AddKey(1769472u, '}', ConsoleKeyEx.NoName);
            #endregion
        }
    }
}