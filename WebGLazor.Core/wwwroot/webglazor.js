// WebGLazor - WebGL 2.0 JavaScript Interop for BlazorWebassembly
// High-performance bindings with zero-copy data transfer

const contexts = {};
let nextId = 1;
let currentGL = null;

// ====================
// Context Management
// ====================

export function initWebGL(canvasElement) {
    let canvas = canvasElement;
    if (typeof canvasElement === 'string') {
        canvas = document.getElementById(canvasElement);
    }
    if (!canvas) return 0;

    const gl = canvas.getContext("webgl2", {
        alpha: true,
        depth: true,
        stencil: true,
        antialias: true,
        premultipliedAlpha: true,
        preserveDrawingBuffer: false,
        powerPreference: "high-performance"
    });
    if (!gl) return 0;

    const id = nextId++;
    contexts[id] = gl;
    currentGL = gl;
    return id;
}

export function makeCurrent(id) {
    if (contexts[id]) {
        currentGL = contexts[id];
        return true;
    }
    return false;
}

export function disposeContext(id) {
    // Stop the animation loop if running
    stopLoopForContext(id);

    const gl = contexts[id];
    if (gl) {
        // Lose the WebGL context to free GPU resources
        const loseContext = gl.getExtension('WEBGL_lose_context');
        if (loseContext) {
            loseContext.loseContext();
        }

        // Remove from tracking
        delete contexts[id];

        // Clear current if it was this context
        if (currentGL === gl) {
            currentGL = null;
        }
    }
}

export function getError() { return currentGL?.getError() ?? 0; }
export function getParameter(pname) { return currentGL?.getParameter(pname) ?? null; }
export function getParameterInt(pname) { return currentGL?.getParameter(pname) ?? 0; }
export function getParameterFloat(pname) { return currentGL?.getParameter(pname) ?? 0; }
export function getParameterBool(pname) { return currentGL?.getParameter(pname) ?? false; }
export function getParameterString(pname) { return currentGL?.getParameter(pname) ?? null; }
export function finish() { currentGL?.finish(); }
export function flush() { currentGL?.flush(); }

export function getCanvasBounds(canvasId) {
    const canvas = document.getElementById(canvasId);
    if (!canvas) return null;
    const rect = canvas.getBoundingClientRect();
    return { x: rect.left, y: rect.top, width: rect.width, height: rect.height };
}

export function getCanvasBoundsForContext(contextId) {
    const gl = contexts[contextId];
    if (!gl) return null;
    const canvas = gl.canvas;
    if (!canvas) return null;
    const rect = canvas.getBoundingClientRect();
    return [rect.left, rect.top, rect.width, rect.height];
}


// ====================
// State Management
// ====================

export function enable(cap) { currentGL.enable(cap); }
export function disable(cap) { currentGL.disable(cap); }
export function isEnabled(cap) { return currentGL.isEnabled(cap); }
export function clearColor(r, g, b, a) { currentGL.clearColor(r, g, b, a); }
export function clearDepth(depth) { currentGL.clearDepth(depth); }
export function clearStencil(s) { currentGL.clearStencil(s); }
export function clear(mask) { currentGL.clear(mask); }
export function viewport(x, y, width, height) { currentGL.viewport(x, y, width, height); }
export function scissor(x, y, width, height) { currentGL.scissor(x, y, width, height); }
export function colorMask(red, green, blue, alpha) { currentGL.colorMask(red, green, blue, alpha); }
export function depthMask(flag) { currentGL.depthMask(flag); }
export function stencilMask(mask) { currentGL.stencilMask(mask); }
export function stencilMaskSeparate(face, mask) { currentGL.stencilMaskSeparate(face, mask); }

// ====================
// Blending
// ====================

export function blendColor(red, green, blue, alpha) { currentGL.blendColor(red, green, blue, alpha); }
export function blendEquation(mode) { currentGL.blendEquation(mode); }
export function blendEquationSeparate(modeRGB, modeAlpha) { currentGL.blendEquationSeparate(modeRGB, modeAlpha); }
export function blendFunc(sfactor, dfactor) { currentGL.blendFunc(sfactor, dfactor); }
export function blendFuncSeparate(srcRGB, dstRGB, srcAlpha, dstAlpha) { currentGL.blendFuncSeparate(srcRGB, dstRGB, srcAlpha, dstAlpha); }

// ====================
// Depth and Stencil
// ====================

export function depthFunc(func) { currentGL.depthFunc(func); }
export function depthRange(zNear, zFar) { currentGL.depthRange(zNear, zFar); }
export function stencilFunc(func, ref, mask) { currentGL.stencilFunc(func, ref, mask); }
export function stencilFuncSeparate(face, func, ref, mask) { currentGL.stencilFuncSeparate(face, func, ref, mask); }
export function stencilOp(fail, zfail, zpass) { currentGL.stencilOp(fail, zfail, zpass); }
export function stencilOpSeparate(face, fail, zfail, zpass) { currentGL.stencilOpSeparate(face, fail, zfail, zpass); }

