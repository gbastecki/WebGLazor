using System.Collections.Concurrent;
using System.Runtime.InteropServices.JavaScript;
using WebGLazor.Interop;

namespace WebGLazor
{
    public partial class WebGLContext : IDisposable
    {
        private static readonly ConcurrentDictionary<int, WebGLContext> _instances = new();

        private int _contextId;
        private Action<double>? _frameCallback;
        private bool _isLoopRunning;
        private bool _disposed;

        public int ContextId => _contextId;
        public bool IsInitialized => _contextId > 0;
        public bool IsLoopRunning => _isLoopRunning;
        public bool IsDisposed => _disposed;

        /// <summary>
        /// Initialize the WebGL 2.0 context for the specified canvas element.
        /// </summary>
        /// <param name="canvasId">The HTML id attribute of the canvas element.</param>
        /// <exception cref="Exception">Thrown if WebGL 2.0 initialization fails.</exception>
        public async Task InitializeAsync(string canvasId)
        {
            await JSHost.ImportAsync("webglazor", "/_content/WebGLazor/webglazor.js");
            _contextId = WebGL2.InitWebGL(canvasId);
            if (_contextId == 0)
            {
                throw new Exception("Failed to initialize WebGL 2.0 context.");
            }
            _instances.TryAdd(_contextId, this);
        }

        /// <summary>
        /// Set this context as the active WebGL context for subsequent operations.
        /// </summary>
        public void MakeCurrent() => WebGL2.MakeCurrent(_contextId);

        /// <summary>
        /// Release all WebGL resources and clean up. Stops the animation loop if running.
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            // Stop the animation loop
            if (_isLoopRunning)
            {
                StopLoop();
            }

            // Dispose the JavaScript context
            if (_contextId > 0)
            {
                WebGL2.DisposeContext(_contextId);
                _instances.TryRemove(_contextId, out _);
                _contextId = 0;
            }
        }

        #region Animation Loop

        /// <summary>
        /// Start the animation loop using requestAnimationFrame.
        /// </summary>
        /// <param name="onFrame">Callback invoked each frame with the timestamp in milliseconds.</param>
        public void StartLoop(Action<double> onFrame)
        {
            _frameCallback = onFrame;
            _isLoopRunning = true;
            _ = WebGL2.StartLoopForContext(_contextId);
        }

        /// <summary>
        /// Stop the animation loop.
        /// </summary>
        public void StopLoop()
        {
            _isLoopRunning = false;
            _frameCallback = null;
            WebGL2.StopLoopForContext(_contextId);
        }

        [JSExport]
        public static void OnFrameStatic(int contextId, double timestamp)
        {
            if (_instances.TryGetValue(contextId, out WebGLContext? context) && context._isLoopRunning)
            {
                context._frameCallback?.Invoke(timestamp);
            }
        }

        #endregion

        #region Canvas Utilities

        /// <summary>
        /// Represents the bounding rectangle of a canvas element.
        /// </summary>
        /// <param name="X">Left edge in viewport pixels</param>
        /// <param name="Y">Top edge in viewport pixels</param>
        /// <param name="Width">Display width in pixels (may differ from canvas resolution)</param>
        /// <param name="Height">Display height in pixels (may differ from canvas resolution)</param>
        public record CanvasBounds(double X, double Y, double Width, double Height);

        /// <summary>
        /// Gets the bounding rectangle of the canvas associated with this context.
        /// Useful for converting mouse coordinates to canvas-relative positions.
        /// </summary>
        /// <returns>Canvas bounds, or null if the context is not initialized.</returns>
        public CanvasBounds? GetCanvasBounds()
        {
            if (_contextId == 0) return null;
            var result = WebGL2.GetCanvasBoundsForContext(_contextId);
            if (result == null || result.Length < 4) return null;
            return new CanvasBounds(result[0], result[1], result[2], result[3]);
        }

        #endregion

        #region State Management

        /// <summary>Enable a WebGL capability.</summary>
        /// <param name="cap">Capability to enable (use <see cref="WebGLConsts"/> constants).</param>
        public void Enable(int cap) { MakeCurrent(); WebGL2.Enable(cap); }

        /// <summary>Disable a WebGL capability.</summary>
        /// <param name="cap">Capability to disable (use <see cref="WebGLConsts"/> constants).</param>
        public void Disable(int cap) { MakeCurrent(); WebGL2.Disable(cap); }

        /// <summary>Check if a WebGL capability is enabled.</summary>
        /// <param name="cap">Capability to check.</param>
        /// <returns>True if the capability is enabled.</returns>
        public bool IsEnabled(int cap) { MakeCurrent(); return WebGL2.IsEnabled(cap); }

        /// <summary>Set the color used when clearing the color buffer.</summary>
        /// <param name="r">Red component (0.0-1.0).</param>
        /// <param name="g">Green component (0.0-1.0).</param>
        /// <param name="b">Blue component (0.0-1.0).</param>
        /// <param name="a">Alpha component (0.0-1.0).</param>
        public void ClearColor(float r, float g, float b, float a) { MakeCurrent(); WebGL2.ClearColor(r, g, b, a); }

        /// <summary>Set the depth value used when clearing the depth buffer.</summary>
        /// <param name="depth">Depth value (0.0-1.0, default 1.0).</param>
        public void ClearDepth(float depth) { MakeCurrent(); WebGL2.ClearDepth(depth); }

        /// <summary>Set the stencil value used when clearing the stencil buffer.</summary>
        /// <param name="s">Stencil clear value (default 0).</param>
        public void ClearStencil(int s) { MakeCurrent(); WebGL2.ClearStencil(s); }

        /// <summary>Clear buffers to preset values.</summary>
        /// <param name="mask">Bitwise OR of buffer bits (COLOR_BUFFER_BIT, DEPTH_BUFFER_BIT, STENCIL_BUFFER_BIT).</param>
        public void Clear(int mask) { MakeCurrent(); WebGL2.Clear(mask); }

        /// <summary>Set the viewport rectangle for rendering.</summary>
        /// <param name="x">Left edge in pixels.</param>
        /// <param name="y">Bottom edge in pixels.</param>
        /// <param name="width">Width in pixels.</param>
        /// <param name="height">Height in pixels.</param>
        public void Viewport(int x, int y, int width, int height) { MakeCurrent(); WebGL2.Viewport(x, y, width, height); }

        /// <summary>Define the scissor box for clipping.</summary>
        public void Scissor(int x, int y, int width, int height) { MakeCurrent(); WebGL2.Scissor(x, y, width, height); }

        /// <summary>Set which color components are written to the framebuffer.</summary>
        public void ColorMask(bool red, bool green, bool blue, bool alpha) { MakeCurrent(); WebGL2.ColorMask(red, green, blue, alpha); }

        /// <summary>Set whether the depth buffer is written to.</summary>
        public void DepthMask(bool flag) { MakeCurrent(); WebGL2.DepthMask(flag); }

        /// <summary>Set the front and back stencil write mask.</summary>
        public void StencilMask(int mask) { MakeCurrent(); WebGL2.StencilMask(mask); }

        /// <summary>Set the front or back stencil write mask.</summary>
        public void StencilMaskSeparate(int face, int mask) { MakeCurrent(); WebGL2.StencilMaskSeparate(face, mask); }

        /// <summary>Return the error code of the last WebGL operation.</summary>
        public int GetError() { MakeCurrent(); return WebGL2.GetError(); }

        /// <summary>Block until all WebGL commands are complete.</summary>
        public void Finish() { MakeCurrent(); WebGL2.Finish(); }

        /// <summary>Flush command buffer to GPU without blocking.</summary>
        public void Flush() { MakeCurrent(); WebGL2.Flush(); }



        /// <summary>
        /// Get an integer parameter value. Use for parameters that return integers.
        /// </summary>
        public int GetParameterInt(int pname)
        {
            MakeCurrent();
            return WebGL2.GetParameterInt(pname);
        }

        /// <summary>
        /// Get a float parameter value. Use for parameters that return floating point values.
        /// </summary>
        public float GetParameterFloat(int pname)
        {
            MakeCurrent();
            return WebGL2.GetParameterFloat(pname);
        }

        /// <summary>
        /// Get a boolean parameter value. Use for parameters that return booleans.
        /// </summary>
        public bool GetParameterBool(int pname)
        {
            MakeCurrent();
            return WebGL2.GetParameterBool(pname);
        }

        /// <summary>
        /// Get a string parameter value. Use for parameters that return strings.
        /// </summary>
        public string? GetParameterString(int pname)
        {
            MakeCurrent();
            return WebGL2.GetParameterString(pname);
        }

        #endregion

        #region Blending

