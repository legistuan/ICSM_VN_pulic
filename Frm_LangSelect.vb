Imports System.Globalization
Imports System.Threading
Public Class Frm_LangSelect

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If RadioGroup1.SelectedIndex = 0 Then
            'myLanguage = "Tiếng Việt"
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("vi-VN")
            'Dim FrmVN = New FrmStart
            'FrmVN.Show()
        Else
            ' myLanguage = "English"
            ' EnglishTranslate()
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("en")
            'Dim FrmEnglish = New FrmStart
            'FrmEnglish.Show()
        End If
    End Sub
End Class