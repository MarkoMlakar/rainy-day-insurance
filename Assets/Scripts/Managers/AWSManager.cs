using Amazon;
using UnityEngine;

namespace Managers
{
    public class AWSManager : MonoBehaviour
    {
        private void Awake()
        {
            UnityInitializer.AttachToGameObject(this.gameObject);
        }
    }
}