        /// <summary>Set the blend color.</summary>
        public void BlendColor(float red, float green, float blue, float alpha) { MakeCurrent(); WebGL2.BlendColor(red, green, blue, alpha); }
        /// <summary>Set the blend equation for both RGB and alpha.</summary>
        public void BlendEquation(int mode) { MakeCurrent(); WebGL2.BlendEquation(mode); }
        /// <summary>Set separate blend equations for RGB and alpha.</summary>
        public void BlendEquationSeparate(int modeRGB, int modeAlpha) { MakeCurrent(); WebGL2.BlendEquationSeparate(modeRGB, modeAlpha); }
        /// <summary>Set the pixel blending factors.</summary>
        public void BlendFunc(int sfactor, int dfactor) { MakeCurrent(); WebGL2.BlendFunc(sfactor, dfactor); }
        /// <summary>Set separate blending factors for RGB and alpha.</summary>
        public void BlendFuncSeparate(int srcRGB, int dstRGB, int srcAlpha, int dstAlpha) { MakeCurrent(); WebGL2.BlendFuncSeparate(srcRGB, dstRGB, srcAlpha, dstAlpha); }

        #endregion

        #region Depth and Stencil

        /// <summary>Set the depth comparison function.</summary>
        public void DepthFunc(int func) { MakeCurrent(); WebGL2.DepthFunc(func); }
        /// <summary>Set the depth range mapping from normalized device coordinates to window coordinates.</summary>
        public void DepthRange(float zNear, float zFar) { MakeCurrent(); WebGL2.DepthRange(zNear, zFar); }
        /// <summary>Set the stencil test function and reference value.</summary>
        public void StencilFunc(int func, int refValue, int mask) { MakeCurrent(); WebGL2.StencilFunc(func, refValue, mask); }
        /// <summary>Set stencil test function separately for front and back faces.</summary>
        public void StencilFuncSeparate(int face, int func, int refValue, int mask) { MakeCurrent(); WebGL2.StencilFuncSeparate(face, func, refValue, mask); }
        /// <summary>Set stencil test actions for stencil/depth pass and fail.</summary>
        public void StencilOp(int fail, int zfail, int zpass) { MakeCurrent(); WebGL2.StencilOp(fail, zfail, zpass); }
        /// <summary>Set stencil test actions separately for front and back faces.</summary>
        public void StencilOpSeparate(int face, int fail, int zfail, int zpass) { MakeCurrent(); WebGL2.StencilOpSeparate(face, fail, zfail, zpass); }

        #endregion

        #region Face Culling

        /// <summary>Specify which faces to cull.</summary>
        public void CullFace(int mode) { MakeCurrent(); WebGL2.CullFace(mode); }
        /// <summary>Specify the winding direction for front-facing polygons.</summary>
        public void FrontFace(int mode) { MakeCurrent(); WebGL2.FrontFace(mode); }

        #endregion

        #region Polygon and Line

        /// <summary>Set the width of rasterized lines.</summary>
        public void LineWidth(float width) { MakeCurrent(); WebGL2.LineWidth(width); }
        /// <summary>Set the scale and units for polygon depth offset.</summary>
        public void PolygonOffset(float factor, float units) { MakeCurrent(); WebGL2.PolygonOffset(factor, units); }

        #endregion

        #region Pixel Storage

        /// <summary>Set pixel storage parameters.</summary>
        public void PixelStorei(int pname, int param) { MakeCurrent(); WebGL2.PixelStorei(pname, param); }
        /// <summary>Set implementation-specific hints.</summary>
        public void Hint(int target, int mode) { MakeCurrent(); WebGL2.Hint(target, mode); }
        /// <summary>Set multisample coverage parameters.</summary>
        public void SampleCoverage(float value, bool invert) { MakeCurrent(); WebGL2.SampleCoverage(value, invert); }

        #endregion

        #region Shaders

        /// <summary>Create a shader object.</summary>
        /// <param name="type">Shader type (VERTEX_SHADER or FRAGMENT_SHADER).</param>
        public WebGLShader CreateShader(int type)
        {
            MakeCurrent();
            return new WebGLShader(WebGL2.CreateShader(type));
        }

        /// <summary>Set the GLSL source code for a shader.</summary>
        /// <param name="shader">Shader object to set source for.</param>
        /// <param name="source">GLSL source code.</param>
        public void ShaderSource(WebGLShader shader, string source)
        {
            MakeCurrent();
            WebGL2.ShaderSource(shader.JsObject, source);
        }

        /// <summary>Compile a shader object.</summary>
        /// <param name="shader">Shader to compile.</param>
        public void CompileShader(WebGLShader shader)
        {
            MakeCurrent();
            WebGL2.CompileShader(shader.JsObject);
        }



        /// <summary>
        /// Get a boolean shader parameter value.
        /// </summary>
        public bool GetShaderParameterBool(WebGLShader shader, int pname)
        {
            MakeCurrent();
            return WebGL2.GetShaderParameterBool(shader.JsObject, pname);
        }

        /// <summary>
        /// Get an integer shader parameter value.
        /// </summary>
        public int GetShaderParameterInt(WebGLShader shader, int pname)
        {
            MakeCurrent();
            return WebGL2.GetShaderParameterInt(shader.JsObject, pname);
        }

        /// <summary>Return the information log for a shader object.</summary>
        public string GetShaderInfoLog(WebGLShader shader)
        {
            MakeCurrent();
            return WebGL2.GetShaderInfoLog(shader.JsObject);
        }

        /// <summary>Delete a shader object.</summary>
        /// <param name="shader">Shader to delete.</param>
        public void DeleteShader(WebGLShader shader)
        {
            MakeCurrent();
            WebGL2.DeleteShader(shader.JsObject);
        }

        /// <summary>Get the GLSL source code of a shader.</summary>
        /// <param name="shader">Shader to get source for.</param>
        public string GetShaderSource(WebGLShader shader)
        {
            MakeCurrent();
            return WebGL2.GetShaderSource(shader.JsObject);
        }

        /// <summary>Check if an object is a valid shader.</summary>
        /// <param name="shader">Object to check.</param>
        public bool IsShader(WebGLShader shader)
        {
            MakeCurrent();
            return WebGL2.IsShader(shader.JsObject);
        }

        #endregion

        #region Programs

        /// <summary>Create a program object.</summary>
        public WebGLProgram CreateProgram()
        {
            MakeCurrent();
            return new WebGLProgram(WebGL2.CreateProgram());
        }

        /// <summary>Attach a shader to a program.</summary>
        /// <param name="program">Program to attach to.</param>
        /// <param name="shader">Compiled shader to attach.</param>
        public void AttachShader(WebGLProgram program, WebGLShader shader)
        {
            MakeCurrent();
            WebGL2.AttachShader(program.JsObject, shader.JsObject);
        }

        /// <summary>Detach a shader from a program.</summary>
        /// <param name="program">Program to detach from.</param>
        /// <param name="shader">Shader to detach.</param>
        public void DetachShader(WebGLProgram program, WebGLShader shader)
        {
            MakeCurrent();
            WebGL2.DetachShader(program.JsObject, shader.JsObject);
        }

        /// <summary>Link a program object.</summary>
        /// <param name="program">Program to link.</param>
        public void LinkProgram(WebGLProgram program)
        {
            MakeCurrent();
            WebGL2.LinkProgram(program.JsObject);
        }

        /// <summary>Validate a program object.</summary>
        /// <param name="program">Program to validate.</param>
        public void ValidateProgram(WebGLProgram program)
        {
            MakeCurrent();
            WebGL2.ValidateProgram(program.JsObject);
        }



        /// <summary>
        /// Get a boolean program parameter value.
        /// </summary>
        public bool GetProgramParameterBool(WebGLProgram program, int pname)
        {
            MakeCurrent();
            return WebGL2.GetProgramParameterBool(program.JsObject, pname);
        }

        /// <summary>
        /// Get an integer program parameter value.
        /// </summary>
        public int GetProgramParameterInt(WebGLProgram program, int pname)
        {
            MakeCurrent();
            return WebGL2.GetProgramParameterInt(program.JsObject, pname);
        }

        /// <summary>Return the information log for a program object.</summary>
        public string GetProgramInfoLog(WebGLProgram program)
        {
            MakeCurrent();
            return WebGL2.GetProgramInfoLog(program.JsObject);
        }

        /// <summary>Set the active program for rendering.</summary>
        /// <param name="program">Program to use.</param>
        public void UseProgram(WebGLProgram program)
        {
            MakeCurrent();
            WebGL2.UseProgram(program.JsObject);
        }

        /// <summary>Delete a program object.</summary>
        /// <param name="program">Program to delete.</param>
        public void DeleteProgram(WebGLProgram program)
        {
            MakeCurrent();
            WebGL2.DeleteProgram(program.JsObject);
        }

        /// <summary>Check if an object is a valid program.</summary>
        /// <param name="program">Object to check.</param>
        public bool IsProgram(WebGLProgram program)
        {
            MakeCurrent();
            return WebGL2.IsProgram(program.JsObject);
        }

        #endregion

        #region Attributes

        public int GetAttribLocation(WebGLProgram program, string name)
        {
            MakeCurrent();
            return WebGL2.GetAttribLocation(program.JsObject, name);
        }

