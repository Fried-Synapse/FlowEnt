using UnityEngine;
using UnityEngine.SceneManagement;

namespace FriedSynapse.FlowEnt.Demo
{
    public class SceneController : MonoBehaviour
    {
        public void Replay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
