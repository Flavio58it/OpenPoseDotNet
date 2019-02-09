﻿using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace OpenPoseDotNet
{

    internal sealed partial class NativeMethods
    {

        #region op::Arrray

        public enum ArrayElementType
        {

            Float,

            Double

        }

        #region float

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void op_core_Array_float_delete(IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr op_core_Array_float_toString(IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr op_core_Array_float_gets(IntPtr array, int index);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int op_core_Array_float_getSize(IntPtr array, int index);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr op_core_Array_float_getSize2(IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint op_core_Array_float_getNumberDimensions(IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint op_core_Array_float_getVolume(IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool op_core_Array_float_empty(IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern float op_core_Array_float_operator_indexes(IntPtr array, IntPtr indexes);

        #endregion

        #region double

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void op_core_Array_double_delete(IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr op_core_Array_double_toString(IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr op_core_Array_double_gets(IntPtr array, int index);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int op_core_Array_double_getSize(IntPtr array, int index);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr op_core_Array_double_getSize2(IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint op_core_Array_double_getNumberDimensions(IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint op_core_Array_double_getVolume(IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool op_core_Array_double_empty(IntPtr array);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern double op_core_Array_double_operator_indexes(IntPtr array, IntPtr indexes);

        #endregion

        #endregion

        #region op::Point

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr op_core_point_int_new(int x, int y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void op_core_point_int_delete(IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr op_core_point_float_new(float x, float y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void op_core_point_float_delete(IntPtr point);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr op_core_point_double_new(double x, double y);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void op_core_point_double_delete(IntPtr point);

        #endregion

        #region op::Rectangle

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr op_core_rectangle_float_new(float x, float y, float width, float height);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void op_core_rectangle_float_delete(IntPtr rectangle);

        #endregion

    }

}