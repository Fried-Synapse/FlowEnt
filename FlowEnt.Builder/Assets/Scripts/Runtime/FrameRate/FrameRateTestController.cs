using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Builder
{
    [Flags]
    public enum TestType
    {
        None = 0x000,
        Tween = 0x001,
        Echo = 0x002,
        Flow = 0x004,
        FlowTween = 0x008,
        Transform = 0x010,
        DoTweenTween = 0x020,
        DoTweenSequence = 0x040,
        DoTweenTransform = 0x080,
        OfficialList = Tween | Echo | Flow | Transform | DoTweenTween | DoTweenSequence | DoTweenTransform,
    }

    public class FrameRateTestController : MonoBehaviour
    {
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

        private int framesCount;
        public int FramesCount => framesCount;

        private async void Start()
        {
#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
            throw new Exception("Disable FlowEnt Debugging for tests");
#endif

            Transform frameRateAnchorInstance = Instantiate(frameRateAnchor);
            frameRateAnchorInstance.parent = transform;
            frameRateAnchorInstance.position = Vector3.zero;

            await Task.Delay(1000);

            List<AbstractFrameRateTest> tests = GetTests();

            string csv = "Name, Amount, Warmup, FPS\n";

            await RunTestAsync(tests[0], 1);

            for (int i = 0; i < tests.Count; i++)
            {
                (double warmup, double fps) = await RunTestAsync(tests[i], count);

                warmup /= count;
                fps /= count;

                Debug.Log($"{tests[i].TestName} - {tests[i].TestAmount} count" +
                          $"\nWarmup: {warmup:0.0} ms" +
                          $"\nFramerate: {fps:0.0} fps");

                csv += $"{tests[i].TestName}, {tests[i].TestAmount}, {warmup}, {fps}\n";
            }

            if (this.tests == TestType.OfficialList)
            {
                File.WriteAllText(Path.Combine(Application.dataPath, "Resources", "FrameRateTestResults.csv"), csv);
            }

            Debug.Log("Test finished.");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        private void Update()
        {
            framesCount++;
        }

        public Transform CreateCube()
            => Instantiate(cube);

        private async Task<(double, double)> RunTestAsync(AbstractFrameRateTest test, int count)
        {
            double warmup = 0;
            double fps = 0;

            test.Load();
            for (int j = 0; j < count; j++)
            {
                await Task.Delay(500);
                await test.RunAsync();
                await Task.Delay(500);

                warmup += test.WarmupTime;
                fps += test.FrameRate;
            }

            test.Unload();

            return (warmup, fps);
        }

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

            List<AbstractFrameRateTest> result = new();

            void addTest(TestType testType, AbstractFrameRateTest test)
            {
                if ((tests & testType) == testType)
                {
                    result.Add(test);
                }
            }

            addTest(TestType.Tween, new TweenFrameRateTest(this, testTime, k256));
            addTest(TestType.Echo, new EchoFrameRateTest(this, testTime, k256));
            addTest(TestType.Flow, new FlowFrameRateTest(this, testTime, k128));
            addTest(TestType.Transform, new TransformFrameRateTest(this, testTime, k8));
            addTest(TestType.FlowTween, new FlowTweenFrameRateTest(this, testTime, k8));
            addTest(TestType.DoTweenTween, new DoTweenTweenFrameRateTest(this, testTime, k256));
            addTest(TestType.DoTweenSequence, new DoTweenSequenceFrameRateTest(this, testTime, k128));
            addTest(TestType.DoTweenTransform, new DoTweenTransformFrameRateTest(this, testTime, k8));

            return result;
        }
    }
}