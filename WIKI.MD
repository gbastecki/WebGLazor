# WebGLazor API Reference

## WebGLCanvas Component

A Blazor component that wraps an HTML canvas element with WebGL 2.0 context initialization. Provides comprehensive event handling and supports unmatched HTML attributes.

Any HTML attributes not explicitly defined as parameters (e.g., `class`, `tabindex`, `data-*`) are captured via `AdditionalAttributes` and passed directly to the underlying `<canvas>` element.

### Core Parameters
Essential properties for canvas configuration.

| Type | Parameter | Default | Description |
|------|-----------|---------|-------------|
| `string` | `Id` | `Guid.NewGuid()` | Unique identifier for the canvas element |
| `int` | `Width` | `800` | Canvas width in pixels |
| `int` | `Height` | `600` | Canvas height in pixels |
| `string` | `Style` | `""` | Inline CSS styles for the canvas |

### Context Callback
Callback invoked when WebGL context is ready.

| Type | Parameter | Default | Description |
|------|-----------|---------|-------------|
| `EventCallback<WebGLContext>` | `OnContextCreated` | `NULL` | Fired after WebGL 2.0 context initialization completes |

### Mouse Events
Callbacks for mouse interactions on the canvas.

| Type | Parameter | Default | Description |
|------|-----------|---------|-------------|
| `EventCallback<MouseEventArgs>` | `OnCanvasMouseMove` | `NULL` | Mouse moved over canvas |
| `EventCallback<MouseEventArgs>` | `OnCanvasMouseDown` | `NULL` | Mouse button pressed |
| `EventCallback<MouseEventArgs>` | `OnCanvasMouseUp` | `NULL` | Mouse button released |
| `EventCallback<MouseEventArgs>` | `OnCanvasClick` | `NULL` | Mouse click |
| `EventCallback<MouseEventArgs>` | `OnCanvasDoubleClick` | `NULL` | Mouse double-click |
| `EventCallback<MouseEventArgs>` | `OnCanvasMouseEnter` | `NULL` | Mouse entered canvas area |
| `EventCallback<MouseEventArgs>` | `OnCanvasMouseLeave` | `NULL` | Mouse left canvas area |
| `EventCallback<MouseEventArgs>` | `OnCanvasMouseOver` | `NULL` | Mouse over canvas (bubbles) |
| `EventCallback<MouseEventArgs>` | `OnCanvasMouseOut` | `NULL` | Mouse out of canvas (bubbles) |
| `EventCallback<WheelEventArgs>` | `OnCanvasWheel` | `NULL` | Mouse wheel scrolled |
| `EventCallback<MouseEventArgs>` | `OnCanvasContextMenu` | `NULL` | Right-click context menu |

### Keyboard Events
Callbacks for keyboard input when canvas has focus.

| Type | Parameter | Default | Description |
|------|-----------|---------|-------------|
| `EventCallback<KeyboardEventArgs>` | `OnCanvasKeyDown` | `NULL` | Key pressed down |
| `EventCallback<KeyboardEventArgs>` | `OnCanvasKeyUp` | `NULL` | Key released |
| `EventCallback<KeyboardEventArgs>` | `OnCanvasKeyPress` | `NULL` | Key press (character input) |

### Touch Events
Callbacks for touch interactions on mobile devices.

| Type | Parameter | Default | Description |
|------|-----------|---------|-------------|
| `EventCallback<TouchEventArgs>` | `OnCanvasTouchStart` | `NULL` | Touch started |
| `EventCallback<TouchEventArgs>` | `OnCanvasTouchEnd` | `NULL` | Touch ended |
| `EventCallback<TouchEventArgs>` | `OnCanvasTouchMove` | `NULL` | Touch moved |
| `EventCallback<TouchEventArgs>` | `OnCanvasTouchCancel` | `NULL` | Touch cancelled |

### Pointer Events
Unified input callbacks for mouse, touch, and stylus.

| Type | Parameter | Default | Description |
|------|-----------|---------|-------------|
| `EventCallback<PointerEventArgs>` | `OnCanvasPointerDown` | `NULL` | Pointer pressed |
| `EventCallback<PointerEventArgs>` | `OnCanvasPointerUp` | `NULL` | Pointer released |
| `EventCallback<PointerEventArgs>` | `OnCanvasPointerMove` | `NULL` | Pointer moved |
| `EventCallback<PointerEventArgs>` | `OnCanvasPointerEnter` | `NULL` | Pointer entered |
| `EventCallback<PointerEventArgs>` | `OnCanvasPointerLeave` | `NULL` | Pointer left |
| `EventCallback<PointerEventArgs>` | `OnCanvasPointerCancel` | `NULL` | Pointer cancelled |
| `EventCallback<PointerEventArgs>` | `OnCanvasPointerOut` | `NULL` | Pointer out (bubbles) |
| `EventCallback<PointerEventArgs>` | `OnCanvasPointerOver` | `NULL` | Pointer over (bubbles) |
| `EventCallback<PointerEventArgs>` | `OnCanvasGotPointerCapture` | `NULL` | Pointer capture acquired |
| `EventCallback<PointerEventArgs>` | `OnCanvasLostPointerCapture` | `NULL` | Pointer capture lost |

### Focus Events
Callbacks for focus state changes.

| Type | Parameter | Default | Description |
|------|-----------|---------|-------------|
| `EventCallback<FocusEventArgs>` | `OnCanvasFocus` | `NULL` | Canvas received focus |
| `EventCallback<FocusEventArgs>` | `OnCanvasBlur` | `NULL` | Canvas lost focus |
| `EventCallback<FocusEventArgs>` | `OnCanvasFocusIn` | `NULL` | Focus entered (bubbles) |
| `EventCallback<FocusEventArgs>` | `OnCanvasFocusOut` | `NULL` | Focus left (bubbles) |

### Drag & Drop Events
Callbacks for HTML5 drag and drop operations.

| Type | Parameter | Default | Description |
|------|-----------|---------|-------------|
| `EventCallback<DragEventArgs>` | `OnCanvasDrag` | `NULL` | Element being dragged |
| `EventCallback<DragEventArgs>` | `OnCanvasDragEnd` | `NULL` | Drag operation ended |
| `EventCallback<DragEventArgs>` | `OnCanvasDragEnter` | `NULL` | Dragged element entered canvas |
| `EventCallback<DragEventArgs>` | `OnCanvasDragLeave` | `NULL` | Dragged element left canvas |
| `EventCallback<DragEventArgs>` | `OnCanvasDragOver` | `NULL` | Dragged element over canvas |
| `EventCallback<DragEventArgs>` | `OnCanvasDragStart` | `NULL` | Drag operation started |
| `EventCallback<DragEventArgs>` | `OnCanvasDrop` | `NULL` | Element dropped on canvas |

### PreventDefault Modifiers
Set to true to call `preventDefault()` on the corresponding event.

| Type | Parameter | Default | Description |
|------|-----------|---------|-------------|
| `bool` | `PreventDefaultOnMouseMove` | `false` | Prevent default mouse move behavior |
| `bool` | `PreventDefaultOnMouseDown` | `false` | Prevent default mouse down behavior |
| `bool` | `PreventDefaultOnMouseUp` | `false` | Prevent default mouse up behavior |
| `bool` | `PreventDefaultOnClick` | `false` | Prevent default click behavior |
| `bool` | `PreventDefaultOnDoubleClick` | `false` | Prevent default double-click behavior |
| `bool` | `PreventDefaultOnWheel` | `false` | Prevent page scrolling on wheel |
| `bool` | `PreventDefaultOnContextMenu` | `false` | Prevent right-click context menu |
| `bool` | `PreventDefaultOnKeyDown` | `false` | Prevent default key down behavior |
| `bool` | `PreventDefaultOnKeyUp` | `false` | Prevent default key up behavior |
| `bool` | `PreventDefaultOnKeyPress` | `false` | Prevent default key press behavior |
| `bool` | `PreventDefaultOnTouchStart` | `false` | Prevent default touch start |
| `bool` | `PreventDefaultOnTouchEnd` | `false` | Prevent default touch end |
| `bool` | `PreventDefaultOnTouchMove` | `false` | Prevent touch scrolling |
| `bool` | `PreventDefaultOnPointerDown` | `false` | Prevent default pointer down |
| `bool` | `PreventDefaultOnPointerUp` | `false` | Prevent default pointer up |
| `bool` | `PreventDefaultOnPointerMove` | `false` | Prevent default pointer move |
| `bool` | `PreventDefaultOnDrag` | `false` | Prevent default drag behavior |
| `bool` | `PreventDefaultOnDragOver` | `false` | Prevent default dragover behavior |
| `bool` | `PreventDefaultOnDrop` | `false` | Prevent default drop behavior |

### StopPropagation Modifiers
Set to true to call `stopPropagation()` on the corresponding event.

