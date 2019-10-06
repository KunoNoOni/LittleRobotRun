using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInAndOut : MonoBehaviour 
{
    public Animator animator;

    private int sceneIndex;

    public void DoFade(int sceneIndexNumber)
    {
        sceneIndex = sceneIndexNumber;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
