# CustomScriptCreator

CustomScriptCreatorは、Unityエディタ上で新しいC#スクリプトを効率的に作成できるカスタムツールです。  
クラス名・継承クラス・テンプレートをGUIから選択し、プロジェクトに即座にスクリプトを生成できます。

![Image](https://github-production-user-asset-6210df.s3.amazonaws.com/124390814/469724372-7054a9f7-5564-406e-94ed-b1359b1edc31.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20250723%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250723T110345Z&X-Amz-Expires=300&X-Amz-Signature=588753e3ba96f7ef4c3255ec4fe6ea81661c5d0f269f5c971af9955407deacb2&X-Amz-SignedHeaders=host)

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

     ![Image_AssetsCreate](https://github-production-user-asset-6210df.s3.amazonaws.com/124390814/469724375-95f73302-1b50-4afa-b4ff-1cf2a98f3727.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20250723%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250723T110351Z&X-Amz-Expires=300&X-Amz-Signature=9b156bccdd57c02bef5bd0b54c79d459b851c0d8d99c5ef95083ce1aacb68224&X-Amz-SignedHeaders=host)

2. **ウィンドウの表示**  
   - メニューから `Tools > Custom Script Creator` を選択  
   - または、プロジェクトビューの右クリックメニューから `Create > Custom Script` を選択

     ![Image_Window](https://github-production-user-asset-6210df.s3.amazonaws.com/124390814/469724376-6d76517c-f1fe-482c-9fed-ab0f6101ab8b.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20250723%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250723T110356Z&X-Amz-Expires=300&X-Amz-Signature=0697811815d4e7d818d3c2e6db6fd55df8f71fcf48d1da5864d6c5b925bebe68&X-Amz-SignedHeaders=host)

## 使い方

1. **BaseClassOptions** と **ScriptTemplate** アセットをウィンドウで指定
2. クラス名・継承クラス・テンプレートを選択
3. 「作成」ボタンを押すと、指定した内容で新しいスクリプトが生成されます

   ![Image_Use](https://github-production-user-asset-6210df.s3.amazonaws.com/124390814/469724373-460f91c4-3180-46ee-85c0-c1ea282bc361.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20250723%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250723T110401Z&X-Amz-Expires=300&X-Amz-Signature=8f10942f30dd855b30f9ce6997577931f4a8b19a455085c0f0a32937ec088104&X-Amz-SignedHeaders=host)

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

![Image_Template](https://github-production-user-asset-6210df.s3.amazonaws.com/124390814/469726782-eeb93f16-27bd-46b9-94e0-d51be4ad1110.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20250723%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250723T110938Z&X-Amz-Expires=300&X-Amz-Signature=86dfa14645717bc7c62734877ff214d248c0d4d33c89bb62e868bc7fee0a7ad8&X-Amz-SignedHeaders=host)

上記のテンプレートの例では、クラスのドキュメントコメントが自動で生成されます。  

