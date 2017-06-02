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
        Engine = Engine.Init(Hres, Vres, Game.NumLayers, 20, 2);
        Window = Window.Create(null, WindowFlags.Vsync);
    }

    /// <summary>
    /// Releases unmanaged resources
    /// </summary>
    public static void Deinit()
    {
        Engine.Deinit();
    }
}
