using Microsoft.Xna.Framework;
using System;

namespace BuildAndDestroy.GameComponents.GameObjects.Pathfinding
{
    /// <summary>
    /// Permet de gérér le pathfinding d'un monstre ou du joueur (pour l'instant il ne va qu'en ligne droite)
    /// </summary>
    public class Path
    {
        Point currentPos;
        Point end;

        /// <summary>
        /// Retourn la destination du trajet
        /// </summary>
        public Point Destination { get { return end; }  }

        public Path(Point currentPos, Point end) 
        {
            this.currentPos = currentPos;
            this.end = end;
        }

        /// <summary>
        /// Permet d'acctualisé la position acctuelle
        /// </summary>
        /// <param name="pos"></param>
        public void UpdateCurrentPos(Point pos)
        {
            currentPos = pos;
        }

        /// <summary>
        /// Obtient la direction dans laquel il faut se rendre
        /// </summary>
        /// <returns></returns>
        public Vector2 GetDirection()
        {
            int dx = end.X - currentPos.X;
            int dy = end.Y - currentPos.Y;

            if (dx == 0 && dy == 0)
            {
                return Vector2.Zero; 
            }
            Vector2 dir = new Vector2(dx, dy);
            dir.Normalize();
            return dir;
        }

        /// <summary>
        /// Obtient la distance à parcourire
        /// </summary>
        /// <returns></returns>
        public float GetDitance()
        {
            float X = currentPos.X - end.X;
            float Y = currentPos.Y - end.Y;

            return MathF.Sqrt(X*X + Y*Y);

        }
    }
}
