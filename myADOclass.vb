Imports System.Data.OleDb
'Imports System.IO
Imports DevExpress.XtraGrid
'Imports DevExpress.XtraGrid.Views.Base

Public Class myADOclass
    '===================================================================================
    '                          Khai báo các thuoojc tính dùng chung
    '===================================================================================
    'public string m_str = "server = VTF\\SQLEXPRESS ;database = QLBTTNVN ;Integrated Security = true";    // Chu?i k?t n?i v?i CSDL
    'Public m_str As String = "server =(local) ;database = QLBTTNVN ;Integrated Security = true" ' Chu?i k?t n?i v?i CSDL

    'Dim IO_Reader_Writer As New IO_Reader_Writer
    '  Dim datafile As String = IO_Reader_Writer.doc1dong(Application.StartupPath + "\DevExpress.XtraDataTRI.v13.2.dll", 1) 'File.dll là file text lưu đường dẫn file dữ liệu tại dòng đầu
    Dim datafile As String '= Application.StartupPath + "\DevExpress.Data.v13.2.dlI"
    'Const mypass = "abc123xyz"
    Public myConnectString As String '= "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + datafile + "; Jet OLEDB:Database Password= " '+ mypass
    Public myConnection As OleDbConnection '= New OleDbConnection(myConnectString) ' Bi?n th?c thi k?t n?i v?i CSDL
    'Private myCommand As New OleDbCommand()
    'Private myDataReader As OleDbDataReader = Nothing
    'Private myDataSet As DataSet = Nothing
    '
    Private myDataAdapter As OleDbDataAdapter       '*****************Devexpress expert added
    '===================================
    Sub New(datafile)
        myConnectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + datafile + "; Jet OLEDB:Database Password= " '+ mypass
        myConnection = New OleDbConnection(myConnectString) ' Bi?n th?c thi k?t n?i v?i CSDL
        KetNoi_Open()
    End Sub

    ' Hàm lấy dữ liệu dạng bảng từ câu truy vấn
    Public Function DtFromQry(ByVal query As String) As DataTable 'As ArrayList(DataTable, OleDbDataAdapter)
        ''===Devexpress expert added
        'If myDataAdapter Is Nothing Then
        '	myDataAdapter = New OleDbDataAdapter(query, myConStr)
        'End If
        'myDataAdapter.SelectCommand = New OleDbCommand(query, New OleDbConnection(myConStr))
        ''===End of '===Devexpress expert added
        myDataAdapter = New OleDbDataAdapter(query, myConnectString)    'Bỏ dòng này nếu dùng lại Devexpress expert added
        Dim myDataTale As DataTable = New DataTable()
        Try
            myDataAdapter.Fill(myDataTale)
            ' MessageBox.Show(query)
            'MessageBox.Show(myDataTale.Rows.Count.ToString)

        Catch ex As Exception
            MessageBox.Show("Lỗi kết nối tới CSDL!")

        Finally

        End Try
        KetNoi_Close()
        Return myDataTale ', myDataAdapter}

    End Function

    'Public Function DtFromQry(ByVal query As String, ByVal myDataAdapter As OleDbDataAdapter) 'As ArrayList(DataTable, OleDbDataAdapter)
    '    'Dim myDataAdapter As OleDbDataAdapter = New OleDbDataAdapter(query, myConStr)
    '    Dim myDataTale As DataTable = New DataTable()
    '    Try
    '        myDataAdapter.Fill(myDataTale)
    '        ' MessageBox.Show(query)
    '        'MessageBox.Show(myDataTale.Rows.Count.ToString)

    '    Catch ex As Exception
    '        MessageBox.Show("Lỗi kết nối tới CSDL!")

    '    Finally
    '       ''' KetNoi_Close()
    '    End Try
    '    Return myDataTale ', myDataAdapter}
    'End Function
    '===================================================================================
    'Tạo DataSet từ CSDL
    'Public Function DSFromQry(ByVal TENBANG As String) As DataSet
    '	Dim QryStr As String = "Select * from " & TENBANG
    '	myDataAdapter = New OleDbDataAdapter(QryStr, myConnection)
    '	myDataAdapter.SelectCommand = New OleDbCommand(QryStr, New OleDbConnection(myConStr))	 'Devexpress expert added
    '	Dim ds As DataSet = myDataSet	'Chú ý nếu 1 ds được Fill ít nhất 1 lần thì phài Clear trước khi Fill lần tiếp theo, nếu ko sẽ dẫn đến lỗi logic
    '	myDataAdapter.Fill(ds, TENBANG)
    '	Return ds
    'End Function
    ' Hàm lấy dữ liệu dạng Dataview từ câu truy vấn
    '  Public Function DtvFromQry(ByVal query As String) As DataView
    ''Dim myDataAdapter = New OleDbDataAdapter(query, myConStr)
    ''myDataAdapter.SelectCommand = New OleDbCommand(query, New OleDbConnection(myConStr))    'Devexpress expert added
    ''Dim myDataTale As DataTable = New DataTable()
    ''Dim myDataview As DataView
    ''Try
    ''    myDataAdapter.Fill(myDataTale)
    ''    myDataview = New DataView(myDataTale)
    ''    ' MessageBox.Show(query)
    ''    'MessageBox.Show(myDataTale.Rows.Count.ToString)
    ''    Return myDataview
    ''Catch ex As Exception
    ''    MessageBox.Show("Lỗi kết nối tới CSDL!")

    ''Finally
    ''   ''' KetNoi_Close()
    ''End Try
    ''Return Nothing
    '  End Function
    '===================================================================================
    ' Hàm thực thi việc trút dữ liệu truy vấn ra DataGridview
    'Public Sub Load_data_Grid(ByVal dgv As DataGridView, ByVal query As String)
    '    Dim myDataTale As DataTable = DtFromQry(query)
    '    dgv.DataSource = myDataTale
    'End Sub
    ''===================================================================================
    'Public Sub Load_data_Grid(ByVal dgv As DataGridView, ByVal MyDataTale As DataTable)
    '    dgv.DataSource = MyDataTale
    'End Sub
    ''===================================================================================
    'Public Sub Load_data_Grid(ByVal dgv As DataGridView, ByVal MyDataView As DataView)
    '    dgv.DataSource = MyDataView
    'End Sub

    'Private Sub upd(ByVal commandText As String)
    '    Dim upd As New OleDb.OleDbCommand
    '    upd.CommandText = commandText   ' "update Customers "
    '    Dim Cust_No = " & TextBox1.Text & ", Cust_Address = " & TextBox3.Text & " ',Cust_Phone='" & TextBox4.Text & "'where Cust_Name='" & TextBox2.Text & "'"             
    '    upd.Connection = myConn
    '    myConn.Open()
    '    upd.ExecuteNonQuery()
    'End Sub
    Public Function Update_CSDL(ByVal xtraGridControl As GridControl, ByVal query As String) As Int32 'Trả về số lần update các rows
        'query là QryString để nối dữ liệu CSDL, xem ví dụ Dim query ở dưới	('===========CHÚ Ý: thực chất lệnh Where ở trên ko có ý nghĩa. Sẽ update toàn bộ changes của dt lên CSDL chứ ko chỉ update những row thỏa mãn ĐK where trong CSDL)
        'Dim query = "select ID, dataVal, dataRef from dataQry"   Select column nào thì update column ấy      
        'Chi co the Update du lieu cho cac truong trong cung 1 bang. 
        ' Nếu query = "select * from dataQry" thi se gặp lỗi ngay do * là chứa các trường từ các bảng khác nhau đã được đưa vào 1 Query "dataQry"
        'Hơn nữa trong lệnh Update phải có trường Data_Nid là trường key để có thể update chính xac, nếu không có trường key thì cũng gặp lỗi

        'myconect.Update_CSDL(Gridcontrol1, "select * from Maindata")

        '++++++++++++++++++
        '===========CHÚ Ý: thực chất lệnh Where ở trên ko có ý nghĩa. Sẽ update toàn bộ changes của dt lên CSDL chứ ko chỉ update những row thỏa mãn ĐK where trong CSDL
        '++++++++++++++++++ Query trên chỉ có ý nghĩa để xác định Table nào và Fields nào của Table trong CSDL được sử dụng để UPDATE.
        Update_CSDL = 0
        ''KetNoi_Open()

        '===========Lỗi xảy ra như sau: Nếu gọi Update_CSDL của Gridview1 và ngay sau đó, 1 gridview2 khác load CSDL này thì sẽ vẫn nhận CSDL chưa update (Mặc dù mở Access thấy đã update CSDL rồi). Lý do có thể
        'giải thích là Gridview2 thực hiện lệnh load dữ liệu khi Update_CSDL chưa hoàn thành.
        'Dim da As OleDbDataAdapter = myDataAdapter	'Devexpress expert added        'Nếu ko dùng dòng này thì ko fly update đc
        '====Đã phát hiện ra lỗi ko flyupdate được. Ko phải giải thích như Devexpress mà Thực chất do trước đây mình dùng dùng myConnection nên bị "trễ". Hiện dùng myConStr như lệnh dưới thì OK.Bởi nếu dùng Myconnection thì Update vào CSDL chậm hơn.
        Dim da As OleDbDataAdapter = New OleDbDataAdapter(query, myConnectString)       'Gốc của mình là Dim da As New OleDbDataAdapter(query, myConStr) nên bị lỗi ko flyupdate, xóa dòng này nếu dùng Devexpress expert added  
        Dim cb As New OleDbCommandBuilder(da)

        xtraGridControl.DefaultView.PostEditor()
        xtraGridControl.DefaultView.UpdateCurrentRow()
        'xtragrid.DefaultView.RefreshData()
        'grv.RefreshData()

        Dim dt As DataTable = xtraGridControl.DataSource
        Dim dtChanges = dt.GetChanges
        Try
            If Not dtChanges Is Nothing Then
                Update_CSDL = da.Update(dtChanges)
                Try
                    dt.AcceptChanges()  'Sau khi Update source database thì Accept changes luôn
                Catch ex As Exception

                End Try

                xtraGridControl.Refresh()
                xtraGridControl.RefreshDataSource()
            End If

        Catch ex As Exception
            'MsgBox(ex.ToString)
            'MsgBox("Gặp lỗi trong quá trình cập nhật dữ liệu" + vbNewLine + query + vbNewLine + xtraGridControl.Name)
            dt.RejectChanges()
            'Exit Function
        End Try
        KetNoi_Close()
        Return Update_CSDL


    End Function
    'Update toàn bộ  từ dataTable vào CSDL
    Public Function Update_CSDL(ByVal dt As DataTable, ByVal query As String) As Int32  'Trả về số lần update các rows
        Update_CSDL = 0
        'KetNoi_Open()

        Dim da As OleDbDataAdapter = New OleDbDataAdapter(query, myConnectString)
        Dim cb As OleDbCommandBuilder = New OleDbCommandBuilder(da)

        Try

            Dim changes As DataTable = dt.GetChanges()
            If Not changes Is Nothing Then
                Update_CSDL = da.Update(changes)
                dt.AcceptChanges()
            End If
            'Update_CSDL = da.Update(dt)

        Catch ex As Exception
            MsgBox(ex.ToString)
            'MsgBox("Gặp lỗi trong quá trình cập nhật dữ liệu")
            dt.RejectChanges()

        End Try
        KetNoi_Close()
        Return Update_CSDL

    End Function
    'Public Function Update_CSDL(ByVal dgv As DataGridView, ByVal Str As String) As Boolean 'Str là QryString để nối dữ liệu CSDL, xem ví dụ Dim Str ở dưới
    '    'Update dữ liệu từ Dt vào dữ liệu trong CSDL theo query Str. Xem Ví dụ Str ở dưới
    '    'myconect.Update_CSDL(dt, "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
    '    'Nhớ là str phải chính = str đã sử dụng để định nghĩa lên dt. 
    '    Update_CSDL = False
    '    KetNoi_Open()
    '    Dim theresult As Int32
    '    Dim da As New OleDbDataAdapter(Str, myConn)
    '    Dim cb As OleDbCommandBuilder = New OleDbCommandBuilder(da)
    '    Dim dt As DataTable
    '    da.Fill(dt)
    '    Try
    '        theresult = da.Update(dt)       '(dt.GetChanges)
    '        'dt = dt.GetChanges
    '        Update_CSDL = True
    '        'dgv.DataSource = dt
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '        MsgBox("Gặp lỗi trong quá trình update dữ liệu")
    '        dt.RejectChanges()
    '        KetNoi_Close()

    '        Exit Function
    '    End Try
    '    Return Update_CSDL
    '    'Dim str = "select ID, dataVal, dataRef from dataQry"   Select column nào thì update column ấy      
    '    'Chi co the Update du lieu cho cac truong trong cung 1 bang. 
    '    ' Nếu Str = "select * from dataQry" thi se gặp lỗi ngay 
    '    ' do * là chứa các trường từ các bảng khác nhau đã được đưa vào 1 Query
    '    'Hơn nữa trong lệnh Update phải có trường Data_Nid là trường key để có thể update chính xac, 
    '    'nếu không có trường key thì cũng gặp lội
    'End Function
    'Public Function Update_CSDL(ByVal grc As DevExpress.XtraGrid.GridControl, ByVal Str As String) As Boolean 'Str là QryString để nối dữ liệu CSDL, xem ví dụ Dim Str ở dưới
    '    'Update dữ liệu từ Dt vào dữ liệu trong CSDL theo query Str. Xem Ví dụ Str ở dưới
    '    'myconect.Update_CSDL(dt, "select * from Maindata where Obj = " + """" + ListBoxControl1.SelectedItem + """")
    '    'Nhớ là str phải chính = str đã sử dụng để định nghĩa lên dt. 
    '    Update_CSDL = False
    '    KetNoi_Open()
    '    Dim theresult As Int32
    '    Dim da As New OleDbDataAdapter(Str, myConn)
    '    Dim cb As OleDbCommandBuilder = New OleDbCommandBuilder(da)
    '    Dim dt As DataTable = New DataTable
    '    da.Fill(dt)
    '    'Dim dt As DataTable = drc.datasource


    '    Try
    '        'Dim changes As DataTable = dt.GetChanges()
    '        'If Not changes Is Nothing Then
    '        '    theresult = da.Update(changes)
    '        '    dt.AcceptChanges()
    '        '    Update_CSDL = True
    '        'End If
    '        theresult = da.Update(dt)   '(dt.GetChanges)

    '        dt = dt.GetChanges
    '        Update_CSDL = True
    '        'grc.DataSource = dt
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '        MsgBox("Gặp lỗi trong quá trình update dữ liệu")
    '        dt.RejectChanges()
    '        KetNoi_Close()

    '        Exit Function
    '    End Try
    '    Return Update_CSDL
    '    'Dim str = "select ID, dataVal, dataRef from dataQry"   Select column nào thì update column ấy      
    '    'Chi co the Update du lieu cho cac truong trong cung 1 bang. 
    '    ' Nếu Str = "select * from dataQry" thi se gặp lỗi ngay 
    '    ' do * là chứa các trường từ các bảng khác nhau đã được đưa vào 1 Query
    '    'Hơn nữa trong lệnh Update phải có trường Data_Nid là trường key để có thể update chính xac, 
    '    'nếu không có trường key thì cũng gặp lội
    'End Function
    'DEL  Row từ CSDL
    '  Public Sub DELRow(ByVal TblName As String, ByVal aNewRow As DataRow)  'TblName là tên bảng trong CSDL Access
    ''KetNoi_Open()
    ''Dim cmd As New OleDbCommand(TblName, myConn)
    ''cmd.CommandText = "DELETE ..........Fom " + TblName + " Where..."
    ''cmd.ExecuteNonQuery()
    ''KetNoi_Close()
    '  End Sub

    Public Sub AddRow_with_ID_col(xtraGridControl As GridControl, ID_FieldName As String, MSAccessTableName As String)
        ''===========Chú ý quan trọng: Không sử dụng được GridView1.SetRowCellValue(0, "Obj", ListBoxControl1.SelectedValue.ToString) mà dùng GridView1.GetDataRow(GridView1.GetSelectedRows(0))("Obj") = ... như dưới đây
        ' ''Dùng dòng này:  GridView1.GetDataRow(GridView1.GetSelectedRows(0))("Obj") = ListBoxControl1.SelectedValue.ToString
        '' ''Không dùng dòng này:  GridView1.SetRowCellValue(0, "Obj", ListBoxControl1.SelectedValue.ToString)	'Không dùng được SetRowCellValue vì sau khi AddNewRow, thử đếm Gridview1.Rowcount vẫn =0, ko hiểu tại sao lại = 0 nhỉ
        ''==ID không phải là AutoIncrement. Nếu ko sẽ lỗi: '==========Nếu ID là AutoInc sẽ lỗi khi bảng Access gốc bị xóa các record dưới cùng đi. Khi này Bảng Access sẽ tự động thêm số lớn hơn ID của row đã bị xóa khi add thêm row. Trong khi Sub này chỉ lấy được số MaxID +1
        ''===ID là Integer
        ''====Tìm MaxID
        Dim SrcdataTable As DataTable = DtFromQry("Select " + ID_FieldName + " from [" + MSAccessTableName + "]")     'xtraGridControl.DataSource
        SrcdataTable.DefaultView.Sort = "ID ASC"
        Dim MaxID As Integer = 0
        Try
            MaxID = SrcdataTable.DefaultView.Item(SrcdataTable.Rows.Count - 1).Item(ID_FieldName)
        Catch ex As Exception

        End Try


        ''====Đặt AutoIncrement cho autoincrementFieldName trong Gridview
        xtraGridControl.DataSource.Columns(ID_FieldName).AutoIncrement = True
        xtraGridControl.DataSource.Columns(ID_FieldName).AutoIncrementSeed = MaxID + 1
        Dim Xtragridview As Views.Grid.GridView = TryCast(xtraGridControl.MainView, Views.Grid.GridView)

        '' ''===trường hợp Gridview không phải MainView thì phải dùng ViewCollection
        ' ''Dim Xtragridview As Views.Grid.GridView
        ' ''For Each View In xtraGridControl.ViewCollection
        ' ''	If TypeOf (View) Is Views.Grid.GridView Then
        ' ''		Xtragridview = View
        ' ''		Exit For
        ' ''	End If
        ' ''Next

        Xtragridview.AddNewRow()
        ''xtraGridControl.DataSource.acceptchanges()


    End Sub
    Public Function RunaSQLCommand(ByVal MySQLStr As String) As Boolean
        'Ví dụ cho MySQLStr
        'Dim SelectCommand As String = "SELECT * FROM Products"
        'Dim UpdateCommand As String = "UPDATE Products SET " & "ProductName = 'Test' WHERE ProductId = 1"
        'Dim InsertCommand As String = "INSERT INTO Products " & "(ProductName,CategoryId) VALUES ('Test', 1) "
        'Dim DeleteCommand As String = "DELETE FROM Products " & " WHERE ProductId = 1"
        ''======4 Lệnh sau ví dụ sử dụng cho OleDbCommand
        'myDataAdapter.SelectCommand = New OleDbCommand(SelectCommand, New OleDbConnection(myConStr))
        'myDataAdapter.UpdateCommand = New OleDbCommand(UpdateCommand, New OleDbConnection(myConStr))
        'myDataAdapter.InsertCommand = New OleDbCommand(InsertCommand, New OleDbConnection(myConStr))
        'myDataAdapter.DeleteCommand = New OleDbCommand(DeleteCommand, New OleDbConnection(myConStr))
        Dim MyResult As Boolean = False
        Dim MyCommand As New OleDbCommand
        KetNoi_Open()
        Try

            MyCommand.Connection = myConnection
            MyCommand.CommandType = CommandType.Text
            MyCommand.CommandText = MySQLStr
           
            MyCommand.ExecuteNonQuery()

            MyResult = True

        Catch ex As Exception
            'MessageBox.Show(ex.Message, "DoCmdRunSQL")
            'MessageBox.Show(ex.Message & ",Common/DoCmdRunSQL")
        Finally

            MyCommand.Dispose()
        End Try
        KetNoi_Close()
        Return MyResult

    End Function
    Public Function RunaSQLCommand(ByVal dt As DataTable, ByVal MySQLStr As String) As Boolean
        'Ví dụ cho MySQLStr
        'Dim SelectCommand As String = "SELECT * FROM Products"
        'Dim UpdateCommand As String = "UPDATE Products SET " & "ProductName = 'Test' WHERE ProductId = 1"
        'Dim InsertCommand As String = "INSERT INTO Products " & "(ProductName,CategoryId) VALUES ('Test', 1) "
        'Dim DeleteCommand As String = "DELETE FROM Products " & " WHERE ProductId = 1"
        ''======4 Lệnh sau ví dụ sử dụng cho OleDbCommand
        'myDataAdapter.SelectCommand = New OleDbCommand(SelectCommand, New OleDbConnection(myConStr))
        'myDataAdapter.UpdateCommand = New OleDbCommand(UpdateCommand, New OleDbConnection(myConStr))
        'myDataAdapter.InsertCommand = New OleDbCommand(InsertCommand, New OleDbConnection(myConStr))
        'myDataAdapter.DeleteCommand = New OleDbCommand(DeleteCommand, New OleDbConnection(myConStr))

        Dim MyResult As Boolean = False
        'If myDataAdapter Is Nothing Then
        'myDataAdapter = New OleDbDataAdapter(query, myConStr)
        'End If
        'myDataAdapter.SelectCommand = New OleDbCommand(query, New OleDbConnection(myConStr))	'Devexpress expert added
        Dim MyCommand As New OleDbCommand
        KetNoi_Open()
        Try

            MyCommand.Connection = myConnection
            MyCommand.CommandType = CommandType.Text
            MyCommand.CommandText = MySQLStr
            'Dim a = MyCommand.ExecuteNonQuery()
            'MyResult = False
            MyCommand.ExecuteNonQuery()
            dt.AcceptChanges()
            'xtragrid.Refresh()
            'xtragrid.RefreshDataSource()
            MyResult = True

        Catch ex As Exception
            'MessageBox.Show(ex.Message, "DoCmdRunSQL")
            'MessageBox.Show(ex.Message & ",Common/DoCmdRunSQL")
        Finally

            MyCommand.Dispose()
        End Try
        KetNoi_Close()
        Return MyResult
        ' Vi du MySQLStr Update: String.Format("UPDATE DataQry SET DataVal = {0}, DataRef = {1} WHERE DataID = {2}", """" + TbxVal.Text + """", """" + TbxRef.Text + """", FstDataID)
        'Dim result As Boolean = mycon.RunaSQLCommand(String.Format("UPDATE DataQry SET DataVal = {0} WHERE DataID = {1}", """" + TbxVal.Text + """", FstDataID))

        'Dim DeleteCommand As String = "DELETE FROM Maindata WHERE Obj = '" + srcObj + "'"
        'Dim INSERTCommand As String = "INSERT INTO MoTa (TenPT, DongThucVat, Ho, Loai, MoTa, Anh, Chitiet) Values ( '" + _
        '                                    Tbx_tenPT.Text + "', '" + _
        '                                    dtv + "', '" + _
        '                                    Tbx_Ho.Text + "', '" + _
        '                                    Tbx_Loai.Text + "', '" + _
        '                                    Tbx_Mota.Text + "', '" + _
        '                                    Tbx_Anh.Text + "', '" + _
        '                                    Tbx_chitiet.Text + "' )"
        ' Dim UPDATECommand As String = "UPDATE Maindata SET Obj = '" + targetObj + "' WHERE Obj = '" + srcObj + "'"
        '' MessageBox.Show("INSERT")
        'theresult = MyCon.RunaSQLCommand(INSERTCommand)
    End Function
    Sub createTable(tableName As String, listfield As String)
        'listfield có cấu trúc như sau:     [Field1] TEXT(10), [Field2] TEXT(10)
        'Get database schema
        '	Dim myconnectClass As New myADOclass
        'Dim con As New OleDb.OleDbConnection(myconnectClass.myConStr)
        'con.Open()
        KetNoi_Open()
        Dim dbSchema As DataTable = myConnection.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, tableName, "TABLE"})
        'con.Close()
        ' If the table exists, the count = 1
        If dbSchema.Rows.Count = 0 Then     'do whatever you want to do if the table does not exist
            ' e.g. create a table
            If tableName = "" Then
                Return
            End If
            Dim cmd As New OleDb.OleDbCommand("CREATE TABLE [" + tableName + "] (" + listfield + ")", myConnection)
            'con.Open()
            cmd.ExecuteNonQuery()
            'con.Close()
            'MessageBox.Show("Table Created Successfully")
        Else

        End If
        KetNoi_Close()
    End Sub
    'Public Sub RunMSAccessQry(ByVal theQryTable As String)
    '    Dim accApp As Object
    '    accApp = GetObject(datafile)

    '    'this will run the query in the database  
    '    accApp.docmd.OpenQuery(theQryTable)
    '    accApp.Run(theQryTable)
    'End Sub
    Public Sub KetNoi_Open()    'Mo ket noi
        Try
            If myConnection.State <> ConnectionState.Open Then     '~ conn.State = ConnectionState.Closed
                'myConnection.ConnectionString = myConnectString	'Đã khai trong phần định nghĩa
                myConnection.Open()
            End If
        Catch '(Exception ex)
            MessageBox.Show("Có vấn đề trong kết nối tới CSDL", "Lỗi kết nối!!")
            Return
        End Try
    End Sub
    Public Sub KetNoi_Close()   'Dong Ket noi
        Try
            If myConnection.State = ConnectionState.Open Then     '~ conn.State = ConnectionState.Closed
                myConnection.Close()
            End If
        Catch '(Exception ex)
            MessageBox.Show("Lỗi kết nối!!")
            Return
        End Try
    End Sub
End Class
