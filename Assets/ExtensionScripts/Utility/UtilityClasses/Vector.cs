using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is extremely slow compared to the built-in Vector structs; as this is not a struct, but a class
/// </summary>
public class VectorX : IEquatable<VectorX>
{
    public const float kEpsilon = 1e-5f;
    public const float kEpsilonNormalSqrt = 1e-15f;

    public float x { get; private set; }
    public float y { get; private set; }
    public float z { get; private set; }
    public float w { get; private set; }

    public VectorX()
    {

    }
    public VectorX(float x)
    {
        this.x = x;
    }
    public VectorX(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public VectorX(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public VectorX(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    public VectorX(Vector2 xy)
    {
        x = xy.x;
        y = xy.y;
    }
    public VectorX(Vector2 xy, float z)
    {
        x = xy.x;
        y = xy.y;
        this.z = z;
    }
    public VectorX(Vector2 xy, float z, float w)
    {
        x = xy.x;
        y = xy.y;
        this.z = z;
        this.w = w;
    }
    public VectorX(float x, Vector2 yz)
    {
        this.x = x;
        y = yz.x;
        z = yz.y;
    }
    public VectorX(float x, Vector2 yz, float w)
    {
        this.x = x;
        y = yz.x;
        z = yz.y;
        this.w = w;
    }
    public VectorX(float x, float y, Vector2 zw)
    {
        this.x = x;
        this.y = y;
        z = zw.x;
        w = zw.y;
    }
    public VectorX(Vector2 xy, Vector2 zw)
    {
        x = xy.x;
        y = xy.y;
        z = zw.x;
        z = zw.y;
    }
    public VectorX(Vector3 xyz)
    {
        x = xyz.x;
        y = xyz.y;
        z = xyz.z;
    }
    public VectorX(Vector3 xyz, float w)
    {
        x = xyz.x;
        y = xyz.y;
        z = xyz.z;
        this.w = w;
    }
    public VectorX(float x, Vector3 yzw)
    {
        this.x = x;
        y = yzw.x;
        z = yzw.y;
        w = yzw.z;
    }
    public VectorX(Vector4 xyzw)
    {
        x = xyzw.x;
        y = xyzw.y;
        z = xyzw.z;
        w = xyzw.w;
    }

    public float this[int i]
    {
        get { return components4[i]; }

        private set
        {
            switch(i)
            {
                case 0:
                    x = value;
                    break;
                case 1:
                    y = value;
                    break;
                case 2:
                    z = value;
                    break;
                case 3:
                    w = value;
                    break;
                default:
                    break;
            }
        }
    }

    public static readonly VectorX one = new VectorX(1, 1, 1, 1);
    public static readonly VectorX zero = new VectorX(0, 0, 0, 0);
    public static readonly VectorX positiveInfinity = new VectorX(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
    public static readonly VectorX negativeInfinity = new VectorX(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

    public VectorX normalized
    {
        get { return this / magnitude4; }
    }

    public Vector2 xy
    {
        get
        {
            return new Vector2(x, y);
        }
    }
    public Vector3 xyz
    {
        get
        {
            return new Vector3(x, y, z);
        }
    }
    public Vector4 xyzw
    {
        get
        {
            return new Vector4(x, y, z, w);
        }
    }

    public float[] components2
    {
        get { return new float[2] { x, y }; }
    }
    public float[] components3
    {
        get { return new float[3] { x, y, z }; }
    }
    public float[] components4
    {
        get { return new float[4] { x, y, z, w }; }
    }

    public float magnitude2
    {
        get { return Mathf.Sqrt(x * x + y * y); }
    }
    public float sqrMagnitude2
    {
        get { return x * x + y * y; }
    }
    public float magnitude3
    {
        get { return Mathf.Sqrt(x * x + y * y + z * z); }
    }
    public float sqrMagnitude3
    {
        get { return x * x + y * y + z * z; }
    }
    public float magnitude4
    {
        get { return Mathf.Sqrt(x * x + y * y + z * z + w * w); }
    }
    public float sqrMagnitude4
    {
        get { return x * x + y * y + z * z + w * w; }
    }

    public static float Distance(VectorX v1, VectorX v2)
    {
        return Mathf.Sqrt((v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y) + (v1.z - v2.z) * (v1.z - v2.z) + (v1.w - v2.w) * (v1.w - v2.w));
    }
    public static float SqrDistance(VectorX v1, VectorX v2)
    {
        return (v1.x - v2.x) * (v1.x - v2.x) + (v1.y - v2.y) * (v1.y - v2.y) + (v1.z - v2.z) * (v1.z - v2.z) + (v1.w - v2.w) * (v1.w - v2.w);
    }
    public static float Dot(VectorX v1, VectorX v2)
    {
        Debug.LogWarning("Not fully implemented");
        return Vector4.Dot(v1, v2);
    }
    public static VectorX Lerp(VectorX from, VectorX to, float t)
    {
        return new VectorX(to.x - from.x, to.y - from.y, to.z - from.z, to.w - from.w) * Mathf.Clamp01(t);
    }
    public static VectorX LerpUnclamped(VectorX from, VectorX to, float t)
    {
        return new VectorX(to.x - from.x, to.y - from.y, to.z - from.z, to.w - from.w) * t;
    }
    public static VectorX Slerp(VectorX from, VectorX to, float t)
    {
        throw new NotImplementedException();
    }
    public static VectorX SlerpUnclamped(VectorX from, VectorX to, float t)
    {
        throw new NotImplementedException();
    }
    public static float Magnitude2(VectorX v)
    {
        return v.magnitude2;
    }
    public static float Magnitude3(VectorX v)
    {
        return v.magnitude3;
    }
    public static float Magnitude4(VectorX v)
    {
        return v.magnitude4;
    }
    public static float SqrMagnitude2(VectorX v)
    {
        return v.sqrMagnitude2;
    }
    public static float SqrMagnitude3(VectorX v)
    {
        return v.sqrMagnitude3;
    }
    public static float SqrMagnitude4(VectorX v)
    {
        return v.sqrMagnitude4;
    }
    public static VectorX Max(VectorX v1, VectorX v2)
    {
        return new VectorX(Mathf.Max(v1.x, v2.x), Mathf.Max(v1.y, v2.y), Mathf.Max(v1.z, v2.z), Mathf.Max(v1.w, v2.w));
    }
    public static VectorX Min(VectorX v1, VectorX v2)
    {
        return new VectorX(Mathf.Min(v1.x, v2.x), Mathf.Min(v1.y, v2.y), Mathf.Min(v1.z, v2.z), Mathf.Min(v1.w, v2.w));
    }
    public static VectorX MoveTowards(VectorX current, VectorX target, float maxDistanceDelta)
    {
        Debug.LogWarning("Not fully implemented");
        return Vector4.MoveTowards(current, target, maxDistanceDelta);
    }
    public static VectorX Normalize(VectorX v)
    {
        return v / v.magnitude4;
    }
    public static VectorX Project(VectorX v1, VectorX v2)
    {
        Debug.LogWarning("Not fully implemented");
        return Vector4.Project(v1, v2);
    }
    public static VectorX Scale(VectorX v1, VectorX v2)
    {
        return new VectorX(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z, v1.w * v2.w);
    }
    public static float ScaleRatio2(VectorX v1, VectorX v2)
    {
        return v1.magnitude2 / v2.magnitude2;
    }
    public static float ScaleRatio3(VectorX v1, VectorX v2)
    {
        return v1.magnitude3 / v2.magnitude3;
    }
    public static float ScaleRatio4(VectorX v1, VectorX v2)
    {
        return v1.magnitude4 / v2.magnitude4;
    }
    public void Normalize()
    {
        var m = magnitude4;
        x /= m;
        y /= m;
        z /= m;
        w /= m;
    }
    public void Scale(VectorX v)
    {
        x *= v.x;
        y *= v.y;
        z *= v.z;
        w *= v.w;
    }
    public void Set(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    public override string ToString()
    {
        return "(" + x + ", " + y + ", " + z + ", " + w + ")";
    }

    /// <summary>
    /// This method should be optimized before use
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        return Equals(obj as VectorX);
    }
    /// <summary>
    /// This method should be optimized before use
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(VectorX other)
    {
        return other != null &&
               x == other.x &&
               y == other.y &&
               z == other.z &&
               w == other.w &&
               EqualityComparer<VectorX>.Default.Equals(normalized, other.normalized) &&
               xy.Equals(other.xy) &&
               xyz.Equals(other.xyz) &&
               xyzw.Equals(other.xyzw) &&
               EqualityComparer<float[]>.Default.Equals(components2, other.components2) &&
               EqualityComparer<float[]>.Default.Equals(components3, other.components3) &&
               EqualityComparer<float[]>.Default.Equals(components4, other.components4) &&
               magnitude2 == other.magnitude2 &&
               sqrMagnitude2 == other.sqrMagnitude2 &&
               magnitude3 == other.magnitude3 &&
               sqrMagnitude3 == other.sqrMagnitude3 &&
               magnitude4 == other.magnitude4 &&
               sqrMagnitude4 == other.sqrMagnitude4;
    }
    /// <summary>
    /// This method should be optimized before use
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        var hashCode = 1329552699;
        hashCode = hashCode * -1521134295 + x.GetHashCode();
        hashCode = hashCode * -1521134295 + y.GetHashCode();
        hashCode = hashCode * -1521134295 + z.GetHashCode();
        hashCode = hashCode * -1521134295 + w.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<VectorX>.Default.GetHashCode(normalized);
        hashCode = hashCode * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(xy);
        hashCode = hashCode * -1521134295 + EqualityComparer<Vector3>.Default.GetHashCode(xyz);
        hashCode = hashCode * -1521134295 + EqualityComparer<Vector4>.Default.GetHashCode(xyzw);
        hashCode = hashCode * -1521134295 + EqualityComparer<float[]>.Default.GetHashCode(components2);
        hashCode = hashCode * -1521134295 + EqualityComparer<float[]>.Default.GetHashCode(components3);
        hashCode = hashCode * -1521134295 + EqualityComparer<float[]>.Default.GetHashCode(components4);
        hashCode = hashCode * -1521134295 + magnitude2.GetHashCode();
        hashCode = hashCode * -1521134295 + sqrMagnitude2.GetHashCode();
        hashCode = hashCode * -1521134295 + magnitude3.GetHashCode();
        hashCode = hashCode * -1521134295 + sqrMagnitude3.GetHashCode();
        hashCode = hashCode * -1521134295 + magnitude4.GetHashCode();
        hashCode = hashCode * -1521134295 + sqrMagnitude4.GetHashCode();
        return hashCode;
    }

    public static VectorX operator +(VectorX vx, Vector2 v2)
    {
        return new VectorX(vx.x + v2.x, vx.y + v2.y, vx.z, vx.w);
    }
    public static VectorX operator -(VectorX vx, Vector2 v2)
    {
        return new VectorX(vx.x - v2.x, vx.y - v2.y, vx.z, vx.w);
    }
    public static VectorX operator +(VectorX vx, Vector3 v3)
    {
        return new VectorX(vx.x + v3.x, vx.y + v3.y, vx.z + v3.z, vx.w);
    }
    public static VectorX operator -(VectorX vx, Vector3 v3)
    {
        return new VectorX(vx.x - v3.x, vx.y - v3.y, vx.z - v3.z, vx.w);
    }
    public static VectorX operator +(VectorX vx, Vector4 v4)
    {
        return new VectorX(vx.x + v4.x, vx.y + v4.y, vx.z + v4.z, vx.w + v4.w);
    }
    public static VectorX operator -(VectorX vx, Vector4 v4)
    {
        return new VectorX(vx.x - v4.x, vx.y - v4.y, vx.z - v4.z, vx.w - v4.w);
    }
    public static VectorX operator +(VectorX v1, VectorX v2)
    {
        return new VectorX(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z, v1.w + v2.w);
    }
    public static VectorX operator -(VectorX v1, VectorX v2)
    {
        return new VectorX(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z, v1.w - v2.w);
    }
    public static VectorX operator *(VectorX v, float f)
    {
        return new VectorX(v.x * f, v.y * f, v.z * f, v.w * f);
    }
    public static VectorX operator /(VectorX v, float f)
    {
        return new VectorX(v.x / f, v.y / f, v.z / f, v.w / f);
    }
    public static bool operator ==(VectorX v1, VectorX v2)
    {
        for(int i = 0; i < v1.components4.Length; i++)
        {
            if(v1.components4[i] != v2.components4[i])
            {
                return false;
            }
        }

        return true;
    }
    public static bool operator !=(VectorX v1, VectorX v2)
    {
        for(int i = 0; i < v1.components4.Length; i++)
        {
            if(v1.components4[i] == v2.components4[i])
            {
                return false;
            }
        }

        return true;
    }
    public static bool operator >(VectorX v1, VectorX v2)
    {
        return v1.sqrMagnitude4 > v2.sqrMagnitude4 ? true : false;
    }
    public static bool operator <(VectorX v1, VectorX v2)
    {
        return v1.sqrMagnitude4 < v2.sqrMagnitude4 ? true : false;
    }
    public static bool operator >=(VectorX v1, VectorX v2)
    {
        return v1.sqrMagnitude4 == v2.sqrMagnitude4 || v1.sqrMagnitude4 > v2.sqrMagnitude4 ? true : false;
    }
    public static bool operator <=(VectorX v1, VectorX v2)
    {
        return v1.sqrMagnitude4 == v2.sqrMagnitude4 || v1.sqrMagnitude4 < v2.sqrMagnitude4 ? true : false;
    }

    public static explicit operator Vector2(VectorX v)
    {
        return v.xy;
    }
    public static explicit operator Vector3(VectorX v)
    {
        return v.xyz;
    }
    public static implicit operator Vector4(VectorX v)
    {
        return v.xyzw;
    }
    public static implicit operator VectorX(Vector2 v)
    {
        return new VectorX(v);
    }
    public static implicit operator VectorX(Vector3 v)
    {
        return new VectorX(v);
    }
    public static implicit operator VectorX(Vector4 v)
    {
        return new VectorX(v);
    }
}
