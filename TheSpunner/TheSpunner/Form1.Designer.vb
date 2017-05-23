<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.article = New System.Windows.Forms.TextBox()
        Me.spuntxt = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(192, 90)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(21, 4)
        Me.ListBox1.TabIndex = 0
        Me.ListBox1.Visible = False
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(8, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(269, 26)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Select Directory of Spunned Articles"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(8, 56)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(268, 28)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Spin It!"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'article
        '
        Me.article.Location = New System.Drawing.Point(125, 44)
        Me.article.Multiline = True
        Me.article.Name = "article"
        Me.article.Size = New System.Drawing.Size(17, 10)
        Me.article.TabIndex = 3
        Me.article.Visible = False
        '
        'spuntxt
        '
        Me.spuntxt.Location = New System.Drawing.Point(12, 138)
        Me.spuntxt.Multiline = True
        Me.spuntxt.Name = "spuntxt"
        Me.spuntxt.Size = New System.Drawing.Size(265, 154)
        Me.spuntxt.TabIndex = 4
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(9, 99)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(267, 26)
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "Copy Spunned Article to ClipBoard"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(287, 305)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.spuntxt)
        Me.Controls.Add(Me.article)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ListBox1)
        Me.Name = "Form1"
        Me.Text = "The Spunner"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents article As System.Windows.Forms.TextBox
    Friend WithEvents spuntxt As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button

End Class
