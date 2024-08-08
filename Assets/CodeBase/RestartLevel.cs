using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase
{
    public class RestartLevel : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene("Main");
        }
        
    }
}