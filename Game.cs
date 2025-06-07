/*
 * ****************************************************************************
 *  Super Mario Clone
 *      Tilengine based super mario implementation, written in C#
 *      Marc Palacios, 2016-2025
 * ****************************************************************************
*/

using Tilengine;

/// <summary>
/// Static struct that holds all game-related data
/// </summary>
struct Game
{
    // various layer indexes
    public const int LayerHUD = 0;
    public const int LayerForeground = 1;
    public const int LayerBackground = 2;
    public const int NumLayers = 3;

    static World world;     // world instance
    static Player player;   // player instance
    public static Spriteset objects;

    static int Lives;       // remaining lives
    static int Time;        // remaining time
    static int Coins;       // collected coins
    static int Score;       // score
    static int frameCount;  // counter for decreasing time

    /// <summary>
    /// Initializes game and loads unmanaged resources
    /// </summary>
    public static void Init()
    {
        Lives = 5;
        Time = 255;
        Coins = 0;
        Score = 0;
        frameCount = 0;

        Actor.CreateList(40);

        HUD.Init();
        HUD.ShowLives(Lives);
        HUD.ShowTime(Time);
        HUD.ShowCoins(Coins);
        HUD.ShowScore(Score);

        world = new World("smw_foreground.tmx", "smw_background.tmx");
        objects = Spriteset.FromFile("Objects");
        player = new Player("SuperMario", 0, 16, 16);
    }

    /// <summary>
    /// Releases unmanaged resources
    /// </summary>
    public static void Deinit()
    {
        HUD.Deinit();
        world.Dispose();
        objects.Dispose();
        Actor.DeleteList();
        player = null;
        world = null;
    }
    
    /// <summary>
    /// Must be called once for each frame in the game loop
    /// </summary>
    public static void Update(int frame)
    {
        frameCount++;
        if (frameCount == 30)
        {
            frameCount = 0;
            if (Time > 0)
                Time--;
            HUD.ShowTime(Time);
        }

        world.Update(frame);
        Actor.UpdateList(world);
    }

    /// <summary>
    /// Collects one coin
    /// </summary>
    public static void AddCoin()
    {
        Coins++;
        HUD.ShowCoins(Coins);
    }

    /// <summary>
    /// Increases score
    /// </summary>
    public static void AddScore(int value)
    {
        Score++;
        HUD.ShowScore(Score);
    }
}