using System;
using UnityEngine;

namespace Extension.Native
{
    public static class VectorExtension
    {
        public static Vector3 SetByIndex(this Vector3 v, int component, float value)
        {
            switch(component)
            {
                case 0:
                    v.x = value;
                    break;
                case 1:
                    v.y = value;
                    break;
                case 2:
                    v.z = value;
                    break;
                default:
                    break;
            }

            return v;
        }

        public static Vector3 SetByIndex(this Vector3 v, int component0, float value0, int component1, float value1)
        {
            if(component0 == component1)
            {
                throw new ArgumentException("component0 cannot be equal to component1");
            }

            switch(component0)
            {
                case 0:
                    v.x = value0;
                    break;
                case 1:
                    v.y = value0;
                    break;
                case 2:
                    v.z = value0;
                    break;
                default:
                    break;
            }

            switch(component1)
            {
                case 0:
                    v.x = value1;
                    break;
                case 1:
                    v.y = value1;
                    break;
                case 2:
                    v.z = value1;
                    break;
                default:
                    break;
            }

            return v;
        }

        /// <summary>
        /// Use this only if you want the Vector3 to be returned, else use Vector3.Set instead.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Vector3 SetByIndex(this Vector3 v, float x, float y, float z)
        {
            v.x = x;
            v.y = y;
            v.z = z;
            return v;
        }

        public static Vector3 Scale(this Vector3 v, float x, float y, float z)
        {
            v.Scale(new Vector3(x, y, z));
            return v;
        }

        #region Does not work properly (At least when axes are combined)
        /// <summary>
        /// Rotates vector v around the Z axis by angle radians.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector3 RotateZ(this Vector3 v, float angle)
        {
            var cos = Mathf.Cos(angle);
            var sin = Mathf.Sin(angle);

            var x = v.x;
            var y = v.y;
            var z = v.z;

            return new Vector3(x * cos - y * sin, x * sin + y * cos, z);
        }

        ///// <summary>
        ///// Rotates vector v around the Y axis by angle radians.
        ///// </summary>
        ///// <param name="v"></param>
        ///// <param name="angle"></param>
        ///// <returns></returns>
        //public static Vector3 RotateY(this Vector3 v, float angle)
        //{
        //    var cos = Mathf.Cos(angle);
        //    var sin = Mathf.Sin(angle);

        //    var x = v.x;
        //    var y = v.y;
        //    var z = v.z;

        //    return new Vector3(x * cos - z * sin, y, x * sin + z * cos);
        //}

        ///// <summary>
        ///// Rotates vector v around the X axis by angle radians.
        ///// </summary>
        ///// <param name="v"></param>
        ///// <param name="angle"></param>
        ///// <returns></returns>
        //public static Vector3 RotateX(this Vector3 v, float angle)
        //{
        //    var cos = Mathf.Cos(angle);
        //    var sin = Mathf.Sin(angle);

        //    var x = v.x;
        //    var y = v.y;
        //    var z = v.z;

        //    return new Vector3(x, z * sin + y * cos, z * cos - y * sin);
        //}

        ///// <summary>
        ///// Rotates vector v around the X, Y and Z axes by a, b and c radians respectively.
        ///// </summary>
        ///// <param name="v"></param>
        ///// <param name="a"></param>
        ///// <param name="b"></param>
        ///// <param name="c"></param>
        ///// <returns></returns>
        //public static Vector3 Rotate(this Vector3 v, float a, float b, float c)
        //{
        //    var cosa = Mathf.Cos(a);
        //    var sina = Mathf.Sin(a);
        //    var cosb = Mathf.Cos(b);
        //    var sinb = Mathf.Sin(b);
        //    var cosc = Mathf.Cos(c);
        //    var sinc = Mathf.Sin(c);

        //    var x = v.x;
        //    var y = v.y;
        //    var z = v.z;

        //    //var vx = new Vector3(x * cosa - y * sina, x * sina + y * cosa, z);
        //    //var vy = new Vector3(x * cosb - z * sinb, y, x * sinb + z * cosb);
        //    //var vz = new Vector3(x, z * sinc + y * cosc, z * cosc - y * sinc);

        //    return new Vector3((x * cosa - y * sina) * cosb - z * sinb, (x * sina + y * cosa) * cosc + z * sinc, (x * sinb + z * cosb) * cosc - y * sinc);
        //}

        ///// <summary>
        ///// Rotates v by angle in radians.
        ///// </summary>
        ///// <param name="v"></param>
        ///// <param name="angle"></param>
        ///// <returns></returns>
        //public static Vector2 Rotate(this Vector2 v, float angle)
        //{
        //    var x = v.x;
        //    var y = v.y;
        //    var cos = Mathf.Cos(angle);
        //    var sin = Mathf.Sin(angle);

        //    return new Vector2(x * cos - y * sin, x * sin + y * cos);
        //}

        ///// <summary>
        ///// Rotates vector v around the Z axis by angle radians.
        ///// </summary>
        ///// <param name="v"></param>
        ///// <param name="angle"></param>
        ///// <returns></returns>
        //public static Vector3 RotateZ(this Vector2 v, Vector3 origin, float angle)
        //{
        //    var cos = Mathf.Cos(angle);
        //    var sin = Mathf.Sin(angle);

