using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class GameObjectController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.DOLocalMove(new Vector3(0f, 0.95f, 0f), Random.Range(1.75f, 2.5f)).OnComplete(() =>
        {
            Destroy(this.gameObject);
        });
    }

    public void DestroyItem() {
        this.transform.DOPause();

        this.transform.DOScale(new Vector3(0f, 0f, 0f), 0.75f).OnComplete(() =>
        {
            Destroy(this.gameObject);
        });
    }
}
