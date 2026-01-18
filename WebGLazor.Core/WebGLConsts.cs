namespace WebGLazor;

public static class WebGLConsts
{
    // Clear buffer masks
    public const int COLOR_BUFFER_BIT = 0x00004000;
    public const int DEPTH_BUFFER_BIT = 0x00000100;
    public const int STENCIL_BUFFER_BIT = 0x00000400;

    // Shader types
    public const int FRAGMENT_SHADER = 0x8B30;
    public const int VERTEX_SHADER = 0x8B31;

    // Shader/Program parameters
    public const int COMPILE_STATUS = 0x8B81;
    public const int LINK_STATUS = 0x8B82;
    public const int VALIDATE_STATUS = 0x8B83;
    public const int DELETE_STATUS = 0x8B80;
    public const int SHADER_TYPE = 0x8B4F;
    public const int ATTACHED_SHADERS = 0x8B85;
    public const int ACTIVE_UNIFORMS = 0x8B86;
    public const int ACTIVE_ATTRIBUTES = 0x8B89;

    // Buffer targets
    public const int ARRAY_BUFFER = 0x8892;
    public const int ELEMENT_ARRAY_BUFFER = 0x8893;
    public const int COPY_READ_BUFFER = 0x8F36;
    public const int COPY_WRITE_BUFFER = 0x8F37;
    public const int TRANSFORM_FEEDBACK_BUFFER = 0x8C8E;
    public const int UNIFORM_BUFFER = 0x8A11;
    public const int PIXEL_PACK_BUFFER = 0x88EB;
    public const int PIXEL_UNPACK_BUFFER = 0x88EC;

    // Buffer usage
    public const int STATIC_DRAW = 0x88E4;
    public const int DYNAMIC_DRAW = 0x88E8;
    public const int STREAM_DRAW = 0x88E0;
    public const int STATIC_READ = 0x88E5;
    public const int DYNAMIC_READ = 0x88E9;
    public const int STREAM_READ = 0x88E1;
    public const int STATIC_COPY = 0x88E6;
    public const int DYNAMIC_COPY = 0x88EA;
    public const int STREAM_COPY = 0x88E2;

    // Data types
    public const int BYTE = 0x1400;
    public const int UNSIGNED_BYTE = 0x1401;
    public const int SHORT = 0x1402;
    public const int UNSIGNED_SHORT = 0x1403;
    public const int INT = 0x1404;
    public const int UNSIGNED_INT = 0x1405;
    public const int FLOAT = 0x1406;
    public const int HALF_FLOAT = 0x140B;

    // Primitive types
    public const int POINTS = 0x0000;
    public const int LINES = 0x0001;
    public const int LINE_LOOP = 0x0002;
    public const int LINE_STRIP = 0x0003;
    public const int TRIANGLES = 0x0004;
    public const int TRIANGLE_STRIP = 0x0005;
    public const int TRIANGLE_FAN = 0x0006;

    // Blending modes
    public const int ZERO = 0;
    public const int ONE = 1;
    public const int SRC_COLOR = 0x0300;
    public const int ONE_MINUS_SRC_COLOR = 0x0301;
    public const int SRC_ALPHA = 0x0302;
    public const int ONE_MINUS_SRC_ALPHA = 0x0303;
    public const int DST_ALPHA = 0x0304;
    public const int ONE_MINUS_DST_ALPHA = 0x0305;
    public const int DST_COLOR = 0x0306;
    public const int ONE_MINUS_DST_COLOR = 0x0307;
    public const int SRC_ALPHA_SATURATE = 0x0308;
    public const int CONSTANT_COLOR = 0x8001;
    public const int ONE_MINUS_CONSTANT_COLOR = 0x8002;
    public const int CONSTANT_ALPHA = 0x8003;
    public const int ONE_MINUS_CONSTANT_ALPHA = 0x8004;

