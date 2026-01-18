using Microsoft.AspNetCore.Components.Web;

namespace WebGLazor.Demo.Pages;

public partial class BarChartsDemo
{
    private readonly string _codeSnippet = @"
    <div class=""card"">
        <div class=""card-title"">Interactive Chart</div>
        <div class=""demo-controls-row"">
            <button class=""btn btn-primary"" @onclick=""RegenerateData"">Regenerate data</button>
            <span style=""font-weight: bold;"">@_selectedBarInfo</span>
        </div>

        <div class=""demo-canvas-wrapper"" style=""position: relative;"" 
             @onmousemove=""OnMouseMove"" 
             @onmouseleave=""OnMouseLeave"" 
             @onclick=""OnClick""
             @ontouchstart=""OnTouchStart""
             @ontouchmove=""OnTouchMove""
             @ontouchend=""OnTouchEnd"">
            <WebGLCanvas @ref=""_canvas""
                         Id=""bar-chart-canvas""
                         Width=""800""
                         Height=""400""
                         OnContextCreated=""OnContextCreated""
                         style=""cursor: crosshair;"" />
            
            @if (_tooltipVisible)
            {
                <div class=""chart-tooltip"" style=""left: @(_tooltipX.ToString(""F2"", CultureInfo.InvariantCulture))px; top: @(_tooltipY.ToString(""F2"", CultureInfo.InvariantCulture))px;"">
                    @_tooltipText
                </div>
            }
        </div>
        
