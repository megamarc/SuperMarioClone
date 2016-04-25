/*
 * ****************************************************************************
 *  Super Mario Clone
 *      Tilengine based super mario implementation, written in C#
 *      Marc Palacios, 2016
 * ****************************************************************************
*/

using Tilengine;

/// <summary>
/// Effect of coin vanishing
/// </summary>
class Collect : Actor
{
    Sprite sprite;
    int sx, sy;

    public Collect(World world, int x, int y)
    {
        sx = x;
        sy = y;
        sprite = Graphics.Engine.GetAvailableSprite();
        sprite.Setup(Game.objects, TileFlags.None);
        UpdateSprite(world);
    }

    public override void Update(World world)
    {
        base.Update(world);
        if (frame < 32)
            UpdateSprite(world);
        else
            Delete();
    }

    public override void Delete()
    {
        base.Delete();
        sprite.Disable();
    }

    void UpdateSprite(World world)
    {
        sprite.Picture = 4 + (frame >> 2);
        sprite.SetPosition(sx - world.X, sy - world.Y);
    }
}