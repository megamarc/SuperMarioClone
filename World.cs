/*
 * ****************************************************************************
 *  Super Mario Clone
 *      Tilengine based super mario implementation, written in C#
 *      Marc Palacios, 2016
 * ****************************************************************************
*/
using Tilengine;

/// <summary>
/// Class that holds world state
/// </summary>
class World
{
    const int delay = 10;
    static int[] coinSequence = { 48, 56, 57, 58, 57};
    static int[] questionSequence = {49, 60, 61, 62, 63};

    Field foreground;   // foreground layer
    Field background;   // background layer
    Type[] tiles;       // maps tile indexes to their type
    
    public int Width;
    public int Height;
    public int X;
    public int Y;   

    int dueframe;
    int step;

    /* tipos de tiles */
    public enum Type : byte
    {
        None,
        Solid,
        OneWay,
        Coin,
        Question,
        Spin,
    }

    /* info de tile */
    public struct Tile
    {
        public int Index;
        public int Row;
        public int Col;
        public Type Type;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="foregroundName">Base name of the foreground layer files (tmx/tsx/png)</param>
    /// <param name="backgroundName">Base name of the background layer files </param>
    public World(string foregroundName, string backgroundName)
    {
        foreground = new Field(Game.LayerForeground, foregroundName, "Layer 1");
        background = new Field(Game.LayerBackground, backgroundName, "Layer 1");

        Width = foreground.Width;
        Height = foreground.Height;
        tiles = new Type[64];
        tiles[8] = tiles[10] = tiles[11] = tiles[12] = tiles[31] = tiles[45] = tiles[46] = tiles[47] = tiles[50] = tiles[51] = Type.Solid;
        tiles[32] = tiles[33] = Type.OneWay;
        tiles[48] = Type.Coin;
        tiles[49] = Type.Question;
        X = 0;
        Y = 48;
    }

    /// <summary>
    /// Release unmanaged resources
    /// </summary>
    public void Delete()
    {
        foreground.Delete();
        background.Delete();
    }

    /// <summary>
    /// Update world state, must be called once in each game loop
    /// </summary>
    /// <param name="frame"></param>
    public void Update(int frame)
    {
        foreground.Layer.SetPosition(X, Y);
        background.Layer.SetPosition(X / 2, 80);

        if (frame >= dueframe)
        {
            dueframe = frame + delay;
            Tileset tileset = foreground.Tileset;
            tileset.CopyTile(coinSequence[step%(coinSequence.Length - 1) + 1], coinSequence[0]);
            tileset.CopyTile(questionSequence[step%(questionSequence.Length - 1) + 1], questionSequence[0]);
            step++;
        }        
    }

    /// <summary>
    /// Returns tile info at specified world coordinates
    /// </summary>
    public Tile GetTile(int x, int y)
    {
        TileInfo tileInfo;
        Tile tile;
        foreground.Layer.GetTileInfo(x, y, out tileInfo);

        tile.Index = tileInfo.Index;
        tile.Row = tileInfo.Row;
        tile.Col = tileInfo.Col;
        if (tileInfo.Index != 0)
            tile.Type = tiles[tileInfo.Index - 1];
        else
            tile.Type = Type.None;
        return tile;
    }

    public bool SetTile(int row, int col, ushort index)
    {
        return foreground.SetTile(row, col, index);
    }

    public bool ClearTile(int row, int col)
    {
        return foreground.ClearTile(row, col);
    }
}