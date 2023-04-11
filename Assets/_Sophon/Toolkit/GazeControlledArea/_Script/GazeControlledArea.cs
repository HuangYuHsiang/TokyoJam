using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sophon.Toolkit
{
    #region 說明
    //將此腳本添加到您想要實現該功能的遊戲物體上（在本例中為多個Cube）。
    //在Unity編輯器中，將XR Rig或Camera Rig的頭部位置轉換（例如OVRPlayerController的CenterEyeAnchor）分配給腳本的playerHead字段。
    //設定stoppingDistance為您希望在玩家視線方向上檢測物體的範圍。
    //設定fieldOfViewAngle為玩家的視角範圍（例如60度）。
    //設置LayerMask以使射線僅與玩家的頭部位置（例如眼球）交互。
    //現在，當玩家視線在多個物體上時，這些物體會停止移動。當視線離開這些物體時，它們會朝玩家頭部移動。注意，您需要為所有受此功能影響的物體（例如多個Cube）添加此腳本。
    #endregion

    public class GazeControlledArea : MonoBehaviour
    {
        public float moveSpeed = 1.0f; // 移動速度
        public LayerMask gazeLayerMask; // 注視物體圖層遮罩
        public float stoppingDistance = 5.0f; // 停止移動的距離
        public float fieldOfViewAngle = 60.0f; // 視野角度
        public List<GameObject> targetObjects; // 要移動的目標物體列表
        private RaycastHit hit;

        private void Update()
        {
            // 獲取玩家頭部朝向
            Vector3 playerHeadDirection = transform.TransformDirection(Vector3.forward);

            // 搜索玩家頭部位置附近的碰撞器
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, stoppingDistance, gazeLayerMask);

            // 遍歷目標物體列表
            foreach (GameObject targetObject in targetObjects)
            {
                bool shouldStop = false;

                // 遍歷碰撞器
                foreach (Collider hitCollider in hitColliders)
                {
                    // 如果碰撞器的標籤是 "GazeControlledObject"
                    if (hitCollider.transform.CompareTag("GazeControlledObject"))
                    {
                        // 計算物體與玩家頭部的方向
                        Vector3 directionToObject = hitCollider.transform.position - transform.position;
                        // 計算玩家頭部朝向與物體方向之間的角度
                        float angle = Vector3.Angle(playerHeadDirection, directionToObject);

                        // 如果角度小於視野角度的一半
                        if (angle < fieldOfViewAngle * 0.5f)
                        {
                            // 玩家正在看著物體，應該停止移動
                            shouldStop = true;
                            break;
                        }
                    }
                }

                // 如果應該停止移動
                if (shouldStop)
                {
                    continue;
                }
                else
                {
                    // 移動目標物體朝玩家頭部
                    targetObject.transform.position = Vector3.MoveTowards(targetObject.transform.position, transform.position, moveSpeed * Time.deltaTime);
                }
            }
        }
    }
}
