# CustomScriptCreator

CustomScriptCreatorは、Unityエディタ上で新しいC#スクリプトを効率的に作成できるカスタムツールです。  
クラス名・継承クラス・テンプレートをGUIから選択し、プロジェクトに即座にスクリプトを生成できます。

## テンプレートのインストールについて

**推奨:**  
テンプレートアセットは、Unityのパッケージマネージャの「サンプル」からインストールすることをおすすめします。  
「CustomScriptCreator」パッケージを選択し、「サンプル」タブからテンプレートをインポートしてください。  
これにより、すぐに利用可能なテンプレートがプロジェクトに追加されます。

## 主な機能

- **クラス名・継承クラスの指定**  
  プルダウンまたは手入力で基底クラスを選択可能。  
  継承なし（No Base Class）も選択できます。

- **テンプレートによるスクリプト生成**  
  ScriptTemplateアセットを利用し、独自のテンプレートからスクリプトを生成。  
  テンプレート内の`#CLASSNAME#`や`#BASECLASS#`は自動で置換されます。

## セットアップ

1. **アセットの作成**  
   - `Assets > Create > ScriptableObjects > Custom Script Creator > Base Class Options`  
     継承クラスの選択肢を定義するアセットを作成します。
   - `Assets > Create > ScriptableObjects > Custom Script Creator > Script Template`  
     スクリプトテンプレートを定義するアセットを作成します。

2. **ウィンドウの表示**  
   - メニューから `Tools > Custom Script Creator` を選択  
   - または、プロジェクトビューの右クリックメニューから `Create > Custom Script` を選択

## 使い方

1. **BaseClassOptions** と **ScriptTemplate** アセットをウィンドウで指定
2. クラス名・継承クラス・テンプレートを選択
3. 「作成」ボタンを押すと、指定した内容で新しいスクリプトが生成されます

## スクリプトテンプレート例

テンプレートでは、`#CLASSNAME#` がクラス名に、`#BASECLASS#` が継承クラス名に置換されます。

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// #CLASSNAME# クラス
/// #BASECLASS# クラスを継承
/// </summary>
public class #CLASSNAME# : #BASECLASS#
{
	// 初期化処理
	void Start()
	{

	}

	// 毎フレーム処理
	void Update()
	{

	}
}

```

上記のテンプレートの例では、クラスのドキュメントコメントが自動で生成されます。  