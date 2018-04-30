using UnityEngine;

public struct ColoredPoint
{
    public Vector3 pos;
    public Color color;

    public ColoredPoint(float x, float y, float z, float r, float g, float b)
    {
        this.pos = new Vector3(x, y, z);
        this.color = new Color(r, g, b);
    }

    public ColoredPoint(Vector3 pos, Color color)
    {
        this.pos = pos;
        this.color = color;
    }

    public static Vector3 operator- (ColoredPoint a, ColoredPoint b)
    {
        return a.pos - b.pos;
    }
}
