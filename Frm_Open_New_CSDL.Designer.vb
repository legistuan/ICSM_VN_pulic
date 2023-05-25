<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Open_New_CSDL
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btn_openCSDL = New System.Windows.Forms.Button()
        Me.btn_newCSDL = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btn_openCSDL
        '
        Me.btn_openCSDL.Location = New System.Drawing.Point(12, 12)
        Me.btn_openCSDL.Name = "btn_openCSDL"
        Me.btn_openCSDL.Size = New System.Drawing.Size(242, 23)
        Me.btn_openCSDL.TabIndex = 0
        Me.btn_openCSDL.Text = "Mở dữ liệu đã có"
        Me.btn_openCSDL.UseVisualStyleBackColor = True
        '
        'btn_newCSDL
        '
        Me.btn_newCSDL.Location = New System.Drawing.Point(12, 46)
        Me.btn_newCSDL.Name = "btn_newCSDL"
        Me.btn_newCSDL.Size = New System.Drawing.Size(242, 23)
        Me.btn_newCSDL.TabIndex = 1
        Me.btn_newCSDL.Text = "Tạo dữ liệu mới"
        Me.btn_newCSDL.UseVisualStyleBackColor = True
        '
        'Frm_Open_New_CSDL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(264, 82)
        Me.Controls.Add(Me.btn_newCSDL)
        Me.Controls.Add(Me.btn_openCSDL)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Frm_Open_New_CSDL"
        Me.Text = "Mở/Tạo CSDL"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btn_openCSDL As System.Windows.Forms.Button
    Friend WithEvents btn_newCSDL As System.Windows.Forms.Button
End Class
