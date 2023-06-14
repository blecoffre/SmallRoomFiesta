using Michsky.MUIP;
using MoreMountains.Feedbacks;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    //[SerializeField] private ProgressBar _bar = default;
    //[SerializeField] private MMF_Player _mmfPlayer = default;

    public override void InstallBindings()
    {
        /// Can be referenced like this, for our current needs this solution should fit
        Container.Bind<ProgressBar>().FromComponentInHierarchy().AsSingle();
        Container.Bind<MMF_Player>().FromComponentInHierarchy().AsSingle();

        /// Or like this
        //Container.Bind<ProgressBar>().WithId("MainProgressBar").FromInstance(_bar).AsSingle();
        //Container.Bind<MMF_Player>().WithId("Main_MMFPlayer").FromInstance(_mmfPlayer).AsSingle();
    }
}