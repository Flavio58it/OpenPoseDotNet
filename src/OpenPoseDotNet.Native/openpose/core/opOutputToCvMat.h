#ifndef _CPP_OP_CORE_OP_OUTPUT_TO_CV_MAT_HPP
#define _CPP_OP_CORE_OP_OUTPUT_TO_CV_MAT_HPP

#include "../shared.h"

DLLEXPORT op::OpOutputToCvMat* op_core_OpOutputToCvMat_new()
{
    return new op::OpOutputToCvMat();
}

DLLEXPORT void op_core_OpOutputToCvMat_delete(op::OpOutputToCvMat* mat)
{
    delete mat;
}

DLLEXPORT cv::Mat* op_core_OpOutputToCvMat_formatToCvMat(const op::OpOutputToCvMat* mat, const op::Array<float>* outputData)
{
    auto& tmp = *outputData;
    auto ret = mat->formatToCvMat(tmp);
    return new cv::Mat(ret);
}

#endif