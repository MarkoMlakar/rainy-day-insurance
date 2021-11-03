using UnityEngine;
using UnityEngine.Android;

namespace Managers
{
    public class LocationManager: MonoBehaviour
    {
        private void Awake()
        {
            // Some android devices do NOT show permissions on their own.
#if PLATFORM_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermission(Permission.FineLocation);
            }
#endif
        }
    }
}