using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace RSMv
{
    public class Game : GameWindow
    {
        GameObjects quad;
        public Game()
            : base(GameWindowSettings.Default, new NativeWindowSettings 
            {
                Size = new Vector2i(800, 600),
                Title = "RSMv",
                APIVersion = new Version(3, 3),
                Profile = ContextProfile.Core
            })
        {
            CenterWindow();
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            
            GL.ClearColor(0.5f, 0.6f, 1.0f, 1.0f);

            quad = new GameObjects(0.5f, -0.25f, 0.0f, 0.2f);
            quad.Load();
            
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            var input = KeyboardState;
            
            if (input.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape))
            {
                Close();
            }

            if (input.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.F11) 
            && input.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.RightAlt))
            {
                if (WindowState == WindowState.Fullscreen)
                {
                    WindowState = WindowState.Normal;
                }
                else
                {
                    WindowState = WindowState.Fullscreen;
                }
            }
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            quad.Render();

            SwapBuffers();
        }
        
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, e.Width, e.Height);
        }
        
        protected override void OnUnload()
        {
            base.OnUnload();
        }
    }
}
