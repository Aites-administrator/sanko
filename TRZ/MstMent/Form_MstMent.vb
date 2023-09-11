Imports T.R.ZCommonClass.DgvForm01
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonCtrl
Imports T.R.ZCommonClass.clsCommonFnc

Public Class Form_MstMent
  Implements IDgvForm01

  '----------------------------------------------
  '          マスタメンテナンス画面
  '
  '
  '----------------------------------------------


#Region "定数定義"

  ''' <summary>
  ''' コントロール配置開始位置（縦）
  ''' </summary>
  Private Const START_POS_Y As Integer = 20

  ''' <summary>
  ''' コントロール配置開始位置（横）
  ''' </summary>
  Private Const START_POS_X As Integer = 20

  ''' <summary>
  ''' コントロール間マージン（横）
  ''' </summary>
  Private Const MARGIN_X As Integer = 20

  ''' <summary>
  ''' コントロール間マージン（縦）
  ''' </summary>
  ''' <remarks>
  '''   コントロールの高さに対するパーセンテージで記述
  ''' </remarks>
  Private Const MARGIN_YP As Integer = 200

  ''' <summary>
  ''' コントロール高さ
  ''' </summary>
  Private Const CONTROL_HEIGHT As Integer = 16

  ''' <summary>
  ''' 1文字の大きさ
  ''' </summary>
  Private Const TEXT_SIZE = 12

  ''' <summary>
  ''' マスタMDBファイルパス
  ''' </summary>
  Private Const MST_ACCESS_PATH As String = "D:\trasa\dat\MST.mdb"

  Private Const PRG_ID As String = "mstmente"
#End Region

#Region "構造体"
  Private Structure ItemSetting
    Dim Fild_Name As String
    Dim Fild_Caption As String
    Dim Fild_Type As Integer
    Dim Fild_Leng As Integer
    Dim Fild_Comment As Integer
    Dim Fild_Key As Integer
    Dim Fild_Dsp As Integer
  End Structure
#End Region

#Region "メンバ"

#Region "プライベート"
  Private _TblSum As New clsTblSum
  Private _GridWidth As Single = 0
  Private _MdbName As String = String.Empty
#End Region

#End Region

