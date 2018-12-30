﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace OpenPoseDotNet
{

    public abstract class PoseExtractorNet : OpenPoseObject
    {

        #region Constructors

        protected PoseExtractorNet()
        {
        }

        protected PoseExtractorNet(IntPtr ptr, bool isEnabledDispose = true):
            base(isEnabledDispose)
        {
            this.NativePtr = ptr;
        }

        #endregion

        #region Methods

        public abstract void ForwardPass(IEnumerable<Array<float>> inputNetData, Point<int> inputDataSize, double[] scaleRatios = null);

        public Array<float> GetPoseKeyPoints()
        {
            this.ThrowIfDisposed();

            var ret = Native.op_PoseExtractorNet_getPoseKeypoints(this.NativePtr);
            return new Array<float>(ret);
        }

        public float GetScaleNetToOutput()
        {
            this.ThrowIfDisposed();
            return Native.op_PoseExtractorNet_getScaleNetToOutput(this.NativePtr);
        }

        public void InitializationOnThread()
        {
            this.ThrowIfDisposed();
            Native.op_PoseExtractorNet_initializationOnThread(this.NativePtr);
        }

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void op_PoseExtractorNet_initializationOnThread(IntPtr net);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr op_PoseExtractorNet_getPoseKeypoints(IntPtr net);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern float op_PoseExtractorNet_getScaleNetToOutput(IntPtr net);

    }

    }

}