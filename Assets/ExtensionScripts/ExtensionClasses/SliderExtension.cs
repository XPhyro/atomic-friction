using UnityEngine.UI;

namespace Extension.Native
{
    public static class SliderExtension
    {
        /// <summary>
        /// Sets the boundaries of the slider, then sets the value of it.
        /// </summary>
        /// <param name="slider"></param>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public static void SetValues(this Slider slider, float value, float min, float max)
        {
            slider.minValue = min;
            slider.maxValue = max;
            slider.value = value;
        }
    }
}
