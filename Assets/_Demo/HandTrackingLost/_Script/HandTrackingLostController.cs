using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HandTrackingLostController : MonoBehaviour
{
    //How to change Material and its properties at runtime in Unity?
    //https://ouzaniabdraouf.medium.com/how-to-change-material-and-its-properties-at-runtime-in-unity-b316fab93f26

/*    [SerializeField] */OVRHand _lefthand;
/*    [SerializeField] */OVRHand _righthand;

    [SerializeField] SkinnedMeshRenderer leftHandSkinnedMeshRenderer; //具體原因不明提示: 必須直接綁定，無法通過GameObject.Find方式取得，否則手部追蹤失效功能會無效，
    [SerializeField] SkinnedMeshRenderer rightHandSkinnedMeshRenderer;

    //[SerializeField] GameObject l_handMeshNode;

    Material left_mtr;
    Material right_mtr;

    bool IsFadeOutAnimation_RightHand = false;
    bool IsFadeOutAnimation_LeftHand = false;

    bool IsFadeInAnimation_RightHand = false;
    bool IsFadeInAnimation_LeftHand = false;

    bool LeftHandEnable = true;
    bool RightHandEnable = true;

    private void Awake()
    {
        GameObject OVRLeftHandObject = GameObject.Find("LeftOVRHand");
        _lefthand = OVRLeftHandObject.GetComponent<OVRHand>();

        GameObject OVRRightHandObject = GameObject.Find("RightOVRHand");
        _righthand = OVRRightHandObject.GetComponent<OVRHand>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //_info = GetComponent<Text>();
        left_mtr = new Material(leftHandSkinnedMeshRenderer.material);
        right_mtr = new Material(rightHandSkinnedMeshRenderer.material);

        //left_mtr.SetFloat("_Opacity", 0.2f);        

        leftHandSkinnedMeshRenderer.material = left_mtr;
        rightHandSkinnedMeshRenderer.material = right_mtr;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.LogWarning(_lefthand.IsTracked);        
        //Debug.LogWarning(_lefthand.HandConfidence);

        if (_lefthand.IsTracked)
        {
            HandFadeInAnimation_LeftHand();
        }
        else
        {
            HandFadeOutAnimation_LeftHand();
        }

        if (_righthand.IsTracked)
        {
            HandFadeInAnimation_RightHand();
        }
        else
        {
            HandFadeOutAnimation_RightHand();        
        }
    }

    void HandFadeOutAnimation_RightHand()
    {
        if (!RightHandEnable 
            ||
            IsFadeOutAnimation_RightHand  
            ||
            IsFadeInAnimation_RightHand
            )
            return;

        DOTween.To(delegate (float value)
        {
            right_mtr.SetFloat("_Opacity", value);   

        }, 0.79f, 0.0f, 0.35f).SetEase(Ease.InQuad).OnComplete(() =>
        {
            
        });

        DOTween.To(delegate (float value)
        {
            right_mtr.SetFloat("_OutlineOpacity", value);

        }, 1.0f, 0.0f, 0.35f).SetEase(Ease.InQuad).OnComplete(() =>
        {
            IsFadeOutAnimation_RightHand = false;
        });

        IsFadeOutAnimation_RightHand = true;
        RightHandEnable = false;
    }
    void HandFadeOutAnimation_LeftHand()
    {
        if (!LeftHandEnable
            ||
            IsFadeOutAnimation_LeftHand 
            ||
            IsFadeInAnimation_LeftHand
            )
            return;

        DOTween.To(delegate (float value)
        {
            left_mtr.SetFloat("_Opacity", value);

        }, 0.79f, 0.0f, 0.35f).SetEase(Ease.InQuad).OnComplete(() =>
        {
            
        });

        DOTween.To(delegate (float value)
        {
            left_mtr.SetFloat("_OutlineOpacity", value);

        }, 1.0f, 0.0f, 0.35f).SetEase(Ease.InQuad).OnComplete(() =>
        {
            IsFadeOutAnimation_LeftHand = false;
        });

        IsFadeOutAnimation_LeftHand = true;
        LeftHandEnable = false;
    }


    void HandFadeInAnimation_RightHand()
    {
        if (RightHandEnable
            ||
            IsFadeOutAnimation_RightHand  
            ||
            IsFadeInAnimation_RightHand
            )
            return;

        DOTween.To(delegate (float value)
        {
            right_mtr.SetFloat("_Opacity", value);

        }, 0.0f, 0.79f, 0.45f).SetEase(Ease.InQuad).OnComplete(() =>
        {
    
        });

        DOTween.To(delegate (float value)
        {
            right_mtr.SetFloat("_OutlineOpacity", value);

        }, 0.0f, 1.0f, 0.45f).SetEase(Ease.InQuad).OnComplete(() =>
        {
            IsFadeInAnimation_RightHand = false;
        });

        IsFadeInAnimation_RightHand = true;
        RightHandEnable = true;
    }
    void HandFadeInAnimation_LeftHand()
    {
        if (LeftHandEnable
            ||
            IsFadeOutAnimation_LeftHand  
            ||
            IsFadeInAnimation_LeftHand)
            return;

        DOTween.To(delegate (float value)
        {
            left_mtr.SetFloat("_Opacity", value);

        }, 0.0f, 0.79f, 0.45f).SetEase(Ease.InQuad).OnComplete(() =>
        {

        });

        DOTween.To(delegate (float value)
        {
            left_mtr.SetFloat("_OutlineOpacity", value);

        }, 0.0f, 1.0f, 0.45f).SetEase(Ease.InQuad).OnComplete(() =>
        {
            IsFadeInAnimation_LeftHand = false;
        });

        IsFadeInAnimation_LeftHand = true;
        LeftHandEnable = true;
    }
}