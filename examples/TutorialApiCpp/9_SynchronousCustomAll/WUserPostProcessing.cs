﻿using System;
using OpenPoseDotNet;
using UserDatum = OpenPoseDotNet.CustomDatum;

namespace SynchronousCustomAll
{

    internal sealed class WUserPostProcessing : UserWorker<UserDatum>
    {

        #region Constructors

        public WUserPostProcessing()
        {
            // User's constructor here
        }

        #endregion

        #region Methods

        #region Overrides

        protected override void InitializationOnThread()
        {
        }

        protected override void Work(UserDatum[] datums)
        {
            try
            {
                // User's post-processing (after OpenPose processing & before OpenPose outputs) here
                // datum.cvOutputData: rendered frame with pose or heatmaps
                // datum.poseKeypoints: Array<float> with the estimated pose
                if (datums != null && datums.Length != 0)
                    foreach (var datum in datums)
                        Cv.BitwiseNot(datum.CvOutputData, datum.CvOutputData);
            }
            catch (Exception e)
            {
                this.Stop();
                OpenPose.Error(e.Message, -1, nameof(this.Work));
            }
        }

        #endregion

        #endregion

    }

}