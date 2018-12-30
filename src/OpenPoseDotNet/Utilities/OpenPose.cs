﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable once CheckNamespace
namespace OpenPoseDotNet
{

    public static partial class OpenPose
    {

        #region Methods

        #region utilities/check

        public static void Check(bool condition,
                                 string message,
                                 int line = -1,
                                 string function = "",
                                 string file = "")
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var messageBytes = Encoding.UTF8.GetBytes(message);
            var functionBytes = Encoding.UTF8.GetBytes(function ?? "");
            var fileBytes = Encoding.UTF8.GetBytes(file ?? "");

            Native.op_check(condition, messageBytes, line, functionBytes, fileBytes);
        }

        #endregion

        #region utilities/errorAndLog

        public static void DebugLog(string message,
                                    Priority priority = Priority.Max,
                                    int line = -1,
                                    string function = "",
                                    string file = "")
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var messageBytes = Encoding.UTF8.GetBytes(message);
            var functionBytes = Encoding.UTF8.GetBytes(function ?? "");
            var fileBytes = Encoding.UTF8.GetBytes(file ?? "");

            Native.op_dLog(messageBytes, priority, line, functionBytes, fileBytes);
        }

        public static void Error(string message,
                                 int line = -1,
                                 string function = "",
                                 string file = "")
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var messageBytes = Encoding.UTF8.GetBytes(message);
            var functionBytes = Encoding.UTF8.GetBytes(function ?? "");
            var fileBytes = Encoding.UTF8.GetBytes(file ?? "");

            Native.op_error(messageBytes, line, functionBytes, fileBytes);
        }

        public static void Log(string message,
                               Priority priority = Priority.Max,
                               int line = -1,
                               string function = "",
                               string file = "")
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var messageBytes = Encoding.UTF8.GetBytes(message);
            var functionBytes = Encoding.UTF8.GetBytes(function ?? "");
            var fileBytes = Encoding.UTF8.GetBytes(file ?? "");

            Native.op_log(messageBytes, priority, line, functionBytes, fileBytes);
        }

        #endregion

        #region utilities/flagsToOpenPose

        public static Point<int> FlagsToPoint(string pointString, string pointExample)
        {
            if (pointString == null)
                throw new ArgumentNullException(nameof(pointString));
            if (pointExample == null)
                throw new ArgumentNullException(nameof(pointExample));

            var pointStringBytes = Encoding.UTF8.GetBytes(pointString);
            var pointExampleBytes = Encoding.UTF8.GetBytes(pointExample);

            Native.op_flagsToPoint_xy(pointStringBytes, pointExampleBytes, out var x, out var y);
            return new Point<int>(x, y);
        }

        public static RenderMode FlagsToRenderMode(int renderFlag, bool gpuBuggy, int renderPoseFlag = -2)
        {
            return Native.op_flagsToRenderMode(renderFlag, gpuBuggy, renderPoseFlag);
        }

        public static PoseModel FlagsToPoseModel(string poseModeString)
        {
            if (poseModeString == null)
                throw new ArgumentNullException(nameof(poseModeString));

            var poseModeStringBytes = Encoding.UTF8.GetBytes(poseModeString);

            return Native.op_flagsToPoseModel(poseModeStringBytes);
        }

        public static ScaleMode FlagsToScaleMode(int keyPointScale)
        {
            return Native.op_flagsToScaleMode(keyPointScale);
        }

        public static IEnumerable<HeatMapType> FlagsToHeatMaps(bool heatMapsAddParts,
                                                               bool heatMapsAddBkg,
                                                               bool heatMapsAddPAFs)
        {
            var ret = Native.op_flagsToHeatMaps(heatMapsAddParts, heatMapsAddBkg, heatMapsAddPAFs);
            using (var vector = new StdVector<HeatMapType>(ret))
                return vector.ToArray();
        }

        public static ScaleMode FlagsToHeatMapScaleMode(int heatMapScale)
        {
            return Native.op_flagsToHeatMapScaleMode(heatMapScale);
        }


        public static Tuple<ProducerType, string> FlagsToProducer(string imageDirectory,
                                                                  string videoPath,
                                                                  string ipCameraPath,
                                                                  int webcamIndex,
                                                                  bool flirCamera,
                                                                  int flirCameraIndex)
        {
            var imageDirectoryBytes = Encoding.UTF8.GetBytes(imageDirectory ?? "");
            var videoPathBytes = Encoding.UTF8.GetBytes(videoPath ?? "");
            var ipCameraPathBytes = Encoding.UTF8.GetBytes(ipCameraPath ?? "");
            Native.op_flagsToProducer(imageDirectoryBytes,
                                      videoPathBytes,
                                      ipCameraPathBytes,
                                      webcamIndex,
                                      flirCamera,
                                      flirCameraIndex,
                                      out var item1, 
                                      out var item2);

            var str = "";
            if (item2 != IntPtr.Zero)
                str = StringHelper.FromStdString(item2, true);

            return new Tuple<ProducerType, string>(item1, str);
        }

        public static DisplayMode FlagsToDisplayMode(int display, bool enabled3d)
        {
            return Native.op_flagsToDisplayMode(display, enabled3d);
        }

        #endregion

        #endregion

        internal sealed partial class Native
        {

            [StructLayout(LayoutKind.Explicit)]
            internal struct NativePointOfInt32
            {

                [FieldOffset(0)]
                public int x;

                [FieldOffset(4)]
                public int y;

            }

            #region utilities/check

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void op_check(bool condition,
                                               byte[] message,
                                               int line,
                                               byte[] function,
                                               byte[] file);

            #endregion

            #region utilities/errorAndLog

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void op_dLog(byte[] message,
                                                      Priority priority,
                                                      int line,
                                                      byte[] function,
                                                      byte[] file);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void op_error(byte[] message,
                                               int line,
                                               byte[] function,
                                               byte[] file);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void op_log(byte[] message,
                                             Priority priority,
                                             int line,
                                             byte[] function,
                                             byte[] file);

            #endregion

            #region utilities/flagsToOpenPose

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr op_flagsToPoint(byte[] pointString, byte[] pointExample);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void op_flagsToPoint_xy(byte[] pointString, byte[] pointExample, out int x, out int y);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern RenderMode op_flagsToRenderMode(int renderFlag, bool gpuBuggy, int renderPoseFlag);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern PoseModel op_flagsToPoseModel(byte[] poseModeString);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ScaleMode op_flagsToScaleMode(int keyPointScale);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr op_flagsToHeatMaps(bool heatMapsAddParts,
                                                           bool heatMapsAddBkg,
                                                           bool heatMapsAddPAFs);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern ScaleMode op_flagsToHeatMapScaleMode(int heatMapScale);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void op_flagsToProducer(byte[] imageDirectory,
                                                         byte[] videoPath,
                                                         byte[] ipCameraPath,
                                                         int webcamIndex,
                                                         bool flirCamera,
                                                         int flirCameraIndex,
                                                         out ProducerType item1,
                                                         out IntPtr item2);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern DisplayMode op_flagsToDisplayMode(int display, bool enabled3d);

            #endregion

        }

    }

}
