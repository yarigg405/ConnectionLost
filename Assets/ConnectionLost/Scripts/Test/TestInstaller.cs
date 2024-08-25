using Infrastructure.DI;
using UnityEngine;


namespace ConnectionLost
{
    internal sealed class TestInstaller : Module
    {
        [SerializeField, Service(typeof(TestMonoBeh))]
        private TestMonoBeh testMonoBeh;

        [SerializeField, Service(typeof(TestNotMonoBeh))]
        private TestNotMonoBeh testNotMonoBeh = new();
    }
}