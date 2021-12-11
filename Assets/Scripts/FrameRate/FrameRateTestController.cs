using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Builder
{
    [Flags]
    public enum TestType
    {
        None = 0x000,
        Tween = 0x001,
        Flow = 0x002,
        FlowTween = 0x004,
        Transform = 0x008,
        DoTweenTween = 0x010,
        DoTweenSequence = 0x020,
        DoTweenTransform = 0x040,
        OfficialList = Tween | Flow | Transform | DoTweenTween | DoTweenSequence | DoTweenTransform,
    }

    public class FrameRateTestController : MonoBehaviour
    {
#pragma warning disable IDE0044, RCS1169
        [SerializeField]
        private TestType tests;

        [SerializeField]
        private int count;

        [SerializeField]
        private float testTime;

        [SerializeField]
        private Transform frameRateAnchor;

        [SerializeField]
        private Transform cube;
#pragma warning restore IDE0044, RCS1169

        private int framesCount;
        public int FramesCount => framesCount;

#pragma warning disable IDE0051, RCS1213
        private async void Start()
        {
            Transform frameRateAnchorInstance = Instantiate(frameRateAnchor);
            frameRateAnchorInstance.parent = transform;
            frameRateAnchorInstance.position = Vector3.zero;

            await Task.Delay(1000);

            List<AbstractFrameRateTest> tests = GetTests();

            string csv = string.Empty;

            for (int i = 0; i < tests.Count; i++)
            {
                double warmup = 0;
                double fps = 0;

                tests[i].Load();
                for (int j = 0; j < count; j++)
                {
                    await Task.Delay(500);
                    await tests[i].RunAsync();
                    await Task.Delay(500);

                    warmup += tests[i].WarmupTime;
                    fps += tests[i].FrameRate;
                }
                tests[i].Unload();

                warmup /= count;
                fps /= count;

                Debug.Log($"{tests[i].TestName} - {tests[i].TestAmount} count" +
                    $"\nWarmup: {warmup:0.0} ms" +
                    $"\nFramerate: {fps:0.0} fps");

                csv += $"{tests[i].TestName}, {tests[i].TestAmount}, {warmup}, {fps}\n";
            }

            File.WriteAllText(Path.Combine(Application.dataPath, "Resources", "FrameRateTestResults.csv"), csv);

            Debug.Log("Test finished.");
        }

        private void Update()
        {
            framesCount++;
        }
#pragma warning restore IDE0051, RCS1213

        public Transform CreateCube()
            => Instantiate(cube);

        private List<AbstractFrameRateTest> GetTests()
        {
            const int k = 1000;
            const int k8 = 8 * k;
            const int k16 = 16 * k;
            const int k32 = 32 * k;
            const int k64 = 64 * k;
            const int k128 = 128 * k;
            const int k256 = 256 * k;
            _ = k8 + k16 + k32 + k64 + k128 + k256;

            List<AbstractFrameRateTest> result = new List<AbstractFrameRateTest>();
            void addTest(TestType testType, AbstractFrameRateTest test)
            {
                if ((tests & testType) == testType)
                {
                    result.Add(test);
                }
            }

            addTest(TestType.Tween, new TweenFrameRateTest(this, testTime, k256));
            addTest(TestType.Flow, new FlowFrameRateTest(this, testTime, k128));
            addTest(TestType.FlowTween, new FlowTweenFrameRateTest(this, testTime, k8));
            addTest(TestType.Transform, new TransformFrameRateTest(this, testTime, k8));
            addTest(TestType.DoTweenTween, new DoTweenTweenFrameRateTest(this, testTime, k256));
            addTest(TestType.DoTweenSequence, new DoTweenSequenceFrameRateTest(this, testTime, k128));
            addTest(TestType.DoTweenTransform, new DoTweenTransformFrameRateTest(this, testTime, k8));

            return result;
        }
    }
}