using System.Numerics;

namespace WebGLazor.Demo.Pages;

public partial class RotatingCubeDemo : IDisposable
{
    private readonly string _codeSnippet = @"
    @implements IDisposable

    <div class=""demo-layout-row"">
        <div class=""demo-main-col"">
            <div class=""card"">
                <div class=""card-title"">3D View</div>
                <div class=""demo-canvas-wrapper"" style=""position: relative;"">
                    <WebGLCanvas @ref=""_canvas""
                                 Id=""cube-canvas""
                                 Width=""800""
                                 Height=""600""
                                 OnContextCreated=""OnContextCreated"" />
                    
                    <div class=""demo-fps-overlay"">
                        FPS: @_fps.ToString(""F0"")<br/>
                        Frame Time: @_frameTime.ToString(""F2"") ms
                    </div>
                </div>
            </div>
        </div>
        
        <div class=""demo-sidebar-col"">
             <div class=""card"">
                <div class=""card-title"">Controls</div>
                
                <div class=""demo-control-group"">
                    <label class=""demo-control-label"">Cube Color</label>
                    <input type=""color"" @bind=""_cubeColor"" @bind:event=""oninput"" class=""demo-control-input"" style=""height: 40px; cursor: pointer;"" />
                </div>
                
                <div class=""demo-control-group"">
                    <label class=""demo-control-label"">Rotation Speed</label>
                    <input type=""range"" min=""0"" max=""5.0"" step=""0.1"" @bind=""_rotationSpeed"" @bind:event=""oninput"" @bind:culture=""CultureInfo.InvariantCulture"" class=""demo-control-input"" />
                    <div class=""demo-control-value"">@_rotationSpeed.ToString(""F1"")x</div>
                </div>
                
                <div class=""demo-control-group"">
                    <label class=""demo-control-label"">Size</label>
                    <input type=""range"" min=""0.1"" max=""2.0"" step=""0.1"" @bind=""_cubeScale"" @bind:event=""oninput"" @bind:culture=""CultureInfo.InvariantCulture"" class=""demo-control-input"" />
                    <div class=""demo-control-value"">@_cubeScale.ToString(""F1"")x</div>
                </div>
                