        /// <summary>Bind a generic vertex attribute index to a named attribute variable.</summary>
        public void BindAttribLocation(WebGLProgram program, int index, string name)
        {
            MakeCurrent();
            WebGL2.BindAttribLocation(program.JsObject, index, name);
        }
        #region Vertex Attributes

        /// <summary>Enable a vertex attribute array.</summary>
        /// <param name="index">Attribute location.</param>
        public void EnableVertexAttribArray(int index)
        {
            MakeCurrent();
            WebGL2.EnableVertexAttribArray(index);
        }

        /// <summary>Disable a vertex attribute array.</summary>
        /// <param name="index">Attribute location.</param>
        public void DisableVertexAttribArray(int index)
        {
            MakeCurrent();
            WebGL2.DisableVertexAttribArray(index);
        }

        /// <summary>Define vertex attribute layout.</summary>
        public void VertexAttribPointer(int index, int size, int type, bool normalized, int stride, int offset)
        {
            MakeCurrent();
            WebGL2.VertexAttribPointer(index, size, type, normalized, stride, offset);
        }

        /// <summary>Define integer vertex attribute layout.</summary>
        public void VertexAttribIPointer(int index, int size, int type, int stride, int offset)
        {
            MakeCurrent();
            WebGL2.VertexAttribIPointer(index, size, type, stride, offset);
        }

        /// <summary>Set divisor for instanced rendering.</summary>
        public void VertexAttribDivisor(int index, int divisor)
        {
            MakeCurrent();
            WebGL2.VertexAttribDivisor(index, divisor);
        }

        #endregion

        /// <summary>
        /// Get an integer vertex attribute value. Use for parameters that return integers.
        /// </summary>
        public int GetVertexAttribInt(int index, int pname)
        {
            MakeCurrent();
            return WebGL2.GetVertexAttribInt(index, pname);
        }

        /// <summary>
        /// Get a float vertex attribute value. Use for parameters that return floats.
        /// </summary>
        public float GetVertexAttribFloat(int index, int pname)
        {
            MakeCurrent();
            return WebGL2.GetVertexAttribFloat(index, pname);
        }

        /// <summary>
        /// Get a boolean vertex attribute value. Use for parameters that return booleans.
        /// </summary>
        public bool GetVertexAttribBool(int index, int pname)
        {
            MakeCurrent();
            return WebGL2.GetVertexAttribBool(index, pname);
        }

        #endregion

        #region Buffers

        /// <summary>Create a buffer object.</summary>
        public WebGLBuffer CreateBuffer()
        {
            MakeCurrent();
            return new WebGLBuffer(WebGL2.CreateBuffer());
        }

        /// <summary>Bind a buffer to a target.</summary>
        /// <param name="target">Buffer binding target (ARRAY_BUFFER, etc.).</param>
        /// <param name="buffer">Buffer to bind (null to unbind).</param>
        public void BindBuffer(int target, WebGLBuffer? buffer)
        {
            MakeCurrent();
            WebGL2.BindBuffer(target, buffer?.JsObject ?? null!);
        }

        /// <summary>Bind a buffer to a specific binding point.</summary>
        public void BindBufferBase(int target, int index, WebGLBuffer buffer)
        {
            MakeCurrent();
            WebGL2.BindBufferBase(target, index, buffer.JsObject);
        }

        /// <summary>Bind a buffer to a specific binding point with offset and size.</summary>
        public void BindBufferRange(int target, int index, WebGLBuffer buffer, int offset, int size)
        {
            MakeCurrent();
            WebGL2.BindBufferRange(target, index, buffer.JsObject, offset, size);
        }

        /// <summary>Create and initialize buffer storage.</summary>
        /// <param name="target">Buffer target (ARRAY_BUFFER, etc.).</param>
        /// <param name="data">Data to upload.</param>
        /// <param name="usage">Usage hint (STATIC_DRAW, etc.).</param>
        public unsafe void BufferData(int target, Span<float> data, int usage)
        {
            MakeCurrent();
            fixed (float* ptr = data)
            {
                WebGL2.BufferData(target, (IntPtr)ptr, data.Length * sizeof(float), usage);
            }
        }

        /// <inheritdoc cref="BufferData(int, Span{float}, int)"/>
        public unsafe void BufferData(int target, Span<ushort> data, int usage)
        {
            MakeCurrent();
            fixed (ushort* ptr = data)
            {
                WebGL2.BufferData(target, (IntPtr)ptr, data.Length * sizeof(ushort), usage);
            }
        }

        /// <inheritdoc cref="BufferData(int, Span{float}, int)"/>
        public unsafe void BufferData(int target, Span<uint> data, int usage)
        {
            MakeCurrent();
            fixed (uint* ptr = data)
            {
                WebGL2.BufferData(target, (IntPtr)ptr, data.Length * sizeof(uint), usage);
            }
        }

        /// <inheritdoc cref="BufferData(int, Span{float}, int)"/>
        public unsafe void BufferData(int target, Span<byte> data, int usage)
        {
            MakeCurrent();
            fixed (byte* ptr = data)
            {
                WebGL2.BufferData(target, (IntPtr)ptr, data.Length, usage);
            }
        }

        /// <summary>Update a subset of buffer data.</summary>
        /// <param name="target">Buffer target.</param>
        /// <param name="offset">Byte offset into buffer.</param>
        /// <param name="data">Data to upload.</param>
        public unsafe void BufferSubData(int target, int offset, Span<float> data)
        {
            MakeCurrent();
            fixed (float* ptr = data)
            {
                WebGL2.BufferSubData(target, offset, (IntPtr)ptr, data.Length * sizeof(float));
            }
        }



        /// <summary>Delete a buffer object.</summary>
        public void DeleteBuffer(WebGLBuffer buffer)
        {
            MakeCurrent();
            WebGL2.DeleteBuffer(buffer.JsObject);
        }

        /// <summary>Check if an object is a valid buffer.</summary>
        public bool IsBuffer(WebGLBuffer buffer)
        {
            MakeCurrent();
            return WebGL2.IsBuffer(buffer.JsObject);
        }

        /// <summary>
        /// Get an integer buffer parameter value.
        /// </summary>
        public int GetBufferParameterInt(int target, int pname)
        {
            MakeCurrent();
            return WebGL2.GetBufferParameterInt(target, pname);
        }

        /// <summary>Copy data between buffer objects.</summary>
        public void CopyBufferSubData(int readTarget, int writeTarget, int readOffset, int writeOffset, int size)
        {
            MakeCurrent();
            WebGL2.CopyBufferSubData(readTarget, writeTarget, readOffset, writeOffset, size);
        }

        #endregion

        #region Vertex Array Objects

        /// <summary>Create a VAO to store vertex attribute state.</summary>
        public WebGLVertexArray CreateVertexArray()
        {
            MakeCurrent();
            return new WebGLVertexArray(WebGL2.CreateVertexArray());
        }

        /// <summary>Bind a VAO.</summary>
        /// <param name="vertexArray">VAO to bind.</param>
        public void BindVertexArray(WebGLVertexArray? vertexArray)
        {
            MakeCurrent();
            WebGL2.BindVertexArray(vertexArray?.JsObject ?? null!);
        }

        /// <summary>Delete a VAO.</summary>
        /// <param name="vertexArray">VAO to delete.</param>
        public void DeleteVertexArray(WebGLVertexArray vertexArray)
        {
            MakeCurrent();
            WebGL2.DeleteVertexArray(vertexArray.JsObject);
        }

        /// <summary>Check if an object is a valid VAO.</summary>
        public bool IsVertexArray(WebGLVertexArray vertexArray)
        {
            MakeCurrent();
            return WebGL2.IsVertexArray(vertexArray.JsObject);
        }

        #endregion

        #region Uniforms

        /// <summary>Get uniform location by name.</summary>
        public WebGLUniformLocation? GetUniformLocation(WebGLProgram program, string name)
        {
            MakeCurrent();
            var loc = WebGL2.GetUniformLocation(program.JsObject, name);
            return loc != null ? new WebGLUniformLocation(loc) : null;
        }

        /// <summary>Get index of a named uniform block.</summary>
        public int GetUniformBlockIndex(WebGLProgram program, string uniformBlockName)
        {
            MakeCurrent();
            return WebGL2.GetUniformBlockIndex(program.JsObject, uniformBlockName);
        }

        /// <summary>Bind a uniform block to a binding point.</summary>
        public void UniformBlockBinding(WebGLProgram program, int uniformBlockIndex, int uniformBlockBinding)
        {
            MakeCurrent();
            WebGL2.UniformBlockBinding(program.JsObject, uniformBlockIndex, uniformBlockBinding);
        }

        /// <summary>Set float uniform.</summary>
        public void Uniform1f(WebGLUniformLocation location, float x)
        {
            MakeCurrent();
            if (location.JsObject != null)
                WebGL2.Uniform1f(location.JsObject, x);
        }

