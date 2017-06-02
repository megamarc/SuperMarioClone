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
class Player : Actor
{
    enum Orientation
    {
        None,
        Right,
        Left
    };

    enum State
    {
        Stand,
        Walk,
        Jump,
        Fall,
        Crouch
    };

    static int timeMove = 15;               /* tiempo en el que se alcanza la velocidad de carrera, en frames */
    static int timeJump = 12;               /* tiempo en el que se alcanza la altura máxima de salto, en frames */
    static int timeFall = 30;               /* tiempo en el que se alcanza la velocidad de caída, en frames */
    static int runSpeed = Fixed.Set(3);     /* velocidad de carrera en píxeles/frame, fix */
    static int jumpSpeed = Fixed.Set(3.7f); /* velocidad inicial de salto, en píxeles/frame, fix */
    static int fallSpeed = Fixed.Set(7);    /* velocidad de caída libre en píxeles/frame, fix5 */
    static int dvx = runSpeed / timeMove;
    static int dvy = fallSpeed / timeFall;

    int x_fix, y_fix;
    int t0Jump;
    int xspeed;
    int yspeed;
    int aspeed;
    int targetSpeed;
    bool jumping;
    bool floor;
    State state;
    Orientation orientation;
    Spriteset spriteset;
    Sprite sprite;
    TileFlags flags;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">Base name of the spriteset (png/txt pair)</param>
    /// <param name="index">Sprite index</param>
    /// <param name="x">Initial x coordinate in world space</param>
    /// <param name="y">Initial y coordinate in world space</param>
    public Player(string name, int index, int x, int y)
    {
        this.x = x;
        this.y = y;

        flags = TileFlags.None;
        spriteset = Spriteset.FromFile(name);
        sprite = Graphics.Engine.Sprites[index];
        sprite.Setup(spriteset, flags);
        orientation = Orientation.Right;
        jumping = false;
        floor = true;
        x_fix = Fixed.Set(x);
        y_fix = Fixed.Set(y);
        state = State.Stand;

        SpriteInfo info; 
        spriteset.GetInfo(0, out info);
        width = info.W;
        height = info.H;
    }

    /// <summary>
    /// Releases unmanaged resources
    /// </summary>
    public override void Delete()
    {
        spriteset.Delete();
        base.Delete();
    }

