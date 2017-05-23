Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Net
Imports System.Threading
Imports System.Threading.Thread
Imports System.Threading.Tasks
Imports System.Text
Public Class Form1
    Dim q As Integer
    Dim thread As System.Threading.Thread
    Dim TC As List(Of Threading.Thread) = New List(Of Threading.Thread)
    Public thread1 As System.Threading.Thread
    Private Property DecaptcherAPI As String
    Dim ulist As New List(Of String)
    Dim slist As New List(Of String)
    'Dim plist As New List(Of String)
    Dim ffl As New List(Of String)
    Dim not1 As Integer
    Dim wlist As New List(Of String)
    Dim lsilist As New List(Of String)
    Dim i As ULong
    'Dim taskOne As Tasks.Task
    Dim Urls As New Queue
    Dim Wrls As New Queue
    Dim Lrls As New Queue
    Dim token As Boolean
    Dim Frls As New Queue
    Dim taskTwo As Tasks.Task
    Dim ww As Integer
    Dim ll As Integer
    Public ctot As Integer
    Dim taskTwoArray As List(Of Tasks.Task) = New List(Of Tasks.Task)
    Dim noof As ULong
    Public cfail As Integer
    Public cF As Integer
    Dim ce As Integer
    Dim captimeout As Integer
    Dim webreqTimeOut As Integer
    Private Function getphpsession()
        Try
        
            Dim request As HttpWebRequest
            Dim response As HttpWebResponse
            Dim url As String = Urls.Dequeue
            Dim CookieJar As New CookieContainer
            Dim CookieList As New CookieCollection
            
            request = DirectCast(WebRequest.Create(url), HttpWebRequest)
            request.AllowAutoRedirect = True
            request.CookieContainer = CookieJar
            request.Timeout = webreqTimeOut
            request.Method = "GET"
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
            request.Referer = url
            request.ContentType = "application/x-www-form-urlencoded"
            request.KeepAlive = True
            response = DirectCast(request.GetResponse(), HttpWebResponse)
            Dim postreqreader As New StreamReader(response.GetResponseStream())
            Dim thepage1 As String = postreqreader.ReadToEnd
            Dim scl As String
            If thepage1.Contains("shortlink") Then
                Dim reg1 As Regex = New Regex("shortlink.*")
                Dim mat1 As Match = reg1.Match(thepage1)
                Dim tempcap As String = mat1.Value
                'MessageBox.Show(tempcap)
                Dim ref As String = tempcap.Substring(17, tempcap.Length - 17 - 4)
                scl = ref.Substring(ref.IndexOf("=") + 1)
                '    MessageBox.Show(scl)
            ElseIf thepage1.Contains("canonical") Then
                Dim reg1 As Regex = New Regex("canonical.*")
                Dim mat1 As Match = reg1.Match(thepage1)
                Dim tempcap As String = mat1.Value
                'MessageBox.Show(tempcap)
                Dim ref As String = tempcap.Substring(17, tempcap.Length - 17 - 4)
                scl = ref.Substring(ref.IndexOf("=") + 1)
            Else
                scl = Nothing
            End If
            Dim btext As String
            If thepage1.Contains("Post Comment") Then
                btext = "Post Comment"
            ElseIf thepage1.Contains("Submit Comment") Then

                btext = "Submit Comment"
            Else
                btext = "Submit"
            End If
            Dim reg2 As Regex = New Regex("Leave a Reply[\w\W]*")
            Dim mat2 As Match = reg2.Match(thepage1)
            Dim tempcap1 As String = mat2.Value
            Dim temp2 As String = tempcap1.Substring(tempcap1.IndexOf("form action=") + 13)
            Dim purl As String = temp2.Substring(0, temp2.IndexOf(""""))
            postreqreader.Close()
            response.Close()
            request.Abort()
            If ww > wlist.Count Then

                ww = 0
            End If
            If ll > lsilist.Count Then

                ll = 0

            End If
            If q > ffl.Count Then
                q = 0
            End If
            ww = ww + 1
            ll = ll + 1
            Dim www As String = wlist.Item(ww)
            Dim lsi As String = lsilist.Item(ll)
            post(url, CookieJar, CookieList, btext, www, lsi, scl, purl)
        Catch ex As Exception
            cF = cF + 1
            'MessageBox.Show(ex.ToString)
        Finally
            Try
                'ProgressBar1.Increment(1)
                If Urls.Count > 0 Then
                    getphpsession()
                Else
                    Cancel()
                    
                End If
            Catch ex As Exception
            End Try
        End Try
    End Function


    Private Function getarticle(ByVal q As Integer)
        Try
            Dim R = New StreamReader(ffl.Item(q).ToString, System.Text.Encoding.Default, True)
            Dim da As String = R.ReadToEnd.Replace("&", " ")
            Return da
        Catch ex As Exception
        End Try
    End Function
    Private Function post(ByVal url, ByVal CookieJar, ByVal CookieList, ByVal btext, ByVal www, ByVal lsi, ByVal scl, ByVal purl)
        Try
            q = q + 1

            Dim postReq As HttpWebRequest
            Dim postresponse As HttpWebResponse
            Dim c As String = getarticle(q)
            Dim postData As String = "author=" + lsi + "&email=" + GenerateString() + "&url=" + www + "&comment=" + c + "&submit=" + btext + "&comment_post_ID=" + scl + "&comment_parent=0"
            postReq = DirectCast(WebRequest.Create(purl), HttpWebRequest)
            Dim encoding As New UTF8Encoding
            Dim byteData As Byte() = encoding.GetBytes(postData)
            postReq.CookieContainer = CookieJar
            postReq.AllowAutoRedirect = True
            postReq.Method = "POST"
            postReq.Referer = url
            postReq.Timeout = webreqTimeOut
            postReq.KeepAlive = True
            postReq.ContentType = "application/x-www-form-urlencoded"
            Dim postreqstream As Stream = postReq.GetRequestStream()
            postreqstream.Write(byteData, 0, byteData.Length)
            postreqstream.Close()
            postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
            Dim postreqreader As New StreamReader(postresponse.GetResponseStream())
            Dim thepage1 As String = postreqreader.ReadToEnd
            postreqreader.Close()
            If thepage1.Contains("moderation") Then
                cfail = cfail + 1
            ElseIf thepage1.Contains(www) Then
                slist.Add(url)
                ctot = ctot + 1
            Else
                cF = cF + 1
            End If
            postReq.Abort()
            postresponse.Close()
            postreqreader.Close()
            postreqstream.Close()
        Catch ex As Exception
            cF = cF + 1
        Finally

        End Try
    End Function
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        BackgroundWorker1.RunWorkerAsync()
    End Sub
    Private Function MainLogic()
        'not1 = Convert.ToInt32(TextBox4.Text)
        '  MessageBox.Show(not1)
        webreqTimeOut = Convert.ToInt32(TextBox2.Text)
        not1 = Convert.ToInt32(TextBox4.Text)
        Label1.Text = "Started.."
        Label1.Refresh()
        For i = 0 To not1 - 1
            If Urls.Count > 0 Then
                Dim T As New Threading.Thread(AddressOf getphpsession)
                T.Start()
                TC.Add(T)
            End If
        Next
    End Function
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        OpenFileDialog1.ShowDialog()
        ulist.AddRange(IO.File.ReadAllLines(OpenFileDialog1.FileName))
        For i = 0 To ulist.Count - 1
            Urls.Enqueue(ulist.Item(i))
        Next
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        OpenFileDialog2.ShowDialog()
        wlist.AddRange(IO.File.ReadAllLines(OpenFileDialog2.FileName))
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        OpenFileDialog3.ShowDialog()
        lsilist.AddRange(IO.File.ReadAllLines(OpenFileDialog3.FileName))
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        SaveFileDialog1.ShowDialog()
        SaveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        SaveFileDialog1.FilterIndex = 2
    End Sub

    Private Sub SaveFileDialog1_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Dim FileToSaveAs As String = SaveFileDialog1.FileName

        Dim objwriter As New System.IO.StreamWriter(FileToSaveAs + ".txt")
        Dim rr As Integer

        For rr = 0 To slist.Count - 1

            Dim gul As String = slist(rr)

            objwriter.WriteLine(gul)

        Next
        MessageBox.Show("Saved!")
        objwriter.Close()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim fileDialog As New FolderBrowserDialog

        'ListBox4.Items.Clear()

        If fileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then

            For Each listFiles As String In Directory.GetFiles(fileDialog.SelectedPath)



                ffl.Add(Path.GetFullPath((listFiles)))

            Next

        End If
    End Sub

    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        GenerateString()
    End Sub
    Public Function GenerateString()
        Dim letters As String = "abcdefghijklmnopqrstuvwxyz0123456789"
        Dim str As String = ""
        Dim r As New Random
        For I As Integer = 0 To 10
            str = str + letters(r.Next(0, 35))
            'str = str + letters(New System.Random().Next(0, letters.Length + 1))
        Next
        Dim emailv As String = str + "@gmail.com"
        Return emailv
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Cancel()

    End Sub
    Sub Cancel()
        Label1.Text = "Wait"
        Label1.Refresh()
        Urls.Clear()
        Dim intCount As Integer = TC.Count
        If intCount > 0 Then
            For Each T As Threading.Thread In TC
                'T.Suspend()
                T.Interrupt()


            Next

        End If
        Label1.Text = "Completed"
    End Sub

    Private Sub Button8_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
     Form2.Show()
    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        thread1 = New System.Threading.Thread(AddressOf MainLogic)
        thread1.Start()
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        '  MessageBox.Show("Background Worker COmpleted")
    End Sub
End Class

Public Class ConfigurableWebClient
    Inherits WebClient


    Public Property Timeout() As System.Nullable(Of Integer)
        Get
            Return m_Timeout
        End Get
        Set(ByVal value As System.Nullable(Of Integer))
            m_Timeout = Value
        End Set
    End Property
    Private m_Timeout As System.Nullable(Of Integer)

    Public Property ConnectionLimit() As System.Nullable(Of Integer)
        Get
            Return m_ConnectionLimit
        End Get
        Set(ByVal value As System.Nullable(Of Integer))
            m_ConnectionLimit = Value
        End Set
    End Property
    Private m_ConnectionLimit As System.Nullable(Of Integer)



    Protected Overrides Function GetWebRequest(ByVal address As Uri) As WebRequest


        Dim baseRequest = MyBase.GetWebRequest(address)

        Dim webRequest = TryCast(baseRequest, HttpWebRequest)

        If webRequest Is Nothing Then

            Return baseRequest
        End If



        If Timeout.HasValue Then

            webRequest.Timeout = Timeout.Value
        End If



        If ConnectionLimit.HasValue Then

            webRequest.ServicePoint.ConnectionLimit = ConnectionLimit.Value
        End If



        Return webRequest

    End Function

End Class
