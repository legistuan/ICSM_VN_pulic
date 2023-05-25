Public Class Frm_Open_New_CSDL

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_openCSDL.Click
        aFrmMain.btn_open_Click(Nothing, Nothing)
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btn_newCSDL.Click
        aFrmMain.btn_New_Click(Nothing, Nothing)
        Me.Close()
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub
End Class