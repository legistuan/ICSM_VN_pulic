Imports System.IO
Public Class IO_Reader_Writer
    Public Sub New()
        If System.IO.File.Exists(Application.StartupPath + "\DevExpress.XtraDataTRI.v13.2.dll") = False Then
            Dim newf As System.IO.FileStream = File.Create(Application.StartupPath + "\DevExpress.XtraDataTRI.v13.2.dll")
            newf.Close()
        End If
    End Sub
    Public Function Database_file()
        Dim myApFileName As String = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
        Dim modifedDate As Date = IO.File.GetLastWriteTime(myApFileName).Date
        Dim AccessedDate As Date = IO.File.GetLastAccessTime(myApFileName).Date
        If DateTime.Compare(AccessedDate, modifedDate) >= 360 Then
            MessageBox.Show("Lỗi dữ liệu", "Xem lại file dữ liệu")
            Application.Exit()
        End If
        Try 'Dùng Try Catch để tránh trường hợp sử dụng file dữ liệu rỗng
            'Xét file dll, nếu chưa có thì tạo và ghi dữ liệu
            If System.IO.File.Exists(Application.StartupPath + "\DevExpress.XtraDataTRI.v13.2.dll") = False Then
                Dim newf As System.IO.FileStream = File.Create(Application.StartupPath + "\DevExpress.XtraDataTRI.v13.2.dll")
                newf.Close()
                ' aFrmMain.btn_open_Click(Nothing, Nothing)
            End If

        Catch
        End Try
        Database_file = doc1dong(Application.StartupPath + "\DevExpress.XtraDataTRI.v13.2.dll", 1)
        Return Database_file
    End Function
    Public Function doc1dong(ByVal FN As String, ByVal n As Integer) As String  'n la dong muon doc

        Dim returnVal As String = ""
        Dim sr As StreamReader
        sr = New StreamReader(FN)
        Dim i As Integer = 1
        While (sr.Peek() >= 0)
            If (i < n) Then
                sr.ReadLine()
            End If

            If (i = n) Then
                returnVal = sr.ReadLine()
            End If
            If (i > n) Then
                Exit While ' TODO: might not be correct. Was : Exit While
            End If

            i = i + 1
        End While
        'sr.Flush()
        sr.Close()
        Return returnVal
    End Function

    Public Function Ghi1dong(ByVal NoiDung As String) As Boolean  'n la dong muon doc
        Dim returnVal As Boolean = True
        'Dim sWriter As StreamWriter = New StreamWriter(Application.StartupPath + "/file.txt", True, System.Text.Encoding.UTF8)
        Dim sWriter As StreamWriter = New StreamWriter(Application.StartupPath + "/DevExpress.XtraDataTRI.v13.2.dll", False, System.Text.Encoding.UTF8)   'DevExpress.XtraDataTRI.v13.2.dll là file chứa đường dẫn tới dataFile tại dòng 1

        sWriter.WriteLine(NoiDung)
        sWriter.Flush()
        sWriter.Close()

        Return returnVal
    End Function

    Public Sub OpentoWrite(ByVal Title As String, ByVal Filter As String)
        Dim f As New OpenFileDialog
        'f.Filter = "VUI data |*.VUI"
        'f.Title = "Chọn File dữ liệu"

        Dim FullFileName As String = ""
        If f.ShowDialog() = DialogResult.OK Then
            FullFileName = f.FileName

            Dim sWriter As StreamWriter = New StreamWriter(Application.StartupPath + "/DevExpress.XtraDataTRI.v13.2.dll", False, System.Text.Encoding.UTF8)   'DevExpress.XtraDataTRI.v13.2.dll là file chứa đường dẫn tới dataFile tại dòng 1

            sWriter.WriteLine(FullFileName)
            sWriter.Flush()
            sWriter.Close()

        End If
    End Sub

    Public Sub Save2file()
        Dim f As New SaveFileDialog
        f.Title = "Lưu File dữ liệu"
        f.Filter = "VUI data |*.VUI"
        Dim Curdata As String = doc1dong(Application.StartupPath + "\DevExpress.XtraDataTRI.v13.2.dll", 1)         'sourceFile
        Dim TargetFile As String = ""

        If f.ShowDialog() = DialogResult.OK Then

            TargetFile = f.FileName
        End If
        If System.IO.File.Exists(Curdata) = True And TargetFile <> "" Then
            System.IO.File.Copy(Curdata, TargetFile)
            MsgBox("Dữ liệu đã được lưu lại")
        End If
    End Sub


End Class