#Region "データグリッドビュー操作関連共通"

  '１つ目のDataGridViewオブジェクト格納先
  Private DG1V1 As DataGridView

  ''' <summary>
  ''' 初期化処理
  ''' </summary>
  ''' <remarks>
  ''' コントロールの初期化（Form_Loadで実行して下さい）
  ''' </remarks>
  Private Sub InitForm01() Implements IDgvForm01.InitForm

    '１つ目のDataGridViewオブジェクトの設定
    DG1V1 = Me.DataGridView1

    ' グリッド初期化
    Call InitGrid(DG1V1, CreateGrid1Src1(), CreateGrid1Layout1())

  End Sub

  ''' <summary>
  ''' 一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   画面毎の抽出内容をここに記載する
  ''' </remarks>
  Private Function CreateGrid1Src1() As String Implements IDgvForm01.CreateGridSrc

    Return CreateSelSqlByTblSum(_TblSum.ListItems)
  End Function


  ''' <summary>
  ''' 一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid1Layout1() As List(Of clsDGVColumnSetting) Implements IDgvForm01.CreateGridlayout
    Dim ret As New List(Of clsDGVColumnSetting)

    ' 表示順に並べ替え
    _TblSum.ListItems.Sort(Function(a, b) a.OrderNo - b.OrderNo)

    ' 表示項目設定
    For idx As Integer = 0 To _TblSum.ListItems.Count - 1
      With _TblSum.ListItems(idx)
        ' カラム幅取得
        Dim tmpColWidth As Single = CalcColumnWidth(_TblSum.ListItems(idx))

        ' 文字配置設定
        Dim tmpAlignment As typAlignment
        Select Case .Type
          Case clsTblSum.typDataType.Dt_Numeric
            tmpAlignment = typAlignment.MiddleRight
          Case clsTblSum.typDataType.Dt_Date
            tmpAlignment = typAlignment.MiddleCenter
          Case Else
            tmpAlignment = typAlignment.MiddleLeft
        End Select

        ' 全体幅を加算
        _GridWidth += tmpColWidth

        ret.Add(New clsDGVColumnSetting(.Title _
                                        , .Name _
                                        , argTextAlignment:=tmpAlignment _
                                        , argColumnWidth:=tmpColWidth _
                                        , artTitleFontSize:=8))

        ' リンクフィールドが存在する場合は追加
        If .LinkTbl <> "" Then

          Dim tmpListItem As New clsTblSum.ListItem
          tmpListItem = GetLnkTblStat(.LinkFile, .LinkTbl, .LinkDspFld)
          ret.Add(New clsDGVColumnSetting(IIf(tmpListItem.Title = .Title, "名称", tmpListItem.Title) _
                                        , .LinkDspFld _
                                        , argTextAlignment:=typAlignment.MiddleLeft _
                                        , argColumnWidth:=CalcColumnWidth(tmpListItem) _
                                        , artTitleFontSize:=8))
          ' 全体幅を加算
          _GridWidth += tmpColWidth
        End If
      End With
    Next

    If _GridWidth < btnRegist.Width + btnDelete.Width + btnClose.Width Then
      _GridWidth = btnRegist.Width + btnDelete.Width + btnClose.Width + 40
    End If
    Return ret
  End Function

  ''' <summary>
  ''' 一覧表示編集可能カラム設定
  ''' </summary>
  ''' <returns>作成した編集可能カラム配列</returns>
  Public Function CreateGrid1EditCol1() As List(Of clsDataGridEditTextBox) Implements IDgvForm01.CreateGridEditCol
    Dim ret As New List(Of clsDataGridEditTextBox)

    With ret
      '.Add(New clsDataGridEditTextBox("タイトル", prmUpdateFnc:=AddressOf [更新時実行関数], prmValueType:=[データ型]))
    End With

    Return ret
  End Function
#End Region

#Region "メソッド"

#Region "プライベート"

#Region "コントロール配置関連"

  ''' <summary>
  ''' 編集用コントロールを画面サイズに合わせて配置
  ''' </summary>
  Private Sub PlacementEditControl()
    Dim tmpPosY As Integer = START_POS_Y
    Dim tmpPosX As Integer = START_POS_X

    For Each tmpEditItem As clsTblSum.EditItem In _TblSum.EditItems
      With tmpEditItem
        If (.Label.Width + .Control.Width + tmpPosX + MARGIN_X) > Me.Width Then
          ' 画面に収まらない場合は1段下げて配置
          tmpPosX = START_POS_X
          tmpPosY += CONTROL_HEIGHT * MARGIN_YP / 100
        End If

        .Label.Location = New Point(tmpPosX, tmpPosY + 2)
        tmpPosX += .Label.Width
        .Control.Location = New Point(tmpPosX, tmpPosY)
        tmpPosX += .Control.Width + MARGIN_X

      End With
    Next

  End Sub

  ''' <summary>
  ''' 一覧表示グリッドに合わせて画面サイズを修正する
  ''' </summary>
  Private Sub ReSizeForm()

    Me.DataGridView1.Width = Me.Width - 40

    'Gridサイズ設定
    If Me.DataGridView1.Width > _GridWidth Then
      Me.DataGridView1.Width = _GridWidth + (_TblSum.ListItems.Count * 5)
    End If

    ' フォームサイズ設定
    RemoveHandler MyBase.Resize, AddressOf Form_Resize
    Me.Width = Me.DataGridView1.Width + 40
    AddHandler MyBase.Resize, AddressOf Form_Resize

    ' 編集コントロール再配置
    Call PlacementEditControl()

    ' Grid再配置
    Dim LastControl As Control = _TblSum.EditItems(_TblSum.EditItems.Count - 1).Control

    With Me.DataGridView1
      '位置修正
      .Location = New Point(.Location.X, LastControl.Location.Y + LastControl.Height + 20)
      '高さ修正
      .Height = Me.Height - .Location.Y - btnClose.Height - 70
    End With


    ' コマンドボタン再配置
    Dim tmpBtnLocationY As Integer = Me.DataGridView1.Height + Me.DataGridView1.Location.Y + 20
    With btnClose
      .Location = New Point(Me.Width - 30 - .Width, tmpBtnLocationY)
    End With

    With btnDelete
      .Location = New Point(Me.Width - 30 - btnClose.Width - .Width - 20, tmpBtnLocationY)
    End With

    With btnRegist
      .Location = New Point(Me.Width - 30 - btnClose.Width - btnDelete.Width - .Width - 20 - 20, tmpBtnLocationY)
    End With

    ' インフォメーションラベル再配置
    With lblInformation
      .Location = New Point(10, Me.Height - .Height - 50)
    End With
  End Sub

  ''' <summary>
  ''' データ表示用カラムサイズを計算
  ''' </summary>
  ''' <param name="prmListItem">表示対象データ</param>
  ''' <returns>Gridサイズ</returns>
  ''' <remarks>
  '''  入力可能最大文字数もしくはタイトル文字長より必要幅を計算する
  ''' </remarks>
  Private Function CalcColumnWidth(prmListItem As clsTblSum.ListItem) As Single
    Dim tmpLength As Integer

    With prmListItem

      ' 入力可能最大文字数とタイトル文字列長を比較し大きい方を基準値に
      If .Title.Length >= .MaxChar Then
        tmpLength = .Title.Length
      Else
        ' 入力可能最大文字数を基準値とする場合、数値形式・日付形式なら縮小
        If .Type = clsTblSum.typDataType.Dt_Numeric _
            Or .Type = clsTblSum.typDataType.Dt_Date Then
          tmpLength = .MaxChar / 1.5
        Else
          tmpLength = .MaxChar / 1.2
        End If
      End If

    End With

    ' 短い文字は調整
    If tmpLength <= 8 Then tmpLength *= 1.8
    If tmpLength <= 5 Then tmpLength *= 2

    Return tmpLength * TEXT_SIZE
  End Function

  ''' <summary>
  ''' 設定情報を画面に反映
  ''' </summary>
  Private Sub EditList2EditControl()
    Dim tmpPosY As Integer = START_POS_Y
    Dim tmpPosX As Integer = START_POS_X

    ' 編集用コントロールを全件作成
    For idx As Integer = 0 To _TblSum.EditItems.Count - 1
      ' 入力テキストボックス（&見出しラベル）作成
      CreateEditControl(_TblSum.EditItems(idx))
    Next

    ' 編集用コントロール配置
    Call PlacementEditControl()

  End Sub

  ''' <summary>
  ''' 設定情報より入力用コントロール作成する
  ''' </summary>
  ''' <param name="prmEditItemData">設定情報</param>
  Private Sub CreateEditControl(ByRef prmEditItemData As clsTblSum.EditItem)

    Dim tmpLbl As New Label
    Dim tmpTxtBox As Control

    '---------------------------------------------
    '               見出しラベル作成
    '---------------------------------------------
    With tmpLbl
      .AutoSize = True
      .Text = prmEditItemData.Title

      ' 一覧選択形式の入力項目には見出しラベルの終端に(*)を付与する
      If prmEditItemData.LnkType = 1 Then
        .Text &= "(*)"
      End If
    End With
    Me.Controls.Add(tmpLbl)   ' 画面に追加しないと ラベルのAutoSizeが機能しません


    '---------------------------------------------
    '         入力用テキストボックス作成
    '---------------------------------------------
    If prmEditItemData.Type = clsTblSum.typDataType.Dt_Date Then
      ' 日付形式
      tmpTxtBox = New TxtDateBase()
    ElseIf prmEditItemData.Type <> clsTblSum.typDataType.Dt_Numeric Then
      ' 文字形式
      tmpTxtBox = New TxtBase(prmEditItemData.MaxChar / 2)
    Else
      ' 数値形式
      If prmEditItemData.LnkType <> 1 AndAlso prmEditItemData.LinkFile <> "MST.MDB" Then
        '直接入力
        tmpTxtBox = New TxtNumericBase(prmEditItemData.MaxChar)
      Else
        '一覧選択画面よりの入力
        Select Case prmEditItemData.LnkFld
          Case "TJCODE" ' 屠場コード入力
            tmpTxtBox = New TxtMstTojyo()
          Case "GNCODE" ' 原産地コード入力
            tmpTxtBox = New TxtMstGensanchi()
          Case "SRCODE" ' 仕入先コード入力
            tmpTxtBox = New TxtMstShiresaki()
          Case "KKCODE" ' 格付けコード入力
            tmpTxtBox = New TxtMstKakuduke
          Case "SBCODE" ' 種別コード入力
            tmpTxtBox = New TxtMstSyubetsu()
          Case "KICODE" ' 規格コード入力
            tmpTxtBox = New TxtMstKikaku()
          Case "GBCODE"
            tmpTxtBox = New TxtMstGB()
          Case "BLOCKCODE"
            tmpTxtBox = New TxtMstBlockCode()
          Case "CMCODE", "LCOMMENT"
            tmpTxtBox = New TxtMstComment()
          Case "TKCODE", "SSCODE"
            tmpTxtBox = New TxtMstCustomer()
          Case "SZCODE"
            tmpTxtBox = New TxtMstManufacturing()
          Case "BICODE"
            tmpTxtBox = New TxtMstItem
          Case Else     ' 不明、数値入力
            tmpTxtBox = New TxtNumericBase(prmEditItemData.MaxChar)
        End Select
      End If
    End If

    ' 入力補助メッセージ設定
    With CType(tmpTxtBox, TxtBase)
      .SetMsgLabel(Me.lblInformation)
      .SetMsgLabelText(prmEditItemData.Comment)
    End With

    With tmpTxtBox
      .Name = "txt_" & prmEditItemData.Name
      If prmEditItemData.MaxChar > 4 Then
        .Width = prmEditItemData.MaxChar / 2 * TEXT_SIZE
      Else
        .Width = prmEditItemData.MaxChar * TEXT_SIZE * 1.5
      End If
      .Height = CONTROL_HEIGHT
      .Tag = prmEditItemData
    End With

    '---------------------------------------------
    '     コントロールをフォームに追加
    '---------------------------------------------
    Me.Controls.Add(tmpTxtBox)

    '---------------------------------------------
    '      コントロールをリストに保持
    '---------------------------------------------
    prmEditItemData.Control = tmpTxtBox
    prmEditItemData.Label = tmpLbl

    '---------------------------------------------
    '      コントロールにイベントを追加
    '---------------------------------------------
    AddHandler tmpTxtBox.Validating, AddressOf EditTxtValidating
    AddHandler tmpTxtBox.Enter, AddressOf TxtEnter

  End Sub

  ''' <summary>
  ''' 設定情報保持
  ''' </summary>
  ''' <param name="prmTblName">対象テーブル</param>
  ''' <param name="prmDspGrpNo">対象グループ</param>
  Private Sub GetItemSetting(prmTblName As String _
                             , prmDspGrpNo As Integer)

    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable

    Call tmpDb.GetResult(tmpDt, SqlSelTblSum(prmTblName))

    With tmpDt
      If .Rows.Count > 0 Then
        For idx As Integer = 0 To .Rows.Count - 1
          ' 表示グループ番号保持
          Dim tmpDspGrpNo As Integer = Integer.Parse(.Rows(idx)("Fild_DspGrp").ToString)
          Dim tmpFildName As String = .Rows(idx)("Fild_Name").ToString.ToUpper

          ' 送信IDが1以上なら削除方法をフラグ方式に設定
          If _TblSum.DeleteType = clsTblSum.typDelKind.Direct _
            AndAlso Integer.Parse(.Rows(idx)("Fild_Send_No")) > 0 Then
            _TblSum.DeleteType = clsTblSum.typDelKind.Flag
          End If

          If .Rows(idx)("FildNo").ToString = "999" Then
            ' 対象テーブル名取得
            _TblSum.TblName = prmTblName
            _TblSum.MstName = .Rows(idx)("Fild_Caption").ToString
          Else
            ' 一覧表示項目取得
            ' 以下を対象外とする
            ' ・名称が "KUBUN"
            ' ・名称が"SFLG"
            If (tmpFildName.Equals("KUBUN") = False) _
              AndAlso tmpFildName.Equals("SFLG") = False Then
              _TblSum.AddListItem(.Rows(idx))
            End If

            ' 編集項目取得
            ' 以下の条件を全て満たす場合、編集項目として設定
            ' ・表示順(Fild_Dsp)が非表示(0)より大きい
            ' ・表示グループ(Fild_DspGrp)が指定された値もしくは0
            If Integer.Parse(.Rows(idx)("Fild_Dsp")) > 0 _
              AndAlso (tmpDspGrpNo = 0 _
                      OrElse tmpDspGrpNo = prmDspGrpNo) Then
              _TblSum.AddEditItem(.Rows(idx))
            End If

          End If
        Next
      End If
    End With

  End Sub

  ''' <summary>
  ''' 全コンロトール入力値クリア
  ''' </summary>
  Private Sub ClearControl()
    For Each tmpEditItem As clsTblSum.EditItem In _TblSum.EditItems
      With tmpEditItem
        DirectCast(.Control, TxtBase).Text = ""
      End With
    Next
  End Sub


  Private Function GetLnkTblStat(prmDbName As String _
                                , prmTblName As String _
                                , prmFldName As String) As clsTblSum.ListItem
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable
    Dim tmpTblSum As New clsTblSum

    Try
      Call tmpDb.GetResult(tmpDt, SqlSelLinkTblStat(prmDbName, prmTblName, prmFldName))
      tmpTblSum.AddListItem(tmpDt.Rows(0))
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("リンクテーブルのステータス取得に失敗しました。")
    End Try

    Return tmpTblSum.ListItems(0)
  End Function

