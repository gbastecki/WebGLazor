using System.Runtime.InteropServices.JavaScript;

namespace WebGLazor.Interop;

public static partial class WebGL2
{
    private const string ModuleName = "webglazor";

    #region Context Management

    [JSImport("initWebGL", ModuleName)]
    public static partial int InitWebGL(string canvasId);

    [JSImport("makeCurrent", ModuleName)]
    public static partial bool MakeCurrent(int contextId);

    [JSImport("getError", ModuleName)]
    public static partial int GetError();

    [JSImport("getParameterInt", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial int GetParameterInt(int pname);

    [JSImport("getParameterFloat", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial float GetParameterFloat(int pname);

    [JSImport("getParameterBool", ModuleName)]
    [return: JSMarshalAs<JSType.Boolean>]
    public static partial bool GetParameterBool(int pname);

    [JSImport("getParameterString", ModuleName)]
    [return: JSMarshalAs<JSType.String>]
    public static partial string? GetParameterString(int pname);


    [JSImport("finish", ModuleName)]
    public static partial void Finish();

    [JSImport("flush", ModuleName)]
    public static partial void Flush();

    [JSImport("disposeContext", ModuleName)]
    public static partial void DisposeContext(int contextId);

    [JSImport("getCanvasBoundsForContext", ModuleName)]
    [return: JSMarshalAs<JSType.Array<JSType.Number>>]
    public static partial double[]? GetCanvasBoundsForContext(int contextId);

    #endregion

    #region State Management

    [JSImport("enable", ModuleName)]
    public static partial void Enable(int cap);

    [JSImport("disable", ModuleName)]
    public static partial void Disable(int cap);

    [JSImport("isEnabled", ModuleName)]
    public static partial bool IsEnabled(int cap);

    [JSImport("clearColor", ModuleName)]
    public static partial void ClearColor(float r, float g, float b, float a);

    [JSImport("clearDepth", ModuleName)]
    public static partial void ClearDepth(float depth);

    [JSImport("clearStencil", ModuleName)]
    public static partial void ClearStencil(int s);

    [JSImport("clear", ModuleName)]
    public static partial void Clear(int mask);

    [JSImport("viewport", ModuleName)]
    public static partial void Viewport(int x, int y, int width, int height);

    [JSImport("scissor", ModuleName)]
    public static partial void Scissor(int x, int y, int width, int height);

    [JSImport("colorMask", ModuleName)]
    public static partial void ColorMask(bool red, bool green, bool blue, bool alpha);

    [JSImport("depthMask", ModuleName)]
    public static partial void DepthMask(bool flag);

    [JSImport("stencilMask", ModuleName)]
    public static partial void StencilMask(int mask);

    [JSImport("stencilMaskSeparate", ModuleName)]
    public static partial void StencilMaskSeparate(int face, int mask);

    #endregion

    #region Blending

    [JSImport("blendColor", ModuleName)]
    public static partial void BlendColor(float red, float green, float blue, float alpha);

    [JSImport("blendEquation", ModuleName)]
    public static partial void BlendEquation(int mode);

    [JSImport("blendEquationSeparate", ModuleName)]
    public static partial void BlendEquationSeparate(int modeRGB, int modeAlpha);

    [JSImport("blendFunc", ModuleName)]
    public static partial void BlendFunc(int sfactor, int dfactor);

    [JSImport("blendFuncSeparate", ModuleName)]
    public static partial void BlendFuncSeparate(int srcRGB, int dstRGB, int srcAlpha, int dstAlpha);

    #endregion

    #region Depth and Stencil

    [JSImport("depthFunc", ModuleName)]
    public static partial void DepthFunc(int func);

    [JSImport("depthRange", ModuleName)]
    public static partial void DepthRange(float zNear, float zFar);

    [JSImport("stencilFunc", ModuleName)]
    public static partial void StencilFunc(int func, int refValue, int mask);

    [JSImport("stencilFuncSeparate", ModuleName)]
    public static partial void StencilFuncSeparate(int face, int func, int refValue, int mask);

    [JSImport("stencilOp", ModuleName)]
    public static partial void StencilOp(int fail, int zfail, int zpass);

    [JSImport("stencilOpSeparate", ModuleName)]
    public static partial void StencilOpSeparate(int face, int fail, int zfail, int zpass);

    #endregion

    #region Face Culling

    [JSImport("cullFace", ModuleName)]
    public static partial void CullFace(int mode);

    [JSImport("frontFace", ModuleName)]
    public static partial void FrontFace(int mode);

    #endregion

    #region Polygon and Line

    [JSImport("lineWidth", ModuleName)]
    public static partial void LineWidth(float width);

    [JSImport("polygonOffset", ModuleName)]
    public static partial void PolygonOffset(float factor, float units);

    #endregion

    #region Pixel Storage

    [JSImport("pixelStorei", ModuleName)]
    public static partial void PixelStorei(int pname, int param);

    [JSImport("hint", ModuleName)]
    public static partial void Hint(int target, int mode);

    [JSImport("sampleCoverage", ModuleName)]
    public static partial void SampleCoverage(float value, bool invert);

    #endregion

    #region Shaders

    [JSImport("createShader", ModuleName)]
    public static partial JSObject CreateShader(int type);

    [JSImport("shaderSource", ModuleName)]
    public static partial void ShaderSource(JSObject shader, string source);

    [JSImport("compileShader", ModuleName)]
    public static partial void CompileShader(JSObject shader);

    [JSImport("getShaderParameter", ModuleName)]
    public static partial JSObject GetShaderParameter(JSObject shader, int pname);

    [JSImport("getShaderParameterInt", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial int GetShaderParameterInt(JSObject shader, int pname);

    [JSImport("getShaderParameterBool", ModuleName)]
    [return: JSMarshalAs<JSType.Boolean>]
    public static partial bool GetShaderParameterBool(JSObject shader, int pname);


    [JSImport("getShaderInfoLog", ModuleName)]
    public static partial string GetShaderInfoLog(JSObject shader);

    [JSImport("getShaderSource", ModuleName)]
    public static partial string GetShaderSource(JSObject shader);

    [JSImport("deleteShader", ModuleName)]
    public static partial void DeleteShader(JSObject shader);

    [JSImport("isShader", ModuleName)]
    public static partial bool IsShader(JSObject shader);

    #endregion

    #region Programs

    [JSImport("createProgram", ModuleName)]
    public static partial JSObject CreateProgram();

    [JSImport("attachShader", ModuleName)]
    public static partial void AttachShader(JSObject program, JSObject shader);

    [JSImport("detachShader", ModuleName)]
    public static partial void DetachShader(JSObject program, JSObject shader);

    [JSImport("linkProgram", ModuleName)]
    public static partial void LinkProgram(JSObject program);

    [JSImport("validateProgram", ModuleName)]
    public static partial void ValidateProgram(JSObject program);

    [JSImport("getProgramParameter", ModuleName)]
    public static partial JSObject GetProgramParameter(JSObject program, int pname);

    [JSImport("getProgramParameterInt", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial int GetProgramParameterInt(JSObject program, int pname);

    [JSImport("getProgramParameterBool", ModuleName)]
    [return: JSMarshalAs<JSType.Boolean>]
    public static partial bool GetProgramParameterBool(JSObject program, int pname);

    [JSImport("getProgramInfoLog", ModuleName)]
    public static partial string GetProgramInfoLog(JSObject program);

    [JSImport("useProgram", ModuleName)]
    public static partial void UseProgram(JSObject? program);

    [JSImport("deleteProgram", ModuleName)]
    public static partial void DeleteProgram(JSObject program);

    [JSImport("isProgram", ModuleName)]
    public static partial bool IsProgram(JSObject program);

    #endregion

    #region Attributes

    [JSImport("getAttribLocation", ModuleName)]
    public static partial int GetAttribLocation(JSObject program, string name);

    [JSImport("bindAttribLocation", ModuleName)]
    public static partial void BindAttribLocation(JSObject program, int index, string name);

    [JSImport("enableVertexAttribArray", ModuleName)]
    public static partial void EnableVertexAttribArray(int index);

    [JSImport("disableVertexAttribArray", ModuleName)]
    public static partial void DisableVertexAttribArray(int index);

    [JSImport("vertexAttribPointer", ModuleName)]
    public static partial void VertexAttribPointer(int index, int size, int type, bool normalized, int stride, int offset);

    [JSImport("vertexAttribIPointer", ModuleName)]
    public static partial void VertexAttribIPointer(int index, int size, int type, int stride, int offset);

    [JSImport("vertexAttribDivisor", ModuleName)]
    public static partial void VertexAttribDivisor(int index, int divisor);

    [JSImport("getVertexAttrib", ModuleName)]
    public static partial JSObject GetVertexAttrib(int index, int pname);

    [JSImport("getVertexAttribInt", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial int GetVertexAttribInt(int index, int pname);

    [JSImport("getVertexAttribFloat", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial float GetVertexAttribFloat(int index, int pname);

    [JSImport("getVertexAttribBool", ModuleName)]
    [return: JSMarshalAs<JSType.Boolean>]
    public static partial bool GetVertexAttribBool(int index, int pname);


    [JSImport("getVertexAttribArray", ModuleName)]
    [return: JSMarshalAs<JSType.Array<JSType.Number>>]
    public static partial double[]? GetVertexAttribArray(int index, int pname);

    #endregion

    #region Buffers

    [JSImport("createBuffer", ModuleName)]
    public static partial JSObject CreateBuffer();

    [JSImport("bindBuffer", ModuleName)]
    public static partial void BindBuffer(int target, JSObject? buffer);

    [JSImport("bindBufferBase", ModuleName)]
    public static partial void BindBufferBase(int target, int index, JSObject buffer);

    [JSImport("bindBufferRange", ModuleName)]
    public static partial void BindBufferRange(int target, int index, JSObject buffer, int offset, int size);

    [JSImport("bufferData", ModuleName)]
    public static partial void BufferData(int target, [JSMarshalAs<JSType.Number>] IntPtr data, int size, int usage);

    [JSImport("bufferSubData", ModuleName)]
    public static partial void BufferSubData(int target, int offset, [JSMarshalAs<JSType.Number>] IntPtr data, int size);

    [JSImport("copyBufferSubData", ModuleName)]
    public static partial void CopyBufferSubData(int readTarget, int writeTarget, int readOffset, int writeOffset, int size);

    [JSImport("getBufferParameter", ModuleName)]
    public static partial JSObject GetBufferParameter(int target, int pname);

    [JSImport("getBufferParameterInt", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial int GetBufferParameterInt(int target, int pname);

    [JSImport("deleteBuffer", ModuleName)]
    public static partial void DeleteBuffer(JSObject buffer);

    [JSImport("isBuffer", ModuleName)]
    public static partial bool IsBuffer(JSObject buffer);

    #endregion

    #region Vertex Array Objects (WebGL 2.0)

    [JSImport("createVertexArray", ModuleName)]
    public static partial JSObject CreateVertexArray();

    [JSImport("bindVertexArray", ModuleName)]
    public static partial void BindVertexArray(JSObject? vertexArray);

    [JSImport("deleteVertexArray", ModuleName)]
    public static partial void DeleteVertexArray(JSObject vertexArray);

    [JSImport("isVertexArray", ModuleName)]
    public static partial bool IsVertexArray(JSObject vertexArray);

    #endregion

    #region Uniforms

    [JSImport("getUniformLocation", ModuleName)]
    public static partial JSObject GetUniformLocation(JSObject program, string name);

    [JSImport("getUniformBlockIndex", ModuleName)]
    public static partial int GetUniformBlockIndex(JSObject program, string uniformBlockName);

    [JSImport("uniformBlockBinding", ModuleName)]
    public static partial void UniformBlockBinding(JSObject program, int uniformBlockIndex, int uniformBlockBinding);

    [JSImport("uniform1f", ModuleName)]
    public static partial void Uniform1f(JSObject location, float x);

    [JSImport("uniform2f", ModuleName)]
    public static partial void Uniform2f(JSObject location, float x, float y);

    [JSImport("uniform3f", ModuleName)]
    public static partial void Uniform3f(JSObject location, float x, float y, float z);

    [JSImport("uniform4f", ModuleName)]
    public static partial void Uniform4f(JSObject location, float x, float y, float z, float w);

    [JSImport("uniform1i", ModuleName)]
    public static partial void Uniform1i(JSObject location, int x);

    [JSImport("uniform2i", ModuleName)]
    public static partial void Uniform2i(JSObject location, int x, int y);

    [JSImport("uniform3i", ModuleName)]
    public static partial void Uniform3i(JSObject location, int x, int y, int z);

    [JSImport("uniform4i", ModuleName)]
    public static partial void Uniform4i(JSObject location, int x, int y, int z, int w);

    [JSImport("uniform1ui", ModuleName)]
    public static partial void Uniform1ui(JSObject location, int x);

    [JSImport("uniformMatrix2fv", ModuleName)]
    public static partial void UniformMatrix2fv(JSObject location, bool transpose, [JSMarshalAs<JSType.Number>] IntPtr data);

    [JSImport("uniformMatrix3fv", ModuleName)]
    public static partial void UniformMatrix3fv(JSObject location, bool transpose, [JSMarshalAs<JSType.Number>] IntPtr data);

    [JSImport("uniformMatrix4fv", ModuleName)]
    public static partial void UniformMatrix4fv(JSObject location, bool transpose, [JSMarshalAs<JSType.Number>] IntPtr data);

    [JSImport("getUniform", ModuleName)]
    public static partial JSObject GetUniform(JSObject program, JSObject location);

    [JSImport("getUniformInt", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial int GetUniformInt(JSObject program, JSObject location);

    [JSImport("getUniformFloat", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial float GetUniformFloat(JSObject program, JSObject location);

    [JSImport("getUniformBool", ModuleName)]
    [return: JSMarshalAs<JSType.Boolean>]
    public static partial bool GetUniformBool(JSObject program, JSObject location);

    [JSImport("getUniformUint", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial double GetUniformUint(JSObject program, JSObject location);

    [JSImport("getUniformArray", ModuleName)]
    [return: JSMarshalAs<JSType.Array<JSType.Number>>]
    public static partial double[]? GetUniformArray(JSObject program, JSObject location);

    #endregion

    #region Drawing

    [JSImport("drawArrays", ModuleName)]
    public static partial void DrawArrays(int mode, int first, int count);

    [JSImport("drawElements", ModuleName)]
    public static partial void DrawElements(int mode, int count, int type, int offset);

    [JSImport("drawArraysInstanced", ModuleName)]
    public static partial void DrawArraysInstanced(int mode, int first, int count, int instanceCount);

    [JSImport("drawElementsInstanced", ModuleName)]
    public static partial void DrawElementsInstanced(int mode, int count, int type, int offset, int instanceCount);

    [JSImport("drawBuffers", ModuleName)]
    public static partial void DrawBuffers([JSMarshalAs<JSType.Array<JSType.Number>>] int[] buffers);

    #endregion

    #region Textures

    [JSImport("createTexture", ModuleName)]
    public static partial JSObject CreateTexture();

    [JSImport("bindTexture", ModuleName)]
    public static partial void BindTexture(int target, JSObject? texture);

    [JSImport("activeTexture", ModuleName)]
    public static partial void ActiveTexture(int texture);

    [JSImport("texImage2D", ModuleName)]
    public static partial void TexImage2D(int target, int level, int internalformat, int width, int height, int border, int format, int type, [JSMarshalAs<JSType.Any>] object? pixels);

    [JSImport("texImage2D_Ptr", ModuleName)]
    public static partial void TexImage2D_Ptr(int target, int level, int internalformat, int width, int height, int border, int format, int type, [JSMarshalAs<JSType.Number>] IntPtr data, int dataLength);

    [JSImport("texImage2DFromImage", ModuleName)]
    public static partial void TexImage2DFromImage(int target, int level, int internalformat, int format, int type, [JSMarshalAs<JSType.Any>] object source);

    [JSImport("texSubImage2D", ModuleName)]
    public static partial void TexSubImage2D(int target, int level, int xoffset, int yoffset, int width, int height, int format, int type, [JSMarshalAs<JSType.Any>] object? pixels);

    [JSImport("texSubImage2D_Ptr", ModuleName)]
    public static partial void TexSubImage2D_Ptr(int target, int level, int xoffset, int yoffset, int width, int height, int format, int type, [JSMarshalAs<JSType.Number>] IntPtr data, int dataLength);

    [JSImport("texImage3D", ModuleName)]
    public static partial void TexImage3D(int target, int level, int internalformat, int width, int height, int depth, int border, int format, int type, [JSMarshalAs<JSType.Any>] object? pixels);

    [JSImport("texParameteri", ModuleName)]
    public static partial void TexParameteri(int target, int pname, int param);

    [JSImport("texParameterf", ModuleName)]
    public static partial void TexParameterf(int target, int pname, float param);

    [JSImport("generateMipmap", ModuleName)]
    public static partial void GenerateMipmap(int target);

    [JSImport("deleteTexture", ModuleName)]
    public static partial void DeleteTexture(JSObject texture);

    [JSImport("isTexture", ModuleName)]
    public static partial bool IsTexture(JSObject texture);

    #endregion

    #region Framebuffers

    [JSImport("createFramebuffer", ModuleName)]
    public static partial JSObject CreateFramebuffer();

    [JSImport("bindFramebuffer", ModuleName)]
    public static partial void BindFramebuffer(int target, JSObject? framebuffer);

    [JSImport("framebufferTexture2D", ModuleName)]
    public static partial void FramebufferTexture2D(int target, int attachment, int textarget, JSObject texture, int level);

    [JSImport("framebufferTextureLayer", ModuleName)]
    public static partial void FramebufferTextureLayer(int target, int attachment, JSObject texture, int level, int layer);

    [JSImport("framebufferRenderbuffer", ModuleName)]
    public static partial void FramebufferRenderbuffer(int target, int attachment, int renderbuffertarget, JSObject renderbuffer);

    [JSImport("checkFramebufferStatus", ModuleName)]
    public static partial int CheckFramebufferStatus(int target);

    [JSImport("deleteFramebuffer", ModuleName)]
    public static partial void DeleteFramebuffer(JSObject framebuffer);

    [JSImport("isFramebuffer", ModuleName)]
    public static partial bool IsFramebuffer(JSObject framebuffer);

    [JSImport("blitFramebuffer", ModuleName)]
    public static partial void BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, int mask, int filter);

    [JSImport("readBuffer", ModuleName)]
    public static partial void ReadBuffer(int src);

    [JSImport("readPixelColor", ModuleName)]
    [return: JSMarshalAs<JSType.Array<JSType.Number>>]
    public static partial int[] ReadPixelColor(int x, int y);

    #endregion

    #region Renderbuffers

    [JSImport("createRenderbuffer", ModuleName)]
    public static partial JSObject CreateRenderbuffer();

    [JSImport("bindRenderbuffer", ModuleName)]
    public static partial void BindRenderbuffer(int target, JSObject? renderbuffer);

    [JSImport("renderbufferStorage", ModuleName)]
    public static partial void RenderbufferStorage(int target, int internalformat, int width, int height);

    [JSImport("renderbufferStorageMultisample", ModuleName)]
    public static partial void RenderbufferStorageMultisample(int target, int samples, int internalformat, int width, int height);

    [JSImport("deleteRenderbuffer", ModuleName)]
    public static partial void DeleteRenderbuffer(JSObject renderbuffer);

    [JSImport("isRenderbuffer", ModuleName)]
    public static partial bool IsRenderbuffer(JSObject renderbuffer);

    #endregion

    #region Samplers (WebGL 2.0)

    [JSImport("createSampler", ModuleName)]
    public static partial JSObject CreateSampler();

    [JSImport("bindSampler", ModuleName)]
    public static partial void BindSampler(int unit, JSObject? sampler);

    [JSImport("samplerParameteri", ModuleName)]
    public static partial void SamplerParameteri(JSObject sampler, int pname, int param);

    [JSImport("samplerParameterf", ModuleName)]
    public static partial void SamplerParameterf(JSObject sampler, int pname, float param);

    [JSImport("deleteSampler", ModuleName)]
    public static partial void DeleteSampler(JSObject sampler);

    [JSImport("isSampler", ModuleName)]
    public static partial bool IsSampler(JSObject sampler);

    #endregion

    #region Queries (WebGL 2.0)

    [JSImport("createQuery", ModuleName)]
    public static partial JSObject CreateQuery();

    [JSImport("beginQuery", ModuleName)]
    public static partial void BeginQuery(int target, JSObject query);

    [JSImport("endQuery", ModuleName)]
    public static partial void EndQuery(int target);

    [JSImport("getQueryParameter", ModuleName)]
    public static partial JSObject GetQueryParameter(JSObject query, int pname);

    [JSImport("getQueryParameterInt", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial int GetQueryParameterInt(JSObject query, int pname);

    [JSImport("getQueryParameterBool", ModuleName)]
    [return: JSMarshalAs<JSType.Boolean>]
    public static partial bool GetQueryParameterBool(JSObject query, int pname);

    [JSImport("deleteQuery", ModuleName)]
    public static partial void DeleteQuery(JSObject query);

    [JSImport("isQuery", ModuleName)]
    public static partial bool IsQuery(JSObject query);

    #endregion

    #region Sync Objects (WebGL 2.0)

    [JSImport("fenceSync", ModuleName)]
    public static partial JSObject FenceSync(int condition, int flags);

    [JSImport("clientWaitSync", ModuleName)]
    public static partial int ClientWaitSync(JSObject sync, int flags, double timeout);

    [JSImport("deleteSync", ModuleName)]
    public static partial void DeleteSync(JSObject sync);

    [JSImport("isSync", ModuleName)]
    public static partial bool IsSync(JSObject sync);

    [JSImport("getSyncParameter", ModuleName)]
    public static partial JSObject GetSyncParameter(JSObject sync, int pname);

    [JSImport("getSyncParameterInt", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial int GetSyncParameterInt(JSObject sync, int pname);


    #endregion

    #region Transform Feedback (WebGL 2.0)

    [JSImport("createTransformFeedback", ModuleName)]
    public static partial JSObject CreateTransformFeedback();

    [JSImport("bindTransformFeedback", ModuleName)]
    public static partial void BindTransformFeedback(int target, JSObject? transformFeedback);

    [JSImport("beginTransformFeedback", ModuleName)]
    public static partial void BeginTransformFeedback(int primitiveMode);

    [JSImport("endTransformFeedback", ModuleName)]
    public static partial void EndTransformFeedback();

    [JSImport("pauseTransformFeedback", ModuleName)]
    public static partial void PauseTransformFeedback();

    [JSImport("resumeTransformFeedback", ModuleName)]
    public static partial void ResumeTransformFeedback();

    [JSImport("transformFeedbackVaryings", ModuleName)]
    public static partial void TransformFeedbackVaryings(JSObject program, [JSMarshalAs<JSType.Array<JSType.String>>] string[] varyings, int bufferMode);

    [JSImport("deleteTransformFeedback", ModuleName)]
    public static partial void DeleteTransformFeedback(JSObject transformFeedback);

    [JSImport("isTransformFeedback", ModuleName)]
    public static partial bool IsTransformFeedback(JSObject transformFeedback);



    #endregion



    #region Buffer Extensions (WebGL 2.0)

    [JSImport("getBufferSubData", ModuleName)]
    public static partial void GetBufferSubData(int target, int srcByteOffset, [JSMarshalAs<JSType.Number>] IntPtr dstData, int dstDataLength, int dstOffset, int length);

    #endregion

    #region Framebuffer Extensions (WebGL 2.0)

    [JSImport("invalidateFramebuffer", ModuleName)]
    public static partial void InvalidateFramebuffer(int target, [JSMarshalAs<JSType.Array<JSType.Number>>] int[] attachments);

    [JSImport("invalidateSubFramebuffer", ModuleName)]
    public static partial void InvalidateSubFramebuffer(int target, [JSMarshalAs<JSType.Array<JSType.Number>>] int[] attachments, int x, int y, int width, int height);

    [JSImport("getFramebufferAttachmentParameter", ModuleName)]
    [return: JSMarshalAs<JSType.Any>]
    public static partial object GetFramebufferAttachmentParameter(int target, int attachment, int pname);

    [JSImport("getFramebufferAttachmentParameterInt", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial int GetFramebufferAttachmentParameterInt(int target, int attachment, int pname);


    #endregion

    #region Renderbuffer Extensions

    [JSImport("getRenderbufferParameter", ModuleName)]
    [return: JSMarshalAs<JSType.Any>]
    public static partial object GetRenderbufferParameter(int target, int pname);

    [JSImport("getRenderbufferParameterInt", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial int GetRenderbufferParameterInt(int target, int pname);


    #endregion

    #region Texture Extensions (WebGL 2.0)

    [JSImport("texStorage2D", ModuleName)]
    public static partial void TexStorage2D(int target, int levels, int internalformat, int width, int height);

    [JSImport("texStorage3D", ModuleName)]
    public static partial void TexStorage3D(int target, int levels, int internalformat, int width, int height, int depth);

    [JSImport("texSubImage3D", ModuleName)]
    public static partial void TexSubImage3D(int target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, int format, int type, [JSMarshalAs<JSType.Any>] object? pixels);

    [JSImport("copyTexSubImage3D", ModuleName)]
    public static partial void CopyTexSubImage3D(int target, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height);

    [JSImport("compressedTexImage2D", ModuleName)]
    public static partial void CompressedTexImage2D(int target, int level, int internalformat, int width, int height, int border, [JSMarshalAs<JSType.Number>] IntPtr data, int dataLength);

    [JSImport("compressedTexSubImage2D", ModuleName)]
    public static partial void CompressedTexSubImage2D(int target, int level, int xoffset, int yoffset, int width, int height, int format, [JSMarshalAs<JSType.Number>] IntPtr data, int dataLength);

    [JSImport("compressedTexImage3D", ModuleName)]
    public static partial void CompressedTexImage3D(int target, int level, int internalformat, int width, int height, int depth, int border, [JSMarshalAs<JSType.Number>] IntPtr data, int dataLength);

    [JSImport("compressedTexSubImage3D", ModuleName)]
    public static partial void CompressedTexSubImage3D(int target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, int format, [JSMarshalAs<JSType.Number>] IntPtr data, int dataLength);

    [JSImport("copyTexImage2D", ModuleName)]
    public static partial void CopyTexImage2D(int target, int level, int internalformat, int x, int y, int width, int height, int border);

    [JSImport("copyTexSubImage2D", ModuleName)]
    public static partial void CopyTexSubImage2D(int target, int level, int xoffset, int yoffset, int x, int y, int width, int height);

    [JSImport("getTexParameter", ModuleName)]
    [return: JSMarshalAs<JSType.Any>]
    public static partial object? GetTexParameter(int target, int pname);

    [JSImport("getTexParameterInt", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial int GetTexParameterInt(int target, int pname);

    [JSImport("getTexParameterFloat", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial float GetTexParameterFloat(int target, int pname);

    #endregion

    #region Program/Shader Extensions

    [JSImport("getFragDataLocation", ModuleName)]
    public static partial int GetFragDataLocation(JSObject program, string name);

    [JSImport("getAttachedShaders", ModuleName)]
    public static partial JSObject[]? GetAttachedShaders(JSObject program);

    [JSImport("getShaderPrecisionFormat", ModuleName)]
    [return: JSMarshalAs<JSType.Any>]
    public static partial object? GetShaderPrecisionFormat(int shaderType, int precisionType);

    [JSImport("getActiveAttrib", ModuleName)]
    public static partial JSObject? GetActiveAttrib(JSObject program, int index);

    [JSImport("getActiveUniform", ModuleName)]
    public static partial JSObject? GetActiveUniform(JSObject program, int index);



    [JSImport("getShaderPrecisionFormatAsArray", ModuleName)]
    [return: JSMarshalAs<JSType.Array<JSType.Number>>]
    public static partial int[]? GetShaderPrecisionFormatAsArray(int shaderType, int precisionType);

    [JSImport("getActiveAttribInfo", ModuleName)]
    [return: JSMarshalAs<JSType.Array<JSType.Any>>]
    public static partial object[]? GetActiveAttribInfo(JSObject program, int index);

    [JSImport("getActiveUniformInfo", ModuleName)]
    [return: JSMarshalAs<JSType.Array<JSType.Any>>]
    public static partial object[]? GetActiveUniformInfo(JSObject program, int index);

    #endregion

    #region Uniform Extensions (WebGL 2.0)

    [JSImport("uniform2ui", ModuleName)]
    public static partial void Uniform2ui(JSObject location, int x, int y);

    [JSImport("uniform3ui", ModuleName)]
    public static partial void Uniform3ui(JSObject location, int x, int y, int z);

    [JSImport("uniform4ui", ModuleName)]
    public static partial void Uniform4ui(JSObject location, int x, int y, int z, int w);

    [JSImport("uniform1fv", ModuleName)]
    public static partial void Uniform1fv(JSObject location, [JSMarshalAs<JSType.Number>] IntPtr data, int count);

    [JSImport("uniform2fv", ModuleName)]
    public static partial void Uniform2fv(JSObject location, [JSMarshalAs<JSType.Number>] IntPtr data, int count);

    [JSImport("uniform3fv", ModuleName)]
    public static partial void Uniform3fv(JSObject location, [JSMarshalAs<JSType.Number>] IntPtr data, int count);

    [JSImport("uniform4fv", ModuleName)]
    public static partial void Uniform4fv(JSObject location, [JSMarshalAs<JSType.Number>] IntPtr data, int count);

    [JSImport("uniform1iv", ModuleName)]
    public static partial void Uniform1iv(JSObject location, [JSMarshalAs<JSType.Number>] IntPtr data, int count);

    [JSImport("uniform2iv", ModuleName)]
    public static partial void Uniform2iv(JSObject location, [JSMarshalAs<JSType.Number>] IntPtr data, int count);

    [JSImport("uniform3iv", ModuleName)]
    public static partial void Uniform3iv(JSObject location, [JSMarshalAs<JSType.Number>] IntPtr data, int count);

    [JSImport("uniform4iv", ModuleName)]
    public static partial void Uniform4iv(JSObject location, [JSMarshalAs<JSType.Number>] IntPtr data, int count);

    [JSImport("uniform1uiv", ModuleName)]
    public static partial void Uniform1uiv(JSObject location, [JSMarshalAs<JSType.Number>] IntPtr data, int count);

    [JSImport("uniform2uiv", ModuleName)]
    public static partial void Uniform2uiv(JSObject location, [JSMarshalAs<JSType.Number>] IntPtr data, int count);

    [JSImport("uniform3uiv", ModuleName)]
    public static partial void Uniform3uiv(JSObject location, [JSMarshalAs<JSType.Number>] IntPtr data, int count);

    [JSImport("uniform4uiv", ModuleName)]
    public static partial void Uniform4uiv(JSObject location, [JSMarshalAs<JSType.Number>] IntPtr data, int count);

    [JSImport("uniformMatrix2x3fv", ModuleName)]
    public static partial void UniformMatrix2x3fv(JSObject location, bool transpose, [JSMarshalAs<JSType.Number>] IntPtr data);

    [JSImport("uniformMatrix3x2fv", ModuleName)]
    public static partial void UniformMatrix3x2fv(JSObject location, bool transpose, [JSMarshalAs<JSType.Number>] IntPtr data);

    [JSImport("uniformMatrix2x4fv", ModuleName)]
    public static partial void UniformMatrix2x4fv(JSObject location, bool transpose, [JSMarshalAs<JSType.Number>] IntPtr data);

    [JSImport("uniformMatrix4x2fv", ModuleName)]
    public static partial void UniformMatrix4x2fv(JSObject location, bool transpose, [JSMarshalAs<JSType.Number>] IntPtr data);

    [JSImport("uniformMatrix3x4fv", ModuleName)]
    public static partial void UniformMatrix3x4fv(JSObject location, bool transpose, [JSMarshalAs<JSType.Number>] IntPtr data);

    [JSImport("uniformMatrix4x3fv", ModuleName)]
    public static partial void UniformMatrix4x3fv(JSObject location, bool transpose, [JSMarshalAs<JSType.Number>] IntPtr data);

    #endregion

    #region Vertex Attribute Extensions

    [JSImport("vertexAttribI4i", ModuleName)]
    public static partial void VertexAttribI4i(int index, int x, int y, int z, int w);

    [JSImport("vertexAttribI4ui", ModuleName)]
    public static partial void VertexAttribI4ui(int index, int x, int y, int z, int w);

    [JSImport("vertexAttrib1f", ModuleName)]
    public static partial void VertexAttrib1f(int index, float x);

    [JSImport("vertexAttrib2f", ModuleName)]
    public static partial void VertexAttrib2f(int index, float x, float y);

    [JSImport("vertexAttrib3f", ModuleName)]
    public static partial void VertexAttrib3f(int index, float x, float y, float z);

    [JSImport("vertexAttrib4f", ModuleName)]
    public static partial void VertexAttrib4f(int index, float x, float y, float z, float w);

    [JSImport("getVertexAttribOffset", ModuleName)]
    public static partial int GetVertexAttribOffset(int index, int pname);

    #endregion

    #region Drawing Extensions (WebGL 2.0)

    [JSImport("drawRangeElements", ModuleName)]
    public static partial void DrawRangeElements(int mode, int start, int end, int count, int type, int offset);

    [JSImport("clearBufferfv", ModuleName)]
    public static partial void ClearBufferfv(int buffer, int drawbuffer, [JSMarshalAs<JSType.Number>] IntPtr values);

    [JSImport("clearBufferiv", ModuleName)]
    public static partial void ClearBufferiv(int buffer, int drawbuffer, [JSMarshalAs<JSType.Number>] IntPtr values);

    [JSImport("clearBufferuiv", ModuleName)]
    public static partial void ClearBufferuiv(int buffer, int drawbuffer, [JSMarshalAs<JSType.Number>] IntPtr values);

    [JSImport("clearBufferfi", ModuleName)]
    public static partial void ClearBufferfi(int buffer, int drawbuffer, float depth, int stencil);

    #endregion

    #region Query Extensions (WebGL 2.0)

    [JSImport("getQuery", ModuleName)]
    public static partial JSObject? GetQuery(int target, int pname);

    #endregion

    #region Sampler Extensions (WebGL 2.0)

    [JSImport("getSamplerParameter", ModuleName)]
    public static partial JSObject? GetSamplerParameter(JSObject sampler, int pname);

    [JSImport("getSamplerParameterInt", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial int GetSamplerParameterInt(JSObject sampler, int pname);

    [JSImport("getSamplerParameterFloat", ModuleName)]
    [return: JSMarshalAs<JSType.Number>]
    public static partial float GetSamplerParameterFloat(JSObject sampler, int pname);

    #endregion

    #region Sync Extensions (WebGL 2.0)

    [JSImport("waitSync", ModuleName)]
    public static partial void WaitSync(JSObject sync, int flags, double timeout);

    #endregion

    #region Uniform Buffer Object Extensions (WebGL 2.0)

    [JSImport("getUniformIndices", ModuleName)]
    [return: JSMarshalAs<JSType.Array<JSType.Number>>]
    public static partial int[]? GetUniformIndices(JSObject program, [JSMarshalAs<JSType.Array<JSType.String>>] string[] uniformNames);





    [JSImport("getActiveUniformBlockName", ModuleName)]
    public static partial string? GetActiveUniformBlockName(JSObject program, int blockIndex);

    #endregion

    #region Context Extensions

    [JSImport("getSupportedExtensions", ModuleName)]
    [return: JSMarshalAs<JSType.Array<JSType.String>>]
    public static partial string[]? GetSupportedExtensions();



    #endregion

    #region Pixel Operations

    [JSImport("readPixels", ModuleName)]
    public static partial void ReadPixels(int x, int y, int width, int height, int format, int type, [JSMarshalAs<JSType.Number>] IntPtr dstData, int dstDataLength);

    #endregion

    #region Color Space Properties (WebGL 2.0)

    [JSImport("getDrawingBufferColorSpace", ModuleName)]
    public static partial string GetDrawingBufferColorSpace();

    [JSImport("setDrawingBufferColorSpace", ModuleName)]
    public static partial void SetDrawingBufferColorSpace(string value);

    [JSImport("getUnpackColorSpace", ModuleName)]
    public static partial string GetUnpackColorSpace();

    [JSImport("setUnpackColorSpace", ModuleName)]
    public static partial void SetUnpackColorSpace(string value);

    #endregion

    #region Animation Loop

    [JSImport("startLoopForContext", ModuleName)]
    public static partial Task StartLoopForContext(int contextId);

    [JSImport("stopLoopForContext", ModuleName)]
    public static partial void StopLoopForContext(int contextId);

    #endregion
}
