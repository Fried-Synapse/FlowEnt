using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace FlowEnt
{
    public class FlowEntFramerateTestController : MonoBehaviour
    {
        [SerializeField]
        private bool runTest;

        [SerializeField]
        private Transform framerateAnchor;

        private int framerateCount;
        private bool testStarted;

        private void Update()
        {
            if (!testStarted)
            {
                return;
            }

            framerateCount++;
        }

        private async void Start()
        {
            if (!runTest)
            {
                return;
            }

            Transform framerateAnchorInstance = Instantiate(framerateAnchor);
            framerateAnchorInstance.parent = transform;
            framerateAnchorInstance.position = Vector3.zero;

            await Task.Delay(2);

            const float testTime = 5f;
            const int testAmount = 128000;
            FlowEntController.Instance.SetMaxCapacity(testAmount);
            testStarted = true;
            for (int i = 0; i < testAmount - 1; i++)
            {
                new Tween(testTime)
                    .Start();
            }

            new Tween(testTime)
                .OnComplete(() =>
                {
                    testStarted = false;
                    Debug.Log(framerateCount / testTime);
                })
                .Start();
        }
    }
}