| Type | Parameter | Default | Description |
|------|-----------|---------|-------------|
| `bool` | `StopPropagationOnMouseMove` | `false` | Stop mouse move from bubbling |
| `bool` | `StopPropagationOnMouseDown` | `false` | Stop mouse down from bubbling |
| `bool` | `StopPropagationOnMouseUp` | `false` | Stop mouse up from bubbling |
| `bool` | `StopPropagationOnClick` | `false` | Stop click from bubbling |
| `bool` | `StopPropagationOnDoubleClick` | `false` | Stop double-click from bubbling |
| `bool` | `StopPropagationOnWheel` | `false` | Stop wheel from bubbling |
| `bool` | `StopPropagationOnContextMenu` | `false` | Stop context menu from bubbling |
| `bool` | `StopPropagationOnKeyDown` | `false` | Stop key down from bubbling |
| `bool` | `StopPropagationOnKeyUp` | `false` | Stop key up from bubbling |
| `bool` | `StopPropagationOnKeyPress` | `false` | Stop key press from bubbling |
| `bool` | `StopPropagationOnTouchStart` | `false` | Stop touch start from bubbling |
| `bool` | `StopPropagationOnTouchEnd` | `false` | Stop touch end from bubbling |
| `bool` | `StopPropagationOnTouchMove` | `false` | Stop touch move from bubbling |
| `bool` | `StopPropagationOnPointerDown` | `false` | Stop pointer down from bubbling |
| `bool` | `StopPropagationOnPointerUp` | `false` | Stop pointer up from bubbling |
| `bool` | `StopPropagationOnPointerMove` | `false` | Stop pointer move from bubbling |
| `bool` | `StopPropagationOnDrag` | `false` | Stop drag from bubbling |
| `bool` | `StopPropagationOnDragOver` | `false` | Stop dragover from bubbling |
| `bool` | `StopPropagationOnDrop` | `false` | Stop drop from bubbling |

---

## WebGL 2.0 API

### Context & Lifecycle

#### InitializeAsync
Initialize WebGL 2.0 context for a canvas element.
`→ Task`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/HTMLCanvasElement/getContext)

| Type | Name | Description |
|------|------|-------------|
| `string` | `canvasId` | HTML canvas element ID |
| `string` | `baseUri` | The base URI to load the WebGLazor JavaScript library from |

#### MakeCurrent
Set this context as active for subsequent operations.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext)

#### Dispose
Release all WebGL resources and clean up.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WEBGL_lose_context/loseContext)

### Animation Loop

#### StartLoop
Start animation using requestAnimationFrame.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/Window/requestAnimationFrame)

| Type | Name | Description |
|------|------|-------------|
| `Action<double>` | `onFrame` | Callback receiving timestamp in milliseconds |

#### StopLoop
Stop the animation loop.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/Window/cancelAnimationFrame)

### Canvas Utilities

#### GetCanvasBounds
Get the bounding rectangle of the canvas for coordinate conversions.
`→ CanvasBounds?`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/Element/getBoundingClientRect)

### State Management

#### Enable
Enable a WebGL capability.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/enable)

| Type | Name | Description |
|------|------|-------------|
| `int` | `cap` | Capability to enable (See Capability constants) |

#### Disable
Disable a WebGL capability.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/disable)

| Type | Name | Description |
|------|------|-------------|
| `int` | `cap` | Capability to disable (See Capability constants) |

#### ClearColor
Set the color used when clearing the color buffer.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/clearColor)

| Type | Name | Description |
|------|------|-------------|
| `float` | `r` | Red component (0.0-1.0) |
| `float` | `g` | Green component (0.0-1.0) |
| `float` | `b` | Blue component (0.0-1.0) |
| `float` | `a` | Alpha component (0.0-1.0) |

#### ClearDepth
Set the depth value used when clearing depth buffer.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/clearDepth)

| Type | Name | Description |
|------|------|-------------|
| `float` | `depth` | Depth value (0.0-1.0, default 1.0) |

#### Clear
Clear buffers to preset values.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/clear)

| Type | Name | Description |
|------|------|-------------|
| `int` | `mask` | Bitwise OR of buffer bits (See ClearMask constants) |

#### IsEnabled
Check if a WebGL capability is enabled.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/isEnabled)

| Type | Name | Description |
|------|------|-------------|
| `int` | `cap` | Capability to check (See Capability constants) |

#### GetError
Return the error code of the last WebGL operation.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getError)

#### Finish
Block until all WebGL commands are complete.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/finish)

#### Flush
Flush command buffer to GPU without blocking.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/flush)

#### GetParameterInt
Get an integer parameter value.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getParameter)

| Type | Name | Description |
|------|------|-------------|
| `int` | `pname` | Parameter name constant |

#### GetParameterFloat
Get a float parameter value.
`→ float`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getParameter)

| Type | Name | Description |
|------|------|-------------|
| `int` | `pname` | Parameter name constant |

#### GetParameterBool
Get a boolean parameter value.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getParameter)

| Type | Name | Description |
|------|------|-------------|
| `int` | `pname` | Parameter name constant |

#### GetParameterString
Get a string parameter value.
`→ string?`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getParameter)

| Type | Name | Description |
|------|------|-------------|
| `int` | `pname` | Parameter name constant |

#### Viewport
Set the viewport rectangle.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/viewport)

| Type | Name | Description |
|------|------|-------------|
| `int` | `x` | Left edge in pixels |
| `int` | `y` | Bottom edge in pixels |
| `int` | `width` | Width in pixels |
| `int` | `height` | Height in pixels |

#### Scissor
Define the scissor box for clipping.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/scissor)

| Type | Name | Description |
|------|------|-------------|
| `int` | `x` | Left edge in pixels |
| `int` | `y` | Bottom edge in pixels |
| `int` | `width` | Width in pixels |
| `int` | `height` | Height in pixels |

#### DepthFunc
Set the depth comparison function.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/depthFunc)

| Type | Name | Description |
|------|------|-------------|
| `int` | `func` | Comparison function (See DepthFunc constants) |

#### BlendFunc
Set pixel blending factors.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/blendFunc)

| Type | Name | Description |
|------|------|-------------|
| `int` | `sfactor` | Source blend factor (See BlendFactor constants) |
| `int` | `dfactor` | Destination blend factor (See BlendFactor constants) |

#### CullFace
Specify which faces to cull.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/cullFace)

| Type | Name | Description |
|------|------|-------------|
| `int` | `mode` | Face mode (See CullFaceMode constants) |

#### ColorMask
Set which color components are written to the framebuffer.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/colorMask)

| Type | Name | Description |
|------|------|-------------|
| `bool` | `red` | Whether red component is written |
| `bool` | `green` | Whether green component is written |
| `bool` | `blue` | Whether blue component is written |
| `bool` | `alpha` | Whether alpha component is written |

#### DepthMask
Set whether depth buffer is written to.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/depthMask)

| Type | Name | Description |
|------|------|-------------|
| `bool` | `flag` | Whether depth buffer is writable |

#### StencilMask
Set the front and back stencil mask.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/stencilMask)

| Type | Name | Description |
|------|------|-------------|
| `int` | `mask` | Bit mask to enable/disable writing |

#### StencilMaskSeparate
Set the front or back stencil mask.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/stencilMaskSeparate)

| Type | Name | Description |
|------|------|-------------|
| `int` | `face` | FRONT, BACK, or FRONT_AND_BACK |
| `int` | `mask` | Bit mask to enable/disable writing |

#### ClearStencil
Set the stencil value used when clearing stencil buffer.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/clearStencil)

| Type | Name | Description |
|------|------|-------------|
| `int` | `s` | Stencil clear value (default 0) |

#### FrontFace
Specify winding direction for front-facing polygons.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/frontFace)

| Type | Name | Description |
|------|------|-------------|
| `int` | `mode` | CW (clockwise) or CCW (counter-clockwise) |

#### BlendColor
Set the blend color used in blending operations.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/blendColor)

| Type | Name | Description |
|------|------|-------------|
| `float` | `red` | Red component (0.0-1.0) |
| `float` | `green` | Green component (0.0-1.0) |
| `float` | `blue` | Blue component (0.0-1.0) |
| `float` | `alpha` | Alpha component (0.0-1.0) |

#### BlendEquation
Set the blend equation for RGB and alpha.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/blendEquation)

| Type | Name | Description |
|------|------|-------------|
| `int` | `mode` | FUNC_ADD, FUNC_SUBTRACT, FUNC_REVERSE_SUBTRACT, MIN, or MAX |

#### BlendEquationSeparate
Set the blend equation separately for RGB and alpha.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/blendEquationSeparate)

| Type | Name | Description |
|------|------|-------------|
| `int` | `modeRGB` | Blend equation for RGB |
| `int` | `modeAlpha` | Blend equation for alpha |

#### BlendFuncSeparate
Set blending factors separately for RGB and alpha.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/blendFuncSeparate)

| Type | Name | Description |
|------|------|-------------|
| `int` | `srcRGB` | Source RGB factor |
| `int` | `dstRGB` | Destination RGB factor |
| `int` | `srcAlpha` | Source alpha factor |
| `int` | `dstAlpha` | Destination alpha factor |

#### DepthRange
Set the depth range mapping from normalized device coordinates.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/depthRange)

| Type | Name | Description |
|------|------|-------------|
| `float` | `zNear` | Near clipping plane depth (0.0-1.0) |
| `float` | `zFar` | Far clipping plane depth (0.0-1.0) |

#### StencilFunc
Set the stencil test function for front and back faces.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/stencilFunc)

| Type | Name | Description |
|------|------|-------------|
| `int` | `func` | Comparison function (NEVER, LESS, EQUAL, etc.) |
| `int` | `ref` | Reference value for the stencil test |
| `int` | `mask` | Bit mask applied to ref and stencil value |

#### StencilFuncSeparate
Set the stencil test function for front or back faces.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/stencilFuncSeparate)

| Type | Name | Description |
|------|------|-------------|
| `int` | `face` | FRONT, BACK, or FRONT_AND_BACK |
| `int` | `func` | Comparison function |
| `int` | `ref` | Reference value |
| `int` | `mask` | Bit mask |

