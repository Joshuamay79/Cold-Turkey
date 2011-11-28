Public Class Form1

    Dim install As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        End
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        install = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\Software\KCTRP", "installpath", "C:\Program Files\ColdTurkey")
        Try
            Process.Start(install & "\ColdTurkey.exe")
            End
        Catch ex As Exception
            MsgBox("Could not locate ColdTurkeys main executable.")
        End Try
        End
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Process.Start("http://getcoldturkey.com/download.html")
        End
    End Sub
End Class
