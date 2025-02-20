using Microsoft.Xna.Framework;


/// <summary>
/// Classe généré pricipalement par ChatGPT // Foncticonne comme Rectangle mais représente une sphere
/// </summary>
public struct Circle
{
    #region ChatGPT
    public Point Center { get; set; }
    public float Radius { get; set; }

    public Circle(Point center, float radius)
    {
        Center = center;
        Radius = radius;
    }

    // Permet de récupérer le diamètre
    public float Diameter => Radius * 2;

    /// <summary>
    /// Si le point est contenu dans la sphere
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public bool Contains(Point point)
    {
        return Vector2.DistanceSquared(Center.ToVector2(), point.ToVector2()) <= Radius * Radius;
    }

    /// <summary>
    /// Si le cercle en param est en collsion avec celui ci
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Intersects(Circle other)
    {
        float distanceSquared = Vector2.DistanceSquared(Center.ToVector2(), other.Center.ToVector2());
        float radiusSum = Radius + other.Radius;
        return distanceSquared <= (radiusSum * radiusSum);
    }

    /// <summary>
    /// Si le rectangle en param est en collsion avec ce cercle
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Intersects(Rectangle rect)
    {
        // Trouver le point le plus proche dans le rectangle
        float closestX = MathHelper.Clamp(Center.X, rect.Left, rect.Right);
        float closestY = MathHelper.Clamp(Center.Y, rect.Top, rect.Bottom);

        // Vérifier la distance entre ce point et le centre du cercle
        float distanceSquared = (Center.X - closestX) * (Center.X - closestX) +
                                (Center.Y - closestY) * (Center.Y - closestY);

        return distanceSquared <= (Radius * Radius);
    }
    #endregion
}