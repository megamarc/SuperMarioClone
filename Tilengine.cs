/*
Copyright (c) 2018-2025 Marc Palacios Domènech

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

/*
*****************************************************************************
* C# Tilengine wrapper - Up to date to library version 2.15
* http://www.tilengine.org
*****************************************************************************
*/

using System;
using System.Runtime.InteropServices;

namespace Tilengine
{
    /// <summary>
    /// Tile data contained in each cell of a cref="Tilemap" object
    /// </summary>
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct Tile
    {
        public ushort index;
        public ushort flags;
    }

    /// <summary>
    /// List of possible exception error codes
    /// </summary>
    public enum Error
    {
        Ok,              // No error
        OutOfMemory,     // Not enough memory
        IdxLayer,        // Layer index out of range
        IdxSprite,       // Sprite index out of range
        IdxAnimation,    // Animation index out of range
        IdxPicture,      // Picture or tile index out of range
        RefTileset,      // Invalid Tileset reference
        RefTilemap,      // Invalid Tilemap reference
        RefSpriteset,    // Invalid Spriteset reference   
        RefPalette,      // Invalid Palette reference
        RefSequence,     // Invalid Sequence reference   
        RefSequencePack, // Invalid SequencePack reference
        RefBitmap,       // Invalid Bitmap reference
        NullPointer,     // Null pointer as argument
        FileNotFound,    // Resource file not found
        WrongFormat,     // Resource file has invalid format
        WrongSize,       // A width or height parameter is invalid
        Unsupported,     // Unsupported function
        RefList,         // Invalid TLN_ObjectList reference
        IdxPalette,      // Palette index out of range
        MaxError,        
    }	

    /// <summary>
    /// List of flag values for window creation
    /// </summary>
    public enum WindowFlags
    {
        Fullscreen	= (1 << 0),
        Vsync 		= (1 << 1),
        S1 			= (1 << 2),
        S2 			= (2 << 2),
        S3 			= (3 << 2),
        S4 			= (4 << 2),
        S5 			= (5 << 2),
        Nearest		= (1 << 6), // unfiltered upscaling
		NoVsync 	= (1 << 7), // disable default vsync
    }

    /// <summary>
    /// Player index for input assignment functions
    /// </summary>
    public enum Player
    {
        P1,
        P2,
        P3,
        P4
    }

    /// <summary>
    /// Standard inputs query for cref="Window.GetInput()"
    /// </summary>
    public enum Input
    {
        None,
        Up,
        Down,
        Left,
        Right,
        Button1,
        Button2,
        Button3,
        Button4,
        Button5,
        Button6,
        Start,
		Quit,
		CRT,

        P1 = (Player.P1 << 4), // request player 1 input (default)
        P2 = (Player.P2 << 4), // request player 2 input
        P3 = (Player.P3 << 4), // request player 3 input
        P4 = (Player.P4 << 4), // request player 4 input

        /* compatibility symbols for pre-1.18 input model */
        Button_A = Button1,
        Button_B = Button2,
        Button_C = Button3,
        Button_D = Button4,
        Button_E = Button5,
        Button_F = Button6,
    }

    /// <summary>
    /// Available blending modes for cref="Layer" and cref="Sprite"
    /// </summary>
    public enum Blend
    {
        None,
        Mix25,
		Mix50,
		Mix75,
        Add,
        Sub,
		Mod,
		Custom,
		Mix = Mix50
    }

    /// <summary>
    /// List of flags for sprites
    /// </summary>
    public enum TileFlags
    {
        None        = (0),          // no flags
        FlipX       = (1 << 15),    // horizontal flip
        FlipY       = (1 << 14),    // vertical flip
        Rotate      = (1 << 13),    // transposes 90º
        Priority    = (1 << 12),    // tile goes in front of regular sprites
        Tileset     = (15 << 7),    // tileset index (0 - 15)
        Palette     = (7 << 4),     // palette index (0 - 7)
    }

    /// <summary>
    /// List of flags for tiles
    /// </summary>
    public enum SpriteFlags
    {
        None        = (0),          // no flags
        FlipX       = (1 << 15),    // horizontal flip
        FlipY       = (1 << 14),    // vertical flip
        Rotate      = (1 << 13),    // transposes 90º (only for square sprites)
        Priority    = (1 << 12),    // sprite goes in front of priority tiles
        Masked      = (1 << 11),    // sprite won't be drawn inside masked region
    }

    /// <summary>
    /// Layer type
    /// </summary>
    public enum LayerType
    {
        None,       // undefined
        Tile,       // tilemap-based layer
        Object,     // objects layer
        Bitmap,     // bitmapped layer
    }

    /// <summary>
    /// Trace levels
    /// </summary>
    public enum LogLevel
    {
        None,       // Don't print anything (default)
        Errors,     // Print only runtime errors
        Verbose,	// Print everything
    }