#End Region

#Region "SQL文作成関連"

  ''' <summary>
  ''' テーブルメンテ用SQL文の作成
  ''' </summary>
  ''' <param name="prmEnableDataOnly">
  '''  有効データ抽出フラグ
  '''   True  : 有効データのみ抽出
  '''   False : 削除済みデータ
  ''' </param>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  ''' テーブル[TBL_SUM]に登録されている情報からテーブルメンテ用SQL文を作成する
  ''' </remarks>
  Private Function CreateSelSqlByTblSum(prmListItems As List(Of clsTblSum.ListItem) _
                                        , Optional prmEnableDataOnly As Boolean = True) As String
    Dim sql As String = String.Empty
    Dim tmpLnkTblCount As Integer = 0

    sql &= " SELECT " & _TblSum.TblName & ".* "
    For Each tmpListItem As clsTblSum.ListItem In prmListItems
      With tmpListItem
        If .LinkTbl <> "" Then
          sql &= ", " & .LinkTbl & "." & .LinkDspFld
        End If
      End With
    Next
    sql &= " FROM " & _TblSum.TblName

    For Each tmpListItem As clsTblSum.ListItem In prmListItems
      With tmpListItem
        If .LinkTbl <> "" Then
          sql &= " LEFT JOIN " & .LinkTbl & " ON " & _TblSum.TblName & "." & .Name & "= " & .LinkTbl & "." & .LinkFld & ") "
          tmpLnkTblCount += 1
        End If
      End With
    Next

    For idx As Integer = 0 To tmpLnkTblCount - 1
      sql = sql.Replace("FROM", " FROM (")
    Next

    If _TblSum.DeleteType = clsTblSum.typDelKind.Flag And prmEnableDataOnly Then
      sql &= " WHERE " & _TblSum.TblName & ".KUBUN <> 9 "
    End If
    sql &= " ORDER BY " & _TblSum.Sortkey

    Return sql
  End Function

  ''' <summary>
  ''' 設定情報取得SQL文作成
  ''' </summary>
  ''' <param name="prmTblName">対象テーブル名</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelTblSum(prmTblName As String) As String
    Dim sql As String = String.Empty

    sql &= " SELECT * "
    sql &= " FROM TBL_SUM "
    sql &= " WHERE TBLNAME = '" & prmTblName & "'"
    sql &= " ORDER BY Fild_DspGrp,Fild_Dsp "

    Return sql
  End Function

  ''' <summary>
  ''' 検索条件を作成
  ''' </summary>
  ''' <returns></returns>
  Private Function CreateSqlWhere(Optional prmbRunAccess As Boolean = False) As String
    Dim sql As String = String.Empty

    For Each tmpEditItem As clsTblSum.EditItem In _TblSum.EditItems
      With tmpEditItem

        ' 主キー設定されている項目のみ対象
        If .Index Then
          sql &= .Name & " = "
          sql &= GetDelimiter(.Type, prmbRunAccess)
          sql &= DirectCast(.Control, TextBox).Text
          sql &= GetDelimiter(.Type, prmbRunAccess)
          sql &= " AND "
        End If
      End With
    Next

    ' 最終の "AND" を削除
    If Not sql.Equals(String.Empty) Then
      sql = Mid(sql, 1, Len(sql) - Len("AND "))
    End If

    Return sql
  End Function

  ''' <summary>
  ''' データ更新用SQL文作成
  ''' </summary>
  ''' <param name="prmUpdMode">
  '''   更新モード
  '''   0 : 通常更新
  '''   1 : 無効データ（KUBUN=9）の再利用
  '''   2 : 削除処理
  ''' </param>
  ''' <returns>作成したSQL文</returns>
  Private Function CreateUpDateSql(Optional prmUpdMode As Integer = 0 _
                                   , Optional prmbRunAccess As Boolean = False) As String
    Dim sql As String = String.Empty
    Dim tmpDate As String = ComGetProcTime()
    Dim tmpDatedelimiter As String = IIf(prmbRunAccess, "#", "'")

    sql &= " UPDATE " & _TblSum.TblName
    sql &= " SET "

    If prmUpdMode = 2 Then
      ' 削除処理
      sql &= "KUBUN = 9 "
      sql &= " , "
    Else
      ' 更新処理

      If prmUpdMode = 1 Then
        ' 無効データのコードを再利用
        sql &= "KUBUN = 1 "
        sql &= " , "
      End If

      ' 通常更新
      For Each tmpEditItem As clsTblSum.EditItem In _TblSum.EditItems
        With tmpEditItem

          ' インデックスデータ（＝キー項目）以外が更新対象
          If .Index = False Then
            Dim tmpValue As String = DirectCast(.Control, TxtBase).Text.Trim
            If tmpValue = "" And .Type = clsTblSum.typDataType.Dt_Numeric Then
              tmpValue = "NULL"
            End If

            sql &= " " & .Name & " = "
            sql &= GetDelimiter(.Type, prmbRunAccess)
            sql &= tmpValue
            sql &= GetDelimiter(.Type, prmbRunAccess)
            sql &= " ,"
          End If
        End With
      Next
    End If

    If _TblSum.HasKdate Then
      sql &= " KDATE = " & tmpDatedelimiter & tmpDate & tmpDatedelimiter
    Else
      sql = sql.Substring(0, sql.Length - 1)
    End If

    ' 抽出条件を追加
    sql = ComAddSqlSearchCondition(sql, CreateSqlWhere())

    If Me.lblLastUpdate.Text <> String.Empty AndAlso prmbRunAccess = False AndAlso _TblSum.HasKdate Then
      sql = ComAddSqlSearchCondition(sql, "KDATE =" & tmpDatedelimiter & Me.lblLastUpdate.Text & tmpDatedelimiter)
    End If

    Return sql
  End Function

  ''' <summary>
  ''' データ追加用SQL文作成
  ''' </summary>
  ''' <returns></returns>
  Private Function CreateInsertSql(Optional prmbRunAccess As Boolean = False) As String
    Dim sql As String = String.Empty

    ' 追加する値をカンマで連結した文字列を保持
    ' Values(値,値,値,値,値, .....)
    Dim tmpValues As String = String.Empty

    ' 項目名 をカンマで連結した文字列を保持
    ' Insert Into TBL( [項目名], [項目名], [項目名]....)
    Dim tmpDst As String = String.Empty

    ' 現在時刻を取得
    Dim tmpDate As String = ComGetProcTime()

    For Each tmpEditItem As clsTblSum.EditItem In _TblSum.EditItems

      With tmpEditItem
        Dim tmpValue As String = Trim(DirectCast(.Control, TxtBase).Text & "")

        ' 必須入力でない未入力の項目は除外する
        If .Required OrElse tmpValue <> "" Then
          tmpDst &= .Name & ","

          tmpValues &= GetDelimiter(.Type, prmbRunAccess)
          tmpValues &= tmpValue
          tmpValues &= GetDelimiter(.Type, prmbRunAccess)
          tmpValues &= ","
        End If
      End With
    Next

    If _TblSum.HasKdate = False Then
      tmpValues = tmpValues.Substring(0, tmpValues.Length - 1)
      tmpDst = tmpDst.Substring(0, tmpDst.Length - 1)
    Else
      tmpDst &= "TDATE,KDATE"
      If prmbRunAccess Then
        tmpValues &= "#" & tmpDate & "#,#" & tmpDate & "#"
      Else
        tmpValues &= "'" & tmpDate & "','" & tmpDate & "'"
      End If
    End If

    If _TblSum.DeleteType = clsTblSum.typDelKind.Flag Then
      tmpDst &= ",KUBUN,SFLG"
      tmpValues &= ",1,1"
    End If

    sql &= " INSERT INTO " & _TblSum.TblName & "(" & tmpDst & ")"
    sql &= " VALUES (" & tmpValues & ")"

    Return sql
  End Function

  ''' <summary>
  ''' データ削除用SQL文作成
  ''' </summary>
  ''' <returns></returns>
  Private Function CreateDeleteSql(Optional prmbRunAccess As Boolean = False) As String
    Dim sql As String = String.Empty

    sql &= " DELETE FROM " & _TblSum.TblName

    ' 抽出条件を追加
    sql = ComAddSqlSearchCondition(sql, CreateSqlWhere())

    If Me.lblLastUpdate.Text <> String.Empty Then
      If prmbRunAccess = False AndAlso _TblSum.HasKdate Then
        sql = ComAddSqlSearchCondition(sql, "KDATE ='" & Me.lblLastUpdate.Text & "'")
      End If
    End If

    Return sql
  End Function

  ''' <summary>
  ''' 区切り文字取得
  ''' </summary>
  ''' <param name="prmType">データ型</param>
  ''' <returns></returns>
  Private Function GetDelimiter(prmType As clsTblSum.typDataType _
                                , Optional prmbRunAccess As Boolean = False) As String
    Dim tmpDelimiter As String = String.Empty

    Select Case prmType
      Case clsTblSum.typDataType.Dt_Numeric      ' 数値
        tmpDelimiter = ""
      Case clsTblSum.typDataType.Dt_Date         ' 日付
        If prmbRunAccess Then
          tmpDelimiter = "#"
        Else
          tmpDelimiter = "'"
        End If
      Case Else                                  ' その他
        tmpDelimiter = "'"
    End Select

    Return tmpDelimiter
  End Function

  Private Function SqlSelLinkTblStat(prmDbName As String _
                                     , prmTblName As String _
                                     , prmFldName As String) As String
    Dim sql As String = String.Empty

    sql &= " SELECT * "
    sql &= " FROM TBL_SUM "
    sql &= " WHERE TBLNAME = '" & prmTblName & "'"
    sql &= "  AND Fild_Name = '" & prmFldName & "'"
    sql &= "  AND DbNAME = '" & prmDbName & "'"

    Return sql
  End Function

#End Region

#Region "データ取得関連"

  ''' <summary>
  ''' データベースより値を取得し画面の編集項目に反映
  ''' </summary>
  Private Sub GetData()
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable

    Try
      ' SQL文実行
      tmpDb.GetResult(tmpDt, ComAddSqlSearchCondition(CreateSelSqlByTblSum(_TblSum.ListItems), CreateSqlWhere()))

      ' データは存在するか？
      If tmpDt.Rows.Count <= 0 Then
        ' 存在しません
        Me.lblLastUpdate.Text = String.Empty   ' 更新日付クリア
      Else
        '存在します

        'データベースより取得した値を画面の編集項目に反映
        For Each tmpEditItem As clsTblSum.EditItem In _TblSum.EditItems
          With tmpEditItem
            DirectCast(.Control, TextBoxBase).Text = tmpDt.Rows(0)(.Name).ToString()
          End With
        Next

        ' 更新日付設定
        If tmpDt.Columns.Contains("KDATE") Then
          Me.lblLastUpdate.Text = tmpDt.Rows(0)("KDATE").ToString()
        Else
          Me.lblLastUpdate.Text = String.Empty
        End If
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception(_TblSum.TblName & "の取得に失敗しました")
    End Try

  End Sub

  ''' <summary>
  ''' データ存在チェック
  ''' </summary>
  ''' <returns></returns>
  Private Function ExistsData(Optional prmEnableDataOnly As Boolean = True) As Boolean
    Dim ret As Boolean = False
    Dim tmpDt As New DataTable
    Dim tmpDb As New clsSqlServer

    Try
      ' SQL文実行
      ' 無効データも含め検索を行う
      tmpDb.GetResult(tmpDt, ComAddSqlSearchCondition(CreateSelSqlByTblSum(_TblSum.ListItems, prmEnableDataOnly), CreateSqlWhere()))
      ret = (tmpDt.Rows.Count = 1)

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("マスターデータの存在チェックに失敗しました")
    End Try

    Return ret
  End Function
