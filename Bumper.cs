/*
 * ****************************************************************************
 *  Super Mario Clone
 *      Tilengine based super mario implementation, written in C#
 *      Marc Palacios, 2016
 * ****************************************************************************
*/

using Tilengine;

/// <summary>
/// Effect of a hitted block from behind bumping a bit
/// </summary>
class Bumper : Actor
{
    int row, col;
    ushort tileIndex;
    Sprite sprite;
    static int[] motion = { 1, 2, 3, 4, 3, 2, 1 };

    public Bumper(World world, Spriteset spriteset, int picture, int row, int col, ushort tileIndex)
    {
        this.row = row;
        this.col = col;
        this.tileIndex = tileIndex;
        frame = 0;

        world.ClearTile(row, col);

        sprite = Graphics.Engine.GetAvailableSprite();
        sprite.Setup(spriteset, TileFlags.None);
        sprite.Picture = picture;
        UpdateSprite(world);
    }
    
    public override void Update(World world)
    {
        base.Update(world);
        if (frame < motion.Length)
            UpdateSprite(world);
        else
        {
            sprite.Disable();
            world.SetTile(row, col, tileIndex);
            Delete();
        }
    }

    public override void Delete()
    {
        base.Delete();
    }

    void UpdateSprite(World world)
    {
        sprite.SetPosition((col * 16) - world.X, (row * 16) - motion[frame] - world.Y);
    }
}