namespace WebGLazor.Demo.Pages;

public partial class ApiReference
{
    private string _searchQuery = "";
    private bool _groupByCategory = true;
    private string _activeTab = "webgl";

    private string GetTabClass(string tab) => _activeTab == tab ? "active" : "";
    private void SetActiveTab(string tab) => _activeTab = tab;

    private record ConstValue(string Name, string Value, string Description);
    private record ApiParam(string Type, string Name, string Description, string? ConstGroup = null);
    private record ApiMethod(string Name, string Description, string ReturnType, string ReferenceUrl, string Category, List<ApiParam> Parameters);
    private record ApiCategory(string Name, List<ApiMethod> Methods);

    private bool MatchesSearch(ApiMethod method)
    {
        if (string.IsNullOrWhiteSpace(_searchQuery)) return true;
        return method.Name.Contains(_searchQuery, StringComparison.OrdinalIgnoreCase) ||
               method.Description.Contains(_searchQuery, StringComparison.OrdinalIgnoreCase);
    }

    private bool MatchesComponentSearch(CanvasParamGroup group)
    {
        if (string.IsNullOrWhiteSpace(_searchQuery)) return true;
        return group.Name.Contains(_searchQuery, StringComparison.OrdinalIgnoreCase) ||
               group.Parameters.Any(p => p.Name.Contains(_searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                         p.Description.Contains(_searchQuery, StringComparison.OrdinalIgnoreCase));
    }

    private IEnumerable<ApiCategory> GetFilteredCategories()
    {
        return _categories.Where(c => c.Methods.Any(m => MatchesSearch(m)));
    }

    private IEnumerable<ApiMethod> GetAllMethodsAlphabetically()
    {
        return _categories.SelectMany(c => c.Methods.Select(m => m with { Category = c.Name }))
                          .Where(MatchesSearch)
                          .OrderBy(m => m.Name);
    }

    private static string GlRef(string method) => $"https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/{method}";
    private static string Gl2Ref(string method) => $"https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/{method}";
    private static string MdnRef(string topic) => $"https://developer.mozilla.org/en-US/docs/Web/API/{topic}";

    // Canvas component parameter records
    private record CanvasParam(string Type, string Name, string DefaultValue, string Description);
    private record CanvasParamGroup(string Name, string Description, List<CanvasParam> Parameters);

    // WebGLCanvas component parameters grouped by category
    private static readonly List<CanvasParamGroup> _canvasParamGroups =
    [
        new("Core Parameters", "Essential properties for canvas configuration.", [
            new("string", "Id", "Guid.NewGuid()", "Unique identifier for the canvas element"),
            new("int", "Width", "800", "Canvas width in pixels"),
            new("int", "Height", "600", "Canvas height in pixels"),
            new("string", "Style", "\"\"", "Inline CSS styles for the canvas")
        ]),
        new("Context Callback", "Callback invoked when WebGL context is ready.", [
            new("EventCallback<WebGLContext>", "OnContextCreated", "NULL", "Fired after WebGL 2.0 context initialization completes")
        ]),
        new("Mouse Events", "Callbacks for mouse interactions on the canvas.", [
            new("EventCallback<MouseEventArgs>", "OnCanvasMouseMove", "NULL", "Mouse moved over canvas"),
            new("EventCallback<MouseEventArgs>", "OnCanvasMouseDown", "NULL", "Mouse button pressed"),
            new("EventCallback<MouseEventArgs>", "OnCanvasMouseUp", "NULL", "Mouse button released"),
            new("EventCallback<MouseEventArgs>", "OnCanvasClick", "NULL", "Mouse click"),
            new("EventCallback<MouseEventArgs>", "OnCanvasDoubleClick", "NULL", "Mouse double-click"),
            new("EventCallback<MouseEventArgs>", "OnCanvasMouseEnter", "NULL", "Mouse entered canvas area"),
            new("EventCallback<MouseEventArgs>", "OnCanvasMouseLeave", "NULL", "Mouse left canvas area"),
            new("EventCallback<MouseEventArgs>", "OnCanvasMouseOver", "NULL", "Mouse over canvas (bubbles)"),
            new("EventCallback<MouseEventArgs>", "OnCanvasMouseOut", "NULL", "Mouse out of canvas (bubbles)"),
            new("EventCallback<WheelEventArgs>", "OnCanvasWheel", "NULL", "Mouse wheel scrolled"),
            new("EventCallback<MouseEventArgs>", "OnCanvasContextMenu", "NULL", "Right-click context menu")
        ]),
        new("Keyboard Events", "Callbacks for keyboard input when canvas has focus.", [
            new("EventCallback<KeyboardEventArgs>", "OnCanvasKeyDown", "NULL", "Key pressed down"),
            new("EventCallback<KeyboardEventArgs>", "OnCanvasKeyUp", "NULL", "Key released"),
            new("EventCallback<KeyboardEventArgs>", "OnCanvasKeyPress", "NULL", "Key press (character input)")
        ]),
        new("Touch Events", "Callbacks for touch interactions on mobile devices.", [
            new("EventCallback<TouchEventArgs>", "OnCanvasTouchStart", "NULL", "Touch started"),
            new("EventCallback<TouchEventArgs>", "OnCanvasTouchEnd", "NULL", "Touch ended"),
            new("EventCallback<TouchEventArgs>", "OnCanvasTouchMove", "NULL", "Touch moved"),
            new("EventCallback<TouchEventArgs>", "OnCanvasTouchCancel", "NULL", "Touch cancelled")
        ]),
        new("Pointer Events", "Unified input callbacks for mouse, touch, and stylus.", [
            new("EventCallback<PointerEventArgs>", "OnCanvasPointerDown", "NULL", "Pointer pressed"),
            new("EventCallback<PointerEventArgs>", "OnCanvasPointerUp", "NULL", "Pointer released"),
            new("EventCallback<PointerEventArgs>", "OnCanvasPointerMove", "NULL", "Pointer moved"),
            new("EventCallback<PointerEventArgs>", "OnCanvasPointerEnter", "NULL", "Pointer entered"),
            new("EventCallback<PointerEventArgs>", "OnCanvasPointerLeave", "NULL", "Pointer left"),
            new("EventCallback<PointerEventArgs>", "OnCanvasPointerCancel", "NULL", "Pointer cancelled"),
            new("EventCallback<PointerEventArgs>", "OnCanvasPointerOut", "NULL", "Pointer out (bubbles)"),
            new("EventCallback<PointerEventArgs>", "OnCanvasPointerOver", "NULL", "Pointer over (bubbles)"),
            new("EventCallback<PointerEventArgs>", "OnCanvasGotPointerCapture", "NULL", "Pointer capture acquired"),
            new("EventCallback<PointerEventArgs>", "OnCanvasLostPointerCapture", "NULL", "Pointer capture lost")
        ]),
        new("Focus Events", "Callbacks for focus state changes.", [
            new("EventCallback<FocusEventArgs>", "OnCanvasFocus", "NULL", "Canvas received focus"),
            new("EventCallback<FocusEventArgs>", "OnCanvasBlur", "NULL", "Canvas lost focus"),
            new("EventCallback<FocusEventArgs>", "OnCanvasFocusIn", "NULL", "Focus entered (bubbles)"),
            new("EventCallback<FocusEventArgs>", "OnCanvasFocusOut", "NULL", "Focus left (bubbles)")
        ]),
        new("Drag & Drop Events", "Callbacks for HTML5 drag and drop operations.", [
            new("EventCallback<DragEventArgs>", "OnCanvasDrag", "NULL", "Element being dragged"),
            new("EventCallback<DragEventArgs>", "OnCanvasDragEnd", "NULL", "Drag operation ended"),
            new("EventCallback<DragEventArgs>", "OnCanvasDragEnter", "NULL", "Dragged element entered canvas"),
            new("EventCallback<DragEventArgs>", "OnCanvasDragLeave", "NULL", "Dragged element left canvas"),
            new("EventCallback<DragEventArgs>", "OnCanvasDragOver", "NULL", "Dragged element over canvas"),
            new("EventCallback<DragEventArgs>", "OnCanvasDragStart", "NULL", "Drag operation started"),
            new("EventCallback<DragEventArgs>", "OnCanvasDrop", "NULL", "Element dropped on canvas")
        ]),
        new("PreventDefault Modifiers", "Set to true to call preventDefault() on the corresponding event.", [
            new("bool", "PreventDefaultOnMouseMove", "false", "Prevent default mouse move behavior"),
            new("bool", "PreventDefaultOnMouseDown", "false", "Prevent default mouse down behavior"),
            new("bool", "PreventDefaultOnMouseUp", "false", "Prevent default mouse up behavior"),
            new("bool", "PreventDefaultOnClick", "false", "Prevent default click behavior"),
            new("bool", "PreventDefaultOnDoubleClick", "false", "Prevent default double-click behavior"),
            new("bool", "PreventDefaultOnWheel", "false", "Prevent page scrolling on wheel"),
            new("bool", "PreventDefaultOnContextMenu", "false", "Prevent right-click context menu"),
            new("bool", "PreventDefaultOnKeyDown", "false", "Prevent default key down behavior"),
            new("bool", "PreventDefaultOnKeyUp", "false", "Prevent default key up behavior"),
            new("bool", "PreventDefaultOnKeyPress", "false", "Prevent default key press behavior"),
            new("bool", "PreventDefaultOnTouchStart", "false", "Prevent default touch start"),
            new("bool", "PreventDefaultOnTouchEnd", "false", "Prevent default touch end"),
            new("bool", "PreventDefaultOnTouchMove", "false", "Prevent touch scrolling"),
            new("bool", "PreventDefaultOnPointerDown", "false", "Prevent default pointer down"),
            new("bool", "PreventDefaultOnPointerUp", "false", "Prevent default pointer up"),
            new("bool", "PreventDefaultOnPointerMove", "false", "Prevent default pointer move"),
            new("bool", "PreventDefaultOnDrag", "false", "Prevent default drag behavior"),
            new("bool", "PreventDefaultOnDragOver", "false", "Prevent default dragover behavior"),
            new("bool", "PreventDefaultOnDrop", "false", "Prevent default drop behavior")
        ]),
        new("StopPropagation Modifiers", "Set to true to call stopPropagation() on the corresponding event.", [
            new("bool", "StopPropagationOnMouseMove", "false", "Stop mouse move from bubbling"),
            new("bool", "StopPropagationOnMouseDown", "false", "Stop mouse down from bubbling"),
            new("bool", "StopPropagationOnMouseUp", "false", "Stop mouse up from bubbling"),
            new("bool", "StopPropagationOnClick", "false", "Stop click from bubbling"),
            new("bool", "StopPropagationOnDoubleClick", "false", "Stop double-click from bubbling"),
            new("bool", "StopPropagationOnWheel", "false", "Stop wheel from bubbling"),
            new("bool", "StopPropagationOnContextMenu", "false", "Stop context menu from bubbling"),
            new("bool", "StopPropagationOnKeyDown", "false", "Stop key down from bubbling"),
            new("bool", "StopPropagationOnKeyUp", "false", "Stop key up from bubbling"),
            new("bool", "StopPropagationOnKeyPress", "false", "Stop key press from bubbling"),
            new("bool", "StopPropagationOnTouchStart", "false", "Stop touch start from bubbling"),
            new("bool", "StopPropagationOnTouchEnd", "false", "Stop touch end from bubbling"),
            new("bool", "StopPropagationOnTouchMove", "false", "Stop touch move from bubbling"),
            new("bool", "StopPropagationOnPointerDown", "false", "Stop pointer down from bubbling"),
            new("bool", "StopPropagationOnPointerUp", "false", "Stop pointer up from bubbling"),
            new("bool", "StopPropagationOnPointerMove", "false", "Stop pointer move from bubbling"),
            new("bool", "StopPropagationOnDrag", "false", "Stop drag from bubbling"),
            new("bool", "StopPropagationOnDragOver", "false", "Stop dragover from bubbling"),
            new("bool", "StopPropagationOnDrop", "false", "Stop drop from bubbling")
        ])
    ];

    // Constant value groups with descriptions
    private static readonly Dictionary<string, List<ConstValue>> _constGroups = new()
    {
        ["ShaderType"] = [
            new("VERTEX_SHADER", "0x8B31", "Vertex shader stage"),
            new("FRAGMENT_SHADER", "0x8B30", "Fragment shader stage")
        ],
        ["BufferTarget"] = [
            new("ARRAY_BUFFER", "0x8892", "Vertex attribute data"),
            new("ELEMENT_ARRAY_BUFFER", "0x8893", "Index data"),
            new("UNIFORM_BUFFER", "0x8A11", "Uniform block data"),
            new("TRANSFORM_FEEDBACK_BUFFER", "0x8C8E", "Transform feedback output")
        ],
        ["BufferUsage"] = [
            new("STATIC_DRAW", "0x88E4", "Data set once, used many times"),
            new("DYNAMIC_DRAW", "0x88E8", "Data modified repeatedly, used many times"),
            new("STREAM_DRAW", "0x88E0", "Data set once, used few times")
        ],
        ["PrimitiveMode"] = [
            new("TRIANGLES", "0x0004", "Individual triangles (3 vertices each)"),
            new("TRIANGLE_STRIP", "0x0005", "Connected triangles sharing edges"),
            new("TRIANGLE_FAN", "0x0006", "Triangles sharing a central vertex"),
            new("LINES", "0x0001", "Individual line segments"),
            new("LINE_STRIP", "0x0003", "Connected line segments"),
            new("POINTS", "0x0000", "Individual points")
        ],
        ["DataType"] = [
            new("FLOAT", "0x1406", "32-bit floating point"),
            new("UNSIGNED_SHORT", "0x1403", "16-bit unsigned integer"),
            new("UNSIGNED_INT", "0x1405", "32-bit unsigned integer"),
            new("UNSIGNED_BYTE", "0x1401", "8-bit unsigned integer"),
            new("INT", "0x1404", "32-bit signed integer")
        ],
        ["Capability"] = [
            new("DEPTH_TEST", "0x0B71", "Depth buffer testing"),
            new("BLEND", "0x0BE2", "Alpha blending"),
            new("CULL_FACE", "0x0B44", "Face culling"),
            new("SCISSOR_TEST", "0x0C11", "Scissor clipping"),
            new("STENCIL_TEST", "0x0B90", "Stencil buffer testing")
        ],
        ["DepthFunc"] = [
            new("LESS", "0x0201", "Pass if depth < buffer"),
            new("LEQUAL", "0x0203", "Pass if depth <= buffer"),
            new("GREATER", "0x0204", "Pass if depth > buffer"),
            new("GEQUAL", "0x0206", "Pass if depth >= buffer"),
            new("EQUAL", "0x0202", "Pass if depth == buffer"),
            new("NOTEQUAL", "0x0205", "Pass if depth != buffer"),
            new("ALWAYS", "0x0207", "Always pass"),
            new("NEVER", "0x0200", "Never pass")
        ],
        ["BlendFactor"] = [
            new("SRC_ALPHA", "0x0302", "Source alpha"),
            new("ONE_MINUS_SRC_ALPHA", "0x0303", "1 - source alpha"),
            new("ONE", "1", "One (full weight)"),
            new("ZERO", "0", "Zero (no contribution)"),
            new("DST_ALPHA", "0x0304", "Destination alpha"),
            new("DST_COLOR", "0x0306", "Destination color")
        ],
        ["CullFaceMode"] = [
            new("BACK", "0x0405", "Cull back-facing polygons"),
            new("FRONT", "0x0404", "Cull front-facing polygons"),
            new("FRONT_AND_BACK", "0x0408", "Cull both faces")
        ],
        ["ClearMask"] = [
            new("COLOR_BUFFER_BIT", "0x4000", "Clear color buffer"),
            new("DEPTH_BUFFER_BIT", "0x0100", "Clear depth buffer"),
            new("STENCIL_BUFFER_BIT", "0x0400", "Clear stencil buffer")
        ],
        ["TextureTarget"] = [
            new("TEXTURE_2D", "0x0DE1", "2D texture"),
            new("TEXTURE_CUBE_MAP", "0x8513", "Cube map texture"),
            new("TEXTURE_3D", "0x806F", "3D texture"),
            new("TEXTURE_2D_ARRAY", "0x8C1A", "2D array texture")
        ],
        ["TextureParameter"] = [
            new("TEXTURE_MIN_FILTER", "0x2801", "Minification filter"),
            new("TEXTURE_MAG_FILTER", "0x2800", "Magnification filter"),
            new("TEXTURE_WRAP_S", "0x2802", "Horizontal wrap mode"),
            new("TEXTURE_WRAP_T", "0x2803", "Vertical wrap mode")
        ],
        ["TextureFilter"] = [
            new("LINEAR", "0x2601", "Bilinear interpolation"),
            new("NEAREST", "0x2600", "Nearest neighbor sampling"),
            new("LINEAR_MIPMAP_LINEAR", "0x2703", "Trilinear filtering"),
            new("NEAREST_MIPMAP_LINEAR", "0x2702", "Nearest with mipmap blending")
        ],
        ["FramebufferTarget"] = [
            new("FRAMEBUFFER", "0x8D40", "Read and draw operations"),
            new("READ_FRAMEBUFFER", "0x8CA8", "Read operations only"),
            new("DRAW_FRAMEBUFFER", "0x8CA9", "Draw operations only")
        ],
        ["FramebufferAttachment"] = [
            new("COLOR_ATTACHMENT0", "0x8CE0", "First color attachment"),
            new("DEPTH_ATTACHMENT", "0x8D00", "Depth buffer attachment"),
            new("STENCIL_ATTACHMENT", "0x8D20", "Stencil buffer attachment"),
            new("DEPTH_STENCIL_ATTACHMENT", "0x821A", "Combined depth+stencil")
        ],
        ["ShaderParameter"] = [
            new("COMPILE_STATUS", "0x8B81", "Compilation success flag")
        ],
        ["ProgramParameter"] = [
            new("LINK_STATUS", "0x8B82", "Link success flag")
        ],
        ["BlendEquation"] = [
            new("FUNC_ADD", "0x8006", "Add source and destination"),
            new("FUNC_SUBTRACT", "0x800A", "Subtract destination from source"),
            new("FUNC_REVERSE_SUBTRACT", "0x800B", "Subtract source from destination"),
            new("MIN", "0x8007", "Minimum of source and destination"),
            new("MAX", "0x8008", "Maximum of source and destination")
        ],
        ["FrontFaceMode"] = [
            new("CW", "0x0900", "Clockwise winding"),
            new("CCW", "0x0901", "Counter-clockwise winding (default)")
        ],
        ["StencilOp"] = [
            new("KEEP", "0x1E00", "Keep current stencil value"),
            new("ZERO", "0", "Set stencil to zero"),
            new("REPLACE", "0x1E01", "Set to reference value"),
            new("INCR", "0x1E02", "Increment (clamp to max)"),
            new("DECR", "0x1E03", "Decrement (clamp to 0)"),
            new("INVERT", "0x150A", "Bitwise invert"),
            new("INCR_WRAP", "0x8507", "Increment with wrap"),
            new("DECR_WRAP", "0x8508", "Decrement with wrap")
        ],
        ["TextureWrap"] = [
            new("REPEAT", "0x2901", "Tile the texture"),
            new("CLAMP_TO_EDGE", "0x812F", "Clamp to edge pixels"),
            new("MIRRORED_REPEAT", "0x8370", "Mirror and tile")
        ],
        ["PixelFormat"] = [
            new("RGBA", "0x1908", "Red, Green, Blue, Alpha"),
            new("RGB", "0x1907", "Red, Green, Blue"),
            new("ALPHA", "0x1906", "Alpha only"),
            new("DEPTH_COMPONENT", "0x1902", "Depth values"),
            new("DEPTH_STENCIL", "0x84F9", "Packed depth + stencil")
        ],
        ["InternalFormat"] = [
            new("RGBA8", "0x8058", "8-bit RGBA"),
            new("RGB8", "0x8051", "8-bit RGB"),
            new("RGBA16F", "0x881A", "16-bit float RGBA"),
            new("RGBA32F", "0x8814", "32-bit float RGBA"),
            new("DEPTH_COMPONENT24", "0x81A6", "24-bit depth"),
            new("DEPTH24_STENCIL8", "0x88F0", "24-bit depth + 8-bit stencil")
        ],
        ["QueryTarget"] = [
            new("ANY_SAMPLES_PASSED", "0x8C2F", "Occlusion query"),
            new("ANY_SAMPLES_PASSED_CONSERVATIVE", "0x8D6A", "Conservative occlusion query"),
            new("TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN", "0x8C88", "Primitives written")
        ],
        ["SyncCondition"] = [
            new("SYNC_GPU_COMMANDS_COMPLETE", "0x9117", "GPU commands completed"),
            new("ALREADY_SIGNALED", "0x911A", "Sync already signaled"),
            new("TIMEOUT_EXPIRED", "0x911B", "Wait timed out"),
            new("CONDITION_SATISFIED", "0x911C", "Condition satisfied"),
            new("WAIT_FAILED", "0x911D", "Wait failed")
        ],
        ["TransformFeedbackMode"] = [
            new("INTERLEAVED_ATTRIBS", "0x8C8C", "All varyings in one buffer"),
            new("SEPARATE_ATTRIBS", "0x8C8D", "Each varying in separate buffer")
        ],
        ["ErrorCode"] = [
            new("NO_ERROR", "0", "No error"),
            new("INVALID_ENUM", "0x0500", "Invalid enum value"),
            new("INVALID_VALUE", "0x0501", "Invalid value"),
            new("INVALID_OPERATION", "0x0502", "Invalid operation"),
            new("OUT_OF_MEMORY", "0x0505", "Out of memory"),
            new("INVALID_FRAMEBUFFER_OPERATION", "0x0506", "Invalid framebuffer operation")
        ]
    };

    private static readonly List<ApiCategory> _categories =
    [
        new("Context & Lifecycle", [
            new("InitializeAsync", "Initialize WebGL 2.0 context for a canvas element", "Task", MdnRef("HTMLCanvasElement/getContext"),
                "Context", [
                    new("string", "canvasId", "HTML canvas element ID"), 
                    new("string", "baseUri", "The base URI to load the WebGLazor JavaScript library from")
                    ]),
            new("MakeCurrent", "Set this context as active for subsequent operations", "", MdnRef("WebGLRenderingContext"),
                "Context", []),
            new("Dispose", "Release all WebGL resources and clean up", "", MdnRef("WEBGL_lose_context/loseContext"),
                "Context", []),
        ]),
        new("Animation Loop", [
            new("StartLoop", "Start animation using requestAnimationFrame", "", MdnRef("Window/requestAnimationFrame"),
                "Animation", [new("Action<double>", "onFrame", "Callback receiving timestamp in milliseconds")]),
            new("StopLoop", "Stop the animation loop", "", MdnRef("Window/cancelAnimationFrame"),
                "Animation", []),
        ]),
        new("Canvas Utilities", [
            new("GetCanvasBounds", "Get the bounding rectangle of the canvas for coordinate conversions", "CanvasBounds?", MdnRef("Element/getBoundingClientRect"),
                "Canvas", []),
        ]),
        new("State Management", [
            new("Enable", "Enable a WebGL capability", "", GlRef("enable"),
                "State", [new("int", "cap", "Capability to enable", "Capability")]),
            new("Disable", "Disable a WebGL capability", "", GlRef("disable"),
                "State", [new("int", "cap", "Capability to disable", "Capability")]),
            new("ClearColor", "Set the color used when clearing the color buffer", "", GlRef("clearColor"),
                "State", [
                    new("float", "r", "Red component (0.0-1.0)"),
                    new("float", "g", "Green component (0.0-1.0)"),
                    new("float", "b", "Blue component (0.0-1.0)"),
                    new("float", "a", "Alpha component (0.0-1.0)")
                ]),
            new("ClearDepth", "Set the depth value used when clearing depth buffer", "", GlRef("clearDepth"),
                "State", [new("float", "depth", "Depth value (0.0-1.0, default 1.0)")]),
            new("Clear", "Clear buffers to preset values", "", GlRef("clear"),
                "State", [new("int", "mask", "Bitwise OR of buffer bits", "ClearMask")]),
            new("Viewport", "Set the viewport rectangle", "", GlRef("viewport"),
                "State", [
                    new("int", "x", "Left edge in pixels"),
                    new("int", "y", "Bottom edge in pixels"),
                    new("int", "width", "Width in pixels"),
                    new("int", "height", "Height in pixels")
                ]),
            new("Scissor", "Define the scissor box for clipping", "", GlRef("scissor"),
                "State", [
                    new("int", "x", "Left edge in pixels"),
                    new("int", "y", "Bottom edge in pixels"),
                    new("int", "width", "Width in pixels"),
                    new("int", "height", "Height in pixels")
                ]),
            new("DepthFunc", "Set the depth comparison function", "", GlRef("depthFunc"),
                "State", [new("int", "func", "Comparison function", "DepthFunc")]),
            new("BlendFunc", "Set pixel blending factors", "", GlRef("blendFunc"),
                "State", [
                    new("int", "sfactor", "Source blend factor", "BlendFactor"),
                    new("int", "dfactor", "Destination blend factor", "BlendFactor")
                ]),
            new("CullFace", "Specify which faces to cull", "", GlRef("cullFace"),
                "State", [new("int", "mode", "Face mode", "CullFaceMode")]),
            new("IsEnabled", "Check if a capability is enabled", "bool", GlRef("isEnabled"),
                "State", [new("int", "cap", "Capability to check", "Capability")]),
            new("GetError", "Return the error code of the last operation", "int", GlRef("getError"),
                "State", []),
            new("Finish", "Block until all commands are complete", "", GlRef("finish"),
                "State", []),
            new("Flush", "Flush command buffer to GPU without blocking", "", GlRef("flush"),
                "State", []),
            new("GetParameterInt", "Get an integer parameter value", "int", GlRef("getParameter"),
                "State", [new("int", "pname", "Parameter name constant")]),
            new("GetParameterFloat", "Get a float parameter value", "float", GlRef("getParameter"),
                "State", [new("int", "pname", "Parameter name constant")]),
            new("GetParameterBool", "Get a boolean parameter value", "bool", GlRef("getParameter"),
                "State", [new("int", "pname", "Parameter name constant")]),
            new("GetParameterString", "Get a string parameter value", "string?", GlRef("getParameter"),
                "State", [new("int", "pname", "Parameter name constant")]),
            new("ColorMask", "Set which color components are written to framebuffer", "", GlRef("colorMask"),
                "State", [new("bool", "red", "Red"), new("bool", "green", "Green"), new("bool", "blue", "Blue"), new("bool", "alpha", "Alpha")]),
            new("DepthMask", "Set whether depth buffer is written to", "", GlRef("depthMask"),
                "State", [new("bool", "flag", "Whether depth buffer is writable")]),
            new("StencilMask", "Set the front and back stencil mask", "", GlRef("stencilMask"),
                "State", [new("int", "mask", "Bit mask to enable/disable writing")]),
            new("ClearStencil", "Set the stencil clear value", "", GlRef("clearStencil"),
                "State", [new("int", "s", "Stencil clear value")]),
            new("FrontFace", "Specify winding direction for front-facing polygons", "", GlRef("frontFace"),
                "State", [new("int", "mode", "CW or CCW")]),
            new("BlendColor", "Set the blend color", "", GlRef("blendColor"),
                "State", [new("float", "red", "Red"), new("float", "green", "Green"), new("float", "blue", "Blue"), new("float", "alpha", "Alpha")]),
            new("BlendEquation", "Set the blend equation", "", GlRef("blendEquation"),
                "State", [new("int", "mode", "FUNC_ADD, FUNC_SUBTRACT, etc.")]),
            new("BlendFuncSeparate", "Set blending factors separately for RGB and alpha", "", GlRef("blendFuncSeparate"),
                "State", [new("int", "srcRGB", "Source RGB factor"), new("int", "dstRGB", "Dest RGB factor"), new("int", "srcAlpha", "Source alpha factor"), new("int", "dstAlpha", "Dest alpha factor")]),
            new("DepthRange", "Set the depth range mapping", "", GlRef("depthRange"),
                "State", [new("float", "zNear", "Near clipping plane"), new("float", "zFar", "Far clipping plane")]),
            new("StencilFunc", "Set the stencil test function", "", GlRef("stencilFunc"),
                "State", [new("int", "func", "Comparison function"), new("int", "ref", "Reference value"), new("int", "mask", "Bit mask")]),
            new("StencilOp", "Set the stencil test actions", "", GlRef("stencilOp"),
                "State", [new("int", "fail", "Stencil fail"), new("int", "zfail", "Depth fail"), new("int", "zpass", "Both pass")]),
            new("StencilMaskSeparate", "Set the front or back stencil mask", "", GlRef("stencilMaskSeparate"),
                "State", [new("int", "face", "FRONT, BACK, or FRONT_AND_BACK"), new("int", "mask", "Bit mask")]),
            new("BlendEquationSeparate", "Set blend equation separately for RGB and alpha", "", GlRef("blendEquationSeparate"),
                "State", [new("int", "modeRGB", "Blend equation for RGB"), new("int", "modeAlpha", "Blend equation for alpha")]),
            new("StencilFuncSeparate", "Set stencil test function for front or back faces", "", GlRef("stencilFuncSeparate"),
                "State", [new("int", "face", "FRONT, BACK, or FRONT_AND_BACK"), new("int", "func", "Comparison function"), new("int", "ref", "Reference value"), new("int", "mask", "Bit mask")]),
            new("StencilOpSeparate", "Set stencil test actions for front or back faces", "", GlRef("stencilOpSeparate"),
                "State", [new("int", "face", "FRONT, BACK, or FRONT_AND_BACK"), new("int", "fail", "Stencil fail"), new("int", "zfail", "Depth fail"), new("int", "zpass", "Both pass")]),
            new("LineWidth", "Set the width of rasterized lines", "", GlRef("lineWidth"),
                "State", [new("float", "width", "Line width in pixels")]),
            new("PolygonOffset", "Set polygon offset", "", GlRef("polygonOffset"),
                "State", [new("float", "factor", "Depth offset factor"), new("float", "units", "Constant depth offset")]),
            new("PixelStorei", "Set pixel storage parameters", "", GlRef("pixelStorei"),
                "State", [new("int", "pname", "Parameter name"), new("int", "param", "Parameter value")]),
            new("Hint", "Set hints for implementation-specific behavior", "", GlRef("hint"),
                "State", [new("int", "target", "Hint target"), new("int", "mode", "DONT_CARE, FASTEST, NICEST")]),
            new("SampleCoverage", "Set multisample coverage parameters", "", GlRef("sampleCoverage"),
                "State", [new("float", "value", "Coverage value 0.0-1.0"), new("bool", "invert", "Invert coverage mask")]),
        ]),
        new("Shaders", [
            new("CreateShader", "Create a shader object", "WebGLShader", GlRef("createShader"),
                "Shaders", [new("int", "type", "Shader type", "ShaderType")]),
            new("ShaderSource", "Set the GLSL source code for a shader", "", GlRef("shaderSource"),
                "Shaders", [
                    new("WebGLShader", "shader", "Shader object to set source for"),
                    new("string", "source", "GLSL source code")
                ]),
            new("CompileShader", "Compile a shader object", "", GlRef("compileShader"),
                "Shaders", [new("WebGLShader", "shader", "Shader to compile")]),
            new("GetShaderParameterInt", "Query integer shader parameter", "int", GlRef("getShaderParameter"),
                "Shaders", [
                    new("WebGLShader", "shader", "Shader to query"),
                    new("int", "pname", "Parameter name", "ShaderParameter")
                ]),
            new("GetShaderParameterBool", "Query boolean shader parameter", "bool", GlRef("getShaderParameter"),
                "Shaders", [
                    new("WebGLShader", "shader", "Shader to query"),
                    new("int", "pname", "Parameter name", "ShaderParameter")
                ]),
            new("GetShaderInfoLog", "Get shader compilation log", "string", GlRef("getShaderInfoLog"),
                "Shaders", [new("WebGLShader", "shader", "Shader to get log for")]),
            new("DeleteShader", "Delete a shader object", "", GlRef("deleteShader"),
                "Shaders", [new("WebGLShader", "shader", "Shader to delete")]),
            new("GetShaderSource", "Get the GLSL source code of a shader", "string", GlRef("getShaderSource"),
                "Shaders", [new("WebGLShader", "shader", "Shader to get source for")]),
            new("IsShader", "Check if an object is a valid shader", "bool", GlRef("isShader"),
                "Shaders", [new("WebGLShader", "shader", "Object to check")]),
            new("GetShaderPrecisionFormat", "Get precision info for a shader type", "ShaderPrecisionFormat?", GlRef("getShaderPrecisionFormat"),
                 "Shaders", [new("int", "shaderType", "Shader type", "ShaderType"), new("int", "precisionType", "Precision type")]),
        ]),
        new("Programs", [
            new("CreateProgram", "Create a program object", "WebGLProgram", GlRef("createProgram"),
                "Programs", []),
            new("AttachShader", "Attach a shader to a program", "", GlRef("attachShader"),
                "Programs", [
                    new("WebGLProgram", "program", "Program to attach to"),
                    new("WebGLShader", "shader", "Compiled shader to attach")
                ]),
            new("DetachShader", "Detach a shader from a program", "", GlRef("detachShader"),
                "Programs", [
                    new("WebGLProgram", "program", "Program to detach from"),
                    new("WebGLShader", "shader", "Shader to detach")
                ]),
            new("LinkProgram", "Link a program object", "", GlRef("linkProgram"),
                "Programs", [new("WebGLProgram", "program", "Program to link")]),
            new("GetProgramParameterInt", "Query integer program parameter", "int", GlRef("getProgramParameter"),
                "Programs", [
                    new("WebGLProgram", "program", "Program to query"),
                    new("int", "pname", "Parameter name", "ProgramParameter")
                ]),
            new("GetProgramParameterBool", "Query program link status", "bool", GlRef("getProgramParameter"),
                "Programs", [
                    new("WebGLProgram", "program", "Program to query"),
                    new("int", "pname", "Parameter name", "ProgramParameter")
                ]),
            new("GetProgramInfoLog", "Get program linking log", "string", GlRef("getProgramInfoLog"),
                "Programs", [new("WebGLProgram", "program", "Program to get log for")]),
            new("UseProgram", "Set the active program for rendering", "", GlRef("useProgram"),
                "Programs", [new("WebGLProgram", "program", "Program to use")]),
            new("DeleteProgram", "Delete a program object", "", GlRef("deleteProgram"),
                "Programs", [new("WebGLProgram", "program", "Program to delete")]),
            new("ValidateProgram", "Validate a program object", "", GlRef("validateProgram"),
                "Programs", [new("WebGLProgram", "program", "Program to validate")]),
            new("IsProgram", "Check if an object is a valid program", "bool", GlRef("isProgram"),
                "Programs", [new("WebGLProgram", "program", "Object to check")]),
            new("GetFragDataLocation", "Get location of a fragment output variable", "int", Gl2Ref("getFragDataLocation"),
                "Programs", [new("WebGLProgram", "program", "Linked program"), new("string", "name", "Output variable name")]),
            new("GetAttachedShaders", "Get list of shaders attached to a program", "WebGLShader[]?", GlRef("getAttachedShaders"),
                "Programs", [new("WebGLProgram", "program", "Program to query")]),
        ]),
        new("Buffers", [
            new("CreateBuffer", "Create a buffer object", "WebGLBuffer", GlRef("createBuffer"),
                "Buffers", []),
            new("BindBuffer", "Bind a buffer to a target", "", GlRef("bindBuffer"),
                "Buffers", [
                    new("int", "target", "Buffer binding target", "BufferTarget"),
                    new("WebGLBuffer?", "buffer", "Buffer to bind (null to unbind)")
                ]),
            new("BufferData", "Create and initialize buffer storage", "", GlRef("bufferData"),
                "Buffers", [
                    new("int", "target", "Buffer target", "BufferTarget"),
                    new("Span<T>", "data", "Data to upload"),
                    new("int", "usage", "Usage hint", "BufferUsage")
                ]),
            new("BufferSubData", "Update a subset of buffer data", "", GlRef("bufferSubData"),
                "Buffers", [
                    new("int", "target", "Buffer target", "BufferTarget"),
                    new("int", "offset", "Byte offset into buffer"),
                    new("Span<T>", "data", "Data to upload")
                ]),
            new("DeleteBuffer", "Delete a buffer object", "", GlRef("deleteBuffer"),
                "Buffers", [new("WebGLBuffer", "buffer", "Buffer to delete")]),
            new("IsBuffer", "Check if an object is a valid buffer", "bool", GlRef("isBuffer"),
                "Buffers", [new("WebGLBuffer", "buffer", "Object to check")]),
            new("GetBufferParameterInt", "Get an integer buffer parameter", "int", GlRef("getBufferParameter"),
                "Buffers", [new("int", "target", "Buffer target", "BufferTarget"), new("int", "pname", "BUFFER_SIZE or BUFFER_USAGE")]),
            new("CopyBufferSubData", "Copy data between buffer objects", "", Gl2Ref("copyBufferSubData"),
                "Buffers", [new("int", "readTarget", "Source buffer"), new("int", "writeTarget", "Dest buffer"), new("int", "readOffset", "Source offset"), new("int", "writeOffset", "Dest offset"), new("int", "size", "Bytes to copy")]),
            new("GetBufferSubData", "Read data from a buffer object", "", Gl2Ref("getBufferSubData"),
                "Buffers", [
                    new("int", "target", "Buffer target"),
                    new("int", "srcByteOffset", "Source offset"),
                    new("Span<byte>", "dstData", "Destination buffer"),
                    new("int", "dstOffset", "Offset in dest (optional, default 0)"),
                    new("int", "length", "Bytes to read (optional, default 0=all)")
                ]),
        ]),
        new("Vertex Array Objects", [
            new("CreateVertexArray", "Create a VAO to store vertex attribute state", "WebGLVertexArray", Gl2Ref("createVertexArray"),
                "VAO", []),
            new("BindVertexArray", "Bind a VAO", "", Gl2Ref("bindVertexArray"),
                "VAO", [new("WebGLVertexArray?", "vao", "VAO to bind (null to unbind)")]),
            new("DeleteVertexArray", "Delete a VAO", "", Gl2Ref("deleteVertexArray"),
                "VAO", [new("WebGLVertexArray", "vao", "VAO to delete")]),
            new("IsVertexArray", "Check if an object is a valid VAO", "bool", Gl2Ref("isVertexArray"),
                "VAO", [new("WebGLVertexArray", "vao", "Object to check")]),
        ]),
        new("Vertex Attributes", [
            new("GetAttribLocation", "Get attribute location by name", "int", GlRef("getAttribLocation"),
                "Attributes", [
                    new("WebGLProgram", "program", "Linked program"),
                    new("string", "name", "Attribute name in shader")
                ]),
            new("EnableVertexAttribArray", "Enable a vertex attribute array", "", GlRef("enableVertexAttribArray"),
                "Attributes", [new("int", "index", "Attribute location")]),
            new("VertexAttribPointer", "Define vertex attribute layout", "", GlRef("vertexAttribPointer"),
                "Attributes", [
                    new("int", "index", "Attribute location"),
                    new("int", "size", "Components per vertex (1-4)"),
                    new("int", "type", "Data type", "DataType"),
                    new("bool", "normalized", "Normalize fixed-point data"),
                    new("int", "stride", "Byte stride between vertices"),
                    new("int", "offset", "Byte offset to first component")
                ]),
            new("VertexAttribDivisor", "Set attribute divisor for instancing", "", Gl2Ref("vertexAttribDivisor"),
                "Attributes", [
                    new("int", "index", "Attribute location"),
                    new("int", "divisor", "0=per-vertex, 1=per-instance")
                ]),
            new("DisableVertexAttribArray", "Disable a vertex attribute array", "", GlRef("disableVertexAttribArray"),
                "Attributes", [new("int", "index", "Attribute location")]),
            new("BindAttribLocation", "Bind an attribute to a specific location", "", GlRef("bindAttribLocation"),
                "Attributes", [
                    new("WebGLProgram", "program", "Program to modify"),
                    new("int", "index", "Target attribute index"),
                    new("string", "name", "Attribute name in shader")
                ]),
            new("VertexAttribIPointer", "Define integer vertex attribute layout", "", Gl2Ref("vertexAttribIPointer"),
                "Attributes", [
                    new("int", "index", "Attribute location"),
                    new("int", "size", "Components per vertex (1-4)"),
                    new("int", "type", "Data type (INT, UNSIGNED_INT)", "DataType"),
                    new("int", "stride", "Byte stride between vertices"),
                    new("int", "offset", "Byte offset to first component")
                ]),
            new("VertexAttribI4i", "Specify the value of a generic vertex attribute (int)", "", Gl2Ref("vertexAttribI4i"),
                "Attributes", [
                    new("int", "index", "Attribute location"),
                    new("int", "x", "X component"), new("int", "y", "Y component"), new("int", "z", "Z component"), new("int", "w", "W component")
                ]),
            new("VertexAttribI4ui", "Specify the value of a generic vertex attribute (uint)", "", Gl2Ref("vertexAttribI4ui"),
                "Attributes", [
                    new("int", "index", "Attribute location"),
                    new("int", "x", "X component"), new("int", "y", "Y component"), new("int", "z", "Z component"), new("int", "w", "W component")
                ]),
            new("VertexAttrib1f", "Specify the value of a generic vertex attribute (1f)", "", GlRef("vertexAttrib"),
                "Attributes", [new("int", "index", "Attribute location"), new("float", "x", "Value")]),
            new("VertexAttrib2f", "Specify the value of a generic vertex attribute (2f)", "", GlRef("vertexAttrib"),
                "Attributes", [new("int", "index", "Attribute location"), new("float", "x", "X"), new("float", "y", "Y")]),
            new("VertexAttrib3f", "Specify the value of a generic vertex attribute (3f)", "", GlRef("vertexAttrib"),
                "Attributes", [new("int", "index", "Attribute location"), new("float", "x", "X"), new("float", "y", "Y"), new("float", "z", "Z")]),
            new("VertexAttrib4f", "Specify the value of a generic vertex attribute (4f)", "", GlRef("vertexAttrib"),
                "Attributes", [new("int", "index", "Attribute location"), new("float", "x", "X"), new("float", "y", "Y"), new("float", "z", "Z"), new("float", "w", "W")]),

            new("GetVertexAttrib", "Get a vertex attribute parameter", "float[]?", GlRef("getVertexAttrib"),
                "Attributes", [new("int", "index", "Attribute location"), new("int", "pname", "Parameter name")]),
            new("GetVertexAttribInt", "Get an integer vertex attribute parameter", "int", GlRef("getVertexAttrib"),
                "Attributes", [new("int", "index", "Attribute index"), new("int", "pname", "Parameter name")]),
            new("GetVertexAttribFloat", "Get a float vertex attribute parameter", "float", GlRef("getVertexAttrib"),
                "Attributes", [new("int", "index", "Attribute index"), new("int", "pname", "Parameter name")]),
            new("GetVertexAttribBool", "Get a boolean vertex attribute parameter", "bool", GlRef("getVertexAttrib"),
                "Attributes", [new("int", "index", "Attribute index"), new("int", "pname", "Parameter name")]),
            new("GetVertexAttribOffset", "Get the address of the specified vertex attribute pointer", "int", GlRef("getVertexAttribOffset"),
                "Attributes", [new("int", "index", "Attribute index"), new("int", "pname", "Parameter name")]),
            new("GetActiveAttrib", "Get info about an active attribute variable", "ActiveInfo?", GlRef("getActiveAttrib"),
                "Attributes", [new("WebGLProgram", "program", "Linked program"), new("int", "index", "Attribute index")]),
        ]),
        new("Uniforms", [
            new("GetUniformLocation", "Get uniform location by name", "WebGLUniformLocation", GlRef("getUniformLocation"),
                "Uniforms", [
                    new("WebGLProgram", "program", "Linked program"),
                    new("string", "name", "Uniform name in shader")
                ]),
            new("GetUniformInt", "Get integer uniform value", "int", GlRef("getUniform"),
                "Uniforms", [new("WebGLProgram", "program", "Linked program"), new("WebGLUniformLocation", "location", "Uniform location")]),
            new("GetUniformFloat", "Get float uniform value", "float", GlRef("getUniform"),
                "Uniforms", [new("WebGLProgram", "program", "Linked program"), new("WebGLUniformLocation", "location", "Uniform location")]),
            new("GetUniformBool", "Get boolean uniform value", "bool", GlRef("getUniform"),
                "Uniforms", [new("WebGLProgram", "program", "Linked program"), new("WebGLUniformLocation", "location", "Uniform location")]),
            new("GetUniformUint", "Get uint uniform value", "uint", GlRef("getUniform"),
                "Uniforms", [new("WebGLProgram", "program", "Linked program"), new("WebGLUniformLocation", "location", "Uniform location")]),
            new("GetUniformFloatArray", "Get float array uniform value", "float[]?", GlRef("getUniform"),
                "Uniforms", [new("WebGLProgram", "program", "Linked program"), new("WebGLUniformLocation", "location", "Uniform location")]),
            new("Uniform1f", "Set float uniform", "", GlRef("uniform"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("float", "x", "Value")
                ]),
            new("Uniform2f", "Set vec2 uniform", "", GlRef("uniform"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("float", "x", "X component"), new("float", "y", "Y component")
                ]),
            new("Uniform3f", "Set vec3 uniform", "", GlRef("uniform"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("float", "x", "X"), new("float", "y", "Y"), new("float", "z", "Z")
                ]),
            new("Uniform4f", "Set vec4 uniform", "", GlRef("uniform"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("float", "x", "X"), new("float", "y", "Y"), new("float", "z", "Z"), new("float", "w", "W")
                ]),
            new("Uniform1i", "Set int or sampler uniform", "", GlRef("uniform"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("int", "x", "Value or texture unit")
                ]),
            new("UniformMatrix4fv", "Set mat4 uniform", "", GlRef("uniformMatrix"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("bool", "transpose", "Transpose the matrix"),
                    new("Span<float>", "data", "16 float values in column-major order")
                ]),
            new("Uniform2i", "Set ivec2 uniform", "", GlRef("uniform"),
                "Uniforms", [new("WebGLUniformLocation", "location", "Location"), new("int", "x", "X"), new("int", "y", "Y")]),
            new("Uniform3i", "Set ivec3 uniform", "", GlRef("uniform"),
                "Uniforms", [new("WebGLUniformLocation", "location", "Location"), new("int", "x", "X"), new("int", "y", "Y"), new("int", "z", "Z")]),
            new("Uniform4i", "Set ivec4 uniform", "", GlRef("uniform"),
                "Uniforms", [new("WebGLUniformLocation", "location", "Location"), new("int", "x", "X"), new("int", "y", "Y"), new("int", "z", "Z"), new("int", "w", "W")]),
            new("Uniform1ui", "Set uint uniform (WebGL 2.0)", "", Gl2Ref("uniform"),
                "Uniforms", [new("WebGLUniformLocation", "location", "Location"), new("int", "x", "Value")]),
            new("Uniform2ui", "Set uvec2 uniform (WebGL 2.0)", "", Gl2Ref("uniform"),
                "Uniforms", [new("WebGLUniformLocation", "location", "Location"), new("int", "x", "X"), new("int", "y", "Y")]),
            new("Uniform3ui", "Set uvec3 uniform (WebGL 2.0)", "", Gl2Ref("uniform"),
                "Uniforms", [new("WebGLUniformLocation", "location", "Location"), new("int", "x", "X"), new("int", "y", "Y"), new("int", "z", "Z")]),
            new("Uniform4ui", "Set uvec4 uniform (WebGL 2.0)", "", Gl2Ref("uniform"),
                "Uniforms", [new("WebGLUniformLocation", "location", "Location"), new("int", "x", "X"), new("int", "y", "Y"), new("int", "z", "Z"), new("int", "w", "W")]),
            new("UniformMatrix2fv", "Set mat2 uniform", "", GlRef("uniformMatrix"),
                "Uniforms", [new("WebGLUniformLocation", "location", "Location"), new("bool", "transpose", "Transpose"), new("Span<float>", "data", "4 floats")]),
            new("UniformMatrix3fv", "Set mat3 uniform", "", GlRef("uniformMatrix"),
                "Uniforms", [new("WebGLUniformLocation", "location", "Location"), new("bool", "transpose", "Transpose"), new("Span<float>", "data", "9 floats")]),
            new("GetActiveUniform", "Get info about an active uniform variable", "ActiveInfo?", GlRef("getActiveUniform"),
                "Uniforms", [new("WebGLProgram", "program", "Linked program"), new("int", "index", "Uniform index")]),
            new("GetUniformIndices", "Get indices of uniform variables", "int[]?", Gl2Ref("getUniformIndices"),
                "Uniforms", [new("WebGLProgram", "program", "Linked program"), new("string[]", "uniformNames", "Array of uniform names")]),
        ]),
        new("Drawing", [
            new("DrawArrays", "Draw primitives from array data", "", GlRef("drawArrays"),
                "Drawing", [
                    new("int", "mode", "Primitive type", "PrimitiveMode"),
                    new("int", "first", "Starting vertex index"),
                    new("int", "count", "Number of vertices to draw")
                ]),
            new("DrawElements", "Draw indexed primitives", "", GlRef("drawElements"),
                "Drawing", [
                    new("int", "mode", "Primitive type", "PrimitiveMode"),
                    new("int", "count", "Number of indices"),
                    new("int", "type", "Index type", "DataType"),
                    new("int", "offset", "Byte offset into index buffer")
                ]),
            new("DrawArraysInstanced", "Draw multiple instances from arrays", "", Gl2Ref("drawArraysInstanced"),
                "Drawing", [
                    new("int", "mode", "Primitive type", "PrimitiveMode"),
                    new("int", "first", "Starting vertex"),
                    new("int", "count", "Vertices per instance"),
                    new("int", "instanceCount", "Number of instances")
                ]),
            new("DrawElementsInstanced", "Draw multiple indexed instances", "", Gl2Ref("drawElementsInstanced"),
                "Drawing", [new("int", "mode", "Primitive type", "PrimitiveMode"), new("int", "count", "Indices"), new("int", "type", "Index type", "DataType"), new("int", "offset", "Byte offset"), new("int", "instanceCount", "Instances")]),
            new("DrawBuffers", "Specify which color buffers to draw into", "", Gl2Ref("drawBuffers"),
                "Drawing", [new("int[]", "buffers", "Color attachments (COLOR_ATTACHMENT0, etc.)")]),
        ]),
        new("Textures", [
            new("CreateTexture", "Create a texture object", "WebGLTexture", GlRef("createTexture"),
                "Textures", []),
            new("BindTexture", "Bind a texture to a target", "", GlRef("bindTexture"),
                "Textures", [
                    new("int", "target", "Texture target", "TextureTarget"),
                    new("WebGLTexture?", "texture", "Texture to bind (null to unbind)")
                ]),
            new("ActiveTexture", "Select the active texture unit", "", GlRef("activeTexture"),
                "Textures", [new("int", "unit", "TEXTURE0 + n")]),
            new("TexImage2D", "Specify a 2D texture image", "", GlRef("texImage2D"),
                "Textures", [
                    new("int", "target", "Texture target", "TextureTarget"),
                    new("int", "level", "Mipmap level"),
                    new("int", "internalformat", "Internal format (RGBA, RGBA8, etc.)"),
                    new("int", "width", "Width in pixels"),
                    new("int", "height", "Height in pixels"),
                    new("int", "border", "Must be 0"),
                    new("int", "format", "Pixel data format"),
                    new("int", "type", "Pixel data type", "DataType"),
                    new("byte[]?", "pixels", "Pixel data (null for empty texture)")
                ]),
             new("TexSubImage2D", "Update a sub-rectangle of an existing 2D texture", "", GlRef("texSubImage2D"),
                 "Textures", [
                     new("int", "target", "Texture target"),
                     new("int", "level", "Mipmap level"),
                     new("int", "xoffset", "X offset"),
                     new("int", "yoffset", "Y offset"),
                     new("int", "width", "Width"),
                     new("int", "height", "Height"),
                     new("int", "format", "Format"),
                     new("int", "type", "Type"),
                     new("byte[]?", "pixels", "Pixel data")
                 ]),
             new("TexSubImage2D", "Update a sub-rectangle of an existing 2D texture (Zero-Copy)", "", GlRef("texSubImage2D"),
                 "Textures", [
                     new("int", "target", "Texture target"),
                     new("int", "level", "Mipmap level"),
                     new("int", "xoffset", "X offset"),
                     new("int", "yoffset", "Y offset"),
                     new("int", "width", "Width"),
                     new("int", "height", "Height"),
                     new("int", "format", "Format"),
                     new("int", "type", "Type"),
                     new("Span<byte>", "pixels", "Pixel data")
                 ]),
            new("TexImage2D", "Specify a 2D texture image (Zero-Copy)", "", GlRef("texImage2D"),
                "Textures", [
                    new("int", "target", "Texture target", "TextureTarget"),
                    new("int", "level", "Mipmap level"),
                    new("int", "internalformat", "Internal format"),
                    new("int", "width", "Width"),
                    new("int", "height", "Height"),
                    new("int", "border", "Border (0)"),
                    new("int", "format", "Format"),
                    new("int", "type", "Type"),
                    new("Span<byte>", "pixels", "Pixel data")
                ]),
            new("TexParameteri", "Set texture parameter", "", GlRef("texParameter"),
                "Textures", [
                    new("int", "target", "Texture target", "TextureTarget"),
                    new("int", "pname", "Parameter name", "TextureParameter"),
                    new("int", "param", "Parameter value")
                ]),
            new("TexParameterf", "Set float texture parameter", "", GlRef("texParameter"),
                "Textures", [
                    new("int", "target", "Texture target", "TextureTarget"),
                    new("int", "pname", "Parameter name", "TextureParameter"),
                    new("float", "param", "Parameter value")
                ]),
            new("TexImage3D", "Specify a 3D texture image", "", Gl2Ref("texImage3D"),
                "Textures", [
                    new("int", "target", "Texture target (TEXTURE_3D or TEXTURE_2D_ARRAY)"),
                    new("int", "level", "Mipmap level"),
                    new("int", "internalformat", "Internal format"),
                    new("int", "width", "Width"),
                    new("int", "height", "Height"),
                    new("int", "depth", "Depth"),
                    new("int", "border", "Must be 0"),
                    new("int", "format", "Pixel format"),
                    new("int", "type", "Pixel type", "DataType"),
                    new("object?", "pixels", "Pixel data")
                ]),
            new("TexImage2DFromImage", "Specify a 2D texture from HTML element", "", GlRef("texImage2D"),
                "Textures", [
                    new("int", "target", "Texture target"),
                    new("int", "level", "Mipmap level"),
                    new("int", "internalformat", "Internal format"),
                    new("int", "format", "Pixel format"),
                    new("int", "type", "Pixel type", "DataType"),
                    new("object", "source", "HTML image/canvas element")
                ]),
            new("CopyTexImage2D", "Copy framebuffer to 2D texture", "", GlRef("copyTexImage2D"),
                "Textures", [
                    new("int", "target", "Texture target"),
                    new("int", "level", "Mipmap level"),
                    new("int", "internalformat", "Internal format"),
                    new("int", "x", "Framebuffer X"),
                    new("int", "y", "Framebuffer Y"),
                    new("int", "width", "Width"),
                    new("int", "height", "Height"),
                    new("int", "border", "Must be 0")
                ]),
            new("CopyTexSubImage2D", "Copy framebuffer to 2D texture sub-region", "", GlRef("copyTexSubImage2D"),
                "Textures", [
                    new("int", "target", "Texture target"),
                    new("int", "level", "Mipmap level"),
                    new("int", "xoffset", "X offset"),
                    new("int", "yoffset", "Y offset"),
                    new("int", "x", "Framebuffer X"),
                    new("int", "y", "Framebuffer Y"),
                    new("int", "width", "Width"),
                    new("int", "height", "Height")
                ]),
            new("CompressedTexImage2D", "Specify a compressed 2D texture", "", GlRef("compressedTexImage2D"),
                "Textures", [
                    new("int", "target", "Texture target"),
                    new("int", "level", "Mipmap level"),
                    new("int", "internalformat", "Compressed format"),
                    new("int", "width", "Width"),
                    new("int", "height", "Height"),
                    new("int", "border", "Must be 0"),
                    new("Span<byte>", "data", "Compressed image data")
                ]),
            new("CompressedTexSubImage2D", "Update compressed 2D texture sub-region", "", GlRef("compressedTexSubImage2D"),
                "Textures", [
                    new("int", "target", "Texture target"),
                    new("int", "level", "Mipmap level"),
                    new("int", "xoffset", "X offset"),
                    new("int", "yoffset", "Y offset"),
                    new("int", "width", "Width"),
                    new("int", "height", "Height"),
                    new("int", "format", "Compressed format"),
                    new("Span<byte>", "data", "Compressed image data")
                ]),
            new("CompressedTexImage3D", "Specify a compressed 3D texture", "", Gl2Ref("compressedTexImage3D"),
                "Textures", [
                    new("int", "target", "Texture target"),
                    new("int", "level", "Mipmap level"),
                    new("int", "internalformat", "Compressed format"),
                    new("int", "width", "Width"),
                    new("int", "height", "Height"),
                    new("int", "depth", "Depth"),
                    new("int", "border", "Must be 0"),
                    new("Span<byte>", "data", "Compressed image data")
                ]),
            new("CompressedTexSubImage3D", "Update compressed 3D texture sub-region", "", Gl2Ref("compressedTexSubImage3D"),
                "Textures", [
                    new("int", "target", "Texture target"),
                    new("int", "level", "Mipmap level"),
                    new("int", "xoffset", "X offset"),
                    new("int", "yoffset", "Y offset"),
                    new("int", "zoffset", "Z offset"),
                    new("int", "width", "Width"),
                    new("int", "height", "Height"),
                    new("int", "depth", "Depth"),
                    new("int", "format", "Compressed format"),
                    new("Span<byte>", "data", "Compressed image data")
                ]),
            new("GenerateMipmap", "Generate mipmaps for bound texture", "", GlRef("generateMipmap"),
                "Textures", [new("int", "target", "Texture target", "TextureTarget")]),
            new("DeleteTexture", "Delete a texture object", "", GlRef("deleteTexture"),
                "Textures", [new("WebGLTexture", "texture", "Texture to delete")]),
            new("IsTexture", "Check if an object is a valid texture", "bool", GlRef("isTexture"),
                "Textures", [new("WebGLTexture", "texture", "Object to check")]),
            new("GetTexParameterInt", "Get an integer texture parameter", "int", GlRef("getTexParameter"),
                "Textures", [new("int", "target", "Texture target"), new("int", "pname", "Parameter name")]),
            new("GetTexParameterFloat", "Get a float texture parameter", "float", GlRef("getTexParameter"),
                "Textures", [new("int", "target", "Texture target"), new("int", "pname", "Parameter name")]),
        ]),
        new("Framebuffers", [
            new("CreateFramebuffer", "Create a framebuffer object", "WebGLFramebuffer", GlRef("createFramebuffer"),
                "Framebuffers", []),
            new("BindFramebuffer", "Bind a framebuffer", "", GlRef("bindFramebuffer"),
                "Framebuffers", [
                    new("int", "target", "Framebuffer target", "FramebufferTarget"),
                    new("WebGLFramebuffer?", "framebuffer", "Framebuffer to bind (null for default)")
                ]),
            new("FramebufferTexture2D", "Attach a texture to a framebuffer", "", GlRef("framebufferTexture2D"),
                "Framebuffers", [
                    new("int", "target", "Framebuffer target", "FramebufferTarget"),
                    new("int", "attachment", "Attachment point", "FramebufferAttachment"),
                    new("int", "textarget", "Texture target (TEXTURE_2D)", "TextureTarget"),
                    new("WebGLTexture", "texture", "Texture to attach"),
                    new("int", "level", "Mipmap level")
                ]),
            new("FramebufferRenderbuffer", "Attach a renderbuffer to framebuffer", "", GlRef("framebufferRenderbuffer"),
                "Framebuffers", [
                    new("int", "target", "Framebuffer target", "FramebufferTarget"),
                    new("int", "attachment", "Attachment point", "FramebufferAttachment"),
                    new("int", "renderbuffertarget", "RENDERBUFFER"),
                    new("WebGLRenderbuffer", "renderbuffer", "Renderbuffer to attach")
                ]),
            new("CheckFramebufferStatus", "Check framebuffer completeness", "int", GlRef("checkFramebufferStatus"),
                "Framebuffers", [new("int", "target", "Framebuffer target", "FramebufferTarget")]),
            new("GetFramebufferAttachmentParameterInt", "Get integer attachment parameter", "int", GlRef("getFramebufferAttachmentParameter"),
                "Framebuffers", [new("int", "target", "Target"), new("int", "attachment", "Attachment"), new("int", "pname", "Parameter")]),
            new("DeleteFramebuffer", "Delete a framebuffer", "", GlRef("deleteFramebuffer"),
                "Framebuffers", [new("WebGLFramebuffer", "framebuffer", "Framebuffer to delete")]),
            new("IsFramebuffer", "Check if an object is a valid framebuffer", "bool", GlRef("isFramebuffer"),
                "Framebuffers", [new("WebGLFramebuffer", "framebuffer", "Object to check")]),
            new("FramebufferTextureLayer", "Attach a layer of a texture to a framebuffer", "", Gl2Ref("framebufferTextureLayer"),
                "Framebuffers", [
                    new("int", "target", "Framebuffer target", "FramebufferTarget"),
                    new("int", "attachment", "Attachment point", "FramebufferAttachment"),
                    new("WebGLTexture", "texture", "Texture to attach"),
                    new("int", "level", "Mipmap level"),
                    new("int", "layer", "Texture layer")
                ]),
            new("BlitFramebuffer", "Transfer a block of pixels between framebuffers", "", Gl2Ref("blitFramebuffer"),
                "Framebuffers", [
                    new("int", "srcX0", "Source X start"),
                    new("int", "srcY0", "Source Y start"),
                    new("int", "srcX1", "Source X end"),
                    new("int", "srcY1", "Source Y end"),
                    new("int", "dstX0", "Destination X start"),
                    new("int", "dstY0", "Destination Y start"),
                    new("int", "dstX1", "Destination X end"),
                    new("int", "dstY1", "Destination Y end"),
                    new("int", "mask", "Bitwise OR of buffer bits", "ClearMask"),
                    new("int", "filter", "Filtering mode")
                ]),
            new("ReadBuffer", "Select a color buffer source for reading pixels", "", Gl2Ref("readBuffer"),
                "Framebuffers", [new("int", "src", "Color buffer source")]),
            new("InvalidateFramebuffer", "Invalidate framebuffer contents", "", Gl2Ref("invalidateFramebuffer"),
                "Framebuffers", [
                    new("int", "target", "Framebuffer target"),
                    new("int[]", "attachments", "Attachments to invalidate")
                ]),
            new("InvalidateSubFramebuffer", "Invalidate a sub-region of framebuffer", "", Gl2Ref("invalidateSubFramebuffer"),
                "Framebuffers", [
                    new("int", "target", "Framebuffer target"),
                    new("int[]", "attachments", "Attachments to invalidate"),
                    new("int", "x", "X offset"),
                    new("int", "y", "Y offset"),
                    new("int", "width", "Width"),
                    new("int", "height", "Height")
                ]),
        ]),
        new("Renderbuffers", [
            new("CreateRenderbuffer", "Create a renderbuffer object", "WebGLRenderbuffer", GlRef("createRenderbuffer"),
                "Renderbuffers", []),
            new("BindRenderbuffer", "Bind a renderbuffer", "", GlRef("bindRenderbuffer"),
                "Renderbuffers", [
                    new("int", "target", "RENDERBUFFER"),
                    new("WebGLRenderbuffer?", "renderbuffer", "Renderbuffer to bind")
                ]),
            new("RenderbufferStorage", "Allocate renderbuffer storage", "", GlRef("renderbufferStorage"),
                "Renderbuffers", [
                    new("int", "target", "RENDERBUFFER"),
                    new("int", "internalformat", "DEPTH_COMPONENT24, STENCIL_INDEX8, etc."),
                    new("int", "width", "Width in pixels"),
                    new("int", "height", "Height in pixels")
                ]),
            new("GetRenderbufferParameterInt", "Get integer renderbuffer parameter", "int", GlRef("getRenderbufferParameter"),
                "Renderbuffers", [new("int", "target", "RENDERBUFFER"), new("int", "pname", "Parameter")]),
            new("DeleteRenderbuffer", "Delete a renderbuffer", "", GlRef("deleteRenderbuffer"),
                "Renderbuffers", [new("WebGLRenderbuffer", "renderbuffer", "Renderbuffer to delete")]),
            new("IsRenderbuffer", "Check if an object is a valid renderbuffer", "bool", GlRef("isRenderbuffer"),
                "Renderbuffers", [new("WebGLRenderbuffer", "renderbuffer", "Object to check")]),
            new("RenderbufferStorageMultisample", "Allocate multisample renderbuffer storage", "", Gl2Ref("renderbufferStorageMultisample"),
                "Renderbuffers", [
                    new("int", "target", "RENDERBUFFER"),
                    new("int", "samples", "Number of samples"),
                    new("int", "internalformat", "Internal format"),
                    new("int", "width", "Width in pixels"),
                    new("int", "height", "Height in pixels")
                ]),
        ]),
        new("Texture Storage (WebGL 2.0)", [
            new("TexStorage2D", "Allocate immutable 2D texture storage", "", Gl2Ref("texStorage2D"),
                "Textures", [
                    new("int", "target", "Texture target", "TextureTarget"),
                    new("int", "levels", "Number of mipmap levels"),
                    new("int", "internalformat", "Sized internal format (RGBA8, etc.)"),
                    new("int", "width", "Width in pixels"),
                    new("int", "height", "Height in pixels")
                ]),
            new("TexStorage3D", "Allocate immutable 3D texture storage", "", Gl2Ref("texStorage3D"),
                "Textures", [
                    new("int", "target", "Texture target", "TextureTarget"),
                    new("int", "levels", "Number of mipmap levels"),
                    new("int", "internalformat", "Sized internal format"),
                    new("int", "width", "Width in pixels"),
                    new("int", "height", "Height in pixels"),
                    new("int", "depth", "Depth in pixels")
                ]),
            new("TexSubImage3D", "Update a portion of a 3D texture", "", Gl2Ref("texSubImage3D"),
                "Textures", [
                    new("int", "target", "Texture target", "TextureTarget"),
                    new("int", "level", "Mipmap level"),
                    new("int", "xoffset", "X offset"), new("int", "yoffset", "Y offset"), new("int", "zoffset", "Z offset"),
                    new("int", "width", "Width"), new("int", "height", "Height"), new("int", "depth", "Depth"),
                    new("int", "format", "Pixel format"), new("int", "type", "Data type", "DataType")
                ]),
            new("CopyTexSubImage3D", "Copy framebuffer to 3D texture", "", Gl2Ref("copyTexSubImage3D"),
                "Textures", [
                    new("int", "target", "Texture target", "TextureTarget"),
                    new("int", "level", "Mipmap level"),
                    new("int", "xoffset", "X offset"), new("int", "yoffset", "Y offset"), new("int", "zoffset", "Z offset"),
                    new("int", "x", "Framebuffer X"), new("int", "y", "Framebuffer Y"),
                    new("int", "width", "Width"), new("int", "height", "Height")
                ]),
        ]),
        new("Drawing Extensions (WebGL 2.0)", [
            new("DrawRangeElements", "Draw indexed primitives in a range", "", Gl2Ref("drawRangeElements"),
                "Drawing", [
                    new("int", "mode", "Primitive type", "PrimitiveMode"),
                    new("int", "start", "Minimum array index"),
                    new("int", "end", "Maximum array index"),
                    new("int", "count", "Number of indices"),
                    new("int", "type", "Index type", "DataType"),
                    new("int", "offset", "Byte offset into index buffer")
                ]),
            new("ClearBufferfv", "Clear a float buffer attachment", "", Gl2Ref("clearBuffer"),
                "Drawing", [
                    new("int", "buffer", "Buffer to clear (COLOR)"),
                    new("int", "drawbuffer", "Draw buffer index"),
                    new("Span<float>", "values", "Clear values (4 floats)")
                ]),
            new("ClearBufferiv", "Clear an integer buffer attachment", "", Gl2Ref("clearBuffer"),
                "Drawing", [
                    new("int", "buffer", "Buffer to clear (COLOR)"),
                    new("int", "drawbuffer", "Draw buffer index"),
                    new("Span<int>", "values", "Clear values (4 ints)")
                ]),
            new("ClearBufferuiv", "Clear an unsigned integer buffer attachment", "", Gl2Ref("clearBuffer"),
                "Drawing", [
                    new("int", "buffer", "Buffer to clear (COLOR)"),
                    new("int", "drawbuffer", "Draw buffer index"),
                    new("Span<uint>", "values", "Clear values (4 uints)")
                ]),
            new("ClearBufferfi", "Clear depth and stencil buffers", "", Gl2Ref("clearBuffer"),
                "Drawing", [
                    new("int", "buffer", "DEPTH_STENCIL"),
                    new("int", "drawbuffer", "Must be 0"),
                    new("float", "depth", "Depth clear value"),
                    new("int", "stencil", "Stencil clear value")
                ]),
        ]),
        new("Query Objects (WebGL 2.0)", [
            new("CreateQuery", "Create a query object", "WebGLQuery", Gl2Ref("createQuery"),
                "Queries", []),
            new("DeleteQuery", "Delete a query object", "", Gl2Ref("deleteQuery"),
                "Queries", [new("WebGLQuery", "query", "Query to delete")]),
            new("BeginQuery", "Begin an asynchronous query", "", Gl2Ref("beginQuery"),
                "Queries", [
                    new("int", "target", "Query type (ANY_SAMPLES_PASSED, etc.)"),
                    new("WebGLQuery", "query", "Query object")
                ]),
            new("EndQuery", "End an asynchronous query", "", Gl2Ref("endQuery"),
                "Queries", [new("int", "target", "Query type")]),
            new("GetQuery", "Get a query object for target", "WebGLQuery?", Gl2Ref("getQuery"),
                "Queries", [new("int", "target", "Query target"), new("int", "pname", "CURRENT_QUERY")]),
            new("GetQueryParameterInt", "Get integer query parameter", "int", Gl2Ref("getQueryParameter"),
                "Queries", [new("WebGLQuery", "query", "Query object"), new("int", "pname", "Parameter")]),
            new("GetQueryParameterBool", "Get boolean query parameter", "bool", Gl2Ref("getQueryParameter"),
                "Queries", [new("WebGLQuery", "query", "Query object"), new("int", "pname", "Parameter")]),
            new("IsQuery", "Check if an object is a valid query", "bool", Gl2Ref("isQuery"),
                "Queries", [new("WebGLQuery", "query", "Object to check")]),
        ]),
        new("Sampler Objects (WebGL 2.0)", [
            new("CreateSampler", "Create a sampler object", "WebGLSampler", Gl2Ref("createSampler"),
                "Samplers", []),
            new("BindSampler", "Bind a sampler to a texture unit", "", Gl2Ref("bindSampler"),
                "Samplers", [
                    new("int", "unit", "Texture unit index"),
                    new("WebGLSampler?", "sampler", "Sampler to bind (null to unbind)")
                ]),
            new("SamplerParameteri", "Set integer sampler parameter", "", Gl2Ref("samplerParameter"),
                "Samplers", [
                    new("WebGLSampler", "sampler", "Sampler object"),
                    new("int", "pname", "Parameter name", "TextureParameter"),
                    new("int", "param", "Parameter value")
                ]),
            new("GetSamplerParameterInt", "Get integer sampler parameter", "int", Gl2Ref("getSamplerParameter"),
                "Samplers", [new("WebGLSampler", "sampler", "Sampler object"), new("int", "pname", "Parameter name")]),
            new("GetSamplerParameterFloat", "Get float sampler parameter", "float", Gl2Ref("getSamplerParameter"),
                "Samplers", [new("WebGLSampler", "sampler", "Sampler object"), new("int", "pname", "Parameter name")]),
            new("DeleteSampler", "Delete a sampler object", "", Gl2Ref("deleteSampler"),
                "Samplers", [new("WebGLSampler", "sampler", "Sampler to delete")]),
            new("SamplerParameterf", "Set float sampler parameter", "", Gl2Ref("samplerParameter"),
                "Samplers", [
                    new("WebGLSampler", "sampler", "Sampler object"),
                    new("int", "pname", "Parameter name", "TextureParameter"),
                    new("float", "param", "Parameter value")
                ]),
            new("IsSampler", "Check if an object is a valid sampler", "bool", Gl2Ref("isSampler"),
                "Samplers", [new("WebGLSampler", "sampler", "Object to check")]),
        ]),
        new("Sync Objects (WebGL 2.0)", [
            new("FenceSync", "Create a sync object", "WebGLSync", Gl2Ref("fenceSync"),
                "Sync", [
                    new("int", "condition", "SYNC_GPU_COMMANDS_COMPLETE"),
                    new("int", "flags", "Must be 0")
                ]),
            new("ClientWaitSync", "Block until sync is signaled", "int", Gl2Ref("clientWaitSync"),
                "Sync", [
                    new("WebGLSync", "sync", "Sync object"),
                    new("int", "flags", "SYNC_FLUSH_COMMANDS_BIT or 0"),
                    new("double", "timeout", "Timeout in nanoseconds")
                ]),
            new("WaitSync", "Server-side wait on sync", "", Gl2Ref("waitSync"),
                "Sync", [
                    new("WebGLSync", "sync", "Sync object"),
                    new("int", "flags", "Must be 0"),
                    new("double", "timeout", "TIMEOUT_IGNORED (-1)")
                ]),
            new("GetSyncParameterInt", "Get integer sync parameter", "int", Gl2Ref("getSyncParameter"),
                "Sync", [new("WebGLSync", "sync", "Sync object"), new("int", "pname", "Parameter")]),
            new("DeleteSync", "Delete a sync object", "", Gl2Ref("deleteSync"),
                "Sync", [new("WebGLSync", "sync", "Sync to delete")]),
            new("IsSync", "Check if an object is a valid sync", "bool", Gl2Ref("isSync"),
                "Sync", [new("WebGLSync", "sync", "Object to check")]),
        ]),
        new("Transform Feedback (WebGL 2.0)", [
            new("CreateTransformFeedback", "Create a transform feedback object", "WebGLTransformFeedback", Gl2Ref("createTransformFeedback"),
                "TransformFeedback", []),
            new("BindTransformFeedback", "Bind a transform feedback object", "", Gl2Ref("bindTransformFeedback"),
                "TransformFeedback", [
                    new("int", "target", "TRANSFORM_FEEDBACK"),
                    new("WebGLTransformFeedback?", "tf", "Object to bind")
                ]),
            new("BeginTransformFeedback", "Begin transform feedback recording", "", Gl2Ref("beginTransformFeedback"),
                "TransformFeedback", [new("int", "primitiveMode", "POINTS, LINES, or TRIANGLES", "PrimitiveMode")]),
            new("EndTransformFeedback", "End transform feedback recording", "", Gl2Ref("endTransformFeedback"),
                "TransformFeedback", []),
            new("TransformFeedbackVaryings", "Specify varyings to capture", "", Gl2Ref("transformFeedbackVaryings"),
                "TransformFeedback", [
                    new("WebGLProgram", "program", "Program to configure"),
                    new("string[]", "varyings", "Names of output variables"),
                    new("int", "bufferMode", "SEPARATE_ATTRIBS or INTERLEAVED_ATTRIBS")
                ]),
            new("PauseTransformFeedback", "Pause transform feedback", "", Gl2Ref("pauseTransformFeedback"),
                "TransformFeedback", []),
            new("ResumeTransformFeedback", "Resume transform feedback", "", Gl2Ref("resumeTransformFeedback"),
                "TransformFeedback", []),
            new("DeleteTransformFeedback", "Delete a transform feedback object", "", Gl2Ref("deleteTransformFeedback"),
                "TransformFeedback", [new("WebGLTransformFeedback", "transformFeedback", "Object to delete")]),
            new("IsTransformFeedback", "Check if an object is a valid transform feedback", "bool", Gl2Ref("isTransformFeedback"),
                "TransformFeedback", [new("WebGLTransformFeedback", "transformFeedback", "Object to check")]),
        ]),
        new("Uniform Buffer Objects (WebGL 2.0)", [
            new("GetUniformBlockIndex", "Get index of a uniform block", "int", Gl2Ref("getUniformBlockIndex"),
                "UBO", [
                    new("WebGLProgram", "program", "Linked program"),
                    new("string", "uniformBlockName", "Block name in shader")
                ]),
            new("UniformBlockBinding", "Assign a binding point to uniform block", "", Gl2Ref("uniformBlockBinding"),
                "UBO", [
                    new("WebGLProgram", "program", "Linked program"),
                    new("int", "uniformBlockIndex", "Block index"),
                    new("int", "uniformBlockBinding", "Binding point (0-based)")
                ]),
            new("BindBufferBase", "Bind buffer to indexed binding point", "", Gl2Ref("bindBufferBase"),
                "UBO", [
                    new("int", "target", "UNIFORM_BUFFER or TRANSFORM_FEEDBACK_BUFFER", "BufferTarget"),
                    new("int", "index", "Binding point index"),
                    new("WebGLBuffer", "buffer", "Buffer to bind")
                ]),
            new("BindBufferRange", "Bind buffer range to indexed binding point", "", Gl2Ref("bindBufferRange"),
                "UBO", [
                    new("int", "target", "Buffer target", "BufferTarget"),
                    new("int", "index", "Binding point index"),
                    new("WebGLBuffer", "buffer", "Buffer to bind"),
                    new("int", "offset", "Byte offset"),
                    new("int", "size", "Byte size")
                ]),
            new("GetActiveUniformBlockName", "Get the name of an active uniform block", "string?", Gl2Ref("getActiveUniformBlockName"),
                "UBO", [new("WebGLProgram", "program", "Linked program"), new("int", "blockIndex", "Block index")]),
        ]),
        new("Uniform Array & Matrix Extensions", [
            new("Uniform1fv", "Set float array uniform", "", GlRef("uniform"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("Span<float>", "data", "Array of float values")
                ]),
            new("Uniform2fv", "Set vec2 array uniform", "", GlRef("uniform"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("Span<float>", "data", "Packed array of vec2 values")
                ]),
            new("Uniform3fv", "Set vec3 array uniform", "", GlRef("uniform"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("Span<float>", "data", "Packed array of vec3 values")
                ]),
            new("Uniform4fv", "Set vec4 array uniform", "", GlRef("uniform"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("Span<float>", "data", "Packed array of vec4 values")
                ]),
            new("Uniform1iv", "Set int array uniform", "", GlRef("uniform"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("Span<int>", "data", "Array of int values")
                ]),
            new("Uniform2iv", "Set ivec2 array uniform", "", GlRef("uniform"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("Span<int>", "data", "Packed array of ivec2 values")
                ]),
            new("Uniform3iv", "Set ivec3 array uniform", "", GlRef("uniform"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("Span<int>", "data", "Packed array of ivec3 values")
                ]),
            new("Uniform4iv", "Set ivec4 array uniform", "", GlRef("uniform"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("Span<int>", "data", "Packed array of ivec4 values")
                ]),
            new("Uniform1uiv", "Set uint array uniform", "", Gl2Ref("uniform"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("Span<uint>", "data", "Array of uint values")
                ]),
            new("Uniform2uiv", "Set uvec2 array uniform", "", Gl2Ref("uniform"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("Span<uint>", "data", "Packed array of uvec2 values")
                ]),
            new("Uniform3uiv", "Set uvec3 array uniform", "", Gl2Ref("uniform"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("Span<uint>", "data", "Packed array of uvec3 values")
                ]),
            new("Uniform4uiv", "Set uvec4 array uniform", "", Gl2Ref("uniform"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("Span<uint>", "data", "Packed array of uvec4 values")
                ]),
            new("UniformMatrix2x3fv", "Set mat2x3 uniform", "", Gl2Ref("uniformMatrix"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("bool", "transpose", "Transpose the matrix"),
                    new("Span<float>", "data", "6 floats in column-major order")
                ]),
            new("UniformMatrix3x2fv", "Set mat3x2 uniform", "", Gl2Ref("uniformMatrix"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("bool", "transpose", "Transpose the matrix"),
                    new("Span<float>", "data", "6 floats in column-major order")
                ]),
            new("UniformMatrix2x4fv", "Set mat2x4 uniform", "", Gl2Ref("uniformMatrix"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("bool", "transpose", "Transpose the matrix"),
                    new("Span<float>", "data", "8 floats in column-major order")
                ]),
            new("UniformMatrix4x2fv", "Set mat4x2 uniform", "", Gl2Ref("uniformMatrix"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("bool", "transpose", "Transpose the matrix"),
                    new("Span<float>", "data", "8 floats in column-major order")
                ]),
            new("UniformMatrix3x4fv", "Set mat3x4 uniform", "", Gl2Ref("uniformMatrix"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("bool", "transpose", "Transpose the matrix"),
                    new("Span<float>", "data", "12 floats in column-major order")
                ]),
            new("UniformMatrix4x3fv", "Set mat4x3 uniform", "", Gl2Ref("uniformMatrix"),
                "Uniforms", [
                    new("WebGLUniformLocation", "location", "Uniform location"),
                    new("bool", "transpose", "Transpose the matrix"),
                    new("Span<float>", "data", "12 floats in column-major order")
                ]),
        ]),
        new("Context & Extensions", [
            new("GetSupportedExtensions", "Get list of supported WebGL extensions", "string[]?", GlRef("getSupportedExtensions"),
                "Extensions", []),
        ]),
        new("Pixel Operations", [
            new("ReadPixels", "Read pixels from framebuffer", "", GlRef("readPixels"),
                "Pixels", [
                    new("int", "x", "X start position"),
                    new("int", "y", "Y start position"),
                    new("int", "width", "Width in pixels"),
                    new("int", "height", "Height in pixels"),
                    new("int", "format", "Pixel format (RGBA)"),
                    new("int", "type", "Data type", "DataType"),
                    new("Span<byte>", "dstData", "Destination buffer")
                ]),
            new("ReadPixelColor", "Read a single pixel color", "int[]", GlRef("readPixels"),
                "Pixels", [new("int", "x", "X position"), new("int", "y", "Y position")]),
        ]),
        new("Color Spaces (WebGL 2.0)", [
            new("DrawingBufferColorSpace", "Get/set the color space of the WebGL drawing buffer", "string", MdnRef("WebGL2RenderingContext/drawingBufferColorSpace"),
                "ColorSpace", [new("string", "value", "\"srgb\" (default) or \"display-p3\"")]),
            new("UnpackColorSpace", "Get/set the color space used when importing textures", "string", MdnRef("WebGL2RenderingContext/unpackColorSpace"),
                "ColorSpace", [new("string", "value", "\"srgb\" (default), \"display-p3\", or \"none\"")]),
        ]),
    ];
}