        /// <summary>Set vec2 uniform.</summary>
        public void Uniform2f(WebGLUniformLocation location, float x, float y)
        {
            MakeCurrent();
            if (location.JsObject != null)
                WebGL2.Uniform2f(location.JsObject, x, y);
        }

        /// <summary>Set vec3 uniform.</summary>
        public void Uniform3f(WebGLUniformLocation location, float x, float y, float z)
        {
            MakeCurrent();
            if (location.JsObject != null)
                WebGL2.Uniform3f(location.JsObject, x, y, z);
        }

        /// <summary>Set vec4 uniform.</summary>
        public void Uniform4f(WebGLUniformLocation location, float x, float y, float z, float w)
        {
            MakeCurrent();
            if (location.JsObject != null)
                WebGL2.Uniform4f(location.JsObject, x, y, z, w);
        }

        /// <summary>Set int uniform.</summary>
        public void Uniform1i(WebGLUniformLocation location, int x)
        {
            MakeCurrent();
            if (location.JsObject != null)
                WebGL2.Uniform1i(location.JsObject, x);
        }

        /// <summary>Set ivec2 uniform.</summary>
        public void Uniform2i(WebGLUniformLocation location, int x, int y)
        {
            MakeCurrent();
            if (location.JsObject != null)
                WebGL2.Uniform2i(location.JsObject, x, y);
        }

        /// <summary>Set ivec3 uniform.</summary>
        public void Uniform3i(WebGLUniformLocation location, int x, int y, int z)
        {
            MakeCurrent();
            if (location.JsObject != null)
                WebGL2.Uniform3i(location.JsObject, x, y, z);
        }

        /// <summary>Set ivec4 uniform.</summary>
        public void Uniform4i(WebGLUniformLocation location, int x, int y, int z, int w)
        {
            MakeCurrent();
            if (location.JsObject != null)
                WebGL2.Uniform4i(location.JsObject, x, y, z, w);
        }

        /// <summary>Set uint uniform.</summary>
        public void Uniform1ui(WebGLUniformLocation location, uint x)
        {
            MakeCurrent();
            if (location.JsObject != null) WebGL2.Uniform1ui(location.JsObject, (int)x);
        }

        /// <summary>Set uvec2 uniform.</summary>
        public void Uniform2ui(WebGLUniformLocation location, uint x, uint y)
        {
            MakeCurrent();
            if (location.JsObject != null) WebGL2.Uniform2ui(location.JsObject, (int)x, (int)y);
        }

        /// <summary>Set uvec3 uniform.</summary>
        public void Uniform3ui(WebGLUniformLocation location, uint x, uint y, uint z)
        {
            MakeCurrent();
            if (location.JsObject != null) WebGL2.Uniform3ui(location.JsObject, (int)x, (int)y, (int)z);
        }

        /// <summary>Set uvec4 uniform.</summary>
        public void Uniform4ui(WebGLUniformLocation location, uint x, uint y, uint z, uint w)
        {
            MakeCurrent();
            if (location.JsObject != null) WebGL2.Uniform4ui(location.JsObject, (int)x, (int)y, (int)z, (int)w);
        }

