Imports DevExpress.XtraSplashScreen
Imports System.IO
Imports DotSpatial.Data
Public Class Frmtinhtoandachitieu
    Dim DatenfileKQ As String = "Đặt tên file kết quả"
    Dim Filetontai As String = "File đã tồn tại, bạn có ghi đè không?"
    Dim Thongbao As String = "Thông báo"
    Dim Khongtheghide As String = "Không thể ghi đè file, có thể file đang mở." + vbNewLine + "Xin đổi tên file đích"
    Dim Khongquyensudung As String = "Không có quyền sử dụng "
    Dim LoiQuyentruycap As String = "Lỗi quyền truy cập"
    Dim LoiThuchiencong As String = "Lỗi thực hiện cộng/nhân các Raster chuẩn hóa " + vbNewLine + "Có thể xảy ra bởi tình trạng thiếu bộ nhớ"
    Dim Bancomuonxem As String = "Bạn có muốn xem bản đồ kết quả (chưa phân khoảng)?"
    Dim Bancomuonphankhoang As String = "Bạn có muốn phân khoảng cho bản đồ kết quả?"
    Dim chaythanhcong As String = "Chạy thành công bản đồ chuẩn hóa"
    Sub New()
        InitializeComponent()
        If Frm_Startup.myLanguage = "English" Then
            EnglishTranslate()
        End If
    End Sub

    Public Overrides Sub ProcessCommand(ByVal cmd As System.Enum, ByVal arg As Object)
        MyBase.ProcessCommand(cmd, arg)
    End Sub

    Public Enum SplashScreenCommand
        SomeCommandId
    End Enum

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Try
        '    SplashScreenManager.CloseForm()
        'Catch

        'End Try
        Dim saveFileDialg As New SaveFileDialog()

        saveFileDialg.Title = DatenfileKQ
        'openFileDialog.InitialDirectory = Application.StartupPath + "\Images"
        saveFileDialg.Filter = "Tiff file|*.TIF|IMG file|*.IMG"
        'AppManager1.
        saveFileDialg.FilterIndex = 1
        saveFileDialg.RestoreDirectory = True
        saveFileDialg.OverwritePrompt = False
        Dim resultname As String
        LabelControl1.Visible = True
        ProgressBarControl1.Visible = True
        If saveFileDialg.ShowDialog() = DialogResult.OK Then
            resultname = saveFileDialg.FileName
            If File.Exists(resultname) Then
                Dim result As DialogResult = MessageBox.Show(Filetontai, Thongbao, MessageBoxButtons.YesNo)
                If result = Windows.Forms.DialogResult.No Then
                    Return
                Else
                    Try
                        File.Delete(resultname)
                    Catch ex As Exception
                        MessageBox.Show(Khongtheghide, Thongbao)
                        Return
                    End Try
                End If

            End If
        Else
            LabelControl1.Visible = False
            ProgressBarControl1.Visible = False
            Return
        End If

        'Dim dt As DataTable = GridControl1.DataSource
        Dim thePath As String
        Dim theFN As String
        Dim tmppath As String = Path.GetTempPath()
        xoatmp(tmppath)
        Try
            If (Not System.IO.Directory.Exists(tmppath + "nft")) Then
                System.IO.Directory.CreateDirectory(tmppath + "nft")
            End If
            tmppath = tmppath + "nft\"
        Catch ex As Exception
            MessageBox.Show(Khongquyensudung + tmppath, LoiQuyentruycap, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        Dim theWeight As Single
        'Dim tmpRaster As IRaster = New raster
        'Dim states As List(Of String) = New List(Of String) (New String() {"AL", "AZ", "CA", "CO", "NV", "OK"})

        'Dim inpRaster2 As IRaster = New raster
        Dim SrcRaster As IRaster = New raster
        Dim WeightedRaster As Raster
        Dim outRaster As IRaster = New Raster
        Dim tmpCongFN As String
        Dim tmpWeightedFN As String
        Dim nameFilecong As String
        Dim nameFilenhan As String
        'output.Filename = "Z:\out1.tif"


        Dim listRaster As List(Of IRaster) = New List(Of IRaster)
        If GridView1.RowCount > 0 Then
            Dim i As Integer
            Try
                ProgressBarControl1.Properties.Step = 1
                ProgressBarControl1.Properties.PercentView = True
                ProgressBarControl1.Properties.Maximum = FrmMain.GridView1.RowCount        'Lấy  count của toàn bộ các ROWs
                ProgressBarControl1.Properties.Minimum = 0

                '=====Tiến hành cộng Raster

                For i = 0 To GridView1.RowCount - 1
                    'SplashScreenManager.ShowForm(GetType(FrmWaiting))
                    theWeight = Convert.ToSingle(GridView1.GetRowCellValue(i, "Weight"))
                    thePath = GridView1.GetRowCellValue(i, "Mapchuanhoa")       'Filename Included
                    theFN = Path.GetFileName(thePath)       'extension Included
                    SrcRaster = Raster.Open(thePath)
                    tmpCongFN = tmppath + "S_" + i.ToString + theFN
                    tmpWeightedFN = tmppath + "W_" + i.ToString + theFN
                    nameFilecong = tmpCongFN
                    GC.Collect()
                    WeightedRaster = WeightRaster(tmpWeightedFN, SrcRaster, theWeight)
                    RasterDisp(SrcRaster, "")       'Dùng "" để không xóa file nguồn (chỉ close and dispose
                    If i = 0 Then   'Chạy vòng lặp lần đầu
                        listRaster.Add(WeightedRaster)  'ListRaster (0)     ' Raster.Open(tmppath + "in1_" + i.ToString + theFN)
                    Else
                        Dim mymagic As New myRasterMagic

                        listRaster.Add(mymagic.RasterMath("Cộng", listRaster(i - 1), WeightedRaster, tmpCongFN, Nothing))

                        RasterDisp(listRaster(i - 1), listRaster(i - 1).Filename)
                        RasterDisp(WeightedRaster, WeightedRaster.Filename)
                        ' listRaster.RemoveAt(i - 1)
                    End If


                    GC.Collect()

                    'File.Delete(tmppath + "S_" + (i - 1).ToString + Path.GetFileName(GridView1.GetRowCellValue((i - 1), "Mapchuanhoa")))
                    'tmpCongFN = tmppath + "S_" + i.ToString + theFN


                    ProgressBarControl1.PerformStep()
                    ProgressBarControl1.Update()
                    'Try
                    '    SplashScreenManager.CloseForm()
                    'Catch

                    'End Try
                    ' FrmRunning.ShowDialog()
                Next
                '    listRaster(GridView1.RowCount - 1).Filename = "z:\a.tif"
                'listRaster(GridView1.RowCount - 1).SaveAs(resultname)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                MessageBox.Show(LoiThuchiencong, Thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
                Me.Close()
            End Try
        End If

        Dim countKhongGioiHan = listRaster.Count
        Dim tmpNhanFN As String = ""
        Try 'tIẾN HÀNH NHÂN RASTER
            GridView1.ClearColumnsFilter()
            GridView1.Columns("Limit").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[Limit] = 'Giới hạn' or [Limit] = 'Limit'")
            If GridView1.RowCount > 0 Then 'Thực hiện nhân các LIMIT
                For j As Integer = 0 To GridView1.RowCount - 1
                    thePath = GridView1.GetRowCellValue(j, "Mapchuanhoa")       'Filename Included
                    theFN = Path.GetFileName(thePath)       'extension Included
                    SrcRaster = Raster.Open(thePath)
                    tmpNhanFN = tmppath + "M_" + j.ToString + theFN
                    If j = GridView1.RowCount - 1 Then
                        tmpNhanFN = resultname
                    End If
                    ' nameFilenhan = tmpNhanFN
                    GC.Collect()

                    Dim mymagic As New myRasterMagic
                    If listRaster.Count = 0 Then        'Tức là Không có kết quả cộng nào của vòng lặp KHÔNG GIỚI HẠN=> Add SrcRaster đầu tiên chứ chẳng nhân với cái gì cả
                        listRaster.Add(SrcRaster)
                    Else
                        listRaster.Add(mymagic.RasterMath("Nhân", listRaster(listRaster.Count - 1), SrcRaster, tmpNhanFN, Nothing))       'listRaster(listRaster.Count-1): Là Raster cuối cùng của ListRas
                        RasterDisp(listRaster(listRaster.Count - 2), listRaster(listRaster.Count - 2).Filename) 'Lúc này phải là listRaster.Count - 2 (không phải là trừ 1 nữa) vì vừa add thêm 1 item vào list.
                        RasterDisp(SrcRaster, "")
                    End If



                    If j < GridView1.RowCount - 1 Then      'Để không dispose Raster kết quả cuối cùng
                        'RasterDisp(listRaster(listRaster.Count - 1), listRaster(listRaster.Count - 1).Filename)
                    End If



                    ProgressBarControl1.PerformStep()
                    ProgressBarControl1.Update()
                    ' FrmRunning.ShowDialog()

                Next

            End If


            listRaster(listRaster.Count - 1).Save()
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            MessageBox.Show(LoiThuchiencong, Thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
            Me.Close()

        End Try

        Me.Close()

        '===Chạy Reclass 
        Dim ok As DialogResult = MessageBox.Show(bancomuonphankhoang, Thongbao, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If ok = Windows.Forms.DialogResult.Yes Then
            Frm_Reclass.Txtinput.Text = resultname
            'Dim minval As Double = 0
            'Dim maxval As Double = 0
            Dim inputRaster As IRaster
            Try
                Frm_Reclass.inputRaster = Raster.Open(resultname)
                Frm_Reclass.inputRaster.GetStatistics()
                Frm_Reclass.minval = inputRaster.Minimum
                Frm_Reclass.maxval = inputRaster.Maximum
                'Txt_cellsize.Text = inputRaster.CellHeight
                Frm_Reclass.ComboBoxEdit1.SelectedIndex = 1
                Frm_Reclass.ComboBoxEdit1.SelectedIndex = 3
                Frm_Reclass.ComboBoxEdit1.Enabled = True
            Catch ex As Exception
                MessageBox.Show("Lỗi file")
                'Txtinput.Text = oldfile
                'Frm_Reclass.ComboBoxEdit1.Enabled = False
            End Try
            Frm_Reclass.Txtoutput.Text = IO.Path.GetFullPath(resultname).Replace(".TIF", "") + "_rec.tif"


            'Frm_Reclass.Txtoutput.Text = resultname
            Frm_Reclass.ShowDialog()
            'Viewer(Txtoutput.Text)
        Else
            Dim viewer As DialogResult = MessageBox.Show(Bancomuonxem, Thongbao, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If viewer = Windows.Forms.DialogResult.Yes Then
                Try
                    SplashScreenManager.ShowForm(GetType(FrmWaiting))
                    ' listRaster(listRaster.Count - 1).Save()
                    frmMapviewer.Map1.Layers.Add(listRaster(listRaster.Count - 1))
                    FrmMain.Bar3.Text = chaythanhcong
                    ' frmMapviewer.Map1.Layers.Add(resultRaster)
                    SplashScreenManager.CloseForm()
                    frmMapviewer.Show()
                    'frmMapviewer.BringToFront()
                    'Application.OpenForms("frmMapviewer").BringToFront()
                    frmMapviewer.Activate()
                    frmMapviewer.TopLevel = True

                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Try
                        SplashScreenManager.CloseForm()
                    Catch
                    End Try
                End Try
            End If
        End If
        'Mở bản đồ kết quả





        ' '' ''Try 'tIẾN HÀNH NHÂN RASTER


        ' '' ''    GridView1.ClearColumnsFilter()
        ' '' ''    GridView1.Columns("Limit").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[Limit] = 'Giới hạn'")
        ' '' ''    If GridView1.RowCount > 0 Then
        ' '' ''        'Dim stopwatch As Stopwatch = stopwatch.StartNew
        ' '' ''        'Thread.Sleep(5000)  ' Nghỉ 1 tý để có thể thực hiện Label1.Text = "Chương trình đang thực hiện nhân các Raster - Limit"
        ' '' ''        'stopwatch.Stop()
        ' '' ''        LabelControl1.Text = "Chương trình đang thực hiện nhân các Raster  " + """Giới hạn"""
        ' '' ''        'Label3.Visible = True
        ' '' ''        'Thread.Sleep(5000)
        ' '' ''        Frmtmp_forProgress.ShowDialog()

        ' '' ''        'Thực hiện nhân các LIMIT
        ' '' ''        ProgressBarControl1.EditValue = 0
        ' '' ''        ProgressBarControl1.Properties.Step = 1
        ' '' ''        ProgressBarControl1.Properties.PercentView = True
        ' '' ''        ProgressBarControl1.Properties.Maximum = GridView1.RowCount
        ' '' ''        ProgressBarControl1.Properties.Minimum = 0



        ' '' ''        For i As Integer = 0 To GridView1.RowCount - 1
        ' '' ''            'SplashScreenManager.ShowForm(GetType(FrmWaiting))
        ' '' ''            thePath = GridView1.GetRowCellValue(i, "Mapchuanhoa")
        ' '' ''            theFN = Path.GetFileName(thePath)
        ' '' ''            ' MessageBox.Show("sd")
        ' '' ''            If i = 0 Then
        ' '' ''                'tmpRaster.Filename = theFN
        ' '' ''                tmpRaster = Raster.Open(thePath)
        ' '' ''                'tmpRaster.SaveAs(tmppath + "sum_" + i.ToString + theFN)
        ' '' ''                tmpRaster.SaveAs(tmppath + "in1nhan_" + i.ToString + theFN)
        ' '' ''                tmpRaster.Close()
        ' '' ''                tmpRaster.Dispose()
        ' '' ''                GC.Collect()
        ' '' ''                inpRaster1 = Raster.Open(tmppath + "in1nhan_" + i.ToString + theFN)
        ' '' ''                'inpRaster1.Close()
        ' '' ''                'inpRaster1.Dispose()
        ' '' ''                nameFilenhan = tmppath + "in1nhan_" + i.ToString + theFN
        ' '' ''            Else

        ' '' ''                SrcRaster = Raster.Open(thePath)
        ' '' ''                outRaster.Filename = tmppath + "nhan_" + i.ToString + theFN

        ' '' ''                outRaster = FrmStart.NhanRaster(inpRaster1, SrcRaster, outRaster.Filename, Nothing)

        ' '' ''                'outRaster.SaveAs(tmppath + "sum_" + i.ToString + theFN)
        ' '' ''                outRaster.SaveAs(tmppath + "in1nhan_" + i.ToString + theFN)
        ' '' ''                'Chỉ cần save ra file vật lý để mở thành inpraster trong bước lặp sau, ko dùng nữa nên có thể close và dispose
        ' '' ''                outRaster.Close()
        ' '' ''                outRaster.Dispose()

        ' '' ''                Try 'Xóa 2 file đầu vào
        ' '' ''                    inpRaster1.Close()
        ' '' ''                    System.IO.File.Delete(inpRaster1.Filename)
        ' '' ''                    inpRaster1.Dispose()
        ' '' ''                Catch ex As Exception

        ' '' ''                End Try

        ' '' ''                Try 'Xóa 2 file đầu vào
        ' '' ''                    SrcRaster.Close()
        ' '' ''                    SrcRaster.Dispose()
        ' '' ''                    'System.IO.File.Delete(inpRaster2.Filename)  'Không xóa, đây là file chuẩn hóa đầu vào, ko phải file tạm
        ' '' ''                Catch ex As Exception

        ' '' ''                End Try
        ' '' ''                GC.Collect()
        ' '' ''                ''tmpRaster.Close()
        ' '' ''                ''tmpRaster = Raster.Open(tmppath + "sum_" + i.ToString + theFN)
        ' '' ''                ''tmpRaster.Close()
        ' '' ''                If i < GridView1.RowCount - 1 Then  'Lần cuối cùng thì ko cần mở.
        ' '' ''                    inpRaster1 = Raster.Open(tmppath + "in1nhan_" + i.ToString + theFN)    'Chính là OutRaster vừa tạo; Dùng để làm in1nhan cho bước lặp tiếp theo
        ' '' ''                    'inpRaster1.Close()
        ' '' ''                    'inpRaster1.Dispose()
        ' '' ''                End If

        ' '' ''                ''tmppath + "in1_" + i.ToString + theFN is the result

        ' '' ''                '' If i = GridView1.RowCount - 1 Then
        ' '' ''                nameFilenhan = tmppath + "in1nhan_" + i.ToString + theFN
        ' '' ''                'End If
        ' '' ''            End If

        ' '' ''            ProgressBarControl1.PerformStep()
        ' '' ''            ProgressBarControl1.Update()
        ' '' ''            'Try
        ' '' ''            '    SplashScreenManager.CloseForm()
        ' '' ''            'Catch

        ' '' ''            'End Try
        ' '' ''        Next
        ' '' ''    End If

        ' '' ''Catch ex As Exception
        ' '' ''    MessageBox.Show(ex.Message)
        ' '' ''    MessageBox.Show("Lỗi thực hiện nhân các Raster chuẩn hóa " + """Giới hạn""" + vbNewLine + "Có thể xảy ra bởi tình trạng thiếu bộ nhớ", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ' '' ''    Return
        ' '' ''    Me.Close()
        ' '' ''End Try


        ' '' ''Dim resultraster As IRaster = New Raster
        ' '' ''Try
        ' '' ''    '============Nhân kết quả cộng và kết quả nhân

        ' '' ''    resultraster.Filename = resultname
        ' '' ''    If nameFilecong = "" Then
        ' '' ''        Dim ketquanhan As IRaster = Raster.Open(nameFilenhan)
        ' '' ''        ketquanhan.SaveAs(resultname)
        ' '' ''        ketquanhan.Close()
        ' '' ''        ketquanhan.Dispose()
        ' '' ''    ElseIf nameFilenhan = "" Then
        ' '' ''        Dim ketquacong As IRaster = Raster.Open(nameFilecong)
        ' '' ''        ketquacong.SaveAs(resultname)
        ' '' ''        ketquacong.Close()
        ' '' ''        ketquacong.Dispose()
        ' '' ''    Else
        ' '' ''        Dim ketquacong As IRaster = Raster.Open(nameFilecong)
        ' '' ''        Dim ketquanhan As IRaster = Raster.Open(nameFilenhan)

        ' '' ''        FrmStart.NhanRaster(ketquacong, ketquanhan, resultname, Nothing)
        ' '' ''        ketquacong.Close()
        ' '' ''        ketquacong.Dispose()
        ' '' ''        ketquanhan.Close()
        ' '' ''        ketquanhan.Dispose()
        ' '' ''    End If
        ' '' ''    GC.Collect()
        ' '' ''    '============Xóa các file tạm
        ' '' ''    Dim s As String
        ' '' ''    For Each s In System.IO.Directory.GetFiles(tmppath)
        ' '' ''        Try
        ' '' ''            System.IO.File.Delete(s)
        ' '' ''        Catch ex As Exception

        ' '' ''        End Try

        ' '' ''    Next s
        ' '' ''    FrmStart.Status1.Caption = "Đã hoàn thành đánh giá thích nghi"
        ' '' ''Catch ex As Exception
        ' '' ''    MessageBox.Show("Lỗi thực hiện lưu kết quả đánh giá thích nghi" + """Giới hạn""" + vbNewLine + "Có thể xảy ra bởi tình trạng thiếu bộ nhớ", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ' '' ''End Try

        '' '' ''Try
        '' '' ''    SplashScreenManager.ShowForm(GetType(FrmWaiting))
        '' '' ''    Dim KQRaster = Raster.Open(resultname)
        '' '' ''    frmMapviewer.Map1.Layers.Add(KQRaster)
        '' '' ''    ' frmMapviewer.Map1.Layers.Add(resultRaster)
        '' '' ''    frmMapviewer.Show()
        '' '' ''    'frmMapviewer.BringToFront()
        '' '' ''    'Application.OpenForms("frmMapviewer").BringToFront()
        '' '' ''    frmMapviewer.Activate()
        '' '' ''    frmMapviewer.TopLevel = True
        '' '' ''    SplashScreenManager.CloseForm()
        '' '' ''Catch ex As Exception
        '' '' ''    MessageBox.Show(ex.Message)
        '' '' ''    Try
        '' '' ''        SplashScreenManager.CloseForm()
        '' '' ''    Catch

        '' '' ''    End Try

        '' '' ''End Try


    End Sub

    Private Sub Me_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
        ' Dim a = Me.IsDisposed
        FrmMain.XtraTabControl1.SelectedTabPageIndex = 3
        GC.Collect()

        '============Xóa các file tạm
        Dim tmppath As String = Path.GetTempPath() + "nft\"
        xoatmp(tmppath)

        GC.Collect()

    End Sub

    Private Sub me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GridControl1.DataSource = FrmMain.GridControl1.DataSource
        GridView1.Columns("Limit").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[Limit] = 'Không giới hạn' [Limit] = 'Non-Limit'")
        FrmMain.formatdgv1(GridView1)
        '============Xóa các file tạm
        Dim tmppath As String = Path.GetTempPath() + "nft\"
        xoatmp(tmppath)
        GC.Collect()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Function WeightRaster(ByVal outputFN As String, ByVal inpRaster As IRaster, ByVal weight As Single) As IRaster

        Dim rasteroptions As String()
        Dim datatype As System.Type = System.Type.GetType("System.Single")
        WeightRaster = Raster.CreateRaster(outputFN, Nothing, inpRaster.NumColumns, inpRaster.NumRows, 1, inpRaster.DataType, rasteroptions)
        'Bounds specify the cellsize and the coordinates of raster corner
        With WeightRaster
            .Filename = outputFN
            .Bounds = inpRaster.Bounds
			.NoDataValue = -9999
            .Projection = inpRaster.Projection

            '    '.NumRowsInFile = IndRaster.NumRows
            '    '.NumColumns = IndRaster.NumColumns

        End With
        For i As Integer = 0 To inpRaster.NumRows - 1
			For j As Integer = 0 To inpRaster.NumColumns - 1
				If inpRaster.Value(i, j) <> inpRaster.NoDataValue And inpRaster.Value(i, j) > -9999 Then
					WeightRaster.Value(i, j) = inpRaster.Value(i, j) * weight
				Else
					WeightRaster.Value(i, j) = -9999
				End If

			Next
        Next

        'If Not outputFN.Contains("abc123xyz.tif") Then
        '    WeightRaster.Save()
        'End If

    End Function

    Sub xoatmp(tmppath)
        Try
            If (Not System.IO.Directory.Exists(tmppath)) Then
                System.IO.Directory.CreateDirectory(tmppath)
            End If
            'tmppath = tmppath + "nft\"
        Catch ex As Exception
            MessageBox.Show(Khongquyensudung + tmppath, LoiQuyentruycap, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try
    End Sub
    Sub RasterDisp(ByVal aRaster As IRaster, ByVal Rasterfilename As String)
        Try
            aRaster.Close()
            GC.Collect()
        Catch ex As Exception

        End Try
        Try
            aRaster.Dispose()
            GC.Collect()
        Catch ex As Exception

        End Try

        If Rasterfilename <> "" Then
            Try
                System.IO.File.Delete(Rasterfilename)
            Catch ex As Exception

            End Try
        End If

    End Sub
    Sub EnglishTranslate()
        DatenfileKQ = "Input a file name"
        Filetontai = "File is existing, do you want to replace?"
        Thongbao = "Information"
        Khongtheghide = "Can not replace, the file could be opening" + vbNewLine + "Please change the ouput file"
        Khongquyensudung = "Can not access"
        LoiQuyentruycap = "Error in file accessing"
        LoiThuchiencong = "Error is happened by lacking of memory"

        Bancomuonxem = "Do you want to view the result (have not been reclassed yet)?"
        Bancomuonphankhoang = "Do you want to reclass for the result data?"
        chaythanhcong = "Evaluating land suitability successful"
    End Sub
End Class