#### StencilOp
Set the stencil test actions for front and back faces.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/stencilOp)

| Type | Name | Description |
|------|------|-------------|
| `int` | `fail` | Action when stencil test fails |
| `int` | `zfail` | Action when stencil passes but depth fails |
| `int` | `zpass` | Action when both tests pass |

#### StencilOpSeparate
Set the stencil test actions for front or back faces.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/stencilOpSeparate)

| Type | Name | Description |
|------|------|-------------|
| `int` | `face` | FRONT, BACK, or FRONT_AND_BACK |
| `int` | `fail` | Action when stencil test fails |
| `int` | `zfail` | Action when stencil passes but depth fails |
| `int` | `zpass` | Action when both tests pass |

#### LineWidth
Set the width of rasterized lines.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/lineWidth)

| Type | Name | Description |
|------|------|-------------|
| `float` | `width` | Line width in pixels (limited on most implementations) |

#### PolygonOffset
Set the scale and units for polygon offset.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/polygonOffset)

| Type | Name | Description |
|------|------|-------------|
| `float` | `factor` | Depth offset factor |
| `float` | `units` | Constant depth offset |

#### PixelStorei
Set pixel storage parameters.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/pixelStorei)

| Type | Name | Description |
|------|------|-------------|
| `int` | `pname` | Parameter name (See PixelStore constants) |
| `int` | `param` | Parameter value |

#### Hint
Set hints for implementation-specific behavior.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/hint)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Hint target (GENERATE_MIPMAP_HINT, etc.) |
| `int` | `mode` | DONT_CARE, FASTEST, or NICEST |

#### SampleCoverage
Set multisample coverage parameters.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/sampleCoverage)

| Type | Name | Description |
|------|------|-------------|
| `float` | `value` | Coverage value (0.0-1.0) |
| `bool` | `invert` | Whether to invert the coverage mask |

### Shaders

#### CreateShader
Create a shader object.
`→ WebGLShader`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/createShader)

| Type | Name | Description |
|------|------|-------------|
| `int` | `type` | Shader type (See ShaderType constants) |

#### ShaderSource
Set the GLSL source code for a shader.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/shaderSource)

| Type | Name | Description |
|------|------|-------------|
| `WebGLShader` | `shader` | Shader object to set source for |
| `string` | `source` | GLSL source code |

#### CompileShader
Compile a shader object.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/compileShader)

| Type | Name | Description |
|------|------|-------------|
| `WebGLShader` | `shader` | Shader to compile |

#### GetShaderParameterBool
Query shader compile status.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getShaderParameter)

| Type | Name | Description |
|------|------|-------------|
| `WebGLShader` | `shader` | Shader to query |
| `int` | `pname` | Parameter name (See ShaderParameter constants) |

#### GetShaderParameterInt
Query integer shader parameter.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getShaderParameter)

| Type | Name | Description |
|------|------|-------------|
| `WebGLShader` | `shader` | Shader to query |
| `int` | `pname` | Parameter name (See ShaderParameter constants) |

#### GetShaderInfoLog
Get shader compilation log.
`→ string`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getShaderInfoLog)

| Type | Name | Description |
|------|------|-------------|
| `WebGLShader` | `shader` | Shader to get log for |

#### DeleteShader
Delete a shader object.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/deleteShader)

| Type | Name | Description |
|------|------|-------------|
| `WebGLShader` | `shader` | Shader to delete |

#### GetShaderSource
Get the GLSL source code of a shader.
`→ string`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getShaderSource)

| Type | Name | Description |
|------|------|-------------|
| `WebGLShader` | `shader` | Shader to get source for |

#### IsShader
Check if an object is a valid shader.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/isShader)

| Type | Name | Description |
|------|------|-------------|
| `WebGLShader` | `shader` | Object to check |

#### GetShaderPrecisionFormat
Get precision info for a shader type and precision type.
`→ ShaderPrecisionFormat?`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getShaderPrecisionFormat)

| Type | Name | Description |
|------|------|-------------|
| `int` | `shaderType` | VERTEX_SHADER or FRAGMENT_SHADER |
| `int` | `precisionType` | LOW_FLOAT, MEDIUM_FLOAT, HIGH_FLOAT, LOW_INT, MEDIUM_INT, HIGH_INT |

### Programs

#### CreateProgram
Create a program object.
`→ WebGLProgram`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/createProgram)

#### AttachShader
Attach a shader to a program.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/attachShader)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Program to attach to |
| `WebGLShader` | `shader` | Compiled shader to attach |

#### DetachShader
Detach a shader from a program.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/detachShader)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Program to detach from |
| `WebGLShader` | `shader` | Shader to detach |

#### LinkProgram
Link a program object.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/linkProgram)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Program to link |

#### GetProgramParameterBool
Query program link status.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getProgramParameter)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Program to query |
| `int` | `pname` | Parameter name (See ProgramParameter constants) |

#### GetProgramParameterInt
Query integer program parameter.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getProgramParameter)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Program to query |
| `int` | `pname` | Parameter name (See ProgramParameter constants) |

#### GetProgramInfoLog
Get program linking log.
`→ string`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getProgramInfoLog)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Program to get log for |

#### UseProgram
Set the active program for rendering.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/useProgram)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Program to use |

#### DeleteProgram
Delete a program object.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/deleteProgram)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Program to delete |

#### ValidateProgram
Validate a program object.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/validateProgram)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Program to validate |

#### IsProgram
Check if an object is a valid program.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/isProgram)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Object to check |

#### GetFragDataLocation
Get location of a fragment output variable.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/getFragDataLocation)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Linked program |
| `string` | `name` | Output variable name |

#### GetAttachedShaders
Get list of shaders attached to a program.
`→ WebGLShader[]?`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getAttachedShaders)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Program to query |

#### GetActiveAttrib
Get info about an active attribute variable.
`→ ActiveInfo?`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getActiveAttrib)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Linked program |
| `int` | `index` | Attribute index |

#### GetActiveUniform
Get info about an active uniform variable.
`→ ActiveInfo?`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getActiveUniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Linked program |
| `int` | `index` | Uniform index |


### Buffers

#### CreateBuffer
Create a buffer object.
`→ WebGLBuffer`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/createBuffer)

#### BindBuffer
Bind a buffer to a target.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bindBuffer)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Buffer binding target (See BufferTarget constants) |
| `WebGLBuffer?` | `buffer` | Buffer to bind (null to unbind) |

#### BufferData
Create and initialize buffer storage.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bufferData)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Buffer target (See BufferTarget constants) |
| `Span<T>` | `data` | Data to upload |
| `int` | `usage` | Usage hint (See BufferUsage constants) |

#### BufferSubData
Update a subset of buffer data.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bufferSubData)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Buffer target (See BufferTarget constants) |
| `int` | `offset` | Byte offset into buffer |
| `Span<T>` | `data` | Data to upload |

#### DeleteBuffer
Delete a buffer object.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/deleteBuffer)

| Type | Name | Description |
|------|------|-------------|
| `WebGLBuffer` | `buffer` | Buffer to delete |

#### IsBuffer
Check if an object is a valid buffer.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/isBuffer)

| Type | Name | Description |
|------|------|-------------|
| `WebGLBuffer` | `buffer` | Object to check |

#### GetBufferParameterInt
Get an integer buffer parameter value.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getBufferParameter)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Buffer target |
| `int` | `pname` | Parameter name (BUFFER_SIZE, BUFFER_USAGE) |

#### CopyBufferSubData
Copy data between buffer objects (WebGL 2.0).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/copyBufferSubData)

| Type | Name | Description |
|------|------|-------------|
| `int` | `readTarget` | Source buffer target |
| `int` | `writeTarget` | Destination buffer target |
| `int` | `readOffset` | Source byte offset |
| `int` | `writeOffset` | Destination byte offset |
| `int` | `size` | Number of bytes to copy |

#### GetBufferSubData
Read data from a buffer object (WebGL 2.0).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/getBufferSubData)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Buffer target |
| `int` | `srcByteOffset` | Byte offset in buffer |
| `Span<byte>` | `dstData` | Destination array |
| `int` | `dstOffset` | Offset in destination (optional) |
| `int` | `length` | Bytes to read (optional, 0 = all) |

### Vertex Array Objects

#### CreateVertexArray
Create a VAO to store vertex attribute state.
`→ WebGLVertexArray`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/createVertexArray)

#### BindVertexArray
Bind a VAO.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/bindVertexArray)

| Type | Name | Description |
|------|------|-------------|
| `WebGLVertexArray?` | `vao` | VAO to bind (null to unbind) |

#### DeleteVertexArray
Delete a VAO.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/deleteVertexArray)

| Type | Name | Description |
|------|------|-------------|
| `WebGLVertexArray` | `vao` | VAO to delete |

#### IsVertexArray
Check if an object is a valid VAO.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/isVertexArray)

| Type | Name | Description |
|------|------|-------------|
| `WebGLVertexArray` | `vao` | Object to check |

### Vertex Attributes

#### GetAttribLocation
Get attribute location by name.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getAttribLocation)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Linked program |
| `string` | `name` | Attribute name in shader |

#### EnableVertexAttribArray
Enable a vertex attribute array.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/enableVertexAttribArray)

| Type | Name | Description |
|------|------|-------------|
| `int` | `index` | Attribute location |

#### DisableVertexAttribArray
Disable a vertex attribute array.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/disableVertexAttribArray)

| Type | Name | Description |
|------|------|-------------|
| `int` | `index` | Attribute location |

