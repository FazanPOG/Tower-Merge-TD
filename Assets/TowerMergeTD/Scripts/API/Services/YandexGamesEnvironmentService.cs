using System.Collections;
using Agava.YandexGames;
using TowerMergeTD.Utils;
using UnityEngine;
using YG;

namespace TowerMergeTD.API
{
    public class YandexGamesEnvironmentService : IAPIEnvironmentService
    {
        public bool IsReady { get; private set; }

        public YandexGamesEnvironmentService(MonoBehaviourWrapper monoBehaviourWrapper)
        {
            IsReady = false;
            monoBehaviourWrapper.StartCoroutine(WaitAPILoad());
        }

        public void GameLoadingAndReady()
        {
            YandexGame.GameReadyAPI();
        }

        public void GameplayStart()
        {
            //YandexGame.GameplayStart();
        }

        public void GameplayStop()
        {
            //YandexGame
        }

        public void SetPaused(bool isPaused)
        {
            if (isPaused)
                GameplayStart();
            else
                GameplayStop();
        }

        private IEnumerator WaitAPILoad()
        {
            yield return new WaitUntil(() => YandexGame.SDKEnabled);
            IsReady = true;
        }
    }
}