using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using static DungeonSurvivor.Core.Events.GameplayEvents.MiniGames.PatternMatch;

namespace DungeonSurvivor.Core.Puzzles.MiniGames.PatternMatch
{
    public class Gameplay : MonoBehaviour
    {
        [SerializeField] private List<Sprite>     itemSprites;
        [SerializeField] private List<GameObject> staticPatternObjects;
        [SerializeField] private List<GameObject> scrambledPatternObjects;
        [SerializeField] private int              matchItemsCount;
        
        private bool       _isSwaping;
        private bool       _isSwapAvailable = true;
        private GameObject _swapCandidate;

        private List<int> GeneratePattern(int maxRange, List<int> blackList = null)
        {
            List<int> generatedPattern = new();
            int       insertCount      = 0;
            while (true)
            {
                if (insertCount == maxRange)
                {
                    break;
                }

                int random = Random.Range(0, maxRange);
                if (generatedPattern.Contains(random)) continue;
                generatedPattern.Add(random);
                insertCount++;
            }

            if (blackList == null) return generatedPattern;
            if (generatedPattern.SequenceEqual(blackList))
            {
                generatedPattern = GeneratePattern(maxRange, blackList);
            }

            return generatedPattern;
        }

        private void DisplayPattern(List<GameObject> gameObjects, List<int> ordering)
        {
            for (int i = 0; i < ordering.Count; i++)
            {
                MatchItem matchItemComponent = gameObjects[i]
                    .AddComponent<MatchItem>();
                matchItemComponent.ID = ordering[i];
                Image spriteRenderer = gameObjects[i]
                    .GetComponent<Image>();
                spriteRenderer.sprite = itemSprites[ordering[i]];
                LeanTween.scale(gameObjects[i], Vector3.one, 1f)
                    .setEaseInOutExpo();
            }
        }

        private GameObject GetMatchItemFromID(int id)
        {
            GameObject matchedItem = null;
            foreach (GameObject obj in scrambledPatternObjects)
            {
                if (obj.GetComponent<MatchItem>()
                        .ID == id)
                {
                    matchedItem = obj;
                }
            }

            return matchedItem;
        }

        private bool CheckPattern()
        {
            for (int i = 0; i < scrambledPatternObjects.Count; i++)
            {
                if (scrambledPatternObjects[i]
                        .GetComponent<MatchItem>()
                        .ID != staticPatternObjects[i]
                        .GetComponent<MatchItem>()
                        .ID)
                {
                    return false;
                }
            }
            return true;
        }

        private void PlayEndAnimation()
        {
            
        }

        private void OnMatchItemClicked(int clickedItem)
        {
            if (_isSwaping)
            {
                return;
            }

            if (_isSwapAvailable)
            {
                _isSwapAvailable = false;
                _swapCandidate   = GetMatchItemFromID(clickedItem);
            }
            else
            {
                GameObject swapWithCandidate = GetMatchItemFromID(clickedItem);
                if (_swapCandidate.Equals(swapWithCandidate))
                {
                    _isSwapAvailable = true;

                    //ToDO: stop tweening swapCandidate
                }
                else
                {
                    int swapCandidateListIndex     = scrambledPatternObjects.IndexOf(_swapCandidate);
                    int swapWithCandidateListIndex = scrambledPatternObjects.IndexOf(swapWithCandidate);

                    if (Math.Abs(swapCandidateListIndex - swapWithCandidateListIndex) == 1)
                    {
                        scrambledPatternObjects[swapCandidateListIndex]     = swapWithCandidate;
                        scrambledPatternObjects[swapWithCandidateListIndex] = _swapCandidate;

                        Vector3 swapCandidatePosition = _swapCandidate.transform.position;
                        _isSwaping = true;
                        LeanTween.move(_swapCandidate, swapWithCandidate.transform.position, 0.2f)
                            .setEaseInBounce();
                        LeanTween.move(swapWithCandidate, swapCandidatePosition, 0.2f)
                            .setOnComplete(() =>
                            {
                                if (!CheckPattern())
                                {
                                    _isSwaping       = false;
                                    _isSwapAvailable = true;
                                }
                                else
                                {
                                    PlayEndAnimation();
                                }
        
                            })
                            .setEaseInBounce();
                    }
                    else
                    {
                        _isSwapAvailable = true;
                        //ToDO: stop tweening swapCandidate
                    }
                }
            }
        }

        private void OnEnable()
        {
            List<int> staticPattern    = GeneratePattern(matchItemsCount);
            List<int> scrambledPattern = GeneratePattern(matchItemsCount, staticPattern);
            staticPatternObjects.ForEach(obj => obj.transform.localScale    = Vector3.zero);
            scrambledPatternObjects.ForEach(obj => obj.transform.localScale = Vector3.zero);
            DisplayPattern(staticPatternObjects, staticPattern);
            DisplayPattern(scrambledPatternObjects, scrambledPattern);
            MatchItemClicked.AddListener(OnMatchItemClicked);
        }

        private void OnDisable()
        {
            MatchItemClicked.RemoveListener(OnMatchItemClicked);
        }
    }
}