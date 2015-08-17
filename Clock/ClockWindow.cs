using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Clock
{
    class ClockWindow : GameWindow
    {
        public ClockWindow() : base(600, 600, GraphicsMode.Default, "Clock")
        {
            VSync = VSyncMode.On;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.White);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

            //Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            Matrix4 projection = Matrix4.CreateOrthographic(Width, Height, 0, 20);

            GL.MatrixMode(MatrixMode.Projection);

            GL.LoadMatrix(ref projection);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[Key.Escape])
                Exit();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);

            GL.MatrixMode(MatrixMode.Modelview);

            GL.LoadMatrix(ref modelview);

            GL.Begin(BeginMode.Lines);

            //DrawLine(new Vector3(0, 0, 4), new Vector3(-0.8f, 0.8f, 4.0f));
            GL.Color3(Color.Blue);
            DrawHourHand(Height /2 * 0.9f);
            //DrawCircle(0, 0, 1.3f, 720);
            GL.Color3(Color.Green);
            DrawMinuteHand(Height/2 * 0.75f);
            //DrawCircle(0, 0, 1f, 720);
            GL.Color3(Color.Red);
            DrawSecondHand(Height/2 * 0.5f);
            //DrawCircle(0, 0, 0.8f, 720);
            GL.Color3(Color.Black);
            DrawCircle(0, 0, Height/2 * 0.98f, 720);

            SwapBuffers();
        }

        public void DrawSecondHand (float length)
        {
            DateTime now = DateTime.Now;
            float angle = (360 / 60) * (now.Second + 15);
            angle *= (float)((Math.PI * 2f) / 360f);
            Vector2 vec = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            vec *= length;

            DrawLine(new Vector3(0, 0, 4), new Vector3(vec.X, vec.Y, 4.0f));
        }

        public void DrawMinuteHand(float length)
        {
            DateTime now = DateTime.Now;
            float angle = (360 / 60) * (now.Minute + 15);
            angle *= (float)((Math.PI * 2f) / 360f);
            Vector2 vec = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            vec *= length;

            DrawLine(new Vector3(0, 0, 4), new Vector3(vec.X, vec.Y, 4.0f));
        }

        public void DrawHourHand(float length)
        {
            DateTime now = DateTime.Now;
            float angle = (360 / 12) * (now.Hour + 3);
            angle *= (float)((Math.PI * 2f) / 360f);
            Vector2 vec = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            vec *= length;

            DrawLine(new Vector3(0, 0, 4), new Vector3(vec.X, vec.Y, 4.0f));
        }

        public static void DrawCircle(float x, float y, float radius, int segments)
        {
            // http://slabode.exofire.net/circle_draw.shtml
            GL.Begin(BeginMode.LineStrip);

            float theta, xx, yy;

            for (int i = 0; i < segments; i++)
            {
                theta = (2.0f * (float)Math.PI * (float)i) / (float)segments;
                xx = radius * (float)Math.Cos(theta);
                yy = radius * (float)Math.Sin(theta);
                GL.Vertex3(x + xx, y + yy, 4);
            }

            theta = (2.0f * (float)Math.PI * (float)0) / (float)segments;
            xx = radius * (float)Math.Cos(theta);
            yy = radius * (float)Math.Sin(theta);
            GL.Vertex3(x + xx, y + yy, 4);

            GL.End();
        }

        private void DrawLine (Vector3 Point1, Vector3 Point2)
        {
            GL.Begin(BeginMode.Lines);

            GL.Vertex3(Point1);
            GL.Vertex3(Point2);

            GL.End();
        }
    }
}
