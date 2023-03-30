using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class GameObjectController : MonoBehaviour
{
    // Start is called before the first frame update

    public void PopUp()
    {
        this.transform.DOScale(new Vector3(1.45f, 1.45f, 1.45f), Random.Range(0.75f, 1.55f)).From(Vector3.zero).SetEase(Ease.InExpo).OnComplete(() =>
        {

        });
    }
    private void Start()
    {

    }

    public void Move()
    {
        //StartCoroutine(CoroutineMove());

        this.transform.DOLocalMove(new Vector3(0f, 0.95f, 0f), Random.Range(3.75f, 4.55f)).OnComplete(() =>
        {
            Destroy(this.gameObject);
        });
    } 

    public void DestroyItem() {
        this.transform.DOPause();

        this.transform.DOScale(new Vector3(0f, 0f, 0f), 0.65f).OnComplete(() =>
        {
            Destroy(this.gameObject);
        });
    }
}
