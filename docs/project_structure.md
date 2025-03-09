# EscapeSimulationプロジェクト構成

## プロジェクト概要
このプロジェクトはUnityを使用した避難シミュレーションシステムです。被験者の避難行動や危険エリアからの脱出をシミュレートし、データを収集・分析することができます。

## ディレクトリ構造
```
EscapeSimulation/
├── Assets/                     # メインのプログラムコードとリソース
│   ├── Scenes/                # Unityのシーンファイル
│   ├── Resources/             # ゲーム内リソース
│   ├── StandaloneFileBrowser/ # ファイルブラウザ関連
│   └── [各種スクリプトファイル]
├── ProjectSettings/           # Unityプロジェクトの設定
├── UserSettings/             # ユーザー固有の設定
└── Packages/                 # 依存パッケージ
```

## 主要なスクリプトファイル

### コアシステム
- `MainScene.cs`
  - メインシーンの制御
  - シミュレーションのコア機能の実装
  - シーン内のオブジェクト管理

- `Stimulus.cs`
  - 刺激の生成と制御
  - 刺激パラメータの管理
  - 刺激表示のタイミング制御

- `Cricket.cs`
  - シミュレーション対象の動作制御
  - 移動パラメータの管理
  - 衝突判定の処理

### 危険エリア関連
- `DangerArea.cs`
  - 危険エリアの定義
  - エリア内の状態管理
  - 危険レベルの制御

- `ExitDangerArea.cs`
  - 危険エリアからの脱出ロジック
  - 脱出経路の計算

- `ExitDangerRoad.cs`
  - 危険な道路からの脱出処理
  - 道路上での避難行動制御

### データ管理
- `FileManeger.cs`
  - ファイルシステムの基本操作
  - ファイル入出力の管理

- `FileReadWrite.cs`
  - データの読み書き処理
  - ファイルフォーマットの処理

- `CsvSave.cs`
  - シミュレーションデータのCSV形式保存
  - データフォーマットの定義
  - 保存処理の実装

### ユーザーインターフェース
- `FID_Slider.cs`
  - スライダーUIコンポーネント
  - パラメータ調整インターフェース

- `Speed_Input.cs`
  - 速度パラメータの入力処理
  - 値の検証と適用

- `Menu.cs`
  - メニュー画面の制御
  - ユーザー操作の管理

### 衝突検知システム
- `Collider.cs`
  - 衝突判定の基本ロジック
  - オブジェクト間の干渉チェック

## リソースファイル
- `stimulus.png`: 通常状態の刺激画像
- `stimulusRed.png`: 警告状態の刺激画像
- `criket.png`: シミュレーション対象のスプライト

## 依存関係
- Unity Engine: シミュレーションの基盤
- System.Drawing.dll: 描画機能ライブラリ

## 設定ファイル
- `ProjectSettings/`: Unityプロジェクトの基本設定
- `UserSettings/`: ユーザー固有の環境設定
- `Assembly-CSharp.csproj`: C#プロジェクト設定
- `EscapeSimulation.sln`: ソリューション設定

## 開発環境
- Unity
- C#
- Visual Studio / Visual Studio Code

## 注意事項
- プロジェクトの実行にはUnityの適切なバージョンが必要です
- シミュレーションデータは適切なパーミッションでアクセス可能なディレクトリに保存される必要があります
- System.Drawing.dllは.NET Frameworkの一部として必要です 