#### BindAttribLocation
Bind an attribute to a specific location before linking.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bindAttribLocation)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Program to modify |
| `int` | `index` | Target attribute index |
| `string` | `name` | Attribute name in shader |

#### VertexAttribPointer
Define vertex attribute layout.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/vertexAttribPointer)

| Type | Name | Description |
|------|------|-------------|
| `int` | `index` | Attribute location |
| `int` | `size` | Components per vertex (1-4) |
| `int` | `type` | Data type (See DataType constants) |
| `bool` | `normalized` | Normalize fixed-point data |
| `int` | `stride` | Byte stride between vertices |
| `int` | `offset` | Byte offset to first component |

#### VertexAttribDivisor
Set attribute divisor for instancing.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/vertexAttribDivisor)

| Type | Name | Description |
|------|------|-------------|
| `int` | `index` | Attribute location |
| `int` | `divisor` | 0=per-vertex, 1=per-instance |

#### VertexAttribIPointer
Define integer vertex attribute layout (WebGL 2.0).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/vertexAttribIPointer)

| Type | Name | Description |
|------|------|-------------|
| `int` | `index` | Attribute location |
| `int` | `size` | Components per vertex (1-4) |
| `int` | `type` | Data type (INT, UNSIGNED_INT, etc.) |
| `int` | `stride` | Byte stride between vertices |
| `int` | `offset` | Byte offset to first component |

### Uniforms

#### GetUniformLocation
Get uniform location by name.
`→ WebGLUniformLocation`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getUniformLocation)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Linked program |
| `string` | `name` | Uniform name in shader |

#### GetUniformInt
Get an integer uniform value.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getUniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Linked program |
| `WebGLUniformLocation` | `location` | Uniform location |

#### GetUniformFloat
Get a float uniform value.
`→ float`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getUniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Linked program |
| `WebGLUniformLocation` | `location` | Uniform location |

#### GetUniformBool
Get a boolean uniform value.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getUniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Linked program |
| `WebGLUniformLocation` | `location` | Uniform location |

#### GetUniformUint
Get a uint uniform value.
`→ uint`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getUniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Linked program |
| `WebGLUniformLocation` | `location` | Uniform location |

#### GetUniformFloatArray
Get a float array uniform value.
`→ float[]?`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getUniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Linked program |
| `WebGLUniformLocation` | `location` | Uniform location |

#### Uniform1f
Set float uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `float` | `x` | Value |

#### Uniform2f
Set vec2 uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `float` | `x` | X component |
| `float` | `y` | Y component |

#### Uniform3f
Set vec3 uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `float` | `x` | X |
| `float` | `y` | Y |
| `float` | `z` | Z |

#### Uniform4f
Set vec4 uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `float` | `x` | X |
| `float` | `y` | Y |
| `float` | `z` | Z |
| `float` | `w` | W |

#### Uniform1i
Set int or sampler uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `int` | `x` | Value or texture unit |

#### UniformMatrix4fv
Set mat4 uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniformMatrix)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `bool` | `transpose` | Transpose the matrix |
| `Span<float>` | `data` | 16 float values in column-major order |

### Uniform Extensions (WebGL 2.0)

#### Uniform2ui
Set uvec2 uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `int` | `x` | X component |
| `int` | `y` | Y component |

#### Uniform3ui
Set uvec3 uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `int` | `x` | X component |
| `int` | `y` | Y component |
| `int` | `z` | Z component |

#### Uniform4ui
Set uvec4 uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `int` | `x` | X component |
| `int` | `y` | Y component |
| `int` | `z` | Z component |
| `int` | `w` | W component |

#### Uniform1iv
Set int array uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `Span<int>` | `data` | Array of int values |

#### Uniform2iv
Set ivec2 array uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `Span<int>` | `data` | Packed array of ivec2 values |

#### Uniform3iv
Set ivec3 array uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `Span<int>` | `data` | Packed array of ivec3 values |

#### Uniform4iv
Set ivec4 array uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `Span<int>` | `data` | Packed array of ivec4 values |

#### Uniform1uiv
Set uint array uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `Span<uint>` | `data` | Array of uint values |

#### Uniform2uiv
Set uvec2 array uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `Span<uint>` | `data` | Packed array of uvec2 values |

#### Uniform3uiv
Set uvec3 array uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `Span<uint>` | `data` | Packed array of uvec3 values |

#### Uniform4uiv
Set uvec4 array uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `Span<uint>` | `data` | Packed array of uvec4 values |


#### UniformMatrix3x2fv
Set mat3x2 uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/uniformMatrix)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `bool` | `transpose` | Transpose the matrix |
| `Span<float>` | `data` | 6 floats in column-major order |

#### UniformMatrix2x4fv
Set mat2x4 uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/uniformMatrix)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `bool` | `transpose` | Transpose the matrix |
| `Span<float>` | `data` | 8 floats in column-major order |

#### UniformMatrix4x2fv
Set mat4x2 uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/uniformMatrix)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `bool` | `transpose` | Transpose the matrix |
| `Span<float>` | `data` | 8 floats in column-major order |

#### UniformMatrix3x4fv
Set mat3x4 uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/uniformMatrix)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `bool` | `transpose` | Transpose the matrix |
| `Span<float>` | `data` | 12 floats in column-major order |

#### UniformMatrix4x3fv
Set mat4x3 uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/uniformMatrix)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `bool` | `transpose` | Transpose the matrix |
| `Span<float>` | `data` | 12 floats in column-major order |

#### Uniform2i
Set ivec2 uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `int` | `x` | X component |
| `int` | `y` | Y component |

#### Uniform3i
Set ivec3 uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `int` | `x` | X |
| `int` | `y` | Y |
| `int` | `z` | Z |

#### Uniform4i
Set ivec4 uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `int` | `x` | X |
| `int` | `y` | Y |
| `int` | `z` | Z |
| `int` | `w` | W |

#### Uniform1ui
Set uint uniform (WebGL 2.0).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `int` | `x` | Value |

#### Uniform1fv
Set float array uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `Span<float>` | `data` | Array of float values |

#### Uniform2fv
Set vec2 array uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `Span<float>` | `data` | Packed array of vec2 values |

#### Uniform3fv
Set vec3 array uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `Span<float>` | `data` | Packed array of vec3 values |

#### Uniform4fv
Set vec4 array uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `Span<float>` | `data` | Packed array of vec4 values |

#### UniformMatrix2fv
Set mat2 uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniformMatrix)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `bool` | `transpose` | Transpose the matrix |
| `Span<float>` | `data` | 4 float values in column-major order |

#### UniformMatrix3fv
Set mat3 uniform.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniformMatrix)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `bool` | `transpose` | Transpose the matrix |
| `Span<float>` | `data` | 9 float values in column-major order |

#### UniformMatrix2x3fv
Set mat2x3 uniform (WebGL 2.0).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/uniformMatrix)

| Type | Name | Description |
|------|------|-------------|
| `WebGLUniformLocation` | `location` | Uniform location |
| `bool` | `transpose` | Transpose the matrix |
| `Span<float>` | `data` | 6 floats in column-major order |


### Drawing

#### DrawArrays
Draw primitives from array data.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/drawArrays)

| Type | Name | Description |
|------|------|-------------|
| `int` | `mode` | Primitive type (See PrimitiveMode constants) |
| `int` | `first` | Starting vertex index |
| `int` | `count` | Number of vertices to draw |

#### DrawElements
Draw indexed primitives.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/drawElements)

| Type | Name | Description |
|------|------|-------------|
| `int` | `mode` | Primitive type (See PrimitiveMode constants) |
| `int` | `count` | Number of indices |
| `int` | `type` | Index type (See DataType constants) |
| `int` | `offset` | Byte offset into index buffer |

#### DrawArraysInstanced
Draw multiple instances from arrays.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/drawArraysInstanced)

| Type | Name | Description |
|------|------|-------------|
| `int` | `mode` | Primitive type (See PrimitiveMode constants) |
| `int` | `first` | Starting vertex |
| `int` | `count` | Vertices per instance |
| `int` | `instanceCount` | Number of instances |

#### DrawElementsInstanced
Draw multiple instances from indexed data (WebGL 2.0).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/drawElementsInstanced)

| Type | Name | Description |
|------|------|-------------|
| `int` | `mode` | Primitive type (See PrimitiveMode constants) |
| `int` | `count` | Number of indices |
| `int` | `type` | Index type (See DataType constants) |
| `int` | `offset` | Byte offset into index buffer |
| `int` | `instanceCount` | Number of instances |

#### DrawBuffers
Specify which color buffers to draw into (WebGL 2.0).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/drawBuffers)

| Type | Name | Description |
|------|------|-------------|
| `int[]` | `buffers` | Array of color attachments (COLOR_ATTACHMENT0, etc.) |


### Textures

#### CreateTexture
Create a texture object.
`→ WebGLTexture`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/createTexture)

#### BindTexture
Bind a texture to a target.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bindTexture)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target (See TextureTarget constants) |
| `WebGLTexture?` | `texture` | Texture to bind (null to unbind) |

#### ActiveTexture
Select the active texture unit.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/activeTexture)

| Type | Name | Description |
|------|------|-------------|
| `int` | `unit` | TEXTURE0 + n |

#### TexImage2D
Specify a 2D texture image.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/texImage2D)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target (See TextureTarget constants) |
| `int` | `level` | Mipmap level |
| `int` | `internalformat` | Internal format (RGBA, RGBA8, etc.) |
| `int` | `width` | Width in pixels |
| `int` | `height` | Height in pixels |
| `int` | `border` | Must be 0 |
| `int` | `format` | Pixel data format |
| `int` | `type` | Pixel data type (See DataType constants) |
| `byte[]?` | `pixels` | Pixel data (null for empty texture) |

