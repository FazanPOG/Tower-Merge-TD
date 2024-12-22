using System.Collections;
//using GamePush;
using TowerMergeTD.Utils;
using UnityEngine;

namespace TowerMergeTD.API
{
    public class GamePushEnvironmentService : IAPIEnvironmentService
    {
        public bool IsReady { get; private set; }

        public GamePushEnvironmentService(MonoBehaviourWrapper monoBehaviourWrapper)
        {
            IsReady = false;
            //monoBehaviourWrapper.StartCoroutine(WaitAPILoad());
        }
        
        public void GameLoadingAndReady()
        {
            //GP_Game.GameReady();
        }

        public void GameplayStart()
        {
            //GP_Game.GameplayStart();
        }

        public void GameplayStop()
        {
            //GP_Game.GameplayStop();
        }

        public void SetPaused(bool isPaused)
        {
            /*
            if(isPaused)
                GP_Game.Pause();
            else
                GP_Game.Resume();
                */
        }

        /*
        private IEnumerator WaitAPILoad()
        {
            yield return new WaitUntil(() => GP_Init.isReady);
            IsReady = true;
        }
        */
    }
}