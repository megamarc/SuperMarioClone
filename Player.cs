/*
 * ****************************************************************************
 *  Super Mario Clone
 *      Tilengine based super mario implementation, written in C#
 *      Marc Palacios, 2016
 * ****************************************************************************
*/

using Tilengine;

/// <summary>
/// Class that holds player state and motion
/// </summary>
class Player
{
    private enum Orientation
    {
        None,
        Right,
        Left
    };

    static int timeMove = 15;               /* tiempo en el que se alcanza la velocidad de carrera, en frames */
    static int timeJump = 12;               /* tiempo en el que se alcanza la altura máxima de salto, en frames */
    static int timeFall = 30;               /* tiempo en el que se alcanza la velocidad de caída, en frames */
    static int runSpeed = Fixed.Set(3);     /* velocidad de carrera en píxeles/frame, fix */
    static int jumpSpeed = Fixed.Set(3.7f); /* velocidad inicial de salto, en píxeles/frame, fix */
    static int fallSpeed = Fixed.Set(7);    /* velocidad de caída libre en píxeles/frame, fix5 */
    static int dvx = runSpeed / timeMove;
    static int dvy = fallSpeed / timeFall;

    int x, y;
    int width, height;
    int t0Jump;
    int xspeed;
    int yspeed;
    int aspeed;
    int targetSpeed;
    bool jumping;
    bool floor;
    Orientation orientation;
    Spriteset spriteset;
    Sprite sprite;
    TileFlags flags;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">Base name of the spriteset (png/txt pair)</param>
    /// <param name="index">Sprite index</param>
    /// <param name="xpos">Initial x coordinate in world space</param>
    /// <param name="ypos">Initial y coordinate in world space</param>
    public Player(string name, int index, int xpos, int ypos)
    {
        flags = TileFlags.None;
        spriteset = Spriteset.FromFile(name);
        sprite = Graphics.Engine.Sprites[index];
        sprite.Setup(spriteset, flags);
        orientation = Orientation.Right;
        jumping = false;
        floor = true;
        x = Fixed.Set(xpos);
        y = Fixed.Set(ypos);

        SpriteInfo info; 
        spriteset.GetInfo(0, out info);
        width = info.W;
        height = info.H;
    }

    /// <summary>
    /// Releases unmanaged resources
    /// </summary>
    public void Delete()
    {
        spriteset.Delete();
    }

    /// <summary>
    /// Returns horizontal position in world space
    /// </summary>
    public int X
    {
        get { return Fixed.Get(x); }
    }

    /// <summary>
    /// Returns vertical position in world space
    /// </summary>
    public int Y
    {
        get { return Fixed.Get(y); }
    }