#End Region

#Region "データ更新関連"

  ''' <summary>
  ''' マスタ新規追加・更新処理
  ''' </summary>
  Private Sub RegistData()
    Dim tmpDb As New clsSqlServer
    Dim sql As String = String.Empty

    ' MDB接続（平行稼働時のみ）
    Dim tmpMstAccess As New clsComDatabase
    Dim sqlAccess As String = String.Empty
    With tmpMstAccess
      .DataSource = MST_ACCESS_PATH
      .Provider = clsComDatabase.typProvider.Mdb
    End With

    ' SQL文の作成
    If ExistsData() Then
      ' 有効データが存在する場合は更新処理
      sql = CreateUpDateSql()                           ' SQL-Server用
      sqlAccess = CreateUpDateSql(prmbRunAccess:=True)  ' Access用
    ElseIf ExistsData(False) Then
      ' 無効データが存在する場合はデータ再利用処理
      If MsgBox("このコードは過去に削除されています。" _
                & vbCrLf & "コードを再利用しますか？", vbYesNo + vbInformation) = vbYes Then
        sql = CreateUpDateSql(prmUpdMode:=1)
        sqlAccess = CreateUpDateSql(prmUpdMode:=1, prmbRunAccess:=True)  ' Access用
      End If
    Else
      ' データが存在しない場合は追加処理
      sql = CreateInsertSql()
      sqlAccess = CreateInsertSql(prmbRunAccess:=True)
    End If

    Try
      tmpDb.TrnStart()
      tmpMstAccess.TrnStart()

      If tmpDb.Execute(sql) <> 1 _
        Or tmpMstAccess.Execute(sqlAccess) <> 1 Then
        Throw New Exception("更新に失敗しました。他のユーザーによって修正されている可能性があります。")
      Else
        tmpMstAccess.TrnCommit()
        tmpDb.TrnCommit()
        ' 一覧再表示
        Controlz(DG1V1.Name).ShowList()
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      tmpDb.TrnRollBack()
      tmpMstAccess.TrnRollBack()
      Throw New Exception("マスタ更新に失敗しました。")
    Finally
      tmpDb.Dispose()
      tmpMstAccess.Dispose()
    End Try

  End Sub

  ''' <summary>
  ''' マスタ削除処理
  ''' </summary>
  Private Sub DeleteData()
    Dim tmpDb As New clsSqlServer
    Dim sql As String = String.Empty


    ' MDB接続（平行稼働時のみ）
    Dim tmpMstAccess As New clsComDatabase
    Dim sqlAccess As String = String.Empty
    With tmpMstAccess
      .DataSource = MST_ACCESS_PATH
      .Provider = clsComDatabase.typProvider.Mdb
    End With

    ' SQL文の作成
    If _TblSum.DeleteType = clsTblSum.typDelKind.Direct Then
      ' 直接削除
      sql = CreateDeleteSql()
      sqlAccess = CreateDeleteSql(prmbRunAccess:=True)
    Else
      '削除フラグ更新
      sql = CreateUpDateSql(prmUpdMode:=2)
      sqlAccess = CreateUpDateSql(prmUpdMode:=2, prmbRunAccess:=True)
    End If

    Try
      tmpDb.TrnStart()
      tmpMstAccess.TrnStart()

      If tmpDb.Execute(sql) <> 1 _
        OrElse tmpMstAccess.Execute(sqlAccess) <> 1 Then
        Throw New Exception("削除に失敗しました。他のユーザーによって修正されている可能性があります。")
      Else
        tmpDb.TrnCommit()
        tmpMstAccess.TrnCommit()
        ' 一覧再表示
        Controlz(DG1V1.Name).ShowList()
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      tmpDb.TrnRollBack()
      tmpMstAccess.TrnRollBack()
      Throw New Exception("マスタ削除に失敗しました。")
    Finally
      tmpDb.Dispose()
      tmpMstAccess.Dispose()
    End Try

  End Sub