#### TexImage2D
Specify a 2D texture image (Zero-Copy).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/texImage2D)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target (See TextureTarget constants) |
| `int` | `level` | Mipmap level |
| `int` | `internalformat` | Internal format |
| `int` | `width` | Width in pixels |
| `int` | `height` | Height in pixels |
| `int` | `border` | Must be 0 |
| `int` | `format` | Pixel data format |
| `int` | `type` | Pixel data type (See DataType constants) |
| `Span<byte>` | `pixels` | Pixel data |

#### TexSubImage2D
Update a sub-rectangle of an existing 2D texture.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/texSubImage2D)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target |
| `int` | `level` | Mipmap level |
| `int` | `xoffset` | X offset |
| `int` | `yoffset` | Y offset |
| `int` | `width` | Width |
| `int` | `height` | Height |
| `int` | `format` | Format |
| `int` | `type` | Type |
| `byte[]?` | `pixels` | Pixel data |

#### TexSubImage2D
Update a sub-rectangle of an existing 2D texture (Zero-Copy).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/texSubImage2D)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target |
| `int` | `level` | Mipmap level |
| `int` | `xoffset` | X offset |
| `int` | `yoffset` | Y offset |
| `int` | `width` | Width |
| `int` | `height` | Height |
| `int` | `format` | Format |
| `int` | `type` | Type |
| `Span<byte>` | `pixels` | Pixel data |

#### TexParameteri
Set texture parameter.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/texParameter)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target (See TextureTarget constants) |
| `int` | `pname` | Parameter name (See TextureParameter constants) |
| `int` | `param` | Parameter value |

#### TexParameterf
Set float texture parameter.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/texParameter)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target (See TextureTarget constants) |
| `int` | `pname` | Parameter name (See TextureParameter constants) |
| `float` | `param` | Parameter value |

#### TexImage3D
Specify a 3D texture image.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/texImage3D)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target (TEXTURE_3D or TEXTURE_2D_ARRAY) |
| `int` | `level` | Mipmap level |
| `int` | `internalformat` | Internal format |
| `int` | `width` | Width in pixels |
| `int` | `height` | Height in pixels |
| `int` | `depth` | Depth in pixels |
| `int` | `border` | Must be 0 |
| `int` | `format` | Pixel data format |
| `int` | `type` | Pixel data type (See DataType constants) |
| `object?` | `pixels` | Pixel data |

#### TexImage2DFromImage
Specify a 2D texture image from an HTML element.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/texImage2D)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target |
| `int` | `level` | Mipmap level |
| `int` | `internalformat` | Internal format |
| `int` | `format` | Pixel data format |
| `int` | `type` | Pixel data type (See DataType constants) |
| `object` | `source` | HTML image/canvas element |

#### CopyTexImage2D
Copy pixels from framebuffer into 2D texture.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/copyTexImage2D)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target |
| `int` | `level` | Mipmap level |
| `int` | `internalformat` | Internal format |
| `int` | `x` | Framebuffer X |
| `int` | `y` | Framebuffer Y |
| `int` | `width` | Width |
| `int` | `height` | Height |
| `int` | `border` | Must be 0 |

#### CopyTexSubImage2D
Copy pixels from framebuffer to 2D texture sub-region.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/copyTexSubImage2D)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target |
| `int` | `level` | Mipmap level |
| `int` | `xoffset` | X offset |
| `int` | `yoffset` | Y offset |
| `int` | `x` | Framebuffer X |
| `int` | `y` | Framebuffer Y |
| `int` | `width` | Width |
| `int` | `height` | Height |

#### CompressedTexImage2D
Specify a compressed 2D texture image.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/compressedTexImage2D)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target |
| `int` | `level` | Mipmap level |
| `int` | `internalformat` | Compressed format |
| `int` | `width` | Width |
| `int` | `height` | Height |
| `int` | `border` | Must be 0 |
| `Span<byte>` | `data` | Compressed image data |

#### CompressedTexSubImage2D
Update a compressed 2D texture sub-region.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/compressedTexSubImage2D)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target |
| `int` | `level` | Mipmap level |
| `int` | `xoffset` | X offset |
| `int` | `yoffset` | Y offset |
| `int` | `width` | Width |
| `int` | `height` | Height |
| `int` | `format` | Compressed format |
| `Span<byte>` | `data` | Compressed image data |

#### CompressedTexImage3D
Specify a compressed 3D texture image.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/compressedTexImage3D)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target |
| `int` | `level` | Mipmap level |
| `int` | `internalformat` | Compressed format |
| `int` | `width` | Width |
| `int` | `height` | Height |
| `int` | `depth` | Depth |
| `int` | `border` | Must be 0 |
| `Span<byte>` | `data` | Compressed image data |

#### CompressedTexSubImage3D
Update a compressed 3D texture sub-region.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/compressedTexSubImage3D)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target |
| `int` | `level` | Mipmap level |
| `int` | `xoffset` | X offset |
| `int` | `yoffset` | Y offset |
| `int` | `zoffset` | Z offset |
| `int` | `width` | Width |
| `int` | `height` | Height |
| `int` | `depth` | Depth |
| `int` | `format` | Compressed format |
| `Span<byte>` | `data` | Compressed image data |

#### GetTexParameterInt
Get an integer texture parameter.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getTexParameter)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target |
| `int` | `pname` | Parameter name |

#### GetTexParameterFloat
Get a float texture parameter.
`→ float`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getTexParameter)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target |
| `int` | `pname` | Parameter name |

#### GenerateMipmap
Generate mipmaps for bound texture.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/generateMipmap)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target (See TextureTarget constants) |

#### DeleteTexture
Delete a texture object.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/deleteTexture)

| Type | Name | Description |
|------|------|-------------|
| `WebGLTexture` | `texture` | Texture to delete |

#### IsTexture
Check if an object is a valid texture.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/isTexture)

| Type | Name | Description |
|------|------|-------------|
| `WebGLTexture` | `texture` | Object to check |

### Framebuffers

#### CreateFramebuffer
Create a framebuffer object.
`→ WebGLFramebuffer`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/createFramebuffer)

#### BindFramebuffer
Bind a framebuffer.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bindFramebuffer)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Framebuffer target (See FramebufferTarget constants) |
| `WebGLFramebuffer?` | `framebuffer` | Framebuffer to bind (null for default) |

#### FramebufferTexture2D
Attach a texture to a framebuffer.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/framebufferTexture2D)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Framebuffer target (See FramebufferTarget constants) |
| `int` | `attachment` | Attachment point (See FramebufferAttachment constants) |
| `int` | `textarget` | Texture target (TEXTURE_2D) (See TextureTarget constants) |
| `WebGLTexture` | `texture` | Texture to attach |
| `int` | `level` | Mipmap level |

#### FramebufferRenderbuffer
Attach a renderbuffer to framebuffer.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/framebufferRenderbuffer)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Framebuffer target (See FramebufferTarget constants) |
| `int` | `attachment` | Attachment point (See FramebufferAttachment constants) |
| `int` | `renderbuffertarget` | RENDERBUFFER |
| `WebGLRenderbuffer` | `renderbuffer` | Renderbuffer to attach |

#### CheckFramebufferStatus
Check framebuffer completeness.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/checkFramebufferStatus)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Framebuffer target (See FramebufferTarget constants) |

#### GetFramebufferAttachmentParameterInt
Get integer attachment parameter.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getFramebufferAttachmentParameter)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Target |
| `int` | `attachment` | Attachment |
| `int` | `pname` | Parameter |

#### DeleteFramebuffer
Delete a framebuffer.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/deleteFramebuffer)

| Type | Name | Description |
|------|------|-------------|
| `WebGLFramebuffer` | `framebuffer` | Framebuffer to delete |

#### IsFramebuffer
Check if an object is a valid framebuffer.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/isFramebuffer)

| Type | Name | Description |
|------|------|-------------|
| `WebGLFramebuffer` | `framebuffer` | Object to check |

#### FramebufferTextureLayer
Attach a layer of a texture to a framebuffer (WebGL 2.0).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/framebufferTextureLayer)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Framebuffer target |
| `int` | `attachment` | Attachment point |
| `WebGLTexture` | `texture` | Texture to attach |
| `int` | `level` | Mipmap level |
| `int` | `layer` | Texture layer |

#### BlitFramebuffer
Transfer a block of pixels between framebuffers (WebGL 2.0).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/blitFramebuffer)

| Type | Name | Description |
|------|------|-------------|
| `int` | `srcX0` | Source X start |
| `int` | `srcY0` | Source Y start |
| `int` | `srcX1` | Source X end |
| `int` | `srcY1` | Source Y end |
| `int` | `dstX0` | Destination X start |
| `int` | `dstY0` | Destination Y start |
| `int` | `dstX1` | Destination X end |
| `int` | `dstY1` | Destination Y end |
| `int` | `mask` | Bitwise OR of buffer bits |
| `int` | `filter` | Filtering mode (NEAREST or LINEAR) |

#### ReadBuffer
Select a color buffer source for reading pixels (WebGL 2.0).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/readBuffer)

| Type | Name | Description |
|------|------|-------------|
| `int` | `src` | Color buffer source |

#### InvalidateFramebuffer
Invalidate the contents of a framebuffer (WebGL 2.0).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/invalidateFramebuffer)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Framebuffer target |
| `int[]` | `attachments` | Attachments to invalidate |

