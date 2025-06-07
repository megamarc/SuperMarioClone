/*
 * ****************************************************************************
 *  Super Mario Clone
 *      Tilengine based super mario implementation, written in C#
 *      Marc Palacios, 2016-2025
 * ****************************************************************************
*/

using Tilengine;

/// <summary>
/// Effect of coin vanishing
/// </summary>
class Collect : Actor
{
    Sprite sprite;

    public Collect(World world, int x, int y)
    {
        this.x = x;
        this.y = y;
        sprite = Graphics.Engine.GetAvailableSprite();
        sprite.Setup(Game.objects, SpriteFlags.None);
        UpdateSprite(world);
    }

    public override void Update(World world)
    {
        base.Update(world);
        if (frame < 32)
            UpdateSprite(world);
        else
            Dispose();
    }

    public override void Dispose()
    {
        base.Dispose();
        sprite.Disable();
    }

    void UpdateSprite(World world)
    {
        sprite.Picture = 4 + (frame >> 2);
        sprite.SetPosition(x - world.X, y - world.Y);
    }
}