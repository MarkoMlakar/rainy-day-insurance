using UI.Panels;
using UnityEngine;
namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;

        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                    Debug.Log("UIManager is NULL");
                
                return _instance;
            }
        }
        
        [SerializeField] private ClientInfoPanel clientInfoPanel;
        [SerializeField] private GameObject borderPanel;

        public Case activeCase;
        private void Awake()
        {
            _instance = this;
        }

        public void CreateNewCase()
        {
            activeCase = new Case();
            int randomCaseId = Random.Range(0, 1000);
            activeCase.id = randomCaseId.ToString();
            clientInfoPanel.gameObject.SetActive(true);
            borderPanel.SetActive(true);
        }
    }
}
