using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlowEnt
{
    [ExecuteInEditMode]
    public class Test : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log($"{((RectTransform)transform).anchoredPosition}");
        }
    }
}
