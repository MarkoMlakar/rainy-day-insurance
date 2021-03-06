using System;
using System.Collections;
using Managers;
using UI.Views;
using UnityEngine;
using UnityEngine.Networking;

namespace UI.Controllers
{
    public class LocationPanel : MonoBehaviour, LocationPanelView.ICallbacks
    {
        private const string BASE_URL = "https://maps.googleapis.com/maps/api/staticmap?";
        [SerializeField] private string mapApiKey;
        [SerializeField] private float latitude;
        [SerializeField] private float longitude;
        [SerializeField] private int mapZoom;
        [SerializeField] private int mapSize;
        [Header("View")] 
        [SerializeField] private LocationPanelView locationPanelView;
        [SerializeField] private UiTweener uiTweener;
        [SerializeField] private GameObject photoPanel;

        private string _constructedURL;
        private IEnumerator Start()
        {
            // Enable device location services: https://docs.unity3d.com/ScriptReference/LocationService.Start.html
            // Check if the user has location service enabled.

            if (!Input.location.isEnabledByUser)
            {
                print("<color=red>Location services are not enabled</color>");
                locationPanelView.SetError("Location services are not enabled");
                locationPanelView.SetLoading(false);

                yield break;
            }
            
            Input.location.Start();

            // Waits until the location service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }
            
            // If the service didn't initialize in 20 seconds this cancels location service use.
            if (maxWait < 1)
            {
                print("<color=red>Timed out</color>");
                locationPanelView.SetError("Timed out");
                locationPanelView.SetLoading(false);

                yield break;
            }
            
            // If the connection failed this cancels location service use.
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                print("<color=red>Unable to determine device location</color>");
                locationPanelView.SetError("Unable to determine device location");
                locationPanelView.SetLoading(false);
                yield break;
            }

            // Stops the location service if there is no need to query location updates continuously.
            Input.location.Stop();
        }

        private void OnEnable()
        {
            locationPanelView.SetLoading(true);
            StartCoroutine(GetMapImage());
        }

        private IEnumerator GetMapImage()
        {
            _constructedURL = BASE_URL +
                              $"center={latitude},{longitude}&zoom={mapZoom}&size={mapSize}x{mapSize}&key={mapApiKey}";
            
            // NOTE: UNet is deprecated. TODO: replace with Netcode: https://docs-multiplayer.unity3d.com/
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(_constructedURL);
            yield return www.SendWebRequest();
            
            locationPanelView.SetLoading(false);
            
            
            if (www.result != UnityWebRequest.Result.Success) 
            {
                locationPanelView.SetError("Location services are not enabled");
            }
            else {
                locationPanelView.SetImage(((DownloadHandlerTexture)www.downloadHandler).texture);
                print("doneeee");
            }
        }
        void LocationPanelView.ICallbacks.OnProcessInfo(string locationNotes)
        {
            UIManager.Instance.activeCase.locationNotes = locationNotes;
            photoPanel.SetActive(true);
        }
        
        void LocationPanelView.ICallbacks.OnBack()
        {
            gameObject.SetActive(false);
        }
    }
}
