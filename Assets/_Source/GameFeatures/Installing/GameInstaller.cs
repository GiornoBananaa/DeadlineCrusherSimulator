using Core.DataLoading;
using Core.Factory;
using Core.ObjectContainer;
using GameFeatures.Clicker;
using GameFeatures.Clicker.Configs;
using GameFeatures.GameState;
using GameFeatures.PlayerInput;
using GameFeatures.TowerDefence;
using GameFeatures.TowerDefence.Configs;
using GameFeatures.TowerDefence.ScheduleGeneration;
using GameFeatures.TowerDefence.TaskPlacing;
using UnityEngine;
using Zenject;

namespace GameFeatures.Installing
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private TextWriterView _textWriterView;
        [SerializeField] private ScheduleView _scheduleView;
        [SerializeField] private RaycastInputListener _raycastInputListener;
        
        public override void InstallBindings()
        {
            BindPlayerInput();
            BindGameStates();
            BindExecuteSystems();
            BindClickerFeature();
            BindTowerDefenceFeature();
            BindDataLoad();
        }
        
        private void BindPlayerInput()
        {
            Container.Bind<RaycastInputListener>().FromInstance(_raycastInputListener).AsSingle();
        }
        
        private void BindGameStates()
        {
            Container.Bind<Game>().AsSingle();
        }
        
        private void BindExecuteSystems()
        {
            Container.BindInterfacesAndSelfTo<Core.EntitySystem.ServiceUpdater>().AsSingle();
        }
        
        private void BindClickerFeature()
        {
            Container.Bind<Clicker.Clicker>().AsSingle();
            Container.Bind<CodeWriter>().AsSingle().NonLazy();
            Container.Bind<TextWriterView>().FromInstance(_textWriterView).AsSingle();
        }
        
        private void BindTowerDefenceFeature()
        {
            Container.Bind<ObjectContainer<Deadline>>().AsSingle();
            Container.Bind<ObjectContainer<Task>>().AsSingle();
            Container.Bind<ObjectContainer<TaskProjectile>>().AsSingle();
            Container.Bind<PoolFactory<Task>>().To<TaskCreator>().AsSingle();
            Container.Bind<PoolFactory<Deadline>>().To<DeadlineCreator>().AsSingle();
            Container.Bind<PoolFactory<TaskProjectile>>().To<TaskProjectileCreator>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeadlinesGenerator>().AsSingle().NonLazy();
            Container.Bind<ScheduleView>().FromInstance(_scheduleView).AsSingle();
            
            Container.Bind<DeadlineMovementSystem>().AsSingle().NonLazy();
            Container.Bind<TaskProjectileMovementSystem>().AsSingle().NonLazy();
            Container.Bind<TaskShootingSystem>().AsSingle().NonLazy();
            Container.Bind<DeadlineHealthStateReactSystem>().AsSingle().NonLazy();
            Container.Bind<TaskHealthStateReactSystem>().AsSingle().NonLazy();
            Container.Bind<DeadlineDamageDealReactSystem>().AsSingle().NonLazy();
            Container.Bind<ProjectileDamageDealReactSystem>().AsSingle().NonLazy();
            
            Container.Bind<ScheduleGrid>().AsSingle().NonLazy();
            Container.Bind<TaskPlacer>().AsSingle().NonLazy();
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