// ====================
// Face Culling
// ====================

export function cullFace(mode) { currentGL.cullFace(mode); }
export function frontFace(mode) { currentGL.frontFace(mode); }

// ====================
// Polygon and Line
// ====================

export function lineWidth(width) { currentGL.lineWidth(width); }
export function polygonOffset(factor, units) { currentGL.polygonOffset(factor, units); }

// ====================
// Pixel Storage
// ====================

export function pixelStorei(pname, param) { currentGL.pixelStorei(pname, param); }
export function hint(target, mode) { currentGL.hint(target, mode); }
export function sampleCoverage(value, invert) { currentGL.sampleCoverage(value, invert); }

// ====================
// Shaders
// ====================

export function createShader(type) { return currentGL.createShader(type); }
export function shaderSource(shader, source) { currentGL.shaderSource(shader, source); }
export function compileShader(shader) { currentGL.compileShader(shader); }
export function getShaderParameter(shader, pname) { return currentGL.getShaderParameter(shader, pname); }
export function getShaderParameterInt(shader, pname) { return currentGL.getShaderParameter(shader, pname) ?? 0; }
export function getShaderParameterBool(shader, pname) { return currentGL.getShaderParameter(shader, pname) ?? false; }
export function getShaderInfoLog(shader) { return currentGL.getShaderInfoLog(shader) || ""; }
export function getShaderSource(shader) { return currentGL.getShaderSource(shader) || ""; }
export function deleteShader(shader) { currentGL.deleteShader(shader); }
export function isShader(shader) { return currentGL.isShader(shader); }

// ====================
// Programs
// ====================

export function createProgram() { return currentGL.createProgram(); }
export function attachShader(program, shader) { currentGL.attachShader(program, shader); }
export function detachShader(program, shader) { currentGL.detachShader(program, shader); }
export function linkProgram(program) { currentGL.linkProgram(program); }
export function validateProgram(program) { currentGL.validateProgram(program); }
export function getProgramParameter(program, pname) { return currentGL.getProgramParameter(program, pname); }
export function getProgramParameterInt(program, pname) { return currentGL.getProgramParameter(program, pname) ?? 0; }
export function getProgramParameterBool(program, pname) { return currentGL.getProgramParameter(program, pname) ?? false; }
export function getProgramInfoLog(program) { return currentGL.getProgramInfoLog(program) || ""; }
export function useProgram(program) { currentGL.useProgram(program); }
export function deleteProgram(program) { currentGL.deleteProgram(program); }
export function isProgram(program) { return currentGL.isProgram(program); }

// ====================
// Attributes
// ====================

export function getAttribLocation(program, name) { return currentGL.getAttribLocation(program, name); }
export function bindAttribLocation(program, index, name) { currentGL.bindAttribLocation(program, index, name); }
export function enableVertexAttribArray(index) { currentGL.enableVertexAttribArray(index); }
export function disableVertexAttribArray(index) { currentGL.disableVertexAttribArray(index); }
export function vertexAttribPointer(index, size, type, normalized, stride, offset) { currentGL.vertexAttribPointer(index, size, type, normalized, stride, offset); }
export function vertexAttribIPointer(index, size, type, stride, offset) { currentGL.vertexAttribIPointer(index, size, type, stride, offset); }
export function vertexAttribDivisor(index, divisor) { currentGL.vertexAttribDivisor(index, divisor); }
export function getVertexAttrib(index, pname) { return currentGL.getVertexAttrib(index, pname); }
export function getVertexAttribInt(index, pname) { return currentGL.getVertexAttrib(index, pname) ?? 0; }
export function getVertexAttribFloat(index, pname) { return currentGL.getVertexAttrib(index, pname) ?? 0; }
export function getVertexAttribBool(index, pname) { return currentGL.getVertexAttrib(index, pname) ?? false; }

export function getVertexAttribArray(index, pname) {
    const val = currentGL.getVertexAttrib(index, pname);
    if (val && (val instanceof Float32Array || val instanceof Int32Array || val instanceof Uint32Array || Array.isArray(val))) {
        return Array.from(val);
    }
    // Return single values as 1-element array if requested as array
    if (typeof val === 'number' || typeof val === 'boolean') {
        return [Number(val)];
    }
    return null;
}

// ====================
// Buffers
// ====================

export function createBuffer() { return currentGL.createBuffer(); }
export function bindBuffer(target, buffer) { currentGL.bindBuffer(target, buffer); }
export function bindBufferBase(target, index, buffer) { currentGL.bindBufferBase(target, index, buffer); }
export function bindBufferRange(target, index, buffer, offset, size) { currentGL.bindBufferRange(target, index, buffer, offset, size); }

export function bufferData(target, dataPtr, size, usage) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const dataView = new Uint8Array(heapU8.buffer, dataPtr, size);
    currentGL.bufferData(target, dataView, usage);
}

export function bufferSubData(target, offset, dataPtr, size) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const dataView = new Uint8Array(heapU8.buffer, dataPtr, size);
    currentGL.bufferSubData(target, offset, dataView);
}

