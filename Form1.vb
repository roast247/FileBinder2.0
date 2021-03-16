Imports System.IO
Imports System.IO.Compression
Imports System.Text

Public Class Form1

    Private BIcon As String = vbNullString
    Private draggable As Boolean
    Private mouseY As Integer
    Private mouseX As Integer

    Public Function Compress(ByVal bytData() As Byte) As Byte()
        Using oMS As New MemoryStream()
            Using oGZipStream As New GZipStream(oMS, CompressionMode.Compress)
                oGZipStream.Write(bytData, 0, bytData.Length)
                oGZipStream.Close()
                ReDim bytData(oMS.ToArray.Length - 1)
                bytData = oMS.ToArray
            End Using
            oMS.Close()
        End Using
        Return bytData
    End Function

    Public Function countitems() As Integer
        Dim num As Integer
        Dim enumerator As IEnumerator
        Try
            enumerator = ListView1.Items.GetEnumerator
            Do While enumerator.MoveNext
                Dim current As ListViewItem = DirectCast(enumerator.Current, ListViewItem)
                num += 1
            Loop
        Finally
            If TypeOf enumerator Is IDisposable Then
                TryCast(enumerator, IDisposable).Dispose()
            End If
        End Try
        Return num
    End Function
    Private Sub KryptonButton1_Click(sender As Object, e As EventArgs) Handles KryptonButton1.Click
        Try
            Dim O As New OpenFileDialog
            With O
                .Title = "Select An Application To Bind It"
                .Filter = "Application | *.exe"
            End With
            If O.ShowDialog = Windows.Forms.DialogResult.OK Then
                ListView1.Items.Add(O.FileName)
                Label1.Text = "- "
            End If
        Catch ex As Exception
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Hand)
        End Try
    End Sub

    Private Sub KryptonButton2_Click(sender As Object, e As EventArgs) Handles KryptonButton2.Click
        Try
            Dim enumerator As IEnumerator
            Try
                enumerator = ListView1.SelectedItems.GetEnumerator
                Do While enumerator.MoveNext
                    DirectCast(enumerator.Current, ListViewItem).Remove()
                Loop
            Finally
                If TypeOf enumerator Is IDisposable Then
                    TryCast(enumerator, IDisposable).Dispose()
                End If
            End Try
        Catch ex As Exception
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Hand)
        End Try
    End Sub

    Private Sub KryptonButton3_Click(sender As Object, e As EventArgs) Handles KryptonButton3.Click
        ListView1.Items.Clear()
    End Sub

    Private Sub KryptonButton4_Click(sender As Object, e As EventArgs) Handles KryptonButton4.Click
        Try
            Dim O As New OpenFileDialog
            With O
                .Title = "Select An Icon"
                .Filter = "Icon | *.ico"
            End With
            If O.ShowDialog = Windows.Forms.DialogResult.OK Then
                BIcon = O.FileName
                Label1.Text = "- "
            End If
        Catch ex As Exception
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Hand)
        End Try
    End Sub

    Private Sub KryptonButton5_Click(sender As Object, e As EventArgs) Handles KryptonButton5.Click
        Try
            Dim Sv As New SaveFileDialog
            With Sv
                .Title = "Save The Binding Stub"
                .Filter = "Application | *.exe"
                .FileName = "Binded"
                If Sv.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim S As String = Sv.FileName
                Dim buffer As Byte() = My.Resources.Stub
                My.Computer.FileSystem.WriteAllBytes(.filename, buffer, False)
                Dim N As Integer = countitems()
                ResourceWriter.WriteResource(S, Encoding.UTF7.GetBytes(N), 1)
                Dim IEnu As IEnumerator
                IEnu = ListView1.Items.GetEnumerator
                For i = 0 To N - 1
                    IEnu.MoveNext()
                    Dim current As ListViewItem = DirectCast(IEnu.Current, ListViewItem)
                    ResourceWriter.WriteResource(S, Compress(File.ReadAllBytes(current.Text)), i + 2)
                Next
                If BIcon <> "" Then
                    IconInjector.InjectIcon(S, BIcon)
                End If
                Label1.Text = "Binded Successfully"
            Else
                Label1.Text = "Binding Failed"
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Asterisk)
            End If
            End With
        Catch ex As Exception
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Hand)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        draggable = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If draggable Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub

    Private Sub Panel1_MouseUp(sender As Object, e As MouseEventArgs) Handles Panel1.MouseUp
        draggable = False
    End Sub

    Private Sub Label4_MouseDown(sender As Object, e As MouseEventArgs) Handles Label4.MouseDown
        draggable = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub
    Private Sub Label4_MouseMove(sender As Object, e As MouseEventArgs) Handles Label4.MouseMove
        If draggable Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub
    Private Sub Label4_MouseUp(sender As Object, e As MouseEventArgs) Handles Label4.MouseUp
        draggable = False
    End Sub
    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        draggable = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub
    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If draggable Then
            Me.Top = Cursor.Position.Y - mouseY
            Me.Left = Cursor.Position.X - mouseX
        End If
    End Sub
    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        draggable = False
    End Sub
End Class