    // Blend equations
    public const int FUNC_ADD = 0x8006;
    public const int FUNC_SUBTRACT = 0x800A;
    public const int FUNC_REVERSE_SUBTRACT = 0x800B;
    public const int MIN = 0x8007;
    public const int MAX = 0x8008;

    // Capability flags
    public const int BLEND = 0x0BE2;
    public const int CULL_FACE = 0x0B44;
    public const int DEPTH_TEST = 0x0B71;
    public const int DITHER = 0x0BD0;
    public const int POLYGON_OFFSET_FILL = 0x8037;
    public const int SAMPLE_ALPHA_TO_COVERAGE = 0x809E;
    public const int SAMPLE_COVERAGE = 0x80A0;
    public const int SCISSOR_TEST = 0x0C11;
    public const int STENCIL_TEST = 0x0B90;
    public const int RASTERIZER_DISCARD = 0x8C89;

    // Cull face modes
    public const int FRONT = 0x0404;
    public const int BACK = 0x0405;
    public const int FRONT_AND_BACK = 0x0408;

    // Front face direction
    public const int CW = 0x0900;
    public const int CCW = 0x0901;

    // Depth functions
    public const int NEVER = 0x0200;
    public const int LESS = 0x0201;
    public const int EQUAL = 0x0202;
    public const int LEQUAL = 0x0203;
    public const int GREATER = 0x0204;
    public const int NOTEQUAL = 0x0205;
    public const int GEQUAL = 0x0206;
    public const int ALWAYS = 0x0207;

    // Stencil operations
    public const int KEEP = 0x1E00;
    public const int REPLACE = 0x1E01;
    public const int INCR = 0x1E02;
    public const int DECR = 0x1E03;
    public const int INVERT = 0x150A;
    public const int INCR_WRAP = 0x8507;
    public const int DECR_WRAP = 0x8508;

    // Texture targets
    public const int TEXTURE_2D = 0x0DE1;
    public const int TEXTURE_3D = 0x806F;
    public const int TEXTURE_CUBE_MAP = 0x8513;
    public const int TEXTURE_2D_ARRAY = 0x8C1A;
    public const int TEXTURE_CUBE_MAP_POSITIVE_X = 0x8515;
    public const int TEXTURE_CUBE_MAP_NEGATIVE_X = 0x8516;
    public const int TEXTURE_CUBE_MAP_POSITIVE_Y = 0x8517;
    public const int TEXTURE_CUBE_MAP_NEGATIVE_Y = 0x8518;
    public const int TEXTURE_CUBE_MAP_POSITIVE_Z = 0x8519;
    public const int TEXTURE_CUBE_MAP_NEGATIVE_Z = 0x851A;

    // Texture parameters
    public const int TEXTURE_MIN_FILTER = 0x2801;
    public const int TEXTURE_MAG_FILTER = 0x2800;
    public const int TEXTURE_WRAP_S = 0x2802;
    public const int TEXTURE_WRAP_T = 0x2803;
    public const int TEXTURE_WRAP_R = 0x8072;
    public const int TEXTURE_BASE_LEVEL = 0x813C;
    public const int TEXTURE_MAX_LEVEL = 0x813D;
    public const int TEXTURE_COMPARE_MODE = 0x884C;
    public const int TEXTURE_COMPARE_FUNC = 0x884D;

    // Texture filter modes
    public const int NEAREST = 0x2600;
    public const int LINEAR = 0x2601;
    public const int NEAREST_MIPMAP_NEAREST = 0x2700;
    public const int LINEAR_MIPMAP_NEAREST = 0x2701;
    public const int NEAREST_MIPMAP_LINEAR = 0x2702;
    public const int LINEAR_MIPMAP_LINEAR = 0x2703;

    // Texture wrap modes
    public const int REPEAT = 0x2901;
    public const int CLAMP_TO_EDGE = 0x812F;
    public const int MIRRORED_REPEAT = 0x8370;

