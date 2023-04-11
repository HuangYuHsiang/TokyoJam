##版本資訊
v0.2: 增加平滑效果
v0.1: 首次釋放


#開發需求
- 讓Dialog和PokeButton這兩個物件可以隨著用戶視角轉動
- 參考影片:https://youtube.com/shorts/kmHm3r0NgBs

##快速說明
- FollowEyeUI:在vr中，UI會跟著頭部視角而自動轉動，讓玩家無論在哪個方向，都可以清楚看到UI。

##範例場景說明
- FollowEyeUI-Demo: 畫面上的UI會跟著頭部視角移動
- FollowEyeUI-Demo2: UI綁定手部(OVRCameraRig>>TrackingSpace>>RightHandAnchor)移動，UI方向會跟著視角移動

##How to use
1. 直接將FollowEyeUIHelper拖曳到要跟著頭部視角轉動的物件
2. To Rotate: 綁定旋轉物件
3. Target: 綁定CenterEyeAnchor,或其他對應的物體
4. X,Y Axis Lock: 可以設定物體是否鎖定某個軸不要旋轉
5. rotationSmoothTime: "提高"數值可以讓選轉更加即時，但有可能會較不平滑
