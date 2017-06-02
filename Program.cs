/*
 * ****************************************************************************
 *  Super Mario Clone
 *      Tilengine based super mario implementation, written in C#
 *      Marc Palacios, 2016
 * ****************************************************************************
*/

using System;
using Tilengine;

/// <summary>
/// Main program
/// </summary>
static class Program
{
    /// <summary>
    /// Application entry point
    /// </summary>
    static void Main()
    {
        /* initialize */
        Graphics.Init();
        Game.Init();

        /* main loop */
        int frame = 0;
        while (Graphics.Window.Process())
        {
            Game.Update(frame);
            Graphics.Window.DrawFrame(frame);
            frame++;
        }

        /* deinitialize and release resources */
        Game.Deinit();
        Graphics.Deinit();
    }
}
