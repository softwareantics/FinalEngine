namespace FinalEngine.Example;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FinalEngine.Input.Keyboards;
using FinalEngine.Input.Mouses;
using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ErrorCode = OpenTK.Graphics.OpenGL4.ErrorCode;
using MouseButton = Input.Mouses.MouseButton;
using Vector2 = System.Numerics.Vector2;

public class ImGuiController : IDisposable
{
    private static bool KHRDebugAvailable = false;

    private readonly List<char> PressedChars = [];

    private int _fontTexture;

    private bool _frameBegun;

    private int _indexBuffer;

    private int _indexBufferSize;

    private Vector2 _scaleFactor = Vector2.One;

    private int _shader;

    private int _shaderFontTextureLocation;

    private int _shaderProjectionMatrixLocation;

    private int _vertexArray;

    private int _vertexBuffer;

    private int _vertexBufferSize;

    private int _windowHeight;

    private int _windowWidth;

    public ImGuiController(int width, int height)
    {
        this._windowWidth = width;
        this._windowHeight = height;

        int major = GL.GetInteger(GetPName.MajorVersion);
        int minor = GL.GetInteger(GetPName.MinorVersion);

        KHRDebugAvailable = major == 4 && minor >= 3 || IsExtensionSupported("KHR_debug");

        nint context = ImGui.CreateContext();
        ImGui.SetCurrentContext(context);
        var io = ImGui.GetIO();
        io.Fonts.AddFontDefault();

        io.BackendFlags |= ImGuiBackendFlags.RendererHasVtxOffset;

        this.CreateDeviceResources();
        SetKeyMappings();

        this.SetPerFrameImGuiData(1f / 60f);

        ImGui.NewFrame();
        this._frameBegun = true;
    }

    public static void CheckGLError(string title)
    {
        ErrorCode error;
        int i = 1;
        while ((error = GL.GetError()) != ErrorCode.NoError)
        {
            Debug.Print($"{title} ({i++}): {error}");
        }
    }

    public static int CreateProgram(string name, string vertexSource, string fragmentSoruce)
    {
        int program = GL.CreateProgram();
        LabelObject(ObjectLabelIdentifier.Program, program, $"Program: {name}");

        int vertex = CompileShader(name, ShaderType.VertexShader, vertexSource);
        int fragment = CompileShader(name, ShaderType.FragmentShader, fragmentSoruce);

        GL.AttachShader(program, vertex);
        GL.AttachShader(program, fragment);

        GL.LinkProgram(program);

        GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int success);
        if (success == 0)
        {
            string info = GL.GetProgramInfoLog(program);
            Debug.WriteLine($"GL.LinkProgram had info log [{name}]:\n{info}");
        }

        GL.DetachShader(program, vertex);
        GL.DetachShader(program, fragment);

        GL.DeleteShader(vertex);
        GL.DeleteShader(fragment);

        return program;
    }

    public static void LabelObject(ObjectLabelIdentifier objLabelIdent, int glObject, string name)
    {
        if (KHRDebugAvailable)
        {
            GL.ObjectLabel(objLabelIdent, glObject, name.Length, name);
        }
    }

    public void CreateDeviceResources()
    {
        this._vertexBufferSize = 10000;
        this._indexBufferSize = 2000;

        int prevVAO = GL.GetInteger(GetPName.VertexArrayBinding);
        int prevArrayBuffer = GL.GetInteger(GetPName.ArrayBufferBinding);

        this._vertexArray = GL.GenVertexArray();
        GL.BindVertexArray(this._vertexArray);
        LabelObject(ObjectLabelIdentifier.VertexArray, this._vertexArray, "ImGui");

        this._vertexBuffer = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, this._vertexBuffer);
        LabelObject(ObjectLabelIdentifier.Buffer, this._vertexBuffer, "VBO: ImGui");
        GL.BufferData(BufferTarget.ArrayBuffer, this._vertexBufferSize, nint.Zero, BufferUsageHint.DynamicDraw);

        this._indexBuffer = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, this._indexBuffer);
        LabelObject(ObjectLabelIdentifier.Buffer, this._indexBuffer, "EBO: ImGui");
        GL.BufferData(BufferTarget.ElementArrayBuffer, this._indexBufferSize, nint.Zero, BufferUsageHint.DynamicDraw);

        this.RecreateFontDeviceTexture();

        string VertexSource = @"#version 330 core

