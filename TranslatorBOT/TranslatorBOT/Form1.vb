Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Net
Imports System.Threading
Imports System.Threading.Thread
Imports System.Threading.Tasks
Imports System.Text
Public Class Form1
    Dim thread As System.Threading.Thread
    Dim thread1 As System.Threading.Thread
    Private Property DecaptcherAPI As String
    Dim ulist As New List(Of String)
    Dim slist As New List(Of String)
    'Dim plist As New List(Of String)
    Dim ffl As New List(Of String)
    Dim R As StreamReader
    Dim i As Integer
    Dim w As StreamWriter
    Dim sd As String
    Dim taskOne As Tasks.Task
    Dim taskTwo As Tasks.Task
    Private Function getphpsession()
        Try
            Dim postData As String
            Dim url As String = "http://ets.freetranslation.com/"
            If CheckBox1.Checked Then
                postData = "sequence=core&dsttext=&mode=html&username=&password=&Submit=FREE+Translation&charset=UTF-8&template=textareaResponse-ETS.asp&lwSrc=&lwDest=&lwPair=&Project=&transType=ETS&language=English%2FGerman&targetServer=ETS&srctext=" + TextBox1.Text + "&srcLang=English&dstLang=English%2FGerman%3A%3A%3A%3Acore%3A%3AETS&submitbut=Translate&respSupplier=FREETRANSLATION&resptext="
            Else

                postData = "sequence=core&dsttext=&mode=html&username=&password=&Submit=FREE+Translation&charset=UTF-8&template=textareaResponse-ETS.asp&lwSrc=eng&lwDest=swe&lwPair=102&Project=&transType=ETS&language=English%2FSwedish&targetServer=ETS&srctext=" + TextBox1.Text + "&srcLang=English&dstLang=English%2FSwedish%3Aeng%3Aswe%3A102%3Acore%3A%3AETS&submitbut=Translate&respSupplier=FREETRANSLATION&resptext="
            End If

            Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
            Dim CookieJar As New CookieContainer
            Dim CookieList As New CookieCollection
            request.AllowAutoRedirect = True
            request.CookieContainer = CookieJar
            request.Method = "POST"
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
            request.Referer = url
            ' request.ContentType = "application/x-www-form-urlencoded; charset=utf-8"
            request.KeepAlive = True
            Dim encoding As New UTF8Encoding
            Dim byteData As Byte() = encoding.GetBytes(postData)
            Dim postreqstream As Stream = request.GetRequestStream()
            postreqstream.Write(byteData, 0, byteData.Length)
            postreqstream.Close()
            Dim postresponse As HttpWebResponse
            postresponse = DirectCast(request.GetResponse(), HttpWebResponse)
            Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
            Dim thepage1 As String = postreqreader.ReadToEnd

            Dim transtext As String = thepage1.Substring(thepage1.IndexOf("104px") + 8)
            Dim tle As Integer = transtext.IndexOf("textarea")
            transtext = transtext.Substring(0, tle - 2)
            ' transtext = transtext.Substring(transtext.IndexOf(">") + 1)
            'transtext = transtext.Substring(0, transtext.Length - 2)
            TextBox2.Text = transtext
            w = New System.IO.StreamWriter(sd + "/article" + i.ToString + ".txt", True, System.Text.Encoding.Default)
            w.WriteLine(TextBox2.Text)
            w.Close()
            R.Close()
            request.Abort()
            postresponse.Close()
            postreqreader.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        thread1 = New System.Threading.Thread(AddressOf main)
        thread1.Start()
    End Sub

    Private Function main()
        For i = 0 To ffl.Count - 1
            Try
                R = New StreamReader(ffl.Item(i).ToString, System.Text.Encoding.Default, True)
                Dim tas As String = R.ReadToEnd
                TextBox1.Text = tas.Replace("&", "")
                getphpsession()
                ProgressBar1.Increment(1)
            Catch ex As Exception
                ProgressBar1.Increment(1)
            End Try
            
        Next
  End Function

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim fileDialog As New FolderBrowserDialog

        'ListBox4.Items.Clear()

        If fileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then

            For Each listFiles As String In Directory.GetFiles(fileDialog.SelectedPath)



                ffl.Add(Path.GetFullPath((listFiles)))

            Next

        End If
        ProgressBar1.Maximum = ffl.Count

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim fileDialog1 As New FolderBrowserDialog



        If fileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then

            sd = fileDialog1.SelectedPath

        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
    End Sub
End Class
