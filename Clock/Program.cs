using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Clock
{

    class Program
    {
        static void Main(string[] args)
        {
            ClockWindow window = new ClockWindow();
            window.Run(60);
        }
    }
}
