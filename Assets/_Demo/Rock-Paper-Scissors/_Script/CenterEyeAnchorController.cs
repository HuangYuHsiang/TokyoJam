using UnityEngine;

public class CenterEyeAnchorController : MonoBehaviour
{
    public GameManager_Mode3 gameManager; // 引用 GameManager

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GazeControlledObject")) // 
        {
            gameManager.GameOver(); // 通知 GameManager 有碰撞
        }
    }
}