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
        private const int CLICK_ANIMATION_INDEX = 1;
        private const int DRAG_ANIMATION_INDEX = 2;
        
        private readonly DiContainer _container;
        private readonly GameStateMachine _gameStateMachine;
        private readonly int _currentLevelIndex;
        private readonly ITutorialBinder _tutorialBinder;
        
        private MonoBehaviourWrapper _monoBehaviourWrapper;
        private TutorialView _tutorialView;
        private Queue<ITutorialAction> _tutorialActions;
        private Queue<string> _tutorialTexts;
        private Queue<TutorialHandImageData> _tutorialHandImageDatas;
        private Coroutine _coroutine;

        public TutorialState(DiContainer container, GameStateMachine gameStateMachine, int currentLevelIndex, ITutorialBinder tutorialBinder)
        {
            _container = container;
            _gameStateMachine = gameStateMachine;
            _currentLevelIndex = currentLevelIndex;
            _tutorialBinder = tutorialBinder;
        }
        
        public void Enter()
        {
            if(_tutorialBinder == null)
                throw new MissingReferenceException($"Tutorial for level {_currentLevelIndex + 1} does not exist");
            
            _monoBehaviourWrapper = _container.Resolve<MonoBehaviourWrapper>();
            _tutorialView = _container.Resolve<TutorialView>();
            
            _container.Resolve<IPauseService>().SetPause(false);
            _container.Resolve<IGameTimerService>().StartTimer();
            
            _tutorialBinder.Bind(_container);
            _tutorialActions = _tutorialBinder.TutorialActions;
            _tutorialTexts = _tutorialBinder.TutorialTexts;
            _tutorialHandImageDatas = _tutorialBinder.TutorialHandDatas;
            
            if(_tutorialActions.Count != _tutorialTexts.Count || _tutorialActions.Count != _tutorialHandImageDatas.Count)
                throw new ArgumentOutOfRangeException($"The counts of tutorial actions, texts, and hand image data do not match. Ensure all lists have the same number of elements.");
            
            _coroutine = _monoBehaviourWrapper.StartCoroutine(StartTutorials());
        }

        private IEnumerator StartTutorials()
        {
            while (_tutorialActions.IsEmpty() == false)
            {
                var currentTutorialAction = _tutorialActions.Dequeue();
                var currentTutorialText = _tutorialTexts.Dequeue();
                var currentHandPosition = _tutorialHandImageDatas.Dequeue();
                
                currentTutorialAction.StartAction();

                UpdateTutorialText(currentTutorialText);
                UpdateHandView(currentHandPosition);
                
                yield return new WaitUntil(() => currentTutorialAction.IsComplete.CurrentValue);
            }
            
            _tutorialView.SetActiveTutorialTextView(false);
            _tutorialView.SetActiveHandImage(false);
            
            _gameStateMachine.EnterIn<WinGameState>();
        }

        private void UpdateTutorialText(string text)
        {
            if (text == String.Empty)
            {
                _tutorialView.SetActiveTutorialTextView(false);
            }
            else
            {
                _tutorialView.SetActiveTutorialTextView(true);
                _tutorialView.SetTutorialText(text);
            }
        }

        private void UpdateHandView(TutorialHandImageData handImageData)
        {
            if (handImageData.Position == Vector2.zero)
            {
                _tutorialView.StopAnimation();
                _tutorialView.SetActiveHandImage(false);
            }
            else
            {
                _tutorialView.SetActiveHandImage(true);
                _tutorialView.UpdateHandImagePosition(handImageData.Position);
                
                switch (handImageData.AnimationIndex)
                {
                    case CLICK_ANIMATION_INDEX:
                        _tutorialView.PlayClickAnimation();
                        break;
                    
                    case DRAG_ANIMATION_INDEX:
                        _tutorialView.PlayDragAnimation();
                        break;
                    
                    default:
                        _tutorialView.StopAnimation();
                        break;
                }
            }
        }
        
        public void Exit()
        {
            Debug.Log($"Exit TutorialState");
            _monoBehaviourWrapper.StopCoroutine(_coroutine);

            foreach (var tutorialAction in _tutorialBinder.TutorialActions)
                tutorialAction.Dispose();
            
            _tutorialActions.Clear();
            _tutorialTexts.Clear();
            _tutorialHandImageDatas.Clear();
        }
    }
}