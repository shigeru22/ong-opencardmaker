using System;
using System.Runtime.InteropServices;

namespace OpenCardMaker.Operations.Interop
{
    public static class ObjectManipulator
    {
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);
    }
}
