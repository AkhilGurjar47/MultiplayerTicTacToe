using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] GameObject crossArrowGameObject;
    [SerializeField] GameObject circleArrowGameObject;
    [SerializeField] GameObject crossYouTextGameObject;
    [SerializeField] GameObject circleYouTextGameObject;

    private void Awake()
    {
        crossArrowGameObject.SetActive(false);
        circleArrowGameObject.SetActive(false);
        crossYouTextGameObject.SetActive(false);
        circleYouTextGameObject.SetActive(false);
    }

    private void Start()
    {
        GameManager.Instance.OnGameStarted += GameManager_OnGameStarted;
        GameManager.Instance.OnCurrentPlayablePlayerTypeChanged += GameManager_OnCurrentPlayablePlayerTypeChanged;
    }

    private void GameManager_OnCurrentPlayablePlayerTypeChanged(object sender, System.EventArgs e)
    {
        UpdateCurrentArrow();
    }

    private void GameManager_OnGameStarted(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.GetLocalPlayerType() == GameManager.PlayerType.Cross)
        {
            crossYouTextGameObject.SetActive(true);
        }
        else
        {
            circleYouTextGameObject.SetActive(true);
        }
        UpdateCurrentArrow();
    }
    private void UpdateCurrentArrow()
    {
        if(GameManager.Instance.GetCurrentPlayablePlayerType() == GameManager.PlayerType.Cross)
        {
            crossArrowGameObject.SetActive(true) ;
            circleArrowGameObject.SetActive(false) ;
        }
        else
        {
            circleArrowGameObject .SetActive(true) ;  
            crossArrowGameObject .SetActive(false) ;
        }
    }
}