#End Region

#Region "入力チェック関連"
  ''' <summary>
  ''' インデックス項目が全て入力されているか？
  ''' </summary>
  ''' <returns>
  '''   True  : インデックス項目は全て入力されている
  '''   False : インデックス項目に未入力に項目が存在する
  ''' </returns>
  Private Function ChkIndexControl() As Boolean
    Dim ret As Boolean = True

    Try
      For Each tmpEditItem As clsTblSum.EditItem In _TblSum.EditItems
        With tmpEditItem
          ' インデックス項目に値が入力されているか？
          If .Index Then
            ret = ret And (DirectCast(.Control, TextBox).Text.Trim <> "")
          End If
        End With
      Next
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("インデックス項目の入力チェックに失敗しました。")
    End Try

    Return ret
  End Function

  ''' <summary>
  ''' 必須入力コントロール値チェック
  ''' </summary>
  '''   True  : 必須入力項目は全て入力されている
  '''   False : 必須入力項目に未入力に項目が存在する
  Private Function ChkRequiredControl(ByRef prmErrControls As List(Of clsTblSum.EditItem)) As Boolean
    Dim ret As Boolean = True

    Try
      For Each tmpEditItem As clsTblSum.EditItem In _TblSum.EditItems
        With tmpEditItem
          If .Required Then
            Dim tmpChk As Boolean = (DirectCast(.Control, TextBox).Text.Trim <> "")

            ' 必須入力かつ未入力のコントロールを配列で返す
            If tmpChk = False Then
              prmErrControls.Add(tmpEditItem)
            End If

            ret = ret And tmpChk
          End If
        End With
      Next

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("必須項目の入力チェックに失敗しました。")
    End Try

    Return ret
  End Function

  ''' <summary>
  ''' 入力形式チェック
  ''' </summary>
  ''' <param name="prmErrControls"></param>
  ''' <returns>
  ''' </returns>
  ''' <remarks>
  ''' 必須入力チェックは[ChkRequiredControl]で行うことっ！
  ''' </remarks>
  Private Function ChkInputValue(ByRef prmErrControls As List(Of clsTblSum.EditItem)) As Boolean
    Dim ret As Boolean = True

    Try
      For Each tmpEditItem As clsTblSum.EditItem In _TblSum.EditItems
        With tmpEditItem
          Dim tmpInputValue As String = DirectCast(.Control, TextBox).Text.Trim
          Dim tmpChk As Boolean

          If (.Required = False And (tmpInputValue = "")) Then
            ' 必須入力項目でないかつ未入力ならチェックOK
            tmpChk = True
          Else
            Select Case .Type
              Case clsTblSum.typDataType.Dt_Numeric
                tmpChk = ComChkNumeric(tmpInputValue)
              Case clsTblSum.typDataType.Dt_Date
                tmpChk = IsDate(tmpInputValue)
              Case clsTblSum.typDataType.Dt_Text
                tmpChk = (tmpInputValue.Length <= .MaxChar)
              Case clsTblSum.typDataType.Dt_Alpha
                tmpChk = (tmpInputValue.Length <= .MaxChar)
              Case Else
                Throw New Exception("テーブル設定エラー [" & _TblSum.TblName & "] 項目[" & .Name & "]のタイプが不正です" _
                                  & .Type & "はデータ種別として設定できません")
            End Select
          End If
          ret = ret And tmpChk
        End With
      Next

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("必須項目の入力チェックに失敗しました。")
    End Try

    Return ret

  End Function
#End Region

#End Region

#End Region

#Region "イベントプロシージャ"

  <STAThread>
  Shared Sub main()
    Call ComStartPrg(PRG_ID, Form_MstMent)
  End Sub

  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_MstMent_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Try
      ' コマンドライン引数取得
      Dim cmds As String() = System.Environment.GetCommandLineArgs()
      Dim tmpGrpNo As Integer
      Dim tmpTblName As String = String.Empty

      ' コマンドライン引数未指定時はテーブル詳細マスタ(TBL_SUM)を起動
      If cmds Is Nothing OrElse cmds.Length = 1 Then
        tmpTblName = "TBL_SUM"
        tmpGrpNo = 0
        _MdbName = "MST.MDB"
      ElseIf cmds.Length = 2 _
              AndAlso Integer.TryParse(cmds(1).Split(",")(2), tmpGrpNo) Then
        tmpTblName = cmds(1).Split(",")(1)
        _MdbName = cmds(1).Split(",")(0).ToString()
      Else
        Throw New Exception("起動パラメータが不正です")
      End If

      If UCase(_MdbName) = UCase("uriage.mdb") Then
        Dim UriageMentPrgPath As String = System.IO.Path.Combine(My.Application.Info.DirectoryPath, "MSTmentUriage.exe")
        Dim p As System.Diagnostics.Process = System.Diagnostics.Process.Start(UriageMentPrgPath, cmds(1))
        End
      End If

      ' データ取得
      Call GetItemSetting(tmpTblName, tmpGrpNo)

      ' 一覧表示
      Call InitForm01()
      With Controlz(DG1V1.Name)
        .AutoSearch = True  ' 自動検索ON
        .lcCallBackCellDoubleClick = AddressOf DgvCellDoubleClick 'ダブルクリック時イベント設定
      End With

      ' 編集用コントロール配置
      Call EditList2EditControl()

      ' グリッドに合わせて画面サイズ変更
      Call ReSizeForm()

      ' 更新日付クリア
      Me.lblLastUpdate.Text = String.Empty
      Me.lblLastUpdate.Visible = False

      ' 1項目目にフォーカスを当てる
      DirectCast(_TblSum.EditItems(0).Control, TxtBase).Focus()

      ' タイトル設定
      Me.Text = _TblSum.MstName & "メンテナンス"

      ' IPC通信起動
      InitIPC(PRG_ID)

    Catch ex As Exception
      Call ComWriteErrLog(ex, False)
      End
    End Try
  End Sub

  ''' <summary>
  ''' 更新時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub EditTxtValidating(sender As Object, e As System.ComponentModel.CancelEventArgs)

    With DirectCast(sender, TxtBase)
      ' 主キーかつテキストが変更されたらDBより値を取得する
      If DirectCast(.Tag, clsTblSum.EditItem).Index _
        AndAlso .Text <> .LastText Then
        If ChkIndexControl() Then
          Call GetData()
        End If
      End If
    End With

  End Sub

  ''' <summary>
  ''' フォーカス取得時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtEnter(sender As Object, e As EventArgs)
    With DirectCast(sender, TxtBase)
      ' IME制御
      If DirectCast(.Tag, clsTblSum.EditItem).Type = clsTblSum.typDataType.Dt_Text Then
        ' 日本語入力
        sender.ImeMode = ImeMode.On
      Else
        ' 半角英数
        sender.ImeMode = ImeMode.Off
      End If

    End With

  End Sub

  ''' <summary>
  ''' 登録ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnRegist_Click(sender As Object, e As EventArgs) Handles btnRegist.Click

    If MsgBox("登録しますか？", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
      Try
        Dim tmpErrItems As New List(Of clsTblSum.EditItem)


        If ChkRequiredControl(tmpErrItems) = False Then ' 必須入力チェック
          ' NG
          ' エラーメッセージ表示
          MsgBox(tmpErrItems(0).Title & "は必須入力項目です", vbOKOnly + vbInformation)
          DirectCast(tmpErrItems(0).Control, TxtBase).Focus()
        ElseIf ChkInputValue(tmpErrItems) = False Then  ' 入力形式チェック
          ' NG
          ' エラーメッセージ表示
          MsgBox(tmpErrItems(0).Title & "の入力形式が不正です", vbOKOnly + vbInformation)
          DirectCast(tmpErrItems(0).Control, TxtBase).Focus()
        Else
          ' OK 
          ' 登録処理実行
          Call RegistData()
          Call ClearControl()
        End If
      Catch ex As Exception
        Call ComWriteErrLog(ex, False)
      End Try
    End If

  End Sub


  ''' <summary>
  ''' 削除ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
    If MsgBox("削除しますか？", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
      Try
        Call DeleteData()
        Call ClearControl()
      Catch ex As Exception
        Call ComWriteErrLog(ex, False)
      End Try
    End If
  End Sub

  ''' <summary>
  ''' 閉じるボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
    Me.Close()
  End Sub

  ''' <summary>
  ''' グリッドダブルクリック時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>
  ''' 選択された値を編集コントロールに設定する
  ''' </remarks>
  Private Sub DgvCellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
    Dim tmpSelectedRow As New Dictionary(Of String, String)(Controlz(DG1V1.Name).SelectedRow)

    'データベースより取得した値を画面の編集項目に反映
    For Each tmpEditItem As clsTblSum.EditItem In _TblSum.EditItems
      With tmpEditItem
        DirectCast(.Control, TextBoxBase).Text = tmpSelectedRow(.Name).ToString()
      End With
    Next

    If tmpSelectedRow.ContainsKey("KDATE") Then
      Me.lblLastUpdate.Text = tmpSelectedRow("KDATE").ToString()
    End If

  End Sub

  ''' <summary>
  ''' フォームサイズ変更時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>
  ''' フォームロード後にイベントを割り当てる
  ''' </remarks>
  Private Sub Form_Resize(sender As Object, e As EventArgs)
    Call ReSizeForm()
  End Sub

#End Region

#Region "インナークラス"

#Region "プライベート"

  ''' <summary>
  ''' テーブル設定操作クラス
  ''' </summary>
  Private Class clsTblSum

