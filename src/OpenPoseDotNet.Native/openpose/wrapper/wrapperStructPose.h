#ifndef _CPP_OP_WRAPPER_WRAPPERSTRUCTPOSE_H_
#define _CPP_OP_WRAPPER_WRAPPERSTRUCTPOSE_H_

#include "../shared.h"

DLLEXPORT op::WrapperStructPose* op_wrapperStructPose_new(const op::PoseMode poseMode,
                                                          const op::Point<int>* netInputSize,
                                                          const op::Point<int>* outputSize,
                                                          const op::ScaleMode keypointScale,
                                                          const int gpuNumber,
                                                          const int gpuNumberStart,
                                                          const int scalesNumber,
                                                          const float scaleGap,
                                                          const op::RenderMode renderMode,
                                                          const op::PoseModel poseModel,
                                                          const bool blendOriginalFrame,
                                                          const float alphaKeypoint,
                                                          const float alphaHeatMap,
                                                          const int defaultPartToRender,
                                                          const char* modelFolder,
                                                          const std::vector<op::HeatMapType>* heatMapTypes,
                                                          const op::ScaleMode heatMapScale,
                                                          const bool addPartCandidates,
                                                          const float renderThreshold,
                                                          const int numberPeopleMax,
                                                          const bool maximizePositives,
                                                          const double fpsMax,
                                                          const char* protoTxtPath,
                                                          const char* caffeModelPath,
                                                          const bool enableGoogleLogging)
{
    const auto& tmp_heatMapTypes = *heatMapTypes;
    return new op::WrapperStructPose(poseMode,
                                     *netInputSize,
                                     *outputSize,
                                     keypointScale,
                                     gpuNumber,
                                     gpuNumberStart,
                                     scalesNumber,
                                     scaleGap,
                                     renderMode,
                                     poseModel,
                                     blendOriginalFrame,
                                     alphaKeypoint,
                                     alphaHeatMap,
                                     defaultPartToRender,
                                     std::string(modelFolder),
                                     tmp_heatMapTypes,
                                     heatMapScale,
                                     addPartCandidates,
                                     renderThreshold,
                                     numberPeopleMax,
                                     maximizePositives,
                                     fpsMax,
                                     std::string(protoTxtPath),
                                     std::string(caffeModelPath),
                                     enableGoogleLogging);
}

DLLEXPORT void op_wrapperStructPose_delete(op::WrapperStructPose* pose)
{
    delete pose;
}

DLLEXPORT op::PoseMode op_wrapperStructPose_get_poseMode(op::WrapperStructPose* pose)
{
    return pose->poseMode;
}

DLLEXPORT void op_wrapperStructPose_set_poseMode(op::WrapperStructPose* pose, const op::PoseMode poseMode)
{
    pose->poseMode = poseMode;
}

#endif