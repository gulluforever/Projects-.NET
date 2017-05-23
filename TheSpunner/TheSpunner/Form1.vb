Imports System.IO

Public Class Form1
    Dim R As StreamReader
    Dim w As StreamWriter
    Dim i As Integer

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim fileDialog As New FolderBrowserDialog



        If fileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then

            For Each listFiles As String In Directory.GetFiles(fileDialog.SelectedPath)



                ListBox1.Items.Add(Path.GetFullPath((listFiles)))

            Next

        End If





    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        spuntxt.Text = "{"
        For i As Integer = 0 To ListBox1.Items.Count - 1
            R = New StreamReader(ListBox1.Items.Item(i).ToString, System.Text.Encoding.UTF8, True)
            article.Text = R.ReadToEnd

            Dim count As Integer = article.TextLength
            article.Text = article.Text.Insert(count, "|")
            spuntxt.AppendText(article.Text)
        Next


        spuntxt.AppendText("}")
        Dim rs As Integer = spuntxt.Text.LastIndexOf("|")
        spuntxt.Text = spuntxt.Text.Remove(rs, 1)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Clipboard.Clear()
        Clipboard.SetText(spuntxt.Text)
    End Sub

   
End Class
