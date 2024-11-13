using System;
using System.Collections.Generic;
using TowerMergeTD.Game.State;
using TowerMergeTD.Game.UI;
using TowerMergeTD.Utils;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TowerMergeTD.Game.Gameplay
{
    //TODO: disable tower create places, finish tutorial
    public class Level1TutorialBinder : ITutorialBinder
    {
        private readonly MonoBehaviourWrapper _monoBehaviourWrapper;
        private readonly MapCoordinator _mapCoordinator;

        private IWaveSpawnerService[] _waveSpawnServices;
        private TowersListView _towersListView;
        private TutorialView _tutorialView;
        private Button _gunTowerButton;

        public Queue<ITutorialAction> TutorialActions { get; } = new Queue<ITutorialAction>();
        public Queue<string> TutorialTexts { get; } = new Queue<string>();

        public Level1TutorialBinder(MonoBehaviourWrapper monoBehaviourWrapper, MapCoordinator mapCoordinator)
        {
            _monoBehaviourWrapper = monoBehaviourWrapper;
            _mapCoordinator = mapCoordinator;
        }
        
        public void Bind(DiContainer diContainer)
        {
            _waveSpawnServices = diContainer.Resolve<IWaveSpawnerService[]>();
            _towersListView = diContainer.Resolve<TowersListView>();
            _tutorialView = diContainer.Resolve<TutorialView>();

            _gunTowerButton = _towersListView.GunTowerButton;
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
        }

        private void BindAction1()
        {
            var waitAction = new WaitingTutorialAction(_monoBehaviourWrapper, 2f);
            TutorialActions.Enqueue(waitAction);
            TutorialTexts.Enqueue("ТЕСТ: туториал ожидания");
            _tutorialView.SetActiveHandImage(false);
            Debug.Log("Action 1");
        }
        
        private void BindAction2()
        {
            var clickAction = new ClickButtonTutorialAction(_gunTowerButton);
            TutorialActions.Enqueue(clickAction);
            TutorialTexts.Enqueue("Нажмите, чтобы поставить башню");

            var tilePos = _mapCoordinator.GetFirstTowerPlaceTilePosition();
            Vector2 offset = new Vector2(0.5f, 4.15f);
            _tutorialView.SetActiveHandImage(true);
            _tutorialView.UpdateHandImagePosition(tilePos + offset);
            Debug.Log("Action 2");
        }
        
        private void BindAction3()
        {
            var spawnAction = new SpawnWaveTutorialAction(_waveSpawnServices);
            TutorialActions.Enqueue(spawnAction);
            TutorialTexts.Enqueue(String.Empty);
        }
        
        private void BindAction4()
        {
            var waitAction2 = new WaitingTutorialAction(_monoBehaviourWrapper, 10f);
            TutorialActions.Enqueue(waitAction2);
            TutorialTexts.Enqueue(String.Empty);
        }
        
        private void BindAction5()
        {
            var clickAction2 = new ClickButtonTutorialAction(_gunTowerButton);
            TutorialActions.Enqueue(clickAction2);
            TutorialTexts.Enqueue("ТЕСТ: туториал кнопки, создание башни 2");
        }
        
        private void BindAction6()
        {
            var mergeAction = new MergeTutorialAction();
            TutorialActions.Enqueue(mergeAction);
            TutorialTexts.Enqueue("ТЕСТ: объедините башни");
        }
        
        private void BindAction7()
        {
            var spawnAction2 = new SpawnWaveTutorialAction(_waveSpawnServices);
            TutorialActions.Enqueue(spawnAction2);
            TutorialTexts.Enqueue(String.Empty);
        }
        
        private void BindAction8()
        {
            var waitAction3 = new WaitingTutorialAction(_monoBehaviourWrapper, 10f);
            TutorialActions.Enqueue(waitAction3);
            TutorialTexts.Enqueue("ТЕСТ: ожидание смерти врага");
        }

        private void BindAction9()
        {
            var waitAction4 = new WaitingTutorialAction(_monoBehaviourWrapper, 5f);
            TutorialActions.Enqueue(waitAction4);
            TutorialTexts.Enqueue("ТЕСТ: удачной игры!");
        }
    }
}