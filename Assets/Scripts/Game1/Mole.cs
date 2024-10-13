using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Mole : MonoBehaviour {
  [Header("Graphics")]
  [SerializeField] private Sprite mole;
  [SerializeField] private Sprite moleHardHat;
  [SerializeField] private Sprite moleHatBroken;
  [SerializeField] private Sprite moleHit;
  [SerializeField] private Sprite moleHatHit;
  [SerializeField] private Sprite orangeSP ,carotSP, chewSP, appleSP;

  [Header("GameManager")]
  [SerializeField] private GameManager gameManager;

  // The offset of the sprite to hide it.
  private Vector2 startPosition = new Vector2(0f, -2.56f);
  private Vector2 endPosition = Vector2.zero;
  // How long it takes to show a mole.
  private float showDuration = 0.4f;
  private float duration = 0.6f;

  private SpriteRenderer spriteRenderer;
  private Animator animator;
  private BoxCollider2D boxCollider2D;
  private Vector2 boxOffset;
  private Vector2 boxSize;
  private Vector2 boxOffsetHidden;
  private Vector2 boxSizeHidden;

  // Mole Parameters 
  private bool hittable = true;
  public enum MoleType { Standard, HardHat, Bomb, orange, carot, chew, apple};
  private MoleType moleType;
  private float hardRate = 0.25f;
  private float bombRate = 0;
  private int moleIndex = 0;

    private bool isTap = false;
    public GameObject fruitGO;

    public GameObject effect1, effect2 , explosion;

    private void Awake()
    {
        // Get references to the components we'll need.
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        // Work out collider values.
        boxOffset = boxCollider2D.offset;
        boxSize = boxCollider2D.size;
        boxOffsetHidden = new Vector2(boxOffset.x, -startPosition.y / 2f);
        boxSizeHidden = new Vector2(boxSize.x, 0f);

        fruitGO.SetActive(false);

        effect1.transform.localScale = Vector3.zero;
        effect1.GetComponent<Image>().DOFade(0, 0);
        explosion.SetActive(false);
    }

    private IEnumerator ShowHide(Vector2 start, Vector2 end) {
    // Make sure we start at the start.
    transform.localPosition = start;
        AudioManager.instance.PlaySFX("spawn");
    // Show the mole.
    float elapsed = 0f;
    while (elapsed < showDuration) {
      transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
      boxCollider2D.offset = Vector2.Lerp(boxOffsetHidden, boxOffset, elapsed / showDuration);
      boxCollider2D.size = Vector2.Lerp(boxSizeHidden, boxSize, elapsed / showDuration);
      // Update at max framerate.
      elapsed += Time.deltaTime;
      yield return null;
    }

    // Make sure we're exactly at the end.
    transform.localPosition = end;
    boxCollider2D.offset = boxOffset;
    boxCollider2D.size = boxSize;

        // Wait for duration to pass.
        if (!isTap)
        {
            Debug.Log("not tap!");
            yield return new WaitForSeconds(duration);

            // Hide the mole.
            elapsed = 0f;
            while (elapsed < showDuration)
            {
                transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
                boxCollider2D.offset = Vector2.Lerp(boxOffset, boxOffsetHidden, elapsed / showDuration);
                boxCollider2D.size = Vector2.Lerp(boxSize, boxSizeHidden, elapsed / showDuration);
                // Update at max framerate.
                elapsed += Time.deltaTime;
                yield return null;
            }
            // Make sure we're exactly back at the start position.
            transform.localPosition = start;
            boxCollider2D.offset = boxOffsetHidden;
            boxCollider2D.size = boxSizeHidden;

            gameManager.isHaveMole = false;
        }
    }

  public void Hide() {
    // Set the appropriate mole parameters to hide it.
    transform.localPosition = startPosition;
    boxCollider2D.offset = boxOffsetHidden;
    boxCollider2D.size = boxSizeHidden;
  }

  private IEnumerator QuickHide() {
    yield return new WaitForSeconds(0.25f);
        if (!hittable)
        {
            Debug.Log("here!");
            float elapsed = 0f;
            while (elapsed < showDuration)
            {
                transform.localPosition = Vector2.Lerp(endPosition, startPosition, elapsed / showDuration);
                boxCollider2D.offset = Vector2.Lerp(boxOffset, boxOffsetHidden, elapsed / showDuration);
                boxCollider2D.size = Vector2.Lerp(boxSize, boxSizeHidden, elapsed / showDuration);
                // Update at max framerate.
                elapsed += Time.deltaTime;
                yield return null;
            }
            Hide();
            isTap = false;
            gameManager.isHaveMole = false;
            //yield return new WaitForSeconds(0.6f);

        }
  }

    private IEnumerator QuickHideForFruit()
    {
        yield return new WaitForSeconds(0.1f);
        if (!hittable)
        {
            Hide();
            isTap = false;
            //gameManager.isHaveMole = false;
        }
    }

    private void OnMouseDown() {
    if (hittable && gameManager.playing) {
      switch (moleType) {
        case MoleType.Standard:
                    AudioManager.instance.PlaySFX("whack");
                    hittable = false;
                    spriteRenderer.sprite = moleHit;
                    isTap = true;
                    PlayFX();
                    Debug.Log("taped!");
                    gameManager.AddScore();
          // Stop the animation
          StopAllCoroutines();
         
          StartCoroutine(QuickHide());
          // Turn off hittable so that we can't keep tapping for score.
          break;
                case MoleType.apple:
                    hittable = false;
                    AudioManager.instance.PlaySFX("fruit");
                    //spriteRenderer.sprite = moleHit;
                    fruitGO.SetActive(true); 
                    isTap = true;
                    PlayFX();
                    gameManager.AddScore();
                    //Stop the animation
                    StopAllCoroutines();
                    StartCoroutine(QuickHideForFruit());
                    // Turn off hittable so that we can't keep tapping for score.
                    
                    break;

                case MoleType.orange:
                    hittable = false;
                    AudioManager.instance.PlaySFX("fruit");
                    //spriteRenderer.sprite = moleHit;
                    fruitGO.SetActive(true);
                    isTap = true;
                    PlayFX();
                    gameManager.AddScore();
                    // Stop the animation
                    StopAllCoroutines();
                    StartCoroutine(QuickHideForFruit());
                    // Turn off hittable so that we can't keep tapping for score.

                    break;

                case MoleType.chew:
                    hittable = false;
                    AudioManager.instance.PlaySFX("fruit");
                    //spriteRenderer.sprite = moleHit;
                    fruitGO.SetActive(true);
                    isTap = true;
                    PlayFX();
                    gameManager.AddScore();
                    // Stop the animation
                    StopAllCoroutines();
                    StartCoroutine(QuickHideForFruit());
                    // Turn off hittable so that we can't keep tapping for score.

                    break;

                case MoleType.carot:
                    hittable = false;
                    AudioManager.instance.PlaySFX("fruit");
                    // spriteRenderer.sprite = moleHit;
                    fruitGO.SetActive(true);
                    isTap = true;
                    PlayFX();
                    gameManager.AddScore();
                    // Stop the animation
                    StopAllCoroutines();
                    StartCoroutine(QuickHideForFruit());
                    // Turn off hittable so that we can't keep tapping for score.

                    break;

                case MoleType.Bomb:
                    // Game over, 1 for bomb
                    hittable = false;
                    AudioManager.instance.PlaySFX("explosion");
                    isTap = true;
                    StopAllCoroutines();
                    StartCoroutine(Bomb());
                    gameManager.DecreaseHealth();
                    gameManager.VibrateBomb();
                    break;
      }
    }
  }

  private void CreateNext() {
    float random = Random.Range(0f, 1f);
    if (random <= 0.33f) {
      // Make a bomb.
      moleType = MoleType.Bomb;
      // The animator handles setting the sprite.
      animator.enabled = true;

    } else {
      animator.enabled = false;
      random = Random.Range(0f, 1f);

      if(random < 0.5f && random >0){
        // Create a standard one.
        moleType = MoleType.Standard;
        spriteRenderer.sprite = mole;
      }else if (random < 0.65f && random >= 0.5f)
            {
                // Create a standard one.
                moleType = MoleType.apple;
                fruitGO.GetComponent<SpriteRenderer>().sprite = appleSP;
                spriteRenderer.sprite = appleSP;
            }
            else if (random < 0.8f && random >= 0.65f)
            {
                // Create a standard one.
                moleType = MoleType.chew;
                spriteRenderer.sprite = chewSP;
                fruitGO.GetComponent<SpriteRenderer>().sprite = chewSP;
            }
            else if (random < 0.9f && random >= 0.85f)
            {
                // Create a standard one.
                moleType = MoleType.orange;
                spriteRenderer.sprite = orangeSP;
                fruitGO.GetComponent<SpriteRenderer>().sprite = orangeSP;
            }
            else if (random <= 1f && random >= 0.9f)
            {
                // Create a standard one.
                moleType = MoleType.carot;
                spriteRenderer.sprite = carotSP;
                fruitGO.GetComponent<SpriteRenderer>().sprite = carotSP;
            }

        }
    // Mark as hittable so we can register an onclick event.
    hittable = true;
  }
  public void Activate() {
   // SetLevel(level);
    CreateNext();
    StartCoroutine(ShowHide(startPosition, endPosition));
  }

  // Used by the game manager to uniquely identify moles. 
  public void SetIndex(int index) {
    moleIndex = index;
  }

  // Used to freeze the game on finish.
  public void StopGame() {
    hittable = false;
    StopAllCoroutines();
  }

    void PlayFX()
    {
        effect1.GetComponent<SpriteRenderer>().DOFade(1, 0.2f);
        effect1.transform.DOScale(1.3f, 0.2f).OnComplete(() =>
        {
            effect1.GetComponent<SpriteRenderer>().DOFade(0, 0.3f);
        });
        effect2.GetComponent<ParticleSystem>().Play();
    }

    IEnumerator Bomb()
    {
        Hide();
        explosion.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        explosion.SetActive(false);
        gameManager.isHaveMole = false;
    }
}
