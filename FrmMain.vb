Imports System.ComponentModel.Composition
Imports System.Linq
Imports System.Data.OleDb
Imports System.Text
Imports System.IO
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Menu
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraGrid.Columns
'Imports DevExpress.XtraCharts
Imports DevExpress.Charts
Imports DevExpress.Skins
Imports DevExpress.LookAndFeel
Imports DevExpress.UserSkins
Imports DevExpress.XtraBars.Helpers
Imports DevExpress.Data.Filtering
Imports DevExpress.XtraGrid.Localization
Imports DotSpatial.Controls
Imports DotSpatial.Symbology
Imports DotSpatial.Controls.Header
Imports DotSpatial.Data
Imports DotSpatial.Topology
Imports DotSpatial.Analysis
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Threading
Imports DevExpress.XtraBars
Imports DevExpress.XtraBars.Ribbon
Imports DevExpress.XtraBars.Ribbon.ViewInfo
Imports System.Diagnostics
Imports System.Reflection
Imports DevExpress.XtraSplashScreen

Public Class FrmMain
    Dim Frm_Sentitive_analysisISopened As Boolean = 0
	Dim Gridview1_Connect As myADOclass	'Chỉ định nghĩa, ko gán giá trị
    Dim Gridview2_Connect As myADOclass 'Chỉ định nghĩa, ko gán giá trị
    Dim Gridcontrol1_dt As DataTable
    'Dim "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """" As String '= "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """"      'Sử dụng để sau này update Datatable không bị lỗi
    Dim chayListboxControl1_selectedIndexChanged As Boolean = False

    Dim XtraTabPage2selected As Boolean 'Xác định Xtratabpage2 có đang selected (active) hay không. Nếu = true=> đang active (focus)
    Dim capnhatlaiTab2_3 As Boolean = True   'Nếu =False thì sẽ ko cần tạo lại các textbox cho 2 tab này
    Public taocaymoivalue As String
    '==========English translator
    Public mucdoquantrong As String = "Mức ảnh hưởng"
    Dim Taocaymoi As String = "Tạo cây mới"
    Dim caymoi As String = "Nhập tên cây đánh giá cần tạo"
    Dim tencaytontai As String = "Tên cây bạn vừa nhập đã tồn tại!"
    Public loi As String = "Lỗi"
    Public thongbao As String = "Thông báo"
    Dim Copycay As String = "Copy dữ liệu cây "
    Dim Copycay1 As String = " thành cây "
    Dim Copycay2 As String = "Copy dữ liệu đánh giá cây trồng"
    Dim Doitencay As String = "Đổi tên cây "
    Dim Doitencay1 As String = " thành cây "
    Dim Doitencay2 As String = "Đổi tên cây trồng"
    Dim Xoacay As String = "Bạn chắc chắn xóa dữ liệu cây này?"
    Dim Tenchitieu As String = "Hãy Nhập tên chỉ tiêu"
    Dim chitieutontai As String = "Chỉ tiêu đã tồn tại"
    Dim aHamhinhthang As String = "Hàm Hình thang"
    Dim giatribc As String = "Nhập giá trị B và C (B < C)!"
    Dim trogiuphinhthang As String = "Xem trợ giúp - phần các hàm chuẩn hóa (hàm hình thang) để hiểu rõ hơn về các thông số A, B, C và D."
    Dim txtaInvalidValueHinhthang As String = "Bạn cần nhập giá trị < B < C < D" + vbNewLine + trogiuphinhthang
    Dim txtbInvalidValueHinhthang As String = "Bạn cần nhập giá trị < C < D và > A" + vbNewLine + trogiuphinhthang
    Dim txtcInvalidValueHinhthang As String = "Bạn cần nhập giá trị < D và > B > A" + vbNewLine + trogiuphinhthang
    Dim txtdInvalidValueHinhthang As String = "Bạn cần nhập giá trị > C > B > A" + vbNewLine + trogiuphinhthang
    Dim aHamkandel As String = "Hàm Kandel"
    Dim giatriab As String = "Nhập giá trị A và B (A < B)!"
    Dim trogiupkandel As String = "Xem trợ giúp - phần các hàm chuẩn hóa (hàm Kandel) để hiểu rõ hơn về các thông số A và B."
    Dim txtaInvalidValueKandel As String = "Bạn cần nhập giá trị < B" + vbNewLine + trogiupkandel
    Dim txtbInvalidValueKandel As String = "Bạn cần nhập giá trị > A" + vbNewLine + trogiupkandel
    Dim aHams1 As String = "Hàm S1"
    Dim aHams2 As String = "Hàm S2"
    Dim trogiupS As String = "Xem trợ giúp - phần các hàm chuẩn hóa (hàm S) để hiểu rõ hơn về các thông số A và B."
    Dim txtaInvalidValueHamS As String = "Bạn cần nhập giá trị < B" + vbNewLine + trogiupS
    Dim txtbInvalidValueHamS As String = "Bạn cần nhập giá trị > A" + vbNewLine + trogiupS
    Dim aHamtheoloai As String = "Hàm theo loại"
    Dim ahamKhongham As String = "Không chạy chuẩn hóa"
    Dim Chacchanxoa As String = "Bạn chắc chắn xóa dữ liệu?"
    Dim Xoathanhcong As String = "Đã xóa dữ liệu thành công!"
    Dim Chacchanluu As String = "Lưu dữ liệu?"
    Dim Luuthanhcong As String = "Đã lưu dữ liệu thành công!"
    Dim Kiemtrachitieu As String = "Kiểm tra lại các bản đồ chỉ tiêu!"
    Dim Kiemtrachuanhoa As String = "Kiểm tra lại các bản đồ chuẩn hóa!"
    Dim Bandoloi As String = "Bản đồ có lỗi!"
    Dim Bandochuanhoaloi As String = "Bản đồ chuẩn hóa có lỗi!"
    Public filetontai As String = "File đã tồn tại, bạn có ghi đè không?"
    Dim Khongkichhoatbuoc4 As String = "Không thể kích hoạt, thực hiện kiểm tra bản đồ chuẩn hóa tại bước 2!"
    Dim Khongkichhoatbuoc3 As String = "Không thể kích hoạt, thực hiện kiểm tra bản đồ chỉ tiêu tại bước 1!"
    Dim Buoc4 As String = "Bước 4"
    Dim Buoc5 As String = "Bước 5"
    Dim Khongtheghide As String = "Không thể ghi đè file, có thể file đang mở."
    Dim Filechuanhoakhac As String = "Hãy dùng tên file chuẩn hóa khác."
    Dim nhapfiletif As String = "Nhập tên file chuẩn hóa (file *.Tif)."
    Public Khongmofile As String = "Không mở được file "
    Public Loibando As String = "Lỗi file bản đồ"
    Dim haychonproject As String = "Bạn phải chọn Project để tiếp tục chương trình!"
    Dim OpenProject As String = "Chọn Project"
    Public filedichdangmo As String = "Có thể File đích đang mở, hãy dùng tên file khác!"
    Public khongthexuat As String = "Không thể xuất dữ liệu"
    Public Tinhtoanthanhcong As String = "Xây dựng thành công bản đồ chuẩn hóa"
    Public Nhomchitieu As String = "Nhóm chỉ tiêu"
    Public Chitieu As String = "Chỉ tiêu"
    Dim Hamsudung As String = "Hàm sử dụng"
    Dim Bandochitieu As String = "Dữ liệu chỉ tiêu"
    Public Bandochuanhoa As String = "Dữ liệu chuẩn hóa"
    Public trongso As String = "Trọng số"
    Dim trongso1 As String = "Tổng trọng số phải = 1! Mời bạn quay lại Tab Xác định Trọng số để sửa lỗi này"
    Dim Gioihan As String = "Loại chỉ tiêu"
    Dim Themchitieu As String = "Thêm chỉ tiêu"
    Dim Sapxeptangdan As String = "Sắp xếp tăng dần"
    Dim Sapxepgiamdan As String = "Sắp xếp giảm dần"
    Dim Bosapxep As String = "Bỏ sắp xếp"
    Dim Datvuadorongcot As String = "Đặt vừa độ rộng cột"
    Dim Datvuadorongmoicot As String = "Đặt vừa độ rộng mọi cột"
    Dim Suaduongdan As String = "Sửa đường dẫn dữ liệu"
    Dim Xaydungbandochuanhoa As String = "Xây dựng bản đồ chuẩn hóa"
    Dim Xemthubando As String = "Xem thử bản đồ chuẩn hóa"
    Dim projectDGTN As String = "Project Đánh giá thích nghi cây trồng|*.nft"
    Dim CTDGTN As String = "Chương trình đánh giá thích nghi đất đai -"
    Dim Xem As String = "Xem bản đồ chỉ tiêu"
    Dim SavePrj As String = "Sao lưu project"
    Dim savetoExcelTitle As String = "Xuất dữ liệu chỉ tiêu sang dạng Excel"
    Dim SavedPrj As String = "Project đã được lưu"
    Dim Chonraster As String = "Chọn file bản đồ raster"
    Dim filedulieutontai As String = "File dữ liệu đã tồn tại"
    Dim filedulieuKhongtontai As String = "File dữ liệu không tồn tại"
    Dim NhapgiatriPhankhoang As String = "Nhập giá trị phân khoảng"
    Dim Boloctontai As String = "Bộ lọc đã tồn tại"
    Dim DulieuTNST As String = "Dữ liệu thích nghi thành phần"
    Dim DulieuTNSTReclass As String = "Dữ liệu thích nghi thành phần đã phân lớp"
    Dim Tenboloc As String = "Tên bộ lọc"
    Dim Dulieuboloctho As String = "Dữ liệu thô của bộ lọc"
    Dim Dulieubolocreclass As String = "Dữ liệu bộ lọc đã phân khoảng"
    Public Giatriphankhoang As String = "Giá trị phân khoảng"
    Dim NameF As String = "Loại dữ liệu"
    Dim SrcTifF As String = "Đường dẫn file đầu vào"
    Dim RecTifF As String = "Đường dẫn file đã phân khoảng"
    Dim dulieuTNSTphanlop As String = "Nhập dữ liệu đánh giá thích nghi Sinh thái đã phân lớp"
    Dim dulieuKTXHphanlop As String = "Nhập dữ liệu đánh giá thích nghi Kinh tế Xã hội đã phân lớp"
    Dim dulieuTNMTphanlop As String = "Nhập dữ liệu đánh giá thích nghi Môi trường đã phân lớp"
    Dim TNST As String = "Thích nghi Sinh thái"
    Dim TNKTXH As String = "Tính khả thi kinh tế - xã hội"
    Dim TDMT As String = "Mức độ tác động môi trường"
    Dim HanChe As String = "Yếu tố hạn chế"
    Dim ghichu As String = "Ghi chú"
    Dim Enc_Check = "Nếu Encs không được tích thì chỉ có thể sử dụng tối đa 1 chỉ tiêu Kinh tế và 1 chỉ tiêu môi trường" + vbNewLine + "Đề nghị xóa bớt chỉ tiêu"
    Dim Enc_KoCheck = "Nếu Encs được tích thì phải có ít nhất 2 chỉ tiêu Kinh tế hoặc 2 chỉ tiêu môi trường trở lên" + vbNewLine + "Đề nghị thêm chỉ tiêu"
    ' Dim myLang As String = Frm_Startup.myLanguage
    <Export("Shell", GetType(ContainerControl))> _
    Private Shared Shell As ContainerControl
    Public Sub New()
        '    Dim AppManager11 = New DotSpatial.Controls.AppManager()
        Shell = Me
        'AppManager1.Directories.Add(Application.StartupPath + "\DotSpatial_Core.1.7\Windows Extensions")
        'AppManager1.Directories.Add(Application.StartupPath + "\DotSpatial_Core.1.7\Plugins")
        'AppManager11.LoadExtensions()
        Me.Controls.Clear()
        'This call is required by the designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call.
        DevExpress.UserSkins.BonusSkins.Register()  'Nhớ add Devexpress.bonusskins to references

        SkinHelper.InitSkinGallery(GalleryControl2, True)
        SkinHelper.InitSkinPopupMenu(PopupMenu2)
        Application.EnableVisualStyles()
        DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "Office 2010 Blue"
        'RibbonControl1.Minimized = True
    End Sub


    Private Sub FrmStart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Shell = Me
        'AppManager1.LoadExtensions()
        'FrmStartup.Hide()
        'Form2.Close()
        If myLang = "English" Then
            EnglishTranslate()
        End If

        Try 'Dùng Try Catch để tránh trường hợp sử dụng file dữ liệu rỗng
            'Dim Curdata As String
            'Dim IO_Reader_Writer As New IO_Reader_Writer
            ''Xét file dll, nếu chưa có thì tạo và ghi dữ liệu
            'If System.IO.File.Exists(Application.StartupPath + "\myFile.dll") = False Then
            'Dim newf As System.IO.FileStream = File.Create(Application.StartupPath + "\myFile.dll")
            'newf.Close()
            'Dim csdl As String = ChonvaLuuCSDL(Chonproject)
            'If csdl = "" Then
            'ChonvaLuuCSDL(Chonproject)
            'End If
            ''Curdata = IO_Reader_Writer.doc1dong(Application.StartupPath + "\myFile.dll", 1)
            'End If
            'Curdata = IO_Reader_Writer.doc1dong(Application.StartupPath + "\myFile.dll", 1)
            'If System.IO.File.Exists(Application.StartupPath + "\" + Curdata) = True Then
            'Curdata = Application.StartupPath + "\" + Curdata  'Trường hợp file mặc định trong myFile.dll là Nafosted.n
            'End If
            'If Curdata = "" Or System.IO.File.Exists(Curdata) = False Then  'Nếu nội dung chưa có đường dẫn hoặc file không tồn tại thì quay lại mở file
            'Dim csdl As String = ChonvaLuuCSDL(Chonproject)

            'If csdl = "" Then
            'ChonvaLuuCSDL(Chonproject)
            'End If
            ''Đọc lại Curdata trong trường hợp ko tồn tại, và đã chạy lại btnchonCSDL
            'Curdata = IO_Reader_Writer.doc1dong(Application.StartupPath + "\myFile.dll", 1)
            'End If

            'GroupControl2.Text = Curdata
            Dim IO_Reader_Writer As New IO_Reader_Writer
            Dim Curdata As String = IO_Reader_Writer.doc1dong(Application.StartupPath + "\myFile.dll", 1)
            If Curdata.Length > 35 Then
                MnuFileHientai.Text = "..." + Curdata.Substring(Curdata.Length - 35, 35)
            Else
                MnuFileHientai.Text = Curdata
            End If

            'MnuFileHientai.AllowTextClipping = True
            'If Curdata.Length > 63 Then
            'GroupControl2.Text = Curdata.Substring(0, 10) + "..." + Curdata.Substring(Curdata.Length - 53, 53)
            'Else
            'GroupControl2.Text = Curdata
            'End If
            Dim ToolTipItem2 As DevExpress.Utils.ToolTipItem = New DevExpress.Utils.ToolTipItem()
            ToolTipItem2.Text = Curdata
            Dim SuperToolTip2 As DevExpress.Utils.SuperToolTip = New DevExpress.Utils.SuperToolTip()
            SuperToolTip2.Items.Add(ToolTipItem2)
            MnuFileHientai.SuperTip = SuperToolTip2
            BarManager1.ShowScreenTipsInMenus = True
            BarManager1.ShowScreenTipsInToolbars = True

        Catch
        End Try
        Gridview1_Connect = New myADOclass  'Gán giá trị = 1 new. Phải có ở vị trí này để khi thay đổi file CSDL thì sử dụng đúng CSDL mới
        'Các thông số ConnectionString sẽ được gán khi Myconnect.open trong class mycon.vb
        Gridview2_Connect = New myADOclass
        Dim Qrydistict As String = "select distinct Obj from Maindata"
        Dim dt_listbox_obj As DataTable = Gridview1_Connect.DtFromQry(Qrydistict)
        ListBoxControl1.Items.Clear()
        If Frm_Startup.inputcaymoi <> "" Then
            ListBoxControl1.Items.Add(Frm_Startup.inputcaymoi)
        End If
        For i = 0 To dt_listbox_obj.Rows.Count - 1
            ListBoxControl1.Items.Add(dt_listbox_obj.Rows(i)("Obj"))
        Next
        ListBoxControl1.Items.Add(taocaymoi)


        'Dim Qry As String = "select * from Maindata "
        'Dim dt As DataTable = MyCon.DtFromQry(Qry)
        '==Initial Gridcontrol1
        'Dim theselectObj = Label1.Text
        'Dim Qry As String = "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """"

        Gridcontrol1_dt = Gridview1_Connect.DtFromQry("select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
        GridControl1.DataSource = Gridcontrol1_dt
        'For i As Integer = 0 To GridView1.Columns.Count - 1
        'GridView1.Columns(i).SortOrder = DevExpress.Data.ColumnSortOrder.None
        'Next
        GridView1.ClearSorting()
        'GridView1.OptionsCustomization.AllowSort = False
        GridView1.Columns("IndGroup").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        'GridControl1.DataBindings.Add("Datasource", dt, "")
        'BarButtonChonfile_ItemClick(Nothing, Nothing)

        'GridControl2.DataSource = Gridcontrol1_dt


        'formatdgv(GridView2)

        'Dim No1 As String =
        'LupFunction.DataBindings.Add("EditValue", GridControl1.DataSource, "UsedFunction")
        'Dim Qry As String = "select UsedFunction from Maindata "
        'Dim dt As DataTable = con.DtFromQry(Qry)
        'LupFunction.Properties.DataSource = dt
        'LupFunction.Properties.DisplayMember = "UsedFunction"
        'LupFunction.Properties.ValueMember = "UsedFunction"
        'Dim dt11 As DataTable = New DataTable
        'dt11.Columns.Add("Txt", GetType(String))
        'dt11.Columns.Add("Val", GetType(String))
        'dt11.Rows.Add("1", "Có")
        'dt11.Rows.Add("Có", "Không")
        'For i As Integer = 0 To 5
        'dt11.Rows.Add(i.ToString, i.ToString)
        'Next
        'DataGridView1.DataSource = dt11
        'RadLim.DataBindings.Clear()


        'RadLim.DataBindings.Add(New Binding("EditValue", DataGridView1.DataSource, "Val", True))

        'TxtIndGroup.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "IndGroup"))
        cboIndGroup.DataBindings.Clear()
        cboIndGroup.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "IndGroup"))
        txtInd.DataBindings.Clear()
        txtInd.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "Indname"))

        TxtS1.DataBindings.Clear()
        TxtS1.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "S1"))
        TxtS2.DataBindings.Clear()
        TxtS2.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "S2"))
        TxtS3.DataBindings.Clear()
        TxtS3.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "S3"))
        TxtN.DataBindings.Clear()
        TxtN.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "N"))

        txt_D.DataBindings.Clear()
        txt_D.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "d"))
        'AddHandler txt_D.Validating, AddressOf txt_D_Validating    'Không cần, đã có event luôn rồi còn addHandler gì nữa
        'AddHandler txt_D.InvalidValue, AddressOf txt_D_InvalidValue
        'AddHandler txt_D.LostFocus, AddressOf checkTxtVal

        Txt_A.DataBindings.Clear()
        Txt_A.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "a"))
        'AddHandler Txt_A.LostFocus, AddressOf checkTxtVal

        Txt_B.DataBindings.Clear()
        Txt_B.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "b"))
        'AddHandler Txt_B.LostFocus, AddressOf checkTxtVal

        Txt_C.DataBindings.Clear()
        Txt_C.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "c"))
        'AddHandler Txt_C.LostFocus, AddressOf checkTxtVal
        'txtToiUu2.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "S2"))
        'TxtFunction.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "UsedFunction"))

        cboFunction.DataBindings.Clear()
        'cboFunction.SelectedIndex = -1
        cboFunction.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "UsedFunction"))

        TxtMap.DataBindings.Clear()
        TxtMap.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "Map"))

        TxtMapchuanhoa.DataBindings.Clear()
        TxtMapchuanhoa.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "Mapchuanhoa"))

        TxtWeight.DataBindings.Clear()
        TxtWeight.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "Weight"))

        AddHandler btnDelete.LostFocus, AddressOf ClearStatus1  'Xóa Status (đã xóa dữ liệu thành công) khi btnDelete lostfocus
        AddHandler BtnUpdate.LostFocus, AddressOf ClearStatus1
        AddHandler XtraTabPage_Step4.LostFocus, AddressOf ClearStatus1
        'Initial cboFunction


        'cboFunction.Properties.TextEditStyle = False


        'Trước khi add datasouce vào ListboxControl1 thì đặt 1 biến = False. Sau đó đặt lại =True
        'Trong biến cố ListBoxControl1_SelectedIndexChanged ta dùng điều kiện
        'If (chayListboxControl1_selectedIndexChanged = False) Then
        'Return
        'End If
        'Như vậy thì biến cố chayListboxControl1_selectedIndexChanged sẽ chỉ chạy  khi selectedchange bởi người sử dụng (Nếu ko sẽ chạy ngay khi add datasouce)
        chayListboxControl1_selectedIndexChanged = False
        'ListBoxControl1.DataSource = dt
        chayListboxControl1_selectedIndexChanged = True
        'ListBoxControl1.DisplayMember = "Obj"
        'ListBoxControl1.Items.Add("fds")


        formatdgv1(GridView1)
        chayListboxControl1_selectedIndexChanged = True
        'ListBoxControl1_SelectedIndexChanged(Nothing, Nothing)
        'khoitao()
        'taocaymoivalue = truonghopcaymoi()
        'If taocaymoivalue = "" Then
        'FrmStart0.Show()
        'Me.Visible = False
        'Return
        'End If
        '''''If ListBoxControl1.ItemCount = 1 Then   'Trường hợp Project rỗng (Tạo Project mới)=> phải add thêm
        '''''ListBoxControl1.Tag.tostring = Frm_Startup.inputcaymoi
        ''''''"chayListboxControl1_selectedIndexChanged = False    Đặt cái này ở đây để ko chạy selected changed khi clear() Đặt lại = True ở cuối If
        '''''chayListboxControl1_selectedIndexChanged = False    'Đặt cái này ở đây để ko chạy selected changed khi clear()

        '''''ListBoxControl1.Items.Insert(0, Frm_Startup.inputcaymoi)

        '''''Dim Qrydistinct As String = "select distinct Obj from Maindata"
        '''''ListBoxControl1.SelectedValue = Frm_Startup.inputcaymoi
        '''''Gridview1_Reload()

        '''''GridView1.AddNewRow()

        '''''GridView1.GetDataRow(GridView1.GetSelectedRows(0))("Obj") = ListBoxControl1.SelectedValue.ToString
        '''''GridView1.GetDataRow(GridView1.GetSelectedRows(0))("IndName") = ""
        '''''GridView1.GetDataRow(GridView1.GetSelectedRows(0))("a") = 0
        '''''GridView1.GetDataRow(GridView1.GetSelectedRows(0))("b") = 1
        '''''cboIndGroup.SelectedIndex = 0
        '''''cboFunction.SelectedIndex = 0
        '''''RadLim.SelectedIndex() = 0

        '''''chayListboxControl1_selectedIndexChanged = True

        '''''btnDelete.Enabled = False
        '''''BtnReload.Enabled = False
        '''''BtnUpdate.Enabled = False
        ''''''clearXtrascroll()
        '''''End If



        'Không hiểu sao phải để RadLim.DataBindings.Add cuối thì mới ok
        RadLim.DataBindings.Clear()
        RadLim.DataBindings.Add(New Binding("EditValue", GridControl1.DataSource, "Limit", True))
        'Dim a = 1
        'Dim b = 2
        XtraTabPage_Step1.Focus()


        'Dim dt4 = Gridview1_Connect.DtFromQry("select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """" + " and (Limit = 'Không giới hạn' or Limit = 'Non-Limit')")
        'Gridcontrol1.DataSource = GridControl1.DataSource
        GridView1.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never
        GridView2.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never
        'GridView1.Columns("Limit").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[Limit] = 'Không giới hạn' or [Limit] = 'Non-Limit'")
        'formatdgv1(GridView1)
        'Gridcontrol1.DataBindings.Add("DataSource", dt4, "SummaryDetailList", True, DataSourceUpdateMode.OnPropertyChanged)
        Me.Text = CTDGTN + ListBoxControl1.Items(0).ToString
        BtnUpdate.Enabled = False

        ListBoxControl1.Tag = ListBoxControl1.SelectedValue
        'btnAdd.Enabled = False
        If ListBoxControl1.Tag.tostring <> Taocaymoi Then
            'btnAdd.Enabled = True
            Gridview2_Connect.createTable(ListBoxControl1.Tag.ToString, "[ID] INTEGER, [Name] TEXT(255), [SrcTif] TEXT(255), [RecTif] TEXT(255), FromTo TEXT (255), GhiChu TEXT (50), PRIMARY KEY (ID)")
            Dim dt_DGTN As DataTable = Gridview2_Connect.DtFromQry("Select * from [" + ListBoxControl1.Tag.ToString + "]")
            If dt_DGTN.Rows.Count = 0 Then
                Dim INSERTCommand As String
                If BarCheckItemComposite.Checked = True Then
                    INSERTCommand = "INSERT INTO [" + ListBoxControl1.Tag.ToString + "] (ID, Name, GhiChu) Values (0, '" + TNST + "', '" + "EnCs checked" + "')"
                Else
                    INSERTCommand = "INSERT INTO [" + ListBoxControl1.Tag.ToString + "] (ID, Name, GhiChu) Values (0, '" + TNST + "', '" + "" + "')"
                End If
                'Dim INSERTCommand As String = "INSERT INTO [" + ListBoxControl1.Tag.ToString + "] (ID, Name,GhiChu) Values (0, '" + TNST + "')"
                Dim theresult = Gridview2_Connect.RunaSQLCommand(dt_DGTN, INSERTCommand)
                INSERTCommand = "INSERT INTO [" + ListBoxControl1.Tag.ToString + "] (ID, Name) Values (1, '" + TNKTXH + "')"
                theresult = Gridview2_Connect.RunaSQLCommand(dt_DGTN, INSERTCommand)
                INSERTCommand = "INSERT INTO [" + ListBoxControl1.Tag.ToString + "] (ID, Name) Values (2, '" + TDMT + "')"
                theresult = Gridview2_Connect.RunaSQLCommand(dt_DGTN, INSERTCommand)
                INSERTCommand = "INSERT INTO [" + ListBoxControl1.Tag.ToString + "] (ID, Name) Values (3, '" + HanChe + "')"
                theresult = Gridview2_Connect.RunaSQLCommand(dt_DGTN, INSERTCommand)
            End If
            GridControl2.DataSource = Gridview2_Connect.DtFromQry("Select * from [" + ListBoxControl1.Tag.ToString + "]")
            If GridView2.GetRowCellValue(0, "GhiChu").ToString = "EnCs checked" Then
                BarCheckItemComposite.Checked = True
            Else
                BarCheckItemComposite.Checked = False
            End If
            'GridView2.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never
            'Dim filterQr As String = "[Name] = '" + ListBoxControl1.Tag.tostring + "'"
            'GridView2.Columns("Name").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(filterQr)	'Nhớ phải dùng biến filterQr chứ đưa trực tiêps "[Name] = '" + ListBoxControl1.Tag.tostring + "'" vào là lỗi

            'GridControl3.DataSource = GridControl2.DataSource   'Gridview2_Connect.DtFromQry("Select * from [" + ListBoxControl1.Tag.tostring + "] where Name <> '" + ListBoxControl1.Tag.tostring + "'")
            'GridView3.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never
            'filterQr = "[Name] <> '" + ListBoxControl1.Tag.tostring + "'"
            'GridView3.Columns("Name").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(filterQr)


            '==Autocomplete cho Indicator name
            Dim autocompleteString As New AutoCompleteStringCollection
            Dim dt_maindata As DataTable = Gridview1_Connect.DtFromQry("Select * from Maindata")
            For i As Integer = 0 To dt_maindata.Rows.Count - 1
                If dt_maindata.Rows(i)("Indname") IsNot System.DBNull.Value Then
                    If Not String.IsNullOrEmpty(dt_maindata.Rows(i)("Indname")) Then
                        autocompleteString.Add(dt_maindata.Rows(i)("Indname").ToString)
                    End If
                End If

            Next
            txtInd.MaskBox.AutoCompleteCustomSource = autocompleteString    '((From row In IndicatorTable.Rows.Cast(Of DataRow)() Select CStr(row("Desc"))).ToArray())
            txtInd.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            txtInd.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource
        End If

        ComboBoxEdit_Hanche.DataBindings.Clear()
        ComboBoxEdit_Hanche.DataBindings.Add(New Binding("Text", GridControl2.DataSource, "GhiChu"))
        GridView2.OptionsCustomization.AllowColumnMoving = False

        If GridView1.RowCount = 0 Then
            Gridview1_Reload()
            Gridview1_Connect.AddRow_with_ID_col(GridControl1, "ID", "MainData")
            'Nếu xử lý các dòng dưới = cách tạo newRow cho DataTable thì có cần AddRow_With_.. nữa ko? chưa thử nhưng với Indicator_TN thì thấy ok khi xử lý theTable.Rows.Add(anewRow) tại DataTable
            GridView1.GetDataRow(GridView1.GetSelectedRows(0))("Obj") = ListBoxControl1.SelectedValue.ToString
            'GridView1.SetRowCellValue(0, "Obj", ListBoxControl1.SelectedValue.ToString)	'Không dùng được SetRowCellValue vì sau khi AddNewRow, thử đếm Gridview1.Rowcount vẫn =0, ko hiểu tại sao lại = 0 nhỉ
            GridView1.GetDataRow(GridView1.GetSelectedRows(0))("IndName") = ""
            'GridView1.SetRowCellValue(-2147483647, "IndName", "44444")
            GridView1.GetDataRow(GridView1.GetSelectedRows(0))("a") = 0
            GridView1.SetRowCellValue(0, "a", 0)
            GridView1.GetDataRow(GridView1.GetSelectedRows(0))("b") = 1
            'GridView1.SetRowCellValue(0, "b", 1)
            cboIndGroup.SelectedIndex = 0
            cboFunction.SelectedIndex = 0
            RadLim.SelectedIndex() = 0

            btnDelete.Enabled = False
            BtnReload.Enabled = False
            BtnUpdate.Enabled = False
        End If
        GridView2.OptionsCustomization.AllowSort = False
        GridView1.OptionsCustomization.AllowFilter = False
        GridView1.OptionsCustomization.AllowColumnMoving = False
        GridView1.ClearColumnsFilter()
        GridView2.OptionsCustomization.AllowFilter = False
        GridControl2.Visible = False
        Me.WindowState = FormWindowState.Maximized
    End Sub
    Sub khoitao()   '===Chạy lần đầu khi file dữ liệu rỗng => Tạo mới cây trồng
        ''Điều kiện If để không cho biến cố SelectedIndexChanged fire khi định nghĩa listbox ListBoxControl1.DataSource = dt
        'If (chayListboxControl1_selectedIndexChanged = False) Then
        'Return
        'End If

        ''===Initial for Gridcontrol1
        ''Dim theselectObj As String = ""
        'ListBoxControl1.Tag.tostring = ""
        'If ListBoxControl1.SelectedValue Is Nothing Then
        'Return
        'End If
        'Dim sucsess As Boolean = Myconnect.Update_CSDL(GridControl1, "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
        ''BtnUpdate_click(Nothing, Nothing)

        'If ListBoxControl1.SelectedValue.ToString <> taocaymoi Then
        'ListBoxControl1.Tag.tostring = ListBoxControl1.SelectedValue.ToString
        'Gridview1_Reload()
        'Else
        'Dim myvalue As String = InputBox("", caymoi)
        ''================

        'If myvalue <> "" Then
        'For i As Integer = 0 To ListBoxControl1.ItemCount - 1
        'If ListBoxControl1.Items(i).ToString.ToUpper = myvalue.ToUpper Then
        'MessageBox.Show(caymoi, thongbao, MessageBoxButtons.OK)
        'Return
        'End If
        'Next
        'ListBoxControl1.Tag.tostring = myvalue
        ''"chayListboxControl1_selectedIndexChanged = False    Đặt cái này ở đây để ko chạy selected changed khi clear() Đặt lại = True ở cuối If
        'chayListboxControl1_selectedIndexChanged = False    'Đặt cái này ở đây để ko chạy selected changed khi clear()
        'ListBoxControl1.Items.Clear()
        'ListBoxControl1.Items.Add(myvalue)
        ''ListBoxControl1.SelectedItem
        'Dim Qrydistict As String = "select distinct Obj from Maindata"
        'Dim dt As DataTable = Myconnect.DtFromQry(Qrydistict)
        'For i = 0 To dt.Rows.Count - 1

        'ListBoxControl1.Items.Add(dt.Rows(i)("Obj"))
        'Next
        'ListBoxControl1.Items.Add(taocaymoi)
        'ListBoxControl1.SelectedValue = myvalue
        'Gridview1_Reload()
        ''GridView1.DeleteRow(0)
        ''GridView1.AddNewRow()
        ''GridView1.GetDataRow(GridView1.GetSelectedRows(0))("Obj") = myvalue

        ''btnAdd_Click(Nothing, Nothing)
        'GridView1.AddNewRow()

        'GridView1.GetDataRow(GridView1.GetSelectedRows(0))("Obj") = ListBoxControl1.SelectedValue.ToString
        'GridView1.GetDataRow(GridView1.GetSelectedRows(0))("IndName") = " "
        'GridView1.GetDataRow(GridView1.GetSelectedRows(0))("a") = 0
        'GridView1.GetDataRow(GridView1.GetSelectedRows(0))("b") = 1
        'cboIndGroup.SelectedIndex = 0
        'cboFunction.SelectedIndex = 0
        'RadLim.SelectedIndex() = 0
        ''BtnUpdate_click(Nothing, Nothing)
        ''txtInd.Text = " "
        ''Txt_A.Text = "1"
        ''Txt_B.Text = "0"
        ''RadLim.SelectedIndex = 0
        ''GridView1.RefreshData()
        'chayListboxControl1_selectedIndexChanged = True
        ''Me.Show()
        ''FrmStart0.Close()
        'Else

        'FrmStart0.Show()
        'Me.Hide()
        'Exit Sub
        'End If
        'End If
        ''ListBoxControl1.Tag.tostring = theselectObj
        ''Label1.Text = theselectObj

        ''===Initial for Lookupedit LupFunction
        ''FrmMain.LupFunction.DataBindings.Add("EditValue", FrmMain.GridControl1.DataSource, "UsedFunction")
        ''Dim QryLupFunction As String = "select UsedFunction from Maindata "
        ''Dim dtLupFunction As DataTable = mycon.DtFromQry(Qry)
        ''FrmMain.LupFunction.Properties.DataSource = dt
        ''FrmMain.LupFunction.Properties.DisplayMember = "UsedFunction"
        ''FrmMain.LupFunction.Properties.ValueMember = "UsedFunction"

        'Me.Text = "Chương trình đánh giá thích nghi đất đai - " + ListBoxControl1.Tag.tostring
        ''Show()
        ''Focus()

        'For i = 0 To PanelControl1.Controls.Count - 1
        'If TypeOf (PanelControl1.Controls(i)) Is DevExpress.XtraEditors.TextEdit Then
        'Dim mytxtbox As DevExpress.XtraEditors.TextEdit = PanelControl1.Controls(i)
        'mytxtbox.SelectionStart = mytxtbox.Text.Length + 1
        ''mytxtbox.ScrollToCaret()
        ''mytxtbox.Refresh()

        'End If
        'Next
        'XtraScrollableControl1.Controls.Clear()
        'XtraScrollableControl2.Controls.Clear()
        'capnhatlaiTab2_3 = False  'Khi thay đổi selectedindex thì bắt buộc phải reinitial xtratabpage2 và 3
    End Sub

    Private Sub ListBoxControl1_MarginChanged(sender As Object, e As EventArgs) Handles ListBoxControl1.MarginChanged

    End Sub
    Private Sub ListBoxControl1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListBoxControl1.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            'Dim edit As ListBoxControl = TryCast(sender, ListBoxControl)
            'Dim vi As BaseListBoxViewInfo = TryCast(edit.GetViewInfo(), BaseListBoxViewInfo)
            'Dim ii As BaseListBoxViewInfo.ItemInfo = TryCast(vi.GetItemInfoByPoint(e.Location), BaseListBoxViewInfo.ItemInfo)
            'If Not ii Is Nothing Then
            'edit.SelectedIndex = ii.Index
            'Text = ii.Index.ToString()
            'PopupMenu1.ShowPopup()
            'End If



            Dim thepoint As System.Drawing.Point = New System.Drawing.Point(e.X, e.Y)
            Dim ind As Integer = ListBoxControl1.IndexFromPoint(thepoint)
            chayListboxControl1_selectedIndexChanged = False
            ListBoxControl1.SelectedIndex = ind
            chayListboxControl1_selectedIndexChanged = True
            If ListBoxControl1.SelectedItem.ToString = Taocaymoi Then
                Return
            End If
            PopupMenu1.ShowPopup(Control.MousePosition)


        End If
    End Sub
    Private Sub BarButtonItemCopycay_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemCopyCay.ItemClick
        Dim srcObj As String = ListBoxControl1.SelectedItem
        Dim targetObj As String = InputBox(Copycay + srcObj + Copycay1, Copycay2)
        If targetObj = "" Then
            Return
        End If
        'targetObj.Replace(" ", "_")
        targetObj = targetObj.Replace(" ", "_")
        'targetObj = Replace(targetObj, " ", "_", Compare:=CompareMethod.Text)

        For i As Integer = 0 To ListBoxControl1.ItemCount - 1
            If ListBoxControl1.Items(i).ToString.ToUpper = targetObj.ToUpper Then
                MessageBox.Show(caymoi, thongbao)
                Return
            End If
        Next
        If targetObj = "" Then
            Return
        End If
        Gridview1_Connect = New myADOclass
        Dim qry As String = "select * from Maindata where Obj = " + """" + srcObj + """"
        Dim dt As DataTable = Gridview1_Connect.DtFromQry(qry)
        Dim dtall As DataTable = Gridview1_Connect.DtFromQry("select * from Maindata")
        ''====Tìm MaxID

        dtall.DefaultView.Sort = "ID ASC"
        Dim MaxID As Integer = 0
        Try
            MaxID = dtall.DefaultView.Item(dtall.Rows.Count - 1).Item("ID")
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        Try
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim STT As String = dt.Rows(i)("TT").ToString
                If STT = "" Then
                    STT = "null"
                End If
                Dim weig As String = dt.Rows(i)("weight").ToString
                If weig = "" Then
                    weig = "null"
                End If
                Dim INSERTCommand As String = "INSERT INTO Maindata (ID, Obj, IndGroup, IndName, S1, S2, S3, N, UsedFunction, a, b, c, d, Map,Mapchuanhoa, TT, Weight, Limit) Values ( " + CType(MaxID + 1 + i, String) + ", '" + _
                targetObj + "', '" + _
                dt.Rows(i)("IndGroup") + "', '" + _
                dt.Rows(i)("IndName") + "', '" + _
                dt.Rows(i)("S1").ToString + "', '" + _
                dt.Rows(i)("S2").ToString + "', '" + _
                dt.Rows(i)("S3").ToString + "', '" + _
                dt.Rows(i)("N").ToString + "', '" + _
                dt.Rows(i)("UsedFunction") + "', '" + _
                dt.Rows(i)("a").ToString + "', '" + _
                dt.Rows(i)("b").ToString + "', '" + _
                dt.Rows(i)("c").ToString + "', '" + _
                dt.Rows(i)("d").ToString + "', '" + _
                dt.Rows(i)("Map") + "', '" + _
                dt.Rows(i)("Mapchuanhoa") + "', " + _
                STT + ", " + _
                 weig + ", '" + _
                dt.Rows(i)("Limit").ToString + "')"

                ''MessageBox.Show("INSERT")
                Dim theresult = Gridview1_Connect.RunaSQLCommand(dt, INSERTCommand)

            Next

            Try
                '=====2. Copy Table có tên bảng là tên cây (Để thực hiện cho bước 5, và bước 6)
                Gridview2_Connect.createTable(targetObj, "[ID] INTEGER, [Name] TEXT(255), [SrcTif] TEXT(255), [RecTif] TEXT(255), FromTo TEXT (255), GhiChu TEXT (50), PRIMARY KEY (ID)")
                Dim InsertCommand1 As String = "Insert INTO " + targetObj + " Select * from " + srcObj
                Gridview1_Connect.RunaSQLCommand(dt, InsertCommand1)
                '=====3. Đổi tên cây trong bảng cây ở bước 2
                Dim UPDATECommand1 As String = "UPDATE " + targetObj + " SET Name = '" + targetObj + " 'WHERE Name = '" + srcObj + "'"
                Dim theresult1 = Gridview1_Connect.RunaSQLCommand(dt, UPDATECommand1)
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try


        '====Initial lại cho startup form
        'FrmStartup.chayListboxControl1_selectedIndexChanged = False

        Gridview1_Connect = New myADOclass
        Dim mdt As DataTable = Gridview1_Connect.DtFromQry("select distinct Obj from Maindata")
        ListBoxControl1.Items.Clear()
        For i = 0 To mdt.Rows.Count - 1
            ListBoxControl1.Items.Add(mdt.Rows(i)("Obj"))
        Next
        ListBoxControl1.Items.Add(Taocaymoi)
    End Sub
    Private Sub BarButtonItemRename_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemRename.ItemClick
        Dim srcObj As String = ListBoxControl1.SelectedItem
        Dim targetObj As String = InputBox(Doitencay + srcObj + Doitencay1, Doitencay2)
        If targetObj = "" Then
            Return
        End If
        targetObj = Replace(targetObj, " ", "_")
        'targetObj = Replace(targetObj, " ", "_", Compare:=CompareMethod.Text)

        'Copy sang cây mới
        For i As Integer = 0 To ListBoxControl1.ItemCount - 1
            If ListBoxControl1.Items(i).ToString.ToUpper = targetObj.ToUpper Then
                MessageBox.Show(tencaytontai, thongbao, MessageBoxButtons.OK)
                Return
            End If
        Next
        If targetObj = "" Then
            Return
        End If
        Gridview1_Connect = New myADOclass
        Dim qry As String = "select * from Maindata where Obj = " + """" + srcObj + """"
        Dim dt As DataTable = Gridview1_Connect.DtFromQry(qry)
        'For i As Integer = 0 To dt.Rows.Count - 1
        'Dim STT As String = dt.Rows(i)("TT").ToString
        'If STT = "" Then
        'STT = "null"
        'End If
        'Dim INSERTCommand As String = "INSERT INTO Maindata (Obj, IndGroup, IndName, UsedFunction, a, b, c, d, Map,Mapchuanhoa, TT, Weight, Limit) Values ( '" + _
        'targetObj + "', '" + _
        'dt.Rows(i)("IndGroup") + "', '" + _
        'dt.Rows(i)("IndName") + "', '" + _
        'dt.Rows(i)("UsedFunction") + "', '" + _
        'dt.Rows(i)("a").ToString + "', '" + _
        'dt.Rows(i)("b").ToString + "', '" + _
        'dt.Rows(i)("c").ToString + "', '" + _
        'dt.Rows(i)("d").ToString + "', '" + _
        'dt.Rows(i)("Map") + "', '" + _
        'dt.Rows(i)("Mapchuanhoa") + "', " + _
        'STT + ", '" + _
        'dt.Rows(i)("Weight").ToString + "', '" + _
        'dt.Rows(i)("Limit").ToString + "')"
        'MessageBox.Show("INSERT")
        'Dim theresult = Myconnect.RunaSQLCommand(INSERTCommand)
        'Next
        ''====Xóa cây đang chọn (sau khi nhân bản với tên khác)
        'BarButtonItemXoaCay_ItemClick(Nothing, Nothing)

        '====1. Đổi tên Cây trong MainData
        Dim UPDATECommand As String = "UPDATE Maindata SET Obj = '" + targetObj + "'WHERE Obj = '" + srcObj + "'"
        Dim theresult = Gridview1_Connect.RunaSQLCommand(dt, UPDATECommand)


        If theresult = True Then
            Try
                '=====2. Đổi tên Table có tên bảng là tên cây (Để thực hiện cho bước 5, và bước 6)
                'Copy sang bảng mới
                Gridview2_Connect.createTable(targetObj, "[ID] INTEGER, [Name] TEXT(255), [SrcTif] TEXT(255), [RecTif] TEXT(255), FromTo TEXT (255), GhiChu TEXT (50), PRIMARY KEY (ID)")
                Dim InsertCommand1 As String = "Insert INTO " + targetObj + " Select * from " + srcObj
                Gridview1_Connect.RunaSQLCommand(dt, InsertCommand1)
                'Xóa bảng cũ
                Dim DELETETableCommand As String = "DROP TABLE " + srcObj
                Gridview1_Connect.RunaSQLCommand(dt, DELETETableCommand)

                '=====3. Đổi tên cây trong bảng cây ở bước 2
                Dim UPDATECommand1 As String = "UPDATE " + targetObj + " SET Name = '" + targetObj + "'WHERE Name = '" + srcObj + "'"
                Dim theresult1 = Gridview1_Connect.RunaSQLCommand(dt, UPDATECommand1)
            Catch ex As Exception

            End Try

        End If
        '====Initial lại cho startup form
        'FrmStartup.chayListboxControl1_selectedIndexChanged = False

        Gridview1_Connect = New myADOclass
        Dim mdt As DataTable = Gridview1_Connect.DtFromQry("select distinct Obj from Maindata")
        ListBoxControl1.Items.Clear()
        For i = 0 To mdt.Rows.Count - 1
            ListBoxControl1.Items.Add(mdt.Rows(i)("Obj"))
        Next
        ListBoxControl1.Items.Add(Taocaymoi)
    End Sub
    Private Sub BarButtonItemXoaCay_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItemXoacay.ItemClick
        Dim srcObj As String = ListBoxControl1.SelectedItem.ToString
        Dim sure As DialogResult = MessageBox.Show(Xoacay, thongbao, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If sure = Windows.Forms.DialogResult.No Then
            Return
        End If

        Gridview1_Connect = New myADOclass
        Dim qry As String = "select * from Maindata where Obj = " + """" + srcObj + """"
        Dim dt As DataTable = Gridview1_Connect.DtFromQry(qry)

        'For i As Integer = 0 To dt.Rows.Count - 1

        Dim DeleteCommand As String = "DELETE FROM Maindata WHERE Obj = '" + srcObj + "'"
        Dim theresult = Gridview1_Connect.RunaSQLCommand(dt, DeleteCommand)
        If theresult = True Then
            Try
                Dim DELETETableCommand As String = "DROP TABLE " + srcObj
                Gridview1_Connect.RunaSQLCommand(dt, DELETETableCommand)
            Catch ex As Exception

            End Try

        End If
        'Next
        If ListBoxControl1.Items.Count = 1 Then     'Trường hợp chỉ còn "Tạo cây trồng mới"

            BtnUpdate.Enabled = False
            btnDelete.Enabled = False
            BtnReload.Enabled = False
        End If
        '====Initial lại cho startup form
        'FrmStartup.chayListboxControl1_selectedIndexChanged = False

        Gridview1_Connect = New myADOclass
        Dim mdt As DataTable = Gridview1_Connect.DtFromQry("select distinct Obj from Maindata")
        ListBoxControl1.Items.Clear()
        Try
            For i = 0 To mdt.Rows.Count - 1
                If mdt.Rows(i)("Obj").ToString <> "" Then
                    ListBoxControl1.Items.Add(mdt.Rows(i)("Obj").ToString)
                End If

            Next
        Catch ex As Exception

        End Try

        ListBoxControl1.Items.Add(taocaymoi)
    End Sub

    Sub clearXtrascroll()
        Me.Text = CTDGTN + ListBoxControl1.Tag.tostring

        For i = 0 To PanelControl1.Controls.Count - 1
            If TypeOf (PanelControl1.Controls(i)) Is DevExpress.XtraEditors.TextEdit Then
                Dim mytxtbox As DevExpress.XtraEditors.TextEdit = PanelControl1.Controls(i)
                mytxtbox.SelectionStart = mytxtbox.Text.Length + 1
                'mytxtbox.ScrollToCaret()
                'mytxtbox.Refresh()

            End If
        Next
        XtraScrollableControl1.Controls.Clear()
        XtraScrollableControl2.Controls.Clear()
        capnhatlaiTab2_3 = False  'Khi thay đổi selectedindex thì bắt buộc phải reinitial xtratabpage2 và 3
    End Sub
    Private Sub ListBoxControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBoxControl1.SelectedIndexChanged
        'Điều kiện If để không cho biến cố SelectedIndexChanged fire khi định nghĩa listbox ListBoxControl1.DataSource = dt
        If (chayListboxControl1_selectedIndexChanged = False) Then
            Return
        End If
        Dim myvalue As String
        '===Initial for Gridcontrol1
        'Dim theselectObj As String = ""
        'ListBoxControl1.Tag.tostring Là tên cây được select
        If ListBoxControl1.SelectedValue Is Nothing Then
            Return
        End If
        Dim sucsess As Boolean = Gridview1_Connect.Update_CSDL(GridControl1, "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
        'BtnUpdate_click(Nothing, Nothing)

        If ListBoxControl1.SelectedValue.ToString <> taocaymoi Then
            ListBoxControl1.Tag = ListBoxControl1.SelectedValue.ToString
            Gridview1_Reload()
        Else

            myvalue = InputBox("", caymoi)
            '================
            myvalue = myvalue.Replace(" ", "_")
            'myvalue = Replace(myvalue, " ", "_", Compare:=CompareMethod.Text)
            'myvalue = Replace(myvalue, " ", "_", Compare:=CompareMethod.Text)
            'myvalue = Replace(myvalue, " ", "_", Compare:=CompareMethod.Text)
            'myvalue = Replace(myvalue, " ", "_", Compare:=CompareMethod.Text)
            If myvalue <> "" Or (myvalue = "" And ListBoxControl1.Items.Count = 1) Then     'Hoặc trường hợp  count = 1 thì rỗng cũng được
                If myvalue = "" Then
                    myvalue = "ABCXYZ"
                End If
                For i As Integer = 0 To ListBoxControl1.ItemCount - 1
                    If ListBoxControl1.Items(i).ToString.ToUpper = myvalue.ToUpper Then
                        MessageBox.Show(tencaytontai, thongbao, MessageBoxButtons.OK)
                        Return
                    End If
                Next
                ListBoxControl1.Tag = myvalue
                '"chayListboxControl1_selectedIndexChanged = False    Đặt cái này ở đây để ko chạy selected changed khi clear() Đặt lại = True ở cuối If
                chayListboxControl1_selectedIndexChanged = False    'Đặt cái này ở đây để ko chạy selected changed khi clear()
                ListBoxControl1.Items.Clear()
                ListBoxControl1.Items.Add(myvalue)
                'ListBoxControl1.SelectedItem
                Dim Qrydistict As String = "select distinct Obj from Maindata"
                Dim dt As DataTable = Gridview1_Connect.DtFromQry(Qrydistict)
                For i = 0 To dt.Rows.Count - 1

                    ListBoxControl1.Items.Add(dt.Rows(i)("Obj"))
                Next
                ListBoxControl1.Items.Add(taocaymoi)
                ListBoxControl1.SelectedValue = myvalue
                Gridview1_Reload()
                Gridview1_Connect.AddRow_with_ID_col(GridControl1, "ID", "MainData")
                'Nếu xử lý các dòng dưới = cách tạo newRow cho DataTable thì có cần AddRow_With_.. nữa ko? chưa thử nhưng với Indicator_TN thì thấy ok khi xử lý theTable.Rows.Add(anewRow) tại DataTable
                GridView1.GetDataRow(GridView1.GetSelectedRows(0))("Obj") = ListBoxControl1.SelectedValue.ToString
                'GridView1.SetRowCellValue(0, "Obj", ListBoxControl1.SelectedValue.ToString)	'Không dùng được SetRowCellValue vì sau khi AddNewRow, thử đếm Gridview1.Rowcount vẫn =0, ko hiểu tại sao lại = 0 nhỉ
                GridView1.GetDataRow(GridView1.GetSelectedRows(0))("IndName") = ""
                'GridView1.SetRowCellValue(-2147483647, "IndName", "44444")
                GridView1.GetDataRow(GridView1.GetSelectedRows(0))("a") = 0
                'GridView1.SetRowCellValue(0, "a", 0)
                GridView1.GetDataRow(GridView1.GetSelectedRows(0))("b") = 1
                'GridView1.SetRowCellValue(0, "b", 1)
                cboIndGroup.SelectedIndex = 0
                cboFunction.SelectedIndex = 0
                RadLim.SelectedIndex() = 0
                'BtnUpdate_click(Nothing, Nothing)
                'txtInd.Text = " "
                'Txt_A.Text = "1"
                'Txt_B.Text = "0"
                'RadLim.SelectedIndex = 0
                'GridView1.RefreshData()
                chayListboxControl1_selectedIndexChanged = True

                btnDelete.Enabled = False
                BtnReload.Enabled = False
                BtnUpdate.Enabled = False
            Else
                ListBoxControl1.SelectedIndex = 0
                BtnUpdate.Enabled = False
                Return
            End If

        End If
        'ListBoxControl1.Tag.tostring = theselectObj
        'Label1.Text = theselectObj

        '===Initial for Lookupedit LupFunction
        'FrmMain.LupFunction.DataBindings.Add("EditValue", FrmMain.GridControl1.DataSource, "UsedFunction")
        'Dim QryLupFunction As String = "select UsedFunction from Maindata "
        'Dim dtLupFunction As DataTable = mycon.DtFromQry(Qry)
        'FrmMain.LupFunction.Properties.DataSource = dt
        'FrmMain.LupFunction.Properties.DisplayMember = "UsedFunction"
        'FrmMain.LupFunction.Properties.ValueMember = "UsedFunction"
        clearXtrascroll()
        '==============================Tạo  MSAccessTable để dùng cho bước 5 và 6
        'Myconnect.createTable(ListBoxControl1.Tag.tostring, "[SrcTif] TEXT(255), [RecTif] TEXT(255), FromTo TEXT (255)")
        'ListBoxControl1.Tag.tostring = ListBoxControl1.SelectedValue
        Gridview2_Connect.createTable(ListBoxControl1.Tag.ToString, "[ID] INTEGER, [Name] TEXT(255), [SrcTif] TEXT(255), [RecTif] TEXT(255), FromTo TEXT (255), GhiChu TEXT (50), PRIMARY KEY (ID)")
        Dim dt_DGTN As DataTable = Gridview2_Connect.DtFromQry("Select * from [" + ListBoxControl1.Tag.ToString + "]")
        If dt_DGTN.Rows.Count = 0 Then
            Dim INSERTCommand As String
            If BarCheckItemComposite.Checked = True Then
                INSERTCommand = "INSERT INTO [" + ListBoxControl1.Tag.ToString + "] (ID, Name, GhiChu) Values (0, '" + TNST + "', '" + "EnCs checked" + "')"
            Else
                INSERTCommand = "INSERT INTO [" + ListBoxControl1.Tag.ToString + "] (ID, Name, GhiChu) Values (0, '" + TNST + "', '" + "" + "')"
            End If

            Dim theresult = Gridview2_Connect.RunaSQLCommand(dt_DGTN, INSERTCommand)
            INSERTCommand = "INSERT INTO [" + ListBoxControl1.Tag.ToString + "] (ID, Name) Values (1, '" + TNKTXH + "')"
            theresult = Gridview2_Connect.RunaSQLCommand(dt_DGTN, INSERTCommand)
            INSERTCommand = "INSERT INTO [" + ListBoxControl1.Tag.ToString + "] (ID, Name) Values (2, '" + TDMT + "')"
            theresult = Gridview2_Connect.RunaSQLCommand(dt_DGTN, INSERTCommand)
            INSERTCommand = "INSERT INTO [" + ListBoxControl1.Tag.ToString + "] (ID, Name) Values (3, '" + HanChe + "')"
            theresult = Gridview2_Connect.RunaSQLCommand(dt_DGTN, INSERTCommand)
        End If
        'Gridcontrol1.DataSource = Gridview1_Connect.DtFromQry("select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """" + " and (Limit = 'Không giới hạn' or Limit = 'Non-Limit')")

        'GridControl2.DataSource = Gridview2_Connect.DtFromQry("Select * from [" + ListBoxControl1.Tag.tostring + "] where Name = '" + ListBoxControl1.Tag.tostring + "'")
        'GridControl3.DataSource = Gridview2_Connect.DtFromQry("Select * from [" + ListBoxControl1.Tag.tostring + "] where Name <> '" + ListBoxControl1.Tag.tostring + "'")

        GridControl2.DataSource = Gridview2_Connect.DtFromQry("Select * from [" + ListBoxControl1.Tag.tostring + "]")
        'GridControl3.DataSource = GridControl2.DataSource
        ''GridView2.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never
        ''Dim filterQr As String = "[Name] = '" + ListBoxControl1.Tag.tostring + "'"
        ''GridView2.Columns("Name").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(filterQr)	'Nhớ phải dùng biến filterQr chứ đưa trực tiêps "[Name] = '" + ListBoxControl1.Tag.tostring + "'" vào là lỗi

        ''GridControl3.DataSource = GridControl2.DataSource	'Gridview2_Connect.DtFromQry("Select * from [" + ListBoxControl1.Tag.tostring + "] where Name <> '" + ListBoxControl1.Tag.tostring + "'")
        ''GridView3.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never
        ''filterQr = "[Name] <> '" + ListBoxControl1.Tag.tostring + "'"
        ''GridView3.Columns("Name").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(filterQr)
        Try
            'Dim mydt As DataTable = Gridview2_Connect.DtFromQry("Select * from [" + ListBoxControl1.Tag.ToString + "]")
            'GridControl2.DataSource = mydt
            GridView2.ClearColumnsFilter()
            If GridView2.GetRowCellValue(0, "GhiChu").ToString <> "EnCs checked" Then
                BarCheckItemComposite.Checked = False
            Else
                BarCheckItemComposite.Checked = True
            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Sub ListBoxControl1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBoxControl1.DoubleClick
        If ListBoxControl1.SelectedValue.ToString = taocaymoi Then
            ListBoxControl1_SelectedIndexChanged(Nothing, Nothing)
            '
            'BtnUpdate.Enabled = False
            'btnDelete.Enabled = False
            'BtnReload.Enabled = False
        End If
        ''Điều kiện If để không cho biến cố SelectedIndexChanged fire khi định nghĩa listbox ListBoxControl1.DataSource = dt
        'If (chayListboxControl1_selectedIndexChanged = False) Then
        'Return
        'End If

        ''===Initial for Gridcontrol1
        'Dim theselectObj As String = ListBoxControl1.SelectedValue
        'Label1.Text = theselectObj

        ''===Initial for Lookupedit LupFunction
        ''FrmMain.LupFunction.DataBindings.Add("EditValue", FrmMain.GridControl1.DataSource, "UsedFunction")
        ''Dim QryLupFunction As String = "select UsedFunction from Maindata "
        ''Dim dtLupFunction As DataTable = mycon.DtFromQry(Qry)
        ''FrmMain.LupFunction.Properties.DataSource = dt
        ''FrmMain.LupFunction.Properties.DisplayMember = "UsedFunction"
        ''FrmMain.LupFunction.Properties.ValueMember = "UsedFunction"

        'Me.Text = "Chương trình đánh giá thích nghi cây trồng - Đánh giá thích nghi " + theselectObj
        ''Show()
        ''Focus()
        'Gridview1_Reload()
    End Sub
    Public Sub formatdgv1(ByVal XtraGrdView As DevExpress.XtraGrid.Views.Grid.GridView)
        XtraGrdView.GridControl.Dock = DockStyle.Fill
        XtraGrdView.OptionsCustomization.AllowSort = False
        'Prevent focused cell from being highlighted
        XtraGrdView.OptionsSelection.EnableAppearanceFocusedCell = False
        'XtraGrdView.OptionsBehavior.Editable = False
        'XtraGrdView.Columns("TT").OptionsColumn.AllowEdit = True
        For Each col In XtraGrdView.Columns     'disable editing for all columns
            col.OptionsColumn.AllowEdit = False
            XtraGrdView.Columns("TT").OptionsColumn.AllowEdit = True
            XtraGrdView.Columns("Weight").OptionsColumn.AllowEdit = True
        Next

        'Draw dotted focus rectangle around entire row
        XtraGrdView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

        XtraGrdView.OptionsBehavior.AllowAddRows = DefaultBoolean.True
        XtraGrdView.OptionsView.NewItemRowPosition = Top    'Lệnh này để hide cái dòng edit row ở trên cùng đi. Ko hiểu tại sao

        XtraGrdView.OptionsView.ColumnAutoWidth = False     'Lê
        XtraGrdView.OptionsView.ShowGroupPanel = False  'fdsfđ
        XtraGrdView.OptionsView.ShowIndicator = False 'invisible header Row  

        'Gridview1_Connect.Set_AUTOINCREMENT_collumn(XtraGrdView.GridControl, "ID", "MainData")
        '================Dòng chẵn 1 màu, dòng lẻ 1 màu
        XtraGrdView.OptionsView.EnableAppearanceEvenRow = True
        XtraGrdView.Appearance.EvenRow.BackColor = Color.Bisque   'GridView2.Appearance.Row.BackColor = Color.Bisque
        'Dim a = XtraGrdView.Columns.ColumnByFieldName("tenPT")
        XtraGrdView.Columns.ColumnByFieldName("Obj").Visible = False
        XtraGrdView.Columns.ColumnByFieldName("ID").Visible = False
        XtraGrdView.Columns.ColumnByFieldName("TT").Caption = mucdoquantrong
        'XtraGrdView.Columns.ColumnByFieldName("TT").Visible = False
        XtraGrdView.Columns.ColumnByFieldName("TT").Width = 100
        XtraGrdView.Columns.ColumnByFieldName("a").Width = 45
        XtraGrdView.Columns.ColumnByFieldName("b").Width = 45
        XtraGrdView.Columns.ColumnByFieldName("c").Width = 45
        XtraGrdView.Columns.ColumnByFieldName("d").Width = 45
        ''XtraGrdView.Columns.ColumnByFieldName("Tinh").Visible = True
        ''XtraGrdView.Columns.ColumnByFieldName("Huyen").Visible = True
        ''XtraGrdView.Columns.ColumnByFieldName("Xa").Visible = True
        XtraGrdView.Columns.ColumnByFieldName("IndGroup").Caption = Nhomchitieu
        XtraGrdView.Columns.ColumnByFieldName("IndGroup").Width = 95
        XtraGrdView.Columns.ColumnByFieldName("IndGroup").Visible = True

        XtraGrdView.Columns.ColumnByFieldName("IndName").Caption = Chitieu
        XtraGrdView.Columns.ColumnByFieldName("IndName").Width = 260

        XtraGrdView.Columns.ColumnByFieldName("UsedFunction").Visible = True
        XtraGrdView.Columns.ColumnByFieldName("UsedFunction").Caption = Hamsudung
        XtraGrdView.Columns.ColumnByFieldName("UsedFunction").Width = 100

        XtraGrdView.Columns.ColumnByFieldName("Map").Visible = True
        XtraGrdView.Columns.ColumnByFieldName("Map").Caption = Bandochitieu
        XtraGrdView.Columns.ColumnByFieldName("Map").Width = 235

        XtraGrdView.Columns.ColumnByFieldName("Mapchuanhoa").Visible = True
        XtraGrdView.Columns.ColumnByFieldName("Mapchuanhoa").Caption = Bandochuanhoa
        XtraGrdView.Columns.ColumnByFieldName("Mapchuanhoa").Width = 235

        XtraGrdView.Columns.ColumnByFieldName("Weight").Caption = trongso
        XtraGrdView.Columns.ColumnByFieldName("Weight").Width = 60

        XtraGrdView.Columns.ColumnByFieldName("Limit").Caption = Gioihan
        XtraGrdView.Columns.ColumnByFieldName("Limit").Width = 95
        'GridView2. = New Font("arial", 10, FontStyle.Regular, GraphicsUnit.Point, 1)
        'GridView2.RowHeadersVisible = False

        XtraGrdView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto
        XtraGrdView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto

        GridControl1.Update()
        SetGridFont(XtraGrdView, New Font("arial", 9))
        'PopupMenuShowing(GridControl2)
        If (XtraGrdView.Name = "GridView2") Then
            XtraGrdView.Columns.ColumnByFieldName("a").Visible = False
            XtraGrdView.Columns.ColumnByFieldName("b").Visible = False
            XtraGrdView.Columns.ColumnByFieldName("c").Visible = False
            XtraGrdView.Columns.ColumnByFieldName("d").Visible = False
            XtraGrdView.Columns.ColumnByFieldName("IndGroup").Visible = False
            XtraGrdView.Columns.ColumnByFieldName("UsedFunction").Visible = False
            XtraGrdView.Columns.ColumnByFieldName("Map").Visible = False
            XtraGrdView.Columns.ColumnByFieldName("Mapchuanhoa").Visible = False
        End If

    End Sub
    Public Sub formatdgv2_3(ByVal XtraGrdView As DevExpress.XtraGrid.Views.Grid.GridView)
        'XtraGrdView.GridControl.Dock = DockStyle.Fill
        XtraGrdView.OptionsSelection.EnableAppearanceFocusedCell = False
        XtraGrdView.OptionsBehavior.Editable = False
        'Draw dotted focus rectangle around entire row
        XtraGrdView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        '  XtraGrdView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

        XtraGrdView.OptionsBehavior.AllowAddRows = DefaultBoolean.True
        XtraGrdView.OptionsView.NewItemRowPosition = Top        'Lệnh này để hide cái dòng edit row ở trên cùng đi. Ko hiểu tại sao
        XtraGrdView.OptionsView.ColumnAutoWidth = False
        XtraGrdView.OptionsView.ShowGroupPanel = False
        XtraGrdView.OptionsView.ShowIndicator = False 'invisible header Row
        'Gridview2_Connect.AddRow_with_AUTOINCREMENT_col(XtraGrdView.GridControl, "ID", ListBoxControl1.Tag.tostring)

        'XtraGrdView.Columns.ColumnByFieldName("ID").Visible = False
        '===Tab5
        'TxtDGTNST.DataBindings.Clear()
        'TxtDGTNST.DataBindings.Add(New Binding("Text", GridControl2.DataSource, "SrcTif"))
        'TxtDGTNST_Reclass.DataBindings.Clear()
        'TxtDGTNST_Reclass.DataBindings.Add(New Binding("Text", GridControl2.DataSource, "RecTif"))
        '---Tab6
        TxtFilterName.DataBindings.Clear()
        TxtRawdata.DataBindings.Clear()
        TxtFilterReclass.DataBindings.Clear()
        TxtFilterName.DataBindings.Add(New Binding("Text", GridControl2.DataSource, "Name"))
        TxtRawdata.DataBindings.Add(New Binding("Text", GridControl2.DataSource, "SrcTif"))
        TxtFilterReclass.DataBindings.Add(New Binding("Text", GridControl2.DataSource, "RecTif"))
        ComboBoxEdit_Hanche.DataBindings.Clear()
        ComboBoxEdit_Hanche.DataBindings.Add(New Binding("Text", GridControl2.DataSource, "GhiChu"))

        XtraGrdView.Columns.ColumnByFieldName("ID").Visible = False
        'XtraGrdView.Columns.ColumnByFieldName("ID").Width = 0
        XtraGrdView.Columns.ColumnByFieldName("Name").Width = 150
        XtraGrdView.Columns.ColumnByFieldName("Name").Caption = NameF
        XtraGrdView.Columns.ColumnByFieldName("SrcTif").Width = 400
        XtraGrdView.Columns.ColumnByFieldName("SrcTif").Caption = SrcTifF
        XtraGrdView.Columns.ColumnByFieldName("RecTif").Width = 400
        XtraGrdView.Columns.ColumnByFieldName("RecTif").Caption = RecTifF
        XtraGrdView.Columns.ColumnByFieldName("FromTo").Width = 200
        XtraGrdView.Columns.ColumnByFieldName("FromTo").Caption = Giatriphankhoang
        XtraGrdView.Columns.ColumnByFieldName("GhiChu").Caption = ghichu
        XtraGrdView.OptionsCustomization.AllowSort = False
    End Sub
    Sub SetGridFont(ByVal view As GridView, ByVal font As Font)

        Dim ap As AppearanceObject

        For Each ap In view.Appearance

            ap.Font = font

        Next

    End Sub
    Private Sub SimpleButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "Office 2010 Blue"
        DevExpress.LookAndFeel.UserLookAndFeel.Default.UseWindowsXPTheme = False
        'DevExpress.LookAndFeel.UserLookAndFeel.Default.Office2003()
    End Sub

    Private Sub ObjSelect_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles ObjSelect.ItemClick
        'FrmStartup.Show()
    End Sub


    'Cho phép Input Null Value
    Private Sub GridView1_ValidatingEditor(sender As Object, e As DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs) Handles GridView1.ValidatingEditor
        Dim view As GridView = CType(sender, GridView)
        If view.FocusedColumn.FieldName = "TT" Then
            If Not e.Value Is Nothing AndAlso e.Value = "" Then
                e.Value = DBNull.Value
            End If
        End If
    End Sub

    Private Sub TinhTrongSo()

        For i As Integer = 0 To GridView1.RowCount - 1
            If GridView1.GetRowCellValue(i, "TT").ToString = "" Then
                'For j As Integer = 0 To dt.Rows.Count - 1
                'dt.Rows(i)("TT") = i + 1   'dt.Rows(j)("TT") = j + 1       Dùng update len DT này ko ăn thua, dùng cái dưới thì ok
                GridView1.SetRowCellValue(i, "TT", i + 1)
                'Next
                Dim a = GridView1.GetRowCellValue(i, "TT").ToString
            End If
        Next

        Dim sumconvertTT As Single = 0 'sumconvertTT = Tổng của 1/TTk   k chạy từ 1 tới n
        If GridView1.RowCount <= 1 Then
            sumconvertTT = 1
        Else
            'For i As Integer = 0 To GridView1.RowCount - 1
            ''sumconvertTT = sumconvertTT + (Convert.ToInt16(GridView1.GetRowCellValue(i, "TT").ToString))
            'sumconvertTT = sumconvertTT + GridView1.GetRowCellValue(i, "TT").ToString
            ''sumconvertTT = sumconvertTT + Convert.ToInt16(e.Value.ToString)
            'Next
            For i As Integer = 0 To GridView1.RowCount - 1
                sumconvertTT = sumconvertTT + (1 / Convert.ToInt16(GridView1.GetRowCellValue(i, "TT").ToString))
            Next
        End If

        'GridView1.BeginDataUpdate()
        For i As Integer = 0 To GridView1.RowCount - 1
            'Dim newval As Single = Math.Round(Convert.ToInt16(GridView1.GetRowCellValue(i, "TT")) / sumconvertTT, 5)
            Dim newval = Math.Round((1 / Convert.ToInt16(GridView1.GetRowCellValue(i, "TT"))) / sumconvertTT, 3)
            'Dim newval As Single = Math.Round(Convert.ToInt16(e.Value.ToString) / sumconvertTT, 3)
            If GridView1.GetRowCellValue(i, "Weight").ToString <> newval.ToString Then    'Nếu ko để if ở đây thì sẽ chạy đi chạy lại Event CellValueChanged
                GridView1.SetRowCellValue(i, "Weight", newval)
            End If


            'dt.Rows(selectedrowID - 1)("TT") = Convert.ToInt16(GridView2.GetRowCellValue(selectedrowID, "TT")) + 1
            'GridView1.RefreshData()
        Next
        'GridView1.EndDataUpdate()
        ''=====================2 lệnh sau rất quan trọng, nếu ko sẽ ko update focused row
        ''GridView1.PostEditor()
        ''GridView1.UpdateCurrentRow()
        ''=====================
        ''Dim a As DataTable = GridControl2.DataSource.getchanges
        ''Dim mycon As MyCon = New MyCon
        'Gridview1_Connect.Update_CSDL(GridControl1, "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")

        ''GridView1.Columns("TT").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        ''GridControl1.Update()
        ''Me.Refresh()
    End Sub
    Private Sub GridView1_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If Not XtraTabControl1.SelectedTabPage Is XtraTabPage_Step3 Then
            Return
        End If
        If e.Column.FieldName = "Weight" Then       'Nếu sửa ở Cột Weight thì out
            'GridView1.SetRowCellValue(GridView1.FocusedRowHandle, "TT", DBNull.Value)
            Return
        End If
        TinhTrongSo()
    End Sub


    Private Sub GridView1_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        'Dim Change_nochange As Boolean = BtnUpdate.Enabled  'Biến dùng lưu giá trị BtnUpdate.Enabled
        ''Vì BtnUpdate.Enable sẽ bị thay đổi trong quá trình Sub GridView1_FocusedRowChanged Nên dùng biến này để đặt lại giá trị cho BtnUpdate.Enable ở cuối Sub
        'Dim Change_nochange1 = BtnUpdate.Text
        If e.PrevFocusedRowHandle < 0 Then
            Return
        End If

        If GridView1.GetDataRow(e.PrevFocusedRowHandle) Is Nothing Then
            Return
        End If
        If GridView1.GetDataRow(e.PrevFocusedRowHandle)("IndName").ToString = "" Or GridView1.GetDataRow(e.PrevFocusedRowHandle)("IndName").ToString = " " Then
            MessageBox.Show(Tenchitieu, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'GridView1.FocusedRowHandle = e.PrevFocusedRowHandle
            XtraTabControl1.SelectedTabPageIndex = 0
            txtInd.Focus()
            Return
        End If

        If GridView1.GetDataRow(e.PrevFocusedRowHandle)("UsedFunction").ToString = aHamhinhthang Then
            If GridView1.GetDataRow(e.PrevFocusedRowHandle)("b").ToString = "" Or GridView1.GetDataRow(e.PrevFocusedRowHandle)("c").ToString = "" Then
                MessageBox.Show(giatribc + vbNewLine + trogiuphinhthang, loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                PicCheckHam.Image = My.Resources.Resources.cancel_32x32
                PicCheckHam.ToolTip = giatribc + vbNewLine + trogiuphinhthang
                GridView1.FocusedRowHandle = e.PrevFocusedRowHandle
                Txt_B.Focus()
                XtraTabControl1.SelectedTabPageIndex = 0
                Return
            Else
                PicCheckHam.Image = My.Resources.Resources.apply_32x32
                PicCheckHam.ToolTip = "OK"
            End If

            'If txt_D.Text = "" Then
            'txt_D.Text = "99999"
            'End If
            'If Txt_A.Text = "" Then
            'Txt_A.Text = "-99999"
            'End If
        ElseIf GridView1.GetDataRow(e.PrevFocusedRowHandle)("UsedFunction").ToString = aHamkandel Then
            If GridView1.GetDataRow(e.PrevFocusedRowHandle)("a").ToString = "" And GridView1.GetDataRow(e.PrevFocusedRowHandle)("b").ToString = "" Then
                MessageBox.Show(giatriab + vbNewLine + trogiupkandel, loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                PicCheckHam.Image = My.Resources.Resources.cancel_32x32
                PicCheckHam.ToolTip = giatriab + vbNewLine + trogiupkandel
                GridView1.FocusedRowHandle = e.PrevFocusedRowHandle
                Txt_A.Focus()
                XtraTabControl1.SelectedTabPageIndex = 0
                Return
            Else
                PicCheckHam.Image = My.Resources.Resources.apply_32x32
                PicCheckHam.ToolTip = "OK"
            End If
            'If Txt_A.Text = "" Then
            'Txt_A.Text = "-99999"
            'End If
            'If Txt_B.Text = "" Then
            'Txt_B.Text = "99999"
            'End If
            'Txt_C.Text = ""
            'txt_D.Text = ""
        ElseIf GridView1.GetDataRow(e.PrevFocusedRowHandle)("UsedFunction").ToString = aHams1 Or GridView1.GetDataRow(e.PrevFocusedRowHandle)("UsedFunction").ToString = aHams2 Then
            If GridView1.GetDataRow(e.PrevFocusedRowHandle)("a").ToString = "" Or GridView1.GetDataRow(e.PrevFocusedRowHandle)("b").ToString = "" Then
                MessageBox.Show(giatriab + vbNewLine + trogiupS, loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                PicCheckHam.Image = My.Resources.Resources.cancel_32x32
                PicCheckHam.ToolTip = giatriab + vbNewLine + trogiupS
                GridView1.FocusedRowHandle = e.PrevFocusedRowHandle
                Txt_A.Focus()
                XtraTabControl1.SelectedTabPageIndex = 0
                Return
                'ElseIf Txt_B.Text = "" Then
                'Txt_B.Text = ((Convert.ToSingle(Txt_A.Text) + Convert.ToSingle(Txt_C.Text)) / 2).ToString
                'Else
                'PicCheckHam.Image = My.Resources.Resources.apply_32x32
                'PicCheckHam.ToolTip = "OK"
            End If
        End If
        '===Xử lý disable khi chọn hàm không chuẩn hóa; hạn chế; ....
        If (cboIndGroup.SelectedItem = "1. Sinh thái" Or cboIndGroup.SelectedItem = "1. Ecology") And (cboFunction.SelectedItem = ahamKhongham) Then
            DisableControl()
            RadLim.SelectedIndex = -1      'Thử bỏ đi do lỗi check và bỏ check cái Enc xong nó cứ báo lỗi tổng trọng số =1khi chuyển đi chuyển lại cái Tab Trọng số    'Có 3 chỗ thử bỏ đi. Nếu phát sinh lỗi thì phải thêm vào cả 3 chỗ nhé
            cboFunction.Enabled = True
        ElseIf cboIndGroup.SelectedItem = "4. Hạn chế" Or cboIndGroup.SelectedItem = "4. Constraint" Then
            DisableControl()
            RadLim.SelectedIndex = -1
        ElseIf BarCheckItemComposite.Checked = False And (cboIndGroup.SelectedItem = "2. Kinh tế - Xã hội" Or cboIndGroup.SelectedItem = "2. Socio - Economic" Or cboIndGroup.SelectedItem = "3. Môi trường" Or cboIndGroup.SelectedItem = "3. Environment") Then
            DisableControl()
            RadLim.SelectedIndex = -1      'Thử bỏ đi do lỗi check và bỏ check cái Enc xong nó cứ báo lỗi tổng trọng số =1khi chuyển đi chuyển lại cái Tab Trọng số    'Có 3 chỗ thử bỏ đi. Nếu phát sinh lỗi thì phải thêm vào cả 3 chỗ nhé

        ElseIf BarCheckItemComposite.Checked = True And (cboIndGroup.SelectedItem = "2. Kinh tế - Xã hội" Or cboIndGroup.SelectedItem = "2. Socio - Economic" Or cboIndGroup.SelectedItem = "3. Môi trường" Or cboIndGroup.SelectedItem = "3. Environment") And (cboFunction.SelectedItem = ahamKhongham) Then      'Nếu lỗi thì thay bằng e.focusrowhalde
            DisableControl()
            RadLim.SelectedIndex = -1      'Thử bỏ đi do lỗi check và bỏ check cái Enc xong nó cứ báo lỗi tổng trọng số =1khi chuyển đi chuyển lại cái Tab Trọng số    'Có 3 chỗ thử bỏ đi. Nếu phát sinh lỗi thì phải thêm vào cả 3 chỗ nhé
            cboFunction.Enabled = True
        Else
            EnableControl()

        End If

        BtnUpdate.Enabled = False
        If GridControl1.DataSource.getchanges IsNot Nothing Then
            BtnUpdate.Enabled = True
        End If
    End Sub



    Private Sub GridView1_RowCellClick(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles GridView1.RowCellClick
        'TxtMap.Text = GridView1.GetDataRow(GridView1.GetSelectedRows(0))("UsedFunction")
    End Sub

    Private Sub GridView1_RowClick(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles GridView1.RowClick
        ''TxtMap.Text = GridView1.GetDataRow(GridView1.GetSelectedRows(0))("UsedFunction")
        'For i = 0 To PanelControl1.Controls.Count - 1
        'If TypeOf (PanelControl1.Controls(i)) Is DevExpress.XtraEditors.TextEdit Then
        'Dim mytxtbox As DevExpress.XtraEditors.TextEdit = PanelControl1.Controls(i)
        'mytxtbox.SelectionStart = mytxtbox.Text.Length
        ''mytxtbox.ScrollToCaret()
        ''mytxtbox.Refresh()

        'End If
        'Next
    End Sub
    'Private Shared Sub DoRowDoubleClick(ByVal view As GridView, ByVal pt As Point)
    'Dim info As GridHitInfo = view.CalcHitInfo(pt)
    'If info.InRow OrElse info.InRowCell Then
    'Dim colCaption As String
    'If info.Column Is Nothing Then
    'colCaption = "N/A"
    'Else
    'colCaption = info.Column.GetCaption()
    'End If
    ''MessageBox.Show(String.Format("DoubleClick on row: {0}, column: {1}.", info.RowHandle, colCaption))
    'Try
    'Dim imagepath As String = view.GetFocusedDataRow()("Anh").ToString()
    ''Form1.PictureBox1.Image = Image.FromFile(imagepath)
    ''Form1.ShowDialog()
    'Catch ex As Exception

    'End Try
    ''Form1.PictureBox1.Load(imagepath)
    ''Form1.PictureEdit1.EditValue = imagepath
    ''Form1.PanelControl1.ContentImage = System.Drawing.Image.("C:\Users\DangTri\Documents\Visual Studio 2010\Projects\ChuCu\WindowsApplication1\bin\Debug\Images\CuSon be be.jpg")
    'End If
    'End Sub

    'Private Sub cboFunction_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    'GridView1.GetDataRow(GridView1.GetSelectedRows(0))("UsedFunction").tostring = cboFunction.Text
    'GridView1.Focus()   'Để refresh lại gridview. Nếu ko có dòng này thì khi add mới row và chọn 1 function thì ko update ngay lên trên gridview mà đợi khi user click lên gridview thì mới update giá trị 
    'End Sub
#Region "==========Xử lý EditValueChanged, Keyup cho các textbox=========" 'EditValueChanged dùng tốt hơn Keyup, nó tránh được lỗi nhảy lùi con trỏ khi gõ số sau dấu thập phân
    'Private Sub Txt_A_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_A.EditValueChanged
    'GridView1.UpdateCurrentRow()
    'BtnUpdate.Enabled = True
    'End Sub

    'Private Sub Txt_B_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_B.EditValueChanged
    'GridView1.UpdateCurrentRow()
    'BtnUpdate.Enabled = True
    'End Sub
    'Private Sub Txt_C_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_C.EditValueChanged
    'GridView1.UpdateCurrentRow()
    'BtnUpdate.Enabled = True
    'End Sub
    'Private Sub Txt_D_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_D.EditValueChanged
    'GridView1.UpdateCurrentRow()
    'BtnUpdate.Enabled = True
    'End Sub
    Private Sub Txt_A_Keyup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_A.KeyUp
        GridView1.UpdateCurrentRow()
        BtnUpdate.Enabled = True
    End Sub

    Private Sub Txt_B_Keyup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_B.KeyUp
        GridView1.UpdateCurrentRow()
        BtnUpdate.Enabled = True
    End Sub
    Private Sub Txt_C_Keyup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txt_C.KeyUp
        GridView1.UpdateCurrentRow()
        BtnUpdate.Enabled = True
    End Sub
    Private Sub Txt_D_Keyup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_D.KeyUp
        GridView1.UpdateCurrentRow()
        BtnUpdate.Enabled = True
    End Sub
    Private Sub cboIndGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboIndGroup.SelectedIndexChanged
        'Dim oldfocusRowHandle = GridView1.FocusedRowHandle
       
        '===Xử lý disable khi chọn hàm không chuẩn hóa; hạn chế; ....
        'If (cboIndGroup.SelectedItem = "1. Sinh thái" Or cboIndGroup.SelectedItem = "1. Ecology") And (cboFunction.SelectedItem = ahamKhongham) Then       'Phải sử dụng GridView1.GetRowCellValue(GridView1.FocusedRowHandle,"UsedFunction") thay cho cboFunction.SelectedItem bởi vì lúc này không hiểu sao cboFunction vẫn chưa update. Lạ nhất  là event gridview1.focusedrowchanged lại thực hiện sau event cboIndGroup.SelectedIndexChanged này
        Try
            If (cboIndGroup.SelectedItem = "1. Sinh thái" Or cboIndGroup.SelectedItem = "1. Ecology") And (GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "UsedFunction").ToString = ahamKhongham) Then
                DisableControl()
                RadLim.SelectedIndex = -1      'Thử bỏ đi do lỗi check và bỏ check cái Enc xong nó cứ báo lỗi tổng trọng số =1khi chuyển đi chuyển lại cái Tab Trọng số    'Có 3 chỗ thử bỏ đi. Nếu phát sinh lỗi thì phải thêm vào cả 3 chỗ nhé
                cboFunction.Enabled = True
            ElseIf cboIndGroup.SelectedItem = "4. Hạn chế" Or cboIndGroup.SelectedItem = "4. Constraint" Then
                DisableControl()
                RadLim.SelectedIndex = -1
            ElseIf BarCheckItemComposite.Checked = False And (cboIndGroup.SelectedItem = "2. Kinh tế - Xã hội" Or cboIndGroup.SelectedItem = "2. Socio - Economic" Or cboIndGroup.SelectedItem = "3. Môi trường" Or cboIndGroup.SelectedItem = "3. Environment") Then
                DisableControl()
                RadLim.SelectedIndex = -1      'Thử bỏ đi do lỗi check và bỏ check cái Enc xong nó cứ báo lỗi tổng trọng số =1khi chuyển đi chuyển lại cái Tab Trọng số    'Có 3 chỗ thử bỏ đi. Nếu phát sinh lỗi thì phải thêm vào cả 3 chỗ nhé

            ElseIf BarCheckItemComposite.Checked = True And (cboIndGroup.SelectedItem = "2. Kinh tế - Xã hội" Or cboIndGroup.SelectedItem = "2. Socio - Economic" Or cboIndGroup.SelectedItem = "3. Môi trường" Or cboIndGroup.SelectedItem = "3. Environment") And (GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "UsedFunction").ToString = ahamKhongham) Then
                DisableControl()
                RadLim.SelectedIndex = -1      'Thử bỏ đi do lỗi check và bỏ check cái Enc xong nó cứ báo lỗi tổng trọng số =1khi chuyển đi chuyển lại cái Tab Trọng số    'Có 3 chỗ thử bỏ đi. Nếu phát sinh lỗi thì phải thêm vào cả 3 chỗ nhé
                cboFunction.Enabled = True
            Else
                EnableControl()

            End If
        Catch ex As Exception

        End Try


  

        'Đặt lại focused Row
        For i As Int16 = 0 To GridView1.RowCount - 1
            If GridView1.GetRowCellValue(i, "IndName") = txtInd.Text Then
                GridView1.FocusedRowHandle = i
            End If
        Next
        GridView1.UpdateCurrentRow()
    End Sub

    Private Sub DisableControl()
        Txt_A.Text = ""
        Txt_B.Text = ""
        Txt_C.Text = ""
        txt_D.Text = ""

        TxtMap.Text = ""


        RadLim.Enabled = False

        cboFunction.Enabled = False
        cboFunction.SelectedIndex = 0

        TxtMap.Enabled = False
        LabelControl10.Enabled = False
        BtnBrowse1.Enabled = False

        TxtMapchuanhoa.Enabled = True
        BtnBrowse2.Enabled = True
        LabelControl12.Enabled = True


        For Each c In PanelControl6.Controls
            c.enabled = False
        Next
    End Sub

    Private Sub EnableControl()


        cboFunction.Enabled = True
        RadLim.Enabled = True
        'If RadLim.SelectedIndex = -1 Then
        'RadLim.SelectedIndex = 0
        'End If
        If cboFunction.SelectedItem <> ahamKhongham Then
            GridView1.UpdateCurrentRow()
            TxtMapchuanhoa.Enabled = True
            BtnBrowse2.Enabled = True
            LabelControl12.Enabled = True
            PanelControl14.Enabled = True

            BtnBrowse1.Enabled = True
            LabelControl8.Enabled = True
            cboFunction.Enabled = True
            LabelControl10.Enabled = True
            TxtMap.Enabled = True
            RadLim.Enabled = True

            'If cboFunction.SelectedIndex = -1 Then
            'cboFunction.SelectedIndex = 0
            'End If
            'For Each c In PanelControl14.Controls
            'c.enabled = True
            'Next
            'addcontrol(cboFunction.SelectedItem)
            For Each c In PanelControl6.Controls
                c.enabled = True
            Next
        End If






        'If cboIndGroup.SelectedItem = "2. Kinh tế - Xã hội" Or cboIndGroup.SelectedItem = "2. Socio - Economic" Or cboIndGroup.SelectedItem = "3. Môi trường" Or cboIndGroup.SelectedItem = "3. Environment" Then
        'Txt_A.Text = ""
        'Txt_B.Text = ""
        'Txt_C.Text = ""
        'txt_D.Text = ""
        'TxtS1.Text = ""
        'TxtS2.Text = ""
        'TxtS3.Text = ""
        'TxtN.Text = ""

        ''TxtMap.Text = ""
        ''TxtMap.Enabled = BarCheckItemComposite.Checked
        ''BtnBrowse1.Enabled = BarCheckItemComposite.Checked
        ''LabelControl10.Enabled = BarCheckItemComposite.Checked
        'Dim mapPath As String = GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "Map").ToString
        'If BarCheckItemComposite.Checked = False Then
        'TxtMapchuanhoa.Text = mapPath   'TxtMap.Text
        'Else
        'If TxtMapchuanhoa.Text = "" Then
        'Try
        'TxtMapchuanhoa.Text = mapPath.Substring(0, mapPath.Length - 4) + "_ch.tif"
        'Catch ex As Exception
        'TxtMapchuanhoa.Text = ""
        'End Try
        'End If


        'End If
        'TxtMapchuanhoa.Enabled = BarCheckItemComposite.Checked
        'BtnBrowse2.Enabled = BarCheckItemComposite.Checked
        'LabelControl12.Enabled = BarCheckItemComposite.Checked
        ''PanelControl14.Enabled = BarCheckItemComposite.Checked

        'LabelControl8.Enabled = BarCheckItemComposite.Checked
        'cboFunction.Enabled = BarCheckItemComposite.Checked
        'RadLim.Enabled = BarCheckItemComposite.Checked
        'If RadLim.Enabled = False Then
        'RadLim.SelectedIndex = -1
        'cboFunction.SelectedIndex = 0
        'End If


        'TxtMap.Enabled = True
        'LabelControl10.Enabled = True
        'BtnBrowse1.Enabled = True
        ''PanelControl6.Visible = False
        ''PanelControl14.Visible = False
        ''For Each c In PanelControl14.Controls
        ''c.enabled = BarCheckItemComposite.Checked
        ''Next
        'For Each c In PanelControl6.Controls
        'c.enabled = BarCheckItemComposite.Checked
        'Next
        'For Each c In PanelControl14.Controls
        'c.enabled = BarCheckItemComposite.Checked
        'Next
        'GridView1.UpdateCurrentRow()
        ''===============Hạn chế
        ''=======
        'ElseIf cboIndGroup.SelectedItem = "4. Hạn chế" Or cboIndGroup.SelectedItem = "4. Constraint" Then

        ''btnAdd.Location = New System.Drawing.Point(615, 8)
        ''BtnReload.Location = New System.Drawing.Point(615, 33)
        ''btnDelete.Location = New System.Drawing.Point(615, 58)
        ''BtnUpdate.Location = New System.Drawing.Point(615, 83)
        'Txt_A.Text = ""
        'Txt_B.Text = ""
        'Txt_C.Text = ""
        'txt_D.Text = ""
        'TxtS1.Text = ""
        'TxtS2.Text = ""
        'TxtS3.Text = ""
        'TxtN.Text = ""
        'TxtMap.Text = ""

        ''BtnBrowse1.Enabled = False
        'LabelControl8.Enabled = False
        'cboFunction.Enabled = False
        'RadLim.Enabled = False
        'RadLim.SelectedIndex = -1
        'cboFunction.SelectedIndex = 0
        ''LabelControl10.Enabled = False
        ''TxtMap.Enabled = BarCheckItemComposite.Checked
        'TxtMap.Enabled = False
        'LabelControl10.Enabled = False
        'BtnBrowse1.Enabled = False

        'TxtMapchuanhoa.Enabled = True
        'BtnBrowse2.Enabled = True
        'LabelControl12.Enabled = True
        ''PanelControl14.Enabled = BarCheckItemComposite.Checked
        ''PanelControl6.Visible = False
        ''PanelControl14.Visible = False
        'For Each c In PanelControl14.Controls
        'c.enabled = False
        'Next
        'For Each c In PanelControl6.Controls
        'c.enabled = False
        'Next
        'GridView1.UpdateCurrentRow()
        'Else
        'GridView1.UpdateCurrentRow()
        'TxtMapchuanhoa.Enabled = True
        'BtnBrowse2.Enabled = True
        'LabelControl12.Enabled = True
        'PanelControl14.Enabled = True

        'BtnBrowse1.Enabled = True
        'LabelControl8.Enabled = True
        'cboFunction.Enabled = True
        'LabelControl10.Enabled = True
        'TxtMap.Enabled = True
        'RadLim.Enabled = True
        'If RadLim.SelectedIndex = -1 Then
        'RadLim.SelectedIndex = 0
        'End If
        ''If cboFunction.SelectedIndex = -1 Then
        ''cboFunction.SelectedIndex = 0
        ''End If
        'For Each c In PanelControl14.Controls
        'c.enabled = True
        'Next
        ''addcontrol(cboFunction.SelectedItem)
        'For Each c In PanelControl6.Controls
        'c.enabled = True
        'Next
        'End If
        ''End If

        'If cboFunction.SelectedItem = ahamKhongham Then
        'Txt_A.Text = ""
        'Txt_B.Text = ""
        'Txt_C.Text = ""
        'txt_D.Text = ""
        'TxtS1.Text = ""
        'TxtS2.Text = ""
        'TxtS3.Text = ""
        'TxtN.Text = ""
        'TxtMap.Text = ""

        ''BtnBrowse1.Enabled = False
        ''LabelControl8.Enabled = False
        ''cboFunction.Enabled = False
        ''RadLim.Enabled = False
        ''RadLim.SelectedIndex = -1
        ''cboFunction.SelectedIndex = -1
        ''LabelControl10.Enabled = False
        ''TxtMap.Enabled = BarCheckItemComposite.Checked
        'TxtMap.Enabled = False
        'LabelControl10.Enabled = False
        'BtnBrowse1.Enabled = False

        'TxtMapchuanhoa.Enabled = True
        'BtnBrowse2.Enabled = True
        'LabelControl12.Enabled = True
        ''PanelControl14.Enabled = BarCheckItemComposite.Checked
        ''PanelControl6.Visible = False
        ''PanelControl14.Visible = False
        'For Each c In PanelControl14.Controls
        'c.enabled = False
        'Next
        'For Each c In PanelControl6.Controls
        'c.enabled = False
        'Next
        'End If

        GridView1.UpdateCurrentRow()
        BtnUpdate.Enabled = True
        'Đặt lại focused Row
        'For i As Int16 = 0 To GridView1.RowCount - 1
        'If GridView1.GetRowCellValue(i, "IndName") = txtInd.Text Then
        'GridView1.FocusedRowHandle = i
        'End If
        'Next
    End Sub

    'Private Sub txtIndGroup_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtIndGroup.KeyUp
    'GridView1.UpdateCurrentRow()
    'End Sub
    Private Sub txtInd_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtInd.KeyUp
        GridView1.UpdateCurrentRow()
        BtnUpdate.Enabled = True
    End Sub

    Private Sub txtInd_Leave(sender As Object, e As EventArgs) Handles txtInd.Leave

        '=======Validate Tên chỉ tiêu
        Dim tmp As Integer = 0
        For i As Int16 = 0 To GridView1.DataRowCount - 1

            If GridView1.GetDataRow(i)("IndName").ToString = "" Then
                If Me.ActiveControl.Name.ToString = "XtraTabControl1" Then
                    'Nếu là Tab2 thì sẽ tự động quay về

                    Exit Sub

                End If
                MessageBox.Show(Tenchitieu, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                'XtraTabControl1.SelectedTabPageIndex = 0
                'GridView1.SelectRow(i)	Thằng dưới hiệu quả hơn
                'GridView1.FocusedRowHandle = i
                txtInd.Focus()
                Return

            End If

            If txtInd.Text = GridView1.GetDataRow(i)("IndName").ToString Then
                tmp = tmp + 1
                If tmp = 2 Then     'Phải đếm tới khi = 2 vì nếu = 1 thì luôn luôn có chính bản thân nó trong gridview1
                    MessageBox.Show(chitieutontai, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    'GridView1.FocusedRowHandle = i
                    txtInd.Focus()
                End If

            End If


        Next
    End Sub
    Private Sub txtInd_LostFocus(sender As Object, e As EventArgs) Handles txtInd.LostFocus

    End Sub
    'Private Sub Txt_A_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_A.KeyUp
    ''Txt_A1.Text = Txt_A.Text

    ''GridView1.UpdateCurrentRow()
    ''Txt_A.ScrollToCaret()
    ''Txt_A.Refresh()
    'End Sub

    'Private Sub Txt_B_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_B.KeyUp
    ''Txt_B1.Text = Txt_B.Text
    ''GridView1.UpdateCurrentRow()
    'End Sub

    'Private Sub Txt_C_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Txt_C.KeyUp
    ''Txt_C1.Text = Txt_C.Text
    ''GridView1.UpdateCurrentRow()
    'End Sub




    'Private Sub txtNO1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNO1.KeyUp
    'GridView1.UpdateCurrentRow()
    ''GridView1.Focus()
    ''txtNO1.Focus()
    ''GridControl1.Refresh()
    ''GridControl1.RefreshDataSource()
    ''MessageBox.Show("")
    'End Sub
    'Private Sub txtNO2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNO2.KeyUp
    'GridView1.UpdateCurrentRow()
    'End Sub
    'Private Sub txtS1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtToiUu1.KeyUp
    'GridView1.UpdateCurrentRow()
    'End Sub
    'Private Sub txtS2_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtToiUu2.KeyUp
    'GridView1.UpdateCurrentRow()
    'End Sub
    Private Sub txtMap_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtMap.KeyUp
        GridView1.UpdateCurrentRow()
    End Sub
    Private Sub txtMapchuanhoa_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtMapchuanhoa.KeyUp
        GridView1.UpdateCurrentRow()
    End Sub
    Private Sub TxtWeight_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtWeight.KeyUp
        GridView1.UpdateCurrentRow()

    End Sub
#End Region

#Region "===========Thêm, xóa sửa========"
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click

        'If GridView1.GetDataRow(GridView1.GetSelectedRows(0))("IndName").ToString = "" Then
        '	MessageBox.Show(Tenchitieu, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '	txtInd.Focus()
        '	Return
        'End If
        If txtInd.Text = "" Or txtInd.Text = " " Then
            txtInd.Focus()
            Return
        End If
        '==========
        If GridView1.GetDataRow(GridView1.GetSelectedRows(0))("UsedFunction").ToString = aHamhinhthang Then
            If GridView1.GetDataRow(GridView1.GetSelectedRows(0))("b").ToString = "" Or GridView1.GetDataRow(GridView1.GetSelectedRows(0))("c").ToString = "" Then
                MessageBox.Show(giatribc + vbNewLine + trogiuphinhthang, loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                PicCheckHam.Image = My.Resources.Resources.cancel_32x32
                PicCheckHam.ToolTip = giatribc + vbNewLine + trogiuphinhthang
                GridView1.FocusedRowHandle = GridView1.GetSelectedRows(0)
                XtraTabControl1.SelectedTabPageIndex = 0
                Txt_B.Focus()
                Return
            Else
                PicCheckHam.Image = My.Resources.Resources.apply_32x32
                PicCheckHam.ToolTip = "OK"
            End If

            'End If
        ElseIf GridView1.GetDataRow(GridView1.GetSelectedRows(0))("UsedFunction").ToString = aHamkandel Then
            If GridView1.GetDataRow(GridView1.GetSelectedRows(0))("a").ToString = "" And GridView1.GetDataRow(GridView1.GetSelectedRows(0))("b").ToString = "" Then
                MessageBox.Show(giatriab + vbNewLine + trogiupkandel, loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                PicCheckHam.Image = My.Resources.Resources.cancel_32x32
                PicCheckHam.ToolTip = giatriab + vbNewLine + trogiupkandel
                GridView1.FocusedRowHandle = GridView1.GetSelectedRows(0)
                Txt_A.Focus()
                XtraTabControl1.SelectedTabPageIndex = 0
                Return
            Else
                PicCheckHam.Image = My.Resources.Resources.apply_32x32
                PicCheckHam.ToolTip = "OK"
            End If
            'If Txt_A.Text = "" Then
            'Txt_A.Text = "-99999"
            'End If
            'If Txt_B.Text = "" Then
            'Txt_B.Text = "99999"
            'End If
            Txt_C.Text = ""
            txt_D.Text = ""
        ElseIf GridView1.GetDataRow(GridView1.GetSelectedRows(0))("UsedFunction").ToString = aHams1 Or GridView1.GetDataRow(GridView1.GetSelectedRows(0))("UsedFunction").ToString = aHams1 Then
            If GridView1.GetDataRow(GridView1.GetSelectedRows(0))("a").ToString = "" Or GridView1.GetDataRow(GridView1.GetSelectedRows(0))("b").ToString = "" Then
                MessageBox.Show(giatriab + vbNewLine + trogiupS, loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                PicCheckHam.Image = My.Resources.Resources.cancel_32x32
                PicCheckHam.ToolTip = giatriab + vbNewLine + trogiupS
                GridView1.FocusedRowHandle = GridView1.GetSelectedRows(0)
                Txt_A.Focus()
                XtraTabControl1.SelectedTabPageIndex = 0
                Return
                'ElseIf Txt_B.Text = "" Then
                'Txt_B.Text = ((Convert.ToSingle(Txt_A.Text) + Convert.ToSingle(Txt_C.Text)) / 2).ToString
                'Else
                'PicCheckHam.Image = My.Resources.Resources.apply_32x32
                'PicCheckHam.ToolTip = "OK"
            End If
        End If

        '===========================
        '===========================
        Gridview1_Connect.AddRow_with_ID_col(GridControl1, "ID", "MainData")
        'GridView1.AddNewRow()
        '''Dim dt As DataTable = GridView1.DataSource
        '''dt.Rows.Add()
        ''GridView1.AddNewRow()
        '''GridView1.OptionsSelection.EnableAppearanceFocusedCell = True
        '''GridView1.OptionsBehavior.Editable = True
        '''Dim MaxID As Integer = Gridview1_Connect.DtFromQry("Select ID from MainData").Rows.Count - 1
        ''Dim Srcdt As DataTable = GridView1.DataSource	'.DtFromQry("Select ID from MainData")	'toàn bộ dữ liệu ID từ Maindata			.Rows.Count - 1
        ''Srcdt.DefaultView.Sort = "ID ASC"
        ''Dim MaxID As Integer = Srcdt.DefaultView.Item(Srcdt.Rows.Count - 1).Item("ID")
        ''GridView1.GetDataRow(GridView1.GetSelectedRows(0))("ID") = MaxID + 1

        GridView1.GetDataRow(GridView1.GetSelectedRows(0))("Obj") = ListBoxControl1.SelectedValue.ToString
        GridView1.GetDataRow(GridView1.GetSelectedRows(0))("IndName") = ""
        txtInd.Focus()
        cboIndGroup.SelectedIndex = 0
        cboFunction.SelectedIndex = 0
        RadLim.SelectedIndex = 0
        'sender.enabled = False
        'btnAdd.ToolTip = "Phải lưu hoặc bỏ thay đổi trước khi thêm chỉ tiêu mới"
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        'Gridview1_Reload()  'Phải reload trước khi save, nếu ko sẽ gặp lỗi
        If GridView1.RowCount = 1 Then
            Return
        End If
        Dim yesno = MessageBox.Show(Chacchanxoa, thongbao, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If yesno = Windows.Forms.DialogResult.Yes Then
            Try

                'GridView1.DeleteRow(GridView1.FocusedRowHandle)
                GridView1.DeleteSelectedRows()
                Dim asd As String
                asd = ""
            Catch ex As Exception
                Dim asd As String
                asd = ""
            End Try

            GridView1.Columns.ColumnByFieldName("ID").Visible = True
            'GridView1.DeleteSelectedRows()
            '=====================2 lệnh sau rất quan trọng, nếu ko sẽ ko update focused row
            GridView1.PostEditor()
            GridView1.UpdateCurrentRow()
            '=====================
            Gridview1_Connect.Update_CSDL(GridControl1, "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
            'Gridview1_Reload()
            'MessageBox.Show("Đã xóa dữ liệu thành công", thongbao)
            Status1.Caption = Xoathanhcong
            Dim a = sender.focus
            capnhatlaiTab2_3 = True
            GridView1.Columns.ColumnByFieldName("ID").Visible = False
        End If

    End Sub
    Private Sub ClearStatus1()
        Status1.Caption = ""
    End Sub
    Private Sub BtnUpdate_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        'If GridControl1.DataSource.GetChanges() Is Nothing Then     'nếu ko có thay đổi gì thì thôi luôn
        'Return
        'End If
        Dim dt_alldata As DataTable = Gridview1_Connect.DtFromQry("Select * from Maindata")
        'If (dt_alldata.GetChanges() Is Nothing) Then   'Bỏ comment là sẽ bị getchanges = nothing trong 1 số trường hợp đã change dữ liệu
        ''MessageBox.Show("ád")
        'Return
        'End If
        '========tạo autocomplete for txtIndname
        Dim autocompleteString As New AutoCompleteStringCollection

        For i As Integer = 0 To dt_alldata.Rows.Count - 1
            Try
                autocompleteString.Add(dt_alldata.Rows(i)("Indname"))
            Catch ex As Exception

            End Try

        Next


        txtInd.MaskBox.AutoCompleteCustomSource = autocompleteString    '((From row In IndicatorTable.Rows.Cast(Of DataRow)() Select CStr(row("Desc"))).ToArray())
        txtInd.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtInd.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource
        '=============
        'Dim tmp As Int16
        'For i As Int16 = 0 To GridView1.DataRowCount - 1

        '	If GridView1.GetDataRow(i)("IndName").ToString = "" Then
        '		MessageBox.Show(Tenchitieu, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '		'XtraTabControl1.SelectedTabPageIndex = 0
        '		txtInd.Focus()
        '		If GridControl1.DataSource.rows.count = 1 Then	'Nếu chỉ có 1 dòng thì thoát For và tiếp tục update. 
        '			Exit For
        '		Else	'Nếu có nhiều dòng thì thoát luôn, ko update nữa
        '			Return
        '		End If

        '	End If
        '	If GridView1.GetDataRow(i)("IndName") IsNot System.DBNull.Value Then
        '		If Not String.IsNullOrEmpty(GridView1.GetDataRow(i)("IndName")) Then
        '			If txtInd.Text = GridView1.GetDataRow(i)("IndName") Then
        '				tmp = tmp + 1
        '				If tmp = 2 Then		'Phải đếm tới khi = 2 vì nếu = 1 thì luôn luôn có chính bản thân nó trong gridview1
        '					MessageBox.Show(chitieutontai, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '					Return
        '				End If

        '			End If
        '		End If
        '	End If

        'Next
        'If TxtWeight.Text = "" Then
        'MessageBox.Show("Hãy nhập giá trị trọng số cho chỉ tiêu", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        'Return
        'End If
        'If TxtMap.Text = "" And GridView1.RowCount > 0 Then
        'MessageBox.Show("Hãy nhập bản đồ chỉ tiêu", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        'Return
        'End If
        'If TxtMapchuanhoa.Text = "" And GridView1.RowCount > 0 Then
        'MessageBox.Show("Hãy nhập bản đồ chuẩn hóa", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        'Return
        'End If
        '''''''''''TxtFunction.Text = FunctionDetect1()

        If cboFunction.SelectedItem = aHamhinhthang Then
            If Txt_B.Text = "" Or Txt_C.Text = "" Then
                MessageBox.Show(giatribc + vbNewLine + trogiuphinhthang, loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                PicCheckHam.Image = My.Resources.Resources.cancel_32x32
                PicCheckHam.ToolTip = giatribc + vbNewLine + trogiuphinhthang

                'XtraTabControl1.SelectedTabPageIndex = 0
                Txt_B.Focus()
                Return
            Else
                PicCheckHam.Image = My.Resources.Resources.apply_32x32
                PicCheckHam.ToolTip = "OK"
            End If

            'If txt_D.Text = "" Then
            'txt_D.Text = "99999"
            'End If
            'If Txt_A.Text = "" Then
            'Txt_A.Text = "-99999"
            'End If
        ElseIf cboFunction.SelectedItem = aHamkandel Then
            If Txt_A.Text = "" And Txt_B.Text = "" Then
                MessageBox.Show(giatriab + vbNewLine + trogiupkandel, loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                PicCheckHam.Image = My.Resources.Resources.cancel_32x32
                PicCheckHam.ToolTip = giatriab + vbNewLine + trogiupkandel
                Txt_A.Focus()
                'XtraTabControl1.SelectedTabPageIndex = 0
                Return
            Else
                PicCheckHam.Image = My.Resources.Resources.apply_32x32
                PicCheckHam.ToolTip = "OK"
            End If
            'If Txt_A.Text = "" Then
            'Txt_A.Text = "-99999"
            'End If
            'If Txt_B.Text = "" Then
            'Txt_B.Text = "99999"
            'End If
            Txt_C.Text = ""
            txt_D.Text = ""
        ElseIf cboFunction.SelectedItem = aHams1 Or cboFunction.SelectedItem = aHams2 Then
            If Txt_A.Text = "" Or Txt_B.Text = "" Then
                MessageBox.Show(giatriab + vbNewLine + trogiupS, loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                PicCheckHam.Image = My.Resources.Resources.cancel_32x32
                PicCheckHam.ToolTip = giatriab + vbNewLine + trogiupS
                Txt_A.Focus()
                'XtraTabControl1.SelectedTabPageIndex = 0
                Return
                'ElseIf Txt_B.Text = "" Then
                'Txt_B.Text = ((Convert.ToSingle(Txt_A.Text) + Convert.ToSingle(Txt_C.Text)) / 2).ToString
                'Else
                'PicCheckHam.Image = My.Resources.Resources.apply_32x32
                'PicCheckHam.ToolTip = "OK"
                Txt_C.Text = ""
                txt_D.Text = ""
            End If
        End If

        GridView1.UpdateCurrentRow()


        ''''''''If TxtFunction.Text = "" Then
        ''''''''Exit Sub
        ''''''''End If
        'Dim yesnocancel = MessageBox.Show(Chacchanluu, thongbao, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
        'If yesnocancel = Windows.Forms.DialogResult.Cancel Then
        ''XtraTabControl1.SelectedTabPage = XtraTabPage1
        'Return
        'ElseIf yesnocancel = Windows.Forms.DialogResult.No Then
        'BtnReload_Click(Nothing, Nothing)
        'Return
        'End If


        'Dim yesno = MessageBox.Show(Chacchanluu, thongbao, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        'If yesno = Windows.Forms.DialogResult.No Then
        '	'XtraTabControl1.SelectedTabPage = XtraTabPage1
        '	Return
        'End If
        Try
            '=====================2 lệnh sau rất quan trọng, nếu ko sẽ ko update focused row
            'GridView1.PostEditor() 'đã đặt luôn vào mycon.update_CSDL
            'GridView1.UpdateCurrentRow()
            '=====================
            Dim success As Integer = Gridview1_Connect.Update_CSDL(GridControl1, "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
            'Gridview1_Reload()
            If success > 0 Then
                'MessageBox.Show("Đã lưu dữ liệu thành công", thongbao)
                BtnUpdate.Focus()
                Status1.Caption = Luuthanhcong
                If sender.focus = False Then
                    Status1.Caption = ""
                End If
                BtnReload.Enabled = True
                btnDelete.Enabled = True

                btnAdd.ToolTip = Themchitieu
            End If

        Catch ex As Exception

        End Try
        capnhatlaiTab2_3 = True      'Sau khi sửa chỉ tiêu thì phải cập nhật lại Tabpage2 và 3
        BtnUpdate.Enabled = False
    End Sub
    Private Function FunctionDetect1() As String
        ''Dim a As String() = {txt_D.Text.Replace(",", ""), Txt_A1.Text.Replace(",", ""), Txt_A.Text.Replace(",", ""), Txt_B1.Text.Replace(",", ""), Txt_B.Text.Replace(",", ""), Txt_C1.Text.Replace(",", ""), Txt_C.Text.Replace(",", ""), txtToiUu2.Text.Replace(",", "")}
        '''======TH1: Không điền 1 txtbox nào => Hàm theo loại
        ''If a(0) = "" And a(1) = "" And a(2) = "" And a(3) = "" And a(4) = "" And a(5) = "" And a(6) = "" And a(7) = "" Then
        ''FunctionDetect1 = "Hàm theo loại"


        '''=====2 ô Ko TNghi <>"" thì => Hàm Hình Thang
        ''ElseIf a(0) <> "" And a(1) <> "" Then       '

        '''===Trường hợp là hàm hình thang thì: KTN1 < TU1 < TU2 < KTN2
        ''If a(6) <> "" And a(7) <> "" Then


        ''If Convert.ToSingle(a(0)) < Convert.ToSingle(a(6)) And Convert.ToSingle(a(6)) < Convert.ToSingle(a(7)) And Convert.ToSingle(a(7)) < Convert.ToSingle(a(1)) Then   'Lớn hơn S nhỏ và nhỏ hơn S lớn
        '''===validate txtTN1, TxtTN2, TxtRTN1, TxtRTN2 ; Trường hợp là hàm hình thang thì 4 ô này phải rỗng
        '''==Rảnh thì viết thêm code để validate. Tạm thời auto set = ""
        ''Txt_A.Text = ""
        ''Txt_B1.Text = ""
        ''Txt_B.Text = ""
        ''Txt_C1.Text = ""
        ''Return aHamhinhthang

        ''Else
        ''MessageBox.Show("Nếu bạn nhập cả 2 giá trị không thích nghi có nghĩa là chỉ tiêu bạn nhập sẽ sử dụng hàm hình thang." + vbNewLine + "Khi này, phải thỏa mãn điều kiện: KTN1 < TU1 < TU2 < KTN2", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End If
        ''Else
        ''MessageBox.Show("Nếu bạn nhập cả 2 giá trị không thích nghi có nghĩa là chỉ tiêu bạn nhập sẽ sử dụng hàm hình thang." + vbNewLine + "Khi này, phải thỏa mãn điều kiện: KTN1 < TU1 < TU2 < KTN2", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End If

        '''=====2 ô Ko TNghi ="" và 2 ô tối ưu được điền thì => Hàm Kandel
        '''''ElseIf a(0) = "" And a(1) = "" And a(6) <> "" And a(7) <> "" Then
        '''''If Convert.ToSingle(a(6)) < Convert.ToSingle(a(7)) Then
        ''''''===validate txtTN1, TxtTN2, TxtRTN1, TxtRTN2 ; Trường hợp là hàm hình thang thì 4 ô này phải rỗng
        ''''''==Rảnh thì viết thêm code để validate. Tạm thời auto set = ""
        '''''TxtTN1.Text = ""
        '''''TxtTN2.Text = ""
        '''''TxtRTN1.Text = ""
        '''''TxtRTN2.Text = ""
        '''''Return aHamKandel
        '''''Else
        '''''MessageBox.Show("Nếu bạn nhập cả 2 giá trị tối ưu có nghĩa là chỉ tiêu bạn nhập sẽ sử dụng hàm Kandel." + vbNewLine + "Khi này, phải thỏa mãn điều kiện: TU1 < TU2", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '''''End If
        '''=====2 ô Ko TNghi ="" và 2 ô tối ưu được điền thì => Hàm Kandel
        ''ElseIf a(0) = "" And a(1) = "" And a(6) <> "" And a(7) <> "" Then
        ''If Convert.ToSingle(a(6)) < Convert.ToSingle(a(7)) Then
        '''===validate txtTN1, TxtTN2, TxtRTN1, TxtRTN2 ; Trường hợp là hàm hình thang thì 4 ô này phải rỗng
        '''==Rảnh thì viết thêm code để validate. Tạm thời auto set = ""
        ''Txt_A.Text = ""
        ''Txt_B1.Text = ""
        ''Txt_B.Text = ""
        ''Txt_C1.Text = ""
        ''Return aHamKandel
        ''Else
        ''MessageBox.Show("Nếu bạn nhập cả 2 giá trị tối ưu có nghĩa là chỉ tiêu bạn nhập sẽ sử dụng hàm Kandel." + vbNewLine + "Khi này, phải thỏa mãn điều kiện: TU1 < TU2", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End If

        '''=====2 ô Ko TNghi ="" và 1 ô tối ưu  S1 được điền thì => Hàm Kandel 1  (TH X>b2)
        ''ElseIf a(0) = "" And a(1) = "" And a(6) <> "" And a(7) = "" Then
        '''If Convert.ToSingle(a(6)) < Convert.ToSingle(a(7)) Then
        '''===validate txtTN1, TxtTN2, TxtRTN1, TxtRTN2 ; Trường hợp là hàm hình thang thì 4 ô này phải rỗng
        '''==Rảnh thì viết thêm code để validate. Tạm thời auto set = ""
        ''Txt_A.Text = ""
        ''Txt_B1.Text = ""
        ''Txt_B.Text = ""
        ''Txt_C1.Text = ""
        ''Return "Hàm Kandel 1"
        '''Else
        '''MessageBox.Show("Nếu bạn nhập cả 2 giá trị tối ưu có nghĩa là chỉ tiêu bạn nhập sẽ sử dụng hàm Kandel." + vbNewLine + "Khi này, phải thỏa mãn điều kiện: TU1 < TU2", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '''End If
        '''=====2 ô Ko TNghi ="" và 1 ô tối ưu S2 được điền thì => Hàm Kandel 2  (TH X<b1)
        ''ElseIf a(0) = "" And a(1) = "" And a(6) = "" And a(7) <> "" Then
        '''If Convert.ToSingle(a(6)) < Convert.ToSingle(a(7)) Then
        '''===validate txtTN1, TxtTN2, TxtRTN1, TxtRTN2 ; Trường hợp là hàm hình thang thì 4 ô này phải rỗng
        '''==Rảnh thì viết thêm code để validate. Tạm thời auto set = ""
        ''Txt_A.Text = ""
        ''Txt_B1.Text = ""
        ''Txt_B.Text = ""
        ''Txt_C1.Text = ""
        ''Return "Hàm Kandel 2"
        '''==============Nếu a(0) <>"" và a(1) = "" => a(2) = a(0) <a(3); a4=a3 <a5; a6=a5;a7 = +vô cùng  => hàm S1
        ''ElseIf a(0) <> "" And a(1) = "" Then
        ''If Txt_B1.Text <> "" And Txt_C1.Text <> "" Then
        ''If Convert.ToSingle(a(0)) < Convert.ToSingle(a(3)) And Convert.ToSingle(a(3)) < Convert.ToSingle(a(5)) Then
        ''Txt_A.Text = txt_D.Text
        ''Txt_B.Text = Txt_B1.Text
        ''Txt_C.Text = Txt_C1.Text
        ''txtToiUu2.Text = "99999"
        ''Txt_A1.Text = ""
        ''Return aHamS1
        ''Else
        ''MessageBox.Show("Nếu bạn chỉ nhập 1 giá trị không thích nghi (KTN1) có nghĩa là chỉ tiêu bạn nhập sẽ sử dụng hàm S1." + vbNewLine + "Khi này, phải thỏa mãn điều kiện: KTN1 < TN2 < RTN2", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)

        ''End If
        ''Else
        ''MessageBox.Show("Nếu bạn chỉ nhập 1 giá trị không thích nghi (KTN1) có nghĩa là chỉ tiêu bạn nhập sẽ sử dụng hàm S1." + vbNewLine + "Khi này, phải thỏa mãn điều kiện: KTN1 < TN2 < RTN2", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)


        ''End If



        '''==============Nếu a(0) ="" và a(1) <> "" => a(2) <a(3) trong khi a(3) = a(1); a4 <a5 trong khi a5=a2; a7=a4;a6 = (-)vô cùng  => hàm S2
        ''ElseIf a(0) = "" And a(1) <> "" Then
        ''If Txt_A.Text <> "" And Txt_B.Text <> "" Then
        ''If Convert.ToSingle(a(1)) > Convert.ToSingle(a(2)) And Convert.ToSingle(a(2)) > Convert.ToSingle(a(4)) Then
        ''Txt_B1.Text = Txt_A1.Text
        ''Txt_C1.Text = Txt_A.Text
        ''txtToiUu2.Text = Txt_B.Text
        ''Txt_C.Text = "-99999"
        '''txtNO2.Text = "9999"
        ''Return aHamS2
        ''Else
        ''MessageBox.Show("Nếu bạn chỉ nhập 1 giá trị không thích nghi (KTN2) có nghĩa là chỉ tiêu bạn nhập sẽ sử dụng hàm S2." + vbNewLine + "Khi này, phải thỏa mãn điều kiện: KTN2 > TN1 > RTN1", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End If
        ''Else
        ''MessageBox.Show("Nếu bạn chỉ nhập 1 giá trị không thích nghi (KTN2) có nghĩa là chỉ tiêu bạn nhập sẽ sử dụng hàm S2." + vbNewLine + "Khi này, phải thỏa mãn điều kiện: KTN2 > TN1 > RTN1", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)


        ''End If

        ''Else
        ''MessageBox.Show("Số liệu bạn nhập không thể nhận dạng được hàm sử dụng" + vbNewLine + "Hãy xem thêm hướng dẫn để hiểu rõ hơn về các hàm sử dụng để chuẩn hóa dữ liệu bản đồ", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
        ''End If
    End Function

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click

        TxtFunction.Text = FunctionDetect1()
        'txt_D_Validating(Nothing, Nothing)
    End Sub
#Region "Validting textbox"

    Sub checkTxtVal()    '(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt_A.LostFocus
        'If cboFunction.SelectedItem = aHamhinhthang Then
        'If Txt_B.Text = "" Or Txt_C.Text = "" Then
        ''MessageBox.Show("Nhập giá trị B và C" + vbNewLine + "Xem trợ giúp - phần các hàm chuẩn hóa (hàm hình thang) để hiểu rõ hơn về các thông số a, b, c, d", loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ''PicCheckHam.Image = My.Resources.Resources.cancel_32x32
        'Else
        'PicCheckHam.Image = My.Resources.Resources.apply_32x32
        'PicCheckHam.ToolTip = "OK"
        'End If

        'ElseIf cboFunction.SelectedItem = aHamkandel Then
        'If Txt_A.Text = "" And Txt_B.Text = "" Then
        ''MessageBox.Show("Nhập giá trị A và B" + vbNewLine + "Xem trợ giúp - phần các hàm chuẩn hóa (hàm Kandel) để hiểu rõ hơn về các thông số a, b, c, d", loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ''PicCheckHam.Image = My.Resources.Resources.cancel_32x32
        ''PicCheckHam.ToolTip = "Nhập giá trị A và B" + vbNewLine + "Xem trợ giúp - phần các hàm chuẩn hóa (hàm Kandel) để hiểu rõ hơn về các thông số a, b, c, d"
        'Else
        'PicCheckHam.Image = My.Resources.Resources.apply_32x32
        'PicCheckHam.ToolTip = "OK"
        'End If

        'ElseIf cboFunction.SelectedItem = aHams1 Or cboFunction.SelectedItem = aHams2 Then
        ''If Txt_A.Text = "" Or Txt_B.Text = "" Or Txt_C.Text = "" Then

        ''Else
        ''PicCheckHam.Image = My.Resources.Resources.apply_32x32
        ''PicCheckHam.ToolTip = "OK"
        ''End If

        'End If
        ''If cboFunction.SelectedItem = aHamhinhthang Then
        ''e.ErrorText = "Bạn cần nhập giá trị < b < c < d"
        ''ElseIf cboFunction.SelectedItem = aHamKandel Then
        ''e.ErrorText = "Bạn cần nhập giá trị < b" + vbNewLine + "Xem trợ giúp - phần các hàm chuẩn hóa (Kandel) để hiểu rõ hơn về các thông số a, b"
        ''ElseIf cboFunction.SelectedItem = aHamS1 Or cboFunction.SelectedItem = aHamS2 Then
        ''e.ErrorText = "Bạn cần nhập giá trị <b < c" + vbNewLine + "Xem trợ giúp - phần các hàm chuẩn hóa (hàm S) để hiểu rõ hơn về các thông số a, b, c"
        ''End If
    End Sub
    Private Sub txt_A_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Txt_A.Validating
        Try
            If Txt_B.Text <> "" Then
                If Convert.ToSingle(sender.Text.Replace(",", "")) >= Convert.ToSingle(Txt_B.Text.Replace(",", "")) Then
                    'sender.Text = ""
                    e.Cancel = True
                    GridControl1.Enabled = False
                Else
                    GridControl1.Enabled = True

                End If
                'Return
            End If
            If Txt_C.Text <> "" Then
                If Convert.ToSingle(sender.Text.Replace(",", "")) >= Convert.ToSingle(Txt_C.Text.Replace(",", "")) Then
                    e.Cancel = True
                    GridControl1.Enabled = False
                Else
                    GridControl1.Enabled = True

                End If
                'Return
            End If
            If txt_D.Text <> "" Then
                If Convert.ToSingle(sender.Text.Replace(",", "")) >= Convert.ToSingle(txt_D.Text.Replace(",", "")) Then
                    e.Cancel = True
                    GridControl1.Enabled = False
                Else
                    GridControl1.Enabled = True

                End If
                'Return
            End If
        Catch ex As Exception

        End Try

    End Sub
    'Private Sub txt_A1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Txt_A1.Validating
    'If Txt_B.Text <> "" Then
    'If Convert.ToSingle(sender.Text.Replace(",", "")) > Convert.ToSingle(Txt_B.Text.Replace(",", "")) - 1 Then
    'e.Cancel = True
    'End If
    'End If
    'If Txt_C.Text <> "" Then
    'If Convert.ToSingle(sender.Text.Replace(",", "")) > Convert.ToSingle(Txt_C.Text.Replace(",", "")) - 1 Then
    'e.Cancel = True
    'End If
    'End If
    'If txt_D.Text <> "" Then
    'If Convert.ToSingle(sender.Text.Replace(",", "")) > Convert.ToSingle(txt_D.Text.Replace(",", "")) - 1 Then
    'e.Cancel = True
    'End If
    'End If
    'End Sub
    Private Sub txt_B_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Txt_B.Validating
        Try
            If Txt_A.Text <> "" Then
                If Convert.ToSingle(sender.Text.Replace(",", "")) <= Convert.ToSingle(Txt_A.Text.Replace(",", "")) Then
                    e.Cancel = True
                    GridControl1.Enabled = False
                Else
                    GridControl1.Enabled = True

                End If
                'Return
            End If
            If Txt_C.Text <> "" Then
                If Convert.ToSingle(sender.Text.Replace(",", "")) >= Convert.ToSingle(Txt_C.Text.Replace(",", "")) Then
                    e.Cancel = True
                    GridControl1.Enabled = False
                Else
                    GridControl1.Enabled = True

                End If
                'Return
            End If
            If txt_D.Text <> "" Then
                If Convert.ToSingle(sender.Text.Replace(",", "")) >= Convert.ToSingle(txt_D.Text.Replace(",", "")) Then
                    e.Cancel = True
                    GridControl1.Enabled = False
                Else
                    GridControl1.Enabled = True

                End If
                'Return
            End If
        Catch ex As Exception

        End Try
    End Sub
    'Private Sub txt_B1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Txt_B1.Validating
    'If Txt_A.Text <> "" Then
    'If Convert.ToSingle(sender.Text.Replace(",", "")) < Convert.ToSingle(Txt_A.Text.Replace(",", "")) - 1 Then
    'e.Cancel = True
    'End If
    'End If
    'If Txt_C.Text <> "" Then
    'If Convert.ToSingle(sender.Text.Replace(",", "")) > Convert.ToSingle(Txt_C.Text.Replace(",", "")) - 1 Then
    'e.Cancel = True
    'End If
    'End If
    'If txt_D.Text <> "" Then
    'If Convert.ToSingle(sender.Text.Replace(",", "")) > Convert.ToSingle(txt_D.Text.Replace(",", "")) - 1 Then
    'e.Cancel = True
    'End If
    'End If
    'End Sub
    Private Sub txt_C_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Txt_C.Validating
        Try
            If Txt_A.Text <> "" Then
                If Convert.ToSingle(sender.Text.Replace(",", "")) <= Convert.ToSingle(Txt_A.Text.Replace(",", "")) Then
                    e.Cancel = True
                    GridControl1.Enabled = False
                Else
                    GridControl1.Enabled = True

                End If
                'Return
            End If
            If Txt_B.Text <> "" Then
                If Convert.ToSingle(sender.Text.Replace(",", "")) <= Convert.ToSingle(Txt_B.Text.Replace(",", "")) Then
                    e.Cancel = True
                    GridControl1.Enabled = False
                Else
                    GridControl1.Enabled = True

                End If
                'Return
            End If
            If txt_D.Text <> "" Then
                If Convert.ToSingle(sender.Text.Replace(",", "")) >= Convert.ToSingle(txt_D.Text.Replace(",", "")) Then
                    e.Cancel = True
                    GridControl1.Enabled = False
                Else
                    GridControl1.Enabled = True

                End If
                'Return
            End If
        Catch ex As Exception

        End Try
    End Sub
    'Private Sub txt_C1_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Txt_C1.Validating
    'If Txt_A.Text <> "" Then
    'If Convert.ToSingle(sender.Text.Replace(",", "")) < Convert.ToSingle(Txt_A.Text.Replace(",", "")) - 1 Then
    'e.Cancel = True
    'End If
    'End If
    'If Txt_B.Text <> "" Then
    'If Convert.ToSingle(sender.Text.Replace(",", "")) < Convert.ToSingle(Txt_B.Text.Replace(",", "")) - 1 Then
    'e.Cancel = True
    'End If
    'End If
    'If txt_D.Text <> "" Then
    'If Convert.ToSingle(sender.Text.Replace(",", "")) > Convert.ToSingle(txt_D.Text.Replace(",", "")) - 1 Then
    'e.Cancel = True
    'End If
    'End If
    'End Sub
    Private Sub txt_D_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txt_D.Validating
        Try
            If Txt_A.Text <> "" Then
                If Convert.ToSingle(sender.Text.Replace(",", "")) <= Convert.ToSingle(Txt_A.Text.Replace(",", "")) Then
                    e.Cancel = True
                    GridControl1.Enabled = False
                Else
                    GridControl1.Enabled = True

                End If
                'Return
            End If
            If Txt_B.Text <> "" Then
                If Convert.ToSingle(sender.Text.Replace(",", "")) <= Convert.ToSingle(Txt_B.Text.Replace(",", "")) Then
                    e.Cancel = True
                    GridControl1.Enabled = False
                Else
                    GridControl1.Enabled = True

                End If
                'Return
            End If
            If Txt_C.Text <> "" Then
                If Convert.ToSingle(sender.Text.Replace(",", "")) <= Convert.ToSingle(Txt_C.Text.Replace(",", "")) Then
                    e.Cancel = True
                    GridControl1.Enabled = False
                Else
                    GridControl1.Enabled = True

                End If
                'Return
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub txt_a_InvalidValue(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs) Handles Txt_A.InvalidValue

        If cboFunction.SelectedItem = aHamhinhthang Then
            e.ErrorText = txtaInvalidValueHinhthang
        ElseIf cboFunction.SelectedItem = aHamkandel Then
            e.ErrorText = txtaInvalidValueKandel
        ElseIf cboFunction.SelectedItem = aHams1 Or cboFunction.SelectedItem = aHams2 Then
            e.ErrorText = txtaInvalidValueHamS
        End If

    End Sub
    Private Sub txt_b_InvalidValue(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs) Handles Txt_B.InvalidValue
        If cboFunction.SelectedItem = aHamhinhthang Then
            e.ErrorText = txtbInvalidValueHinhthang
        ElseIf cboFunction.SelectedItem = aHamkandel Then
            e.ErrorText = txtbInvalidValueKandel
        ElseIf cboFunction.SelectedItem = aHams1 Or cboFunction.SelectedItem = aHams2 Then
            e.ErrorText = txtbInvalidValueHamS
        End If

    End Sub
    Private Sub txt_c_InvalidValue(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs) Handles Txt_C.InvalidValue
        If cboFunction.SelectedItem = aHamhinhthang Then
            e.ErrorText = txtcInvalidValueHinhthang
        Else 'Lúc này sẽ là hàm S1 hoặc S2 vì Kandel ko cho nhập C
            'trong code mới cũng ko nhập c, chỉ nhập a, b
            'e.ErrorText = "Bạn cần nhập giá trị > b > a" + vbNewLine + "Xem trợ giúp - phần các hàm chuẩn hóa (hàm S) để hiểu rõ hơn về các thông số a, b, c"
        End If

    End Sub
    Private Sub txt_D_InvalidValue(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs) Handles txt_D.InvalidValue

        'Chỉ có D trong trường hợp hàm Hình thang
        e.ErrorText = txtdInvalidValueHinhthang
    End Sub
#End Region

    'Private Sub txtNO1_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNO1.EditValueChanged

    'End Sub
    'Private Function FunctionDetect() As String


    'Dim a As String() = {txt_D.Text.Replace(",", ""), Txt_A1.Text.Replace(",", ""), Txt_C.Text.Replace(",", ""), txtToiUu2.Text.Replace(",", "")}
    ''Dim a As ArrayList = New ArrayList
    ''a.Add(Convert.ToSingle(txtNO1.Text))
    ''a.Add(Convert.ToSingle(txtNO2.Text))
    ''a.Add(Convert.ToSingle(txtS1.Text))
    ''a.Add(Convert.ToSingle(txtS2.Text))


    ''==========1. Trường hợp ko điền cà NO1 và NO2 hoặc S1 và S2
    'If (a(0) = "" And a(1) = "") Then
    'MessageBox.Show("Hãy nhập ""Giá trị không thích nghi""", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
    'txt_D.Focus()
    'Exit Function
    'ElseIf (a(2) = "" And a(3) = "") Then
    'MessageBox.Show("Hãy nhập ""Giá trị tối ưu""", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
    'Txt_C.Focus()
    'Exit Function

    ''==========2. Trường hợp chỉ điền 1 giá trị NO1 hoặc NO2 => Xét S1, S2. Nếu No1 điền thì s2 phải điền (S1 phải ="") và ngược lại
    'ElseIf a(0) = "" And a(1) <> "" Then
    'If (a(2) <> "" And a(3) = "") Then
    'If Convert.ToSingle(a(1)) < Convert.ToSingle(a(2)) Then
    'Return "Hàm chữ S"
    'Else
    'MessageBox.Show("Nếu giá trị không thích nghi thứ nhất không được điền thì giá trị không thích nghi thứ hai phải nhỏ hơn giá trị tối ưu thứ nhất!", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
    'End If

    'Else
    'MessageBox.Show("Nếu giá trị không thích nghi thứ nhất không được điền thì phải nhập ""Giá trị tối ưu thứ nhất""" + vbNewLine + "Giá trị tối ưu thứ hai sẽ bị xóa", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
    'txtToiUu2.Text = ""
    'End If
    'ElseIf a(0) <> "" And a(1) = "" Then
    'If (a(2) = "" And a(3) <> "") Then
    'If Convert.ToSingle(a(0)) > Convert.ToSingle(a(3)) Then
    'Return "Hàm chữ S1"
    'Else
    'MessageBox.Show("Nếu giá trị không thích nghi thứ hai không được điền thì giá trị không thích nghi nhất phải lớn hơn giá trị tối ưu thứ hai!", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
    'End If
    'Else
    'MessageBox.Show("Nếu giá trị không thích nghi thứ hai không được điền thì phải nhập ""Giá trị tối ưu thứ hai""" + vbNewLine + "Giá trị tối ưu thứ nhất sẽ bị xóa", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
    'Txt_C.Text = ""
    'End If

    ''==========3. Trường hợp đã điền cả 2 giá trị NO1 và NO2 => thì cả 2 giá trị của S1 và S2 cũng phải được điền. Xét giá trị S1 và S2 để xác định hàm Kandel hay hàm hình thang
    'ElseIf a(0) <> "" And a(1) <> "" Then
    'If (a(2) = "" Or a(3) = "") Then    'Không điền 1 giá trị S1 hoặc S2
    'MessageBox.Show("Hãy nhập ""Giá trị tối ưu""", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
    'Return Nothing
    'Else    'Trường hợp điền cả S1 và S2
    'If Convert.ToSingle(a(0)) < Convert.ToSingle(a(1)) Then       'Lớn hơn N nhỏ và nhỏ hơn N lớn     'N lớn hay N nhỏ xác định bởi dấu > hay < trong lệnh If này. Còn trên giao diện thì luôn lớn hơn N0 và Nhỏ hơn N1
    ''=> Nếu là dấu < có nghĩa là N0 < N1; => N0 là số nhỏ, N1 là số lớn => Lớn hơn N nhỏ và nhỏ hơn N lớn.;. Nếu dấu > có nghĩa là N0 > N1 (N0 sẽ là N lớn và N1 sẽ là N nhỏ) và nghĩa là Lớn hơn N lớn và nhỏ hơn N nhỏ
    'If Convert.ToSingle(a(2)) > Convert.ToSingle(a(3)) And Convert.ToSingle(a(2)) > Convert.ToSingle(a(1)) And Convert.ToSingle(a(3)) < Convert.ToSingle(a(0)) Then   'Lớn hơn S lớn và nhỏ hơn S nhỏ và S lớn phải nhỏ hơn N lớn đồng thời S nhỏ phải nhỏ hơn N nhỏ
    ''Hàm Kandel
    'Return aHamKandel
    'Else
    'MessageBox.Show("Nếu giá trị không thích nghi thứ nhất < giá trị không thích nghi thứ hai thì giá trị tối ưu thứ nhất phải lớn hơn giá trị tối ưu thứ hai và" + vbNewLine + "giá trị tối ưu thứ nhất phải lớn hơn giá trị không thích nghi thứ hai và" + vbNewLine + "giá trị tối ưu thứ hai phải nhỏ hơn giá trị không thích nghi thứ nhất" + vbNewLine + "Đây là dữ liệu chỉ tiêu sử dụng hàm Kandel", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)

    'End If

    'End If
    'If Convert.ToSingle(a(0)) > Convert.ToSingle(a(1)) Then       'Lớn hơn N Lớn và Nhỏ hơn N Nhỏ
    'If Convert.ToSingle(a(2)) < Convert.ToSingle(a(3)) And Convert.ToSingle(a(2)) > Convert.ToSingle(a(1)) And Convert.ToSingle(a(3)) < Convert.ToSingle(a(0)) Then   'Lớn hơn S nhỏ và nhỏ hơn S lớn
    'Return aHamhinhthang
    'Else
    'MessageBox.Show("Nếu giá trị không thích nghi thứ nhất > giá trị không thích nghi thứ hai thì giá trị tối ưu thứ nhất phải nhỏ hơn giá trị tối ưu thứ hai và" + vbNewLine + "giá trị tối ưu thứ nhất phải lớn hơn giá trị không thích nghi thứ hai và" + vbNewLine + "giá trị tối ưu thứ hai phải nhỏ hơn giá trị không thích nghi thứ nhất" + vbNewLine + "Đây là dữ liệu chỉ tiêu sử dụng hàm hình thang", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
    'End If
    'End If
    'End If
    'Else
    'MessageBox.Show("Hãy nhập lại giá trị không thích nghi hoặc giá trị tối ưu", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
    'End If




    'Return Nothing
    'End Function
    Private Sub BtnReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReload.Click
        Gridview1_Reload()
        '
        btnAdd.ToolTip = Themchitieu

        BtnUpdate.Enabled = False
    End Sub
#End Region
    Public Sub Gridview1_Reload()
        Dim i As Integer
        i = GridView1.FocusedRowHandle

        Gridview1_Connect = New myADOclass
        'Dim myConStr = con.myConStr
        Dim theselectObj = ListBoxControl1.SelectedValue
        Dim Qry As String = String.Format("select * from Maindata where Obj = ""{0}""", theselectObj)
        'Dim da As OleDbDataAdapter = New OleDbDataAdapter(Qry, myConStr)
        Gridcontrol1_dt = Gridview1_Connect.DtFromQry(Qry)
        GridControl1.DataSource = Gridcontrol1_dt
        'Gridcontrol1.DataSource = GridControl1.DataSource
        GridControl1.RefreshDataSource()

        '===re select focused row
        'GridView1.SelectRow(i) Không ăn thua
        GridView1.FocusedRowHandle = i

        cboIndGroup.DataBindings.Clear()
        cboIndGroup.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "IndGroup"))

        TxtS1.DataBindings.Clear()
        TxtS1.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "S1"))
        TxtS2.DataBindings.Clear()
        TxtS2.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "S2"))
        TxtS3.DataBindings.Clear()
        TxtS3.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "S3"))
        TxtN.DataBindings.Clear()
        TxtN.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "N"))

        txtInd.DataBindings.Clear()
        txtInd.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "Indname"))
        txt_D.DataBindings.Clear()
        txt_D.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "d"))
        'Txt_A1.DataBindings.Clear()
        'Txt_A1.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "a"))
        Txt_A.DataBindings.Clear()
        Txt_A.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "a"))
        'Txt_B1.DataBindings.Clear()
        'Txt_B1.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "b"))
        Txt_B.DataBindings.Clear()
        Txt_B.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "b"))
        'Txt_C1.DataBindings.Clear()
        'Txt_C1.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "c"))
        Txt_C.DataBindings.Clear()
        Txt_C.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "c"))
        'txtToiUu2.DataBindings.Clear()
        'txtToiUu2.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "S2"))
        cboFunction.DataBindings.Clear()
        cboFunction.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "UsedFunction"))
        TxtFunction.DataBindings.Clear()
        TxtFunction.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "UsedFunction"))
        TxtMap.DataBindings.Clear()
        TxtMap.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "Map"))
        TxtMapchuanhoa.DataBindings.Clear()
        TxtMapchuanhoa.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "Mapchuanhoa"))
        TxtWeight.DataBindings.Clear()
        TxtWeight.DataBindings.Add(New Binding("Text", GridControl1.DataSource, "Weight"))

        RadLim.DataBindings.Clear()
        RadLim.DataBindings.Add(New Binding("EditValue", GridControl1.DataSource, "Limit", True))

    End Sub
    Private Sub BtnBrowse1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBrowse1.Click
        Dim thepath As String = FillPath_byOpendialog(TxtMap.Text)
        If thepath = "" Then
            'TxtMap.Text = thepath
            'GridView1.UpdateCurrentRow()
            Return
        End If
        Dim FN As String = IO.Path.GetFileNameWithoutExtension(thepath)
        Dim dir As String = IO.Path.GetDirectoryName(thepath)
        TxtMap.Text = thepath


        Dim pathchuanhoa As String
        '===Nhập giá trị mặc định cho txtMapchuanhoa
        Try
            Dim va As String = cboIndGroup.SelectedItem
            If BarCheckItemComposite.Checked = False And (va = "4. Hạn chế" Or va = "4. Constraint" Or va = "2. Kinh tế - Xã hội" Or va = "2. Socio - Economic" Or va = "3. Môi trường" Or va = "3. Environment") Then

                If thepath.Substring(thepath.Length - 3).ToUpper = "ADF" Then
                    pathchuanhoa = thepath.Replace("\HDR.ADF", ".tif")
                Else
                    pathchuanhoa = dir + "\" + FN + ".tif"
                End If
            Else
                If thepath.Substring(thepath.Length - 3).ToUpper = "ADF" Then
                    pathchuanhoa = thepath.Replace("\HDR.ADF", "_ch.tif")
                Else
                    pathchuanhoa = dir + "\" + FN + "_ch.tif"
                End If


            End If

            TxtMapchuanhoa.Text = pathchuanhoa
            checkEncs_step2()
        Catch ex As Exception

        End Try

        GridView1.UpdateCurrentRow()
        BtnUpdate.Enabled = True
    End Sub
    Private Sub BtnBrowse2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBrowse2.Click
        TxtMapchuanhoa.Text = FillPath_bySaveFiledialog(TxtMapchuanhoa.Text)
        GridView1.UpdateCurrentRow()
        BtnUpdate.Enabled = True
    End Sub
    Public Function FillPath_byOpendialog(ByVal theText As String)      'theText là giá trị trả về khi người dùng click cancel. Lấy = giá trị cũ của textbox.

        'Dim openFileDialog As New OpenFileDialog()
        Dim openFileDialog As New OpenFileDialog()

        openFileDialog.Title = Chonraster
        'openFileDialog.InitialDirectory = Application.StartupPath + "\Images"
        'openFileDialog.Filter = "File được hỗ trợ|*.Tif;HDR.ADF;*.IMG|Tiff file|*.TIF|Gridfile|HDR.ADF|IMG file|*.IMG"
        openFileDialog.Filter = "File được hỗ trợ|*.Tif"
        'AppManager1.
        openFileDialog.FilterIndex = 1
        openFileDialog.RestoreDirectory = True
        'openFileDialog.OverwritePrompt = False
        Dim fileName As String = theText    'TxtMap.Text
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            fileName = openFileDialog.FileName

            'Else : Exit Function

        End If
        Return fileName
    End Function
    Public Function FillPath_bySaveFiledialog(ByVal theText As String)      'theText là giá trị trả về khi người dùng click cancel. Lấy = giá trị cũ của textbox.

        'Dim openFileDialog As New OpenFileDialog()
        Dim saveFileDialg As New SaveFileDialog()

        saveFileDialg.Title = Chonraster
        'openFileDialog.InitialDirectory = Application.StartupPath + "\Images"
        'saveFileDialg.Filter = "Tiff file|*.TIF|IMG file|*.IMG"
        saveFileDialg.Filter = "Tiff file|*.TIF"
        'AppManager1.
        saveFileDialg.FilterIndex = 1
        saveFileDialg.RestoreDirectory = True
        saveFileDialg.OverwritePrompt = False
        Dim fileName As String = theText    'TxtMap.Text
        If saveFileDialg.ShowDialog() = DialogResult.OK Then
            fileName = saveFileDialg.FileName

            'Else : Exit Function

        End If
        Return fileName
    End Function

    Public Sub BarButtonChonfile_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonChonfile.ItemClick
        Dim f As New OpenFileDialog
        f.Filter = projectDGTN
        f.Title = OpenProject
        Dim FullFileName As String = ""
        If f.ShowDialog() = DialogResult.OK Then
            FullFileName = f.FileName
            If FullFileName.Length > 63 Then
                MnuFileHientai.Text = "..." + FullFileName.Substring(FullFileName.Length - 35, 35)
                GroupControl2.Text = FullFileName.Substring(0, 10) + "..." + FullFileName.Substring(FullFileName.Length - 53, 53)
            Else
                GroupControl2.Text = FullFileName
                MnuFileHientai.Text = FullFileName
            End If
            Dim sWriter As StreamWriter = New StreamWriter(Application.StartupPath + "/myFile.dll", False, System.Text.Encoding.UTF8)   'myFile.dll là file chứa đường dẫn tới dataFile tại dòng 1

            sWriter.WriteLine(FullFileName)
            sWriter.Flush()
            sWriter.Close()
            ''LoadMainForm()
            '================Thay đổi đường dẫn cho Data Resource Trong trường hợp dùng dataset...
            ''Dim St As String = IO.File.ReadAllText("C:\Test.txt") 'Ð?c h?t file
            'Dim St() As String = IO.File.ReadAllLines(Application.StartupPath + "/LocalKnowledge.exe.config") 'Ð?c h?t các dòng c?a file
            ''.File.WriteAllText("C:\Test.txt", St) 'Ghi chu?i St vào file
            'Dim mypass = "abc123xyz"
            'St(6) = "connectionString=""Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + FullFileName + "; Jet OLEDB:Database Password= " + mypass + ""
            'IO.File.WriteAllLines(Application.StartupPath + "/LocalKnowledge.exe.config", St) 'Ghi m?ng String St vào file, m?i ph?n t? là 1 dòng
            Try
                Gridview1_Connect.KetNoi_Close()
            Catch ex As Exception

            End Try
            Gridview1_Connect = New myADOclass
            Gridview2_Connect = New myADOclass
            Gridview1_Reload()
            Dim listbox1Qry As String = "select distinct Obj from Maindata"

            Dim dt As DataTable = Gridview1_Connect.DtFromQry(listbox1Qry)
            ListBoxControl1.Items.Clear()
            For i = 0 To dt.Rows.Count - 1
                ListBoxControl1.Items.Add(dt.Rows(i)("Obj"))
            Next

            'Dim mylist = (From row In dt.AsEnumerable()
            'Select row.Field(Of String)("Obj")).Distinct().ToList()
            'ListBoxControl1.DataSource = mylist

            ListBoxControl1.Items.Add(Taocaymoi)
            ''====Initial lại cho startup form
            ''FrmStartup.chayListboxControl1_selectedIndexChanged = False
            'Dim Qry As String = "select distinct Obj from Maindata"
            'Dim con As MyCon = New MyCon
            ''FrmStartup.ListBoxControl1.DataSource = con.DtFromQry(Qry)
            'DataGridView1.DataSource = con.DtFromQry(Qry)
            ''FrmStartup.chayListboxControl1_selectedIndexChanged = True
            ''FrmStartup.ListBoxControl1.DisplayMember = "Obj"
            ''FrmStartup.Show()
            ''FrmStartup.Focus()
        End If
        ''''Dim f As New OpenFileDialog
        ''''f.Filter = "Dữ liệu Đánh giá thích nghi cây trồng|*.nft"
        ''''f.Title = "Chọn File dữ liệu"
        ''''Dim FullFileName As String = " "
        ''''If f.ShowDialog() = DialogResult.OK Then
        ''''FullFileName = f.FileName
        '''''MnuFileHientai.ToolTipText = FullFileName

        ''''If FullFileName.Length > 50 Then
        ''''MnuFileHientai.Text = FullFileName.Substring(0, 10) + "..." + FullFileName.Substring(FullFileName.Length - 40, 40)
        ''''Else
        ''''MnuFileHientai.Text = FullFileName
        ''''End If
        ''''Dim sWriter As StreamWriter = New StreamWriter(Application.StartupPath + "/myFile.dll", False, System.Text.Encoding.UTF8)   'myFile.dll là file chứa đường dẫn tới dataFile tại dòng 1

        ''''sWriter.WriteLine(FullFileName)
        ''''sWriter.Flush()
        ''''sWriter.Close()
        ''''''LoadMainForm()
        '''''================Thay đổi đường dẫn cho Data Resource Trong trường hợp dùng dataset...
        ''''''Dim St As String = IO.File.ReadAllText("C:\Test.txt") 'Ð?c h?t file
        '''''Dim St() As String = IO.File.ReadAllLines(Application.StartupPath + "/LocalKnowledge.exe.config") 'Ð?c h?t các dòng c?a file
        ''''''.File.WriteAllText("C:\Test.txt", St) 'Ghi chu?i St vào file
        '''''Dim mypass = "abc123xyz"
        '''''St(6) = "connectionString=""Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + FullFileName + "; Jet OLEDB:Database Password= " + mypass + ""
        '''''IO.File.WriteAllLines(Application.StartupPath + "/LocalKnowledge.exe.config", St) 'Ghi m?ng String St vào file, m?i ph?n t? là 1 dòng



        '''''====Initial lại cho startup form
        '''''FrmStartup.chayListboxControl1_selectedIndexChanged = False
        ''''Dim Qry As String = "select distinct Obj from Maindata"
        ''''Myconnect = New MyCon
        ''''Dim mdt As DataTable = Myconnect.DtFromQry(Qry)
        ''''ListBoxControl1.Items.Clear()
        ''''For i = 0 To mdt.Rows.Count - 1
        ''''ListBoxControl1.Items.Add(mdt.Rows(i)("Obj"))
        ''''Next
        '''''ListBoxControl1.DataSource = con.DtFromQry("select distinct Obj from Maindata")
        '''''DataGridView1.DataSource = con.DtFromQry(Qry)
        '''''FrmStartup.chayListboxControl1_selectedIndexChanged = True
        '''''FrmStartup.ListBoxControl1.DisplayMember = "Obj"
        '''''FrmStartup.Show()
        '''''FrmStartup.Focus()
        ''''Gridview1_Reload()
        ''''End If
    End Sub




    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim i As Integer
        i = GridView1.FocusedRowHandle

        Gridview1_Connect = New myADOclass
        Dim myConStr = Gridview1_Connect.myConnectString
        Dim theselectObj = Label1.Text
        Dim Qry As String = String.Format("select * from Maindata where Obj = ""{0}""", theselectObj)
        Dim da As OleDbDataAdapter = New OleDbDataAdapter(Qry, myConStr)
        Gridcontrol1_dt = Gridview1_Connect.DtFromQry(Qry)
        GridControl1.DataSource = Gridcontrol1_dt
        GridControl1.RefreshDataSource()
        '===re select focused row
        'GridView1.SelectRow(i) Không ăn thua
        GridView1.FocusedRowHandle = i

    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'Dim a As IRaster
        'hamhinhthang(a, "fds", 21)
        ''tst.Show()
        ''TxtFunction.Text = FunctionDetect()
    End Sub



    Private Sub XtraTabControl1_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles XtraTabControl1.SelectedPageChanged
        'GridView1.ClearColumnsFilter()
        'GridView2.ClearColumnsFilter()
        'Gridview1_Connect.Update_CSDL(GridControl1, "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
        RadDGTN.Enabled = BarCheckItemComposite.Checked
        Dim b = Gridview2_Connect.Update_CSDL(GridControl2, "select * from [" + ListBoxControl1.Tag.ToString + "]")
        If cboFunction.SelectedItem = aHamhinhthang Then
            If Txt_B.Text = "" Or Txt_C.Text = "" Then
                XtraTabControl1.SelectedTabPageIndex = 0
                If e.PrevPage Is XtraTabPage_Step1 Then
                    MessageBox.Show(giatribc + vbNewLine + trogiuphinhthang, loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
                Txt_B.Focus()
                Return
            End If
        ElseIf cboFunction.SelectedItem = aHamkandel Then
            If Txt_A.Text = "" And Txt_B.Text = "" Then
                XtraTabControl1.SelectedTabPageIndex = 0
                If e.PrevPage Is XtraTabPage_Step1 Then
                    MessageBox.Show(giatriab + vbNewLine + trogiupkandel, loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
                Txt_A.Focus()
                Return

            End If
        ElseIf cboFunction.SelectedItem = aHams1 Or cboFunction.SelectedItem = aHams2 Then
            If Txt_A.Text = "" Or Txt_B.Text = "" Then

                XtraTabControl1.SelectedTabPageIndex = 0
                If e.PrevPage Is XtraTabPage_Step1 Then
                    MessageBox.Show(giatriab + vbNewLine + trogiupS, loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
                Txt_A.Focus()

                Return

            End If
        End If

        BtnUpdate_click(BtnUpdate, Nothing)
        'Gridview2_Connect.Update_CSDL(GridControl2, "select * from [" + ListBoxControl1.Tag.tostring + "]")
        'Gridview2_Connect.Update_CSDL(GridControl3, "select * from [" + ListBoxControl1.Tag.tostring + "]")
        'Gridview1_Reload()  'Sau khi cập nhât thì phải reload, nếu không sẽ gặp lỗi: Ví dụ, trong trường hợp chuyển sang tabpage2, mình đã đặt lại dt = ... và gridcontrol.datasource=dt nhưng ko hiểu sao ko cập nhật cái dt mới từ CSDL vào; Đặt luôn trong mycon.update_CSDL cho nó ngon
        'Bỏ dòng reload ở trên vì đã sửa mycon class theo commend của Devexpress Expert
        '=====================2 lệnh sau rất quan trọng, nếu ko sẽ ko update focused row
        'GridView1.PostEditor()
        'GridView1.UpdateCurrentRow()
        '=====================
        'If XtraTabPage1.Focused = True And Not (GridControl1.DataSource.getchanges) Is Nothing Then
        'Sử dụng XtraTabPage1.Tag = "Đang focus"  để xác định rằng vừa rời khỏi. Khi nào focus vào tabpage1 thì lại đặt tag lại
        '=======================================================
        If e.PrevPage Is XtraTabPage_Step3 Then
            'GridView1.Columns.ColumnByFieldName("TT").Visible = False
            '========Xét Tổng Weight = 1??
            Dim aa = WeightValidate()
            If aa(0) = -1 Then     'Tổng Weight không bằng 1
                'XtraTabControl1.SelectedTabPage = XtraTabPage_Step3     'Quay lại step3 - Trọng số
                Return
            End If
        End If
        If XtraTabControl1.SelectedTabPage Is XtraTabPage_Step1 Then
            GridView1.Columns.ColumnByFieldName("S1").Visible = True
            GridView1.Columns.ColumnByFieldName("S2").Visible = True
            GridView1.Columns.ColumnByFieldName("S3").Visible = True
            GridView1.Columns.ColumnByFieldName("N").Visible = True
            BarCheckItemComposite.Enabled = True
        Else
            GridView1.Columns.ColumnByFieldName("N").Visible = False
            GridView1.Columns.ColumnByFieldName("S3").Visible = False
            GridView1.Columns.ColumnByFieldName("S2").Visible = False
            GridView1.Columns.ColumnByFieldName("S1").Visible = False
            'GridView1.Columns.ColumnByFieldName("N").
        End If
        '===============TABPAGE 1111111111======================
        '=======================================================
        '=======================================================
        If XtraTabControl1.SelectedTabPage Is XtraTabPage_Step1 Then
            GridView1.ClearColumnsFilter()
            GridView2.ClearColumnsFilter()
            XtraTabPage_Step1.Focus()
            XtraTabPage_Step3.PageEnabled = False
            XtraTabPage3.PageEnabled = False
            XtraTabPage_Step2.PageEnabled = True
            XtraTabPage_Step4.PageEnabled = False
            XtraTabPage6.PageEnabled = False
            XtraTabPage_Step5.PageEnabled = False
            BarCheckItemComposite.Enabled = True
            'GridControl1.BringToFront()
            GridControl2.Visible = False
            'GridControl1.DataSource = Gridview1_Connect.DtFromQry("select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
            'GridControl1.Refresh()
            'GridView1.Columns("Limit").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[Limit] <> ''")
            'GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")
            GridView1.Columns("Limit").ClearFilter()
            GridView1.Columns("IndGroup").ClearFilter()
            '''''For i As Integer = 0 To GridView1.Columns.Count - 1
            '''''GridView1.Columns(i).SortOrder = DevExpress.Data.ColumnSortOrder.None
            '''''Next
            '''''GridView1.Columns("IndGroup").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            Gridview1_Reload()
            XtraTabControl1.Height = 145
            'GridView1.clearfilter()

            'Label2.Text = XtraTabPage3.Tag



            'If XtraTabPage1.Tag = "Đang focus" And Not (GridControl1.DataSource.getchanges) Is Nothing Then
            ''Nếu đang ở tab1 và vừa sửa dữ liệu thì 
            ''capnhatlaiTab2_3 = True    'Đã có trong BtnUpdate_click
            'BtnUpdate_click(Nothing, Nothing)
            'XtraTabPage1.Tag = "Mất focus"
            'End If


            '=======================================================
            '===============XtraTabPage_Step2======================STEP2 - BẢN ĐỒ CHUẨN HÓA
            '=======================================================
            '=======================================================
        ElseIf XtraTabControl1.SelectedTabPage Is XtraTabPage_Step2 Then    '= 3 ThenfrmCongRaster.ShowDialog()
            '****Xét trường hợp CheckEnco và số lượng Chỉ tiêu KT|Chỉ tiêu MT để có cho chuyển sang TabStep2 hay không
            '****Nếu Check Encs đang KHÔNG check thì chỉ có thể có tối đa 1 MT và tối đa 1KT Nếu có hơn thì không cho chuyển
            '****Nếu Check Encs đang Check thì bắt buộc phải có hơn 1 chỉ tiêu MT HOẶC hơn 1 chỉ tiêu KT
            GridView1.Columns("IndGroup").ClearFilter()
            GridView1.Columns("Limit").ClearFilter()
            GridView1.ClearColumnsFilter()
            GridView2.ClearColumnsFilter()
            BarCheckItemComposite.Enabled = False
            checkEncs_step2()   'Nếu ko pass check thì chuyển về tabpage1 và khi chuyển về tabpage1 thì exitsub này luôn bằng lệnh if bên dưới
            If XtraTabControl1.SelectedTabPage Is XtraTabPage_Step1 Then
                Return
            End If

            '****
            '****

            '================
            'GridControl1.BringToFront()
            GridControl2.Visible = False
            'BarCheckItemComposite.Enabled = True
            'GridView1.Columns("Limit").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")
            'GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")
            GridView1.ClearColumnsFilter()
            GridView2.OptionsCustomization.AllowSort = True
            '''''For i As Integer = 0 To GridView1.Columns.Count - 1
            '''''GridView1.Columns(i).SortOrder = DevExpress.Data.ColumnSortOrder.None
            '''''Next
            '''''GridView1.Columns("IndGroup").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            GridView2.OptionsCustomization.AllowSort = False
            'GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] <> '4. Hạn chế' and [IndGroup] <> '4. Constraint' and [IndGroup] <> '2. Kinh tế - Xã hội' and [IndGroup] <> '2. Socio - Economic' and [IndGroup] <> '3. Môi trường' and [IndGroup] <> '3. Environment'")
            XtraTabPage_Step3.PageEnabled = True
            XtraTabPage_Step4.PageEnabled = False
            XtraTabPage6.PageEnabled = False
            XtraTabPage_Step5.PageEnabled = False
            'Frmtmp_forProgress.ShowDialog()
            '.ShowDialog()
            'SplashScreenManager.ShowForm(GetType(WaitForm1))

            'FrmRunning.TopMost = True
            'FrmRunning.Show

            Dim a As Integer = GridView1.RowCount - 1
            If a Mod 2 = 0 Then
                XtraTabControl1.Height = 52 + (GridView1.RowCount * 13)
            Else

                XtraTabControl1.Height = 42 + (GridView1.RowCount * 13)
            End If


            '=============================
            'Thoát nếu ko cần cập nhật lại
            '=============================

            If capnhatlaiTab2_3 = False And XtraScrollableControl2.Controls.Count <> 0 Then
                For j As Integer = 0 To GridView1.RowCount - 1
                    Dim Pic As DevExpress.XtraEditors.PictureEdit = FindControl(XtraScrollableControl2, "PictureBoxInfo" + j.ToString)

                    If Pic.ToolTip = Bandochuanhoaloi Or Pic.ToolTip.ToString = filedulieuKhongtontai Then
                        XtraTabPage_Step4.PageEnabled = False
                        XtraTabPage_Step4.Tooltip = Khongkichhoatbuoc4
                        XtraTabPage_Step4.Tag = 1
                        Return
                    End If
                Next
                If XtraTabPage_Step4.Tag = 0 Then
                    XtraTabPage_Step4.PageEnabled = True
                End If

                Return
            End If


            '''''For i As Integer = 0 To GridView1.Columns.Count - 1
            '''''GridView1.Columns(i).SortOrder = DevExpress.Data.ColumnSortOrder.None
            '''''Next
            '''''GridView1.Columns("IndGroup").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            '''''GridControl1.DataSource = Gridview1_Connect.DtFromQry("select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
            ''''''GridView1.ClearColumnsFilter()
            ''''''GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] <> '4. Hạn chế' and [IndGroup] <> '4. Constraint' and [IndGroup] <> '2. Kinh tế - Xã hội' and [IndGroup] <> '2. Socio - Economic' and [IndGroup] <> '3. Môi trường' and [IndGroup] <> '3. Environment'")
            SplashScreenManager.ShowForm(GetType(FrmWaiting))

            If XtraTabPage_Step2.Tag > 0 Then
                '''Label2.Text = ""
                ''XtraTabControl1.SelectedTabPageIndex = 2
                ''MessageBox.Show(Kiemtrachitieu, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)

                ''Exit Sub
            End If
            XtraTabPage_Step4.Tag = 0
            'XtraTabPage2selected = True
            'PanelControl3.Controls.Clear()
            XtraScrollableControl2.Controls.Clear()
            'Dim myLabel As DevExpress.XtraEditors.LabelControl = New DevExpress.XtraEditors.LabelControl
            Dim x As Integer
            Dim y As Integer
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
            For i As Integer = 0 To GridView1.RowCount - 1
                If i Mod 2 = 0 Then
                    x = 0
                    y = i / 2
                Else
                    x = 645      'Là khoảng cách giữa control của dãy bên trái và dãy bên phải
                    y = (i - 1) / 2
                End If

                '
                'LabelControl13
                '
                Dim indName As String = GridView1.GetDataRow(i)("IndName").ToString()
                Dim LabelMap = New DevExpress.XtraEditors.LabelControl()
                LabelMap.Location = New System.Drawing.Point(5 + x, 10 + 25 * y)
                LabelMap.Name = "LabelMap" + i.ToString
                LabelMap.Size = New System.Drawing.Size(130, 13)
                LabelMap.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
                LabelMap.Text = indName
                LabelMap.ToolTip = indName
                '
                'TextEdit1
                '
                Dim Mapchuanhoa As String = GridView1.GetDataRow(i)("Mapchuanhoa").ToString()
                Dim TxtMapchuanhoa = New DevExpress.XtraEditors.TextEdit()
                TxtMapchuanhoa.Location = New System.Drawing.Point(135 + x, 7 + 25 * y)
                TxtMapchuanhoa.MenuManager = RibbonControl1
                TxtMapchuanhoa.Name = "TxtMapchuanhoa" + i.ToString
                'TextEdit1.Properties.ReadOnly = True
                TxtMapchuanhoa.Size = New System.Drawing.Size(315, 20)
                TxtMapchuanhoa.Text = Mapchuanhoa
                TxtMapchuanhoa.Tag = i
                TxtMapchuanhoa.Properties.ReadOnly = True
                AddHandler TxtMapchuanhoa.KeyUp, AddressOf mytxtMapchuanhoa_KeyUp
                '
                'SimpleButton1
                '
                Dim BtnBrowsechuanhoa = New DevExpress.XtraEditors.SimpleButton()
                BtnBrowsechuanhoa.Location = New System.Drawing.Point(456 + x, 5 + 25 * y)
                BtnBrowsechuanhoa.Name = "BtnBrowsechuanhoa" + i.ToString
                BtnBrowsechuanhoa.Size = New System.Drawing.Size(32, 23)
                'BtnBrowsechuanhoa.Text = "Browse"
                BtnBrowsechuanhoa.Image = CType(resources.GetObject("BarButtonChonfile.LargeGlyph"), System.Drawing.Image)
                BtnBrowsechuanhoa.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
                BtnBrowsechuanhoa.ToolTip = nhapfiletif
                BtnBrowsechuanhoa.Tag = i
                AddHandler BtnBrowsechuanhoa.Click, AddressOf BtnBrowsechuanhoa_Click
                '
                'PictureBox1
                '
                Dim PictureBoxInfo = New DevExpress.XtraEditors.PictureEdit
                PictureBoxInfo.Image = My.Resources.Resources.cancel_32x32
                PictureBoxInfo.Location = New System.Drawing.Point(490 + x, 5 + 25 * y)
                PictureBoxInfo.Name = "PictureBoxInfo" + i.ToString
                PictureBoxInfo.Size = New System.Drawing.Size(27, 23)
                'PictureBoxInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
                PictureBoxInfo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
                PictureBoxInfo.ToolTip = filedulieuKhongtontai
                PictureBoxInfo.BackColor = Color.Transparent
                PictureBoxInfo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder


                '
                'SimpleButton1  Chạy chuẩn hóa
                '
                Dim Btnrun = New DevExpress.XtraEditors.SimpleButton()
                Btnrun.Location = New System.Drawing.Point(520 + x, 5 + 25 * y)
                Btnrun.Name = "Btnrun" + i.ToString
                Btnrun.Size = New System.Drawing.Size(32, 23)
                'Btnrun.Text = "Run"
                Btnrun.Image = Global.NhanDienMauDat.My.Resources.Resources.run2
                Btnrun.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
                Btnrun.Tag = i
                Btnrun.ToolTip = Xaydungbandochuanhoa
                AddHandler Btnrun.Click, AddressOf BtnRun_Click


                Dim funct As String = GridView1.GetRowCellValue(i, "UsedFunction")

                If BarCheckItemComposite.Checked = False Then
                    Dim IndG As String = GridView1.GetRowCellValue(i, "IndGroup")
                    If IndG = "4. Hạn chế" Or IndG = "4. Constraint" Or IndG = "2. Kinh tế - Xã hội" Or IndG = "2. Socio - Economic" Or IndG = "3. Môi trường" Or IndG = "3. Environment" Then
                        Btnrun.Enabled = False
                    End If
                Else
                    Btnrun.Enabled = True
                End If
                If funct = ahamKhongham Then
                    Btnrun.Enabled = False
                End If
                Dim mappath As String = GridView1.GetDataRow(i)("Map").ToString()
                If System.IO.File.Exists(mappath) = False OrElse GridView1.GetDataRow(i)("Mapchuanhoa").ToString() = "" Then
                    'MessageBox.Show("")
                    'Btnrun.Enabled = False
                    Btnrun.ToolTip = Loibando + "-" + Bandochitieu
                    RemoveHandler Btnrun.Click, AddressOf BtnRun_Click
                    'Else

                    'If System.IO.File.Exists(mappath) = True Then
                    'Try
                    'Raster.Open(mappath)
                    'Catch ex As Exception
                    'Btnrun.Enabled = False
                    'End Try

                    'End If

                End If



                '
                'SimpleButton1  Xem  chuẩn hóa
                '
                Dim BtnviewChuanhoa = New DevExpress.XtraEditors.SimpleButton()
                BtnviewChuanhoa.Location = New System.Drawing.Point(557 + x, 5 + 25 * y)
                BtnviewChuanhoa.Name = "BtnviewChuanhoa" + i.ToString
                BtnviewChuanhoa.Size = New System.Drawing.Size(32, 23)
                'Btnview.Text = xem
                BtnviewChuanhoa.Tag = i     'TxtMapchuanhoa.Text
                BtnviewChuanhoa.Image = SimpleButton1.Image

                BtnviewChuanhoa.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
                BtnviewChuanhoa.ToolTip = Xemthubando
                AddHandler BtnviewChuanhoa.Click, AddressOf BtnviewChuanhoa_Click
                '
                'Add to panel3
                '
                'PanelControl3.Controls.AddRange({LabelMap, TxtMapchuanhoa, BtnBrowsechuanhoa, PictureBoxInfo})
                '=== Thay đổi picturebox.image và Btnview.tag nếu inputraster ok
                '==================================Sửa thành check đường dẫn, ko check file cho nó nhanh
                Dim filefullpath As String = TxtMapchuanhoa.Text
                If System.IO.File.Exists(filefullpath) = True Then
                    PictureBoxInfo.Image = My.Resources.Resources.apply_32x32
                    PictureBoxInfo.ToolTip = filedulieutontai
                Else
                    XtraTabPage_Step4.Tag = XtraTabPage_Step4.Tag + 1
                End If

                'Dim inputRaster As Raster
                'Try
                'inputRaster = Raster.Open(TxtMapchuanhoa.Text)  'Nếu mở được thì sẽ tự động tiếp tục, nếu ko thì Catch
                'PictureBoxInfo.Image = My.Resources.Resources.apply_32x32
                'PictureBoxInfo.ToolTip = "OK"
                ''Btnview.Tag = inputRaster
                'inputRaster.Close()
                'inputRaster.Dispose()
                'GC.Collect()
                'Catch ex As Exception
                ''MessageBox.Show("Không mở được bản đồ chỉ tiêu thứ " + i.ToString, "")
                'XtraTabPage5.Tag = XtraTabPage5.Tag + 1
                'End Try

                XtraScrollableControl2.Controls.AddRange({LabelMap, TxtMapchuanhoa, BtnBrowsechuanhoa, PictureBoxInfo, Btnrun, BtnviewChuanhoa})
                XtraScrollableControl2.Refresh()

            Next

            'For i As Integer = 0 To GridView1.RowCount - 1

            'Next


            'xử lý enable tabpage5

            'If XtraTabPage5.Tag > 0 Then
            'XtraTabPage5.PageEnabled = False
            'XtraTabPage5.Tooltip = "Không thể kích hoạt. Kiểm tra lại Bước 4"
            'Else
            'XtraTabPage5.PageEnabled = True
            'XtraTabPage5.Tooltip = "Bước 5"
            'End If
            SplashScreenManager.CloseForm() 'Đặt ở trên vòng for j; Nếu đặt ở sau chương trình sẽ return nếu gặp pic.tooltip = lỗi
            '=======================================================
            '===============XtraTabPage_Step3======================Step3333333333333
            '=======================================================Trọng số
            '=======================================================
            'GridControl1.BringToFront()
        ElseIf XtraTabControl1.SelectedTabPage Is XtraTabPage_Step3 Then

            For j As Integer = 0 To GridView1.RowCount - 1
                Dim Pic As DevExpress.XtraEditors.PictureEdit = FindControl(XtraScrollableControl2, "PictureBoxInfo" + j.ToString)

                'If "abc" = "fdsfsd" And (Pic.ToolTip = Bandoloi Or Pic.ToolTip = filedulieuKhongtontai) Then
                If Pic.ToolTip.ToString = Bandoloi Or Pic.ToolTip.ToString = filedulieuKhongtontai Then
                    XtraTabPage_Step4.PageEnabled = False
                    XtraTabPage_Step4.Tooltip = Khongkichhoatbuoc4
                    XtraTabPage_Step4.Tag = 1
                    'XtraTabPage_Step3.PageEnabled = False
                    MessageBox.Show(Kiemtrachuanhoa, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    XtraTabPage_Step3.Tooltip = Khongkichhoatbuoc4
                    XtraTabControl1.SelectedTabPage = XtraTabPage_Step2

                    Return
                Else
                    XtraTabPage_Step4.Tag = 0
                End If
            Next
            'If XtraTabPage5.Tag = 0 Then
            XtraTabPage_Step4.PageEnabled = True
            If BarCheckItemComposite.Checked = True Then
                For i = 0 To GridView1.RowCount - 1
                    If GridView1.GetRowCellValue(i, "Limit").ToString = "" And GridView1.GetRowCellValue(i, "IndGroup").ToString <> "4. Hạn chế" And GridView1.GetRowCellValue(i, "IndGroup").ToString <> "4. Constraint" And GridView1.GetRowCellValue(i, "UsedFunction").ToString <> ahamKhongham Then
                        If myLang = "English" Then
                            MessageBox.Show("""" + GridView1.GetRowCellValue(i, "IndName").ToString + """" + " is Limit or Non-Limit indicator?", thongbao)
                        Else
                            MessageBox.Show("Chỉ tiêu " + """" + GridView1.GetRowCellValue(i, "IndName").ToString + """" + " thuộc nhóm Giới hạn hay Không giới hạn?", thongbao)
                        End If


                        'XtraTabControl1.SelectedTabPage = XtraTabPage_Step2
                        XtraTabControl1.SelectedTabPage = XtraTabPage_Step1
                        'Gridview1_Reload()
                        'XtraTabControl1.Height = 145
                        Return
                    End If

                Next
            End If
            BarCheckItemComposite.Enabled = False
            GridControl2.Visible = False
            'GridControl1.BringToFront()
            'GridControl2.Visible = False
            If txtInd.Text.ToString = "" Then
                MessageBox.Show(Tenchitieu, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraTabControl1.SelectedTabPageIndex = 0
                'txtInd.Focus()
                Return
            End If
            'XtraTabPage3.PageEnabled = True
            'XtraTabPage_Step2.PageEnabled = True
            XtraTabPage_Step4.PageEnabled = True
            XtraTabPage6.PageEnabled = False
            XtraTabPage_Step5.PageEnabled = False


            'GridView1.Columns.ColumnByFieldName("TT").Visible = False
            'GridView1.Columns.ColumnByFieldName("TT").Visible = True
            'GridView1.Columns.ColumnByFieldName("Weight").Visible = False
            'GridView1.Columns.ColumnByFieldName("Weight").Visible = True
            'Dim a As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs = New DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(GridView1.FocusedRowHandle, GridView1.FocusedRowHandle)

            'GridView1_FocusedRowChanged(GridView1, a)
            'khởi tạo DataGridView2
            'DataGridView2.DataSource = GridControl1.DataSource
            'For Each col As DataGridViewTextBoxColumn In DataGridView2.Columns
            'If col.Name <> "IndName" And col.Name <> "TT" Then
            'DataGridView2.Columns.Item(col.Name).Visible = False
            'End If
            'Next
            'DataGridView2.Columns.Item("IndName").Width = 300
            'DataGridView2.Columns.Item("TT").Width = 40
            'DataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            'DataGridView2.MultiSelect = False
            'DataGridView2.ColumnHeadersVisible = False
            'DataGridView2.RowHeadersVisible = False
            XtraTabControl1.Height = 145
            GridView1.Columns("Limit").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[Limit] = 'Không giới hạn' or [Limit] = 'Non-Limit'")

            'Gridcontrol1.Dock = DockStyle.Fill
            'Gridcontrol1.BringToFront()
            'Dim dt As DataTable = Gridview1_Connect.DtFromQry("select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """" + " and (Limit = 'Không giới hạn' or Limit = 'Non-Limit')")

            'GridControl1.DataSource = dt
            'GridControl1.RefreshDataSource()
            ''Gridview1_Reload()

            ''GridControl2.DataSource = dt
            ''Trong lần đầu tiên chưa nhập giá trị cho TT thì sẽ nhập tự động
            ''Sửa lại thành cứ vào là sắp xếp Descending theo Weight sau đó là nhập lại thứ tứ luôn. Và tính luôn cả weight tự động nữa
            ''GridView1.Columns("TT").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            '''''For i As Integer = 0 To GridView1.Columns.Count - 1
            '''''GridView1.Columns(i).SortOrder = DevExpress.Data.ColumnSortOrder.None
            '''''Next


            weightCal("KTXH")
            weightCal("TNMT")
            weightCal("TNST")

            RadDGTN.SelectedIndex = 0

            'End If
            'FrmRunning.Close()
            '=======================================================
            '===============TABPAGE 3333333333======================
            '=======================================================BẢN ĐỒ CHỈ TIÊU?????
            '=======================================================
            'ElseIf XtraTabControl1.SelectedTabPage Is XtraTabPage3 Then    '= 2 Then

            '    MessageBox.Show("Bước 3???", "Liên hệ tác giả")
            'XtraTabPage_Step3.PageEnabled = True
            'XtraTabPage_Step2.PageEnabled = False
            ''GridControl1.BringToFront()
            'GridControl2.Visible = False
            ''=================Tab Bản đồ chỉ tiêu
            ''FrmRunning.Show()
            'XtraTabPage_Step4.PageEnabled = False
            'XtraTabPage6.PageEnabled = False
            ''GridControl1.DataSource = Gridview1_Connect.DtFromQry("select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
            ''GridControl1.BringToFront()
            'GridView1.ClearColumnsFilter()

            ''GridView1.Columns("Limit").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[Limit] <> ''")
            ''GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")
            ''''''For i As Integer = 0 To GridView1.Columns.Count - 1
            ''''''GridView1.Columns(i).SortOrder = DevExpress.Data.ColumnSortOrder.None
            ''''''Next
            ''''''GridView1.Columns("IndGroup").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            'Dim x As Integer
            'Dim y As Integer
            ''XtraTabPage4.Tag = "Can Active"
            ''Label2.Text = XtraTabPage4.Tag
            'Dim a As Integer = GridView1.RowCount - 1
            'If a Mod 2 = 0 Then     'sỐ CHỈ TIÊU LÀ LẺ
            'XtraTabControl1.Height = 52 + (GridView1.RowCount * 13)
            'Else    'Số chỉ tiêu là chẵn
            'XtraTabControl1.Height = 42 + (GridView1.RowCount * 13)
            'End If
            ''ol()

            ''=============================
            ''Thoát nếu ko cần cập nhật lại
            ''=============================
            'If capnhatlaiTab2_3 = False And XtraScrollableControl1.Controls.Count <> 0 Then
            'For j As Integer = 0 To GridView1.RowCount - 1
            'Dim Pic As DevExpress.XtraEditors.PictureEdit = FindControl(XtraScrollableControl1, "PictureBoxInfo" + j.ToString)

            'If Pic.ToolTip = Bandoloi Then
            ''XtraTabPage4.PageEnabled = False
            ''XtraTabPage4.Tooltip = Khongkichhoatbuoc3
            'XtraTabPage_Step2.Tag = 1
            'Return
            'End If
            'Next
            ''Nếu tiếp được đến đây có nghĩa là ko có PictureBoxInfo.Image nào = cancel
            'XtraTabPage_Step2.PageEnabled = True
            'XtraTabPage_Step2.Tooltip = Buoc4
            'XtraTabPage_Step2.Tag = 0
            'Return
            'End If
            ''''''For i As Integer = 0 To GridView1.Columns.Count - 1
            ''''''GridView1.Columns(i).SortOrder = DevExpress.Data.ColumnSortOrder.None
            ''''''Next
            ''''''GridView1.Columns("IndGroup").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            'Try
            'SplashScreenManager.CloseForm()
            'Catch ex As Exception

            'End Try

            'SplashScreenManager.ShowForm(GetType(FrmWaiting))



            'XtraTabPage_Step2.PageEnabled = True
            'XtraTabPage2selected = True
            ''PanelControl3.Controls.Clear()
            'XtraScrollableControl1.Controls.Clear()
            ''Dim myLabel As DevExpress.XtraEditors.LabelControl = New DevExpress.XtraEditors.LabelControl

            'Dim xtraTabpage4canActive As Integer = 0
            'Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
            'For i As Integer = 0 To GridView1.RowCount - 1
            'If i Mod 2 = 0 Then
            'x = 0
            'y = i / 2

            'Else
            'x = 640      'Là khoảng cách giữa control của dãy bên trái và dãy bên phải
            'y = (i - 1) / 2

            'End If

            ''
            ''LabelControl13
            ''
            'Dim indName As String = GridView1.GetDataRow(i)("IndName").ToString()
            'Dim LabelMap = New DevExpress.XtraEditors.LabelControl()
            'LabelMap.Location = New System.Drawing.Point(5 + x, 10 + 25 * y)
            'LabelMap.Name = "LabelMap" + i.ToString
            'LabelMap.Size = New System.Drawing.Size(130, 13)
            'LabelMap.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None
            'LabelMap.Text = indName
            'LabelMap.ToolTip = indName
            ''
            ''TextEdit1
            ''
            'Dim Map As String = GridView1.GetDataRow(i)("Map").ToString()
            'Dim TxtMap = New DevExpress.XtraEditors.TextEdit()
            'TxtMap.Location = New System.Drawing.Point(135 + x, 7 + 25 * y)
            'TxtMap.MenuManager = RibbonControl1
            'TxtMap.Name = "TxtMap" + i.ToString
            'TxtMap.Properties.ReadOnly = True
            ''TextEdit1.Properties.ReadOnly = True
            'TxtMap.Size = New System.Drawing.Size(367, 20)
            'TxtMap.Text = Map
            'TxtMap.Tag = i
            'AddHandler TxtMap.KeyUp, AddressOf mytxtMap_KeyUp
            ''
            ''SimpleButton1
            ''
            'Dim BtnBrowse = New DevExpress.XtraEditors.SimpleButton()
            'BtnBrowse.Location = New System.Drawing.Point(508 + x, 5 + 25 * y)
            'BtnBrowse.Name = "BtnBrowse" + i.ToString
            'BtnBrowse.Size = New System.Drawing.Size(32, 23)
            ''BtnBrowse.Text = "Browse"
            'BtnBrowse.Image = CType(resources.GetObject("BarButtonChonfile.LargeGlyph"), System.Drawing.Image)
            'BtnBrowse.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
            'BtnBrowse.ToolTip = Chonraster
            'BtnBrowse.Tag = i
            'AddHandler BtnBrowse.Click, AddressOf BtnBrowse_Click
            ''
            ''PictureBox1
            ''
            'Dim PictureBoxInfo = New DevExpress.XtraEditors.PictureEdit
            'PictureBoxInfo.Image = My.Resources.Resources.cancel_32x32
            'PictureBoxInfo.Location = New System.Drawing.Point(543 + x, 5 + 25 * y)
            'PictureBoxInfo.Name = "PictureBoxInfo" + i.ToString
            'PictureBoxInfo.Size = New System.Drawing.Size(27, 23)
            ''PictureBoxInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
            'PictureBoxInfo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom
            'PictureBoxInfo.ToolTip = filedulieuKhongtontai
            'PictureBoxInfo.BackColor = Color.Transparent
            'PictureBoxInfo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            ''==================================Sửa thành check đường dẫn, ko check file cho nó nhanh
            'Dim filefullpath As String = TxtMap.Text
            'If System.IO.File.Exists(filefullpath) = True Then
            'PictureBoxInfo.Image = My.Resources.Resources.apply_32x32
            'PictureBoxInfo.ToolTip = filedulieutontai
            'Else
            'xtraTabpage4canActive = xtraTabpage4canActive + 1
            'End If
            ''''''Dim inputRaster As Raster
            ''''''Try
            ''''''inputRaster = Raster.Open(TxtMap.Text)
            ''''''PictureBoxInfo.Image = My.Resources.Resources.apply_32x32
            ''''''PictureBoxInfo.ToolTip = "OK"
            ''''''inputRaster.Close()
            ''''''inputRaster.Dispose()
            ''''''GC.Collect()
            ''''''Catch ex As Exception
            ''''''xtraTabpage4canActive = xtraTabpage4canActive + 1

            '''''''XtraTabPage4.Tag = "Không active vì có bản đồ chỉ tiêu lỗi"
            '''''''Label2.Text = XtraTabPage4.Tag
            ''''''''MessageBox.Show("Không mở được bản đồ chỉ tiêu thứ " + (i + 1).ToString, "")
            ''''''End Try

            ''SimpleButton1  Xem ban do chi tieu
            ''
            'Dim Btnview = New DevExpress.XtraEditors.SimpleButton()
            'Btnview.Location = New System.Drawing.Point(574 + x, 5 + 25 * y)
            'Btnview.Name = "Btnview" + i.ToString
            'Btnview.Size = New System.Drawing.Size(32, 23)
            ''Btnview.Text = xem
            'Btnview.Tag = i     'TxtMapchuanhoa.Text
            'Btnview.Image = SimpleButton1.Image
            'Btnview.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter
            'Btnview.ToolTip = Xem
            'AddHandler Btnview.Click, AddressOf Btnview_Click
            ''
            ''Add to panel3
            ''
            ''PanelControl3.Controls.AddRange({LabelMap, TxtMap, BtnBrowse, PictureBoxInfo})
            'XtraScrollableControl1.Controls.AddRange({LabelMap, TxtMap, BtnBrowse, PictureBoxInfo, Btnview})
            'XtraScrollableControl1.Refresh()
            'Next
            ''XtraTabPage4.Tag = xtraTabpage4canActive
            ''If xtraTabpage4canActive > 0 Then
            ''XtraTabPage4.PageEnabled = False
            ''XtraTabPage4.Tooltip = "Không thể kích hoạt do có bản đồ chỉ tiêu còn bị lỗi"
            ''Else
            ''XtraTabPage4.PageEnabled = True
            ''XtraTabPage4.Tooltip = "Bước 4"
            ''End If
            'SplashScreenManager.CloseForm() 'Đặt ở trên vòng for j; Nếu đặt ở sau chương trình sẽ return nếu gặp pic.tooltip = lỗi
            ''===========Cứ cho tab4 Active thoải mái đi
            'For j As Integer = 0 To GridView1.RowCount - 1
            'Dim Pic As DevExpress.XtraEditors.PictureEdit = FindControl(XtraScrollableControl1, "PictureBoxInfo" + j.ToString)

            'If Pic.ToolTip = Bandoloi Or Pic.ToolTip.ToString = filedulieuKhongtontai Then
            ''XtraTabPage4.PageEnabled = False
            ''XtraTabPage4.Tooltip = Khongkichhoatbuoc3
            'XtraTabPage_Step2.Tag = 1
            'Return
            'End If
            'Next
            ''Nếu tiếp được đến đây có nghĩa là ko có PictureBoxInfo.Image nào = cancel
            'XtraTabPage_Step2.PageEnabled = True
            'XtraTabPage_Step2.Tooltip = Buoc4
            'XtraTabPage_Step2.Tag = 0
            'GridView1.ClearColumnsFilter()
            '''''
            '''''click vào tab0 hoặc tab2 khi đang ở tab 1
            '''''Bỏ luôn mấy cái if này và update dữ liệu luôn khi chuyển tabpage
            ''''If XtraTabPage2selected = True And (XtraTabControl1.SelectedTabPageIndex = 0 Or XtraTabControl1.SelectedTabPageIndex = 2) Then
            '''''Update dữ liệu vừa thay đổi khi browse map
            ''''My connect.Update_CSDL(GridControl1,   "select * from MainData")
            ''''End If
            '''If XtraTabControl1.SelectedTabPageIndex <> 1 Then   'Đặt lệnh if này ở sau lệnh If trên: XtraTabPage2selected = True And (XtraTabControl1.SelectedTabPageIndex = 0 Or XtraTabControl1.SelectedTabPageIndex = 2
            '''XtraTabPage2selected = False
            '''End If
            ''''Dim dr As DataRow
            ''''Dim s As List(Of String) = dt.AsEnumerable().Select(dr >= dr.field("Map").ToString()).ToList()
            ''''dr.Field<string>("FuncName")

            ''==========================================
            '''==========================================Xử lý tabpage 4
            ''==========================================
            ''FrmRunning.Close()


            ''==============================================
            ''==============================================             XtraTabPage_Step4 
            ''==============================================ĐÁNH GIÁ Thích nghi thành phần
            ''==============================================

        ElseIf XtraTabControl1.SelectedTabPage Is XtraTabPage_Step4 Then    '= 4 Then
            GridControl2.DataSource = Gridview2_Connect.DtFromQry("Select * from [" + ListBoxControl1.Tag.ToString + "]")
            GridView1.ClearColumnsFilter()
            GridView2.ClearColumnsFilter()
            checkEncs_step2()
            ''GridControl2.BringToFront()
            'GridView2.ClearSorting()

            'GridControl2.BringToFront()
            GridControl2.Visible = True
            GridView2.Columns("Name").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[Name] <> '" & HanChe & "'")         '("[IndGroup] <> '2. Kinh tế - Xã hội' and [IndGroup] <> '3. Môi trường' and [IndGroup] <> '2. Socio - Economic' and [IndGroup] <> '3. Environment' and [IndGroup] <> '4. Hạn chế' and [IndGroup] <> '4. Constraint'")
            'If BarCheckItemComposite.Checked = True Then
            '    GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '1. Sinh thái' OR [IndGroup] = '1. Ecology'")         '("[IndGroup] <> '2. Kinh tế - Xã hội' and [IndGroup] <> '3. Môi trường' and [IndGroup] <> '2. Socio - Economic' and [IndGroup] <> '3. Environment' and [IndGroup] <> '4. Hạn chế' and [IndGroup] <> '4. Constraint'")
            'Else
            '    GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] <> '1. Sinh thái' AND [IndGroup] <> '1. Ecology'")  'GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '4. Hạn chế' or [IndGroup] = '4. Constraint' or [IndGroup] = '2. Kinh tế - Xã hội' or [IndGroup] = '2. Socio - Economic' or [IndGroup] = '3. Môi trường' or [IndGroup] = '3. Environment'")
            'End If
            'GridView2.Columns("ID").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            'GridView2.FocusedRowHandle = -1
            'GridView2.FocusedRowHandle = 0
            GridView2.OptionsCustomization.AllowSort = False

            XtraTabControl1.Height = 145
            ' If TxtDGKTXH_Reclass.Text <> "" And TxtDGTNMT_Reclass.Text <> "" And TxtDGTNST_Reclass.Text <> "" Then
            XtraTabPage_Step5.PageEnabled = True
            'End If

            formatdgv2_3(GridView2)
            If XtraTabPage_Step4.Tag > 0 Then
                'Label2.Text = ""
                XtraTabControl1.SelectedTabPage = XtraTabPage_Step2
                MessageBox.Show(Kiemtrachuanhoa, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)

                Exit Sub
            End If

            GridView2.FocusedRowHandle = -1
            GridView2.FocusedRowHandle = 0

            'formatdgv(Gridview1)
            '============Xóa các file tạm
            Dim tmppath As String = Path.GetTempPath() + "nft\"
            xoatmp(tmppath)
            GC.Collect()
            'GridControl3.Dock = DockStyle.Fill
            'Đừng xóa dòng datasource dưới.

            'TxtDGTNST.DataBindings.Clear()
            'TxtDGTNST.DataBindings.Add(New Binding("Text", GridControl2.DataSource, "SrcTif"))
            'TxtDGTNST_Reclass.DataBindings.Clear()
            'TxtDGTNST_Reclass.DataBindings.Add(New Binding("Text", GridControl2.DataSource, "RecTif"))
            'Dim filterQr As String = "[Name] = '" + ListBoxControl1.Tag.ToString + "'"
            'GridView2.Columns("Name").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(filterQr)
            'GridView2.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never
            'GridView2.Columns("Name").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")

            '=====Focus 2 text để kích hoạt Gridview2FocusRowchange => Hiển thị Gridview1 theo đúng bộ lọc
            TxtDGKTXH.Focus()
            TxtDGTNST.Focus()
            ' '' ' 'Mới bỏ các dòng với 5comment' dưới ngày 06/08/19  Vì đã đưa vào trong hàm check_Encs_Step2
            ' '' ''TxtDGTNST.Text = GridView2.GetDataRow(0)("SrcTif").ToString
            ' '' ''TxtDGKTXH.Text = GridView2.GetDataRow(1)("SrcTif").ToString
            ' '' ''TxtDGTNMT.Text = GridView2.GetDataRow(2)("SrcTif").ToString

            ' '' ''TxtDGTNST_Reclass.Text = GridView2.GetDataRow(0)("RecTif").ToString
            ' '' ''TxtDGKTXH_Reclass.Text = GridView2.GetDataRow(1)("RecTif").ToString
            ' '' ''TxtDGTNMT_Reclass.Text = GridView2.GetDataRow(2)("RecTif").ToString

            '' '' ''Enabled/disable textbox, runbtn... của KTXH và TNMT nếu nó chỉ có 1 chỉ tiêu (tức là không tổng hợp)
            ' '' ''TxtDGKTXH.Focus()   'Để lọc lấy các chỉ tiêu KTXH ở Gridview1
            ' '' ''If GridView1.RowCount = 1 Then
            ' '' ''    'LabelControl21.ForeColor = Color.Gray
            ' '' ''    TxtDGKTXH.Enabled = False
            ' '' ''    BtnBrowseDGKTXH.Enabled = False
            ' '' ''    BtnRunDGKTXH.Enabled = False

            ' '' ''    'ComboBoxEditSoLopDGKTXH.Enabled = False
            ' '' ''    'BtnRunDGKTXH_Reclass.Enabled = False
            ' '' ''    'BtnBrowseDGKTXH_Reclass.Enabled = False
            ' '' ''Else
            ' '' ''    TxtDGKTXH.Enabled = True
            ' '' ''    BtnBrowseDGKTXH.Enabled = True
            ' '' ''    BtnRunDGKTXH.Enabled = True
            ' '' ''    'LabelControl21.ForeColor = Color.Black
            ' '' ''    'ComboBoxEditSoLopDGKTXH.Enabled = True
            ' '' ''    'BtnRunDGKTXH_Reclass.Enabled = True
            ' '' ''    'BtnBrowseDGKTXH_Reclass.Enabled = True
            ' '' ''End If
            ' '' ''TxtDGTNMT.Focus()   'Để lọc lấy các chỉ tiêu TNMT ở Gridview1
            ' '' ''If GridView1.RowCount = 1 Then
            ' '' ''    LabelControl22.Enabled = False
            ' '' ''    TxtDGTNMT.Enabled = False
            ' '' ''    BtnBrowseDGTNMT.Enabled = False
            ' '' ''    BtnRunDGTNMT.Enabled = False
            ' '' ''    'ComboBoxEditSoLopDGTNMT.Enabled = False
            ' '' ''    'BtnRunDGTNMT_Reclass.Enabled = False
            ' '' ''    'BtnBrowseDGTNMT_Reclass.Enabled = False
            ' '' ''Else
            ' '' ''    LabelControl21.Enabled = True
            ' '' ''    TxtDGTNMT.Enabled = True
            ' '' ''    BtnBrowseDGTNMT.Enabled = True
            ' '' ''    BtnRunDGTNMT.Enabled = True
            ' '' ''    'ComboBoxEditSoLopDGTNMT.Enabled = True
            ' '' ''    'BtnRunDGTNMT_Reclass.Enabled = True
            ' '' ''    'BtnBrowseDGTNMT_Reclass.Enabled = True
            ' '' ''End If
            ' '' ''TxtDGTNST.Focus()
            ' '' '' ''=====Set ComboBoxEditSoLopDG INDEX
            ' '' ''Try
            ' '' ''    Dim fromtoDGTNST = GridView2.GetDataRow(0)("FromTo").ToString.Substring(0, GridView2.GetDataRow(0)("FromTo").ToString.Length - 1)    'Loại bỏ dấu | cuối cùng
            ' '' ''    Dim lstfromtoDGTNSTVal = fromtoDGTNST.Split("|")
            ' '' ''    ComboBoxEditSoLopDGTNST.SelectedIndex = lstfromtoDGTNSTVal.Count / 3 - 1
            ' '' ''Catch ex As Exception
            ' '' ''End Try
            ' '' ''Try
            ' '' ''    Dim fromtoDGKTXH = GridView2.GetDataRow(1)("FromTo").ToString.Substring(0, GridView2.GetDataRow(1)("FromTo").ToString.Length - 1)    'Loại bỏ dấu | cuối cùng
            ' '' ''    Dim lstfromtoDGKTXHVal = fromtoDGKTXH.Split("|")
            ' '' ''    ComboBoxEditSoLopDGKTXH.SelectedIndex = lstfromtoDGKTXHVal.Count / 3 - 1
            ' '' ''Catch ex As Exception
            ' '' ''End Try
            ' '' ''Try
            ' '' ''    Dim fromtoDGTNMT = GridView2.GetDataRow(2)("FromTo").ToString.Substring(0, GridView2.GetDataRow(2)("FromTo").ToString.Length - 1)    'Loại bỏ dấu | cuối cùng
            ' '' ''    Dim lstfromtoDGTNMTVal = fromtoDGTNMT.Split("|")
            ' '' ''    ComboBoxEditSoLopDGTNMT.SelectedIndex = lstfromtoDGTNMTVal.Count / 3 - 1
            ' '' ''Catch ex As Exception
            ' '' ''End Try
            '' '' ''=====================
            '' '' ''GridView2.SetdRowCellValue(0, "Name", "Đánh giá Thích nghi Sinh thái")
            '' '' ''GridView2.SetRowCellValue(1, "Name", "Đánh giá Thích nghi Kinh tế Xã hội")
            '' '' ''GridView2.SetRowCellValue(2, "Name", "Đánh giá Thích nghi Môi trường")

            ' '' ''GridView2.Columns.ColumnByFieldName("SrcTif").Caption = DulieuTNST
            ' '' ''GridView2.Columns.ColumnByFieldName("RecTif").Caption = DulieuTNSTReclass
            ' '' ''GridView2.Columns.ColumnByFieldName("FromTo").Width = 350
            '' '' ''GridView2.Columns.ColumnByFieldName("Name").Visible = True
            ' '' ''Try
            ' '' ''    'If GridView2.GetRowCellValue(0, "FromTo").ToString <> "" Then
            ' '' ''    CreateFromToTxtbox(ComboBoxEditSoLopDGTNST, XtraScrollableControl3, GridView2.GetRow(0), True, False)
            ' '' ''    'End If
            ' '' ''Catch
            ' '' ''End Try
            '' '' ''GridView2.Columns.ColumnByFieldName("GhiChu").Visible = False


            '' '' ''Mới bỏ 5 dòng dưới ngày 06/08/19
            '' '' ''If BarCheckItemComposite.Checked = False Then
            '' '' ''    GridView2.ClearColumnsFilter()
            '' '' ''    GridView2.Columns("Name").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[Name] = '" + TNST + "'")
            '' '' ''    TxtDGTNST.Focus()
            '' '' ''End If



            '==============================================
            '==============================================             XtraTabPage6 
            '==============================================
            '==============================================BỘ LỌC

        ElseIf XtraTabControl1.SelectedTabPage Is XtraTabPage6 Then
            'GridControl2.BringToFront()
            GridControl2.Visible = True
            XtraTabPage_Step5.PageEnabled = True
            'formatdgv2_3(GridView3)
            'GridControl3.Dock = DockStyle.Fill
            'GridControl3.BringToFront()
            If GridControl2.DataSource.rows.count = 1 Then  'Trường hợp chưa có Row cho bộ lọc
                Gridview2_Connect.AddRow_with_ID_col(GridControl2, "ID", ListBoxControl1.Tag.ToString)
                Gridview2_Connect.Update_CSDL(GridControl2, "select * from [" + ListBoxControl1.Tag.ToString + "]")
            End If
            'Dim filterQr As String = "[Name] <> '" + ListBoxControl1.Tag.ToString + "'"
            'GridView2.Columns("Name").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo(filterQr)
            GridView2.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never
            'GridView2.Columns("Name").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")

            'CreateFromToTxtbox(ComboBoxEdit_solopFilter, XtraScrollableControl4, GridView2.GetRow(GridView2.FocusedRowHandle), True)
            GridView2.Columns.ColumnByFieldName("Name").Visible = True
            'GridView2.Columns.ColumnByFieldName("GhiChu").Visible = True
            'GridView2.Columns.ColumnByFieldName("Name").Caption = Tenboloc
            'GridView2.Columns.ColumnByFieldName("Name").Width = 185
            'GridView2.Columns.ColumnByFieldName("SrcTif").Caption = Dulieuboloctho
            'GridView2.Columns.ColumnByFieldName("SrcTif").Width = 400
            'GridView2.Columns.ColumnByFieldName("RecTif").Caption = Dulieubolocreclass
            'GridView2.Columns.ColumnByFieldName("RecTif").Width = 400
            'GridView2.Columns.ColumnByFieldName("FromTo").Width = 200

            TxtFilterName.DataBindings.Clear()
            TxtFilterName.DataBindings.Add(New Binding("Text", GridControl2.DataSource, "Name"))

            TxtRawdata.DataBindings.Clear()
            TxtRawdata.DataBindings.Add(New Binding("Text", GridControl2.DataSource, "SrcTif"))

            TxtFilterReclass.DataBindings.Clear()
            TxtFilterReclass.DataBindings.Add(New Binding("Text", GridControl2.DataSource, "RecTif"))

            ComboBoxEdit_Hanche.DataBindings.Clear()
            ComboBoxEdit_Hanche.DataBindings.Add(New Binding("Text", GridControl2.DataSource, "GhiChu"))
            '==============================================
            '==============================================             XtraTabPage5 
            '==============================================
        ElseIf XtraTabControl1.SelectedTabPage Is XtraTabPage_Step5 Then
            'disable sort tại Tabpage5, nếu ko sẽ lỗi  BtnRunDGxxx_Click() khi user sort vì dùng RowHaldle=0: Dim SrcRasterFN = GridView2.GetRowCellValue(0, "RecTif") 
            'For Each col As GridColumn In GridView2.Columns
            'col.ClearSorting()
            'Next
            GridView1.ClearColumnsFilter()
            GridView2.ClearColumnsFilter()
            GridView2.ClearSorting()

            'GridControl2.BringToFront()
            GridControl2.Visible = True

            'If BarCheckItemComposite.Checked = True Then
            '    GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '1. Sinh thái' OR [IndGroup] = '1. Ecology'")         '("[IndGroup] <> '2. Kinh tế - Xã hội' and [IndGroup] <> '3. Môi trường' and [IndGroup] <> '2. Socio - Economic' and [IndGroup] <> '3. Environment' and [IndGroup] <> '4. Hạn chế' and [IndGroup] <> '4. Constraint'")
            'Else
            '    GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] <> '1. Sinh thái' AND [IndGroup] <> '1. Ecology'")  'GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '4. Hạn chế' or [IndGroup] = '4. Constraint' or [IndGroup] = '2. Kinh tế - Xã hội' or [IndGroup] = '2. Socio - Economic' or [IndGroup] = '3. Môi trường' or [IndGroup] = '3. Environment'")
            'End If
            GridView2.Columns("ID").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            GridView2.FocusedRowHandle = -1
            GridView2.FocusedRowHandle = 0
            GridView2.OptionsCustomization.AllowSort = False
            'GridView2.Columns("Name").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")
            'Dim msgYesNo As DialogResult = MessageBox.Show("Xây dựng bản đồ đánh giá mức độ thích hợp", "", MessageBoxButtons.YesNo)
            'If msgYesNo = Windows.Forms.DialogResult.Yes Then
            '	RunDGTHop()
            'End If
            RadioGroup1.SelectedIndex = 0
        End If

        '=============XÉT Previous TAB
        '=============

        If e.PrevPage Is XtraTabPage_Step3 Then
            'GridView1.Columns.ColumnByFieldName("TT").Visible = False
            '========Xét Tổng Weight = 1??
            'Dim aa = WeightValidate()
            'If aa(0) = -1 Then     'Tổng Weight không bằng 1
            'XtraTabControl1.SelectedTabPage =  XtraTab_Step3
            'Return
            'End If
            'If BarCheckItemComposite.Checked = False Then

            'For i As Integer = 0 To GridView1.RowCount - 1
            'If GridView1.GetRowCellValue(i, "IndGroup") = "2. Kinh tế - Xã hội" Or GridView1.GetRowCellValue(i, "IndGroup") = "2. Socio - Economic" Or GridView1.GetRowCellValue(i, "IndGroup") = "3. Môi trường" Or GridView1.GetRowCellValue(i, "IndGroup") = "3. Environment" Then
            'GridView1.SetRowCellValue(i, "Weight", "")
            'End If

            'Next
            'End If
            'GridView1.Columns("TT").Visible = False

        ElseIf e.PrevPage Is XtraTabPage_Step4 Then
            'GridControl2.SendToBack()


        ElseIf e.PrevPage Is XtraTabPage6 Then
            'GridControl3.SendToBack()
            'TxtFilterName.DataBindings.Clear()
            'TxtRawdata.DataBindings.Clear()
            'TxtFilterReclass.DataBindings.Clear()
            'TxtFilterName.Text = ""
            'TxtFilterReclass.Text = ""
            'TxtRawdata.Text = ""
        ElseIf e.PrevPage Is XtraTabPage_Step5 Then
            If Not XtraTabControl1.SelectedTabPage Is XtraTabPage_Step4 Then
                XtraTabPage_Step5.PageEnabled = False
            End If
            GridView2.OptionsCustomization.AllowSort = False
        End If

        For i = 0 To GridView1.RowCount - 1
            If GridView1.GetRowCellValue(i, "Limit").ToString = Gioihan Or GridView1.GetRowCellValue(i, "Limit").ToString = "" Then

                ''dt.Rows(i)("TT") = i + 1   'dt.Rows(j)("TT") = j + 1       Dùng update len DT này ko ăn thua, dùng cái dưới thì ok

                GridView1.SetRowCellValue(i, "TT", DBNull.Value)
                GridView1.SetRowCellValue(i, "Weight", DBNull.Value)
            End If
        Next
        GC.Collect()
    End Sub
    Private Sub BtnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUp.Click
        '''DataGridView1.DataSource = GridControl1.DataSource
        '''Dim dt As DataTable = DataGridView1.DataSource
        ''''Dim selectedrowID = GridView1.FocusedRowHandle    '("TT").row
        ''''Dim selectedrowID1 = DataGridView1.SelectedRows(0).Index
        ''''Dim dt As DataTable = GridControl1.DataSource

        ''''selectedrowIndex = selectedrow.row
        ''''Dim uperrow = GridView1.Selectedindex
        ''''Dim mygridview As DataView = TryCast(GridView1.DataSource, DataView)

        '''DataGridView1.BeginEdit(True)
        ''''dt.AcceptChanges()
        '''If DataGridView1.SelectedRows(0).Index > 0 Then
        ''''Tăng thứ tự của chỉ tiêu đằng trước    'Không thể sử dụng được selectedrowID - 1 mà phải dùng for để trừ giá trị của TT
        '''For i As Integer = 0 To GridView1.RowCount - 1
        '''If DataGridView1.Rows.Item(i).Cells("TT").Value = Convert.ToInt16(DataGridView1.SelectedRows(0).Cells("TT").Value) - 1 Then 'Convert.ToInt16(GridView1.GetRowCellValue(selectedrowID, "TT")) - 1 Then
        ''''GridView1.SetRowCellValue(i, "TT", Convert.ToInt16(GridView1.GetRowCellValue(selectedrowID, "TT")))
        '''DataGridView1.Rows.Item(i).Cells("TT").Value = Convert.ToInt16(DataGridView1.SelectedRows(0).Cells("TT").Value)
        '''End If
        ''''dt.Rows(selectedrowID - 1)("TT") = Convert.ToInt16(GridView1.GetRowCellValue(selectedrowID, "TT")) + 1
        '''Next
        ''''Giảm thứ tự của chỉ tiêu đang chọn
        ''''GridView1.SetFocusedRowCellValue("TT", Convert.ToInt16(GridView1.GetRowCellValue(selectedrowID, "TT")) - 1) '= Convert.ToInt16(dt.Rows(selectedrowID)("TT")) - 1   'dt.Rows(selectedrowID)("TT")) - 1
        '''DataGridView1.SelectedRows(0).Cells("TT").Value = Convert.ToInt16(DataGridView1.SelectedRows(0).Cells("TT").Value) - 1 '= Convert.ToInt16(dt.Rows(selectedrowID)("TT")) - 1   'dt.Rows(selectedrowID)("TT")) - 1

        '''End If
        '''DataGridView1.EndEdit()
        ''''dt.AcceptChanges()
        ''''Dim a As DataView = GridView1.DataSource
        '''Dim a = GridControl1.DataSource.getchanges()
        '''Dim b = DataGridView1.DataSource.getchanges()
        '''Dim c = dt.GetChanges
        ''''Dim myconect As MyCon = New MyCon
        ''''Static dt As DataTable = GridControl1.DataSource
        ''''my conect.Update_CSDL(dt, "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
        ''''My connect.Update_CSDL(GridControl1,   "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """" + " and TT = " + GridView1.GetFocusedRowCellValue("TT").ToString)
        '''Dim sucsess As Boolean = Myconnect.Update_CSDL(dt, "select * from Maindata")
        '''If sucsess <> True Then
        ''''MessageBox.Show("d")
        '''DataGridView1.DataSource = Myconnect.DtFromQry("select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
        '''GridControl1.DataSource = DataGridView1.DataSource
        '''End If
        '''GridView1.Columns("TT").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        '''GridView1.RefreshData()
        '''GridView1.DataSource.sort() = "TT"  'Cũng như dòng lệnh trên
        '++++++++++++++++++++
        ''''DataGridView1.DataSource = TryCast(GridControl1.DataSource, DataTable)
        ''''Dim dt As DataTable = GridControl1.DataSource
        ''''GridControl1.DataSource = Nothing
        ''''GridControl1.DataSource = Gridcontrol1_dt
        ''''GridControl1.
        ''''dt.DefaultView.Sort() = "TT"
        ''''GridView1.RefreshData()
        ''''GridView1.ValidateEditor()
        ''''Me.Refresh()
        ''''GridView1.Focus()
        ''''GridView1_RowClick(Nothing, Nothing)


        '''''''DataGridView1.DataSource = GridControl1.DataSource
        'Dim selectedrowID = GridView1.FocusedRowHandle    '("TT").row
        '''''''Dim selectedrowID1 = DataGridView1.SelectedRows(0).Index
        '''''''Dim dt As DataTable = GridControl1.DataSource

        '''''''selectedrowIndex = selectedrow.row
        '''''''Dim uperrow = GridView1.Selectedindex
        GridView1.BeginDataUpdate()
        Dim oldfocusedTT = GridView1.GetFocusedRowCellValue("TT") - 1      'GridView1.GetFocusedDataRow

        If GridView1.GetFocusedRowCellValue("TT") > 1 Then
            'Tăng thứ tự của chỉ tiêu đằng trước    'Không thể sử dụng được selectedrowID - 1 mà phải dùng for để trừ giá trị của TT
            For i As Integer = 0 To GridView1.RowCount - 1
                If GridView1.GetRowCellValue(i, "TT") = Convert.ToInt16(GridView1.GetFocusedRowCellValue("TT")) - 1 Then 'Convert.ToInt16(GridView1.GetRowCellValue(selectedrowID, "TT")) - 1 Then
                    'GridView1.SetRowCellValue(i, "TT", Convert.ToInt16(GridView1.GetRowCellValue(selectedrowID, "TT")))
                    GridView1.SetRowCellValue(i, "TT", Convert.ToInt16(GridView1.GetFocusedRowCellValue("TT")))
                End If

                'dt.Rows(selectedrowID - 1)("TT") = Convert.ToInt16(GridView1.GetRowCellValue(selectedrowID, "TT")) + 1
            Next
            'Giảm thứ tự của chỉ tiêu đang chọn
            'GridView1.SetFocusedRowCellValue("TT", Convert.ToInt16(GridView1.GetRowCellValue(selectedrowID, "TT")) - 1) '= Convert.ToInt16(dt.Rows(selectedrowID)("TT")) - 1   'dt.Rows(selectedrowID)("TT")) - 1
            GridView1.SetFocusedRowCellValue("TT", Convert.ToInt16(GridView1.GetFocusedRowCellValue("TT")) - 1) '= Convert.ToInt16(dt.Rows(selectedrowID)("TT")) - 1   'dt.Rows(selectedrowID)("TT")) - 1

        End If
        GridView1.EndDataUpdate()
        'Dim a As DataView = GridView1.DataSource
        Dim a = GridControl1.DataSource.getchanges()
        'Dim myconect As MyCon = New MyCon
        'Static dt As DataTable = GridControl1.DataSource
        'myconect.Update_CSDL(dt, "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
        'My connect.Update_CSDL(GridControl1,   "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """" + " and TT = " + GridView1.GetFocusedRowCellValue("TT").ToString)
        'Dim sucsess As Boolean = My connect.Update_CSDL(GridControl1,   "select * from Maindata")

        'Dim myConStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + Application.StartupPath + "\Nafosted_2.nft; Jet OLEDB:Database Password= abc"
        'Dim da As OleDbDataAdapter = New OleDbDataAdapter("select * from Maindata", myConStr)
        'Dim cb As OleDbCommandBuilder = New OleDbCommandBuilder(da)
        '=====================2 lệnh sau rất quan trọng, nếu ko sẽ ko update focused row
        'GridView1.PostEditor()
        'GridView1.UpdateCurrentRow()
        '=====================
        Dim success = Gridview1_Connect.Update_CSDL(GridControl1, "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
        'GridView1_Reload()
        'GridControl1.DataSource = Myconnect.DtFromQry("select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """" + " and Limit = 'Không giới hạn'")
        'If sucsess = True Then


        'Else
        '	'MessageBox.Show("d")
        '	Gridcontrol1.DataSource = Gridview1_Connect.DtFromQry("select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """" + " and (Limit = 'Không giới hạn' or Limit = 'Non-Limit')")
        'End If
        'Dim x As DataRowView = GridView1.GetFocusedRow

        GridView1.Columns("TT").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        'GridView1.Columns("TT").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        'DataGridView1.DataSource = TryCast(GridControl1.DataSource, DataTable)
        'Dim dt As DataTable = GridControl1.DataSource
        'GridControl1.DataSource = Nothing
        'GridControl1.DataSource = Gridcontrol1_dt
        'GridControl1.
        'dt.DefaultView.Sort() = "TT"
        'GridView1.RefreshData()
        'GridView1.ValidateEditor()
        BtnWeightCal_Click(Nothing, Nothing)
        Me.Refresh()
        'GridView1.Focus()
        'GridView1_RowClick(Nothing, Nothing)
        BtnUpdate.Enabled = True
    End Sub

    Private Sub BtnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDown.Click
        GridView1.BeginDataUpdate()
        Dim oldfocusedTT = GridView1.GetFocusedRowCellValue("TT") - 1      'GridView1.GetFocusedDataRow

        If GridView1.GetFocusedRowCellValue("TT") < GridView1.RowCount Then
            'Tăng thứ tự của chỉ tiêu đằng trước    'Không thể sử dụng được selectedrowID - 1 mà phải dùng for để trừ giá trị của TT
            For i As Integer = 0 To GridView1.RowCount - 1
                If GridView1.GetRowCellValue(i, "TT") = Convert.ToInt16(GridView1.GetFocusedRowCellValue("TT")) + 1 Then 'Convert.ToInt16(GridView1.GetRowCellValue(selectedrowID, "TT")) - 1 Then
                    GridView1.SetRowCellValue(i, "TT", Convert.ToInt16(GridView1.GetFocusedRowCellValue("TT")))
                End If

            Next
            'Giảm thứ tự của chỉ tiêu đang chọn
            GridView1.SetFocusedRowCellValue("TT", Convert.ToInt16(GridView1.GetFocusedRowCellValue("TT")) + 1) '= Convert.ToInt16(dt.Rows(selectedrowID)("TT")) - 1   'dt.Rows(selectedrowID)("TT")) - 1

        End If
        GridView1.EndDataUpdate()
        '=====================2 lệnh sau rất quan trọng, nếu ko sẽ ko update focused row
        'GridView1.PostEditor()
        'GridView1.UpdateCurrentRow()
        '=====================
        Dim success = Gridview1_Connect.Update_CSDL(GridControl1, "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
        'GridView1_Reload()
        'GridControl1.DataSource = Myconnect.DtFromQry("select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """" + " and Limit = 'Không giới hạn'")
        'If sucsess = True Then


        'Else
        ''MessageBox.Show("d")
        'GridControl1.DataSource = Myconnect.DtFromQry("select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """" + "AND Limit = 'Limit'")
        'End If
        GridView1.Columns("TT").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        'GridView1.Columns("TT").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        BtnWeightCal_Click(Nothing, Nothing)
        Me.Refresh()
        BtnUpdate.Enabled = True
    End Sub
    Private Sub mytxtMap_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'For i = 0 To GridView1.RowCount - 1
        Dim therowindex As Integer = sender.tag     'therowindex = i
        GridView1.GetDataRow(therowindex)("Map") = sender.text
        'Next

        GridView1.UpdateCurrentRow()
    End Sub
    Private Sub BtnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer = sender.tag
        Dim thetextbox As DevExpress.XtraEditors.TextEdit = FindControl(XtraScrollableControl1, "TxtMap" + i.ToString)
        Dim oldtext = thetextbox.Text
        thetextbox.Text = FillPath_byOpendialog(thetextbox.Text)
        Dim pathchuanhoa As String
        Dim FN As String = IO.Path.GetFileNameWithoutExtension(thetextbox.Text)
        Dim dir As String = IO.Path.GetDirectoryName(thetextbox.Text)
        If BarCheckItemComposite.Checked = True Then
            If thetextbox.Text.Substring(thetextbox.Text.Length - 3).ToUpper = "ADF" Then
                pathchuanhoa = thetextbox.Text.Replace("\HDR.ADF", "_ch.tif")
            Else
                pathchuanhoa = dir + "\" + FN + "_ch.tif"
            End If
        Else
            If thetextbox.Text.Substring(thetextbox.Text.Length - 3).ToUpper = "ADF" Then
                pathchuanhoa = thetextbox.Text.Replace("\HDR.ADF", ".tif")
            Else
                pathchuanhoa = dir + "\" + FN + ".tif"
            End If
        End If


        'Nếu ko thay đổi sang file khác thì thoát
        If thetextbox.Text = oldtext Then
            Return
        End If
        GridView1.GetDataRow(i)("Map") = thetextbox.Text        'Cẩn thận lỗi khi người dùng Sort???
        GridView1.GetDataRow(i)("MapChuanHoa") = pathchuanhoa
        GridView1.UpdateCurrentRow()

        Dim inputMap As Raster
        Dim PictureBoxInfo As DevExpress.XtraEditors.PictureEdit = FindControl(XtraScrollableControl1, "PictureBoxInfo" + i.ToString)
        PictureBoxInfo.ToolTip = Bandoloi
        Try
            inputMap = Raster.Open(thetextbox.Text)
            PictureBoxInfo.Image = My.Resources.Resources.apply_32x32
            PictureBoxInfo.ToolTip = filedulieutontai
            inputMap.Close()
            inputMap.Dispose()
            'Try     '==Chỉ -1 khi text cũ đã ko mở được. Nếu ko cứ browse ok lại -1 rồi lại browse thêm lần nữa lại -1 thì cứ âm mãi
            'Raster.Open(oldtext)

            'Catch ex As Exception
            'XtraTabPage4.Tag = Convert.ToInt16(XtraTabPage4.Tag) - 1

            'End Try


        Catch ex As Exception
            'Try     '==Chỉ +1 khi text cũ đã mở được. Nếu ko cứ browse...
            'Raster.Open(oldtext)
            'XtraTabPage4.Tag = Convert.ToInt16(XtraTabPage4.Tag) + 1
            'Catch 'ex As Exception

            'End Try

            'XtraTabPage4.PageEnabled = False
            PictureBoxInfo.Image = My.Resources.Resources.cancel_32x32
            PictureBoxInfo.ToolTip = Bandoloi
            'MessageBox.Show("Không mở được bản đồ chỉ tiêu thứ " + i.ToString, "")
        End Try
        'If Convert.ToInt16(XtraTabPage4.Tag) > 0 Then
        'XtraTabPage4.PageEnabled = False
        'XtraTabPage4.Tooltip = "Không thể kích hoạt, Thực hiện lại bước 3"
        'Else
        'XtraTabPage4.PageEnabled = True
        'XtraTabPage4.Tooltip = "Bước 4"
        'End If

        XtraTabPage_Step2.PageEnabled = True
        XtraTabPage_Step2.Tooltip = Buoc4
        XtraTabPage_Step2.Tag = 0
        For j As Integer = 0 To GridView1.RowCount - 1
            Dim Pic As DevExpress.XtraEditors.PictureEdit = FindControl(XtraScrollableControl1, "PictureBoxInfo" + j.ToString)

            If Pic.ToolTip.ToString = Bandoloi Or Pic.ToolTip.ToString = filedulieuKhongtontai Then
                'XtraTabPage4.PageEnabled = False
                'XtraTabPage4.Tooltip = Khongkichhoatbuoc3
                XtraTabPage_Step2.Tag = 1
                Return
            End If
        Next
        BtnUpdate.Enabled = True
    End Sub
    Private Sub mytxtMapchuanhoa_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'For i = 0 To GridView1.RowCount - 1
        Dim therowindex As Integer = sender.tag     'therowindex = i
        GridView1.GetDataRow(therowindex)("Mapchuanhoa") = sender.text
        'Next

        GridView1.UpdateCurrentRow()
    End Sub
    Private Sub BtnBrowsechuanhoa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer = sender.tag
        Dim thetextbox As DevExpress.XtraEditors.TextEdit = FindControl(XtraScrollableControl2, "TxtMapchuanhoa" + i.ToString)
        Dim oldtext = thetextbox.Text
        thetextbox.Text = FillPath_bySaveFiledialog(thetextbox.Text)
        'Nếu ko thay đổi sang file khác thì thoát
        If thetextbox.Text = oldtext Then
            Return
        End If
        GridView1.GetDataRow(i)("Mapchuanhoa") = thetextbox.Text
        GridView1.UpdateCurrentRow()

        Dim inputMap As Raster
        Dim PictureBoxInfo As DevExpress.XtraEditors.PictureEdit = FindControl(XtraScrollableControl2, "PictureBoxInfo" + i.ToString)
        PictureBoxInfo.ToolTip = Bandochuanhoaloi
        Try
            inputMap = Raster.Open(thetextbox.Text)

            'Try     '==Chỉ -1 khi text cũ đã ko mở được. Nếu ko cứ browse ok lại -1 rồi lại browse thêm lần nữa lại -1 thì cứ âm mãi
            'Raster.Open(oldtext)

            'Catch ex As Exception
            'XtraTabPage5.Tag = Convert.ToInt16(XtraTabPage5.Tag) - 1
            'End Try
            PictureBoxInfo.Image = My.Resources.Resources.apply_32x32
            PictureBoxInfo.ToolTip = filedulieutontai
            RasterDisp(inputMap, "")
            'inputMap.Dispose()
        Catch ex As Exception
            'Try     '==Chỉ +1 khi text cũ đã mở được. Nếu ko cứ browse...
            'Raster.Open(oldtext)
            'XtraTabPage5.Tag = Convert.ToInt16(XtraTabPage5.Tag) + 1
            'Catch 'ex As Exception

            'End Try

            'XtraTabPage5.PageEnabled = False
            PictureBoxInfo.Image = My.Resources.Resources.cancel_32x32
            PictureBoxInfo.ToolTip = Bandoloi
            'MessageBox.Show("Không mở được bản đồ chỉ tiêu thứ " + i.ToString, "")
        End Try
        'If Convert.ToInt16(XtraTabPage5.Tag) > 0 Then
        'XtraTabPage5.PageEnabled = False
        'XtraTabPage5.Tooltip = "Không thể kích hoạt, Thực hiện lại bước 5"
        'Else
        'XtraTabPage5.PageEnabled = True
        'XtraTabPage5.Tooltip = "Bước 5"
        'End If
        XtraTabPage_Step4.PageEnabled = True
        XtraTabPage_Step4.Tooltip = Buoc5
        XtraTabPage_Step4.Tag = 0
        For j As Integer = 0 To GridView1.RowCount - 1
            Dim Pic As DevExpress.XtraEditors.PictureEdit = FindControl(XtraScrollableControl2, "PictureBoxInfo" + j.ToString)
            If Pic.ToolTip = Bandochuanhoaloi Or Pic.ToolTip.ToString = filedulieuKhongtontai Then
                XtraTabPage_Step4.PageEnabled = False
                XtraTabPage_Step4.Tooltip = Khongkichhoatbuoc4
                XtraTabPage_Step4.Tag = 1
                Return
            End If
        Next

        BtnUpdate.Enabled = True
    End Sub
    Private Sub BtnRun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '=====================2 lệnh sau rất quan trọng, nếu ko sẽ ko update focused row
        'GridView1.PostEditor()
        'GridView1.UpdateCurrentRow()
        GC.Collect()
        '=====================
        Gridview1_Connect.Update_CSDL(GridControl1, "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
        'Gridview1_Reload()
        Dim i As Integer = sender.tag
        Dim btnview As DevExpress.XtraEditors.SimpleButton = FindControl(XtraScrollableControl2, "Btnview" + i.ToString)
        ''''Cẩn thận bị lỗi do người dùng sort datagridview
        ''''Dim bdochitieu As String = GridView1.GetDataRow(i)("Map").ToString()      'Lấy bản đồ nguồn chỉ tiêu để chạy ra file bản đồ chuẩn hóa
        Dim bdochitieu As String = GridView1.GetDataRow(i)("Map").ToString()        'FindControl(XtraScrollableControl1, "TxtMap" + i.ToString).Text '
        If Not File.Exists(bdochitieu) Then
            MessageBox.Show(Kiemtrachitieu, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim theoutputrasterFN As String = FindControl(XtraScrollableControl2, "TxtMapchuanhoa" + i.ToString).Text 'GridView1.GetDataRow(i)("Mapchuanhoa").ToString()
        If File.Exists(theoutputrasterFN) Then
            Dim result As DialogResult = MessageBox.Show(filetontai, thongbao, MessageBoxButtons.YesNo)
            If result = Windows.Forms.DialogResult.No Then
                Return
            Else
                Try
                    File.Delete(theoutputrasterFN)
                Catch ex As Exception
                    MessageBox.Show(Khongtheghide + vbNewLine + Filechuanhoakhac, thongbao)
                    Return
                End Try
            End If

        End If
        If theoutputrasterFN.Length > 4 Then
            If theoutputrasterFN.Substring(theoutputrasterFN.Length - 3).ToUpper <> "TIF" Then
                MessageBox.Show(nhapfiletif)
                Return
            End If
        Else
            MessageBox.Show(nhapfiletif)
            Return
        End If
        SplashScreenManager.ShowForm(GetType(FrmWaiting))
        Dim thefunction As String = GridView1.GetDataRow(i)("UsedFunction").ToString()
        Dim theinputraster As DotSpatial.Data.Raster
        Try
            theinputraster = Raster.Open(bdochitieu)
        Catch ex As Exception
            MessageBox.Show(Kiemtrachitieu, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        Dim outputraster As DotSpatial.Data.Raster
        If thefunction = aHams1 Then
            outputraster = hamS1(theinputraster, theoutputrasterFN, i)
        ElseIf thefunction = aHams2 Then
            outputraster = hamS2(theinputraster, theoutputrasterFN, i)
        ElseIf thefunction = aHamhinhthang Then
            outputraster = hamhinhthang(theinputraster, theoutputrasterFN, i)

        ElseIf thefunction = aHamkandel Then
            outputraster = hamKandel(theinputraster, theoutputrasterFN, i)
            'ElseIf thefunction = "Hàm Kandel 1" Then
            'outputraster = hamKandel1(theinputraster, theoutputraster, i)
            'ElseIf thefunction = "Hàm Kandel 2" Then
            ''outputraster = hamKandel2(theinputraster, theoutputraster, i)
        ElseIf thefunction = aHamtheoloai Then
            outputraster = hamtheoloai(theinputraster, theoutputrasterFN, i)
        Else
            MessageBox.Show("", "?")
        End If
        'btnview.Tag = outputraster
        outputraster.Close()
        outputraster.Dispose()
        'Xử lý pictureinfo
        Dim inputRaster As Raster
        Dim PictureBoxInfo As DevExpress.XtraEditors.PictureEdit = FindControl(XtraScrollableControl2, "PictureBoxInfo" + i.ToString)
        Try
            inputRaster = Raster.Open(theoutputrasterFN)

            PictureBoxInfo.Image = My.Resources.Resources.apply_32x32
            PictureBoxInfo.ToolTip = "OK"
            inputRaster.Close()
            inputRaster.Dispose()
        Catch ex As Exception
            PictureBoxInfo.Image = My.Resources.Resources.cancel_32x32
            PictureBoxInfo.ToolTip = Bandochuanhoaloi
            'MessageBox.Show("Không mở được bản đồ chỉ tiêu thứ " + i.ToString, "")
        End Try
        Try
            SplashScreenManager.CloseForm()
        Catch ex As Exception

        End Try

        'Xử lý enable Tabpage5
        For j As Integer = 0 To GridView1.RowCount - 1
            Dim Pic As DevExpress.XtraEditors.PictureEdit = FindControl(XtraScrollableControl2, "PictureBoxInfo" + i.ToString)
            If Pic.ToolTip = Bandoloi Then
                XtraTabPage_Step4.PageEnabled = False
                XtraTabPage_Step4.Tooltip = Khongkichhoatbuoc4
                XtraTabPage_Step4.Tag = 1
                Return

            End If
        Next
        'Nếu tiếp được đến đây có nghĩa là ko có PictureBoxInfo.Image nào = cancel
        XtraTabPage_Step4.PageEnabled = True
        XtraTabPage_Step4.Tooltip = Buoc5
        XtraTabPage_Step4.Tag = 0
        GC.Collect()
    End Sub
    Private Sub Btnview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim theRaster As Raster = sender.tag
        'Dim frmmapviewer = New MapViewer.Form1
        'Dim a = theRaster.Filename
        GC.Collect()

        Dim therasterFN As String = FindControl(XtraScrollableControl1, "TxtMap" + sender.tag.ToString).Text 'GridView1.GetDataRow(i)("Mapchuanhoa").ToString()
        Dim inpRaster As IRaster

        Try
            SplashScreenManager.ShowForm(GetType(FrmWaiting))
            Try
                inpRaster = Raster.Open(therasterFN)
            Catch ex As Exception
                MessageBox.Show(Khongmofile + therasterFN, Loibando)
                Return

            End Try
            Dim tlayer As IMapRasterLayer = frmMapviewer.Map1.Layers.Add(inpRaster)
            'Dim tlayer As IMapRasterLayer = frmMapviewer.Map1.AddLayer(therasterFN)
            'RasterLayersymbology(tlayer)

            frmMapviewer.Show()
            'frmMapviewer.BringToFront()
            'Application.OpenForms("frmMapviewer").BringToFront()
            frmMapviewer.Activate()
            frmMapviewer.TopLevel = True
            Try
                SplashScreenManager.CloseForm()
            Catch

            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Try
                SplashScreenManager.CloseForm()
            Catch

            End Try

        End Try

    End Sub
    Private Sub BtnviewChuanhoa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim theRaster As Raster = sender.tag
        'Dim frmmapviewer = New MapViewer.Form1
        'Dim a = theRaster.Filename
        GC.Collect()

        Dim therasterFN As String = FindControl(XtraScrollableControl2, "TxtMapchuanhoa" + sender.tag.ToString).Text 'GridView1.GetDataRow(i)("Mapchuanhoa").ToString()
        Dim inpRaster As IRaster

        Try
            SplashScreenManager.ShowForm(GetType(FrmWaiting))
            Try
                inpRaster = Raster.Open(therasterFN)
            Catch ex As Exception
                MessageBox.Show(Khongmofile + therasterFN, Loibando)
                Return

            End Try
            Dim tlayer As IMapRasterLayer = frmMapviewer.Map1.Layers.Add(inpRaster)
            'Dim tlayer As IMapRasterLayer = frmMapviewer.Map1.AddLayer(therasterFN)
            RasterLayersymbology(tlayer)

            frmMapviewer.Show()
            'frmMapviewer.BringToFront()
            'Application.OpenForms("frmMapviewer").BringToFront()
            frmMapviewer.Activate()
            frmMapviewer.TopLevel = True
            Try
                SplashScreenManager.CloseForm()
            Catch

            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Try
                SplashScreenManager.CloseForm()
            Catch

            End Try

        End Try

    End Sub
    Private Sub SimpleButton1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        XtraTabControl1_SelectedPageChanged(Nothing, Nothing)
    End Sub
    Public Shared Function FindControl(ByVal root As Control, ByVal target As String) As Control
        If root.Name.Equals(target) Then
            Return root
        End If
        For i As Integer = 0 To root.Controls.Count - 1
            If root.Controls(i).Name.Equals(target) Then
                Return root.Controls(i)
            End If
        Next
        For i As Integer = 0 To root.Controls.Count - 1
            Dim result As Control
            For k As Integer = 0 To root.Controls(i).Controls.Count - 1
                result = FindControl(root.Controls(i).Controls(k), target)
                If result IsNot Nothing Then
                    Return result
                End If
            Next
        Next
        Return Nothing
    End Function

    'Private Sub XtraTabPage2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles XtraTabPage2.GotFocus
    'Dim a = 3
    'End Sub

    'Private Sub XtraTabPage2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles XtraTabPage2.Leave
    'Dim a = 0
    ''If XtraTabControl1.SelectedTabPageIndex = 0 Or XtraTabControl1.SelectedTabPageIndex = 2 Then
    ''End If
    'If XtraTabPage1.Focused = True Or XtraTabPage3.Focused = True Then  'Nếu click sang tab1 hoặc tab 3 sau khi rời tab2 thì chạy



    'BtnUpdate_click(Nothing, Nothing)

    'End If
    'End Sub

    Private Sub XtraTabPage2_TabIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles XtraTabPage3.TabIndexChanged

    End Sub



    Private Sub BtnChonCSDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnChonCSDL.Click
        Dim f As New OpenFileDialog
        f.Filter = projectDGTN
        f.Title = OpenProject
        Dim FullFileName As String = ""
        If f.ShowDialog() = DialogResult.OK Then
            FullFileName = f.FileName
            'MnuFileHientai.ToolTipText = FullFileName
            If FullFileName.Length > 63 Then
                MnuFileHientai.Text = "..." + FullFileName.Substring(FullFileName.Length - 35, 35)
                GroupControl2.Text = FullFileName.Substring(0, 10) + "..." + FullFileName.Substring(FullFileName.Length - 53, 53)
            Else
                GroupControl2.Text = FullFileName
                MnuFileHientai.Text = FullFileName
            End If
            Dim sWriter As StreamWriter = New StreamWriter(Application.StartupPath + "/myFile.dll", False, System.Text.Encoding.UTF8)   'myFile.dll là file chứa đường dẫn tới dataFile tại dòng 1

            sWriter.WriteLine(FullFileName)
            sWriter.Flush()
            sWriter.Close()
            ''LoadMainForm()
            '================Thay đổi đường dẫn cho Data Resource Trong trường hợp dùng dataset...
            ''Dim St As String = IO.File.ReadAllText("C:\Test.txt") 'Ð?c h?t file
            'Dim St() As String = IO.File.ReadAllLines(Application.StartupPath + "/LocalKnowledge.exe.config") 'Ð?c h?t các dòng c?a file
            ''.File.WriteAllText("C:\Test.txt", St) 'Ghi chu?i St vào file
            'Dim mypass = "abc123xyz"
            'St(6) = "connectionString=""Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + FullFileName + "; Jet OLEDB:Database Password= " + mypass + ""
            'IO.File.WriteAllLines(Application.StartupPath + "/LocalKnowledge.exe.config", St) 'Ghi m?ng String St vào file, m?i ph?n t? là 1 dòng
            Gridview1_Reload()
            Dim listbox1Qry As String = "select distinct Obj from Maindata"
            Gridview1_Connect = New myADOclass
            Dim dt As DataTable = Gridview1_Connect.DtFromQry(listbox1Qry)
            ListBoxControl1.Items.Clear()
            For i = 0 To dt.Rows.Count - 1
                ListBoxControl1.Items.Add(dt.Rows(i)("Obj"))
            Next

            'Dim mylist = (From row In dt.AsEnumerable()
            'Select row.Field(Of String)("Obj")).Distinct().ToList()
            'ListBoxControl1.DataSource = mylist

            ListBoxControl1.Items.Add(Taocaymoi)
            ''====Initial lại cho startup form
            ''FrmStartup.chayListboxControl1_selectedIndexChanged = False
            'Dim Qry As String = "select distinct Obj from Maindata"
            'Dim con As MyCon = New MyCon
            ''FrmStartup.ListBoxControl1.DataSource = con.DtFromQry(Qry)
            'DataGridView1.DataSource = con.DtFromQry(Qry)
            ''FrmStartup.chayListboxControl1_selectedIndexChanged = True
            ''FrmStartup.ListBoxControl1.DisplayMember = "Obj"
            ''FrmStartup.Show()
            ''FrmStartup.Focus()
        End If
    End Sub
    Function ChonvaLuuCSDL(theTitle As String) As String
        Dim f As New OpenFileDialog
        f.Filter = projectDGTN
        f.Title = theTitle
        Dim FullFileName As String = ""
        If f.ShowDialog() = DialogResult.OK Then
            FullFileName = f.FileName
            'MnuFileHientai.ToolTipText = FullFileName
            If FullFileName.Length > 63 Then
                MnuFileHientai.Text = "..." + FullFileName.Substring(FullFileName.Length - 35, 35)
                GroupControl2.Text = FullFileName.Substring(0, 10) + "..." + FullFileName.Substring(FullFileName.Length - 53, 53)
            Else
                GroupControl2.Text = FullFileName
                MnuFileHientai.Text = FullFileName
            End If




            Dim sWriter As StreamWriter = New StreamWriter(Application.StartupPath + "/myFile.dll", False, System.Text.Encoding.UTF8)   'myFile.dll là file chứa đường dẫn tới dataFile tại dòng 1

            sWriter.WriteLine(FullFileName)
            sWriter.Flush()
            sWriter.Close()
            ''LoadMainForm()
            '================Thay đổi đường dẫn cho Data Resource Trong trường hợp dùng dataset...
            ''Dim St As String = IO.File.ReadAllText("C:\Test.txt") 'Ð?c h?t file
            'Dim St() As String = IO.File.ReadAllLines(Application.StartupPath + "/LocalKnowledge.exe.config") 'Ð?c h?t các dòng c?a file
            ''.File.WriteAllText("C:\Test.txt", St) 'Ghi chu?i St vào file
            'Dim mypass = "abc123xyz"
            'St(6) = "connectionString=""Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + FullFileName + "; Jet OLEDB:Database Password= " + mypass + ""
            'IO.File.WriteAllLines(Application.StartupPath + "/LocalKnowledge.exe.config", St) 'Ghi m?ng String St vào file, m?i ph?n t? là 1 dòng
            Gridview1_Reload()
            Dim listbox1Qry As String = "select distinct Obj from Maindata"
            Gridview1_Connect = New myADOclass
            Dim dt As DataTable = Gridview1_Connect.DtFromQry(listbox1Qry)
            ListBoxControl1.Items.Clear()
            For i = 0 To dt.Rows.Count - 1
                ListBoxControl1.Items.Add(dt.Rows(i)("Obj"))
            Next

            'Dim mylist = (From row In dt.AsEnumerable()
            'Select row.Field(Of String)("Obj")).Distinct().ToList()
            'ListBoxControl1.DataSource = mylist

            ListBoxControl1.Items.Add(Taocaymoi)
            ''====Initial lại cho startup form
            ''FrmStartup.chayListboxControl1_selectedIndexChanged = False
            'Dim Qry As String = "select distinct Obj from Maindata"
            'Dim con As MyCon = New MyCon
            ''FrmStartup.ListBoxControl1.DataSource = con.DtFromQry(Qry)
            'DataGridView1.DataSource = con.DtFromQry(Qry)
            ''FrmStartup.chayListboxControl1_selectedIndexChanged = True
            ''FrmStartup.ListBoxControl1.DisplayMember = "Obj"
            ''FrmStartup.Show()
            ''FrmStartup.Focus()
        Else

            'Dim az As DialogResult = MessageBox.Show(haychonproject, Chonproject, MessageBoxButtons.RetryCancel, MessageBoxIcon.Question)
            'If az = Windows.Forms.DialogResult.Cancel Then
            ''Application.Exit()
            'Me.Close()
            'FrmStart0.Show()
            'Else
            ''FullFileName = ""
            'ChonvaLuuCSDL(Chonproject)
            'End If

        End If
        Return FullFileName
    End Function

    Private Sub BtnLuuCSDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLuuCSDL.Click

    End Sub
    Private Sub BarButtonBckup_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonBckup.ItemClick
        Dim f As New SaveFileDialog
        f.Title = SavePrj
        f.Filter = "nft data |*.nft"
        Dim IO_Reader_Writer As New IO_Reader_Writer
        Dim Curdata As String = IO_Reader_Writer.doc1dong(Application.StartupPath + "\myFile.dll", 1)         'sourceFile
        Dim TargetFile As String = ""

        If f.ShowDialog() = DialogResult.OK Then

            TargetFile = f.FileName
            System.IO.File.Copy(Curdata, TargetFile, True)
            Status1.Caption = SavedPrj
        End If
    End Sub
#Region "======Xuất dữ liệu======"
    Private Sub BtnXuatDL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnXuatDL.Click

    End Sub
    Private Sub BarButtonexport_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonexport.ItemClick
        GridviewExport(GridView1)
    End Sub
    Private Sub GridviewExport(ByVal agridview As GridView)
        Dim saveDialog As New SaveFileDialog()
        saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010)(.xlsx)|*.XLSX|RichText File (.rtf)|*.rtf|Pdf File|*.pdf|Html File (.html)|*.html"
        saveDialog.Title = savetoExcelTitle
        'OpenFileDialog.Filter = "Tất các files|*.*|Doc Files|*.DOC;*.DOCX|Excel Files|*.XLS;*.XLSX|Pdf Files|*.PDF"
        If saveDialog.ShowDialog() <> DialogResult.Cancel Then
            Dim exportFilePath As String = saveDialog.FileName
            Dim fileExtenstion As String = New FileInfo(exportFilePath).Extension
            'Dim imageExporter As NImageExporter = chartControl.ImageExporter

            With agridview
                '.OptionsPrint.PrintDetails = True
                '.OptionsPrint.ExpandAllDetails = True
                .OptionsPrint.AutoWidth = False

            End With
            Try
                Select Case fileExtenstion
                    Case ".xls"

                        agridview.ExportToXls(exportFilePath)
                        Exit Select
                    Case ".xlsx"
                        agridview.ExportToXlsx(exportFilePath)
                        Exit Select
                    Case ".rtf"
                        agridview.ExportToRtf(exportFilePath)
                        Exit Select
                    Case ".pdf"
                        agridview.ExportToPdf(exportFilePath)
                        Exit Select
                    Case ".html"
                        agridview.ExportToHtml(exportFilePath)
                        Exit Select
                    Case ".mht"
                        agridview.ExportToMht(exportFilePath)
                        Exit Select
                    Case Else
                        Exit Select

                End Select

                System.Diagnostics.Process.Start(exportFilePath)
            Catch ex As Exception
                MessageBox.Show(filedichdangmo, khongthexuat)
            End Try

            'Dim pdfExport As New Process()
            ''pdfExport.StartInfo.FileName = "AcroRd32.exe"
            'pdfExport.StartInfo.Arguments = "exportFilePath"
            'pdfExport.Start()
        End If

        'End Using
    End Sub
    Private Sub GridcontrolExport(ByVal agridcontrol As DevExpress.XtraGrid.GridControl)
        Using saveDialog As New SaveFileDialog()
            saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html"
            If saveDialog.ShowDialog() <> DialogResult.Cancel Then
                Dim exportFilePath As String = saveDialog.FileName
                Dim fileExtenstion As String = New FileInfo(exportFilePath).Extension
                'Dim imageExporter As NImageExporter = chartControl.ImageExporter
                Try

                    Select Case fileExtenstion
                        Case ".xls"
                            agridcontrol.ExportToXls(exportFilePath)
                            Exit Select
                        Case ".xlsx"
                            agridcontrol.ExportToXlsx(exportFilePath)
                            Exit Select
                        Case ".rtf"
                            agridcontrol.ExportToRtf(exportFilePath)
                            Exit Select
                        Case ".pdf"
                            agridcontrol.ExportToPdf(exportFilePath)
                            Exit Select
                        Case ".html"
                            agridcontrol.ExportToHtml(exportFilePath)
                            Exit Select
                        Case ".mht"
                            agridcontrol.ExportToMht(exportFilePath)
                            Exit Select
                        Case Else
                            Exit Select

                    End Select
                    System.Diagnostics.Process.Start(exportFilePath)
                Catch ex As Exception
                    MessageBox.Show(filedichdangmo, khongthexuat)
                End Try
            End If
        End Using
    End Sub
#End Region
#Region "Các hàm"
    Function hamS(ByVal IndRaster As IRaster, ByVal outputFN As String, ByVal TTchitieu As Integer) As IRaster
        'hamS = New Raster()
        'Dim rasteroptions As String()
        'hamS = Raster.CreateRaster(outputFN, Nothing, IndRaster.NumColumns, IndRaster.NumRows, 1, IndRaster.DataType, rasteroptions)

        'With hamS
        '.Filename = outputFN
        '.Bounds = IndRaster.Bounds
        '.NoDataValue = -9999
        '.Projection = IndRaster.Projection

        '''.NumRowsInFile = IndRaster.NumRows
        '''.NumColumns = IndRaster.NumColumns

        'End With
        ''hamS = IndRaster
        ''Dim c = hamS.Value.Item(50, 50)
        'For i As Integer = 0 To IndRaster.NumRows - 1
        'For j As Integer = 0 To IndRaster.NumColumns - 1
        ''hamS.Value(i, j) = IndRaster.Value(i, j)
        'If IndRaster.Value(i, j) <> IndRaster.NoDataValue AND IndRaster.Value(i, j) >-9999  Then
        'If IndRaster.Value(i, j) <> IndRaster.NoDataValue AND IndRaster.Value(i, j) >-9999  Then

        'End If
        'hamS.Value(i, j) = IndRaster.Value(i, j) * 2
        'End If
        'Next
        'Next
        'hamS.Save()
        'MessageBox.Show(Tinhtoanthanhcong)

    End Function
    Function hamhinhthang(ByVal IndRaster As IRaster, ByVal outputFN As String, ByVal TTchitieu As Integer) As IRaster
        'hamS = New Raster()
        Dim a As Single
        Dim b As Single
        Dim c As Single
        Dim d As Single
        Try
            '===Đối với hàm hình thang, không thể thiếu b hoặc c (Phải có đủ cả b và c)
            '===Nếu thiếu a thì đặt a = - vô cùng (-99999) nếu thiếu d thì đặt d = +99999
            If GridView1.GetDataRow(TTchitieu)("b").ToString = "" Or GridView1.GetDataRow(TTchitieu)("c").ToString = "" Then
                MessageBox.Show(giatribc + vbNewLine + trogiuphinhthang, loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                PicCheckHam.Image = My.Resources.Resources.cancel_32x32
                PicCheckHam.ToolTip = giatribc + vbNewLine + trogiuphinhthang
                Txt_B.Focus()
                Return Nothing
            End If

            b = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("b").Replace(",", ""))  'Convert.ToSingle(txtToiUu1.Text.Replace(",", ""))
            c = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("c").Replace(",", ""))  'Convert.ToSingle(txtToiUu2.Text.Replace(",", "")) Then


            If GridView1.GetDataRow(TTchitieu)("a").ToString = "" Then
                a = -99999
            Else
                a = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("a").ToString.Replace(",", ""))  'Convert.ToSingle(txtNO1.Text.Replace(",", ""))
            End If

            If GridView1.GetDataRow(TTchitieu)("d").ToString = "" Then
                d = 99999
            Else
                d = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("d").ToString.Replace(",", ""))     'Convert.ToSingle(txtNO2.Text.Replace(",", ""))
                'd = Convert.t
            End If


            Dim rasteroptions As String()
            Dim datatype As System.Type = System.Type.GetType("System.Single")
            hamhinhthang = Raster.CreateRaster(outputFN, Nothing, IndRaster.NumColumns, IndRaster.NumRows, 1, datatype, rasteroptions)

            With hamhinhthang
                .Filename = outputFN
                .Bounds = IndRaster.Bounds
                .NoDataValue = -9999
                .Projection = IndRaster.Projection

                ''.NumRowsInFile = IndRaster.NumRows
                ''.NumColumns = IndRaster.NumColumns

            End With
            'hamS = IndRaster
            'Dim c = hamS.Value.Item(50, 50)
            For i As Integer = 0 To IndRaster.NumRows - 1
                For j As Integer = 0 To IndRaster.NumColumns - 1
                    'hamS.Value(i, j) = IndRaster.Value(i, j)
                    If IndRaster.Value(i, j) <> IndRaster.NoDataValue And IndRaster.Value(i, j) > -9999 Then
                        If IndRaster.Value(i, j) <= a Or IndRaster.Value(i, j) >= d Then
                            hamhinhthang.Value(i, j) = 0
                        ElseIf IndRaster.Value(i, j) > a And IndRaster.Value(i, j) < b Then
                            hamhinhthang.Value(i, j) = Math.Round((IndRaster.Value(i, j) - a) / (b - a), 2)

                        ElseIf IndRaster.Value(i, j) >= b And IndRaster.Value(i, j) <= c Then
                            hamhinhthang.Value(i, j) = 1

                        ElseIf IndRaster.Value(i, j) > c And IndRaster.Value(i, j) < d Then
                            hamhinhthang.Value(i, j) = Math.Round(1 - Math.Round((IndRaster.Value(i, j) - c) / (d - c), 2), 2)
                        Else 'x>=d

                            MessageBox.Show("Lỗi gì đó, ")
                        End If
                    Else
                        hamhinhthang.Value(i, j) = -9999
                    End If
                Next
            Next
            hamhinhthang.Save()
            hamhinhthang.GetStatistics()
            hamhinhthang.GetUniqueValues()

            Status1.Caption = Tinhtoanthanhcong
            'MessageBox.Show(Tinhtoanthanhcong)
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            MessageBox.Show(loi, Khongtheghide + vbNewLine + Filechuanhoakhac)
        End Try
    End Function
    Function hamKandel(ByVal IndRaster As IRaster, ByVal outputFN As String, ByVal TTchitieu As Integer) As IRaster

        Dim a As Single
        Dim b As Single
        Try
            '===Đối với hàm Kandel, không thể thiếu cùng lúc a và b (Có thể thiếu 1 trong 2)
            '===Nếu thiếu a thì đặt a = - vô cùng (-99999) nếu thiếu d thì đặt d = +99999
            If GridView1.GetDataRow(TTchitieu)("a").ToString = "" And GridView1.GetDataRow(TTchitieu)("b").ToString = "" Then
                MessageBox.Show(giatriab + vbNewLine + trogiupkandel, loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                PicCheckHam.Image = My.Resources.Resources.cancel_32x32
                PicCheckHam.ToolTip = giatriab + vbNewLine + trogiupkandel
                Txt_A.Focus()
                Return Nothing
            End If

            If GridView1.GetDataRow(TTchitieu)("a").ToString = "" Then
                a = -99999
            ElseIf GridView1.GetDataRow(TTchitieu)("a").ToString <> "" Then
                a = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("a").ToString.Replace(",", ""))  'Convert.ToSingle(txtToiUu1.Text.Replace(",", ""))
            End If

            If GridView1.GetDataRow(TTchitieu)("b").ToString = "" Then
                b = 99999
            ElseIf GridView1.GetDataRow(TTchitieu)("b").ToString <> "" Then
                b = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("b").Replace(",", ""))  'Convert.ToSingle(txtToiUu2.Text.Replace(",", "")) Then

            End If


            Dim rasteroptions As String()
            Dim datatype As System.Type = System.Type.GetType("System.Single")
            hamKandel = Raster.CreateRaster(outputFN, Nothing, IndRaster.NumColumns, IndRaster.NumRows, 1, datatype, rasteroptions)

            With hamKandel
                .Filename = outputFN
                .Bounds = IndRaster.Bounds
                .NoDataValue = -9999
                .Projection = IndRaster.Projection

                ''.NumRowsInFile = IndRaster.NumRows
                ''.NumColumns = IndRaster.NumColumns

            End With
            'hamS = IndRaster
            'Dim c = hamS.Value.Item(50, 50)
            For i As Integer = 0 To IndRaster.NumRows - 1
                For j As Integer = 0 To IndRaster.NumColumns - 1
                    'hamS.Value(i, j) = IndRaster.Value(i, j)
                    If IndRaster.Value(i, j) <> IndRaster.NoDataValue And IndRaster.Value(i, j) > -9999 Then
                        If IndRaster.Value(i, j) < a Then
                            hamKandel.Value(i, j) = Math.Round(1 / (1 + Math.Pow((IndRaster.Value(i, j) - a) / (a - 0.5), 2)), 2)
                        ElseIf IndRaster.Value(i, j) >= a And IndRaster.Value(i, j) <= b Then
                            hamKandel.Value(i, j) = 1
                        ElseIf IndRaster.Value(i, j) > b Then
                            hamKandel.Value(i, j) = Math.Round(1 / (1 + Math.Pow((IndRaster.Value(i, j) - b) / (b - 0.5), 2)), 2)
                        End If
                    Else
                        hamKandel.Value(i, j) = -9999

                    End If
                Next
            Next
            hamKandel.Save()
            hamKandel.GetStatistics()
            hamKandel.GetUniqueValues()
            Status1.Caption = Tinhtoanthanhcong
            'MessageBox.Show(Tinhtoanthanhcong)
        Catch ex As Exception
            MessageBox.Show(loi, Khongtheghide + vbNewLine + Filechuanhoakhac)
            'MessageBox.Show(ex.Message)
        End Try
    End Function
    Function hamKandel1(ByVal IndRaster As IRaster, ByVal outputFN As String, ByVal TTchitieu As Integer) As IRaster
        ''=====2 ô Ko TNghi ="" và 1 ô tối ưu  S1 được điền thì => Hàm Kandel 1  (TH X>S1 ; X >b1 thì tối ưu, ngược lại thì dùng phân số)
        'Dim S1 As Single = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("a").tostring.Replace(",", ""))     'Convert.ToSingle(txtToiUu1.Text.Replace(",", ""))
        ''Dim S2 As Single = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("S2").tostring.Replace(",", ""))     'Convert.ToSingle(txtToiUu2.Text.Replace(",", ""))
        'Dim rasteroptions As String()
        'Dim datatype As System.Type = System.Type.GetType("System.Single")
        'hamKandel1 = Raster.CreateRaster(outputFN, Nothing, IndRaster.NumColumns, IndRaster.NumRows, 1, datatype, rasteroptions)

        'With hamKandel1
        '.Filename = outputFN
        '.Bounds = IndRaster.Bounds
        '.NoDataValue = -9999
        '.Projection = IndRaster.Projection

        '''.NumRowsInFile = IndRaster.NumRows
        '''.NumColumns = IndRaster.NumColumns

        'End With
        ''hamS = IndRaster
        ''Dim c = hamS.Value.Item(50, 50)
        'For i As Integer = 0 To IndRaster.NumRows - 1
        'For j As Integer = 0 To IndRaster.NumColumns - 1
        ''hamS.Value(i, j) = IndRaster.Value(i, j)
        'If IndRaster.Value(i, j) <> IndRaster.NoDataValue AND IndRaster.Value(i, j) >-9999  Then
        'If IndRaster.Value(i, j) >= S1 Then
        'hamKandel1.Value(i, j) = 1
        'Else
        'hamKandel1.Value(i, j) = 1 / (1 + Math.Pow((IndRaster.Value(i, j) - S1) / (S1 - 0.5), 2))
        'End If
        'End If
        'Next
        'Next
        'hamKandel1.Save()
        'MessageBox.Show(Tinhtoanthanhcong)

    End Function
    Function hamKandel2(ByVal IndRaster As IRaster, ByVal outputFN As String, ByVal TTchitieu As Integer) As IRaster
        ''=====2 ô Ko TNghi ="" và 1 ô tối ưu  S2  được điền thì => Hàm Kandel 1  (TH X<S2 ; X <b2 thì tối ưu, ngược lại (>b2) thì dùng phân số)
        ''Dim S1 As Integer = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("S1").Replace(",", ""))     'Convert.ToSingle(txtToiUu1.Text.Replace(",", ""))
        'Dim S2 As Single = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("b").Replace(",", ""))     'Convert.ToSingle(txtToiUu2.Text.Replace(",", ""))
        'Dim rasteroptions As String()
        'Dim datatype As System.Type = System.Type.GetType("System.Single")
        'hamKandel2 = Raster.CreateRaster(outputFN, Nothing, IndRaster.NumColumns, IndRaster.NumRows, 1, datatype, rasteroptions)

        'With hamKandel2
        '.Filename = outputFN
        '.Bounds = IndRaster.Bounds
        '.NoDataValue = -9999
        '.Projection = IndRaster.Projection

        '''.NumRowsInFile = IndRaster.NumRows
        '''.NumColumns = IndRaster.NumColumns

        'End With
        ''hamS = IndRaster
        ''Dim c = hamS.Value.Item(50, 50)
        'For i As Integer = 0 To IndRaster.NumRows - 1
        'For j As Integer = 0 To IndRaster.NumColumns - 1
        ''hamS.Value(i, j) = IndRaster.Value(i, j)
        'If IndRaster.Value(i, j) <> IndRaster.NoDataValue AND IndRaster.Value(i, j) >-9999  Then
        'If IndRaster.Value(i, j) > S2 Then
        'hamKandel2.Value(i, j) = 1 / (1 + Math.Pow((IndRaster.Value(i, j) - S2) / (S2 - 0.5), 2))
        'Else
        'hamKandel2.Value(i, j) = 1

        'End If

        'End If
        'Next
        'Next
        'hamKandel2.Save()
        'MessageBox.Show(Tinhtoanthanhcong)

    End Function
    Function hamS1(ByVal IndRaster As IRaster, ByVal outputFN As String, ByVal TTchitieu As Integer) As IRaster
        Dim a As Single
        Dim b As Single
        Dim z As Single
        Try
            '===Đối với hàm S, không thể thiếu a, hoặc b
            '===
            If GridView1.GetDataRow(TTchitieu)("a").ToString = "" Or GridView1.GetDataRow(TTchitieu)("b").ToString = "" Then
                MessageBox.Show(giatriab + vbNewLine + trogiupS, loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                PicCheckHam.Image = My.Resources.Resources.cancel_32x32
                PicCheckHam.ToolTip = giatriab + vbNewLine + trogiupS
                Txt_A.Focus()
                Return Nothing
            End If
            a = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("a").ToString.Replace(",", ""))  'Convert.ToSingle(txtToiUu1.Text.Replace(",", ""))
            b = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("b").ToString.Replace(",", ""))  'Convert.ToSingle(txtToiUu2.Text.Replace(",", "")) Then
            'GridView1.GetDataRow(TTchitieu)("b").tostring = "" Then
            z = (a + b) / 2
            'Else
            'z = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("z").Replace(",", ""))  'Convert.ToSingle(txtNO1.Text.Replace(",", ""))
            'End If


            'hamS = New Raster()
            'Dim a As Single = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("a").Replace(",", ""))          'Convert.ToSingle(txtNO1.Text.Replace(",", ""))
            'Dim b As Single = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("b").Replace(",", ""))          'Convert.ToSingle(TxtTN2.Text.Replace(",", ""))
            'Dim c As Single = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("c").Replace(",", ""))              'Convert.ToSingle(TxtRTN2.Text.Replace(",", ""))
            Dim rasteroptions As String()
            Dim datatype As System.Type = System.Type.GetType("System.Single")
            hamS1 = Raster.CreateRaster(outputFN, Nothing, IndRaster.NumColumns, IndRaster.NumRows, 1, datatype, rasteroptions)

            With hamS1
                .Filename = outputFN
                .Bounds = IndRaster.Bounds
                .NoDataValue = -9999
                .Projection = IndRaster.Projection

                ''.NumRowsInFile = IndRaster.NumRows
                ''.NumColumns = IndRaster.NumColumns

            End With
            'hamS = IndRaster
            'Dim c = hamS.Value.Item(50, 50)
            For i As Integer = 0 To IndRaster.NumRows - 1
                For j As Integer = 0 To IndRaster.NumColumns - 1
                    'hamS.Value(i, j) = IndRaster.Value(i, j)
                    If IndRaster.Value(i, j) <> IndRaster.NoDataValue And IndRaster.Value(i, j) > -9999 Then
                        If IndRaster.Value(i, j) < a Then
                            hamS1.Value(i, j) = 0
                        ElseIf IndRaster.Value(i, j) >= a And IndRaster.Value(i, j) < z Then
                            hamS1.Value(i, j) = Math.Round(2.0 * Math.Pow((IndRaster.Value(i, j) - a) / (b - a), 2), 2)

                        ElseIf IndRaster.Value(i, j) >= z And IndRaster.Value(i, j) <= b Then
                            hamS1.Value(i, j) = Math.Round(1 - 2.0 * Math.Pow((IndRaster.Value(i, j) - b) / (b - a), 2), 2)
                            'Dim ass = 1
                        Else 'x>c

                            hamS1.Value(i, j) = 1
                        End If
                    Else
                        hamS1.Value(i, j) = -9999
                    End If
                Next
            Next
            hamS1.Save()
            hamS1.GetStatistics()
            hamS1.GetUniqueValues()
            Status1.Caption = Tinhtoanthanhcong
            'MessageBox.Show(Tinhtoanthanhcong)
        Catch ex As Exception
            MessageBox.Show(loi, Khongtheghide + vbNewLine + Filechuanhoakhac)
            'MessageBox.Show(ex.Message)
        End Try

    End Function
    Function hamS2(ByVal IndRaster As IRaster, ByVal outputFN As String, ByVal TTchitieu As Integer) As IRaster
        Dim a As Single
        Dim b As Single
        Dim z As Single
        Try
            '===Đối với hàm S, không thể thiếu a, hoặc b
            '===
            If GridView1.GetDataRow(TTchitieu)("a").ToString = "" Or GridView1.GetDataRow(TTchitieu)("b").ToString = "" Then
                MessageBox.Show(giatriab + vbNewLine + trogiupS, loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                PicCheckHam.Image = My.Resources.Resources.cancel_32x32
                PicCheckHam.ToolTip = giatriab + vbNewLine + trogiupS
                Txt_A.Focus()
                Return Nothing
            End If

            a = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("a").ToString.Replace(",", ""))  'Convert.ToSingle(txtToiUu1.Text.Replace(",", ""))
            b = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("b").ToString.Replace(",", ""))  'Convert.ToSingle(txtToiUu2.Text.Replace(",", "")) Then
            'If GridView1.GetDataRow(TTchitieu)("b").tostring = "" Then
            z = (a + b) / 2
            'Else
            'b = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("b").Replace(",", ""))  'Convert.ToSingle(txtNO1.Text.Replace(",", ""))
            'End If

            Dim rasteroptions As String()
            Dim datatype As System.Type = System.Type.GetType("System.Single")
            hamS2 = Raster.CreateRaster(outputFN, Nothing, IndRaster.NumColumns, IndRaster.NumRows, 1, datatype, rasteroptions)

            With hamS2
                .Filename = outputFN
                .Bounds = IndRaster.Bounds
                .NoDataValue = -9999
                '.Projection = IndRaster.Projection

                ''.NumRowsInFile = IndRaster.NumRows
                ''.NumColumns = IndRaster.NumColumns

            End With
            'hamS = IndRaster
            'Dim c = hamS.Value.Item(50, 50)
            For i As Integer = 0 To IndRaster.NumRows - 1
                For j As Integer = 0 To IndRaster.NumColumns - 1
                    'hamS.Value(i, j) = IndRaster.Value(i, j)
                    If IndRaster.Value(i, j) <> IndRaster.NoDataValue And IndRaster.Value(i, j) > -9999 Then
                        If IndRaster.Value(i, j) > b Then
                            hamS2.Value(i, j) = 0
                        ElseIf IndRaster.Value(i, j) >= z And IndRaster.Value(i, j) <= b Then
                            hamS2.Value(i, j) = Math.Round(2 * Math.Pow((IndRaster.Value(i, j) - b) / (b - a), 2), 2)

                        ElseIf IndRaster.Value(i, j) >= a And IndRaster.Value(i, j) < z Then
                            hamS2.Value(i, j) = Math.Round(1 - 2 * Math.Pow((IndRaster.Value(i, j) - a) / (b - a), 2), 2)

                        Else 'x>c

                            hamS2.Value(i, j) = 1
                        End If
                    Else
                        hamS2.Value(i, j) = -9999
                    End If
                Next
            Next

            'hamS2.GetStatistics()
            'hamS2.GetUniqueValues()
            hamS2.Save()
            Status1.Caption = Tinhtoanthanhcong
            'MessageBox.Show(Tinhtoanthanhcong)
        Catch ex As Exception
            MessageBox.Show(loi, Khongtheghide + vbNewLine + Filechuanhoakhac)
            'MessageBox.Show(ex.Message)
        End Try

    End Function
    Function hamtheoloai(ByVal IndRaster As IRaster, ByVal outputFN As String, ByVal TTchitieu As Integer) As IRaster
        Dim a As Single
        Dim b As Single
        Dim c As Single
        Try
            ''===Đối với hàm S, không thể thiếu a, hoặc c
            ''===
            'If GridView1.GetDataRow(TTchitieu)("a").tostring = "" Or GridView1.GetDataRow(TTchitieu)("c").tostring = "" Then
            'MessageBox.Show("Nhập giá trị A và C" + vbNewLine + "Xem trợ giúp - phần các hàm chuẩn hóa (hàm S) để hiểu rõ hơn về các thông số a, b, c", loi, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'PicCheckHam.Image = My.Resources.Resources.cancel_32x32
            'PicCheckHam.ToolTip = "Nhập giá trị A và C" + vbNewLine + "Xem trợ giúp - phần các hàm chuẩn hóa (hàm S) để hiểu rõ hơn về các thông số a, b, c"
            'Return Nothing
            'ElseIf GridView1.GetDataRow(TTchitieu)("a") <> "" Then
            'a = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("a").tostring.Replace(",", ""))  'Convert.ToSingle(txtToiUu1.Text.Replace(",", ""))
            'ElseIf GridView1.GetDataRow(TTchitieu)("c") <> "" Then
            'c = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("c").tostring.Replace(",", ""))  'Convert.ToSingle(txtToiUu2.Text.Replace(",", "")) Then

            'End If

            'If GridView1.GetDataRow(TTchitieu)("b").tostring = "" Then
            'b = (a + c) / 2
            'Else
            'b = Convert.ToSingle(GridView1.GetDataRow(TTchitieu)("b").Replace(",", ""))  'Convert.ToSingle(txtNO1.Text.Replace(",", ""))
            'End If

            Dim rasteroptions As String()
            Dim datatype As System.Type = System.Type.GetType("System.Single")
            hamtheoloai = Raster.CreateRaster(outputFN, Nothing, IndRaster.NumColumns, IndRaster.NumRows, 1, datatype, rasteroptions)

            With hamtheoloai
                .Filename = outputFN
                .Bounds = IndRaster.Bounds
                .NoDataValue = -9999
                .Projection = IndRaster.Projection

                ''.NumRowsInFile = IndRaster.NumRows
                ''.NumColumns = IndRaster.NumColumns

            End With
            'hamS = IndRaster
            'Dim c = hamS.Value.Item(50, 50)
            IndRaster.GetStatistics()
            Dim inpMax As Single = IndRaster.Maximum
            Dim inpMin As Single = IndRaster.Minimum
            For i As Integer = 0 To IndRaster.NumRows - 1
                For j As Integer = 0 To IndRaster.NumColumns - 1
                    'hamS.Value(i, j) = IndRaster.Value(i, j)
                    If IndRaster.Value(i, j) <> IndRaster.NoDataValue And IndRaster.Value(i, j) > -9999 Then
                        hamtheoloai.Value(i, j) = Math.Round((IndRaster.Value(i, j) - inpMin) / (inpMax - inpMin), 2)
                    Else
                        hamtheoloai.Value(i, j) = -9999
                    End If
                Next
            Next
            hamtheoloai.Save()


            hamtheoloai.GetStatistics()
            hamtheoloai.GetUniqueValues()
            Return hamtheoloai
            'MessageBox.Show(Tinhtoanthanhcong)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MessageBox.Show(loi, Khongtheghide + vbNewLine + Filechuanhoakhac)
        End Try

    End Function
#End Region

    Private Shared Function AddOperation(ByVal val1 As Single, ByVal val2 As Single) As Single
        Return val1 + val2
    End Function
    Private Shared Function MultipleOperation(ByVal val1 As Single, ByVal val2 As Single) As Single
        Return val1 * val2
    End Function

    'Public Overloads Function ExecuteRasterAdd(ByVal input1 As IRaster, ByVal input2 As IRaster, ByVal output As IRaster, ByVal cancelProgressHandler As ICancelProgressHandler) As Boolean
    'Dim magic As New RasterMagic(AddressOf AddOperation)
    'Return magic.RasterMath(input1, input2, output, cancelProgressHandler)
    'End Function
    Public Function CongRaster(ByVal input1 As IRaster, ByVal input2 As IRaster, ByVal outputFN As String, ByVal cancelProgressHandler As ICancelProgressHandler) As IRaster
        Dim magic1 As New myRasterMagic
        Return magic1.RasterMath("Cộng", input1, input2, outputFN, cancelProgressHandler)
    End Function
    Public Function NhanRaster(ByVal input1 As IRaster, ByVal input2 As IRaster, ByVal outputFN As String, ByVal cancelProgressHandler As ICancelProgressHandler) As IRaster
        Dim magic1 As New myRasterMagic
        Return magic1.RasterMath("Nhân", input1, input2, outputFN, cancelProgressHandler)
    End Function

    Private Sub btnRastercal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRastercal.Click
        Dim dt As DataTable = GridControl1.DataSource
        Dim thePath As String
        Dim theFN As String
        Dim tmppath As String = Path.GetTempPath()
        Dim tmpRaster As IRaster = New raster
        Dim inpRaster1 As IRaster = New raster
        Dim inpRaster2 As IRaster = New raster
        Dim outRaster As IRaster = New Raster
        'output.Filename = "Z:\out1.tif"
        For i As Integer = 0 To GridView1.RowCount - 1
            thePath = GridView1.GetRowCellValue(i, "Map")
            theFN = Path.GetFileName(thePath)

            If i = 0 Then
                'tmpRaster.Filename = theFN
                tmpRaster = Raster.Open(thePath)
                'tmpRaster.SaveAs(tmppath + "sum_" + i.ToString + theFN + ".tif")
                tmpRaster.SaveAs(tmppath + "in1_" + i.ToString + theFN + ".tif")
                tmpRaster.Close()

                'tmpRaster = Raster.Open(tmppath + "sum_" + i.ToString + theFN + ".tif")
                'tmpRaster.Close()

                inpRaster1 = Raster.Open(tmppath + "in1_" + i.ToString + theFN + ".tif")
                inpRaster1.Close()
            Else

                inpRaster2 = Raster.Open(thePath)
                outRaster.Filename = tmppath + "sum_" + i.ToString + theFN + ".tif"
                outRaster = CongRaster(inpRaster1, inpRaster2, outRaster.Filename, Nothing)
                'outRaster.SaveAs(tmppath + "sum_" + i.ToString + theFN + ".tif")
                outRaster.SaveAs(tmppath + "in1_" + i.ToString + theFN + ".tif")
                'tmpRaster.Close()
                'tmpRaster = Raster.Open(tmppath + "sum_" + i.ToString + theFN + ".tif")
                'tmpRaster.Close()

                inpRaster1 = Raster.Open(tmppath + "in1_" + i.ToString + theFN + ".tif")
                inpRaster1.Close()

            End If
        Next
        'tmppath + "in1_" + i.ToString + theFN + ".tif" is the result


        'Dim dt As DataTable = GridControl1.DataSource
        'Dim thePath As String
        'Dim theFN As String
        'Dim tmppath As String = Path.GetTempPath()
        'Dim tmpRaster As IRaster = New raster
        'Dim inpRaster1 As IRaster = New raster
        'Dim inpRaster2 As IRaster = New raster
        'Dim outRaster As IRaster = New Raster
        ''output.Filename = "Z:\out1.tif"
        'For i As Integer = 0 To GridView1.RowCount - 1
        'thePath = GridView1.GetRowCellValue(i, "Map")
        'theFN = Path.GetFileName(thePath)

        'If i = 0 Then
        ''tmpRaster.Filename = theFN
        'tmpRaster = Raster.Open(thePath)
        ''tmpRaster.SaveAs(tmppath + "sum_" + i.ToString + theFN + ".tif")
        'tmpRaster.SaveAs(tmppath + "in1_" + i.ToString + theFN + ".tif")
        'tmpRaster.Close()

        ''tmpRaster = Raster.Open(tmppath + "sum_" + i.ToString + theFN + ".tif")
        ''tmpRaster.Close()

        'inpRaster1 = Raster.Open(tmppath + "in1_" + i.ToString + theFN + ".tif")
        'inpRaster1.Close()
        'Else

        'inpRaster2 = Raster.Open(thePath)
        'outRaster.Filename = tmppath + "sum_" + i.ToString + theFN + ".tif"
        'outRaster = CongRaster(inpRaster1, inpRaster2, outRaster, Nothing)
        ''outRaster.SaveAs(tmppath + "sum_" + i.ToString + theFN + ".tif")
        'outRaster.SaveAs(tmppath + "in1_" + i.ToString + theFN + ".tif")
        ''tmpRaster.Close()
        ''tmpRaster = Raster.Open(tmppath + "sum_" + i.ToString + theFN + ".tif")
        ''tmpRaster.Close()

        'inpRaster1 = Raster.Open(tmppath + "in1_" + i.ToString + theFN + ".tif")
        'inpRaster1.Close()

        'End If
        'Next


        ''tmpRaster.Filename = "z:\tst.tif"
        ''tmpRaster.Save()
    End Sub
    Private Sub BtnDelRas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelRas.Click

        'Dim f As String = "z:\a2.tif"
        'Dim tmpRaster As IRaster = New raster
        'tmpRaster = Raster.Open(f)
        'tmpRaster.SaveAs("z:\tst1.tif")

        'tmpRaster.Dispose()
        'tmpRaster.Close()
        'File.Delete(f)
    End Sub
    Sub addcontrol(ByVal ham As String)
        'BtnBrowse1.Enabled = True
        LblTU.Visible = True
        Txt_A.Size = New System.Drawing.Size(47, 20)
        Txt_B.Size = New System.Drawing.Size(47, 20)
        Txt_C.Size = New System.Drawing.Size(47, 20)
        txt_D.Size = New System.Drawing.Size(47, 20)
        If ham = aHams1 Then
            'btnAdd.Location = New System.Drawing.Point(1033, 8)
            'BtnReload.Location = New System.Drawing.Point(1033, 33)
            'btnDelete.Location = New System.Drawing.Point(1033, 58)
            'BtnUpdate.Location = New System.Drawing.Point(1033, 83)
            PanelControl6.Visible = True
            'PanelControl15.Dock = DockStyle.Fill
            'PanelControl15.BringToFront()

            '
            'Txt_A
            Txt_A.Location = New System.Drawing.Point(224, 34)
            Txt_A.Visible = True
            '
            'Txt_C
            Txt_B.Location = New System.Drawing.Point(132, 7)
            Txt_B.Visible = True
            '
            'LblKTN
            LblKTN.Location = New System.Drawing.Point(10, 40)
            LblKTN.Visible = True
            '
            'LblTU
            'LblTU.Location = New System.Drawing.Point(10, 11)

            '
            'txt_D
            txt_D.Text = ""
            txt_D.Visible = False
            '
            'Txt_c
            Txt_C.Text = ""
            Txt_C.Visible = False
            '
            'PictureBox1
            PictureBox1.Visible = False
            '
            'LblLessthan
            LblLessthan.Location = New System.Drawing.Point(198, 37)
            LblLessthan.Visible = True
            '
            'LblGreater
            LblGreater.Visible = True
            LblGreater.Location = New System.Drawing.Point(185, 10)
            '
            'LblLE
            LblLE.Visible = False
            '
            'LblGE
            LblGE.Visible = False
            '
            'LblLEL
            LblLEL.Visible = False
            '
            'LblLELE
            LblLELE.Visible = False

        ElseIf (ham = aHams2) Then
            'btnAdd.Location = New System.Drawing.Point(1033, 8)
            'BtnReload.Location = New System.Drawing.Point(1033, 33)
            'btnDelete.Location = New System.Drawing.Point(1033, 58)
            'BtnUpdate.Location = New System.Drawing.Point(1033, 83)
            PanelControl6.Visible = True
            '
            'Txt_A
            Txt_A.Location = New System.Drawing.Point(224, 7)
            Txt_A.Visible = True
            '
            'Txt_C
            Txt_B.Location = New System.Drawing.Point(132, 34)
            Txt_B.Visible = True
            '
            'LblKTN
            LblKTN.Location = New System.Drawing.Point(10, 40)
            LblKTN.Visible = True

            'LblTU
            'LblTU.Location = New System.Drawing.Point(10, 11)
            '
            'PictureBox1
            PictureBox1.Visible = False
            '
            'Txt_B
            Txt_C.Text = ""
            Txt_C.Visible = False
            '
            'txt_D
            txt_D.Text = ""
            txt_D.Visible = False
            '
            'LblLessthan
            LblLessthan.Location = New System.Drawing.Point(198, 10)
            LblLessthan.Visible = True
            '
            'LblGreater
            LblGreater.Visible = True
            LblGreater.Location = New System.Drawing.Point(185, 37)
            '
            'LblLE
            LblLE.Visible = False
            '
            'LblGE
            LblGE.Visible = False
            '
            'LblLEL
            LblLEL.Visible = False
            '
            'LblLELE
            LblLELE.Visible = False
        ElseIf (ham = aHamkandel) Then

            'btnAdd.Location = New System.Drawing.Point(1033, 8)
            'BtnReload.Location = New System.Drawing.Point(1033, 33)
            'btnDelete.Location = New System.Drawing.Point(1033, 58)
            'BtnUpdate.Location = New System.Drawing.Point(1033, 83)
            PanelControl6.Visible = True
            '
            'LblKTN
            '
            LblKTN.Visible = False
            '
            'txt_D
            '
            txt_D.Text = ""
            Me.txt_D.Visible = False
            '
            'LblTU

            '
            'Txt_C
            '
            Me.Txt_C.Visible = False
            Txt_C.Text = ""
            '
            'LblLELE
            '
            Me.LblLELE.Location = New System.Drawing.Point(185, 10)
            Me.LblLELE.Visible = True

            '
            'Txt_A
            '
            Me.Txt_A.Location = New System.Drawing.Point(132, 7)
            Me.Txt_A.Visible = True

            '
            'Txt_B
            '
            Me.Txt_B.Location = New System.Drawing.Point(224, 7)
            Me.Txt_B.Visible = True
            '
            'LblLessthan
            '
            Me.LblLessthan.Visible = False
            '
            'LblLEL
            '
            Me.LblLEL.Visible = False
            '
            'LblGreater
            '
            Me.LblGreater.Visible = False

            '
            'Picture bracket
            PictureBox1.Visible = False
            LblGE.Visible = False
            LblLE.Visible = False
        ElseIf (ham = aHamhinhthang) Then
            PanelControl6.Visible = True
            'btnAdd.Location = New System.Drawing.Point(1033, 8)
            'BtnReload.Location = New System.Drawing.Point(1033, 33)
            'btnDelete.Location = New System.Drawing.Point(1033, 58)
            'BtnUpdate.Location = New System.Drawing.Point(1033, 83)
            '
            'LblKTN
            '
            Me.LblKTN.Location = New System.Drawing.Point(8, 52)
            LblKTN.Visible = True
            '
            'txt_D
            '
            Me.txt_D.Location = New System.Drawing.Point(132, 60)
            Me.txt_D.Name = "txt_D"
            Me.txt_D.Visible = True
            '
            'LblTU
            '
            'Me.LblTU.Location = New System.Drawing.Point(18, 11)
            'Me.LblTU.Name = "LblTU"
            'Me.LblTU.Size = New System.Drawing.Size(59, 13)
            'Me.LblTU.TabIndex = 14
            'Me.LblTU.Text = "Giá trị tối ưu"

            '
            'Txt_C
            '
            Me.Txt_C.Location = New System.Drawing.Point(224, 7)
            Me.Txt_C.Visible = True
            '
            'LblLELE
            '
            Me.LblLELE.Visible = True
            Me.LblLELE.Location = New System.Drawing.Point(185, 10)
            '
            'Txt_A
            '
            Me.Txt_A.Location = New System.Drawing.Point(224, 34)
            Me.Txt_A.Visible = True

            '
            'Txt_B
            '
            Me.Txt_B.Location = New System.Drawing.Point(132, 7)
            Me.Txt_B.Visible = True
            '
            'LblLessthan
            '
            Me.LblLessthan.Visible = False
            '
            'LblLEL
            '
            Me.LblLEL.Visible = False
            '
            'LblGreater
            '
            Me.LblGreater.Visible = False

            '
            'PictureBox1
            '
            Me.PictureBox1.Visible = True
            '
            'LblGE
            '
            LblGE.Visible = True
            Me.LblGE.Location = New System.Drawing.Point(185, 63)

            '
            'Lblcontrol3
            '
            LblLE.Visible = True
            Me.LblLE.Location = New System.Drawing.Point(198, 37)


        ElseIf (ham = aHamtheoloai) Then
            PanelControl6.Visible = False
            'For Each Control As Control In PanelControl6.Controls
            'If Not TypeOf Control Is TextEdit Then
            'Control.Visible = False
            'End If



            'Next
            'Txt_A.Visible = True
            'Txt_A.Location = New System.Drawing.Point(25, 7)
            'Txt_A.Size = New System.Drawing.Size(250, 20)
            'Txt_B.Visible = True
            'Txt_B.Location = New System.Drawing.Point(25, 32)
            'Txt_B.Size = New System.Drawing.Size(250, 20)
            'Txt_C.Visible = True
            'Txt_C.Location = New System.Drawing.Point(25, 57)
            'Txt_C.Size = New System.Drawing.Size(250, 20)
            'txt_D.Visible = True
            'txt_D.Location = New System.Drawing.Point(25, 82)
            'txt_D.Size = New System.Drawing.Size(250, 20)



            'btnAdd.Location = New System.Drawing.Point(750, 8)
            'BtnReload.Location = New System.Drawing.Point(750, 33)
            'btnDelete.Location = New System.Drawing.Point(750, 58)
            'BtnUpdate.Location = New System.Drawing.Point(750, 83)
            Txt_A.Text = ""
            Txt_B.Text = ""
            Txt_C.Text = ""
            txt_D.Text = ""
            'ElseIf ham = ahamKhongham Then
            'DisableControl()
            'cboFunction.Enabled = True
        End If

        'If ham <> ahamKhongham Then
        'EnableControl()
        'RadLim.SelectedIndex = 0
        'RadLim.Enabled = True
        'End If

        '===Xử lý disable khi chọn hàm không chuẩn hóa; hạn chế; ....
        'If (cboIndGroup.SelectedItem = "1. Sinh thái" Or cboIndGroup.SelectedItem = "1. Ecology") And (cboFunction.SelectedItem = ahamKhongham) Then       'Phải sử dụng GridView1.GetRowCellValue(GridView1.FocusedRowHandle,"UsedFunction") thay cho cboFunction.SelectedItem bởi vì lúc này không hiểu sao cboFunction vẫn chưa update. Lạ nhất  là event gridview1.focusedrowchanged lại thực hiện sau event cboIndGroup.SelectedIndexChanged này
        Try
            If GridView1.GetRow(GridView1.FocusedRowHandle) Is Nothing Then     'Trường hợp xóa Row thì cho nó focus lên hagf đầu tiên.
                'Không hoạt động, không hiểu sao không set focus về dòng đầu tiên được
                GridControl1.ForceInitialize()
                GridView1.OptionsSelection.EnableAppearanceFocusedRow = True
                'GridView1.OptionsSelection.MultiSelect = True
                GridView1.FocusedRowHandle = 0
                GridView1.SelectRow(0)
                'GridView1.OptionsSelection.MultiSelect = False
                GridView1.FocusedRowHandle = 1
                GridView1.SelectRow(1)
                GridView1.FocusedRowHandle = 0
                GridView1.FocusedRowHandle = GridView1.GetVisibleRowHandle(0)
                GridView1.SelectRow(0)
            End If

            If (cboIndGroup.SelectedItem = "1. Sinh thái" Or cboIndGroup.SelectedItem = "1. Ecology") And (GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "UsedFunction").ToString = ahamKhongham) Then
                DisableControl()
                RadLim.SelectedIndex = -1      'Thử bỏ đi do lỗi check và bỏ check cái Enc xong nó cứ báo lỗi tổng trọng số =1khi chuyển đi chuyển lại cái Tab Trọng số    'Có 3 chỗ thử bỏ đi. Nếu phát sinh lỗi thì phải thêm vào cả 3 chỗ nhé
                cboFunction.Enabled = True
            ElseIf cboIndGroup.SelectedItem = "4. Hạn chế" Or cboIndGroup.SelectedItem = "4. Constraint" Then
                DisableControl()
                RadLim.SelectedIndex = -1
            ElseIf BarCheckItemComposite.Checked = False And (cboIndGroup.SelectedItem = "2. Kinh tế - Xã hội" Or cboIndGroup.SelectedItem = "2. Socio - Economic" Or cboIndGroup.SelectedItem = "3. Môi trường" Or cboIndGroup.SelectedItem = "3. Environment") Then
                DisableControl()
                RadLim.SelectedIndex = -1      'Thử bỏ đi do lỗi check và bỏ check cái Enc xong nó cứ báo lỗi tổng trọng số =1khi chuyển đi chuyển lại cái Tab Trọng số    'Có 3 chỗ thử bỏ đi. Nếu phát sinh lỗi thì phải thêm vào cả 3 chỗ nhé

            ElseIf BarCheckItemComposite.Checked = True And (cboIndGroup.SelectedItem = "2. Kinh tế - Xã hội" Or cboIndGroup.SelectedItem = "2. Socio - Economic" Or cboIndGroup.SelectedItem = "3. Môi trường" Or cboIndGroup.SelectedItem = "3. Environment") And (GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "UsedFunction").ToString = ahamKhongham) Then
                DisableControl()
                RadLim.SelectedIndex = -1      'Thử bỏ đi do lỗi check và bỏ check cái Enc xong nó cứ báo lỗi tổng trọng số =1khi chuyển đi chuyển lại cái Tab Trọng số    'Có 3 chỗ thử bỏ đi. Nếu phát sinh lỗi thì phải thêm vào cả 3 chỗ nhé
                cboFunction.Enabled = True
            Else
                EnableControl()

            End If
        Catch ex As Exception

        End Try


        GridView1.UpdateCurrentRow()
        BtnUpdate.Enabled = True
        'Đặt lại focused Row
        For i As Int16 = 0 To GridView1.RowCount - 1
            If GridView1.GetRowCellValue(i, "IndName") = txtInd.Text Then
                GridView1.FocusedRowHandle = i
                Exit For
            End If
        Next

    End Sub


    Private Sub BarButtonItem3_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Raster2tif.ShowDialog()

    End Sub

    Private Sub BarButtonItem4_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        'Vector2Raster.ShowDialog()
        Frm_Vector2GeoTifGoc.ShowDialog()
    End Sub

    Private Sub BarButtonItem5_ItemClick(ByVal sender As System.Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick
        Frm_createSlope.ShowDialog()
    End Sub

    Private Sub cboFunction_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFunction.SelectedIndexChanged
        GridView1.UpdateCurrentRow()
        addcontrol(sender.selecteditem)
        BtnUpdate.Enabled = True
    End Sub

    Private Sub RadLim_EnabledChanged(sender As Object, e As EventArgs) Handles RadLim.EnabledChanged
        'If RadLim.Enabled = True Then
        'RadLim.SelectedIndex = 0
        'Else : RadLim.SelectedIndex = -1
        'End If
    End Sub

    Private Sub RadLim_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadLim.SelectedIndexChanged
        If RadLim.SelectedIndex = 0 Then
            GridView1.SetFocusedRowCellValue("Weight", DBNull.Value)
        End If
        GridView1.UpdateCurrentRow()
        BtnUpdate.Enabled = True
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim myConStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + Application.StartupPath + "\Nafosted_2.nft; Jet OLEDB:Database Password= abc"
        Dim da As OleDbDataAdapter = New OleDbDataAdapter("select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """", myConStr)
        Dim myDataTale As DataTable = New DataTable()
        Dim myConn As New OleDbConnection(myConStr)
        myConn.Open()
        da.Fill(myDataTale)
        GridControl1.DataSource = myDataTale
        GridView1.Columns("TT").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
    End Sub
    Private Sub weightCal(Group As String)
        GridView1.Columns("IndGroup").ClearFilter()
        GridView1.Columns("Limit").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[Limit] = 'Không giới hạn' or [Limit] = 'Non-Limit'")
        If Group = "TNST" Then
            'GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")
            GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '1. Sinh thái' OR [IndGroup] = '1. Ecology'")         '("[IndGroup] <> '2. Kinh tế - Xã hội' and [IndGroup] <> '3. Môi trường' and [IndGroup] <> '2. Socio - Economic' and [IndGroup] <> '3. Environment' and [IndGroup] <> '4. Hạn chế' and [IndGroup] <> '4. Constraint'")' GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] <> '2. Kinh tế - Xã hội' and [IndGroup] <> '3. Môi trường' and [IndGroup] <> '2. Socio - Economic' and [IndGroup] <> '3. Environment' and [IndGroup] <> '4. Hạn chế' and [IndGroup] <> '4. Constraint'")
        ElseIf Group = "KTXH" Then
            'GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")
            GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '2. Kinh tế - Xã hội' or [IndGroup] = '2. Socio - Economic'")
        ElseIf Group = "TNMT" Then
            'GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")
            GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '3. Môi trường' or [IndGroup] = '3. Environment'")
        End If

        GridView1.Columns("Weight").SortOrder = DevExpress.Data.ColumnSortOrder.None
        'Dim dt As DataTable = GridView1.DataSource.totable  '.DataSource
        'Dim table As DataTable = DirectCast(GridView2.DataSource, DataView).Table
        'Dim filteredDataView As New DataView(table)
        'filteredDataView.RowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(GridView2.ActiveFilterCriteria)
        'Dim dt As DataTable = DirectCast(GridView1.DataSource, DataView).Table
        'Dim filteredDataView As New DataView(dt)
        'filteredDataView.RowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(GridView1.ActiveFilterCriteria)
        Dim dt As DataTable = GridControl1.DataSource
        'GridView1.Columns("TT").Visible = True
        'GridView1.Columns("TT").OptionsColumn.AllowEdit = True
        'For i As Integer = 0 To dt.Rows.Count - 1
        'If dt.Rows(i)("TT").ToString = "" Then
        ''For j As Integer = 0 To dt.Rows.Count - 1
        'dt.Rows(i)("TT") = i + 1   'dt.Rows(j)("TT") = j + 1       Dùng update len DT này ko ăn thua, dùng cái dưới thì ok
        'GridView1.SetRowCellValue(i, "TT", i + 1)
        ''Next

        'End If
        'Next
        For i As Integer = 0 To GridView1.RowCount - 1
            If GridView1.GetRowCellValue(i, "TT").ToString = "" Then
                'For j As Integer = 0 To dt.Rows.Count - 1
                'dt.Rows(i)("TT") = i + 1   'dt.Rows(j)("TT") = j + 1       Dùng update len DT này ko ăn thua, dùng cái dưới thì ok
                GridView1.SetRowCellValue(i, "TT", i + 1)
                'Next
                Dim a = GridView1.GetRowCellValue(i, "TT").ToString
            End If
        Next
        'GridView1.Columns("Weight").SortOrder = DevExpress.Data.ColumnSortOrder.Descending
        ''''''GridView1.Columns("TT").Visible = False
        '=====================2 lệnh sau rất quan trọng, nếu ko sẽ ko update focused row
        'GridView1.PostEditor()
        'GridView1.UpdateCurrentRow()
        '=====================
        'Myconnect.Update_CSDL(dt, "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
        'BtnWeightCal_Click(Nothing, Nothing)
        'GridView1.DataSource.sort() = "TT"         'Dùng cái này là lỗi nặng luôn. Sau này cứ Up lên 1 bước là lại đổi focus row
        'GridView1.Columns("Weight").SortOrder = DevExpress.Data.ColumnSortOrder.None    'Nhớ phải giữ 2 cái này để sort
        'GridView1.Columns("TT").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending    'Nhớ phải giữ 2 cái này để sort, ko được xóa TT

    End Sub
    Private Sub BtnWeightCal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnWeightCal.Click


    End Sub

    Private Sub BtnTop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTop.Click
        For i As Integer = 0 To GridView1.RowCount - 1
            BtnUp_Click(Nothing, Nothing)
        Next

    End Sub

    Private Sub BtnBottom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBottom.Click

        For i As Integer = 0 To GridView1.RowCount - 1
            BtnDown_Click(Nothing, Nothing)
        Next
    End Sub

    Private Sub Button6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Frmtinhtoandachitieu.ShowDialog()
    End Sub



    Private Sub XtraTabPage5_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles XtraTabPage_Step4.GotFocus
        Frmtinhtoandachitieu.ShowDialog()
    End Sub





    'Private Sub ribbon_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
    'Dim rc As RibbonControl = TryCast(sender, RibbonControl)

    'End Sub

    'Private Sub RibbonControl1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RibbonControl1.MouseMove
    'Dim hi As RibbonHitInfo = sender.CalcHitInfo(New System.Drawing.Point(e.X, e.Y))

    'If hi.InPage = True Then
    'RibbonControl1.Minimized = True

    ''MessageBox.Show("")
    ''If Not RibbonControl1.Pages(0) Is hi.Page Then
    ''Text &= " ! "
    '''RibbonControl1.Pages(0) = hi.Page
    ''End If
    'End If
    'End Sub



#Region "Minimize the RibbonControl1"
    'Private Sub FrmStart_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove

    'End Sub
    'Private hitInfo As RibbonHitInfo
    'Private Sub RibbonControl1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RibbonControl1.MouseMove
    ''hitInfo = RibbonControl1.CalcHitInfo(e.Location)
    'End Sub

    'Private Sub FrmStart_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
    ''MessageBox.Show("")
    'End Sub
    'Private Sub XtraTabControl1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles XtraTabControl1.MouseMove
    ''Dim bounds As Rectangle = RibbonControl1.ViewInfo.Bounds
    ''If (Not bounds.Contains(e.Location)) Then
    ''RibbonControl1.Minimized = True
    ''End If
    ''MessageBox.Show("")
    'Dim bounds As Rectangle = RibbonControl1.ViewInfo.Bounds
    'If (Not bounds.Contains(e.Location)) Then
    'Debug.WriteLine(bounds.ToString())
    'RibbonControl1.Minimized = True
    'Dim [property] As PropertyInfo = GetType(RibbonControl).GetProperty("MinimizedRibbonPopupForm", BindingFlags.NonPublic Or BindingFlags.Instance)
    'Dim form As Object = [property].GetValue(RibbonControl1, Nothing)
    'If form IsNot Nothing Then
    'Dim method As MethodInfo = GetType(RibbonControl).GetMethod("DestroyPopupForms", BindingFlags.NonPublic Or BindingFlags.Instance)
    'method.Invoke(RibbonControl1, Nothing)
    'End If
    'End If
    'End Sub    'Private Sub PanelControl1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PanelControl1.MouseMove
    'ribbo1minimize(e)
    'End Sub
    'Private Sub PanelControl8_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PanelControl8.MouseMove
    'ribbo1minimize(e)
    'End Sub
    'Private Sub PanelControl6_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PanelControl6.MouseMove
    'ribbo1minimize(e)
    'End Sub
    'Private Sub ListBoxControl1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GridControl1.MouseMove
    'ribbo1minimize(e)
    'End Sub
    'Private Sub Gridcontrol1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListBoxControl1.MouseMove
    'ribbo1minimize(e)
    'End Sub
    'Private Sub ribbo1minimize(ByVal e As System.Windows.Forms.MouseEventArgs)

    'Dim bounds As Rectangle = RibbonControl1.ViewInfo.Bounds
    'If (Not bounds.Contains(e.Location)) Then
    'Debug.WriteLine(bounds.ToString())
    'RibbonControl1.Minimized = True
    'Dim [property] As PropertyInfo = GetType(RibbonControl).GetProperty("MinimizedRibbonPopupForm", BindingFlags.NonPublic Or BindingFlags.Instance)
    'Dim form As Object = [property].GetValue(RibbonControl1, Nothing)
    'If form IsNot Nothing Then
    'Dim method As MethodInfo = GetType(RibbonControl).GetMethod("DestroyPopupForms", BindingFlags.NonPublic Or BindingFlags.Instance)
    'method.Invoke(RibbonControl1, Nothing)
    'End If
    'End If

    'End Sub
#End Region


    Private Sub BarButtonItemMapview_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarButtonItemMapview.ItemClick
        'For i As Integer = 0 To GridView1.RowCount - 1
        'Dim thePath = GridView1.GetRowCellValue(i, "Mapchuanhoa")
        'Next

        Try
            SplashScreenManager.ShowForm(GetType(FrmWaiting))
            frmMapviewer.Show()
            frmMapviewer.Activate()
            frmMapviewer.TopLevel = True
            SplashScreenManager.CloseForm()
        Catch ex As Exception
            SplashScreenManager.CloseForm()
        End Try


    End Sub

    Private Sub XtraTabPage1_GotFocus(sender As Object, e As EventArgs) Handles XtraTabPage_Step1.GotFocus
        XtraTabPage_Step1.Tag = "Đang focus"
        capnhatlaiTab2_3 = False
    End Sub
    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim a = Raster.OpenFile("D:\TSTdata\BDCB\a.tif", False)
        a.Close()
        a.Dispose()
        GC.Collect()
        'Gridview1_Reload()
        'Dim dt As DataTable = Myconnect.DtFromQry("select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """" + " and Limit = 'Không giới hạn'")
        'GridControl1.DataSource = dt
        MessageBox.Show("")
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim a = Raster.Open("D:\TSTdata\BDCB\a.tif")
        'MessageBox.Show("")
        'a.Close()
        'a.Dispose()
        'GC.Collect()
        ''TxtMap.Properties.Appearance.TextOptions.HAlignment = HorzAlignment.Far
        ''TxtMap.Properties.Appearance.Options.UseTextOptions = True
        ''Dim indcount As Integer
        ''indcount = GridView1.RowCount
        ''For i As Integer = 0 To indcount - 1
        ''Dim theMapstring As String = Convert.ToString(GridView1.GetRowCellValue(i, "Map"))
        ''Dim inputRaster As DotSpatial.Data.Raster
        ''Try
        ''inputRaster = DotSpatial.Data.Raster.Open(theMapstring)

        ''ListBox1.Items.Add(Convert.ToString(GridView1.GetRowCellValue(i, "Map")))
        ''Catch ex As Exception
        '''If inputRaster Is Nothing Then
        ''MessageBox.Show("Không tìm thấy file bản đồ " & theMapstring, "Lỗi bản đồ tại chỉ tiêu số " & i + 1)
        ''Exit Sub
        '''End If
        ''End Try

        ''Next
    End Sub

#Region "Tùy biến popup menu cho Gridview2"
    ''Private Sub GridView2_PopupMenuShowing(ByVal sender As Object, ByVal e As PopupMenuShowingEventArgs) Handles GridView2.PopupMenuShowing
    ''If e.MenuType = GridMenuType.Column Then
    ''Dim menu As GridViewColumnMenu = e.Menu

    ''If Not menu.Column Is Nothing Then
    '''menu.Items.Add(CreateCheckItem("Can Moved", menu.Column, Nothing))
    ''menu.Items.Add(createmenuitem("Đặt vừa độ rộng cột 123", menu.Column, Nothing, New EventHandler(AddressOf bestfitcolumn)))
    ''End If
    ''End If
    ''End Sub


    'Lấy Item dựa trên GridStringId. Từ đó xác định ẩn hay hiện nó dựa trên sub gridView1_ShowGridMenu
    Private Function GetItemByStringId(ByVal menu As DXPopupMenu, ByVal id As GridStringId) As DXMenuItem
        For Each item As DXMenuItem In menu.Items
            If item.Caption = GridLocalizer.Active.GetLocalizedString(id) Then
                Return item
            End If
        Next item
        Return Nothing
    End Function

    Private Sub gridView1_PopupMenuShowing(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView1.PopupMenuShowing
        'Thực hiện với menu chuột phải lên header của Trường
        If e.MenuType = GridMenuType.Column Then    'Thực hiện với menu chuột phải lên header của Trường
            '===HIDE ===nhiều item cùng lúc
            Dim hideItem(8) As DXMenuItem       'số 2 có nghĩa là có 3 phần tử 0..2
            'Dim hideItem(0) As DXMenuItem = GetItemByStringId(e.Menu, GridStringId.MenuColumnColumnCustomization)

            hideItem(0) = GetItemByStringId(e.Menu, GridStringId.MenuColumnColumnCustomization)
            hideItem(1) = GetItemByStringId(e.Menu, GridStringId.MenuColumnRemoveColumn)
            hideItem(2) = GetItemByStringId(e.Menu, GridStringId.MenuColumnFilterEditor)

            hideItem(3) = GetItemByStringId(e.Menu, GridStringId.MenuColumnFindFilterShow)
            hideItem(4) = GetItemByStringId(e.Menu, GridStringId.MenuColumnBestFit)
            hideItem(5) = GetItemByStringId(e.Menu, GridStringId.MenuColumnGroup)
            hideItem(6) = GetItemByStringId(e.Menu, GridStringId.MenuColumnFindFilterHide)
            hideItem(7) = GetItemByStringId(e.Menu, GridStringId.MenuColumnAutoFilterRowShow)
            'hideItem(8) = GetItemByStringId(e.Menu, GridStringId.MenuColumnGroupBox)
            hideItem(8) = GetItemByStringId(e.Menu, GridStringId.MenuGroupPanelShow)

            'hideItem(3) = GetItemByStringId(e.Menu, GridStringId.MenuColumnSortAscending)
            'hideItem(4) = GetItemByStringId(e.Menu, GridStringId.MenuColumnSortDescending)
            'hideItem(5) = GetItemByStringId(e.Menu, GridStringId.MenuColumnClearSorting)
            For i = 0 To hideItem.Count - 1
                Try
                    hideItem(i).Visible = False
                Catch ex As Exception

                End Try

            Next

            Dim menu As GridViewColumnMenu = e.Menu

            If Not menu.Column Is Nothing Then
                'menu.Items.Add(CreateCheckItem("Can Moved", menu.Column, Nothing))
                menu.Items.Add(create_menuitem(Datvuadorongcot, menu.Column, Nothing, New EventHandler(AddressOf bestfitcolumn)))
                menu.Items.Add(create_menuitem(Suaduongdan, menu.Column, Nothing, New EventHandler(AddressOf Search_Replace)))
                GetItemByStringId(e.Menu, GridStringId.MenuColumnSortAscending).Caption = Sapxeptangdan
                GetItemByStringId(e.Menu, GridStringId.MenuColumnSortDescending).Caption = Sapxepgiamdan
                GetItemByStringId(e.Menu, GridStringId.MenuColumnClearSorting).Caption = Bosapxep
                GetItemByStringId(e.Menu, GridStringId.MenuColumnBestFitAllColumns).Caption = Datvuadorongmoicot
                '====Cái này hide mà vẫn hiện, remove luôn
                menu.Items.Remove(GetItemByStringId(e.Menu, GridStringId.MenuGroupPanelShow))
            End If

            '===HIDE ===1 item
            ''''''Dim miCustomize As DXMenuItem = GetItemByStringId(e.Menu, GridStringId.MenuColumnColumnCustomization)
            ''''''If miCustomize IsNot Nothing Then
            ''''''miCustomize.Visible = False
            ''''''End If

            '''===========================
            '''===Disable ====Group By This Column
            ''Dim miGroup As DXMenuItem = GetItemByStringId(e.Menu, GridStringId.MenuColumnGroup)
            ''If miGroup IsNot Nothing Then
            ''miGroup.Enabled = False
            ''End If

            '''===========================
            '''===THÊM MENU ITEM====
            ''Dim menu As GridViewColumnMenu = e.Menu
            ''Dim bestfitItem = create_menuitem("Đặt vừa độ rộng cột 123", menu.Column, Nothing, New EventHandler(AddressOf bestfitcolumn))
            ''e.Menu.Items.Add(bestfitItem)

            '''===========================
            '''===THÊM MENUCHECKED ITEM====
            ''Dim canmoveItem = create_checkedmenuitem("canmove", menu.Column, Nothing, New EventHandler(AddressOf canmoveclick))
            ''e.Menu.Items.Add(canmoveItem)

            '''===========================
            '''===ĐỔI CAPTION CỦA ITEM====
            ''Dim miGroup As DXMenuItem = GetItemByStringId(e.Menu, GridStringId.MenuColumnGroup)
            ''miGroup.Caption = "Nhóm theo trường này ...."
        End If
    End Sub
    Private Sub gridView2_PopupMenuShowing(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GridView2.PopupMenuShowing
        'Thực hiện với menu chuột phải lên header của Trường
        If e.MenuType = GridMenuType.Column Then    'Thực hiện với menu chuột phải lên header của Trường
            '===HIDE ===nhiều item cùng lúc
            Dim hideItem(8) As DXMenuItem       'số 2 có nghĩa là có 3 phần tử 0..2

            hideItem(0) = GetItemByStringId(e.Menu, GridStringId.MenuColumnColumnCustomization)
            hideItem(1) = GetItemByStringId(e.Menu, GridStringId.MenuColumnRemoveColumn)
            hideItem(2) = GetItemByStringId(e.Menu, GridStringId.MenuColumnFilterEditor)

            hideItem(3) = GetItemByStringId(e.Menu, GridStringId.MenuColumnFindFilterShow)
            hideItem(4) = GetItemByStringId(e.Menu, GridStringId.MenuColumnBestFit)
            hideItem(5) = GetItemByStringId(e.Menu, GridStringId.MenuColumnGroup)
            hideItem(6) = GetItemByStringId(e.Menu, GridStringId.MenuColumnFindFilterHide)
            hideItem(7) = GetItemByStringId(e.Menu, GridStringId.MenuColumnAutoFilterRowShow)

            hideItem(8) = GetItemByStringId(e.Menu, GridStringId.MenuGroupPanelShow)

            For i = 0 To hideItem.Count - 1
                Try
                    hideItem(i).Visible = False
                Catch ex As Exception

                End Try

            Next

            Dim menu As GridViewColumnMenu = e.Menu

            If Not menu.Column Is Nothing Then
                menu.Items.Add(create_menuitem(Datvuadorongcot, menu.Column, Nothing, New EventHandler(AddressOf bestfitcolumn)))
                menu.Items.Add(create_menuitem(Suaduongdan, menu.Column, Nothing, New EventHandler(AddressOf Search_Replace)))
                GetItemByStringId(e.Menu, GridStringId.MenuColumnSortAscending).Caption = Sapxeptangdan
                GetItemByStringId(e.Menu, GridStringId.MenuColumnSortDescending).Caption = Sapxepgiamdan
                GetItemByStringId(e.Menu, GridStringId.MenuColumnClearSorting).Caption = Bosapxep
                GetItemByStringId(e.Menu, GridStringId.MenuColumnBestFitAllColumns).Caption = Datvuadorongmoicot
                '====Cái này hide mà vẫn hiện, remove luôn
                menu.Items.Remove(GetItemByStringId(e.Menu, GridStringId.MenuGroupPanelShow))
            End If
        End If
    End Sub
    Function create_menuitem(ByVal caption As String, ByVal column As GridColumn, ByVal image As Image, ByVal e As EventHandler)
        Dim item As DXMenuItem = New DXMenuItem(caption, e, image)
        'Dim item As DXMenuItem = New DXMenuItem("best fit",  New EventHandler(AddressOf bestfitcolumn))
        item.Tag = New MenuColumnInfo(column)
        Return item
    End Function
    Sub bestfitcolumn(ByVal sender As Object, ByVal e As EventArgs)
        Dim item As DXMenuItem = sender
        Dim info As MenuColumnInfo = CType(item.Tag, MenuColumnInfo)
        If info Is Nothing Then Return
        '===Main command
        info.Column.BestFit()
        'info.Column.Caption = "abcdefgh"
        'Messagebox.show("")
    End Sub
    'Sub Search_Replace(ByVal sender As Object, ByVal e As EventArgs)
    'FrmSearch_Replace.ShowDialog()

    'End Sub
    'Sub Search_Replace(ByVal oldval As String, ByVal newval As String)
    Sub Search_Replace(ByVal sender As Object, ByVal e As EventArgs)
        Dim item As DXMenuItem = sender
        Dim info As MenuColumnInfo = CType(item.Tag, MenuColumnInfo)
        If info Is Nothing Then Return
        Dim col As GridColumn = info.Column
        If col.FieldName <> "Map" And col.FieldName <> "Mapchuanhoa" And col.FieldName <> "SrcTif" And col.FieldName <> "RecTif" Then
            Return
        End If
        FrmSearch_Replace.Tag = col '.FieldName
        FrmSearch_Replace.ShowDialog()

    End Sub
    Function create_checkedmenuitem(ByVal caption As String, ByVal column As GridColumn, ByVal image As Image, ByVal e As EventHandler)
        Dim item As DXMenuCheckItem = New DXMenuCheckItem(caption, column.OptionsColumn.AllowMove, image, e)

        item.Tag = New MenuColumnInfo(column)
        Return item
    End Function
    'Menu item click handler.
    Sub canmoveclick(ByVal sender As Object, ByVal e As EventArgs)
        Dim item As DXMenuCheckItem = sender
        Dim info As MenuColumnInfo = CType(item.Tag, MenuColumnInfo)
        If info Is Nothing Then Return
        '===Main command
        info.Column.OptionsColumn.AllowMove = item.Checked
    End Sub

    Class MenuColumnInfo
        Public Sub New(ByVal column As GridColumn)
            Me.Column = column
        End Sub
        Public Column As GridColumn
    End Class
    'Function CreateCheckItem(ByVal caption As String, ByVal column As GridColumn, ByVal image As Image) As DXMenuCheckItem
    'Dim item As DXMenuCheckItem = New DXMenuCheckItem(caption, column.OptionsColumn.AllowMove, image, New EventHandler(AddressOf OnCanMovedItemClick))
    'item.Tag = New MenuColumnInfo(column)
    'Return item
    'End Function
    ''Menu item click handler.
    'Sub OnCanMovedItemClick(ByVal sender As Object, ByVal e As EventArgs)
    'Dim item As DXMenuCheckItem = sender
    'Dim info As MenuColumnInfo = CType(item.Tag, MenuColumnInfo)
    'If info Is Nothing Then Return
    'info.Column.OptionsColumn.AllowMove = item.Checked
    'End Sub
    'Sub bestfitcolumn(ByVal XtraGrdView As DevExpress.XtraGrid.Views.Grid.GridView)

    'The class that stores menu specific information.

#End Region

    Sub RasterLayersymbology(tlayer As IRasterLayer)
        tlayer.DataSet.GetStatistics()
        Dim minval As Single = tlayer.Minimum
        Dim maxval As Single = tlayer.Maximum

        '''Dim tlayer As IMapRasterLayer = frmMapviewer.Map1.AddLayer("D:\TSTdata\x.tif")
        '''set the color scheme

        '''create an instance for a colorscheme
        Dim scheme As ColorScheme = New ColorScheme()

        If maxval > 0.75 Then
            If minval < 0.25 Then
                Dim category1 As ColorCategory = New ColorCategory(minval - 0.001, 0.25, Color.Bisque, Color.Bisque)
                category1.LegendText = minval.ToString + " - 0.25"
                scheme.AddCategory(category1)

                Dim category2 As ColorCategory = New ColorCategory(0.2499, 0.5, Color.OrangeRed, Color.OrangeRed)
                category2.LegendText = "0.25 - 0.5"
                scheme.AddCategory(category2)

                'create another category
                Dim category3 As ColorCategory = New ColorCategory(0.4999, 0.75, Color.Red, Color.Red)
                category3.LegendText = "0.5 - 0.75"
                scheme.AddCategory(category3)

                Dim category4 As ColorCategory = New ColorCategory(0.7499, maxval + 0.001, Color.DarkRed, Color.DarkRed)
                category4.LegendText = "0.75 - " + maxval.ToString
                scheme.AddCategory(category4)
                '================================================
            ElseIf minval >= 0.25 And minval < 0.5 Then
                Dim category2 As ColorCategory = New ColorCategory(minval - 0.001, 0.5, Color.OrangeRed, Color.OrangeRed)
                category2.LegendText = minval.ToString + " - 0.5"
                scheme.AddCategory(category2)

                'create another category
                Dim category3 As ColorCategory = New ColorCategory(0.4999, 0.75, Color.Red, Color.Red)
                category3.LegendText = "0.5 - 0.75"
                scheme.AddCategory(category3)

                Dim category4 As ColorCategory = New ColorCategory(0.7499, maxval + 0.001, Color.DarkRed, Color.DarkRed)
                category4.LegendText = "0.75 - " + maxval.ToString
                scheme.AddCategory(category4)

            ElseIf minval >= 0.5 And minval < 0.75 Then
                Dim category3 As ColorCategory = New ColorCategory(0.4999, 0.75, Color.Red, Color.Red)
                category3.LegendText = minval.ToString + "  - 0.75"
                scheme.AddCategory(category3)

                Dim category4 As ColorCategory = New ColorCategory(0.7499, maxval + 0.001, Color.DarkRed, Color.DarkRed)
                category4.LegendText = "0.75 - " + maxval.ToString
                scheme.AddCategory(category4)

            ElseIf minval >= 0.75 And minval < 0.75 Then
                Dim category3 As ColorCategory = New ColorCategory(minval - 0.001, maxval + 0.001, Color.DarkRed, Color.DarkRed)
                category3.LegendText = minval.ToString + "  - " + maxval.ToString
                scheme.AddCategory(category3)
            End If

        ElseIf maxval > 0.5 And maxval <= 0.75 Then
            If minval < 0.25 Then
                Dim category1 As ColorCategory = New ColorCategory(minval - 0.001, 0.25, Color.Bisque, Color.Bisque)
                category1.LegendText = minval.ToString + " - 0.25"
                scheme.AddCategory(category1)

                Dim category2 As ColorCategory = New ColorCategory(0.2499, 0.5, Color.OrangeRed, Color.OrangeRed)
                category2.LegendText = "0.25 - 0.5"
                scheme.AddCategory(category2)

                'create another category
                Dim category3 As ColorCategory = New ColorCategory(0.4999, maxval + 0.001, Color.Red, Color.Red)
                category3.LegendText = "0.5 - " + maxval.ToString
                scheme.AddCategory(category3)

                '================================================
            ElseIf minval >= 0.25 And minval < 0.5 Then
                Dim category2 As ColorCategory = New ColorCategory(minval - 0.001, 0.5, Color.OrangeRed, Color.OrangeRed)
                category2.LegendText = minval.ToString + " - 0.5"
                scheme.AddCategory(category2)

                'create another category
                Dim category3 As ColorCategory = New ColorCategory(0.4999, maxval + 0.001, Color.Red, Color.Red)
                category3.LegendText = "0.5 - " + maxval.ToString
                scheme.AddCategory(category3)

            ElseIf minval >= 0.5 And minval < 0.75 Then
                Dim category3 As ColorCategory = New ColorCategory(0.4999, maxval + 0.001, Color.Red, Color.Red)
                category3.LegendText = minval.ToString + "  - " + maxval.ToString
                scheme.AddCategory(category3)
            End If
        ElseIf maxval > 2.5 And maxval <= 0.5 Then
            If minval < 0.25 Then
                Dim category1 As ColorCategory = New ColorCategory(minval - 0.001, 0.25, Color.Bisque, Color.Bisque)
                category1.LegendText = minval.ToString + " - 0.25"
                scheme.AddCategory(category1)

                Dim category2 As ColorCategory = New ColorCategory(0.2499, maxval + 0.001, Color.OrangeRed, Color.OrangeRed)
                category2.LegendText = "0.25 - " + maxval.ToString
                scheme.AddCategory(category2)

                '================================================
            ElseIf minval >= 0.25 And minval < 0.5 Then
                Dim category2 As ColorCategory = New ColorCategory(minval - 0.001, maxval + 0.001, Color.OrangeRed, Color.OrangeRed)
                category2.LegendText = minval.ToString + " - " + maxval.ToString
                scheme.AddCategory(category2)
            End If
        ElseIf maxval <= 2.5 Then
            If minval < 0.25 Then
                Dim category1 As ColorCategory = New ColorCategory(minval - 0.001, maxval + 0.001, Color.Bisque, Color.Bisque)
                category1.LegendText = minval.ToString + " - " + maxval.ToString
                scheme.AddCategory(category1)
            End If

        End If

        tlayer.Symbolizer.Scheme = scheme
        ''refresh the layer display in the map
        tlayer.WriteBitmap()
    End Sub

    Private Sub BarButtonItem7_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarButtonItem7.ItemClick
        Frm_Reclass.ShowDialog()
    End Sub




#Region "Thực hiện Đánh Thích Nghi Sinh thái (Bước 5)"
    Dim DatenfileKQ As String = "Đặt tên file đánh giá sinh thái"
    'Dim Filetontai As String = "File đã tồn tại, bạn có ghi đè không?"
    'Dim Thongbao As String = "Thông báo"
    'Dim Khongtheghide As String = "Không thể ghi đè file, có thể file đang mở." + vbNewLine + "Xin đổi tên file đích"
    Public Khongquyensudung As String = "Không có quyền sử dụng "
    Public LoiQuyentruycap As String = "Lỗi quyền truy cập"
    Public LoiThuchiencong As String = "Lỗi thực hiện cộng/nhân các Raster chuẩn hóa " + vbNewLine + "Có thể xảy ra bởi tình trạng thiếu bộ nhớ"
    Public Bancomuonxem As String = "Bạn có muốn xem bản đồ kết quả (chưa phân khoảng)?"
    Public Bancomuonphankhoang As String = "Bạn có muốn phân khoảng cho bản đồ kết quả?"
    Public chaythanhcong As String = "Chạy thành công bản đồ chuẩn hóa"
    Public saveFileDialg As New SaveFileDialog()
    Sub DGTN_ThanhPhan(sender As Control, TxtDGTN As TextEdit, ComboBoxEditSoLopDGTN As ComboBoxEdit, BtnRunDGTN_Reclass As Control)
        Dim resultname As String = ""
        TxtDGTN.Focus()
        resultname = TxtDGTN.Text

        If String.IsNullOrEmpty(resultname) = True Then
            Return
            'sender.enable = False
        End If
        'LabelControl1.Visible = True
        ProgressBarControl1.Visible = True
        ProgressBarControl2.Visible = True
        Dim thePath As String
        Dim theFN As String
        Dim tmppath As String = Path.GetTempPath()

        Try
            If (Not System.IO.Directory.Exists(tmppath + "nft")) Then
                System.IO.Directory.CreateDirectory(tmppath + "nft")
            End If
            tmppath = tmppath + "nft\"
        Catch ex As Exception
            MessageBox.Show(Khongquyensudung + tmppath, LoiQuyentruycap, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try
        xoatmp(tmppath)
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
        'Dim nameFilenhan As String
        'output.Filename = "Z:\out1.tif"
        ProgressBarControl1.EditValue = 0.1
        'GridcontrolLimit_NonLimit.DataSource = GridControl1.DataSource
        ProgressBarControl1.Properties.Step = 1
        ProgressBarControl1.Properties.PercentView = True
        GridView1.Columns("Limit").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")
        ProgressBarControl1.Properties.Maximum = GridView1.RowCount        'Lấy  count của toàn bộ các ROWs
        ProgressBarControl1.Properties.Minimum = 0
        ProgressBarControl1.Update()

        '==========
        '=====Tiến hành CỘNG Raster
        '==========
        GridView1.Columns("Limit").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")
        GridView1.Columns("Limit").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[Limit] = 'Không giới hạn' or [Limit] = 'Non-Limit'")
        ProgressBarControl1.Properties.Maximum = ProgressBarControl1.Properties.Maximum + GridView1.RowCount  'Cộng thêm 1 lần các chỉ tiêu Không giới hạn vì ở bước CỘNG này phải cập nhật ProgressBar1 2 lần
        'Lần đầu là tính Weight, Lần thứ 2 là Tính Cộng RasterMagic
        Dim listRaster As List(Of IRaster) = New List(Of IRaster)
        If GridView1.RowCount > 0 Then
            Dim i As Integer
            Try
                For i = 0 To GridView1.RowCount - 1
                    'SplashScreenManager.ShowForm(GetType(FrmWaiting))
                    theWeight = Convert.ToSingle(GridView1.GetRowCellValue(i, "Weight").ToString)
                    thePath = GridView1.GetRowCellValue(i, "Mapchuanhoa")       'Filename Included
                    theFN = Path.GetFileName(thePath)       'extension Included
                    SrcRaster = Raster.Open(thePath)
                    tmpCongFN = tmppath + "S_" + i.ToString + theFN
                    tmpWeightedFN = tmppath + "W_" + i.ToString + theFN
                    nameFilecong = tmpCongFN
                    GC.Collect()
                    WeightedRaster = WeightRaster(tmpWeightedFN, SrcRaster, ProgressBarControl2, theWeight)
                    ProgressBarControl1.PerformStep()
                    ProgressBarControl1.Update()
                    RasterDisp(SrcRaster, "")       'Dùng "" để không xóa file nguồn (chỉ close and dispose
                    If i = 0 Then   'Chạy vòng lặp lần đầu
                        listRaster.Add(WeightedRaster)  'ListRaster (0)     'Raster.Open(tmppath + "in1_" + i.ToString + theFN)
                    Else
                        Dim mymagic As New myRasterMagic
                        ProgressBarControl2.EditValue = 0.1
                        listRaster.Add(mymagic.RasterMath("Cộng", listRaster(listRaster.Count - 1), WeightedRaster, tmpCongFN, ProgressBarControl2, Nothing))
                        If i < GridView1.RowCount - 1 Then      'Để không dispose Raster kết quả cuối cùng
                            RasterDisp(listRaster(i - 1), listRaster(i - 1).Filename)
                            RasterDisp(WeightedRaster, WeightedRaster.Filename)
                        End If

                        'listRaster.RemoveAt(i - 1)
                    End If
                    ProgressBarControl1.PerformStep()
                    ProgressBarControl1.Update()
                    'ProgressBarControl1.Text = (i / GridView1.RowCount - 1).ToString
                    GC.Collect()
                    'File.Delete(tmppath + "S_" + (i - 1).ToString + Path.GetFileName(Gridview1.GetRowCellValue((i - 1), "Mapchuanhoa")))
                    'tmpCongFN = tmppath + "S_" + i.ToString + theFN
                    'Try
                    'SplashScreenManager.CloseForm()
                    'Catch
                    'End Try
                    'FrmRunning.ShowDialog()
                Next
                'listRaster(Gridview1.RowCount - 1).Filename = "z:\a.tif"
                'listRaster(Gridview1.RowCount - 1).SaveAs(resultname)
            Catch ex As Exception
                'MessageBox.Show(ex.Message)
                MessageBox.Show(LoiThuchiencong, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
                xoatmp(tmppath)
                Return
                Me.Close()

            End Try
            RasterDisp(WeightedRaster, WeightedRaster.Filename)
        End If



        '====================
        '===================='TIẾN HÀNH NHÂN RASTER
        '====================
        Dim countKhongGioiHan = listRaster.Count
        Dim tmpNhanFN As String = ""

        'Try
        'GridView1.ClearColumnsFilter()
        GridView1.Columns("Limit").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")
        GridView1.Columns("Limit").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[Limit] = 'Giới hạn' or [Limit] = 'Limit'")
        If GridView1.RowCount > 0 Then 'Thực hiện nhân các LIMIT
            For j As Integer = 0 To GridView1.RowCount - 1
                thePath = GridView1.GetRowCellValue(j, "Mapchuanhoa")       'Filename Included
                theFN = Path.GetFileName(thePath)       'extension Included
                SrcRaster = Raster.Open(thePath)
                tmpNhanFN = tmppath + "M_" + j.ToString + theFN
                GC.Collect()

                Dim mymagic As New myRasterMagic
                'Xét trường hợp J = 0 và J >0
                If listRaster.Count = 0 Then        'Tức là Không có kết quả cộng nào của vòng lặp KHÔNG GIỚI HẠN=> Add SrcRaster đầu tiên chứ chẳng nhân với cái gì cả
                    listRaster.Add(SrcRaster)
                Else
                    listRaster.Add(mymagic.RasterMath("Nhân", listRaster(listRaster.Count - 1), SrcRaster, tmpNhanFN, ProgressBarControl2, Nothing))       'listRaster(listRaster.Count-1): Là Raster cuối cùng của ListRas
                    'If j < Gridview1.RowCount - 1 Then      'Để không dispose Raster kết quả cuối cùng
                    RasterDisp(listRaster(listRaster.Count - 2), listRaster(listRaster.Count - 2).Filename) 'Lúc này phải là listRaster.Count - 2 (không phải là trừ 1 nữa) vì vừa add thêm 1 item vào list.
                    RasterDisp(SrcRaster, "")
                    'End If
                    '''listRaster.Add(mymagic.RasterMath("Nhân", listRaster(listRaster.Count - 1), SrcRaster, tmpNhanFN,ProgressBarControl4 , Nothing))       'listRaster(listRaster.Count-1): Là Raster cuối cùng của ListRas
                    '''RasterDisp(listRaster(listRaster.Count - 2), listRaster(listRaster.Count - 2).Filename) 'Lúc này phải là listRaster.Count - 2 (không phải là trừ 1 nữa) vì vừa add thêm 1 item vào list.
                    '''RasterDisp(SrcRaster, "")

                End If
                ProgressBarControl1.PerformStep()
                ProgressBarControl1.Update()


                'If j < Gridview1.RowCount - 1 Then		 'Để không dispose Raster kết quả cuối cùng
                '	'RasterDisp(listRaster(listRaster.Count - 1), listRaster(listRaster.Count - 1).Filename)
                'End If
                'FrmRunning.ShowDialog()

            Next

        End If


        listRaster(listRaster.Count - 1).SaveAs(resultname)
        Dim DGTN_Raster As IRaster = Raster.Open(resultname)
        DGTN_Raster.GetStatistics()
        For i As Integer = 0 To listRaster.Count - 1
            If listRaster(i).IsDisposed = False Then
                listRaster(i).Close()
                listRaster(i).Dispose()
            End If
        Next
        GridView2.SetRowCellValue(GridView2.FocusedRowHandle, "FromTo", DGTN_Raster.Minimum.ToString + "|" + DGTN_Raster.Maximum.ToString + "|1|")
        GridView2.UpdateCurrentRow()
        CreateFromToTxtbox(ComboBoxEditSoLopDGTNST, XtraScrollableControl3, GridView2.GetRow(GridView2.FocusedRowHandle), True, False)
        ComboBoxEditSoLopDGTN.SelectedIndex = 3
        GridView2.UpdateCurrentRow()
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")

        RasterDisp(DGTN_Raster, "")
        BtnRunDGTN_Reclass.Enabled = True
        Bar3.Text = "Thực hiện thành công đánh giá thích nghi sinh thái"
        ProgressBarControl1.Visible = False
        ProgressBarControl2.Visible = False
        'Catch ex As Exception
        'MessageBox.Show(ex.Message)
        'MessageBox.Show(LoiThuchiencong, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Return
        ''Me.Close()
        'BtnRunDGTNST_Reclass.Enabled = False
        'End Try

        'XtraTabControl1.SelectedTabPageIndex = 3
        '============Xóa các file tạm
        GC.Collect()
        Try
            For i = 0 To listRaster.Count - 1
                RasterDisp(listRaster(i), "")
            Next
        Catch ex As Exception
        End Try

        xoatmp(tmppath)
        GC.Collect()
        GridView1.Columns("Limit").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")
        GridView2.FocusedRowHandle = -1
        GridView2.FocusedRowHandle = 0
    End Sub
    Private Sub BtnRunDGTNST_Click(sender As Object, e As EventArgs) Handles BtnRunDGTNST.Click
        TxtDGTNST.Focus()
        DGTN_ThanhPhan(sender, TxtDGTNST, ComboBoxEditSoLopDGTNST, BtnRunDGTNST_Reclass)
    End Sub
    Private Sub BtnRunDGKTXH_Click(sender As Object, e As EventArgs) Handles BtnRunDGKTXH.Click
        TxtDGKTXH.Focus()
        DGTN_ThanhPhan(sender, TxtDGKTXH, ComboBoxEditSoLopDGKTXH, BtnRunDGKTXH_Reclass)
    End Sub
    Private Sub BtnRunDGTNMT_Click(sender As Object, e As EventArgs) Handles BtnRunDGTNMT.Click
        TxtDGTNMT.Focus()
        DGTN_ThanhPhan(sender, TxtDGTNMT, ComboBoxEditSoLopDGTNMT, BtnRunDGTNMT_Reclass)
    End Sub

    Function WeightRaster(ByVal outputFN As String, ByVal inpRaster As IRaster, ByVal progr As DevExpress.XtraEditors.ProgressBarControl, ByVal weight As Single) As IRaster

        '==Progress
        progr.EditValue = 0.1
        progr.Properties.PercentView = True
        progr.Properties.Step = 1
        progr.Properties.Maximum = (inpRaster.NumRows) '* (RasterMath.Bounds.NumColumns)
        progr.Properties.Minimum = 0
        progr.Update()
        '====

        Dim rasteroptions As String()
        Dim datatype As System.Type = System.Type.GetType("System.Single")
        WeightRaster = Raster.CreateRaster(outputFN, Nothing, inpRaster.NumColumns, inpRaster.NumRows, 1, inpRaster.DataType, rasteroptions)
        'Bounds specify the cellsize and the coordinates of raster corner
        With WeightRaster
            .Filename = outputFN
            .Bounds = inpRaster.Bounds
            .NoDataValue = -9999
            .Projection = inpRaster.Projection

            ''.NumRowsInFile = IndRaster.NumRows
            ''.NumColumns = IndRaster.NumColumns

        End With
        For i As Integer = 0 To inpRaster.NumRows - 1
            For j As Integer = 0 To inpRaster.NumColumns - 1
                If inpRaster.Value(i, j) <> inpRaster.NoDataValue And inpRaster.Value(i, j) > -9999 Then
                    WeightRaster.Value(i, j) = inpRaster.Value(i, j) * weight
                Else
                    WeightRaster.Value(i, j) = -9999
                End If
            Next
            progr.PerformStep()
            progr.Update()
        Next

        'If Not outputFN.Contains("abc123xyz.tif") Then
        'WeightRaster.Save()
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
        Dim filePaths As String() = Directory.GetFiles(tmppath)
        For Each filePath As String In filePaths
            Try
                File.Delete(filePath)

                'Array.ForEach(Directory.GetFiles(tmppath), File.Delete(tmppath))
            Catch ex As Exception
                Return
            End Try
        Next

    End Sub
    Public Sub RasterDisp(ByVal aRaster As IRaster, ByVal Rasterfilename As String)
        Try
            aRaster.Close()
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()
        Catch ex As Exception

        End Try
        Try
            aRaster.Dispose()
            GC.Collect()
            GC.WaitForPendingFinalizers()
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
#End Region

#Region "Phân lớp KQ ĐG TNST"
    'Dim KQDGTNSTminval As Double = 0
    'Dim KQDGTNSTmaxval As Double = 0
    Dim listTxteditPhanlopKQDGTNST As List(Of DevExpress.XtraEditors.TextEdit) = New List(Of DevExpress.XtraEditors.TextEdit)
    Dim listTxteditPhanlopFilter As List(Of DevExpress.XtraEditors.TextEdit) = New List(Of DevExpress.XtraEditors.TextEdit)
    Dim DGTNST_Raster As IRaster

    Dim chonfileRa As String = "Chọn file bản đồ Raster"
    Public Tu As String = "Từ"
    Public Den As String = "Đến"
    Public Giatrimoi As String = "Giá trị mới"
    Public Bancannha As String = "Bạn cần nhập giá trị lớn hơn giá trị trước" + vbNewLine + "và nhỏ hơn giá trị sau"
    Public Datten As String = "Đặt tên file Tif chuẩn hóa "
    Public Haynhapduong As String = "Hãy nhập đường dẫn file đầu vào"
    Public Haydatten As String = "Hãy đặt tên cho file đầu ra"
    Public Mo As String = "Mở bản đồ kết quả?"
    Public khongmoduocfile As String = "Không mở được file "
    'Private Sub ComboBoxEditSoLopDGTNST_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEditSoLopDGTNST.SelectedIndexChanged

    '	listTxteditPhanlopKQDGTNST.Clear()

    '	''For Each ctr In Groupcontrol3.Controls        'i As Integer = 0 To Groupcontrol3.Controls.Count - 1
    '	'GroupControl3.Controls.Clear()
    '	'Me.Height = 160
    '	''Next
    '	'GroupControl3.Refresh()
    '	XtraScrollableControl3.Controls.Clear()
    '	Dim DGTNRasterMinval As Integer
    '	Dim DGTNRasterMaxval As Integer
    '	Dim FromTo = GridView2.GetRowCellValue(GridView2.FocusedRowHandle, "FromTo")
    '	'If FromTo Is System.DBNull.Value Then
    '	'================MỞ LẠI FILE RAWfILTER

    '	Try
    '		Dim DGTNRaster As IRaster
    '		Dim filename = TxtDGTNST.Text
    '		SplashScreenManager.ShowForm(GetType(FrmWaiting))
    '		DGTNRaster = Raster.Open(filename)
    '		ComboBoxEditSoLopDGTNST.Enabled = True
    '		DGTNRaster.GetStatistics()
    '		DGTNRasterMinval = DGTNRaster.Minimum
    '		DGTNRasterMaxval = DGTNRaster.Maximum


    '		RasterDisp(DGTNRaster, "")	 '"" để close và dispose chứ ko xóa file gốc
    '		GC.Collect()
    '		Try
    '			SplashScreenManager.CloseForm()
    '		Catch

    '		End Try

    '	Catch ex As Exception
    '		'MessageBox.Show(ex.ToString)
    '		SplashScreenManager.CloseForm()

    '	End Try
    '	'Return
    '	'End If

    '	For i As Integer = 1 To ComboBoxEditSoLopDGTNST.SelectedIndex + 1

    '		Dim label1, label2, label3 As New Label
    '		Dim txteditFrom, txteditTo, txteditNewVal As New DevExpress.XtraEditors.TextEdit

    '		'label1 = New Label
    '		label1.Name = "labelFrom" + i.ToString
    '		label1.Size = New System.Drawing.Size(20, 13)
    '		label1.Location = New System.Drawing.Point(20, 15 + 25 * (i - 1))
    '		label1.Text = Tu

    '		txteditFrom.Name = "txtFrom" + i.ToString
    '		txteditFrom.Size = New System.Drawing.Size(52, 20)
    '		txteditFrom.Location = New System.Drawing.Point(50, 11 + 25 * (i - 1))

    '		txteditFrom.Properties.Mask.BeepOnError = True
    '		txteditFrom.Properties.Mask.EditMask = "f5"
    '		txteditFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
    '		txteditFrom.Properties.ReadOnly = True

    '		If i = 1 Then
    '			txteditFrom.Text = KQDGTNSTminval
    '		Else
    '			'(maxval - minval) / (ComboBoxEdit1.SelectedIndex + 1) là bước nhảy
    '			txteditFrom.Text = (KQDGTNSTmaxval - KQDGTNSTminval) / (ComboBoxEditSoLopDGTNST.SelectedIndex + 1) * (i - 1)	   '((maxval - minval) / (i)) * (i - 1)
    '		End If

    '		label2.Name = "labelTo" + i.ToString
    '		label2.Size = New System.Drawing.Size(27, 13)
    '		label2.Location = New System.Drawing.Point(110, 15 + 25 * (i - 1))
    '		label2.Text = Den

    '		txteditTo.Name = "txtTo" + i.ToString
    '		txteditTo.Size = New System.Drawing.Size(52, 20)
    '		txteditTo.Location = New System.Drawing.Point(145, 11 + 25 * (i - 1))
    '		txteditTo.Text = (KQDGTNSTmaxval - KQDGTNSTminval) / (ComboBoxEditSoLopDGTNST.SelectedIndex + 1) * i '((maxval - minval) / (i + 1)) * (i)
    '		txteditTo.Properties.Mask.BeepOnError = True
    '		txteditTo.Properties.Mask.EditMask = "f5"
    '		txteditTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
    '		If i = ComboBoxEditSoLopDGTNST.SelectedIndex + 1 Then
    '			txteditTo.Text = KQDGTNSTmaxval
    '			txteditTo.Properties.ReadOnly = True
    '		End If
    '		AddHandler txteditTo.KeyUp, AddressOf txteditTo_KeyUp
    '		AddHandler txteditTo.InvalidValue, AddressOf txteditTo_InvalidValue
    '		AddHandler txteditTo.Validating, AddressOf txteditTo_Validating

    '		label3.Name = "labelNewValue" + i.ToString
    '		label3.Size = New System.Drawing.Size(54, 13)
    '		label3.Location = New System.Drawing.Point(210, 15 + 25 * (i - 1))
    '		label3.Text = Giatrimoi

    '		txteditNewVal.Name = "txtNewValue" + i.ToString
    '		txteditNewVal.Size = New System.Drawing.Size(28, 20)
    '		txteditNewVal.Location = New System.Drawing.Point(275, 11 + 25 * (i - 1))
    '		txteditNewVal.Text = i.ToString
    '		'txteditNewVal.Properties.Mask.BeepOnError = True
    '		'txteditNewVal.Properties.Mask.EditMask = "n0"
    '		'txteditNewVal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
    '		'txteditNewVal.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True
    '		txteditNewVal.Properties.ReadOnly = True


    '		XtraScrollableControl3.Controls.Add(label1)
    '		XtraScrollableControl3.Controls.Add(label2)
    '		XtraScrollableControl3.Controls.Add(label3)
    '		XtraScrollableControl3.Controls.Add(txteditFrom)
    '		XtraScrollableControl3.Controls.Add(txteditTo)
    '		XtraScrollableControl3.Controls.Add(txteditNewVal)

    '		listTxteditPhanlopKQDGTNST.Add(txteditFrom)
    '		listTxteditPhanlopKQDGTNST.Add(txteditTo)
    '		listTxteditPhanlopKQDGTNST.Add(txteditNewVal)
    '		'Me.Size = New System.Drawing.Size(Me.Width, 155 + 25 * (i - 1))
    '		'GroupControl4.Height = 311 + 25 * (i - 1)

    '		'ProgressBarControl1.Location = New System.Drawing.Point(811, ProgressBarControl1.Location.Y)

    '		ProgressBarControl2.Visible = False
    '	Next
    '	'Dim a = getListTxtBoxVal(sender, XtraScrollableControl3)
    '	''Dim UPDATECommand As String = "UPDATE [" + ListBoxControl1.Tag.tostring + "] SET FromTo = '" + a + "'WHERE Name = '" + ListBoxControl1.Tag.tostring + "'"
    '	''Gridview2_Connect.RunaSQLCommand(GridControl3.DataSource, UPDATECommand)
    '	''Me.Refresh()
    '	GridView2.SetRowCellValue(0, "FromTo", a)
    '	Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.tostring + "]")
    '	GridView2.UpdateCurrentRow()
    'End Sub
    Function getListTxtBoxVal(ComboBoxEditSoLop As ComboBoxEdit, scrollable As XtraScrollableControl) As String
        getListTxtBoxVal = ""

        For i As Integer = 0 To ComboBoxEditSoLop.SelectedIndex
            Dim Fromtxtbox As DevExpress.XtraEditors.TextEdit = FrmMain.FindControl(scrollable, "txtFrom" + i.ToString)
            If Fromtxtbox Is Nothing Then   'Trường hợp ko có textbox nào
                Exit For
            End If
            Dim Totxtbox As DevExpress.XtraEditors.TextEdit = FrmMain.FindControl(scrollable, "txtTo" + i.ToString)
            Dim NewValtxtbox As DevExpress.XtraEditors.TextEdit = FrmMain.FindControl(scrollable, "txtNewValue" + i.ToString)
            getListTxtBoxVal = getListTxtBoxVal + Fromtxtbox.Text + "|" + Totxtbox.Text + "|" + NewValtxtbox.Text + "|"
        Next
        Return getListTxtBoxVal
    End Function
    Sub DGTN_Reclass(sender As Control, TxtDGTN As TextEdit, TxtDGTN_Reclass As TextEdit, ComboBoxEditSoLopDGTN As ComboBoxEdit, BtnRunDGTN_Reclass As Control)
        If TxtDGTN.Text = "" Then
            MessageBox.Show(Haynhapduong)
            Return
        ElseIf TxtDGTN_Reclass.Text = "" Then
            MessageBox.Show(Haydatten)
            Return
        End If
        ProgressBarControl3.EditValue = 0
        ProgressBarControl3.Update()
        Dim DGTN_Raster = Raster.Open(TxtDGTN.Text)
        '=======
        'Me.Size = New System.Drawing.Size(Me.Width, 175 + 25 * (ComboBoxEdit1.SelectedIndex))
        'ProgressBarControl1.Location = New System.Drawing.Point(111, 115 + 25 * (ComboBoxEdit1.SelectedIndex))
        ProgressBarControl3.Visible = True
        ProgressBarControl3.Properties.Step = 1
        ProgressBarControl3.Properties.PercentView = True
        ProgressBarControl3.Properties.Maximum = (DGTN_Raster.NumRows - 1) * (ComboBoxEditSoLopDGTN.SelectedIndex + 1)   'Lấy  count của toàn bộ các ROWs
        ProgressBarControl3.Properties.Minimum = 0
        '===========
        Dim rasteroptions As String()
        Dim datatype As System.Type = System.Type.GetType("System.Int16")
        Dim reclassedRaster As IRaster = Raster.CreateRaster(TxtDGTN_Reclass.Text, Nothing, DGTN_Raster.NumColumns, DGTN_Raster.NumRows, 1, datatype, rasteroptions)


        Try
            With reclassedRaster
                .Filename = TxtDGTN_Reclass.Text
                .Bounds = DGTN_Raster.Bounds
                .NoDataValue = -9999
                .Projection = DGTN_Raster.Projection

                ''.NumRowsInFile = IndRaster.NumRows
                ''.NumColumns = IndRaster.NumColumns

            End With
            For i As Integer = 0 To ComboBoxEditSoLopDGTN.SelectedIndex
                Dim txtFrom = Convert.ToDouble(FindControl(XtraScrollableControl3, "txtFrom" + (i).ToString).Text)     'Convert.ToDouble(listTxteditPhanlopKQDGTNST(i * 3 + 0).Text)		 'PanelControl1.Controls.Find("txtFrom" + i.ToString, False)(0) 'FrmStart.FindControl(PanelControl1, "txtFrom" + i.ToString)
                Dim txtTo = Convert.ToDouble(FindControl(XtraScrollableControl3, "txtTo" + (i).ToString).Text)         'Convert.ToDouble(listTxteditPhanlopKQDGTNST(i * 3 + 1).Text)	  'PanelControl1.Controls.Find("txtTo" + i.ToString, False)(0) 'FrmStart.FindControl(PanelControl1, "txtTo" + i.ToString)
                Dim txtNewValue = Convert.ToInt16(FindControl(XtraScrollableControl3, "txtNewValue" + (i).ToString).Text)       'Convert.ToInt16(listTxteditPhanlopKQDGTNST(i * 3 + 2).Text)	  'PanelControl1.Controls.Find("txtNewValue" + i.ToString, False)(0)   'FrmStart.FindControl(PanelControl1, "txtNewValue" + i.ToString)

                '''''****03.01.2019**** Thêm đoạn này vào để sửa cái revert (trong phiên bản cũ: phân lớp KTXH và MT bị ngược lại, càng tích cực thì giá trị mới càng bị bé trong giao diện)
                '''''Trong phiên bản mới, càng tích cực thì trên giao diện có số càng to. Và Do đó phải làm thêm 1 bước chỉnh sửa này để cho phù hợp logic trong bước 5; Đánh giá tổng hợp
                ''''THôi, để sửa ở phần Tổng hợp ở bước 5
                ''''If TxtDGTN_Reclass.Name <> "TxtDGTNST_Reclass" Then
                ''''txtNewValue = ComboBoxEditSoLopDGTN.SelectedIndex - txtNewValue
                ''''End If

                If i = ComboBoxEditSoLopDGTN.SelectedIndex Then
                    txtTo = txtTo + 1 'Để trường hợp <txtTo sẽ trở thành txtTo + 1 Nếu ko giá trị max sẽ bị loại khỏi kết quả
                End If
                For k As Integer = 0 To DGTN_Raster.NumRows - 1
                    For j As Integer = 0 To DGTN_Raster.NumColumns - 1

                        If DGTN_Raster.Value(k, j) <> DGTN_Raster.NoDataValue And DGTN_Raster.Value(k, j) > -9999 Then
                            If DGTN_Raster.Value(k, j) >= txtFrom AndAlso DGTN_Raster.Value(k, j) < txtTo Then
                                reclassedRaster.Value(k, j) = txtNewValue
                                'Exit For
                            Else

                            End If
                        Else
                            reclassedRaster.Value(k, j) = -9999
                        End If

                    Next
                    ProgressBarControl3.PerformStep()
                    ProgressBarControl3.Update()
                Next
            Next
            reclassedRaster.Save() 'As(Txtoutput.Text)
            reclassedRaster.GetStatistics()

            ProgressBarControl3.Visible = False

            RasterDisp(DGTN_Raster, "")
            RasterDisp(reclassedRaster, "")
            Dim ok As DialogResult = MessageBox.Show(Mo, thongbao, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ok = Windows.Forms.DialogResult.Yes Then
                Viewer(TxtDGTN_Reclass.Text)
            End If
            'MessageBox.Show("Tính toán thành công hàm chuẩn hóa")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'MessageBox.Show("Xem lại các giá trị nhập cho a, b, c, d" + vbNewLine + "Xem lại các giá trị vào, ra của chỉ tiêu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub BtnRunDGTNST_Reclass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRunDGTNST_Reclass.Click
        TxtDGTNST_Reclass.Focus()
        DGTN_Reclass(sender, TxtDGTNST, TxtDGTNST_Reclass, ComboBoxEditSoLopDGTNST, BtnRunDGTNST_Reclass)
    End Sub
    Private Sub BtnRunDGKTXH_Reclass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRunDGKTXH_Reclass.Click
        TxtDGKTXH_Reclass.Focus()
        DGTN_Reclass(sender, TxtDGKTXH, TxtDGKTXH_Reclass, ComboBoxEditSoLopDGKTXH, BtnRunDGKTXH_Reclass)
    End Sub
    Private Sub BtnRunDGTNMT_Reclass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRunDGTNMT_Reclass.Click
        TxtDGTNMT_Reclass.Focus()
        DGTN_Reclass(sender, TxtDGTNMT, TxtDGTNMT_Reclass, ComboBoxEditSoLopDGTNMT, BtnRunDGTNMT_Reclass)
    End Sub
    Public Sub Viewer(therasterFN As String)
        'Dim theRaster As Raster = sender.tag
        'Dim frmmapviewer = New MapViewer.Form1
        'Dim a = theRaster.Filename
        GC.Collect()


        Dim inpRaster As IRaster

        Try
            SplashScreenManager.ShowForm(GetType(FrmWaiting))
            Try
                inpRaster = Raster.Open(therasterFN)
            Catch ex As Exception
                MessageBox.Show(khongmoduocfile + therasterFN, thongbao)
                Return

            End Try
            Dim tlayer As IMapRasterLayer = frmMapviewer.Map1.Layers.Add(inpRaster)
            'Dim tlayer As IMapRasterLayer = frmMapviewer.Map1.AddLayer(therasterFN)
            'FrmStart.RasterLayersymbology(tlayer)

            frmMapviewer.Show()
            'frmMapviewer.BringToFront()
            'Application.OpenForms("frmMapviewer").BringToFront()
            frmMapviewer.Activate()
            frmMapviewer.TopLevel = True
            Try
                SplashScreenManager.CloseForm()
            Catch

            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Try
                SplashScreenManager.CloseForm()
            Catch

            End Try

        End Try

    End Sub

    Function checkColumExist_Addcolumn(ByVal fs As IFeatureSet, ByVal fieldname As String) As Boolean
        'Kiểm tra sự tồn tại của fiel newcode
        checkColumExist_Addcolumn = False
        For Each col As DataColumn In fs.DataTable.Columns
            If col.ColumnName = fieldname Then
                checkColumExist_Addcolumn = True
                Exit For
            End If

        Next
        If checkColumExist_Addcolumn = False Then 'nếu chưa tồn tại thì add
            Dim newcol As DataColumn = New DataColumn(fieldname, GetType(Int16))
            fs.DataTable.Columns.Add(newcol)
        End If
        fs.Save()
    End Function

    Private Sub txteditTo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim myname As String = sender.name
        Dim stt As Integer = myname.Replace("txtTo", "")
        Try
            Dim nexttxtbox As DevExpress.XtraEditors.TextEdit = FrmMain.FindControl(XtraScrollableControl3, "txtFrom" + (stt + 1).ToString)
            nexttxtbox.Text = sender.text
        Catch ex As Exception

        End Try
        Dim a = getListTxtBoxVal(ComboBoxEditSoLopDGTNST, XtraScrollableControl3)


        GridView2.SetRowCellValue(0, "FromTo", a)
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")
    End Sub

    Private Sub txteditTo_InvalidValue(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs)
        e.ErrorText = Bancannha
    End Sub
    Private Sub txteditTo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim myname As String = sender.name
        Dim stt As Integer = myname.Replace("txtTo", "")
        Try
            Dim nexttxtbox1 As DevExpress.XtraEditors.TextEdit = FrmMain.FindControl(XtraScrollableControl3, "txtTo" + (stt + 1).ToString)
            If Not nexttxtbox1 Is Nothing Then
                If sender.Text <> "" And nexttxtbox1.Text <> "" Then
                    If Convert.ToSingle(sender.Text.Replace(",", "")) >= Convert.ToSingle(nexttxtbox1.Text.Replace(",", "")) Then   ' or Convert.ToDouble(sender.Text.Replace(",", "")) >= FilterRasterMaxval Or Convert.ToDouble(sender.Text.Replace(",", "")) <= FilterRasterMinval Then
                        e.Cancel = True
                    End If
                    'ElseIf sender.Text = "" Or nexttxtbox1.Text = "" Then
                    'If Convert.ToDouble(sender.Text.Replace(",", "")) >= FilterRasterMaxval Or Convert.ToDouble(sender.Text.Replace(",", "")) <= FilterRasterMinval Then
                    '		e.Cancel = True
                    '	End If

                End If
            Else

            End If
            Dim previoustxtbox1 As DevExpress.XtraEditors.TextEdit = FrmMain.FindControl(XtraScrollableControl3, "txtTo" + (stt - 1).ToString)
            If Not previoustxtbox1 Is Nothing Then
                If sender.Text <> "" And previoustxtbox1.Text <> "" Then
                    If Convert.ToDouble(sender.Text.Replace(",", "")) <= Convert.ToSingle(previoustxtbox1.Text.Replace(",", "")) Then
                        e.Cancel = True
                    End If
                End If
                'ElseIf sender.Text = "" Or nexttxtbox1.Text = "" Then
                '	If Convert.ToDouble(sender.Text.Replace(",", "")) >= FilterRasterMaxval Or Convert.ToDouble(sender.Text.Replace(",", "")) <= FilterRasterMinval Then
                '		e.Cancel = True
                '	End If
            End If

        Catch ex As Exception

        End Try
    End Sub


    Private Sub BtnSavePhanlopKQDGTNST_Click(sender As Object, e As EventArgs) Handles BtnBrowseDGTNST_Reclass.Click
        TxtDGTNST_Reclass.Focus()
        TxtDGTNST_Reclass.Text = saveRasterDialog(True)
        GridView2.SetRowCellValue(0, "RecTif", TxtDGTNST_Reclass.Text)
        GridView2.UpdateCurrentRow()
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")
    End Sub
    Private Sub BtnSavePhanlopKQDGKTXH_Click(sender As Object, e As EventArgs) Handles BtnBrowseDGKTXH_Reclass.Click
        TxtDGKTXH_Reclass.Focus()
        TxtDGKTXH_Reclass.Text = saveRasterDialog(True)
        GridView2.SetRowCellValue(1, "RecTif", TxtDGKTXH_Reclass.Text)
        GridView2.UpdateCurrentRow()
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")
    End Sub
    Private Sub BtnSavePhanlopKQDGTNMT_Click(sender As Object, e As EventArgs) Handles BtnBrowseDGTNMT_Reclass.Click
        TxtDGTNMT_Reclass.Focus()
        TxtDGTNMT_Reclass.Text = saveRasterDialog(True)
        GridView2.SetRowCellValue(2, "RecTif", TxtDGTNMT_Reclass.Text)
        GridView2.UpdateCurrentRow()
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")
    End Sub
    Private Sub BrowseDG_TNST_KTXH_TNMT(TxtDG As TextEdit, Txt_Reclass As TextEdit, ComboBoxEditSoLop As ComboBoxEdit, BtnRunReclass As Button, revert As Boolean)
        Dim filename As String = saveRasterDialog(True)
        Dim DG_Raster As IRaster
        'AppManager1.Directories.Add("Plugins")
        Try
            SplashScreenManager.ShowForm(GetType(FrmWaiting))
            DG_Raster = Raster.Open(filename)
            DG_Raster.GetStatistics()
            If DG_Raster.Minimum = DG_Raster.Maximum Then
                MessageBox.Show(Loibando + vbNewLine + filename, thongbao)
                Return
            End If


            GridView2.SetRowCellValue(GridView2.FocusedRowHandle, "FromTo", DG_Raster.Minimum.ToString + "|" + DG_Raster.Maximum.ToString + "|1|")
            GridView2.UpdateCurrentRow()

            CreateFromToTxtbox(ComboBoxEditSoLop, XtraScrollableControl3, GridView2.GetRow(GridView2.FocusedRowHandle), True, revert)
            ComboBoxEditSoLop.SelectedIndex = 3
            'Txt_cellsize.Text = DGTNST_Raster.CellHeight
            'ComboBoxEditSoLop.SelectedIndex = 1
            'ComboBoxEditSoLop.SelectedIndex = 3
            ComboBoxEditSoLop.Enabled = True

            BtnRunReclass.Enabled = True
            RasterDisp(DG_Raster, "")   '"" để close và dispose chứ ko xóa file gốc
            GC.Collect()
            Try
                SplashScreenManager.CloseForm()
            Catch

            End Try

        Catch ex As Exception
            'MessageBox.Show(ex.ToString)
            Try
                SplashScreenManager.CloseForm()
            Catch

            End Try

            'ComboBoxEditSoLop.Enabled = False
            XtraScrollableControl3.Controls.Clear()
            BtnRunReclass.Enabled = False
            'Return
        End Try
        TxtDG.Text = filename
        If Txt_Reclass.Text = "" Then
            Txt_Reclass.Text = TxtDG.Text.Replace(".tif", "") + "_rec.tif"
        End If
        GridView2.SetRowCellValue(GridView2.FocusedRowHandle, "SrcTif", TxtDG.Text)
        GridView2.SetRowCellValue(GridView2.FocusedRowHandle, "RecTif", Txt_Reclass.Text)
        GridView2.UpdateCurrentRow()
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")
        TxtDG.Focus()       'Để chạy event TxtDG..._GotFocus
        'Myconnect.createTable("Cây cà phê", "([Field1] TEXT(10), [Field2] TEXT(10))")
    End Sub
    Private Sub BtnBrowseDGTNST_Click(sender As Object, e As EventArgs) Handles BtnBrowseDGTNST.Click
        TxtDGTNST.Focus()
        BrowseDG_TNST_KTXH_TNMT(TxtDGTNST, TxtDGTNST_Reclass, ComboBoxEditSoLopDGTNST, BtnRunDGTNST_Reclass, False)
    End Sub
    Private Sub BtnBrowseDGKTXH_Click(sender As Object, e As EventArgs) Handles BtnBrowseDGKTXH.Click
        TxtDGKTXH.Focus()
        BrowseDG_TNST_KTXH_TNMT(TxtDGKTXH, TxtDGKTXH_Reclass, ComboBoxEditSoLopDGKTXH, BtnRunDGKTXH_Reclass, True)
    End Sub
    Private Sub BtnBrowseDGTNMT_Click(sender As Object, e As EventArgs) Handles BtnBrowseDGTNMT.Click
        TxtDGTNMT.Focus()
        BrowseDG_TNST_KTXH_TNMT(TxtDGTNMT, TxtDGTNMT_Reclass, ComboBoxEditSoLopDGTNMT, BtnRunDGTNMT_Reclass, True)
    End Sub
    Function saveRasterDialog(OverwritePrompt As Boolean) As String
        Dim saveFileDialg As New SaveFileDialog()

        saveFileDialg.Title = Datten
        'openFileDialog.InitialDirectory = Application.StartupPath + "\Images"
        saveFileDialg.Filter = "Tiff file|*.tif"
        'AppManager1.
        saveFileDialg.FilterIndex = 1
        saveFileDialg.RestoreDirectory = True
        saveFileDialg.OverwritePrompt = OverwritePrompt
        Dim fileName As String = "" 'TxtMap.Text
        If saveFileDialg.ShowDialog() = DialogResult.OK Then
            fileName = saveFileDialg.FileName

        End If
        Return fileName
    End Function
    Function OpenRasterDialog() As String
        Dim OpenFileDialg As New OpenFileDialog()

        OpenFileDialg.Title = Datten
        'openFileDialog.InitialDirectory = Application.StartupPath + "\Images"
        OpenFileDialg.Filter = "Tiff file|*.tif"
        'AppManager1.
        OpenFileDialg.FilterIndex = 1
        OpenFileDialg.RestoreDirectory = True

        Dim fileName As String = "" 'TxtMap.Text
        If OpenFileDialg.ShowDialog() = DialogResult.OK Then
            fileName = OpenFileDialg.FileName

        End If
        Return fileName
    End Function
#End Region

    Sub EnglishTranslate()
        mucdoquantrong = "Influence levels"
        Taocaymoi = "Create a new object"
        caymoi = "Input a name for of object"
        tencaytontai = "The object is existed!"
        loi = "Error"
        thongbao = "Information"
        Copycay = "Copy  "
        Copycay1 = " to "
        Copycay2 = "Copy object"
        Doitencay = "Rename object from "
        Doitencay1 = " to "
        Doitencay2 = "Rename object"
        Xoacay = "Are you sure to delete the object? "
        Tenchitieu = "Input indicator name"
        chitieutontai = "The indicator is existed"
        aHamhinhthang = "Trapezoidal membership function"
        giatribc = "Input value for B and C (B < C)!"
        trogiuphinhthang = "Referencing to Trapezoidal membership function section in user guide."
        txtaInvalidValueHinhthang = "You have to input a new value < B < C < D" + vbNewLine + trogiuphinhthang
        txtbInvalidValueHinhthang = "You have to input a new value < C < D and > A" + vbNewLine + trogiuphinhthang
        txtcInvalidValueHinhthang = "You have to input a new value < D and > B > A" + vbNewLine + trogiuphinhthang
        txtdInvalidValueHinhthang = "You have to input a new value > C > B > A" + vbNewLine + trogiuphinhthang
        aHamkandel = "Kandel membership function"
        giatriab = "Input value for A and B (A < B)!"
        trogiupkandel = "Referencing to Kandel membership function section in user guide."
        txtaInvalidValueKandel = "You have to input a new value < B" + vbNewLine + trogiupkandel
        txtbInvalidValueKandel = "You have to input a new value > A" + vbNewLine + trogiupkandel
        aHams1 = "S1 - membership function"
        aHams2 = "S2 - membership function"
        trogiupS = "Referencing to S - membership function section in user guide."
        txtaInvalidValueHamS = "You have to input a new value < B" + vbNewLine + trogiupS
        txtbInvalidValueHamS = "You have to input a new value > A" + vbNewLine + trogiupS
        aHamtheoloai = "Membership function of classes"
        ahamKhongham = "Not run standardizing"
        Chacchanxoa = "Are you sure to delete?"
        Xoathanhcong = "Deleted successful!"
        Chacchanluu = "Do you want to save project?"
        Luuthanhcong = "Saved successful!"
        Kiemtrachitieu = "Checking for valid indicator map data!"
        Kiemtrachuanhoa = "Checking for valid standardized map data!"
        Bandoloi = "The indicator map data is invalid!"
        Bandochuanhoaloi = "The standardized map data is invalid!"
        filetontai = "The file is existed, do you want to replace?"
        Khongkichhoatbuoc4 = "Can’t active, go to step 4 to check standardized map data!"
        Khongkichhoatbuoc3 = "Can’t active, go to step 3 to check indicator map data!"
        Buoc4 = "Step 4"
        Buoc5 = "Step 5"
        Khongtheghide = "Can’t replace, check to sure the file doesn’t  open."
        Filechuanhoakhac = "Using another file name!"
        nhapfiletif = "Input a name for standardized map data (file *.Tif)."
        Khongmofile = "Can’t open "
        Loibando = "Invalid map data."
        haychonproject = "Select a project to continue!"
        OpenProject = "Choose a file name"
        filedichdangmo = "The target file is not replaced, select another file!"
        khongthexuat = "Can’t export data"
        Tinhtoanthanhcong = "Create standardized map data successful!"
        Nhomchitieu = "Indicator group"
        Chitieu = "Indicator name"
        Hamsudung = "Used function"
        Bandochitieu = "Criteria data"
        Bandochuanhoa = "Standardized data"
        trongso = "Weights"
        Gioihan = "Types of criteria"
        Themchitieu = "Add an indicator"
        Sapxeptangdan = "Sort"
        Sapxepgiamdan = "Sort decrease"
        Bosapxep = "Remove sorted"
        Datvuadorongcot = "Best fit to column"
        Datvuadorongmoicot = "Best fit to all columns"
        Suaduongdan = "Change path for data"
        Xaydungbandochuanhoa = "Create standardized map data"
        Xemthubando = "View standardized map data"
        projectDGTN = "Project|*.nft"
        CTDGTN = "Land suitability assessment - "
        Xem = "Map view"
        SavePrj = "Save project"
        savetoExcelTitle = "Export criteria data to Excel file"
        SavedPrj = "Project is saved successful"
        Chonraster = "Choose a Tif file"
        filedulieutontai = "Data file is existed"
        filedulieuKhongtontai = "Data file is not existed"
        trongso1 = "Sum of weighting should be 1! Please go back to the previous tab to fix the error"
        DatenfileKQ = "Input a file name"
        'filetontai = "File is existing, do you want to replace?"
        'thongbao = "Information"
        'Khongtheghide = "Can not replace, the file could be opening" + vbNewLine + "Please change the ouput file"
        Khongquyensudung = "Can not access"
        LoiQuyentruycap = "Error in file accessing"
        LoiThuchiencong = "Error is happened by lacking of memory"

        Bancomuonxem = "Do you want to view the result (have not been reclassed yet)?"
        Bancomuonphankhoang = "Do you want to reclass for the result data?"
        chaythanhcong = "Evaluating land suitability successful"

        'Reclass bước 5
        chonfileRa = "Choose an input Raster"
        Tu = "From"
        Den = "To"
        Giatrimoi = "New value"
        Bancannha = "New value should be greater than previous value" + vbNewLine + "and less than later value"
        Datten = "Input a name for ouput standized raster"
        Haynhapduong = "Input a full file name for input raster"
        Haydatten = "Input a name for ouput raster"
        Mo = "Open output file?"
        thongbao = "Information"
        khongmoduocfile = "Can not open file "
        NhapgiatriPhankhoang = "Input reclasses value"
        Boloctontai = "The filter name is existing"
        ghichu = "Note"
        DulieuTNST = "The partial performance scores"
        DulieuTNSTReclass = "The partial performance indices"
        Tenboloc = "Filter name"
        Dulieuboloctho = "Raw data"
        Dulieubolocreclass = "Reclass data"
        Giatriphankhoang = "Classifying values"
        NameF = "Data type groups"
        SrcTifF = "Source tif files"
        RecTifF = "Reclassed tif files"

        TNST = "Agro-ecological aptitude"
        TNKTXH = "Socio-economic feasibility"
        TDMT = "Environment impact"
        HanChe = "Constraint factor"
        Enc_Check = "If Encs is checked then only one Environment criterion and one socio-economic criterion are used" + vbNewLine + "Please delete criteria"
        Enc_KoCheck = "If Encs is checked then at least two Environment criteria or two socio-economic criteria are used" + vbNewLine + "Please add more criteria"
    End Sub
    Private Sub FrmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'If BtnUpdate.Enabled = True Then
        'BtnUpdate_click(Nothing, Nothing)
        'End If
    End Sub
    Private Sub FrmStart_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        '============Xóa các file tạm
        Dim tmppath As String = Path.GetTempPath() + "nft\"
        If System.IO.Directory.Exists(tmppath) Then
            Dim s As String
            For Each s In System.IO.Directory.GetFiles(tmppath)
                Try
                    System.IO.File.Delete(s)
                Catch ex As Exception

                End Try

            Next s
        End If

        'FrmStartup.Close()
        Application.Exit()
    End Sub

#Region "Tab6"
    'Dim FilterRasterMinval As Single
    'Dim FilterRasterMaxval As Single
    Private Sub BtnBrowseRawFilter_Click(sender As Object, e As EventArgs) Handles BtnBrowseRawFilter.Click
        'Dim oldfile = TxtFilterName.Text
        'TxtFilterName.Text = ""
        Dim FilterRaster As IRaster
        Dim filename = FillPath_byOpendialog("")
        'saveRasterDialog(False)
        If filename = "" Then
            Return
        End If
        Try

            SplashScreenManager.ShowForm(GetType(FrmWaiting))
            FilterRaster = Raster.Open(filename)
            If FilterRaster.Minimum = FilterRaster.Maximum Then
                MessageBox.Show(Loibando + vbNewLine + filename, thongbao)
                Return
            End If
            TxtRawdata.Text = filename
            'ComboBoxEdit_solopFilter.Enabled = True
            FilterRaster.GetStatistics()

            GridView2.SetRowCellValue(GridView2.FocusedRowHandle, "FromTo", FilterRaster.Minimum.ToString + "|" + FilterRaster.Maximum.ToString + "|1|")
            GridView2.UpdateCurrentRow()
            CreateFromToTxtbox(ComboBoxEdit_solopFilter, XtraScrollableControl4, GridView2.GetRow(GridView2.FocusedRowHandle), True, False)
            ComboBoxEdit_solopFilter.SelectedIndex = 1
            'Txt_cellsize.Text = DGTNST_Raster.CellHeight
            'ComboBoxEdit_solopFilter.SelectedIndex = 1
            'ComboBoxEdit_solopFilter.SelectedIndex = 3
            'ComboBoxEdit_solopFilter_SelectedIndexChanged(Nothing, Nothing)
            'ComboBoxEdit_solopFilter.Enabled = True
            TxtRawdata.Text = filename
            'GridView3.SetRowCellValue(GridView3.FocusedRowHandle, "FromTo", "")
            If TxtFilterReclass.Text = "" Then
                TxtFilterReclass.Text = TxtRawdata.Text.Replace(".tif", "") + "_rec.tif"
            End If


            'BtnRunFilterReclass.Enabled = True
            RasterDisp(FilterRaster, "")   '"" để close và dispose chứ ko xóa file gốc
            GC.Collect()
            Try
                SplashScreenManager.CloseForm()
            Catch

            End Try

        Catch ex As Exception
            'MessageBox.Show(ex.ToString)
            Try
                SplashScreenManager.CloseForm()
            Catch

            End Try

            'ComboBoxEdit_solopFilter.Enabled = False
            XtraScrollableControl4.Controls.Clear()
            'BtnRunFilterReclass.Enabled = False
        End Try
        GridView2.UpdateCurrentRow()
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")
    End Sub

    Private Sub BtnAddBoloc_Click(sender As Object, e As EventArgs) Handles BtnAddBoloc.Click
        If GridView2.GetDataRow(GridView2.GetSelectedRows(0))("Name").ToString = "" Then     'Nếu chưa có bộ lọc nào thì chương trình tự tạo ra 1 row với bộ lọc chưa có tên, lúc này hãy rename nó đi và thoát
            BtnRenameBoloc_Click(Nothing, Nothing)
            Return
        End If
        Dim FilterName = InputBox("Input a Filter name")
        If FilterName = "" Then
            Return
        End If

        For i As Integer = 0 To GridView2.RowCount - 1
            If GridView2.GetRowCellValue(i, "Name").ToString.ToUpper = FilterName.ToUpper Then
                MessageBox.Show(Boloctontai, thongbao, MessageBoxButtons.OK)
                Return
            End If
        Next

        'ComboBoxEdit_solopFilter.Enabled = False
        'GridView3.AddNewRow()
        '==========AddrowWithAutoInc sẽ lỗi khi bảng Access gốc bị xóa các record dưới cùng đi. Khi này Bảng Access sẽ tự động thêm số lớn hơn ID của row đã bị xóa khi add thêm row. Trong khi Sub này chỉ lấy được số MaxID +1
        '''''Nếu chỉ Set ID = PrimaryKey, mà KHÔNG phải AutoIncrement thì OK
        ComboBoxEdit_Hanche.DataBindings.Clear()
        Gridview2_Connect.AddRow_with_ID_col(GridControl2, "ID", ListBoxControl1.Tag.ToString)
        ComboBoxEdit_Hanche.DataBindings.Add(New Binding("Text", GridControl2.DataSource, "GhiChu"))
        ''Nếu AddNewRow thì lại phải reFill lại Datasource cho Gridcontrol. Nếu ko sẽ ko update, xóa được các record vừa thêm vào do ko nhận được ID
        ''Dim anewRow As DataRow = GridControl2.DataSource.NewRow
        ''anewRow("Name") = FilterName
        ''GridControl2.DataSource.Rows.Add(anewRow)

        '''''3 dòng dưới do Devexpress khuyên dùng
        ''Dim rowHandle As Integer = GridView1.GetRowHandle(GridView1.DataRowCount)
        ''If (GridView1.IsNewItemRow(rowHandle)) Then
        ''GridView1.SetRowCellValue(rowHandle, "Name", FilterName)
        ''End If
        GridView2.GetDataRow(GridView2.GetSelectedRows(0))("Name") = FilterName
        'GridView2.GetDataRow(GridView2.GetSelectedRows(0))("GhiChu") = ComboBoxEdit_Hanche.
        ''Myconnect.Update_CSDL(GridControl3, "Select * from [" + ListBoxControl1.Tag.tostring + "] where Name = '" + TxtFilterName.Text + "'")
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")

        'GridControl3.DataSource = Gridview2_Connect.DtFromQry("Select * from [" + ListBoxControl1.Tag.tostring + "] where Name <> '" + ListBoxControl1.Tag.tostring + "'")
        'Dim InsertCommand As String = "INSERT INTO [" + ListBoxControl1.Tag.tostring + "] (Name) Values ('" + a + "')"
        'Myconnect.RunaSQLCommand(InsertCommand)
        'GridControl3.DataSource = Myconnect.DtFromQry("Select * from [" + ListBoxControl1.Tag.tostring + "] where Name <> '" + ListBoxControl1.Tag.tostring + "'")
        formatdgv2_3(GridView2)
        'GridControl3.Refresh()
        'GridControl3.RefreshDataSource()
    End Sub

    'Private Sub TxtFilterName_EditValueChanged(sender As Object, e As EventArgs) Handles TxtFilterName.EditValueChanged
    '	GridView3.UpdateCurrentRow()
    'End Sub

    Private Sub BtnSaveFilterReclass_Click(sender As Object, e As EventArgs) Handles BtnSaveFilterReclass.Click
        TxtFilterReclass.Text = saveRasterDialog(True)
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")
        GridView2.UpdateCurrentRow()
    End Sub
    Dim chaycbxSolopIndexchanged As Boolean = True

    Private Sub ComboBoxEditSoLopDGTNST_GotFocus(sender As Object, e As EventArgs) Handles ComboBoxEditSoLopDGTNST.GotFocus
        TxtDGTNST_Reclass.Focus()
    End Sub
    Private Sub ComboBoxEditSoLopDGTNST_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEditSoLopDGTNST.SelectedIndexChanged
        If chaycbxSolopIndexchanged = False Then
            Return
        End If
        CreateFromToTxtbox(ComboBoxEditSoLopDGTNST, XtraScrollableControl3, GridView2.GetRow(0), False, False) 'Không sử dụng FromTo từ bảng để tạo txtbox

        ''========
        Dim a = getListTxtBoxVal(sender, XtraScrollableControl3)
        If a = "" Then  'Trường hợp ko có txtbox nào trong Scroll
            Return
        End If
        'Dim HandleRow As Int16 = GridView2.FocusedRowHandle

        GridView2.SetRowCellValue(0, "FromTo", a)
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")

        GridView2.UpdateCurrentRow()
    End Sub

    Private Sub ComboBoxEditSoLopDGKTXH_GotFocus(sender As Object, e As EventArgs) Handles ComboBoxEditSoLopDGKTXH.GotFocus
        TxtDGKTXH_Reclass.Focus()
    End Sub
    Private Sub ComboBoxEditSoLopDGKTXH_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEditSoLopDGKTXH.SelectedIndexChanged
        If chaycbxSolopIndexchanged = False Then
            Return
        End If
        CreateFromToTxtbox(ComboBoxEditSoLopDGKTXH, XtraScrollableControl3, GridView2.GetRow(1), False, True) 'Không sử dụng FromTo từ bảng để tạo txtbox

        ''========
        Dim a = getListTxtBoxVal(sender, XtraScrollableControl3)
        If a = "" Then  'Trường hợp ko có txtbox nào trong Scroll
            Return
        End If
        'Dim HandleRow As Int16 = GridView2.FocusedRowHandle

        GridView2.SetRowCellValue(1, "FromTo", a)
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")

        GridView2.UpdateCurrentRow()
    End Sub

    Private Sub ComboBoxEditSoLopDGTNMT_GotFocus(sender As Object, e As EventArgs) Handles ComboBoxEditSoLopDGTNMT.GotFocus
        TxtDGTNMT_Reclass.Focus()
    End Sub
    Private Sub ComboBoxEditSoLopDGTNMT_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEditSoLopDGTNMT.SelectedIndexChanged
        If chaycbxSolopIndexchanged = False Then
            Return
        End If
        CreateFromToTxtbox(ComboBoxEditSoLopDGTNMT, XtraScrollableControl3, GridView2.GetRow(2), False, False) 'Không sử dụng FromTo từ bảng để tạo txtbox

        ''========
        Dim a = getListTxtBoxVal(sender, XtraScrollableControl3)
        If a = "" Then  'Trường hợp ko có txtbox nào trong Scroll
            Return
        End If
        'Dim HandleRow As Int16 = GridView2.FocusedRowHandle

        GridView2.SetRowCellValue(2, "FromTo", a)
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")

        GridView2.UpdateCurrentRow()
    End Sub
    Private Sub ComboBoxEdit_solopFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit_solopFilter.SelectedIndexChanged
        If chaycbxSolopIndexchanged = False Then
            Return
        End If
        CreateFromToTxtbox(ComboBoxEdit_solopFilter, XtraScrollableControl4, GridView2.GetRow(GridView2.FocusedRowHandle), False, True)   'Không sử dụng FromTo từ bảng để tạo txtbox

        ''========
        Dim a = getListTxtBoxVal(sender, XtraScrollableControl4)

        Dim HandleRow As Int16 = GridView2.FocusedRowHandle

        GridView2.SetRowCellValue(HandleRow, "FromTo", a)
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")

        GridView2.UpdateCurrentRow()
    End Sub

    Sub CreateFromToTxtbox(cbx As ComboBoxEdit, xtraScroll As XtraScrollableControl, r As DataRowView, DuatextlenTxb As Boolean, revert As Boolean)
        Dim fromto As String = r.Item("fromto").ToString
        Dim minval, maxval As Double
        Dim lstfromtoVal As String()
        Dim solopReclass

        If fromto = "" Then         'Lệnh If để lấy Minval, MaxVal, solopreclass để phục vụ vòng lặp
            solopReclass = cbx.SelectedIndex + 1
            DuatextlenTxb = False   'Nếu FromTo = "" thì ko thể đưa txt từ FromTo trong bảng lên teextbox 
            Try
                Dim SrcRaster As IRaster
                Dim filename = r.Item("SrcTif").ToString
                SplashScreenManager.ShowForm(GetType(FrmWaiting))
                SrcRaster = Raster.Open(filename)
                SrcRaster.GetStatistics()
                minval = SrcRaster.Minimum
                maxval = SrcRaster.Maximum

                RasterDisp(SrcRaster, "")   '"" để close và dispose chứ ko xóa file gốc
                GC.Collect()
                Try
                    SplashScreenManager.CloseForm()
                Catch
                End Try
                If minval = maxval Then
                    MessageBox.Show(Loibando + vbNewLine + filename, thongbao)
                    'return
                End If
            Catch ex As Exception
                SplashScreenManager.CloseForm()
                'Return
            End Try
        Else
            If DuatextlenTxb = False Then
                solopReclass = cbx.SelectedIndex + 1
                fromto = fromto.Substring(0, fromto.Length - 1)    'Loại bỏ dấu | cuối cùng
                lstfromtoVal = fromto.Split("|")
                minval = lstfromtoVal(0)
                maxval = lstfromtoVal(lstfromtoVal.Count - 2)      'lstfromtoVal.Count-1 là NewValue cuối cùng, Maxvalue phải -2

            Else
                fromto = fromto.Substring(0, fromto.Length - 1)    'Loại bỏ dấu | cuối cùng
                lstfromtoVal = fromto.Split("|")
                minval = lstfromtoVal(0)
                maxval = lstfromtoVal(lstfromtoVal.Count - 2)      'lstfromtoVal.Count-1 là NewValue cuối cùng, Maxvalue phải -2

                Dim myCount = lstfromtoVal.Count
                solopReclass = myCount / 3
                chaycbxSolopIndexchanged = False
                cbx.SelectedIndex = solopReclass - 1
                chaycbxSolopIndexchanged = True
            End If

        End If
        xtraScroll.Controls.Clear()
        If minval = maxval Then
            Return
        End If
        For i As Integer = 0 To solopReclass - 1
            Dim label1, label2, label3 As New Label
            Dim txteditFrom, txteditTo, txteditNewVal As New DevExpress.XtraEditors.TextEdit

            'label1 = New Label
            label1.Name = "labelFrom" + i.ToString
            label1.Size = New System.Drawing.Size(31, 13)
            label1.Location = New System.Drawing.Point(20, 15 + 25 * (i))
            label1.Text = Tu

            txteditFrom.Name = "txtFrom" + i.ToString
            txteditFrom.Size = New System.Drawing.Size(52, 20)
            txteditFrom.Location = New System.Drawing.Point(51, 11 + 25 * (i))

            txteditFrom.Properties.Mask.BeepOnError = True
            txteditFrom.Properties.Mask.EditMask = "f5"
            txteditFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
            txteditFrom.Properties.ReadOnly = True
            If DuatextlenTxb = False Then
                If i = 0 Then
                    txteditFrom.Text = minval
                Else
                    ''(maxval - minval) / (ComboBoxEdit1.SelectedIndex + 1) là bước nhảy
                    txteditFrom.Text = minval + ((maxval - minval) / (cbx.SelectedIndex + 1) * (i))  '((maxval - minval) / (i)) * (i - 1)
                End If
            Else
                txteditFrom.Text = lstfromtoVal(i * 3)
            End If



            label2.Name = "labelTo" + i.ToString
            label2.Size = New System.Drawing.Size(27, 13)
            label2.Location = New System.Drawing.Point(115, 15 + 25 * (i))
            label2.Text = Den

            txteditTo.Name = "txtTo" + i.ToString
            txteditTo.Size = New System.Drawing.Size(52, 20)
            txteditTo.Location = New System.Drawing.Point(145, 11 + 25 * (i))
            'txteditTo.Text = (FilterRasterMaxval - FilterRasterMinval) / (sender.SelectedIndex + 1) * i	'((maxval - minval) / (i + 1)) * (i)
            txteditTo.Properties.Mask.BeepOnError = True
            txteditTo.Properties.Mask.EditMask = "f5"
            txteditTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
            'If i = solopReclass Then
            '	txteditTo.Text = maxval
            '	txteditTo.Properties.ReadOnly = True
            'End If
            If DuatextlenTxb = False Then
                If i = solopReclass - 1 Then
                    txteditTo.Text = maxval
                    txteditTo.Properties.ReadOnly = True
                Else
                    txteditTo.Text = minval + ((maxval - minval) / (cbx.SelectedIndex + 1) * (i + 1))    '((maxval - minval) / (i + 1)) * (i)

                End If
            Else
                If i = solopReclass - 1 Then
                    txteditTo.Text = maxval
                    txteditTo.Properties.ReadOnly = True
                Else
                    txteditTo.Text = lstfromtoVal(i * 3 + 1)
                End If
            End If


            If cbx.Name = ComboBoxEditSoLopDGTNST.Name Then
                AddHandler txteditTo.KeyUp, AddressOf txteditTo_KeyUp
                AddHandler txteditTo.InvalidValue, AddressOf txteditTo_InvalidValue
                AddHandler txteditTo.Validating, AddressOf txteditTo_Validating
            Else
                AddHandler txteditTo.KeyUp, AddressOf txteditToFilter_KeyUp
                AddHandler txteditTo.InvalidValue, AddressOf txteditToFilter_InvalidValue
                AddHandler txteditTo.Validating, AddressOf txteditToFilter_Validating
            End If

            '=============NewValue
            label3.Name = "labelNewValue" + i.ToString
            label3.Size = New System.Drawing.Size(60, 13)
            label3.Location = New System.Drawing.Point(210, 15 + 25 * (i))
            label3.Text = Giatrimoi

            txteditNewVal.Name = "txtNewValue" + i.ToString
            txteditNewVal.Size = New System.Drawing.Size(28, 20)
            txteditNewVal.Location = New System.Drawing.Point(275, 11 + 25 * (i))
            If revert = False Then
                txteditNewVal.Text = (i).ToString
            Else
                txteditNewVal.Text = (i).ToString      '***************Không làm revert nữa 03.01.2019. Trong phiên bản cũ thì làm revert. Tức là trong giao diện, khi click vào 
                'các textbox KTXH hoặc MT thì giá trị newval sẽ sắp xếp ngược lại với TNST: tức là từ to xuống bé
                'txteditNewVal.Text = (solopReclass - 1 - i).ToString
            End If

            'If XtraTabControl1.SelectedTabPage Is XtraTabPage6 Then     'Trường hợp Tab6 thì cho chạy từ 0 
            'txteditNewVal.Text = (i).ToString
            'End If
            'txteditNewVal.Properties.Mask.BeepOnError = True
            'txteditNewVal.Properties.Mask.EditMask = "n0"
            'txteditNewVal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
            'txteditNewVal.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True
            If ComboBoxEdit_Hanche.SelectedItem <> "Hạn chế" And ComboBoxEdit_Hanche.SelectedItem <> "Constraint" Then
                txteditNewVal.Properties.ReadOnly = True
            End If



            xtraScroll.Controls.Add(label1)
            xtraScroll.Controls.Add(label2)
            xtraScroll.Controls.Add(label3)
            xtraScroll.Controls.Add(txteditFrom)
            xtraScroll.Controls.Add(txteditTo)
            xtraScroll.Controls.Add(txteditNewVal)

            'listTxteditPhanlopFilter.Add(txteditFrom)
            'listTxteditPhanlopFilter.Add(txteditTo)
            'listTxteditPhanlopFilter.Add(txteditNewVal)
            'Me.Size = New System.Drawing.Size(Me.Width, 155 + 25 * (i - 1))
            'GroupControl4.Height = 311 + 25 * (i - 1)

            'ProgressBarControl1.Location = New System.Drawing.Point(811, ProgressBarControl1.Location.Y)

            ProgressBarControl5.Visible = False
        Next
    End Sub
    Private Sub txteditToFilter_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim myname As String = sender.name
        Dim stt As Integer = myname.Replace("txtTo", "")
        Try
            Dim nexttxtbox As DevExpress.XtraEditors.TextEdit = FrmMain.FindControl(XtraScrollableControl4, "txtFrom" + (stt + 1).ToString)
            nexttxtbox.Text = sender.text
        Catch ex As Exception

        End Try
        Dim a = getListTxtBoxVal(ComboBoxEdit_solopFilter, XtraScrollableControl4)

        Dim HandleRow As Int16 = GridView2.FocusedRowHandle

        GridView2.SetRowCellValue(HandleRow, "FromTo", a)
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")
    End Sub

    Private Sub txteditToFilter_InvalidValue(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.InvalidValueExceptionEventArgs)
        e.ErrorText = Bancannha
    End Sub
    Private Sub txteditToFilter_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim myname As String = sender.name
        Dim stt As Integer = myname.Replace("txtTo", "")
        Try
            Dim nexttxtbox1 As DevExpress.XtraEditors.TextEdit = FrmMain.FindControl(XtraScrollableControl4, "txtTo" + (stt + 1).ToString)
            If Not nexttxtbox1 Is Nothing Then
                If sender.Text <> "" And nexttxtbox1.Text <> "" Then
                    If Convert.ToSingle(sender.Text.Replace(",", "")) >= Convert.ToSingle(nexttxtbox1.Text.Replace(",", "")) Then   ' or Convert.ToDouble(sender.Text.Replace(",", "")) >= FilterRasterMaxval Or Convert.ToDouble(sender.Text.Replace(",", "")) <= FilterRasterMinval Then
                        e.Cancel = True
                    End If
                    'ElseIf sender.Text = "" Or nexttxtbox1.Text = "" Then
                    '	If Convert.ToDouble(sender.Text.Replace(",", "")) >= FilterRasterMaxval Or Convert.ToDouble(sender.Text.Replace(",", "")) <= FilterRasterMinval Then
                    '		e.Cancel = True
                    '	End If

                End If
            Else

            End If
            Dim previoustxtbox1 As DevExpress.XtraEditors.TextEdit = FrmMain.FindControl(XtraScrollableControl4, "txtTo" + (stt - 1).ToString)
            If Not previoustxtbox1 Is Nothing Then
                If sender.Text <> "" And previoustxtbox1.Text <> "" Then
                    If Convert.ToDouble(sender.Text.Replace(",", "")) <= Convert.ToSingle(previoustxtbox1.Text.Replace(",", "")) Then
                        e.Cancel = True
                    End If
                End If
                'ElseIf sender.Text = "" Or nexttxtbox1.Text = "" Then
                '	If Convert.ToDouble(sender.Text.Replace(",", "")) >= FilterRasterMaxval Or Convert.ToDouble(sender.Text.Replace(",", "")) <= FilterRasterMinval Then
                '		e.Cancel = True
                '	End If
            End If

        Catch ex As Exception

        End Try

    End Sub
    Private Sub BtnFilterReclass_Click(sender As Object, e As EventArgs) Handles BtnRunFilterReclass.Click

        If TxtRawdata.Text = "" Then
            MessageBox.Show(Haynhapduong)
            Return
        ElseIf TxtFilterReclass.Text = "" Then
            MessageBox.Show(Haydatten)
            Return
        End If
        If FindControl(XtraScrollableControl4, "txtFrom0") Is Nothing Then
            MessageBox.Show(NhapgiatriPhankhoang)
            Return
        End If
        For i As Integer = 0 To ComboBoxEdit_solopFilter.SelectedIndex

            If FindControl(XtraScrollableControl4, "txtFrom" + (i).ToString).Text = "" Or FindControl(XtraScrollableControl4, "txtTo" + (i).ToString).Text = "" Or FindControl(XtraScrollableControl4, "txtNewValue" + (i).ToString).Text = "" Then
                MessageBox.Show(NhapgiatriPhankhoang)
                Return
            End If
        Next



        ProgressBarControl5.EditValue = 0
        ProgressBarControl5.Update()
        Dim Raw_Raster = Raster.Open(TxtRawdata.Text)
        '=======
        'Me.Size = New System.Drawing.Size(Me.Width, 175 + 25 * (ComboBoxEdit1.SelectedIndex))
        'ProgressBarControl1.Location = New System.Drawing.Point(111, 115 + 25 * (ComboBoxEdit1.SelectedIndex))
        ProgressBarControl5.Visible = True
        ProgressBarControl5.Properties.Step = 1
        ProgressBarControl5.Properties.PercentView = True
        ProgressBarControl5.Properties.Maximum = (Raw_Raster.NumRows - 1) * (ComboBoxEdit_solopFilter.SelectedIndex + 1)    'Lấy  count của toàn bộ các ROWs
        ProgressBarControl5.Properties.Minimum = 0
        '===========
        Dim rasteroptions As String()
        Dim datatype As System.Type = System.Type.GetType("System.Int16")
        Dim reclassedRaster As IRaster = Raster.CreateRaster(TxtFilterReclass.Text, Nothing, Raw_Raster.NumColumns, Raw_Raster.NumRows, 1, datatype, rasteroptions)

        With reclassedRaster
            .Filename = TxtFilterReclass.Text
            .Bounds = Raw_Raster.Bounds
            .NoDataValue = -9999
            .Projection = Raw_Raster.Projection

            ''.NumRowsInFile = IndRaster.NumRows
            ''.NumColumns = IndRaster.NumColumns

        End With
        Try

            For i As Integer = 0 To ComboBoxEdit_solopFilter.SelectedIndex

                Dim txtFrom = Convert.ToDouble(FindControl(XtraScrollableControl4, "txtFrom" + (i).ToString).Text)       'PanelControl1.Controls.Find("txtFrom" + i.ToString, False)(0) 'FrmStart.FindControl(PanelControl1, "txtFrom" + i.ToString)
                Dim txtTo = Convert.ToDouble(FindControl(XtraScrollableControl4, "txtTo" + (i).ToString).Text)       'PanelControl1.Controls.Find("txtTo" + i.ToString, False)(0) 'FrmStart.FindControl(PanelControl1, "txtTo" + i.ToString)
                Dim txtNewValue = Convert.ToInt16(FindControl(XtraScrollableControl4, "txtNewValue" + (i).ToString).Text)        'PanelControl1.Controls.Find("txtNewValue" + i.ToString, False)(0)   'FrmStart.FindControl(PanelControl1, "txtNewValue" + i.ToString)
                If i = ComboBoxEdit_solopFilter.SelectedIndex Then
                    txtTo = txtTo + 1 'Để trường hợp <txtTo sẽ trở thành txtTo + 1 Nếu ko giá trị max sẽ bị loại khỏi kết quả
                End If
                For k As Integer = 0 To Raw_Raster.NumRows - 1
                    For j As Integer = 0 To Raw_Raster.NumColumns - 1

                        If Raw_Raster.Value(k, j) <> Raw_Raster.NoDataValue And Raw_Raster.Value(k, j) > -9999 Then
                            If Raw_Raster.Value(k, j) >= txtFrom AndAlso Raw_Raster.Value(k, j) < txtTo Then
                                reclassedRaster.Value(k, j) = txtNewValue
                                'Exit For
                            End If
                        Else
                            reclassedRaster.Value(k, j) = -9999
                        End If

                    Next
                    ProgressBarControl5.PerformStep()
                    ProgressBarControl5.Update()
                Next
            Next
            reclassedRaster.Save() 'As(Txtoutput.Text)
            reclassedRaster.GetStatistics()

            ProgressBarControl5.Visible = False

            RasterDisp(Raw_Raster, "")
            RasterDisp(reclassedRaster, "")
            Dim ok As DialogResult = MessageBox.Show(Mo, thongbao, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If ok = Windows.Forms.DialogResult.Yes Then
                Viewer(TxtFilterReclass.Text)
            End If
            'MessageBox.Show("Tính toán thành công hàm chuẩn hóa")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            'MessageBox.Show("Xem lại các giá trị nhập cho a, b, c, d" + vbNewLine + "Xem lại các giá trị vào, ra của chỉ tiêu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnRenameBoloc_Click(sender As Object, e As EventArgs) Handles BtnRenameBoloc.Click
        If GridView2.FocusedRowHandle < 0 Then
            BtnAddBoloc_Click(BtnAddBoloc, Nothing)
            Return
        End If
        Dim srcObj = TxtFilterName.Text
        Dim TargetObj = InputBox("")
        If TargetObj = "" Then
            Return
        End If

        For i As Integer = 0 To GridView2.RowCount - 1
            If GridView2.GetRowCellValue(i, "Name").ToString.ToUpper = TargetObj.ToUpper Then
                MessageBox.Show(Boloctontai, thongbao, MessageBoxButtons.OK)
                Return
            End If
        Next
        TxtFilterName.Text = TargetObj
        'Dim InsertCommand As String = "INSERT INTO [" + ListBoxControl1.Tag.tostring + "] (Name) Values ('" + a + "')"
        'Myconnect.RunaSQLCommand(InsertCommand)
        'GridControl3.DataSource = Myconnect.DtFromQry("Select * from [" + ListBoxControl1.Tag.tostring + "] where Name <> '" + ListBoxControl1.Tag.tostring + "'")
        'TxtFilterName.Text = a	   'Sử dụng để update đúng đối tượng
        GridView2.UpdateCurrentRow()
        ''Myconnect.Update_CSDL(GridControl3, "Select * from [" + ListBoxControl1.Tag.tostring + "] where Name = '" + TxtFilterName.Text + "'")
        ''Myconnect.Update_CSDL(GridControl3, "Select * from [" + ListBoxControl1.Tag.tostring + "] where ID = '" + GridView3.GetRowCellValue(GridView3.FocusedRowHandle, "ID") + "'")
        'Dim UPDATECommand As String = "UPDATE [" + ListBoxControl1.Tag.tostring + "] SET Name = '" + TargetObj + "'WHERE Name = '" + srcObj + "'"
        'Dim theresult = Gridview2_Connect.RunaSQLCommand(GridControl3.DataSource, UPDATECommand)
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")
    End Sub
    Private Sub BtnXoaBoloc_Click(sender As Object, e As EventArgs) Handles BtnXoaBoloc.Click
        'Gridview1_Reload()  'Phải reload trước khi save, nếu ko sẽ gặp lỗi

        Dim yesno = MessageBox.Show(Chacchanxoa, thongbao, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If yesno = Windows.Forms.DialogResult.Yes Then
            GridView2.DeleteSelectedRows()
            '=====================2 lệnh sau rất quan trọng, nếu ko sẽ ko update focused row
            'GridView1.PostEditor()
            'GridView1.UpdateCurrentRow()
            '=====================
            Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")
            'Gridview1_Reload()
            'MessageBox.Show("Đã xóa dữ liệu thành công", thongbao)
            Status1.Caption = Xoathanhcong
            Dim a = sender.focus

        End If
    End Sub

    Private Sub GridView3_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs)
        '=====================
        '=====================Tránh lỗi khi load dữ liệu lần đầu
        If e.PrevFocusedRowHandle < 0 Then
            Return
        End If

        If GridView2.GetDataRow(e.PrevFocusedRowHandle) Is Nothing Then
            Return
        End If

        CreateFromToTxtbox(ComboBoxEdit_solopFilter, XtraScrollableControl4, GridView2.GetRow(GridView2.FocusedRowHandle), True, False)

        '=============================
        '================Nếu GridView3.GetRowCellValue(GridView3.FocusedRowHandle, "FromTo") là dbnull thì thoát
        'Dim FromTo = GridView3.GetRowCellValue(GridView3.FocusedRowHandle, "FromTo")
        'If FromTo Is System.DBNull.Value Then
        '	'================MỞ LẠI FILE RAWfILTER
        '	Try
        '		Dim FilterRaster As IRaster
        '		Dim filename = TxtRawdata.Text
        '		SplashScreenManager.ShowForm(GetType(FrmWaiting))
        '		FilterRaster = Raster.Open(filename)
        '		ComboBoxEdit_solopFilter.Enabled = True
        '		FilterRaster.GetStatistics()
        '		FilterRasterMinval = FilterRaster.Minimum
        '		FilterRasterMaxval = FilterRaster.Maximum

        '		If FilterRasterMaxval = FilterRasterMinval Then
        '			ComboBoxEdit_solopFilter.SelectedIndex = 0
        '		Else
        '			ComboBoxEdit_solopFilter.SelectedIndex = 0
        '			ComboBoxEdit_solopFilter.SelectedIndex = 3
        '		End If
        '		'ComboBoxEdit_solopFilter_SelectedIndexChanged(Nothing, Nothing)


        '		'BtnRunFilterReclass.Enabled = True
        '		RasterDisp(FilterRaster, "")   '"" để close và dispose chứ ko xóa file gốc
        '		GC.Collect()
        '		Try
        '			SplashScreenManager.CloseForm()
        '		Catch

        '		End Try

        '	Catch ex As Exception
        '		'MessageBox.Show(ex.ToString)
        '		Try
        '			SplashScreenManager.CloseForm()
        '		Catch

        '		End Try

        '		ComboBoxEdit_solopFilter.Enabled = False

        '		'BtnRunFilterReclass.Enabled = False
        '	End Try
        '	Return

        'End If

        'FromTo = FromTo.Substring(0, FromTo.Length - 1)	   'Loại bỏ dấu | cuối cùng
        'Dim listtxtboxval As String() = FromTo.Split("|")
        'Dim myCount = listtxtboxval.Count
        'Dim solopReclass = myCount / 3

        ''ComboBoxEdit_solopFilter.SelectedIndex() = 0
        'ComboBoxEdit_solopFilter.SelectedIndex() = solopReclass - 1


        'GridView3.SetRowCellValue(GridView3.FocusedRowHandle, "FromTo", FromTo + "|")
        ''For i As Integer = 0 To myCount - 1
        ''	MessageBox.Show(listtxtboxval(i))
        ''Next

        'For i As Integer = 1 To listtxtboxval.Count / 3

        '	Dim label1, label2, label3 As New Label
        '	Dim txteditFrom, txteditTo, txteditNewVal As New DevExpress.XtraEditors.TextEdit

        '	'label1 = New Label
        '	label1.Name = "labelFrom" + i.ToString
        '	label1.Size = New System.Drawing.Size(20, 13)
        '	label1.Location = New System.Drawing.Point(0, 15 + 25 * (i - 1))
        '	label1.Text = Tu

        '	txteditFrom.Name = "txtFrom" + i.ToString
        '	txteditFrom.Size = New System.Drawing.Size(52, 20)
        '	txteditFrom.Location = New System.Drawing.Point(30, 11 + 25 * (i - 1))

        '	txteditFrom.Properties.Mask.BeepOnError = True
        '	txteditFrom.Properties.Mask.EditMask = "f5"
        '	txteditFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        '	txteditFrom.Properties.ReadOnly = True

        '	'If i = 1 Then
        '	'	txteditFrom.Text = FilterRasterMinval
        '	'Else
        '	''(maxval - minval) / (ComboBoxEdit1.SelectedIndex + 1) là bước nhảy
        '	'txteditFrom.Text = (FilterRasterMaxval - FilterRasterMinval) / (sender.SelectedIndex + 1) * (i - 1)	   '((maxval - minval) / (i)) * (i - 1)
        '	txteditFrom.Text = listtxtboxval((i - 1) * 3)

        '	'End If

        '	label2.Name = "labelTo" + i.ToString
        '	label2.Size = New System.Drawing.Size(27, 13)
        '	label2.Location = New System.Drawing.Point(98, 15 + 25 * (i - 1))
        '	label2.Text = Den

        '	txteditTo.Name = "txtTo" + i.ToString
        '	txteditTo.Size = New System.Drawing.Size(52, 20)
        '	txteditTo.Location = New System.Drawing.Point(125, 11 + 25 * (i - 1))
        '	'txteditTo.Text = (FilterRasterMaxval - FilterRasterMinval) / (sender.SelectedIndex + 1) * i	'((maxval - minval) / (i + 1)) * (i)
        '	txteditTo.Text = listtxtboxval((i - 1) * 3 + 1)
        '	txteditTo.Properties.Mask.BeepOnError = True
        '	txteditTo.Properties.Mask.EditMask = "f5"
        '	txteditTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        '	If i = solopReclass Then
        '		'txteditTo.Text = FilterRasterMaxval
        '		txteditTo.Properties.ReadOnly = True
        '	End If
        '	AddHandler txteditTo.KeyUp, AddressOf txteditToFilter_KeyUp
        '	AddHandler txteditTo.InvalidValue, AddressOf txteditToFilter_InvalidValue
        '	AddHandler txteditTo.Validating, AddressOf txteditToFilter_Validating

        '	label3.Name = "labelNewValue" + i.ToString
        '	label3.Size = New System.Drawing.Size(54, 13)
        '	label3.Location = New System.Drawing.Point(188, 15 + 25 * (i - 1))
        '	label3.Text = Giatrimoi

        '	txteditNewVal.Name = "txtNewValue" + i.ToString
        '	txteditNewVal.Size = New System.Drawing.Size(28, 20)
        '	txteditNewVal.Location = New System.Drawing.Point(245, 11 + 25 * (i - 1))
        '	txteditNewVal.Text = i.ToString
        '	'txteditNewVal.Properties.Mask.BeepOnError = True
        '	'txteditNewVal.Properties.Mask.EditMask = "n0"
        '	'txteditNewVal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
        '	'txteditNewVal.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True
        '	txteditNewVal.Properties.ReadOnly = True


        '	XtraScrollableControl4.Controls.Add(label1)
        '	XtraScrollableControl4.Controls.Add(label2)
        '	XtraScrollableControl4.Controls.Add(label3)
        '	XtraScrollableControl4.Controls.Add(txteditFrom)
        '	XtraScrollableControl4.Controls.Add(txteditTo)
        '	XtraScrollableControl4.Controls.Add(txteditNewVal)

        '	listTxteditPhanlopFilter.Add(txteditFrom)
        '	listTxteditPhanlopFilter.Add(txteditTo)
        '	listTxteditPhanlopFilter.Add(txteditNewVal)
        '	'Me.Size = New System.Drawing.Size(Me.Width, 155 + 25 * (i - 1))
        '	'GroupControl4.Height = 311 + 25 * (i - 1)

        '	'ProgressBarControl1.Location = New System.Drawing.Point(811, ProgressBarControl1.Location.Y)

        '	ProgressBarControl3.Visible = False
        'Next
    End Sub

#End Region

    Private Sub BtnBrowseDGxxx_Click(sender As Object, e As EventArgs) Handles BtnBrowseDGxxx.Click

        TxtDGxxx.Text = saveRasterDialog(True)

    End Sub

    Private Sub BtnRunDGxxx_Click(sender As Object, e As EventArgs) Handles BtnRunDGxxx.Click
        'Dim thePath As String
        'Dim theFN As String
        Dim tmppath As String = Path.GetTempPath()

        Try
            If (Not System.IO.Directory.Exists(tmppath + "nft")) Then
                System.IO.Directory.CreateDirectory(tmppath + "nft")
            End If
            tmppath = tmppath + "nft\"
        Catch ex As Exception
            MessageBox.Show(Khongquyensudung + tmppath, LoiQuyentruycap, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try
        xoatmp(tmppath)

        Dim resultname As String = TxtDGxxx.Text
        If String.IsNullOrEmpty(resultname) = True Then
            MessageBox.Show("Chỉ định file đầu ra")
            Return

        End If

        Dim TNSTRaster As IRaster
        Dim KTXHRaster As IRaster
        Dim TNMTRaster As IRaster

        '=============
        ProgressBarControl7.Visible = True
        ProgressBarControl8.Visible = True
        ProgressBarControl7.EditValue = 0.1

        ProgressBarControl7.Properties.Step = 1
        ProgressBarControl7.Properties.PercentView = True

        ProgressBarControl7.Properties.Maximum = 3
        ProgressBarControl7.Properties.Minimum = 0
        ProgressBarControl7.Update()

        Try
            'SrcRaster = Raster.Open(TxtDGTNST_Reclass.Text) 'Do sử dụng chung Gridview2 nên khi chuyển sang tab6 hoặc tab7 thì TxtDGTNST_Reclass.Text đã thay đổi vì Binding, thay đổi focus trên Gridview
            Dim TNSTRasterFN = GridView2.GetRowCellValue(0, "RecTif")
            If Not File.Exists(TNSTRasterFN) Then
                MessageBox.Show(dulieuTNSTphanlop, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            TNSTRaster = Raster.Open(TNSTRasterFN)

        Catch ex As Exception
            MessageBox.Show(dulieuTNSTphanlop)
            XtraTabControl1.SelectedTabPage = XtraTabPage_Step4
            Return
        End Try
        '=============
        Dim listRaster As List(Of IRaster) = New List(Of IRaster)
        Dim MyMagic As New myRasterMagic
        Dim KQDGTN1 As IRaster
        Dim thepath As String
        Dim theFN As String
        Dim tmpKTXHRevertRaster As IRaster
        Dim tmpTNMTRevertRaster As IRaster

        '================'================'================'================'================
        '================'================'================'================'================
        '================'================TH 1. EnCs checked component     '================
        '================'================'================'================'================
        '================'================'================'================'================
        If BarCheckItemComposite.Checked = True Then
            Try
                Dim KTXHRasterFN = GridView2.GetRowCellValue(1, "RecTif")
                Dim TNMTRasterFN = GridView2.GetRowCellValue(2, "RecTif")
                If Not File.Exists(KTXHRasterFN) Then
                    MessageBox.Show(dulieuKTXHphanlop, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'XtraTabControl1.SelectedTabPage = XtraTabPage5
                    Return
                End If
                If Not File.Exists(TNMTRasterFN) Then
                    MessageBox.Show(dulieuTNMTphanlop, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    'XtraTabControl1.SelectedTabPage = XtraTabPage5
                    Return
                End If
                KTXHRaster = Raster.Open(KTXHRasterFN)
                TNMTRaster = Raster.Open(TNMTRasterFN)
                tmpKTXHRevertRaster = MyMagic.RevertRaster(KTXHRaster, tmppath + "TmpKTXHRevert.Tif")
                tmpTNMTRevertRaster = MyMagic.RevertRaster(TNMTRaster, tmppath + "TmpTNMTRevert.Tif")
            Catch ex As Exception
                MessageBox.Show(dulieuKTXHphanlop + " .+. " + dulieuTNMTphanlop, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
                'XtraTabControl1.SelectedTabPage = XtraTabPage5
                'Return
            End Try

            '============================
            '=======================1.1. ST - KTXH - MT         GIẢM CẤP
            '============================
            If RadioGroup1.SelectedIndex = 0 Then
                '=======================1.1. ST - KTXH - MT

                'If KTXHRaster.Filename <> "" And TNMTRaster.Filename <> "" Then
                'Dim tmphieu As IRaster
                'tmphieu = MyMagic.RasterMath("Trừ", TNSTRaster, KTXHRaster, tmppath + "tmphieu.Tif", ProgressBarControl8, Nothing)
                'KQDGTN1 = MyMagic.RasterMath("Trừ", tmphieu, TNMTRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                'RasterDisp(TNSTRaster, tmppath + "tmphieu.Tif")
                'ElseIf KTXHRaster.Filename <> "" And TNMTRaster.Filename = "" Then
                'KQDGTN1 = MyMagic.RasterMath("Trừ", TNSTRaster, KTXHRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                'ElseIf KTXHRaster.Filename = "" And TNMTRaster.Filename <> "" Then
                'KQDGTN1 = MyMagic.RasterMath("Trừ", TNSTRaster, TNMTRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                'Else
                'KQDGTN1 = TNSTRaster
                'End If

                '***Phiên bản 03.01.2019 Revert giá trị KTXH và MT. Trong phiên bản cũ phải làm ngược giá trị reclass ở bước 4
                '**********HIỆN TẠI THÌ CẢ 3 HỢP PHẦN ST-KTXH-MT ĐỀU CÓ HƯỚNG LÀ GIÁ TRỊ CÀNG LỚN CÀNG TỐI ƯU (TỐT, TÍCH CỰC)

                If KTXHRaster.Filename <> "" And TNMTRaster.Filename <> "" Then
                    Dim tmphieu As IRaster

                    tmphieu = MyMagic.RasterMath("Trừ", TNSTRaster, tmpKTXHRevertRaster, tmppath + "tmphieu.Tif", ProgressBarControl8, Nothing)
                    KQDGTN1 = MyMagic.RasterMath("Trừ", tmphieu, tmpTNMTRevertRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                    RasterDisp(TNSTRaster, tmppath + "tmphieu.Tif")

                ElseIf KTXHRaster.Filename <> "" And TNMTRaster.Filename = "" Then
                    KQDGTN1 = MyMagic.RasterMath("Trừ", TNSTRaster, tmpKTXHRevertRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                ElseIf KTXHRaster.Filename = "" And TNMTRaster.Filename <> "" Then
                    KQDGTN1 = MyMagic.RasterMath("Trừ", TNSTRaster, tmpTNMTRevertRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                Else
                    KQDGTN1 = TNSTRaster
                End If
                '============================
                '=======================1.2. MIN 3 HỢP PHẦN:  TIẾN HÀNH TÍNH MIN CỦA 3 hợp phần TNST, KTXH VÀ TNMT          LIBIG
                '============================
            ElseIf RadioGroup1.SelectedIndex = 1 Then
                ProgressBarControl8.EditValue = 0.1
                ''''If KTXHRaster.Filename <> "" And TNMTRaster.Filename <> "" Then
                '''''KQDGTN1 = Min3RASTER(TNSTRaster, KTXHRaster, TNMTRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8)
                ''''Dim TmpMin As IRaster
                ''''TmpMin = MyMagic.RasterMath("MIN_DaoChieuRaster2", TNSTRaster, KTXHRaster, tmppath + "TmpMin.Tif", ProgressBarControl8, Nothing)
                ''''KQDGTN1 = MyMagic.RasterMath("MIN_DaoChieuRaster2", TmpMin, TNMTRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                ''''RasterDisp(TmpMin, tmppath + "TmpMin.Tif")
                ''''ElseIf KTXHRaster.Filename <> "" And TNMTRaster.Filename = "" Then
                '''''KQDGTN1 = MinRASTER(TNSTRaster, KTXHRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8)
                ''''KQDGTN1 = MyMagic.RasterMath("MIN_DaoChieuRaster2", TNSTRaster, KTXHRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                ''''ElseIf KTXHRaster.Filename = "" And TNMTRaster.Filename <> "" Then
                '''''KQDGTN1 = MinRASTER(TNSTRaster, TNMTRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8)
                ''''KQDGTN1 = MyMagic.RasterMath("MIN_DaoChieuRaster2", TNSTRaster, TNMTRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                ''''Else
                ''''KQDGTN1 = TNSTRaster
                ''''End If
                '***Phiên bản 03.01.2019 Không cần dùng Min đảo chiều mà dùng min chuẩn luôn giá trị KTXH và MT. Trong phiên bản cũ phải làm ngược giá trị reclass ở bước 4 do đó phải dùng Min_Đảo chiều
                If KTXHRaster.Filename <> "" And TNMTRaster.Filename <> "" Then
                    'KQDGTN1 = Min3RASTER(TNSTRaster, KTXHRaster, TNMTRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8)
                    Dim TmpMin As IRaster
                    TmpMin = MyMagic.RasterMath("MIN", TNSTRaster, KTXHRaster, tmppath + "TmpMin.Tif", ProgressBarControl8, Nothing)
                    KQDGTN1 = MyMagic.RasterMath("MIN", TmpMin, TNMTRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                    RasterDisp(TmpMin, tmppath + "TmpMin.Tif")
                ElseIf KTXHRaster.Filename <> "" And TNMTRaster.Filename = "" Then
                    'KQDGTN1 = MinRASTER(TNSTRaster, KTXHRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8)
                    KQDGTN1 = MyMagic.RasterMath("MIN", TNSTRaster, KTXHRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                ElseIf KTXHRaster.Filename = "" And TNMTRaster.Filename <> "" Then
                    'KQDGTN1 = MinRASTER(TNSTRaster, TNMTRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8)
                    KQDGTN1 = MyMagic.RasterMath("MIN", TNSTRaster, TNMTRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                Else
                    KQDGTN1 = TNSTRaster
                End If
                '============================
                '=======================1.3.  MIN (ST, KTXH) - TNMT
                '============================
            ElseIf RadioGroup1.SelectedIndex = 2 Then

                '''''If KTXHRaster.Filename <> "" And TNMTRaster.Filename <> "" Then
                '''''Dim TmpMin As IRaster
                ''''''TmpMin = MinRASTER(TNSTRaster, KTXHRaster, tmppath + "TmpMin.Tif", ProgressBarControl8)
                '''''ProgressBarControl8.EditValue = 0.1
                '''''TmpMin = MyMagic.RasterMath("MIN_DaoChieuRaster2", TNSTRaster, KTXHRaster, tmppath + "TmpMin.Tif", ProgressBarControl8, Nothing)
                '''''KQDGTN1 = MyMagic.RasterMath("Trừ", TmpMin, TNMTRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                '''''RasterDisp(TNSTRaster, tmppath + "TmpMin.Tif")
                '''''ElseIf KTXHRaster.Filename <> "" And TNMTRaster.Filename = "" Then
                ''''''KQDGTN1 = MinRASTER(TNSTRaster, KTXHRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8)
                '''''KQDGTN1 = MyMagic.RasterMath("MIN_DaoChieuRaster2", TNSTRaster, KTXHRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                '''''ElseIf KTXHRaster.Filename = "" And TNMTRaster.Filename <> "" Then
                '''''KQDGTN1 = MyMagic.RasterMath("Trừ", TNSTRaster, TNMTRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                '''''Else
                '''''KQDGTN1 = TNSTRaster
                '''''End If
                '***Phiên bản 03.01.2019 Không cần dùng Min đảo chiều mà dùng min chuẩn luôn giá trị KTXH và MT. Trong phiên bản cũ phải làm ngược giá trị reclass ở bước 4 do đó phải dùng Min_Đảo chiều
                If KTXHRaster.Filename <> "" And TNMTRaster.Filename <> "" Then
                    Dim TmpMin As IRaster
                    'TmpMin = MinRASTER(TNSTRaster, KTXHRaster, tmppath + "TmpMin.Tif", ProgressBarControl8)
                    ProgressBarControl8.EditValue = 0.1
                    TmpMin = MyMagic.RasterMath("MIN", TNSTRaster, KTXHRaster, tmppath + "TmpMin.Tif", ProgressBarControl8, Nothing)    'Đổi MIN_DaoChieuRaster2 thành Min. Giữ nguyên chiều của KTXHRaster
                    KQDGTN1 = MyMagic.RasterMath("Trừ", TmpMin, tmpTNMTRevertRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                    RasterDisp(TNSTRaster, tmppath + "TmpMin.Tif")
                ElseIf KTXHRaster.Filename <> "" And TNMTRaster.Filename = "" Then
                    'KQDGTN1 = MinRASTER(TNSTRaster, KTXHRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8)
                    KQDGTN1 = MyMagic.RasterMath("MIN", TNSTRaster, KTXHRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                ElseIf KTXHRaster.Filename = "" And TNMTRaster.Filename <> "" Then
                    KQDGTN1 = MyMagic.RasterMath("Trừ", TNSTRaster, tmpTNMTRevertRaster, tmppath + "KQDGTN1.Tif", ProgressBarControl8, Nothing)
                Else
                    KQDGTN1 = TNSTRaster
                End If


            End If

            RasterDisp(tmpKTXHRevertRaster, tmppath + "TmpKTXHRevert.Tif")
            RasterDisp(tmpTNMTRevertRaster, tmppath + "TmpTNMTRevert.Tif")
            listRaster.Add(KQDGTN1)
            '=============Hết 3 trường hợp trong mục COMPOSITE 3 hợp phần checked
        Else '================'================'================'================'================KHÔNG CHECK ENCs
            '================'================'================'================'================
            '================'================TH 2. Chỉ Composite TNST          '================
            '================'================'================'================'================
            '================'================'================'================'================
            GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '2. Kinh tế - Xã hội' or [IndGroup] = '2. Socio - Economic' or [IndGroup] = '3. Môi trường' or [IndGroup] = '3. Environment'")
            listRaster.Add(TNSTRaster)
            Dim SrcRaster As IRaster = New raster

            For i As Integer = 0 To GridView1.RowCount - 1
                thepath = GridView1.GetRowCellValue(i, "Mapchuanhoa")       'Filename Included
                theFN = Path.GetFileName(thepath)       'extension Included
                If Not File.Exists(thepath) Then
                    MessageBox.Show(Kiemtrachuanhoa, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    XtraTabControl1.SelectedTabPage = XtraTabPage_Step4
                    Return
                End If
            Next
           
            Dim tmpRevertRaster As IRaster      '**** 03.01.2019

            '=======================2.1. ST - KTXH - MT
            If RadioGroup1.SelectedIndex = 0 Then '=====
                '=======================2.1. ST - KTXH - MT
                For i As Integer = 0 To GridView1.RowCount - 1
                    thepath = GridView1.GetRowCellValue(i, "Mapchuanhoa")       'Filename Included
                    theFN = Path.GetFileName(thepath)       'extension Included

                    Try
                        SrcRaster = Raster.Open(thepath)
                        tmpRevertRaster = MyMagic.RevertRaster(SrcRaster, theFN)    '**** 03.01.2019
                    Catch ex As Exception
                        MessageBox.Show(Kiemtrachuanhoa, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraTabControl1.SelectedTabPage = XtraTabPage_Step4
                        Return
                    End Try
                    '03.01.2019     Sửa từ SrcRaster thành MyMagic.RevertRaster(SrcRaster) ở dòng dưới đây
                    listRaster.Add(MyMagic.RasterMath("Trừ", listRaster(listRaster.Count - 1), tmpRevertRaster, tmppath + i.ToString + "TruBaThanhPhan.Tif", ProgressBarControl8, Nothing))
                    If i > 0 Then   'Trường hợp i=0 thì listRaster(listRaster.Count - 2) sẽ là TNST
                        'RasterDisp(listRaster(listRaster.Count - 2), listRaster(listRaster.Count - 2).filenane)
                        'RasterDisp(SrcRaster, "")
                        RasterDisp(tmpRevertRaster, theFN)
                    End If
                    'listRaster(listRaster.Count - 1).Save()
                Next

                '
                '============================
            ElseIf RadioGroup1.SelectedIndex = 1 Then '====================2.2. MIN(ST, KTXH, MT)
                For i As Integer = 0 To GridView1.RowCount - 1
                    thepath = GridView1.GetRowCellValue(i, "Mapchuanhoa")       'Filename Included
                    theFN = Path.GetFileName(thepath)       'extension Included
                    Try
                        SrcRaster = Raster.Open(thepath)
                    Catch ex As Exception
                        MessageBox.Show(Kiemtrachuanhoa, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraTabControl1.SelectedTabPage = XtraTabPage_Step4
                        Return
                    End Try


                    ProgressBarControl8.EditValue = 0.1
                    '*****03.01.2019 Chuyển từ Min_Dảo chiều về MIN
                    listRaster.Add(MyMagic.RasterMath("MIN", listRaster(listRaster.Count - 1), SrcRaster, tmppath + i.ToString + "MinBaThanhPhan.Tif", ProgressBarControl8, Nothing))

                    If i > 0 Then   'Trường hợp i=0 thì listRaster(listRaster.Count - 2) sẽ là TNST
                        RasterDisp(listRaster(listRaster.Count - 2), listRaster(listRaster.Count - 2).Filename)
                        RasterDisp(SrcRaster, "")
                    End If
                Next


                '============================
            ElseIf RadioGroup1.SelectedIndex = 2 Then ''=======================2.3. MIN(ST, KTXH) - MT
                '===2.3.1. Xét Min (ST,KTXH)
                GridView1.ClearColumnsFilter()
                GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '2. Kinh tế - Xã hội' or [IndGroup] = '2. Socio - Economic'")
                'MessageBox.Show(GridView1.RowCount.ToString + "Đang tính Min ST-KTXH")
                For i As Integer = 0 To GridView1.RowCount - 1
                    thepath = GridView1.GetRowCellValue(i, "Mapchuanhoa")       'Filename Included
                    'MessageBox.Show(i.ToString + "_" + thepath)
                    theFN = Path.GetFileName(thepath)       'extension Included
                    Try
                        SrcRaster = Raster.Open(thepath)
                    Catch ex As Exception
                        MessageBox.Show(Kiemtrachuanhoa, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraTabControl1.SelectedTabPage = XtraTabPage_Step4
                        Return
                    End Try

                    ProgressBarControl8.EditValue = 0.1
                    '*****03.01.2019 Chuyển từ Min_Dảo chiều về MIN
                    listRaster.Add(MyMagic.RasterMath("MIN", listRaster(listRaster.Count - 1), SrcRaster, tmppath + i.ToString + "MinBaThanhPhan.Tif", ProgressBarControl8, Nothing))
                    'listRaster.Add(MinRASTER(listRaster(listRaster.Count - 1), SrcRaster, tmppath + i.ToString + "MinBaThanhPhan.Tif", ProgressBarControl8))
                    If i > 0 Then   'Trường hợp i=0 thì listRaster(listRaster.Count - 2) sẽ là TNST
                        RasterDisp(listRaster(listRaster.Count - 2), listRaster(listRaster.Count - 2).Filename)
                    End If
                Next

                '===2.3.2. Hiệu giữa Min (ST,KTXH) & MT
                GridView1.ClearColumnsFilter()
                GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '3. Môi trường' or [IndGroup] = '3. Environment'")
                'MessageBox.Show(GridView1.RowCount.ToString + "Đang tính Hiệu giữa Min ST-KTXH và MT")
                For i As Integer = 0 To GridView1.RowCount - 1
                    thepath = GridView1.GetRowCellValue(i, "Mapchuanhoa")       'Filename Included
                    'MessageBox.Show(i.ToString + " " + thepath)
                    theFN = Path.GetFileName(thepath)       'extension Included
                    Try
                        SrcRaster = Raster.Open(thepath)
                        tmpRevertRaster = MyMagic.RevertRaster(SrcRaster, theFN)    '**** 03.01.2019
                    Catch ex As Exception
                        MessageBox.Show(Kiemtrachuanhoa, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        XtraTabControl1.SelectedTabPage = XtraTabPage_Step4
                        Return
                    End Try
                    '*****03.01.2019
                    listRaster.Add(MyMagic.RasterMath("Trừ", listRaster(listRaster.Count - 1), tmpRevertRaster, tmppath + i.ToString + "TruMin_MT.Tif", ProgressBarControl8, Nothing))
                    If i > 0 Then   'Trường hợp i=0 thì listRaster(listRaster.Count - 2) sẽ là TNST
                        RasterDisp(listRaster(listRaster.Count - 2), tmppath + (i - 1).ToString + "TruBaThanhPhan.Tif")
                        RasterDisp(SrcRaster, "")
                        RasterDisp(tmpRevertRaster, theFN)
                    End If
                Next
            End If



        End If


        ProgressBarControl7.PerformStep()
        ProgressBarControl7.Update()


        '================'================'================'================'================
        '================'======2.  TIẾN HÀNH NHÂN KQDGTN1 với Constraint   '================
        '================'================'================'================'================
        GridView1.ClearColumnsFilter()
        GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '4. Hạn chế' or [IndGroup] = '4. Constraint'")

        Dim tmpNhanFN As String
        Dim hancheRaster As IRaster
        For i As Integer = 0 To GridView1.RowCount - 1
            thepath = GridView1.GetRowCellValue(i, "Mapchuanhoa")       'Filename Included
            theFN = Path.GetFileName(thepath)       'extension Included
            If Not File.Exists(thepath) Then
                MessageBox.Show(Kiemtrachuanhoa + " (Constraint map)", thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraTabControl1.SelectedTabPage = XtraTabPage_Step4
                ProgressBarControl7.EditValue = 0
                ProgressBarControl7.Update()
                ProgressBarControl8.EditValue = 0
                ProgressBarControl8.Update()
                Return
            End If
        Next
        For i As Integer = 0 To GridView1.RowCount - 1
            thepath = GridView1.GetRowCellValue(i, "Mapchuanhoa")       'Filename Included
            theFN = Path.GetFileName(thepath)       'extension Included
            Try
                hancheRaster = Raster.Open(thepath)
            Catch ex As Exception
                MessageBox.Show(Kiemtrachuanhoa, thongbao, MessageBoxButtons.OK, MessageBoxIcon.Error)
                XtraTabControl1.SelectedTabPage = XtraTabPage_Step4
                Return
            End Try
            tmpNhanFN = tmppath + "M_" + i.ToString + theFN
            GC.Collect()
            listRaster.Add(MyMagic.RasterMath("Nhân", listRaster(listRaster.Count - 1), hancheRaster, tmpNhanFN, ProgressBarControl8, Nothing))       'listRaster(listRaster.Count-1): Là Raster cuối cùng của ListRas
            RasterDisp(listRaster(listRaster.Count - 2), listRaster(listRaster.Count - 2).Filename) 'Lúc này phải là listRaster.Count - 2 (không phải là trừ 1 nữa) vì vừa add thêm 1 item vào list.
            RasterDisp(hancheRaster, "")
        Next

        ProgressBarControl7.PerformStep()
        ProgressBarControl7.Update()

        '================'================'================'================'================
        '================'======3.  TIẾN HÀNH UNSIGNED     '================'================
        '================'================'================'================'================
        Dim unsigned As IRaster
        Dim rasteroptions As String()
        Dim datatype As System.Type = System.Type.GetType("System.Int16")
        Try
            unsigned = Raster.CreateRaster(resultname, Nothing, listRaster(listRaster.Count - 1).NumColumns, listRaster(listRaster.Count - 1).NumRows, 1, datatype, rasteroptions)
        Catch ex As Exception
            Return
        End Try

        ProgressBarControl7.PerformStep()
        ProgressBarControl7.Update()

        With unsigned     'Bước này có cộng thêm 1 cho BaHopPhan vì lý thuyết trừ giữa minTNST_KTXH và TNMTRaster thì phải + thêm 1 ( Do 2 raster này chạy từ 1-4)
            .Filename = resultname
            .Bounds = listRaster(listRaster.Count - 1).Bounds
            .NoDataValue = -9999
            .Projection = listRaster(listRaster.Count - 1).Projection
        End With
        'listRaster(listRaster.Count - 1).Save()
        ProgressBarControl8.Text = "3/3"
        ProgressBarControl8.EditValue = 0.1
        ProgressBarControl8.Properties.PercentView = True
        ProgressBarControl8.Properties.Step = 1
        ProgressBarControl8.Properties.Maximum = (listRaster(listRaster.Count - 1).NumRows) '* (KQDGXXX_signed.NumColumns)
        ProgressBarControl8.Properties.Minimum = 0
        ProgressBarControl8.Update()
        For i As Integer = 0 To listRaster(listRaster.Count - 1).NumRows - 1
            For j As Integer = 0 To listRaster(listRaster.Count - 1).NumColumns - 1
                If listRaster(listRaster.Count - 1).Value(i, j) <> listRaster(listRaster.Count - 1).NoDataValue And listRaster(listRaster.Count - 1).Value(i, j) > -9999 Then
                    If listRaster(listRaster.Count - 1).Value(i, j) < 0 Then
                        unsigned.Value(i, j) = 0
                    Else
                        unsigned.Value(i, j) = listRaster(listRaster.Count - 1).Value(i, j)
                    End If
                Else
                    unsigned.Value(i, j) = -9999
                End If
            Next
        Next
        ProgressBarControl8.PerformStep()
        ProgressBarControl8.Update()
        '================4. SAVE FILE CUỐI CÙNG
        '===============
        'unsigned.SaveAs(resultname)

        ProgressBarControl7.PerformStep()
        ProgressBarControl7.Update()
        unsigned.Save()
        ProgressBarControl7.Visible = False
        ProgressBarControl8.Visible = False
        '================
        '===============
        GridView1.ClearColumnsFilter()
        If BarCheckItemComposite.Checked = True Then
            GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '1. Sinh thái' OR [IndGroup] = '1. Ecology'")         '("[IndGroup] <> '2. Kinh tế - Xã hội' and [IndGroup] <> '3. Môi trường' and [IndGroup] <> '2. Socio - Economic' and [IndGroup] <> '3. Environment' and [IndGroup] <> '4. Hạn chế' and [IndGroup] <> '4. Constraint'")         'GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] <> '4. Hạn chế' and [IndGroup] <> '4. Constraint' and [IndGroup] <> '2. Kinh tế - Xã hội' and [IndGroup] <> '2. Socio - Economic' and [IndGroup] <> '3. Môi trường' and [IndGroup] <> '3. Environment'")
            GridView2.FocusedRowHandle = -1
            GridView2.FocusedRowHandle = 0
        Else
            GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] <> '1. Sinh thái' AND [IndGroup] <> '1. Ecology'")         '("[IndGroup] <> '2. Kinh tế - Xã hội' and [IndGroup] <> '3. Môi trường' and [IndGroup] <> '2. Socio - Economic' and [IndGroup] <> '3. Environment' and [IndGroup] <> '4. Hạn chế' and [IndGroup] <> '4. Constraint'")             'GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '4. Hạn chế' or [IndGroup] = '4. Constraint' or [IndGroup] = '2. Kinh tế - Xã hội' or [IndGroup] = '2. Socio - Economic' or [IndGroup] = '3. Môi trường' or [IndGroup] = '3. Environment'")
        End If
        Bar3.Text = "Thực hiện thành công đánh giá xxx"


        GC.Collect()
        '============Xóa các file tạm
        Try
            For i = 0 To listRaster.Count - 1
                RasterDisp(listRaster(i), "")
            Next
        Catch ex As Exception

        End Try

        xoatmp(tmppath)
        GC.Collect()
    End Sub


    '====Min 2 raster, Raster thứ 2 bị đảo chiều
    'Public Function MinRASTER(ByVal input1 As IRaster, ByVal input2 As IRaster, ByVal outputFN As String, ByVal cancelProgressHandler As ICancelProgressHandler) As IRaster
    'Dim magic1 As New myRasterMagic
    'Return magic1.RasterMath("MIN", input1, input2, outputFN, cancelProgressHandler)
    'End Function
    Function MinRASTER(raster1 As IRaster, raster2 As IRaster, ByVal outputFN As String, ByVal progr As DevExpress.XtraEditors.ProgressBarControl) As IRaster
        ''==Progress
        'progr.EditValue = 0.1
        'progr.Properties.PercentView = True
        'progr.Properties.Step = 1
        'progr.Properties.Maximum = (raster1.NumRows) '* (RasterMath.Bounds.NumColumns)
        'progr.Properties.Minimum = 0
        'progr.Update()
        ''====
        ''Dim MinRASTER As IRaster
        'Dim rasteroptions As String()
        'Dim datatype As System.Type = System.Type.GetType("System.Single")
        'MinRASTER = Raster.CreateRaster(outputFN, Nothing, raster1.NumColumns, raster1.NumRows, 1, raster1.DataType, rasteroptions)
        ''Bounds specify the cellsize and the coordinates of raster corner
        'With MinRASTER
        '.Filename = outputFN    'tmppath + "MinTNST_KTXH.Tif"
        '.Bounds = raster1.Bounds
        '.NoDataValue = -9999
        '.Projection = raster1.Projection
        'End With
        'For i As Integer = 0 To raster1.NumRows - 1
        'For j As Integer = 0 To raster1.NumColumns - 1
        'If i = 466 And j = 928 Then
        'MessageBox.Show("")
        'End If
        'If raster1.Value(i, j) <> raster1.NoDataValue And raster1.Value(i, j) > -9999 Then
        'If raster2.Value(i, j) <> raster2.NoDataValue And raster2.Value(i, j) > -9999 Then
        ''===Xử lý đảo chiều giá trị file Raster2. Ví dụ Kinhte: 0 là 0 ảnh hưởng... 3 là ảnh hưởng nhiều. 
        ''Thì đảo lại thành 3 là không ảnh hưởng, 0 là ảnh hưởng nhiều rồi lấy Min thì ra kết quả cho định luật Libick
        'Dim tmpRaster2Value As Integer = raster2.Maximum - raster2.Value(i, j)
        'If raster1.Value(i, j) >= tmpRaster2Value Then      'raster2.Value(i, j) Then
        'MinRASTER.Value(i, j) = tmpRaster2Value         'raster2.Value(i, j)
        'Else : MinRASTER.Value(i, j) = raster1.Value(i, j)
        'End If

        'Else
        'MinRASTER.Value(i, j) = -9999
        'End If
        'End If
        'Next
        'progr.PerformStep()
        'progr.Update()
        'Next
    End Function

    '=====Min của 3 Raster, trong đó Raster2 và Raster3 lấy Max rồi đảo chiều
    Function Min3RASTER(raster1 As IRaster, raster2 As IRaster, raster3 As IRaster, ByVal outputFN As String, ByVal progr As DevExpress.XtraEditors.ProgressBarControl) As IRaster
        ''==Progress
        'progr.EditValue = 0.1
        'progr.Properties.PercentView = True
        'progr.Properties.Step = 1
        'progr.Properties.Maximum = (raster1.NumRows) '* (RasterMath.Bounds.NumColumns)
        'progr.Properties.Minimum = 0
        'progr.Update()
        ''====
        ''Dim MinRASTER As IRaster
        'Dim rasteroptions As String()
        'Dim datatype As System.Type = System.Type.GetType("System.Single")
        'Min3RASTER = Raster.CreateRaster(outputFN, Nothing, raster1.NumColumns, raster1.NumRows, 1, raster1.DataType, rasteroptions)
        ''Bounds specify the cellsize and the coordinates of raster corner
        'With Min3RASTER
        '.Filename = outputFN    'tmppath + "MinTNST_KTXH.Tif"
        '.Bounds = raster1.Bounds
        '.NoDataValue = -9999
        '.Projection = raster1.Projection
        'End With
        'For i As Integer = 0 To raster1.NumRows - 1
        'For j As Integer = 0 To raster1.NumColumns - 1
        'If raster1.Value(i, j) <> raster1.NoDataValue And raster1.Value(i, j) > -9999 Then
        'If raster2.Value(i, j) <> raster2.NoDataValue And raster2.Value(i, j) > -9999 Then
        'If raster3.Value(i, j) <> raster3.NoDataValue And raster3.Value(i, j) > -9999 Then
        ''===Xử lý đảo chiều giá trị file Raster2. Ví dụ Kinhte: 0 là 0 ảnh hưởng... 3 là ảnh hưởng nhiều. 
        ''Thì đảo lại thành 3 là không ảnh hưởng, 0 là ảnh hưởng nhiều rồi lấy Min thì ra kết quả cho định luật Libick
        'Dim tmpMinRaster2_3 As Integer '= raster2.Maximum - raster2.Value(i, j)
        'If raster2.Maximum - raster2.Value(i, j) >= raster3.Maximum - raster3.Value(i, j) Then
        'tmpMinRaster2_3 = raster3.Value(i, j)
        'Else
        'tmpMinRaster2_3 = raster2.Value(i, j)
        'End If

        'If raster1.Value(i, j) >= tmpMinRaster2_3 Then
        'Min3RASTER.Value(i, j) = tmpMinRaster2_3
        'Else
        'Min3RASTER.Value(i, j) = raster1.Value(i, j)
        'End If
        'Else
        'Min3RASTER.Value(i, j) = -9999
        'End If
        'Else
        'Min3RASTER.Value(i, j) = -9999
        'End If
        'Else
        'Min3RASTER.Value(i, j) = -9999
        'End If
        'Next
        'progr.PerformStep()
        'progr.Update()
        'Next
    End Function
    '==============Min 2 Raster
    Function MinRASTER1(raster1 As IRaster, raster2 As IRaster, ByVal outputFN As String, ByVal progr As DevExpress.XtraEditors.ProgressBarControl) As IRaster
        ''==Progress
        'progr.EditValue = 0.1
        'progr.Properties.PercentView = True
        'progr.Properties.Step = 1
        'progr.Properties.Maximum = (raster1.NumRows) '* (RasterMath.Bounds.NumColumns)
        'progr.Properties.Minimum = 0
        'progr.Update()
        ''====
        ''Dim MinRASTER As IRaster
        'Dim rasteroptions As String()
        'Dim datatype As System.Type = System.Type.GetType("System.Single")
        'MinRASTER1 = Raster.CreateRaster(outputFN, Nothing, raster1.NumColumns, raster1.NumRows, 1, raster1.DataType, rasteroptions)
        ''Bounds specify the cellsize and the coordinates of raster corner
        'With MinRASTER1
        '.Filename = outputFN    'tmppath + "MinTNST_KTXH.Tif"
        '.Bounds = raster1.Bounds
        '.NoDataValue = -9999
        '.Projection = raster1.Projection
        'End With
        'For i As Integer = 0 To raster1.NumRows - 1
        'For j As Integer = 0 To raster1.NumColumns - 1
        'If raster1.Value(i, j) <> raster1.NoDataValue And raster1.Value(i, j) > -9999 Then
        'If raster2.Value(i, j) <> raster2.NoDataValue And raster2.Value(i, j) > -9999 Then
        'If raster1.Value(i, j) >= raster2.Value(i, j) Then
        'MinRASTER1.Value(i, j) = raster2.Value(i, j)
        'Else : MinRASTER1.Value(i, j) = raster1.Value(i, j)
        'End If

        'Else
        'MinRASTER1.Value(i, j) = -9999
        'End If
        'End If
        'Next
        'progr.PerformStep()
        'progr.Update()
        'Next
    End Function
    Private Sub Button7_Click(sender As Object, e As EventArgs)
        GridControl1.RefreshDataSource()
        'Gridcontrol1.Refresh()
        GridView1.RefreshData()
        'GridView1.RefreshEditor(True)
        'GridView1.RefreshRow(0)
        GridView1.PopulateColumns()
        GridView1.LayoutChanged()
        'Gridcontrol1.DataBindings.Add(New Binding()
        'Gridcontrol1.DataSource = Gridview1_Connect.DtFromQry("select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
    End Sub


    Private Sub BarButtonItem8_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarButtonItem8.ItemClick

        FrmXdungXoiMonCh.ShowDialog()
    End Sub

    Private Sub GridControl1_Click(sender As Object, e As EventArgs) Handles GridControl1.Click

    End Sub

    Private Sub GridView1_MouseMove(sender As Object, e As MouseEventArgs) Handles GridView1.MouseMove
        'e.Location = GridView1.Columns(0).iteminfo
    End Sub

    Private Sub ToolTipController1_GetActiveObjectInfo(sender As Object, e As ToolTipControllerGetActiveObjectInfoEventArgs) Handles toolTipController11.GetActiveObjectInfo


        e.Info = New ToolTipControlInfo("jki", "i")
        'Dim hi As GridHitInfo = GridView1.CalcHitInfo(e.ControlMousePosition)
        'If Not hi.Column Is Nothing Then
        'Dim o = hi.RowHandle.ToString() + hi.Column.FieldName
        'Dim info As ToolTipControlInfo = New ToolTipControlInfo(o, hi.Column.FieldName)
        'info.ImmediateToolTip = True
        'e.Info = info
        'End If
        'If Not (TypeOf e.SelectedControl Is DevExpress.XtraGrid.GridControl) Then
        'Return
        'End If
        'Dim gvTemp As GridView = TryCast((CType(e.SelectedControl, DevExpress.XtraGrid.GridControl)).MainView, GridView)
        'Dim ghi As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = gvTemp.CalcHitInfo(e.ControlMousePosition)
        'If ghi.HitTest = GridHitTest.Column Then
        'e.Info = New ToolTipControlInfo(ghi.Column, ghi.Column.FieldName)
        'End If
        ToolTipController1.ShowHint(e.Info)
    End Sub
    Private Sub defaultToolTipController1_DefaultController_GetActiveObjectInfo(ByVal sender As Object, ByVal e As DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs) Handles DefaultToolTipController1.DefaultController.GetActiveObjectInfo


        If Not (TypeOf e.SelectedControl Is DevExpress.XtraGrid.GridControl) Then
            Return
        End If
        Dim hi As GridHitInfo = GridView1.CalcHitInfo(e.ControlMousePosition)
        If Not hi.Column Is Nothing Then
            Dim obj = hi.RowHandle.ToString() + hi.Column.FieldName
            Dim info As ToolTipControlInfo = New ToolTipControlInfo()
            Try
                Select Case GridView1.GetDataRow(hi.RowHandle)("UsedFunction").ToString

                    Case aHams1
                        Select Case hi.Column.FieldName
                            Case "A", "a"
                                info = New ToolTipControlInfo(obj, "Giá trị cận trên cho Không thích nghi")
                            Case "B", "b"
                                info = New ToolTipControlInfo(obj, "Giá trị cận dưới cho  thích nghi")

                        End Select
                    Case aHams2
                        Select Case hi.Column.FieldName
                            Case "A", "a"
                                info = New ToolTipControlInfo(obj, "Giá trị cận trên cho Thích nghi")
                            Case "B", "b"
                                info = New ToolTipControlInfo(obj, "Giá trị cận dưới cho không thích nghi")
                        End Select
                    Case aHamkandel
                        Select Case hi.Column.FieldName
                            Case "A", "a"
                                info = New ToolTipControlInfo(obj, "Giá trị thích nghi cận dưới")
                            Case "B", "b"
                                info = New ToolTipControlInfo(obj, "Giá trị thích nghi cận trên")

                        End Select
                    Case aHamhinhthang
                        Select Case hi.Column.FieldName
                            Case "A", "a"
                                info = New ToolTipControlInfo(obj, "Giá trị Không thích nghi cận dưới")
                            Case "B", "b"
                                info = New ToolTipControlInfo(obj, "Giá trị Thích nghi cận dưới")
                            Case "C", "c"
                                info = New ToolTipControlInfo(obj, "Giá trị Thích nghi cận trên")
                            Case "D", "d"
                                info = New ToolTipControlInfo(obj, "Giá trị Không thích nghi cận trên")
                        End Select
                    Case aHamtheoloai
                        Select Case hi.Column.FieldName
                            Case "a"
                                info = New ToolTipControlInfo(obj, "Không thích nghi")

                            Case "b"
                                info = New ToolTipControlInfo(obj, "Thích nghi")

                            Case "c"
                                info = New ToolTipControlInfo(obj, "Rất thích nghi")

                            Case "d"
                                info = New ToolTipControlInfo(obj, "Thích nghi tối ưu")

                        End Select
                        'info.ImmediateToolTip = True

                End Select
            Catch ex As Exception

            End Try
            e.Info = info
        End If


        'If Not hi.Column Is Nothing Then
        'Dim o = hi.RowHandle.ToString() + hi.Column.FieldName
        'Dim info As ToolTipControlInfo = New ToolTipControlInfo(o, hi.Column.FieldName)
        'info.ImmediateToolTip = True
        'e.Info = info
        'End If



        'Dim gvTemp As GridView = TryCast((CType(e.SelectedControl, DevExpress.XtraGrid.GridControl)).MainView, GridView)
        'Dim ghi As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = gvTemp.CalcHitInfo(e.ControlMousePosition)
        'If ghi.HitTest = GridHitTest.Column Then
        'e.Info = New ToolTipControlInfo(ghi.Column, ghi.Column.FieldName)
        'End If


    End Sub

    Private Sub TxtS1_Keyup(sender As Object, e As EventArgs) Handles TxtS1.KeyUp
        GridView1.UpdateCurrentRow()
        BtnUpdate.Enabled = True
    End Sub
    Private Sub TxtS2_Keyup(sender As Object, e As EventArgs) Handles TxtS2.KeyUp
        GridView1.UpdateCurrentRow()
        BtnUpdate.Enabled = True
    End Sub
    Private Sub TxtS3_Keyup(sender As Object, e As EventArgs) Handles TxtS3.KeyUp
        GridView1.UpdateCurrentRow()
        BtnUpdate.Enabled = True
    End Sub
    Private Sub TxtN_Keyup(sender As Object, e As EventArgs) Handles TxtN.KeyUp
        GridView1.UpdateCurrentRow()
        BtnUpdate.Enabled = True
    End Sub

    Private Sub ComboBoxEdit_Hanche_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEdit_Hanche.SelectedIndexChanged
        Dim HandleRow As Int16
        Try
            HandleRow = GridView2.FocusedRowHandle
        Catch ex As Exception

        End Try


        GridView2.SetRowCellValue(HandleRow, "GhiChu", ComboBoxEdit_Hanche.SelectedItem)
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")

        GridView2.UpdateCurrentRow()

        '==Đếm số Txtedit trong XtraScrollableControl4
        Dim Txtcount As Integer = 0
        For Each Txt As Control In XtraScrollableControl4.Controls
            If Txt.GetType Is GetType(TextEdit) Then
                Txtcount = Txtcount + 1
            End If
        Next

        If ComboBoxEdit_Hanche.SelectedItem = "Hạn chế" Or ComboBoxEdit_Hanche.SelectedItem = "Constrain" Then
            ComboBoxEdit_solopFilter.SelectedIndex = 1
            ComboBoxEdit_solopFilter.Enabled = False
            If Txtcount > 0 Then
                For i As Integer = 0 To Txtcount / 3 - 1
                    TryCast(FindControl(XtraScrollableControl4, "txtNewValue" + i.ToString), TextEdit).Properties.ReadOnly = False
                Next
            End If

        Else
            'ComboBoxEdit_solopFilter.SelectedIndex = 4
            ComboBoxEdit_solopFilter.Enabled = True

            If Txtcount > 0 Then
                For i As Integer = 0 To Txtcount / 3 - 1
                    TryCast(FindControl(XtraScrollableControl4, "txtNewValue" + i.ToString), TextEdit).Properties.ReadOnly = True
                Next
            End If

        End If
    End Sub

    Private Sub GridView2_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles GridView2.FocusedRowChanged
        If XtraTabControl1.SelectedTabPage Is XtraTabPage_Step1 Then
            Return
        End If
        GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")
        Try
            If GridView2.FocusedRowHandle = 0 Then
                GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '1. Sinh thái' OR [IndGroup] = '1. Ecology'")         '("[IndGroup] <> '2. Kinh tế - Xã hội' and [IndGroup] <> '3. Môi trường' and [IndGroup] <> '2. Socio - Economic' and [IndGroup] <> '3. Environment' and [IndGroup] <> '4. Hạn chế' and [IndGroup] <> '4. Constraint'")
                CreateFromToTxtbox(ComboBoxEditSoLopDGTNST, XtraScrollableControl3, GridView2.GetRow(0), True, False)
            ElseIf GridView2.FocusedRowHandle = 1 Then
                GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '2. Kinh tế - Xã hội' OR [IndGroup] = '2. Socio - Economic'")
                CreateFromToTxtbox(ComboBoxEditSoLopDGKTXH, XtraScrollableControl3, GridView2.GetRow(1), True, True)
            ElseIf GridView2.FocusedRowHandle = 2 Then
                GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '3. Môi trường' OR [IndGroup] = '3. Environment'")
                CreateFromToTxtbox(ComboBoxEditSoLopDGTNMT, XtraScrollableControl3, GridView2.GetRow(2), True, True)
            ElseIf GridView2.FocusedRowHandle = 3 Then
                GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '4. Hạn chế' OR [IndGroup] = '4. Constraint'")
            End If

        Catch ex As Exception

        End Try

    End Sub
    'Private Sub GridView2_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GridView2.CustomDrawCell
    'If (e.Column.Name = "Name" And e.RowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle) Then
    'e.Handled = True
    'End If
    'End Sub

    'Private Sub GridView1_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GridView1.CustomDrawCell
    'If (e.Column.Name = "IndName" And e.RowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle) Then
    'e.Handled = True
    'End If
    'End Sub
    Private Sub TxtDGTNST_GotFocus(sender As Object, e As EventArgs) Handles TxtDGTNST.GotFocus
        GridView2.FocusedRowHandle = 0
    End Sub
    Private Sub TxtDGKTXH_GotFocus(sender As Object, e As EventArgs) Handles TxtDGKTXH.GotFocus
        GridView2.FocusedRowHandle = 1
    End Sub
    Private Sub TxtDGTNMT_GotFocus(sender As Object, e As EventArgs) Handles TxtDGTNMT.GotFocus
        GridView2.FocusedRowHandle = 2
    End Sub

    Private Sub RadDGTN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadDGTN.SelectedIndexChanged
        Dim aa = WeightValidate()
        If aa(0) = -1 Then     'tổng weight không = 1
            RadDGTN.SelectedIndex = aa(1)
            'Return
        End If
        If RadDGTN.SelectedIndex = 0 Then
            GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")
            'GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] <> '2. Kinh tế - Xã hội' and [IndGroup] <> '3. Môi trường' and [IndGroup] <> '2. Socio - Economic' and [IndGroup] <> '3. Environment'")
            GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '1. Sinh thái' OR [IndGroup] = '1. Ecology'")         '("[IndGroup] <> '2. Kinh tế - Xã hội' and [IndGroup] <> '3. Môi trường' and [IndGroup] <> '2. Socio - Economic' and [IndGroup] <> '3. Environment' and [IndGroup] <> '4. Hạn chế' and [IndGroup] <> '4. Constraint'")      ' GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] <> '2. Kinh tế - Xã hội' and [IndGroup] <> '3. Môi trường' and [IndGroup] <> '2. Socio - Economic' and [IndGroup] <> '3. Environment' and [IndGroup] <> '4. Hạn chế' and [IndGroup] <> '4. Constraint'")
            weightCal("TNST")
        ElseIf RadDGTN.SelectedIndex = 1 Then
            GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")
            GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '2. Kinh tế - Xã hội' or [IndGroup] = '2. Socio - Economic'")
            weightCal("KTXH")
        ElseIf RadDGTN.SelectedIndex = 2 Then
            GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("")
            GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '3. Môi trường' or [IndGroup] = '3. Environment'")
            weightCal("TNMT")
        End If
    End Sub
    Private Function WeightValidate()
        '========Xét Tổng Weight = 1??
        Dim Weig As Double = 0
        Dim WeigVal As Double
        GridView1.Columns("Limit").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[Limit] = 'Không giới hạn' or [Limit] = 'Non-Limit'")
        For i As Integer = 0 To GridView1.RowCount - 1
            Try
                WeigVal = CType(GridView1.GetRowCellValue(i, "Weight").ToString, Single)
            Catch ex As Exception
                Continue For
            End Try
            Weig = Weig + WeigVal
        Next
        Dim alist As New List(Of Integer)
        alist.Add(1)
        alist.Add(1)
        If GridView1.RowCount > 0 And (Weig < 0.98 Or Weig > 1.02) Then
            'Dim c = 2
            MessageBox.Show(Trongso1, thongbao)
            'XtraTabControl1.SelectedTabPage = XtraTabPage_Step3
            alist(0) = -1
            alist(1) = RadDGTN.SelectedIndex
            Return alist
            Exit Function
        End If
        alist(1) = RadDGTN.SelectedIndex
        Return alist
        '================
    End Function
    Private Sub TxtDGTNST_Reclass_GotFocus(sender As Object, e As EventArgs) Handles TxtDGTNST_Reclass.GotFocus
        GridView2.FocusedRowHandle = 0
    End Sub

    Private Sub TxtDGTNST_Reclass_TextChanged(sender As Object, e As EventArgs) Handles TxtDGTNST_Reclass.TextChanged
        GridView2.SetRowCellValue(0, "RecTif", TxtDGTNST_Reclass.Text)
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")
        If BarCheckItemComposite.Checked = True Then
            If TxtDGKTXH_Reclass.Text <> "" And TxtDGTNMT_Reclass.Text <> "" And TxtDGTNST_Reclass.Text <> "" Then
                'XtraTabPage7.PageEnabled = True
            Else
                'XtraTabPage7.PageEnabled = False
            End If
        Else
            If TxtDGTNST_Reclass.Text <> "" Then
                'XtraTabPage7.PageEnabled = True
            Else
                'XtraTabPage7.PageEnabled = False
            End If
        End If

    End Sub

    Private Sub TxtDGKTXH_Reclass_GotFocus(sender As Object, e As EventArgs) Handles TxtDGKTXH_Reclass.GotFocus
        GridView2.FocusedRowHandle = 1
    End Sub
    Private Sub TxtDGTNSTKTXH_Reclass_TextChanged(sender As Object, e As EventArgs) Handles TxtDGKTXH_Reclass.TextChanged
        GridView2.SetRowCellValue(1, "RecTif", TxtDGKTXH_Reclass.Text)
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")
        If TxtDGKTXH_Reclass.Text <> "" And TxtDGTNMT_Reclass.Text <> "" And TxtDGTNST_Reclass.Text <> "" Then
            'XtraTabPage7.PageEnabled = True
        Else
            'XtraTabPage7.PageEnabled = False
        End If
    End Sub

    Private Sub TxtDGTNMT_Reclass_GotFocus(sender As Object, e As EventArgs) Handles TxtDGTNMT_Reclass.GotFocus
        GridView2.FocusedRowHandle = 2
    End Sub
    Private Sub TxtDGTNMT_Reclass_TextChanged(sender As Object, e As EventArgs) Handles TxtDGTNMT_Reclass.TextChanged
        GridView2.SetRowCellValue(2, "RecTif", TxtDGTNMT_Reclass.Text)
        Gridview2_Connect.Update_CSDL(GridControl2, "Select * from [" + ListBoxControl1.Tag.ToString + "]")
        If BarCheckItemComposite.Checked = True Then
            If TxtDGKTXH_Reclass.Text <> "" And TxtDGTNMT_Reclass.Text <> "" And TxtDGTNST_Reclass.Text <> "" Then
                XtraTabPage_Step5.PageEnabled = True
            Else
                XtraTabPage_Step5.PageEnabled = False
            End If
        Else
            If TxtDGTNST_Reclass.Text <> "" Then
                XtraTabPage_Step5.PageEnabled = True
            Else
                XtraTabPage_Step5.PageEnabled = False
            End If
        End If


    End Sub

    Private Sub BtnGdv1ClearFilter_Click_1(sender As Object, e As EventArgs) Handles BtnGdv1ClearFilter.Click
        GridView1.ClearColumnsFilter()
    End Sub

    Private Sub BarCheckItemComposite_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles BarCheckItemComposite.CheckedChanged



        'Dim truefalse As Boolean = True
        'If BarCheckItemComposite.Checked = False Then
        'If BarCheckItemComposite.Checked = True Then
        'For i = 0 To GridView1.RowCount - 1
        'GridView1.SetRowCellValue(i, "Limit", RadLim.EditValue)
        'Next

        'End If

        '====
        '===Apply for Tab1
        'EnableControl()
        GridView2.ClearColumnsFilter()
        If BarCheckItemComposite.Checked = True Then
            GridView2.SetRowCellValue(0, "GhiChu", "EnCs checked")
        Else
            GridView2.SetRowCellValue(0, "GhiChu", "")
            ' GridView2.Columns("Name").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[Name] = '" + TNST + "'")
            TxtDGTNST.Focus()
        End If


         '===Xử lý disable khi chọn hàm không chuẩn hóa; hạn chế; ....
        If (cboIndGroup.SelectedItem = "1. Sinh thái" Or cboIndGroup.SelectedItem = "1. Ecology") And (cboFunction.SelectedItem = ahamKhongham) Then
            DisableControl()
            RadLim.SelectedIndex = -1      'Thử bỏ đi do lỗi check và bỏ check cái Enc xong nó cứ báo lỗi tổng trọng số =1khi chuyển đi chuyển lại cái Tab Trọng số    'Có 3 chỗ thử bỏ đi. Nếu phát sinh lỗi thì phải thêm vào cả 3 chỗ nhé
            cboFunction.Enabled = True
        ElseIf cboIndGroup.SelectedItem = "4. Hạn chế" Or cboIndGroup.SelectedItem = "4. Constraint" Then
            DisableControl()
            RadLim.SelectedIndex = -1
        ElseIf BarCheckItemComposite.Checked = False And (cboIndGroup.SelectedItem = "2. Kinh tế - Xã hội" Or cboIndGroup.SelectedItem = "2. Socio - Economic" Or cboIndGroup.SelectedItem = "3. Môi trường" Or cboIndGroup.SelectedItem = "3. Environment") Then
            DisableControl()
            RadLim.SelectedIndex = -1      'Thử bỏ đi do lỗi check và bỏ check cái Enc xong nó cứ báo lỗi tổng trọng số =1khi chuyển đi chuyển lại cái Tab Trọng số    'Có 3 chỗ thử bỏ đi. Nếu phát sinh lỗi thì phải thêm vào cả 3 chỗ nhé

        ElseIf BarCheckItemComposite.Checked = True And (cboIndGroup.SelectedItem = "2. Kinh tế - Xã hội" Or cboIndGroup.SelectedItem = "2. Socio - Economic" Or cboIndGroup.SelectedItem = "3. Môi trường" Or cboIndGroup.SelectedItem = "3. Environment") And (cboFunction.SelectedItem = ahamKhongham) Then
            DisableControl()
            RadLim.SelectedIndex = -1      'Thử bỏ đi do lỗi check và bỏ check cái Enc xong nó cứ báo lỗi tổng trọng số =1khi chuyển đi chuyển lại cái Tab Trọng số    'Có 3 chỗ thử bỏ đi. Nếu phát sinh lỗi thì phải thêm vào cả 3 chỗ nhé
            cboFunction.Enabled = True
        Else
            EnableControl()

        End If

        'If cboIndGroup.SelectedItem = "2. Kinh tế - Xã hội" Or cboIndGroup.SelectedItem = "2. Socio - Economic" Or cboIndGroup.SelectedItem = "3. Môi trường" Or cboIndGroup.SelectedItem = "3. Environment" Then
        'Txt_A.Text = ""
        'Txt_B.Text = ""
        'Txt_C.Text = ""
        'txt_D.Text = ""
        'TxtS1.Text = ""
        'TxtS2.Text = ""
        'TxtS3.Text = ""
        'TxtN.Text = ""
        ''TxtMap.Text = ""
        ''TxtMap.Enabled = BarCheckItemComposite.Checked
        ''BtnBrowse1.Enabled = BarCheckItemComposite.Checked
        ''LabelControl10.Enabled = BarCheckItemComposite.Checked
        'Dim mapPath As String = GridView1.GetRowCellValue(GridView1.FocusedRowHandle, "Map").ToString
        'If BarCheckItemComposite.Checked = False Then
        'TxtMapchuanhoa.Text = mapPath
        'Else
        'Try
        'TxtMapchuanhoa.Text = mapPath.Substring(0, TxtMap.Text.Length - 4) + "_ch.tif"
        'Catch ex As Exception
        'TxtMapchuanhoa.Text = ""
        'End Try

        'End If

        'TxtMapchuanhoa.Enabled = BarCheckItemComposite.Checked
        'BtnBrowse2.Enabled = BarCheckItemComposite.Checked
        'LabelControl12.Enabled = BarCheckItemComposite.Checked

        'LabelControl8.Enabled = BarCheckItemComposite.Checked
        'cboFunction.Enabled = BarCheckItemComposite.Checked
        'RadLim.Enabled = BarCheckItemComposite.Checked
        'RadLim.SelectedIndex = -1
        'cboFunction.SelectedIndex = 0

        'TxtMap.Enabled = True
        'LabelControl10.Enabled = True
        'BtnBrowse1.Enabled = True

        ''PanelControl6.Visible = False
        ''PanelControl14.Visible = False
        ''For Each c In PanelControl14.Controls
        ''c.enabled = BarCheckItemComposite.Checked
        ''Next
        'For Each c In PanelControl6.Controls
        'c.enabled = BarCheckItemComposite.Checked
        'Next
        'For Each c In PanelControl14.Controls
        'c.enabled = BarCheckItemComposite.Checked
        'Next
        'GridView1.UpdateCurrentRow()
        ''End If'============hạn chế
        'ElseIf cboIndGroup.SelectedItem = "4. Hạn chế" Or cboIndGroup.SelectedItem = "4. Constraint" Then

        ''btnAdd.Location = New System.Drawing.Point(615, 8)
        ''BtnReload.Location = New System.Drawing.Point(615, 33)
        ''btnDelete.Location = New System.Drawing.Point(615, 58)
        ''BtnUpdate.Location = New System.Drawing.Point(615, 83)
        'Txt_A.Text = ""
        'Txt_B.Text = ""
        'Txt_C.Text = ""
        'txt_D.Text = ""
        'TxtS1.Text = ""
        'TxtS2.Text = ""
        'TxtS3.Text = ""
        'TxtN.Text = ""
        'TxtMap.Text = ""

        'TxtMap.Enabled = False
        'LabelControl10.Enabled = False
        'BtnBrowse1.Enabled = False

        'TxtMapchuanhoa.Enabled = True
        'BtnBrowse2.Enabled = True
        'LabelControl12.Enabled = True

        'LabelControl8.Enabled = False
        'cboFunction.Enabled = False
        'RadLim.Enabled = False
        'RadLim.SelectedIndex = -1
        'cboFunction.SelectedIndex = 0

        'TxtMap.Enabled = False
        ''PanelControl6.Visible = False
        ''PanelControl14.Visible = False
        'For Each c In PanelControl14.Controls
        'c.enabled = False
        'Next
        'For Each c In PanelControl6.Controls
        'c.enabled = False
        'Next
        'GridView1.UpdateCurrentRow()
        'Else
        'GridView1.UpdateCurrentRow()

        'BtnBrowse1.Enabled = True
        'LabelControl8.Enabled = True
        'cboFunction.Enabled = True
        'LabelControl10.Enabled = True
        'TxtMap.Enabled = True
        'RadLim.Enabled = True
        'If RadLim.SelectedIndex = -1 Then
        'RadLim.SelectedIndex = 0
        'End If
        'If cboFunction.SelectedIndex = -1 Then
        'cboFunction.SelectedIndex = 0
        'End If
        'For Each c In PanelControl14.Controls
        'c.enabled = True
        'Next
        ''addcontrol(cboFunction.SelectedItem)
        'For Each c In PanelControl6.Controls
        'c.enabled = True
        'Next
        'End If


        'GridView1.UpdateCurrentRow()
        'BtnUpdate.Enabled = True
        ''Đặt lại focused Row
        'For i As Int16 = 0 To GridView1.RowCount - 1
        'If GridView1.GetRowCellValue(i, "IndName") = txtInd.Text Then
        'GridView1.FocusedRowHandle = i
        'End If
        'Next


        '===Apply for Tab2
        RadDGTN.Enabled = BarCheckItemComposite.Checked
        'RadDGTN.
        '===Apply for Tab5
        LabelControl21.Enabled = BarCheckItemComposite.Checked
        LabelControl22.Enabled = BarCheckItemComposite.Checked
        LabelControl24.Enabled = BarCheckItemComposite.Checked
        LabelControl25.Enabled = BarCheckItemComposite.Checked
        LabelControl26.Enabled = BarCheckItemComposite.Checked
        LabelControl27.Enabled = BarCheckItemComposite.Checked
        TxtDGKTXH.Enabled = BarCheckItemComposite.Checked
        TxtDGTNMT.Enabled = BarCheckItemComposite.Checked
        TxtDGKTXH_Reclass.Enabled = BarCheckItemComposite.Checked
        TxtDGTNMT_Reclass.Enabled = BarCheckItemComposite.Checked
        BtnBrowseDGKTXH.Enabled = BarCheckItemComposite.Checked
        BtnBrowseDGTNMT.Enabled = BarCheckItemComposite.Checked
        BtnRunDGKTXH.Enabled = BarCheckItemComposite.Checked
        BtnRunDGKTXH_Reclass.Enabled = BarCheckItemComposite.Checked
        BtnRunDGTNMT.Enabled = BarCheckItemComposite.Checked
        BtnRunDGTNMT_Reclass.Enabled = BarCheckItemComposite.Checked
        ComboBoxEditSoLopDGKTXH.Enabled = BarCheckItemComposite.Checked
        ComboBoxEditSoLopDGTNMT.Enabled = BarCheckItemComposite.Checked
        BtnBrowseDGKTXH_Reclass.Enabled = BarCheckItemComposite.Checked
        BtnBrowseDGTNMT_Reclass.Enabled = BarCheckItemComposite.Checked

        If XtraTabControl1.SelectedTabPage Is XtraTabPage_Step5 Then
            If BarCheckItemComposite.Checked = True Then
                GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '1. Sinh thái' OR [IndGroup] = '1. Ecology'")         '("[IndGroup] <> '2. Kinh tế - Xã hội' and [IndGroup] <> '3. Môi trường' and [IndGroup] <> '2. Socio - Economic' and [IndGroup] <> '3. Environment' and [IndGroup] <> '4. Hạn chế' and [IndGroup] <> '4. Constraint'")          ''' GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] <> '4. Hạn chế' and [IndGroup] <> '4. Constraint' and [IndGroup] <> '2. Kinh tế - Xã hội' and [IndGroup] <> '2. Socio - Economic' and [IndGroup] <> '3. Môi trường' and [IndGroup] <> '3. Environment'")
            Else
                GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] <> '1. Sinh thái' AND [IndGroup] <> '1. Ecology'")         '("[IndGroup] <> '2. Kinh tế - Xã hội' and [IndGroup] <> '3. Môi trường' and [IndGroup] <> '2. Socio - Economic' and [IndGroup] <> '3. Environment' and [IndGroup] <> '4. Hạn chế' and [IndGroup] <> '4. Constraint'")          '' GridView1.Columns("IndGroup").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[IndGroup] = '4. Hạn chế' or [IndGroup] = '4. Constraint' or [IndGroup] = '2. Kinh tế - Xã hội' or [IndGroup] = '2. Socio - Economic' or [IndGroup] = '3. Môi trường' or [IndGroup] = '3. Environment'")
            End If
        End If

        'GridView2.FocusedRowHandle = -1
        'GridView2.FocusedRowHandle = 0
        'Cập nhật lại tabpage4
        'If XtraTabControl1.SelectedTabPage Is XtraTabPage_Step2 Then
        'XtraScrollableControl2.Controls.Clear()
        'XtraTabControl1.SelectedTabPage = XtraTabPage1
        'XtraTabPage_Step2.PageEnabled = True
        'XtraTabPage3.PageEnabled = True
        'XtraTabControl1.SelectedTabPage = XtraTabPage_Step2
        'End If
        Dim b = Gridview2_Connect.Update_CSDL(GridControl2, "select * from [" + ListBoxControl1.Tag.ToString + "]")
    End Sub

    Private Sub BarButtonItem9_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarButtonItem9.ItemClick
        Frm_Validation.ShowDialog()
    End Sub



    Private Sub Languageselect_ItemClick(sender As Object, e As ItemClickEventArgs) Handles Languageselect.ItemClick
        Frm_LangSelect.ShowDialog()

    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Frm_modan.ShowDialog()
    End Sub

    Private Declare Function WinExec Lib "kernel32" (ByVal lpCmdLine As String, ByVal nCmdShow As Long) As Long
    Private Sub BarButtonItem1_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Dim thepath As String
        If Frm_Startup.myLanguage = "English" Then
            thepath = Application.StartupPath + "\UserGuide\UserGuide.pdf"
        Else
            thepath = Application.StartupPath + "\UserGuide\HDSD.pdf"
        End If
        Dim Foxitpath As String = Application.StartupPath
        Dim command As String = """" + Foxitpath + "\UserGuide\FoxitReader.exe" + """" + " " + """" + thepath + """"

        WinExec(command, 8)
    End Sub

    Public Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If Frm_Sentitive_analysisISopened = 0 Then
            Frm_Sentitive_analysisISopened = 1

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = True
            Me.Hide()
            Frm_Sentitive_analysis.ShowDialog()
            Me.Show()
        Else
            Me.Hide()
            Frm_Sentitive_analysis.ShowDialog()
            Me.Show()
        End If
    End Sub

    Private Sub BarButtonMoMau_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarButtonMoMau.ItemClick
        Dim f As New OpenFileDialog
        f.Filter = projectDGTN
        f.Title = OpenProject
        f.InitialDirectory = Application.StartupPath + "\SampleProject"

        Dim FullFileName As String = ""
        If f.ShowDialog() = DialogResult.OK Then
            FullFileName = f.FileName
            If FullFileName.Length > 63 Then
                MnuFileHientai.Text = "..." + FullFileName.Substring(FullFileName.Length - 35, 35)
                GroupControl2.Text = FullFileName.Substring(0, 10) + "..." + FullFileName.Substring(FullFileName.Length - 53, 53)
            Else
                GroupControl2.Text = FullFileName
                MnuFileHientai.Text = FullFileName
            End If
            Dim sWriter As StreamWriter = New StreamWriter(Application.StartupPath + "/myFile.dll", False, System.Text.Encoding.UTF8)   'myFile.dll là file chứa đường dẫn tới dataFile tại dòng 1

            sWriter.WriteLine(FullFileName)
            sWriter.Flush()
            sWriter.Close()

            Gridview1_Reload()
            Dim listbox1Qry As String = "select distinct Obj from Maindata"
            Gridview1_Connect = New myADOclass
            Dim dt As DataTable = Gridview1_Connect.DtFromQry(listbox1Qry)
            ListBoxControl1.Items.Clear()
            For i = 0 To dt.Rows.Count - 1
                ListBoxControl1.Items.Add(dt.Rows(i)("Obj"))
            Next

            ListBoxControl1.Items.Add(Taocaymoi)

        End If
    End Sub
    Private Sub checkEncs_step2()
        Dim KTcount As Int16 = 0
        Dim MTcount As Int16 = 0
        Dim KTfile As String = ""
        Dim MTfile As String = ""
        For i = 0 To GridView1.RowCount - 1
            Try
                If GridView1.GetRowCellValue(i, "IndGroup").ToString = "2. Kinh tế - Xã hội" Or GridView1.GetRowCellValue(i, "IndGroup") = "2. Socio - Economic" Then
                    'If GridView1.GetRowCellValue(i, "Limit") = "Không giới hạn" Or GridView1.GetRowCellValue(i, "Limit") = "Non-Limit" Then
                    KTcount = KTcount + 1
                    'End If
                    KTfile = GridView1.GetRowCellValue(i, "Mapchuanhoa").ToString
                End If
                If GridView1.GetRowCellValue(i, "IndGroup").ToString = "3. Môi trường" Or GridView1.GetRowCellValue(i, "IndGroup") = "3. Environment" Then
                    'If GridView1.GetRowCellValue(i, "Limit") = "Không giới hạn" Or GridView1.GetRowCellValue(i, "Limit") = "Non-Limit" Then
                    MTcount = MTcount + 1
                    MTfile = GridView1.GetRowCellValue(i, "Mapchuanhoa").ToString
                    'End If

                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try

        Next
        'End Sub
        'Private Sub checkEncs_step3()

        '===========================Xử lý tabpage 4
        LabelControl21.Enabled = True
        LabelControl22.Enabled = True
        LabelControl24.Enabled = True
        LabelControl25.Enabled = True
        LabelControl26.Enabled = True
        LabelControl27.Enabled = True
        TxtDGKTXH.Enabled = True
        BtnBrowseDGKTXH.Enabled = TxtDGKTXH.Enabled
        BtnRunDGKTXH.Enabled = TxtDGKTXH.Enabled
        TxtDGKTXH_Reclass.Enabled = TxtDGKTXH.Enabled
        BtnBrowseDGKTXH_Reclass.Enabled = TxtDGKTXH.Enabled
        ComboBoxEditSoLopDGKTXH.Enabled = TxtDGKTXH.Enabled
        BtnRunDGKTXH_Reclass.Enabled = TxtDGKTXH.Enabled
        TxtDGTNMT.Enabled = True
        BtnBrowseDGTNMT.Enabled = TxtDGTNMT.Enabled
        BtnRunDGTNMT.Enabled = TxtDGTNMT.Enabled
        TxtDGTNMT_Reclass.Enabled = TxtDGTNMT.Enabled
        BtnBrowseDGTNMT_Reclass.Enabled = TxtDGTNMT.Enabled
        ComboBoxEditSoLopDGTNMT.Enabled = TxtDGTNMT.Enabled
        BtnRunDGTNMT_Reclass.Enabled = TxtDGTNMT.Enabled
        Try
            TxtDGTNST.Text = GridView2.GetRowCellValue(0, "SrcTif").ToString  'Sửa 19/08/07
            TxtDGTNST_Reclass.Text = GridView2.GetRowCellValue(0, "RecTif").ToString 'Sửa 19/08/07
        Catch ex As Exception

        End Try


        If BarCheckItemComposite.Checked = False Then           'Trường hợp không check encS
            Panel1.Visible = True
            If KTcount > 1 Or MTcount > 1 Then
                MessageBox.Show(Enc_Check, thongbao, MessageBoxButtons.OK)
                XtraTabControl1.SelectedTabPage = XtraTabPage_Step1
                BarCheckItemComposite.Enabled = True
                Return
            End If
            TxtDGKTXH_Reclass.Text = KTfile
            If TxtDGKTXH_Reclass.Text <> "" Then
                GridView2.SetRowCellValue(1, "RecTif", TxtDGKTXH_Reclass.Text) 'Sửa 19/08/07
            End If
            TxtDGTNMT_Reclass.Text = MTfile
            If TxtDGTNMT_Reclass.Text <> "" Then
                GridView2.SetRowCellValue(2, "RecTif", TxtDGTNMT_Reclass.Text)  'Sửa 19/08/07
            End If
            LabelControl21.Enabled = False
            LabelControl22.Enabled = False
            LabelControl24.Enabled = False
            LabelControl25.Enabled = False
            LabelControl26.Enabled = False
            LabelControl27.Enabled = False
            TxtDGKTXH.Enabled = False
            BtnBrowseDGKTXH.Enabled = TxtDGKTXH.Enabled
            BtnRunDGKTXH.Enabled = TxtDGKTXH.Enabled
            TxtDGKTXH_Reclass.Enabled = TxtDGKTXH.Enabled
            BtnBrowseDGKTXH_Reclass.Enabled = TxtDGKTXH.Enabled
            ComboBoxEditSoLopDGKTXH.Enabled = TxtDGKTXH.Enabled
            BtnRunDGKTXH_Reclass.Enabled = TxtDGKTXH.Enabled
            TxtDGTNMT.Enabled = False
            BtnBrowseDGTNMT.Enabled = TxtDGTNMT.Enabled
            BtnRunDGTNMT.Enabled = TxtDGTNMT.Enabled
            TxtDGTNMT_Reclass.Enabled = TxtDGTNMT.Enabled
            BtnBrowseDGTNMT_Reclass.Enabled = TxtDGTNMT.Enabled
            ComboBoxEditSoLopDGTNMT.Enabled = TxtDGTNMT.Enabled
            BtnRunDGTNMT_Reclass.Enabled = TxtDGTNMT.Enabled
        Else        'BarCheckItemComposite.Checked = true   'Trường hợp CHECKED encS
            Panel1.Visible = False
            If KTcount < 2 And MTcount < 2 Then     '
                MessageBox.Show(Enc_KoCheck, thongbao, MessageBoxButtons.OK)
                XtraTabControl1.SelectedTabPage = XtraTabPage_Step1
                XtraTabControl1.Height = 145
                BarCheckItemComposite.Enabled = True
                Return
            End If


            '====Enable / Disable txtbox DGTNMT và DGKTXH (và các txt, label đi kèm) khi Chkbox Encs được chọn và chỉ có 1 chỉ tiêu MT hoặc 1 chỉ tiêu KT được chọn 
            If KTcount <= 1 And MTcount > 1 Then
                'TxtDGKTXH.Enabled = False
                TxtDGKTXH_Reclass.Text = KTfile
                If TxtDGKTXH_Reclass.Text <> "" Then
                    GridView2.SetRowCellValue(1, "RecTif", TxtDGKTXH_Reclass.Text) 'Sửa 19/08/07
                End If

                LabelControl21.Enabled = False
                LabelControl24.Enabled = False
                LabelControl25.Enabled = False

                TxtDGKTXH.Enabled = False
                BtnBrowseDGKTXH.Enabled = TxtDGKTXH.Enabled
                BtnRunDGKTXH.Enabled = TxtDGKTXH.Enabled
                TxtDGKTXH_Reclass.Enabled = TxtDGKTXH.Enabled
                BtnBrowseDGKTXH_Reclass.Enabled = TxtDGKTXH.Enabled
                ComboBoxEditSoLopDGKTXH.Enabled = TxtDGKTXH.Enabled
                BtnRunDGKTXH_Reclass.Enabled = TxtDGKTXH.Enabled
                TxtDGKTXH.Text = ""

            ElseIf KTcount > 1 And MTcount <= 1 Then

                TxtDGTNMT_Reclass.Text = MTfile
                If TxtDGTNMT_Reclass.Text <> "" Then
                    GridView2.SetRowCellValue(2, "RecTif", TxtDGTNMT_Reclass.Text)  'Sửa 19/08/07
                End If

                LabelControl22.Enabled = False
                LabelControl26.Enabled = False
                LabelControl27.Enabled = False
                TxtDGTNMT.Enabled = False
                BtnBrowseDGTNMT.Enabled = TxtDGTNMT.Enabled
                BtnRunDGTNMT.Enabled = TxtDGTNMT.Enabled
                TxtDGTNMT_Reclass.Enabled = TxtDGTNMT.Enabled
                BtnBrowseDGTNMT_Reclass.Enabled = TxtDGTNMT.Enabled
                ComboBoxEditSoLopDGTNMT.Enabled = TxtDGTNMT.Enabled
                BtnRunDGTNMT_Reclass.Enabled = TxtDGTNMT.Enabled

                TxtDGTNMT.Text = ""
            End If

        End If
    End Sub

    Private Sub TxtDGTNMT_EditValueChanged(sender As Object, e As EventArgs) Handles TxtDGTNMT.EditValueChanged

    End Sub

    Private Sub BarButtonItem10_ItemClick(sender As Object, e As ItemClickEventArgs) Handles BarButtonItem10.ItemClick
        'MaptoolWSC1.ShowDialog()
        Dim inputRaster As DotSpatial.Data.IRaster
        Try
            inputRaster = DotSpatial.Data.Raster.Open("D:\MyDocuments\Visual Studio 2013\Projects\Nafosted2013\GioiThieuISPONRE\QTRI_DATA\Dem_QTri.tif")
        Catch ex As Exception
            'If inputRaster Is Nothing Then
            MessageBox.Show("Không mở được dữ liệu", "Lỗi dữ liệu")
            Exit Sub
            'End If
        End Try
        Dim outputRaster As DotSpatial.Data.IRaster
        Dim mymagic As New myRasterMagic
        'ProgressBarControl2.EditValue = 0.1
        outputRaster = mymagic.RevertRaster(inputRaster, "D:\MyDocuments\Visual Studio 2013\Projects\Nafosted2013\GioiThieuISPONRE\QTRI_DATA\ab1.tif")
    End Sub

    Private Sub GridControl1_DoubleClick(sender As Object, e As EventArgs) Handles GridControl1.DoubleClick
        GridControl1.Enabled = True
    End Sub
End Class