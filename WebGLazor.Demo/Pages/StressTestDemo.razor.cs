using Microsoft.AspNetCore.Components;

namespace WebGLazor.Demo.Pages;

public partial class StressTestDemo : IDisposable
{
    private readonly string _codeSnippet = @"
    @implements IDisposable

    <div class=""card"">
        <div class=""demo-controls-row"" style=""justify-content: space-between;"">
            <div class=""demo-controls-inline"">
                <label class=""demo-control-label"" style=""margin-bottom: 0; margin-right: 0.5rem;"">Object Count:</label>
                <input type=""range"" min=""1000"" max=""1000000"" step=""1000"" @bind=""_objectCount"" @bind:event=""oninput"" @bind:culture=""CultureInfo.InvariantCulture"" @onchange=""OnCountChanged"" style=""width: 200px;"" />
                <span class=""demo-slider-value"">@_objectCount.ToString(""N0"")</span>
            </div>
            
            <div class=""demo-fps-display"">
                FPS: @_fps.ToString(""F0"")
            </div>
            
            <div>
                <button class=""btn @(_isAnimating ? ""btn-secondary"" : ""btn-primary"")"" @onclick=""ToggleAnimation"">
                    @(_isAnimating ? ""Stop"" : ""Restart"")
                </button>
            </div>
        </div>
    
