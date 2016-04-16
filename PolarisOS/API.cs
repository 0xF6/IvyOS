using System;
using PolarisOS.Driver.Core;

namespace PolarisOS
{
    public class API
    {
        public static void STI() { } //? Plugged

        // Basically the way this works is a number is stored in EAX, this is the function
        // we want to use. All of these can be accessed through software interrupt 0x80
        public static unsafe void HandleInt0x80(ref Cosmos.Core.INTs.IRQContext aContext)
        {
            if (aContext.EAX == 1) // Print
            {
                byte* ptr = (byte*)aContext.ESI;
                for (int i = 0; ptr[i] != 0; i++)
                {
                    Console.Write((char)ptr[i]);
                }
            }
            else if (aContext.EAX == 2) // Read
            {
                STIEnabler se = new STIEnabler();
                se.Enable();
                STI(); //! We need to enable interrupts so we can read, but for some reason this does not work :(

                byte* ptr = (byte*)aContext.EDI; // Input buffer
                string str = Console.ReadLine();
                for (int i = 0; i < str.Length; i++)
                    ptr[i] = (byte)str[i];
            }
        }
    }
}