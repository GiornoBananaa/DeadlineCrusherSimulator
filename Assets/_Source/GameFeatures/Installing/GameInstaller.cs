using Core.DataLoading;
using Core.Factory;
using Core.ObjectContainer;
using GameFeatures.Clicker;
using GameFeatures.GameState;
using GameFeatures.Menu;
using GameFeatures.PlayerInput;
using GameFeatures.TowerDefence;
using GameFeatures.TowerDefence.Configs;
using GameFeatures.TowerDefence.ScheduleGeneration;
using GameFeatures.TowerDefence.TaskPlacing;
using GameFeatures.WorkProgress;
using UnityEngine;
using Zenject;

namespace GameFeatures.Installing
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private RaycastInputListener _raycastInputListener;
        [SerializeField] private TaskPlacingPredictionView _taskPlacingPredictionView;
        [SerializeField] private TextWriterView _textWriterView;
        [SerializeField] private ScheduleView _scheduleView;
        [SerializeField] private MenuPanelView _menuPanelView;
        
        public override void InstallBindings()
        {
            BindDataLoad();
            BindPlayerInput();
            BindGameStates();
            BindMenu();
            BindWorkProgress();
            BindEntitySystems();
            BindClickerFeature();
            BindTowerDefenceFeature();
        }
        
        private void BindPlayerInput()
        {
            Container.Bind<RaycastInputListener>().FromInstance(_raycastInputListener).AsSingle();
        }
        
        private void BindGameStates()
        {
            Container.Bind<GameStateMachine>().AsSingle();
        }
        
        private void BindMenu()
        {
            Container.Bind<MenuPanelView>().FromInstance(_menuPanelView).AsSingle();
        }
        
        private void BindEntitySystems()
        {
            Container.BindInterfacesAndSelfTo<Core.EntitySystem.ServiceUpdater>().AsSingle();
            
            Container.Bind<ObjectContainer<Deadline>>().AsSingle();
            Container.Bind<ObjectContainer<Task>>().AsSingle();
            Container.Bind<ObjectContainer<TaskProjectile>>().AsSingle();
        }
        
        private void BindWorkProgress()
        {
            Container.BindInterfacesAndSelfTo<WorkCounter>().AsSingle();
            Container.BindInterfacesAndSelfTo<ExpirationCounter>().AsSingle();
        }
        
        private void BindClickerFeature()
        {
            Container.BindInterfacesAndSelfTo<ClickCounter>().AsSingle();
            Container.BindInterfacesAndSelfTo<CodeWriter>().AsSingle().NonLazy();
            Container.Bind<TextWriterView>().FromInstance(_textWriterView).AsSingle();
        }
        
        private void BindTowerDefenceFeature()
        {
            Container.BindInterfacesAndSelfTo<EntityResetter>().AsSingle();
            
            Container.Bind<IPoolFactory<Task>>().To<TaskCreator>().AsSingle();
            Container.Bind<IPoolFactory<Deadline>>().To<DeadlineCreator>().AsSingle();
            Container.Bind<IPoolFactory<TaskProjectile>>().To<TaskProjectileCreator>().AsSingle();
            Container.BindInterfacesAndSelfTo<DeadlinesGenerator>().AsSingle().NonLazy();
            Container.Bind<ScheduleView>().FromInstance(_scheduleView).AsSingle();
            
            Container.Bind<DeadlineMovementSystem>().AsSingle().NonLazy();
            Container.Bind<TaskProjectileMovementSystem>().AsSingle().NonLazy();
            Container.Bind<TaskProjectileLifeTimeSystem>().AsSingle().NonLazy();
            Container.Bind<TaskShootingSystem>().AsSingle().NonLazy();
            Container.Bind<DeadlineHealthStateReactSystem>().AsSingle().NonLazy();
            Container.Bind<TaskHealthStateReactSystem>().AsSingle().NonLazy();
            Container.Bind<DeadlineDamageDealReactSystem>().AsSingle().NonLazy();
            Container.Bind<DeadlineExpireSystem>().AsSingle().NonLazy();
            Container.Bind<ProjectileDamageDealReactSystem>().AsSingle().NonLazy();
            
            Container.Bind<ScheduleGrid>().AsSingle().NonLazy();
            Container.Bind<TaskPlacingPredictionView>().FromInstance(_taskPlacingPredictionView).AsSingle();
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
            resourceLoader.LoadResource(PathData.TASKS_PROJECTILE_CONFIG_PATH,
                typeof(TaskProjectileConfig), dataRepository);
            resourceLoader.LoadResource(PathData.DEADLINES_CONFIG_PATH,
                typeof(DeadlinesConfig), dataRepository);
            resourceLoader.LoadResource(PathData.SCHEDULE_GRID_CONFIG_PATH,
                typeof(ScheduleGridConfig), dataRepository);
            resourceLoader.LoadResource(PathData.SCHEDULE_GENERATION_CONFIG_PATH,
                typeof(GenerationConfig), dataRepository);
            resourceLoader.LoadResource(PathData.WORK_PROGRESS_CONFIG_PATH,
                typeof(WorkProgressConfig), dataRepository);
            resourceLoader.LoadResource(PathData.WORK_EXPIRATION_CONFIG_PATH,
                typeof(WorkExpirationConfig), dataRepository);
        }
    }
}