export function copyBufferSubData(readTarget, writeTarget, readOffset, writeOffset, size) {
    currentGL.copyBufferSubData(readTarget, writeTarget, readOffset, writeOffset, size);
}

export function getBufferParameter(target, pname) { return currentGL.getBufferParameter(target, pname); }
export function getBufferParameterInt(target, pname) { return currentGL.getBufferParameter(target, pname) ?? 0; }
export function deleteBuffer(buffer) { currentGL.deleteBuffer(buffer); }
export function isBuffer(buffer) { return currentGL.isBuffer(buffer); }

// ====================
// Vertex Array Objects (WebGL 2.0)
// ====================

export function createVertexArray() { return currentGL.createVertexArray(); }
export function bindVertexArray(vertexArray) { currentGL.bindVertexArray(vertexArray); }
export function deleteVertexArray(vertexArray) { currentGL.deleteVertexArray(vertexArray); }
export function isVertexArray(vertexArray) { return currentGL.isVertexArray(vertexArray); }

// ====================
// Uniforms
// ====================

export function getUniform(program, location) { return currentGL.getUniform(program, location); }
export function getUniformInt(program, location) { return currentGL.getUniform(program, location) ?? 0; }
export function getUniformFloat(program, location) { return currentGL.getUniform(program, location) ?? 0; }
export function getUniformBool(program, location) { return currentGL.getUniform(program, location) ?? false; }
export function getUniformUint(program, location) { return currentGL.getUniform(program, location) ?? 0; }

export function getUniformArray(program, location) {
    const val = currentGL.getUniform(program, location);
    if (!val) return null;
    return (val instanceof Float32Array || val instanceof Int32Array || val instanceof Uint32Array || Array.isArray(val))
        ? Array.from(val)
        : [Number(val)];
}
export function getUniformLocation(program, name) { return currentGL.getUniformLocation(program, name); }
export function getUniformBlockIndex(program, uniformBlockName) { return currentGL.getUniformBlockIndex(program, uniformBlockName); }
export function uniformBlockBinding(program, uniformBlockIndex, uniformBlockBinding) { currentGL.uniformBlockBinding(program, uniformBlockIndex, uniformBlockBinding); }

export function uniform1f(location, x) { currentGL.uniform1f(location, x); }
export function uniform2f(location, x, y) { currentGL.uniform2f(location, x, y); }
export function uniform3f(location, x, y, z) { currentGL.uniform3f(location, x, y, z); }
export function uniform4f(location, x, y, z, w) { currentGL.uniform4f(location, x, y, z, w); }
export function uniform1i(location, x) { currentGL.uniform1i(location, x); }
export function uniform2i(location, x, y) { currentGL.uniform2i(location, x, y); }
export function uniform3i(location, x, y, z) { currentGL.uniform3i(location, x, y, z); }
export function uniform4i(location, x, y, z, w) { currentGL.uniform4i(location, x, y, z, w); }
export function uniform1ui(location, x) { currentGL.uniform1ui(location, x); }

export function uniformMatrix2fv(location, transpose, dataPtr) {
    const heapF32 = getHeapF32();
    if (!heapF32) { console.error("WebGLazor: Could not find WASM HEAPF32"); return; }
    const offset = dataPtr / 4;
    const dataView = heapF32.subarray(offset, offset + 4);
    currentGL.uniformMatrix2fv(location, transpose, dataView);
}

export function uniformMatrix3fv(location, transpose, dataPtr) {
    const heapF32 = getHeapF32();
    if (!heapF32) { console.error("WebGLazor: Could not find WASM HEAPF32"); return; }
    const offset = dataPtr / 4;
    const dataView = heapF32.subarray(offset, offset + 9);
    currentGL.uniformMatrix3fv(location, transpose, dataView);
}

export function uniformMatrix4fv(location, transpose, dataPtr) {
    const heapF32 = getHeapF32();
    if (!heapF32) { console.error("WebGLazor: Could not find WASM HEAPF32"); return; }
    const offset = dataPtr / 4;
    const dataView = heapF32.subarray(offset, offset + 16);
    currentGL.uniformMatrix4fv(location, transpose, dataView);
}

// ====================
// Drawing
// ====================

export function drawArrays(mode, first, count) { currentGL.drawArrays(mode, first, count); }
export function drawElements(mode, count, type, offset) { currentGL.drawElements(mode, count, type, offset); }
export function drawArraysInstanced(mode, first, count, instanceCount) { currentGL.drawArraysInstanced(mode, first, count, instanceCount); }
export function drawElementsInstanced(mode, count, type, offset, instanceCount) { currentGL.drawElementsInstanced(mode, count, type, offset, instanceCount); }
export function drawBuffers(buffers) { currentGL.drawBuffers(buffers); }

// ====================
// Textures
// ====================

export function createTexture() { return currentGL.createTexture(); }
export function bindTexture(target, texture) { currentGL.bindTexture(target, texture); }
export function activeTexture(texture) { currentGL.activeTexture(texture); }
export function texImage2D(target, level, internalformat, width, height, border, format, type, pixels) {
    currentGL.texImage2D(target, level, internalformat, width, height, border, format, type, pixels);
}

