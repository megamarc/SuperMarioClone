/*
 * ****************************************************************************
 *  Super Mario Clone
 *      Tilengine based super mario implementation, written in C#
 *      Marc Palacios, 2016
 * ****************************************************************************
*/

/// <summary>
/// Base class for any actor entity. Derived classes must override Update() and Delete() methods
/// </summary>
abstract class Actor
{
    static Actor[] list;
    int index;

    protected int x, y;
    protected int width, height;
    protected int frame;

    /// <summary>
    /// Creates the main list of actors
    /// </summary>
    public static void CreateList(int size)
    {
        list = new Actor[size];
    }

    /// <summary>
    /// Calls the "Update" method on all active actors
    /// </summary>
    public static void UpdateList(World world)
    {
        foreach (Actor actor in list)
        {
            if (actor != null)
                actor.Update(world);
        }
    }

    /// <summary>
    /// Adds an actor to the list
    /// </summary>
    /// <param name="actor"></param>
    public static bool Add(Actor actor)
    {
        for (int c = 0; c < list.Length; c++)
        {
            if (list[c] == null)
            {
                actor.index = c;
                list[c] = actor;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Removes an actor from the list
    /// </summary>
    public static void Remove(Actor actor)
    {
        list[actor.index] = null;
    }

    /// <summary>
    /// Calls the "Delete" method on all actors and deletes the list
    /// </summary>
    public static void DeleteList()
    {
        foreach (Actor actor in list)
        {
            if (actor != null)
                actor.Delete();
        }
        list = null;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public Actor()
    {
        frame = 0;
        Actor.Add(this);
    }
    
    /// <summary>
    /// Called on each frame. Must be overriden by derived classes
    /// </summary>
    public virtual void Update(World world)
    {
        frame++;
    }

    /// <summary>
    /// Called on actor deletion. Must be overriden by derived classes
    /// </summary>
    public virtual void Delete()
    {
        Actor.Remove(this);
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
}
