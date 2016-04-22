/*
 * ****************************************************************************
 *  Super Mario Clone
 *      Tilengine based super mario implementation, written in C#
 *      Marc Palacios, 2016
 * ****************************************************************************
*/

using Tilengine;

/// <summary>
/// Helper for managing a related tileset/tilemap pair
/// </summary>
struct Field
{
    public Tileset Tileset;
    public Tilemap Tilemap;
    public Layer Layer;
    public int Width;
    public int Height;

    /// <summary>
    /// Main constructir
    /// </summary>
    /// <param name="layer">Layer index</param>
    /// <param name="filename">Base filename of the tmx/tsx/png</param>
    /// <param name="layername">Layer name inside the tmx file</param>
    public Field(int index, string filename, string layername)
    {
        Layer = Graphics.Engine.Layers[index];
        Tileset = Tileset.FromFile(filename + ".tsx");
        Tilemap = Tilemap.FromFile(filename + ".tmx", layername);
        Layer.Setup(Tileset, Tilemap);
        Width = Tilemap.Cols * Tileset.Width;
        Height = Tilemap.Rows * Tileset.Height;
    }

    /// <summary>
    /// Releases unmanaged resources
    /// </summary>
    public void Delete()
    {
        Tileset.Delete();
        Tilemap.Delete();
    }

    /// <summary>
    /// Sets a tile in the tilemap
    /// </summary>
    public bool SetTile(int row, int col, ushort index)
    {
        Tilengine.Tile tile;
        tile.flags = 0;
        tile.index = (ushort)(index + 1);
        return Tilemap.SetTile(row, col, ref tile);
    }

    /// <summary>
    /// Clears a tile in the tilemap as empty
    /// </summary>
    public bool ClearTile(int row, int col)
    {
        Tilengine.Tile tile;
        tile.flags = 0;
        tile.index = 0;
        return Tilemap.SetTile(row, col, ref tile);
    }
}