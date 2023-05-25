<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Startup
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Startup))
        Me.GroupControlLang = New DevExpress.XtraEditors.GroupControl()
        Me.RadLang = New DevExpress.XtraEditors.RadioGroup()
        Me.GroupControlProj = New DevExpress.XtraEditors.GroupControl()
        Me.RadProj = New DevExpress.XtraEditors.RadioGroup()
        Me.BtnOK = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnExit = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GroupControlLang, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControlLang.SuspendLayout()
        CType(Me.RadLang.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControlProj, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControlProj.SuspendLayout()
        CType(Me.RadProj.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControlLang
        '
        Me.GroupControlLang.Controls.Add(Me.RadLang)
        Me.GroupControlLang.Location = New System.Drawing.Point(9, 9)
        Me.GroupControlLang.Name = "GroupControlLang"
        Me.GroupControlLang.Size = New System.Drawing.Size(138, 97)
        Me.GroupControlLang.TabIndex = 0
        Me.GroupControlLang.Text = "Language selection"
        '
        'RadLang
        '
        Me.RadLang.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadLang.Location = New System.Drawing.Point(2, 21)
        Me.RadLang.Name = "RadLang"
        Me.RadLang.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Tiếng Việt"), New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "English")})
        Me.RadLang.Size = New System.Drawing.Size(134, 74)
        Me.RadLang.TabIndex = 1
        '
        'GroupControlProj
        '
        Me.GroupControlProj.Controls.Add(Me.RadProj)
        Me.GroupControlProj.Location = New System.Drawing.Point(156, 9)
        Me.GroupControlProj.Name = "GroupControlProj"
        Me.GroupControlProj.Size = New System.Drawing.Size(138, 97)
        Me.GroupControlProj.TabIndex = 1
        Me.GroupControlProj.Text = "Tạo mới/Mở Project"
        '
        'RadProj
        '
        Me.RadProj.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadProj.Location = New System.Drawing.Point(2, 21)
        Me.RadProj.Name = "RadProj"
        Me.RadProj.Properties.Items.AddRange(New DevExpress.XtraEditors.Controls.RadioGroupItem() {New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Mở project gần đây"), New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Mở Project sẵn có"), New DevExpress.XtraEditors.Controls.RadioGroupItem(True, "Tạo Project mới")})
        Me.RadProj.Size = New System.Drawing.Size(134, 74)
        Me.RadProj.TabIndex = 0
        '
        'BtnOK
        '
        Me.BtnOK.Location = New System.Drawing.Point(26, 112)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(100, 23)
        Me.BtnOK.TabIndex = 2
        Me.BtnOK.Text = "Tiếp tục"
        '
        'BtnExit
        '
        Me.BtnExit.Location = New System.Drawing.Point(176, 112)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(100, 23)
        Me.BtnExit.TabIndex = 3
        Me.BtnExit.Text = "Thoát"
        '
        'Frm_Startup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(303, 143)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.GroupControlProj)
        Me.Controls.Add(Me.GroupControlLang)
        Me.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Frm_Startup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FrmStart0"
        CType(Me.GroupControlLang, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControlLang.ResumeLayout(False)
        CType(Me.RadLang.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControlProj, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControlProj.ResumeLayout(False)
        CType(Me.RadProj.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupControlLang As DevExpress.XtraEditors.GroupControl
    Friend WithEvents RadLang As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents GroupControlProj As DevExpress.XtraEditors.GroupControl
    Friend WithEvents RadProj As DevExpress.XtraEditors.RadioGroup
    Friend WithEvents BtnOK As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents AppManager1 As DotSpatial.Controls.AppManager
End Class