export function texImage2D_Ptr(target, level, internalformat, width, height, border, format, type, dataPtr, dataLength) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const dataView = new Uint8Array(heapU8.buffer, dataPtr, dataLength);
    currentGL.texImage2D(target, level, internalformat, width, height, border, format, type, dataView);
}
export function texImage2DFromImage(target, level, internalformat, format, type, source) {
    currentGL.texImage2D(target, level, internalformat, format, type, source);
}
export function texSubImage2D(target, level, xoffset, yoffset, width, height, format, type, pixels) {
    currentGL.texSubImage2D(target, level, xoffset, yoffset, width, height, format, type, pixels);
}

export function texSubImage2D_Ptr(target, level, xoffset, yoffset, width, height, format, type, dataPtr, dataLength) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const dataView = new Uint8Array(heapU8.buffer, dataPtr, dataLength);
    currentGL.texSubImage2D(target, level, xoffset, yoffset, width, height, format, type, dataView);
}
export function texImage3D(target, level, internalformat, width, height, depth, border, format, type, pixels) {
    currentGL.texImage3D(target, level, internalformat, width, height, depth, border, format, type, pixels);
}
export function texParameteri(target, pname, param) { currentGL.texParameteri(target, pname, param); }
export function texParameterf(target, pname, param) { currentGL.texParameterf(target, pname, param); }
export function generateMipmap(target) { currentGL.generateMipmap(target); }
export function deleteTexture(texture) { currentGL.deleteTexture(texture); }
export function isTexture(texture) { return currentGL.isTexture(texture); }

// ====================
// Framebuffers
// ====================

export function createFramebuffer() { return currentGL.createFramebuffer(); }
export function bindFramebuffer(target, framebuffer) { currentGL.bindFramebuffer(target, framebuffer); }
export function framebufferTexture2D(target, attachment, textarget, texture, level) { currentGL.framebufferTexture2D(target, attachment, textarget, texture, level); }
export function framebufferTextureLayer(target, attachment, texture, level, layer) { currentGL.framebufferTextureLayer(target, attachment, texture, level, layer); }
export function framebufferRenderbuffer(target, attachment, renderbuffertarget, renderbuffer) { currentGL.framebufferRenderbuffer(target, attachment, renderbuffertarget, renderbuffer); }
export function checkFramebufferStatus(target) { return currentGL.checkFramebufferStatus(target); }
export function deleteFramebuffer(framebuffer) { currentGL.deleteFramebuffer(framebuffer); }
export function isFramebuffer(framebuffer) { return currentGL.isFramebuffer(framebuffer); }
export function blitFramebuffer(srcX0, srcY0, srcX1, srcY1, dstX0, dstY0, dstX1, dstY1, mask, filter) {
    currentGL.blitFramebuffer(srcX0, srcY0, srcX1, srcY1, dstX0, dstY0, dstX1, dstY1, mask, filter);
}
export function readBuffer(src) { currentGL.readBuffer(src); }

export function readPixelColor(x, y) {
    const pixels = new Uint8Array(4);
    currentGL.readPixels(x, y, 1, 1, currentGL.RGBA, currentGL.UNSIGNED_BYTE, pixels);
    return Array.from(pixels);
}

// ====================
// Renderbuffers
// ====================

export function createRenderbuffer() { return currentGL.createRenderbuffer(); }
export function bindRenderbuffer(target, renderbuffer) { currentGL.bindRenderbuffer(target, renderbuffer); }
export function renderbufferStorage(target, internalformat, width, height) { currentGL.renderbufferStorage(target, internalformat, width, height); }
export function renderbufferStorageMultisample(target, samples, internalformat, width, height) { currentGL.renderbufferStorageMultisample(target, samples, internalformat, width, height); }
export function deleteRenderbuffer(renderbuffer) { currentGL.deleteRenderbuffer(renderbuffer); }
export function isRenderbuffer(renderbuffer) { return currentGL.isRenderbuffer(renderbuffer); }

// ====================
// Samplers (WebGL 2.0)
// ====================

export function createSampler() { return currentGL.createSampler(); }
export function bindSampler(unit, sampler) { currentGL.bindSampler(unit, sampler); }
export function samplerParameteri(sampler, pname, param) { currentGL.samplerParameteri(sampler, pname, param); }
export function samplerParameterf(sampler, pname, param) { currentGL.samplerParameterf(sampler, pname, param); }
export function deleteSampler(sampler) { currentGL.deleteSampler(sampler); }
export function isSampler(sampler) { return currentGL.isSampler(sampler); }

// ====================
// Queries (WebGL 2.0)
// ====================

