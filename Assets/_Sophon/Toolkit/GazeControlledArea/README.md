## GazeControlledArea
GazeControlledArea是一款在VR中實現物件根據玩家注視狀態自動移動的功能。

## 版本資訊
v0.1: 首次釋出

## 功能特點
- 自動檢測玩家注視物體
- 當玩家注視物體時停止移動
- 可設定移動速度、視野角度等參數
- 可自由添加和移除目標物體

## 如何使用
1. 將GazeControlledArea組件拖拉到用戶頭部物件（例如CenterEyeAnchor）上。
2. moveSpeed屬性：設定物體移動速度。
3. gazeLayerMask屬性：設定注視物體圖層遮罩。
4. stoppingDistance屬性：設定停止移動的距離。
5. fieldOfViewAngle屬性：設定視野角度。
6. targetObjects屬性：將要移動的目標物體添加至列表。

## 注意事項
1. 確保目標物體具有標籤 "GazeControlledObject"。
2. 視野角度設定過大可能導致物體不易停止，過小可能導致物體無法移動。