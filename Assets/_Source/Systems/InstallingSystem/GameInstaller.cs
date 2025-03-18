using Core.InstallationSystem.DataLoadingSystem;
using GameFeatures.ClickerFeature;
using GameFeatures.ClickerFeature.Configs;
using GameFeatures.ClickerFeature.Views;
using GameFeatures.TowerDefenceFeature;
using GameFeatures.TowerDefenceFeature.Configs;
using GameFeatures.TowerDefenceFeature.ScheduleGeneration;
using Systems.FactorySystem;
using Systems.GameStateSystem;
using Systems.GenerationSystem;
using Systems.ObjectContainerSystem;
using UnityEngine;
using Zenject;

namespace Systems.InstallingSystem
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private TextWriterView _textWriterView;
        [SerializeField] private ScheduleView _scheduleView;
        
        public override void InstallBindings()
        {
            BindGameStates();
            BindClickerFeature();
            BindTowerDefenceFeature();
            BindDataLoad();
        }

        public void BindGameStates()
        {
            Container.Bind<Game>().AsSingle();
        }
        
        private void BindClickerFeature()
        {
            Container.Bind<Clicker>().AsSingle();
            Container.Bind<CodeWriter>().AsSingle().NonLazy();
            Container.Bind<TextWriterView>().FromInstance(_textWriterView).AsSingle();
        }
        
        private void BindTowerDefenceFeature()
        {
            Container.Bind<ObjectContainer<Deadline>>().AsSingle();
            Container.Bind<ObjectContainer<Task>>().AsSingle();
            Container.Bind<PoolFactory<Task>>().To<TaskCreator>().AsSingle();
            Container.Bind<PoolFactory<Deadline>>().To<DeadlineCreator>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeadlinesGenerator>().AsSingle().NonLazy();
            Container.Bind<ScheduleView>().FromInstance(_scheduleView).AsSingle();
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
            resourceLoader.LoadResource(PathData.CODE_WRITING_CONFIG_PATH,
                typeof(CodeWritingConfig), dataRepository);
            resourceLoader.LoadResource(PathData.TASKS_CONFIG_PATH,
                typeof(TasksConfig), dataRepository);
            resourceLoader.LoadResource(PathData.DEADLINES_CONFIG_PATH,
                typeof(DeadlinesConfig), dataRepository);
            resourceLoader.LoadResource(PathData.SCHEDULE_GENERATION_CONFIG_PATH,
                typeof(GenerationConfig), dataRepository);
        }
    }
}