                <div class=""demo-control-group"">
                    <label class=""demo-control-label"">Animation</label>
                    <button class=""btn @(_isAnimating ? ""btn-secondary"" : ""btn-primary"")"" @onclick=""ToggleAnimation"" style=""width: 100%;"">
                        @(_isAnimating ? ""Pause"" : ""Resume"")
                    </button>
                </div>
            </div>
        </div>
    </div>

@code {
    private WebGLCanvas? _canvas;
    private WebGLContext? _gl;
    private WebGLProgram? _program;
    
    private string _cubeColor = ""#f7b23b"";
    private float _rotationSpeed = 1.0f;
    private float _cubeScale = 1.0f;
    private bool _isAnimating = true;

    private double _fps;
    private double _frameTime;
    private double _lastFrameTime;
    private int _frameCount;
    private double _timeAccumulator;
    
    private float _angleX;
    private float _angleY;

    private WebGLUniformLocation? _uModelViewProjection;
    private WebGLUniformLocation? _uColor;
    
    private Matrix4x4 _projection;

    private void OnContextCreated(WebGLContext gl)
    {
        _gl = gl;
        
        const string vsSource = @""#version 300 es
        in vec3 a_position;
        in vec3 a_normal;
        
        uniform mat4 u_mvp;
        
        out vec3 v_normal;
        
        void main() {
            gl_Position = u_mvp * vec4(a_position, 1.0);
            v_normal = a_normal;
        }"";

        const string fsSource = @""#version 300 es
        precision mediump float;
        
        uniform vec3 u_color;
        in vec3 v_normal;
        out vec4 outColor;
        
        void main() {
            // simple directional light
            vec3 lightDir = normalize(vec3(0.5, 0.7, 1.0));
            float diff = max(dot(normalize(v_normal), lightDir), 0.2);
            
            vec3 finalColor = u_color * diff;
            outColor = vec4(finalColor, 1.0);
        }"";

        var vs = gl.CreateShader(WebGLConsts.VERTEX_SHADER);
        gl.ShaderSource(vs, vsSource);
        gl.CompileShader(vs);
        
        if (!gl.GetShaderParameterBool(vs, WebGLConsts.COMPILE_STATUS))
        {
            Console.WriteLine($""VS Error: {gl.GetShaderInfoLog(vs)}"");
        }

        var fs = gl.CreateShader(WebGLConsts.FRAGMENT_SHADER);
        gl.ShaderSource(fs, fsSource);
        gl.CompileShader(fs);
        
        if (!gl.GetShaderParameterBool(fs, WebGLConsts.COMPILE_STATUS))
        {
            Console.WriteLine($""FS Error: {gl.GetShaderInfoLog(fs)}"");
        }

        _program = gl.CreateProgram();
        gl.AttachShader(_program, vs);
        gl.AttachShader(_program, fs);
        gl.LinkProgram(_program);
        gl.UseProgram(_program);

        float[] cubeData = GetCubeData();
        
        var vao = gl.CreateVertexArray();
        gl.BindVertexArray(vao);
        
        var vbo = gl.CreateBuffer();
        gl.BindBuffer(WebGLConsts.ARRAY_BUFFER, vbo);
        gl.BufferData(WebGLConsts.ARRAY_BUFFER, cubeData.AsSpan(), WebGLConsts.STATIC_DRAW);
        
        int stride = 6 * sizeof(float);
        
        var posLoc = gl.GetAttribLocation(_program, ""a_position"");
        gl.EnableVertexAttribArray(posLoc);
        gl.VertexAttribPointer(posLoc, 3, WebGLConsts.FLOAT, false, stride, 0);
        
        var normLoc = gl.GetAttribLocation(_program, ""a_normal"");
        gl.EnableVertexAttribArray(normLoc);
        gl.VertexAttribPointer(normLoc, 3, WebGLConsts.FLOAT, false, stride, 3 * sizeof(float));
        
        _uModelViewProjection = gl.GetUniformLocation(_program, ""u_mvp"");
        _uColor = gl.GetUniformLocation(_program, ""u_color"");
        
        gl.Enable(WebGLConsts.DEPTH_TEST);
        gl.ClearColor(0.1f, 0.1f, 0.15f, 1.0f);
        
        _projection = Matrix4x4.CreatePerspectiveFieldOfView
        (
             (float)(Math.PI / 4),
             800f / 600f,
             0.1f,
             100.0f
        );

        gl.StartLoop(OnFrame);
    }

    private void OnFrame(double timestamp)
    {
        if (_gl == null || _program == null) return;
        
        if (_lastFrameTime > 0)
        {
            double delta = timestamp - _lastFrameTime;
            _frameTime = delta;
            
            _frameCount++;
            _timeAccumulator += delta;
            if (_timeAccumulator >= 500) // Update FPS every 500ms
            {
                _fps = (_frameCount / _timeAccumulator) * 1000.0;
                _frameCount = 0;
                _timeAccumulator = 0;
                StateHasChanged();
            }
            
            // Update animation if not paused
            if (_isAnimating)
            {
                float dtSeconds = (float)delta / 1000.0f;
                float speed = 1.0f * _rotationSpeed;
                _angleX += speed * dtSeconds;
                _angleY += speed * 0.7f * dtSeconds;
            }
        }
        _lastFrameTime = timestamp;

        _gl.Clear(WebGLConsts.COLOR_BUFFER_BIT | WebGLConsts.DEPTH_BUFFER_BIT);
        
        if (System.Drawing.ColorTranslator.FromHtml(_cubeColor) is var c)
        {
             _gl.Uniform3f(_uColor!, c.R / 255f, c.G / 255f, c.B / 255f);
        }
        
        var model = Matrix4x4.CreateScale(_cubeScale) * 
                    Matrix4x4.CreateRotationX(_angleX) * 
                    Matrix4x4.CreateRotationY(_angleY) * 
                    Matrix4x4.CreateTranslation(0, 0, -5.0f);
                    
        var mvp = model * _projection;
        
        // Send matrix
        float[] matrixData =
        [
            mvp.M11,
            mvp.M12,
            mvp.M13,
            mvp.M14,
            mvp.M21,
            mvp.M22,
            mvp.M23,
            mvp.M24,
            mvp.M31,
            mvp.M32,
            mvp.M33,
            mvp.M34,
            mvp.M41,
            mvp.M42,
            mvp.M43,
            mvp.M44,
        ];
        _gl.UniformMatrix4fv(_uModelViewProjection!, false, matrixData.AsSpan());
        
        _gl.DrawArrays(WebGLConsts.TRIANGLES, 0, 36);
    }
    
    private void ToggleAnimation()
    {
        _isAnimating = !_isAnimating;
    }
    
    public void Dispose()
    {
        _gl?.StopLoop();
    }

    private float[] GetCubeData()
    {
        // Format: x, y, z, nx, ny, nz
        // 6 faces, 2 triangles each
        
        return [
            // Front face
            -1.0f, -1.0f,  1.0f,  0.0f,  0.0f,  1.0f,
             1.0f, -1.0f,  1.0f,  0.0f,  0.0f,  1.0f,
             1.0f,  1.0f,  1.0f,  0.0f,  0.0f,  1.0f,
             1.0f,  1.0f,  1.0f,  0.0f,  0.0f,  1.0f,
            -1.0f,  1.0f,  1.0f,  0.0f,  0.0f,  1.0f,
            -1.0f, -1.0f,  1.0f,  0.0f,  0.0f,  1.0f,

            // Back face
            -1.0f, -1.0f, -1.0f,  0.0f,  0.0f, -1.0f,
            -1.0f,  1.0f, -1.0f,  0.0f,  0.0f, -1.0f,
             1.0f,  1.0f, -1.0f,  0.0f,  0.0f, -1.0f,
             1.0f,  1.0f, -1.0f,  0.0f,  0.0f, -1.0f,
             1.0f, -1.0f, -1.0f,  0.0f,  0.0f, -1.0f,
            -1.0f, -1.0f, -1.0f,  0.0f,  0.0f, -1.0f,

            // Top face
            -1.0f,  1.0f, -1.0f,  0.0f,  1.0f,  0.0f,
            -1.0f,  1.0f,  1.0f,  0.0f,  1.0f,  0.0f,
             1.0f,  1.0f,  1.0f,  0.0f,  1.0f,  0.0f,
             1.0f,  1.0f,  1.0f,  0.0f,  1.0f,  0.0f,
             1.0f,  1.0f, -1.0f,  0.0f,  1.0f,  0.0f,
            -1.0f,  1.0f, -1.0f,  0.0f,  1.0f,  0.0f,

            // Bottom face
            -1.0f, -1.0f, -1.0f,  0.0f, -1.0f,  0.0f,
             1.0f, -1.0f, -1.0f,  0.0f, -1.0f,  0.0f,
             1.0f, -1.0f,  1.0f,  0.0f, -1.0f,  0.0f,
             1.0f, -1.0f,  1.0f,  0.0f, -1.0f,  0.0f,
            -1.0f, -1.0f,  1.0f,  0.0f, -1.0f,  0.0f,
            -1.0f, -1.0f, -1.0f,  0.0f, -1.0f,  0.0f,

            // Right face
             1.0f, -1.0f, -1.0f,  1.0f,  0.0f,  0.0f,
             1.0f,  1.0f, -1.0f,  1.0f,  0.0f,  0.0f,
             1.0f,  1.0f,  1.0f,  1.0f,  0.0f,  0.0f,
             1.0f,  1.0f,  1.0f,  1.0f,  0.0f,  0.0f,
             1.0f, -1.0f,  1.0f,  1.0f,  0.0f,  0.0f,
             1.0f, -1.0f, -1.0f,  1.0f,  0.0f,  0.0f,

            // Left face
            -1.0f, -1.0f, -1.0f, -1.0f,  0.0f,  0.0f,
            -1.0f, -1.0f,  1.0f, -1.0f,  0.0f,  0.0f,
            -1.0f,  1.0f,  1.0f, -1.0f,  0.0f,  0.0f,
            -1.0f,  1.0f,  1.0f, -1.0f,  0.0f,  0.0f,
            -1.0f,  1.0f, -1.0f, -1.0f,  0.0f,  0.0f,
            -1.0f, -1.0f, -1.0f, -1.0f,  0.0f,  0.0f
        ];
    }
}";
    private WebGLCanvas? _canvas;
    private WebGLContext? _gl;
    private WebGLProgram? _program;

