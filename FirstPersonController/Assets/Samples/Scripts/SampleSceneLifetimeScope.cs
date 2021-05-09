using UnityEngine;
using VContainer;
using VContainer.Unity;
using VHS.Audio;

namespace Sample
{
    public class SampleSceneLifetimeScope : LifetimeScope
    {
        [SerializeField] SampleScenePresenter _presenter;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<AudioService>(Lifetime.Singleton);
            builder.RegisterComponent(_presenter);
        }

        private void Reset()
        {
            _presenter = transform.GetComponent<SampleScenePresenter>();
        }
    }
}
