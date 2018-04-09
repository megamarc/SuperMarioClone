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
    Field foreground;   // foreground layer
    Field background;   // background layer
        
    public int Width;
    public int Height;
    public int X;
    public int Y;   

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
        public ushort Index;
        public int Row;
        public int Col;
        public Type Type;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="foregroundName">TMX file with the foreground map</param>
    /// <param name="backgroundName">TMX file with the background map</param>
    public World(string foregroundFile, string backgroundFile)
    {
        foreground = new Field(Game.LayerForeground, foregroundFile);
        background = new Field(Game.LayerBackground, backgroundFile);

        Width = foreground.Width;
        Height = foreground.Height;
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
        tile.Type = (Type)tileInfo.Type;
        return tile;
    }

    public void SetTile(int row, int col, ushort index)
    {
        foreground.SetTile(row, col, index);
    }

    public void ClearTile(int row, int col)
    {
        foreground.ClearTile(row, col);
    }
}