        <div class=""demo-canvas-wrapper demo-canvas-dark"" style=""height: 600px;"">
            <WebGLCanvas @ref=""_canvas""
                         Id=""stress-canvas""
                         Width=""1200""
                         Height=""600""
                         OnContextCreated=""OnContextCreated"" />
        </div>
    </div>

@code {
    private WebGLCanvas? _canvas;
    private WebGLContext? _gl;
    private WebGLProgram? _program;
    
    private int _objectCount = 50000;
    private int _currentRenderCount = 50000;
    private bool _isAnimating = true;
    
    private double _fps;
    private double _lastFrameTime;
    private int _frameCount;
    private double _timeAccumulator;
    
    private WebGLUniformLocation? _uTime;
    private WebGLUniformLocation? _uResolution;

    private void OnContextCreated(WebGLContext gl)
    {
        _gl = gl;

        const string vsSource = @""#version 300 es
        in vec2 a_position;
        
        uniform float u_time;
        uniform vec2 u_resolution;
        
        out vec3 v_color;
        
        // Pseudo-random function
        float random(float seed) {
            return fract(sin(seed) * 43758.5453123);
        }

        void main() {
            float id = float(gl_InstanceID);
            
            // Random start pos based on ID
            float rx = random(id * 0.123);
            float ry = random(id * 0.321);
            
            // Animate
            float x = fract(rx + u_time * (0.05 + 0.1 * random(id)));
            float y = fract(ry + u_time * (0.02 + 0.05 * random(id + 1.0)));
            
            // Map to -1..1
            vec2 pos = vec2(x, y) * 2.0 - 1.0;
            
            // Aspect ratio correction (keep squares square-ish)
            float aspect = u_resolution.x / u_resolution.y;
            pos.x *= aspect > 1.0 ? 1.0 : 1.0/aspect; // Simple spread
            
            // Size
            float size = 0.005; 
            
            gl_Position = vec4(pos + a_position * size, 0.0, 1.0);
            
            // Color based on ID
            v_color = vec3(
                0.5 + 0.5 * sin(id * 0.1 + u_time),
                0.5 + 0.5 * cos(id * 0.2 + u_time),
                0.5 + 0.5 * sin(id * 0.3)
            );
        }"";

        const string fsSource = @""#version 300 es
        precision mediump float;
        in vec3 v_color;
        out vec4 outColor;
        void main() {
            outColor = vec4(v_color, 1.0);
        }"";

        var vs = gl.CreateShader(WebGLConsts.VERTEX_SHADER);
        gl.ShaderSource(vs, vsSource);
        gl.CompileShader(vs);

        var fs = gl.CreateShader(WebGLConsts.FRAGMENT_SHADER);
        gl.ShaderSource(fs, fsSource);
        gl.CompileShader(fs);

        _program = gl.CreateProgram();
        gl.AttachShader(_program, vs);
        gl.AttachShader(_program, fs);
        gl.LinkProgram(_program);
        gl.UseProgram(_program);

        float[] quad = 
        [
            -0.5f, -0.5f,
             0.5f, -0.5f,
            -0.5f,  0.5f,
             0.5f, -0.5f,
             0.5f,  0.5f,
            -0.5f,  0.5f
        ];

        var vao = gl.CreateVertexArray();
        gl.BindVertexArray(vao);
        
        var buffer = gl.CreateBuffer();
        gl.BindBuffer(WebGLConsts.ARRAY_BUFFER, buffer);
        gl.BufferData(WebGLConsts.ARRAY_BUFFER, quad.AsSpan(), WebGLConsts.STATIC_DRAW);
        
        var posLoc = gl.GetAttribLocation(_program, ""a_position"");
        gl.EnableVertexAttribArray(posLoc);
        gl.VertexAttribPointer(posLoc, 2, WebGLConsts.FLOAT, false, 0, 0);
        
        _uTime = gl.GetUniformLocation(_program, ""u_time"");
        _uResolution = gl.GetUniformLocation(_program, ""u_resolution"");
        
        gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        
        gl.Viewport(0, 0, 1200, 600);
        if (_uResolution?.JsObject != null)
        {
            gl.Uniform2f(_uResolution, 1200, 600);
        }

        gl.StartLoop(OnFrame);
    }
    
    private void OnFrame(double timestamp)
    {
        if (_gl == null || _program == null) return;

        if (_lastFrameTime > 0)
        {
            double delta = timestamp - _lastFrameTime;
            _frameCount++;
            _timeAccumulator += delta;
            
            if (_timeAccumulator >= 500)
            {
                _fps = (_frameCount / _timeAccumulator) * 1000.0;
                _frameCount = 0;
                _timeAccumulator = 0;
                StateHasChanged();
            }
        }
        _lastFrameTime = timestamp;

        _gl.Clear(WebGLConsts.COLOR_BUFFER_BIT);
        
        if (_uTime?.JsObject != null)
        {
            _gl.Uniform1f(_uTime, (float)(timestamp / 1000.0));
        }

        _gl.DrawArraysInstanced(WebGLConsts.TRIANGLES, 0, 6, _currentRenderCount);
    }

    private void OnCountChanged(ChangeEventArgs e)
    {
        _currentRenderCount = _objectCount;
    }
    
    private void ToggleAnimation()
    {
        if (_gl == null) return;
        
        _isAnimating = !_isAnimating;
        if (_isAnimating)
        {
            _lastFrameTime = 0;
            _gl.StartLoop(OnFrame);
        }
        else
        {
            _gl.StopLoop();
        }
    }
    
    public void Dispose()
    {
        _gl?.StopLoop();
    }
}";

    private WebGLCanvas? _canvas;
    private WebGLContext? _gl;
    private WebGLProgram? _program;

    private int _objectCount = 50000;
    private int _currentRenderCount = 50000;
    private bool _isAnimating = true;

    private double _fps;
    private double _lastFrameTime;
    private int _frameCount;
    private double _timeAccumulator;

    private WebGLUniformLocation? _uTime;
    private WebGLUniformLocation? _uResolution;