export function createQuery() { return currentGL.createQuery(); }
export function beginQuery(target, query) { currentGL.beginQuery(target, query); }
export function endQuery(target) { currentGL.endQuery(target); }
export function getQueryParameter(query, pname) { return currentGL.getQueryParameter(query, pname); }
export function getQueryParameterInt(query, pname) { return currentGL.getQueryParameter(query, pname) ?? 0; }
export function getQueryParameterBool(query, pname) { return currentGL.getQueryParameter(query, pname) ?? false; }
export function deleteQuery(query) { currentGL.deleteQuery(query); }
export function isQuery(query) { return currentGL.isQuery(query); }

// ====================
// Sync Objects (WebGL 2.0)
// ====================

export function fenceSync(condition, flags) { return currentGL.fenceSync(condition, flags); }
export function clientWaitSync(sync, flags, timeout) { return currentGL.clientWaitSync(sync, flags, timeout); }
export function deleteSync(sync) { currentGL.deleteSync(sync); }
export function isSync(sync) { return currentGL.isSync(sync); }
export function getSyncParameter(sync, pname) { return currentGL.getSyncParameter(sync, pname); }
export function getSyncParameterInt(sync, pname) { return currentGL.getSyncParameter(sync, pname) ?? 0; }

// ====================
// Transform Feedback (WebGL 2.0)
// ====================

export function createTransformFeedback() { return currentGL.createTransformFeedback(); }
export function bindTransformFeedback(target, transformFeedback) { currentGL.bindTransformFeedback(target, transformFeedback); }
export function beginTransformFeedback(primitiveMode) { currentGL.beginTransformFeedback(primitiveMode); }
export function endTransformFeedback() { currentGL.endTransformFeedback(); }
export function pauseTransformFeedback() { currentGL.pauseTransformFeedback(); }
export function resumeTransformFeedback() { currentGL.resumeTransformFeedback(); }
export function transformFeedbackVaryings(program, varyings, bufferMode) { currentGL.transformFeedbackVaryings(program, varyings, bufferMode); }
export function deleteTransformFeedback(transformFeedback) { currentGL.deleteTransformFeedback(transformFeedback); }
export function isTransformFeedback(transformFeedback) { return currentGL.isTransformFeedback(transformFeedback); }



// ====================
// Buffer Extensions (WebGL 2.0)
// ====================

export function getBufferSubData(target, srcByteOffset, dstDataPtr, dstDataLength, dstOffset, length) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const dstData = new Uint8Array(heapU8.buffer, dstDataPtr, dstDataLength);
    currentGL.getBufferSubData(target, srcByteOffset, dstData, dstOffset, length);
}

// ====================
// Framebuffer Extensions (WebGL 2.0)
// ====================

export function invalidateFramebuffer(target, attachments) { currentGL.invalidateFramebuffer(target, attachments); }
export function invalidateSubFramebuffer(target, attachments, x, y, width, height) { currentGL.invalidateSubFramebuffer(target, attachments, x, y, width, height); }
export function getFramebufferAttachmentParameter(target, attachment, pname) { return currentGL.getFramebufferAttachmentParameter(target, attachment, pname); }
export function getFramebufferAttachmentParameterInt(target, attachment, pname) { return currentGL.getFramebufferAttachmentParameter(target, attachment, pname) ?? 0; }

// ====================
// Renderbuffer Extensions
// ====================


export function getRenderbufferParameter(target, pname) { return currentGL.getRenderbufferParameter(target, pname); }
export function getRenderbufferParameterInt(target, pname) { return currentGL.getRenderbufferParameter(target, pname) ?? 0; }

// ====================
// Texture Extensions (WebGL 2.0)
// ====================

export function texStorage2D(target, levels, internalformat, width, height) { currentGL.texStorage2D(target, levels, internalformat, width, height); }
export function texStorage3D(target, levels, internalformat, width, height, depth) { currentGL.texStorage3D(target, levels, internalformat, width, height, depth); }
export function texSubImage3D(target, level, xoffset, yoffset, zoffset, width, height, depth, format, type, pixels) {
    currentGL.texSubImage3D(target, level, xoffset, yoffset, zoffset, width, height, depth, format, type, pixels);
}
export function copyTexSubImage3D(target, level, xoffset, yoffset, zoffset, x, y, width, height) {
    currentGL.copyTexSubImage3D(target, level, xoffset, yoffset, zoffset, x, y, width, height);
}
export function compressedTexImage2D(target, level, internalformat, width, height, border, dataPtr, dataLength) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const dataView = new Uint8Array(heapU8.buffer, dataPtr, dataLength);
    currentGL.compressedTexImage2D(target, level, internalformat, width, height, border, dataView);
}
export function compressedTexSubImage2D(target, level, xoffset, yoffset, width, height, format, dataPtr, dataLength) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const dataView = new Uint8Array(heapU8.buffer, dataPtr, dataLength);
    currentGL.compressedTexSubImage2D(target, level, xoffset, yoffset, width, height, format, dataView);
}
export function compressedTexImage3D(target, level, internalformat, width, height, depth, border, dataPtr, dataLength) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const dataView = new Uint8Array(heapU8.buffer, dataPtr, dataLength);
    currentGL.compressedTexImage3D(target, level, internalformat, width, height, depth, border, dataView);
}
export function compressedTexSubImage3D(target, level, xoffset, yoffset, zoffset, width, height, depth, format, dataPtr, dataLength) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const dataView = new Uint8Array(heapU8.buffer, dataPtr, dataLength);
    currentGL.compressedTexSubImage3D(target, level, xoffset, yoffset, zoffset, width, height, depth, format, dataView);
}
export function copyTexImage2D(target, level, internalformat, x, y, width, height, border) {
    currentGL.copyTexImage2D(target, level, internalformat, x, y, width, height, border);
}
export function copyTexSubImage2D(target, level, xoffset, yoffset, x, y, width, height) {
    currentGL.copyTexSubImage2D(target, level, xoffset, yoffset, x, y, width, height);
}
export function getTexParameter(target, pname) { return currentGL.getTexParameter(target, pname); }
export function getTexParameterInt(target, pname) { return currentGL.getTexParameter(target, pname) ?? 0; }
export function getTexParameterFloat(target, pname) { return currentGL.getTexParameter(target, pname) ?? 0; }

