using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    public GameObject[] pages;
    public Button play;
    public Button previous;
    public Button next;
    public Image panel;

    private FadeInAndOut fadeInAndOut;
    private int pageIndex = 0;

    private void Start()
    {
        fadeInAndOut = GameObject.Find("Fader").GetComponent<FadeInAndOut>();
    }

    public void PlayButton()
    {
        FadeToLevel();
    }

    public void PreviousButton()
    {
        next.interactable = true;
        pages[pageIndex].SetActive(false);
        pageIndex--;
        pages[pageIndex].SetActive(true); 
        if (pageIndex == 0)
            previous.interactable = false;
    }

    public void NextButton()
    {
        previous.interactable = true;
        pages[pageIndex].SetActive(false);
        pageIndex++;
        pages[pageIndex].SetActive(true);
        if (pageIndex == pages.Length-1)
            next.interactable = false;
    }

    private void FadeToLevel()
    {
        previous.interactable = false;
        play.interactable = false;
        next.interactable = false;
        
        fadeInAndOut.DoFade(2);
    }

}