        /// <summary>Set mat2 uniform.</summary>
        public unsafe void UniformMatrix2fv(WebGLUniformLocation location, bool transpose, Span<float> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (float* ptr = data) { WebGL2.UniformMatrix2fv(location.JsObject, transpose, (IntPtr)ptr); }
            }
        }

        /// <summary>Set mat3 uniform.</summary>
        public unsafe void UniformMatrix3fv(WebGLUniformLocation location, bool transpose, Span<float> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (float* ptr = data) { WebGL2.UniformMatrix3fv(location.JsObject, transpose, (IntPtr)ptr); }
            }
        }

        /// <summary>Set mat4 uniform.</summary>
        public unsafe void UniformMatrix4fv(WebGLUniformLocation location, bool transpose, Span<float> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (float* ptr = data) { WebGL2.UniformMatrix4fv(location.JsObject, transpose, (IntPtr)ptr); }
            }
        }

        /// <summary>Get info about an active uniform variable.</summary>
        public ActiveInfo? GetActiveUniform(WebGLProgram program, int index)
        {
            MakeCurrent();
            var result = WebGL2.GetActiveUniformInfo(program.JsObject, index);
            if (result == null || result.Length < 3) return null;
            return new ActiveInfo((string)result[0], Convert.ToInt32(result[1]), Convert.ToInt32(result[2]));
        }

        /// <summary>Get an integer uniform value.</summary>
        public int GetUniformInt(WebGLProgram program, WebGLUniformLocation location)
        {
            MakeCurrent();
            return WebGL2.GetUniformInt(program.JsObject, location.JsObject);
        }

        /// <summary>Get a float uniform value.</summary>
        public float GetUniformFloat(WebGLProgram program, WebGLUniformLocation location)
        {
            MakeCurrent();
            return WebGL2.GetUniformFloat(program.JsObject, location.JsObject);
        }

        /// <summary>Get a boolean uniform value.</summary>
        public bool GetUniformBool(WebGLProgram program, WebGLUniformLocation location)
        {
            MakeCurrent();
            return WebGL2.GetUniformBool(program.JsObject, location.JsObject);
        }

        /// <summary>Get a uint uniform value.</summary>
        public uint GetUniformUint(WebGLProgram program, WebGLUniformLocation location)
        {
            MakeCurrent();
            return (uint)WebGL2.GetUniformUint(program.JsObject, location.JsObject);
        }

        /// <summary>Get a uniform array/vector value as floats.</summary>
        public float[]? GetUniformFloatArray(WebGLProgram program, WebGLUniformLocation location)
        {
            MakeCurrent();
            var result = WebGL2.GetUniformArray(program.JsObject, location.JsObject);
            if (result == null) return null;
            var floats = new float[result.Length];
            for (int i = 0; i < result.Length; i++) floats[i] = (float)result[i];
            return floats;
        }

        #endregion

        #region Drawing

        /// <summary>Render primitives from array data.</summary>
        /// <param name="mode">Primitive type (TRIANGLES, LINES, etc.).</param>
        /// <param name="first">Start index in arrays.</param>
        /// <param name="count">Number of indices to be rendered.</param>
        public void DrawArrays(int mode, int first, int count)
        {
            MakeCurrent();
            WebGL2.DrawArrays(mode, first, count);
        }

        /// <summary>Render primitives from array data.</summary>
        /// <param name="mode">Primitive type (TRIANGLES, LINES, etc.).</param>
        /// <param name="count">Number of elements to be rendered.</param>
        /// <param name="type">Type of values in Element Array Buffer.</param>
        /// <param name="offset">Offset in bytes in Element Array Buffer.</param>
        public void DrawElements(int mode, int count, int type, int offset)
        {
            MakeCurrent();
            WebGL2.DrawElements(mode, count, type, offset);
        }

        /// <summary>Draw multiple instances of a range of elements.</summary>
        public void DrawArraysInstanced(int mode, int first, int count, int instanceCount)
        {
            MakeCurrent();
            WebGL2.DrawArraysInstanced(mode, first, count, instanceCount);
        }

        /// <summary>Draw multiple instances of a set of elements.</summary>
        public void DrawElementsInstanced(int mode, int count, int type, int offset, int instanceCount)
        {
            MakeCurrent();
            WebGL2.DrawElementsInstanced(mode, count, type, offset, instanceCount);
        }

        /// <summary>Specifies a list of color buffers to be drawn into.</summary>
        public void DrawBuffers(int[] buffers)
        {
            MakeCurrent();
            WebGL2.DrawBuffers(buffers);
        }

        #endregion

        #region Textures

        /// <summary>Create a texture object.</summary>
        public WebGLTexture CreateTexture()
        {
            MakeCurrent();
            return new WebGLTexture(WebGL2.CreateTexture());
        }

        /// <summary>Bind a named texture to a texturing target.</summary>
        public void BindTexture(int target, WebGLTexture? texture)
        {
            MakeCurrent();
            WebGL2.BindTexture(target, texture?.JsObject ?? null!);
        }

        /// <summary>Select active texture unit.</summary>
        public void ActiveTexture(int texture)
        {
            MakeCurrent();
            WebGL2.ActiveTexture(texture);
        }

        /// <summary>Specify a two-dimensional texture image.</summary>
        public void TexImage2D(int target, int level, int internalformat, int width, int height, int border, int format, int type, object? pixels)
        {
            MakeCurrent();
            WebGL2.TexImage2D(target, level, internalformat, width, height, border, format, type, pixels);
        }

        /// <summary>Specify a two-dimensional texture image (Zero-Copy).</summary>
        public unsafe void TexImage2D(int target, int level, int internalformat, int width, int height, int border, int format, int type, Span<byte> pixels)
        {
            MakeCurrent();
            fixed (byte* ptr = pixels)
            {
                WebGL2.TexImage2D_Ptr(target, level, internalformat, width, height, border, format, type, (IntPtr)ptr, pixels.Length);
            }
        }

        /// <summary>Set texture parameters (int).</summary>
        public void TexParameteri(int target, int pname, int param)
        {
            MakeCurrent();
            WebGL2.TexParameteri(target, pname, param);
        }

        /// <summary>Set texture parameters (float).</summary>
        public void TexParameterf(int target, int pname, float param)
        {
            MakeCurrent();
            WebGL2.TexParameterf(target, pname, param);
        }

        /// <summary>Generate mipmaps for a specified texture object.</summary>
        public void GenerateMipmap(int target)
        {
            MakeCurrent();
            WebGL2.GenerateMipmap(target);
        }

        /// <summary>Delete a texture object.</summary>
        public void DeleteTexture(WebGLTexture texture)
        {
            MakeCurrent();
            WebGL2.DeleteTexture(texture.JsObject);
        }

        /// <summary>Check if an object is a valid texture.</summary>
        public bool IsTexture(WebGLTexture texture)
        {
            MakeCurrent();
            return WebGL2.IsTexture(texture.JsObject);
        }

        /// <summary>Update a sub-rectangle of an existing 2D texture.</summary>
        public void TexSubImage2D(int target, int level, int xoffset, int yoffset, int width, int height, int format, int type, object? pixels)
        {
            MakeCurrent();
            WebGL2.TexSubImage2D(target, level, xoffset, yoffset, width, height, format, type, pixels);
        }

        /// <summary>Update a sub-rectangle of an existing 2D texture (Zero-Copy).</summary>
        public unsafe void TexSubImage2D(int target, int level, int xoffset, int yoffset, int width, int height, int format, int type, Span<byte> pixels)
        {
            MakeCurrent();
            fixed (byte* ptr = pixels)
            {
                WebGL2.TexSubImage2D_Ptr(target, level, xoffset, yoffset, width, height, format, type, (IntPtr)ptr, pixels.Length);
            }
        }

        /// <summary>Specify a three-dimensional texture image.</summary>
        public void TexImage3D(int target, int level, int internalformat, int width, int height, int depth, int border, int format, int type, object? pixels)
        {
            MakeCurrent();
            WebGL2.TexImage3D(target, level, internalformat, width, height, depth, border, format, type, pixels);
        }

        /// <summary>Specify a two-dimensional texture image from an HTML element.</summary>
        public void TexImage2DFromImage(int target, int level, int internalformat, int format, int type, object source)
        {
            MakeCurrent();
            WebGL2.TexImage2DFromImage(target, level, internalformat, format, type, source);
        }

        #endregion

        #region Framebuffers

        /// <summary>Create a framebuffer object.</summary>
        public WebGLFramebuffer CreateFramebuffer()
        {
            MakeCurrent();
            return new WebGLFramebuffer(WebGL2.CreateFramebuffer());
        }

        /// <summary>Bind a framebuffer to a target.</summary>
        public void BindFramebuffer(int target, WebGLFramebuffer? framebuffer)
        {
            MakeCurrent();
            WebGL2.BindFramebuffer(target, framebuffer?.JsObject);
        }

        /// <summary>Attach a texture image to a framebuffer object.</summary>
        public void FramebufferTexture2D(int target, int attachment, int textarget, WebGLTexture texture, int level)
        {
            MakeCurrent();
            WebGL2.FramebufferTexture2D(target, attachment, textarget, texture.JsObject, level);
        }

        /// <summary>Attach a renderbuffer object to a framebuffer object.</summary>
        public void FramebufferRenderbuffer(int target, int attachment, int renderbuffertarget, WebGLRenderbuffer renderbuffer)
        {
            MakeCurrent();
            WebGL2.FramebufferRenderbuffer(target, attachment, renderbuffertarget, renderbuffer.JsObject);
        }

        /// <summary>Check the completeness status of a framebuffer.</summary>
        public int CheckFramebufferStatus(int target)
        {
            MakeCurrent();
            return WebGL2.CheckFramebufferStatus(target);
        }

        /// <summary>Delete a framebuffer object.</summary>
        public void DeleteFramebuffer(WebGLFramebuffer framebuffer)
        {
            MakeCurrent();
            WebGL2.DeleteFramebuffer(framebuffer.JsObject);
        }

        /// <summary>Check if an object is a valid framebuffer.</summary>
        public bool IsFramebuffer(WebGLFramebuffer framebuffer)
        {
            MakeCurrent();
            return WebGL2.IsFramebuffer(framebuffer.JsObject);
        }

        /// <summary>Attach a level of a texture layer to a framebuffer.</summary>
        public void FramebufferTextureLayer(int target, int attachment, WebGLTexture texture, int level, int layer)
        {
            MakeCurrent();
            WebGL2.FramebufferTextureLayer(target, attachment, texture.JsObject, level, layer);
        }

        /// <summary>Transfer a block of pixels between framebuffers.</summary>
        public void BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, int mask, int filter)
        {
            MakeCurrent();
            WebGL2.BlitFramebuffer(srcX0, srcY0, srcX1, srcY1, dstX0, dstY0, dstX1, dstY1, mask, filter);
        }

        /// <summary>Select a color buffer source for reading pixels.</summary>
        public void ReadBuffer(int src)
        {
            MakeCurrent();
            WebGL2.ReadBuffer(src);
        }

        /// <summary>Read a pixel color from the framebuffer.</summary>
        public int[] ReadPixelColor(int x, int y)
        {
            MakeCurrent();
            return WebGL2.ReadPixelColor(x, y);
        }

        #endregion

        #region Renderbuffers

        /// <summary>Create a renderbuffer object.</summary>
        public WebGLRenderbuffer CreateRenderbuffer()
        {
            MakeCurrent();
            return new WebGLRenderbuffer(WebGL2.CreateRenderbuffer());
        }

        /// <summary>Bind a renderbuffer to a target.</summary>
        public void BindRenderbuffer(int target, WebGLRenderbuffer? renderbuffer)
        {
            MakeCurrent();
            WebGL2.BindRenderbuffer(target, renderbuffer?.JsObject);
        }

        /// <summary>Create and initialize a renderbuffer object's data store.</summary>
        public void RenderbufferStorage(int target, int internalformat, int width, int height)
        {
            MakeCurrent();
            WebGL2.RenderbufferStorage(target, internalformat, width, height);
        }

        /// <summary>Create and initialize a multisample renderbuffer object's data store.</summary>
        public void RenderbufferStorageMultisample(int target, int samples, int internalformat, int width, int height)
        {
            MakeCurrent();
            WebGL2.RenderbufferStorageMultisample(target, samples, internalformat, width, height);
        }

        /// <summary>Delete a renderbuffer object.</summary>
        public void DeleteRenderbuffer(WebGLRenderbuffer renderbuffer)
        {
            MakeCurrent();
            WebGL2.DeleteRenderbuffer(renderbuffer.JsObject);
        }

        /// <summary>Check if an object is a valid renderbuffer.</summary>
        public bool IsRenderbuffer(WebGLRenderbuffer renderbuffer)
        {
            MakeCurrent();
            return WebGL2.IsRenderbuffer(renderbuffer.JsObject);
        }

        #endregion

        #region Samplers (WebGL 2.0)

        /// <summary>Create a sampler object.</summary>
        public WebGLSampler CreateSampler()
        {
            MakeCurrent();
            return new WebGLSampler(WebGL2.CreateSampler());
        }

        /// <summary>Bind a sampler to a texture unit.</summary>
        public void BindSampler(int unit, WebGLSampler? sampler)
        {
            MakeCurrent();
            WebGL2.BindSampler(unit, sampler?.JsObject);
        }

        /// <summary>Set sampler parameters (int).</summary>
        public void SamplerParameteri(WebGLSampler sampler, int pname, int param)
        {
            MakeCurrent();
            WebGL2.SamplerParameteri(sampler.JsObject, pname, param);
        }

        /// <summary>Set sampler parameters (float).</summary>
        public void SamplerParameterf(WebGLSampler sampler, int pname, float param)
        {
            MakeCurrent();
            WebGL2.SamplerParameterf(sampler.JsObject, pname, param);
        }

        /// <summary>Delete a sampler object.</summary>
        public void DeleteSampler(WebGLSampler sampler)
        {
            MakeCurrent();
            WebGL2.DeleteSampler(sampler.JsObject);
        }

        /// <summary>Check if an object is a valid sampler.</summary>
        public bool IsSampler(WebGLSampler sampler)
        {
            MakeCurrent();
            return WebGL2.IsSampler(sampler.JsObject);
        }

        #endregion

        #region Queries (WebGL 2.0)

        /// <summary>Create a query object.</summary>
        public WebGLQuery CreateQuery()
        {
            MakeCurrent();
            return new WebGLQuery(WebGL2.CreateQuery());
        }

        /// <summary>Begin an asynchronous query.</summary>
        public void BeginQuery(int target, WebGLQuery query)
        {
            MakeCurrent();
            WebGL2.BeginQuery(target, query.JsObject);
        }

        /// <summary>End an asynchronous query.</summary>
        public void EndQuery(int target)
        {
            MakeCurrent();
            WebGL2.EndQuery(target);
        }

        /// <summary>Delete a query object.</summary>
        public void DeleteQuery(WebGLQuery query)
        {
            MakeCurrent();
            WebGL2.DeleteQuery(query.JsObject);
        }

        /// <summary>Check if an object is a valid query.</summary>
        public bool IsQuery(WebGLQuery query)
        {
            MakeCurrent();
            return WebGL2.IsQuery(query.JsObject);
        }



        /// <summary>
        /// Get an integer query parameter value.
        /// </summary>
        public int GetQueryParameterInt(WebGLQuery query, int pname)
        {
            MakeCurrent();
            return WebGL2.GetQueryParameterInt(query.JsObject, pname);
        }

        /// <summary>
        /// Get a boolean query parameter value.
        /// </summary>
        public bool GetQueryParameterBool(WebGLQuery query, int pname)
        {
            MakeCurrent();
            return WebGL2.GetQueryParameterBool(query.JsObject, pname);
        }

        #endregion

        #region Sync Objects (WebGL 2.0)

        /// <summary>Create a sync object and insert it into the GL command stream.</summary>
        public WebGLSync FenceSync(int condition, int flags)
        {
            MakeCurrent();
            return new WebGLSync(WebGL2.FenceSync(condition, flags));
        }

        /// <summary>Block and wait for a sync object to become signaled.</summary>
        public int ClientWaitSync(WebGLSync sync, int flags, double timeout)
        {
            MakeCurrent();
            return WebGL2.ClientWaitSync(sync.JsObject, flags, timeout);
        }

        /// <summary>Delete a sync object.</summary>
        public void DeleteSync(WebGLSync sync)
        {
            MakeCurrent();
            WebGL2.DeleteSync(sync.JsObject);
        }

        /// <summary>Check if an object is a valid sync object.</summary>
        public bool IsSync(WebGLSync sync)
        {
            MakeCurrent();
            return WebGL2.IsSync(sync.JsObject);
        }



        /// <summary>
        /// Get an integer sync parameter value.
        /// </summary>
        public int GetSyncParameterInt(WebGLSync sync, int pname)
        {
            MakeCurrent();
            return WebGL2.GetSyncParameterInt(sync.JsObject, pname);
        }

        #endregion

        #region Transform Feedback (WebGL 2.0)

        /// <summary>Create a transform feedback object.</summary>
        public WebGLTransformFeedback CreateTransformFeedback()
        {
            MakeCurrent();
            return new WebGLTransformFeedback(WebGL2.CreateTransformFeedback());
        }

        /// <summary>Bind a transform feedback object.</summary>
        public void BindTransformFeedback(int target, WebGLTransformFeedback? transformFeedback)
        {
            MakeCurrent();
            WebGL2.BindTransformFeedback(target, transformFeedback?.JsObject);
        }

        /// <summary>Activate transform feedback mode.</summary>
        public void BeginTransformFeedback(int primitiveMode)
        {
            MakeCurrent();
            WebGL2.BeginTransformFeedback(primitiveMode);
        }

        /// <summary>End transform feedback mode.</summary>
        public void EndTransformFeedback()
        {
            MakeCurrent();
            WebGL2.EndTransformFeedback();
        }

        /// <summary>Pause transform feedback operations.</summary>
        public void PauseTransformFeedback()
        {
            MakeCurrent();
            WebGL2.PauseTransformFeedback();
        }

        /// <summary>Resume transform feedback operations.</summary>
        public void ResumeTransformFeedback()
        {
            MakeCurrent();
            WebGL2.ResumeTransformFeedback();
        }

        /// <summary>Specify values to record in transform feedback buffers.</summary>
        public void TransformFeedbackVaryings(WebGLProgram program, string[] varyings, int bufferMode)
        {
            MakeCurrent();
            WebGL2.TransformFeedbackVaryings(program.JsObject, varyings, bufferMode);
        }

        /// <summary>Delete a transform feedback object.</summary>
        public void DeleteTransformFeedback(WebGLTransformFeedback transformFeedback)
        {
            MakeCurrent();
            WebGL2.DeleteTransformFeedback(transformFeedback.JsObject);
        }

        /// <summary>Check if an object is a valid transform feedback object.</summary>
        public bool IsTransformFeedback(WebGLTransformFeedback transformFeedback)
        {
            MakeCurrent();
            return WebGL2.IsTransformFeedback(transformFeedback.JsObject);
        }

        #endregion

        #region Buffer Extensions (WebGL 2.0)

        /// <summary>Read data from a buffer object.</summary>
        public unsafe void GetBufferSubData(int target, int srcByteOffset, Span<byte> dstData, int dstOffset = 0, int length = 0)
        {
            MakeCurrent();
            fixed (byte* ptr = dstData)
            {
                WebGL2.GetBufferSubData(target, srcByteOffset, (IntPtr)ptr, dstData.Length, dstOffset, length == 0 ? dstData.Length : length);
            }
        }

        #endregion

        #region Framebuffer Extensions (WebGL 2.0)

        /// <summary>Invalidate the contents of a framebuffer.</summary>
        public void InvalidateFramebuffer(int target, int[] attachments)
        {
            MakeCurrent();
            WebGL2.InvalidateFramebuffer(target, attachments);
        }

        /// <summary>Invalidate the contents of a sub-region of a framebuffer.</summary>
        public void InvalidateSubFramebuffer(int target, int[] attachments, int x, int y, int width, int height)
        {
            MakeCurrent();
            WebGL2.InvalidateSubFramebuffer(target, attachments, x, y, width, height);
        }

        /// <summary>
        /// Get an integer framebuffer attachment parameter value.
        /// </summary>
        public int GetFramebufferAttachmentParameterInt(int target, int attachment, int pname)
        {
            MakeCurrent();
            return WebGL2.GetFramebufferAttachmentParameterInt(target, attachment, pname);
        }

        #endregion

        #region Renderbuffer Extensions

        /// <summary>
        /// Get an integer renderbuffer parameter value.
        /// </summary>
        public int GetRenderbufferParameterInt(int target, int pname)
        {
            MakeCurrent();
            return WebGL2.GetRenderbufferParameterInt(target, pname);
        }

        #endregion

        #region Texture Extensions (WebGL 2.0)

        /// <summary>Specify a two-dimensional texture storage.</summary>
        public void TexStorage2D(int target, int levels, int internalformat, int width, int height)
        {
            MakeCurrent();
            WebGL2.TexStorage2D(target, levels, internalformat, width, height);
        }

        /// <summary>Specify a three-dimensional texture storage.</summary>
        public void TexStorage3D(int target, int levels, int internalformat, int width, int height, int depth)
        {
            MakeCurrent();
            WebGL2.TexStorage3D(target, levels, internalformat, width, height, depth);
        }

        /// <summary>Update a sub-rectangle of an existing 3D texture.</summary>
        public void TexSubImage3D(int target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, int format, int type, object? pixels)
        {
            MakeCurrent();
            WebGL2.TexSubImage3D(target, level, xoffset, yoffset, zoffset, width, height, depth, format, type, pixels);
        }

        /// <summary>Copy a portion of the framebuffer to a 3D texture.</summary>
        public void CopyTexSubImage3D(int target, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height)
        {
            MakeCurrent();
            WebGL2.CopyTexSubImage3D(target, level, xoffset, yoffset, zoffset, x, y, width, height);
        }

        /// <summary>Specify a compressed two-dimensional texture image.</summary>
        public unsafe void CompressedTexImage2D(int target, int level, int internalformat, int width, int height, int border, Span<byte> data)
        {
            MakeCurrent();
            fixed (byte* ptr = data)
            {
                WebGL2.CompressedTexImage2D(target, level, internalformat, width, height, border, (IntPtr)ptr, data.Length);
            }
        }

        /// <summary>Update a sub-rectangle of a compressed 2D texture.</summary>
        public unsafe void CompressedTexSubImage2D(int target, int level, int xoffset, int yoffset, int width, int height, int format, Span<byte> data)
        {
            MakeCurrent();
            fixed (byte* ptr = data)
            {
                WebGL2.CompressedTexSubImage2D(target, level, xoffset, yoffset, width, height, format, (IntPtr)ptr, data.Length);
            }
        }

        /// <summary>Specify a compressed three-dimensional texture image.</summary>
        public unsafe void CompressedTexImage3D(int target, int level, int internalformat, int width, int height, int depth, int border, Span<byte> data)
        {
            MakeCurrent();
            fixed (byte* ptr = data)
            {
                WebGL2.CompressedTexImage3D(target, level, internalformat, width, height, depth, border, (IntPtr)ptr, data.Length);
            }
        }

        /// <summary>Update a sub-rectangle of a compressed 3D texture.</summary>
        public unsafe void CompressedTexSubImage3D(int target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, int format, Span<byte> data)
        {
            MakeCurrent();
            fixed (byte* ptr = data)
            {
                WebGL2.CompressedTexSubImage3D(target, level, xoffset, yoffset, zoffset, width, height, depth, format, (IntPtr)ptr, data.Length);
            }
        }

        /// <summary>Copy pixels from the current color buffer into a 2D texture.</summary>
        public void CopyTexImage2D(int target, int level, int internalformat, int x, int y, int width, int height, int border)
        {
            MakeCurrent();
            WebGL2.CopyTexImage2D(target, level, internalformat, x, y, width, height, border);
        }

        /// <summary>Copy a portion of the framebuffer to a 2D texture.</summary>
        public void CopyTexSubImage2D(int target, int level, int xoffset, int yoffset, int x, int y, int width, int height)
        {
            MakeCurrent();
            WebGL2.CopyTexSubImage2D(target, level, xoffset, yoffset, x, y, width, height);
        }

        /// <summary>
        /// Get an integer texture parameter value.
        /// </summary>
        public int GetTexParameterInt(int target, int pname)
        {
            MakeCurrent();
            return WebGL2.GetTexParameterInt(target, pname);
        }

        /// <summary>
        /// Get a float texture parameter value.
        /// </summary>
        public float GetTexParameterFloat(int target, int pname)
        {
            MakeCurrent();
            return WebGL2.GetTexParameterFloat(target, pname);
        }

        #endregion

        #region Program/Shader Extensions

        /// <summary>Get location of a fragment output variable.</summary>
        public int GetFragDataLocation(WebGLProgram program, string name)
        {
            MakeCurrent();
            return WebGL2.GetFragDataLocation(program.JsObject, name);
        }

        /// <summary>Get list of shaders attached to a program.</summary>
        public WebGLShader[]? GetAttachedShaders(WebGLProgram program)
        {
            MakeCurrent();
            var shaders = WebGL2.GetAttachedShaders(program.JsObject);
            if (shaders == null) return null;

            var result = new WebGLShader[shaders.Length];
            for (int i = 0; i < shaders.Length; i++)
            {
                result[i] = new WebGLShader(shaders[i]);
            }
            return result;
        }

        /// <summary>Precision format of the shader.</summary>
        public record ShaderPrecisionFormat(int RangeMin, int RangeMax, int Precision);

        /// <summary>Get precision info for a shader type and precision type.</summary>
        public ShaderPrecisionFormat? GetShaderPrecisionFormat(int shaderType, int precisionType)
        {
            MakeCurrent();
            var result = WebGL2.GetShaderPrecisionFormatAsArray(shaderType, precisionType);
            if (result == null || result.Length < 3) return null;
            return new ShaderPrecisionFormat(result[0], result[1], result[2]);
        }

        /// <summary>Active attribute info.</summary>
        public record ActiveInfo(string Name, int Size, int Type);

        /// <summary>Get info about an active attribute variable.</summary>
        public ActiveInfo? GetActiveAttrib(WebGLProgram program, int index)
        {
            MakeCurrent();
            var result = WebGL2.GetActiveAttribInfo(program.JsObject, index);
            if (result == null || result.Length < 3) return null;
            return new ActiveInfo((string)result[0], Convert.ToInt32(result[1]), Convert.ToInt32(result[2]));
        }


        /// <summary>Get the value at a specific index in a generic vertex attribute.</summary>
        public float[]? GetVertexAttrib(int index, int pname)
        {
            MakeCurrent();
            var result = WebGL2.GetVertexAttribArray(index, pname);
            if (result == null) return null;
            var floats = new float[result.Length];
            for (int i = 0; i < result.Length; i++) floats[i] = (float)result[i];
            return floats;
        }

        /// <summary>Get offset of a vertex attribute.</summary>
        public int GetVertexAttribOffset(int index, int pname)
        {
            MakeCurrent();
            return WebGL2.GetVertexAttribOffset(index, pname);
        }

        #endregion

        #region Uniform Extensions (WebGL 2.0)

        /// <summary>Set float array uniform.</summary>
        public unsafe void Uniform1fv(WebGLUniformLocation location, Span<float> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (float* ptr = data) { WebGL2.Uniform1fv(location.JsObject, (IntPtr)ptr, data.Length); }
            }
        }

        /// <summary>Set vec2 array uniform.</summary>
        public unsafe void Uniform2fv(WebGLUniformLocation location, Span<float> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (float* ptr = data) { WebGL2.Uniform2fv(location.JsObject, (IntPtr)ptr, data.Length / 2); }
            }
        }

        /// <summary>Set vec3 array uniform.</summary>
        public unsafe void Uniform3fv(WebGLUniformLocation location, Span<float> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (float* ptr = data) { WebGL2.Uniform3fv(location.JsObject, (IntPtr)ptr, data.Length / 3); }
            }
        }

        /// <summary>Set vec4 array uniform.</summary>
        public unsafe void Uniform4fv(WebGLUniformLocation location, Span<float> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (float* ptr = data) { WebGL2.Uniform4fv(location.JsObject, (IntPtr)ptr, data.Length / 4); }
            }
        }

        /// <summary>Set int array uniform.</summary>
        public unsafe void Uniform1iv(WebGLUniformLocation location, Span<int> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (int* ptr = data) { WebGL2.Uniform1iv(location.JsObject, (IntPtr)ptr, data.Length); }
            }
        }

        /// <summary>Set ivec2 array uniform.</summary>
        public unsafe void Uniform2iv(WebGLUniformLocation location, Span<int> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (int* ptr = data) { WebGL2.Uniform2iv(location.JsObject, (IntPtr)ptr, data.Length / 2); }
            }
        }

        /// <summary>Set ivec3 array uniform.</summary>
        public unsafe void Uniform3iv(WebGLUniformLocation location, Span<int> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (int* ptr = data) { WebGL2.Uniform3iv(location.JsObject, (IntPtr)ptr, data.Length / 3); }
            }
        }

        /// <summary>Set ivec4 array uniform.</summary>
        public unsafe void Uniform4iv(WebGLUniformLocation location, Span<int> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (int* ptr = data) { WebGL2.Uniform4iv(location.JsObject, (IntPtr)ptr, data.Length / 4); }
            }
        }

        /// <summary>Set uint array uniform.</summary>
        public unsafe void Uniform1uiv(WebGLUniformLocation location, Span<uint> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (uint* ptr = data) { WebGL2.Uniform1uiv(location.JsObject, (IntPtr)ptr, data.Length); }
            }
        }

        /// <summary>Set uvec2 array uniform.</summary>
        public unsafe void Uniform2uiv(WebGLUniformLocation location, Span<uint> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (uint* ptr = data) { WebGL2.Uniform2uiv(location.JsObject, (IntPtr)ptr, data.Length / 2); }
            }
        }

        /// <summary>Set uvec3 array uniform.</summary>
        public unsafe void Uniform3uiv(WebGLUniformLocation location, Span<uint> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (uint* ptr = data) { WebGL2.Uniform3uiv(location.JsObject, (IntPtr)ptr, data.Length / 3); }
            }
        }

        /// <summary>Set uvec4 array uniform.</summary>
        public unsafe void Uniform4uiv(WebGLUniformLocation location, Span<uint> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (uint* ptr = data) { WebGL2.Uniform4uiv(location.JsObject, (IntPtr)ptr, data.Length / 4); }
            }
        }

        /// <summary>Set 2x3 matrix array uniform.</summary>
        public unsafe void UniformMatrix2x3fv(WebGLUniformLocation location, bool transpose, Span<float> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (float* ptr = data) { WebGL2.UniformMatrix2x3fv(location.JsObject, transpose, (IntPtr)ptr); }
            }
        }

        /// <summary>Set 3x2 matrix array uniform.</summary>
        public unsafe void UniformMatrix3x2fv(WebGLUniformLocation location, bool transpose, Span<float> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (float* ptr = data) { WebGL2.UniformMatrix3x2fv(location.JsObject, transpose, (IntPtr)ptr); }
            }
        }

        /// <summary>Set 2x4 matrix array uniform.</summary>
        public unsafe void UniformMatrix2x4fv(WebGLUniformLocation location, bool transpose, Span<float> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (float* ptr = data) { WebGL2.UniformMatrix2x4fv(location.JsObject, transpose, (IntPtr)ptr); }
            }
        }

        /// <summary>Set 4x2 matrix array uniform.</summary>
        public unsafe void UniformMatrix4x2fv(WebGLUniformLocation location, bool transpose, Span<float> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (float* ptr = data) { WebGL2.UniformMatrix4x2fv(location.JsObject, transpose, (IntPtr)ptr); }
            }
        }

        /// <summary>Set 3x4 matrix array uniform.</summary>
        public unsafe void UniformMatrix3x4fv(WebGLUniformLocation location, bool transpose, Span<float> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (float* ptr = data) { WebGL2.UniformMatrix3x4fv(location.JsObject, transpose, (IntPtr)ptr); }
            }
        }

        /// <summary>Set 4x3 matrix array uniform.</summary>
        public unsafe void UniformMatrix4x3fv(WebGLUniformLocation location, bool transpose, Span<float> data)
        {
            MakeCurrent();
            if (location.JsObject != null)
            {
                fixed (float* ptr = data) { WebGL2.UniformMatrix4x3fv(location.JsObject, transpose, (IntPtr)ptr); }
            }
        }

        #endregion

        #region Vertex Attribute Extensions

        /// <summary>Specify a constant value for a generic vertex attribute (int).</summary>
        public void VertexAttribI4i(int index, int x, int y, int z, int w)
        {
            MakeCurrent();
            WebGL2.VertexAttribI4i(index, x, y, z, w);
        }

        /// <summary>Specify a constant value for a generic vertex attribute (uint).</summary>
        public void VertexAttribI4ui(int index, int x, int y, int z, int w)
        {
            MakeCurrent();
            WebGL2.VertexAttribI4ui(index, x, y, z, w);
        }

        /// <summary>Specify a constant value for a generic vertex attribute (float).</summary>
        public void VertexAttrib1f(int index, float x)
        {
            MakeCurrent();
            WebGL2.VertexAttrib1f(index, x);
        }

        /// <summary>Specify a constant value for a generic vertex attribute (vec2).</summary>
        public void VertexAttrib2f(int index, float x, float y)
        {
            MakeCurrent();
            WebGL2.VertexAttrib2f(index, x, y);
        }

        /// <summary>Specify a constant value for a generic vertex attribute (vec3).</summary>
        public void VertexAttrib3f(int index, float x, float y, float z)
        {
            MakeCurrent();
            WebGL2.VertexAttrib3f(index, x, y, z);
        }

        /// <summary>Specify a constant value for a generic vertex attribute (vec4).</summary>
        public void VertexAttrib4f(int index, float x, float y, float z, float w)
        {
            MakeCurrent();
            WebGL2.VertexAttrib4f(index, x, y, z, w);
        }

        #endregion

        #region Drawing Extensions (WebGL 2.0)

        /// <summary>Render primitives from a range of elements in an array.</summary>
        public void DrawRangeElements(int mode, int start, int end, int count, int type, int offset)
        {
            MakeCurrent();
            WebGL2.DrawRangeElements(mode, start, end, count, type, offset);
        }

        /// <summary>Clear a float buffer.</summary>
        public unsafe void ClearBufferfv(int buffer, int drawbuffer, Span<float> values)
        {
            MakeCurrent();
            fixed (float* ptr = values)
            {
                WebGL2.ClearBufferfv(buffer, drawbuffer, (IntPtr)ptr);
            }
        }

        /// <summary>Clear an int buffer.</summary>
        public unsafe void ClearBufferiv(int buffer, int drawbuffer, Span<int> values)
        {
            MakeCurrent();
            fixed (int* ptr = values)
            {
                WebGL2.ClearBufferiv(buffer, drawbuffer, (IntPtr)ptr);
            }
        }

        /// <summary>Clear a uint buffer.</summary>
        public unsafe void ClearBufferuiv(int buffer, int drawbuffer, Span<uint> values)
        {
            MakeCurrent();
            fixed (uint* ptr = values)
            {
                WebGL2.ClearBufferuiv(buffer, drawbuffer, (IntPtr)ptr);
            }
        }

        /// <summary>Clear the depth and stencil buffers.</summary>
        public void ClearBufferfi(int buffer, int drawbuffer, float depth, int stencil)
        {
            MakeCurrent();
            WebGL2.ClearBufferfi(buffer, drawbuffer, depth, stencil);
        }

        #endregion

        #region Query Extensions (WebGL 2.0)

        /// <summary>Get a query parameter value.</summary>
        public WebGLQuery? GetQuery(int target, int pname)
        {
            MakeCurrent();
            var result = WebGL2.GetQuery(target, pname);
            return result != null ? new WebGLQuery(result) : null;
        }

        #endregion

        #region Sampler Extensions (WebGL 2.0)

        /// <summary>
        /// Get an integer sampler parameter value.
        /// </summary>
        public int GetSamplerParameterInt(WebGLSampler sampler, int pname)
        {
            MakeCurrent();
            return WebGL2.GetSamplerParameterInt(sampler.JsObject, pname);
        }

        /// <summary>
        /// Get a float sampler parameter value.
        /// </summary>
        public float GetSamplerParameterFloat(WebGLSampler sampler, int pname)
        {
            MakeCurrent();
            return WebGL2.GetSamplerParameterFloat(sampler.JsObject, pname);
        }

        #endregion

        #region Sync Extensions (WebGL 2.0)

        /// <summary>Wait for a sync object to become signaled.</summary>
        public void WaitSync(WebGLSync sync, int flags, double timeout)
        {
            MakeCurrent();
            WebGL2.WaitSync(sync.JsObject, flags, timeout);
        }

        #endregion

        #region Uniform Buffer Object Extensions (WebGL 2.0)

        /// <summary>Get indices of uniform variables.</summary>
        public int[]? GetUniformIndices(WebGLProgram program, string[] uniformNames)
        {
            MakeCurrent();
            return WebGL2.GetUniformIndices(program.JsObject, uniformNames);
        }

        /// <summary>Get the name of an active uniform block.</summary>
        public string? GetActiveUniformBlockName(WebGLProgram program, int blockIndex)
        {
            MakeCurrent();
            return WebGL2.GetActiveUniformBlockName(program.JsObject, blockIndex);
        }

        #endregion

        #region Context Extensions

        /// <summary>Get a list of supported extensions.</summary>
        public string[]? GetSupportedExtensions()
        {
            MakeCurrent();
            return WebGL2.GetSupportedExtensions();
        }

        #endregion

        #region Pixel Operations

        /// <summary>Read pixels from the color buffer.</summary>
        public unsafe void ReadPixels(int x, int y, int width, int height, int format, int type, Span<byte> dstData)
        {
            MakeCurrent();
            fixed (byte* ptr = dstData)
            {
                WebGL2.ReadPixels(x, y, width, height, format, type, (IntPtr)ptr, dstData.Length);
            }
        }

        #endregion

        #region Color Space Properties (WebGL 2.0)

        /// <summary>
        /// Gets or sets the color space of the WebGL drawing buffer.
        /// Values: "srgb" (default) or "display-p3"
        /// </summary>
        public string DrawingBufferColorSpace
        {
            get { MakeCurrent(); return WebGL2.GetDrawingBufferColorSpace(); }
            set { MakeCurrent(); WebGL2.SetDrawingBufferColorSpace(value); }
        }

        /// <summary>
        /// Gets or sets the color space to convert to when importing textures.
        /// Values: "srgb" (default), "display-p3", or "none"
        /// </summary>
        public string UnpackColorSpace
        {
            get { MakeCurrent(); return WebGL2.GetUnpackColorSpace(); }
            set { MakeCurrent(); WebGL2.SetUnpackColorSpace(value); }
        }

        #endregion
    }

    #region WebGL Object Wrappers

    /// <summary>
    /// Base class for WebGL object wrappers.
    /// </summary>
    public class WebGLObjectWrapper(JSObject jsObject)
    {
        public JSObject JsObject { get; } = jsObject;
    }

    /// <summary>WebGL shader object wrapper.</summary>
    public class WebGLShader(JSObject obj) : WebGLObjectWrapper(obj) { }

    /// <summary>WebGL program object wrapper.</summary>
    public class WebGLProgram(JSObject obj) : WebGLObjectWrapper(obj) { }

    /// <summary>WebGL buffer object wrapper.</summary>
    public class WebGLBuffer(JSObject obj) : WebGLObjectWrapper(obj) { }

    /// <summary>WebGL uniform location wrapper.</summary>
    public class WebGLUniformLocation(JSObject obj) : WebGLObjectWrapper(obj) { }

    /// <summary>WebGL framebuffer object wrapper.</summary>
    public class WebGLFramebuffer(JSObject obj) : WebGLObjectWrapper(obj) { }

    /// <summary>WebGL texture object wrapper.</summary>
    public class WebGLTexture(JSObject obj) : WebGLObjectWrapper(obj) { }

    /// <summary>WebGL renderbuffer object wrapper.</summary>
    public class WebGLRenderbuffer(JSObject obj) : WebGLObjectWrapper(obj) { }

    /// <summary>WebGL 2.0 vertex array object wrapper.</summary>
    public class WebGLVertexArray(JSObject obj) : WebGLObjectWrapper(obj) { }

    /// <summary>WebGL 2.0 sampler object wrapper.</summary>
    public class WebGLSampler(JSObject obj) : WebGLObjectWrapper(obj) { }

    /// <summary>WebGL 2.0 query object wrapper.</summary>
    public class WebGLQuery(JSObject obj) : WebGLObjectWrapper(obj) { }

    /// <summary>WebGL 2.0 sync object wrapper.</summary>
    public class WebGLSync(JSObject obj) : WebGLObjectWrapper(obj) { }

    /// <summary>WebGL 2.0 transform feedback object wrapper.</summary>
    public class WebGLTransformFeedback(JSObject obj) : WebGLObjectWrapper(obj) { }

    #endregion

}