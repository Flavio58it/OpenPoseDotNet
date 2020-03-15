﻿using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace OpenPoseDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr op_loadImage(byte[] fullFilePath, LoadImageFlag openCvFlags);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern DataFormat op_stringToDataFormat(byte[] dataFormat);

    }

}
