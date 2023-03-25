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
