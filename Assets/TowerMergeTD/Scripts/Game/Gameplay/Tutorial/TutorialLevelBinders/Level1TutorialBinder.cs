using System;
using System.Collections.Generic;
using System.Linq;
using TowerMergeTD.Game.State;
using TowerMergeTD.Game.UI;
using TowerMergeTD.Utils;
using UnityEngine;
using Zenject;

namespace TowerMergeTD.Game.Gameplay
{
    public class Level1TutorialBinder : ITutorialBinder
    {
        private const int NONE_ANIMATION_INDEX = 0;
        private const int CLICK_ANIMATION_INDEX = 1;
        private const int DRAG_ANIMATION_INDEX = 2;
        
        private readonly MonoBehaviourWrapper _monoBehaviourWrapper;
        private readonly MapCoordinator _mapCoordinator;

        private IInput _input;
        private IWaveSpawnerService[] _waveSpawnServices;
        private TowersListView _towersListView;
        private TutorialView _tutorialView;

        public Queue<ITutorialAction> TutorialActions { get; } = new Queue<ITutorialAction>();
        public Queue<string> TutorialTexts { get; } = new Queue<string>();
        public Queue<TutorialHandImageData> TutorialHandDatas { get; } = new Queue<TutorialHandImageData>();

        public Level1TutorialBinder(MonoBehaviourWrapper monoBehaviourWrapper, MapCoordinator mapCoordinator)
        {
            _monoBehaviourWrapper = monoBehaviourWrapper;
            _mapCoordinator = mapCoordinator;
        }
        
        public void Bind(DiContainer diContainer)
        {
            _input = diContainer.Resolve<IInput>();
            _waveSpawnServices = diContainer.Resolve<IWaveSpawnerService[]>();
            _towersListView = diContainer.Resolve<TowersListView>();
            _tutorialView = diContainer.Resolve<TutorialView>();
            diContainer.Resolve<TowerSellView>().CanEnabled = false;

            _towersListView.SetButtonInteractable(TowerType.Rocket, false);
            _tutorialView.Init();
            _tutorialView.SetActiveHandImage(false);

            BindAction1();
            BindAction2();
            BindAction3();
            BindAction4();
            BindAction5();
            BindAction6();
            BindAction7();
            BindAction8();
            BindAction9();
            BindAction10();
            BindAction11();
            BindAction12();
            BindAction13();
            BindAction14();
            BindAction15();
        }

        private void BindAction1()
        {
            var waitAction = new WaitingTutorialAction(_monoBehaviourWrapper, _input, 2f);
            TutorialActions.Enqueue(waitAction);
            TutorialTexts.Enqueue(String.Empty);
            TutorialHandDatas.Enqueue(new TutorialHandImageData(Vector2.zero, NONE_ANIMATION_INDEX));
        }

        private void BindAction2()
        {
            var towerPlaces = _mapCoordinator.GetAllTowerPlaceTileWorldPositions();
            var towerPlaceClickTutorialAction = new TowerPlaceClickTutorialAction(towerPlaces.First(), _input, _mapCoordinator, _towersListView);
            TutorialActions.Enqueue(towerPlaceClickTutorialAction);
            TutorialTexts.Enqueue("Нажмите, чтобы поставить башню");
            
            var tilePos = _mapCoordinator.GetFirstTowerPlaceTileWorldPosition();
            Vector2 offset = new Vector2(0.5f, 4.15f);
            TutorialHandDatas.Enqueue(new TutorialHandImageData(tilePos + offset, CLICK_ANIMATION_INDEX));
        }

        private void BindAction3()
        {
            var showViewTutorialAction = new ShowViewTutorialAction(_monoBehaviourWrapper, _towersListView.gameObject);
            TutorialActions.Enqueue(showViewTutorialAction);
            TutorialTexts.Enqueue(String.Empty);
            TutorialHandDatas.Enqueue(new TutorialHandImageData(Vector2.zero, NONE_ANIMATION_INDEX));
        }

        private void BindAction4()
        {
            var clickAction = new CreateTowerTutorialAction(_monoBehaviourWrapper, _towersListView, TowerType.Gun);
            TutorialActions.Enqueue(clickAction);
            TutorialTexts.Enqueue("Нажмите, чтобы выбрать и поставить башню. Для этого требуются ресурсы.");
            var tilePos = _mapCoordinator.GetFirstTowerPlaceTileWorldPosition();
            Vector2 offset = new Vector2(1.15f, 3.65f);
            TutorialHandDatas.Enqueue(new TutorialHandImageData(tilePos + offset, CLICK_ANIMATION_INDEX));
        }
        
        private void BindAction5()
        {
            var spawnAction = new SpawnWaveTutorialAction(_waveSpawnServices);
            TutorialActions.Enqueue(spawnAction);
            TutorialTexts.Enqueue(String.Empty);
            TutorialHandDatas.Enqueue(new TutorialHandImageData(Vector2.zero, NONE_ANIMATION_INDEX));
        }
        