// ====================
// Program/Shader Extensions
// ====================

export function getFragDataLocation(program, name) { return currentGL.getFragDataLocation(program, name); }
export function getAttachedShaders(program) { return currentGL.getAttachedShaders(program); }
export function getShaderPrecisionFormat(shaderType, precisionType) {
    const format = currentGL.getShaderPrecisionFormat(shaderType, precisionType);
    if (!format) return null;
    return { rangeMin: format.rangeMin, rangeMax: format.rangeMax, precision: format.precision };
}
export function getActiveAttrib(program, index) {
    const info = currentGL.getActiveAttrib(program, index);
    if (!info) return null;
    return { name: info.name, size: info.size, type: info.type };
}
export function getActiveUniform(program, index) {
    const info = currentGL.getActiveUniform(program, index);
    if (!info) return null;
    return { name: info.name, size: info.size, type: info.type };
}


// Returns [rangeMin, rangeMax, precision] as int array, or null
export function getShaderPrecisionFormatAsArray(shaderType, precisionType) {
    const format = currentGL.getShaderPrecisionFormat(shaderType, precisionType);
    if (!format) return null;
    return [format.rangeMin, format.rangeMax, format.precision];
}

// Returns [name, size, type] as mixed array, or null
export function getActiveAttribInfo(program, index) {
    const info = currentGL.getActiveAttrib(program, index);
    if (!info) return null;
    return [info.name, info.size, info.type];
}

// Returns [name, size, type] as mixed array, or null
export function getActiveUniformInfo(program, index) {
    const info = currentGL.getActiveUniform(program, index);
    if (!info) return null;
    return [info.name, info.size, info.type];
}

// ====================
// Uniform Extensions (WebGL 2.0)
// ====================

export function uniform2ui(location, x, y) { currentGL.uniform2ui(location, x, y); }
export function uniform3ui(location, x, y, z) { currentGL.uniform3ui(location, x, y, z); }
export function uniform4ui(location, x, y, z, w) { currentGL.uniform4ui(location, x, y, z, w); }

export function uniform1fv(location, dataPtr, count) {
    const heapF32 = getHeapF32();
    if (!heapF32) { console.error("WebGLazor: Could not find WASM HEAPF32"); return; }
    const offset = dataPtr / 4;
    const dataView = heapF32.subarray(offset, offset + count);
    currentGL.uniform1fv(location, dataView);
}
export function uniform2fv(location, dataPtr, count) {
    const heapF32 = getHeapF32();
    if (!heapF32) { console.error("WebGLazor: Could not find WASM HEAPF32"); return; }
    const offset = dataPtr / 4;
    const dataView = heapF32.subarray(offset, offset + count * 2);
    currentGL.uniform2fv(location, dataView);
}
export function uniform3fv(location, dataPtr, count) {
    const heapF32 = getHeapF32();
    if (!heapF32) { console.error("WebGLazor: Could not find WASM HEAPF32"); return; }
    const offset = dataPtr / 4;
    const dataView = heapF32.subarray(offset, offset + count * 3);
    currentGL.uniform3fv(location, dataView);
}
export function uniform4fv(location, dataPtr, count) {
    const heapF32 = getHeapF32();
    if (!heapF32) { console.error("WebGLazor: Could not find WASM HEAPF32"); return; }
    const offset = dataPtr / 4;
    const dataView = heapF32.subarray(offset, offset + count * 4);
    currentGL.uniform4fv(location, dataView);
}

export function uniform1iv(location, dataPtr, count) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const dataView = new Int32Array(heapU8.buffer, dataPtr, count);
    currentGL.uniform1iv(location, dataView);
}
export function uniform2iv(location, dataPtr, count) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const dataView = new Int32Array(heapU8.buffer, dataPtr, count * 2);
    currentGL.uniform2iv(location, dataView);
}
export function uniform3iv(location, dataPtr, count) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const dataView = new Int32Array(heapU8.buffer, dataPtr, count * 3);
    currentGL.uniform3iv(location, dataView);
}
export function uniform4iv(location, dataPtr, count) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const dataView = new Int32Array(heapU8.buffer, dataPtr, count * 4);
    currentGL.uniform4iv(location, dataView);
}

