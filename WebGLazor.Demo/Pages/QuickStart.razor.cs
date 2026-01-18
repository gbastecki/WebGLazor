namespace WebGLazor.Demo.Pages;

public partial class QuickStart
{
    private const string _basicCode = @"
<WebGLCanvas @ref=""_canvas"" 
             Id=""quickstart-canvas"" 
             Width=""400"" 
             Height=""300""
             OnContextCreated=""OnContextCreated"" />

@code {
    private WebGLCanvas? _canvas;

    private void OnContextCreated(WebGLContext gl)
    {
        gl.ClearColor(0.1f, 0.1f, 0.15f, 1.0f);
        gl.Clear(WebGLConsts.COLOR_BUFFER_BIT);
        
        const string vertexShader = @""#version 300 es
            in vec2 aPosition;
            in vec3 aColor;
            out vec3 vColor;
            void main() {
                gl_Position = vec4(aPosition, 0.0, 1.0);
                vColor = aColor;
            }"";
        
        const string fragmentShader = @""#version 300 es
            precision mediump float;
            in vec3 vColor;
            out vec4 fragColor;
            void main() {
                fragColor = vec4(vColor, 1.0);
            }"";
        
        var vs = gl.CreateShader(WebGLConsts.VERTEX_SHADER);
        gl.ShaderSource(vs, vertexShader);
        gl.CompileShader(vs);
        
        var fs = gl.CreateShader(WebGLConsts.FRAGMENT_SHADER);
        gl.ShaderSource(fs, fragmentShader);
        gl.CompileShader(fs);
        
        var program = gl.CreateProgram();
        gl.AttachShader(program, vs);
        gl.AttachShader(program, fs);
        gl.LinkProgram(program);
        gl.UseProgram(program);
        
        // Triangle vertices with colors
        float[] vertices = [
            // Position      // Color
             0.0f,  0.5f,    1.0f, 0.2f, 0.3f,
            -0.5f, -0.5f,    0.2f, 1.0f, 0.3f,
             0.5f, -0.5f,    0.2f, 0.3f, 1.0f
        ];
        
        var vao = gl.CreateVertexArray();
        gl.BindVertexArray(vao);
        
        var buffer = gl.CreateBuffer();
        gl.BindBuffer(WebGLConsts.ARRAY_BUFFER, buffer);
        gl.BufferData(WebGLConsts.ARRAY_BUFFER, vertices.AsSpan(), WebGLConsts.STATIC_DRAW);
        
        var posLoc = gl.GetAttribLocation(program, ""aPosition"");
        var colorLoc = gl.GetAttribLocation(program, ""aColor"");
        
        gl.EnableVertexAttribArray(posLoc);
        gl.VertexAttribPointer(posLoc, 2, WebGLConsts.FLOAT, false, 20, 0);
        
        gl.EnableVertexAttribArray(colorLoc);
        gl.VertexAttribPointer(colorLoc, 3, WebGLConsts.FLOAT, false, 20, 8);
        
        gl.DrawArrays(WebGLConsts.TRIANGLES, 0, 3);
    }
}";

    private WebGLCanvas? _canvas;

    private void OnContextCreated(WebGLContext gl)
    {
        gl.ClearColor(0.1f, 0.1f, 0.15f, 1.0f);
        gl.Clear(WebGLConsts.COLOR_BUFFER_BIT);

        const string vertexShader = @"#version 300 es
            in vec2 aPosition;
            in vec3 aColor;
            out vec3 vColor;
            void main() {
                gl_Position = vec4(aPosition, 0.0, 1.0);
                vColor = aColor;
            }";

        const string fragmentShader = @"#version 300 es
            precision mediump float;
            in vec3 vColor;
            out vec4 fragColor;
            void main() {
                fragColor = vec4(vColor, 1.0);
            }";

        var vs = gl.CreateShader(WebGLConsts.VERTEX_SHADER);
        gl.ShaderSource(vs, vertexShader);
        gl.CompileShader(vs);

        var fs = gl.CreateShader(WebGLConsts.FRAGMENT_SHADER);
        gl.ShaderSource(fs, fragmentShader);
        gl.CompileShader(fs);

        var program = gl.CreateProgram();
        gl.AttachShader(program, vs);
        gl.AttachShader(program, fs);
        gl.LinkProgram(program);
        gl.UseProgram(program);

        // Triangle vertices with colors
        float[] vertices = [
            // Position      // Color
             0.0f,  0.5f,    1.0f, 0.2f, 0.3f,
            -0.5f, -0.5f,    0.2f, 1.0f, 0.3f,
             0.5f, -0.5f,    0.2f, 0.3f, 1.0f
        ];

        var vao = gl.CreateVertexArray();
        gl.BindVertexArray(vao);

        var buffer = gl.CreateBuffer();
        gl.BindBuffer(WebGLConsts.ARRAY_BUFFER, buffer);
        gl.BufferData(WebGLConsts.ARRAY_BUFFER, vertices.AsSpan(), WebGLConsts.STATIC_DRAW);

        var posLoc = gl.GetAttribLocation(program, "aPosition");
        var colorLoc = gl.GetAttribLocation(program, "aColor");

        gl.EnableVertexAttribArray(posLoc);
        gl.VertexAttribPointer(posLoc, 2, WebGLConsts.FLOAT, false, 20, 0);

        gl.EnableVertexAttribArray(colorLoc);
        gl.VertexAttribPointer(colorLoc, 3, WebGLConsts.FLOAT, false, 20, 8);

        gl.DrawArrays(WebGLConsts.TRIANGLES, 0, 3);
    }
}
