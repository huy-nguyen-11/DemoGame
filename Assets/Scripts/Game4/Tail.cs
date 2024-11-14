using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tail : MonoBehaviour
{
    [SerializeField] private List<Image> listIcons;
    [SerializeField] private List<Sprite> listSprites;
    [SerializeField] private List<string> listColors;

    public int num;
    void Start()
    {
        AssignRandomIcons();
    }

    private void AssignRandomIcons()
    {
        List<Sprite> shuffledSprites = new List<Sprite>(listSprites);
        List<string> shuffledColors = new List<string>(listColors);

        for (int i = 0; i < shuffledSprites.Count; i++)
        {
            int randomIndex = Random.Range(0, shuffledSprites.Count) + num; 
            if(randomIndex > shuffledSprites.Count - 1)
            {
                randomIndex = num;
            }
            Sprite temp = shuffledSprites[i];  
            shuffledSprites[i] = shuffledSprites[randomIndex];
            shuffledSprites[randomIndex] = temp;

            string tempColor = shuffledColors[i];
            shuffledColors[i] = shuffledColors[randomIndex];
            shuffledColors[randomIndex] = tempColor;
        }

        for (int i = 0; i < listIcons.Count; i++)
        {
            if (i < shuffledSprites.Count)
            {
                listIcons[i].sprite = shuffledSprites[i];
                listIcons[i].transform.parent.GetComponent<Image>().color = HexToColor(shuffledColors[i]);
            }
        }
    }

    public Color HexToColor(string hex)
    {
        Color color = Color.black;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}
