/*
 * ****************************************************************************
 *  Super Mario Clone
 *      Tilengine based super mario implementation, written in C#
 *      Marc Palacios, 2016
 * ****************************************************************************
*/

using Tilengine;

/// <summary>
/// Static struct that holds Tilengine related resources (engine and window)
/// </summary>
struct Graphics
{
    public const int Hres = 400;
    public const int Vres = 240;

    public static Engine Engine;
    public static Window Window;

    /// <summary>
    /// Initializes Tilengine and built-in window
    /// </summary>
    public static void Init()
    {
        Engine = Engine.Init(Hres, Vres, Game.NumLayers, 1, 0);
        Window = Window.Create("overlay.bmp", WindowFlags.Vsync);
        Engine.BackgroundColor = new Color(0, 96, 184);
        Window.Blur = false;
    }

    /// <summary>
    /// Releases unmanaged resources
    /// </summary>
    public static void Deinit()
    {
        Window.Delete();
        Engine.Deinit();
    }
}
