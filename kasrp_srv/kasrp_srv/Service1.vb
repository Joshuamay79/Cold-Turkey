Imports System.ServiceProcess
Imports System.IO
Imports System.Security.Cryptography
Imports Microsoft.Win32
Imports Microsoft.VisualBasic
Imports System.Net.Sockets
Imports System.Net
Imports System.Runtime.InteropServices

Public Class Service1
    Inherits System.ServiceProcess.ServiceBase

    Dim mut As Threading.Mutex
    Private m_previousExecutionState As UInteger
    Friend WithEvents timer As System.Timers.Timer
    Friend WithEvents adder As System.IO.FileSystemWatcher
    Dim install As String

#Region " Component Designer generated code "

    Public Sub New()
        MyBase.New()
        MyBase.CanHandleSessionChangeEvent = True
        ' This call is required by the Component Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call

    End Sub

    'UserService overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    Protected Overloads Sub OnStop(ByVal e As System.EventArgs)
        MyBase.OnStop()

        ' Restore previous state
        ' No way to recover; already exiting

    End Sub

    ' The main entry point for the process
    <MTAThread()> _
    Shared Sub Main()
        Dim ServicesToRun() As System.ServiceProcess.ServiceBase

        ' More than one NT Service may run within the same process. To add
        ' another service to this process, change the following line to
        ' create a second service object. For example,
        '
        '   ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
        '
        ServicesToRun = New System.ServiceProcess.ServiceBase() {New Service1}

        System.ServiceProcess.ServiceBase.Run(ServicesToRun)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    ' NOTE: The following procedure is required by the Component Designer
    ' It can be modified using the Component Designer.  
    ' Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.timer = New System.Timers.Timer()
        Me.adder = New System.IO.FileSystemWatcher()
        CType(Me.timer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.adder, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'timer
        '
        Me.timer.Enabled = True
        Me.timer.Interval = 60000.0R
        '
        'adder
        '
        Me.adder.EnableRaisingEvents = True
        Me.adder.Filter = "add_to_hosts"
        '
        'Service1
        '
        Me.CanStop = False
        Me.ServiceName = "KCTRP"
        CType(Me.timer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.adder, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub

#End Region

    Public sWinDir As String = Environ("WinDir")
    Public shostlocation As String = sWinDir & "\system32\drivers\etc\hosts"
    Public Value As Object
    Public sValue, srvTimeS As String
    Dim expectedtime, srvTime, timeleft As Double
    Dim logofflogon As Boolean
    Public fs As FileStream
    Public sw As StreamWriter
    Dim wrapper As New Simple3Des("ct_textbox")
    Dim wrapper2 As New Simple3Des("ct_checkbox")

    Protected Overrides Sub OnStart(ByVal args() As String)

        mut = New Threading.Mutex(False, "KeepmealivepleaseKCTRP")

        Try
            install = Registry.GetValue("HKEY_LOCAL_MACHINE\Software\KCTRP", "installpath", "C:\Program Files\ColdTurkey")
        Catch ex As Exception
            install = "C:\Program Files\ColdTurkey"
        End Try

        'Try to contact server to make sure firewall is not blocking me
        Try
            srvTimeS = GetHTML("http://getcoldturkey.com/time.php")
        Catch ex As Exception
            srvTimeS = "0"
        End Try
        If Val(srvTimeS) = 0 Then
            End
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        File.WriteAllText(install & "\data\done", "0")

        If My.Computer.FileSystem.FileExists(shostlocation) Then
            SetAttr(shostlocation, vbNormal)
        Else
            System.IO.File.AppendAllText(shostlocation, "")
        End If

        fs = New FileStream(shostlocation, FileMode.Append, FileAccess.Write, FileShare.Read)
        sw = New StreamWriter(fs)
        SetAttr(shostlocation, vbReadOnly)
        logofflogon = False

        adder.Path = sWinDir & "\system32\drivers\etc"

    End Sub
    Protected Overrides Sub OnSessionChange(ByVal changeDescription As System.ServiceProcess.SessionChangeDescription)
        Select Case changeDescription.Reason
            Case SessionChangeReason.SessionLogoff
                timer.Enabled = False
                logofflogon = True
            Case SessionChangeReason.SessionLogon
                If logofflogon Then
                    logofflogon = False
                End If
                timer.Enabled = True
        End Select

    End Sub

    Private Sub timer_Elapsed(ByVal sender As System.Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timer.Elapsed

        Threading.Thread.Sleep(1000)

        Try
            srvTimeS = GetHTML("http://getcoldturkey.com/time.php")
        Catch ex As Exception
            srvTimeS = "0"
        End Try
        srvTime = Val(srvTimeS)
        Try
            expectedtime = Val(wrapper.DecryptData(Registry.GetValue("HKEY_USERS\.DEFAULT\Software\KCTRP", "tc0", "604800")))
        Catch ex As Exception
            expectedtime = (srvTime + 604800)
            Registry.SetValue("HKEY_USERS\.DEFAULT\Software\KCTRP", "tc0", wrapper.EncryptData((srvTime + 604800)))
        End Try

        If srvTime >= expectedtime Then
            stopandgetout()
        End If

    End Sub

    Private Sub stopandgetout()
        sw.Close()

        Dim fileReader, original As String
        Dim startpos As Integer
        fileReader = My.Computer.FileSystem.ReadAllText(shostlocation)
        startpos = InStr(1, fileReader, "#### ColdTurkey Entries ####", 1)
        If startpos <> 0 And startpos <= 2 Then
            original = ""
        ElseIf startpos = 0 Then
            original = fileReader
        Else
            original = Microsoft.VisualBasic.Left(fileReader, startpos - 3)
        End If
        If My.Computer.FileSystem.FileExists(shostlocation) Then
            SetAttr(shostlocation, vbNormal)
        End If

        Dim fs2 As New FileStream(shostlocation, FileMode.Create, FileAccess.Write, FileShare.Read)
        Dim sw2 As New StreamWriter(fs2)
        sw2.Write(original)
        sw2.Close()
        SetAttr(shostlocation, vbReadOnly)
        File.WriteAllText(install & "\data\done", "1")

        Me.Stop()
    End Sub

    Function GetHTML(ByVal strPage As String) As String
        Dim strReply As String = "0"
        'Dim objErr As ErrObject

        'Try
        Dim objHttpRequest As System.Net.HttpWebRequest
        Dim objHttpResponse As System.Net.HttpWebResponse
        objHttpRequest = System.Net.HttpWebRequest.Create(strPage)
        objHttpResponse = objHttpRequest.GetResponse
        Dim objStrmReader As New System.IO.StreamReader(objHttpResponse.GetResponseStream)

        strReply = objStrmReader.ReadToEnd()

        'Catch ex As Exception
        'strReply = "ERROR! " + ex.Message.ToString
        'End Try

        Return strReply

    End Function

    Private Sub adder_Changed(ByVal sender As System.Object, ByVal e As System.IO.FileSystemEventArgs) Handles adder.Changed

        If My.Computer.FileSystem.FileExists(sWinDir & "\system32\drivers\etc\add_to_hosts") Then
            Dim toAdd As String
            toAdd = System.IO.File.ReadAllText(sWinDir & "\system32\drivers\etc\add_to_hosts")
            SetAttr(shostlocation, vbNormal)
            sw.Write(toAdd)
            sw.Flush()
            SetAttr(shostlocation, vbReadOnly)
            Try
                System.IO.File.Delete(sWinDir & "\system32\drivers\etc\add_to_hosts")
            Catch ex As Exception
            End Try
        End If

    End Sub
End Class

Public NotInheritable Class Simple3Des
    Private TripleDes As New TripleDESCryptoServiceProvider
    Private Function TruncateHash(
        ByVal key As String,
        ByVal length As Integer) As Byte()

        Dim sha1 As New SHA1CryptoServiceProvider

        ' Hash the key.
        Dim keyBytes() As Byte =
            System.Text.Encoding.Unicode.GetBytes(key)
        Dim hash() As Byte = sha1.ComputeHash(keyBytes)

        ' Truncate or pad the hash.
        ReDim Preserve hash(length - 1)
        Return hash
    End Function
    Sub New(ByVal key As String)
        ' Initialize the crypto provider.
        TripleDes.Key = TruncateHash(key, TripleDes.KeySize \ 8)
        TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8)
    End Sub
    Public Function EncryptData(
        ByVal plaintext As String) As String

        ' Convert the plaintext string to a byte array.
        Dim plaintextBytes() As Byte =
            System.Text.Encoding.Unicode.GetBytes(plaintext)

        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the encoder to write to the stream.
        Dim encStream As New CryptoStream(ms,
            TripleDes.CreateEncryptor(),
            System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
        encStream.FlushFinalBlock()

        ' Convert the encrypted stream to a printable string.
        Return Convert.ToBase64String(ms.ToArray)
    End Function
    Public Function DecryptData(
    ByVal encryptedtext As String) As String
        Dim encryptedBytes() As Byte
        ' Convert the encrypted text string to a byte array.
        Try
            encryptedBytes = Convert.FromBase64String(encryptedtext)
        Catch ef As System.FormatException
            'encryptedBytes = 
            End
        End Try
        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the decoder to write to the stream.
        Dim decStream As New CryptoStream(ms,
            TripleDes.CreateDecryptor(),
            System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
        decStream.FlushFinalBlock()

        ' Convert the plaintext stream to a string.
        Return System.Text.Encoding.Unicode.GetString(ms.ToArray)
    End Function

End Class
