namespace FriedSynapse.FlowEnt
{
    public class BounceEasing : IEasing
    {
        public BounceEasing(int bounces)
        {
            this.bounces = bounces;

            float bit = 1f / bounces;
            float index = bit * 0.5f;
            float coeficient = 1f + index;
            peaks = new float[bounces];
            for (int i = 0; i < bounces; i++)
            {
                float root = (coeficient * index) - coeficient;
                peaks[i] = root * root;
                index += bit;
            }
        }

        private readonly float[] peaks;
        private readonly int bounces;
        public float GetValue(float t)
        {
            float scaledT = t * bounces;
            int segment = (int)scaledT;
            if (segment >= bounces)
            {
                segment = bounces - 1;
            }
            float root = (2f * bounces * t) - ((segment * 2f) + 1f);
            return (-(root * root) + 1) * peaks[segment];
        }
    }
}