    /// <summary>
    /// Update state, must be called once in each game loop
    /// </summary>
    /// <param name="world"></param>
    /// <param name="frame"></param>
    public void Update(World world, int frame)
    {
        Orientation motion = Orientation.None;
        
        /* andar o correr */
        if (floor)
        {
            if (Graphics.Window.GetInput(Input.Button_A))
            {
                targetSpeed = runSpeed;
                aspeed = 4;
            }
            else
            {
                targetSpeed = runSpeed / 2;
                aspeed = 6;
            }
        }

        /* andar a la derecha */
        if (Graphics.Window.GetInput(Input.Right))
        {
            motion = orientation = Orientation.Right;
            xspeed += dvx;
            if (xspeed > targetSpeed)
                xspeed = targetSpeed;
        }

        /* andar a la izquierda */
        else if (Graphics.Window.GetInput(Input.Left))
        {
            motion = orientation = Orientation.Left;
            xspeed -= dvx;
            if (xspeed < -targetSpeed)
                xspeed = -targetSpeed;
        }

        /* saltar */
        if (Graphics.Window.GetInput(Input.Button_B))
        {
            if (!jumping && floor)
            {
                jumping = true;
                t0Jump = frame;
            }
        }
        else if (jumping)
            jumping = false;

        /* impulso */
        if (jumping && frame - t0Jump < timeJump)
            yspeed = -jumpSpeed;

        /* gravedad */
        if (!floor)
        {
            yspeed += dvy;
            if (yspeed > fallSpeed)
                yspeed = fallSpeed;
        }

        /* sin mover en el suelo: frenado */
        if (motion == Orientation.None && floor)
        {
            if (xspeed > 0)
            {
                xspeed -= dvx;
                if (xspeed < 0)
                    xspeed = 0;
            }
            else if (xspeed < 0)
            {
                xspeed += dvx;
                if (xspeed > 0)
                    xspeed = 0;
            }
        }

        int oldx = Fixed.Get(x);
        int oldy = Fixed.Get(y);
        
        x += xspeed;
        y += yspeed;

        int intx = Fixed.Get(x);
        int inty = Fixed.Get(y);
        int tmpx = intx;
        int tmpy = inty;

        /* ajusta posición final */
        if (intx < 0)
        {
            intx = 0;
            xspeed = 0;
        }
        else if (intx > world.Width - 16)
        {
            intx = world.Width - 16;
            xspeed = 0;
        }

        /* salto */
        if (inty < oldy)
        {
            floor = false;
            int[] points = { 4, 12 };
            int c;
            for (c = 0; c < points.Length; c++)
            {
                World.Tile tile = world.GetTile(intx + points[c], inty);
                if (tile.Type == World.Type.Solid || tile.Type == World.Type.Question)
                {
                    inty = (inty + 16) & ~15;
                    yspeed = 0;
                    t0Jump = 0;
                    if (tile.Type == World.Type.Question)
                        world.SetTile(tile.Row, tile.Col, 51);
                }
                else if (tile.Type == World.Type.Coin)
                {
                    world.ClearTile(tile.Row, tile.Col);
                    Game.AddCoin();
                }
            }
        }

        /* reposo/caída */
        else
        {
            int[] points = { 4, 12 };
            floor = false;
            int c;
            for (c = 0; c < points.Length; c++)
            {
                World.Tile tile = world.GetTile(intx + points[c], inty + height);
                if (tile.Type == World.Type.Solid || tile.Type == World.Type.OneWay || tile.Type == World.Type.Question)
                {
                    inty &= ~15;
                    floor = true;
                    yspeed = 0;
                }
                else if (tile.Type == World.Type.Coin)
                {
                    world.ClearTile(tile.Row, tile.Col);
                    Game.AddCoin();
                }
            }
        }
        
        /* izquierda */
        if (intx < oldx)
        {
            int[] points = {0,8,16,24,31};
            int c;
            for (c = 0; c < points.Length; c++)
            {
                World.Tile tile = world.GetTile(intx, inty + points[c]);
                if (tile.Type == World.Type.Solid || tile.Type == World.Type.Question)
                {
                    intx = (intx + 16) & ~15;
                    xspeed = 0;
                }
                else if (tile.Type == World.Type.Coin)
                {
                    world.ClearTile(tile.Row, tile.Col);
                    Game.AddCoin();
                }
            }
        }

        /* derecha */
        else if (intx > oldx)
        {
            int[] points = { 0, 8, 16, 24, 31 };
            int c;
            for (c = 0; c < points.Length; c++)
            {
                World.Tile tile = world.GetTile(intx + width, inty + points[c]);
                if (tile.Type == World.Type.Solid || tile.Type == World.Type.Question)
                {
                    intx &= ~15;
                    xspeed = 0;
                }
                else if (tile.Type == World.Type.Coin)
                {
                    world.ClearTile(tile.Row, tile.Col);
                    Game.AddCoin();
                }
            }
        }

        /* corrige si ha cambiado */
        if (tmpx != intx)
            x = Fixed.Set(intx);
        if (tmpy != inty)
            y = Fixed.Set(inty);

        /* actualiza mundo */
        if (xspeed > 0)
        {
            if (intx - world.X > 160)
                world.X = intx - 160;
            if (world.X + Graphics.Hres > world.Width)
                world.X = world.Width - Graphics.Hres;
        }
        else if (xspeed < 0)
        {
            if (intx - world.X < 120)
                world.X = intx - 120;
            if (world.X < 0)
                world.X = 0;
        }

        /* dibuja */
        if (orientation == Orientation.Right)
            flags &= ~TileFlags.FlipX;
        else if (orientation == Orientation.Left)
            flags |= TileFlags.FlipX;
        sprite.Flags = flags;

        sprite.SetPosition(intx - world.X, inty - world.Y);

        /* parado */
        if (xspeed==0 && yspeed==0)
            sprite.Picture = 0;
        /* en salto */
        else if (yspeed < 0)
            sprite.Picture = 7;
        /* en caída */
        else if (yspeed > 0)
            sprite.Picture = 8;
        /* en carrera */
        else if (orientation == Orientation.Right && xspeed > 0 || orientation == Orientation.Left && xspeed < 0)
            sprite.Picture = (frame / aspeed) % 3;
        /* frenado */
        else
            sprite.Picture = 9;
    }
}
