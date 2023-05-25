Public Class zFRMtst 

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim cn As New XLS_Import
        GridControl1.DataSource = cn.DtFromQry("Select * from Sheet1")
    End Sub
End Class