    /// <summary>
    /// Update state, must be called once in each game loop
    /// </summary>
    /// <param name="world"></param>
    /// <param name="frame"></param>
    public override void Update(World world)
    {
        Orientation motion = Orientation.None;

        base.Update(world);

        switch (state)
        {
            case State.Stand:
                break;

            case State.Walk:
                break;

            case State.Jump:
                break;

            case State.Fall:
                break;

            case State.Crouch:
                break;
        }
        
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
        else if (floor && xspeed > 0)
        {
            xspeed -= dvx;
            if (xspeed < 0)
                xspeed = 0;
        }

        /* andar a la izquierda */
        if (Graphics.Window.GetInput(Input.Left))
        {
            motion = orientation = Orientation.Left;
            xspeed -= dvx;
            if (xspeed < -targetSpeed)
                xspeed = -targetSpeed;
        }
        else if (floor && xspeed < 0)
        {
            xspeed += dvx;
            if (xspeed > 0)
                xspeed = 0;
        }

        /* iniciar salto */
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

        /* salto */
        if (jumping && frame - t0Jump < timeJump)
            yspeed = -jumpSpeed;

        /* gravedad */
        else if (!floor)
        {
            yspeed += dvy;
            if (yspeed > fallSpeed)
                yspeed = fallSpeed;
        }

        /* posicion actual */
        int oldx = Fixed.Get(x_fix);
        int oldy = Fixed.Get(y_fix);
        
        /* actualiza posicion */
        x_fix += xspeed;
        y_fix += yspeed;
        x = Fixed.Get(x_fix);
        y = Fixed.Get(y_fix);

        int tmpx = x;
        int tmpy = y;

        /* ajusta posición final */
        if (x < 0)
        {
            x = 0;
            xspeed = 0;
        }
        else if (x > world.Width - 16)
        {
            x = world.Width - 16;
            xspeed = 0;
        }

        /* salto */
        if (y < oldy)
        {
            floor = false;
            int[] points = { 4, 12 };
            int c;
            for (c = 0; c < points.Length; c++)
            {
                World.Tile tile = world.GetTile(x + points[c], y);
                if (tile.Type == World.Type.Solid || tile.Type == World.Type.Question)
                {
                    y = (y + 16) & ~15;
                    yspeed = 0;
                    t0Jump = 0;
                    if (tile.Type == World.Type.Question)
                        new Bumper(world, Game.objects, 0, tile.Row, tile.Col, 51);
                }
                else if (tile.Type == World.Type.Coin)
                {
                    world.ClearTile(tile.Row, tile.Col);
                    new Collect(world, tile.Col * 16, tile.Row * 16);
                    Game.AddCoin();
                }
            }
        }

        /* reposo/caída */
        else
        {
            int[] points = { 4, 12 };
            int c;
            World.Tile tile;
            floor = false;
            for (c = 0; c < points.Length; c++)
            {
                tile = world.GetTile(x + points[c], y + height);
                if (tile.Type == World.Type.Solid || tile.Type == World.Type.OneWay || tile.Type == World.Type.Question)
                {
                    y &= ~15;
                    floor = true;
                    yspeed = 0;
                }
                else if (tile.Type == World.Type.Coin)
                {
                    world.ClearTile(tile.Row, tile.Col);
                    new Collect(world, tile.Col * 16, tile.Row * 16);
                    Game.AddCoin();
                }
            }
        }
        
        /* izquierda */
        if (x < oldx)
        {
            int[] points = {0,8,16,24,31};
            int c;
            for (c = 0; c < points.Length; c++)
            {
                World.Tile tile = world.GetTile(x, y + points[c]);
                if (tile.Type == World.Type.Solid || tile.Type == World.Type.Question)
                {
                    x = (x + 16) & ~15;
                    xspeed = 0;
                }
                else if (tile.Type == World.Type.Coin)
                {
                    world.ClearTile(tile.Row, tile.Col);
                    new Collect(world, tile.Col * 16, tile.Row * 16);
                    Game.AddCoin();
                }
            }
        }

        /* derecha */
        else if (x > oldx)
        {
            int[] points = { 0, 8, 16, 24, 31 };
            int c;
            for (c = 0; c < points.Length; c++)
            {
                World.Tile tile = world.GetTile(x + width, y + points[c]);
                if (tile.Type == World.Type.Solid || tile.Type == World.Type.Question)
                {
                    x &= ~15;
                    xspeed = 0;
                }
                else if (tile.Type == World.Type.Coin)
                {
                    world.ClearTile(tile.Row, tile.Col);
                    new Collect(world, tile.Col * 16, tile.Row * 16);
                    Game.AddCoin();
                }
            }
        }

        /* reajusta fix si se ha corregido */
        if (tmpx != x)
            x_fix = Fixed.Set(x);
        if (tmpy != y)
            y_fix = Fixed.Set(y);

        /* actualiza mundo */
        if (xspeed > 0)
        {
            if (x - world.X > 160)
                world.X = x - 160;
            if (world.X + Graphics.Hres > world.Width)
                world.X = world.Width - Graphics.Hres;
        }
        else if (xspeed < 0)
        {
            if (x - world.X < 120)
                world.X = x - 120;
            if (world.X < 0)
                world.X = 0;
        }

        /* dibuja */
        if (orientation == Orientation.Right)
            flags &= ~TileFlags.FlipX;
        else if (orientation == Orientation.Left)
            flags |= TileFlags.FlipX;
        sprite.Flags = flags;

        sprite.SetPosition(x - world.X, y - world.Y);

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