#### InvalidateSubFramebuffer
Invalidate a sub-region of a framebuffer (WebGL 2.0).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/invalidateSubFramebuffer)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Framebuffer target |
| `int[]` | `attachments` | Attachments to invalidate |
| `int` | `x` | X offset |
| `int` | `y` | Y offset |
| `int` | `width` | Width |
| `int` | `height` | Height |

### Renderbuffers

#### CreateRenderbuffer
Create a renderbuffer object.
`→ WebGLRenderbuffer`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/createRenderbuffer)

#### BindRenderbuffer
Bind a renderbuffer.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bindRenderbuffer)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | RENDERBUFFER |
| `WebGLRenderbuffer?` | `renderbuffer` | Renderbuffer to bind |

#### RenderbufferStorage
Allocate renderbuffer storage.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/renderbufferStorage)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | RENDERBUFFER |
| `int` | `internalformat` | DEPTH_COMPONENT24, STENCIL_INDEX8, etc. |
| `int` | `width` | Width in pixels |
| `int` | `height` | Height in pixels |

#### GetRenderbufferParameterInt
Get integer renderbuffer parameter.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getRenderbufferParameter)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | RENDERBUFFER |
| `int` | `pname` | Parameter |

#### DeleteRenderbuffer
Delete a renderbuffer.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/deleteRenderbuffer)

| Type | Name | Description |
|------|------|-------------|
| `WebGLRenderbuffer` | `renderbuffer` | Renderbuffer to delete |

#### IsRenderbuffer
Check if an object is a valid renderbuffer.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/isRenderbuffer)

| Type | Name | Description |
|------|------|-------------|
| `WebGLRenderbuffer` | `renderbuffer` | Object to check |

#### RenderbufferStorageMultisample
Allocate multisample renderbuffer storage (WebGL 2.0).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/renderbufferStorageMultisample)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | RENDERBUFFER |
| `int` | `samples` | Number of samples |
| `int` | `internalformat` | Internal format |
| `int` | `width` | Width in pixels |
| `int` | `height` | Height in pixels |

### Texture Storage (WebGL 2.0)

#### TexStorage2D
Allocate immutable 2D texture storage.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/texStorage2D)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target (See TextureTarget constants) |
| `int` | `levels` | Number of mipmap levels |
| `int` | `internalformat` | Sized internal format (RGBA8, etc.) |
| `int` | `width` | Width in pixels |
| `int` | `height` | Height in pixels |

#### TexStorage3D
Allocate immutable 3D texture storage.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/texStorage3D)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target (See TextureTarget constants) |
| `int` | `levels` | Number of mipmap levels |
| `int` | `internalformat` | Sized internal format |
| `int` | `width` | Width in pixels |
| `int` | `height` | Height in pixels |
| `int` | `depth` | Depth in pixels |

#### TexSubImage3D
Update a portion of a 3D texture.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/texSubImage3D)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target (See TextureTarget constants) |
| `int` | `level` | Mipmap level |
| `int` | `xoffset` | X offset |
| `int` | `yoffset` | Y offset |
| `int` | `zoffset` | Z offset |
| `int` | `width` | Width |
| `int` | `height` | Height |
| `int` | `depth` | Depth |
| `int` | `format` | Pixel format |
| `int` | `type` | Data type (See DataType constants) |

#### CopyTexSubImage3D
Copy framebuffer to 3D texture.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/copyTexSubImage3D)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Texture target (See TextureTarget constants) |
| `int` | `level` | Mipmap level |
| `int` | `xoffset` | X offset |
| `int` | `yoffset` | Y offset |
| `int` | `zoffset` | Z offset |
| `int` | `x` | Framebuffer X |
| `int` | `y` | Framebuffer Y |
| `int` | `width` | Width |
| `int` | `height` | Height |

### Vertex Attribute Extensions

#### VertexAttribI4i
Specify the value of a generic vertex attribute (int).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/vertexAttribI)

| Type | Name | Description |
|------|------|-------------|
| `int` | `index` | Attribute location |
| `int` | `x` | X component |
| `int` | `y` | Y component |
| `int` | `z` | Z component |
| `int` | `w` | W component |

#### VertexAttribI4ui
Specify the value of a generic vertex attribute (uint).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/vertexAttribI)

| Type | Name | Description |
|------|------|-------------|
| `int` | `index` | Attribute location |
| `int` | `x` | X component |
| `int` | `y` | Y component |
| `int` | `z` | Z component |
| `int` | `w` | W component |

#### VertexAttrib1f
Specify the value of a generic vertex attribute (1f).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/vertexAttrib)

| Type | Name | Description |
|------|------|-------------|
| `int` | `index` | Attribute location |
| `float` | `x` | Value |

#### VertexAttrib2f
Specify the value of a generic vertex attribute (2f).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/vertexAttrib)

| Type | Name | Description |
|------|------|-------------|
| `int` | `index` | Attribute location |
| `float` | `x` | X component |
| `float` | `y` | Y component |

#### VertexAttrib3f
Specify the value of a generic vertex attribute (3f).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/vertexAttrib)

| Type | Name | Description |
|------|------|-------------|
| `int` | `index` | Attribute location |
| `float` | `x` | X component |
| `float` | `y` | Y component |
| `float` | `z` | Z component |

#### VertexAttrib4f
Specify the value of a generic vertex attribute (4f).
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/vertexAttrib)

| Type | Name | Description |
|------|------|-------------|
| `int` | `index` | Attribute location |
| `float` | `x` | X component |
| `float` | `y` | Y component |
| `float` | `z` | Z component |
| `float` | `w` | W component |

#### GetVertexAttrib
Get a vertex attribute parameter.
`→ float[]?`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getVertexAttrib)

| Type | Name | Description |
|------|------|-------------|
| `int` | `index` | Attribute location |
| `int` | `pname` | Parameter name |

#### GetVertexAttribInt
Get an integer vertex attribute parameter.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getVertexAttrib)

| Type | Name | Description |
|------|------|-------------|
| `int` | `index` | Attribute location |
| `int` | `pname` | Parameter name |

#### GetVertexAttribFloat
Get a float vertex attribute parameter.
`→ float`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getVertexAttrib)

| Type | Name | Description |
|------|------|-------------|
| `int` | `index` | Attribute location |
| `int` | `pname` | Parameter name |

#### GetVertexAttribBool
Get a boolean vertex attribute parameter.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getVertexAttrib)

| Type | Name | Description |
|------|------|-------------|
| `int` | `index` | Attribute index |
| `int` | `pname` | Parameter name |

#### GetVertexAttribOffset
Get the offset of a vertex attribute.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getVertexAttribOffset)

| Type | Name | Description |
|------|------|-------------|
| `int` | `index` | Attribute location |
| `int` | `pname` | Parameter name |

### Drawing Extensions (WebGL 2.0)


#### DrawRangeElements
Draw indexed primitives in a range.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/drawRangeElements)

| Type | Name | Description |
|------|------|-------------|
| `int` | `mode` | Primitive type (See PrimitiveMode constants) |
| `int` | `start` | Minimum array index |
| `int` | `end` | Maximum array index |
| `int` | `count` | Number of indices |
| `int` | `type` | Index type (See DataType constants) |
| `int` | `offset` | Byte offset into index buffer |

#### ClearBufferfv
Clear a float buffer attachment.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/clearBuffer)

| Type | Name | Description |
|------|------|-------------|
| `int` | `buffer` | Buffer to clear (COLOR) |
| `int` | `drawbuffer` | Draw buffer index |
| `Span<float>` | `values` | Clear values (4 floats) |

#### ClearBufferiv
Clear an integer buffer attachment.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/clearBuffer)

| Type | Name | Description |
|------|------|-------------|
| `int` | `buffer` | Buffer to clear (COLOR) |
| `int` | `drawbuffer` | Draw buffer index |
| `Span<int>` | `values` | Clear values (4 ints) |

#### ClearBufferuiv
Clear an unsigned integer buffer attachment.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/clearBuffer)

| Type | Name | Description |
|------|------|-------------|
| `int` | `buffer` | Buffer to clear (COLOR) |
| `int` | `drawbuffer` | Draw buffer index |
| `Span<uint>` | `values` | Clear values (4 uints) |

#### ClearBufferfi
Clear depth and stencil buffers.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/clearBuffer)

| Type | Name | Description |
|------|------|-------------|
| `int` | `buffer` | DEPTH_STENCIL |
| `int` | `drawbuffer` | Must be 0 |
| `float` | `depth` | Depth clear value |
| `int` | `stencil` | Stencil clear value |


### Query Objects (WebGL 2.0)

#### CreateQuery
Create a query object.
`→ WebGLQuery`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/createQuery)

#### DeleteQuery
Delete a query object.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/deleteQuery)

| Type | Name | Description |
|------|------|-------------|
| `WebGLQuery` | `query` | Query to delete |

#### BeginQuery
Begin an asynchronous query.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/beginQuery)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Query type (ANY_SAMPLES_PASSED, etc.) |
| `WebGLQuery` | `query` | Query object |

#### EndQuery
End an asynchronous query.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/endQuery)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Query type |

#### GetQuery
Get a query object for target.
`→ WebGLQuery?`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/getQuery)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Query target |
| `int` | `pname` | CURRENT_QUERY |

#### GetQueryParameterInt
Get integer query parameter.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/getQueryParameter)

| Type | Name | Description |
|------|------|-------------|
| `WebGLQuery` | `query` | Query object |
| `int` | `pname` | Parameter |

