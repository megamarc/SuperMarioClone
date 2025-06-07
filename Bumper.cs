/*
 * ****************************************************************************
 *  Super Mario Clone
 *      Tilengine based super mario implementation, written in C#
 *      Marc Palacios, 2016-2025
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
        this.x = col * 16;
        this.y = row * 16;
        this.row = row;
        this.col = col;
        this.tileIndex = tileIndex;
        frame = 0;

        world.ClearTile(row, col);

        sprite = Graphics.Engine.GetAvailableSprite();
        sprite.Setup(spriteset, SpriteFlags.None);
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
            Dispose();
        }
    }

    public override void Dispose()
    {
        base.Dispose();
    }

    void UpdateSprite(World world)
    {
        sprite.SetPosition(x - world.X, y - motion[frame] - world.Y);
    }
}