export function uniform1uiv(location, dataPtr, count) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const dataView = new Uint32Array(heapU8.buffer, dataPtr, count);
    currentGL.uniform1uiv(location, dataView);
}
export function uniform2uiv(location, dataPtr, count) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const dataView = new Uint32Array(heapU8.buffer, dataPtr, count * 2);
    currentGL.uniform2uiv(location, dataView);
}
export function uniform3uiv(location, dataPtr, count) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const dataView = new Uint32Array(heapU8.buffer, dataPtr, count * 3);
    currentGL.uniform3uiv(location, dataView);
}
export function uniform4uiv(location, dataPtr, count) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const dataView = new Uint32Array(heapU8.buffer, dataPtr, count * 4);
    currentGL.uniform4uiv(location, dataView);
}

// Non-square matrix uniforms
export function uniformMatrix2x3fv(location, transpose, dataPtr) {
    const heapF32 = getHeapF32();
    if (!heapF32) { console.error("WebGLazor: Could not find WASM HEAPF32"); return; }
    const offset = dataPtr / 4;
    const dataView = heapF32.subarray(offset, offset + 6);
    currentGL.uniformMatrix2x3fv(location, transpose, dataView);
}
export function uniformMatrix3x2fv(location, transpose, dataPtr) {
    const heapF32 = getHeapF32();
    if (!heapF32) { console.error("WebGLazor: Could not find WASM HEAPF32"); return; }
    const offset = dataPtr / 4;
    const dataView = heapF32.subarray(offset, offset + 6);
    currentGL.uniformMatrix3x2fv(location, transpose, dataView);
}
export function uniformMatrix2x4fv(location, transpose, dataPtr) {
    const heapF32 = getHeapF32();
    if (!heapF32) { console.error("WebGLazor: Could not find WASM HEAPF32"); return; }
    const offset = dataPtr / 4;
    const dataView = heapF32.subarray(offset, offset + 8);
    currentGL.uniformMatrix2x4fv(location, transpose, dataView);
}
export function uniformMatrix4x2fv(location, transpose, dataPtr) {
    const heapF32 = getHeapF32();
    if (!heapF32) { console.error("WebGLazor: Could not find WASM HEAPF32"); return; }
    const offset = dataPtr / 4;
    const dataView = heapF32.subarray(offset, offset + 8);
    currentGL.uniformMatrix4x2fv(location, transpose, dataView);
}
export function uniformMatrix3x4fv(location, transpose, dataPtr) {
    const heapF32 = getHeapF32();
    if (!heapF32) { console.error("WebGLazor: Could not find WASM HEAPF32"); return; }
    const offset = dataPtr / 4;
    const dataView = heapF32.subarray(offset, offset + 12);
    currentGL.uniformMatrix3x4fv(location, transpose, dataView);
}
export function uniformMatrix4x3fv(location, transpose, dataPtr) {
    const heapF32 = getHeapF32();
    if (!heapF32) { console.error("WebGLazor: Could not find WASM HEAPF32"); return; }
    const offset = dataPtr / 4;
    const dataView = heapF32.subarray(offset, offset + 12);
    currentGL.uniformMatrix4x3fv(location, transpose, dataView);
}

// ====================
// Vertex Attribute Extensions
// ====================

export function vertexAttribI4i(index, x, y, z, w) { currentGL.vertexAttribI4i(index, x, y, z, w); }
export function vertexAttribI4ui(index, x, y, z, w) { currentGL.vertexAttribI4ui(index, x, y, z, w); }
export function vertexAttrib1f(index, x) { currentGL.vertexAttrib1f(index, x); }
export function vertexAttrib2f(index, x, y) { currentGL.vertexAttrib2f(index, x, y); }
export function vertexAttrib3f(index, x, y, z) { currentGL.vertexAttrib3f(index, x, y, z); }
export function vertexAttrib4f(index, x, y, z, w) { currentGL.vertexAttrib4f(index, x, y, z, w); }
export function getVertexAttribOffset(index, pname) { return currentGL.getVertexAttribOffset(index, pname); }

// ====================
// Drawing Extensions (WebGL 2.0)
// ====================

export function drawRangeElements(mode, start, end, count, type, offset) {
    currentGL.drawRangeElements(mode, start, end, count, type, offset);
}

export function clearBufferfv(buffer, drawbuffer, valuesPtr) {
    const heapF32 = getHeapF32();
    if (!heapF32) { console.error("WebGLazor: Could not find WASM HEAPF32"); return; }
    const offset = valuesPtr / 4;
    const values = heapF32.subarray(offset, offset + 4);
    currentGL.clearBufferfv(buffer, drawbuffer, values);
}
export function clearBufferiv(buffer, drawbuffer, valuesPtr) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const values = new Int32Array(heapU8.buffer, valuesPtr, 4);
    currentGL.clearBufferiv(buffer, drawbuffer, values);
}
export function clearBufferuiv(buffer, drawbuffer, valuesPtr) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const values = new Uint32Array(heapU8.buffer, valuesPtr, 4);
    currentGL.clearBufferuiv(buffer, drawbuffer, values);
}
export function clearBufferfi(buffer, drawbuffer, depth, stencil) {
    currentGL.clearBufferfi(buffer, drawbuffer, depth, stencil);
}

