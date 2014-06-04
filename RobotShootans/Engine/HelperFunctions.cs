using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotShootans.Engine
{
    /// <summary>
    /// 
    /// </summary>
    public static class HelperFunctions
    {
        /// <summary>
        /// Will check if the vector is within the bounds and if so, returns new Vector Value
        /// </summary>
        /// <param name="vectorIn">The vector to check</param>
        /// <param name="minX">Minimum X value</param>
        /// <param name="maxX">Maximum X value</param>
        /// <param name="minY">Minimum Y value</param>
        /// <param name="maxY">Maximum Y value</param>
        /// <returns>Corrected vector</returns>
        public static Vector2 KeepVectorInBounds(Vector2 vectorIn, int minX, int maxX, int minY, int maxY)
        {
            if (vectorIn.X < minX)
                vectorIn.X = minX;
            if (vectorIn.X > maxX)
                vectorIn.X = maxX;
            if (vectorIn.Y < minY)
                vectorIn.Y = minY;
            if (vectorIn.Y > maxY)
                vectorIn.Y = maxY;

            return vectorIn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="returnDegrees"></param>
        /// <returns></returns>
        public static float GetBearingBetweenTwoPoints(Vector2 p1, Vector2 p2, bool returnDegrees = true)
        {
            float x1 = p1.X;
            float y1 = p1.Y;
            float x2 = p2.X;
            float y2 = p2.Y;

            double a, o;

            if (y1 < y2)
                a = y2 - y1;
            else
                a = y1 - y2;

            if (x1 < x2)
                o = x2 - x1;
            else
                o = x1 - x2;

            float theta = (float)Math.Atan(o / a);

            theta = theta * (180.0f / 3.142f);

            if (y1 < y2)
            {
                // south of player
                if (x1 > x2)
                    // east of player
                    theta = 180.0f + theta;
                else
                    // west of player
                    theta = 180.0f - theta;
            }
            else
            {
                // north of player
                if (x1 > x2)
                {
                    theta = 360.0f - theta;
                }
            }

            if (returnDegrees)
                return theta;
            else
                return theta * (3.142f / 180.0f);
        }

        /// <summary>
        /// Returns a Vector2 with a speedX and speedY given a bearing
        /// </summary>
        /// <param name="bearingIn"></param>
        /// <param name="speedIn"></param>
        /// <returns></returns>
        public static Vector2 GetVectorFromBearingAndSpeed(double bearingIn, double speedIn)
        {
            Vector2 result = Vector2.Zero;

            double theta = bearingIn;
            double h = speedIn;
            int Xmulti = 1;
            int Ymulti = 1;

            if(bearingIn % 90.0 == 0)
            {
                if(bearingIn == 0.0 || bearingIn == 360.0)
                {
                    result.X = 0f;
                    result.Y = (float)-h;
                }
                else if(bearingIn == 180.0)
                {
                    result.X = 0f;
                    result.Y = (float)h;
                }
                if (bearingIn == 90.0)
                {
                    result.X = (float)h;
                    result.Y = 0f;
                }
                else if (bearingIn == 270.0)
                {
                    result.X = (float)-h;
                    result.Y = 0f;
                }
            }
            else
            {
                if(bearingIn > 0.0 && bearingIn < 90.0)
                {
                    theta = bearingIn;
                    Ymulti = -1;
                }
                else if(bearingIn > 90.0 && bearingIn < 180.0)
                {
                    theta = bearingIn - 90.0;
                }
                else if(bearingIn > 180.0 && bearingIn < 270.0)
                {
                    theta = bearingIn - 180.0;
                    Xmulti = -1;
                }
                else if(bearingIn > 270.0 && bearingIn < 360.0)
                {
                    theta = bearingIn - 270.0;
                    Xmulti = -1;
                    Ymulti = -1;
                }

                double o = Math.Cos(theta) * h;
                double a = Math.Sin(theta) * h;

                result.X = (float)(a * Xmulti);
                result.Y = (float)(o * Ymulti);
            }

            return result;
        }

        /// <summary>
        /// Returns distance between two points
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static double GetDistanceBetweenTwoPoints(Vector2 point1, Vector2 point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }
    }
}
