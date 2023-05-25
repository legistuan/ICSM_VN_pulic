Imports System.IO
Imports System.Globalization
Imports System.Threading
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.Net.Cache

Public Class Frm_Startup
    Public myLanguage As String
    Public ProjOption As String
    Public CurData As String
    Public Shared inputcaymoi As String = ""
    Dim myconnect As myADOclass
    Dim loifileProject As String = "Có lỗi với file Project gần đây, xin chọn một Project"
    Dim chonproj As String = "Chọn Project"
    Dim chonfilemoi As String = "Tạo Project mới - Đặt tên cho Project"
    Dim projectDGTN As String = "Project Đánh giá thích nghi cây trồng|*.nft"
    Private Sub FrmStart0_Load(sender As Object, e As EventArgs) Handles Me.Load
        RadLang.SelectedIndex = 0
        RadProj.SelectedIndex = 0

        check_date()
        'Các thông số ConnectionString sẽ được gán khi Myconnect.open trong class mycon.vb

    End Sub


    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click

        If RadLang.SelectedIndex = 0 Then
            myLanguage = "Tiếng Việt"
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("vi-VN")

        Else
            myLanguage = "English"
            EnglishTranslate()
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("en")
            'Dim FrmEnglish = New FrmStart
            'FrmEnglish.Show()
        End If


        Dim IO_Reader_Writer As New IO_Reader_Writer

        If RadProj.SelectedIndex = 0 Then       '=====================================================Mở Project gần đây
            CurData = IO_Reader_Writer.doc1dong(Application.StartupPath + "\myFile.dll", 1)
            Try 'Dùng Try Catch để tránh trường hợp sử dụng file dữ liệu rỗng

                If CurData = "" Or System.IO.File.Exists(CurData) = False Then  'Nếu nội dung chưa có đường dẫn hoặc file không tồn tại thì quay lại mở file
                    CurData = ChonvaLuuCSDL(loifileProject, projectDGTN)

                    If CurData = "" Then
                        Return
                    Else
                        IO_Reader_Writer.Ghi1dong(CurData)
                    End If
                End If
                'Tồn tại CurData
                inputboxchoNewPrj()
                'FrmStart.Show()
                'Me.Hide()

            Catch
            End Try

            '====================================================
            '==================================================== 'Mở Project sẵn có
        ElseIf RadProj.SelectedIndex = 1 Then
            CurData = ChonvaLuuCSDL(chonproj, projectDGTN)
            If CurData = "" Then
                Return
            Else
                'FrmStart.Show()
                'Me.Hide()
                IO_Reader_Writer.Ghi1dong(CurData)
                inputboxchoNewPrj()
            End If

            '====================================================
            '==================================================== 'Tạo Project mới
        Else
            Dim f As New SaveFileDialog
            f.Title = chonfilemoi
            f.Filter = "nft data |*.nft"
            Dim TmpData = Application.StartupPath + "\Nafosted.Template"         'sourceFile
            Dim TargetFile As String = ""

            If f.ShowDialog() = DialogResult.OK Then
                CurData = f.FileName
                System.IO.File.Copy(TmpData, CurData, True)
                IO_Reader_Writer.Ghi1dong(CurData)

                inputboxchoNewPrj()

            Else
                Return
            End If
        End If

    End Sub
    Sub inputboxchoNewPrj()
        myconnect = New myADOclass
        Dim Qry As String = "select Obj from Maindata"
        Dim dt As DataTable = myconnect.DtFromQry(Qry)  'Dùng để xác định xem cái file mở ra có Obj nào chưa, trường hợp chưa có dữ liệu thì phải dùng inputbox để add OBJ
        If dt.Rows.Count = 0 Then
            Me.Hide()
            If myLanguage = "Tiếng Việt" Then
                inputcaymoi = InputBox("", "Nhập tên cây đánh giá cần tạo")
            Else
                inputcaymoi = InputBox("", "Input a name of object")
            End If


            If inputcaymoi <> "" Then
                inputcaymoi = Replace(inputcaymoi, " ", "_")
                FrmMain.Show()
            Else
                Me.Show()
            End If
        Else
            FrmMain.Show()
            Me.Hide()
        End If

    End Sub
    Function ChonvaLuuCSDL(theTitle As String, projectDGTN1 As String) As String
        Dim f As New OpenFileDialog
        f.Filter = projectDGTN1
        f.Title = theTitle
        f.InitialDirectory = Application.StartupPath
        Dim FullFileName As String = ""
        If f.ShowDialog() = DialogResult.OK Then
            FullFileName = f.FileName
            Dim sWriter As StreamWriter = New StreamWriter(Application.StartupPath + "/myFile.dll", False, System.Text.Encoding.UTF8)   'myFile.dll là file chứa đường dẫn tới dataFile tại dòng 1

            sWriter.WriteLine(FullFileName)
            sWriter.Flush()
            sWriter.Close()
        Else

        End If
        Return FullFileName
    End Function
    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Application.Exit()
    End Sub

    Private Sub RadLang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadLang.SelectedIndexChanged
        If RadLang.SelectedIndex = 0 Then
            GroupControlLang.Text = "Lựa chọn ngôn ngữ"
            GroupControlProj.Text = "Tạo mới/Mở Project"
            BtnExit.Text = "Thoát"
            BtnOK.Text = "Tiếp tục"
            RadProj.Properties.Items(0).Description = "Mở Project gần đây"
            RadProj.Properties.Items(1).Description = "Mở một Project"
            RadProj.Properties.Items(2).Description = "Tạo mới Project"
        Else
            GroupControlLang.Text = "Language selection"
            GroupControlProj.Text = "New/Open project"
            BtnExit.Text = "Quit"
            BtnOK.Text = "Continue"
            RadProj.Properties.Items(0).Description = "Recent Project"
            RadProj.Properties.Items(1).Description = "Open Project"
            RadProj.Properties.Items(2).Description = "New Project"
        End If
    End Sub

    Sub EnglishTranslate()
        loifileProject = "The last project is error, Please choose another Project"
        chonproj = "Choose a Project"
        chonfilemoi = "Create a new Project - Input a new name"
        projectDGTN = "Project|*.nft"
    End Sub

    Public Shared Function GetFastestNISTDate() As DateTime
        Dim result = DateTime.MinValue
        ' Initialize the list of NIST time servers
        ' http://tf.nist.gov/tf-cgi/servers.cgi
        Dim servers As String() = New String() {"nist1-ny.ustiming.org", "nist1-nj.ustiming.org", "nist1-pa.ustiming.org", "time-a.nist.gov", "time-b.nist.gov", "nist1.aol-va.symmetricom.com", _
            "nist1.columbiacountyga.gov", "nist1-chi.ustiming.org", "nist.expertsmi.com", "nist.netservicesgroup.com"}

        ' Try 5 servers in random order to spread the load
        Dim rnd As New Random()
        For Each server As String In servers.OrderBy(Function(s) rnd.NextDouble()).Take(5)
            Try
                ' Connect to the server (at port 13) and get the response
                Dim serverResponse As String = String.Empty
                Using reader = New StreamReader(New System.Net.Sockets.TcpClient(server, 13).GetStream())
                    serverResponse = reader.ReadToEnd()
                End Using

                ' If a response was received
                If Not String.IsNullOrEmpty(serverResponse) Then
                    ' Split the response string ("55596 11-02-14 13:54:11 00 0 0 478.1 UTC(NIST) *")
                    Dim tokens As String() = serverResponse.Split(" "c)

                    ' Check the number of tokens
                    If tokens.Length >= 6 Then
                        ' Check the health status
                        Dim health As String = tokens(5)
                        If health = "0" Then
                            ' Get date and time parts from the server response
                            Dim dateParts As String() = tokens(1).Split("-"c)
                            Dim timeParts As String() = tokens(2).Split(":"c)

                            ' Create a DateTime instance
                            Dim utcDateTime As New DateTime(Convert.ToInt32(dateParts(0)) + 2000, Convert.ToInt32(dateParts(1)), Convert.ToInt32(dateParts(2)), Convert.ToInt32(timeParts(0)), Convert.ToInt32(timeParts(1)), Convert.ToInt32(timeParts(2)))

                            ' Convert received (UTC) DateTime value to the local timezone
                            result = utcDateTime.ToLocalTime()

                            ' Response successfully received; exit the loop

                            Return result
                        End If

                    End If

                End If
                ' Ignore exception and try the next server
            Catch
            End Try
        Next
        Return result
    End Function
    Sub check_date1()
        'Dim a As Date = #11/8/2018#     '12:00:00 AM
        'Dim todaydate As Date = GetNistTime1()
        ''        If DateTime.Today > a Then
        'If todaydate = #11/4/2018 10:37:26 AM# Then
        '    MessageBox.Show("Lỗi kết nối internet")
        '    Return
        'End If
        'If todaydate > a Then
        '    MessageBox.Show("Hết thời gian dùng thử, xin liên hệ tác giả!")
        '    Return
        'End If


    End Sub
    Sub check_date()
        Dim TheExpiredated As Date = #9/9/2021#     '12:00:00 AM THÁNG/NGÀY/NĂM====NGÀY HẾT HẠN
        Dim todaydate As Date = GetDateTime()       'get datetime : EXIT APP if can not access internet
        If todaydate > TheExpiredated Then
            MessageBox.Show("Trial expired!" + vbNewLine + "Please contact to ngodangtri@gmail.com if you need support," + vbNewLine + "Thank you.", "Information")
            Application.Exit()
            Application.ExitThread()
            Environment.Exit(Environment.ExitCode)
        End If
    End Sub

    Public Shared Function GetDateTime() As DateTime
        Dim dateTime As DateTime
        Dim request As System.Net.HttpWebRequest = DirectCast(System.Net.WebRequest.Create("http://www.microsoft.com"), System.Net.HttpWebRequest)
        request.Method = "GET"
        request.Accept = "text/html, application/xhtml+xml, */*"
        request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)"
        request.ContentType = "application/x-www-form-urlencoded"
        request.CachePolicy = New System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore)
        Try
            Dim response As System.Net.HttpWebResponse = DirectCast(request.GetResponse(), System.Net.HttpWebResponse)
            If response.StatusCode = System.Net.HttpStatusCode.OK Then
                Dim todaysDates As String = response.Headers("date")
                dateTime = Date.ParseExact(todaysDates, "ddd, dd MMM yyyy HH:mm:ss 'GMT'", System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat, System.Globalization.DateTimeStyles.AssumeUniversal)
            End If
        Catch ex As Exception
            MessageBox.Show("Internet error!", "License check")
            Application.Exit()
            Application.ExitThread()
            Environment.Exit(Environment.ExitCode)
        End Try
        Return dateTime
    End Function
    Public Shared Function GetNistTime1() As DateTime
        'Dim dateTime__1 As DateTime = #11/4/2018 10:37:26 AM#    'DateTime.MinValue

        'Dim request As HttpWebRequest = DirectCast(WebRequest.Create("http://nist.time.gov/actualtime.cgi?lzbc=siqm9b"), HttpWebRequest)
        'request.Method = "GET"
        'request.Accept = "text/html, application/xhtml+xml, */*"
        'request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)"
        'request.ContentType = "application/x-www-form-urlencoded"
        'request.CachePolicy = New RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)
        ''No caching
        'Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
        'If response.StatusCode = HttpStatusCode.OK Then
        '    Dim stream As New StreamReader(response.GetResponseStream())
        '    Dim html As String = stream.ReadToEnd()
        '    '<timestamp time=\"1395772696469995\" delay=\"1395772696469995\"/>
        '    Dim time As String = Regex.Match(html, "(?<=\btime="")[^""]*").Value
        '    If time = "" Then
        '        Return dateTime__1
        '    End If
        '    Dim milliseconds As Double = Convert.ToInt64(time) / 1000.0
        '    dateTime__1 = New DateTime(1970, 1, 1).AddMilliseconds(milliseconds).ToLocalTime()
        '    ' Else dateTime__1=
        'End If

        'Return dateTime__1
    End Function

    '=======================================================
    'Service provided by Telerik (www.telerik.com)
    'Conversion powered by NRefactory.
    'Twitter: @telerik
    'Facebook: facebook.com/telerik
    '=======================================================

End Class