#### GetQueryParameterBool
Get boolean query parameter.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/getQueryParameter)

| Type | Name | Description |
|------|------|-------------|
| `WebGLQuery` | `query` | Query object |
| `int` | `pname` | Parameter |

#### IsQuery
Check if an object is a valid query.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/isQuery)

| Type | Name | Description |
|------|------|-------------|
| `WebGLQuery` | `query` | Object to check |

### Sampler Objects (WebGL 2.0)

#### CreateSampler
Create a sampler object.
`→ WebGLSampler`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/createSampler)

#### BindSampler
Bind a sampler to a texture unit.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/bindSampler)

| Type | Name | Description |
|------|------|-------------|
| `int` | `unit` | Texture unit index |
| `WebGLSampler?` | `sampler` | Sampler to bind (null to unbind) |

#### SamplerParameteri
Set integer sampler parameter.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/samplerParameter)

| Type | Name | Description |
|------|------|-------------|
| `WebGLSampler` | `sampler` | Sampler object |
| `int` | `pname` | Parameter name (See TextureParameter constants) |
| `int` | `param` | Parameter value |

#### GetSamplerParameterInt
Get integer sampler parameter.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/getSamplerParameter)

| Type | Name | Description |
|------|------|-------------|
| `WebGLSampler` | `sampler` | Sampler object |
| `int` | `pname` | Parameter name |

#### GetSamplerParameterFloat
Get float sampler parameter.
`→ float`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/getSamplerParameter)

| Type | Name | Description |
|------|------|-------------|
| `WebGLSampler` | `sampler` | Sampler object |
| `int` | `pname` | Parameter name |

#### DeleteSampler
Delete a sampler object.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/deleteSampler)

| Type | Name | Description |
|------|------|-------------|
| `WebGLSampler` | `sampler` | Sampler to delete |

#### SamplerParameterf
Set float sampler parameter.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/samplerParameter)

| Type | Name | Description |
|------|------|-------------|
| `WebGLSampler` | `sampler` | Sampler object |
| `int` | `pname` | Parameter name (See TextureParameter constants) |
| `float` | `param` | Parameter value |

#### IsSampler
Check if an object is a valid sampler.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/isSampler)

| Type | Name | Description |
|------|------|-------------|
| `WebGLSampler` | `sampler` | Object to check |

### Sync Objects (WebGL 2.0)

#### FenceSync
Create a sync object.
`→ WebGLSync`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/fenceSync)

| Type | Name | Description |
|------|------|-------------|
| `int` | `condition` | SYNC_GPU_COMMANDS_COMPLETE |
| `int` | `flags` | Must be 0 |

#### ClientWaitSync
Block until sync is signaled.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/clientWaitSync)

| Type | Name | Description |
|------|------|-------------|
| `WebGLSync` | `sync` | Sync object |
| `int` | `flags` | SYNC_FLUSH_COMMANDS_BIT or 0 |
| `double` | `timeout` | Timeout in nanoseconds |

#### WaitSync
Server-side wait on sync.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/waitSync)

| Type | Name | Description |
|------|------|-------------|
| `WebGLSync` | `sync` | Sync object |
| `int` | `flags` | Must be 0 |
| `double` | `timeout` | TIMEOUT_IGNORED (-1) |

#### GetSyncParameterInt
Get integer sync parameter.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/getSyncParameter)

| Type | Name | Description |
|------|------|-------------|
| `WebGLSync` | `sync` | Sync object |
| `int` | `pname` | Parameter |

#### DeleteSync
Delete a sync object.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/deleteSync)

| Type | Name | Description |
|------|------|-------------|
| `WebGLSync` | `sync` | Sync to delete |

#### IsSync
Check if an object is a valid sync object.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/isSync)

| Type | Name | Description |
|------|------|-------------|
| `WebGLSync` | `sync` | Object to check |

### Transform Feedback (WebGL 2.0)

#### CreateTransformFeedback
Create a transform feedback object.
`→ WebGLTransformFeedback`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/createTransformFeedback)

#### BindTransformFeedback
Bind a transform feedback object.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/bindTransformFeedback)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | TRANSFORM_FEEDBACK |
| `WebGLTransformFeedback?` | `tf` | Object to bind |

#### BeginTransformFeedback
Begin transform feedback recording.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/beginTransformFeedback)

| Type | Name | Description |
|------|------|-------------|
| `int` | `primitiveMode` | POINTS, LINES, or TRIANGLES (See PrimitiveMode constants) |

#### EndTransformFeedback
End transform feedback recording.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/endTransformFeedback)

#### TransformFeedbackVaryings
Specify varyings to capture.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/transformFeedbackVaryings)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Program to configure |
| `string[]` | `varyings` | Names of output variables |
| `int` | `bufferMode` | SEPARATE_ATTRIBS or INTERLEAVED_ATTRIBS |

#### PauseTransformFeedback
Pause transform feedback.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/pauseTransformFeedback)

#### ResumeTransformFeedback
Resume transform feedback.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/resumeTransformFeedback)

#### DeleteTransformFeedback
Delete a transform feedback object.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/deleteTransformFeedback)

| Type | Name | Description |
|------|------|-------------|
| `WebGLTransformFeedback` | `transformFeedback` | Object to delete |

#### IsTransformFeedback
Check if an object is a valid transform feedback object.
`→ bool`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/isTransformFeedback)

| Type | Name | Description |
|------|------|-------------|
| `WebGLTransformFeedback` | `transformFeedback` | Object to check |

### Uniform Buffer Objects (WebGL 2.0)

#### GetUniformBlockIndex
Get index of a uniform block.
`→ int`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/getUniformBlockIndex)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Linked program |
| `string` | `uniformBlockName` | Block name in shader |

#### UniformBlockBinding
Assign a binding point to uniform block.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/uniformBlockBinding)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Linked program |
| `int` | `uniformBlockIndex` | Block index |
| `int` | `uniformBlockBinding` | Binding point (0-based) |

#### BindBufferBase
Bind buffer to indexed binding point.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/bindBufferBase)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | UNIFORM_BUFFER or TRANSFORM_FEEDBACK_BUFFER (See BufferTarget constants) |
| `int` | `index` | Binding point index |
| `WebGLBuffer` | `buffer` | Buffer to bind |

#### BindBufferRange
Bind buffer range to indexed binding point.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/bindBufferRange)

| Type | Name | Description |
|------|------|-------------|
| `int` | `target` | Buffer target (See BufferTarget constants) |
| `int` | `index` | Binding point index |
| `WebGLBuffer` | `buffer` | Buffer to bind |
| `int` | `offset` | Byte offset |
| `int` | `size` | Byte size |

#### GetUniformIndices
Get indices of uniform variables.
`→ int[]?`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/getUniformIndices)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Linked program |
| `string[]` | `uniformNames` | Array of uniform names |

#### GetActiveUniformBlockName
Get the name of an active uniform block.
`→ string?`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/getActiveUniformBlockName)

| Type | Name | Description |
|------|------|-------------|
| `WebGLProgram` | `program` | Linked program |
| `int` | `blockIndex` | Block index |


### Context & Extensions

#### GetSupportedExtensions
Get list of supported WebGL extensions.
`→ string[]?`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getSupportedExtensions)

### Pixel Operations

#### ReadPixels
Read pixels from framebuffer.
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/readPixels)

| Type | Name | Description |
|------|------|-------------|
| `int` | `x` | X start position |
| `int` | `y` | Y start position |
| `int` | `width` | Width in pixels |
| `int` | `height` | Height in pixels |
| `int` | `format` | Pixel format (RGBA) |
| `int` | `type` | Data type (See DataType constants) |
| `Span<byte>` | `dstData` | Destination buffer |

#### ReadPixelColor
Read a single pixel color.
`→ int[]`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/readPixels)

| Type | Name | Description |
|------|------|-------------|
| `int` | `x` | X position |
| `int` | `y` | Y position |

### Color Spaces (WebGL 2.0)

#### DrawingBufferColorSpace
Get/set the color space of the WebGL drawing buffer.
`→ string`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/drawingBufferColorSpace)

| Type | Name | Description |
|------|------|-------------|
| `string` | `value` | "srgb" (default) or "display-p3" |

