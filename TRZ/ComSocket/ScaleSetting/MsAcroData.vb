Imports System.Text.Encoding

Public Class MsAcroData

#Region "Enum"

  ''' <summary>
  ''' データヘッダーの並びを定義
  ''' </summary>
  Public Enum DataName
    コマンド = 0
    フィラー1
    総バイト数
    処理識別番号
    要求モード
    アンサー電文要求
    明細部バイト数
    機械番号
    シーケンス番号
    エラーコード
    送信レコード件数
    要求レコード件数
    フィラー2
  End Enum

  ''' <summary>
  ''' 計量実績の並びを定義
  ''' </summary>
  ''' <remarks>カンマ区切りの為、サイズ設置は無し</remarks>
  Public Enum WeighingResults
    制御区分 = 0
    シーケンス番号
    通し番号
    機械番号
    実績発生日付
    実績発生時刻
    部位番号
    得意先番号
    指図番号
    加工日
    加工日印字有無
    加工時刻
    賞味期限
    賞味期限印字有無
    賞味時刻
    枝肉番号
    牛豚区分
    左右
    ＳＰ
    入本数
    計量フラグ
    重量
    管理値重量
    単価
    金額
    畜種番号
    格付番号
    カット規格番号
    保存温度番号
    個体識別番号
    ロット番号
    仕入先コード
    商品コード
    自社管理コード
    メーカーコード
    予備コード
    と場番号
    原産地番号
    セット番号
    ケース番号
    と畜番号
    と畜日
    作業区分
    ＪＡＳ区分
    出荷先番号
    風袋重量
    品種番号
    ブランド番号
    生産者番号
    製造元番号
    実績区分
    態様番号
    フリー１番号
    フリー２番号
    フリー３番号
    フリー４番号
    フリー５番号
    担当者番号
    容器包装イメージ番号
    オンラインフラグ
    加算モード
    加減算フラグ
    指図シーケンス番号
  End Enum

  ''' <summary>
  ''' 計量実績の並びを定義
  ''' </summary>
  ''' <remarks>カンマ区切りの為、サイズ設置は無し</remarks>
  Public Enum MstItemRequestStatus
    制御区分 = 0
    部門
    部位番号
    単価
    定重量
    入本数
    個体識別印字種類
    牛豚区分
    上限重量
    下限重量
    保存温度番号
    容器包装イメージ番号
    風袋０
    風袋１
    風袋２
    風袋３
    風袋４
    風袋５
    風袋６
    風袋７
    風袋８
    風袋９
    商品コード１
    商品コード２
    商品コード３
    商品コード４
    商品コード５
    自社管理コード
    加工日印字有無
    加工時刻印字有無
    加工時刻フラグ
    加工時刻
    賞味期限印字有無
    賞味時刻印字有無
    賞味時刻フラグ
    賞味日数
    賞味時刻
    賞味時間
    加工日タイトル番号
    畜種番号
    品種番号
    ブランド番号
    カット規格番号
    格付番号
    原産地番号
    フリー１番号
    フリー２番号
    フリー３番号
    フリー４番号
    フリー５番号
    フォーマット番号１
    フォーマット番号２

  End Enum
#End Region

#Region "メンバ"
  Private _DataHeader As List(Of ColumData)
  Private _WeighingResults() As String
  Private _MstRequestStatus() As String
#End Region

#Region "定数定義"
  Public Const SCALE_HEADER_TITLE As String = "制御区分:2000,シーケンス番号:2001,通し番号:2002,機械番号:2003,実績発生日付:2004,実績発生時刻:2005,部位番号:2006,得意先番号:2007,指図番号:2008,加工日:2009,加工日印字有無:2010,加工時刻:2011,賞味期限:2012,賞味期限印字有無:2013,賞味時刻:2014,枝肉番号:2015,牛/豚区分:2016,左右:2017,SP:2018,入本数:2019,計量フラグ:2020,重量:2021,管理値重量:2022,単価:2023,金額:2024,畜種番号:2025,格付番号:2026,カット規格番号:2027,保存温度番号:2028,個体識別番号:2029,ロット番号:2030,仕入先番号:2031,商品コード:2032,自社管理コード:2033,メーカーコード:2034,予備コード:2035,と場番号:2036,原産地番号:2037,セット番号:2038,ケース番号:2039,と畜番号:2040,と畜日:2041,作業区分:2042,JAS区分:2043,出荷先番号:2044,風袋:2045,品種番号:2047,ブランド番号:2048,生産者番号:2049,製造元番号:2050,実績区分:2051,態様番号:2052,フリー１番号:2053,フリー２番号:2054,フリー３番号:2055,フリー４番号:2056,フリー５番号:2057,担当者番号:2058,容器包装イメージ番号:2059,オンラインフラグ:2060,加算モード:2061,加減算フラグ:2065,指図シーケンス番号:2066"
#End Region

#Region "プロパティー"
#Region "パブリック"


  ''' <summary>
  ''' 固定長部分サイズ取得
  ''' </summary>
  ''' <returns>固定長部分サイズ</returns>
  ''' <remarks>メインヘッダー + サブヘッダー + 明細（除く可変長部）</remarks>
  Public ReadOnly Property HeaderLength As Long
    Get
      Return MainHeaderLength + SubHeaderLength + DetailSectionLength
    End Get
  End Property

  ''' <summary>
  ''' メインヘッダーサイズ取得
  ''' </summary>
  ''' <returns>メインヘッダーサイズ（Byte）</returns>
  Public ReadOnly Property MainHeaderLength As Long
    Get
      Return _DataHeader(DataName.コマンド).DataLength _
        + _DataHeader(DataName.フィラー1).DataLength _
        + _DataHeader(DataName.総バイト数).DataLength
    End Get
  End Property

  ''' <summary>
  ''' サブヘッダーサイズ取得
  ''' </summary>
  ''' <returns>サブヘッダーサイズ（Byte）</returns>
  Public ReadOnly Property SubHeaderLength As Long
    Get
      Return _DataHeader(DataName.処理識別番号).DataLength _
        + _DataHeader(DataName.要求モード).DataLength _
        + _DataHeader(DataName.アンサー電文要求).DataLength _
        + _DataHeader(DataName.明細部バイト数).DataLength
    End Get
  End Property


  ''' <summary>
  ''' 明細サイズ取得
  ''' </summary>
  ''' <returns>明細サイズ（byte）</returns>
  ''' <remarks>データ部（可変長）を除く</remarks>
  Public ReadOnly Property DetailSectionLength As Long
    Get
      Return _DataHeader(DataName.機械番号).DataLength _
        + _DataHeader(DataName.シーケンス番号).DataLength _
        + _DataHeader(DataName.エラーコード).DataLength _
        + _DataHeader(DataName.送信レコード件数).DataLength _
        + _DataHeader(DataName.要求レコード件数).DataLength _
        + _DataHeader(DataName.フィラー2).DataLength
    End Get
  End Property


#End Region
#End Region

#Region "コンストラクタ"

  Public Sub New(Optional prmReceiveData() As Byte = Nothing)

    ReDim _WeighingResults([Enum].GetValues(GetType(WeighingResults)).Length - 1)

    Call InitHeaderData()

    If prmReceiveData IsNot Nothing Then
      Call SetHeader(prmReceiveData)
    End If

  End Sub

#End Region

#Region "メソッド"

#Region "プライベート"

  ''' <summary>
  ''' データヘッダーリスト初期化
  ''' </summary>
  ''' <remarks>
  ''' 各項目のサイズを定義
  ''' ※順序は[Enum]DataNameで設定
  ''' </remarks>
  Private Sub InitHeaderData()

    ' 要素数文初期化
    _DataHeader = New List(Of ColumData)
    For idx = 0 To [Enum].GetValues(GetType(DataName)).Length - 1
      _DataHeader.Add(Nothing)
    Next

    _DataHeader(DataName.コマンド) = New ColumData(1)
    _DataHeader(DataName.フィラー1) = New ColumData(7, " "c)
    _DataHeader(DataName.総バイト数) = New ColumData(8)
    _DataHeader(DataName.処理識別番号) = New ColumData(4)
    _DataHeader(DataName.要求モード) = New ColumData(1)
    _DataHeader(DataName.アンサー電文要求) = New ColumData(1)
    _DataHeader(DataName.明細部バイト数) = New ColumData(8)
    _DataHeader(DataName.機械番号) = New ColumData(4)
    _DataHeader(DataName.シーケンス番号) = New ColumData(4)
    _DataHeader(DataName.エラーコード) = New ColumData(4)
    _DataHeader(DataName.送信レコード件数) = New ColumData(6)
    _DataHeader(DataName.要求レコード件数) = New ColumData(6)
    _DataHeader(DataName.フィラー2) = New ColumData(10, " "c)

  End Sub


