﻿
/*
 * This sample program is ported by C# from examples/tutorial_api_cpp/4_asynchronous_loop_custom_input_and_output.cpp.
*/

using System;
using System.Diagnostics;
using OpenPoseDotNet;
using UserDatum = OpenPoseDotNet.CustomDatum;

namespace AsynchronousLoopCustomInputAndOutput
{

    internal class Program
    {

        #region Fields

        private static string ImagePath;

        #endregion

        #region Constructor

        static Program()
        {
            // Custom OpenPose flags
            // Producer
            Flags.ImageDir = "examples/media/";    // Process a directory of images. Read all standard formats (jpg, png, bmp, etc.).
        }

        #endregion

        #region Methods

        private static void Main()
        {
            TutorialApiCpp4();
        }

        #region Helpers

        private static int TutorialApiCpp4()
        {
            try
            {
                OpenPose.Log("Starting OpenPose demo...", Priority.High);
                var timeBegin = new Stopwatch();
                timeBegin.Start();

                // logging_level
                OpenPose.Check(0 <= Flags.LoggingLevel && Flags.LoggingLevel <= 255, "Wrong logging_level value.");
                ConfigureLog.PriorityThreshold = (Priority)Flags.LoggingLevel;
                Profiler.SetDefaultX((ulong)Flags.ProfileSpeed);

                // Applying user defined configuration - GFlags to program variables
                // outputSize
                var outputSize = OpenPose.FlagsToPoint(Flags.OutputResolution, "-1x-1");
                // netInputSize
                var netInputSize = OpenPose.FlagsToPoint(Flags.NetResolution, "-1x368");
                // faceNetInputSize
                var faceNetInputSize = OpenPose.FlagsToPoint(Flags.FaceNetResolution, "368x368 (multiples of 16)");
                // handNetInputSize
                var handNetInputSize = OpenPose.FlagsToPoint(Flags.HandNetResolution, "368x368 (multiples of 16)");
                // poseModel
                var poseModel = OpenPose.FlagsToPoseModel(Flags.ModelPose);
                // JSON saving
                if (!string.IsNullOrEmpty(Flags.WriteKeyPoint))
                    OpenPose.Log("Flag `write_keypoint` is deprecated and will eventually be removed. Please, use `write_json` instead.", Priority.Max);
                // keypointScale
                var keypointScale = OpenPose.FlagsToScaleMode(Flags.KeyPointScale);
                // heatmaps to add
                var heatMapTypes = OpenPose.FlagsToHeatMaps(Flags.HeatmapsAddParts, Flags.HeatmapsAddBackground, Flags.HeatmapsAddPAFs);
                var heatMapScale = OpenPose.FlagsToHeatMapScaleMode(Flags.HeatmapsScale);
                // >1 camera view?
                var multipleView = (Flags.Enable3D || Flags.Views3D > 1);
                // Enabling Google Logging
                const bool enableGoogleLogging = true;

                // Configuring OpenPose
                OpenPose.Log("Configuring OpenPose...", Priority.High);
                using (var opWrapperT = new Wrapper<UserDatum>(ThreadManagerMode.Asynchronous))
                {
                    // Pose configuration (use WrapperStructPose{} for default and recommended configuration)
                    using (var pose = new WrapperStructPose(!Flags.BodyDisabled,
                                                            netInputSize,
                                                            outputSize,
                                                            keypointScale,
                                                            Flags.NumGpu,
                                                            Flags.NumGpuStart,
                                                            Flags.ScaleNumber,
                                                            (float)Flags.ScaleGap,
                                                            OpenPose.FlagsToRenderMode(Flags.RenderPose, multipleView),
                                                            poseModel,
                                                            !Flags.DisableBlending,
                                                            (float)Flags.AlphaPose,
                                                            (float)Flags.AlphaHeatmap,
                                                            Flags.PartToShow,
                                                            Flags.ModelFolder,
                                                            heatMapTypes,
                                                            heatMapScale,
                                                            Flags.PartCandidates,
                                                            (float)Flags.RenderThreshold,
                                                            Flags.NumberPeopleMax,
                                                            Flags.MaximizePositives,
                                                            Flags.FpsMax,
                                                            enableGoogleLogging))
                    // Face configuration (use op::WrapperStructFace{} to disable it)
                    using (var face = new WrapperStructFace(Flags.Face,
                                                            faceNetInputSize,
                                                            OpenPose.FlagsToRenderMode(Flags.FaceRender, multipleView, Flags.RenderPose),
                                                            (float)Flags.FaceAlphaPose,
                                                            (float)Flags.FaceAlphaHeatmap,
                                                            (float)Flags.FaceRenderThreshold))
                    // Hand configuration (use op::WrapperStructHand{} to disable it)
                    using (var hand = new WrapperStructHand(Flags.Hand,
                                                            handNetInputSize,
                                                            Flags.HandScaleNumber,
                                                            (float)Flags.HandScaleRange, Flags.HandTracking,
                                                            OpenPose.FlagsToRenderMode(Flags.HandRender, multipleView, Flags.RenderPose),
                                                            (float)Flags.HandAlphaPose,
                                                            (float)Flags.HandAlphaHeatmap,
                                                            (float)Flags.HandRenderThreshold))
                    // Extra functionality configuration (use op::WrapperStructExtra{} to disable it)
                    using (var extra = new WrapperStructExtra(Flags.Enable3D,
                                                              Flags.MinViews3D,
                                                              Flags.Identification,
                                                              Flags.Tracking,
                                                              Flags.IkThreads))
                    // Output (comment or use default argument to disable any output)
                    using (var output = new WrapperStructOutput(Flags.CliVerbose,
                                                                Flags.WriteKeyPoint,
                                                                OpenPose.StringToDataFormat(Flags.WriteKeyPointFormat),
                                                                Flags.WriteJson,
                                                                Flags.WriteCocoJson,
                                                                Flags.WriteCocoFootJson,
                                                                Flags.WriteCocoJsonVariant,
                                                                Flags.WriteImages,
                                                                Flags.WriteImagesFormat,
                                                                Flags.WriteVideo,
                                                                Flags.WriteVideoFps,
                                                                Flags.WriteHeatmaps,
                                                                Flags.WriteHeatmapsFormat,
                                                                Flags.WriteVideoAdam,
                                                                Flags.WriteBvh,
                                                                Flags.UdpHost,
                                                                Flags.UdpPort))
                    {
                        opWrapperT.Configure(pose);
                        opWrapperT.Configure(face);
                        opWrapperT.Configure(hand);
                        opWrapperT.Configure(extra);
                        opWrapperT.Configure(output);

                        // No GUI. Equivalent to: opWrapper.configure(op::WrapperStructGui{});
                        // Set to single-thread (for sequential processing and/or debugging and/or reducing latency)
                        if (Flags.DisableMultiThread)
                            opWrapperT.DisableMultiThreading();

                        // Start, run, and stop processing - exec() blocks this thread until OpenPose wrapper has finished
                        OpenPose.Log("Starting thread(s)...", Priority.High);
                        opWrapperT.Start();

                        // User processing
                        var userInputClass = new UserInputClass(Flags.ImageDir);
                        var userOutputClass = new UserOutputClass();
                        var userWantsToExit = false;
                        while (!userWantsToExit && !userInputClass.IsFinished())
                        {
                            // Push frame
                            using (var datumToProcess = userInputClass.CreateDatum())
                            {
                                if (datumToProcess != null)
                                {
                                    var successfullyEmplaced = opWrapperT.WaitAndEmplace(datumToProcess);
                                    // Pop frame
                                    if (successfullyEmplaced && opWrapperT.WaitAndPop(out var datumProcessed))
                                    {
                                        userWantsToExit = userOutputClass.Display(datumProcessed);
                                        userOutputClass.PrintKeyPoints(datumProcessed);
                                        datumProcessed.Dispose();
                                    }
                                    else
                                        OpenPose.Log("Processed datum could not be emplaced.", Priority.High, -1, nameof(TutorialApiCpp4));
                                }
                            }
                        }
                    }

                    OpenPose.Log("Stopping thread(s)", Priority.High);
                    opWrapperT.Stop();
                }

                // Measuring total time
                timeBegin.Stop();
                var totalTimeSec = timeBegin.ElapsedMilliseconds / 1000d;
                var message = $"OpenPose demo successfully finished. Total time: {totalTimeSec} seconds.";
                OpenPose.Log(message, Priority.High);

                // Return successful message
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        #endregion

        #endregion

    }

}