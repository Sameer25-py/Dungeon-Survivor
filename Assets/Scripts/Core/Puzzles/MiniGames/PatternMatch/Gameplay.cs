using System;
using System.Collections.Generic;
using System.Linq;
using DungeonSurvivor.Core.Events;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using static DungeonSurvivor.Core.Events.GameplayEvents.MiniGames;

namespace DungeonSurvivor.Core.Puzzles.MiniGames.PatternMatch
{
    public class Gameplay : MonoBehaviour
    {
        [SerializeField] private List<Sprite>     itemSprites;
        [SerializeField] private List<GameObject> staticPatternObjects;
        [SerializeField] private List<GameObject> scrambledPatternObjects;
        [SerializeField] private int              matchItemsCount;

        [SerializeField] private Image staticPatternBackDrop;
        [SerializeField] private Image scrambledPatternBackDrop;
        [SerializeField] private Color correctPatternColor;
        [SerializeField] private Color wrongPatternColor;

        [SerializeField] private GameObject staticItemBox;
        [SerializeField] private GameObject scrambledItemBox;

        private bool       _isSwaping;
        private bool       _isSwapAvailable = true;
        private GameObject _swapCandidate;
        private LTDescr    _swapCandidateSelectTween;

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
            LeanTween.color(scrambledPatternBackDrop.rectTransform, correctPatternColor, 0.5f)
                .setEaseInExpo()
                .setOnComplete(() =>
                {
                    LeanTween.move(scrambledItemBox.GetComponent<RectTransform>(), Vector3.zero, 0.3f)
                        .setEaseOutExpo()
                        .setDelay(0.5f);
                    LeanTween.move(staticItemBox.GetComponent<RectTransform>(), Vector3.zero, 0.3f)
                        .setEaseOutExpo()
                        .setDelay(0.5f)
                        .setOnComplete(() =>
                        {
                            float delay = 0.1f;
                            foreach (GameObject obj in scrambledPatternObjects)
                            {
                                LeanTween.rotateAround(obj, Vector3.forward, -360f, 2f)
                                    .setDelay(delay)
                                    .setEaseOutExpo();

                                delay *= 2f;
                            }

                            MiniGameCompleted?.Invoke();
                        });
                });
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
                _swapCandidateSelectTween = LeanTween.scale(_swapCandidate, new Vector3(1.2f, 1.2f, 1.2f), 0.5f)
                    .setEaseShake()
                    .setLoopClamp();
            }
            else
            {
                GameObject swapWithCandidate = GetMatchItemFromID(clickedItem);
                LeanTween.cancel(_swapCandidateSelectTween.id);
                _swapCandidate.GetComponent<RectTransform>()
                    .localScale = Vector3.one;
                if (_swapCandidate.Equals(swapWithCandidate))
                {
                    _isSwapAvailable = true;
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
                        LeanTween.cancel(_swapCandidateSelectTween.id);
                        _swapCandidate.GetComponent<RectTransform>()
                            .localScale = Vector3.one;
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
            LeanTween.color(staticPatternBackDrop.rectTransform, correctPatternColor, 0.5f)
                .setEaseLinear();
            DisplayPattern(scrambledPatternObjects, scrambledPattern);
            LeanTween.color(scrambledPatternBackDrop.rectTransform, wrongPatternColor, 0.5f)
                .setEaseLinear();
            GameplayEvents.MiniGames.PatternMatch.MatchItemClicked.AddListener(OnMatchItemClicked);
        }

        private void OnDisable()
        {
            GameplayEvents.MiniGames.PatternMatch.MatchItemClicked.RemoveListener(OnMatchItemClicked);
        }
    }
}