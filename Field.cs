/*
 * ****************************************************************************
 *  Super Mario Clone
 *      Tilengine based super mario implementation, written in C#
 *      Marc Palacios, 2016-2025
 * ****************************************************************************
*/

using Tilengine;

/// <summary>
/// Helper for managing a related tileset/tilemap pair
/// </summary>
struct Field
{
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
    public Field(int index, string filename)
    {
        Layer = Graphics.Engine.Layers[index];
        Tilemap = Tilemap.FromFile(filename, null);
        Layer.Tilemap = Tilemap;
        Graphics.Engine.SetBackgroundColor(Tilemap);
        Width = Layer.Width;
        Height = Layer.Height;
    }

    /// <summary>
    /// Releases unmanaged resources
    /// </summary>
    public void Dispose()
    {
        Tilemap.Dispose();
    }

    /// <summary>
    /// Sets a tile in the tilemap
    /// </summary>
    public void SetTile(int row, int col, ushort index)
    {
        Tilengine.Tile tile;
        tile.flags = 0;
        tile.index = (ushort)(index + 1);
        Tilemap.SetTile(row, col, ref tile);
    }

    /// <summary>
    /// Clears a tile in the tilemap as empty
    /// </summary>
    public void ClearTile(int row, int col)
    {
        Tilengine.Tile tile;
        tile.flags = 0;
        tile.index = 0;
        Tilemap.SetTile(row, col, ref tile);
    }
}