    private void OnContextCreated(WebGLContext gl)
    {
        _gl = gl;

        const string vsSource = @"#version 300 es
        in vec2 a_position;
        
        uniform float u_time;
        uniform vec2 u_resolution;
        
        out vec3 v_color;
        
        // Pseudo-random function
        float random(float seed) {
            return fract(sin(seed) * 43758.5453123);
        }

        void main() {
            float id = float(gl_InstanceID);
            
            // Random start pos based on ID
            float rx = random(id * 0.123);
            float ry = random(id * 0.321);
            
            // Animate
            float x = fract(rx + u_time * (0.05 + 0.1 * random(id)));
            float y = fract(ry + u_time * (0.02 + 0.05 * random(id + 1.0)));
            
            // Map to -1..1
            vec2 pos = vec2(x, y) * 2.0 - 1.0;
            
            // Aspect ratio correction (keep squares square-ish)
            float aspect = u_resolution.x / u_resolution.y;
            pos.x *= aspect > 1.0 ? 1.0 : 1.0/aspect; // Simple spread
            
            // Size
            float size = 0.005; 
            
            gl_Position = vec4(pos + a_position * size, 0.0, 1.0);
            
            // Color based on ID
            v_color = vec3(
                0.5 + 0.5 * sin(id * 0.1 + u_time),
                0.5 + 0.5 * cos(id * 0.2 + u_time),
                0.5 + 0.5 * sin(id * 0.3)
            );
        }";

        const string fsSource = @"#version 300 es
        precision mediump float;
        in vec3 v_color;
        out vec4 outColor;
        void main() {
            outColor = vec4(v_color, 1.0);
        }";

        var vs = gl.CreateShader(WebGLConsts.VERTEX_SHADER);
        gl.ShaderSource(vs, vsSource);
        gl.CompileShader(vs);

        var fs = gl.CreateShader(WebGLConsts.FRAGMENT_SHADER);
        gl.ShaderSource(fs, fsSource);
        gl.CompileShader(fs);

        _program = gl.CreateProgram();
        gl.AttachShader(_program, vs);
        gl.AttachShader(_program, fs);
        gl.LinkProgram(_program);
        gl.UseProgram(_program);

        float[] quad =
        [
            -0.5f, -0.5f,
             0.5f, -0.5f,
            -0.5f,  0.5f,
             0.5f, -0.5f,
             0.5f,  0.5f,
            -0.5f,  0.5f
        ];

        var vao = gl.CreateVertexArray();
        gl.BindVertexArray(vao);

        var buffer = gl.CreateBuffer();
        gl.BindBuffer(WebGLConsts.ARRAY_BUFFER, buffer);
        gl.BufferData(WebGLConsts.ARRAY_BUFFER, quad.AsSpan(), WebGLConsts.STATIC_DRAW);

        var posLoc = gl.GetAttribLocation(_program, "a_position");
        gl.EnableVertexAttribArray(posLoc);
        gl.VertexAttribPointer(posLoc, 2, WebGLConsts.FLOAT, false, 0, 0);

        _uTime = gl.GetUniformLocation(_program, "u_time");
        _uResolution = gl.GetUniformLocation(_program, "u_resolution");

        gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

        gl.Viewport(0, 0, 1200, 600);
        if (_uResolution?.JsObject != null)
        {
            gl.Uniform2f(_uResolution, 1200, 600);
        }

        gl.StartLoop(OnFrame);
    }

    private void OnFrame(double timestamp)
    {
        if (_gl == null || _program == null) return;

        if (_lastFrameTime > 0)
        {
            double delta = timestamp - _lastFrameTime;
            _frameCount++;
            _timeAccumulator += delta;

            if (_timeAccumulator >= 500)
            {
                _fps = (_frameCount / _timeAccumulator) * 1000.0;
                _frameCount = 0;
                _timeAccumulator = 0;
                StateHasChanged();
            }
        }
        _lastFrameTime = timestamp;

        _gl.Clear(WebGLConsts.COLOR_BUFFER_BIT);

        if (_uTime?.JsObject != null)
        {
            _gl.Uniform1f(_uTime, (float)(timestamp / 1000.0));
        }

        _gl.DrawArraysInstanced(WebGLConsts.TRIANGLES, 0, 6, _currentRenderCount);
    }

    private void OnCountChanged(ChangeEventArgs e)
    {
        _currentRenderCount = _objectCount;
    }

    private void ToggleAnimation()
    {
        if (_gl == null) return;

        _isAnimating = !_isAnimating;
        if (_isAnimating)
        {
            _lastFrameTime = 0;
            _gl.StartLoop(OnFrame);
        }
        else
        {
            _gl.StopLoop();
        }
    }

    public void Dispose()
    {
        _gl?.StopLoop();
    }
}
