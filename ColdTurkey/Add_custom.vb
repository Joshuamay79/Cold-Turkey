Imports System.IO

Public Class Add_custom

    Dim install As String
    Dim list_cus3 As New ListBox

    Private Sub addbox_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles addbox_cancel.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    Private Sub addbox_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles addbox_add.Click
        Try
            install = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\Software\KCTRP", "installpath", "C:\Program Files\ColdTurkey")
        Catch ex As Exception
            install = "C:\Program Files\ColdTurkey"
        End Try

        Dim tempString2 As String = txt_add2.Text.ToString
        If StrComp(tempString2, "") = 0 Then
        ElseIf InStr(tempString2, ".") = 0 Then
            MsgBox("The domain should have an extention (like .com or .net).")
        Else
            If InStr(tempString2, "http://") = 1 Then
                tempString2 = Microsoft.VisualBasic.Right(tempString2, tempString2.Length - 7)
            End If
            If InStr(tempString2, "www.") = 1 Then
                tempString2 = Microsoft.VisualBasic.Right(tempString2, tempString2.Length - 4)
            End If
            If InStr(tempString2, "/") <> 0 Then
                Dim loc As Integer
                loc = InStr(tempString2, "/")
                tempString2 = Microsoft.VisualBasic.Left(tempString2, loc - 1)
            End If
            If list_cus3.Items.Contains(tempString2) Then
                MsgBox("This address is already in the list.")
            ElseIf StrComp(tempString2, "getcoldturkey.com") = 0 Then
                MsgBox("This is a mandatory address and must not be blocked.")
            Else
                list_cus2.Items.Add(tempString2)
                txt_add2.Text = ""
            End If

        End If
    End Sub

    Private Sub Add_custom_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            install = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\Software\KCTRP", "installpath", "C:\Program Files\ColdTurkey")
        Catch ex As Exception
            install = "C:\Program Files\ColdTurkey"
        End Try

        Try
            Dim reader As String
            reader = My.Computer.FileSystem.ReadAllText(install + "\data\custom")
            Dim strs() As String
            strs = Split(reader, Environment.NewLine)
            For Each line As String In strs
                If StrComp(line, "") <> 0 Then
                    list_cus3.Items.Add(line)
                    list_cus2.Items.Add(line)
                End If
            Next
        Catch
        End Try


    End Sub

    Private Sub txt_add2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_add2.KeyPress
        If e.KeyChar = Chr(13) Then
            addbox_add.PerformClick()
        End If
    End Sub

    Private Sub addbox_remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles addbox_remove.Click
        Dim locofitem As Integer
        locofitem = list_cus2.SelectedIndex
        If list_cus3.Items.Contains(list_cus2.SelectedItem) Then
            MsgBox("Haha nice try, this item is currently blocked.")
        Else
            list_cus2.Items.Remove(list_cus2.SelectedItem)
            If locofitem <= list_cus2.Items.Count And locofitem > 0 Then
                list_cus2.SetSelected(locofitem - 1, True)
            ElseIf locofitem = 0 And list_cus2.Items.Count > 0 Then
                list_cus2.SetSelected(0, True)
            End If

        End If
    End Sub

    Private Sub addbox_done_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles addbox_done.Click

        Dim new_added As Boolean = False
        For Each itm As String In list_cus2.Items
            If list_cus3.Items.Contains(itm) Then
            Else
                new_added = True
            End If
        Next
        If new_added = True Then
            Using sw As New IO.StreamWriter(install + "\data\custom", False)
                For Each itm As String In list_cus2.Items
                    sw.WriteLine(itm)
                Next
                sw.Close()
            End Using
            Using sw As New IO.StreamWriter(Environ("WinDir") & "\system32\drivers\etc\add_to_hosts", True)
                For Each itm As String In list_cus2.Items
                    If list_cus3.Items.Contains(itm) Then
                    Else
                        sw.WriteLine("0.0.0.0 " + itm)
                        sw.WriteLine("0.0.0.0 www." + itm)
                    End If
                Next
                sw.Close()
            End Using
            Me.Hide()
            MsgBox("New items added. " + vbNewLine + "You might need to restart your browser windows to see the changes.", MsgBoxStyle.OkOnly, "Cold Turkey")
        Else
            Me.Hide()
            MsgBox("No new items added.", MsgBoxStyle.OkOnly, "Cold Turkey")
        End If

        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub txt_add2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_add2.TextChanged
        If txt_add2.Text.Contains(Keys.Return) Then
            addbox_add.PerformClick()
        End If
    End Sub


    Private Sub import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles import.Click

        Dim filepath As String

        If importfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            filepath = importfile.FileName

            If File.Exists(filepath) Then

                Try
                    Dim reader As String
                    reader = My.Computer.FileSystem.ReadAllText(filepath)
                    Dim strs() As String
                    strs = Split(reader, Environment.NewLine)
                    For Each line As String In strs
                        If StrComp(line, "") = 0 Then
                        ElseIf InStr(line, ".") = 0 Then
                            MsgBox("This domain should have an extention. (like .com or .net)" + vbNewLine + line)
                        Else
                            If StrComp(line, "") <> 0 Then

                                If InStr(line, "http://") = 1 Then
                                    line = Microsoft.VisualBasic.Right(line, line.Length - 7)
                                End If
                                If InStr(line, "www.") = 1 Then
                                    line = Microsoft.VisualBasic.Right(line, line.Length - 4)
                                End If
                                If InStr(line, "/") <> 0 Then
                                    Dim loc As Integer
                                    loc = InStr(line, "/")
                                    line = Microsoft.VisualBasic.Left(line, loc - 1)
                                End If
                                If list_cus2.Items.Contains(line) Then
                                    MsgBox("This address is already in the list." + vbNewLine + line)
                                ElseIf StrComp(line, "getcoldturkey.com") = 0 Then
                                    MsgBox("This is a mandatory address and must not be blocked." + vbNewLine + line)
                                Else
                                    list_cus2.Items.Add(line)
                                    Using sw As New IO.StreamWriter(install + "\data\custom", False)
                                        For Each itm As String In list_cus2.Items
                                            sw.WriteLine(itm)
                                        Next
                                    End Using
                                End If
                            End If
                        End If
                    Next
                Catch
                    MsgBox("Error reading file.")
                End Try

            End If
        End If

    End Sub

End Class