#End Region

#Region "パブリック"
  ''' <summary>
  ''' 計量器より受信したデータ(固定長部分)をプロパティーに詰める
  ''' </summary>
  ''' <param name="prmReceiveData">受信データ</param>
  Public Sub SetHeader(prmReceiveData() As Byte)
    Dim tmpReceiveData As String = GetEncoding("UTF-8").GetString(prmReceiveData)
    Dim tmpStartPos As Integer = 0
    Dim tmpDataLength As Integer = 0

    For idx As Integer = 0 To _DataHeader.Count - 1
      tmpStartPos += tmpDataLength
      tmpDataLength = _DataHeader(idx).DataLength
      _DataHeader(idx).Value = tmpReceiveData.Substring(tmpStartPos, tmpDataLength)
    Next

  End Sub

  Public Sub SetHeaderData(prmDataNameIdx As DataName, prmData As String)
    _DataHeader(prmDataNameIdx).Value = prmData
  End Sub

  ''' <summary>
  ''' 計量器より受信したデータ（計量実績）を変数に詰める
  ''' </summary>
  ''' <param name="prmReceiveData">受信データ</param>
  ''' <remarks>
  ''' 最初の改行コード以降のデータを受信データと見做す
  ''' ※複数レコード受信未対応
  ''' </remarks>
  Public Sub SetWeighingResults(prmReceiveData() As Byte)
    Dim tmpWeightResult As String = String.Empty

    tmpWeightResult = GetEncoding("UTF-8").GetString(prmReceiveData)
    tmpWeightResult = tmpWeightResult.Substring(InStr(tmpWeightResult, vbCrLf) + 1)

    _WeighingResults = tmpWeightResult.Split(","c)
  End Sub

  ''' <summary>
  ''' 計量器より受信したデータ（マスタ要求）を変数に詰める
  ''' </summary>
  ''' <param name="prmReceiveData"></param>
  Public Sub SetMstRequestStatus(prmReceiveData() As Byte)
    Dim tmpMstRequestStatus As String = String.Empty

    tmpMstRequestStatus = GetEncoding("UTF-8").GetString(prmReceiveData)
    tmpMstRequestStatus = tmpMstRequestStatus.Substring(InStr(tmpMstRequestStatus, vbCrLf) + 1)

    _MstRequestStatus = tmpMstRequestStatus.Split(","c)
  End Sub

  ''' <summary>
  ''' 計量実績データ個別設定
  ''' </summary>
  ''' <param name="prmWeighingResultsIdx">設定する項目</param>
  ''' <param name="prmData">設定する値</param>
  Public Sub SetWeighingResultsData(prmWeighingResultsIdx As WeighingResults, prmData As String)
    _WeighingResults(prmWeighingResultsIdx) = prmData
  End Sub
  ''' <summary>
  ''' ヘッダーデータ取得
  ''' </summary>
  ''' <param name="prmDataNameIdx"></param>
  ''' <returns></returns>
  Public Function GetHeaderData(prmDataNameIdx As DataName) As String
    Return _DataHeader(prmDataNameIdx).Value
  End Function

  ''' <summary>
  ''' 計量実績データ取得
  ''' </summary>
  ''' <param name="prmWeighingResultsIdx"></param>
  ''' <returns></returns>
  Public Function GetWeighingResults(prmWeighingResultsIdx As WeighingResults) As String
    Return _WeighingResults(prmWeighingResultsIdx)
  End Function

  ''' <summary>
  ''' マスタ受信要求ステータス取得
  ''' </summary>
  ''' <param name="prmMstRequestStatusIdx"></param>
  ''' <returns></returns>
  Public Function GetMstRequestStatus(prmMstRequestStatusIdx As MstItemRequestStatus) As String
    Return _MstRequestStatus(prmMstRequestStatusIdx)
  End Function

  Public Function GetHeaderText() As String
    Dim tmpText As String = String.Empty

    For idx As Integer = 0 To _DataHeader.Count - 1
      tmpText &= _DataHeader(idx).Value
    Next

    Return tmpText
  End Function

  Public Function GetWeighingResultsText() As String
    'Dim tmpFormatText As String = "制御区分:2000,シーケンス番号:2001,通し番号:2002,機械番号:2003,実績発生日付:2004,実績発生時刻:2005,部位番号:2006,得意先番号:2007,指図番号:2008,加工日:2009,加工日印字有無:2010,加工時刻:2011,賞味期限:2012,賞味期限印字有無:2013,賞味時刻:2014,枝肉番号:2015,牛/豚区分:2016,左右:2017,SP:2018,入本数:2019,計量フラグ:2020,重量:2021,管理値重量:2022,単価:2023,金額:2024,畜種番号:2025,格付番号:2026,カット規格番号:2027,保存温度番号:2028,個体識別番号:2029,ロット番号:2030,仕入先番号:2031,商品コード:2032,自社管理コード:2033,メーカーコード:2034,予備コード:2035,と場番号:2036,原産地番号:2037,セット番号:2038,ケース番号:2039,と畜番号:2040,と畜日:2041,作業区分:2042,JAS区分:2043,出荷先番号:2044,風袋:2045,品種番号:2047,ブランド番号:2048,生産者番号:2049,製造元番号:2050,実績区分:2051,態様番号:2052,フリー１番号:2053,フリー２番号:2054,フリー３番号:2055,フリー４番号:2056,フリー５番号:2057,担当者番号:2058,容器包装イメージ番号:2059,オンラインフラグ:2060,加算モード:2061,加減算フラグ:2065,指図シーケンス番号:2066"

    Return SCALE_HEADER_TITLE & vbCrLf & Join(_WeighingResults, ",")
  End Function
