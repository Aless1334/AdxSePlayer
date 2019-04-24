# AdxSePlayer

### 説明
　CRI ADX2 LEで作成したサウンドを一行の命令で再生できるスクリプトです。

### 使い方
1.  CRI ADX2 LEを導入する。
2.  4つの.csをインポートする。
3.  UniRxをプロジェクトに導入していない場合、AtomSourcePoolOnUniRx.cs関連の書式を消す（後々修正予定）。
4.  Hierarchyに空のオブジェクトを作成し、AdxSePlayerをそれにアタッチする。
5.  AdxSePlayer.cueSheetNameに使うキューシートの名前を入力する。
6.  CriAtomSourceが付いたPrefabを一つ作成し、AdxSePlayer.sourcePrefabにアタッチする。
7.  音を鳴らしたいタイミングで、`AdxSePlayer.PlayAudio(音のキー名[, volume, pitch])`。

### What is this?
It's static sound effects player for CRI ADX2 LE. You can play sound on your script only use one line method.
