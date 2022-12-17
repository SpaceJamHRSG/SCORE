using System;
using UnityEngine;

namespace Utility
{
    public static class MotionUtil
    {
        public static float Pendulum(float t, float variation, float speed, float phase)
        {
            return variation * Mathf.Sin(t * speed + phase * (float)Math.PI);
        }
    }
}