    // Pixel formats
    public const int ALPHA = 0x1906;
    public const int RGB = 0x1907;
    public const int RGBA = 0x1908;
    public const int LUMINANCE = 0x1909;
    public const int LUMINANCE_ALPHA = 0x190A;
    public const int DEPTH_COMPONENT = 0x1902;
    public const int DEPTH_STENCIL = 0x84F9;
    public const int RED = 0x1903;
    public const int RG = 0x8227;
    public const int RED_INTEGER = 0x8D94;
    public const int RG_INTEGER = 0x8228;
    public const int RGB_INTEGER = 0x8D98;
    public const int RGBA_INTEGER = 0x8D99;

    // Internal formats (sized)
    public const int R8 = 0x8229;
    public const int R16F = 0x822D;
    public const int R32F = 0x822E;
    public const int R8UI = 0x8232;
    public const int RG8 = 0x822B;
    public const int RG16F = 0x822F;
    public const int RG32F = 0x8230;
    public const int RGB8 = 0x8051;
    public const int RGBA8 = 0x8058;
    public const int RGB16F = 0x881B;
    public const int RGBA16F = 0x881A;
    public const int RGB32F = 0x8815;
    public const int RGBA32F = 0x8814;
    public const int DEPTH_COMPONENT16 = 0x81A5;
    public const int DEPTH_COMPONENT24 = 0x81A6;
    public const int DEPTH_COMPONENT32F = 0x8CAC;
    public const int DEPTH24_STENCIL8 = 0x88F0;
    public const int DEPTH32F_STENCIL8 = 0x8CAD;

    // Framebuffer targets and attachments
    public const int FRAMEBUFFER = 0x8D40;
    public const int READ_FRAMEBUFFER = 0x8CA8;
    public const int DRAW_FRAMEBUFFER = 0x8CA9;
    public const int COLOR_ATTACHMENT0 = 0x8CE0;
    public const int COLOR_ATTACHMENT1 = 0x8CE1;
    public const int COLOR_ATTACHMENT2 = 0x8CE2;
    public const int COLOR_ATTACHMENT3 = 0x8CE3;
    public const int COLOR_ATTACHMENT4 = 0x8CE4;
    public const int COLOR_ATTACHMENT5 = 0x8CE5;
    public const int COLOR_ATTACHMENT6 = 0x8CE6;
    public const int COLOR_ATTACHMENT7 = 0x8CE7;
    public const int DEPTH_ATTACHMENT = 0x8D00;
    public const int STENCIL_ATTACHMENT = 0x8D20;
    public const int DEPTH_STENCIL_ATTACHMENT = 0x821A;

    // Framebuffer status
    public const int FRAMEBUFFER_COMPLETE = 0x8CD5;
    public const int FRAMEBUFFER_INCOMPLETE_ATTACHMENT = 0x8CD6;
    public const int FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT = 0x8CD7;
    public const int FRAMEBUFFER_INCOMPLETE_DIMENSIONS = 0x8CD9;
    public const int FRAMEBUFFER_UNSUPPORTED = 0x8CDD;
    public const int FRAMEBUFFER_INCOMPLETE_MULTISAMPLE = 0x8D56;

    // Renderbuffer
    public const int RENDERBUFFER = 0x8D41;

    // Texture units
    public const int TEXTURE0 = 0x84C0;
    public const int TEXTURE1 = 0x84C1;
    public const int TEXTURE2 = 0x84C2;
    public const int TEXTURE3 = 0x84C3;
    public const int TEXTURE4 = 0x84C4;
    public const int TEXTURE5 = 0x84C5;
    public const int TEXTURE6 = 0x84C6;
    public const int TEXTURE7 = 0x84C7;
    public const int MAX_TEXTURE_IMAGE_UNITS = 0x8872;
    public const int MAX_COMBINED_TEXTURE_IMAGE_UNITS = 0x8B4D;

    // Query targets (WebGL 2.0)
    public const int ANY_SAMPLES_PASSED = 0x8C2F;
    public const int ANY_SAMPLES_PASSED_CONSERVATIVE = 0x8D6A;
    public const int TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN = 0x8C88;

