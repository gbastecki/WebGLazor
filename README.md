# WebGLazor
![https://github.com/github/docs/actions/workflows/main.yml](https://github.com/gbastecki/WebGLazor/actions/workflows/build.yml/badge.svg)
[![GitHub](https://img.shields.io/github/license/gbastecki/WebGLazor?color=0000a4&style=plastic)](https://github.com/gbastecki/WebGLazor/blob/main/LICENSE)

**WebGLazor** is a high-performance, low-level WebGL 2.0 binding library for Blazor WebAssembly. 
It provides a direct, zero-copy interface to the pure WebGL 2.0 API, enabling developers to build hardware-accelerated 2D and 3D graphics applications completely in C#.

---

## Table of Contents

- [Key Features](#key-features)
    - [Maximum Performance with Zero-Copy](#maximum-performance-with-zero-copy)
    - [Full WebGL 2.0 API Coverage](#full-webgl-20-api-coverage)
    - [Developer Friendly](#developer-friendly)
    - [AOT & Trimming](#aot--trimming)
- [API Reference](#api-reference)
- [Package requirements](#package-requirements)
- [Installation](#installation)
- [Quick Start](#quick-start)
- [License](#license)
- [Third-Party Libraries](#third-party-libraries)

---

## Key Features

### Maximum Performance with Zero-Copy
WebGLazor is engineered for speed. It utilizes direct memory access to the WebAssembly heap, allowing C# `Span<T>` and arrays to be passed directly to WebGL without the overhead of data marshaling or serialization.
- **Direct Memory Access:** Uses `pinned` memory pointers for buffer and texture operations.
- **Zero Allocations:** Rendering loops are designed to run with zero garbage collection pressure.
- **Fast Interop:** Optimized internal mappings bypass the standard Blazor JS interop overhead for critical paths.

### Full WebGL 2.0 API Coverage
WebGLazor exposes the **entire** WebGL 2.0 standard.
- **Advanced Rendering:** VAOs, Uniform Buffer Objects (UBOs), Transform Feedback, and Multi-Render Targets (MRT).
- **Textures:** Support for 3D textures, 2D Array textures, and immutable texture storage.
- **State Management:** Complete control over Samplers, Sync Objects, and Query objects.
- **Shaders:** Full GLSL shader support with strict typing for shader sources and precision formats.

### Developer Friendly
- **Strong Typing:** All WebGL constants and enums are strongly typed to prevent invalid state usage.
- **Blazor Component:** Includes the `<WebGLCanvas>` component for instant setup.
- **Context Management:** Built-in support for multiple WebGL contexts and automatic context loss handling.

### AOT & Trimming
- WebGLazor is AOT-compatible and trimming safe, ensuring optimal performance in production builds.

---

## API Reference
For a complete and detailed reference of all available classes, methods, and constants, please read the **[API Reference Wiki](WIKI.MD)**.

- **[WebGLCanvas Component](WIKI.MD#webglcanvas-component)**: Setup and event handling.
- **[WebGL 2.0 Implementation](WIKI.MD#webgl-20-api)**: Detailed method signatures and usage.
- **[Constants](WIKI.MD#constants)**: Enumeration of all WebGL 2.0 constants.

---

## Package requirements
This package uses [`Microsoft.AspNetCore.Components.Web`](https://www.nuget.org/packages/Microsoft.AspNetCore.Components.Web/) package:

| .NET Version | Required Package |
|--------------|------------------|
| .NET 10.0 | `Microsoft.AspNetCore.Components.Web` ≥ 10.0.0 |

---

## Installation
Install the package via NuGet:
```shell
dotnet add package WebGLazor
```

Add package reference
```xml
<PackageReference Include="WebGLazor" Version="*" />
```

## Quick Start

1.  **Add the Namespace:**
    Add `using WebGLazor;` to your `_Imports.razor`.

2.  **Use the Component:**
    Add the canvas to your page and handle the context creation.
    ```razor
    <WebGLCanvas Width="800" Height="600" OnContextCreated="OnContextCreated" />

    @code {
        private WebGLContext _gl;

        private void OnContextCreated(WebGLContext gl)
        {
            _gl = gl;
            
            // Set clear color to black, fully opaque
            _gl.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            
            // Clear the color buffer with specified clear color
            _gl.Clear(WebGLConsts.COLOR_BUFFER_BIT);
        }
    }
    ```
 3. **Constants:**
    Use WebGL constants from `WebGLConsts` class, e.g., `WebGLConsts.COLOR_BUFFER_BIT`.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Third-Party Libraries

See the [NOTICE](NOTICE) file for attribution information.
