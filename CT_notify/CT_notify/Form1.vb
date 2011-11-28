Imports Microsoft.Win32
Imports System.Security.Cryptography
Imports System.Diagnostics
Imports System.IO

Public Class Form1

    Dim done As Integer
    Dim install, pop As String

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Hide()

        Try
            install = Registry.GetValue("HKEY_CURRENT_USER\Software\KCTRP", "installpath", "C:\Program Files\ColdTurkey")
        Catch ex As Exception
            install = "C:\Program Files\ColdTurkey"
        End Try

        Try
            done = Val(File.ReadAllText(install & "\data\done"))
        Catch ex As Exception
            MsgBox("Error: Service not Started")
            End
        End Try

        If done = 1 Then
            pop = Registry.GetValue("HKEY_CURRENT_USER\Software\KCTRP", "pop", "0").ToString
            If Val(pop) = 1 Then
                End
            Else
                Registry.SetValue("HKEY_CURRENT_USER\Software\KCTRP", "pop", "1")
                Process.Start(install & "\ct_popup.exe")
                End
            End If
        End If

        watcher.Path() = install & "\data"

    End Sub

    Private Sub watcher_Changed(ByVal sender As System.Object, ByVal e As System.IO.FileSystemEventArgs) Handles watcher.Changed

        done = Val(File.ReadAllText(install & "\data\done"))

        If done = 1 Then
            pop = Registry.GetValue("HKEY_CURRENT_USER\Software\KCTRP", "pop", "0").ToString
            If Val(pop) = 1 Then
                End
            ElseIf (Val(pop) <> 1) Then
                Registry.SetValue("HKEY_CURRENT_USER\Software\KCTRP", "pop", "1")
                Process.Start(install & "\ct_popup.exe")
                End
            End If
        End If

    End Sub

    Private Sub Form1_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.Hide()
    End Sub

End Class