    // Query parameters
    public const int QUERY_RESULT = 0x8866;
    public const int QUERY_RESULT_AVAILABLE = 0x8867;

    // Sync objects (WebGL 2.0)
    public const int SYNC_GPU_COMMANDS_COMPLETE = 0x9117;
    public const int SYNC_STATUS = 0x9114;
    public const int SYNC_CONDITION = 0x9113;
    public const int SIGNALED = 0x9119;
    public const int UNSIGNALED = 0x9118;
    public const int ALREADY_SIGNALED = 0x911A;
    public const int TIMEOUT_EXPIRED = 0x911B;
    public const int CONDITION_SATISFIED = 0x911C;
    public const int WAIT_FAILED = 0x911D;
    public const int SYNC_FLUSH_COMMANDS_BIT = 0x00000001;

    // Transform feedback (WebGL 2.0)
    public const int TRANSFORM_FEEDBACK = 0x8E22;
    public const int INTERLEAVED_ATTRIBS = 0x8C8C;
    public const int SEPARATE_ATTRIBS = 0x8C8D;
    public const int TRANSFORM_FEEDBACK_PAUSED = 0x8E23;
    public const int TRANSFORM_FEEDBACK_ACTIVE = 0x8E24;

    // Uniform buffer (WebGL 2.0)
    public const int UNIFORM_BUFFER_BINDING = 0x8A28;
    public const int UNIFORM_BLOCK_BINDING = 0x8A3F;
    public const int UNIFORM_BLOCK_DATA_SIZE = 0x8A40;
    public const int UNIFORM_BLOCK_ACTIVE_UNIFORMS = 0x8A42;

    // Sampler parameters
    public const int COMPARE_REF_TO_TEXTURE = 0x884E;

    // Pixel store parameters
    public const int UNPACK_FLIP_Y_WEBGL = 0x9240;
    public const int UNPACK_PREMULTIPLY_ALPHA_WEBGL = 0x9241;
    public const int UNPACK_COLORSPACE_CONVERSION_WEBGL = 0x9243;
    public const int UNPACK_ALIGNMENT = 0x0CF5;
    public const int PACK_ALIGNMENT = 0x0D05;
    public const int UNPACK_ROW_LENGTH = 0x0CF2;
    public const int UNPACK_SKIP_ROWS = 0x0CF3;
    public const int UNPACK_SKIP_PIXELS = 0x0CF4;
    public const int PACK_ROW_LENGTH = 0x0D02;
    public const int PACK_SKIP_ROWS = 0x0D03;
    public const int PACK_SKIP_PIXELS = 0x0D04;

    // Hints
    public const int DONT_CARE = 0x1100;
    public const int FASTEST = 0x1101;
    public const int NICEST = 0x1102;
    public const int GENERATE_MIPMAP_HINT = 0x8192;
    public const int FRAGMENT_SHADER_DERIVATIVE_HINT = 0x8B8B;

    // Error codes
    public const int NO_ERROR = 0;
    public const int INVALID_ENUM = 0x0500;
    public const int INVALID_VALUE = 0x0501;
    public const int INVALID_OPERATION = 0x0502;
    public const int OUT_OF_MEMORY = 0x0505;
    public const int INVALID_FRAMEBUFFER_OPERATION = 0x0506;

    // Vertex array object (WebGL 2.0)
    public const int VERTEX_ARRAY_BINDING = 0x85B5;

    // Draw buffers (WebGL 2.0)
    public const int DRAW_BUFFER0 = 0x8825;
    public const int DRAW_BUFFER1 = 0x8826;
    public const int DRAW_BUFFER2 = 0x8827;
    public const int DRAW_BUFFER3 = 0x8828;
    public const int MAX_DRAW_BUFFERS = 0x8824;
    public const int MAX_COLOR_ATTACHMENTS = 0x8CDF;

    // Instanced rendering (WebGL 2.0)
    public const int VERTEX_ATTRIB_ARRAY_DIVISOR = 0x88FE;
}
