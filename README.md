# AdxSePlayer

### 説明
　CRI ADX2 LEで作成したサウンドを一行の命令で再生できるスクリプトです。

### 使い方
1.  CRI ADX2 LEを導入する。
2.  ReleaseにあるunityPackageを用途に応じてインポートする。
3.  Hierarchyに空のオブジェクトを作成し、AdxSePlayerをそれにアタッチする。
4.  AdxSePlayer.cueSheetNameに使うキューシートの名前を入力する。
5.  CriAtomSourceが付いたPrefabを一つ作成し、AdxSePlayer.sourcePrefabにアタッチする。
6.  音を鳴らしたいタイミングで、`AdxSePlayer.PlayAudio(音のキー名, 追加オプション)`。

### 追加オプション
- VolumeParam `new VolumeParam(float volume)`
音量を設定できます。
- PitchParam `new PitchParam(float pitch)`
ピッチを設定できます。
- AisacParam `new AisacParam(string aisacName, float aisacValue)`
AISAC値を設定できます。
- BusSendparam `new BusSendParam(string busName, float busSendParam)`
バスへのセンド値を設定できます。

### What is this?
It's static sound effects player for CRI ADX2 LE. You can play sound on your script only use one line method.
