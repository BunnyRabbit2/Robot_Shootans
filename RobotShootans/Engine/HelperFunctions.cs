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
    }
}