    /// <summary>
    /// Data used to create cref="Spriteset" objects
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SpriteData
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string Name;
        public int X;
        public int Y;
        public int W;
        public int H;
    }

    /// <summary>
    /// Data returned by cref="Spriteset.GetSpriteInfo" with dimensions of the requested sprite
    /// </summary>
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct SpriteInfo
    {
        public int W;
        public int H;
    }

    /// <summary>
    /// Data returned by cref="Layer.GetTile" about a given tile inside a background layer
    /// </summary>
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct TileInfo
    {
        public ushort Index;
        public ushort Flags;
        public int Row;
        public int Col;
        public int Xoffset;
        public int Yoffset;
        public byte Color;
        public byte Type;
        public bool Empty;
    }

	/// <summary>
	/// Data returned by cref="ObjectList.GetInfo" about a given object inside an objects layer
	/// </summary>
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct ObjectInfo
	{
		public ushort Id;           // unique ID
		public ushort Gid;          // graphic ID (tile index)
		public ushort Flags;        // attributes (FlipX, FlipY, Priority...)
		public int X;               // horizontal position
		public int Y;               // vertical position
		public int Width;           // horizontal size
		public int Height;          // vertical size
		public byte Type;           // type property
		[MarshalAs(UnmanagedType.I1)]
		public bool Visible;        // visible property
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string Name;         // name property
	}

	/// <summary>
	/// Image Tile items for cref="Tileset.FromImages"
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct TileImage
	{
		public IntPtr Bitmap;
		public ushort Id;
		public byte Type;
	}

	/// <summary>
	/// Sprite state
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct SpriteState
	{
		public int X;                   // Screen position x
		public int Y;                   // Screen position y
		public int W;                   // Actual width in screen (after scaling)
		public int H;                   // Actual height in screen (after scaling)
		public SpriteFlags Flags;       // flags
		public IntPtr Palette;          // Native palette reference
		public IntPtr Spriteset;        // Native spriteset reference
		public int Index;               // Graphic index inside spriteset
		[MarshalAs(UnmanagedType.I1)]
		public bool Enabled;            // enabled or not
		[MarshalAs(UnmanagedType.I1)]
		public bool Collision;          // per-pixel collision detection enabled or not
	}	

    /// <summary>
    /// cref="Tileset" attributes for constructor
    /// </summary>
    [StructLayoutAttribute(LayoutKind.Sequential)]
	public struct TileAttributes
	{
		public byte Type;		// type of tile
		public bool Priority;	// priority bit set
	}

    /// <summary>
    /// Data used to define each frame of an animation for cref="Sequence" objects
    /// </summary>
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct SequenceFrame
    {
	    public int Index;	// tile/sprite index
	    public int Delay;	// delay for next frame
    }

    /// <summary>
    /// Data used to define each frame of a color cycle for cref="Sequence" objects
    /// </summary>
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct ColorStrip
    {
        public int Delay;
        public byte First;
        public byte Count;
        public byte Dir;
    }

    /// <summary>
    /// Sequence info returned by cref="Sequence.GetInfo"
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct SequenceInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Name;	    // sequence name
        public int NumFrames;	// number of frames
    }

    /// <summary>
    /// Represents a color value in RGB format
    /// </summary>
    public struct Color
    {
        public byte R,G,B;
        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
    }

    /// <summary>
    /// overlays for CRT effect
    /// </summary>
	public enum Overlay
	{
		None,
		ShadowMask,
		Aperture,
		Scanlines,
		Custom
	}

    /// <summary>
    /// pixel mapping for cref="Layer.SetPixelMapping"
    /// </summary>
	public struct PixelMap
	{
		public ushort Dx, Dy;
	}

    /// <summary>
    /// Generic Tilengine exception
    /// </summary>
    public class TilengineException : Exception
    {
        public TilengineException(string message) : base(message)
        {
        }
    }

    public delegate void VideoCallback(int line);
	public delegate byte BlendFunction(byte src, byte dst);

    /// <summary>
    /// Main object for engine creation and rendering
    /// </summary>
    public class Engine : IDisposable
    {
        // singleton
        private static Engine instance = null;

        public Layer[] Layers;
		public Sprite[] Sprites;
		public Animation[] Animations;

        public int Width;		// Width in pixels of the framebuffer
        public int Height;		// Height in pixels of the framebuffer
        public uint Version;	// Tilengine dll version, in a 32-bit integer

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_Init(int hres, int vres, int numlayers, int numsprites, int numanimations);

        [DllImport("Tilengine")]
        private static extern void TLN_Deinit();

        [DllImport("Tilengine")]
		private static extern void TLN_SetTargetFps(int fps);

		[DllImport("Tilengine")]
		private static extern int TLN_GetTargetFps();

        [DllImport("Tilengine")]
        private static extern int TLN_GetWidth();

        [DllImport("Tilengine")]
        private static extern int TLN_GetHeight();

        [DllImport("Tilengine")]
        private static extern uint TLN_GetNumObjects();

        [DllImport("Tilengine")]
        private static extern uint TLN_GetUsedMemory();

        [DllImport("Tilengine")]
        private static extern uint TLN_GetVersion();

        [DllImport("Tilengine")]
        private static extern int TLN_GetNumLayers();

        [DllImport("Tilengine")]
        private static extern int TLN_GetNumSprites();

        [DllImport("Tilengine")]
        private static extern void TLN_SetBGColor(byte r, byte g, byte b);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetBGColorFromTilemap(IntPtr tilemap);

        [DllImport("Tilengine")]
        private static extern void TLN_DisableBGColor();

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetBGBitmap(IntPtr bitmap);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetBGPalette(IntPtr palette);

        [DllImport("Tilengine")]
        private static extern void TLN_SetRasterCallback(VideoCallback callback);

        [DllImport("Tilengine")]
        private static extern void TLN_SetFrameCallback(VideoCallback callback);

        [DllImport("Tilengine")]
        private static extern void TLN_SetRenderTarget(byte[] data, int pitch);

        [DllImport("Tilengine")]
        private static extern void TLN_UpdateFrame(int time);

        [DllImport("Tilengine")]
        private static extern void TLN_BeginFrame(int frame);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_DrawNextScanline();

        [DllImport("Tilengine")]
        private static extern void TLN_SetLastError(Error error);

        [DllImport("Tilengine")]
        private static extern Error TLN_GetLastError();

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_GetErrorString(Error error);

        [DllImport("Tilengine")]
        private static extern int TLN_GetAvailableSprite();

        [DllImport("Tilengine")]
        private static extern int TLN_GetAvailableAnimation();

        [DllImport("Tilengine")]
        private static extern void TLN_SetLoadPath(string path);

        [DllImport("Tilengine")]
        private static extern void TLN_SetCustomBlendFunction(BlendFunction function);

		[DllImport("Tilengine")]
		private static extern bool TLN_SetGlobalPalette(int index, IntPtr palette);
		
		[DllImport("Tilengine")]
		private static extern IntPtr TLN_GetGlobalPalette(int index);		
		
		[DllImport("Tilengine")]
		private static extern void TLN_SetLogLevel(int logLevel);
		
		[DllImport("Tilengine")]
		private static extern bool TLN_OpenResourcePack(string filename, string key);
		
		[DllImport("Tilengine")]
		private static extern void TLN_CloseResourcePack();

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_LoadWorld(string tmxfile, int first_layer);

        [DllImport("Tilengine")]
        private static extern void TLN_SetWorldPosition(int x, int y);

        [DllImport("Tilengine")]
        private static extern void TLN_ReleaseWorld();

        private Engine (int hres, int vres, int numLayers, int numSprites, int numAnimations)
        {
            int c;

            this.Width = hres;
            this.Height = vres;
            this.Version = TLN_GetVersion();

            Layers = new Layer[numLayers];
            for (c = 0; c < numLayers; c++)
                Layers[c].index = c;

            Sprites = new Sprite[numSprites];
            for (c = 0; c < numSprites; c++)
                Sprites[c].index = c;

            Animations = new Animation[numAnimations];
            for (c = 0; c < numAnimations; c++)
                Animations[c].index = c;
        }

        /// <summary>
        /// Initializes the graphic engine
        /// </summary>
        /// <param name="hres">horizontal resolution in pixels</param>
        /// <param name="vres">vertical resolution in pixels</param>
        /// <param name="numLayers">number of layers</param>
        /// <param name="numSprites">number of sprites</param>
        /// <param name="numAnimations">number of animations</param>
        /// <returns>Engine instance</returns>
        /// <remarks>This is a singleton object: calling Init multiple times will return the same reference</remarks>
        public static Engine Init(int hres, int vres, int numLayers, int numSprites, int numAnimations)
        {
            // singleton
            if (instance == null)
            {
                IntPtr retval = TLN_Init(hres, vres, numLayers, numSprites, numAnimations);
                Engine.ThrowException(retval != IntPtr.Zero);
                instance = new Engine(hres, vres, numLayers, numSprites, numAnimations);
            }
            return instance;
        }

        /// <summary>
        /// Deinits engine and frees associated resources
        /// </summary>
        public void Dispose()
        {
            if (instance != null)
                TLN_Deinit();
            instance = null;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Engine()
        {
            Dispose();
        }

        /// <summary>
        /// Returns the number of objets used by the engine so far
        /// </summary>
        public uint NumObjects
        {
            get { return TLN_GetNumObjects(); }
        }

        /// <summary>
        /// Returns the total amount of memory used by the objects so far
        /// </summary>
        public uint UsedMemory
        {
            get { return TLN_GetUsedMemory(); }
        }

        /// <summary>
        /// Sets the background color, that is the color of the pixel when there isn't any layer or sprite at that position
        /// </summary>
        public void SetBackgroundColor(Color value)
        {
            TLN_SetBGColor(value.R, value.G, value.B);
        }

        /// <summary>
        /// Sets the background color from a tilemap, that is the color of the pixel when there isn't any layer or sprite at that position
        /// </summary>
        public void SetBackgroundColor(Tilemap tilemap)
        {
            TLN_SetBGColorFromTilemap(tilemap.ptr);
        }

        /// <summary>
		/// Disales background color rendering. If you know that the last background layer will always
        /// cover the entire screen, you can disable it to gain some performance
        /// </summary>
        public void DisableBackgroundColor()
        {
            TLN_DisableBGColor();
        }

        /// <summary>
        /// Sets an optional, static bitmap as background instead of a solid color
        /// </summary>
        public void SetBackgroundBitmap(Bitmap  bitmap)
        {
            bool ok = TLN_SetBGBitmap(bitmap != null? bitmap.ptr : IntPtr.Zero);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Sets the palette for the optional background bitmap
        /// </summary>
        public void SetBackgroundPalette(Palette palette)
        {
            bool ok = TLN_SetBGPalette(palette != null? palette.ptr : IntPtr.Zero);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Sets the output surface for rendering
        /// </summary>
        /// <param name="data">Array of bytes that will hold the render target</param>
        /// <param name="pitch">Number of bytes per each scanline of the framebuffer</param>
        /// <remarks>The render target pixel format must be 32 bits RGBA</remarks>
        public void SetRenderTarget(byte[] data, int pitch)
        {
            TLN_SetRenderTarget(data, pitch);
        }

        /// <summary>
        /// Enables raster effects processing, like a virtual HBLANK interrupt where
        /// any render parameter can be modified between scanlines.
        /// </summary>
        /// <param name="callback">name of the user-defined function to call for each scanline. Set Null to disable.</param>
        public void SetRasterCallback(VideoCallback callback)
        {
            TLN_SetRasterCallback(callback);
        }

        /// <summary>
        /// Enables user callback for each drawn frame, like a virtual VBLANK interrupt
        /// </summary>
        /// <param name="callback">name of the user-defined function to call for each frame. Set Null to disable.</param>
        public void SetFrameCallback(VideoCallback callback)
        {
            TLN_SetFrameCallback(callback);
        }

        /// <summary>
        /// Sets base path for all data loading .FromFile() static methods
        /// </summary>
        public void SetLoadPath(string loadPath)
        {
            TLN_SetLoadPath(loadPath);
        }

        /// <summary>
        /// Sets custom blend function to use in sprites or background layers when cref="Blend.Custom" mode
		/// is selected with the cref="Layer.BlendMode" and cref="Sprite.BlendMode" properties.
        /// </summary>
        /// <param name="function">user-defined function to call when blending that takes
		/// two integer arguments: source component intensity, destination component intensity, and returns
		/// the desired intensity.
        /// </param>
        public void SetCustomBlendFunction(BlendFunction function)
        {
            TLN_SetCustomBlendFunction(function);
        }

        /// <summary>
        /// Starts active rendering of the current frame
        /// </summary>
        /// <param name="frame">Timestamp value</param>
        /// <remarks>This method is used for active rendering combined with DrawNextScanline(), instead of using delegates for raster effects</remarks>
        public void BeginFrame(int frame)
        {
            TLN_BeginFrame(frame);
        }

        /// <summary>
        /// Draws the next scanline of the frame when doing active rendering (without delegates)
        /// </summary>
        /// <returns>true if there are still scanlines to draw or false when the frame is complete</returns>
        public bool DrawNextScanline()
        {
            return TLN_DrawNextScanline();
        }

        /// <summary>
        /// Throws a TilengineException after an unsuccessful operation
        /// </summary>
        /// <param name="success"></param>
        internal static void ThrowException(bool success)
        {
            if (success == false)
            {
                Error error = TLN_GetLastError();
                IntPtr intptr = TLN_GetErrorString(error);
                throw new TilengineException(System.Runtime.InteropServices.Marshal.PtrToStringAnsi(intptr));
            }
        }

        /// <summary>
        /// Returns reference to first unused sprite slot. The method doesn't allocate the sprite, it just returns a reference to it.
        /// </summary>
        /// <returns></returns>
        public Sprite GetAvailableSprite()
        {
            int index = TLN_GetAvailableSprite();
            Engine.ThrowException(index != -1);
            return Sprites[index];
        }

        /// <summary>
        /// Returns reference to first unused animation slot. The method doesn't allocate the animation, it just returns a reference to it.
        /// </summary>
        /// <returns></returns>
        public Animation GetAvailableAnimation()
        {
            int index = TLN_GetAvailableAnimation();
            Engine.ThrowException(index != -1);
            return Animations[index];
        }

        /// <summary>
        /// Sets/gets desired target frames per second, by default 60
        /// </summary>
        public int TargetFPS
        {
            set { TLN_SetTargetFps(value); }
            get { return TLN_GetTargetFps(); }
        }

        /// <summary>
        /// Sets one of the eight global palettes used by tiled layers
        /// </summary>
        /// <param name="index">Palette index [0 - 7]</param>
        /// <param name="palette">Reference of palette to set, or NULL to disable it</param>
        public void SetGlobalPalette(int index, Palette palette)
        {
            bool ok = TLN_SetGlobalPalette(index, palette.ptr);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Returns one of the eight global palettes
        /// </summary>
        /// <param name="index">Index of global palette to query [0 - 7]</param>
        /// <returns>Palette reference or NULL if not set</returns>
        public Palette GetGlobalPalette(int index)
        {
            IntPtr value = TLN_GetGlobalPalette(index);
            Engine.ThrowException(value != IntPtr.Zero);
            return new Palette(value);
        }

        /// <summary>
        /// Open the resource package with optional aes-128 key and binds it
        /// </summary>
        /// <param name="path">File path with the resource package (.dat extension)</param>
        /// <param name="key">Optional string with aes decryption key</param>
        /// <remarks>
        /// When the package is opened, it's globally bind to all TLN_LoadXXX functions. 
        /// The assets inside the package are indexed with their original path/file as when
        /// they were plain files.As long as the structure used to build the package
        /// matches the original structure of the assets, the LoadPath property and FromFile() methods
        /// functions will work transparently, easing the migration with minimal changes.
        /// </remarks>
        public void OpenResourcePack(string path, string key=null)
        {
            bool ok = TLN_OpenResourcePack(path, key);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Closes current resource package and unbinds it 
        /// </summary>
        /// <see cref="Engine.OpenResourcePack"></see>
        public void CloseResourcePack()
        {
            TLN_CloseResourcePack();
        }

        /// <summary>
        /// Verbosity of trace messages
        /// </summary>
        public void SetLogLevel(LogLevel value)
        {
            TLN_SetLogLevel((int)value);
        }

        /// <summary>
        /// Opens a whole tmx file and enables world mode, where layers move according their parallax factor and sprites are given world coordinates instead of screen coordinates.
        /// </summary>
        /// <param name="tmxfile">TMX file to load</param>
        /// <param name="first_layer">Index of the first layer to place layers read from the file</param>
        public void OpenWorld(string tmxfile, int first_layer = 0)
        {
            bool ok = TLN_LoadWorld(tmxfile, first_layer);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Sets the world position, that is the top-left corner of the screen in world coordinates. All layers and attached sprites are moved accordingly. Requires world mode to be enabled with OpenWorld() method.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(int x, int y)
        {
            TLN_SetWorldPosition(x, y);
        }

        /// <summary>
        /// Disables world mode and returns to normal screen coordinates. Releases used memory by the tmx file
        /// </summary>
        public void CloseWorld()
        {
            TLN_ReleaseWorld();
        }
    }

    /// <summary>
    /// Built-in windowing and user input
    /// </summary>
    public class Window : IDisposable
    {
        // singleton
        private static Window instance;

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_CreateWindow(string overlay, WindowFlags flags);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_CreateWindowThread(string overlay, WindowFlags flags);

        [DllImport("Tilengine")]
        private static extern void TLN_SetWindowTitle (string title);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_ProcessWindow();

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_IsWindowActive();

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_GetInput(Input id);

        [DllImport("Tilengine")]
        private static extern void TLN_EnableInput (Player player, bool enable);

        [DllImport("Tilengine")]
        private static extern void TLN_AssignInputJoystick (Player player, int index);

        [DllImport("Tilengine")]
        private static extern void TLN_DefineInputKey (Player player, Input input, int keycode);

        [DllImport("Tilengine")]
        private static extern void TLN_DefineInputButton (Player player, Input input, byte joybutton);

        [DllImport("Tilengine")]
        private static extern void TLN_DrawFrame(int time);

        [DllImport("Tilengine")]
        private static extern void TLN_WaitRedraw();

        [DllImport("Tilengine")]
        private static extern void TLN_DeleteWindow();

		[DllImport("Tilengine")]
		private static extern void TLN_EnableCRTEffect(Overlay overlay, byte overlay_factor, byte threshold, byte v0, byte v1, byte v2, byte v3, bool blur, byte glow_factor);

		[DllImport("Tilengine")]
		private static extern void TLN_DisableCRTEffect();

        [DllImport("Tilengine")]
        private static extern void TLN_Delay(uint msecs);

        [DllImport("Tilengine")]
        private static extern uint TLN_GetTicks();

        [DllImport("Tilengine")]
        private static extern int GetAverageFps();

        [DllImport("Tilengine")]
        private static extern int TLN_GetWindowWidth();

        [DllImport("Tilengine")]
        private static extern int TLN_GetWindowHeight();

        [DllImport("Tilengine")]
        private static extern int TLN_GetWindowScaleFactor();

        [DllImport("Tilengine")]
        private static extern void TLN_SetWindowScaleFactor(int factor);

        /// <summary>
        /// Returns the number of milliseconds since application start
        /// </summary>
        public uint Ticks
        {
            get { return TLN_GetTicks(); }
        }

        /// <summary>
        /// Returns average frames per second
        /// </summary>
        public int AverageFPS
        {
            get { return GetAverageFps(); }
        }

        /// <summary>
        /// Gets/sets integer window scaling factor
        /// </summary>
        public int ScaleFactor
        {
            get { return TLN_GetWindowScaleFactor(); }
            set { TLN_SetWindowScaleFactor(value); }
        }

        /// <summary>
        /// Returns window width in pixels
        /// </summary>
        public int Width
        {
            get { return TLN_GetWindowWidth(); }
        }

        /// <summary>
        /// Returns window height in pixels
        /// </summary>
        public int Height
        {
            get { return TLN_GetWindowHeight(); }
        }

        /// <summary>
        /// Returns true if window is active or false if the user has requested to end the application (by pressing Esc key or clicking the close button)
        /// </summary>
        public bool Active
        {
            get { return TLN_IsWindowActive(); }
        }

        /// <summary>
        /// Creates a window for rendering
        /// </summary>
        /// <param name="flags">Combined mask of the possible creation flags</param>
        /// <returns>Window instance</returns>
        /// <remarks>This is a singleton object: calling Init multiple times will return the same reference</remarks>
        public static Window Create(WindowFlags flags)
        {
            // singleton
            if (instance == null)
            {
                bool retval = TLN_CreateWindow (null, flags);
                Engine.ThrowException(retval);
                instance = new Window();
            }
            return instance;
        }

        /// <summary>
        /// Creates a multithreaded window for rendering
        /// </summary>
        /// <param name="overlay">Optional path of a bmp file to overlay (for emulating RGB mask, scanlines, etc)</param>
        /// <param name="flags">Combined mask of the possible creation flags</param>
        /// <returns>Window instance</returns>
        /// <remarks>This is a singleton object: calling Init multiple times will return the same reference</remarks>
        public static Window CreateThreaded(string overlay, WindowFlags flags)
        {
            // singleton
            if (instance == null)
            {
                bool retval = TLN_CreateWindowThread (overlay, flags);
                Engine.ThrowException(retval);
                instance = new Window();
            }
            return instance;
        }

        /// <summary>
        /// Sets the title of the window
        /// </summary>
        public void SetTitle(string title)
        {
            TLN_SetWindowTitle(title);
        }

        /// <summary>
        /// Does basic window housekeeping in signgle-threaded window. Must be called for each frame in game loop
        /// </summary>
        /// <returns>true if window is active or false if the user has requested to end the application (by pressing Esc key or clicking the close button)</returns>
        /// <remarks>This method must be called only for single-threaded windows, created with Create() method.</remarks>
        public bool Process ()
        {
            return TLN_ProcessWindow ();
        }

        /// <summary>
        /// Returns the state of a given input
        /// </summary>
        /// <param name="id">Input identifier to check state</param>
        /// <returns>true if that input is pressed or false if not</returns>
        public bool GetInput(Input id)
        {
            return TLN_GetInput(id);
        }

        /// <summary>
        /// Enables or disables input for specified player
        /// </summary>
        /// <param name="player">Player number to configure</param>
        /// <param name="enable"></param>
        public void EnableInput(Player player, bool enable)
        {
            TLN_EnableInput(player, enable);
        }

        /// <summary>
        /// Assigns a joystick index to the specified player
        /// </summary>
        /// <param name="player">Player number to configure</param>
        /// <param name="index"></param>
        public void AssignInputJoystick(Player player, int index)
        {
            TLN_AssignInputJoystick (player, index);
        }

        /// <summary>
        /// Assigns a keyboard input to a player
        /// </summary>
        /// <param name="player">Player number to configure</param>
        /// <param name="input"></param>
        /// <param name="keycode"></param>
        public void DefineInputKey(Player player, Input input, int keycode)
        {
            TLN_DefineInputKey (player, input, keycode);
        }

        /// <summary>
        /// Assigns a button joystick input to a player
        /// </summary>
        /// <param name="player">Player number to configure</param>
        /// <param name="input"></param>
        /// <param name="joybutton"></param>
        public void DefineInputButton(Player player, Input input, byte joybutton)
        {
            TLN_DefineInputButton (player, input, joybutton);
        }

        /// <summary>
        /// Draws a frame to the window
        /// </summary>
        /// <remarks>This method does delegate-driven rendering</remarks>
        public void DrawFrame()
        {
            TLN_DrawFrame(0);
        }

        /// <summary>
        /// Thread synchronization for multithreaded window. Waits until the current frame has ended rendering
        /// </summary>
        public void WaitRedraw()
        {
            TLN_WaitRedraw();
        }

        /// <summary>
        ///
        /// </summary>
		public void EnableCRTEffect(Overlay overlay, byte overlay_factor, byte threshold, byte v0, byte v1, byte v2, byte v3, bool blur, byte glow_factor)
		{
			TLN_EnableCRTEffect(overlay, overlay_factor, threshold, v0, v1, v2, v3, blur, glow_factor);
		}

        /// <summary>
        ///
        /// </summary>
		public void DisableCRTEffect()
		{
			TLN_DisableCRTEffect();
		}

        /// <summary>
        /// Suspends execition for a fixed time
        /// </summary>
        /// <param name="msecs">Number of milliseconds to wait</param>
        public void Delay(uint msecs)
        {
            TLN_Delay(msecs);
        }

        /// <summary>
        /// Destroys active window
        /// </summary>
        public void Dispose()
        {
            if (instance != null)
                TLN_DeleteWindow();
            instance = null;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Window()
        {
            Dispose();
        }
    }

    /// <summary>
    /// Background layer management
    /// </summary>
    public struct Layer
    {
		internal int index;

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetLayer(int nlayer, IntPtr tileset, IntPtr tilemap);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetLayerTilemap(int nlayer, IntPtr tilemap);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_GetLayerTilemap(int nlayer);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetLayerPalette(int nlayer, IntPtr palette);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_GetLayerPalette(int nlayer);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetLayerBitmap(int nlayer, IntPtr bitmap);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern IntPtr TLN_GetLayerBitmap(int nlayer);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetLayerPosition(int nlayer, int hstart, int vstart);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetLayerScaling(int nlayer, float xfactor, float yfactor);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetLayerTransform(int nlayer, float angle, float dx, float dy, float sx, float sy);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
		private static extern bool TLN_SetLayerPixelMapping (int nlayer, PixelMap[] table);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetLayerBlendMode(int nlayer, Blend mode, byte factor);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetLayerColumnOffset(int nlayer, int[] offset);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetLayerWindow(int nlayer, int x1, int y1, int x2, int y2, bool invert);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetLayerWindowColor(int nlayer, byte r, byte g, byte b, Blend blend);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_DisableLayerWindow(int nlayer);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_DisableLayerWindowColor(int nlayer);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetLayerMosaic (int nlayer, int width, int height);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_DisableLayerMosaic(int nlayer);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_ResetLayerMode(int nlayer);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetLayerPriority(int nlayer, bool enable);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetLayerParent(int nlayer, int parent);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_DisableLayerParent(int nlayer);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_DisableLayer(int nlayer);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_EnableLayer(int nlayer);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_GetLayerTile(int nlayer, int x, int y, out TileInfo info);

        [DllImport("Tilengine")]
        private static extern int TLN_GetLayerWidth(int nlayer);

        [DllImport("Tilengine")]
        private static extern int TLN_GetLayerHeight(int nlayer);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetLayerObjects(int nlayer, IntPtr objects, IntPtr tileset);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_GetLayerObjects(int nlayer);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_GetLayerTileset(int nlayer);

        [DllImport("Tilengine")]
        private static extern int TLN_GetLayerX(int nlayer);

        [DllImport("Tilengine")] 
        private static extern int TLN_GetLayerY(int nlayer);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetLayerParallaxFactor(int nlayer, float x, float y);

        /// <summary>
        /// Sets/gets the layer palette, overriding the one contained in the associated Bitmap or Tileset
        /// </summary>
        public Palette Palette
        {
            get
            {
                IntPtr value = TLN_GetLayerPalette(index);
                Engine.ThrowException(value != IntPtr.Zero);
                return new Palette(value);
            }
            set
            {
                bool ok = TLN_SetLayerPalette(index, value.ptr);
                Engine.ThrowException(ok);
            }
        }

        /// <summary>
        /// Configures a Bitmap layer with the specified bitmap
        /// </summary>
        public Bitmap Bitmap
        {
            set
            {
                bool ok = TLN_SetLayerBitmap(index, value.ptr);
                Engine.ThrowException(ok);
            }
            get
            {
                IntPtr value = TLN_GetLayerBitmap(index);
                return value != IntPtr.Zero ? new Bitmap(value) : null;
            }
        }

        /// <summary>
        /// Configures a Tile layer with the specified tilemap
        /// </summary>
        public Tilemap Tilemap
        {
            set
            {
                bool ok = TLN_SetLayerTilemap(index, value.ptr);
                Engine.ThrowException(ok);
            }
            get
            {
                IntPtr value = TLN_GetLayerTilemap(index);
                return value != IntPtr.Zero ? new Tilemap(value) : null;
            }
        }

        /// <summary>
        /// Configures a Object layer with the specified object list.
        /// </summary>
        public ObjectList ObjectList
        {
            set
            {
                bool ok = TLN_SetLayerObjects(index, value.ptr, IntPtr.Zero);
                Engine.ThrowException(ok);
            }
            get
            {
                IntPtr value = TLN_GetLayerObjects(index);
                return value != IntPtr.Zero ? new ObjectList(value) : null;
            }
        }

        /// <summary>
        /// Returns layer width in pixels
        /// </summary>
        public int Width
        {
            get { return TLN_GetLayerWidth(index); }
        }

        /// <summary>
        /// Returns layer height in pixels
        /// </summary>
        public int Height
        {
            get { return TLN_GetLayerHeight(index); }
        }

        /// <summary>
        /// Returns layer horizontal position (scroll offset). Set with cref="SetPosition"/>
        /// </summary>
        public int X
        {
            get { return TLN_GetLayerX(index); }
        }

        /// <summary>
        /// Returns layer vertical position (scroll offset). Set with cref="SetPosition"/>
        /// </summary>
        public int Y
        {
            get { return TLN_GetLayerY(index); }
        }

        /// <summary>
        /// Set up a Tile layer with explicit Tileset and Tilemap
        /// </summary>
        /// <param name="tileset">Tileset object to set</param>
        /// <param name="tilemap">Tilemap object to set</param>
        /// <remarks>Deprecated, use property cref="Tilemap" instead to configure a Tile layer</remarks>
        public void Setup(Tileset tileset, Tilemap tilemap)
        {
            bool ok = TLN_SetLayer(index, tileset.ptr, tilemap.ptr);
            Engine.ThrowException(ok);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(int x, int y)
        {
            bool ok = TLN_SetLayerPosition(index, x, y);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Sets simple scaling
        /// </summary>
        /// <param name="sx">Horizontal scale factor</param>
        /// <param name="sy">Vertical scale factor</param>
        /// <remarks>
        /// By default the scaling factor of a given layer is 1.0f, 1.0f, which means
        /// no scaling.Use values below 1.0 to downscale (shrink) and above 1.0 to upscale (enlarge).
        /// Call cref="Reset" to disable scaling
        /// </remarks>
        public void SetScaling(float sx, float sy)
        {
            bool ok = TLN_SetLayerScaling(index, sx, sy);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Sets affine transform matrix to enable rotating and/or scaling
        /// </summary>
        /// <param name="angle">Rotation angle in degrees</param>
        /// <param name="dx">Horizontal displacement</param>
        /// <param name="dy">Vertical displacement</param>
        /// <param name="sx">Horizontal scaling</param>
        /// <param name="sy">Vertical scaling</param>
        public void SetTransform(float angle, float dx, float dy, float sx, float sy)
        {
            bool ok = TLN_SetLayerTransform(index, angle, dx, dy, sx, sy);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Sets the table for pixel mapping render mode
        /// </summary>
		/// <param name="map"></param>
		public void SetPixelMap(PixelMap[] map)
        {
            bool ok = TLN_SetLayerPixelMapping(index, map);
            Engine.ThrowException(ok);
		}

        /// <summary>
        /// Disables scaling and affine transformation, restoring the original pixel size
        /// </summary>
        public void Reset()
        {
            bool ok = TLN_ResetLayerMode(index);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Sets the blending mode (transparency effect)
        /// </summary>
        public void SetBlendMode(Blend mode)
        {
            bool ok = TLN_SetLayerBlendMode(index, mode, 0);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Enables column offset, where each vertical column of a Tile layer can be shifted by a different amount of pixels.
        /// <param name="offsets"/>Array of offsets to set, one position per screen column Set null to disable column offset mode</param>
        /// </summary>
        public void SetColumnOffset(int[] offsets)
        {
            bool ok = TLN_SetLayerColumnOffset(index, offsets);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Enables clipping rectangle region
        /// </summary>
        /// <param name="x1">Left coordinate</param>
        /// <param name="y1">Top coordinate</param>
        /// <param name="x2">Right coordinate</param>
        /// <param name="y2">Bottom coordinate</param>
        /// <param name="invert">false=clip outer region, true=clip inner region</param>
        public void EnableWindow(int x1, int y1, int x2, int y2, bool invert=false)
        {
            bool ok = TLN_SetLayerWindow(index, x1, y1, x2, y2, invert);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Enables solid color processing on clipped region in window layer
        /// </summary>
        /// <param name="color">Color for the window</param>
        /// <param name="blend">Blend mode</param>
        /// When color is enabled on window, the area outside the clipped region gets filled with this color.
        /// If one of blending modes is selected, color math is performed with underlying layer
        public void EnableWindowColor(Color color, Blend blend)
        {
            bool ok = TLN_SetLayerWindowColor(index, color.R, color.G, color.B, blend);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Disables layer window clipping
        /// </summary>
        /// <seealso cref="SetWindow"/>
        public void DisableWindow()
        {
            bool ok = TLN_DisableLayerWindow(index);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Disables color processing for window
        /// </summary>
        /// <seealso cref="SetWindowColor"/>
        public void DisableWindowColor()
        {
            bool ok = TLN_DisableLayerWindowColor(index);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Enables mosaic effect (pixelation)
        /// </summary>
        /// <param name="width">Vorizontal pixel size</param>
        /// <param name="height">Vertical pixel size</param>
        public void EnableMosaic(int width, int height)
        {
            bool ok = TLN_SetLayerMosaic(index, width, height);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Disables mosaic effect
        /// </summary>
        public void DisableMosaic()
        {
            bool ok = TLN_DisableLayerMosaic(index);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Gets information about the tile located in specified tilemap space
        /// </summary>
        /// <param name="x">Horizontal position in pixels from the left border</param>
        /// <param name="y">Vertical position in pixels from the top border</param>
        /// <param name="info">Application-allocated TileInfo structure that will we filled with the data</param>
        public void GetTileInfo(int x, int y, out TileInfo info)
        {
            bool ok = TLN_GetLayerTile(index, x, y, out info);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Sets layer priority. All tiles in the layer will appear in front of sprites
        /// </summary>
        public void EnablePriority()
        {
            TLN_SetLayerPriority(index, true);
        }

        /// <summary>
        /// Clears layer priority. All tiles in the layer will appear behind sprites
        /// </summary>
        public void DisablePriority()
        {
            TLN_SetLayerPriority(index, false);
        }

        /// <summary>
        /// Enables parent layer index. It will scroll and scale together with the parent layer.
        /// </summary>
        public void EnableParentLayer(int parent)
        {
            bool ok = TLN_SetLayerParent(index, parent);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Removes parent layer index. The layer will scroll and scale independently from the previous parent layer.
        /// </summary>
        public void DisableParentLayer(int parent)
        {
            bool ok = TLN_SetLayerParent(index, -1);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Enables the layer so it will be drawn. Must have been configured with Tilemap, Bitmap or ObjectList properties
        /// </summary>
        public void Enable()
        {
            bool ok = TLN_EnableLayer(index);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Disables layer so it won't be drawn. Can be enabled again with Enable() method.
        /// </summary>
        public void Disable()
        {
            bool ok = TLN_DisableLayer(index);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Overrides the default layer parallax factor, which is used to calculate the position of the layer in world coordinates when world mode is enabled (see Engine.OpenWorld)
        /// </summary>
        /// <param name="x">Horizontal parallax factor (1.0 means 1:1 pixel/world ratio)</param>
        /// <param name="y">Horizontal parallax factor</param>
        public void SetParallaxFactor(float px, float py)
        {
            bool ok = TLN_SetLayerParallaxFactor(index, px, py);
            Engine.ThrowException(ok);
        }
    }

    /// <summary>
    /// Sprite management
    /// </summary>
    public struct Sprite
    {
		internal int index;

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_ConfigSprite(int nsprite, IntPtr spriteset, SpriteFlags flags);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetSpriteSet(int nsprite, IntPtr spriteset);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetSpriteFlags(int nsprite, SpriteFlags flags);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_EnableSpriteFlag(int nsprite, uint flag, bool enable);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetSpritePivot(int nsprite, float px, float py);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetSpritePosition(int nsprite, int x, int y);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetSpritePicture(int nsprite, int entry);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetSpritePalette(int nsprite, IntPtr palette);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetSpriteBlendMode(int nsprite, Blend mode, byte factor);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetSpriteScaling(int nsprite, float sx, float sy);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_ResetSpriteScaling(int nsprite);

        [DllImport("Tilengine")]
        private static extern int TLN_GetSpritePicture(int nsprite);

        [DllImport("Tilengine")] 
        private static extern int TLN_GetSpriteX(int nsprite);

        [DllImport("Tilengine")] 
        private static extern int TLN_GetSpriteY(int nsprite);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_EnableSpriteCollision(int nsprite, [MarshalAsAttribute(UnmanagedType.I1)] bool enable);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_GetSpriteCollision(int nsprite);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_GetSpriteState(int nsprite, out SpriteState state);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetFirstSprite(int nsprite);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetNextSprite(int nsprite, int next);

        [DllImport("Tilengine")]
        public static extern void TLN_SetSpritesMaskRegion(int top_line, int bottom_line);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_SetSpriteAnimation(int nsprite, IntPtr sequence, int loop);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_DisableSpriteAnimation(int nsprite);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_PauseSpriteAnimation(int index);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        public static extern bool TLN_ResumeSpriteAnimation(int index);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_DisableSprite(int nsprite);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_GetSpritePalette(int nsprite);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetSpriteWorldPosition(int nsprite, int x, int y);

        /// <summary>
        /// Sets/gets the actual graphic index inside the spriteset for the sprite
        /// </summary>
        public int Picture
        {
            set
            {
                bool ok = TLN_SetSpritePicture(index, value);
                Engine.ThrowException(ok);
            }
            get
            {
                int value = TLN_GetSpritePicture(index);
                Engine.ThrowException(value != -1);
                return value;
            }
        }

        /// <summary>
        /// Sets/gets the sprite palette, overriding the one defined in the associated spriteset
        /// </summary>
        public Palette Palette
        {
            set
            {
                bool ok = TLN_SetSpritePalette(index, value.ptr);
                Engine.ThrowException(ok);
            }
            get
            {
                IntPtr value = TLN_GetSpritePalette(index);
                return value != IntPtr.Zero ? new Palette(value) : null;
            }
        }

        /// <summary>
        /// Read-only property that returns the horizontal position of the sprite in pixels from the left border of the screen.
        /// </summary>
        public int X
        {
            get { return TLN_GetSpriteX(index); }
        }

        /// <summary>
        /// Read-only property that rturns the vertical position of the sprite in pixels from the top border of the screen.
        /// </summary>
        public int Y
        {
            get { return TLN_GetSpriteY(index); }
        }

        /// <summary>
        /// Returns true if the sprite si involved in a collision with another sprite at pixel level. Must be enabled with EnableCollisions() method.
        /// </summary>
        public bool Collision
        {
            get { return TLN_GetSpriteCollision(index); }
        }

        /// <summary>
        /// Configures a sprite, setting spriteset and flags at once
        /// </summary>
        /// <param name="spriteset">Spriteset for the sprite</param>
        /// <param name="flags">Combination of SpriteFlags</param>
        public void Setup(Spriteset spriteset, SpriteFlags flags)
        {
            bool ok = TLN_ConfigSprite(index, spriteset.ptr, flags);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Assigns the spriteset to the sprite.
        /// <param name="spriteset">Spriteset to assign</param>
        /// </summary>
        public void SetSpriteset(Spriteset spriteset)
        {
            bool ok = TLN_SetSpriteSet(index, spriteset.ptr);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Sets the combination of flags for the sprite
        /// <param name="flags">Combination of SpriteFlags</param>
        /// </summary>
        public void SetFlags(SpriteFlags flags)
        {
            bool ok = TLN_SetSpriteFlags(index, flags);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Enables (activates) a combination of SpriteFlags for the sprite.
        /// </summary>
        /// <param name="flag">Combination of SpriteFlags to enable</param>
        public void EnableFlag(uint flag)
        {
            bool ok = TLN_EnableSpriteFlag(index, flag, true);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Disables (deactivates) a combination of SpriteFlags for the sprite.
        /// </summary>
        /// <param name="flag">Combination of SpriteFlags to disable</param>
        public void DisableFlag(uint flag)
        {
            bool ok = TLN_EnableSpriteFlag(index, flag, false);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Sets the pivot point of the sprite, which is the point around which it is placed and scaled. By default is 0.0 (top-left corner of the sprite).
        /// </summary>
        /// <param name="x">horizontal normalized value (0.0 = full left, 1.0 = full right)</param>
        /// <param name="y">vertical normalized value (0.0 = full top, 1.0 = full bottom)</param>
        /// <remarks>Sprite pivot is reset automatically to default position after changing the spriteset</remarks>
        public void SetPivot(float x, float y)
        {
            bool ok = TLN_SetSpritePivot(index, x, y);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Sets the sprite position in screen space
        /// </summary>
        /// <param name="x">Horizontal position in pixels of the pivot point in screen</param>
        /// <param name="y">Vertical position in pixels of the pivot point in screen</param>
        public void SetPosition(int x, int y)
        {
            bool ok = TLN_SetSpritePosition(index, x, y);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Sets the sprite position in world space, if world mode is active
        /// </summary>
        /// <param name="x">Horizontal position in pixels of the pivot point in world space</param>
        /// <param name="y">Vertical position in pixels of the pivot point in world space</param>
        public void SetWorldPosition(int x, int y)
        {
            bool ok = TLN_SetSpritePosition(index, x, y);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Sets the scaling factor of the sprite. 1.0 means no scaling, values below 1.0 will shrink the sprite and values above 1.0 will enlarge it.
        /// </summary>
        /// <param name="sx">Horizontal scale factor</param>
        /// <param name="sy">Vertical scale factor</param>
        public void EnableScaling(float sx, float sy)
        {
            bool ok = TLN_SetSpriteScaling(index, sx, sy);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Disables scaling transformation, restoring the original pixel size of the sprite.
        /// </summary>
        public void DisableScaling()
        {
            bool ok = TLN_ResetSpriteScaling(index);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Sets blending mode
        /// </summary>
        /// <param name="mode">Blend mode to set</param>
        public void SetBlendMode(Blend mode)
        {
            bool ok = TLN_SetSpriteBlendMode(index, mode, 0);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Enable sprite collision checking at pixel level
        /// </summary>
        public void EnableCollisions()
        {
            bool ok = TLN_EnableSpriteCollision(index, true);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Enable sprite collision checking at pixel level
        /// </summary>
        public void DisableCollisions()
        {
            bool ok = TLN_EnableSpriteCollision(index, false);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Gets the current state of the sprite, which includes its position, picture, palette, flags and other properties.
        /// </summary>
        /// <param name="state">Application-allocated SpriteState structure that will receive the data</param>
        public void GetState(out SpriteState state)
        {
            bool ok = TLN_GetSpriteState(index, out state);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Sets this the first sprite to drawn in sequence (beginning of list)
        /// </summary>
        public void SetFirst()
        {
            bool ok = TLN_SetFirstSprite(index);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Sets the index of next sprite to draw after this one, building the list
        /// </summary>
        /// <param name="next">Index of the next sprite to dtaw after this</param>
        public void SetNext(int next)
        {
            bool ok = TLN_SetNextSprite(index, next);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Static method for all spites. Define the exclusion region for sprites that have SpriteFlags.Masked flag set
        /// </summary>
        /// <param name="top">Top scanline of the esxclusion region</param>
        /// <param name="bottom">Bottom line of the exclusion region</param>
        public static void SetMaskRegion(int top, int bottom)
        {
            TLN_SetSpritesMaskRegion(top, bottom);
        }

        /// <summary>
        /// Starts a sprite animation
        /// </summary>
        /// <param name="sequence">Sequence object with the animation definition</param>
        /// <param name="loop">number of times to loop, 0=infinite</param>
        public void EnableAnimation(Sequence sequence, int loop)
        {
            bool ok = TLN_SetSpriteAnimation(index, sequence.ptr, loop);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Disables sprite animation, if it was previously set with SetAnimation.
        /// </summary>
        public void DisableAnimation()
        {
            bool ok = TLN_DisableSpriteAnimation(index);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Pauses the animation effect. Resume with ResumeAnimation.
        /// </summary>
        public void PauseAnimation(bool pause)
        {
            bool ok = TLN_PauseSpriteAnimation(index);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Resumes the animation previously paused with PauseAnimation.
        /// </summary>
        public void ResumeAnimation(bool pause)
        {
            bool ok = TLN_ResumeSpriteAnimation(index);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Disables the sprite and returns it to the pool of available sprites
        /// </summary>
        public void Disable()
        {
            bool ok = TLN_DisableSprite(index);
            Engine.ThrowException(ok);
        }
    }

    /// <summary>
    /// Palette animation management (color cycle)
    /// </summary>
    public struct Animation
    {
		internal int index;

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetPaletteAnimation(int index, IntPtr palette, IntPtr sequence, [MarshalAsAttribute(UnmanagedType.I1)] bool blend);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetPaletteAnimationSource(int index, IntPtr palette);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_GetAnimationState(int index);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetAnimationDelay(int index, int delay);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_DisableAnimation(int index);

        /// <summary>
        /// Returns true if the animation is active, false if it is not running or has finished.
        /// </summary>
        public bool Active
        {
            get { return TLN_GetAnimationState(index); }
        }

        /// <summary>
        /// Starts a palette animation (color cycle)
        /// </summary>
        /// <param name="palette">Palette object to animate</param>
        /// <param name="sequence">Sequence to assign for the palette animation</param>
        /// <param name="blend">true to perform inter-frame bleding, false to disable blending and use discrete steps</param>
        public void Enable(Palette palette, Sequence sequence, bool blend)
        {
            bool ok = TLN_SetPaletteAnimation(index, palette.ptr, sequence.ptr, blend);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Allows to change the palette animation source, which is the palette that will be animated. Allow to color-cycle a palette that is itself changing between frames.
        /// </summary>
        /// <param name="palette">Palette source for the animation</param>
        public void SetPaletteSource(Palette palette)
        {
            bool ok = TLN_SetPaletteAnimationSource(index, palette.ptr);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Sets the delay in milliseconds between animation frames for whole animation.
        /// </summary>
        public void SetDelay(int delay)
        {
            bool ok = TLN_SetAnimationDelay(index, delay);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Disables this animation
        /// </summary>
        public void Disable()
        {
            bool ok = TLN_DisableAnimation(index);
            Engine.ThrowException(ok);
        }
    }

    /// <summary>
    /// Spriteset resource for Sprite objects
    /// </summary>
    public class Spriteset : ICloneable, IDisposable
    {
        internal IntPtr ptr;
        bool owned;

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_CreateSpriteset(IntPtr bitmap, SpriteData[] rects, int entries);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_LoadSpriteset(string name);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_CloneSpriteset(IntPtr src);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_GetSpriteInfo(IntPtr spriteset, int entry, out SpriteInfo info);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_GetSpritesetPalette(IntPtr spriteset);

        [DllImport("Tilengine")]
        private static extern int TLN_FindSpritesetSprite(IntPtr spriteset, string name);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetSpritesetData(IntPtr spriteset, int entry, SpriteData[] data, byte[] pixels, int pitch);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_DeleteSpriteset(IntPtr Spriteset);

        /// <summary>
        /// Returns the palette associated to the spriteset
        /// </summary>
        public Palette Palette
        {
            get { return new Palette(TLN_GetSpritesetPalette(ptr)); }
        }

        /// <summary>
        /// Internal constructor
        /// </summary>
        internal Spriteset (IntPtr res, bool owned = false)
        {
            ptr = res;
            this.owned = owned;
        }

        /// <summary>
        /// Creates a new spriteset
        /// </summary>
        /// <param name="bitmap">Bitmap containing the sprite graphics</param>
        /// <param name="data">Array of SpriteData structures with sprite descriptions</param>
        public Spriteset (Bitmap bitmap, SpriteData[] data)
        {
            IntPtr retval = TLN_CreateSpriteset(bitmap.ptr, data, data.Length);
            Engine.ThrowException(retval != IntPtr.Zero);
            ptr = retval;
            owned = true;
        }

        /// <summary>
        /// Loads a spriteset from an image png and its associated atlas descriptor
        /// </summary>
        /// <param name="filename">Base name of the files containing the spriteset, with or without .png extension</param>
        /// <remarks>
        /// The spriteset comes in a pair of files: an image file(bmp or png) and a standarized atlas descriptor(json, csv or txt)
        /// The supported json format is the array.
        /// </remarks>
        public static Spriteset FromFile (string filename)
        {
            IntPtr retval = TLN_LoadSpriteset (filename);
            Engine.ThrowException(retval != IntPtr.Zero);
            return new Spriteset(retval, true);
        }

        /// <summary>
        /// Creates a duplicate of the specified spriteset and its associated palette
        /// </summary>
        /// <returns>Cloned spriteset</returns>
        public object Clone ()
        {
            IntPtr retval = TLN_CloneSpriteset(ptr);
            Engine.ThrowException(retval != IntPtr.Zero);
            return new Spriteset(retval, true);
        }

        /// <summary>
        /// Query the details about the specified sprite inside a spriteset
        /// </summary>
        /// <param name="index">The entry index inside the spriteset [0, num_sprites - 1]</param>
        /// <param name="info">Pointer to application-allocated SpriteInfo structure that will receive the data</param>
        public void GetInfo (int index, out SpriteInfo info)
        {
            bool ok = TLN_GetSpriteInfo (ptr, index, out info);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Returns the index of a sprite inside the spriteset by its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Index of the sprite</returns>
        public int FindSprite(string name)
        {
            int index = TLN_FindSpritesetSprite(ptr, name);
            Engine.ThrowException(index != -1);
            return index;
        }

        /// <summary>
        /// Sets attributes and pixels of a given sprite inside the spriteset
        /// </summary>
        /// <param name="entry">The entry index inside the spriteset to modify [0, num_sprites - 1]</param>
        /// <param name="data">Pointer to a user-provided SpriteData structure with sprite description</param>
        /// <param name="pixels">Pointer to source pixel data</param>
        /// <param name="pitch"></param>
        public void SetSpritesetData(int entry, SpriteData[] data, byte[] pixels, int pitch)
        {
            bool ok = TLN_SetSpritesetData(ptr, entry, data, pixels, pitch);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Deletes the spriteset and releases memory
        /// </summary>
        public void Dispose ()
        {
            if (owned && ptr != IntPtr.Zero)
            {
                bool ok = TLN_DeleteSpriteset (ptr);
                Engine.ThrowException(ok);
                ptr = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Spriteset()
        {
            Dispose();
        }
    }

    /// <summary>
    /// Tileset resource for Tile layers
    /// </summary>
    public class Tileset : ICloneable, IDisposable
    {
        internal IntPtr ptr;
        bool owned;

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_CreateTileset(int numtiles, int width, int height, IntPtr palette, IntPtr sequencepack, TileAttributes[] attributes);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_CreateImageTileset(int numtiles, TileImage[] images);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_LoadTileset(string filename);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_CloneTileset(IntPtr src);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetTilesetPixels(IntPtr tileset, int entry, byte[] srcdata, int srcpitch);

        [DllImport("Tilengine")]
        private static extern int TLN_GetTileWidth(IntPtr tileset);

        [DllImport("Tilengine")]
        private static extern int TLN_GetTileHeight(IntPtr tileset);

        [DllImport("Tilengine")]
        private static extern int TLN_GetTilesetNumTiles(IntPtr tileset);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_GetTilesetPalette(IntPtr tileset);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_GetTilesetSequencePack(IntPtr tileset);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_DeleteTileset(IntPtr tileset);

        /// <summary>
        /// Returns the width in pixels of each individual tile in the tileset
        /// </summary>
        public int Width
        {
            get { return TLN_GetTileWidth(ptr); }
        }

        /// <summary>
        /// Returns the height in pixels of each individual tile in the tileset
        /// </summary>
        public int Height
        {
            get { return TLN_GetTileHeight(ptr); }
        }

        /// <summary>
        /// Returns the number of different tiles in tileset
        /// </summary>
        public int NumTiles
        {
            get { return TLN_GetTilesetNumTiles(ptr); }
        }

        /// <summary>
        /// Returns a reference to the palette associated to the tileset
        /// </summary>
        public Palette Palette
        {
            get { return new Palette(TLN_GetTilesetPalette(ptr)); }
        }

        /// <summary>
        /// Returns a reference to the optional sequence pack associated to the tileset
        /// </summary>
        public SequencePack SequencePack
        {
            get { return new SequencePack(TLN_GetTilesetSequencePack(ptr)); }
        }

        /// <summary>
        /// Internal constructor
        /// </summary>
        internal Tileset (IntPtr res, bool owned = false)
        {
            ptr = res;
            this.owned = owned;
        }

        /// <summary>
        /// Creates a tile-based tileset
        /// </summary>
        /// <param name="numTiles">Number of tiles that the tileset will hold</param>
        /// <param name="width">Width of each tile (must be multiple of 8)</param>
        /// <param name="height">Height of each tile (must be multiple of 8)</param>
        /// <param name="palette">Palette object to assign</param>
        /// <param name="sp">Optional sequence pack with associated tileset animations. Can be null</param>
        /// <param name="attributes">Optional array of attributes, one for each tile. Can be null</param>
        public Tileset(int numTiles, int width, int height, Palette palette, SequencePack sp=null, TileAttributes[] attributes=null)
        {
            IntPtr retval = TLN_CreateTileset(numTiles, width, height, palette.ptr, sp.ptr != null ? sp.ptr : IntPtr.Zero, attributes);
            Engine.ThrowException(retval != IntPtr.Zero);
            ptr = retval;
            owned = true;
        }

        /// <summary>
        /// Loads a tileset from a Tiled .tsx file
        /// </summary>
        /// <param name="filename">TSX file to load</param>
        /// <returns>Reference to the newly loaded tileset</returns>
        /// <remarks>An associated palette is also created, it can be obtained with the property cref="Tileset::Palette"</remarks>
        public static Tileset FromFile(string filename)
        {
            IntPtr retval = TLN_LoadTileset(filename);
            Engine.ThrowException(retval != IntPtr.Zero);
            return new Tileset(retval, true);
        }

        public static Tileset FromImages(int numTiles, TileImage[] images)
        {
            IntPtr retval = TLN_CreateImageTileset(numTiles, images);
            Engine.ThrowException(retval != IntPtr.Zero);
            return new Tileset(retval, true);
        }

        /// <summary>
        /// Creates a deep copy of the tileset
        /// </summary>
        /// <returns>A reference to the newly cloned tileset</returns>
        public object Clone()
        {
            IntPtr retval = TLN_CloneTileset(ptr);
            Engine.ThrowException(retval != IntPtr.Zero);
            return new Tileset(retval, true);
        }

        /// <summary>
        /// Sets pixel data for a tile in a tile-based tileset
        /// </summary>
        /// <param name="entry">Number of tile to set [0, num_tiles - 1]</param>
        /// <param name="pixels">Array of bytes with source pixel data</param>
        /// <param name="pitch">Bytes per line of source data</param>
        /// <remarks>Care must be taken in providing pixel data and pitch as it can crash the aplication</remarks>
        public void SetPixels(int entry, byte[] pixels, int pitch)
        {
            bool ok = TLN_SetTilesetPixels(ptr, entry, pixels, pitch);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Deletes the tileset and releases used memory
        /// </summary>
        public void Dispose()
        {
            if (owned && ptr != IntPtr.Zero)
            {
                bool ok = TLN_DeleteTileset(ptr);
                Engine.ThrowException(ok);
                ptr = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Tileset()
        {
            Dispose();
        }
    }

    /// <summary>
    /// Tilemap resource for Tile layers
    /// </summary>
    public class Tilemap : ICloneable, IDisposable
    {
        internal IntPtr ptr;
        bool owned;

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_CreateTilemap(int rows, int cols, Tile[] tiles, uint bgcolor, IntPtr tileset);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_LoadTilemap(string filename, string layername);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_CloneTilemap(IntPtr src);

        [DllImport("Tilengine")]
        private static extern int TLN_GetTilemapRows(IntPtr tilemap);

        [DllImport("Tilengine")]
        private static extern int TLN_GetTilemapCols(IntPtr tilemap);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetTilemapTileset2(IntPtr tilemap, IntPtr tileset, int index);

        [DllImport("Tilengine")] 
        private static extern IntPtr TLN_GetTilemapTileset2(IntPtr tilemap, int index);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_GetTilemapTile(IntPtr tilemap, int row, int col, out Tile tile);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetTilemapTile(IntPtr tilemap, int row, int col, ref Tile tile);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_CopyTiles(IntPtr src, int srcrow, int srccol, int rows, int cols, IntPtr dst, int dstrow, int dstcol);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_GetTilemapTiles(IntPtr tilemap, int row, int col);
        
        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_DeleteTilemap(IntPtr tilemap);

        /// <summary>
        /// Returns the number of columns
        /// </summary>
        public int Cols
        {
            get { return TLN_GetTilemapCols(ptr); }
        }

        /// <summary>
        /// Returns the number of rows
        /// </summary>
        public int Rows
        {
            get { return TLN_GetTilemapRows(ptr); }
        }

        /// <summary>
        /// Gets/sets the default tileset associated to the tilemap
        /// </summary>
        public Tileset Tileset
        {
            get { return new Tileset(TLN_GetTilemapTileset2(ptr, 0)); }
            set
            {
                bool ok = TLN_SetTilemapTileset2(ptr, value.ptr, 0);
                Engine.ThrowException(ok);
            }
        }

        /// <summary>
        /// Intrernal constructor
        /// </summary>
        internal Tilemap (IntPtr res, bool owned = false)
        {
            ptr = res;
            this.owned = owned;
        }

        /// <summary>
        /// Creates a new tilemap with the specified number of rows and columns, using the provided tiles and background color.
        /// </summary>
        /// <param name="rows">Number of rows (vertical dimension)</param>
        /// <param name="cols">Number of columns (horizontal dimension)</param>
        /// <param name="tiles">Array of Tile structures with tile data</param>
        /// <param name="bgcolor">Background color value</param>
        /// <param name="tileset">Optional reference to associated tileset, can be null</param>
        public Tilemap(int rows, int cols, Tile[] tiles, Color bgcolor, Tileset tileset=null)
        {
            long color;
            color = 0xFF000000 + (bgcolor.R << 16) + (bgcolor.G << 8) + bgcolor.B;

            IntPtr retval = TLN_CreateTilemap(rows, cols, tiles, (uint)color, tileset != null? tileset.ptr : IntPtr.Zero);
            Engine.ThrowException(retval != IntPtr.Zero);
            ptr = retval;
            owned = true;
        }

        /// <summary>
        /// Loads a tilemap layer from a Tiled .tmx file
        /// </summary>
        /// <param name="filename">TMX file with the tilemap</param>
        /// <param name="layername">Optional name of the layer inside the tmx file to load. null to load the first layer</param>
        /// <returns></returns>
        public static Tilemap FromFile(string filename, string layername=null)
        {
            IntPtr retval = TLN_LoadTilemap(filename, layername);
            Engine.ThrowException(retval != IntPtr.Zero);
            return new Tilemap(retval, true);
        }

        /// <summary>
        /// Creates a deep copy of the tilemap
        /// </summary>
        /// <returns> Cloned tilemap object</returns>
        public object Clone()
        {
            IntPtr retval = TLN_CloneTilemap(ptr);
            Engine.ThrowException(retval != IntPtr.Zero);
            return new Tilemap(retval, true);
        }

        /// <summary>
        /// Sets a tileset at the specified index in the tilemap.
        /// </summary>
        /// <param name="tileset">Tileset to set</param>
        /// <param name="index">Tileset index [0 - 7]</param>
        public void SetTileset(Tileset tileset, int index)
        {
            bool ok = TLN_SetTilemapTileset2(ptr, tileset.ptr, index);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Gets the tileset at the specified index in the tilemap.
        /// </summary>
        /// <param name="index">Tileset index [0 - 7]</param>
        /// <returns>Tileset at given position</returns>
        public Tileset GetTileset(int index)
        {
            IntPtr tilesetPtr = TLN_GetTilemapTileset2(ptr, index);
            Engine.ThrowException(tilesetPtr != IntPtr.Zero);
            return new Tileset(tilesetPtr);
        }

        /// <summary>
        /// Sets a tile of a tilemap
        /// </summary>
        /// <param name="row">Row (vertical position) of the tile [0 - num_rows - 1]</param>
        /// <param name="col">Column (horizontal position) of the tile [0 - num_cols - 1]</param>
        /// <param name="tile">Tile data to set</param>
        public void SetTile(int row, int col, ref Tile tile)
        {
            bool ok = TLN_SetTilemapTile(ptr, row, col, ref tile);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Gets data of a single tile inside a tilemap
        /// </summary>
        /// <param name="row">Row (vertical position) of the tile [0 - num_rows - 1]</param>
        /// <param name="col">Column (horizontal position) of the tile [0 - num_cols - 1]</param>
        /// <param name="tile">Application-allocated Tile structure that will get the data</param>
        public void GetTile(int row, int col, out Tile tile)
        {
            bool ok = TLN_GetTilemapTile(ptr, row, col, out tile);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Copies rectangular blocks of tiles between two tilemaps
        /// </summary>
        /// <param name="srcRow">Starting row (vertical position) inside the source tilemap</param>
        /// <param name="srcCol">Starting column (horizontal position) inside the source tilemap</param>
        /// <param name="rows">Number of rows to copy</param>
        /// <param name="cols">Number of columns to copy</param>
        /// <param name="dst">Destination tilemap</param>
        /// <param name="dstRow">Starting row (vertical position) inside the target tilemap</param>
        /// <param name="dstCol">Starting column (horizontal position) inside the target tilemap</param>
        /// <remarks>Use this function to implement tile streaming</remarks>
        public void CopyTiles(int srcRow, int srcCol, int rows, int cols, Tilemap dst, int dstRow, int dstCol)
        {
            bool ok = TLN_CopyTiles(ptr, srcRow, srcCol, rows, cols, dst.ptr, dstRow, dstCol);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Deletes the tilemap and releases memory
        /// </summary>
        public void Dispose()
        {
            if (owned && ptr != IntPtr.Zero)
            {
                bool ok = TLN_DeleteTilemap(ptr);
                Engine.ThrowException(ok);
                ptr = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Tilemap()
        {
            Dispose();
        }
    }

    /// <summary>
    /// Palette resource
    /// </summary>
    public class Palette : ICloneable, IDisposable
    {
        internal IntPtr ptr;
        bool owned;

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_CreatePalette(int entries);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_LoadPalette(string filename);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_ClonePalette(IntPtr src);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetPaletteColor(IntPtr palette, int index, byte r, byte g, byte b);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_MixPalettes(IntPtr src1, IntPtr src2, IntPtr dst, byte factor);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_AddPaletteColor(IntPtr palette, byte r, byte g, byte b, byte start, byte num);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SubPaletteColor(IntPtr palette, byte r, byte g, byte b, byte start, byte num);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_ModPaletteColor(IntPtr palette, byte r, byte g, byte b, byte start, byte num);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_GetPaletteData(IntPtr palette, int index);

        [DllImport("Tilengine")]
        private static extern int TLN_GetPaletteNumColors(IntPtr palette);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_DeletePalette(IntPtr palette);

        /// <summary>
        /// Returns the number of colors in the palette
        /// </summary>
        public int NumColors
        {
            get { return TLN_GetPaletteNumColors(ptr); }
        }

        /// <summary>
        /// Internal constructor for creating a Palette from an existing resource pointer
        /// </summary>
        internal Palette (IntPtr res, bool owned = false)
        {
            ptr = res;
            this.owned = owned;
        }

        /// <summary>
        /// Creates a new palette with the specified number of entries.
        /// </summary>
        /// <param name="entries">Number of color entries, up to 256</param>
        public Palette(int entries)
        {
            IntPtr retval = TLN_CreatePalette(entries);
            Engine.ThrowException(retval != IntPtr.Zero);
            ptr = retval;
            owned = true;
        }

        /// <summary>
        /// Loads a palette from a standard .act file (Adobe Color Table)
        /// </summary>
        /// <param name="filename">ACT file containing the palette to load</param>
        /// <returns>Loaded palette</returns>
        public static Palette FromFile(string filename)
        {
            IntPtr retval = TLN_LoadPalette(filename);
            Engine.ThrowException(retval != IntPtr.Zero);
            return new Palette(retval, true);
        }

        /// <summary>
        /// Creates a deep copy of the palette
        /// </summary>
        /// <returns>Cloned object</returns>
        public object Clone()
        {
            IntPtr retval = TLN_ClonePalette(ptr);
            Engine.ThrowException(retval != IntPtr.Zero);
            return new Palette(retval, true);
        }

        /// <summary>
        /// Sets the RGB color value of a palette entry
        /// </summary>
        /// <param name="index">Index of the palette entry to modify (0-255)</param>
        /// <param name="color">RGB Color value</param>
        public void SetColor(int index, Color color)
        {
            bool ok = TLN_SetPaletteColor(ptr, index, color.R, color.G, color.B);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Mixes palette with a second palette using a factor.
        /// </summary>
        /// <param name="palette">Palette to mix with</param>
        /// <param name="factor">Mix factor [0-255], 0=100% source, 255=100% destination</param>
        /// <remarks>The original palette gets modified with the mix, no third palette is created</remarks>
        public void Mix(Palette palette, byte factor=128)
        {
            bool ok = TLN_MixPalettes(ptr, palette.ptr, ptr, factor);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Modifies a range of colors by adding the provided color value to the selected range. The result is always a brighter color.
        /// </summary>
        /// <param name="color">Color to add</param>
        /// <param name="first">index of the first color entry to modify</param>
        /// <param name="count">number of colors from start to modify</param>
        public void AddColor(Color color, byte first, byte count)
        {
            bool ok = TLN_AddPaletteColor(ptr, color.R, color.G, color.B, first, count);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Modifies a range of colors by subtracting the provided color value to the selected range. The result is always a darker color.
        /// </summary>
        /// <param name="color">Color to substract</param>
        /// <param name="first">index of the first color entry to modify</param>
        /// <param name="count">number of colors from start to modify</param>
        public void SubColor(Color color, byte first, byte count)
        {
            bool ok = TLN_SubPaletteColor(ptr, color.R, color.G, color.B, first, count);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Modifies a range of colors by modulating (normalized product) the provided color value to the selected range. The result is always a darker color.
        /// </summary>
        /// <param name="color">Color to modulate (multiply)</param>
        /// <param name="first">index of the first color entry to modify</param>
        /// <param name="count">number of colors from start to modify</param>
        public void MulColor(Color color, byte first, byte count)
        {
            bool ok =  TLN_ModPaletteColor(ptr, color.R, color.G, color.B, first, count);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Deletes palette and releases memory
        /// </summary>
        public void Dispose()
        {
            if (owned && ptr != IntPtr.Zero)
            {
                bool ok = TLN_DeletePalette(ptr);
                Engine.ThrowException(ok);
                ptr = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Palette()
        {
            Dispose();
        }
    }

    /// <summary>
    /// Bitmap resource
    /// </summary>
    public class Bitmap : ICloneable, IDisposable
    {
        internal IntPtr ptr;
        bool owned;

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_CreateBitmap(int width, int height, int bpp);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_LoadBitmap(string filename);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_CloneBitmap(IntPtr src);

        [DllImport("Tilengine")]
        private static extern int TLN_GetBitmapWidth(IntPtr bitmap);

        [DllImport("Tilengine")]
        private static extern int TLN_GetBitmapHeight(IntPtr bitmap);

        [DllImport("Tilengine")]
        private static extern int TLN_GetBitmapDepth(IntPtr bitmap);

        [DllImport("Tilengine")]
        private static extern int TLN_GetBitmapPitch(IntPtr bitmap);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_GetBitmapPtr(IntPtr bitmap, int x, int y);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_GetBitmapPalette(IntPtr bitmap);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_SetBitmapPalette(IntPtr bitmap, IntPtr palette);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_DeleteBitmap(IntPtr bitmap);

        /// <summary>
        /// Gets/sets Raw pixel data
        /// </summary>
        public byte[] PixelData
        {
            get
            {
                byte[] pixelData = new byte[Pitch * Height];
                IntPtr ptrPixelData = TLN_GetBitmapPtr(ptr, 0, 0);
                Marshal.Copy(ptrPixelData, pixelData, 0, pixelData.Length);
                return pixelData;
            }

            set
            {
                if (Pitch * Height != value.Length) { throw new ArgumentException(); }
                IntPtr ptrPixelData = TLN_GetBitmapPtr(ptr, 0, 0);
                Marshal.Copy(value, 0, ptrPixelData, value.Length);
            }
        }

        /// <summary>
        /// Returns with in pixels
        /// </summary>
        public int Width
        {
            get { return TLN_GetBitmapWidth(ptr); }
        }

        /// <summary>
        /// Returns height in pixels
        /// </summary>
        public int Height
        {
            get { return TLN_GetBitmapHeight(ptr); }
        }

        /// <summary>
        /// Returns bitmap depth in bits per pixel (bpp)
        /// </summary>
        public int Depth
        {
            get { return TLN_GetBitmapDepth(ptr); }
        }

        /// <summary>
        /// Returns the pitch (bytes per scanline) of the bitmap in bytes.
        /// </summary>
        public int Pitch
        {
            get { return TLN_GetBitmapPitch(ptr); }
        }

        /// <summary>
        /// Returns the Palette object of the bitmap
        /// </summary>
        public Palette Palette
        {
            get { return new Palette(TLN_GetBitmapPalette(ptr)); }
            set
            {
                bool ok = TLN_SetBitmapPalette(ptr, value.ptr);
                Engine.ThrowException(ok);
            }
        }

        /// <summary>
        /// Internal constructor for creating a Bitmap from an existing resource pointer
        /// </summary>
        internal Bitmap(IntPtr res, bool owned = false)
        {
            ptr = res;
            this.owned = owned;
        }

        /// <summary>
        /// Creates a new memory bitmap with the specified width, height and bits per pixel (bpp).
        /// </summary>
        /// <param name="width">Width in pixels</param>
        /// <param name="height">Height in pixels</param>
        /// <param name="bpp">Bits per pixel</param>
        public Bitmap(int width, int height, int bpp)
        {
            IntPtr retval = TLN_CreateBitmap(width, height, bpp);
            Engine.ThrowException(retval != IntPtr.Zero);
            ptr = retval;
            owned = true;
        }

        /// <summary>
        /// Loads a .png or .bmp bitmap from a file.
        /// </summary>
        /// <param name="filename">Path of the bitmap file to load</param>
        /// <returns>Created Bitmap object</returns>
        public static Bitmap FromFile(string filename)
        {
            IntPtr retval = TLN_LoadBitmap(filename);
            Engine.ThrowException(retval != IntPtr.Zero);
            return new Bitmap(retval, true);
        }

        /// <summary>
        /// Creates a deep copy of the bitmap
        /// </summary>
        /// <returns>Cloned object</returns>
        public object Clone()
        {
            IntPtr retval = TLN_CloneBitmap(ptr);
            Engine.ThrowException(retval != IntPtr.Zero);
            return new Bitmap(retval, true);
        }

        /// <summary>
        /// Deletes bitmap and releases memory
        /// </summary>
        public void Dispose()
        {
            if (owned && ptr != IntPtr.Zero)
            {
                bool ok = TLN_DeleteBitmap(ptr);
                Engine.ThrowException(ok);
                ptr = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Bitmap()
        {
            Dispose();
        }
    }

    /// <summary>
    /// ObjectList resource
    /// </summary>
    public class ObjectList : ICloneable, IDisposable
    {
        internal IntPtr ptr;
        bool owned;

        [DllImport("Tilengine")] 
        private static extern IntPtr TLN_CreateObjectList();

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_LoadObjectList(string filename, string layername);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_CloneObjectList(IntPtr src);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_AddTileObjectToList(IntPtr list, ushort id, ushort gid, ushort flags, int x, int y);

        [DllImport("Tilengine")]
        private static extern int TLN_GetListNumObjects(IntPtr list);

        // TODO implmentment this function to get the object info. TLN_GetListObject allows info to be null, but C# doesn't
        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_GetListObject(IntPtr list, out ObjectInfo info);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_DeleteObjectList(IntPtr list);

        /// <summary>
        /// Returns the number of objects in the list
        /// </summary>
        public int Length
        {
            get { return TLN_GetListNumObjects(ptr); }
        }

        /// <summary>
        /// Internal constructor for creating an ObjectList from an existing resource pointer
        /// </summary>
        internal ObjectList(IntPtr res, bool owned = false)
        {
            ptr = res;
            this.owned = owned;
        }

        /// <summary>
        /// Creates an empty object list.that must be populated with cref="ObjectList.Add" />
        /// </summary>
        public ObjectList()
        {
            IntPtr retval = TLN_CreateObjectList();
            Engine.ThrowException(retval != IntPtr.Zero);
            ptr = retval;
            owned = true;
        }

        /// <summary>
        /// Loads an object list from a Tiled object layer
        /// </summary>
        /// <param name="filename">Path of the .tmx file containing the list</param>
        /// <param name="layername">Name of the layer to load</param>
        /// <returns>Created object</returns>
        public static ObjectList FromFile(string filename, string layername)
        {
            IntPtr retval = TLN_LoadObjectList(filename, layername);
            Engine.ThrowException(retval != IntPtr.Zero);
            return new ObjectList(retval, true);
        }

        /// <summary>
        /// Creates a deep copy of the objects list
        /// </summary>
        /// <returns>Cloned object</returns>
        public object Clone()
        {
            IntPtr retval = TLN_CloneObjectList(ptr);
            Engine.ThrowException(retval != IntPtr.Zero);
            return new ObjectList(retval, true);
        }

        /// <summary>
        /// Adds an image-based tile (rectangle) item to the list
        /// </summary>
        /// <param name="id">Unique ID of the object inside the list</param>
        /// <param name="gid">Graphic Id (tile index) of the tileset object</param>
        /// <param name="flags">Combination of Flags</param>
        /// <param name="x">Layer-space horizontal coordinate of the top-left corner</param>
        /// <param name="y">Layer-space bertical coordinate of the top-left corner</param>
        public void Add(ushort id, ushort gid, ushort flags, int x, int y)
        {
            bool ok = TLN_AddTileObjectToList(ptr, id, gid, flags, x, y);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Deletes object list and releases memory
        /// </summary>
        public void Dispose()
        {
            if (owned && ptr != IntPtr.Zero)
            {
                bool ok = TLN_DeleteObjectList(ptr);
                Engine.ThrowException(ok);
                ptr = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~ObjectList()
        {
            Dispose();
        }
    }

    /// <summary>
    /// Sequence resource for layer, sprite and palette animations
    /// </summary>
    public class Sequence : ICloneable, IDisposable
    {
        internal IntPtr ptr;
        bool owned;

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_CreateSequence(string name, int target, int num_frames, SequenceFrame[] frames);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_CreateSpriteSequence(string name, IntPtr spriteset, string basename, int delay);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_CreateCycle(string name, int num_strips, ColorStrip[] strips);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_CloneSequence(IntPtr sequence);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_GetSequenceInfo(IntPtr sequence, out SequenceInfo info);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_DeleteSequence(IntPtr sequence);

        /// <summary>
        /// Internal constructor for creating a Sequence from an existing resource pointer
        /// </summary>
        /// <param name="res"></param>
        internal Sequence (IntPtr res, bool owned = false)
        {
            ptr = res;
            this.owned = owned;
        }

        /// <summary>
        /// Creates a new sequence for the tile animation engine
        /// </summary>
        /// <param name="name">String with an unique name to query later</param>
        /// <param name="target">For tileset animations, the tile index to animate</param>
        /// <param name="frames">Array of SequenceFrame structures, one for each keyframe</param>
        public Sequence(string name, int target, SequenceFrame[] frames)
        {
            IntPtr retval = TLN_CreateSequence(name, target, frames.Length, frames);
            Engine.ThrowException(retval != IntPtr.Zero);
            ptr = retval;
            owned = true;
        }

        /// <summary>
        /// Creates a color cycle sequence for palette animation
        /// </summary>
        /// <param name="name">String with an unique name to query later</param>
        /// <param name="strips">Array of ColorStrip structures, one for each color strip</param>
        public Sequence(string name, ColorStrip[] strips)
        {
            IntPtr retval = TLN_CreateCycle(name, strips.Length, strips);
            Engine.ThrowException(retval != IntPtr.Zero);
            ptr = retval;
            owned = true;
        }

        /// <summary>
        /// Creates a name-based spriteset sequence for sprite animation
        /// </summary>
        /// <param name="name">String with an unique name to query later</param>
        /// <param name="spriteset">Spriteset with the animation frames</param>
        /// <param name="basename">Base of the sprite name for the numbered sequence</param>
        /// <param name="delay">Number of ticks to delay between frames</param>
        public Sequence(string name, Spriteset spriteset, string basename, int delay)
        {
            IntPtr retval = TLN_CreateSpriteSequence(name, spriteset.ptr, basename, delay);
            Engine.ThrowException(retval != IntPtr.Zero);
            ptr = retval;
            owned = true;
        }

        /// <summary>
        /// Creates a deep copy of the sequence
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            IntPtr retval = TLN_CloneSequence(ptr);
            Engine.ThrowException(retval != IntPtr.Zero);
            return new Sequence(retval, true);
        }

        /// <summary>
        /// Returns information about the sequence
        /// </summary>
        /// <param name="info">Appliaction-allocated SequenceInfo structure that will receive the data</param>
        public void GetInfo(out SequenceInfo info)
        {
            bool ok = TLN_GetSequenceInfo(ptr, out info);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Deletes sequence and releases used memory
        /// </summary>
        public void Dispose()
        {
            if (owned && ptr != IntPtr.Zero)
            {
                bool ok = TLN_DeleteSequence(ptr);
                Engine.ThrowException(ok);
                ptr = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Sequence()
        {
            Dispose();
        }
    }

    /// <summary>
    /// SequencePack resource, container for Sequence objects
    /// </summary>
    public class SequencePack : IDisposable
    {
        internal IntPtr ptr;
        bool owned;

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_CreateSequencePack();

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_LoadSequencePack(string filename);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_FindSequence(IntPtr sp, string name);

        [DllImport("Tilengine")]
        private static extern IntPtr TLN_GetSequence(IntPtr sp, int index);

        [DllImport("Tilengine")]
        private static extern int TLN_GetSequencePackCount(IntPtr sp);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_AddSequenceToPack(IntPtr sp, IntPtr sequence);

        [DllImport("Tilengine")]
        [return: MarshalAsAttribute(UnmanagedType.I1)]
        private static extern bool TLN_DeleteSequencePack(IntPtr sp);

        /// <summary>
        /// Internal constructor for creating a SequencePack from an existing resource pointer
        /// </summary>
        internal SequencePack (IntPtr res, bool owned = false)
        {
            ptr = res;
            this.owned = owned;
        }

        /// <summary>
        /// Loads a sqx file containing one or more sequences
        /// </summary>
        /// <param name="filename">Path to sqx file with sequence descriptions</param>
        /// <returns>Created sequence pack</returns>
        public static SequencePack FromFile(string filename)
        {
            IntPtr retval = TLN_LoadSequencePack(filename);
            Engine.ThrowException(retval != IntPtr.Zero);
            return new SequencePack(retval, true);
        }

        /// <summary>
        /// Returns the number of sequences in the pack
        /// </summary>
        public int NumSequences
        {
            get { return TLN_GetSequencePackCount(ptr); }
        }

        /// <summary>
        /// Returns the sequence inside a sequence pack that matches with the given name
        /// </summary>
        /// <param name="name">Name of the sequence to find</param>
        /// <returns></returns>
        public Sequence GetSequence(string name)
        {
            IntPtr retval = TLN_FindSequence(ptr, name);
            Engine.ThrowException(retval != IntPtr.Zero);
            return new Sequence(retval);
        }

        /// <summary>
        /// Returns the sequence inside a sequence pack that has the index specified.
        /// </summary>
        /// <param name="index">Zero-based index of the sequence to find</param>
        /// <returns></returns>
        public Sequence GetSequence(int index)
        {
            IntPtr retval = TLN_GetSequence(ptr, index);
            Engine.ThrowException(retval != IntPtr.Zero);
            return new Sequence(retval);
        }

        /// <summary>
        /// Adds a sequence to a sequence pack
        /// </summary>
        /// <param name="sequence">Sequence to add to the pack</param>
        public void Add(Sequence sequence)
        {
            bool ok = TLN_AddSequenceToPack(ptr, sequence.ptr);
            Engine.ThrowException(ok);
        }

        /// <summary>
        /// Deletes sequence pack and releases used memory
        /// </summary>
        public void Dispose()
        {
            if (owned && ptr != IntPtr.Zero)
            {
                bool ok = TLN_DeleteSequencePack(ptr);
                Engine.ThrowException(ok);
                ptr = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~SequencePack()
        {
            Dispose();
        }
    }
}