        <p class=""demo-hint"">Hover over bars to see values. Click on a bar to select it.</p>
    </div>

@code {
    private WebGLCanvas? _canvas;
    private WebGLContext? _gl;
    private WebGLProgram? _program;
    
    private int _dataCount = 20;
    private float[] _data = [];
    private float[] _barColors = [];
    
    private bool _tooltipVisible;
    private double _tooltipX;
    private double _tooltipY;
    private string _tooltipText = """";
    private string _selectedBarInfo = ""Click a bar to select"";
    
    private double _canvasWidth = 800;
    private double _canvasHeight = 400;

    protected override void OnInitialized()
    {
        RegenerateData();
    }

    private void RegenerateData()
    {
        _data = new float[_dataCount];
        _barColors = new float[_dataCount * 3];
        var rnd = new Random();
        for (int i = 0; i < _dataCount; i++)
        {
            _data[i] = (float)rnd.NextDouble();
            
            _barColors[i * 3 + 0] = (float)rnd.NextDouble();
            _barColors[i * 3 + 1] = (float)rnd.NextDouble();
            _barColors[i * 3 + 2] = (float)rnd.NextDouble();
        }
        
        if (_gl != null)
        {
            Render();
        }
    }

    private void OnContextCreated(WebGLContext gl)
    {
        _gl = gl;
        
        const string vsSource = @""#version 300 es
        in vec2 a_position;
        in vec3 a_color;
        out vec3 v_color;
        void main() {
            gl_Position = vec4(a_position, 0.0, 1.0);
            v_color = a_color;
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

        Render();
    }

    private void Render(int highlightedIndex = -1)
    {
        if (_gl == null || _program == null) return;
        
        _gl.Viewport(0, 0, (int)_canvasWidth, (int)_canvasHeight);
        _gl.ClearColor(0.98f, 0.98f, 0.98f, 1.0f);
        _gl.Clear(WebGLConsts.COLOR_BUFFER_BIT);

        var vertexData = new float[_dataCount * 6 * 5];
        
        float barWidthUser = 2.0f / _dataCount;
        float padding = barWidthUser * 0.1f;
        float barW = barWidthUser - padding;
        
        for (int i = 0; i < _dataCount; i++)
        {
            float x1 = -1.0f + i * barWidthUser + padding / 2;
            float x2 = x1 + barW;
            float y1 = -0.8f;
            float y2 = -0.8f + (_data[i] * 1.6f);

            float r = _barColors[i * 3 + 0];
            float g = _barColors[i * 3 + 1];
            float b = _barColors[i * 3 + 2];
            
            if (highlightedIndex == i)
            {
                r = Math.Min(1.0f, r - 0.2f);
                g = Math.Min(1.0f, g - 0.2f);
                b = Math.Min(1.0f, b - 0.2f);
            }

            int offset = i * 6 * 5;
            
            AddVertex(vertexData, offset + 0, x1, y1, r, g, b);
            AddVertex(vertexData, offset + 5, x2, y1, r, g, b);
            AddVertex(vertexData, offset + 10, x1, y2, r, g, b);
            
            AddVertex(vertexData, offset + 15, x1, y2, r, g, b);
            AddVertex(vertexData, offset + 20, x2, y1, r, g, b);
            AddVertex(vertexData, offset + 25, x2, y2, r, g, b);
        }

        var vao = _gl.CreateVertexArray();
        _gl.BindVertexArray(vao);

        var vbo = _gl.CreateBuffer();
        _gl.BindBuffer(WebGLConsts.ARRAY_BUFFER, vbo);
        _gl.BufferData(WebGLConsts.ARRAY_BUFFER, vertexData.AsSpan(), WebGLConsts.DYNAMIC_DRAW);

        var posLoc = _gl.GetAttribLocation(_program, ""a_position"");
        var colorLoc = _gl.GetAttribLocation(_program, ""a_color"");

        int stride = 5 * sizeof(float);
        _gl.EnableVertexAttribArray(posLoc);
        _gl.VertexAttribPointer(posLoc, 2, WebGLConsts.FLOAT, false, stride, 0);

        _gl.EnableVertexAttribArray(colorLoc);
        _gl.VertexAttribPointer(colorLoc, 3, WebGLConsts.FLOAT, false, stride, 2 * sizeof(float));

        _gl.DrawArrays(WebGLConsts.TRIANGLES, 0, _dataCount * 6);
        
        _gl.DeleteBuffer(vbo);
        _gl.DeleteVertexArray(vao);
    }

    private void AddVertex(float[] arr, int offset, float x, float y, float r, float g, float b)
    {
        arr[offset + 0] = x;
        arr[offset + 1] = y;
        arr[offset + 2] = r;
        arr[offset + 3] = g;
        arr[offset + 4] = b;
    }


    public void OnMouseMove(MouseEventArgs e)
    {
        UpdateInteraction(e.ClientX, e.ClientY);
    }

    public void OnMouseLeave()
    {
        _tooltipVisible = false;
        Render(-1);
    }

    public void OnClick(MouseEventArgs e)
    {
        int index = GetBarIndexAt(e.ClientX, e.ClientY);
        if (index >= 0)
        {
            _selectedBarInfo = $""Selected: Bar {index + 1} (Value: {_data[index]:F2})"";
        }
        else
        {
            _selectedBarInfo = ""Click a bar to select"";
        }
    }

    public void OnTouchStart(TouchEventArgs e)
    {
        if (e.Touches.Length > 0)
        {
            UpdateInteraction(e.Touches[0].ClientX, e.Touches[0].ClientY);
        }
    }

    public void OnTouchMove(TouchEventArgs e)
    {
        if (e.Touches.Length > 0)
        {
            UpdateInteraction(e.Touches[0].ClientX, e.Touches[0].ClientY);
        }
    }

    public void OnTouchEnd(TouchEventArgs e)
    {
        _tooltipVisible = false;
        Render(-1);
    }

    private void UpdateInteraction(double clientX, double clientY)
    {
        int index = GetBarIndexAt(clientX, clientY);
        
        if (index >= 0)
        {
            var bounds = _gl?.GetCanvasBounds();
            if (bounds != null)
            {
                double localX = clientX - bounds.X;
                double localY = clientY - bounds.Y;
                
                _tooltipVisible = true;
                _tooltipX = localX;
                _tooltipY = localY;
                _tooltipText = $""Value: {_data[index]:F2}"";
                Render(index);
                StateHasChanged();
            }
        }
        else
        {
            if (_tooltipVisible)
            {
                _tooltipVisible = false;
                Render(-1);
                StateHasChanged();
            }
        }
    }

    private int GetBarIndexAt(double clientX, double clientY)
    {
        if (_gl == null) return -1;

        var bounds = _gl.GetCanvasBounds();
        if (bounds == null) return -1;

        double x = clientX - bounds.X;
        double y = clientY - bounds.Y;

        double width = bounds.Width;
        double height = bounds.Height;
        
        if (x < 0 || x > width || y < 0 || y > height) return -1;
            
        double slotWidth = width / _dataCount;
        int index = (int)(x / slotWidth);
        
        if (index < 0) index = 0;
        if (index >= _dataCount) index = _dataCount - 1;
        
        float ndcY = 1.0f - 2.0f * ((float)y / (float)height);
        
        float barBottom = -0.8f;
        float barTop = -0.8f + (_data[index] * 1.6f);
        
        if (ndcY >= barBottom && ndcY <= barTop)
        {
            return index;
        }
        
        return -1;
    }
}";

    private WebGLCanvas? _canvas;
    private WebGLContext? _gl;
    private WebGLProgram? _program;

    private int _dataCount = 20;
    private float[] _data = [];
    private float[] _barColors = [];

    private bool _tooltipVisible;
    private double _tooltipX;
    private double _tooltipY;
    private string _tooltipText = "";
    private string _selectedBarInfo = "Click a bar to select";

    private double _canvasWidth = 800;
    private double _canvasHeight = 400;

    protected override void OnInitialized()
    {
        RegenerateData();
    }

    private void RegenerateData()
    {
        _data = new float[_dataCount];
        _barColors = new float[_dataCount * 3];
        var rnd = new Random();
        for (int i = 0; i < _dataCount; i++)
        {
            _data[i] = (float)rnd.NextDouble();

            _barColors[i * 3 + 0] = (float)rnd.NextDouble();
            _barColors[i * 3 + 1] = (float)rnd.NextDouble();
            _barColors[i * 3 + 2] = (float)rnd.NextDouble();
        }

        if (_gl != null)
        {
            Render();
        }
    }

    private void OnContextCreated(WebGLContext gl)
    {
        _gl = gl;

        const string vsSource = @"#version 300 es
        in vec2 a_position;
        in vec3 a_color;
        out vec3 v_color;
        void main() {
            gl_Position = vec4(a_position, 0.0, 1.0);
            v_color = a_color;
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

        Render();
    }

    private void Render(int highlightedIndex = -1)
    {
        if (_gl == null || _program == null) return;

        _gl.Viewport(0, 0, (int)_canvasWidth, (int)_canvasHeight);
        _gl.ClearColor(0.98f, 0.98f, 0.98f, 1.0f);
        _gl.Clear(WebGLConsts.COLOR_BUFFER_BIT);

        var vertexData = new float[_dataCount * 6 * 5];

        float barWidthUser = 2.0f / _dataCount;
        float padding = barWidthUser * 0.1f;
        float barW = barWidthUser - padding;

        for (int i = 0; i < _dataCount; i++)
        {
            float x1 = -1.0f + i * barWidthUser + padding / 2;
            float x2 = x1 + barW;
            float y1 = -0.8f;
            float y2 = -0.8f + (_data[i] * 1.6f);

            float r = _barColors[i * 3 + 0];
            float g = _barColors[i * 3 + 1];
            float b = _barColors[i * 3 + 2];

            if (highlightedIndex == i)
            {
                r = Math.Min(1.0f, r - 0.2f);
                g = Math.Min(1.0f, g - 0.2f);
                b = Math.Min(1.0f, b - 0.2f);
            }

            int offset = i * 6 * 5;

            AddVertex(vertexData, offset + 0, x1, y1, r, g, b);
            AddVertex(vertexData, offset + 5, x2, y1, r, g, b);
            AddVertex(vertexData, offset + 10, x1, y2, r, g, b);

            AddVertex(vertexData, offset + 15, x1, y2, r, g, b);
            AddVertex(vertexData, offset + 20, x2, y1, r, g, b);
            AddVertex(vertexData, offset + 25, x2, y2, r, g, b);
        }

        var vao = _gl.CreateVertexArray();
        _gl.BindVertexArray(vao);

        var vbo = _gl.CreateBuffer();
        _gl.BindBuffer(WebGLConsts.ARRAY_BUFFER, vbo);
        _gl.BufferData(WebGLConsts.ARRAY_BUFFER, vertexData.AsSpan(), WebGLConsts.DYNAMIC_DRAW);

        var posLoc = _gl.GetAttribLocation(_program, "a_position");
        var colorLoc = _gl.GetAttribLocation(_program, "a_color");

        int stride = 5 * sizeof(float);
        _gl.EnableVertexAttribArray(posLoc);
        _gl.VertexAttribPointer(posLoc, 2, WebGLConsts.FLOAT, false, stride, 0);

        _gl.EnableVertexAttribArray(colorLoc);
        _gl.VertexAttribPointer(colorLoc, 3, WebGLConsts.FLOAT, false, stride, 2 * sizeof(float));

        _gl.DrawArrays(WebGLConsts.TRIANGLES, 0, _dataCount * 6);

        _gl.DeleteBuffer(vbo);
        _gl.DeleteVertexArray(vao);
    }

    private void AddVertex(float[] arr, int offset, float x, float y, float r, float g, float b)
    {
        arr[offset + 0] = x;
        arr[offset + 1] = y;
        arr[offset + 2] = r;
        arr[offset + 3] = g;
        arr[offset + 4] = b;
    }


    public void OnMouseMove(MouseEventArgs e)
    {
        UpdateInteraction(e.ClientX, e.ClientY);
    }

    public void OnMouseLeave()
    {
        _tooltipVisible = false;
        Render(-1);
    }

    public void OnClick(MouseEventArgs e)
    {
        int index = GetBarIndexAt(e.ClientX, e.ClientY);
        if (index >= 0)
        {
            _selectedBarInfo = $"Selected: Bar {index + 1} (Value: {_data[index]:F2})";
        }
        else
        {
            _selectedBarInfo = "Click a bar to select";
        }
    }

    public void OnTouchStart(TouchEventArgs e)
    {
        if (e.Touches.Length > 0)
        {
            UpdateInteraction(e.Touches[0].ClientX, e.Touches[0].ClientY);
        }
    }

    public void OnTouchMove(TouchEventArgs e)
    {
        if (e.Touches.Length > 0)
        {
            UpdateInteraction(e.Touches[0].ClientX, e.Touches[0].ClientY);
        }
    }

    public void OnTouchEnd(TouchEventArgs e)
    {
        _tooltipVisible = false;
        Render(-1);
    }

    private void UpdateInteraction(double clientX, double clientY)
    {
        int index = GetBarIndexAt(clientX, clientY);

        if (index >= 0)
        {
            var bounds = _gl?.GetCanvasBounds();
            if (bounds != null)
            {
                double localX = clientX - bounds.X;
                double localY = clientY - bounds.Y;

                _tooltipVisible = true;
                _tooltipX = localX;
                _tooltipY = localY;
                _tooltipText = $"Value: {_data[index]:F2}";
                Render(index);
                StateHasChanged();
            }
        }
        else
        {
            if (_tooltipVisible)
            {
                _tooltipVisible = false;
                Render(-1);
                StateHasChanged();
            }
        }
    }

    private int GetBarIndexAt(double clientX, double clientY)
    {
        if (_gl == null) return -1;

        var bounds = _gl.GetCanvasBounds();
        if (bounds == null) return -1;

        double x = clientX - bounds.X;
        double y = clientY - bounds.Y;

        double width = bounds.Width;
        double height = bounds.Height;

        if (x < 0 || x > width || y < 0 || y > height) return -1;

        double slotWidth = width / _dataCount;
        int index = (int)(x / slotWidth);

        if (index < 0) index = 0;
        if (index >= _dataCount) index = _dataCount - 1;

        float ndcY = 1.0f - 2.0f * ((float)y / (float)height);

        float barBottom = -0.8f;
        float barTop = -0.8f + (_data[index] * 1.6f);

        if (ndcY >= barBottom && ndcY <= barTop)
        {
            return index;
        }

        return -1;
    }
}