#Region "列挙体"

    ''' <summary>
    ''' 削除方法
    ''' </summary>
    Enum typDelKind
      ''' <summary>
      ''' 直接削除
      ''' </summary>
      Direct = 0
      ''' <summary>
      ''' 削除フラグ設定
      ''' </summary>
      Flag
    End Enum

    Enum typDataType
      ''' <summary>
      ''' 数値
      ''' </summary>
      Dt_Numeric = 0

      ''' <summary>
      ''' 日付形式
      ''' </summary>
      Dt_Date = 1

      ''' <summary>
      ''' 文字列
      ''' </summary>
      Dt_Text = 2

      ''' <summary>
      ''' 英字
      ''' </summary>
      Dt_Alpha = 3
    End Enum
#End Region

#Region "構造体"

    ''' <summary>
    ''' 編集項目構造体
    ''' </summary>
    Public Structure EditItem

      ''' <summary>
      ''' 項目名
      ''' </summary>
      ''' <remarks>対応するデータベーステーブルの項目名</remarks>
      Dim Name As String

      ''' <summary>
      ''' データ型
      ''' </summary>
      Dim Type As typDataType

      ''' <summary>
      ''' 主キーか？
      ''' </summary>
      ''' <remarks>
      '''  True :主キー
      '''  False:主キーでない
      ''' </remarks>
      Dim Index As Boolean

      ''' <summary>
      ''' 必須入力か？
      ''' </summary>
      ''' <remarks>
      '''  True :必須入力
      '''  False:必須入力ではない
      ''' </remarks>
      Dim Required As Boolean

      ''' <summary>
      ''' 入力可能最大文字数
      ''' </summary>
      Dim MaxChar As Long

      ''' <summary>
      ''' 項目タイトル
      ''' </summary>
      Dim Title As String

      ''' <summary>
      ''' 入力時のヘルプコンテキスト
      ''' </summary>
      Dim Comment As String

      ''' <summary>
      ''' リンクテーブル
      ''' </summary>
      Dim LnkTbl As String

      ''' <summary>
      ''' リンク項目
      ''' </summary>
      Dim LnkFld As String

      ''' <summary>
      ''' リンクテーブル入力形式
      ''' </summary>
      ''' <remarks>
      '''   0:直接入力
      '''   1:一覧選択
      '''   2:コンボボックス（未実装）
      ''' </remarks>
      Dim LnkType As Integer

      ''' <summary>
      ''' 編集用コントロール
      ''' </summary>
      Dim Control As Control

      ''' <summary>
      ''' 見出し表示用コントロール
      ''' </summary>
      Dim Label As Label

      Dim LinkTbl As String
      Dim LinkFile As String

    End Structure

    ''' <summary>
    ''' 一覧表示項目構造体
    ''' </summary>
    Public Structure ListItem
      ''' <summary>
      ''' 項目名
      ''' </summary>
      ''' <remarks>対応するデータベーステーブルの項目名</remarks>
      Dim Name As String

      ''' <summary>
      ''' 項目タイトル
      ''' </summary>
      Dim Title As String

      ''' <summary>
      ''' データ型
      ''' </summary>
      Dim Type As typDataType

      ''' <summary>
      ''' 主キーか？
      ''' </summary>
      ''' <remarks>
      '''  True :主キー
      '''  False:主キーでない
      ''' </remarks>
      Dim Index As Boolean

      ''' <summary>
      ''' 並び順
      ''' </summary>
      Dim OrderNo As Integer

      ''' <summary>
      ''' 入力可能最大文字数
      ''' </summary>
      Dim MaxChar As Long

      Dim LinkFile As String
      Dim LinkTbl As String
      Dim LinkFld As String
      Dim LinkDspType As String
      Dim LinkDspFld As String

    End Structure