#End Region


#End Region

#Region "内部クラス"

  ''' <summary>
  ''' データ項目1つを定義
  ''' </summary>
  Public Class ColumData

#Region "メンバ"

#Region "プライベート"
    ''' <summary>
    ''' データ長
    ''' </summary>
    Dim _tmpDataLength As Long = 0

    ''' <summary>
    ''' データ値
    ''' </summary>
    Dim _tmpValue As String = String.Empty

    ''' <summary>
    ''' 不足分を埋める文字
    ''' </summary>
    Dim _tmpPaddingChar As Char
#End Region

#End Region

#Region "プロパティー"

#Region "パブリック"

    ''' <summary>
    ''' データ値
    ''' </summary>
    ''' <returns></returns>
    Public Property Value As String
      Get
        Return _tmpValue
      End Get
      Set(value As String)
        _tmpValue = value.PadLeft(_tmpDataLength, _tmpPaddingChar)
      End Set
    End Property

    ''' <summary>
    ''' データ長
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property DataLength As Long
      Get
        Return _tmpDataLength
      End Get
    End Property

#End Region
#End Region

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="prmDataLength">データ長</param>
    Public Sub New(prmDataLength As Long, Optional prmPaddingChar As Char = "0"c)
      _tmpDataLength = prmDataLength
      _tmpPaddingChar = prmPaddingChar
    End Sub

#End Region

  End Class

#End Region

End Class
