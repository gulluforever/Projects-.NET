Public Class Form2

    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label3.Text = "  Posted : " + Form1.ctot.ToString
        Label12.Text = "Moderated : " + Form1.cfail.ToString
        Label7.Text = "  Failed : " + Form1.cF.ToString
    End Sub

    Private Sub Form2_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Form1.thread1.Abort()
    End Sub
End Class