    private string _cubeColor = "#f7b23b";
    private float _rotationSpeed = 1.0f;
    private float _cubeScale = 1.0f;
    private bool _isAnimating = true;

    private double _fps;
    private double _frameTime;
    private double _lastFrameTime;
    private int _frameCount;
    private double _timeAccumulator;

    private float _angleX;
    private float _angleY;

    private WebGLUniformLocation? _uModelViewProjection;
    private WebGLUniformLocation? _uColor;

    private Matrix4x4 _projection;

    private void OnContextCreated(WebGLContext gl)
    {
        _gl = gl;

        const string vsSource = @"#version 300 es
        in vec3 a_position;
        in vec3 a_normal;
        
        uniform mat4 u_mvp;
        
        out vec3 v_normal;
        
        void main() {
            gl_Position = u_mvp * vec4(a_position, 1.0);
            v_normal = a_normal;
        }";

        const string fsSource = @"#version 300 es
        precision mediump float;
        
        uniform vec3 u_color;
        in vec3 v_normal;
        out vec4 outColor;
        
        void main() {
            // simple directional light
            vec3 lightDir = normalize(vec3(0.5, 0.7, 1.0));
            float diff = max(dot(normalize(v_normal), lightDir), 0.2);
            
            vec3 finalColor = u_color * diff;
            outColor = vec4(finalColor, 1.0);
        }";

        var vs = gl.CreateShader(WebGLConsts.VERTEX_SHADER);
        gl.ShaderSource(vs, vsSource);
        gl.CompileShader(vs);

        if (!gl.GetShaderParameterBool(vs, WebGLConsts.COMPILE_STATUS))
        {
            Console.WriteLine($"VS Error: {gl.GetShaderInfoLog(vs)}");
        }

        var fs = gl.CreateShader(WebGLConsts.FRAGMENT_SHADER);
        gl.ShaderSource(fs, fsSource);
        gl.CompileShader(fs);

        if (!gl.GetShaderParameterBool(fs, WebGLConsts.COMPILE_STATUS))
        {
            Console.WriteLine($"FS Error: {gl.GetShaderInfoLog(fs)}");
        }

        _program = gl.CreateProgram();
        gl.AttachShader(_program, vs);
        gl.AttachShader(_program, fs);
        gl.LinkProgram(_program);
        gl.UseProgram(_program);

        float[] cubeData = GetCubeData();

        var vao = gl.CreateVertexArray();
        gl.BindVertexArray(vao);

        var vbo = gl.CreateBuffer();
        gl.BindBuffer(WebGLConsts.ARRAY_BUFFER, vbo);
        gl.BufferData(WebGLConsts.ARRAY_BUFFER, cubeData.AsSpan(), WebGLConsts.STATIC_DRAW);

        int stride = 6 * sizeof(float);

        var posLoc = gl.GetAttribLocation(_program, "a_position");
        gl.EnableVertexAttribArray(posLoc);
        gl.VertexAttribPointer(posLoc, 3, WebGLConsts.FLOAT, false, stride, 0);

        var normLoc = gl.GetAttribLocation(_program, "a_normal");
        gl.EnableVertexAttribArray(normLoc);
        gl.VertexAttribPointer(normLoc, 3, WebGLConsts.FLOAT, false, stride, 3 * sizeof(float));

        _uModelViewProjection = gl.GetUniformLocation(_program, "u_mvp");
        _uColor = gl.GetUniformLocation(_program, "u_color");

        gl.Enable(WebGLConsts.DEPTH_TEST);
        gl.ClearColor(0.1f, 0.1f, 0.15f, 1.0f);

        _projection = Matrix4x4.CreatePerspectiveFieldOfView
        (
             (float)(Math.PI / 4),
             800f / 600f,
             0.1f,
             100.0f
        );

        gl.StartLoop(OnFrame);
    }

