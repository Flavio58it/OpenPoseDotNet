﻿using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace OpenPoseDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern IntPtr op_PoseCpuRenderer_new(PoseModel poseModel,
                                                           float renderThreshold,
                                                           bool blendOriginalFrame,
                                                           float alphaKeyPoint,
                                                           float alphaHeatMap,
                                                           uint elementToRender);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void op_PoseCpuRenderer_delete(IntPtr renderer);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void op_PoseCpuRenderer_initializationOnThread(IntPtr renderer);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void op_PoseCpuRenderer_renderPose(IntPtr renderer,
                                                                IntPtr outputData,
                                                                IntPtr poseKeypoints,
                                                                float scaleInputToOutput,
                                                                float scaleNetToOutput,
                                                                out int item1,
                                                                out IntPtr item2);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern void op_PoseCpuRenderer_setElementToRender(IntPtr renderer, int elementToRender);

    }

}