        private void BindAction6()
        {
            var waitAction = new WaitingTutorialAction(_monoBehaviourWrapper, _input,10f);
            TutorialActions.Enqueue(waitAction);
            TutorialTexts.Enqueue(String.Empty);
            TutorialHandDatas.Enqueue(new TutorialHandImageData(Vector2.zero, NONE_ANIMATION_INDEX));
        }

        private void BindAction7()
        {
            var waitAction = new WaitingTutorialAction(_monoBehaviourWrapper, _input,5f);
            TutorialActions.Enqueue(waitAction);
            TutorialTexts.Enqueue("Добывайте ресурсы, уничтожая врагов");
            Vector2 handPosition = new Vector2(-8.1f, 7.3f);
            TutorialHandDatas.Enqueue(new TutorialHandImageData(handPosition, CLICK_ANIMATION_INDEX));
        }
        
        private void BindAction8()
        {
            var waitAction = new WaitingTutorialAction(_monoBehaviourWrapper, _input,6f);
            TutorialActions.Enqueue(waitAction);
            TutorialTexts.Enqueue("Не дайте противникам добраться до базы, чтобы сохранить здоровье.");
            Vector2 handPosition = new Vector2(-8.1f, 8.1f);
            TutorialHandDatas.Enqueue(new TutorialHandImageData(handPosition, CLICK_ANIMATION_INDEX));
        }
        
        private void BindAction9()
        {
            var towerPlaces = _mapCoordinator.GetAllTowerPlaceTileWorldPositions();
            var towerPlaceClickTutorialAction = new TowerPlaceClickTutorialAction(towerPlaces[1], _input, _mapCoordinator, _towersListView);
            TutorialActions.Enqueue(towerPlaceClickTutorialAction);
            TutorialTexts.Enqueue("Нажмите, чтобы поставить вторую башню");
            
            var tilePos = _mapCoordinator.GetFirstTowerPlaceTileWorldPosition();
            Vector2 offset = new Vector2(1.5f, 4.15f);
            TutorialHandDatas.Enqueue(new TutorialHandImageData(tilePos + offset, CLICK_ANIMATION_INDEX));
        }
        
        private void BindAction10()
        {
            var clickAction = new CreateTowerTutorialAction(_monoBehaviourWrapper, _towersListView, TowerType.Gun);
            TutorialActions.Enqueue(clickAction);
            TutorialTexts.Enqueue("Нажмите, чтобы поставить вторую башню");
            var tilePos = _mapCoordinator.GetFirstTowerPlaceTileWorldPosition();
            Vector2 offset = new Vector2(2.25f, 3.65f);
            TutorialHandDatas.Enqueue(new TutorialHandImageData(tilePos + offset, CLICK_ANIMATION_INDEX));
        }
        
        private void BindAction11()
        {
            var mergeAction = new MergeTutorialAction();
            TutorialActions.Enqueue(mergeAction);
            TutorialTexts.Enqueue("Объедините башни");
            var tilePos = _mapCoordinator.GetFirstTowerPlaceTileWorldPosition();
            Vector2 offset = new Vector2(1.5f, 4.15f);
            TutorialHandDatas.Enqueue(new TutorialHandImageData(tilePos + offset, DRAG_ANIMATION_INDEX));
        }
        
        private void BindAction12()
        {
            var waitAction = new WaitingTutorialAction(_monoBehaviourWrapper, _input,5f);
            TutorialActions.Enqueue(waitAction);
            TutorialTexts.Enqueue("Объединяйте одиннаковые башни, чтобы получить их усиленную версию!");
            TutorialHandDatas.Enqueue(new TutorialHandImageData(Vector2.zero, NONE_ANIMATION_INDEX));
        }
        
        private void BindAction13()
        {
            var spawnAction = new SpawnWaveTutorialAction(_waveSpawnServices);
            TutorialActions.Enqueue(spawnAction);
            TutorialTexts.Enqueue(String.Empty);
            TutorialHandDatas.Enqueue(new TutorialHandImageData(Vector2.zero, NONE_ANIMATION_INDEX));
        }
        
        private void BindAction14()
        {
            var waitAction = new WaitingTutorialAction(_monoBehaviourWrapper, _input,15f);
            TutorialActions.Enqueue(waitAction);
            TutorialTexts.Enqueue(String.Empty);
            TutorialHandDatas.Enqueue(new TutorialHandImageData(Vector2.zero, NONE_ANIMATION_INDEX));
        }

        private void BindAction15()
        {
            var waitAction = new WaitingTutorialAction(_monoBehaviourWrapper, _input, 5f);
            TutorialActions.Enqueue(waitAction);
            TutorialTexts.Enqueue("Обучение завершено, удачной игры!");
            TutorialHandDatas.Enqueue(new TutorialHandImageData(Vector2.zero, NONE_ANIMATION_INDEX));
        }
    }
}