#End Region

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()

      '初期化処理
      Me.EditItems = New List(Of EditItem)
      Me.ListItems = New List(Of ListItem)
      Me.DeleteType = typDelKind.Direct

    End Sub

#End Region

#Region "メンバ"
#Region "プライベート"

    ''' <summary>
    ''' 更新フラグ有無フラグ
    ''' </summary>
    Private _HasKdate As Boolean = False

#End Region
#End Region

#Region "プロパティ"

#Region "パブリック"
    ''' <summary>
    ''' 操作対象テーブル名
    ''' </summary>
    ''' <returns>テーブル名文字列</returns>
    Public Property TblName As String

    ''' <summary>
    ''' 操作対象テーブル日本語名称
    ''' </summary>
    ''' <returns></returns>
    Public Property MstName As String

    ''' <summary>
    ''' 削除方法
    ''' </summary>
    ''' <returns>削除種別</returns>
    ''' <remarks>
    ''' 送信IDの最大値により決定
    '''  0以下：直接削除
    '''  1以上：削除フラグ設定による削除
    ''' </remarks>
    Public Property DeleteType As typDelKind

    ''' <summary>
    ''' 編集項目リスト
    ''' </summary>
    ''' <returns></returns>
    Public Property EditItems As List(Of EditItem)

    ''' <summary>
    ''' 一覧表示項目リスト
    ''' </summary>
    ''' <returns></returns>
    Public Property ListItems As List(Of ListItem)

    ''' <summary>
    ''' ソートキー
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Sortkey As String
      Get
        Dim tmpSortKey As String = String.Empty

        For idx As Integer = 0 To ListItems.Count - 1
          With ListItems(idx)
            If .Index Then
              tmpSortKey &= .Name & " ,"
            End If
          End With
        Next

        ' インデックスが存在しない場合は最初の項目をソートキーにする
        If tmpSortKey.Equals(String.Empty) Then
          tmpSortKey = ListItems(0).Name & ","
        End If

        Return tmpSortKey.Substring(0, InStrRev(tmpSortKey, ",") - 1)
      End Get
    End Property

    ''' <summary>
    ''' 更新フラグ有無フラグ
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property HasKdate As Boolean
      Get
        Return _HasKdate
      End Get
    End Property

