Imports DevExpress.XtraCharts
Imports DevExpress.XtraEditors
Imports DevExpress.XtraBars.Helpers
Imports System.Linq
Imports System.Threading
Imports System.Text
Imports System.IO
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraBars
Imports DevExpress.XtraTreeList
Imports System.Threading.Tasks
Imports DevExpress.XtraSplashScreen


Public Class FrmInput
    Dim Form_Loaded As Boolean = False
    'Dim myChart As ChartControl = New ChartControl


    Dim MyADOClass As myADOclass
    Dim BottomPanel As Boolean '=> Sẽ hiện luôn panel lúc mở giao diện 'Tuy nhiên giá trị này =0 thì ẩn, =1 thì hiện; Ban đầu đặt =0 (ẩn) thì khi khởi tạo menuChecked nó sẽ checked và chạy method anhienPanel nên nó sẽ thay đổi giá trị =1, và giao diện sẽ hiển thị leftPanel
    Dim LeftPanel As Boolean 'Tượng tự như trên 
    Const quote As String = """"
    ' Dim Save1Flag As Boolean = False
    ' Dim saveboolean As Boolean = False   '=False thì cmbRegion_SelectedIndexChanged và cmbmYear sẽ ko kích hoạt lệnh save, True thì cmbIndexChanged là hỏi có save hay ko; khi tbxVal được keyUp thi sẽ = True; đặt = False sau khi save trong btnSaveGrV1_Click
    'Lại dùng saveBolean hay hơn; Không dùng savebolean nữa mà dùng dt.GetChanges().Rows.Count <> 0 để xác định GridView1 có thay đổihay ko
    Friend Shared WithEvents ToolTip1 As System.Windows.Forms.ToolTip = New Windows.Forms.ToolTip 'Khai bao de su dung ToolTip cho Label
    Dim AllowTextboxvalue As New AllowTextboxValue
    Dim mytrisview As New TrisTreeViewFromDataTable()
    Public Sub New()
        DevExpress.UserSkins.BonusSkins.Register()
        DevExpress.Skins.SkinManager.EnableFormSkins()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        DevExpress.UserSkins.BonusSkins.Register()  'Nhớ add Devexpress.bonusskins to references
        'SkinHelper.InitSkinGallery(BarEditItem1.Properties.Gallery.GalleryControl)
        'SkinHelper.InitSkinGallery(GalleryControl1, True)

        SkinHelper.InitSkinPopupMenu(MnuThemes)
        Application.EnableVisualStyles()
        DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "Office 2010 Blue"




        ''dataGridView1.DataSource = TrisTreeListFromDT.MotCap(TreeList1, "", "RegionGrp", "Region", "")
        ''dataGridView1.DataSource = mytrisview.HaiCap(TriStateTreeView1.Nodes, "Cap2", "RegionGrp", "Region", "Region")
        ''dataGridView1.DataSource = mytrisview.BaCap(TriStateTreeView1.Nodes, "Cap3", "Theme", "SubTheme", "IndicatorTN", "Ind")
        '' dataGridView1.DataSource = mytrisview.BonCap(TriStateTreeView1.Nodes, "Cap3", "Theme", "SubTheme", "Desc", "IndicatorTN", "Ind")
        ''dataGridView1.DataSource = mytrisview.MuoiCap(TriStateTreeView1.Nodes, "Cap3", "Theme", "SubTheme", "Desc", "IndicatorTN", "IndicatorTN", "IndicatorTN", "IndicatorTN", "Unit", " TenTuSo, TenMauSo", "MyNote", "Ind", "ID")
        ''dataGridView1.DataSource = mytrisview.BaCap(TriStateTreeView1.Nodes, "", "Theme", "SubTheme", "IndicatorTN", "Ind", "ID")
        ''For Each item In CklUrban.CheckedItems '= 0 To CheckedListBox_Urban.ItemCount - 1
        ''    'Qry = Qry + "Urban = " + CheckedListBox_Urban.CheckedItems.Item(i).ToString + " Or "
        ''    QryUrban = QryUrban + "Region = " + """" + item.row(1) + """" + " Or "
        ''Next
        ''Dim a = TriStateTreeView1.Nodes.Item("_Theme0_SubTheme0")
        ''mytrisview.MotCap(TriStateTreeView1.Nodes, "", "RegionGrp", "Region")
        ''Dim b = TriStateTreeView1.Nodes.Item("Indicator").
        ''dataGridView1.DataSource = mytrisview.MotCap(TriStateTreeView1.Nodes, "", "RegionGrp", "Region", "Min(ID)")
        ''dataGridView1.DataSource = mytrisview.MotCap(TriStateTreeView1.Nodes, "", "RegionGrp", "Region", "")
    End Sub

    Private Sub FrmStart_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        btnSaveGrV1_Click(Nothing, Nothing)
    End Sub
    Private Sub FrmInput_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try 'Dùng Try Catch để tránh trường hợp sử dụng file dữ liệu rỗng
            'AddHandler myChart.MouseMove, AddressOf myChart_MouseMove
            'AddHandler myChart.KeyDown, AddressOf myChart_Keydown
            Dim IO_Reader_Writer As New IO_Reader_Writer
            Dim Curdata As String = IO_Reader_Writer.doc1dong(Application.StartupPath + "\myFile.dll", 1)
            If Curdata = "" Or System.IO.File.Exists(Curdata) = False Then  'Nếu nội dung chưa có đường dẫn hoặc file không tồn tại thì quay lại mở file
                mnuChonfile_Click(Nothing, Nothing)
            End If
            MnuFileHientai.Hint = Curdata

            'MnuFileHientai.Text = Curdata.Substring(0, 25) + "..."
            If Curdata.Length > 25 Then
                MnuFileHientai.Caption = Curdata.Substring(0, 25) + "..."
            Else
                MnuFileHientai.Caption = Curdata
            End If
            MyADOClass = New myADOclass 'Load lai du lieu moi trong truong hop MnuChonDulieu.Click




            ''Bỏ nút Close của FrmInput
            'Dim disableclosefrm As New ClssDisableCloseForm
            'disableclosefrm.DisableCloseButton(Me.Handle)

            'Hai lệnh này để đặt Format cho number theo kiểu En-US: Tức là dấu thập phân là dấu "."
            My.Application.ChangeCulture("en-US")
            My.Application.Culture.NumberFormat = System.Globalization.NumberFormatInfo.CurrentInfo
            BoTabStop() 'Bo hết tabstop cho mọi control trên form


            '===An hoặc hiện Left và Bottom panel
            If MnuBangDuLieu.Checked = True Then
                BottomPanel = 0
                AnHienBottomPanel()
            Else
                BottomPanel = 1
                AnHienBottomPanel()
            End If
            If MnuThongTin.Checked = True Then
                LeftPanel = 0
                AnHienLeftPanel()
            Else
                LeftPanel = 1
                AnHienLeftPanel()
            End If
        Catch
        End Try

        LoadMainForm()

        cmbChartViewType.SelectedItem = "Dạng hoa gió 1"


        '  SkinHelper.InitSkinPopupMenu(BarEditItem1)

        XtraTabControl2.SelectedTabPageIndex = 0
        'XtraTabControl2.TabPages.Remove(Me.TabPage1)
        '==Thay đổi kích thước form 
        '' Đặt Form vào giữa màn hình
        Dim scr As Screen = Screen.PrimaryScreen 'đi lấy màn hình chính
        Me.Left = (scr.WorkingArea.Width - Me.Width) / 2
        Me.Top = 0      '(scr.WorkingArea.Height - Me.Height) / 2

        'Kéo dài Form = chiều cao màn hình, chiều rộng đặt = 1024

        Me.Size = New System.Drawing.Size(1024, scr.WorkingArea.Height)
        BarManager1.ShowScreenTipsInToolbars = True '===Show tooltip cho ToolBarItem
        BarManager1.ShowScreenTipsInMenus = True '===Show tooltip cho SubMenuItems
        'Me.WindowState = FormWindowState.Minimized
        'FrmWaiting.Show()
        'FrmWaiting.ShowDialog()
        'System.Threading.Thread.Sleep(2000)
        Form_Loaded = True
        'Load luôn các txtbox khi khởi động (Nhớ để sau lệnh Form_Loaded = True)
        BtnOK_Click(Nothing, Nothing)
        CmbmYear.SelectedIndex = CmbmYear.Items.Count - 1


        CheckedListBox_Year.CheckOnClick = True
        CheckedListBox_Ind.CheckOnClick = True
        CheckedListBox_Urban.CheckOnClick = True
        ' CkbInd_Sort.Checked = True
    End Sub

    Public Sub LoadMainForm()

        '==========================================================
        ''==============="Khởi tạo các giá trị cho XtraXtraTabControl2" - ~ chọn Tabpage1 - Thông tin
        '==========================================================
        'MyADOClass.Load_data_Grid(GridView1, "Select * from main")
        'AddChart(TabPage2, CheckedListBox_Ind.CheckedItems.Item(0).row(0).ToString)

        Try
            '===Ini Ckl_Indicator
            'Dim Table_Indicator As DataTable = MyADOClass.DtFromQry("Select ID, Theme, Ind, IndicatorTN,mynote from zLuuInd order by IndicatorTN")
            Dim Table_Indicator As DataTable = MyADOClass.DtFromQry("Select ID, Pilar, Theme, Ind,IndicatorTN, Desc, mynote From " + IndTab + " where IndicatorTN <>'' order by ID")

            CheckedListBox_Ind.DataSource = Nothing
            CheckedListBox_Ind.DataSource = Table_Indicator
            CheckedListBox_Ind.DisplayMember = "IndicatorTN"
            '===Ini Clb_Year
            Dim Table_Year As DataTable = MyADOClass.DtFromQry("Select myear From " + mYearTab + "  order by mYear")
            CheckedListBox_Year.DataSource = Nothing
            CheckedListBox_Year.DataSource = Table_Year
            CheckedListBox_Year.DisplayMember = "myear"
            '===Ini Clb_Urban
            Dim Table_Urban As DataTable = MyADOClass.DtFromQry("Select RegionGrp, Region From " + RegionTab + "  order by ID")
            CheckedListBox_Urban.DataSource = Table_Urban
            CheckedListBox_Urban.DisplayMember = "Region"

            '===ini Ckb_CheckAll            '===Phải chuyển CheckState.Indeterminate trước để kích hoạt checkChange Event
            CkbUrb_ChkAll.CheckState = CheckState.Indeterminate
            CkbInd_ChkAll.CheckState = CheckState.Indeterminate
            CkbYear_ChkAll.CheckState = CheckState.Indeterminate
            CkbUrb_ChkAll.CheckState = CheckState.Checked
            CkbInd_ChkAll.CheckState = CheckState.Checked
            CkbYear_ChkAll.CheckState = CheckState.Checked
            '============================
            '====Fill GridView1
            '====Fill CmbRegion và CmbmYear
            '============================
            'Dim myQuery As String = "Select * From " + DataQry + " "    ' where Ind = '1.1. Tổng dân số' and Xap like 'p%'"
            'Dim myQuery As String = "Select mYear, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, Desc,IndicatorTN,DataVal,Unit,DataRef,myNote,ID,ID from  DataQry Order by ID, mYear, Region"
            'Dim myQuery As String = "Select ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, Desc,IndicatorTN,DataVal,Unit,DataRef,myNote from " + "DataQry" + " Order by mYear, Region"
            Dim myQuery As String = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN, Desc,TuSo, MauSo, DataVal, ChuanHoa, TarMin, TarMax,Unit,DataRef,  myNote from " + DataQryTab + " Order by mYear, Region"
            'MyADOClass.KetNoi_Open()
            Dim DataQryTable = MyADOClass.DtFromQry(myQuery)         'Du lieu la bagn DataQry cua CSDL
            GridControl1.DataSource = DataQryTable           'MyADOClass.Load_data_Grid(GridControl1, DataQryTable)
            formatdgv(GridView1)
            GridControl2.DataSource = DataQryTable
            formatdgv(GridView2)
            'Dim Gridview2Qry As String = QryfromLeftPanel()
            'GridControl2.DataSource = MyADOClass.DtFromQry(Gridview2Qry)
            'GridView1.Columns("DataID").Visible = True
            'GridView1.Columns("ID").Caption = "ID"
            'GridView1.Columns("ID").Width = 35
            'MyADOClass.KetNoi_Close()
            'Không cho Select 1 cell mà bắt select cả Row
            'GridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            '==
            ' BtnSaveGrV1.Enabled = False   'Để không hỏi có save hay ko
            ' BtnSaveGrV2.Enabled = False 
            Dim distinctdataview As DataView = New DataView(GridControl1.DataSource)   'New DataView(DataQryTable)
            ' Dim DisctinctColumn As String() = New String() {"Region","mYear"}    'Dùng String() nếu muốn Distinct theo nhiều cột
            '==

            '            Dim DisctinRegionTable As DataTable = distinctdataview.ToTable(True, "Region")
            Dim DisctinRegionTable As DataTable = distinctdataview.ToTable(True, "Region")
            cmbRegion.DataSource = DisctinRegionTable   '.Columns("Region")
            cmbRegion.DisplayMember = "Region"
            cmbRegion.SelectedItem = 0
            'cmbRegion_SelectedIndexChanged(Nothing, Nothing)
            '==
            Dim DistincmYearTable As DataTable = distinctdataview.ToTable(True, "mYear")
            CmbmYear.DataSource = DistincmYearTable '.Columns("mYear")
            CmbmYear.SelectedItem = 0
            CmbmYear.DisplayMember = "mYear"

            '' '' ''==========================================================
            ' '' '' ''==============="Khởi tạo các giá trị cho Panel Chart"  ~ chọn Tabpage2 - Đồ thị
            '' '' ''==========================================================
            '' '' ''===Ini Clb_Year1
            ' '' ''CheckedListBox_Year.DataSource = Table_Year
            ' '' ''CheckedListBox_Year.DisplayMember = "mYear"
            '' '' ''===Ini Clb_Urban1
            ' '' ''CheckedListBox_Urban.DataSource = Table_Urban
            ' '' ''CheckedListBox_Urban.DisplayMember = "Region"
            '' '' ''===ini Ckb_CheckAll
            ' '' ''CkbUrb_ChkAll1.CheckState = CheckState.Checked
            ' '' ''CkbYear_ChkAll1.CheckState = CheckState.Checked
            '' '' ''Table_Indicator1 là Table ko chứa các giá trị Dataval = String, tức là thuộc nhóm 2, 3,4 
            ' '' ''Dim Table_Indicator1 As DataTable = MyADOClass.DtFromQry("Select ID, Theme, Ind, IndicatorTN,mynote  From " + IndTable + "  where mynote = " + quote + "Nhóm 2: Giá trị phần trăm " + quote + " Or mynote = " + quote + "Nhóm 3: Giá trị số thực" + quote + " Or mynote = " + quote + "Nhóm 1: Giá trị số nguyên" + quote + " Order by ID ")
            '' '' ''===Ini Cmb_Indicator1
            ' '' ''cmb_Ind.DataSource = Table_Indicator1
            ' '' ''cmb_Ind.DisplayMember = "IndicatorTN"
            ' '' ''cmb_Ind.SelectedIndex = 0
            '============================
            '====Fill GridView2 'Khi FormLoad thi se tu dong kich hoat cmb_Ind_SelectedIndexChanged
            '=======================va event tren se lam nhiem vu load data vao GridView2, ne ta co the bo doan code duoi di
            'Dim Dgv2ind As String = cmb_Ind.Items(0)("Ind").ToString    ' cmb_Ind.SelectedItem.ToString
            'Dim Dgv2Query As String = "Select ID, mYear, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, Desc,IndicatorTN,DataVal,Unit,DataRef,myNote from  DataQry Where Ind = " + quote + Dgv2ind + quote + " Order by mYear, Region"
            ''MyADOClass.KetNoi_Open()
            'Dim Dgv2QueryTable As DataTable = MyADOClass.TableFromQry(Dgv2Query)      'Du lieu la bagn DataQry cua CSDL
            'MyADOClass.Load_data_Grid(GridView2, Dgv2QueryTable)

            'dataGridView3.Columns("ID").Visible = False


            ''MyADOClass.KetNoi_Close()
            'Không cho Select 1 cell mà bắt select cả Row
            'GridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            'GridView2.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
            'GridView2.Columns("ID").Visible = True

            'GridView2.Columns("ID").HeaderText = "ID"
            'GridView2.Columns("ID").Width = 25
            'btnSaveGrV1.BackgroundImage = System.Drawing.Image.FromFile(Application.StartupPath + "\ico\Save.png")
            'btnSaveGrV1.BackgroundImageLayout = ImageLayout.Stretch
            'btnSaveGrV2.BackgroundImage = System.Drawing.Image.FromFile(Application.StartupPath + "\ico\Save.png")
            'btnSaveGrV2.BackgroundImageLayout = ImageLayout.Stretch

            ' FrmWaiting.Close()
            'Me.WindowState = FormWindowState.Maximized
            'fullScreen()

            'myChart.Height = myChart.Height - 2
            ''Dim myDiagram As XYDiagram = CType(myChart.Diagram, XYDiagram)
            ''myDiagram.EnableAxisXZooming = True
            ''myDiagram.EnableAxisYZooming = True
            'myDiagram.ZoomingOptions = True
            'BtnSave1.Location = New System.Drawing.Point(3, myChart.Height - 35)
            'lblLabel3.Location = New System.Drawing.Point(38, myChart.Height - 20)
            'cmbViewType.Location = New System.Drawing.Point(185, myChart.Height - 27)
        Catch   'Dùng Try Catch để tránh trường hợp sử dụng file dữ liệu rỗng

        End Try
        XtraScrollable_Thongtin.Controls.Clear()
        CheckedListBox_Urban.CheckAll()
        CheckedListBox_Ind.CheckAll()
        CheckedListBox_Year.CheckAll()
        CmbSoSanh.SelectedItem = "Đồ thị theo một Năm"


        '==TreeList
        '==TreeList
        '==TreeList
        '==TreeList
        'Dim TrisTreeListFromDT As TrisTreeListFromDataTable = New TrisTreeListFromDataTable
        'dataGridView1.DataSource = TrisTreeListFromDT.BaCap(TreeList1, "", "Theme", "SubTheme", "IndicatorTN", "Tinh_Ind", "ID")
        'TreeList1.ExpandAll()
        'TreeList1.OptionsView.ShowCheckBoxes = True
        'TreeList1.OptionsBehavior.Editable = False
        'TreeList1.OptionsBehavior.AllowIndeterminateCheckState = True
        'TreeList1.OptionsBehavior.AllowRecursiveNodeChecking = True
        'Dim TreeListColumn1 = New DevExpress.XtraTreeList.Columns.TreeListColumn()
        'TreeList1.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {TreeListColumn1})
        ''TreeListColumn1.Caption = "TreeListColumn1"
        ''TreeListColumn1.FieldName = "TreeListColumn1"
        ''TreeListColumn1.Name = "TreeListColumn1"
        ''TreeListColumn1.Visible = True
        ''TreeListColumn1.VisibleIndex = 0
    End Sub
    ''Private Sub formatdgv(ByVal adatagridview As DevExpress.XtraGrid.Views.Grid.GridView)
    ''    '================Dòng chẵn 1 màu, dòng lẻ 1 màu
    ''    adatagridview.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque '.FloralWhite
    ''    'adatagridview.RowsDefaultCellStyle.BackColor = Color.Bisque
    ''    '=======Format ColumHeader      'Đối tượng là colum của dataTable đừng nhầm với của DataGridview
    ''    'Đặt tên Header
    ''    adatagridview.Columns("ID").Visible = False
    ''    adatagridview.Columns(" TenTuSo, TenMauSo").Visible = False
    ''    adatagridview.Columns("Desc").Visible = False
    ''    adatagridview.Columns("mYear").Caption = "Năm"
    ''    adatagridview.Columns("mYear").Width = 50
    ''    adatagridview.Columns("Region").Caption = "Đô thị"
    ''    adatagridview.Columns("Theme").Caption = "Chủ đề"
    ''    adatagridview.Columns("Ind").Caption = "Chỉ số"
    ''    adatagridview.Columns("Ind").Width = 75
    ''    adatagridview.Columns("IndicatorTN").Caption = "Tên chỉ số"
    ''    adatagridview.Columns("IndicatorTN").Width = 160
    ''    adatagridview.Columns("DataVal").Caption = "Giá trị"
    ''    adatagridview.Columns("Unit").Caption = "Đơn vị"
    ''    adatagridview.Columns("Unit").Width = 80
    ''    adatagridview.Columns("DataRef").Caption = "Nguồn dữ liệu"
    ''    adatagridview.Columns("DataRef").Width = 200
    ''    adatagridview.Columns("myNote").Caption = "Nhóm chỉ số"
    ''    adatagridview.Columns("myNote").Width = 130
    ''    'adatagridview.Columns("ValDlb").Visible = False
    ''    'adatagridview.Columns("RegionGrp").Caption = "Nhóm đô thị"
    ''    'adatagridview.Columns("RegionGrp").Width = 130

    ''    adatagridview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
    ''    adatagridview.Font = New Font("Times New Roman", 12, FontStyle.Regular, GraphicsUnit.Point, 1)
    ''    '======Fomat column GIA Tri
    ''    Dim DataValCell_Obj As DataGridViewCellStyle = New DataGridViewCellStyle()
    ''    DataValCell_Obj.Format = "#,###.00"      '"#,###.##" =>nếu muốn bỏ các dấu 00 nếu là integer
    ''    'NubmerCell_Obj.Format = "#,#.00;(#,#.00)"
    ''    'NubmerCell_Obj.Format = String.Format("g", System.Globalization.NumberFormatInfo.CurrentInfo)
    ''    DataValCell_Obj.Alignment = DataGridViewContentAlignment.MiddleRight
    ''    DataValCell_Obj.BackColor = Color.Bisque
    ''    DataValCell_Obj.ForeColor = Color.Blue

    ''    DataValCell_Obj.Font = New Font("Arial", 13.0F, GraphicsUnit.Pixel)
    ''    DataValCell_Obj.Font = New Font(GridView1.Font, FontStyle.Bold)
    ''    adatagridview.Columns("DataVal").DefaultCellStyle = DataValCell_Obj

    ''    'adatagridview.Columns("DataRef").DefaultCellStyle = NumberCell_Obj
    ''    adatagridview.Columns("DataRef").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
    ''    adatagridview.Columns("DataRef").DefaultCellStyle.Font = New Font(GridView1.Font, FontStyle.Regular)
    ''    'adatagridview.Columns("DataRef").DefaultCellStyle.BackColor = Color.Bisque
    ''    adatagridview.Columns("DataRef").DefaultCellStyle.ForeColor = Color.Blue
    ''    'adatagridview.Columns("Unit").DefaultCellStyle = NumberCell_Obj

    ''End Sub
    Public Sub formatdgv(ByVal XtraGrdView As DevExpress.XtraGrid.Views.Grid.GridView)
        XtraGrdView.OptionsBehavior.EditorShowMode = EditorShowMode.Click   'Để có thể fire event Gridview1.RowCellClick http://www.devexpress.com/Support/Center/Question/Details/Q406257  
        XtraGrdView.OptionsMenu.EnableColumnMenu = False    'Tắt chức năng chuột phải lên Column Header
        XtraGrdView.PopulateColumns()
        ' Dim col As DataColumn
        For Each col In XtraGrdView.Columns     'disable editing for all columns
            col.OptionsColumn.AllowEdit = False
            XtraGrdView.Columns("DataVal").OptionsColumn.AllowEdit = True     'enable editing for specific column
            XtraGrdView.Columns("DataRef").OptionsColumn.AllowEdit = True
            XtraGrdView.Columns("TarMax").OptionsColumn.AllowEdit = True     'enable editing for specific column
            XtraGrdView.Columns("TarMin").OptionsColumn.AllowEdit = True
        Next
        XtraGrdView.Columns.ColumnByFieldName("ID").Visible = False
        XtraGrdView.Columns.ColumnByFieldName("DataID").Visible = False
        XtraGrdView.Columns.ColumnByFieldName("RegionGrp").Visible = False
        XtraGrdView.Columns.ColumnByFieldName("Ind").Visible = False
        XtraGrdView.Columns.ColumnByFieldName("TenTuSo").Visible = False
        XtraGrdView.Columns.ColumnByFieldName("TenMauSo").Visible = False
        XtraGrdView.Columns.ColumnByFieldName("TuSo").Visible = False
        XtraGrdView.Columns.ColumnByFieldName("MauSo").Visible = False
        XtraGrdView.Columns.ColumnByFieldName("SoNhan").Visible = False
        XtraGrdView.Columns.ColumnByFieldName("Desc").Visible = False
        XtraGrdView.Columns.ColumnByFieldName("myNote").Visible = False
        XtraGrdView.Columns.ColumnByFieldName("mYear").Caption = "Năm"
        XtraGrdView.Columns.ColumnByFieldName("mYear").Width = 40
        XtraGrdView.Columns.ColumnByFieldName("Region").Caption = "Địa phương"
        XtraGrdView.Columns.ColumnByFieldName("Theme").Caption = "Chủ đề"
        XtraGrdView.Columns.ColumnByFieldName("Theme").Width = 150
        XtraGrdView.Columns.ColumnByFieldName("DataVal").Caption = "G.trị thực"
        XtraGrdView.Columns.ColumnByFieldName("DataVal").DisplayFormat.FormatType = FormatType.Numeric
        XtraGrdView.Columns.ColumnByFieldName("DataVal").DisplayFormat.FormatString = "f2"
        XtraGrdView.Columns.ColumnByFieldName("DataRef").Caption = "Nguồn dữ liệu"
        XtraGrdView.Columns.ColumnByFieldName("DataRef").Width = 270
        XtraGrdView.Columns.ColumnByFieldName("Unit").Caption = "Đơn vị"
        XtraGrdView.Columns.ColumnByFieldName("TarMin").Caption = "Cận dưới"
        XtraGrdView.Columns.ColumnByFieldName("TarMin").DisplayFormat.FormatType = FormatType.Numeric
        XtraGrdView.Columns.ColumnByFieldName("TarMin").DisplayFormat.FormatString = "f2"
        XtraGrdView.Columns.ColumnByFieldName("TarMax").Caption = "Cận trên"
        XtraGrdView.Columns.ColumnByFieldName("TarMax").DisplayFormat.FormatType = FormatType.Numeric
        XtraGrdView.Columns.ColumnByFieldName("TarMax").DisplayFormat.FormatString = "f2"
        XtraGrdView.Columns.ColumnByFieldName("ChuanHoa").Caption = "G.trị C.Hóa"
        XtraGrdView.Columns.ColumnByFieldName("ChuanHoa").DisplayFormat.FormatType = FormatType.Numeric
        XtraGrdView.Columns.ColumnByFieldName("ChuanHoa").DisplayFormat.FormatString = "f2"
        XtraGrdView.Columns.ColumnByFieldName("IndicatorTN").Caption = "Chỉ tiêu"
        XtraGrdView.Columns.ColumnByFieldName("IndicatorTN").Width = 325
        'Prevent focused cell from being highlighted
        'XtraGrdView.OptionsSelection.EnableAppearanceFocusedCell = False
        'XtraGrdView.OptionsBehavior.Editable = False
        ''Draw dotted focus rectangle around entire row
        'XtraGrdView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus

        'XtraGrdView.OptionsBehavior.AllowAddRows = DefaultBoolean.True
        'XtraGrdView.OptionsView.NewItemRowPosition = Top

        XtraGrdView.OptionsView.ColumnAutoWidth = False
        XtraGrdView.OptionsView.ShowGroupPanel = False
        XtraGrdView.OptionsView.ShowIndicator = False 'invisible header Row

        '================Dòng chẵn 1 màu, dòng lẻ 1 màu
        XtraGrdView.OptionsView.EnableAppearanceEvenRow = True
        XtraGrdView.Appearance.EvenRow.BackColor = Color.Bisque   'GridView2.Appearance.Row.BackColor = Color.Bisque
        'Dim a = XtraGrdView.Columns.ColumnByFieldName("tenPT")
        'XtraGrdView.Columns.ColumnByFieldName("Obj").Visible = False
        'XtraGrdView.Columns.ColumnByFieldName("ID").Visible = False
        'XtraGrdView.Columns.ColumnByFieldName("TT").Visible = False
        'XtraGrdView.Columns.ColumnByFieldName("TT").Width = 25
        'XtraGrdView.Columns.ColumnByFieldName("a").Width = 45
        'XtraGrdView.Columns.ColumnByFieldName("b").Width = 45
        'XtraGrdView.Columns.ColumnByFieldName("c").Width = 45
        'XtraGrdView.Columns.ColumnByFieldName("d").Width = 45
        ' ''XtraGrdView.Columns.ColumnByFieldName("Tinh").Visible = True
        ' ''XtraGrdView.Columns.ColumnByFieldName("Huyen").Visible = True
        ' ''XtraGrdView.Columns.ColumnByFieldName("Xa").Visible = True
        'XtraGrdView.Columns.ColumnByFieldName("IndGroup").Caption = "Nhóm chỉ tiêu"
        'XtraGrdView.Columns.ColumnByFieldName("IndGroup").Width = 95
        'XtraGrdView.Columns.ColumnByFieldName("IndGroup").Visible = True

        'XtraGrdView.Columns.ColumnByFieldName("IndName").Caption = "Chỉ tiêu"
        'XtraGrdView.Columns.ColumnByFieldName("IndName").Width = 260

        'XtraGrdView.Columns.ColumnByFieldName("UsedFunction").Visible = True
        'XtraGrdView.Columns.ColumnByFieldName("UsedFunction").Caption = "Hàm sử dụng"
        'XtraGrdView.Columns.ColumnByFieldName("UsedFunction").Width = 100

        ''XtraGrdView.Columns.ColumnByFieldName("NO1").Visible = True
        ' ''XtraGrdView.Columns.ColumnByFieldName("NO1").Caption = "Không t.nghi nếu >="
        ''XtraGrdView.Columns.ColumnByFieldName("NO1").Width = 50

        ''XtraGrdView.Columns.ColumnByFieldName("NO2").Visible = True
        ' ''XtraGrdView.Columns.ColumnByFieldName("NO2").Caption = "Không t.nghi nếu <="
        ''XtraGrdView.Columns.ColumnByFieldName("NO2").Width = 50

        ''XtraGrdView.Columns.ColumnByFieldName("TN1").Visible = True
        ''XtraGrdView.Columns.ColumnByFieldName("TN1").Width = 50
        ''XtraGrdView.Columns.ColumnByFieldName("TN2").Visible = True
        ''XtraGrdView.Columns.ColumnByFieldName("TN2").Width = 50
        ''XtraGrdView.Columns.ColumnByFieldName("RTN1").Visible = True
        ''XtraGrdView.Columns.ColumnByFieldName("RTN1").Width = 50
        ''XtraGrdView.Columns.ColumnByFieldName("RTN2").Visible = True
        ''XtraGrdView.Columns.ColumnByFieldName("RTN2").Width = 50

        ''XtraGrdView.Columns.ColumnByFieldName("S1").Visible = True
        ''XtraGrdView.Columns.ColumnByFieldName("S1").Caption = "TU1"
        ''XtraGrdView.Columns.ColumnByFieldName("S1").Width = 50

        ''XtraGrdView.Columns.ColumnByFieldName("S2").Visible = True
        ''XtraGrdView.Columns.ColumnByFieldName("S2").Caption = "TU2"
        ''XtraGrdView.Columns.ColumnByFieldName("S2").Width = 50

        'XtraGrdView.Columns.ColumnByFieldName("Map").Visible = True
        'XtraGrdView.Columns.ColumnByFieldName("Map").Caption = "Bản đồ chỉ tiêu"
        'XtraGrdView.Columns.ColumnByFieldName("Map").Width = 235

        'XtraGrdView.Columns.ColumnByFieldName("Mapchuanhoa").Visible = True
        'XtraGrdView.Columns.ColumnByFieldName("Mapchuanhoa").Caption = "Bản đồ chuẩn hóa"
        'XtraGrdView.Columns.ColumnByFieldName("Mapchuanhoa").Width = 235

        'XtraGrdView.Columns.ColumnByFieldName("Weight").Caption = "Trọng số"
        'XtraGrdView.Columns.ColumnByFieldName("Weight").Width = 60

        'XtraGrdView.Columns.ColumnByFieldName("Limit").Caption = "Giới hạn"
        'XtraGrdView.Columns.ColumnByFieldName("Limit").Width = 95
        ''GridView2. = New Font("arial", 10, FontStyle.Regular, GraphicsUnit.Point, 1)
        '' GridView2.RowHeadersVisible = False

        XtraGrdView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto
        XtraGrdView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto

        'GridControl1.Update()
        SetGridFont(XtraGrdView, New Font("arial", 9))
        ' PopupMenuShowing(GridControl2)
        'If (XtraGrdView.Name = "GridView2") Then
        '    XtraGrdView.Columns.ColumnByFieldName("a").Visible = False
        '    XtraGrdView.Columns.ColumnByFieldName("b").Visible = False
        '    XtraGrdView.Columns.ColumnByFieldName("c").Visible = False
        '    XtraGrdView.Columns.ColumnByFieldName("d").Visible = False
        '    XtraGrdView.Columns.ColumnByFieldName("IndGroup").Visible = False
        '    XtraGrdView.Columns.ColumnByFieldName("UsedFunction").Visible = False
        '    XtraGrdView.Columns.ColumnByFieldName("Map").Visible = False
        '    XtraGrdView.Columns.ColumnByFieldName("Mapchuanhoa").Visible = False
        'End If
    End Sub

    Sub SetGridFont(ByVal view As DevExpress.XtraGrid.Views.Grid.GridView, ByVal font As Font)
        Dim ap As AppearanceObject
        For Each ap In view.Appearance
            ap.Font = font
        Next

    End Sub
    Private Sub fullScreen()
        ' Hàm thực hiện full toàn màn hình giao diện chương trình
        ' Retrieve the working rectangle from the Screen class
        ' using the PrimaryScreen and the WorkingArea properties.
        Dim workingRectangle As System.Drawing.Rectangle = Screen.PrimaryScreen.WorkingArea
        ' Set the size of the form slightly less than size of 
        ' working rectangle.
        Me.Size = New System.Drawing.Size(workingRectangle.Width, workingRectangle.Height)
        'Me.Size = New System.Drawing.Size(1024, workingRectangle.Height)
        ' Set the location so the entire form is visible.
        Me.Location = New System.Drawing.Point(0, 0)
    End Sub

    ''Private Sub cmbViewType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ''    If (cmbViewType.SelectedItem = "1") Then
    ''        myChart.SeriesTemplate.View = New LineSeriesView()
    ''    ElseIf (cmbViewType.SelectedItem = "2") Then
    ''        myChart.SeriesTemplate.View = New StackedBarSeriesView
    ''    ElseIf (cmbViewType.SelectedItem = "3") Then
    ''        myChart.SeriesTemplate.View = New SideBySideBarSeriesView
    ''        'ElseIf (cmbViewType.SelectedItem = "4") Then
    ''        '    myChart.SeriesTemplate.View = New DoughnutSeriesView
    ''        'ElseIf (cmbViewType.SelectedItem = "5") Then
    ''        '    myChart.SeriesTemplate.View = New PieSeriesView
    ''        'ElseIf (cmbViewType.SelectedItem = "6") Then
    ''        '    myChart.SeriesTemplate.View = New SideBySideBarSeriesView
    ''        'ElseIf (cmbViewType.SelectedItem = "7") Then
    ''        '    myChart.SeriesTemplate.View = New SideBySideBarSeriesView
    ''        'ElseIf (cmbViewType.SelectedItem = "8") Then
    ''        '    myChart.SeriesTemplate.View = New SideBySideBarSeriesView
    ''        'ElseIf (cmbViewType.SelectedItem = "9") Then
    ''        '    myChart.SeriesTemplate.View = New SideBySideBarSeriesView
    ''        'ElseIf (cmbViewType.SelectedItem = "10") Then
    ''        '    myChart.SeriesTemplate.View = New SideBySideBarSeriesView
    ''    End If



    ''End Sub

    ''========SUB ẨN HIỆN Panel========
    ''Public Sub AnHienPanel2(ByVal mySplitContainer As SplitContainer)
    ''    If (BottomPanel = 0) Then       'Nếu đang ẩn thì hiện bằng cách kéo Splitcontainer3 ngắn đi 200)
    ''        BottomPanel = 1
    ''        mySplitContainer.Panel2Collapsed = False
    ''        'splitContainer1.SplitterDistance = XtraTabControl2.Height - 100
    ''        'splitContainer1.Height = XtraTabControl2.Height - 30

    ''    Else    'Lúc này Grv thuộc tính đang hiện, Phải ẩn nó đi
    ''        BottomPanel = 0
    ''        mySplitContainer.Panel2Collapsed = True
    ''        'splitContainer1.Height = XtraTabControl2.Height + 100
    ''        'splitContainer1.SplitterDistance = XtraTabControl2.Height - 30
    ''    End If
    ''End Sub
    ''Public Sub AnHienPanel1(ByVal mySplitContainer As SplitContainer)
    ''    mySplitContainer.SplitterDistance = 250
    ''    If (LeftPanel = 0) Then       'Nếu đang ẩn thì hiện
    ''        LeftPanel = 1
    ''        mySplitContainer.Panel1Collapsed = False    'Hien len
    ''        'splitContainer1.SplitterDistance = XtraTabControl2.Height - 100
    ''        'splitContainer1.Height = XtraTabControl2.Height - 30

    ''    Else    'Lúc này Grv thuộc tính đang hiện, Phải ẩn nó đi
    ''        LeftPanel = 0
    ''        mySplitContainer.Panel1Collapsed = True
    ''        'splitContainer1.Height = XtraTabControl2.Height + 100
    ''        'splitContainer1.SplitterDistance = XtraTabControl2.Height - 30
    ''    End If
    ''End Sub
    Private Sub AnHienLeftPanel()
        If (LeftPanel = 0) Then       'Nếu đang ẩn thì hiện bằng cách kéo Splitcontainer3 ngắn đi 200)
            LeftPanel = 1
            SplitContainer2.Panel1Collapsed = False
            'SplitContainer2.SplitterDistance = SplitContainer2.Width - 250
            'Button1.Location = New Point(Me.Width - 290, Button1.Location.Y)
            'splitContainer1.SplitterDistance = XtraTabControl2.Height - 100
            'splitContainer1.Height = XtraTabControl2.Height - 30

        Else    'Lúc này Grv thuộc tính đang hiện, Phải ẩn nó đi
            'Button1.Location = New Point(Me.Width - 40, Button1.Location.Y)
            LeftPanel = 0
            SplitContainer2.Panel1Collapsed = True
            'splitContainer1.Height = XtraTabControl2.Height + 100
            'splitContainer1.SplitterDistance = XtraTabControl2.Height - 30
        End If

        Me.Refresh()
    End Sub
    Private Sub AnHienBottomPanel()
        If (BottomPanel = 0) Then       'Nếu đang ẩn thì hiện bằng cách kéo Splitcontainer3 ngắn đi 200)
            BottomPanel = 1
            splitContainer1.Panel2Collapsed = False
            'splitContainer1.SplitterDistance = XtraTabControl2.Height - 100
            'splitContainer1.Height = XtraTabControl2.Height - 30

        Else    'Lúc này Grv thuộc tính đang hiện, Phải ẩn nó đi
            BottomPanel = 0
            splitContainer1.Panel2Collapsed = True
            'splitContainer1.Height = XtraTabControl2.Height + 100
            'splitContainer1.SplitterDistance = XtraTabControl2.Height - 30
        End If


        '=====Chart Width
        ChartCustomize(xtraChart)



        'XtraTabControl2.TabPages(1).AutoScroll = True
    End Sub

    Private Sub btnButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        splitContainer1.SplitterDistance = splitContainer1.Height - 150
        AnHienBottomPanel()
        Me.Refresh()
    End Sub
    ''Private Sub btnButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''    'Dim myTable As DataTable = MyADOClass.Get_table("Select Ind  From " + IndTable + " icator")
    ''    Dim myTable As DataTable = gridcontrol1.datasource
    ''    Dim TableIndicator As DataTable = MyADOClass.TableFromQry("Select *  From " + IndTable + " ")
    ''    Dim Y As Integer = 25
    ''    For r As Integer = 0 To myTable.Rows.Count - 1

    ''        ''myTable.Rows(r)("Ind").ToString()
    ''        'Dim myButton As Button = New Button
    ''        'myButton.Name = myTable.Rows(r)("ind").ToString()
    ''        'myButton.Text = myTable.Rows(r)("ind").ToString()
    ''        'myButton.Location = New Point(15, Y)

    ''        'Y = Y + 130
    ''        ''Me.Controls.Add(myButton)
    ''        'splitContainer1.AutoScroll = True
    ''        'splitContainer1.Panel1.AutoScroll = True
    ''        ''splitContainer1.Container.Add(myButton)
    ''        'splitContainer1.Panel1.Controls.Add(myButton)
    ''        ''myButton.BringToFront()
    ''        ''splitContainer1.Panel1.Width = splitContainer1.Panel1.Width + 30
    ''    Next

    ''End Sub

    ''Private Sub btnButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''    'splitContainer4.SplitterDistance = 55
    ''    SplitContainer2.Panel2Collapsed = True
    ''    Me.Refresh()
    ''    'splitContainer4.Panel2.Width = 2
    ''End Sub

    ''Private Sub CheckedListBox_Urban_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckedListBox_Urban.Click
    ''    '    'Chỉ xét với checklist đầu tiên tức là "Chọn tất cả?"
    ''    '    'Lưu ý: CheckedListBox_Urban.SelectedItem trả về dataRowView
    ''    '    'CheckedListBox_Urban.SelectedItem.Row(0) trả về Giá trị Row đầu tiên của DataRowview
    ''    Dim clickedItem = CheckedListBoxControl1.SelectedItem   'CheckedListBox_Urban.SelectedItem.Row(0)  'Sau khi click vào 1 dòng thì lại trả về 1 

    ''    '    If (clickedItem.ToString = "Chọn tất cả") Then
    ''    '        'Nếu checklist đầu tiên này đang chưa check thì
    ''    '        If clickedItem.Row(0) <> CheckState.Checked Then
    ''    '            For i = 1 To CheckedListBox_Urban.ItemCount - 2
    ''    '                CheckedListBox_Urban.SetItemChecked(i, True)
    ''    '            Next
    ''    '        ElseIf clickedItem.Row(0) = CheckState.Checked Then 'Nếu "Chọn tất cả" đang được check
    ''    '            For i = 1 To CheckedListBox_Urban.ItemCount - 2
    ''    '                CheckedListBox_Urban.SetItemChecked(i, False)
    ''    '            Next
    ''    '        Else 'Trường hơp
    ''    '        End If
    ''    '    Else
    ''    '        CheckedListBox_Urban.SetItemCheckState(0, CheckState.Indeterminate)

    ''    '    End If
    ''    If CheckedListBox_Urban.SelectedIndex <> 0 Then
    ''        CheckedListBox_Urban.SetItemCheckState(0, CheckState.Indeterminate)
    ''        ' CheckedListBoxControl1.SelectedItem

    ''    End If
    ''End Sub

    ''Private Sub CheckedListBox_Urban_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBox_Urban.ItemCheck
    ''    If (e.Index = 0) Then           ' e là checklist được check 'CheckedListBox_Urban.SelectedIndex
    ''        'Nếu checklist đầu tiên này đang chưa check thì
    ''        If e.CurrentValue <> CheckState.Checked Then
    ''            For i = 1 To CheckedListBox_Urban.ItemCount - 2
    ''                CheckedListBox_Urban.SetItemChecked(i, True)
    ''            Next
    ''        ElseIf e.CurrentValue = CheckState.Checked Then 'Nếu "Chọn tất cả" đang được check
    ''            For i = 1 To CheckedListBox_Urban.ItemCount - 2 '==Co the thay bang
    ''                CheckedListBox_Urban.SetItemChecked(i, False)
    ''            Next
    ''        Else 'Trường hơp
    ''        End If
    ''    Else
    ''        'CheckedListBox_Urban.SetItemCheckState(0, CheckState.Indeterminate)
    ''        ' e.CurrentValue.Indeterminate()
    ''    End If
    ''    'CheckedListBox_Urban.
    ''    'If (e.Index = 0) Then
    ''    '    checkAll(e)
    ''    'Else
    ''    '    CheckedListBox_Urban.SetItemCheckState(0, CheckState.Indeterminate)
    ''    'End If

    ''End Sub

    Private Sub CheckedListBox_Urban_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckedListBox_Urban.SelectedIndexChanged
        If Form_Loaded = False Then
            Return
        End If
        Try 'Đặt try catch để tránh trường hợp chưa khởi tạo
            Dim CkbTable As DataTable = CheckedListBox_Urban.DataSource
            Dim i As Integer = CheckedListBox_Urban.SelectedIndex
            CkbUrb_ChkAll.CheckState = CheckState.Indeterminate
            Select Case CkbTable.Rows(i)("RegionGrp")
                Case CkbUrb_ChkGrp1.Text
                    CkbUrb_ChkGrp1.CheckState = CheckState.Indeterminate
                Case CkbUrb_ChkGrp2.Text
                    CkbUrb_ChkGrp2.CheckState = CheckState.Indeterminate
                Case CkbUrb_ChkGrp3.Text
                    CkbUrb_ChkGrp3.CheckState = CheckState.Indeterminate
                Case CkbUrb_ChkGrp4.Text
                    CkbUrb_ChkGrp4.CheckState = CheckState.Indeterminate
                Case CkbUrb_ChkGrp5.Text
                    CkbUrb_ChkGrp5.CheckState = CheckState.Indeterminate
                Case CkbUrb_ChkGrp6.Text
                    CkbUrb_ChkGrp6.CheckState = CheckState.Indeterminate
            End Select
        Catch

        End Try

        If XtraTabControl2.SelectedTabPageIndex = 1 And CmbSoSanh.SelectedItem = "Đồ thị theo một Địa bàn" Then
            If CheckedListBox_Urban.CheckedItems.Count > 0 Then
                CheckedListBox_Urban.UnCheckAll()
                CheckedListBox_Urban.CheckOnClick = True

                'CkbInd_ChkAll.CheckState = CheckState.Indeterminate
                'CkbUrb_ChkAll.CheckState = CheckState.Indeterminate
                'CkbUrb_ChkAll.CheckState = CheckState.Unchecked
                'CheckedListBox_Urban.SetItemChecked(CheckedListBox_Urban.SelectedIndex, True)
            End If
            CkbUrb_ChkAll.CheckState = CheckState.Indeterminate
        End If

    End Sub


    Private Sub CheckedListBox_Ind_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckedListBox_Ind.SelectedIndexChanged
        If Form_Loaded = False Then
            Return
        End If
        If XtraTabControl2.SelectedTabPageIndex = 1 And CmbSoSanh.SelectedItem = "Đồ thị theo một Chỉ tiêu" Then
            CheckedListBox_Ind.UnCheckAll()
        End If
        CkbInd_ChkAll.CheckState = CheckState.Indeterminate

        '        CkbYear_ChkAll.CheckState = CheckState.Indeterminate

        'If XtraTabControl2.SelectedTabPageIndex = 1 And CmbSoSanh.SelectedItem = "Đồ thị theo một Chỉ số" Then
        '    If CheckedListBox_Ind.CheckedItems.Count > 0 Then
        '        CkbInd_ChkAll.CheckState = CheckState.Indeterminate
        '        CkbInd_ChkAll.CheckState = CheckState.Unchecked
        '        CheckedListBox_Ind.SetItemChecked(CheckedListBox_Ind.SelectedIndex, True)
        '    End If
        '    'ElseIf XtraTabControl2.SelectedTabPageIndex = 1 And CmbSoSanh.SelectedItem = "Đồ thị theo một Năm" Then
        '    '    If CheckedListBox_Year.CheckedItems.Count > 1 Then
        '    '        CkbYear_ChkAll.CheckState = CheckState.Indeterminate
        '    '        CkbYear_ChkAll.CheckState = CheckState.Unchecked
        '    '        CheckedListBox_Year.SetItemChecked(CheckedListBox_Year.SelectedIndex, True)
        '    '    End If

        '    'ElseIf XtraTabControl2.SelectedTabPageIndex = 1 And CmbSoSanh.SelectedItem = "Đồ thị theo một Địa bàn" Then
        '    '    If CheckedListBox_Urban.CheckedItems.Count > 1 Then
        '    '        CkbUrb_ChkAll.CheckState = CheckState.Indeterminate
        '    '        CkbUrb_ChkAll.CheckState = CheckState.Unchecked
        '    '        CheckedListBox_Urban.SetItemChecked(CheckedListBox_Urban.SelectedIndex, True)
        '    '    End If
        'End If
        ' ''CkbInd_ChkAll.CheckState = CheckState.Indeterminate
        ''CkbYear_ChkAll.CheckState = CheckState.Indeterminate
    End Sub

    ' Private Sub CheckedListBox_Year_GotFocus(sender As Object, e As EventArgs) Handles CheckedListBox_Year.GotFocus
    'sender.uncheckall()
    'CheckedListBox_Year.SetItemChecked(CheckedListBox_Ind.SelectedIndex, True)
    ' ''===Không cho uncheck tất cả các Item.
    'If sender.CheckedItems.Count = 0 Then
    '    ' CheckedListBox_Ind.Items(e.Index).CheckState = CheckState.Checked
    '    TryCast(sender, DevExpress.XtraEditors.CheckedListBoxControl).SetItemChecked(CheckedListBox_Ind.SelectedIndex, True)
    '    Return
    'End If
    '  End Sub


    'Private Async Sub DisableButtonAsync(ByVal seconds As Int32)
    '    Await Task.Delay(seconds * 1000)
    '    CheckedListBox_Year.UnCheckAll()
    'End Sub
    Private Sub CheckedListBox_Year_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckedListBox_Year.SelectedIndexChanged
        If Form_Loaded = False Then
            Return
        End If
        If XtraTabControl2.SelectedTabPageIndex = 1 And CmbSoSanh.SelectedItem = "Đồ thị theo một Năm" Then
            'SplashScreenManager.ShowForm(GetType(FrmWaiting))
            'System.Threading.Thread.Sleep(500)

            'SplashScreenManager.CloseForm()
            CheckedListBox_Year.UnCheckAll()
        End If

        CkbYear_ChkAll.CheckState = CheckState.Indeterminate
        'If CheckedListBox_Year.CheckedItems.Count = CheckedListBox_Year.ItemCount Then
        '    CkbYear_ChkAll.CheckState = CheckState.Checked
        'ElseIf CheckedListBox_Year.CheckedItemsCount = 0 Then
        '    CkbYear_ChkAll.CheckState = CheckState.Unchecked
        'Else

        'End If

        'If XtraTabControl2.SelectedTabPageIndex = 1 And CmbSoSanh.SelectedItem = "Đồ thị theo một Năm" Then
        '    'If CheckedListBox_Year.CheckedItems.Count > 0 Then
        '    'CkbYear_ChkAll.CheckState = CheckState.Indeterminate
        '    CkbYear_ChkAll.CheckState = CheckState.Unchecked

        '    CheckedListBox_Year.SetItemChecked(CheckedListBox_Year.SelectedIndex, True)
        '    'End If

        'End If

        'CkbInd_ChkAll.CheckState = CheckState.Indeterminate
        'CkbYear_ChkAll.CheckState = CheckState.Indeterminate

        'If XtraTabControl2.SelectedTabPageIndex = 1 And CheckedListBox_Ind.CheckedItems.Count > 1 Then
        '    If CmbSoSanh.SelectedItem = "Đồ thị theo một Chỉ số" Then
        '        CkbInd_ChkAll.Checked = False
        '        'CheckedListBox_Urban.Items.Item(CheckedListBox_Urban.SelectedIndex).checked = True
        '        'CheckedListBox_Urban.SetSelected(CheckedListBox_Urban.SelectedIndex, True)
        '        'CheckedListBox_Urban.SetSelected(CheckedListBox_Urban.SelectedIndex, False)
        '        CheckedListBox_Ind.SetItemChecked(CheckedListBox_Ind.SelectedIndex, True)
        '    End If

        'End If


    End Sub
    Private Sub CheckedListBox_Region_ItemCheck(sender As Object, e As DevExpress.XtraEditors.Controls.ItemCheckEventArgs) Handles CheckedListBox_Urban.ItemCheck
        If Form_Loaded = False Then
            Return
        End If
        Dim MousePos As System.Drawing.Point = sender.PointToClient(MousePosition)
        Dim Ckb_Bnd As System.Drawing.Rectangle = sender.bounds
        If MousePos.X < 0 Or MousePos.X > Ckb_Bnd.Width Or MousePos.Y < 0 Or MousePos.Y > Ckb_Bnd.Height Then
            Return
        End If
        ''===Không cho uncheck tất cả các Item.
        If sender.CheckedItems.Count = 0 Then
            ' CheckedListBox_Ind.Items(e.Index).CheckState = CheckState.Checked
            '  TryCast(sender, DevExpress.XtraEditors.CheckedListBoxControl).SetItemChecked(e.Index, True)
            Return
        End If
        If XtraTabControl2.SelectedTabPageIndex = 1 Then
            BtnOK_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub CheckedListBox_Ind_ItemCheck(sender As Object, e As DevExpress.XtraEditors.Controls.ItemCheckEventArgs) Handles CheckedListBox_Ind.ItemCheck
        If Form_Loaded = False Then
            Return
        End If
        Dim MousePos As System.Drawing.Point = sender.PointToClient(MousePosition)
        Dim Ckb_Bnd As System.Drawing.Rectangle = sender.bounds
        If MousePos.X < 0 Or MousePos.X > Ckb_Bnd.Width Or MousePos.Y < 0 Or MousePos.Y > Ckb_Bnd.Height Then
            Return
        End If
        ''===Không cho uncheck tất cả các Item.
        If sender.CheckedItems.Count = 0 Then
            ' CheckedListBox_Ind.Items(e.Index).CheckState = CheckState.Checked
            ' TryCast(sender, DevExpress.XtraEditors.CheckedListBoxControl).SetItemChecked(e.Index, True)
            Return
        End If
        ''CheckEdit1.
        ''Dim hi As DevExpress.XtraCharts.ChartHitInfo = ChartControl1.CalcHitInfo(e.Location)
        ''If Not hi Is Nothing Then
        ''    TextEdit2.Focus()
        ''End If
        If XtraTabControl2.SelectedTabPageIndex = 1 Then
            BtnOK_Click(Nothing, Nothing)
        End If

    End Sub
    'Private Sub mySetNodeCheckState(ByVal tl As TreeList, ByVal node As DevExpress.XtraTreeList.Nodes.TreeListNode)
    '    Dim checkState As Windows.Forms.CheckState = node.CheckState
    '    Dim resCheckState As Windows.Forms.CheckState = checkState.Unchecked
    '    'Select Case checkState
    '    '    Case checkState.Checked
    '    '        resCheckState = checkState.Unchecked
    '    '    Case checkState.Unchecked
    '    '        resCheckState = checkState.Indeterminate
    '    '    Case checkState.Indeterminate
    '    '        resCheckState = checkState.Checked
    '    'End Select
    '    If (node.CheckState = checkState.Indeterminate Or node.CheckState = checkState.Checked) Then
    '        resCheckState = checkState.Unchecked
    '    Else
    '        resCheckState = checkState.Checked
    '    End If
    '    tl.SetNodeCheckState(node, resCheckState, True)
    'End Sub
    Private Sub CheckedListBox_Year_ItemCheck(sender As Object, e As DevExpress.XtraEditors.Controls.ItemCheckEventArgs) Handles CheckedListBox_Year.ItemCheck
        If Form_Loaded = False Then
            Return
        End If
        Dim MousePos As System.Drawing.Point = sender.PointToClient(MousePosition)
        Dim Ckb_Bnd As System.Drawing.Rectangle = sender.bounds

        'Mouse ngoài vị trí CKL thì thoát
        If MousePos.X < 0 Or MousePos.X > Ckb_Bnd.Width Or MousePos.Y < 0 Or MousePos.Y > Ckb_Bnd.Height Then
            Return
        End If

        ''CkbYear_ChkAll.CheckState = CheckState.Indeterminate
        ' ''===Không cho uncheck tất cả các Item.
        If sender.CheckedItems.Count = 0 Then
            ' CheckedListBox_Ind.Items(e.Index).CheckState = CheckState.Checked
            ' If CheckedListBox_Ind.SelectedIndex <> 0 Then
            'TryCast(sender, DevExpress.XtraEditors.CheckedListBoxControl).SetItemChecked(e.Index, True)
            'End If

            Return
        End If
        ''If XtraTabControl2.SelectedTabPageIndex = 1 Then
        ''    If Not e.State = CheckState.Checked Then
        ''        'CheckedListBox_Year.SetItemChecked(e.Index, True)
        ''        Return
        ''    End If
        ''End If
        If XtraTabControl2.SelectedTabPageIndex = 1 Then
            BtnOK_Click(Nothing, Nothing)
        End If
    End Sub
    Protected Sub SetItemCheckStateCore(ByVal index As Integer, ByVal value As System.Windows.Forms.CheckState)
        If value = System.Windows.Forms.CheckState.Checked Then
            CheckedListBox_Year.UnCheckAll()
        End If
        SetItemCheckStateCore(index, value)
    End Sub
    Dim FirstTime_XtraTabControl2_SelectedPageChanging As Boolean = True
    Private Sub XtraTabControl2_SelectedPageChanging(sender As Object, e As DevExpress.XtraTab.TabPageChangingEventArgs) Handles XtraTabControl2.SelectedPageChanging
        If Form_Loaded = False Then
            Return
        End If
        If e.Page Is XtraTabPage_DoThi Then
            If CmbSoSanh.SelectedItem = "Đồ thị theo một Chỉ số" Then
                CkbInd_ChkAll.CheckState = CheckState.Unchecked
                Try
                    CheckedListBox_Ind.SetItemChecked(CheckedListBox_Ind.SelectedIndex, True)
                Catch ex As Exception

                End Try

            ElseIf CmbSoSanh.SelectedItem = "Đồ thị theo một Địa bàn" Then
                CkbUrb_ChkAll.CheckState = CheckState.Unchecked
                CheckedListBox_Urban.SetItemChecked(CheckedListBox_Urban.SelectedIndex, True)
            ElseIf CmbSoSanh.SelectedItem = "Đồ thị theo một Năm" Then
                CkbYear_ChkAll.CheckState = CheckState.Unchecked
                CheckedListBox_Year.SetItemChecked(CheckedListBox_Year.SelectedIndex, True)
            End If
        End If
        If FirstTime_XtraTabControl2_SelectedPageChanging = True Then
            XtraTabControl1.SelectedTabPageIndex = 2

        End If
        FirstTime_XtraTabControl2_SelectedPageChanging = False
    End Sub
    Dim FirstTime_XtraTabControl2_SelectedPageChanged As Boolean = True
    Private Sub XtraTabControl2_SelectedPageChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles XtraTabControl2.SelectedPageChanged
        'BtnOK_Click(Nothing, Nothing)    'Khi chuyển Tab thì Update lại dữ liệu nếu đã thay đổi và save dữ liệu ở Tab kia, Nếu ko sẽ gây ra lỗi khi click btnsave1 và btnsave2 (lỗi do đã dữ liệu nguồn đã update)
        'Tuy nhiên như vậy khi chuyển sagn Tabpage Thông tin thì chạy hơi chậm, nó phải tạo lại control textedit
        If XtraTabControl2.SelectedTabPageIndex = 0 Then
            'XtraXtraTabControl2.Visible = True
            'TableLayoutPanel1.Visible = False
            'MainTabPage1()
            MnuBangDuLieu.Checked = True
            MnuBangDuLieu.Enabled = True
            ' AnHienBottomPanel()     'Hiện Bottom Panel ở Tab Thong tin
            MnuBangDuLieu.Checked = True
            GridControl1.BringToFront()
            'MnuExportXls.Enabled = True
            BtnOK.Text = "TẠO Ô NHẬP LIỆU"
            '===Ini Ckl_Indicator
            'Dim Table_Indicator As DataTable = MyADOClass.DtFromQry("Select ID, Theme, Ind, IndicatorTN,mynote  From " + IndTable + "  order by IndicatorTN")
            'CheckedListBox_Ind.DataSource = Table_Indicator
            'CheckedListBox_Ind.DisplayMember = "IndicatorTN"
            Panel2.Visible = False
            '  SplitContainer4.Panel1Collapsed = True
            BtnSaveGrV2.Visible = False
            BtnSaveGrV1.Visible = True
            pnlPanel1.Visible = True
        Else
            'gridcontrol2.datasource = Nothing
            ' XtraXtraTabControl2.Visible = False
            ' TableLayoutPanel1.Visible = True
            'MainTabPage2()
            'MnuExportXls.Enabled = False
            'AnHienBottomPanel()     'Ẩn Bottom Panel ở Tab Đồ thị

            '========================
            'MnuBangDuLieu.Checked = False
            'MnuBangDuLieu.Enabled = False
            GridControl2.BringToFront()
            BtnOK.Text = "TẠO ĐỒ THỊ"
            '===Ini Ckl_Indicator
            'Dim Table_Indicator As DataTable = MyADOClass.DtFromQry("Select ID, Theme, Ind, IndicatorTN,mynote  From " + IndTable + "  where mynote = " + quote + "Nhóm 2: Giá trị phần trăm " + quote + " Or mynote = " + quote + "Nhóm 3: Giá trị số thực" + quote + " Or mynote = " + quote + "Nhóm 1: Giá trị số nguyên" + quote + " Order by IndicatorTN ")
            'CheckedListBox_Ind.DataSource = Table_Indicator
            'CheckedListBox_Ind.DisplayMember = "IndicatorTN"
            Panel2.Visible = True
            'SplitContainer4.Panel1Collapsed = False
            'SplitContainer4.SplitterDistance = 10
            BtnSaveGrV2.Visible = True
            BtnSaveGrV1.Visible = False
            pnlPanel1.Visible = False

            '=============
            'Enable hoặc disable CkbInd_ChkAll khi chọn Đồ thị theo một năm và Disable CkbYear_ChkAll khi chọn Đồ thị theo một chỉ số 
            disableChkAll()
            'sẽ không giữ tình trạng chọn nhiều chỉ số trong trường hợp Đồ thị theo một Năm và không giữ tình trạng chọn nhiều năm nếu CmbSoSanh = Đồ thị theo một chỉ số
            If XtraTabControl2.SelectedTabPageIndex = 1 And CmbSoSanh.SelectedItem = "Đồ thị theo một Chỉ số" Then
                'CkbInd_ChkAll.CheckState = CheckState.Checked   'Phải check trước thì uncheck mới có hiệu lực
                'CkbInd_ChkAll.CheckState = CheckState.Unchecked 'Cần uncheck để không chọn 1 row nào
                ''CheckedListBox_Ind.SetItemChecked(CheckedListBox_Ind.SelectedIndex, True)
                CheckedListBox_Ind.UnCheckAll()
                CheckedListBox_Ind.SetItemChecked(CheckedListBox_Ind.SelectedIndex, True)
                CkbInd_ChkAll.CheckState = CheckState.Indeterminate
            ElseIf XtraTabControl2.SelectedTabPageIndex = 1 And CmbSoSanh.SelectedItem = "Đồ thị theo một Địa bàn" Then
                CheckedListBox_Urban.UnCheckAll()
                CheckedListBox_Urban.SetItemChecked(CheckedListBox_Urban.SelectedIndex, True)
                CkbUrb_ChkAll.CheckState = CheckState.Indeterminate
            ElseIf XtraTabControl2.SelectedTabPageIndex = 1 And CmbSoSanh.SelectedItem = "Đồ thị theo một Năm" Then
                'CkbYear_ChkAll.CheckState = CheckState.Checked
                'CkbYear_ChkAll.CheckState = CheckState.Unchecked
                '
                CheckedListBox_Year.UnCheckAll()
                CheckedListBox_Year.SetItemChecked(CheckedListBox_Year.SelectedIndex, True)
                CkbYear_ChkAll.CheckState = CheckState.Indeterminate
            End If
            'formatdgv(GridView2)
        End If


        '=============
        'Enable hoặc disable CkbInd_ChkAll khi chọn Đồ thị theo một năm và Disable CkbYear_ChkAll khi chọn Đồ thị theo một chỉ số 
        disableChkAll()
        'Khi chuyển sang Tabpage2, sẽ không giữ tình trạng chọn nhiều chỉ số trong trường hợp Đồ thị theo một Năm và không giữ tình trạng chọn nhiều năm nếu CmbSoSanh = Đồ thị theo một chỉ số
        If FirstTime_XtraTabControl2_SelectedPageChanged = True Then
            BtnOK_Click(Nothing, Nothing)

        End If
        FirstTime_XtraTabControl2_SelectedPageChanged = False
    End Sub

    Private Sub CkbInd_Descript_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CkbInd_Descript.CheckedChanged
        'Dim Table_Indicator As DataTable = CheckedListBox_Ind.DataSource
        'Dim sortTable = New Sort_Table  'Tạo đối tượng từ class sort_Table
        If CkbInd_Descript.Checked = True Then
            CheckedListBox_Ind.DisplayMember = "Ind"
            'Sắp xếp theo Ind symbol
            'sortTable.sortDataTable(Table_Indicator, "ind", True)
        Else
            CheckedListBox_Ind.DisplayMember = "IndicatorTN"
            'sortTable.sortDataTable(Table_Indicator, "IndicatorTN", True)
        End If
        ' CkbInd_ChkAll.CheckState = CheckState.Unchecked

        'If CkbInd_Descript.Checked = True Then
        '    CheckedListBox_Ind.DisplayMember = "Ind"
        'Else
        '    CheckedListBox_Ind.DisplayMember = "IndicatorTN"
        'End If
        'CkbInd_ChkAll.CheckState = CheckState.Unchecked
    End Sub

    Private Sub CkbInd_Sort_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CkbInd_Sort.CheckStateChanged
        Dim Table_Indicator As DataTable = CheckedListBox_Ind.DataSource    '.totable    'Phai chuyen thanh Table vi lúc định nghĩa, checklistInd dùng datasource là dataview
        Dim sortTable = New Sort_Table  'tạo đối tượng từ Class Sort_Table

        If CkbInd_Sort.Checked = False Then

            'CkbInd_Descript_CheckedChanged(Nothing, Nothing)
            sortTable.sortDataTable(Table_Indicator, "ID", True)
        Else
            sortTable.sortDataTable(Table_Indicator, "IndicatorTN", True)
        End If


        ''MyADOClass.KetNoi_Open()
        ''Dim TableIndicator As DataTable = MyADOClass.TableFromQry("Select ID, Ind, IndicatorTN  From " + IndTable + " ")
        ''MyADOClass.KetNoi_Close()

        ' ''TableIndicator.Columns (0).
        ''Dim SortAbleView As DataView = TableIndicator.DefaultView
        ''CheckedListBox_Ind.DataSource = SortAbleView
        ''If CkbInd_Sort.Checked = True Then
        ''    SortAbleView.Sort = "ID"
        ''Else
        ''    SortAbleView.Sort = "IndicatorTN"
        ''End If
        ' ''===Ini Cmb_Indicator
        ' ''CheckedListBox_Ind.DataSource = TableIndicator
        ' ''CheckedListBox_Ind.DisplayMember = "IndicatorTN"

    End Sub

    Private Sub cmbRegion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbRegion.SelectedIndexChanged
        If Form_Loaded = False Then
            Return
        End If
        '==Do cmbRegion selectedIndexChanged và CmbmYear_SelectedIndexChanged có nội dung giống nhau nên cho chạy cmbMyear như dưới luôn
        CmbmYear_SelectedIndexChanged(Nothing, Nothing)
        '' '' ''=Xác định xem có phải save hay ko
        '' '' ''If savebolean = True Then
        '' '' ''    btnSaveGrV1_Click(Nothing, Nothing)
        '' '' ''End If
        ' '' ''Dim dt As DataTable = gridcontrol1.datasource
        ' '' ''Dim dtchange As DataTable = dt.GetChanges()
        ' '' ''If (dtchange IsNot Nothing) AndAlso (dtchange.Rows.Count <> 0) Then     'Xác định xem datagridview có bị change ko
        ' '' ''    btnSaveGrV1_Click(Nothing, Nothing)
        ' '' ''End If

        ' '' ''Dim myTable As DataTable = gridcontrol1.datasource
        '' '' ''===Filter theo nam lua chon tren combo
        ' '' ''Dim FilterView As DataView = myTable.DefaultView
        '' '' ''FilterView.RowFilter = "ID >= 0"    '=======Bỏ Filterview của GridView1 để FilterRow theo Urban mới;Khong can thiet
        ' '' ''Try
        ' '' ''    FilterView.RowFilter = "Region = '" + cmbRegion.SelectedItem("Region") + "' AND mYear = '" + CmbmYear.SelectedItem("mYear") + "'"

        ' '' ''Catch ex As Exception

        ' '' ''End Try


        '' '' ''==='Trước tiên phải xóa hết giá trị cũ  => Chạy dùng hàm FindControl nhanh hơn vòng lặp for trên rất nhiều
        '' '' ''Có cái này tự nhiên treo luôn
        '' '' ''For i As Integer = 0 To myTable.Rows.Count - 1
        '' '' ''    Dim TbxVal = FindControl(XtraScrollableControl1, "TbxVal" + myTable(i)("Ind").ToString)
        '' '' ''    Dim TbxRef = FindControl(XtraScrollableControl1, "TbxRef" + myTable(i)("Ind").ToString)
        '' '' ''    If TbxVal IsNot Nothing AndAlso TbxVal.GetType Is GetType(TextBox) Then
        '' '' ''        TbxVal.Text = ""
        '' '' ''    End If
        '' '' ''Next

        ' '' ''Dim FilDataRows = Nothing
        '' '' ''Phải dùng Try Catch và If Return Bởi vì Khi mới khởi tạo, cmb chưa select Item nào nên FilDataRows = nothing => báo lỗi
        '' '' ''cmbRegion.SelectedIndex = 0
        ' '' ''Try
        ' '' ''    'Chọn mọi Rows thỏa mãn ĐK Region = CmbUrban.Selected và mYear = cmbmYear.Selected cho vào dataRow
        ' '' ''    FilDataRows = myTable.Select("Region= " + "'" + cmbRegion.SelectedItem("Region").ToString + "' And mYear = '" + CmbmYear.SelectedItem("mYear").ToString + "'")
        ' '' ''Catch
        ' '' ''End Try
        ' '' ''If FilDataRows Is Nothing Then
        ' '' ''    Return
        ' '' ''End If
        ' '' ''For i As Integer = 0 To FilDataRows.Length - 1
        ' '' ''    ' ''===Điền thông tin lên Tbx
        ' '' ''    ''For Each tbxVal In XtraScrollableControl1.Controls
        ' '' ''    ''    If tbxVal.GetType Is GetType(TextBox) Then
        ' '' ''    ''        If tbxVal.name.Substring(0, 6) = "TbxVal" And tbxVal.name.Substring(6, tbxVal.name.length - 6) = FilDataRows(i)("Ind").ToString Then 'tbxVal.GetType Is GetType(TextBox) Then
        ' '' ''    ''            tbxVal.text = FilDataRows(i)("DataVal").ToString
        ' '' ''    ''            'MessageBox.Show("")
        ' '' ''    ''            Exit For
        ' '' ''    ''        End If
        ' '' ''    ''    End If
        ' '' ''    ''Next    

        ' '' ''    '===Điền thông tin lên Tbx  => Chạy dùng hàm FindControl nhanh hơn vòng lặp for trên rất nhiều
        ' '' ''    Dim TbxVal = FindControl(XtraScrollableControl1, "TbxVal" + FilDataRows(i)("Ind").ToString)
        ' '' ''    Dim TbxRef = FindControl(XtraScrollableControl1, "TbxRef" + FilDataRows(i)("Ind").ToString)
        ' '' ''    If TbxVal IsNot Nothing AndAlso TbxVal.GetType Is GetType(TextBox) Then
        ' '' ''        'Trước tiên phải xóa hết giá trị cũ
        ' '' ''        TbxVal.Text = ""
        ' '' ''        TbxVal.Text = FilDataRows(i)("DataVal").ToString
        ' '' ''    End If
        ' '' ''    If TbxVal IsNot Nothing AndAlso TbxVal.GetType Is GetType(Windows.Forms.ComboBox) Then
        ' '' ''        If FilDataRows(i)("DataVal").ToString = "" Then
        ' '' ''            'CType(TbxVal, Windows.Forms.ComboBox).SelectedIndex = 0  'Để trong trường hợp dataVal = "" thì nó chọn luôn giá trị đầu tiên của combo
        ' '' ''            CType(TbxVal, Windows.Forms.ComboBox).Text = ""  'Để trong trường hợp dataVal = "" thì nó chọn luôn giá trị đầu tiên

        ' '' ''        Else
        ' '' ''            CType(TbxVal, Windows.Forms.ComboBox).SelectedItem = FilDataRows(i)("DataVal").ToString
        ' '' ''        End If
        ' '' ''    End If
        ' '' ''    If TbxRef IsNot Nothing Then
        ' '' ''        TbxRef.Text = FilDataRows(i)("DataRef").ToString
        ' '' ''    End If
        ' '' ''Next
        '' '' ''Dim a = cmbRegion.SelectedItem("Region")  'Giá trị của trường Region tại dataRow đang selected.
        '' '' ''Dim b = a

        '' '' ''For i As Integer = 0 To GridView1.Rows.Count - 1
        '' '' ''    '===Điền thông tin lên Tbx
        '' '' ''    For Each tbxVal In XtraScrollableControl1.Controls
        '' '' ''        If tbxVal.GetType Is GetType(TextBox) Then
        '' '' ''            If tbxVal.name.Substring(0, 6) = "TbxVal" And tbxVal.name.Substring(6, tbxVal.name.length - 6) = GridView1.Rows(i).Cells("Ind").Value.ToString Then 'tbxVal.GetType Is GetType(TextBox) Then
        '' '' ''                tbxVal.text = GridView1.Rows(i).Cells("DataVal").Value.ToString
        '' '' ''                'MessageBox.Show("")
        '' '' ''                'Exit For
        '' '' ''            End If
        '' '' ''        End If
        '' '' ''    Next    '
        '' '' ''Next
        ' '' '' ''Dim curRow As Integer = GridView1.CurrentCell.RowIndex
        ' '' '' ''Dim c = GridView1.Rows(i).Cells("Region").Value.ToString
        ' '' '' ''Dim d = cmbRegion.SelectedItem("Region").ToString

        ' '' '' ''=====Chuyển đổi select cho DataGridView

        '' '' ''For i As Integer = 0 To GridView1.Rows.Count - 1
        '' '' ''    If GridView1.Rows(i).Cells("Region").Value.ToString = cmbRegion.SelectedItem("Region").ToString And GridView1.Rows(i).Cells("mYear").Value.ToString = CmbmYear.SelectedItem("mYear").ToString Then
        '' '' ''        'MessageBox.Show("")
        '' '' ''        GridView1.Rows(i).Selected = True
        '' '' ''        GridView1.FirstDisplayedScrollingRowIndex = i   'Scroll to the select row
        '' '' ''        Exit For
        '' '' ''    End If
        '' '' ''Next
    End Sub

    Private Sub CmbmYear_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbmYear.SelectedIndexChanged
        If Form_Loaded = False Then
            Return
        End If
        ''=Xác định xem có phải save hay ko
        'If BtnSaveGrV1.Enabled = True Then
        '	btnSaveGrV1_Click(Nothing, Nothing)
        'End If
        'Dim dt As DataTable = GridControl1.DataSource
        ''Dim dtchange As DataTable = dt.GetChanges()

        ''If (dtchange IsNot Nothing) AndAlso (dtchange.Rows.Count <> 0) Then     'Xác định xem datagridview có bị change ko
        ''    btnSaveGrV1_Click(Nothing, Nothing)
        ''End If

        Dim myTable As DataTable = GridControl1.DataSource
        Dim FilterView As DataView = myTable.DefaultView
        Try     'Filter theo nam lua chon tren Combo
            'FilterView.RowFilter = "ID >= 0"    '=======Bỏ Filterview của GridView1 để FilterRow theo Urban mới
            FilterView.RowFilter = "Region = '" + cmbRegion.SelectedItem("Region") + "' AND mYear = '" + CmbmYear.SelectedItem("mYear") + "'"

        Catch ex As Exception

        End Try

        ''==='Trước tiên phải xóa hết giá trị cũ  => Chạy dùng hàm FindControl nhanh hơn vòng lặp for trên rất nhiều
        ''Thực ra cũng ko cần Những lệnh này vì nếu 1 năm được tạo từ chương trình thì sẽ tạo ra mọi Urban và Ind giá trị = ""
        ''Nên khi click chọn 1 urban nào thì nó cũng update giá trị 0 lên texbox tức là nó sẽ tự xóa.
        ''Tuy nhiên lúc đầu do ko tạo năm = chương trình nên có nhiều Urban ko có giá trị năm đó nên nó bỏ qua, ko update giá trị "" lên
        ''Có cái này là đơ máy ngay
        'For i As Integer = 0 To myTable.Rows.Count - 1
        '    Dim TbxVal = FindControl(XtraScrollableControl1, "TbxVal" + myTable(i)("Ind").ToString)
        '    Dim TbxRef = FindControl(XtraScrollableControl1, "TbxRef" + myTable(i)("Ind").ToString)
        '    If TbxVal IsNot Nothing AndAlso TbxVal.GetType Is GetType(TextBox) Then
        '        TbxVal.Text = ""
        '    End If
        'Next
        Dim FilDataRows
        'Dim FilDataRows '= Nothing
        'Phải dùng Try Catch và If Return Bởi vì Khi mới khởi tạo, cmb chưa select Item nào nên FilDataRows = nothing => báo lỗi
        Try
            'Chọn mọi Rows thỏa mãn ĐK Region = CmbUrban.Selected và mYear = cmbmYear.Selected cho vào dataRow
            FilDataRows = myTable.Select("Region= " + "'" + cmbRegion.SelectedItem("Region").ToString + "' And mYear = '" + CmbmYear.SelectedItem("mYear").ToString + "'")

        Catch
        End Try
        If FilDataRows Is Nothing Then
            Return
        End If
        ''=====Chuyển đổi select cho DataGridView
        'For i As Integer = 0 To GridView1.Rows.Count - 1
        '    If GridView1.Rows(i).Cells("Region").Value.ToString = cmbRegion.SelectedItem("Region").ToString And GridView1.Rows(i).Cells("mYear").Value.ToString = CmbmYear.SelectedItem("mYear").ToString Then
        '        'MessageBox.Show("")
        '        GridView1.Rows(i).Selected = True
        '        'GridView1.FirstDisplayedScrollingRowIndex = i   'Scroll to the select row
        '        Exit For
        '    End If
        'Next

        For i As Integer = 0 To FilDataRows.Length - 1
            ' ''===Điền thông tin lên Tbx
            ''For Each tbxVal In XtraScrollableControl1.Controls
            ''    If tbxVal.GetType Is GetType(TextBox) Then
            ''        If tbxVal.name.Substring(0, 6) = "TbxVal" And tbxVal.name.Substring(6, tbxVal.name.length - 6) = FilDataRows(i)("Ind").ToString Then 'tbxVal.GetType Is GetType(TextBox) Then
            ''            tbxVal.text = FilDataRows(i)("DataVal").ToString
            ''            'MessageBox.Show("")
            ''            Exit For
            ''        End If
            ''    End If
            ''Next    '
            '===Điền thông tin lên Tbx  => Chạy dùng hàm FindControl nhanh hơn vòng lặp for trên rất nhiều
            Dim TbxVal = FindControl(XtraScrollable_Thongtin, "TbxVal" + FilDataRows(i)("Ind").ToString)
            Dim TbxRef = FindControl(XtraScrollable_Thongtin, "TbxRef" + FilDataRows(i)("Ind").ToString)
            Dim TarMin = FindControl(XtraScrollable_Thongtin, "TarMin" + FilDataRows(i)("Ind").ToString)
            Dim TarMax = FindControl(XtraScrollable_Thongtin, "TarMax" + FilDataRows(i)("Ind").ToString)
            If TbxVal IsNot Nothing And TbxRef IsNot Nothing And FilDataRows(i)("DataVal") IsNot System.DBNull.Value Then
                TbxVal.Text = FilDataRows(i)("DataVal")
                TbxRef.Text = FilDataRows(i)("DataRef").ToString
            End If

            If TbxVal IsNot Nothing AndAlso TbxVal.GetType Is GetType(Windows.Forms.ComboBox) Then
                If FilDataRows(i)("DataVal").ToString = "" Then
                    'CType(TbxVal, Windows.Forms.ComboBox).SelectedIndex = 0  'Để trong trường hợp dataVal = "" thì nó chọn luôn giá trị đầu tiên của combo
                    CType(TbxVal, Windows.Forms.ComboBox).Text = ""  'Để trong trường hợp dataVal = "" thì nó chọn luôn giá trị đầu tiên

                Else
                    CType(TbxVal, Windows.Forms.ComboBox).SelectedItem = FilDataRows(i)("DataVal")
                End If
            ElseIf TbxVal IsNot Nothing AndAlso TbxVal.GetType Is GetType(TextEdit) Then
                If FilDataRows(i)("DataVal").ToString = "" Then
                    'CType(TbxVal, Windows.Forms.ComboBox).SelectedIndex = 0  'Để trong trường hợp dataVal = "" thì nó chọn luôn giá trị đầu tiên của combo
                    TbxVal.Text = ""  'Để trong trường hợp dataVal = "" thì nó chọn luôn giá trị đầu tiên

                Else
                    TbxVal.Text = FilDataRows(i)("DataVal")
                End If
            End If
            If TbxRef IsNot Nothing Then
                TbxRef.Text = FilDataRows(i)("DataRef").ToString
            End If
            If TarMin IsNot Nothing Then
                TarMin.Text = FilDataRows(i)("TarMin").ToString
            End If
            If TarMax IsNot Nothing Then
                TarMax.Text = FilDataRows(i)("TarMax").ToString
            End If
            '===========ĐIỀN GIÁ TRỊ CHO CÁC TEXTBOX TRÊN CANCUL FORM
            If FilDataRows(i)("Ind") = CanCulForm.TxtInd.Text Then
                CanCulForm.Txt_TuSo.Text = FilDataRows(i)("TuSo").ToString
                CanCulForm.TxT_MauSo.Text = FilDataRows(i)("MauSo").ToString
                CanCulForm.Txt_KQ.Text = FilDataRows(i)("DataVal").ToString
            End If
        Next


        'Dim Ind = CanCulForm.TxtInd.Text
        'Dim theTable As DataTable = GridControl1.DataSource

        'Dim FilDataRows1 = theTable.Select("Region= " + "'" + cmbRegion.SelectedItem("Region").ToString + "' And mYear = '" + CmbmYear.SelectedItem("mYear").ToString + "' And Ind = '" + Ind + "'")

        ''cmbRegion.DisplayMember = "Region"
        ''CmbmYear.DisplayMember = "mYear"
    End Sub

    Private Shared Function FindControl(ByVal root As Control, ByVal target As String) As Control
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

    Private Sub btnSaveGrV1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveGrV1.Click

        'BtnSaveGrV1.Enabled = False 

        'Dim dtchange As DataTable = dt.GetChanges()

        'If (dtchange Is Nothing) Then     'Xác định xem datagridview có bị change ko
        '    Return
        'Else    'Trường hợp dtchange <> nothing thì mới xet được lệnh dtchange.rows
        '    If dtchange.Rows.Count = 0 Then
        '        Return
        '    End If
        'End If

        If BtnSaveGrV1.Enabled = False Then     ' =true khi keyup duoc kich hoat
            Return
        End If
        BtnSaveGrV1.Enabled = False      'Neu qua duoc If then o tren tuc la SaveBoolean dang =true, =>chuyen ve False
        Dim str = "select DataID, DataVal, DataRef, TarMin, TarMax from " + DataTab   'Chỉ update DataVal và DataRef,TarMin, TarMax
        Dim dt As DataTable = GridControl1.DataSource
        Dim dlgResult As DialogResult = MessageBox.Show("Bạn có muốn lưu thay đổi vào CSDL", "Lưu thay đổi", MessageBoxButtons.YesNoCancel)
        If dlgResult = Windows.Forms.DialogResult.Yes Then
            Dim theresult = MyADOClass.Update_CSDL(GridControl1.DataSource, str)
            ''=======================
            ''Sửa lỗi first Row không thể Update từ textbox bằng lệnh MyADOClass.Update_CSDL. Không hiểu sao sinh ra lỗi này
            ''=> Riêng đối với FirstRow thì phải Update thủ công

            'Dim fstRow As DataRow = GridView1.GetDataRow(0)  'Rows.Item(0)
            'Dim FstID As Long = fstRow.Item("ID") '.Value
            'Dim FstInd As String = fstRow.Item("Ind") '.Value
            'Dim TbxVal As TextEdit = FindControl(XtraScrollable_Thongtin, "TbxVal" + FstInd)
            'Dim TbxRef As TextEdit = FindControl(XtraScrollable_Thongtin, "TbxRef" + FstInd)
            ''Dim result As Boolean = MyADOClass.RunaSQLCommand("UPDATE DataQry SET DataVal = " + TbxVal.Text + " , DataRef = " + TbxRef.Text + "WHERE ID = " + FstID.ToString)
            'Try
            '    If TbxVal.Text <> "" Then
            '        'Dim result As Boolean = MyADOClass.RunaSQLCommand(String.Format("UPDATE DataQry SET DataVal = {0}, DataRef = {1} WHERE ID = {2}", """" + TbxVal.Text + """", """" + TbxRef.Text + """", FstID))
            '        Dim result As Boolean = MyADOClass.RunaSQLCommand(String.Format("UPDATE DataQry SET DataVal = {0} WHERE ID = {1}", """" + TbxVal.Text + """", FstID))
            '    End If
            '    If TbxRef.Text <> "" Then
            '        Dim result As Boolean = MyADOClass.RunaSQLCommand(String.Format("UPDATE DataQry SET DataRef = {0} WHERE ID = {1}", """" + TbxRef.Text + """", FstID))
            '    End If
            'Catch

            'End Try

            ''*****************Update dữ liệu cho GridView2  ' Sẽ gây ra lỗi khi gridcontrol2 chưa được tag lần đầu tiên
            '===>Nên bỏ đi cho đỡ phức tạp
            If GridControl2.Tag <> "" Then
                Dim newTable As DataTable = MyADOClass.DtFromQry(GridControl2.Tag)
                GridView2.GridControl.DataSource = newTable
            End If

            '=======================

        ElseIf dlgResult = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        ElseIf dlgResult = Windows.Forms.DialogResult.No Then
            'Reject any change in datagridview
            dt.RejectChanges()

        End If
    End Sub

    Private Sub btnSaveGrV2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveGrV2.Click
        'BtnSaveGrV2.Enabled = False 

        'Dim dtchange As DataTable = dt.GetChanges()

        'If (dtchange Is Nothing) Then     'Xác định xem datagridview có bị change ko
        '    Return
        'Else    'Trường hợp dtchange <> nothing thì mới xet được lệnh dtchange.rows
        '    If dtchange.Rows.Count = 0 Then
        '        Return
        '    End If
        'End If
        'Có cái này thì đỡ phải save khi ko cần, nhưng
        'If BtnSaveGrV2.Enabled = False  Then   '=False la mac dinh
        '    Return
        'End If
        BtnSaveGrV2.Enabled = False      'Neu qua duoc If then o tren tuc la SaveBoolean dang =true, =>chuyen ve False
        Dim dt As DataTable = GridControl2.DataSource
        Dim str = "select DataID, DataVal, DataRef, TarMin, TarMax From " + DataTab + " "
        Dim dlgResult As DialogResult = MessageBox.Show("Bạn có muốn lưu thay đổi vào CSDL", "Lưu thay đổi", MessageBoxButtons.YesNoCancel)
        If dlgResult = Windows.Forms.DialogResult.Yes Then
            MyADOClass.Update_CSDL(GridControl2.DataSource, str)
            ''===============Update dữ liệu cho GridView1
            Dim newTable As DataTable = MyADOClass.DtFromQry(GridControl1.Tag)
            GridView1.GridControl.DataSource = newTable
            CmbmYear_SelectedIndexChanged(Nothing, Nothing)
            'BtnOK_Click(Nothing, Nothing) Lệnh này ko chạy vì tabChart đang active
        ElseIf dlgResult = Windows.Forms.DialogResult.Cancel Then
            Return
        ElseIf dlgResult = Windows.Forms.DialogResult.No Then
            'Reject any change in datagridview
            Dim dlgResult1 As DialogResult = MessageBox.Show("Bạn thực sự muốn hủy những thay đổi vừa rồi?", "Hủy thay đổi", MessageBoxButtons.YesNo)
            If dlgResult = Windows.Forms.DialogResult.Yes Then
                dt.RejectChanges()
            End If
            Return
        End If

    End Sub

    Private Sub BtnCalculClick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim thename As String = sender.name
        'Truoc tien phai xac dinh Row tren Datagridview ma chua gia tri cua doi tuong hien tai
        Dim urban As String = cmbRegion.SelectedItem("Region").ToString 'Phair de y them Region vi datasource cua cmburban la 1table chu ko phai 1 list
        Dim mYear As String = CmbmYear.SelectedItem("myear").ToString
        Dim Ind As String = thename.Substring(6, thename.Length - 6)    'Bo 6 ky tu dau tien vi ten la: CalculTQ01
        '=============Focus tới Row hiện tại
        For i As Integer = 0 To GridView1.RowCount - 1
            If GridView1.GetDataRow(i).Item("Region").ToString = cmbRegion.SelectedItem("Region").ToString And GridView1.GetDataRow(i).Item("mYear").ToString = CmbmYear.SelectedItem("mYear").ToString And GridView1.GetDataRow(i).Item("Ind").ToString = Ind Then
                'GridView1.SelectRow(i)
                GridView1.FocusedRowHandle = i
                Exit For
            End If
        Next
        '================
        Dim theTable As DataTable = GridControl1.DataSource
        Dim FilDataRows = theTable.Select("Region= " + "'" + urban + "' And mYear = '" + mYear + "' And Ind = '" + Ind + "'")
        'If FilDataRows(0)("Calcul") Is DBNull.Value Then
        '    MsgBox("Không hướng dẫn cách tính cụ thể cho chỉ số này")
        'Else
        '    Dim CalculVal As String = FilDataRows(0)("Calcul")
        '    MessageBox.Show(CalculVal, "Cách tính chỉ số")
        'End If

        'Dim CanculForm As Form = DirectCast(Activator.CreateInstance(Type.[GetType]("Indicator_TN." + "PTKT_HQKTVM1")), Form)

        pnlPanel1.Dock = DockStyle.Top
        CanCulForm.PanelControl2.Controls.Add(pnlPanel1)
        CanCulForm.TxtInd.Text = Ind
        CanCulForm.Txt_TuSo.Text = FilDataRows(0)("TuSo").ToString
        CanCulForm.TxT_MauSo.Text = FilDataRows(0)("MauSo").ToString
        CanCulForm.Txt_KQ.Text = FilDataRows(0)("DataVal").ToString
        CanCulForm.LblTuSo.Text = FilDataRows(0)("TenTuSo").ToString
        CanCulForm.LblMauSo.Text = FilDataRows(0)("TenMauSo").ToString
        ' CanCulForm.RichEditControl1.LoadDocument("ThongtinChiTieu\" + Ind.ToString + ".docx")
        CanCulForm.Text = FilDataRows(0)("Desc").ToString
        CmbmYear.Tag = mYear      'CmbmYear.SelectedIndex
        cmbRegion.Tag = urban
        AddHandler CanCulForm.Closing, AddressOf CanculForm_formClosing
        CanCulForm.ShowDialog()
    End Sub

    'Khong an thua cai keydown
    Private Sub BoTabStop() 'Bo hết tabstop của các control trên form
        For Each contr In Me.Controls   'bỏ Tabstop cho các control trên form
            contr.tabstop = False
            Try
                For Each contr1 In contr.controls   'bỏ Tabstop cho các control trên contr   (contr là 1 containerControl nằm trên form)
                    contr1.tabstop = False
                    Try
                        For Each contr2 In contr1.controls  'bỏ Tabstop cho các control trên contr1   (contr1 là 1 containerControl nằm trên form)
                            contr2.tabstop = False
                            Try
                                For Each contr3 In contr2.controls  'bỏ Tabstop cho các control trên contr1   (contr1 là 1 containerControl nằm trên form)
                                    contr3.tabstop = False
                                    Try
                                        For Each contr4 In contr3.controls  'bỏ Tabstop cho các control trên contr1   (contr1 là 1 containerControl nằm trên form)
                                            contr4.tabstop = False
                                            Try
                                                For Each contr5 In contr4.controls  'bỏ Tabstop cho các control trên contr1   (contr1 là 1 containerControl nằm trên form)
                                                    contr5.tabstop = False
                                                    Try
                                                        For Each contr6 In contr5.controls  'bỏ Tabstop cho các control trên contr1   (contr1 là 1 containerControl nằm trên form)
                                                            contr6.tabstop = False
                                                            Try
                                                                For Each contr7 In contr6.controls  'bỏ Tabstop cho các control trên contr1   (contr1 là 1 containerControl nằm trên form)
                                                                    contr7.tabstop = False
                                                                    Try
                                                                        For Each contr8 In contr7.controls  'bỏ Tabstop cho các control trên contr1   (contr1 là 1 containerControl nằm trên form)
                                                                            contr8.tabstop = False
                                                                            Try
                                                                                For Each contr9 In contr8.controls  'bỏ Tabstop cho các control trên contr1   (contr1 là 1 containerControl nằm trên form)
                                                                                    contr9.tabstop = False
                                                                                    Try
                                                                                        For Each contr10 In contr9.controls  'bỏ Tabstop cho các control trên contr1   (contr1 là 1 containerControl nằm trên form)
                                                                                            contr10.tabstop = False

                                                                                        Next
                                                                                    Catch ex As Exception

                                                                                    End Try
                                                                                Next
                                                                            Catch ex As Exception

                                                                            End Try
                                                                        Next
                                                                    Catch ex As Exception

                                                                    End Try
                                                                Next
                                                            Catch ex As Exception

                                                            End Try
                                                        Next
                                                    Catch ex As Exception

                                                    End Try
                                                Next
                                            Catch ex As Exception

                                            End Try
                                        Next
                                    Catch ex As Exception

                                    End Try
                                Next
                            Catch ex As Exception

                            End Try

                        Next
                    Catch ex As Exception

                    End Try
                Next
            Catch ex As Exception

            End Try

        Next
    End Sub

    Private Function QryfromLeftPanel() As String   '(ByVal ActiveTabPage As String) 
        '=================================
        '1. Tạo Qrystr là Query theo điều kiện checked của LeftPanel
        '2. Tạo myTable từ bảng DataQry Access. Chọn theo điều kiện QryStr trên
        '3. Khởi tạo cho DataGridview từ myTable
        '=================================
        Dim Qrystr As String = ""
        'Dim activeDevGridview As DevExpress.XtraGrid.Views.Grid.GridView
        'If ActiveTabPage = "Thông tin" Then
        '    activeDevGridview = GridView1
        'Else    'ActiveTabPage = "Đồ thị"
        '    activeDevGridview = GridView2
        'End If
        Dim CklUrban, CklYear As DevExpress.XtraEditors.CheckedListBoxControl
        'Đặt Constant trước
        'If ActiveTabPage = "Thông tin" Then
        'If XtraTabControl2.SelectedTabPageIndex = 0 Then   'Trường hợp 1: TabPage1 is actived thì clkUrban là CheckListBox thực thụ
        'Nếu rơi vào THợp 2, TabPage2 activated thì clkUrban  là Combobox
        CklUrban = CheckedListBox_Urban
        CklYear = CheckedListBox_Year
        'Else 'Trường hợp 2: TabPage2 is actived: clkUrban  là Combobox
        '    CklUrban = CheckedListBox_Urban
        '    CklYear = CheckedListBox_Year
        ' End If
        '====
        '====Qry Urban
        Dim QryUrban As String = ""
        If CklUrban.ItemCount = 0 Then 'Lúc này FormLoad chưa đưa dữ liệu và checkListBox 
            Return Qrystr
        End If
        For Each item In CklUrban.CheckedItems '= 0 To CheckedListBox_Urban.ItemCount - 1
            'Qry = Qry + "Urban = " + CheckedListBox_Urban.CheckedItems.Item(i).ToString + " Or "
            QryUrban = QryUrban + "Region = " + """" + item.row(1) + """" + " Or "
        Next
        If QryUrban <> "" Then      '=====================Sưả Where nếu muốn datagridview chỉ nhận  Urban trong cmbRegion
            QryUrban = QryUrban.Substring(0, QryUrban.Length - 4)
        Else
            'MessageBox.Show("Bạn cần chọn ít nhất một Đô thị", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ' Return Qrystr
            'QryUrban = "Region = " + """" + "abcded*%P*&xxxabc" + """" 'Để dùng cho trường hợp ngwoif dùng ko chọn 1 chỉ số nào
        End If

        '====
        '====Qry Indicator
        Dim QryIndicator As String = ""
        'If ActiveTabPage = "Thông tin" Then
        'If XtraTabControl2.SelectedTabPageIndex = 0 Then   'Trường hợp 1: TabPage1 is actived thì clkUrban là CheckListBox thực thụ
        'Nếu rơi vào THợp 2, TabPage2 activated thì clkUrban  là Combobox

        For Each item In CheckedListBox_Ind.CheckedItems '= 0 To CheckedListBox_Urban.ItemCount - 1
            'QryIndicator = QryIndicator + item.Row(1).ToString + " , "      'ITem.Row(1) tức là lấy collumn thứ 1 của Row cho Item ấy. 1 Item này có khá nhiều Row
            QryIndicator = QryIndicator + "Ind = " + """" + item.row("Ind") + """" + " Or "     'ITem.Row(1) tức là lấy collumn thứ 1 của Row cho Item ấy. 1 Item này có khá nhiều Row
        Next
        'Else 'Trường hợp 2: TabPage2 is actived: clkUrban  là Combobox
        '    CklUrban = CheckedListBox_Urban
        '    CklYear = CheckedListBox_Year
        '    QryIndicator = QryIndicator + "Ind = " + """" + cmb_Ind.SelectedItem.Row("Ind").ToString + """" + " OR "      'ITem.Row(1) tức là lấy collumn thứ 1 của Row cho Item ấy. 1 Item này có khá nhiều Row
        ' End If

        If QryIndicator <> "" Then
            QryIndicator = QryIndicator.Substring(0, QryIndicator.Length - 4)
        Else
            'MessageBox.Show("Bạn cần chọn ít nhất một Chỉ số", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '  Return Qrystr
            'QryIndicator = "Ind = " + """" + "abcded*%P*&xxxabc" + """" 'Để dùng cho trường hợp ngwoif dùng ko chọn 1 chỉ số nào
        End If

        '====
        '====Qry Year
        Dim QryYear As String = ""
        For Each item In CklYear.CheckedItems '= 0 To CheckedListBox_Urban.ItemCount - 1
            QryYear = QryYear + "mYear = " + """" + item.row(0) + """" + " Or "
        Next
        If QryYear <> "" Then
            QryYear = QryYear.Substring(0, QryYear.Length - 4)
        Else
            ' MessageBox.Show("Bạn cần chọn ít nhất một Năm dữ liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ' Return Qrystr
            'QryYear = "mYear = " + """" + "abcded*%P*&xxxabc" + """" 'Để dùng cho trường hợp ngwoif dùng ko chọn 1 chỉ số nào

        End If

        'If QryUrban = "" Or QryIndicator = "" Or QryYear = "" Then
        '    Return
        'End If

        If QryUrban <> "" And QryIndicator <> "" And QryYear <> "" Then
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Where (" + QryUrban + ") AND (" + QryIndicator + ") AND (" + QryYear + ")" + "  Order by ID, mYear, Region"
        ElseIf QryUrban = "" And QryIndicator = "" And QryYear = "" Then
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Order by ID, mYear, Region"
        ElseIf QryUrban = "" And QryIndicator = "" Then
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Where (" + QryYear + ")" + "  Order by ID, mYear, Region"
        ElseIf QryUrban = "" And QryYear = "" Then
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Where (" + QryIndicator + ")" + "  Order by ID, mYear, Region"
        ElseIf QryYear = "" And QryIndicator = "" Then
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Where (" + QryUrban + ")" + "  Order by ID, mYear, Region"
        ElseIf (QryUrban = "") Then
            'Qrystr = "Select Urban, Year," + QryIndicator + " from " + myTable + " Where (" + QryUrban + """" + ") AND (" + QryYear + """" + ")"
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Where  (" + QryIndicator + ") AND (" + QryYear + ")" + "  Order by ID, mYear, Region"
            'Qrystr = "Select DataID,ID, mYear, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, Desc,Desc1,DataVal,Unit,DataRef,myNote from " + myDataTablename + " Where (" + QryUrban + ") AND (" + QryIndicator + ") AND (" + QryYear + ")" + "Order by ID,mYear, Region"
        ElseIf QryIndicator = "" Then
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Where (" + QryUrban + ") AND (" + QryYear + ")" + "  Order by ID, mYear, Region"
        ElseIf QryYear = "" Then
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Where (" + QryUrban + ") AND (" + QryIndicator + ") " + "  Order by ID, mYear, Region"

        Else
            'Qrystr = "Select Urban, Year," + QryIndicator + " from " + myTable
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Order by mYear, Region"
        End If
        Return Qrystr
    End Function

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Tao them 1 column cho bang du lieu, column nay dang so va =gia tri cua column dataVal (dang text)
        Dim mysortTable As Sort_Table = New Sort_Table
        Dim dgv2dtable As DataTable = GridControl2.DataSource
        mysortTable.sortDataTable(dgv2dtable, "RegionGrp, ValDlb", True)
        xtraChart.Refresh()





        ''===Filter theo Row có giá trị dataVal <>""
        'Dim FilterView As DataView = dgv2dtable.DefaultView
        'Try
        '    FilterView.RowFilter = "Dataval <> ''"
        'Catch ex As Exception
        'End Try

        'If dgv2dtable.Columns("ValDlb") Is Nothing Then
        '    Try
        '        dgv2dtable.Columns.Add(New DataColumn("ValDlb", Type.GetType("System.Double")))
        '    Catch ex As Exception

        '    End Try

        'End If
        'For r As Integer = 0 To dgv2dtable.Rows.Count - 1
        '    Try
        '        dgv2dtable.Rows(r)("Valdlb") = dgv2dtable.Rows(r)("DataVAL")
        '    Catch ex As Exception

        '    End Try
        'Next
        'Dim mysortTable As Sort_Table = New Sort_Table
        'mysortTable.sortDataTable(dgv2dtable, "RegionGrp", True)


        'gridcontrol2.datasource = dgv2dtable

        'myChart.DataSource = dgv2dtable             'gridcontrol2.datasource


        'If RadIndBased.Checked = True Then
        '    myChart.SeriesDataMember = "Ind"
        '    myChart.SeriesTemplate.ArgumentDataMember = "Region"
        'Else
        '    myChart.SeriesDataMember = "Region"
        '    myChart.SeriesTemplate.ArgumentDataMember = "Ind"

        'End If
        'myChart.SeriesDataMember = "mYear"
        ''chartControl1.Series.Item(0).ValueDataMembers = "data_value"

        ''myChart.SeriesTemplate.ValueDataMembers.AddRange(New String() {"TQ03"})
        ''myChart.SeriesTemplate.ValueDataMembers.AddRange(New String() {ValueDataMembers})
        'myChart.SeriesTemplate.ValueDataMembers.AddRange(New String() {"ValDlb"})
        ''chartControl1.SeriesTemplate.View = New StackedBarSeriesView()
        ''chartControl1.SeriesTemplate.View = New LineSeriesView
        'myChart.SeriesTemplate.View = New LineSeriesView
        'myChart.SeriesNameTemplate.BeginText = ""
        ''Me.Controls.Add(chartControl1)
        'Panel_Chart.Controls.Add(myChart)
        'ChartCustomize(myChart)

        ''myChart.SeriesDataMember = "mYear"
        ' ''chartControl1.Series.Item(0).ValueDataMembers = "data_value"
        ''myChart.SeriesTemplate.ArgumentDataMember = "Region"
        ' ''myChart.SeriesTemplate.ValueDataMembers.AddRange(New String() {"TQ03"})
        ' ''myChart.SeriesTemplate.ValueDataMembers.AddRange(New String() {ValueDataMembers})
        ''myChart.SeriesTemplate.ValueDataMembers.AddRange(New String() {"ValDlb"})
        ' ''chartControl1.SeriesTemplate.View = New StackedBarSeriesView()
        ' ''chartControl1.SeriesTemplate.View = New LineSeriesView
        ''myChart.SeriesTemplate.View = New LineSeriesView
        ''myChart.SeriesNameTemplate.BeginText = "Năm: "
        ' ''Me.Controls.Add(chartControl1)
        'Panel_Chart.Controls.Add(myChart)
        'ChartCustomize(myChart)
        '' myChart.Refresh()

        ''=====Chart Width
        'XtraScrollableControl2.AutoScroll = True
        ''XtraTabControl2.TabPages(1).AutoScroll = True

        'Dim numRow As Integer = GridView2.Rows.Count

        'myChart.Width = numRow * 30

        'If myChart.Width < XtraScrollableControl2.Width - 10 Then
        '    myChart.Width = XtraScrollableControl2.Width - 2
        '    myChart.Height = XtraScrollableControl2.Height - 2
        '    'lblLabel3.Location = New System.Drawing.Point(lblLabel3.Location.X, myChart.Height)
        'Else 'Trường hợp này có thêm VERTICAL scroll nên phải để nhỏ hơn
        '    myChart.Height = XtraScrollableControl2.Height - 20
        '    'lblLabel3.Location = New System.Drawing.Point(lblLabel3.Location.X, myChart.Height + 20)
        'End If
        ''BtnSave1.Location = New System.Drawing.Point(3, myChart.Height - 35)
        ''lblLabel3.Location = New System.Drawing.Point(38, myChart.Height - 20)
        ''cmbViewType.Location = New System.Drawing.Point(185, myChart.Height - 27)

    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Dim Table_Urb As DataTable = CheckedListBox_Urban.DataSource    '.totable    'Phai chuyen thanh Table vi lúc định nghĩa, checklistInd dùng datasource là dataview
        Dim mysortTable = New Sort_Table  'tạo đối tượng từ Class Sort_Table
        Try
            mysortTable.sortDataTable(Table_Urb, "Region", True)
            '        Dim mysortTable As Sort_Table = New Sort_Table
            '        Dim dgv2dtable As DataTable = gridcontrol2.datasource
            '        mysortTable.sortDataTable(dgv2dtable, "ValDlb,Region", True)
            '        myChart.Refresh()
        Catch ex As Exception

        End Try

        'If RadioButton1.Checked = True Then


        '    Try
        '        sortTable.sortDataTable(Table_Urb, "Region", True)
        '        Dim mysortTable As Sort_Table = New Sort_Table
        '        Dim dgv2dtable As DataTable = gridcontrol2.datasource
        '        mysortTable.sortDataTable(dgv2dtable, "ValDlb,Region", True)
        '        myChart.Refresh()
        '    Catch ex As Exception

        '    End Try

        'End If


    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Dim Table_Urb As DataTable = CheckedListBox_Urban.DataSource    '.totable    'Phai chuyen thanh Table vi lúc định nghĩa, checklistInd dùng datasource là dataview
        Dim sortTable = New Sort_Table  'tạo đối tượng từ Class Sort_Table
        If RadioButton2.Checked = True Then


            Try
                sortTable.sortDataTable(Table_Urb, "RegionGrp", True)
                Dim mysortTable As Sort_Table = New Sort_Table
                Dim dgv2dtable As DataTable = GridControl2.DataSource
                mysortTable.sortDataTable(dgv2dtable, "RegionGrp, ValDlb", True)
                xtraChart.Refresh()
            Catch ex As Exception

            End Try

        End If

    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click

        'picRunning.Visible = True
        'picRunning.BringToFront()
        'picRunning.Image = Image.FromFile("E:\MyDocuments\My Pictures\IMG_1216.jpg")
        'picPictureBox1.Image = Image.FromFile(Application.StartupPath + "\Running.gif")
        'Thread.Sleep(100)
        'FrmWaiting.ShowDialog()

        '=================================
        '1. Tạo Qrystr là Query theo điều kiện checked của LeftPanel
        '2. Tạo myTable từ bảng DataQry Access. Chọn theo điều kiện QryStr trên
        '3. Khởi tạo cho DataGridview từ myTable
        '=================================
        Dim activeDevGridview As DevExpress.XtraGrid.Views.Grid.GridView
        Dim activeDevGridControl As DevExpress.XtraGrid.GridControl
        If XtraTabControl2.SelectedTabPageIndex = 0 Then
            If BtnSaveGrV1.Enabled = True Then
                btnSaveGrV1_Click(Nothing, Nothing)
            End If
            activeDevGridview = GridView1

        Else
            If BtnSaveGrV2.Enabled = True Then
                btnSaveGrV2_Click(Nothing, Nothing)
            End If
            activeDevGridview = GridView2
        End If
        activeDevGridControl = activeDevGridview.GridControl
        Dim CklUrban, CklYear As DevExpress.XtraEditors.CheckedListBoxControl
        'Đặt Constant trước
        'If XtraTabControl2.SelectedTabPageIndex = 0 Then   'Trường hợp 1: TabPage1 is actived thì clkUrban là CheckListBox thực thụ
        'Nếu rơi vào THợp 2, TabPage2 activated thì clkUrban  là Combobox
        CklUrban = CheckedListBox_Urban
        CklYear = CheckedListBox_Year
        'Else 'Trường hợp 2: TabPage2 is actived: clkUrban  là Combobox
        '    CklUrban = CheckedListBox_Urban 'CheckedListBox_Urban
        '    CklYear = CheckedListBox_Year   'CheckedListBox_Year
        'End If
        '====
        '====Qry Urban
        Dim QryUrban As String = ""
        For Each item In CklUrban.CheckedItems '= 0 To CheckedListBox_Urban.ItemCount - 1
            'Qry = Qry + "Urban = " + CheckedListBox_Urban.CheckedItems.Item(i).ToString + " Or "
            QryUrban = QryUrban + "Region = " + """" + item.row(1) + """" + " Or "
        Next


        If QryUrban <> "" Then      '=====================Sưả Where nếu muốn datagridview chỉ nhận  Urban trong cmbRegion
            QryUrban = QryUrban.Substring(0, QryUrban.Length - 4)
        Else
            'If XtraTabControl2.SelectedTabPageIndex = 0 Then
            '    MessageBox.Show("Bạn cần chọn ít nhất một Đô thị", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'End If

            'Return
            ''QryUrban = "Region = " + """" + "abcded*%P*&xxxabc" + """" 'Để dùng cho trường hợp ngwoif dùng ko chọn 1 chỉ số nào
        End If

        '====
        '====Qry Indicator
        Dim QryIndicator As String = ""
        'If XtraTabControl2.SelectedTabPageIndex = 0 Then   'Trường hợp 1: TabPage1 is actived thì clkUrban là CheckListBox thực thụ
        'Nếu rơi vào THợp 2, TabPage2 activated thì clkUrban  là Combobox
        If CheckedListBox_Ind.CheckedItems.Count > 0 Then
            For Each item In CheckedListBox_Ind.CheckedItems '= 0 To CheckedListBox_Urban.ItemCount - 1
                'QryIndicator = QryIndicator + item.Row(1).ToString + " , "      'ITem.Row(1) tức là lấy collumn thứ 1 của Row cho Item ấy. 1 Item này có khá nhiều Row
                QryIndicator = QryIndicator + "Ind = " + """" + item.row("Ind") + """" + " Or "     'ITem.Row(1) tức là lấy collumn thứ 1 của Row cho Item ấy. 1 Item này có khá nhiều Row
            Next
        End If

        'Else 'Trường hợp 2: TabPage2 is actived: clkUrban  là Combobox
        '    CklUrban = CheckedListBox_Urban    'CheckedListBox_Urban
        '    CklYear = CheckedListBox_Year   'CheckedListBox_Year
        '    QryIndicator = QryIndicator + "Ind = " + """" + cmb_Ind.SelectedItem.Row("Ind").ToString + """" + " OR "      'ITem.Row(1) tức là lấy collumn thứ 1 của Row cho Item ấy. 1 Item này có khá nhiều Row
        'End If

        If QryIndicator <> "" Then
            QryIndicator = QryIndicator.Substring(0, QryIndicator.Length - 4)
        Else
            'If XtraTabControl2.SelectedTabPageIndex = 0 Then
            '    MessageBox.Show("Bạn cần chọn ít nhất một Chỉ tiêu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'End If

            'Return
            ''QryIndicator = "Ind = " + """" + "abcded*%P*&xxxabc" + """" 'Để dùng cho trường hợp ngwoif dùng ko chọn 1 chỉ số nào
        End If

        '====
        '====Qry Year
        Dim QryYear As String = ""
        For Each item In CklYear.CheckedItems '= 0 To CheckedListBox_Urban.ItemCount - 1
            QryYear = QryYear + "mYear = " + """" + item.row(0) + """" + " Or "
        Next
        If QryYear <> "" Then
            QryYear = QryYear.Substring(0, QryYear.Length - 4)
        Else
            'If XtraTabControl2.SelectedTabPageIndex = 0 Then
            '    MessageBox.Show("Bạn cần chọn ít nhất một Năm dữ liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'End If
            'Return
            ''QryYear = "mYear = " + """" + "abcded*%P*&xxxabc" + """" 'Để dùng cho trường hợp ngwoif dùng ko chọn 1 chỉ số nào

        End If

        'If QryUrban = "" Or QryIndicator = "" Or QryYear = "" Then
        '    Return
        'End If

        Dim Qrystr As String = ""
        If QryUrban <> "" And QryIndicator <> "" And QryYear <> "" Then
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Where (" + QryUrban + ") AND (" + QryIndicator + ") AND (" + QryYear + ")" + "  Order by ID, mYear, Region"
        ElseIf QryUrban = "" And QryIndicator = "" And QryYear = "" Then
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Order by ID, mYear, Region"
        ElseIf QryUrban = "" And QryIndicator = "" Then
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Where (" + QryYear + ")" + "  Order by ID, mYear, Region"
        ElseIf QryUrban = "" And QryYear = "" Then
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Where (" + QryIndicator + ")" + "  Order by ID, mYear, Region"
        ElseIf QryYear = "" And QryIndicator = "" Then
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Where (" + QryUrban + ")" + "  Order by ID, mYear, Region"
        ElseIf (QryUrban = "") Then
            'Qrystr = "Select Urban, Year," + QryIndicator + " from " + myTable + " Where (" + QryUrban + """" + ") AND (" + QryYear + """" + ")"
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Where  (" + QryIndicator + ") AND (" + QryYear + ")" + "  Order by ID, mYear, Region"
            'Qrystr = "Select DataID,ID, mYear, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, Desc,Desc1,DataVal,Unit,DataRef,myNote from " + myDataTablename + " Where (" + QryUrban + ") AND (" + QryIndicator + ") AND (" + QryYear + ")" + "Order by ID,mYear, Region"
        ElseIf QryIndicator = "" Then
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Where (" + QryUrban + ") AND (" + QryYear + ")" + "  Order by ID, mYear, Region"
        ElseIf QryYear = "" Then
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Where (" + QryUrban + ") AND (" + QryIndicator + ") " + "  Order by ID, mYear, Region"

        Else
            'Qrystr = "Select mYear, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, Desc,Desc1,DataVal,Unit,DataRef,myNote,DataID,ID from  DataQry Order by ID, mYear, Region"
            'Qrystr = "Select Urban, Year," + QryIndicator + " from " + myTable
            Qrystr = "Select DataID,ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Order by mYear, Region"
            'Qrystr = "Select DataID,ID, mYear, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, Desc,Desc1,DataVal,Unit,DataRef,myNote from " + myDataTablename
        End If
        ''MyADOClass.KetNoi_Open()
        Dim myTable As DataTable = MyADOClass.DtFromQry(Qrystr)
        activeDevGridControl.DataSource = myTable
        activeDevGridControl.Tag = Qrystr
        'MyADOClass.Load_data_Grid(activeDevGridview, myTable)
        ''MyADOClass.KetNoi_Close()
        ''Sắp xếp thứ tự lại các cột vì Query từ mdb bị lộn thứ tự
        'myTable.Columns("mYear").SetOrdinal(1)
        'myTable.Columns("Region").SetOrdinal(2)
        'myTable.Columns("Theme").SetOrdinal(3)
        'myTable.Columns("Desc").SetOrdinal(4)
        'myTable.Columns("DataVal").SetOrdinal(5)
        'myTable.Columns("unit").SetOrdinal(6)
        ''GridView1.Columns("DataRef").set
        'myTable.Columns("DataRef").SetOrdinal(7)
        'myTable.Columns("Ind").SetOrdinal(8)

        'Dim colDecimal As DataColumn = New DataColumn '("DecimalCol")
        ''colDecimal.DataType = System.Type.GetType("System.Decimal")
        ' ''colDecimal.ColumnName = "myCol"
        ''colDecimal.Caption = "fdw"
        'myTable.Columns.Add(colDecimal)


        ''For r As Integer = 0 To myTable.Rows.Count - 1
        ''    Try
        ''        myTable.Rows(r)(colDecimal) = CType(myTable.Rows(r)("DataRef"), Decimal)
        ''    Catch
        ''    End Try
        ''Next
        ''myTable.Columns.Item(0).
        ''Dim a = myTable.Rows.Count
        ''Me.GridView1.Columns("DataVal").DefaultCellStyle.Format = Decimal.ToString("G", System.Globalization.NumberFormatInfo.CurrentInfo)

        If (myTable.Rows.Count <> 0) Then
            activeDevGridControl.DataSource = myTable
            'MyADOClass.Load_data_Grid(activeDevGridview, myTable)
            'formatdgv1(GridView1 )

        Else
            MessageBox.Show("Không có dữ liệu thỏa mãn điều kiện bạn chọn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return

        End If



        '==============================================================='===============================================================
        '===============================================================
        '+++_2. Thực hiện add Controls vào Page1 hoặc Chart vào Page2

        '=====================================
        '====Trường hợp 1: TabPage1 is actived => Add Controls
        '1. Tạo IndicatorTable từ bảng Ind Access. Chọn theo điều kiện checked của LeftPanel và Sắp xếp theo ID
        '2. Tạo DataRef Table từ bảng DataQry Access. Lấy tất DataRef nhưng lọc distinct trường DataRef
        '=====================================
        If XtraTabControl2.SelectedTabPageIndex = 0 Then   'Trường hợp 1: TabPage1 is actived
            AddControl(myTable)
        Else 'Trường hợp 2: TabPage2 is actived
            '=====================================
            '====Trường hợp 2: TabPage2 is actived - Chart
            '=====================================
            'AddChart(XtraScrollableControl2, CheckedListBox_Ind.SelectedItem.row(1).ToString)    'row(1).ToString =>Ind
            AddChart(XtraScrollable_Chart)

        End If
        picRunning.Visible = False
    End Sub

    Private Sub CanculForm_formClosing(sender As Object, e As FormClosingEventArgs) 'Handles Me.FormClosing
        SplitContainer2.Panel2.Controls.Add(pnlPanel1)
        pnlPanel1.Dock = DockStyle.Bottom
        pnlPanel1.SendToBack()
        Dim CurRegion = GridView1.GetDataRow(0)("Region")
        Dim CurYear = GridView1.GetDataRow(0)("mYear")
        For i As Integer = 0 To cmbRegion.Items.Count - 1
            If cmbRegion.Items(i)("Region").ToString = CurRegion Then
                cmbRegion.SelectedIndex = i
                'CmbmYear.
                Exit For
            End If
        Next
        For i As Integer = 0 To CmbmYear.Items.Count - 1
            If CmbmYear.Items(i)("mYear").ToString = CurYear Then
                CmbmYear.SelectedIndex = i
                'CmbmYear.
                Exit For
            End If
        Next
    End Sub

    Private Sub validInputValue(sender As Object, e As KeyPressEventArgs)
        Dim CurRowHandle As Integer = sender.defaultview.FocusedRowHandle
        Dim thenote As String = sender.defaultview.GetDataRow(CurRowHandle).Item("mynote").ToString
        Dim mycellTextEdit As TextEdit = TryCast(sender.defaultview.ActiveEditor, TextEdit)      'Chuyển Cell đang active sang textEdit để chạy AllowtextboxValue
        Try
            Select Case thenote
                Case "Nhóm 1: Giá trị số nguyên"
                    AllowTextboxvalue.AllowIntegerNumber(mycellTextEdit, e)
                    ' AllowTextboxvalue.NumberFormat(mycellTextEdit, 3)
                Case "Nhóm 2: Giá trị số thực"
                    AllowTextboxvalue.AllowFloatNumber(mycellTextEdit, e)
                    'AllowTextboxvalue.NumberFormat(mycellTextEdit, 3)
                Case "Nhóm 3: Giá trị phần trăm"
                    AllowTextboxvalue.AllowPercent(mycellTextEdit, e)
            End Select
        Catch ex As Exception

        End Try


    End Sub
    '===========Cái này để hiển thị Tooltip cho SubmenuItems// Hoặc chỉ cần dùng lệnh BarManager1.ShowScreenTipsInMenus trong form_Load là xong
    Private Sub BarManager1_HighlightedLinkChanged(sender As Object, e As HighlightedLinkChangedEventArgs) Handles BarManager1.HighlightedLinkChanged
        'toolTipController1.HideHint()
        'If e.Link Is Nothing Then
        '    Return
        'End If
        'Dim link As BarSubItemLink = TryCast(e.PrevLink, BarSubItemLink)
        'If link IsNot Nothing Then
        '    link.CloseMenu()
        'End If
        'Try
        '    Dim Info = New ToolTipControlInfo(e.Link.Item, e.Link.Item.SuperTip.ToString)
        '    ToolTipController1.ShowHint(Info)
        'Catch ex As Exception

        'End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Dim TrisTreeListFromDT As TrisTreeListFromDataTable = New TrisTreeListFromDataTable
        'TrisTreeListFromDT.HaiCap(TreeList2, "", "Theme", "SubTheme", "Ind", "ID")

        Dim mytrisListview As New Class1()
        '==Nhớ phải AADD Collumn	
        Dim tlCollumn As New DevExpress.XtraTreeList.Columns.TreeListColumn()
        TreeList1.Columns.AddRange(New DevExpress.XtraTreeList.Columns.TreeListColumn() {tlCollumn})
        mytrisListview.HaiCap(TreeList1, "abc", "RegionGrp", "Region", "zLuuUrban", "UrbanID")
        TreeList1.OptionsBehavior.Editable = False
        TreeList1.OptionsView.ShowCheckBoxes = True
        TreeList1.OptionsBehavior.AllowIndeterminateCheckState = True
        TreeList1.OptionsBehavior.AllowRecursiveNodeChecking = True
        TreeList1.OptionsBehavior.AutoChangeParent = True
        'TreeList2.OptionsView.ShowCheckBoxes = True
        'TreeList2.OptionsBehavior.AllowIndeterminateCheckState = True
    End Sub

#Region "XỬ LÝ TAB THÔNG TIN"
    Sub AddControl(myTable As DataTable)

        XtraScrollable_Thongtin.Controls.Clear()

        Dim tabindex As Integer = 0

        Dim QryIndicator1 As String = ""
        For Each item In CheckedListBox_Ind.CheckedItems '= 0 To CheckedListBox_Urban.ItemCount - 1
            QryIndicator1 = QryIndicator1 + "Ind = " + """" + item.row("ind") + """" + " Or "
        Next
        If QryIndicator1 <> "" Then
            QryIndicator1 = "Select *  From " + IndTab + "  where " + QryIndicator1.Substring(0, QryIndicator1.Length - 4) + "Order by ID"
        Else
            QryIndicator1 = "Select *  From " + IndTab + " Order by ID"
        End If
        'MyADOClass.KetNoi_Open()
        Dim IndicatorTable As DataTable = MyADOClass.DtFromQry(QryIndicator1)

        Dim Y As Integer = 10
        XtraScrollable_Thongtin.AutoScroll = True

        Dim autocompleteString As New AutoCompleteStringCollection

        Dim DataRefTable As DataTable = MyADOClass.DtFromQry("select distinct DataRef From " + DataQryTab + " ")

        For i As Integer = 0 To DataRefTable.Rows.Count - 1
            If DataRefTable.Rows(i)("DataRef").ToString = "" Then    ' Nothing 
                Continue For   'Tiếp tục vòng lặp và bỏ qua giá trị null
            Else
                autocompleteString.Add(DataRefTable.Rows(i)("DataRef"))
            End If
        Next
        'string[] postSource = dtPosts
        '.AsEnumerable()
        '        .Select<System.Data.DataRow, String>(x => x.Field<String>("Title"))
        '        .ToArray();\
        Dim FstTbxVal As DevExpress.XtraEditors.TextEdit = New TextEdit      'Sử dụng để Focus vào textbox đầu tiên được add
        For r As Integer = 0 To IndicatorTable.Rows.Count - 1
            'myTable.Rows(r)("Ind").ToString()
            '=======MyLabel========
            Dim myLabel As LabelControl = New LabelControl  'Label
            myLabel.Name = "Lbl" + IndicatorTable.Rows(r)("Ind").ToString()
            myLabel.Text = IndicatorTable.Rows(r)("IndicatorTN").ToString()
            ToolTip1.SetToolTip(myLabel, IndicatorTable.Rows(r)("Desc").ToString())
            myLabel.ShowToolTips = True
            myLabel.Location = New Point(15, Y)
            'myLabel.Font = New System.Drawing.Font("Times New Roman", 9.0!)

            XtraScrollable_Thongtin.Controls.Add(myLabel)

            '=========================
            '=======ValTextBox========
            '=========================
            '---Xét Note xem Ind thuộc nhóm nào để định nghĩa là textbox hay combo
            Dim Ind As String = IndicatorTable.Rows(r)("Ind").ToString()
            Dim FilDataRows = IndicatorTable.Select("Ind = " + "'" + Ind + "'")
            'gridcontrol1.datasource = Table_Indicator
            Dim thenote As String = FilDataRows(0)("mynote").ToString
            'Dim TbxVal As Control
            Dim TbxVal As DevExpress.XtraEditors.TextEdit = New TextEdit
            'TbxVal.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[True]
            'TbxVal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
            'TbxVal.Properties.Mask.BeepOnError = True
            Select Case thenote
                'Case "Nhóm 1: Giá trị số nguyên"
                '    TbxVal.Properties.Mask.EditMask = "n0"
                'Case "Nhóm 2: Giá trị số thực"
                '    TbxVal.Properties.Mask.EditMask = "f3"
                'Case "Nhóm 3: Giá trị phần trăm"
                '    TbxVal.Properties.Mask.EditMask = "f3"
                Case "Nhóm 4: Có/Không"
                    ' ''Dim CKautocompleteString As New AutoCompleteStringCollection
                    ' ''CKautocompleteString.AddRange(New String() {"Có", "Không"})
                    ' ''TbxVal.MaskBox.AutoCompleteCustomSource = CKautocompleteString    '((From row In IndicatorTable.Rows.Cast(Of DataRow)() Select CStr(row("Desc"))).ToArray())
                    ' ''TbxVal.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    ' ''TbxVal.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource                        'TbxVal = New Windows.Forms.ComboBox

                    '' ''If CType(TbxVal, Windows.Forms.ComboBox).Items.Count < 1 Then
                    '' ''    CType(TbxVal, Windows.Forms.ComboBox).Items.AddRange(New String() {"Có", "Không"})
                    '' ''    'CType(TbxVal, Windows.Forms.ComboBox).Items.Add("Không")
                    '' ''End If
                    ' '' ''CType(TbxVal, Windows.Forms.ComboBox).DropDownStyle = ComboBoxStyle.DropDownList
                    ' '' ''CType(TbxVal, Windows.Forms.ComboBox).SelectedIndex = 0
                    ' '' ''Case "Nhóm 2: Giá trị phần trăm"
                    ' '' ''Case "Nhóm 3: Giá trị số thực"
                    ' '' ''Case "Nhóm 1: Giá trị số nguyên"
                Case "Nhóm 5: Cấp quản lý"
                    ' ''Dim CapQLautocompleteString As New AutoCompleteStringCollection
                    ' ''CapQLautocompleteString.AddRange(New String() {"TP Trung ương", "TP Tỉnh lỵ", "Thị xã Tỉnh lỵ", "TP thuộc tỉnh", "Thị xã thuộc tỉnh"})
                    ' ''TbxVal.MaskBox.AutoCompleteCustomSource = CapQLautocompleteString
                    ' ''TbxVal.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    ' ''TbxVal.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource                      'TbxVal = New Windows.Forms.ComboBox

                    '' ''TbxVal = New Windows.Forms.ComboBox
                    '' ''If CType(TbxVal, Windows.Forms.ComboBox).Items.Count < 1 Then
                    '' ''    CType(TbxVal, Windows.Forms.ComboBox).Items.Add("TP Trung ương")
                    '' ''    CType(TbxVal, Windows.Forms.ComboBox).Items.Add("TP Tỉnh lỵ")
                    '' ''    CType(TbxVal, Windows.Forms.ComboBox).Items.Add("Thị xã Tỉnh lỵ")
                    '' ''    CType(TbxVal, Windows.Forms.ComboBox).Items.Add("TP thuộc tỉnh")
                    '' ''    CType(TbxVal, Windows.Forms.ComboBox).Items.Add("Thị xã thuộc tỉnh")
                    '' ''End If
                    ' '' ''CType(TbxVal, Windows.Forms.ComboBox).DropDownStyle = ComboBoxStyle.DropDownList
                    ' '' ''CType(TbxVal, Windows.Forms.ComboBox).SelectedIndex = 0
                Case "Nhóm 6: Loại đô thị"
                    ' '' ''TbxVal = New Windows.Forms.ComboBox
                    ' '' ''If CType(TbxVal, Windows.Forms.ComboBox).Items.Count < 1 Then
                    ' '' ''    CType(TbxVal, Windows.Forms.ComboBox).Items.AddRange(New String() {"Loại đặc biệt", "Loại I", "Loại II", "Loại III", "Loại IV"})

                    ' '' ''    'CType(TbxVal, Windows.Forms.ComboBox).Items.Add("Loại đặc biệt")
                    ' '' ''    'CType(TbxVal, Windows.Forms.ComboBox).Items.Add("Loại I")
                    ' '' ''    'CType(TbxVal, Windows.Forms.ComboBox).Items.Add("Loại II")
                    ' '' ''    'CType(TbxVal, Windows.Forms.ComboBox).Items.Add("Loại III")
                    ' '' ''    'CType(TbxVal, Windows.Forms.ComboBox).Items.Add("Loại IV")
                    ' '' ''End If
                    '' '' ''CType(TbxVal, Windows.Forms.ComboBox).DropDownStyle = ComboBoxStyle.DropDownList
                    '' '' ''CType(TbxVal, Windows.Forms.ComboBox).SelectedIndex = 0
                    '' ''Dim LoaiDTautocompleteString As New AutoCompleteStringCollection
                    '' ''LoaiDTautocompleteString.AddRange(New String() {"Loại đặc biệt", "Loại I", "Loại II", "Loại III", "Loại IV"})
                    '' ''TbxVal.MaskBox.AutoCompleteCustomSource = LoaiDTautocompleteString
                    '' ''TbxVal.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                    '' ''TbxVal.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource                        'TbxVal = New Windows.Forms.ComboBox


                    'Case "Nhóm 7: Ký tự"
                Case Else
                    ' ''TbxVal = New TextBox
                    ' ''TbxVal.Font = New System.Drawing.Font("Times new Roman", 11.0!, FontStyle.Bold)
                    ''TbxVal.Font = New System.Drawing.Font("Arial", 9, FontStyle.Bold) '(TbxVal.Font.FontFamily, TbxVal.Font.Size, FontStyle.Bold)
                    ' ''TbxVal.Text.Format("#,###.00")

                    ' ''If (TbxVal.Name = "ValTQ01") Then
                    ' ''AddHandler TbxVal.LostFocus, AddressOf TextLostFocus

                    ' ''AddHandler CType(TbxVal, TextBox).Leave, AddressOf TbxValLeave    'Lạ lắm, leave thì tốt, lostfocus là nó quay vòng vòng cái message

            End Select
            TbxVal.ForeColor = Color.Blue
            'TbxVal.BackColor = Color.Bisque
            AddHandler TbxVal.Leave, AddressOf TbxLeave
            AddHandler TbxVal.KeyUp, AddressOf TbxValRef_Keyup
            AddHandler TbxVal.Enter, AddressOf TbxValRef_Enter

            AddHandler CType(TbxVal, TextEdit).KeyPress, AddressOf TbxValKeypress
            AddHandler TbxVal.Spin, AddressOf TbxVal_Spin   'Không cho lăn chuột để tăng, giảm số trên textboxVAL
            'If TypeOf TbxVal Is System.Windows.Forms.ComboBox Then

            TbxVal.Location = New System.Drawing.Point(240, Y - 5)
            TbxVal.Size = New System.Drawing.Size(35, 25)
            'If r < 10 Then
            '    TbxVal.Name = "TbxVal" + IndicatorTable.Rows(r)("Ind").ToString() + "00" + r.ToString
            'ElseIf r >= 10 And r <= 100 Then
            '    TbxVal.Name = "TbxVal" + IndicatorTable.Rows(r)("Ind").ToString() + "0" + r.ToString
            'Else
            'TbxVal.Name = "TbxVal" + IndicatorTable.Rows(r)("Ind").ToString() + r.ToString
            'End If
            TbxVal.Name = "TbxVal" + IndicatorTable.Rows(r)("Ind").ToString()
            TbxVal.ToolTip = "Giá trị thực của chỉ tiêu"

            XtraScrollable_Thongtin.Controls.Add(TbxVal)
            If r = 0 Then   'And TbxVal.GetType Is GetType(TextBox) Then   'Chỉ được thực hiện nếu đặt sau lệnh Add lên Form
                FstTbxVal = TbxVal  'CType(TbxVal, TextBox)
            End If

            '===========================
            '=======CalCulButton========
            '===========================
            Dim BtnCalcul As SimpleButton = New SimpleButton
            BtnCalcul.Location = New System.Drawing.Point(278, Y - 6)
            BtnCalcul.Size = New System.Drawing.Size(23, 24)
            BtnCalcul.Name = "Calcul" + IndicatorTable.Rows(r)("Ind").ToString()
            BtnCalcul.ToolTip = "Cách tính giá trị thực tế chỉ tiêu"
            'BtnCalcul.Image = VUI.My.Resources.Resources.info_2_

            '  BtnCalcul.BackColor = Nothing
            BtnCalcul.Appearance.BackColor = System.Drawing.Color.Transparent
            BtnCalcul.Image = Global.Indicator_TN.My.Resources.Resources.info_16x16  'System.Drawing.Image.FromFile(Application.StartupPath + "\ico\info.png")
            BtnCalcul.ImageLocation = ImageLocation.MiddleCenter
            BtnCalcul.ForeColor = System.Drawing.Color.Transparent
            'BtnCalcul.BackgroundImageLayout = ImageLayout.Stretch
            BtnCalcul.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
            ' BtnCalcul.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003
            'BtnCalcul.Image = System.Drawing.Image.FromFile(Application.StartupPath + "\ico\info.png")
            'BtnCalcul.ImageLayout = ImageLayout.Stretch

            AddHandler BtnCalcul.Click, AddressOf BtnCalculClick

            XtraScrollable_Thongtin.Controls.Add(BtnCalcul)
            BtnCalcul.TabStop = False
            'RichTextBox1.
            '===========================
            '=======REF TEXTBOX========
            '===========================
            Dim TbxRef As TextEdit = New TextEdit
            TbxRef.Location = New System.Drawing.Point(315, Y - 5)
            TbxRef.Size = New System.Drawing.Size(200, 25)
            TbxRef.Name = "TbxRef" + IndicatorTable.Rows(r)("Ind").ToString()
            TbxRef.ToolTip = "Nguồn thu thập giá trị chỉ tiêu"
            'TbxRef.Anchor = AnchorStyles.Left
            'TbxVal.AutoCompleteMode = AutoCompleteMode.Suggest
            ' '' '' '' ''++++++++++Chuyen phan From row... ra ngoaif vong lap For
            TbxRef.MaskBox.AutoCompleteCustomSource = autocompleteString    '((From row In IndicatorTable.Rows.Cast(Of DataRow)() Select CStr(row("Desc"))).ToArray())
            TbxRef.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            TbxRef.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource
            XtraScrollable_Thongtin.Controls.Add(TbxRef)
            TbxRef.ForeColor = Color.Blue
            'TbxVal.BackColor = Color.Bisque
            AddHandler TbxRef.Leave, AddressOf TbxLeave
            AddHandler TbxRef.Enter, AddressOf TbxValRef_Enter
            AddHandler TbxRef.KeyUp, AddressOf TbxValRef_Keyup

            '===========================
            '=======TbxVal_SrcButton========
            '===========================
            Dim Btn_Valsrc As SimpleButton = New SimpleButton
            Btn_Valsrc.Location = New System.Drawing.Point(518, Y - 6)
            Btn_Valsrc.Size = New System.Drawing.Size(24, 23)
            Btn_Valsrc.Name = "ValSrc" + IndicatorTable.Rows(r)("Ind").ToString()
            Btn_Valsrc.ToolTip = "Dữ liệu nguồn tính giá trị thực tế cho chỉ tiêu"

            'Btn_Valsrc.BackColor = Nothing
            Btn_Valsrc.Appearance.BackColor = System.Drawing.Color.Transparent
            'Btn_Valsrc.BackgroundImage = System.Drawing.Image.FromFile(Application.StartupPath + "\ico\info.png")
            Btn_Valsrc.Image = Global.Indicator_TN.My.Resources.Resources.info_16x16
            Btn_Valsrc.BackgroundImageLayout = ImageLayout.Stretch
            Btn_Valsrc.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
            '  AddHandler Btn_Valsrc.Click, AddressOf Btn_ValsrcClick      'Chuột trái thì chọn file; Chuột phải thì mở file
            XtraScrollable_Thongtin.Controls.Add(Btn_Valsrc)
            Btn_Valsrc.TabStop = False

            '===========================
            '=======TarMin TEXTBOX========
            '===========================
            Dim TarMin As TextEdit = New TextEdit
            TarMin.Location = New System.Drawing.Point(580, Y - 5)
            TarMin.Size = New System.Drawing.Size(35, 23)
            TarMin.Name = "TarMin" + IndicatorTable.Rows(r)("Ind").ToString()
            TarMin.ForeColor = Color.Blue
            TarMin.ToolTip = "Giá trị mục tiêu cận dưới"

            'TarMin.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[True]
            'TarMin.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
            'TarMin.Properties.Mask.BeepOnError = True

            AddHandler TarMin.KeyUp, AddressOf TbxValRef_Keyup
            AddHandler TarMin.KeyPress, AddressOf TbxValKeypress
            AddHandler TarMin.Spin, AddressOf TbxVal_Spin   'Không cho lăn chuột để tăng, giảm số trên textboxVAL
            AddHandler TarMin.Enter, AddressOf TbxValRef_Enter
            AddHandler TarMin.Leave, AddressOf TbxLeave

            XtraScrollable_Thongtin.Controls.Add(TarMin)

            '===========================
            '=======Button_TarMinSrc========
            '===========================
            Dim Btn_TarMinSrc As SimpleButton = New SimpleButton
            Btn_TarMinSrc.Location = New System.Drawing.Point(620, Y - 6)
            Btn_TarMinSrc.Size = New System.Drawing.Size(24, 23)
            Btn_TarMinSrc.Name = "TarMinSrc" + IndicatorTable.Rows(r)("Ind").ToString()
            Btn_TarMinSrc.ToolTip = "Nguồn dữ liệu xác định giá trị mục tiêu cận dưới"

            'Btn_TarMinSrc.BackColor = Nothing
            Btn_TarMinSrc.Appearance.BackColor = System.Drawing.Color.Transparent
            'Btn_TarMinSrc.BackgroundImage = System.Drawing.Image.FromFile(Application.StartupPath + "\ico\info.png")
            Btn_TarMinSrc.Image = Global.Indicator_TN.My.Resources.Resources.info_16x16
            Btn_TarMinSrc.BackgroundImageLayout = ImageLayout.Stretch
            Btn_TarMinSrc.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
            'AddHandler Btn_Targesrc.Click, AddressOf Btn_TargeSrcClick      'Chuột trái thì chọn file; Chuột phải thì mở file

            XtraScrollable_Thongtin.Controls.Add(Btn_TarMinSrc)
            Btn_TarMinSrc.TabStop = False
            '===========================
            '=======TarMax TEXTBOX========
            '===========================
            Dim TarMax As TextEdit = New TextEdit
            TarMax.Location = New System.Drawing.Point(655, Y - 5)
            TarMax.Size = New System.Drawing.Size(35, 23)
            TarMax.Name = "TarMax" + IndicatorTable.Rows(r)("Ind").ToString()
            TarMax.ToolTip = "Giá trị mục tiêu cận trên"
            TarMax.ForeColor = Color.Blue

            'TarMax.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.[True]
            'TarMax.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric
            'TarMax.Properties.Mask.BeepOnError = True
            'Select Case thenote
            '    Case "Nhóm 1: Giá trị số nguyên"
            '        TarMax.Properties.Mask.EditMask = "n0"
            '    Case "Nhóm 2: Giá trị số thực"
            '        TarMax.Properties.Mask.EditMask = "f3"
            '    Case "Nhóm 3: Giá trị phần trăm"
            '        TarMax.Properties.Mask.EditMask = "f3"
            'End Select

            AddHandler TarMax.KeyUp, AddressOf TbxValRef_Keyup
            AddHandler TarMax.KeyPress, AddressOf TbxValKeypress
            AddHandler TarMax.Spin, AddressOf TbxVal_Spin   'Không cho lăn chuột để tăng, giảm số trên textboxVAL
            AddHandler TarMax.Enter, AddressOf TbxValRef_Enter
            AddHandler TarMax.Leave, AddressOf TbxLeave

            XtraScrollable_Thongtin.Controls.Add(TarMax)
            '===========================
            '=======Button_TarMaxSrc========
            '===========================
            Dim Btn_TarMaxSrc As SimpleButton = New SimpleButton
            Btn_TarMaxSrc.Location = New System.Drawing.Point(692, Y - 6)
            Btn_TarMaxSrc.Size = New System.Drawing.Size(24, 23)
            Btn_TarMaxSrc.Name = "TarMaxSrc" + IndicatorTable.Rows(r)("Ind").ToString()
            Btn_TarMaxSrc.ToolTip = "Nguồn dữ liệu xác định giá trị mục tiêu cận dưới"
            Btn_TarMaxSrc.Appearance.BackColor = System.Drawing.Color.Transparent
            Btn_TarMaxSrc.Image = Global.Indicator_TN.My.Resources.Resources.info_16x16
            Btn_TarMaxSrc.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
            'Btn_TarMaxSrc.BackColor = Nothing
            'Btn_TarMaxSrc.BackgroundImage = Global.Indicator_TN.My.Resources.Resources.info_16x16   'System.Drawing.Image.FromFile(Application.StartupPath + "\ico\info.png")
            ''BtnCalcul.BackgroundImage = My.Resources.information
            'Btn_TarMaxSrc.BackgroundImageLayout = ImageLayout.Stretch
            ''AddHandler Btn_Targesrc.Click, AddressOf Btn_TargeSrcClick      'Chuột trái thì chọn file; Chuột phải thì mở file

            XtraScrollable_Thongtin.Controls.Add(Btn_TarMaxSrc)
            Btn_TarMaxSrc.TabStop = False

            '=========
            Y = Y + 30
        Next

        '=====================================================
        '=======Khởi tạo giá trị cho CmbRegion và CmbmYear
        '=====================================================
        'cmbRegion.DataSource = gridcontrol1.datasource
        'cmbRegion.DisplayMember = "Region"
        'cmbRegion.SelectedIndex = 0
        'CmbmYear.DataSource = gridcontrol1.datasource
        'CmbmYear.DisplayMember = "mYear"
        'CmbmYear.SelectedIndex = 0
        'DataTable dt = dataView.ToTable(true, new String[]("distinctField1", "distinctField2"));
        'savebolean = False   'Để không hỏi có save không khi thay đổi cmbUrbanIndex từ lệnh cmbRegion.SelectedItem = 0
        Dim distinctdataview As DataView = New DataView(myTable)
        ' Dim DisctinctColumn As String() = New String() {"Region","mYear"}    'Dùng String() nếu muốn Distinct theo nhiều cột
        Dim DisctinRegionTable As DataTable = distinctdataview.ToTable(True, "Region")
        cmbRegion.DataSource = DisctinRegionTable '.Columns("Region")
        cmbRegion.DisplayMember = "Region"
        cmbRegion.SelectedItem = 0

        Dim DisctinmYearTable As DataTable = distinctdataview.ToTable(True, "mYear")
        CmbmYear.DataSource = DisctinmYearTable '.Columns("mYear")
        CmbmYear.SelectedItem = 0
        CmbmYear.DisplayMember = "mYear"
        '============Tạo Dataview Filter theo Urban và mYear cho GridView1
        Dim FilterView As DataView = myTable.DefaultView
        FilterView.RowFilter = "Region = '" + cmbRegion.SelectedItem("Region") + "' AND mYear = '" + CmbmYear.SelectedItem("mYear") + "'"

        '=================================
        'Dim DisctinctColumn As String() = New String() {"Region", "mYear"}    'Dùng String() nếu muốn Distinct theo nhiều cột
        'MyADOClass.Load_data_Grid(GridView1, distinctdataview.ToTable(True, New String() {"Region", "mYear"}))

        'Phải focus tại đây, vì nếu đặt ở trên thì mấy cái Cmbobox cmbmYear và CmbRegion sẽ chiếm focus
        FstTbxVal.Focus() 'Đặt Focus mouse lên DataVal Textbox đầu tiên

        'picRunning.SendToBack()

        'RichTextBox1.Text = My.Computer.FileSystem.ReadAllText("D:\MyDocuments\Visual Studio 2013\Projects\_Experiences" & "\experience.rtf")
        'RichEditControl1.Text = My.Computer.FileSystem.ReadAllText("D:\MyDocuments\Visual Studio 2013\Projects\_Experiences" & "\experience.doc")
        'RichEditControl1.LoadDocument("D:\MyDocuments\Visual Studio 2013\Projects\_Experiences" & "\experience.docx")
        ' RichTextBox1.LoadFile("D:\MyDocuments\Visual Studio 2013\Projects\_Experiences" & "\experience.rtf")

    End Sub
    Private Sub TbxValRef_Keyup(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If (e.KeyCode >= 96 And e.KeyCode <= 105) Or (e.KeyCode >= 48 And e.KeyCode <= 57) Or (e.KeyCode = Keys.Delete Or e.KeyCode = Keys.Back) Then
            BtnSaveGrV1.Enabled = True

        End If
        Dim thename As String = sender.name.ToString.ToLower
        If thename.Contains("tbxref") Then
            BtnSaveGrV1.Enabled = True
        End If
        'If (e.KeyCode = Keys.Enter) Then
        '    ''Chuyển enter thành Tab
        '    If e.Shift Then
        '        ' Reverse tabbing if Shift key is held down.
        '        Me.ProcessTabKey(False)
        '    Else
        '        ' Forward tabbing if Shift key is not held down.
        '        Me.ProcessTabKey(True)
        '    End If
        'End If
        '============
        '==================Hiển thị luôn giá trị nhập lên GridView1
        '=============
        'If sender.text = "" Then    'Tránh trường hợp không nhập giá trị mà cũng update
        '    Return						'Truoc de o TbxLeave thi can, gio thi ko can nua
        'End If
        Dim CurInd = sender.name.Substring(6, sender.name.length - 6)
        'Dim curRow As DataGridViewRow = GridView1.SelectedRows(0)

        'Dim HandleRow As Int16 = GridView1.GetSelectedRows(0)
        'Dim curRow As DataRow = GridView1.GetDataRow(HandleRow)

        'Dim fstRow As DataRow = GridView1.GetDataRow(0)  'Rows.Item(0)
        'Dim curRow As DataGridViewRow = GridView1.GetSelectedRows(0)
        '===Phai select lai de tranh truong hop User Select 1 row <> row hien tai dang nhap lieu (Ind Tbx <> Ind Row)
        '===Không cần thiết select lại đâu vì User mà select 1 Row <> rồi thì Tbx hiện tại còn focus đâu, chỉ cần dùng DIm trên
        'Dim curRow As DataGridViewRow = _
        '        (From r As DataGridViewRow In GridView1.Rows _
        '         Where r.Cells("Region").Value.ToString() = cmbRegion.SelectedItem("Region").ToString And _
        '               r.Cells("mYear").Value.ToString() = CmbmYear.SelectedItem("mYear").ToString And _
        '               r.Cells("Ind").Value.ToString() = CurInd _
        '         Select r).FirstOrDefault()

        'If Not curRow Is Nothing Then
        '    curRow.Selected = True
        'End If
        For i As Integer = 0 To GridView1.RowCount - 1
            If GridView1.GetDataRow(i).Item("Region").ToString = cmbRegion.SelectedItem("Region").ToString And GridView1.GetDataRow(i).Item("mYear").ToString = CmbmYear.SelectedItem("mYear").ToString And GridView1.GetDataRow(i).Item("Ind").ToString = CurInd Then
                'GridView1.SelectRow(i)
                GridView1.FocusedRowHandle = i
                Exit For
            End If
        Next
        Dim curRow As DataRow = GridView1.GetDataRow(GridView1.FocusedRowHandle)
        Try
            If sender.name.Substring(0, 6) = "TbxVal" Then
                curRow.Item("dataVal") = CType(sender.text, Double)
            ElseIf sender.name.Substring(0, 6) = "TbxRef" Then
                curRow.Item("DataRef") = sender.text
            ElseIf sender.name.Substring(0, 6) = "TarMin" Then
                curRow.Item("TarMin") = CType(sender.text, Double)
            ElseIf sender.name.Substring(0, 6) = "TarMax" Then
                curRow.Item("TarMax") = CType(sender.text, Double)
            End If
            Dim chuanhoa As Double = (CType(curRow.Item("dataVal"), Double) - CType(curRow.Item("TarMin"), Double)) / (CType(curRow.Item("TarMax"), Double) - CType(curRow.Item("TarMin"), Double))
            If chuanhoa > 1 Then
                curRow.Item("ChuanHoa") = 1
            ElseIf chuanhoa < 0 Then
                curRow.Item("ChuanHoa") = 0
            Else
                curRow.Item("ChuanHoa") = chuanhoa
            End If

        Catch
            '  MessageBox.Show("The Indicator value should be a number", "")
        End Try




        ''Duoc nhung cham lam
        ''========Chọn Row tương ứng với Textbox vừa lostFocus để update giá trị vào GridView1
        'Dim DgvTable As DataTable = gridcontrol1.datasource
        'Dim CurRow
        'Dim CurRegion = cmbRegion.SelectedItem("Region")
        'Dim CurYear = CmbmYear.SelectedItem("mYear")
        'Dim CurInd = sender.name.Substring(6, sender.name.length - 6)       'TbxRefTQ01=>Chỉ số như TQ01, TQ03...
        ''Try
        ''Chọn mọi Rows thỏa mãn ĐK Region = CmbUrban.Selected và mYear = cmbmYear.Selected cho vào dataRow
        ''CurRow = DgvTable.Select("Region= " + "'" + cmbRegion.SelectedItem("Region").ToString + "' And mYear = '" + CmbmYear.SelectedItem("mYear").ToString + "'")
        'CurRow = DgvTable.Select("Region= " + "'" + cmbRegion.SelectedItem("Region").ToString + "' And mYear = '" + CmbmYear.SelectedItem("mYear").ToString + "' And Ind = '" + CurInd + "'")
        ''Catch
        ''End Try

        ''Đặt giá trị của Cell = Giá trị của Tbx vừa lostFocus
        'Try
        '    If sender.name.Substring(0, 6) = "TbxVal" Then
        '        CurRow(0)("dataVal") = sender.text  'DblVal 'CType(sender.text, Double)
        '    ElseIf sender.name.Substring(0, 6) = "TbxRef" Then
        '        CurRow(0)("DataRef") = sender.text  'DblVal 'CType(sender.text, Double)
        '    End If

        'Catch
        '    ' MessageBox.Show("The Indicator value should be a number", "")
        'End Try
    End Sub

    Private Sub TbxValCombo_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim CurInd = sender.name.Substring(6, sender.name.length - 6)
        ''=====Chuyển đổi select cho DataGridView
        For i As Integer = 0 To GridView1.RowCount - 1
            If GridView1.GetDataRow(i).Item("Ind").Value.ToString = CurInd Then
                'MessageBox.Show("")
                ' GridView1.Rows(i).Selected = True
                GridView1.SelectRow(i)
                'GridView1.FirstDisplayedScrollingRowIndex = i   'Scroll to the select row
                Exit For
            End If
        Next
        'Dim curRow As DataGridViewRow = GridView1.SelectedRows(0)
        Dim HandleRow As Int16 = GridView1.GetSelectedRows(0)
        Dim curRow As DataRow = GridView1.GetDataRow(HandleRow)
        Try
            If curRow.Item("dataVal").Value.ToString <> sender.text Then    'Có If ở đây để tránh việc lặp: value từ Dgv update lên tbx rồi lại update ngược lại từ tbx xuống dgv
                'If sender.name.Substring(0, 6) = "TbxVal" Then
                curRow.Item("dataVal").Value = sender.text  'DblVal 'CType(sender.text, Double)
                'ElseIf sender.name.Substring(0, 6) = "TbxRef" Then
                'curRow.Cells("DataRef").Value = sender.text  'DblVal 'CType(sender.text, Double)
                'End If
            End If
        Catch
            ' MessageBox.Show("The Indicator value should be a number", "")
        End Try

    End Sub
    Private Sub TbxVal_Spin(sender As Object, e As DevExpress.XtraEditors.Controls.SpinEventArgs)   'Không cho lăn chuột để tăng, giảm số trên textboxVAL
        e.Handled = True

    End Sub

    Private Sub TbxValKeypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        'savebolean = True 'Để khi CmbmYear va CmbRegion có indexchaged thì chạy luôn save vì lúc này đã có tđổi trong textbox

        Dim thename As String = sender.name
        Dim Ind As String = thename.Substring(6, thename.Length - 6)    'Bo 6 ky tu dau tien vi ten la: TbxValTQ01
        ''MyADOClass.KetNoi_Open()
        Dim Table_Indicator As DataTable = MyADOClass.DtFromQry("Select Ind, myNote  From " + IndTab + " ")
        ''MyADOClass.KetNoi_Close()

        Dim FilDataRows = Table_Indicator.Select("Ind = " + "'" + Ind + "'")
        'gridcontrol1.datasource = Table_Indicator
        Dim thenote As String = FilDataRows(0)("mynote")

        Select Case thenote
            Case "Nhóm 1: Giá trị số nguyên"
                AllowTextboxvalue.AllowIntegerNumber(sender, e)
                'AllowTextboxvalue.NumberFormat(sender, 3)
            Case "Nhóm 2: Giá trị số thực"
                AllowTextboxvalue.AllowFloatNumber(sender, e)
                ' AllowTextboxvalue.NumberFormat(sender, 3)
            Case "Nhóm 3: Giá trị phần trăm"
                AllowTextboxvalue.AllowPercent(sender, e)

        End Select

        'Dim ThuTu As Int16 = Convert.ToInt16(thename.Substring(thename.Length - 3, thename.Length))
        'Dim nextTT As Int16
        'Dim thenextTbx As TextEdit = FindControl(XtraTabPage_Thongtin, thename.Substring(0, thename.Length - 3) + ThuTu)
        'If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
        '    thenextTbx.Focus()
        'End If
    End Sub


    Private Sub TbxLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        sender.BackColor = Color.White
        'Dim Mytxt As TextEdit = CType(sender, TextEdit)
        'sender.Font = New System.Drawing.Font(Mytxt.Font.FontFamily, Mytxt.Font.Size - 2, FontStyle.Regular)

        '    'Dim ans As DialogResult
        '    ''Dim tbx As TextBox
        '    ''tbx.
        '    'Dim m = sender.name
        '    'Dim DblVal As Double
        '    'Try
        '    '    DblVal = CType(sender.text, Double)
        '    'Catch ex As Exception
        '    '    ans = MessageBox.Show("Dữ liệu của bạn không phải kiểu số?", "Xác nhận kiểu giá trị", MessageBoxButtons.YesNo)   'Trường hợp cType lỗi (Không phải number)
        '    'End Try

        '    'If ans = Windows.Forms.DialogResult.No Then
        '    '    sender.focus()
        '    '    sender.selectall()
        '    '    Return
        '    'End If

        '    'If DblVal > 100 Then
        '    '    'sender.Selectnextcontrol()  'Đánh lạc hướng focus của txbox, ko có nó cứ luẩn quẩn lostfocus rồi báo msg

        '    '    ans = MessageBox.Show("Giá trị phải nằm trong khoảng 0 - 100%", "Xác nhận giá trị", MessageBoxButtons.YesNo)
        '    '    If ans = Windows.Forms.DialogResult.No Then
        '    '        sender.focus()
        '    '        sender.selectall()
        '    '        Return
        '    '    End If

        '    'End If

        '    ''========Chọn Row tương ứng với Textbox vừa lostFocus để update giá trị vào GridView1
        '    Dim DgvTable As DataTable = gridcontrol1.datasource
        '    Dim CurRow
        '    Dim CurRegion = cmbRegion.SelectedItem("Region")
        '    Dim CurYear = CmbmYear.SelectedItem("mYear")
        '    Dim CurInd = sender.name.Substring(6, sender.name.length - 6)       '=>Chỉ số như TQ01, TQ03...
        '    'Try
        '    'Chọn mọi Rows thỏa mãn ĐK Region = CmbUrban.Selected và mYear = cmbmYear.Selected cho vào dataRow
        '    'CurRow = DgvTable.Select("Region= " + "'" + cmbRegion.SelectedItem("Region").ToString + "' And mYear = '" + CmbmYear.SelectedItem("mYear").ToString + "'")
        '    CurRow = DgvTable.Select("Region= " + "'" + cmbRegion.SelectedItem("Region").ToString + "' And mYear = '" + CmbmYear.SelectedItem("mYear").ToString + "' And Ind = '" + CurInd + "'")
        '    'Catch
        '    'End Try

        '    'Đặt giá trị của Cell = Giá trị của Tbx vừa lostFocus
        '    Try
        '        CurRow(0)("dataVal") = sender.text  'DblVal 'CType(sender.text, Double)
        '    Catch
        '        ' MessageBox.Show("The Indicator value should be a number", "")
        '    End Try

    End Sub
    'Private Sub TextEditChartHeigh_Enter(sender As Object, e As EventArgs) Handles TextEditChartHeigh.Enter

    'End Sub
    Private Sub TbxValRef_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
        ''========Chọn Row tương ứng với Textbox vừa lostFocus để update giá trị vào GridView1
        Dim DgvTable As DataTable = GridControl1.DataSource

        Dim CurRow
        Dim CurRegion = cmbRegion.SelectedItem("Region")
        Dim CurYear = CmbmYear.SelectedItem("mYear")
        Dim CurInd = sender.name.Substring(6, sender.name.length - 6)       'TbxRefTQ01=>Chỉ số như TQ01, TQ03...
        sender.BackColor = Color.LightSkyBlue
        'Try
        'Chọn mọi Rows thỏa mãn ĐK Region = CmbUrban.Selected và mYear = cmbmYear.Selected cho vào dataRow
        'CurRow = DgvTable.Select("Region= " + "'" + cmbRegion.SelectedItem("Region").ToString + "' And mYear = '" + CmbmYear.SelectedItem("mYear").ToString + "'")
        CurRow = DgvTable.Select("Region= " + "'" + cmbRegion.SelectedItem("Region").ToString + "' And mYear = '" + CmbmYear.SelectedItem("mYear").ToString + "' And Ind = '" + CurInd + "'")
        'Dim qry = From theRow As DataGridViewRow In dgvMembers.Rows, _
        '          theCell As DataGridViewCell In theRow.Cells _
        '          Where theCell.Value.ToString.ToUpper = searchText _
        '          Select theCell
        '================
        '=================LINQ=================
        '================
        'Dim row As DataGridViewRow = _
        '        (From r As DataGridViewRow In GridView1.Rows _
        '         Where r.Cells("Region").Value.ToString() = cmbRegion.SelectedItem("Region").ToString And _
        '               r.Cells("mYear").Value.ToString() = CmbmYear.SelectedItem("mYear").ToString And _
        '               r.Cells("Ind").Value.ToString() = CurInd _
        '         Select r).FirstOrDefault()

        'If Not row Is Nothing Then
        '    row.Selected = True
        'End If

        For i As Integer = 0 To GridView1.RowCount - 1
            If GridView1.GetDataRow(i).Item("Region").ToString = cmbRegion.SelectedItem("Region").ToString And GridView1.GetDataRow(i).Item("mYear").ToString = CmbmYear.SelectedItem("mYear").ToString And GridView1.GetDataRow(i).Item("Ind").ToString = CurInd Then
                GridView1.SelectRow(i)
                GridView1.FocusedRowHandle = i
            End If
        Next

        'If row.Index > 0 Then
        '    GridView1.FirstDisplayedScrollingRowIndex = row.Index - 1   'Scroll to the select row

        'Else
        '    GridView1.FirstDisplayedScrollingRowIndex = row.Index
        'End If

        Dim Mytxt As TextEdit = CType(sender, TextEdit)
        sender.selectall()

        ' ' sender.Font = New System.Drawing.Font("Arial", 9, FontStyle.Bold) '(sender.Font.FontFamily, sender.Font.Size, FontStyle.Bold)
        'sender.Font = New System.Drawing.Font(Mytxt.Font.FontFamily, Mytxt.Font.Size + 2, FontStyle.Bold)
        ''Duoc nhung cham lam
        'Dim CurInd = sender.name.Substring(6, sender.name.length - 6)       'TbxRefTQ01=>Bo 6 text dau =>Chỉ số như TQ01, TQ03...
        'Dim dt As DataTable = gridcontrol1.datasource
        'For i As Integer = 0 To GridView1.Rows.Count - 1
        '    If GridView1.Rows(i).Cells("Ind").Value.ToString = CurInd Then
        '        'MessageBox.Show("")
        '        GridView1.Rows(i).Selected = True
        '        GridView1.FirstDisplayedScrollingRowIndex = i   'Scroll to the select row
        '        Exit For
        '    End 
        'Next

    End Sub

#End Region
#Region "GridControl1 - GridControl2"
    Private Sub GridView1_ValidatingEditor(sender As Object, e As DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs) Handles GridView1.ValidatingEditor
        '===Cho phép Null Value Input
        Dim view As GridView = CType(sender, GridView)
        If view.FocusedColumn.FieldName = "DataVal" Then
            If Not e.Value Is Nothing AndAlso e.Value = "" Then
                e.Value = DBNull.Value
            End If
        End If
    End Sub
    Private Sub GridView1_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles GridView1.RowCellClick
        'Event này Chỉ fire khi có lệnh EditorShowMode =... đã chạy GridView1.OptionsBehavior.EditorShowMode = EditorShowMode.Click    'Để có thể fire event Gridview1.RowCellClick http://www.devexpress.com/Support/Center/Question/Details/Q406257  
        Dim a = sender.name

        For Each tbx As Control In XtraScrollable_Thongtin.Controls
            If tbx.GetType = GetType(TextEdit) And (tbx.Name.Contains("Tbx") = True Or tbx.Name.Contains("Tar") = True) Then
                tbx.BackColor = Color.White
            End If
        Next

        Dim tbxvalname As String = "TbxVal" + GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Ind")
        Dim TbxVal = FindControl(XtraScrollable_Thongtin, tbxvalname)
        If GridView1.FocusedColumn.FieldName <> "DataRef" And GridView1.FocusedColumn.FieldName <> "TarMin" And GridView1.FocusedColumn.FieldName <> "TarMax" And TbxVal IsNot Nothing Then
            TbxVal.Focus()
            TbxVal.BackColor = Color.LightSkyBlue
            Return
        End If
        Dim tbxRefname As String = "TbxRef" + GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Ind")
        Dim tbxRef = FindControl(XtraScrollable_Thongtin, tbxRefname)
        Dim TarMin = FindControl(XtraScrollable_Thongtin, "TarMin" + GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Ind"))
        Dim TarMax = FindControl(XtraScrollable_Thongtin, "TarMax" + GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Ind"))

        If GridView1.FocusedColumn.FieldName = "DataRef" And tbxRef IsNot Nothing Then
            tbxRef.Focus()
            tbxRef.BackColor = Color.LightSkyBlue
        ElseIf GridView1.FocusedColumn.FieldName = "TarMin" And TarMin IsNot Nothing Then
            TarMin.Focus()
            TarMin.BackColor = Color.LightSkyBlue
        ElseIf GridView1.FocusedColumn.FieldName = "TarMax" And TarMax IsNot Nothing Then
            TarMax.Focus()
            TarMax.BackColor = Color.LightSkyBlue
        End If
        'TbxVal.Select()
        'TbxVal.Show()

    End Sub

    Private Sub GridView1_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView1.RowCellStyle
        'Event này rất hay, dùng để đổi màu (style) cho các Row
        Dim myview As GridView = sender
        If myview.FocusedRowHandle = e.RowHandle Then
            e.Appearance.BackColor = Color.LightSkyBlue
        End If

    End Sub



    Private Sub GridView2_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles GridView2.CustomDrawCell
        ''====Không hiển thị nút filter ở column caption
        'If (e.Column.Name = "fsd" And e.RowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle) Then
        '    e.Handled = True
        'End If
    End Sub
    Private Sub GridView2_CustomDrawFilterPanel(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomDrawObjectEventArgs) Handles GridView2.CustomDrawFilterPanel
        Dim args As DevExpress.XtraGrid.Drawing.GridFilterPanelInfoArgs = DirectCast(e.Info, DevExpress.XtraGrid.Drawing.GridFilterPanelInfoArgs)
        args.ActiveButtonInfo.Bounds = Rectangle.Empty
    End Sub
    Private Sub GridView2_ColumnFilterChanged(sender As Object, e As EventArgs) Handles GridView2.ColumnFilterChanged

    End Sub
    Private Sub GridView2_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GridView2.RowCellStyle
        'Event này rất hay, dùng để đổi màu (style) cho các Row
        Dim myview As GridView = sender
        If myview.FocusedRowHandle = e.RowHandle Then
            e.Appearance.BackColor = Color.LightSkyBlue
        End If
    End Sub

    Private Sub GridControl1_EditorKeyup(sender As Object, e As KeyEventArgs) Handles GridControl1.EditorKeyUp
        '===============Có thể dùng GridView1_CellValueChanging event thay cho GridControl1_EditorKeyup
        'Private Sub GridView1_CellValueChanging(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanging
        'Sử dụng e.Value.ToString thay cho GridView1.ActiveEditor.Text
        'End Sub
        '=================Đặt  giá trị textbox như giá trị trong ActiveCell

        Dim mycellTextEdit1 As TextEdit = TryCast(GridView1.ActiveEditor, TextEdit)
        Dim tbxvalname As String = "TbxVal" + GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Ind")
        Dim TbxVal = FindControl(XtraScrollable_Thongtin, tbxvalname)
        If GridView1.FocusedColumn.FieldName <> "DataRef" And TbxVal IsNot Nothing Then 'Cứ khác DataRef là cho vào DataVal
            TbxVal.Text = GridView1.ActiveEditor.Text
        End If

        Dim tbxRefname As String = "TbxRef" + GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Ind")
        Dim tbxRef = FindControl(XtraScrollable_Thongtin, tbxRefname)
        Dim TarMin = FindControl(XtraScrollable_Thongtin, "TarMin" + GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Ind"))
        Dim TarMax = FindControl(XtraScrollable_Thongtin, "TarMax" + GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Ind"))
        If GridView1.FocusedColumn.FieldName = "DataRef" And tbxRef IsNot Nothing Then
            tbxRef.Text = mycellTextEdit1.Text
        ElseIf GridView1.FocusedColumn.FieldName = "TarMin" And TarMin IsNot Nothing Then
            TarMin.Text = mycellTextEdit1.Text
        ElseIf GridView1.FocusedColumn.FieldName = "TarMax" And TarMax IsNot Nothing Then
            TarMax.Text = mycellTextEdit1.Text

        End If
    End Sub

    Private Sub GridControl1_EditorKeyPress(sender As Object, e As KeyPressEventArgs) Handles GridControl1.EditorKeyPress
        ' Private Sub GridControl1_keyup(sender As Object, e As KeyEventArgs) Handles GridControl1.KeyUp
        validInputValue(sender, e)
        'If GridView1.FocusedColumn.Name = "DataVal" Then
        'If (e.KeyCode >= 96 And e.KeyCode <= 105) Or (e.KeyCode >= 48 And e.KeyCode <= 57) Then
        If e.Handled = False And e.KeyChar <> ChrW(Keys.Enter) Then   ''Đặt Giá trị BtnSaveGrV2.Enabled = True mỗi lần thay đổi giá trị tại DataVal hoặc DataRef

            BtnSaveGrV1.Enabled = True

        End If




    End Sub

    Private Sub GridControl2_EditorKeyPress(sender As Object, e As KeyPressEventArgs) Handles GridControl2.EditorKeyPress
        validInputValue(sender, e)
        If e.Handled = False And e.KeyChar <> ChrW(Keys.Enter) Then   ''Đặt Giá trị BtnSaveGrV2.Enabled = True mỗi lần thay đổi giá trị tại DataVal hoặc DataRef
            BtnSaveGrV2.Enabled = True
        End If

    End Sub
#Region "Sửa trực tiếp trên GridView2; Chỉ cho nhập giá tri số lên Dataval;"
    Private Sub GridView2_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView2.CellValueChanged
        AddChart(XtraScrollable_Chart)    'Refresh myChart
    End Sub

    'Private Sub tbDgv2_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
    '    'BtnSaveGrV1.Enabled = True => Đặt tại GridView2_KeyPress
    '    'Dim thename As String = sender.name
    '    If GridView2.FocusedColumn.ColumnHandle = GridView2.Columns("DataVal").ColumnHandle Then
    '        Dim CurRow As Integer = GridView2.FocusedRowHandle
    '        'Dim Ind As String = GridView2.Rows(CurRow).Cells("Ind").Value
    '        ''MyADOClass.KetNoi_Open()
    '        ' '' ''Dim Table_Indicator As DataTable = MyADOClass.TableFromQry("Select Ind, myNote  From " + IndTable + " ")
    '        '' '' '''MyADOClass.KetNoi_Close()

    '        ' '' ''Dim FilDataRows = Table_Indicator.Select("Ind = " + "'" + Ind + "'")
    '        ' '' ''gridcontrol1.datasource = Table_Indicator
    '        '' '' ''GridView1.CurrentCell.RowIndex
    '        'Dim thenote As String = GridView2.CurrentCell.Value
    '        Dim thenote As String = GridView2.GetDataRow(CurRow).Item("mynote").ToString
    '         Select Case thenote
    '            Case "Nhóm 1: Giá trị số nguyên"
    '                AllowTextboxvalue.AllowIntegerNumber(sender, e)
    '                AllowTextboxvalue.NumberFormat(sender, 3)
    '            Case "Nhóm 2: Giá trị số thực"
    '                AllowTextboxvalue.AllowFloatNumber(sender, e)
    '                AllowTextboxvalue.NumberFormat(sender, 3)
    '            Case "Nhóm 3: Giá trị phần trăm"
    '                AllowTextboxvalue.AllowPercent(sender, e)

    '        End Select

    '        If sender.text.contains(".") Or sender.text.length = 0 Then   'Neu da co dau cham hoac chua co text naof thi ko cho cham nua
    '            If (e.KeyChar = "."c) Then
    '                e.Handled = True
    '            End If
    '        End If
    '        'Neu chua co cham thi khong cho go so lon hon 10 va cho phep go "."



    '        If e.KeyChar = Chr(8) Then ' Allow backspace
    '            e.Handled = False
    '        End If
    '    End If
    '    If e.Handled = False Then   ''Đặt Giá trị BtnSaveGrV2.Enabled = True mỗi lần thay đổi giá trị tại DataVal hoặc DataRef
    '        BtnSaveGrV2.Enabled = True
    '    End If
    '    ''If Not Char.IsNumber(e.KeyChar) Then
    '    ''    e.Handled = True
    '    ''End If
    'End Sub
    'Private Sub tb_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    ''This handles if they paste crap in the textbox since that doesn't fire the KeyDown event
    '    'Dim tb As TextBox = TryCast(sender, TextBox)
    '    'Dim i As Integer
    '    'If Not String.IsNullOrEmpty(tb.Text) AndAlso Not Integer.TryParse(tb.Text, i) Then
    '    '    Dim sb As New StringBuilder()
    '    '    For i1 As Integer = 0 To tb.Text.Length - 1
    '    '        If Char.IsNumber(tb.Text(i1)) Then
    '    '            sb.Append(tb.Text(i1))
    '    '        End If
    '    '    Next
    '    '    tb.Text = sb.ToString()
    '    'End If
    'End Sub
#End Region

    '==================================================================================
#Region "Sửa trực tiếp trên GridView1; Chỉ cho nhập giá tri số lên Dataval;  "
    ''Private Sub GridView1_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles GridView1.CellValidating
    ''    If e.ColumnIndex = GridView1.Columns("DataVal").Index Then
    ''        'this is our numeric column
    ''        Dim i As Integer
    ''        If Not Double.TryParse(Convert.ToString(e.FormattedValue), i) Then
    ''            e.Cancel = True
    ''            MessageBox.Show("Phải nhập giá trị số")
    ''        End If
    ''    End If
    ''End Sub

    'Private Sub GridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles GridView1.EditingControlShowing
    '    If GridView1.FocusedColumn.ColumnHandle < 0 Then
    '        'i dont know if it ever fires with -1, so to be safe
    '        Return
    '    End If

    '    If GridView1.FocusedColumn.ColumnHandle = GridView1.Columns("DataVal").ColumnHandle Or GridView1.FocusedColumn.ColumnHandle = GridView1.Columns("DataRef").ColumnHandle Then
    '        ' our numeric column; Chi ap dung khong cho nhap string doi voi dataval
    '        Dim DgvTbxEdit As DataGridViewTextBoxEditingControl = GridView1.EditingControl
    '        'Chuyen DataGridviewTextboxEditingcontrol sang textbox de co the nhan handler Keypress
    '        Dim tbDgv1 As TextBox = TryCast(DgvTbxEdit, TextBox)  ''''Lệnh này quan trọng nhất, nó chuyển cell đang edit thanh textbox


    '        If tbDgv1 Is Nothing Then
    '            System.Diagnostics.Debugger.Break()
    '            'you changed it to a non TextBox control. Add more support
    '            Return
    '        End If

    '        AddHandler tbDgv1.KeyPress, New KeyPressEventHandler(AddressOf tbDgv1_KeyPress)
    '    Else
    '        'AddHandler GridView1.KeyPress, New KeyPressEventHandler(AddressOf tbDgv1_KeyPress)

    '        'AddHandler tb.KeyPress, New KeyPressEventHandler
    '    End If



    '    'If GridView2.CurrentCell.ColumnIndex <> GridView2.Columns("DataVal").Index Then
    '    '    'not our numeric column; Chi ap dung khong cho nhap string doi voi dataval
    '    '    Return
    '    'End If

    '    'Dim tb As TextBox = TryCast(GridView2.EditingControl, TextBox)  ''''Lệnh này quan trọng nhất, nó chuyển cell đang edit thanh textbox
    '    'If tb Is Nothing Then
    '    '    System.Diagnostics.Debugger.Break()
    '    '    'you changed it to a non TextBox control. Add more support
    '    '    Return
    '    'End If
    '    ''editingBox = tb
    '    'AddHandler tb.KeyPress, New KeyPressEventHandler(AddressOf tb_KeyPress)
    '    ''AddHandler tb.TextChanged, New EventHandler(AddressOf tb_TextChanged)
    'End Sub

    'Private Sub tbDgv1_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
    '    'BtnSaveGrV1.Enabled = True => Đặt tại GridView1_KeyPress
    '    'Dim thename As String = sender.name
    '    If GridView1.CurrentCell.ColumnIndex = GridView1.Columns("DataVal").ColumnHandle Then  'Chỉ strict input đối với DataVal

    '        Dim CurRow As Integer = GridView1.CurrentCell.RowIndex

    '        Dim thenote As String = GridView1.GetDataRow(CurRow).Item("mynote").ToString
    '        Select Case thenote
    '            Case "Nhóm 4: Có/Không"
    '                ''Chỉ cho gõ key C hoặc K để autocomplete tới có hoặc không
    '                'e.KeyChar = e.KeyChar.ToString.ToUpper
    '                'If (e.KeyChar <> "C"c) AndAlso (e.KeyChar <> "K"c) AndAlso (e.KeyChar <> "c"c) AndAlso (e.KeyChar <> "k"c) Or sender.text.length > 0 Then
    '                '    e.Handled = True
    '                'End If
    '            Case "Nhóm 2: Giá trị phần trăm"
    '                If Not [Char].IsDigit(e.KeyChar) AndAlso Not [Char].IsControl(e.KeyChar) AndAlso (e.KeyChar <> "."c) AndAlso (e.KeyChar <> "-"c) Then
    '                    e.Handled = True
    '                End If

    '                'Có giá trị % > 100%, khong, gia tri nao >100 thi chuyen sang Nhom khac
    '                If sender.text.length > 0 And Not sender.text.contains(".") And Not CType(sender, TextBox).SelectionLength > 0 Then 'CType(sender, TextBox).SelectionLength = sender.textlength Then ' để ko cho GTri >100  <-100; sender.selectionglength = sender.textlengthThen=> khi text dc selectall
    '                    If CType(sender.text, Double) > 10 AndAlso (e.KeyChar <> "."c) Then
    '                        e.Handled = True
    '                    End If
    '                    If CType(sender.text, Double) < -10 AndAlso (e.KeyChar <> "."c) Then    ' để ko cho GTri ?100
    '                        e.Handled = True
    '                    End If
    '                End If
    '            Case "Nhóm 3: Giá trị số thực"
    '                If Not [Char].IsDigit(e.KeyChar) AndAlso Not [Char].IsControl(e.KeyChar) AndAlso (e.KeyChar <> "."c) Then
    '                    e.Handled = True
    '                End If
    '            Case "Nhóm 1: Giá trị số nguyên"
    '                If Not [Char].IsDigit(e.KeyChar) AndAlso Not [Char].IsControl(e.KeyChar) Then
    '                    e.Handled = True
    '                End If
    '            Case "Nhóm 5: Cấp quản lý"
    '                ''Chỉ cho gõ key T hoặc t để autocomplete tới có hoặc TP, TX; Không cho gõ hơn 1 ký tự
    '                'e.KeyChar = e.KeyChar.ToString.ToUpper
    '                'If (e.KeyChar <> "T"c AndAlso (e.KeyChar <> "t"c) Or sender.text.length > 0) Then
    '                '    e.Handled = True
    '                'End If
    '            Case "Nhóm 6: Loại đô thị"
    '                ''Chỉ cho gõ key L hoặc l để autocomplete tới Loại; Không cho gõ hơn 1 ký tự
    '                'e.KeyChar = e.KeyChar.ToString.ToUpper
    '                'If (e.KeyChar <> "L"c AndAlso (e.KeyChar <> "l"c) Or sender.text.length > 0) Then
    '                '    e.Handled = True
    '                'End If
    '            Case "Nhóm 7: Ký tự"
    '        End Select

    '        If sender.text.contains(".") Or sender.text.length = 0 Then   'Neu da co dau cham hoac chua co text naof thi ko cho cham nua
    '            If (e.KeyChar = "."c) Then
    '                e.Handled = True
    '            End If
    '        End If
    '        'Neu chua co cham thi khong cho go so lon hon 10 va cho phep go "."



    '        If e.KeyChar = Chr(8) Then ' Allow backspace
    '            e.Handled = False
    '        End If
    '    End If

    '    'BtnSaveGrV2.Enabled = True
    '    ''If Not Char.IsNumber(e.KeyChar) Then
    '    ''    e.Handled = True
    '    ''End If
    '    If e.Handled = False Then       'Đặt Giá trị BtnSaveGrV1.Enabled = False  mỗi lần thay đổi giá trị tại DataVal hoặc DataRef
    '        saveboolean = True
    '    End If
    'End Sub

#End Region
#End Region


#Region "Check ALL"
    Private Sub CheckUrbAll(ByVal Ckb_ChkAll As CheckBox, ByVal myCheckedListBox As CheckedListBoxControl)
        'Nếu check thì
        If Ckb_ChkAll.CheckState = CheckState.Checked Then
            For i = 0 To myCheckedListBox.ItemCount - 1
                myCheckedListBox.SetItemChecked(i, True)
            Next
            CkbUrb_ChkGrp1.CheckState = CheckState.Checked
            CkbUrb_ChkGrp2.CheckState = CheckState.Checked
            CkbUrb_ChkGrp3.CheckState = CheckState.Checked
            CkbUrb_ChkGrp4.CheckState = CheckState.Checked
            CkbUrb_ChkGrp5.CheckState = CheckState.Checked
            CkbUrb_ChkGrp6.CheckState = CheckState.Checked
        ElseIf Ckb_ChkAll.CheckState = CheckState.Unchecked Then 'Nếu "Chọn tất cả" đang được check
            For i = 0 To myCheckedListBox.ItemCount - 1
                myCheckedListBox.SetItemChecked(i, False)
            Next
            myCheckedListBox.SetItemChecked(0, True)
            CkbUrb_ChkGrp1.CheckState = CheckState.Unchecked
            CkbUrb_ChkGrp2.CheckState = CheckState.Unchecked
            CkbUrb_ChkGrp3.CheckState = CheckState.Unchecked
            CkbUrb_ChkGrp4.CheckState = CheckState.Unchecked
            CkbUrb_ChkGrp5.CheckState = CheckState.Unchecked
            CkbUrb_ChkGrp6.CheckState = CheckState.Unchecked
        Else 'Trường hơp
        End If
    End Sub
    Private Sub CkbUrb_ChkGrp1_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CkbUrb_ChkGrp1.CheckStateChanged
        Dim RegionGrp As String = sender.text
        CheckRegionGrp(sender, CheckedListBox_Urban, RegionGrp)
    End Sub
    Private Sub CheckRegionGrp(ByVal Ckb_ChkRegionGrp As CheckBox, ByVal myCheckedListBox As CheckedListBoxControl, ByVal RegionGrp As String)
        'Nếu check thì
        Dim CkbTable As DataTable = myCheckedListBox.DataSource
        If Ckb_ChkRegionGrp.CheckState = CheckState.Checked Then
            For i = 0 To myCheckedListBox.ItemCount - 1
                If CkbTable.Rows(i)("RegionGrp").ToString = RegionGrp Then
                    myCheckedListBox.SetItemChecked(i, True)
                End If

            Next
            ' CkbUrb_ChkAll.CheckState = CheckState.Indeterminate
        ElseIf Ckb_ChkRegionGrp.CheckState = CheckState.Unchecked Then 'Nếu "Chọn tất cả" đang được check
            For i = 0 To myCheckedListBox.ItemCount - 1
                If CkbTable.Rows(i)("RegionGrp").ToString = RegionGrp Then
                    myCheckedListBox.SetItemChecked(i, False)
                End If
            Next
            'CkbUrb_ChkAll.CheckState = CheckState.Unchecked
            'Else 'Trường hơp
        End If
    End Sub
    Private Sub CkbUrb_ChkGrp1_CheckedChanged(sender As Object, e As EventArgs) Handles CkbUrb_ChkGrp1.CheckedChanged

    End Sub
    Private Sub CkbUrb_ChkGrp2_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CkbUrb_ChkGrp2.CheckStateChanged
        Dim RegionGrp As String = sender.text

        CheckRegionGrp(sender, CheckedListBox_Urban, RegionGrp)
    End Sub
    Private Sub CkbUrb_ChkGrp3_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CkbUrb_ChkGrp3.CheckStateChanged
        Dim RegionGrp As String = sender.text

        CheckRegionGrp(sender, CheckedListBox_Urban, RegionGrp)
    End Sub
    Private Sub CkbUrb_ChkGrp4_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CkbUrb_ChkGrp4.CheckStateChanged
        Dim RegionGrp As String = sender.text
        CheckRegionGrp(sender, CheckedListBox_Urban, RegionGrp)
    End Sub
    Private Sub CkbUrb_ChkGrp5_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CkbUrb_ChkGrp5.CheckStateChanged
        Dim RegionGrp As String = sender.text
        CheckRegionGrp(sender, CheckedListBox_Urban, RegionGrp)
    End Sub
    Private Sub CkbUrb_ChkGrp6_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CkbUrb_ChkGrp6.CheckStateChanged
        Dim RegionGrp As String = sender.text
        CheckRegionGrp(sender, CheckedListBox_Urban, RegionGrp)
    End Sub
    Private Sub CkbUrb_ChkAll_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CkbUrb_ChkAll.CheckStateChanged
        Dim MousePos As System.Drawing.Point = sender.PointToClient(MousePosition)
        Dim Ckb_Bnd As System.Drawing.Rectangle = sender.bounds
        'mouse nằm ngoài vị trí thì thôi luôn
        If MousePos.X < 0 Or MousePos.X > Ckb_Bnd.Width Or MousePos.Y < 0 Or MousePos.Y > Ckb_Bnd.Height Then
            Return
        End If
        CheckUrbAll(sender, CheckedListBox_Urban)

        'If XtraTabControl2.SelectedTabPageIndex = 1 Then
        '    BtnOK_Click(Nothing, Nothing)
        'End If
    End Sub

    Private Sub CheckAll(ByVal Ckb_ChkAll As CheckBox, ByVal myCheckedListBox As DevExpress.XtraEditors.CheckedListBoxControl)
        'Nếu check thì
        If Ckb_ChkAll.CheckState = CheckState.Checked Then
            'For i = 0 To myCheckedListBox.ItemCount - 1
            '    myCheckedListBox.SetItemChecked(i, True)
            'Next
            myCheckedListBox.CheckAll()
        ElseIf Ckb_ChkAll.CheckState = CheckState.Unchecked Then 'Nếu "Chọn tất cả" đang được check
            'For i = 0 To myCheckedListBox.ItemCount - 1
            '    myCheckedListBox.SetItemChecked(i, False)
            'Next
            myCheckedListBox.UnCheckAll()
            '===Không cho uncheck tất cả các Item.

            myCheckedListBox.SetItemChecked(0, True)

        Else 'Trường hơp
        End If
    End Sub
    Private Sub disableChkAll()
        'Disable CkbInd_ChkAll khi chọn Đồ thị theo một năm và Disable CkbYear_ChkAll khi chọn Đồ thị theo một chỉ số 
        If XtraTabControl2.SelectedTabPageIndex = 1 Then
            If CmbSoSanh.SelectedItem = "Đồ thị theo một Năm" Then
                CkbYear_ChkAll.Enabled = False
                CkbInd_ChkAll.Enabled = True
                CkbInd_KT.Enabled = True
                CkbInd_XH.Enabled = True
                CkbInd_MT.Enabled = True
                CkbUrb_ChkAll.Enabled = True
            ElseIf CmbSoSanh.SelectedItem = "Đồ thị theo một Chỉ tiêu" Then
                CkbInd_ChkAll.Enabled = False
                CkbInd_KT.Enabled = False
                CkbInd_XH.Enabled = False
                CkbInd_MT.Enabled = False
                CkbYear_ChkAll.Enabled = True
                CkbUrb_ChkAll.Enabled = True
            ElseIf CmbSoSanh.SelectedItem = "Đồ thị theo một Địa bàn" Then
                CkbUrb_ChkAll.Enabled = False
                CkbInd_ChkAll.Enabled = True
                CkbInd_KT.Enabled = True
                CkbInd_XH.Enabled = True
                CkbInd_MT.Enabled = True
                CkbYear_ChkAll.Enabled = True
            End If
        Else
            CkbInd_ChkAll.Enabled = True
            CkbYear_ChkAll.Enabled = True
            CkbUrb_ChkAll.Enabled = True
            CkbInd_KT.Enabled = True
            CkbInd_XH.Enabled = True
            CkbInd_MT.Enabled = True
        End If
    End Sub
    Private Sub CkbInd_KT_CheckStateChanged(sender As Object, e As EventArgs) Handles CkbInd_KT.CheckStateChanged
        CheckIndGrp(sender, CheckedListBox_Ind, "Kinh tế")
    End Sub
    Private Sub CkbInd_XH_CheckStateChanged(sender As Object, e As EventArgs) Handles CkbInd_XH.CheckStateChanged
        CheckIndGrp(sender, CheckedListBox_Ind, "Xã hội")
    End Sub
    Private Sub CkbInd_MT_CheckStateChanged(sender As Object, e As EventArgs) Handles CkbInd_MT.CheckStateChanged
        CheckIndGrp(sender, CheckedListBox_Ind, "Môi trường")
    End Sub
    Private Sub CkbInd_ChkAll_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CkbInd_ChkAll.CheckStateChanged
        Dim MousePos As System.Drawing.Point = sender.PointToClient(MousePosition)
        Dim Ckb_Bnd As System.Drawing.Rectangle = sender.bounds
        ''mouse nằm ngoài vị trí thì thôi luôn
        If MousePos.X < 0 Or MousePos.X > Ckb_Bnd.Width Or MousePos.Y < 0 Or MousePos.Y > Ckb_Bnd.Height Then
            Return
        End If
        CheckAll(sender, CheckedListBox_Ind)
        If sender.CheckState = CheckState.Checked Then
            CkbInd_XH.CheckState = CheckState.Checked
            CkbInd_KT.CheckState = CheckState.Checked
            CkbInd_MT.CheckState = CheckState.Checked
        ElseIf sender.CheckState = CheckState.Unchecked Then 'Nếu "Chọn tất cả" đang được check
            CkbInd_XH.CheckState = CheckState.Unchecked
            CkbInd_KT.CheckState = CheckState.Unchecked
            CkbInd_MT.CheckState = CheckState.Unchecked
        End If
    End Sub
    Private Sub CheckIndGrp(ByVal Ckb_ChkIndGrp As CheckBox, ByVal myCheckedListBox As CheckedListBoxControl, ByVal Pilar As String)
        'Nếu check thì
        Dim CkbTable As DataTable = myCheckedListBox.DataSource
        If Ckb_ChkIndGrp.CheckState = CheckState.Checked Then
            For i = 0 To myCheckedListBox.ItemCount - 1
                If CkbTable.Rows(i)("Pilar").ToString = Pilar Then
                    myCheckedListBox.SetItemChecked(i, True)
                End If

            Next
            ' CkbUrb_ChkAll.CheckState = CheckState.Indeterminate
        ElseIf Ckb_ChkIndGrp.CheckState = CheckState.Unchecked Then 'Nếu "Chọn tất cả" đang được check
            For i = 0 To myCheckedListBox.ItemCount - 1
                If CkbTable.Rows(i)("Pilar").ToString = Pilar Then
                    myCheckedListBox.SetItemChecked(i, False)
                End If
            Next

        End If
    End Sub

    Private Sub CkbYear_ChkAll_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CkbYear_ChkAll.CheckStateChanged
        Dim MousePos As System.Drawing.Point = sender.PointToClient(MousePosition)
        Dim Ckb_Bnd As System.Drawing.Rectangle = sender.bounds
        'mouse nằm ngoài vị trí thì thôi luôn
        If MousePos.X < 0 Or MousePos.X > Ckb_Bnd.Width Or MousePos.Y < 0 Or MousePos.Y > Ckb_Bnd.Height Then
            Return
        End If
        CheckAll(sender, CheckedListBox_Year)
        'If XtraTabControl2.SelectedTabPageIndex = 1 Then
        '    BtnOK_Click(Nothing, Nothing)
        'End If
    End Sub
#End Region

#Region "TreeList"
    Private Sub TriStateTreeView1_AfterCheck(sender As Object, e As TreeViewEventArgs)
        'Dim node As TreeNode = e.Node
        '' MessageBox.Show(node.Name, node.Text)
        'For i = 0 To CheckedListBox_Urban.ItemCount - 1         'CheckedListBox_Urban.Items.count doesn't work with bound data and should use 'ItemCount'
        '    Dim myDataRow1 As DataRowView = CheckedListBox_Urban.GetItem(i)
        '    Dim myText As String = CheckedListBox_Urban.GetDisplayItemValue(i)
        '    If node.Text = myText Then 'Since in a Bound Mode the CheckedListBoxControl.Items collection is empty, use the CheckedListBoxControl.GetItem(i) method Instead of CheckedListBoxControl.Items(i)
        '        If node.Checked = True Then
        '            CheckedListBox_Urban.SetItemCheckState(i, CheckState.Checked)
        '        Else
        '            CheckedListBox_Urban.SetItemCheckState(i, CheckState.Unchecked)
        '        End If
        '        Return
        '    End If
        'Next
    End Sub

    Private Sub TriStateTreeView1_BeforeCheck(sender As Object, e As TreeViewCancelEventArgs)

    End Sub


    Private Sub TreeList1_BeforeCheckNode(sender As Object, e As CheckNodeEventArgs) Handles TreeList1.BeforeCheckNode
        '===========Sử dụng Để chỉ có thể check hoặc uncheck. Không cho Indeterminate khi click
        If (e.State = CheckState.Indeterminate) Then
            If e.State = CheckState.Checked Then
                e.State = CheckState.Unchecked
            Else
                e.State = CheckState.Checked
            End If
        End If
    End Sub
    Private Sub TreeList1_MouseClick(sender As Object, e As MouseEventArgs) Handles TreeList1.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim trl As TreeList = sender
            Dim hi As DevExpress.XtraTreeList.TreeListHitInfo = trl.CalcHitInfo(e.Location)        'trl.CalcHitInfo(trl.PointToClient(MousePosition))
            'If hi.HitInfoType = HitInfoType.NodeCheckBox Or hi.HitInfoType = HitInfoType.Then Then
            '    Return
            'End If
            Dim node As DevExpress.XtraTreeList.Nodes.TreeListNode = hi.Node

            If hi Is Nothing OrElse hi.HitInfoType = HitInfoType.Cell Then
                mySetNodeCheckState(trl, node)
            End If
        End If

    End Sub
    Private Sub mySetNodeCheckState(ByVal tl As TreeList, ByVal node As DevExpress.XtraTreeList.Nodes.TreeListNode)
        Dim checkState As Windows.Forms.CheckState = node.CheckState
        Dim resCheckState As Windows.Forms.CheckState = checkState.Unchecked
        'Select Case checkState
        '    Case checkState.Checked
        '        resCheckState = checkState.Unchecked
        '    Case checkState.Unchecked
        '        resCheckState = checkState.Indeterminate
        '    Case checkState.Indeterminate
        '        resCheckState = checkState.Checked
        'End Select
        If (node.CheckState = checkState.Indeterminate Or node.CheckState = checkState.Checked) Then
            resCheckState = checkState.Unchecked
        Else
            resCheckState = checkState.Checked
        End If
        tl.SetNodeCheckState(node, resCheckState, True)
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim co As GetListNodeAtSpecificLevel = New GetListNodeAtSpecificLevel(1)
        TreeList1.NodesIterator.DoLocalOperation(co, TreeList1.Nodes)
        Dim result As List(Of Nodes.TreeListNode) = co.NodesOnLevel

        'TreeList2.Nodes.Clear()
        'For Each node In result
        '    'TreeList2.Nodes.Add(node)
        '    TreeList2.AppendNode(New Object() {node.GetValue(0)}, -1)
        'Next
    End Sub
    ''   /*
    ''* node - node being visited
    ''* clevel - current level
    ''* rlevel - requested level
    ''* result - result queue
    ''*/
    'Function drill(node As TreeNode, clevel, rlevel, result) As Queue
    '    If (clevel = rlevel) Then
    '        result.enqueue(node)
    '    Else
    '        If (node.left IsNot Nothing) Then
    '            drill(node.left, clevel + 1, rlevel, result)
    '        End If
    '        If (node.right <> Nothing) Then
    '            drill(node.right, clevel + 1, rlevel, result)
    '        End If

    '    End If
    '    Return result
    'End Function
    'Function FindRootNode(treeNode As TreeNode) As TreeNode
    '    While (treeNode.Parent Is Not Nothing)
    '        treeNode = treeNode.Parent
    '    End While
    '    Return treeNode
    'End Function

#End Region

#Region "MENU"

    Dim IndTab, RegionTab, mYearTab, DataTab, DataQryTab As String
    Private Sub Mnu_CapVung_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles Mnu_CapVung.CheckedChanged

        If Mnu_CapVung.Checked = True Then
            IndTab = "Vung_Ind"
            RegionTab = "Vung_Region"
            mYearTab = "Vung_mYear"
            DataTab = "Vung_Data"
            DataQryTab = "Vung_DataQry"
        End If
        If Form_Loaded = True Then      'Nếu Form đã load rồi thì load lại, còn khi khởi tạo Form_Load thì ko chạy Load Main_Form trong Event này nếu ko sẽ chạy thành 2 lần LoadMainForm
            refreshLeftPanelforDothi()
        End If

        'Mnu_CapTinh.Checked = False
        'Mnu_CapHuyen.Checked = False
    End Sub

    Private Sub Mnu_CapTinh_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles Mnu_CapTinh.CheckedChanged

        If Mnu_CapTinh.Checked = True Then
            IndTab = "Tinh_Ind"
            RegionTab = "Tinh_Region"
            mYearTab = "Tinh_mYear"
            DataTab = "Tinh_Data"
            DataQryTab = "Tinh_DataQry"
        End If
        If Form_Loaded = True Then      'Nếu Form đã load rồi thì load lại, còn khi khởi tạo Form_Load thì ko chạy Load Main_Form trong Event này nếu ko sẽ chạy thành 2 lần LoadMainForm
            refreshLeftPanelforDothi()
        End If
        'Mnu_CapVung.Checked = False
        'Mnu_CapHuyen.Checked = False
    End Sub

    Private Sub Mnu_CapHuyen_CheckedChanged(sender As Object, e As ItemClickEventArgs) Handles Mnu_CapHuyen.CheckedChanged

        If Mnu_CapHuyen.Checked = True Then
            IndTab = "Huyen_Ind"
            RegionTab = "Huyen_Region"
            mYearTab = "Huyen_mYear"
            DataTab = "Tinh_Data"
            DataQryTab = "Huyen_DataQry"
        End If
        If Form_Loaded = True Then      'Nếu Form đã load rồi thì load lại, còn khi khởi tạo Form_Load thì ko chạy Load Main_Form trong Event này nếu ko sẽ chạy thành 2 lần LoadMainForm
            refreshLeftPanelforDothi()
        End If
        'Mnu_CapVung.Checked = False
        'Mnu_CapTinh.Checked = False
    End Sub
    Sub refreshLeftPanelforDothi()
        Try
            Dim Table_Indicator As DataTable = MyADOClass.DtFromQry("Select ID, Theme, Ind,IndicatorTN, Desc, mynote From " + IndTab + " where IndicatorTN <>'' order by ID")

            CheckedListBox_Ind.DataSource = Nothing
            CheckedListBox_Ind.DataSource = Table_Indicator
            CheckedListBox_Ind.DisplayMember = "IndicatorTN"
            '===Ini Clb_Year
            Dim Table_Year As DataTable = MyADOClass.DtFromQry("Select myear From " + mYearTab + "  order by mYear")
            CheckedListBox_Year.DataSource = Nothing
            CheckedListBox_Year.DataSource = Table_Year
            CheckedListBox_Year.DisplayMember = "myear"
            '===Ini Clb_Urban
            Dim Table_Urban As DataTable = MyADOClass.DtFromQry("Select RegionGrp, Region From " + RegionTab + "  order by ID")
            CheckedListBox_Urban.DataSource = Table_Urban
            CheckedListBox_Urban.DisplayMember = "Region"



            '============================
            '====Fill GridView1
            '====Fill CmbRegion và CmbmYear
            '============================
            'Dim myQuery As String = "Select * From " + DataQry + " "    ' where Ind = '1.1. Tổng dân số' and Xap like 'p%'"
            'Dim myQuery As String = "Select mYear, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, Desc,IndicatorTN,DataVal,Unit,DataRef,myNote,ID,ID from  DataQry Order by ID, mYear, Region"
            'Dim myQuery As String = "Select ID, mYear, RegionGrp, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, Desc,IndicatorTN,DataVal,Unit,DataRef,myNote from " + "DataQry" + " Order by mYear, Region"

            Dim Gridview2Qry As String = QryfromLeftPanel()
            GridControl2.DataSource = MyADOClass.DtFromQry(Gridview2Qry)

            '===ini Ckb_CheckAll            '===Phải chuyển CheckState.Indeterminate trước để kích hoạt checkChange Event
            'CkbUrb_ChkAll.CheckState = CheckState.Indeterminate
            'CkbInd_ChkAll.CheckState = CheckState.Indeterminate
            'CkbYear_ChkAll.CheckState = CheckState.Indeterminate
            'CkbUrb_ChkAll.CheckState = CheckState.Checked
            'CkbInd_ChkAll.CheckState = CheckState.Checked
            'CkbYear_ChkAll.CheckState = CheckState.Checked


            'sẽ không giữ tình trạng chọn nhiều chỉ số trong trường hợp Đồ thị theo một Năm và không giữ tình trạng chọn nhiều năm nếu CmbSoSanh = Đồ thị theo một chỉ số
            If XtraTabControl2.SelectedTabPageIndex = 1 And CmbSoSanh.SelectedItem = "Đồ thị theo một Chỉ số" Then
                'CkbInd_ChkAll.CheckState = CheckState.Checked   'Phải check trước thì uncheck mới có hiệu lực
                'CkbInd_ChkAll.CheckState = CheckState.Unchecked 'Cần uncheck để không chọn 1 row nào
                ''CheckedListBox_Ind.SetItemChecked(CheckedListBox_Ind.SelectedIndex, True)
                CheckedListBox_Ind.UnCheckAll()
                CheckedListBox_Ind.SetItemChecked(CheckedListBox_Ind.SelectedIndex, True)
                CkbInd_ChkAll.CheckState = CheckState.Indeterminate
                CheckedListBox_Urban.CheckAll()
                CheckedListBox_Year.CheckAll()
            ElseIf XtraTabControl2.SelectedTabPageIndex = 1 And CmbSoSanh.SelectedItem = "Đồ thị theo một Địa bàn" Then
                CheckedListBox_Urban.UnCheckAll()
                CheckedListBox_Urban.SetItemChecked(CheckedListBox_Urban.SelectedIndex, True)
                CkbUrb_ChkAll.CheckState = CheckState.Indeterminate
                CheckedListBox_Ind.CheckAll()
                CheckedListBox_Year.CheckAll()

            ElseIf XtraTabControl2.SelectedTabPageIndex = 1 And CmbSoSanh.SelectedItem = "Đồ thị theo một Năm" Then
                CheckedListBox_Year.UnCheckAll()
                CheckedListBox_Year.SetItemChecked(CheckedListBox_Year.SelectedIndex, True)
                CkbYear_ChkAll.CheckState = CheckState.Indeterminate
                CheckedListBox_Urban.CheckAll()
                CheckedListBox_Ind.CheckAll()

            End If

            BtnOK_Click(Nothing, Nothing)
        Catch ex As Exception

        End Try

    End Sub
    Private Sub MnuBangDuLieu_CheckStateChanged(ByVal sender As Object, ByVal e As ItemClickEventArgs) Handles MnuBangDuLieu.CheckedChanged
        AnHienBottomPanel()
    End Sub

    Private Sub MnuThongTin_CheckedChanged(ByVal sender As Object, ByVal e As ItemClickEventArgs) Handles MnuThongTin.CheckedChanged
        AnHienLeftPanel()
    End Sub

    Private Sub MnuExportXls_Click(ByVal sender As System.Object, ByVal e As ItemClickEventArgs) Handles MnuExportXls.ItemClick
        mnuExelExport("MnuExportXls")
    End Sub


    Private Sub MnuExcelSheets_Click(ByVal sender As System.Object, ByVal e As ItemClickEventArgs) Handles MnuExcelSheets.ItemClick
        mnuExelExport("MnuExcelSheets")
    End Sub

    Private Sub mnuExelExport(ByVal mnu As String)
        '=================================
        '1. Tạo Qrystr là Query theo điều kiện checked của LeftPanel
        '2. Tạo myTable từ bảng DataQry Access. Chọn theo điều kiện QryStr trên
        '=================================
        Dim activeDevGridview As DevExpress.XtraGrid.Views.Grid.GridView

        If XtraTabControl2.SelectedTabPageIndex = 0 Then
            activeDevGridview = GridView1
        Else
            activeDevGridview = GridView2
        End If
        Dim CklUrban, CklYear As CheckedListBoxControl
        'Đặt Constant trước
        ' If XtraTabControl2.SelectedTabPageIndex = 0 Then   'Trường hợp 1: TabPage1 is actived thì clkUrban là CheckListBox thực thụ
        'Nếu rơi vào THợp 2, TabPage2 activated thì clkUrban  là Combobox
        CklUrban = CheckedListBox_Urban
        CklYear = CheckedListBox_Year
        'Else 'Trường hợp 2: TabPage2 is actived: clkUrban  là Combobox
        '    CklUrban = CheckedListBox_Urban     'CheckedListBox_Urban
        '    CklYear = CheckedListBox_Year       'CheckedListBox_Year
        'End If
        '====
        '====Qry Urban
        Dim QryUrban As String = ""
        For Each item In CklUrban.CheckedItems '= 0 To CheckedListBox_Urban.ItemCount - 1
            'Qry = Qry + "Urban = " + CheckedListBox_Urban.CheckedItems.Item(i).ToString + " Or "
            QryUrban = QryUrban + "Region = " + """" + item.row(1) + """" + " Or "
        Next
        If QryUrban <> "" Then      '=====================Sưả Where nếu muốn datagridview chỉ nhận  Urban trong cmbRegion
            QryUrban = QryUrban.Substring(0, QryUrban.Length - 4)
        Else
            MessageBox.Show("Bạn cần chọn ít nhất một Đô thị", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
            'QryUrban = "Region = " + """" + "abcded*%P*&xxxabc" + """" 'Để dùng cho trường hợp ngwoif dùng ko chọn 1 chỉ số nào
        End If

        '====
        '====Qry Indicator
        Dim QryIndicator As String = ""
        'If XtraTabControl2.SelectedTabPageIndex = 0 Then   'Trường hợp 1: TabPage1 is actived thì clkUrban là CheckListBox thực thụ
        'Nếu rơi vào THợp 2, TabPage2 activated thì clkUrban  là Combobox

        For Each item In CheckedListBox_Ind.CheckedItems '= 0 To CheckedListBox_Urban.ItemCount - 1
            'QryIndicator = QryIndicator + item.Row(1).ToString + " , "      'ITem.Row(1) tức là lấy collumn thứ 1 của Row cho Item ấy. 1 Item này có khá nhiều Row
            QryIndicator = QryIndicator + "Ind = " + """" + item.row("Ind") + """" + " Or "     'ITem.Row(1) tức là lấy collumn thứ 1 của Row cho Item ấy. 1 Item này có khá nhiều Row
        Next
        'Else 'Trường hợp 2: TabPage2 is actived: clkUrban  là Combobox
        '    CklUrban = CheckedListBox_Urban     'CheckedListBox_Urban
        '    CklYear = CheckedListBox_Year   'CheckedListBox_Year
        '    QryIndicator = QryIndicator + "Ind = " + """" + cmb_Ind.SelectedItem.Row("Ind").ToString + """" + " OR "      'ITem.Row(1) tức là lấy collumn thứ 1 của Row cho Item ấy. 1 Item này có khá nhiều Row
        'End If

        If QryIndicator <> "" Then
            QryIndicator = QryIndicator.Substring(0, QryIndicator.Length - 4)
        Else
            MessageBox.Show("Bạn cần chọn ít nhất một Chỉ số", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
            'QryIndicator = "Ind = " + """" + "abcded*%P*&xxxabc" + """" 'Để dùng cho trường hợp ngwoif dùng ko chọn 1 chỉ số nào
        End If

        '====
        '====Qry Year
        Dim QryYear As String = ""
        For Each item In CklYear.CheckedItems '= 0 To CheckedListBox_Urban.ItemCount - 1
            QryYear = QryYear + "mYear = " + """" + item.row(0) + """" + " Or "
        Next
        If QryYear <> "" Then
            QryYear = QryYear.Substring(0, QryYear.Length - 4)
        Else
            MessageBox.Show("Bạn cần chọn ít nhất một Năm dữ liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
            'QryYear = "mYear = " + """" + "abcded*%P*&xxxabc" + """" 'Để dùng cho trường hợp ngwoif dùng ko chọn 1 chỉ số nào

        End If


        Dim Qrystr As String = ""

        If (QryUrban <> "" Or QryYear <> "") Then
            'Qrystr = "Select Urban, Year," + QryIndicator + " from " + myTable + " Where (" + QryUrban + """" + ") AND (" + QryYear + """" + ")"
            Qrystr = "Select DataID,ID, mYear, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit,DataRef, TarMin, TarMax, ChuanHoa,  myNote from " + DataQryTab + " Where (" + QryUrban + ") AND (" + QryIndicator + ") AND (" + QryYear + ")" + " Order by mYear, Region, ID"
        Else
            'Qrystr = "Select Urban, Year," + QryIndicator + " from " + myTable
            Qrystr = "Select DataID,ID, mYear, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, IndicatorTN,Desc, TuSo, MauSo, DataVal, Unit, DataRef, TarMin, TarMax, ChuanHoa, myNote from " + DataQryTab + " Order by mYear, Region, ID"
        End If
        'MyADOClass.KetNoi_Open()
        Dim myTable As DataTable = MyADOClass.DtFromQry(Qrystr)
        'MyADOClass.KetNoi_Close()


        '=================
        'Xuất dữ liệu==========
        '=================
        'Dim myTable As DataTable = MyADOClass.TableFromQry("Select ID, mYear, Region,Theme,Ind,  TenTuSo, TenMauSo, SoNhan, Desc,IndicatorTN,DataVal,Unit,DataRef from  DataQry Order by mYear, Region")
        Dim Dt_Export As New Dt_Export
        'Dt_Export.exportDataToExcel(gridcontrol1.datasource)
        'Dt_Export.exportDataToExcel1(gridcontrol1.datasource)
        'Dim FilDataRows = myTable.Select("Region= " + "'" + cmbRegion.SelectedItem("Region").ToString + "' And mYear = '" + CmbmYear.SelectedItem("mYear").ToString + "'")
        'Dim selectedRows = New List(Of DataRow)()
        'selectedRows = FilDataRows
        Dim FilDataRows = Nothing
        ''If myTable.Rows.Count < 1000 Then
        ''    Dt_Export.exportDataToExcel(gridcontrol1.datasource)
        ''Else
        Dim excelfile As String
        If mnu = "MnuExportXls" Then
            excelfile = Dt_Export.subExportToExcel(FilDataRows, myTable, CklUrban.CheckedItems)   'CklUrban.CheckedItems là list các Urban được chọn, dùng để làm các Sheet

        Else
            excelfile = Dt_Export.subExportToExcelSheet(FilDataRows, myTable, CklUrban.CheckedItems)   'CklUrban.CheckedItems là list các Urban được chọn, dùng để làm các Sheet
        End If
        If excelfile <> "" Then
            Dim ans As DialogResult = MessageBox.Show("Bạn có muốn mở file vừa xuất?", "Xuất file .. ", MessageBoxButtons.YesNo)
            If ans = Windows.Forms.DialogResult.Yes Then
                System.Diagnostics.Process.Start(excelfile)
            End If
        End If
    End Sub

    Private Sub MnuSuaCautruc_Click(ByVal sender As System.Object, ByVal e As ItemClickEventArgs) Handles MnuSuaCautruc.ItemClick
        If XtraTabControl2.SelectedTabPageIndex = 0 Then
            If BtnSaveGrV1.Enabled = True Then
                btnSaveGrV1_Click(Nothing, Nothing)
            End If
        Else
            If BtnSaveGrV2.Enabled = True Then
                btnSaveGrV2_Click(Nothing, Nothing)
            End If
        End If
        Me.Hide()

        FrmUpdateStructure.ShowDialog()

    End Sub

    Private Sub mnuChonfile_Click(ByVal sender As System.Object, ByVal e As ItemClickEventArgs) Handles mnuChonfile.ItemClick
        Dim f As New OpenFileDialog
        f.Filter = "VUI data |*.VUI"
        f.Title = "Chọn File dữ liệu"

        Dim FullFileName As String = " "
        If f.ShowDialog() = DialogResult.OK Then
            FullFileName = f.FileName
            MnuFileHientai.Hint = FullFileName
            If FullFileName.Length > 25 Then
                MnuFileHientai.Caption = FullFileName.Substring(0, 25) + "..."
            Else
                MnuFileHientai.Caption = FullFileName
            End If
            Dim sWriter As StreamWriter = New StreamWriter(Application.StartupPath + "/myFile.dll", False, System.Text.Encoding.UTF8)   'myFile.dll là file chứa đường dẫn tới dataFile tại dòng 1

            sWriter.WriteLine(FullFileName)
            sWriter.Flush()
            sWriter.Close()
            LoadMainForm()
        End If
        'Dim IO_Reader_Writer As New IO_Reader_Writer
        'Dim Curdata As String = IO_Reader_Writer.doc1dong(Application.StartupPath + "\myFile.dll", 1)         'sourceFile
        'ToolStripStatusLabel1.Text = Curdata

    End Sub

    Private Sub MnuLuudulieu_Click(ByVal sender As System.Object, ByVal e As ItemClickEventArgs) Handles MnuLuudulieu.ItemClick
        If XtraTabControl2.SelectedTabPageIndex = 0 Then
            If BtnSaveGrV1.Enabled = True Then
                btnSaveGrV1_Click(Nothing, Nothing)
            End If
        Else
            If BtnSaveGrV2.Enabled = True Then
                btnSaveGrV2_Click(Nothing, Nothing)
            End If
        End If
        Dim f As New SaveFileDialog
        f.Title = "Lưu File dữ liệu"
        f.Filter = "VUI data |*.VUI"
        Dim IO_Reader_Writer As New IO_Reader_Writer
        Dim Curdata As String = IO_Reader_Writer.doc1dong(Application.StartupPath + "\myFile.dll", 1)         'sourceFile
        Dim TargetFile As String = ""

        If f.ShowDialog() = DialogResult.OK Then

            TargetFile = f.FileName
            System.IO.File.Copy(Curdata, TargetFile, True)
            MsgBox("Dữ liệu đã được lưu lại")
        End If
        'If System.IO.File.Exists(Curdata) = True And TargetFile <> "" Then  'FIle nguồn tồn tại và nhấn save
        '    If System.IO.File.Exists(TargetFile) = True Then                'Nếu File đích đã tồn tại => hỏi có ghi đè
        '        Dim ans As DialogResult = MessageBox.Show("Bạn muốn ghi đè?", "File đích đã tồn tại", MessageBoxButtons.YesNo)
        '        If ans = Windows.Forms.DialogResult.Yes Then
        '            System.IO.File.Copy(Curdata, TargetFile, True)
        '            MsgBox("Dữ liệu đã được lưu lại")
        '        End If
        '    End If

        'End If

    End Sub
    Private Declare Function WinExec Lib "kernel32" (ByVal lpCmdLine As String, ByVal nCmdShow As Long) As Long

    Private Sub mnuHuongdansudung_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHuongdansudung.ItemClick
        'System.Diagnostics.Process.Start(Application.StartupPath + "\UserGuide\FoxitReader.exe " + Application.StartupPath + "\UserGuide\UserGuide.pdf")
        'System.Diagnostics.Process.Start(Application.StartupPath + "\UserGuide\FoxitReader.exe " + Application.StartupPath + "\UserGuide\UserGuide.pdf")
        Dim thepath As String = Application.StartupPath
        Dim command As String = """" + thepath + "\UserGuide\FoxitReader.exe" + """" + " " + """" + thepath + "\UserGuide\UserGuide.pdf" + """"

        WinExec(command, 8)     'Declare Winexec nhw tren
        'Dung cai duoi de chay bang chuong trinh mac dinh cua Window
        'Dim command As String = thepath + "\UserGuide\UserGuide.pdf"
        'System.Diagnostics.Process.Start(command)
    End Sub

    Private Sub MnuFileHientai_Click(ByVal sender As System.Object, ByVal e As ItemClickEventArgs) Handles MnuFileHientai.ItemClick
        Dim IO_Reader_Writer As New IO_Reader_Writer
        Dim Curdata As String = IO_Reader_Writer.doc1dong(Application.StartupPath + "\myFile.dll", 1)
        MsgBox("Bạn đang thao tác trên file dữ liệu tại đường dẫn:" + Chr(13) + Curdata)
    End Sub

    Private Sub mnuTacGia_Click(ByVal sender As System.Object, ByVal e As ItemClickEventArgs) Handles mnuTacGia.ItemClick

        'System.Diagnostics.Process.Start(Application.StartupPath + "\About Us.exe")
        Frm_modan.ShowDialog()
    End Sub

    Private Sub MnuThoat_Click(ByVal sender As System.Object, ByVal e As ItemClickEventArgs) Handles MnuThoat.ItemClick
        If XtraTabControl2.SelectedTabPageIndex = 0 Then
            If BtnSaveGrV1.Enabled = True Then
                btnSaveGrV1_Click(Nothing, Nothing)
            End If
        Else
            If BtnSaveGrV2.Enabled = True Then
                btnSaveGrV2_Click(Nothing, Nothing)
            End If
        End If
        'Dim dt As DataTable = gridcontrol1.datasource
        'Dim dtchange As DataTable = dt.GetChanges()

        'If (dtchange Is Nothing) Then     'Xác định xem datagridview có bị change ko
        '    Application.Exit()
        'Else    'Trường hợp dtchange <> nothing thì mới xet được lệnh dtchange.rows
        '    If dtchange.Rows.Count = 0 Then
        '        Application.Exit()
        '    End If
        'End If

        'Dim str = "select DataID, dataVal, DataRef From " + DataQry + " "
        'Dim dlgResult As DialogResult = MessageBox.Show("Bạn có muốn lưu thay đổi vào CSDL", "Lưu thay đổi", MessageBoxButtons.YesNoCancel)
        'If dlgResult = Windows.Forms.DialogResult.Yes Then
        '    MyADOClass.Update_CSDL(gridcontrol1.datasource, str)
        'ElseIf dlgResult = Windows.Forms.DialogResult.Cancel Then       'Sẽ không thoát chương trình
        '    Exit Sub
        'ElseIf dlgResult = Windows.Forms.DialogResult.No Then
        '    'Reject any change in datagridview
        '    dt.RejectChanges()

        'End If
        Application.Exit()
    End Sub

#End Region


#Region "CHART"

    Sub AddChart(ByVal ContainerCtr As Control)   'ValueDataMembers là trường giá trị Chỉ số

        ''Tao them 1 column cho bang du lieu, column nay dang so va =gia tri cua column ChuanHoa (dang text)

        'Dim dgv2dtable As DataTable = GridControl2.DataSource
        ''===Filter theo Row có giá trị ChuanHoa <>""
        'Dim FilterView As DataView = dgv2dtable.DefaultView
        'Try
        '    FilterView.RowFilter = "ChuanHoa <> ''"
        'Catch ex As Exception
        'End Try

        'If dgv2dtable.Columns("ValDlb") Is Nothing Then
        '    Try
        '        dgv2dtable.Columns.Add(New DataColumn("ValDlb", Type.GetType("System.Double")))
        '    Catch ex As Exception

        '    End Try

        'End If
        'For r As Integer = 0 To dgv2dtable.Rows.Count - 1
        '    Try
        '        dgv2dtable.Rows(r)("Valdlb") = dgv2dtable.Rows(r)("ChuanHoa")
        '    Catch ex As Exception

        '    End Try
        'Next


        'Dim mysortTable As Sort_Table = New Sort_Table

        'mysortTable.sortDataTable(dgv2dtable, "valdlb, IndicatorTN", False)


        'gridcontrol2.datasource




        'If RadIndBased.Checked = True Then
        '    myChart.SeriesDataMember = "Desc"
        '    myChart.SeriesTemplate.ArgumentDataMember = "Region"
        'Else
        '    myChart.SeriesDataMember = "Region"
        '    myChart.SeriesTemplate.ArgumentDataMember = "Desc"

        'End If

        'chartControl1.Series.Item(0).ValueDataMembers = "data_value"

        'myChart.SeriesTemplate.ValueDataMembers.AddRange(New String() {"TQ03"})
        'myChart.SeriesTemplate.ValueDataMembers.AddRange(New String() {ValueDataMembers})
        xtraChart.SeriesTemplate.ValueDataMembers.AddRange(New String() {"ChuanHoa"})
        'chartControl1.SeriesTemplate.View = New StackedBarSeriesView()
        'chartControl1.SeriesTemplate.View = New LineSeriesView
        'myChart.SeriesTemplate.View = New LineSeriesView
        'myChart.SeriesNameTemplate.BeginText = ""
        GridView2.Columns("Theme").OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList
        GridView2.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = True
        Dim table As DataTable = DirectCast(GridView2.DataSource, DataView).Table
        Dim filteredDataView As New DataView(table)
        filteredDataView.RowFilter = DevExpress.Data.Filtering.CriteriaToWhereClauseHelper.GetDataSetWhere(GridView2.ActiveFilterCriteria)
        Dim SrcChart_table As DataTable = filteredDataView.ToTable
        'Dim dtv As DataView = GridView2.DataSource
        '
        ' dtv.RowFilter = "ChuanHoa <> 0 and ChuanHoa is not null"
        'dtv.RowFilter = "ChuanHoa =0"
        '(gv.Sort())
        '===Tittle Format
        Dim MyTittle As ChartTitle = New ChartTitle
        MyTittle.Alignment = StringAlignment.Center
        xtraChart.Titles.Clear()
        If CmbSoSanh.SelectedItem = "Đồ thị theo một Năm" Then
            'dgv2dtable.DefaultView.Sort = ("Region ASC, Ind ASC, valdlb asc")
            filteredDataView.Sort = ("ID ASC, Region ASC, ChuanHoa ASC")  'ID = IndID
            'dtv.Sort = ("Region ASC")
            'GridView2.Columns("ID").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
            '=====
            ' GridView2.Columns("ChuanHoa").FilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo("[ChuanHoa] <> 0 and [ChuanHoa] IsNot System.DBNull.Value ")

            'myChart.DataSource = GridControl2.DataSource
            xtraChart.DataSource = Nothing
            'myChart.DataSource = GridView2.DataSource
            xtraChart.DataSource = SrcChart_table 'filteredDataView
            'myChart.DataSource = filteredDataView
            '======


            'myChart.SeriesNameTemplate.BeginText = "Năm "
            'myChart.SeriesDataMember = "mYear"

            'myChart.SeriesNameTemplate.BeginText = CheckedListBox_Ind.SelectedItem("IndicatorTN").ToString
            xtraChart.SeriesDataMember = "Region"
            xtraChart.SeriesTemplate.ArgumentDataMember = "IndicatorTN"

            MyTittle.Text = "Đồ thị năm " + CheckedListBox_Year.SelectedItem("mYear").ToString

            'CType(myChart.Diagram, XYDiagram).AxisX.Title.Text = "Đồ thị năm " + CheckedListBox_Year.SelectedItem("mYear").ToString

        ElseIf CmbSoSanh.SelectedItem = "Đồ thị theo một Chỉ tiêu" Then    'Đồ thị theo một chỉ số
            'dgv2dtable.DefaultView.Sort = ("mYear ASC, Region ASC,  valdlb asc")
            'dgv2dtable.DefaultView.Sort = ("mYear ASC,  ChuanHoa asc")

            filteredDataView.Sort = ("region ASC, mYear ASC, ChuanHoa asc")
            xtraChart.DataSource = SrcChart_table

            xtraChart.SeriesNameTemplate.BeginText = ""
            xtraChart.SeriesDataMember = "mYear"
            xtraChart.SeriesTemplate.ArgumentDataMember = "Region"

            MyTittle.Text = "Đồ thị chỉ tiêu " + CheckedListBox_Ind.SelectedItem("IndicatorTN").ToString
            ' CType(myChart.Diagram, XYDiagram).AxisX.Title.Text = "Đồ thị chỉ số " + CheckedListBox_Ind.SelectedItem("IndicatorTN").ToString
        ElseIf CmbSoSanh.SelectedItem = "Đồ thị theo một Địa bàn" Then    'Đồ thị theo một chỉ số
            'dgv2dtable.DefaultView.Sort = ("mYear ASC, Ind ASC, valdlb asc")
            filteredDataView.Sort = ("ID ASC, myear ASC, ChuanHoa asc")       'ID=IndID
            xtraChart.DataSource = SrcChart_table


            xtraChart.SeriesNameTemplate.BeginText = ""
            xtraChart.SeriesDataMember = "mYear"
            xtraChart.SeriesTemplate.ArgumentDataMember = "IndicatorTN"

            MyTittle.Text = "Đồ thị " + CheckedListBox_Urban.SelectedItem("Region").ToString
            ' CType(myChart.Diagram, XYDiagram).AxisX.Title.Text = "Đồ thị " + CheckedListBox_Urban.SelectedItem("Region").ToString
        End If



        xtraChart.Titles.Add(MyTittle)
        'Me.Controls.Add(chartControl1)

        xtraChart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center
        xtraChart.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside
        xtraChart.Legend.Direction = LegendDirection.LeftToRight
        xtraChart.Legend.UseCheckBoxes = True
        xtraChart.Legend.Font = New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point, 1)
        ChartCustomize(xtraChart)
        ContainerCtr.Controls.Add(xtraChart)
        xtraChart.BringToFront()
        xtraChart.Focus()
        'myChart.SeriesDataMember = "mYear"
        'myChart.SeriesTemplate.ArgumentDataMember = "Desc"
        ''chartControl1.Series.Item(0).ValueDataMembers = "data_value"
        'myChart.SeriesTemplate.ArgumentDataMember = "Region"
        ''myChart.SeriesTemplate.ValueDataMembers.AddRange(New String() {"TQ03"})
        ''myChart.SeriesTemplate.ValueDataMembers.AddRange(New String() {ValueDataMembers})
        'myChart.SeriesTemplate.ValueDataMembers.AddRange(New String() {"ValDlb"})
        ''chartControl1.SeriesTemplate.View = New StackedBarSeriesView()
        ''chartControl1.SeriesTemplate.View = New LineSeriesView
        'myChart.SeriesTemplate.View = New LineSeriesView


        ''Me.Controls.Add(chartControl1)
        'BtnSave1.Location = New System.Drawing.Point(3, myChart.Height - 35)
        'lblLabel3.Location = New System.Drawing.Point(38, myChart.Height - 20)
        'cmbViewType.Location = New System.Drawing.Point(185, myChart.Height - 27)
        'myChart.Anchor = AnchorStyles.Bottom And AnchorStyles.Left And AnchorStyles.Right And AnchorStyles.Top
        'myChart.Anchor = AnchorStyles.None
    End Sub
    Sub ChartCustomize(ByVal myChartCtr As ChartControl)
        Try 'Sử dụng cho Radar chart
            Dim Diagram As RadarDiagram = CType(xtraChart.Diagram, RadarDiagram)
            Diagram.AxisX.Label.ResolveOverlappingOptions.AllowHide = False
            Diagram.AxisX.Label.TextDirection = RadarAxisXLabelTextDirection.Radial
            Diagram.AxisX.Label.Font = New Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point, 1)
            ' '' ''==Strip
            '' ''Diagram.AxisY.Thickness = 2
            ' '' ''Diagram.AxisX.Strips.Add(New Strip("Strip 1", 5, 15))
            ' '' ''Diagram.BackColor = Color.White
            '' ''Diagram.BackColor = Color.Red
            '' ''Diagram.FillStyle.FillMode = FillMode.Gradient
            '' ''CType(Diagram.FillStyle.Options, PolygonGradientFillOptions).Color2 = Color.Green
            '' ''CType(Diagram.FillStyle.Options, DevExpress.XtraCharts.PolygonGradientFillOptions).GradientMode = PolygonGradientMode.FromCenter
            ' '' ''Diagram.FillStyle.Options = FillMode.Gradient
            '' ''Diagram.AxisY.Interlaced = True
            '' ''Diagram.AxisX.Interlaced = False

            ' '' ''Diagram.AxisY.InterlacedFillStyle
            ' '' '' Diagram.AxisY.InterlacedFillStyle.FillMode = FillMode.Hatch
            '' ''Diagram.AxisY.InterlacedFillStyle.FillMode = FillMode.Gradient
            '' ''Diagram.AxisY.InterlacedColor = Color.Blue
            '' ''CType(Diagram.AxisY.InterlacedFillStyle.Options, PolygonGradientFillOptions).Color2 = Color.Green
            '' ''CType(Diagram.AxisY.InterlacedFillStyle.Options, PolygonGradientFillOptions).GradientMode = PolygonGradientMode.FromCenter
            '' ''Diagram.AxisY.Tickmarks.MinorVisible = False
            '' ''Diagram.AxisY.NumericScaleOptions.GridSpacing = 0.2
            '' ''Diagram.AxisY.NumericScaleOptions.AutoGrid = False
            ' '' ''Diagram.AxisY.WholeRange.MaxValue = 1
            ' '' ''Diagram.AxisY.VisualRange.MaxValue = 1
            ' '' ''Diagram.AxisX.WholeRange.MaxValue = 1
            ' '' ''Diagram.AxisX.VisualRange.MaxValue = 1
            ' '' ''Diagram.AxisX.VisualRange.AutoSideMargins = False
            ' '' ''Diagram.AxisY.VisualRange.AutoSideMargins = False
            ' '' ''Diagram.AxisX.WholeRange.AutoSideMargins = False
            ' '' ''Diagram.AxisY.WholeRange.AutoSideMargins = False
            '' ''Diagram.AxisY.GridLines.Visible = True
            '' ''Diagram.AxisY.GridLines.LineStyle.Thickness = 2
            '' ''Diagram.AxisY.GridLines.Color = Color.Red
            '' ''Diagram.AxisY.GridLines.LineStyle.DashStyle = DashStyle.Solid
            ' '' ''Diagram.AxisX.InterlacedColor = Color.AliceBlue

            ' '' ''Diagram.AxisX.InterlacedFillStyle.FillMode = FillMode.Solid
            '' '' '' Customize the strip's behavior.
            ' '' ''Diagram.AxisX.Strips(0).Visible = True
            ' '' ''Diagram.AxisX.Strips(0).ShowAxisLabel = True
        Catch ex As Exception

        End Try

        '====Chart Type
        If (cmbChartViewType.SelectedItem = "Dạng điểm") Then
            xtraChart.SeriesTemplate.View = New LineSeriesView()
        ElseIf (cmbChartViewType.SelectedItem = "Dạng cột so sánh") Then
            xtraChart.SeriesTemplate.View = New StackedBarSeriesView
        ElseIf (cmbChartViewType.SelectedItem = "Dạng cột") Then
            xtraChart.SeriesTemplate.View = New SideBySideBarSeriesView
        ElseIf (cmbChartViewType.SelectedItem = "Dạng hoa gió 1") Then
            xtraChart.SeriesTemplate.View = New RadarAreaSeriesView
        ElseIf (cmbChartViewType.SelectedItem = "Dạng hoa gió 2") Then
            xtraChart.SeriesTemplate.View = New RadarLineSeriesView

        ElseIf (cmbChartViewType.SelectedItem = "Dạng hoa gió 3") Then
            xtraChart.SeriesTemplate.View = New RadarPointSeriesView
        ElseIf (cmbChartViewType.SelectedItem = "6") Then
            xtraChart.SeriesTemplate.View = New PolarLineSeriesView
        ElseIf (cmbChartViewType.SelectedItem = "7") Then
            xtraChart.SeriesTemplate.View = New PolarAreaSeriesView
        ElseIf (cmbChartViewType.SelectedItem = "8") Then
            xtraChart.SeriesTemplate.View = New PolarPointSeriesView
        ElseIf (cmbChartViewType.SelectedItem = "9") Then
            xtraChart.SeriesTemplate.View = New PieSeriesView
        ElseIf (cmbChartViewType.SelectedItem = "10") Then
            xtraChart.SeriesTemplate.View = New DoughnutSeriesView
        End If


        Try
            'For Each sr In myChartCtr.Series
            '    sr.LabelsVisibility = True
            'Next

            '========X Customize
            CType(myChartCtr.Diagram, XYDiagram).AxisX.Title.Visible = False
            CType(myChartCtr.Diagram, XYDiagram).AxisX.Label.Angle = -45
            CType(myChartCtr.Diagram, XYDiagram).AxisY.Label.EnableAntialiasing = True
            CType(myChartCtr.Diagram, XYDiagram).AxisX.Label.Font = New Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Point, 1)
            '========Y Customize
            CType(myChartCtr.Diagram, XYDiagram).AxisY.Interlaced = True
            'CType(myChartCtr.Diagram, XYDiagram).AxisY.GridSpacing = 1000
            CType(myChartCtr.Diagram, XYDiagram).AxisY.Label.Angle = 0
            CType(myChartCtr.Diagram, XYDiagram).AxisY.Label.EnableAntialiasing = True
            CType(myChartCtr.Diagram, XYDiagram).AxisY.Label.Font = New Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Point, 1)

            'CType(myChartCtr.Diagram, XYDiagram).AxisY.DateTimeOptions.Format = DateTimeFormat.MonthAndDay
        Catch ex As Exception

        End Try

        ''=====Chart Width
        'XtraScrollable_Chart.AutoScroll = True
        ''XtraTabControl2.TabPages(1).AutoScroll = True
        ''Xét số Chỉ số hoặc số Năm được chọn (Sẽ bằng tổng count chỉ số + năm được chọn -1 do sẽ có duy nhất 1 chỉ số hoặc 1 năm được chọn)
        'Dim a As Integer = CheckedListBox_Ind.CheckedItems.Count + CheckedListBox_Year.CheckedItems.Count - 1
        'Dim numRow As Integer = GridView2.RowCount
        'Try
        '    If a <> 0 Then
        '        If cmbChartViewType.SelectedIndex = 0 Or cmbChartViewType.SelectedIndex = 1 Or cmbChartViewType.SelectedIndex = 2 Then
        '            myChart.Width = numRow * 90 / a
        '        Else
        '            myChart.Width = numRow / a
        '        End If
        '    End If

        'Catch ex As Exception

        'End Try



        'If myChart.Width < XtraScrollable_Chart.Width - 10 Then
        '    myChart.Width = XtraScrollable_Chart.Width - 2
        '    'myChart.Height = XtraScrollable_Chart.Height - 2
        '    ''''lblLabel3.Location = New System.Drawing.Point(lblLabel3.Location.X, myChart.Height)
        'Else 'Trường hợp này có thêm VERTICAL scroll nên phải để nhỏ hơn
        '    'myChart.Height = XtraScrollable_Chart.Height - 20
        '    ''''lblLabel3.Location = New System.Drawing.Point(lblLabel3.Location.X, myChart.Height + 20)
        'End If
        ''myChart.Width = 2000
        ''myChart.Height = 1000
        If TxtChartWidth.Text <> "" Then
            xtraChart.Width = Convert.ToDouble(TxtChartWidth.Text)
        End If
        If TxtChartHeigh.Text <> "" Then
            xtraChart.Height = Convert.ToDouble(TxtChartHeigh.Text)
        End If
        '========Chart View Type
        'cmbChartViewType_SelectedIndexChanged(Nothing, Nothing)
        'GridView2.Columns("myNote").Visible = False
        ''GridView2.Columns("ValDlb").Visible = False
        'GridView2.Columns("RegionGrp").Caption = "Nhóm đô thị"
        'GridView2.Columns("RegionGrp").Width = 200
    End Sub
    '==========================
    '====Đổi màu cho Axis LABEL
    '==========================
    Private Sub xtraChart_CustomDrawAxisLabel1(sender As Object, e As CustomDrawAxisLabelEventArgs) Handles xtraChart.CustomDrawAxisLabel
        Dim axis As AxisBase = e.Item.Axis
        If TypeOf axis Is AxisX Or TypeOf axis Is AxisX3D Or TypeOf axis Is RadarAxisX Then
            Dim axisTxt As String = e.Item.Text
            'For i 
            If axisTxt = "Tỷ lệ hộ nghèo nông thôn" Then
                e.Item.TextColor = Color.Red
            ElseIf axisTxt = "Tỷ lệ hộ nghèo thành thị" Then
                e.Item.Text = "+" + e.Item.Text
                e.Item.TextColor = Color.Green
            ElseIf axisTxt = "Năng suất lao động xã hội" Then
                e.Item.Text = "Zero"
                e.Item.TextColor = Color.Blue
            End If
        Else

        End If

    End Sub
    Private Sub ChartControl1_MouseWheel(sender As Object, e As MouseEventArgs)
        'CType(sender, ChartControl).BeginInit()
        'Dim diagram As XYDiagram = TryCast((CType(sender, ChartControl)).Diagram, XYDiagram)
        'Dim minValueX As Double = diagram.AxisX.VisualRange.MinValueInternal
        'Dim maxValueX As Double = diagram.AxisX.VisualRange.MaxValueInternal
        'Dim minValueY As Double = diagram.AxisY.VisualRange.MinValueInternal
        'Dim maxValueY As Double = diagram.AxisY.VisualRange.MaxValueInternal
        'Dim scrollMinValueX As Double = diagram.AxisX.WholeRange.MinValueInternal
        'Dim scrollMaxValueX As Double = diagram.AxisX.WholeRange.MaxValueInternal
        'Dim scrollMinValueY As Double = diagram.AxisY.WholeRange.MinValueInternal
        'Dim scrollMaxValueY As Double = diagram.AxisY.WholeRange.MaxValueInternal
        'Dim coord As DiagramCoordinates = diagram.PointToDiagram(e.Location)
        'Dim x As Double = coord.NumericalArgument
        'Dim y As Double = coord.NumericalValue
        'If e.Delta > 0 AndAlso maxValueY - minValueY > 0.1 AndAlso maxValueX - minValueX > 0.1 Then
        '	diagram.AxisX.VisualRange.SetMinMaxValues(0.2 * x + 0.8 * minValueX, 0.2 * x + 0.8 * maxValueX)
        '	diagram.AxisY.VisualRange.SetMinMaxValues(0.2 * y + 0.8 * minValueY, 0.2 * y + 0.8 * maxValueY)
        'End If
        'If e.Delta < 0 AndAlso (minValueX > scrollMinValueX OrElse maxValueX < scrollMinValueX OrElse minValueY > scrollMinValueY OrElse maxValueY < scrollMaxValueY) Then
        '	Dim minValueInternalX As Double = If((1.2 * minValueX - 0.2 * x >= scrollMinValueX), 1.2 * minValueX - 0.2 * x, scrollMinValueX)
        '	Dim maxValueInternalX As Double = If((1.2 * maxValueX - 0.2 * x <= scrollMaxValueX), 1.2 * maxValueX - 0.2 * x, scrollMaxValueX)
        '	Dim minValueInternalY As Double = If((1.2 * minValueY - 0.2 * y >= scrollMinValueY), 1.2 * minValueY - 0.2 * y, scrollMinValueY)
        '	Dim maxValueInternalY As Double = If((1.2 * maxValueY - 0.2 * y <= scrollMaxValueY), 1.2 * maxValueY - 0.2 * y, scrollMaxValueY)
        '	diagram.AxisX.VisualRange.SetMinMaxValues(minValueInternalX, maxValueInternalX)
        '	diagram.AxisY.VisualRange.SetMinMaxValues(minValueInternalY, maxValueInternalY)
        'End If
        'CType(sender, ChartControl).EndInit()
    End Sub

    Private Sub cmbChartViewType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbChartViewType.SelectedIndexChanged
        ChartCustomize(xtraChart)
        'BtnOK_Click(Nothing, Nothing)
    End Sub
    Private Sub CmbSoSanh_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbSoSanh.SelectedIndexChanged
        If XtraTabControl2.SelectedTabPageIndex = 0 Then
            Return 'Để không chạy khi khởi động
        End If
        '=============
        'Enable hoặc disable CkbInd_ChkAll khi chọn Đồ thị theo một năm và Disable CkbYear_ChkAll khi chọn Đồ thị theo một chỉ số 
        disableChkAll()
        'sẽ không giữ tình trạng chọn nhiều chỉ số trong trường hợp Đồ thị theo một Năm và không giữ tình trạng chọn nhiều năm nếu CmbSoSanh = Đồ thị theo một chỉ số
        If XtraTabControl2.SelectedTabPageIndex = 1 And CmbSoSanh.SelectedItem = "Đồ thị theo một Chỉ tiêu" Then
            'CkbInd_ChkAll.CheckState = CheckState.Checked   'Phải check trước thì uncheck mới có hiệu lực
            'CkbInd_ChkAll.CheckState = CheckState.Unchecked 'Cần uncheck để không chọn 1 row nào
            ''CheckedListBox_Ind.SetItemChecked(CheckedListBox_Ind.SelectedIndex, True)
            CheckedListBox_Ind.UnCheckAll()
            CheckedListBox_Ind.SetItemChecked(CheckedListBox_Ind.SelectedIndex, True)

            CkbInd_KT.CheckState = CheckState.Indeterminate
            CkbInd_XH.CheckState = CheckState.Indeterminate
            CkbInd_MT.CheckState = CheckState.Indeterminate
            CkbInd_ChkAll.CheckState = CheckState.Indeterminate

            CheckedListBox_Urban.CheckAll()
            CkbUrb_ChkAll.CheckState = CheckState.Checked
            CkbYear_ChkAll.CheckState = CheckState.Checked
            CheckedListBox_Year.CheckAll()
        ElseIf XtraTabControl2.SelectedTabPageIndex = 1 And CmbSoSanh.SelectedItem = "Đồ thị theo một Địa bàn" Then
            CheckedListBox_Urban.UnCheckAll()
            CheckedListBox_Urban.SetItemChecked(CheckedListBox_Urban.SelectedIndex, True)
            CkbUrb_ChkAll.CheckState = CheckState.Indeterminate

            CkbInd_KT.CheckState = CheckState.Checked
            CkbInd_XH.CheckState = CheckState.Checked
            CkbInd_MT.CheckState = CheckState.Checked
            CkbInd_ChkAll.CheckState = CheckState.Checked
            CheckedListBox_Ind.CheckAll()
            CkbYear_ChkAll.CheckState = CheckState.Checked
            CheckedListBox_Year.CheckAll()
        ElseIf XtraTabControl2.SelectedTabPageIndex = 1 And CmbSoSanh.SelectedItem = "Đồ thị theo một Năm" Then
            'CkbYear_ChkAll.CheckState = CheckState.Checked
            'CkbYear_ChkAll.CheckState = CheckState.Unchecked
            '
            CheckedListBox_Year.UnCheckAll()

            Try
                CheckedListBox_Year.SetItemChecked(CheckedListBox_Year.SelectedIndex, True)
            Catch ex As Exception

            End Try

            CkbYear_ChkAll.CheckState = CheckState.Indeterminate
            CkbInd_KT.CheckState = CheckState.Checked
            CkbInd_XH.CheckState = CheckState.Checked
            CkbInd_MT.CheckState = CheckState.Checked
            CkbInd_ChkAll.CheckState = CheckState.Checked
            CheckedListBox_Ind.CheckAll()
            CkbUrb_ChkAll.CheckState = CheckState.Checked
            CheckedListBox_Urban.CheckAll()
        End If
        BtnOK_Click(Nothing, Nothing)
    End Sub

    Private Sub btnXChanger_Click(sender As Object, e As EventArgs) Handles btnXChanger.Click
        Dim tmp As Object = xtraChart.SeriesTemplate.ArgumentDataMember
        xtraChart.SeriesTemplate.ArgumentDataMember = xtraChart.SeriesDataMember
        xtraChart.SeriesDataMember = tmp
        xtraChart.Refresh()


    End Sub

    Private Sub btnChartExport_Click(sender As Object, e As EventArgs) Handles btnChartExport.Click
        SaveFileDialog1.DefaultExt = "jpg"
        SaveFileDialog1.Filter = "Tiff file (*.tif)|*.tif|Jpeg file (*.jpg)|*.jpg|Png File (*.png)|*.png|Gif file (*.gif)|*.gif|Window metafile format (*.wmf)|*.wmf|Portable document format (*.pdf)|*.pdf"
        SaveFileDialog1.FileName = ""
        SaveFileDialog1.ShowDialog()

        Dim outFname = SaveFileDialog1.FileName
        If outFname = "" Then
            Exit Sub
        End If
        Dim theImgFormat As System.Drawing.Imaging.ImageFormat
        Select Case outFname.Substring(outFname.Length - 3)
            Case "jpg"
                theImgFormat = System.Drawing.Imaging.ImageFormat.Jpeg
            Case "png"
                theImgFormat = System.Drawing.Imaging.ImageFormat.Png
            Case "tif"
                theImgFormat = System.Drawing.Imaging.ImageFormat.Tiff
            Case "gif"
                theImgFormat = System.Drawing.Imaging.ImageFormat.Gif
            Case "wmf"
                theImgFormat = System.Drawing.Imaging.ImageFormat.Wmf
            Case "pdf"
                theImgFormat = System.Drawing.Imaging.ImageFormat.Exif      'Lấy tạm là Exif cho trường hợp Pdf
        End Select


        If theImgFormat Is System.Drawing.Imaging.ImageFormat.Exif Then
            xtraChart.ExportToPdf(outFname)
        Else
            xtraChart.ExportToImage(outFname, theImgFormat)
        End If
    End Sub


    Private Sub TxtChartWidth_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtChartWidth.KeyPress
        AllowTextboxvalue.AllowIntegerNumber(sender, e)
    End Sub
    Private Sub TxtChartHeigh_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtChartHeigh.KeyPress
        AllowTextboxvalue.AllowIntegerNumber(sender, e)
    End Sub
    Private Sub TxtChartWidth_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtChartWidth.KeyUp
        xtraChart.Width = Convert.ToDouble(TxtChartWidth.Text)
        xtraChart.Height = Convert.ToDouble(TxtChartHeigh.Text)
    End Sub
    Private Sub TxtChartHeigh_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtChartHeigh.KeyUp
        xtraChart.Width = Convert.ToDouble(TxtChartWidth.Text)
        xtraChart.Height = Convert.ToDouble(TxtChartHeigh.Text)
    End Sub
    Private Sub TxtChartWidth_Spin(sender As Object, e As DevExpress.XtraEditors.Controls.SpinEventArgs) Handles TxtChartWidth.Spin
        txt_Chart_WH(sender, e, TxtChartHeigh)
    End Sub



    Private Sub TxtChartHeigh_Spin(sender As Object, e As DevExpress.XtraEditors.Controls.SpinEventArgs) Handles TxtChartHeigh.Spin
        txt_Chart_WH(sender, e, TxtChartWidth)
    End Sub
    Sub txt_Chart_WH(sender As Object, e As DevExpress.XtraEditors.Controls.SpinEventArgs, otherTextEdit As TextEdit)
        ''===otherTextEdit: Nếu gọi ở TxtChartWidth_Spin thì otherTextEdit = TxtChartHeigh; và ngược lại
        Dim oldValue = Convert.ToInt32(sender.text.ToString.Replace(",", "").Replace(".", ""))
        Dim newValue As Int32
        If e.IsSpinUp = True Then
            newValue = oldValue + 50
        Else
            newValue = oldValue - 50
        End If
        otherTextEdit.Text = Math.Round(Convert.ToDouble(newValue) / Convert.ToDouble(oldValue) * Convert.ToDouble(otherTextEdit.Text))
        sender.text = newValue
        Dim a As Integer = CheckedListBox_Ind.CheckedItems.Count + CheckedListBox_Year.CheckedItems.Count - 1
        Dim numRow As Integer = GridView2.RowCount

        If cmbChartViewType.SelectedIndex = 0 Or cmbChartViewType.SelectedIndex = 1 Or cmbChartViewType.SelectedIndex = 2 Then
            xtraChart.Width = numRow * 90 / a
        Else
            xtraChart.Width = numRow / a
        End If


        If xtraChart.Width < XtraScrollable_Chart.Width - 10 Then
            xtraChart.Width = XtraScrollable_Chart.Width - 2
            'myChart.Height = XtraScrollable_Chart.Height - 2
            ''''lblLabel3.Location = New System.Drawing.Point(lblLabel3.Location.X, myChart.Height)
        Else 'Trường hợp này có thêm VERTICAL scroll nên phải để nhỏ hơn
            'myChart.Height = XtraScrollable_Chart.Height - 20
            ''''lblLabel3.Location = New System.Drawing.Point(lblLabel3.Location.X, myChart.Height + 20)
        End If
        'myChart.Width = 2000
        'myChart.Height = 1000
        If TxtChartWidth.Text <> "" Then
            xtraChart.Width = Convert.ToDouble(TxtChartWidth.Text)
        End If
        If TxtChartHeigh.Text <> "" Then
            xtraChart.Height = Convert.ToDouble(TxtChartHeigh.Text)
        End If

        Dim MyTittle = xtraChart.Titles.Item(0)
        xtraChart.Titles.Clear()
        xtraChart.Titles.Add(MyTittle)
        xtraChart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center
        xtraChart.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside
        xtraChart.Legend.Direction = LegendDirection.LeftToRight
        sender.focus()
    End Sub
    Private Sub xtraChart_MouseMove(sender As Object, e As MouseEventArgs)
        'If TxtChartWidth.Focused = True Then
        '    Return
        'End If
        'Dim hi As DevExpress.XtraCharts.ChartHitInfo = myChart.CalcHitInfo(e.Location)
        'If Not hi Is Nothing Then
        '    '   sender.focus()
        '    ' TxtChartWidth.Focus()
        'End If
    End Sub


    Private Sub xtraChart_KeyDown(sender As Object, e As KeyEventArgs) Handles xtraChart.KeyDown
        If e.Control Then
            TxtChartWidth.Focus()
        Else
            sender.focus()
        End If
    End Sub

#End Region

    Private Sub xtraChart_KeyPress(sender As Object, e As KeyPressEventArgs) Handles xtraChart.KeyPress
        'e.keycode()
        'e.keychar
    End Sub



    Private Sub xtraChart_MouseDown(sender As Object, e As MouseEventArgs) Handles xtraChart.MouseDown
        xtraChart.Focus()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        GridControl2.ExportToXlsx("z:\a")
        '  GridControl2.ExportToXlsx()
    End Sub
End Class
