using UnityEngine;

namespace Models
{
    [System.Serializable]
    public class Case
    {
        public string id;
        public string name;
        public string date;
        public string location;
        public string locationNotes;
        public string photoNotes;
        public byte[] photoTaken;
    }
}