        //    var ox = origin.x;
        //    var oy = origin.y;

        //    var x = v.x - ox;
        //    var y = v.y - oy;

        //    return new Vector3(x * cos - y * sin + ox, x * sin + y * cos + oy, 0f);
        //}

        ///// <summary>
        ///// Rotates vector v around the Y axis by angle radians.
        ///// </summary>
        ///// <param name="v"></param>
        ///// <param name="angle"></param>
        ///// <returns></returns>
        //public static Vector3 RotateY(this Vector2 v, Vector3 origin, float angle)
        //{
        //    var cos = Mathf.Cos(angle);
        //    var sin = Mathf.Sin(angle);

        //    var ox = origin.x;
        //    var oz = origin.z;

        //    var x = v.x - ox;
        //    var z = -oz;

        //    return new Vector3(x * cos - z * sin + ox, v.y, x * sin + z * cos + oz);
        //}

        ///// <summary>
        ///// Rotates vector v around the X axis by angle radians.
        ///// </summary>
        ///// <param name="v"></param>
        ///// <param name="angle"></param>
        ///// <returns></returns>
        //public static Vector3 RotateX(this Vector2 v, Vector3 origin, float angle)
        //{
        //    var cos = Mathf.Cos(angle);
        //    var sin = Mathf.Sin(angle);

        //    var oy = origin.y;
        //    var oz = origin.z;

        //    var y = v.y - oy;
        //    var z = -oz;

        //    return new Vector3(v.x, z * sin + y * cos + oy, z * cos - y * sin + oz);
        //}

        ///// <summary>
        ///// Rotates vector v around the X, Y and Z axes by a, b and c radians respectively; assuming v.z=0.
        ///// </summary>
        ///// <param name="v"></param>
        ///// <param name="a"></param>
        ///// <param name="b"></param>
        ///// <param name="c"></param>
        ///// <returns></returns>
        //public static Vector3 Rotate3D(this Vector2 v, float a, float b, float c)
        //{
        //    var cosa = Mathf.Cos(a);
        //    var sina = Mathf.Sin(a);
        //    var cosb = Mathf.Cos(b);
        //    var sinb = Mathf.Sin(b);
        //    var cosc = Mathf.Cos(c);
        //    var sinc = Mathf.Sin(c);

        //    var x = v.x;
        //    var y = v.y;
        //    var z = 0f;

        //    return new Vector3((x * cosa - y * sina) * cosb,
        //                        (x * sina + y * cosa) * cosc,
        //                        x * sinb * cosc - y * sinc);
        //}

        ///// <summary>
        ///// Rotates vector v around the X, Y and Z axes by a, b and c radians respectively; assuming v.z=0.
        ///// </summary>
        ///// <param name="v"></param>
        ///// <param name="a"></param>
        ///// <param name="b"></param>
        ///// <param name="c"></param>
        ///// <returns></returns>
        //public static Vector3 Rotate3D(this Vector2 v, Vector3 origin, float a, float b, float c)
        //{
        //    var cosa = Mathf.Cos(a);
        //    var sina = Mathf.Sin(a);
        //    var cosb = Mathf.Cos(b);
        //    var sinb = Mathf.Sin(b);
        //    var cosc = Mathf.Cos(c);
        //    var sinc = Mathf.Sin(c);

        //    var ox = origin.x;
        //    var oy = origin.y;
        //    var oz = origin.z;

        //    var x = v.x - ox;
        //    var y = v.y - oy;
        //    var z = 0f - oz;

        //    return new Vector3((x * cosa - y * sina) * cosb - z * sinb + ox,
        //                        (x * sina + y * cosa) * cosc + z * sinc + oy,
        //                        (x * sinb + z * cosb) * cosc - y * sinc + oz);
        //} 
        #endregion

        ///// <summary>
        ///// Rotates v by a, b and c in 3D space around origin..
        ///// </summary>
        ///// <param name="v"></param>
        ///// <param name="a"></param>
        ///// <param name="b"></param>
        ///// <param name="c"></param>
        ///// <returns></returns>
        //public static Vector3 Rotate3D(this Vector2 v, Vector3 origin, float a, float b, float c)
        //{
        //    var cosa = Mathf.Cos(a);
        //    var sina = Mathf.Sin(a);
        //    var cosb = Mathf.Cos(b);
        //    var sinb = Mathf.Sin(b);
        //    var cosc = Mathf.Cos(c);
        //    var sinc = Mathf.Sin(c);

        //    //var cab = cosc * sina * sinb;

        //    //return new Vector3(-y * cab,
        //    //                    -x * cab,
        //    //                    y * cab - x * (cosc * cosa * sinb + sinc * sina * cosb));

        //    var x = v.x - origin.x;
        //    var y = v.y - origin.y;
        //    var z = -origin.z;

        //    x = (x * cosb - z * sinb) * cosc - y * sinc;
        //    y = (y * cosa - z * sina) * cosc + x * sinc;
        //    z = (z * cosa + y * sina) * cosb + x * sinb;

        //    return new Vector3(x + origin.x, y + origin.y, z + origin.z);
        //}
    }
}
