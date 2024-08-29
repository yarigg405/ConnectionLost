using UnityEngine;
using Yrr.Entitaz;


namespace ConnectionLost
{
    internal sealed class ContentContainer : MonoBehaviour
    {
        public IEntita ContentEntita { get; private set; }
        public bool IsEmpty => ContentEntita == null;

        public void SetContent(IEntita contentEntita)
        {
            ContentEntita = contentEntita;
        }
    }
}