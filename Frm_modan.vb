Public Class Frm_modan
    Dim nThoiGianCho As Integer

    Private Sub Frm_modan_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DoubleClick
        Me.Dispose()
    End Sub

    Private Sub Frm_modan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.AllowTransparency = True
        'nThoiGianCho = 15000 ' 15 giây
        Me.Opacity = 1 ' Tuong duong 100%
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'If nThoiGianCho <= 0 Then
        '    If Me.Opacity <= 0.03 Then
        '        Me.Dispose() ' Unload form
        '    End If
        '    Me.Opacity = Me.Opacity - 0.03
        'Else
        '    nThoiGianCho = nThoiGianCho - 100
        'End If
        Timer1.Interval = 1
        '        Form1.btnButton2.Text = r
        'If Me.Opacity >= 0.002 Then
        Me.Opacity = Me.Opacity - 0.0015 'Số 0.005 càng bé thì mờ dần càng chậm
        If Me.Opacity <= 0.005 Then
            Me.Dispose() ' Unload form
        End If

    End Sub

    Private Sub Frm_modan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        'nThoiGianCho = 0

    End Sub

    Private Sub RichTextBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles RichTextBox1.DoubleClick
        Me.Dispose()
    End Sub

    Private Sub RichTextBox1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles RichTextBox1.MouseClick
        Me.Dispose()
    End Sub

    Private Sub RichTextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RichTextBox1.TextChanged
        ' Me.Dispose()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class