uniform mat4 projection_matrix;

layout(location = 0) in vec2 in_position;
layout(location = 1) in vec2 in_texCoord;
layout(location = 2) in vec4 in_color;

out vec4 color;
out vec2 texCoord;

void main()
{
    gl_Position = projection_matrix * vec4(in_position, 0, 1);
    color = in_color;
    texCoord = in_texCoord;
}";
        string FragmentSource = @"#version 330 core

uniform sampler2D in_fontTexture;

in vec4 color;
in vec2 texCoord;

out vec4 outputColor;

void main()
{
    outputColor = color * texture(in_fontTexture, texCoord);
}";

        this._shader = CreateProgram("ImGui", VertexSource, FragmentSource);
        this._shaderProjectionMatrixLocation = GL.GetUniformLocation(this._shader, "projection_matrix");
        this._shaderFontTextureLocation = GL.GetUniformLocation(this._shader, "in_fontTexture");

        int stride = Unsafe.SizeOf<ImDrawVert>();
        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, stride, 0);
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, stride, 8);
        GL.VertexAttribPointer(2, 4, VertexAttribPointerType.UnsignedByte, true, stride, 16);

        GL.EnableVertexAttribArray(0);
        GL.EnableVertexAttribArray(1);
        GL.EnableVertexAttribArray(2);

        GL.BindVertexArray(prevVAO);
        GL.BindBuffer(BufferTarget.ArrayBuffer, prevArrayBuffer);

        CheckGLError("End of ImGui setup");
    }

    public void DestroyDeviceObjects()
    {
        this.Dispose();
    }

    /// <summary>
    ///   Frees all graphics resources used by the renderer.
    /// </summary>
    public void Dispose()
    {
        GL.DeleteVertexArray(this._vertexArray);
        GL.DeleteBuffer(this._vertexBuffer);
        GL.DeleteBuffer(this._indexBuffer);

        GL.DeleteTexture(this._fontTexture);
        GL.DeleteProgram(this._shader);
    }

    /// <summary>
    ///   Recreates the device texture used to render text.
    /// </summary>
    public void RecreateFontDeviceTexture()
    {
        var io = ImGui.GetIO();

        io.Fonts.GetTexDataAsRGBA32(out nint pixels, out int width, out int height, out _);

        int mips = (int)Math.Floor(Math.Log(Math.Max(width, height), 2));

        int prevActiveTexture = GL.GetInteger(GetPName.ActiveTexture);
        GL.ActiveTexture(TextureUnit.Texture0);
        int prevTexture2D = GL.GetInteger(GetPName.TextureBinding2D);

        this._fontTexture = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, this._fontTexture);
        GL.TexStorage2D(TextureTarget2d.Texture2D, mips, SizedInternalFormat.Rgba8, width, height);
        LabelObject(ObjectLabelIdentifier.Texture, this._fontTexture, "ImGui Text Atlas");

        GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, width, height, PixelFormat.Bgra, PixelType.UnsignedByte, pixels);

        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, mips - 1);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);

        // Restore state
        GL.BindTexture(TextureTarget.Texture2D, prevTexture2D);
        GL.ActiveTexture((TextureUnit)prevActiveTexture);

        io.Fonts.SetTexID(this._fontTexture);

        io.Fonts.ClearTexData();
    }

    /// <summary>
    ///   Renders the ImGui draw list data.
    /// </summary>
    public void Render()
    {
        if (this._frameBegun)
        {
            this._frameBegun = false;
            ImGui.Render();
            this.RenderImDrawData(ImGui.GetDrawData());
        }
    }

    /// <summary>
    ///   Updates ImGui input and IO configuration state.
    /// </summary>
    public void Update(IKeyboard keyboard, IMouse mouse, float deltaSeconds)
    {
        if (this._frameBegun)
        {
            ImGui.Render();
        }

        this.SetPerFrameImGuiData(deltaSeconds);
        this.UpdateImGuiInput(keyboard, mouse);

        this._frameBegun = true;
        ImGui.NewFrame();
    }

    public void WindowResized(int width, int height)
    {
        this._windowWidth = width;
        this._windowHeight = height;
    }

    internal void MouseScroll(Vector2 offset)
    {
        var io = ImGui.GetIO();

        io.MouseWheel = offset.Y;
        io.MouseWheelH = offset.X;
    }

    internal void PressChar(char keyChar)
    {
        this.PressedChars.Add(keyChar);
    }

    private static int CompileShader(string name, ShaderType type, string source)
    {
        int shader = GL.CreateShader(type);
        LabelObject(ObjectLabelIdentifier.Shader, shader, $"Shader: {name}");

        GL.ShaderSource(shader, source);
        GL.CompileShader(shader);

        GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);
        if (success == 0)
        {
            string info = GL.GetShaderInfoLog(shader);
            Debug.WriteLine($"GL.CompileShader for shader '{name}' [{type}] had info log:\n{info}");
        }

        return shader;
    }

    private static bool IsExtensionSupported(string name)
    {
        int n = GL.GetInteger(GetPName.NumExtensions);
        for (int i = 0; i < n; i++)
        {
            string extension = GL.GetString(StringNameIndexed.Extensions, i);
            if (extension == name)
            {
                return true;
            }
        }

        return false;
    }

    private static void SetKeyMappings()
    {
        var io = ImGui.GetIO();
        io.KeyMap[(int)ImGuiKey.Tab] = (int)Keys.Tab;
        io.KeyMap[(int)ImGuiKey.LeftArrow] = (int)Keys.Left;
        io.KeyMap[(int)ImGuiKey.RightArrow] = (int)Keys.Right;
        io.KeyMap[(int)ImGuiKey.UpArrow] = (int)Keys.Up;
        io.KeyMap[(int)ImGuiKey.DownArrow] = (int)Keys.Down;
        io.KeyMap[(int)ImGuiKey.PageUp] = (int)Keys.PageUp;
        io.KeyMap[(int)ImGuiKey.PageDown] = (int)Keys.PageDown;
        io.KeyMap[(int)ImGuiKey.Home] = (int)Keys.Home;
        io.KeyMap[(int)ImGuiKey.End] = (int)Keys.End;
        io.KeyMap[(int)ImGuiKey.Delete] = (int)Keys.Delete;
        io.KeyMap[(int)ImGuiKey.Backspace] = (int)Keys.Backspace;
        io.KeyMap[(int)ImGuiKey.Enter] = (int)Keys.Enter;
        io.KeyMap[(int)ImGuiKey.Escape] = (int)Keys.Escape;
        io.KeyMap[(int)ImGuiKey.A] = (int)Keys.A;
        io.KeyMap[(int)ImGuiKey.C] = (int)Keys.C;
        io.KeyMap[(int)ImGuiKey.V] = (int)Keys.V;
        io.KeyMap[(int)ImGuiKey.X] = (int)Keys.X;
        io.KeyMap[(int)ImGuiKey.Y] = (int)Keys.Y;
        io.KeyMap[(int)ImGuiKey.Z] = (int)Keys.Z;
    }

    private void RenderImDrawData(ImDrawDataPtr draw_data)
    {
        if (draw_data.CmdListsCount == 0)
        {
            return;
        }

        // Get intial state.
        int prevVAO = GL.GetInteger(GetPName.VertexArrayBinding);
        int prevArrayBuffer = GL.GetInteger(GetPName.ArrayBufferBinding);
        int prevProgram = GL.GetInteger(GetPName.CurrentProgram);
        bool prevBlendEnabled = GL.GetBoolean(GetPName.Blend);
        bool prevScissorTestEnabled = GL.GetBoolean(GetPName.ScissorTest);
        int prevBlendEquationRgb = GL.GetInteger(GetPName.BlendEquationRgb);
        int prevBlendEquationAlpha = GL.GetInteger(GetPName.BlendEquationAlpha);
        int prevBlendFuncSrcRgb = GL.GetInteger(GetPName.BlendSrcRgb);
        int prevBlendFuncSrcAlpha = GL.GetInteger(GetPName.BlendSrcAlpha);
        int prevBlendFuncDstRgb = GL.GetInteger(GetPName.BlendDstRgb);
        int prevBlendFuncDstAlpha = GL.GetInteger(GetPName.BlendDstAlpha);
        bool prevCullFaceEnabled = GL.GetBoolean(GetPName.CullFace);
        bool prevDepthTestEnabled = GL.GetBoolean(GetPName.DepthTest);
        int prevActiveTexture = GL.GetInteger(GetPName.ActiveTexture);
        GL.ActiveTexture(TextureUnit.Texture0);
        int prevTexture2D = GL.GetInteger(GetPName.TextureBinding2D);
        Span<int> prevScissorBox = stackalloc int[4];
        unsafe
        {
            fixed (int* iptr = &prevScissorBox[0])
            {
                GL.GetInteger(GetPName.ScissorBox, iptr);
            }
        }

        // Bind the element buffer (thru the VAO) so that we can resize it.
        GL.BindVertexArray(this._vertexArray);
        // Bind the vertex buffer so that we can resize it.
        GL.BindBuffer(BufferTarget.ArrayBuffer, this._vertexBuffer);
        for (int i = 0; i < draw_data.CmdListsCount; i++)
        {
            var cmd_list = draw_data.CmdListsRange[i];

            int vertexSize = cmd_list.VtxBuffer.Size * Unsafe.SizeOf<ImDrawVert>();
            if (vertexSize > this._vertexBufferSize)
            {
                int newSize = (int)Math.Max(this._vertexBufferSize * 1.5f, vertexSize);

                GL.BufferData(BufferTarget.ArrayBuffer, newSize, nint.Zero, BufferUsageHint.DynamicDraw);
                this._vertexBufferSize = newSize;

                Console.WriteLine($"Resized dear imgui vertex buffer to new size {this._vertexBufferSize}");
            }

            int indexSize = cmd_list.IdxBuffer.Size * sizeof(ushort);
            if (indexSize > this._indexBufferSize)
            {
                int newSize = (int)Math.Max(this._indexBufferSize * 1.5f, indexSize);
                GL.BufferData(BufferTarget.ElementArrayBuffer, newSize, nint.Zero, BufferUsageHint.DynamicDraw);
                this._indexBufferSize = newSize;

                Console.WriteLine($"Resized dear imgui index buffer to new size {this._indexBufferSize}");
            }
        }

        // Setup orthographic projection matrix into our constant buffer
        var io = ImGui.GetIO();
        var mvp = Matrix4.CreateOrthographicOffCenter(
            0.0f,
            io.DisplaySize.X,
            io.DisplaySize.Y,
            0.0f,
            -1.0f,
            1.0f);

        GL.UseProgram(this._shader);
        GL.UniformMatrix4(this._shaderProjectionMatrixLocation, false, ref mvp);
        GL.Uniform1(this._shaderFontTextureLocation, 0);
        CheckGLError("Projection");

        GL.BindVertexArray(this._vertexArray);
        CheckGLError("VAO");

        draw_data.ScaleClipRects(io.DisplayFramebufferScale);

        GL.Enable(EnableCap.Blend);
        GL.Enable(EnableCap.ScissorTest);
        GL.BlendEquation(BlendEquationMode.FuncAdd);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        GL.Disable(EnableCap.CullFace);
        GL.Disable(EnableCap.DepthTest);

        // Render command lists
        for (int n = 0; n < draw_data.CmdListsCount; n++)
        {
            var cmd_list = draw_data.CmdListsRange[n];

            GL.BufferSubData(BufferTarget.ArrayBuffer, nint.Zero, cmd_list.VtxBuffer.Size * Unsafe.SizeOf<ImDrawVert>(), cmd_list.VtxBuffer.Data);
            CheckGLError($"Data Vert {n}");

            GL.BufferSubData(BufferTarget.ElementArrayBuffer, nint.Zero, cmd_list.IdxBuffer.Size * sizeof(ushort), cmd_list.IdxBuffer.Data);
            CheckGLError($"Data Idx {n}");

            for (int cmd_i = 0; cmd_i < cmd_list.CmdBuffer.Size; cmd_i++)
            {
                var pcmd = cmd_list.CmdBuffer[cmd_i];
                if (pcmd.UserCallback != nint.Zero)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    GL.ActiveTexture(TextureUnit.Texture0);
                    GL.BindTexture(TextureTarget.Texture2D, (int)pcmd.TextureId);
                    CheckGLError("Texture");

                    // We do _windowHeight - (int)clip.W instead of (int)clip.Y because gl has flipped Y when it comes to these coordinates
                    var clip = pcmd.ClipRect;
                    GL.Scissor((int)clip.X, this._windowHeight - (int)clip.W, (int)(clip.Z - clip.X), (int)(clip.W - clip.Y));
                    CheckGLError("Scissor");

                    if ((io.BackendFlags & ImGuiBackendFlags.RendererHasVtxOffset) != 0)
                    {
                        GL.DrawElementsBaseVertex(PrimitiveType.Triangles, (int)pcmd.ElemCount, DrawElementsType.UnsignedShort, (nint)(pcmd.IdxOffset * sizeof(ushort)), unchecked((int)pcmd.VtxOffset));
                    }
                    else
                    {
                        GL.DrawElements(BeginMode.Triangles, (int)pcmd.ElemCount, DrawElementsType.UnsignedShort, (int)pcmd.IdxOffset * sizeof(ushort));
                    }

                    CheckGLError("Draw");
                }
            }
        }

        GL.Disable(EnableCap.Blend);
        GL.Disable(EnableCap.ScissorTest);

        // Reset state
        GL.BindTexture(TextureTarget.Texture2D, prevTexture2D);
        GL.ActiveTexture((TextureUnit)prevActiveTexture);
        GL.UseProgram(prevProgram);
        GL.BindVertexArray(prevVAO);
        GL.Scissor(prevScissorBox[0], prevScissorBox[1], prevScissorBox[2], prevScissorBox[3]);
        GL.BindBuffer(BufferTarget.ArrayBuffer, prevArrayBuffer);
        GL.BlendEquationSeparate((BlendEquationMode)prevBlendEquationRgb, (BlendEquationMode)prevBlendEquationAlpha);
        GL.BlendFuncSeparate(
            (BlendingFactorSrc)prevBlendFuncSrcRgb,
            (BlendingFactorDest)prevBlendFuncDstRgb,
            (BlendingFactorSrc)prevBlendFuncSrcAlpha,
            (BlendingFactorDest)prevBlendFuncDstAlpha);
        if (prevBlendEnabled)
        {
            GL.Enable(EnableCap.Blend);
        }
        else
        {
            GL.Disable(EnableCap.Blend);
        }

        if (prevDepthTestEnabled)
        {
            GL.Enable(EnableCap.DepthTest);
        }
        else
        {
            GL.Disable(EnableCap.DepthTest);
        }

        if (prevCullFaceEnabled)
        {
            GL.Enable(EnableCap.CullFace);
        }
        else
        {
            GL.Disable(EnableCap.CullFace);
        }

        if (prevScissorTestEnabled)
        {
            GL.Enable(EnableCap.ScissorTest);
        }
        else
        {
            GL.Disable(EnableCap.ScissorTest);
        }
    }

    /// <summary>
    ///   Sets per-frame data based on the associated window. This is called by Update(float).
    /// </summary>
    private void SetPerFrameImGuiData(float deltaSeconds)
    {
        var io = ImGui.GetIO();
        io.DisplaySize = new Vector2(
            this._windowWidth / this._scaleFactor.X,
            this._windowHeight / this._scaleFactor.Y);
        io.DisplayFramebufferScale = this._scaleFactor;
        io.DeltaTime = deltaSeconds; // DeltaTime is in seconds.
    }

    private void UpdateImGuiInput(IKeyboard keyboard, IMouse mouse)
    {
        var io = ImGui.GetIO();

        io.MouseDown[0] = mouse.IsButtonDown(MouseButton.Left);
        io.MouseDown[1] = mouse.IsButtonDown(MouseButton.Right);
        io.MouseDown[2] = mouse.IsButtonDown(MouseButton.Middle);

        var screenPoint = new Vector2i((int)mouse.Location.X, (int)mouse.Location.Y);
        var point = screenPoint;//wnd.PointToClient(screenPoint);
        io.MousePos = new Vector2(point.X, point.Y);

        foreach (Key key in Enum.GetValues(typeof(Key)))
        {
            if (key == Key.Unknown)
            {
                continue;
            }

            io.KeysDown[(int)key] = keyboard.IsKeyDown(key);
        }

        foreach (char c in this.PressedChars)
        {
            io.AddInputCharacter(c);
        }

        this.PressedChars.Clear();
    }
}