#### UnpackColorSpace
Get/set the color space used when importing textures.
`→ string`
[MDN Docs](https://developer.mozilla.org/en-US/docs/Web/API/WebGL2RenderingContext/unpackColorSpace)

| Type | Name | Description |
|------|------|-------------|
| `string` | `value` | "srgb" (default), "display-p3", or "none" |

---

## Constants

### ShaderType

| Name | Value | Description |
|------|-------|-------------|
| `VERTEX_SHADER` | `0x8B31` | Vertex shader stage |
| `FRAGMENT_SHADER` | `0x8B30` | Fragment shader stage |

### BufferTarget

| Name | Value | Description |
|------|-------|-------------|
| `ARRAY_BUFFER` | `0x8892` | Vertex attribute data |
| `ELEMENT_ARRAY_BUFFER` | `0x8893` | Index data |
| `UNIFORM_BUFFER` | `0x8A11` | Uniform block data |
| `TRANSFORM_FEEDBACK_BUFFER` | `0x8C8E` | Transform feedback output |

### BufferUsage

| Name | Value | Description |
|------|-------|-------------|
| `STATIC_DRAW` | `0x88E4` | Data set once, used many times |
| `DYNAMIC_DRAW` | `0x88E8` | Data modified repeatedly, used many times |
| `STREAM_DRAW` | `0x88E0` | Data set once, used few times |

### PrimitiveMode

| Name | Value | Description |
|------|-------|-------------|
| `TRIANGLES` | `0x0004` | Individual triangles (3 vertices each) |
| `TRIANGLE_STRIP` | `0x0005` | Connected triangles sharing edges |
| `TRIANGLE_FAN` | `0x0006` | Triangles sharing a central vertex |
| `LINES` | `0x0001` | Individual line segments |
| `LINE_STRIP` | `0x0003` | Connected line segments |
| `POINTS` | `0x0000` | Individual points |

### DataType

| Name | Value | Description |
|------|-------|-------------|
| `FLOAT` | `0x1406` | 32-bit floating point |
| `UNSIGNED_SHORT` | `0x1403` | 16-bit unsigned integer |
| `UNSIGNED_INT` | `0x1405` | 32-bit unsigned integer |
| `UNSIGNED_BYTE` | `0x1401` | 8-bit unsigned integer |
| `INT` | `0x1404` | 32-bit signed integer |

### Capability

| Name | Value | Description |
|------|-------|-------------|
| `DEPTH_TEST` | `0x0B71` | Depth buffer testing |
| `BLEND` | `0x0BE2` | Alpha blending |
| `CULL_FACE` | `0x0B44` | Face culling |
| `SCISSOR_TEST` | `0x0C11` | Scissor clipping |
| `STENCIL_TEST` | `0x0B90` | Stencil buffer testing |

### DepthFunc

| Name | Value | Description |
|------|-------|-------------|
| `LESS` | `0x0201` | Pass if depth < buffer |
| `LEQUAL` | `0x0203` | Pass if depth <= buffer |
| `GREATER` | `0x0204` | Pass if depth > buffer |
| `GEQUAL` | `0x0206` | Pass if depth >= buffer |
| `EQUAL` | `0x0202` | Pass if depth == buffer |
| `NOTEQUAL` | `0x0205` | Pass if depth != buffer |
| `ALWAYS` | `0x0207` | Always pass |
| `NEVER` | `0x0200` | Never pass |

### BlendFactor

| Name | Value | Description |
|------|-------|-------------|
| `SRC_ALPHA` | `0x0302` | Source alpha |
| `ONE_MINUS_SRC_ALPHA` | `0x0303` | 1 - source alpha |
| `ONE` | `1` | One (full weight) |
| `ZERO` | `0` | Zero (no contribution) |
| `DST_ALPHA` | `0x0304` | Destination alpha |
| `DST_COLOR` | `0x0306` | Destination color |

### CullFaceMode

| Name | Value | Description |
|------|-------|-------------|
| `BACK` | `0x0405` | Cull back-facing polygons |
| `FRONT` | `0x0404` | Cull front-facing polygons |
| `FRONT_AND_BACK` | `0x0408` | Cull both faces |

### ClearMask

| Name | Value | Description |
|------|-------|-------------|
| `COLOR_BUFFER_BIT` | `0x4000` | Clear color buffer |
| `DEPTH_BUFFER_BIT` | `0x0100` | Clear depth buffer |
| `STENCIL_BUFFER_BIT` | `0x0400` | Clear stencil buffer |

### TextureTarget

| Name | Value | Description |
|------|-------|-------------|
| `TEXTURE_2D` | `0x0DE1` | 2D texture |
| `TEXTURE_CUBE_MAP` | `0x8513` | Cube map texture |
| `TEXTURE_3D` | `0x806F` | 3D texture |
| `TEXTURE_2D_ARRAY` | `0x8C1A` | 2D array texture |

### TextureParameter

| Name | Value | Description |
|------|-------|-------------|
| `TEXTURE_MIN_FILTER` | `0x2801` | Minification filter |
| `TEXTURE_MAG_FILTER` | `0x2800` | Magnification filter |
| `TEXTURE_WRAP_S` | `0x2802` | Horizontal wrap mode |
| `TEXTURE_WRAP_T` | `0x2803` | Vertical wrap mode |

### TextureFilter

| Name | Value | Description |
|------|-------|-------------|
| `LINEAR` | `0x2601` | Bilinear interpolation |
| `NEAREST` | `0x2600` | Nearest neighbor sampling |
| `LINEAR_MIPMAP_LINEAR` | `0x2703` | Trilinear filtering |
| `NEAREST_MIPMAP_LINEAR` | `0x2702` | Nearest with mipmap blending |

### FramebufferTarget

| Name | Value | Description |
|------|-------|-------------|
| `FRAMEBUFFER` | `0x8D40` | Read and draw operations |
| `READ_FRAMEBUFFER` | `0x8CA8` | Read operations only |
| `DRAW_FRAMEBUFFER` | `0x8CA9` | Draw operations only |

### FramebufferAttachment

| Name | Value | Description |
|------|-------|-------------|
| `COLOR_ATTACHMENT0` | `0x8CE0` | First color attachment |
| `DEPTH_ATTACHMENT` | `0x8D00` | Depth buffer attachment |
| `STENCIL_ATTACHMENT` | `0x8D20` | Stencil buffer attachment |
| `DEPTH_STENCIL_ATTACHMENT` | `0x821A` | Combined depth+stencil |

### ShaderParameter

| Name | Value | Description |
|------|-------|-------------|
| `COMPILE_STATUS` | `0x8B81` | Compilation success flag |

### ProgramParameter

| Name | Value | Description |
|------|-------|-------------|
| `LINK_STATUS` | `0x8B82` | Link success flag |

### BlendEquation

| Name | Value | Description |
|------|-------|-------------|
| `FUNC_ADD` | `0x8006` | Add source and destination |
| `FUNC_SUBTRACT` | `0x800A` | Subtract destination from source |
| `FUNC_REVERSE_SUBTRACT` | `0x800B` | Subtract source from destination |
| `MIN` | `0x8007` | Minimum of source and destination |
| `MAX` | `0x8008` | Maximum of source and destination |

### FrontFaceMode

| Name | Value | Description |
|------|-------|-------------|
| `CW` | `0x0900` | Clockwise winding |
| `CCW` | `0x0901` | Counter-clockwise winding (default) |

### StencilOp

| Name | Value | Description |
|------|-------|-------------|
| `KEEP` | `0x1E00` | Keep current stencil value |
| `ZERO` | `0` | Set stencil to zero |
| `REPLACE` | `0x1E01` | Set to reference value |
| `INCR` | `0x1E02` | Increment (clamp to max) |
| `DECR` | `0x1E03` | Decrement (clamp to 0) |
| `INVERT` | `0x150A` | Bitwise invert |
| `INCR_WRAP` | `0x8507` | Increment with wrap |
| `DECR_WRAP` | `0x8508` | Decrement with wrap |

### TextureWrap

| Name | Value | Description |
|------|-------|-------------|
| `REPEAT` | `0x2901` | Tile the texture |
| `CLAMP_TO_EDGE` | `0x812F` | Clamp to edge pixels |
| `MIRRORED_REPEAT` | `0x8370` | Mirror and tile |

### PixelFormat

| Name | Value | Description |
|------|-------|-------------|
| `RGBA` | `0x1908` | Red, Green, Blue, Alpha |
| `RGB` | `0x1907` | Red, Green, Blue |
| `ALPHA` | `0x1906` | Alpha only |
| `DEPTH_COMPONENT` | `0x1902` | Depth values |
| `DEPTH_STENCIL` | `0x84F9` | Packed depth + stencil |

### InternalFormat

| Name | Value | Description |
|------|-------|-------------|
| `RGBA8` | `0x8058` | 8-bit RGBA |
| `RGB8` | `0x8051` | 8-bit RGB |
| `RGBA16F` | `0x881A` | 16-bit float RGBA |
| `RGBA32F` | `0x8814` | 32-bit float RGBA |
| `DEPTH_COMPONENT24` | `0x81A6` | 24-bit depth |
| `DEPTH24_STENCIL8` | `0x88F0` | 24-bit depth + 8-bit stencil |

### QueryTarget

| Name | Value | Description |
|------|-------|-------------|
| `ANY_SAMPLES_PASSED` | `0x8C2F` | Occlusion query |
| `ANY_SAMPLES_PASSED_CONSERVATIVE` | `0x8D6A` | Conservative occlusion query |
| `TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN` | `0x8C88` | Primitives written |

### SyncCondition

| Name | Value | Description |
|------|-------|-------------|
| `SYNC_GPU_COMMANDS_COMPLETE` | `0x9117` | GPU commands completed |
| `ALREADY_SIGNALED` | `0x911A` | Sync already signaled |
| `TIMEOUT_EXPIRED` | `0x911B` | Wait timed out |
| `CONDITION_SATISFIED` | `0x911C` | Condition satisfied |
| `WAIT_FAILED` | `0x911D` | Wait failed |

### TransformFeedbackMode

| Name | Value | Description |
|------|-------|-------------|
| `INTERLEAVED_ATTRIBS` | `0x8C8C` | All varyings in one buffer |
| `SEPARATE_ATTRIBS` | `0x8C8D` | Each varying in separate buffer |

### ErrorCode

| Name | Value | Description |
|------|-------|-------------|
| `NO_ERROR` | `0` | No error |
| `INVALID_ENUM` | `0x0500` | Invalid enum value |
| `INVALID_VALUE` | `0x0501` | Invalid value |
| `INVALID_OPERATION` | `0x0502` | Invalid operation |
| `OUT_OF_MEMORY` | `0x0505` | Out of memory |
| `INVALID_FRAMEBUFFER_OPERATION` | `0x0506` | Invalid framebuffer operation |
