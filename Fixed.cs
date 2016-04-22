/*
 * ****************************************************************************
 *  Super Mario Clone
 *      Tilengine based super mario implementation, written in C#
 *      Marc Palacios, 2016
 * ****************************************************************************
*/

/// <summary>
/// Helper for managing fixed point decimal values
/// </summary>
struct Fixed
{
    private const int factor = 10;

    /// <summary>
    /// Builds fixed point from integer
    /// </summary>
    public static int Set(int value)
    {
        return value << factor;
    }

    /// <summary>
    /// Builds fixed point from float
    /// </summary>    
    public static int Set(float value)
    {
        return (int)(value * (1 << factor));
    }

    /// <summary>
    /// Converts fixed point back to integer
    /// </summary>    
    public static int Get(int value)
    {
        return value >> factor;
    }
}
