﻿using System;
using OpenPoseDotNet;
using UserDatum = OpenPoseDotNet.CustomDatum;

namespace SynchronousCustomAll
{

    // This worker will just read and return all the jpg files in a directory
    internal sealed class WUserOutput : UserWorkerConsumer<UserDatum>
    {

        #region Methods

        protected override void InitializationOnThread()
        {
        }

        protected override void WorkConsumer(StdSharedPtr<UserDatum>[] datumsPtr)
        {
            try
            {
                // User's displaying/saving/other processing here
                // datum.cvOutputData: rendered frame with pose or heatmaps
                // datum.poseKeypoints: Array<float> with the estimated pose
                if (datumsPtr != null && datumsPtr.Length != 0)
                {
                    var datum = datumsPtr[0].Get();

                    // Show in command line the resulting pose keypoints for body, face and hands
                    OpenPose.Log("\nKeypoints:");
                    // Accesing each element of the keypoints
                    var poseKeypoints = datum.PoseKeyPoints;
                    OpenPose.Log("Person pose keypoints:");
                    for (var person = 0; person < poseKeypoints.GetSize(0); person++)
                    {
                        OpenPose.Log($"Person {person} (x, y, score):");
                        for (var bodyPart = 0; bodyPart < poseKeypoints.GetSize(1); bodyPart++)
                        {
                            var valueToPrint = "";
                            for (var xyscore = 0; xyscore < poseKeypoints.GetSize(2); xyscore++)
                            {
                                valueToPrint += poseKeypoints[new[] { person, bodyPart, xyscore }] + " ";
                            }
                            OpenPose.Log(valueToPrint);
                        }
                    }

                    OpenPose.Log(" ");
                    // Alternative: just getting std::string equivalent
                    OpenPose.Log($"Face keypoints: {datum.FaceKeyPoints}");
                    OpenPose.Log($"Left hand keypoints: {datum.HandKeyPoints[0]}");
                    OpenPose.Log($"Right hand keypoints: {datum.HandKeyPoints[1]}");
                    // Heatmaps
                    var poseHeatMaps = datum.PoseHeatMaps;
                    if (!poseHeatMaps.Empty)
                    {
                        OpenPose.Log($"Pose heatmaps size: [{poseHeatMaps.GetSize(0)}, {poseHeatMaps.GetSize(1)}, {poseHeatMaps.GetSize(2)}]");
                        var faceHeatMaps = datum.FaceHeatMaps;
                        OpenPose.Log($"Face heatmaps size: [{faceHeatMaps.GetSize(0)}, {faceHeatMaps.GetSize(1)}, {faceHeatMaps.GetSize(2)}, {faceHeatMaps.GetSize(3)}]");
                        var handHeatMaps = datum.HandHeatMaps;
                        OpenPose.Log($"Left hand heatmaps size: [{handHeatMaps[0].GetSize(0)}, {handHeatMaps[0].GetSize(1)}, {handHeatMaps[0].GetSize(2)}, {handHeatMaps[0].GetSize(3)}]");
                        OpenPose.Log($"Right hand heatmaps size: [{handHeatMaps[1].GetSize(0)}, {handHeatMaps[1].GetSize(1)}, {handHeatMaps[1].GetSize(2)}, {handHeatMaps[1].GetSize(3)}]");
                    }

                    // Display results (if enabled)
                    if (!Flags.NoDisplay)
                    {
                        using (var cvMat = OpenPose.OP_OP2CVCONSTMAT(datum.CvOutputData))
                        {
                            // Display rendered output image
                            Cv.ImShow($"{OpenPose.OpenPoseNameAndVersion()} - Tutorial C++ API", cvMat);
                            // Display image and sleeps at least 1 ms (it usually sleeps ~5-10 msec to display the image)
                            var key = (char)Cv.WaitKey(1);
                            if (key == 27)
                                this.Stop();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                this.Stop();
                OpenPose.Error(e.Message, -1, nameof(this.WorkConsumer));
            }
        }

        #endregion

    }

}