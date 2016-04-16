namespace PolarisOS.HAL.KeyBoard
{
    using Cosmos.HAL;
    using Layout;

    // INFO: We recommend to set the keylayout in the BeforeRun() method to make sure that
    //       the arrow keys does not appear as a pretty fuckedup random unicode char..
    public class KeyLayout
    {
        public enum KeyLayouts : byte { QWERTY, QWERTZ, AZERTY }

        private static void ChangeKeyMap(KeyLayoutBase keys)
        {
            Global.Keyboard.SetKeyLayout(keys);
        }

        public static void SwitchKeyLayout(KeyLayouts layout)
        {
            switch (layout)
            {
                case KeyLayouts.AZERTY:
                    ChangeKeyMap(new AZERTY()); break;
                case KeyLayouts.QWERTY:
                    ChangeKeyMap(new QWERTY()); break;
                case KeyLayouts.QWERTZ:
                    ChangeKeyMap(new QWERTZ()); break;
            }
        }
    }
}