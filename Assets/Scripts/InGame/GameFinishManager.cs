using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MinGun
{
    public class GameFinishManager : MonoBehaviour
    {
        private LapSystem[] lapSystems;

        public GameObject leaderBoard;
        
        private Transform rankingNamesContainer;
        private Transform rankingTimesContainer;

        private void Start()
        {
            lapSystems = FindObjectsOfType<LapSystem>();
            
            leaderBoard = GameObject.Find("Canvas").transform.GetChild(1).Find("LeaderBoard").gameObject;
            
            rankingNamesContainer = leaderBoard.transform.GetChild(0).Find("RankingNames").transform;
            rankingTimesContainer = leaderBoard.transform.GetChild(0).Find("RankingTimes").transform;

            leaderBoard.SetActive(false);
        }
        public IEnumerator GameFinish(LapSystem lapSystem)
        {
            if (GameManager.Instance.isGameEnd)
                yield break;
            if (GameManager.Instance.isFinished == false)
            {
                GameManager.Instance.isFinished = true;

                if (lapSystem.driverType == Driver.Enemy)
                {
                    switch (lapSystem.driverType)
                    {
                        case Driver.Enemy:
                            Debug.Log($"게임 끝! AI승리!");
                            break;
                        case Driver.Player:
                            Debug.Log($"게임 끝! 플레이어 승리!");
                            break;
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        Debug.Log(10 - i);
                        yield return new WaitForSeconds(1f);
                    }
                }
            }

            GameManager.Instance.isGameEnd = true;
            
            GameManager.Instance.SetTimeScale(0f);
            
            // 만약 두번째 요소가 우승했을 시 첫번째와 스왑
            if (lapSystems[1].GetFinishTime < lapSystems[0].GetFinishTime)
                (lapSystems[0], lapSystems[1]) = (lapSystems[1], lapSystems[0]);
            
            Text[] rankingNames = rankingNamesContainer.GetComponentsInChildren<Text>();
            Text[] rankingTimes = rankingTimesContainer.GetComponentsInChildren<Text>();

            for (int i = 0; i < lapSystems.Length; i++)
            {
                rankingNames[i].text = $"{i + 1}. {lapSystems[i].driverType}";

                string timeText = TimeSpan.FromSeconds(lapSystems[i].lapTimeList.Sum()).ToString(@"mm\:ss\.ff");
                if (!lapSystems[i].isFinished)
                    timeText = "Retire";
                
                rankingTimes[i].text = $"{timeText}";
            }

            leaderBoard.SetActive(true);
        }

    }
}
