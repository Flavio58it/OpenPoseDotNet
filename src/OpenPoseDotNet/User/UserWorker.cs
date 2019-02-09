﻿using System;

// ReSharper disable once CheckNamespace
namespace OpenPoseDotNet
{

    public class UserWorker<T> : Worker<T>
        where T : Datum
    {

        #region Fields

        private readonly OpenPose.DataType _DataType;

        private readonly UserWorkerDelegateMediator _Mediator;

        #endregion

        #region Constructors

        protected UserWorker() :
            base(IntPtr.Zero)
        {
            this._DataType = GenericHelpers.CheckDatumSupportTypes<T>();
            this._Mediator = new UserWorkerDelegateMediator(this._DataType)
            {
                InitializationOnThread = this.OnInitializationOnThread,
                Work = this.OnWork
            };
            this._Mediator.Setup();

            this.NativePtr = this._Mediator.NativePtr;
        }

        internal UserWorker(IntPtr ptr, bool isEnabledDispose = true) :
            base(ptr, isEnabledDispose)
        {
            this._DataType = GenericHelpers.CheckDatumSupportTypes<T>();
            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public bool IsRunning
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.op_UserWorker_isRunning(this._DataType, this.NativePtr);
            }
        }

        #endregion

        #region Methods

        protected virtual void InitializationOnThread()
        {
            this.ThrowIfDisposed();
        }

        public void Stop()
        {
            this.ThrowIfDisposed();
            NativeMethods.op_UserWorker_stop(this._DataType, this.NativePtr);
        }

        protected virtual void Work(StdSharedPtr<T>[] datums)
        {
            this.ThrowIfDisposed();
        }

        #region Overrides

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.op_UserWorker_delete(this._DataType, this.NativePtr);

            this._Mediator?.Dispose();
        }

        #endregion

        #region Helpers

        private void OnInitializationOnThread()
        {
            this.InitializationOnThread();
        }

        private void OnWork(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
            {
                this.Work(null);
                return;
            }

            // ptr is std::shared_ptr<std::vector<std::shared_ptr<TDatum>>>
            var content = NativeMethods.std_shared_ptr_TDatum_get(this._DataType, ptr);
            if (content == IntPtr.Zero)
            {
                this.Work(null);
                return;
            }

            using (var vector = new StdVector<StdSharedPtr<T>>(content, false))
                this.Work(vector.ToArray());
        }

        #endregion

        #endregion

    }

}