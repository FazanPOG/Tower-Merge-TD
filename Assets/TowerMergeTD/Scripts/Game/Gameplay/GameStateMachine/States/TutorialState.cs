using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using TowerMergeTD.Game.UI;
using TowerMergeTD.Utils;
using UnityEngine;
using Zenject;

namespace TowerMergeTD.Game.Gameplay
{
    public class TutorialState : IGameState
    {
        private readonly DiContainer _container;
        private readonly GameStateMachine _gameStateMachine;
        private readonly int _currentLevelIndex;
        private readonly ITutorialBinder _tutorialBinder;
        
        private MonoBehaviourWrapper _monoBehaviourWrapper;
        private TutorialView _tutorialView;
        private Queue<ITutorialAction> _tutorialActions;
        private Queue<string> _tutorialTexts;

        public TutorialState(DiContainer container, GameStateMachine gameStateMachine, int currentLevelIndex, ITutorialBinder tutorialBinder)
        {
            _container = container;
            _gameStateMachine = gameStateMachine;
            _currentLevelIndex = currentLevelIndex;
            _tutorialBinder = tutorialBinder;
        }
        
        public void Enter()
        {
            Debug.Log($"---Enter tutorial state---");
            
            if(_tutorialBinder == null)
                throw new MissingReferenceException($"Tutorial for level {_currentLevelIndex + 1} does not exist");
            
            _monoBehaviourWrapper = _container.Resolve<MonoBehaviourWrapper>();
            _tutorialView = _container.Resolve<TutorialView>();
            
            _tutorialBinder.Bind(_container);
            _tutorialActions = _tutorialBinder.TutorialActions;
            _tutorialTexts = _tutorialBinder.TutorialTexts;
            
            _monoBehaviourWrapper.StartCoroutine(StartTutorials());
        }

        private IEnumerator StartTutorials()
        {
            while (_tutorialActions.IsEmpty() == false)
            {
                var currentTutorial = _tutorialActions.Dequeue();
                var currentTutorialText = _tutorialTexts.Dequeue();
                
                currentTutorial.StartAction();

                if (currentTutorialText == String.Empty)
                {
                    _tutorialView.Hide();
                    _tutorialView.SetActiveHandImage(false);
                }
                else
                {
                    _tutorialView.Show();
                    _tutorialView.SetTutorialText(currentTutorialText);
                }
                
                yield return new WaitUntil(() => currentTutorial.IsComplete.CurrentValue);
            }
            
            _tutorialView.Hide();
            _tutorialView.SetActiveHandImage(false);
            _gameStateMachine.EnterIn<WinGameState>();
        }
        
        public void Exit()
        {
            
        }
    }
}