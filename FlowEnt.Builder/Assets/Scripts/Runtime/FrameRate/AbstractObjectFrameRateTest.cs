using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Builder
{
    public abstract class AbstractObjectFrameRateTest : AbstractFrameRateTest
    {
        protected AbstractObjectFrameRateTest(FrameRateTestController controller, float testTime, int testAmount) : base(controller, testTime, testAmount)
        {
        }

        private readonly List<Transform> transforms = new();
        protected List<Transform> Transforms => transforms;
        public override void Load()
        {
            for (int i = 0; i < internalTestAmount; i++)
            {
                Transform cube = Controller.CreateCube();
                cube.transform.position = new Vector3(0, 0, i);
                transforms.Add(cube);
            }
        }

        public override void Unload()
        {
            for (int i = 0; i < transforms.Count; i++)
            {
                Object.Destroy(transforms[i].gameObject);
            }
            transforms.Clear();
        }
    }
}
