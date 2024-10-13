using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour {
  [SerializeField] private List<Mole> moles;

  [Header("UI objects")]
  [SerializeField] private GameObject playButton;
  [SerializeField] private GameObject gameUI;
  [SerializeField] private GameObject bombText;
  [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI textCongratulate;
    [SerializeField] private List<Image> listStars;
    [SerializeField] private Sprite onStar, offStar;
    [SerializeField] private GameObject camera;

  // Hardcoded variables you may want to tune.
  private float startingTime = 30f;

  // Global variables
  private float timeRemaining;
  private HashSet<Mole> currentMoles = new HashSet<Mole>();
  private int score;
  public bool playing = false;
    public bool isHaveMole = false;
    public int health = 4;

  // This is public so the play button can see it.
  public void StartGame() {
    // Hide/show the UI elements we don't/do want to see.
    playButton.SetActive(false);
    bombText.SetActive(false);
    gameUI.SetActive(true);
    // Hide all the visible moles.
    for (int i = 0; i < moles.Count; i++) {
      moles[i].Hide();
      moles[i].SetIndex(i);
    }
    // Remove any old game state.
    currentMoles.Clear();
    // Start with 30 seconds.
    timeRemaining = startingTime;
    score = 0;
    scoreText.text = "0";
    playing = true;
        isHaveMole = false;

        health = 4;
        foreach (Image item in listStars)
        {
            item.sprite = onStar;
        }

        textCongratulate.transform.localScale = Vector3.zero;
  }

  public void GameOver() {
        bombText.SetActive(true);
        // Hide all moles.
        foreach (Mole mole in moles) {
      mole.StopGame();
    }
    // Stop the game and show the start UI.
    playing = false;
    playButton.SetActive(true);
  }

    public void DecreaseHealth()
    {
        health--;
        if(health >= 0)
        {
            for (int i = 0; i < listStars.Count; i++)
            {
                if (i < health)
                {
                    listStars[i].sprite = onStar;
                }
                else
                {
                    listStars[i].sprite = offStar;
                }
            }
        }
        else
        {
            GameOver();
        }
       

    }

  // Update is called once per frame
  void Update() {
    if (playing) {
            if (!isHaveMole)
            {
                Debug.Log("spawn!");
                isHaveMole = true;
                int index = Random.Range(0, moles.Count);
                moles[index].Activate();
                Debug.Log("index la:" + index);
            }
        }
  }

  public void AddScore() {
    // Add and update score.
    score += 10;
    scoreText.text = $"{score}";

        if(score % 100 == 0)
        {
            int rate = Random.Range(1, 3);
            switch (rate)
            {
                case 1:
                    textCongratulate.text = "EXCELLENT!";
                    break;
                case 2:
                    textCongratulate.text = "WELL DONE!";
                    break;
                case 3:
                    textCongratulate.text = "GODD JOB!";
                    break;
            }
            AudioManager.instance.PlaySFX("welldone");
            textCongratulate.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                DOVirtual.DelayedCall(1, () =>
                {
                    textCongratulate.transform.localScale = Vector3.zero;
                });
            });
        }
  }

    public void VibrateBomb()
    {
        camera.transform.DOShakePosition(0.3f, 0.2f, 40, 90, false, true);
    }

    
}
