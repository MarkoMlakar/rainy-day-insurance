using UnityEngine;
using UnityEngine.Android;

namespace Managers
{
    public class LocationManager: MonoBehaviour
    {
        private void Awake()
        {
             
#if UNITY_ANDROID
         if (!Permission.HasUserAuthorizedPermission( Permission.FineLocation ) )
             Permission.RequestUserPermission( Permission.FineLocation );
        #endif 
        }
    }
}