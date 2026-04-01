# SummerMoblieGame
<a name="japanese"></a>
## 概要
本プロジェクトは、Androidモバイルデバイス固有の入力方式に特化した技術実験として制作しました。タッチスクリーンのボタンや仮想スティックではなく、端末のジャイロスコープ・加速度センサーを利用した傾きによる操作でゲームプレイを実現することを目的としています。
ハードウェアセンサーからの生データの読み取り・ノイズ除去・スムージング処理を経て、物理的な傾きを快適で予測しやすいゲーム内移動に変換するという、入力プログラミング・物理演算・ゲームフィールの交点に位置する課題に取り組みました。

![altgif](https://media3.giphy.com/media/aUovxH8Vf9qDu/giphy.gif)

## 主な技術的取り組み
**ジャイロスコープ・加速度センサー入力**
- Unityのセンサーアクセスに入力を Input.gyro および Input.acceleration で取得
- センサーノイズによるジッタを除去しつつ意図的な動きを保持するローパスフィルタを適用
- 起動時にデバイス向きのキャリブレーションを実施し、プレイヤーが自然に持った状態が移動ゼロ（ニュートラル）になるよう調整 — 傾いたデフォルト状態を防ぐ
- Androidのセンサー座標系とUnityのワールド空間の間のプラットフォーム固有の座標変換に対応

**物理ベースの移動**
- センサー入力を位置や速度に直接反映するのではなく、Rigidbodyへの力ベクトル（ForceMode）として変換して適用
- これにより、ゲームオブジェクトが傾きによって慣性を持ち、急に止まらず自然に減速するような物理的な重みのある移動感を実現
- 傾き角度に応じたクランプとスケーリングにより、小さな傾きは緩やかな移動、大きな傾きは速い移動へと比例的に制御

**ゲームループ・状態管理**
- 入力読み取り・入力処理・移動適用をそれぞれ独立したコンポーネントで担当するクリーンな分離設計
- シンプルなステートマシンによるゲーム状態（アイドル / プレイ中 / ゲームオーバー）の管理
- フルシーンリロードなしのスコアトラッキングとセッション再開

**モバイルUI対応**
- UnityのCanvas Scalerを参照解像度方式で使用し、複数のAndroid画面サイズに対応したUIスケーリング
- モーション操作と並行したタッチベースのポーズ・再開コントロール

## アーキテクチャ・設計方針
| パターン | 使用箇所 |
| ----------- | ----------- |
| コンポーネント分離 | 入力読み取り・物理適用・ゲーム状態を独立したMonoBehaviourで分割 |
| ローパスフィルタ | ジャイロ生データへの高周波ノイズ除去のために適用 |
| ステートマシン | enum ベースのシンプルなゲームフロー制御 |
| 物理駆動の移動 | Transformの直接操作ではなく、RigidbodyへのForceMode適用 |

本プロジェクトで最も挑戦的だったのはセンサーキャリブレーションでした。ジャイロスコープの生データはデバイスによって大きく異なり、プレイヤーの端末の持ち方によっても変化します。この問題を解決するため、セッション開始時に基準姿勢を取得し、以降のすべての回転をその姿勢からの相対値として算出する方式を採用しました（絶対的なワールド空間への依存を排除）。

| 技術スタック | |
| ----------- | ----------- |
| エンジン | Unity (LTS) |
| 言語 | C# |
| 対象プラットフォーム | Android |
| 入力API | Unity Input System — Input.gyro、Input.acceleration|
| 物理演算 | Unity Rigidbody（ForceMode.Force） |
| バージョン管理 | Git |

## 学んだこと
- モバイルにおけるハードウェアセンサーデータの読み取りと処理の方法、およびフィルタリングがプレイヤー体験に与える影響
- ジャイロスコープ（回転速度）と加速度センサー（線形加速度＋重力）の違いと、各センサーの使いどころ
- 傾き操作において、Transform直接操作よりも物理駆動入力の方がなぜ気持ちよく感じられるか — そのトレードオフも含めて理解
- デバイスを問わない入力キャリブレーション設計 — プレイヤーの端末の持ち方に依存しないゲームの実現
- Androidビルド設定、APK署名、Unityにおけるモバイルのパフォーマンスチューニングについての実践的な知見

<a name="english"></a>
## Overview
This project was built as a focused technical experiment into mobile-specific input on Android: using the phone's gyroscope and accelerometer to control in-game movement by physically tilting the device, rather than relying on touchscreen buttons or virtual joysticks.
The goal was to understand how to read raw hardware sensor data, apply smoothing to eliminate noise, and translate physical rotation into responsive, predictable game movement — a problem that sits at the intersection of input programming, physics, and player feel.

## Core Technical Focus
**Gyroscope & Accelerometer Input**
- Reads raw rotation data from Input.gyro and Input.acceleration via Unity's sensor API
- Applies low-pass filtering to smooth out jitter from sensor noise while preserving intentional movement
- Handles device orientation calibration on startup so that the player's natural hold position maps to neutral movement — avoiding a tilted default state
- Accounts for platform-specific sensor coordinate remapping between Android's sensor frame and Unity's world space

**Physics-Based Movement**
- Sensor input is translated into force vectors applied to a Rigidbody rather than directly setting position or velocity
- This creates naturally weighted movement that feels physical — the game object gains momentum from tilt and slows gradually, not abruptly
- Movement is clamped and scaled relative to tilt angle, so small tilts produce gentle movement and sharp tilts produce fast movement in a proportional, controllable way

**ゲームループ・状態管理**
- Clean separation between input reading, input processing, and movement application — each handled in its own component
- Game state (idle / playing / game over) managed through a simple state machine
- Score tracking and session restart without requiring a full scene reload

**モバイルUI対応**
- UI scaled using Unity's Canvas Scaler with reference resolution to support multiple Android screen sizes
- Touch-based pause and restart controls alongside the motion input

## Architecture & Design Decisions
| Pattern | Where Used |
| ----------- | ----------- |
| Component Separation | Input reading, physics application, and game state are independent MonoBehaviours |
| Low-Pass Filter | Applied to raw gyro data to remove high-frequency sensor noise |
| State Machine | Simple enum-based game state to control flow cleanly |
| Physics-Driven Movement | ForceMode applied to Rigidbody rather than direct Transform manipulation |

The most challenging part of this project was sensor calibration — raw gyroscope data varies significantly between devices and changes depending on how the player is holding the phone. Solving this required capturing a baseline orientation at session start and computing all subsequent rotations relative to that offset rather than to absolute world space.

| Technical Stack | |
| ----------- | ----------- |
| Engine | Unity (LTS) |
| Language | C# |
| Target Platform | Android |
| Input API | Unity Input System — Input.gyro、Input.acceleration|
| Physics | Unity Rigidbody（ForceMode.Force） |
| Version Control | Git |

## What I Learned
- How to read and process raw hardware sensor data on mobile and why filtering matters for player experience
- The difference between gyroscope (rotation rate) and accelerometer (linear + gravity) data, and when to use each
- Why physics-driven input feels better than direct transform input for tilt controls — and the tradeoffs involved
- How to handle device-agnostic input calibration so the game works regardless of how tightly or loosely a player holds the phone
- Practical experience with Android build settings, APK signing, and mobile performance considerations in Unity
