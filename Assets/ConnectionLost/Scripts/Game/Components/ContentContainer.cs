using UnityEngine;
using Yrr.Entitaz;


namespace ConnectionLost
{
    internal sealed class ContentContainer : MonoBehaviour, IEntitazComponent
    {
        public Entita ContentEntita { get; private set; }
        public bool IsEmpty => ContentEntita == null;

        public void SetContent(Entita contentEntita)
        {
            ContentEntita = contentEntita;
        }
    }
}