// ====================
// Query Extensions (WebGL 2.0)
// ====================

export function getQuery(target, pname) { return currentGL.getQuery(target, pname); }

// ====================
// Sampler Extensions (WebGL 2.0)
// ====================

export function getSamplerParameter(sampler, pname) { return currentGL.getSamplerParameter(sampler, pname); }
export function getSamplerParameterInt(sampler, pname) { return currentGL.getSamplerParameter(sampler, pname) ?? 0; }
export function getSamplerParameterFloat(sampler, pname) { return currentGL.getSamplerParameter(sampler, pname) ?? 0.0; }

// ====================
// Sync Extensions (WebGL 2.0)
// ====================

export function waitSync(sync, flags, timeout) { currentGL.waitSync(sync, flags, timeout); }

// ====================
// Uniform Buffer Object Extensions (WebGL 2.0)
export function getUniformIndices(program, uniformNames) { return currentGL.getUniformIndices(program, uniformNames); }

export function getActiveUniformBlockName(program, blockIndex) { return currentGL.getActiveUniformBlockName(program, blockIndex); }

// ====================
// Context Extensions
// ====================

export function getSupportedExtensions() { return currentGL.getSupportedExtensions(); }

// ====================
// Pixel Operations
// ====================

export function readPixels(x, y, width, height, format, type, dstDataPtr, dstDataLength) {
    const heapU8 = getHeapU8();
    if (!heapU8) { console.error("WebGLazor: Could not find WASM HEAPU8"); return; }
    const pixels = new Uint8Array(heapU8.buffer, dstDataPtr, dstDataLength);
    currentGL.readPixels(x, y, width, height, format, type, pixels);
}

// ====================
// Color Space Properties (WebGL 2.0)
// ====================

export function getDrawingBufferColorSpace() { return currentGL.drawingBufferColorSpace; }
export function setDrawingBufferColorSpace(value) { currentGL.drawingBufferColorSpace = value; }
export function getUnpackColorSpace() { return currentGL.unpackColorSpace; }
export function setUnpackColorSpace(value) { currentGL.unpackColorSpace = value; }

// ====================
// Animation Loop
// ====================

const contextLoops = new Map(); // contextId -> loopId
let dotnetExports = null;

async function ensureDotnetExports() {
    if (!dotnetExports && globalThis.getDotnetRuntime) {
        const runtime = globalThis.getDotnetRuntime(0);
        dotnetExports = await runtime.getAssemblyExports("WebGLazor.Core");
    }
    return dotnetExports;
}

export async function startLoopForContext(contextId) {
    if (contextLoops.has(contextId)) return;

    const exports = await ensureDotnetExports();
    if (!exports?.WebGLazor?.WebGLContext) {
        console.error("WebGLazor: Could not find WebGLContext exports.");
        return;
    }

    const onFrame = exports.WebGLazor.WebGLContext.OnFrameStatic;

    function step(timestamp) {
        makeCurrent(contextId);
        onFrame(contextId, timestamp);
        if (contextLoops.has(contextId)) {
            contextLoops.set(contextId, requestAnimationFrame(step));
        }
    }
    contextLoops.set(contextId, requestAnimationFrame(step));
}


export function stopLoopForContext(contextId) {
    const loopId = contextLoops.get(contextId);
    if (loopId) {
        cancelAnimationFrame(loopId);
        contextLoops.delete(contextId);
    }
}

// ====================
// Helper Functions
// ====================

function getHeapU8() {
    if (globalThis.Blazor?.runtime?.Module?.HEAPU8) return globalThis.Blazor.runtime.Module.HEAPU8;
    if (globalThis.Blazor?.runtime?.HEAPU8) return globalThis.Blazor.runtime.HEAPU8;
    if (globalThis.Module?.HEAPU8) return globalThis.Module.HEAPU8;
    if (globalThis.getDotnetRuntime) {
        try { return globalThis.getDotnetRuntime(0).INTERNAL.heapU8; } catch (e) { }
    }
    return null;
}

function getHeapF32() {
    if (globalThis.Blazor?.runtime?.Module?.HEAPF32) return globalThis.Blazor.runtime.Module.HEAPF32;
    if (globalThis.Blazor?.runtime?.HEAPF32) return globalThis.Blazor.runtime.HEAPF32;
    if (globalThis.Module?.HEAPF32) return globalThis.Module.HEAPF32;
    if (globalThis.getDotnetRuntime) {
        try {
            const heapU8 = globalThis.getDotnetRuntime(0).INTERNAL.heapU8;
            return new Float32Array(heapU8.buffer);
        } catch (e) { }
    }
    return null;
}