    private void OnFrame(double timestamp)
    {
        if (_gl == null || _program == null) return;

        if (_lastFrameTime > 0)
        {
            double delta = timestamp - _lastFrameTime;
            _frameTime = delta;

            _frameCount++;
            _timeAccumulator += delta;
            if (_timeAccumulator >= 500) // Update FPS every 500ms
            {
                _fps = (_frameCount / _timeAccumulator) * 1000.0;
                _frameCount = 0;
                _timeAccumulator = 0;
                StateHasChanged();
            }

            // Update animation if not paused
            if (_isAnimating)
            {
                float dtSeconds = (float)delta / 1000.0f;
                float speed = 1.0f * _rotationSpeed;
                _angleX += speed * dtSeconds;
                _angleY += speed * 0.7f * dtSeconds;
            }
        }
        _lastFrameTime = timestamp;

        _gl.Clear(WebGLConsts.COLOR_BUFFER_BIT | WebGLConsts.DEPTH_BUFFER_BIT);

        if (System.Drawing.ColorTranslator.FromHtml(_cubeColor) is var c)
        {
            _gl.Uniform3f(_uColor!, c.R / 255f, c.G / 255f, c.B / 255f);
        }

        var model = Matrix4x4.CreateScale(_cubeScale) *
                    Matrix4x4.CreateRotationX(_angleX) *
                    Matrix4x4.CreateRotationY(_angleY) *
                    Matrix4x4.CreateTranslation(0, 0, -5.0f);

        var mvp = model * _projection;

        // Send matrix
        float[] matrixData =
        [
            mvp.M11,
            mvp.M12,
            mvp.M13,
            mvp.M14,
            mvp.M21,
            mvp.M22,
            mvp.M23,
            mvp.M24,
            mvp.M31,
            mvp.M32,
            mvp.M33,
            mvp.M34,
            mvp.M41,
            mvp.M42,
            mvp.M43,
            mvp.M44,
        ];
        _gl.UniformMatrix4fv(_uModelViewProjection!, false, matrixData.AsSpan());

        _gl.DrawArrays(WebGLConsts.TRIANGLES, 0, 36);
    }