#End Region

#End Region

#Region "メソッド"

#Region "パブリック"

    ''' <summary>
    ''' 編集項目追加処理
    ''' </summary>
    ''' <param name="prmSrcData">追加対象パラメータ</param>
    Public Sub AddEditItem(prmSrcData As DataRow)
      Dim tmpSrcData As New EditItem

      With tmpSrcData
        .Name = prmSrcData("Fild_Name").ToString
        .Type = Integer.Parse(prmSrcData("Fild_Type"))
        .Index = ("0" <> prmSrcData("Fild_Key").ToString())
        .Required = (0 < Integer.Parse(prmSrcData("Fild_Check").ToString))
        .MaxChar = Integer.Parse(prmSrcData("Fild_Leng").ToString)
        .Title = prmSrcData("Fild_Caption").ToString
        .Comment = prmSrcData("Fild_Comment").ToString
        .LnkTbl = prmSrcData("LTBL").ToString
        .LnkFld = prmSrcData("LFILD").ToString
        .LnkType = Integer.Parse(prmSrcData("LTYPE").ToString)
        .LinkTbl = prmSrcData("LTBL").ToString
        .LinkFile = prmSrcData("LMDB").ToString
      End With

      Me.EditItems.Add(tmpSrcData)

      _HasKdate = _HasKdate Or (UCase(tmpSrcData.Name) = UCase("KDATE"))
    End Sub

    ''' <summary>
    ''' 一覧表示項目追加処理
    ''' </summary>
    ''' <param name="prmSrcData">追加対象パラメータ</param>
    Public Sub AddListItem(prmSrcData As DataRow)
      Dim tmpSrcData As New ListItem

      With tmpSrcData
        .Name = prmSrcData("Fild_Name").ToString
        .Title = prmSrcData("Fild_Caption").ToString
        .Type = Integer.Parse(prmSrcData("Fild_Type"))
        .Index = ("1" = prmSrcData("Fild_Key").ToString)
        .OrderNo = Integer.Parse(prmSrcData("FildNo").ToString)
        .MaxChar = Integer.Parse(prmSrcData("Fild_Leng").ToString)
        .LinkFile = prmSrcData("LMDB").ToString
        .LinkTbl = prmSrcData("LTBL").ToString
        .LinkFld = prmSrcData("LFILD").ToString
        .LinkDspFld = prmSrcData("LNAME").ToString
        .LinkDspType = Integer.Parse(prmSrcData("LTYPE").ToString)
      End With

      Me.ListItems.Add(tmpSrcData)

      _HasKdate = _HasKdate Or (UCase(tmpSrcData.Name) = UCase("KDATE"))
    End Sub

#End Region

#End Region

  End Class




#End Region

#End Region

#Region "テスト"


#End Region

End Class
