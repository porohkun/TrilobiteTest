using System;
using UnityEngine;

[Serializable]
public struct Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public static bool operator ==(Point p1, Point p2)
    {
        return p1.X == p2.X && p1.Y == p2.Y;
    }

    public static bool operator !=(Point p1, Point p2)
    {
        return p1.X != p2.X || p1.Y != p2.Y;
    }

    public static Point operator +(Point p1, Point p2)
    {
        return new Point(p1.X + p2.X, p1.Y + p2.Y);
    }

    public static Point operator -(Point p1, Point p2)
    {
        return new Point(p1.X - p2.X, p1.Y - p2.Y);
    }

    public static Vector2 operator *(Point p, float m)
    {
        return new Vector2(p.X * m, p.Y * m);
    }

    public static implicit operator Vector3(Point p)
    {
        return new Vector3(p.X, 0f, p.Y);
    }

    public override bool Equals(object Point)
    {
        return this == (Point)Point;
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() + Y.GetHashCode();
    }

    public override string ToString()
    {
        return String.Format("[{0}; {1}]", X, Y);
    }
}
