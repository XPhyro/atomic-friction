using System;
using UnityEngine;

public struct Maths
{
    public static float Avg(float x, float y)
    {
        return (x + y) / 2;
    }
    
    public static float LerpLinear(float from, float to, float speed, float deltaTime)
    {
        var maxDiff = to - from;
        var requiredTime = Mathf.Abs(maxDiff / speed);

        if(Mathf.Approximately(maxDiff, 0) || requiredTime < deltaTime)
        {
            return to;
        }

        var delta = Mathf.Abs(speed) * Mathf.Sign(maxDiff) * deltaTime;
        var result = to + delta;

        return result;
    }

    public static float IntegrateTrapezoidal(Func<float, float> func, float lower, float upper, uint stepCount)
    {
        var h = Mathf.Abs(upper - lower) / stepCount;
        var area = (func(lower) + func(upper)) / 2;

        var x = lower + h;
        while (x < upper)
        {
            area += func(x);
            x += h;
        }

        return area * h;
    }

    public static float Differentiate(Func<float, float> func, float origin, float step)
    {
        return (func(origin + step) - func(origin - step)) / (2 * step);
    }
}