    private void ToggleAnimation()
    {
        _isAnimating = !_isAnimating;
    }

    public void Dispose()
    {
        _gl?.StopLoop();
    }

    private float[] GetCubeData()
    {
        // Format: x, y, z, nx, ny, nz
        // 6 faces, 2 triangles each

        return [
            // Front face
            -1.0f, -1.0f,  1.0f,  0.0f,  0.0f,  1.0f,
             1.0f, -1.0f,  1.0f,  0.0f,  0.0f,  1.0f,
             1.0f,  1.0f,  1.0f,  0.0f,  0.0f,  1.0f,
             1.0f,  1.0f,  1.0f,  0.0f,  0.0f,  1.0f,
            -1.0f,  1.0f,  1.0f,  0.0f,  0.0f,  1.0f,
            -1.0f, -1.0f,  1.0f,  0.0f,  0.0f,  1.0f,

            // Back face
            -1.0f, -1.0f, -1.0f,  0.0f,  0.0f, -1.0f,
            -1.0f,  1.0f, -1.0f,  0.0f,  0.0f, -1.0f,
             1.0f,  1.0f, -1.0f,  0.0f,  0.0f, -1.0f,
             1.0f,  1.0f, -1.0f,  0.0f,  0.0f, -1.0f,
             1.0f, -1.0f, -1.0f,  0.0f,  0.0f, -1.0f,
            -1.0f, -1.0f, -1.0f,  0.0f,  0.0f, -1.0f,

            // Top face
            -1.0f,  1.0f, -1.0f,  0.0f,  1.0f,  0.0f,
            -1.0f,  1.0f,  1.0f,  0.0f,  1.0f,  0.0f,
             1.0f,  1.0f,  1.0f,  0.0f,  1.0f,  0.0f,
             1.0f,  1.0f,  1.0f,  0.0f,  1.0f,  0.0f,
             1.0f,  1.0f, -1.0f,  0.0f,  1.0f,  0.0f,
            -1.0f,  1.0f, -1.0f,  0.0f,  1.0f,  0.0f,

            // Bottom face
            -1.0f, -1.0f, -1.0f,  0.0f, -1.0f,  0.0f,
             1.0f, -1.0f, -1.0f,  0.0f, -1.0f,  0.0f,
             1.0f, -1.0f,  1.0f,  0.0f, -1.0f,  0.0f,
             1.0f, -1.0f,  1.0f,  0.0f, -1.0f,  0.0f,
            -1.0f, -1.0f,  1.0f,  0.0f, -1.0f,  0.0f,
            -1.0f, -1.0f, -1.0f,  0.0f, -1.0f,  0.0f,

            // Right face
             1.0f, -1.0f, -1.0f,  1.0f,  0.0f,  0.0f,
             1.0f,  1.0f, -1.0f,  1.0f,  0.0f,  0.0f,
             1.0f,  1.0f,  1.0f,  1.0f,  0.0f,  0.0f,
             1.0f,  1.0f,  1.0f,  1.0f,  0.0f,  0.0f,
             1.0f, -1.0f,  1.0f,  1.0f,  0.0f,  0.0f,
             1.0f, -1.0f, -1.0f,  1.0f,  0.0f,  0.0f,

            // Left face
            -1.0f, -1.0f, -1.0f, -1.0f,  0.0f,  0.0f,
            -1.0f, -1.0f,  1.0f, -1.0f,  0.0f,  0.0f,
            -1.0f,  1.0f,  1.0f, -1.0f,  0.0f,  0.0f,
            -1.0f,  1.0f,  1.0f, -1.0f,  0.0f,  0.0f,
            -1.0f,  1.0f, -1.0f, -1.0f,  0.0f,  0.0f,
            -1.0f, -1.0f, -1.0f, -1.0f,  0.0f,  0.0f
        ];
    }
}
