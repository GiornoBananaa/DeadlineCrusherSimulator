using Core.InstallationSystem.DataLoadingSystem;
using GameFeatures.ClickerFeature;
using GameFeatures.ClickerFeature.Configs;
using GameFeatures.ClickerFeature.Views;
using UnityEngine;
using Zenject;

namespace Systems.InstallingSystem
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private TextWriterView _textWriterView;
        
        public override void InstallBindings()
        {
            BindClickerFeature();
            BindDataLoad();
        }

        private void BindClickerFeature()
        {
            Container.Bind<Clicker>().AsSingle();
            Container.Bind<CodeWriter>().AsSingle().NonLazy();
            Container.Bind<TextWriterView>().FromInstance(_textWriterView).AsSingle();
        }
        
        private void BindDataLoad()
        {
            IResourceLoader resourceLoader = new ResourceLoader();
            IRepository<ScriptableObject> dataRepository = new DataRepository<ScriptableObject>();
            
            LoadResources(resourceLoader, dataRepository);
            
            Container.Bind<IRepository<ScriptableObject>>().FromInstance(dataRepository).AsSingle();
        }
        
        private void LoadResources(IResourceLoader resourceLoader, IRepository<ScriptableObject> dataRepository)
        {
            resourceLoader.LoadResource(PathData.CODE_WRITING_PATH,
                typeof(CodeWritingConfig), dataRepository);
        }
    }
}
