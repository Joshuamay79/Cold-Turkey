Option Explicit On
Imports System.IO
Imports System.Security.Cryptography
Imports Microsoft.Win32
Imports Microsoft.VisualBasic
Imports ServiceTools

Public Class coldturkey
    Dim shostlocation, sWinDir, conditiontext As String
    Dim srv_alreadyexists As Boolean = False
    Dim actualhour As Integer
    Dim wrapper As New Simple3Des("ct_textbox")
    Dim wrapper2 As New Simple3Des("ct_checkbox")
    Dim install As String
    Dim firsttimeclickcus As Boolean = True

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim tc0 As Integer
        Try
            install = Registry.GetValue("HKEY_CURRENT_USER\Software\KCTRP", "installpath", "C:\Program Files\ColdTurkey")
        Catch ex As Exception
            install = "C:\Program Files\ColdTurkey"
        End Try

        Try
            If srvcont.Status = ServiceProcess.ServiceControllerStatus.Stopped Or ServiceProcess.ServiceControllerStatus.StopPending Then
                srv_alreadyexists = True
            End If
            If srvcont.Status = ServiceProcess.ServiceControllerStatus.Running Then

                Dim srvTimeS, blockeduntil As String
                Dim srvTime, value As Double
                Try
                    srvTimeS = GetHTML("http://getcoldturkey.com/time.php")
                Catch ex As Exception
                    MsgBox("Error contacting the time server. You need to be plugged into the Internet.")
                    End
                End Try
                srvTime = Double.Parse(srvTimeS)
                Try
                    tc0 = Val(wrapper.DecryptData(Registry.GetValue("HKEY_USERS\.DEFAULT\Software\KCTRP", "tc0", "604800")))
                Catch ex As Exception
                    tc0 = 604800
                End Try
                value = tc0 - srvTime
                If value = 0 Then
                    MsgBox("Opps, I should have unblocked you, but for some reason it didn't work! I'm going to fix that right now..." + vbNewLine + "Please restart me after clicking OK!")
                    erroredout()
                End If
                blockeduntil = "You are still blocked for: " & SecondsToText(value)
                Dim ans As Integer = MsgBox(blockeduntil + vbNewLine + "Click OK to add sites to block or Cancel to continue working.", MsgBoxStyle.OkCancel, "Cold Turkey")
                If ans = 1 Then
                    Me.Hide()
                    Add_custom.ShowDialog()
                    End
                Else
                End If
                End
            End If
        Catch f As Exception
        End Try
        sWinDir = Environ("WinDir")
        shostlocation = sWinDir & "\system32\drivers\etc\hosts"

        My.Computer.FileSystem.CreateDirectory(install & "\data")

        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "dd/MM/yyyy"
        DateTimePicker1.MinDate = Date.Today
        DateTimePicker1.MaxDate = DateAdd("d", 7, CDate(Date.Today))
        mornin.DropDownStyle = ComboBoxStyle.DropDownList
        mornin.Text = ""
        hour.DropDownStyle = ComboBoxStyle.DropDownList
        hour.Text = ""
        hour24.DropDownStyle = ComboBoxStyle.DropDownList
        hour24.Text = ""
        minute.DropDownStyle = ComboBoxStyle.DropDownList
        minute.Text = ""

        facebookconnect.Visible = False
        facebookconnect.Navigate("about:blank")

        pan_sites.Visible = True
        pan_custom.Visible = False
        pan_apps.Visible = False
        Dim tab_custom_u As New Font(tab_custom.Font.Name, tab_custom.Font.Size, FontStyle.Underline)
        tab_custom.Font = tab_custom_u
        Dim tab_programs_u As New Font(tab_programs.Font.Name, tab_programs.Font.Size, FontStyle.Underline)
        tab_programs.Font = tab_programs_u
        Dim tab_sites_u As New Font(tab_sites.Font.Name, tab_sites.Font.Size, FontStyle.Regular)
        tab_sites.Font = tab_sites_u

        tab_sites.Cursor = Cursors.Arrow
        tab_custom.Cursor = Cursors.Hand
        tab_programs.Cursor = Cursors.Hand

        Try
            Dim reader As String
            reader = My.Computer.FileSystem.ReadAllText(install + "\data\custom")
            Dim strs() As String
            strs = Split(reader, Environment.NewLine)
            For Each line As String In strs
                If StrComp(line, "") <> 0 Then
                    list_cus.Items.Add(line)
                End If
            Next
        Catch
        End Try

        Try
            If File.Exists(install + "\data\sites") Then
                Dim sites As String = File.ReadAllText(install + "\data\sites")

                chk_facebook.Checked = False
                chk_myspace.Checked = False
                chk_twitter.Checked = False
                chk_agames.Checked = False
                chk_stumble.Checked = False
                chk_collegehumor.Checked = False
                chk_ebay.Checked = False
                chk_failblog.Checked = False
                chk_reddit.Checked = False
                chk_shotmail.Checked = False
                chk_youtube.Checked = False
                chk_warthree.Checked = False
                chk_wikipedia.Checked = False

                If sites.Contains("a") Then
                    chk_facebook.Checked = True
                End If
                If sites.Contains("b") Then
                    chk_myspace.Checked = True
                End If
                If sites.Contains("c") Then
                    chk_twitter.Checked = True
                End If
                If sites.Contains("d") Then
                    chk_agames.Checked = True
                End If
                If sites.Contains("e") Then
                    chk_stumble.Checked = True
                End If
                If sites.Contains("f") Then
                    chk_collegehumor.Checked = True
                End If
                If sites.Contains("g") Then
                    chk_ebay.Checked = True
                End If
                If sites.Contains("h") Then
                    chk_failblog.Checked = True
                End If
                If sites.Contains("i") Then
                    chk_reddit.Checked = True
                End If
                If sites.Contains("j") Then
                    chk_shotmail.Checked = True
                End If
                If sites.Contains("k") Then
                    chk_youtube.Checked = True
                End If
                If sites.Contains("l") Then
                    chk_warthree.Checked = True
                End If
                If sites.Contains("m") Then
                    chk_wikipedia.Checked = True
                End If
                If sites.Contains("z") Then
                    chk_24.Checked = True
                End If

            End If
        Catch ex As Exception

        End Try

        importfile.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

    End Sub

    Function SecondsToText(ByVal Seconds) As String
        Dim bAddComma As Boolean
        Dim Result As String = ""
        Dim sTemp As String
        Dim days, hours, minutes As Integer

        If Seconds <= 0 Or Not IsNumeric(Seconds) Then
            SecondsToText = "0 seconds"
            Exit Function
        End If

        Seconds = Fix(Seconds)

        If Seconds >= 86400 Then
            days = Fix(Seconds / 86400)
        Else
            days = 0
        End If

        If Seconds - (days * 86400) >= 3600 Then
            hours = Fix((Seconds - (days * 86400)) / 3600)
        Else
            hours = 0
        End If

        If Seconds - (hours * 3600) - (days * 86400) >= 60 Then
            minutes = Fix((Seconds - (hours * 3600) - (days * 86400)) / 60)
        Else
            minutes = 0
        End If

        Seconds = Seconds - (minutes * 60) - (hours * 3600) - _
           (days * 86400)

        If Seconds > 0 Then Result = Seconds & " second" & AutoS(Seconds)

        If minutes > 0 Then
            bAddComma = Result <> ""

            sTemp = minutes & " minute" & AutoS(minutes)
            If bAddComma Then sTemp = sTemp & ", "
            Result = sTemp & Result
        End If

        If hours > 0 Then
            bAddComma = Result <> ""

            sTemp = hours & " hour" & AutoS(hours)
            If bAddComma Then sTemp = sTemp & ", "
            Result = sTemp & Result
        End If

        If days > 0 Then
            bAddComma = Result <> ""
            sTemp = days & " day" & AutoS(days)
            If bAddComma Then sTemp = sTemp & ", "
            Result = sTemp & Result
        End If

        SecondsToText = Result
    End Function

    Function SecondsToText2(ByVal Seconds) As String
        Dim bAddComma As Boolean
        Dim Result As String = ""
        Dim sTemp As String
        Dim days, hours, minutes As Integer

        Seconds = Fix(Seconds)

        If Seconds >= 86400 Then
            days = Fix(Seconds / 86400)
        Else
            days = 0
        End If

        If Seconds - (days * 86400) >= 3600 Then
            hours = Fix((Seconds - (days * 86400)) / 3600)
        Else
            hours = 0
        End If

        If Seconds - (hours * 3600) - (days * 86400) >= 60 Then
            minutes = Fix((Seconds - (hours * 3600) - (days * 86400)) / 60)
        Else
            minutes = 0
        End If

        Seconds = Seconds - (minutes * 60) - (hours * 3600) - _
           (days * 86400)

        If Seconds > 0 Then Result = ""

        If minutes > 0 Then
            bAddComma = Result <> ""
            If days = 0 Then
                sTemp = minutes & " minute" & AutoS(minutes)
                If bAddComma Then sTemp = sTemp & ", "
                Result = sTemp & Result
            End If

        End If

        If hours > 0 Then
            bAddComma = Result <> ""

            sTemp = hours & " hour" & AutoS(hours)
            If bAddComma Then sTemp = sTemp & ", "
            Result = sTemp & Result
        End If

        If days > 0 Then
            bAddComma = Result <> ""
            sTemp = days & " day" & AutoS(days)
            If bAddComma Then sTemp = sTemp & ", "
            Result = sTemp & Result
        End If

        SecondsToText2 = Result
    End Function

    Function AutoS(ByVal Number)
        If Number = 1 Then AutoS = "" Else AutoS = "s"
    End Function

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If updatestatus.Enabled = True Then
            If updatestatus.Checked = False Then
                updatestatus.Checked = True
            Else
                updatestatus.Checked = False
            End If
        End If
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If srvcont.Status = ServiceProcess.ServiceControllerStatus.Running Then
                MsgBox("Please wait until your current ban is lifted.", MsgBoxStyle.Information, "Cold Turkey")
                End
            End If
        Catch f As Exception
        End Try

        If File.Exists(install + "\data\sites") Then
            File.Delete(install + "\data\sites")
        End If

        If chk_facebook.Checked = True Then
            File.AppendAllText(install + "\data\sites", "a")
        End If
        If chk_myspace.Checked = True Then
            File.AppendAllText(install + "\data\sites", "b")
        End If
        If chk_twitter.Checked = True Then
            File.AppendAllText(install + "\data\sites", "c")
        End If
        If chk_agames.Checked = True Then
            File.AppendAllText(install + "\data\sites", "d")
        End If
        If chk_stumble.Checked = True Then
            File.AppendAllText(install + "\data\sites", "e")
        End If
        If chk_collegehumor.Checked = True Then
            File.AppendAllText(install + "\data\sites", "f")
        End If
        If chk_ebay.Checked = True Then
            File.AppendAllText(install + "\data\sites", "g")
        End If
        If chk_failblog.Checked = True Then
            File.AppendAllText(install + "\data\sites", "h")
        End If
        If chk_reddit.Checked = True Then
            File.AppendAllText(install + "\data\sites", "i")
        End If
        If chk_shotmail.Checked = True Then
            File.AppendAllText(install + "\data\sites", "j")
        End If
        If chk_youtube.Checked = True Then
            File.AppendAllText(install + "\data\sites", "k")
        End If
        If chk_warthree.Checked = True Then
            File.AppendAllText(install + "\data\sites", "l")
        End If
        If chk_wikipedia.Checked = True Then
            File.AppendAllText(install + "\data\sites", "m")
        End If
        If chk_24.Checked = True Then
            File.AppendAllText(install + "\data\sites", "z")
        End If

        If chk_facebook.Checked = False And chk_myspace.Checked = False _
            And chk_twitter.Checked = False And chk_agames.Checked = False _
            And chk_stumble.Checked = False And chk_collegehumor.Checked = False And chk_ebay.Checked = False _
            And chk_failblog.Checked = False And chk_reddit.Checked = False And chk_shotmail.Checked = False _
            And chk_youtube.Checked = False And chk_warthree.Checked = False _
            And chk_wikipedia.Checked = False And list_cus.Items.Count = 0 Then
            MsgBox("Common, you need the balls to block at least one thing!")
        Else
            If (hour.Text = "" Or minute.Text = "" Or mornin.Text = "") And (hour24.Text = "" Or minute.Text = "") Then
                MsgBox("You didn't give me a valid time. You don't want to be blocked forever, do you?")
            Else
                timecheck()
            End If
        End If

    End Sub

    Private Sub timecheck()

        Dim CurrentDate, SelectDate As Integer

        CurrentDate = Val(Microsoft.VisualBasic.DateAndTime.Day(Now))
        SelectDate = Val(Microsoft.VisualBasic.Left(Format(DateTimePicker1.Value, "dd/MM/yyyy"), 2))

        If chk_24.Checked = False Then
            If StrComp(mornin.Text, "PM", vbTextCompare) = 0 And Val(hour.Text) >= 1 And Val(hour.Text) <= 11 Then
                actualhour = Val(hour.Text) + 12
            ElseIf Val(hour.Text) = 12 And StrComp(mornin.Text, "PM", vbTextCompare) <> 0 Then
                actualhour = 0
            Else
                actualhour = Val(hour.Text)
            End If
        Else
            actualhour = Val(hour24.Text)
        End If


        If CurrentDate <> SelectDate Then
            showsites()
        Else
            If Val(TimeOfDay.Hour) = actualhour Then
                If Val(TimeOfDay.Minute) >= Val(minute.Text) Then
                    MsgBox("Haha, nice try... but I can't go back in time!")
                Else
                    showsites()
                End If
            ElseIf Val(TimeOfDay.Hour) > actualhour Then
                MsgBox("Haha, nice try... but I can't go back in time!")
            Else
                showsites()
            End If

        End If
    End Sub

    Private Sub showsites()

        lastchance.full_list.Clear()
        If chk_facebook.Checked = True Then
            lastchance.full_list.Items.Add("Facebook")
        End If
        If chk_myspace.Checked = True Then
            lastchance.full_list.Items.Add("Myspace")
        End If

        If chk_twitter.Checked = True Then
            lastchance.full_list.Items.Add("Twitter")
        End If

        If chk_youtube.Checked = True Then
            Dim a As New System.Windows.Forms.ListViewItem("YouTube")
            a.ForeColor = Color.Purple
            lastchance.full_list.Items.Add(a)
        End If

        If chk_shotmail.Checked = True Then
            Dim a As New System.Windows.Forms.ListViewItem("Hotmail")
            a.ForeColor = Color.Purple
            lastchance.full_list.Items.Add(a)
            Dim b = New System.Windows.Forms.ListViewItem("MSN / Windows Live")
            b.ForeColor = Color.Purple
            lastchance.full_list.Items.Add(b)
        End If

        If chk_agames.Checked = True Then
            lastchance.full_list.Items.Add("AddictingGames")
        End If

        If chk_collegehumor.Checked = True Then
            lastchance.full_list.Items.Add("CollegeHumor")
        End If

        If chk_stumble.Checked = True Then
            lastchance.full_list.Items.Add("StumbleUpon")
        End If

        If chk_ebay.Checked = True Then
            Dim a As New System.Windows.Forms.ListViewItem("Ebay")
            a.ForeColor = Color.Purple
            lastchance.full_list.Items.Add(a)
        End If

        If chk_failblog.Checked = True Then
            lastchance.full_list.Items.Add("FailBlog.org")
        End If

        If chk_reddit.Checked = True Then
            lastchance.full_list.Items.Add("Reddit")
        End If

        If chk_wikipedia.Checked = True Then
            Dim a As New System.Windows.Forms.ListViewItem("Wikipedia")
            a.ForeColor = Color.Purple
            lastchance.full_list.Items.Add(a)
        End If

        For Each itm As String In list_cus.Items
            lastchance.full_list.Items.Add(itm)
        Next

        Dim response As DialogResult
        Me.Hide()
        response = lastchance.ShowDialog

        If response = DialogResult.OK Then   ' User chose Yes.
            updatefacebook()
        Else
            Me.Show()
        End If

    End Sub


    Private Sub updatefacebook()
        If updatestatus.Checked = True Then
            Form2.ShowDialog()
            Exitme()
        Else
            Exitme()
        End If
    End Sub

    Private Sub Exitme()

        Dim msgout, install As String
        Dim secondsout As Integer

        If DateAndTime.Month(Now) <> DateTimePicker1.Value.Month Then
            secondsout = ((Day(DateSerial(DateTimePicker1.Value.Year, DateAndTime.Month(Now) + 1, 0)) - DateAndTime.Day(Now)) + DateTimePicker1.Value.Day) * 86400
            If TimeOfDay.Hour < actualhour Then
                secondsout = secondsout + ((actualhour - TimeOfDay.Hour) * 60 * 60)
                If TimeOfDay.Minute < Val(minute.Text) Then
                    secondsout = secondsout + ((Val(minute.Text) - TimeOfDay.Minute) * 60)
                ElseIf TimeOfDay.Minute > Val(minute.Text) Then
                    secondsout = secondsout - ((TimeOfDay.Minute - (Val(minute.Text))) * 60)
                End If
            ElseIf TimeOfDay.Hour = actualhour Then
                If TimeOfDay.Minute < Val(minute.Text) Then
                    secondsout = secondsout + ((Val(minute.Text) - TimeOfDay.Minute) * 60)
                ElseIf TimeOfDay.Minute > Val(minute.Text) Then
                    secondsout = secondsout - ((TimeOfDay.Minute - Val(minute.Text)) * 60)
                End If
            ElseIf TimeOfDay.Hour > actualhour Then
                secondsout = secondsout - ((TimeOfDay.Hour - actualhour) * 60 * 60)
                If TimeOfDay.Minute < Val(minute.Text) Then
                    secondsout = secondsout + ((Val(minute.Text) - TimeOfDay.Minute) * 60)
                ElseIf TimeOfDay.Minute > Val(minute.Text) Then
                    secondsout = secondsout - ((TimeOfDay.Minute - (Val(minute.Text))) * 60)
                End If
            End If

        ElseIf DateAndTime.Month(Now) = DateTimePicker1.Value.Month Then
            secondsout = (DateTimePicker1.Value.Day - DateAndTime.Day(Now)) * 86400
            If TimeOfDay.Hour < actualhour Then
                secondsout = secondsout + ((actualhour - TimeOfDay.Hour) * 60 * 60)
                If TimeOfDay.Minute < Val(minute.Text) Then
                    secondsout = secondsout + ((Val(minute.Text) - TimeOfDay.Minute) * 60)
                ElseIf TimeOfDay.Minute > Val(minute.Text) Then
                    secondsout = secondsout - ((TimeOfDay.Minute - (Val(minute.Text))) * 60)
                End If
            ElseIf TimeOfDay.Hour = actualhour Then
                If TimeOfDay.Minute < Val(minute.Text) Then
                    secondsout = secondsout + ((Val(minute.Text) - TimeOfDay.Minute) * 60)
                ElseIf TimeOfDay.Minute > Val(minute.Text) Then
                    secondsout = secondsout - ((TimeOfDay.Minute - Val(minute.Text)) * 60)
                End If
            ElseIf TimeOfDay.Hour > actualhour Then
                secondsout = secondsout - ((TimeOfDay.Hour - actualhour) * 60 * 60)
                If TimeOfDay.Minute < Val(minute.Text) Then
                    secondsout = secondsout + ((Val(minute.Text) - TimeOfDay.Minute) * 60)
                ElseIf TimeOfDay.Minute > Val(minute.Text) Then
                    secondsout = secondsout - ((TimeOfDay.Minute - (Val(minute.Text))) * 60)
                End If
            End If
        End If
        secondsout = secondsout - (TimeOfDay.Second)

        If My.Computer.FileSystem.FileExists(shostlocation) Then
            SetAttr(shostlocation, vbNormal)
        End If
        Dim fs As FileStream
        Dim sw As StreamWriter
        Try
            fs = New FileStream(shostlocation, FileMode.Append, FileAccess.Write, FileShare.Read)
            sw = New StreamWriter(fs)
            sw.WriteLine("")
            sw.WriteLine("#### ColdTurkey Entries ####")
            If chk_facebook.Checked = True Then
                sw.WriteLine("0.0.0.0 facebook.com")
                sw.WriteLine("0.0.0.0 www.facebook.com")
                sw.WriteLine("0.0.0.0 ah8.facebook.com")
                sw.WriteLine("0.0.0.0 api.connect.facebook.com")
                sw.WriteLine("0.0.0.0 apps.facebook.com")
                sw.WriteLine("0.0.0.0 apps.new.facebook.com")
                sw.WriteLine("0.0.0.0 badge.facebook.com")
                sw.WriteLine("0.0.0.0 blog.facebook.com")
                sw.WriteLine("0.0.0.0 ck.connect.facebook.com")
                sw.WriteLine("0.0.0.0 connect.facebook.com")
                sw.WriteLine("0.0.0.0 da-dk.facebook.com")
                sw.WriteLine("0.0.0.0 de-de.facebook.com")
                sw.WriteLine("0.0.0.0 de.facebook.com")
                sw.WriteLine("0.0.0.0 depauw.facebook.com")
                sw.WriteLine("0.0.0.0 developer.facebook.com")
                sw.WriteLine("0.0.0.0 developers.facebook.com")
                sw.WriteLine("0.0.0.0 el-gr.facebook.com")
                sw.WriteLine("0.0.0.0 en-gb.facebook.com")
                sw.WriteLine("0.0.0.0 en-us.facebook.com")
                sw.WriteLine("0.0.0.0 es-es.facebook.com")
                sw.WriteLine("0.0.0.0 es-la.facebook.com")
                sw.WriteLine("0.0.0.0 fr-fr.facebook.com")
                sw.WriteLine("0.0.0.0 fr.facebook.com")
                sw.WriteLine("0.0.0.0 fsu.facebook.com")
                sw.WriteLine("0.0.0.0 hs.facebook.com")
                sw.WriteLine("0.0.0.0 hy-am.facebook.com")
                sw.WriteLine("0.0.0.0 iphone.facebook.com")
                sw.WriteLine("0.0.0.0 it-it.facebook.com")
                sw.WriteLine("0.0.0.0 ja-jp.facebook.com")
                sw.WriteLine("0.0.0.0 lite.facebook.com")
                sw.WriteLine("0.0.0.0 login.facebook.com")
                sw.WriteLine("0.0.0.0 m.facebook.com")
                sw.WriteLine("0.0.0.0 new.facebook.com")
                sw.WriteLine("0.0.0.0 nn-no.facebook.com")
                sw.WriteLine("0.0.0.0 northland.facebook.com")
                sw.WriteLine("0.0.0.0 nyu.facebook.com")
                sw.WriteLine("0.0.0.0 photos-a.ak.facebook.com")
                sw.WriteLine("0.0.0.0 photos-b.ak.facebook.com")
                sw.WriteLine("0.0.0.0 photos-b.ll.facebook.com")
                sw.WriteLine("0.0.0.0 photos-f.ak.facebook.com")
                sw.WriteLine("0.0.0.0 photos-g.ak.facebook.com")
                sw.WriteLine("0.0.0.0 presby.facebook.com")
                sw.WriteLine("0.0.0.0 profile.ak.facebook.com")
                sw.WriteLine("0.0.0.0 pt-br.facebook.com")
                sw.WriteLine("0.0.0.0 pt-pt.facebook.com")
                sw.WriteLine("0.0.0.0 ru-ru.facebook.com")
                sw.WriteLine("0.0.0.0 static.ak.connect.facebook.com")
                sw.WriteLine("0.0.0.0 static.ak.facebook.com")
                sw.WriteLine("0.0.0.0 static.new.facebook.com")
                sw.WriteLine("0.0.0.0 sv-se.facebook.com")
                sw.WriteLine("0.0.0.0 te-in.facebook.com")
                sw.WriteLine("0.0.0.0 th-th.facebook.com")
                sw.WriteLine("0.0.0.0 touch.facebook.com")
                sw.WriteLine("0.0.0.0 tr-tr.facebook.com")
                sw.WriteLine("0.0.0.0 ufl.facebook.com")
                sw.WriteLine("0.0.0.0 uillinois.facebook.com")
                sw.WriteLine("0.0.0.0 uppsalauni.facebook.com")
                sw.WriteLine("0.0.0.0 video.ak.facebook.com")
                sw.WriteLine("0.0.0.0 vthumb.ak.facebook.com")
                sw.WriteLine("0.0.0.0 wiki.developers.facebook.com")
                sw.WriteLine("0.0.0.0 wm.facebook.com")
                sw.WriteLine("0.0.0.0 ww.facebook.com")
                sw.WriteLine("0.0.0.0 zh-cn.facebook.com")

                sw.WriteLine("0.0.0.0 www.ah8.facebook.com")
                sw.WriteLine("0.0.0.0 www.api.connect.facebook.com")
                sw.WriteLine("0.0.0.0 www.apps.facebook.com")
                sw.WriteLine("0.0.0.0 www.apps.new.facebook.com")
                sw.WriteLine("0.0.0.0 www.badge.facebook.com")
                sw.WriteLine("0.0.0.0 www.blog.facebook.com")
                sw.WriteLine("0.0.0.0 www.ck.connect.facebook.com")
                sw.WriteLine("0.0.0.0 www.connect.facebook.com")
                sw.WriteLine("0.0.0.0 www.da-dk.facebook.com")
                sw.WriteLine("0.0.0.0 www.de-de.facebook.com")
                sw.WriteLine("0.0.0.0 www.de.facebook.com")
                sw.WriteLine("0.0.0.0 www.depauw.facebook.com")
                sw.WriteLine("0.0.0.0 www.developer.facebook.com")
                sw.WriteLine("0.0.0.0 www.developers.facebook.com")
                sw.WriteLine("0.0.0.0 www.el-gr.facebook.com")
                sw.WriteLine("0.0.0.0 www.en-gb.facebook.com")
                sw.WriteLine("0.0.0.0 www.en-us.facebook.com")
                sw.WriteLine("0.0.0.0 www.es-es.facebook.com")
                sw.WriteLine("0.0.0.0 www.es-la.facebook.com")
                sw.WriteLine("0.0.0.0 www.fr-fr.facebook.com")
                sw.WriteLine("0.0.0.0 www.fr.facebook.com")
                sw.WriteLine("0.0.0.0 www.fsu.facebook.com")
                sw.WriteLine("0.0.0.0 www.hs.facebook.com")
                sw.WriteLine("0.0.0.0 www.hy-am.facebook.com")
                sw.WriteLine("0.0.0.0 www.iphone.facebook.com")
                sw.WriteLine("0.0.0.0 www.it-it.facebook.com")
                sw.WriteLine("0.0.0.0 www.ja-jp.facebook.com")
                sw.WriteLine("0.0.0.0 www.lite.facebook.com")
                sw.WriteLine("0.0.0.0 www.login.facebook.com")
                sw.WriteLine("0.0.0.0 www.m.facebook.com")
                sw.WriteLine("0.0.0.0 www.new.facebook.com")
                sw.WriteLine("0.0.0.0 www.nn-no.facebook.com")
                sw.WriteLine("0.0.0.0 www.northland.facebook.com")
                sw.WriteLine("0.0.0.0 www.nyu.facebook.com")
                sw.WriteLine("0.0.0.0 www.photos-a.ak.facebook.com")
                sw.WriteLine("0.0.0.0 www.photos-b.ak.facebook.com")
                sw.WriteLine("0.0.0.0 www.photos-b.ll.facebook.com")
                sw.WriteLine("0.0.0.0 www.photos-f.ak.facebook.com")
                sw.WriteLine("0.0.0.0 www.photos-g.ak.facebook.com")
                sw.WriteLine("0.0.0.0 www.presby.facebook.com")
                sw.WriteLine("0.0.0.0 www.profile.ak.facebook.com")
                sw.WriteLine("0.0.0.0 www.pt-br.facebook.com")
                sw.WriteLine("0.0.0.0 www.pt-pt.facebook.com")
                sw.WriteLine("0.0.0.0 www.ru-ru.facebook.com")
                sw.WriteLine("0.0.0.0 www.static.ak.connect.facebook.com")
                sw.WriteLine("0.0.0.0 www.static.ak.facebook.com")
                sw.WriteLine("0.0.0.0 www.static.new.facebook.com")
                sw.WriteLine("0.0.0.0 www.sv-se.facebook.com")
                sw.WriteLine("0.0.0.0 www.te-in.facebook.com")
                sw.WriteLine("0.0.0.0 www.th-th.facebook.com")
                sw.WriteLine("0.0.0.0 www.touch.facebook.com")
                sw.WriteLine("0.0.0.0 www.tr-tr.facebook.com")
                sw.WriteLine("0.0.0.0 www.ufl.facebook.com")
                sw.WriteLine("0.0.0.0 www.uillinois.facebook.com")
                sw.WriteLine("0.0.0.0 www.uppsalauni.facebook.com")
                sw.WriteLine("0.0.0.0 www.video.ak.facebook.com")
                sw.WriteLine("0.0.0.0 www.vthumb.ak.facebook.com")
                sw.WriteLine("0.0.0.0 www.wiki.developers.facebook.com")
                sw.WriteLine("0.0.0.0 www.wm.facebook.com")
                sw.WriteLine("0.0.0.0 www.ww.facebook.com")
                sw.WriteLine("0.0.0.0 www.zh-cn.facebook.com")

            End If
            If chk_myspace.Checked = True Then
                sw.WriteLine("0.0.0.0 myspace.com")
                sw.WriteLine("0.0.0.0 www.myspace.com")
                sw.WriteLine("0.0.0.0 api.myspace.com")
                sw.WriteLine("0.0.0.0 ar.myspace.com")
                sw.WriteLine("0.0.0.0 at.myspace.com")
                sw.WriteLine("0.0.0.0 au.myspace.com")
                sw.WriteLine("0.0.0.0 b.myspace.com")
                sw.WriteLine("0.0.0.0 belgie.myspace.com")
                sw.WriteLine("0.0.0.0 belgique.myspace.com")
                sw.WriteLine("0.0.0.0 blog.myspace.com")
                sw.WriteLine("0.0.0.0 blogs.myspace.com")
                sw.WriteLine("0.0.0.0 br.myspace.com")
                sw.WriteLine("0.0.0.0 browseusers.myspace.com")
                sw.WriteLine("0.0.0.0 bulletins.myspace.com")
                sw.WriteLine("0.0.0.0 ca.myspace.com")
                sw.WriteLine("0.0.0.0 cf.myspace.com")
                sw.WriteLine("0.0.0.0 collect.myspace.com")
                sw.WriteLine("0.0.0.0 comment.myspace.com")
                sw.WriteLine("0.0.0.0 cp.myspace.com")
                sw.WriteLine("0.0.0.0 creative-origin.myspace.com")
                sw.WriteLine("0.0.0.0 de.myspace.com")
                sw.WriteLine("0.0.0.0 dk.myspace.com")
                sw.WriteLine("0.0.0.0 es.myspace.com")
                sw.WriteLine("0.0.0.0 events.myspace.com")
                sw.WriteLine("0.0.0.0 fi.myspace.com")
                sw.WriteLine("0.0.0.0 forum.myspace.com")
                sw.WriteLine("0.0.0.0 forums.myspace.com")
                sw.WriteLine("0.0.0.0 fr.myspace.com")
                sw.WriteLine("0.0.0.0 friends.myspace.com")
                sw.WriteLine("0.0.0.0 groups.myspace.com")
                sw.WriteLine("0.0.0.0 home.myspace.com")
                sw.WriteLine("0.0.0.0 ie.myspace.com")
                sw.WriteLine("0.0.0.0 in.myspace.com")
                sw.WriteLine("0.0.0.0 invites.myspace.com")
                sw.WriteLine("0.0.0.0 it.myspace.com")
                sw.WriteLine("0.0.0.0 jobs.myspace.com")
                sw.WriteLine("0.0.0.0 jp.myspace.com")
                sw.WriteLine("0.0.0.0 ksolo.myspace.com")
                sw.WriteLine("0.0.0.0 la.myspace.com")
                sw.WriteLine("0.0.0.0 lads.myspace.com")
                sw.WriteLine("0.0.0.0 latino.myspace.com")
                sw.WriteLine("0.0.0.0 m.myspace.com")
                sw.WriteLine("0.0.0.0 mediaservices.myspace.com")
                sw.WriteLine("0.0.0.0 messaging.myspace.com")
                sw.WriteLine("0.0.0.0 music.myspace.com")
                sw.WriteLine("0.0.0.0 mx.myspace.com")
                sw.WriteLine("0.0.0.0 nl.myspace.com")
                sw.WriteLine("0.0.0.0 no.myspace.com")
                sw.WriteLine("0.0.0.0 nz.myspace.com")
                sw.WriteLine("0.0.0.0 pl.myspace.com")
                sw.WriteLine("0.0.0.0 profile.myspace.com")
                sw.WriteLine("0.0.0.0 profileedit.myspace.com")
                sw.WriteLine("0.0.0.0 pt.myspace.com")
                sw.WriteLine("0.0.0.0 ru.myspace.com")
                sw.WriteLine("0.0.0.0 school.myspace.com")
                sw.WriteLine("0.0.0.0 schweiz.myspace.com")
                sw.WriteLine("0.0.0.0 se.myspace.com")
                sw.WriteLine("0.0.0.0 searchservice.myspace.com")
                sw.WriteLine("0.0.0.0 secure.myspace.com")
                sw.WriteLine("0.0.0.0 signups.myspace.com")
                sw.WriteLine("0.0.0.0 suisse.myspace.com")
                sw.WriteLine("0.0.0.0 svizzera.myspace.com")
                sw.WriteLine("0.0.0.0 tr.myspace.com")
                sw.WriteLine("0.0.0.0 uk.myspace.com")
                sw.WriteLine("0.0.0.0 us.myspace.com")
                sw.WriteLine("0.0.0.0 vids.myspace.com")
                sw.WriteLine("0.0.0.0 viewmorepics.myspace.com")
                sw.WriteLine("0.0.0.0 zh.myspace.com")

                sw.WriteLine("0.0.0.0 www.api.myspace.com")
                sw.WriteLine("0.0.0.0 www.ar.myspace.com")
                sw.WriteLine("0.0.0.0 www.at.myspace.com")
                sw.WriteLine("0.0.0.0 www.au.myspace.com")
                sw.WriteLine("0.0.0.0 www.b.myspace.com")
                sw.WriteLine("0.0.0.0 www.belgie.myspace.com")
                sw.WriteLine("0.0.0.0 www.belgique.myspace.com")
                sw.WriteLine("0.0.0.0 www.blog.myspace.com")
                sw.WriteLine("0.0.0.0 www.blogs.myspace.com")
                sw.WriteLine("0.0.0.0 www.br.myspace.com")
                sw.WriteLine("0.0.0.0 www.browseusers.myspace.com")
                sw.WriteLine("0.0.0.0 www.bulletins.myspace.com")
                sw.WriteLine("0.0.0.0 www.ca.myspace.com")
                sw.WriteLine("0.0.0.0 www.cf.myspace.com")
                sw.WriteLine("0.0.0.0 www.collect.myspace.com")
                sw.WriteLine("0.0.0.0 www.comment.myspace.com")
                sw.WriteLine("0.0.0.0 www.cp.myspace.com")
                sw.WriteLine("0.0.0.0 www.creative-origin.myspace.com")
                sw.WriteLine("0.0.0.0 www.de.myspace.com")
                sw.WriteLine("0.0.0.0 www.dk.myspace.com")
                sw.WriteLine("0.0.0.0 www.es.myspace.com")
                sw.WriteLine("0.0.0.0 www.events.myspace.com")
                sw.WriteLine("0.0.0.0 www.fi.myspace.com")
                sw.WriteLine("0.0.0.0 www.forum.myspace.com")
                sw.WriteLine("0.0.0.0 www.forums.myspace.com")
                sw.WriteLine("0.0.0.0 www.fr.myspace.com")
                sw.WriteLine("0.0.0.0 www.friends.myspace.com")
                sw.WriteLine("0.0.0.0 www.groups.myspace.com")
                sw.WriteLine("0.0.0.0 www.home.myspace.com")
                sw.WriteLine("0.0.0.0 www.ie.myspace.com")
                sw.WriteLine("0.0.0.0 www.in.myspace.com")
                sw.WriteLine("0.0.0.0 www.invites.myspace.com")
                sw.WriteLine("0.0.0.0 www.it.myspace.com")
                sw.WriteLine("0.0.0.0 www.jobs.myspace.com")
                sw.WriteLine("0.0.0.0 www.jp.myspace.com")
                sw.WriteLine("0.0.0.0 www.ksolo.myspace.com")
                sw.WriteLine("0.0.0.0 www.la.myspace.com")
                sw.WriteLine("0.0.0.0 www.lads.myspace.com")
                sw.WriteLine("0.0.0.0 www.latino.myspace.com")
                sw.WriteLine("0.0.0.0 www.m.myspace.com")
                sw.WriteLine("0.0.0.0 www.mediaservices.myspace.com")
                sw.WriteLine("0.0.0.0 www.messaging.myspace.com")
                sw.WriteLine("0.0.0.0 www.music.myspace.com")
                sw.WriteLine("0.0.0.0 www.mx.myspace.com")
                sw.WriteLine("0.0.0.0 www.nl.myspace.com")
                sw.WriteLine("0.0.0.0 www.no.myspace.com")
                sw.WriteLine("0.0.0.0 www.nz.myspace.com")
                sw.WriteLine("0.0.0.0 www.pl.myspace.com")
                sw.WriteLine("0.0.0.0 www.profile.myspace.com")
                sw.WriteLine("0.0.0.0 www.profileedit.myspace.com")
                sw.WriteLine("0.0.0.0 www.pt.myspace.com")
                sw.WriteLine("0.0.0.0 www.ru.myspace.com")
                sw.WriteLine("0.0.0.0 www.school.myspace.com")
                sw.WriteLine("0.0.0.0 www.schweiz.myspace.com")
                sw.WriteLine("0.0.0.0 www.se.myspace.com")
                sw.WriteLine("0.0.0.0 www.searchservice.myspace.com")
                sw.WriteLine("0.0.0.0 www.secure.myspace.com")
                sw.WriteLine("0.0.0.0 www.signups.myspace.com")
                sw.WriteLine("0.0.0.0 www.suisse.myspace.com")
                sw.WriteLine("0.0.0.0 www.svizzera.myspace.com")
                sw.WriteLine("0.0.0.0 www.tr.myspace.com")
                sw.WriteLine("0.0.0.0 www.uk.myspace.com")
                sw.WriteLine("0.0.0.0 www.us.myspace.com")
                sw.WriteLine("0.0.0.0 www.vids.myspace.com")
                sw.WriteLine("0.0.0.0 www.viewmorepics.myspace.com")
                sw.WriteLine("0.0.0.0 www.zh.myspace.com")
            End If
            If chk_twitter.Checked = True Then
                sw.WriteLine("0.0.0.0 twitter.com")
                sw.WriteLine("0.0.0.0 www.twitter.com")
                sw.WriteLine("0.0.0.0 apiwiki.twitter.com")
                sw.WriteLine("0.0.0.0 assets0.twitter.com")
                sw.WriteLine("0.0.0.0 assets3.twitter.com")
                sw.WriteLine("0.0.0.0 blog.fr.twitter.com")
                sw.WriteLine("0.0.0.0 blog.twitter.com")
                sw.WriteLine("0.0.0.0 business.twitter.com")
                sw.WriteLine("0.0.0.0 chirp.twitter.com")
                sw.WriteLine("0.0.0.0 dev.twitter.com")
                sw.WriteLine("0.0.0.0 explore.twitter.com")
                sw.WriteLine("0.0.0.0 help.twitter.com")
                sw.WriteLine("0.0.0.0 integratedsearch.twitter.com")
                sw.WriteLine("0.0.0.0 m.twitter.com")
                sw.WriteLine("0.0.0.0 mobile.twitter.com")
                sw.WriteLine("0.0.0.0 mobile.blog.twitter.com")
                sw.WriteLine("0.0.0.0 platform.twitter.com")
                sw.WriteLine("0.0.0.0 search.twitter.com")
                sw.WriteLine("0.0.0.0 static.twitter.com")
                sw.WriteLine("0.0.0.0 status.twitter.com")

                sw.WriteLine("0.0.0.0 www.apiwiki.twitter.com")
                sw.WriteLine("0.0.0.0 www.assets0.twitter.com")
                sw.WriteLine("0.0.0.0 www.assets3.twitter.com")
                sw.WriteLine("0.0.0.0 www.blog.fr.twitter.com")
                sw.WriteLine("0.0.0.0 www.blog.twitter.com")
                sw.WriteLine("0.0.0.0 www.business.twitter.com")
                sw.WriteLine("0.0.0.0 www.chirp.twitter.com")
                sw.WriteLine("0.0.0.0 www.dev.twitter.com")
                sw.WriteLine("0.0.0.0 www.explore.twitter.com")
                sw.WriteLine("0.0.0.0 www.help.twitter.com")
                sw.WriteLine("0.0.0.0 www.integratedsearch.twitter.com")
                sw.WriteLine("0.0.0.0 www.m.twitter.com")
                sw.WriteLine("0.0.0.0 www.mobile.twitter.com")
                sw.WriteLine("0.0.0.0 www.mobile.blog.twitter.com")
                sw.WriteLine("0.0.0.0 www.platform.twitter.com")
                sw.WriteLine("0.0.0.0 www.search.twitter.com")
                sw.WriteLine("0.0.0.0 www.static.twitter.com")
                sw.WriteLine("0.0.0.0 www.status.twitter.com")
            End If
            If chk_youtube.Checked = True Then
                sw.WriteLine("0.0.0.0 youtube.com")
                sw.WriteLine("0.0.0.0 www.youtube.com")
                sw.WriteLine("0.0.0.0 apiblog.youtube.com")
                sw.WriteLine("0.0.0.0 au.youtube.com")
                sw.WriteLine("0.0.0.0 ca.youtube.com")
                sw.WriteLine("0.0.0.0 de.youtube.com")
                sw.WriteLine("0.0.0.0 es.youtube.com")
                sw.WriteLine("0.0.0.0 fr.youtube.com")
                sw.WriteLine("0.0.0.0 gdata.youtube.com")
                sw.WriteLine("0.0.0.0 help.youtube.com")
                sw.WriteLine("0.0.0.0 hk.youtube.com")
                sw.WriteLine("0.0.0.0 img.youtube.com")
                sw.WriteLine("0.0.0.0 in.youtube.com")
                sw.WriteLine("0.0.0.0 it.youtube.com")
                sw.WriteLine("0.0.0.0 jp.youtube.com")
                sw.WriteLine("0.0.0.0 m.youtube.com")
                sw.WriteLine("0.0.0.0 nl.youtube.com")
                sw.WriteLine("0.0.0.0 nz.youtube.com")
                sw.WriteLine("0.0.0.0 sjc-static7.sjc.youtube.com")
                sw.WriteLine("0.0.0.0 sjl-static16.sjl.youtube.com")
                sw.WriteLine("0.0.0.0 uk.youtube.com")
                sw.WriteLine("0.0.0.0 upload.youtube.com")
                sw.WriteLine("0.0.0.0 web1.nyc.youtube.com")

                sw.WriteLine("0.0.0.0 www.apiblog.youtube.com")
                sw.WriteLine("0.0.0.0 www.au.youtube.com")
                sw.WriteLine("0.0.0.0 www.ca.youtube.com")
                sw.WriteLine("0.0.0.0 www.de.youtube.com")
                sw.WriteLine("0.0.0.0 www.es.youtube.com")
                sw.WriteLine("0.0.0.0 www.fr.youtube.com")
                sw.WriteLine("0.0.0.0 www.gdata.youtube.com")
                sw.WriteLine("0.0.0.0 www.help.youtube.com")
                sw.WriteLine("0.0.0.0 www.hk.youtube.com")
                sw.WriteLine("0.0.0.0 www.img.youtube.com")
                sw.WriteLine("0.0.0.0 www.in.youtube.com")
                sw.WriteLine("0.0.0.0 www.it.youtube.com")
                sw.WriteLine("0.0.0.0 www.jp.youtube.com")
                sw.WriteLine("0.0.0.0 www.m.youtube.com")
                sw.WriteLine("0.0.0.0 www.nl.youtube.com")
                sw.WriteLine("0.0.0.0 www.nz.youtube.com")
                sw.WriteLine("0.0.0.0 www.sjc-static7.sjc.youtube.com")
                sw.WriteLine("0.0.0.0 www.sjl-static16.sjl.youtube.com")
                sw.WriteLine("0.0.0.0 www.uk.youtube.com")
                sw.WriteLine("0.0.0.0 www.upload.youtube.com")
                sw.WriteLine("0.0.0.0 www.web1.nyc.youtube.com")

            End If
            If chk_shotmail.Checked = True Then
                sw.WriteLine("0.0.0.0 live.com")
                sw.WriteLine("0.0.0.0 www.live.com")
                sw.WriteLine("0.0.0.0 webim.live.sg")
                sw.WriteLine("0.0.0.0 www.webim.live.sg")
                sw.WriteLine("0.0.0.0 e-messenger.net")
                sw.WriteLine("0.0.0.0 www.e-messenger.net")
                sw.WriteLine("0.0.0.0 iloveim.com")
                sw.WriteLine("0.0.0.0 www.iloveim.com")
                sw.WriteLine("0.0.0.0 meebo.com")
                sw.WriteLine("0.0.0.0 www.meebo.com")
                sw.WriteLine("0.0.0.0 messengerfx.com")
                sw.WriteLine("0.0.0.0 www.messengerfx.com")
                sw.WriteLine("0.0.0.0 ebuddy.com")
                sw.WriteLine("0.0.0.0 www.ebuddy.com")
                sw.WriteLine("0.0.0.0 web-messenger.eu")
                sw.WriteLine("0.0.0.0 www.web-messenger.eu")
                sw.WriteLine("0.0.0.0 msn2go.com")
                sw.WriteLine("0.0.0.0 www.msn2go.com")
                sw.WriteLine("0.0.0.0 f1messenger.com")
                sw.WriteLine("0.0.0.0 www.f1messenger.com")

                sw.WriteLine("0.0.0.0 favorites.live.com")
                sw.WriteLine("0.0.0.0 g.live.com")
                sw.WriteLine("0.0.0.0 gallery.live.com")
                sw.WriteLine("0.0.0.0 get.live.com")
                sw.WriteLine("0.0.0.0 gfx2.mail.live.com")
                sw.WriteLine("0.0.0.0 groups.live.com")
                sw.WriteLine("0.0.0.0 home.live.com")
                sw.WriteLine("0.0.0.0 home.spaces.live.com")
                sw.WriteLine("0.0.0.0 hotmail.live.com")
                sw.WriteLine("0.0.0.0 ideas.live.com")
                sw.WriteLine("0.0.0.0 im.live.com")
                sw.WriteLine("0.0.0.0 images.domains.live.com")
                sw.WriteLine("0.0.0.0 intl.local.live.com")
                sw.WriteLine("0.0.0.0 local.live.com")
                sw.WriteLine("0.0.0.0 localsearch.live.com")
                sw.WriteLine("0.0.0.0 login.live.com")
                sw.WriteLine("0.0.0.0 lsrvsc.spaces.live.com")
                sw.WriteLine("0.0.0.0 mail.live.com")
                sw.WriteLine("0.0.0.0 messenger.live.com")
                sw.WriteLine("0.0.0.0 messenger.services.live.com")
                sw.WriteLine("0.0.0.0 mobile.live.com")
                sw.WriteLine("0.0.0.0 my.live.com")
                sw.WriteLine("0.0.0.0 people.live.com")
                sw.WriteLine("0.0.0.0 photo.live.com")
                sw.WriteLine("0.0.0.0 profile.live.com")
                sw.WriteLine("0.0.0.0 qna.live.com")
                sw.WriteLine("0.0.0.0 settings.messenger.live.com")
                sw.WriteLine("0.0.0.0 shared.live.com")
                sw.WriteLine("0.0.0.0 sn103w.snt103.mail.live.com")
                sw.WriteLine("0.0.0.0 sn105w.snt105.mail.live.com")
                sw.WriteLine("0.0.0.0 sn110w.snt110.mail.live.com")
                sw.WriteLine("0.0.0.0 spaces.live.com")
                sw.WriteLine("0.0.0.0 tou.live.com")

                sw.WriteLine("0.0.0.0 www.favorites.live.com")
                sw.WriteLine("0.0.0.0 www.g.live.com")
                sw.WriteLine("0.0.0.0 www.gallery.live.com")
                sw.WriteLine("0.0.0.0 www.get.live.com")
                sw.WriteLine("0.0.0.0 www.gfx2.mail.live.com")
                sw.WriteLine("0.0.0.0 www.groups.live.com")
                sw.WriteLine("0.0.0.0 www.home.live.com")
                sw.WriteLine("0.0.0.0 www.home.spaces.live.com")
                sw.WriteLine("0.0.0.0 www.hotmail.live.com")
                sw.WriteLine("0.0.0.0 www.ideas.live.com")
                sw.WriteLine("0.0.0.0 www.im.live.com")
                sw.WriteLine("0.0.0.0 www.images.domains.live.com")
                sw.WriteLine("0.0.0.0 www.intl.local.live.com")
                sw.WriteLine("0.0.0.0 www.local.live.com")
                sw.WriteLine("0.0.0.0 www.localsearch.live.com")
                sw.WriteLine("0.0.0.0 www.login.live.com")
                sw.WriteLine("0.0.0.0 www.lsrvsc.spaces.live.com")
                sw.WriteLine("0.0.0.0 www.mail.live.com")
                sw.WriteLine("0.0.0.0 www.messenger.live.com")
                sw.WriteLine("0.0.0.0 www.messenger.services.live.com")
                sw.WriteLine("0.0.0.0 www.mobile.live.com")
                sw.WriteLine("0.0.0.0 www.my.live.com")
                sw.WriteLine("0.0.0.0 www.people.live.com")
                sw.WriteLine("0.0.0.0 www.photo.live.com")
                sw.WriteLine("0.0.0.0 www.profile.live.com")
                sw.WriteLine("0.0.0.0 www.qna.live.com")
                sw.WriteLine("0.0.0.0 www.settings.messenger.live.com")
                sw.WriteLine("0.0.0.0 www.shared.live.com")
                sw.WriteLine("0.0.0.0 www.sn103w.snt103.mail.live.com")
                sw.WriteLine("0.0.0.0 www.sn105w.snt105.mail.live.com")
                sw.WriteLine("0.0.0.0 www.sn110w.snt110.mail.live.com")
                sw.WriteLine("0.0.0.0 www.spaces.live.com")
                sw.WriteLine("0.0.0.0 www.tou.live.com")
            End If
            If chk_agames.Checked = True Then
                sw.WriteLine("0.0.0.0 addictinggames.com")
                sw.WriteLine("0.0.0.0 www.addictinggames.com")
            End If
            If chk_collegehumor.Checked = True Then
                sw.WriteLine("0.0.0.0 collegehumor.com")
                sw.WriteLine("0.0.0.0 www.collegehumor.com")
            End If
            If chk_stumble.Checked = True Then
                sw.WriteLine("0.0.0.0 stumbleupon.com")
                sw.WriteLine("0.0.0.0 www.stumbleupon.com")
            End If
            If chk_ebay.Checked = True Then
                sw.WriteLine("0.0.0.0 ebay.com")
                sw.WriteLine("0.0.0.0 ebay.co.uk")
                sw.WriteLine("0.0.0.0 benl.ebay.be")
                sw.WriteLine("0.0.0.0 ebay.ca")
                sw.WriteLine("0.0.0.0 ebay.cn")
                sw.WriteLine("0.0.0.0 ebay.com.au")
                sw.WriteLine("0.0.0.0 ebay.com.cn")
                sw.WriteLine("0.0.0.0 ebay.de")
                sw.WriteLine("0.0.0.0 ebay.es")
                sw.WriteLine("0.0.0.0 ebay.fr")
                sw.WriteLine("0.0.0.0 ebay.ie")
                sw.WriteLine("0.0.0.0 ebay.in")
                sw.WriteLine("0.0.0.0 ebay.it")
                sw.WriteLine("0.0.0.0 ebay.nl")
                sw.WriteLine("0.0.0.0 ebay.pl")
                sw.WriteLine("0.0.0.0 ebay.ph")
                sw.WriteLine("0.0.0.0 ebay.jobs")
                sw.WriteLine("0.0.0.0 cgi.ebay.com")
                sw.WriteLine("0.0.0.0 cgi.ebay.co.uk")
                sw.WriteLine("0.0.0.0 cgi.benl.ebay.be")
                sw.WriteLine("0.0.0.0 cgi.ebay.ca")
                sw.WriteLine("0.0.0.0 cgi.ebay.cn")
                sw.WriteLine("0.0.0.0 cgi.ebay.com.au")
                sw.WriteLine("0.0.0.0 cgi.ebay.com.cn")
                sw.WriteLine("0.0.0.0 cgi.ebay.de")
                sw.WriteLine("0.0.0.0 cgi.ebay.es")
                sw.WriteLine("0.0.0.0 cgi.ebay.fr")
                sw.WriteLine("0.0.0.0 cgi.ebay.ie")
                sw.WriteLine("0.0.0.0 cgi.ebay.in")
                sw.WriteLine("0.0.0.0 cgi.ebay.it")
                sw.WriteLine("0.0.0.0 cgi.ebay.nl")
                sw.WriteLine("0.0.0.0 cgi.ebay.pl")
                sw.WriteLine("0.0.0.0 cgi.ebay.ph")
                sw.WriteLine("0.0.0.0 cgi.ebay.jobs")

                sw.WriteLine("0.0.0.0 www.ebay.com")
                sw.WriteLine("0.0.0.0 www.ebay.co.uk")
                sw.WriteLine("0.0.0.0 www.benl.ebay.be")
                sw.WriteLine("0.0.0.0 www.ebay.ca")
                sw.WriteLine("0.0.0.0 www.ebay.cn")
                sw.WriteLine("0.0.0.0 www.ebay.com.au")
                sw.WriteLine("0.0.0.0 www.ebay.com.cn")
                sw.WriteLine("0.0.0.0 www.ebay.de")
                sw.WriteLine("0.0.0.0 www.ebay.es")
                sw.WriteLine("0.0.0.0 www.ebay.fr")
                sw.WriteLine("0.0.0.0 www.ebay.ie")
                sw.WriteLine("0.0.0.0 www.ebay.in")
                sw.WriteLine("0.0.0.0 www.ebay.it")
                sw.WriteLine("0.0.0.0 www.ebay.nl")
                sw.WriteLine("0.0.0.0 www.ebay.pl")
                sw.WriteLine("0.0.0.0 www.ebay.ph")
                sw.WriteLine("0.0.0.0 www.ebay.jobs")
                sw.WriteLine("0.0.0.0 www.cgi.ebay.com")
                sw.WriteLine("0.0.0.0 www.cgi.ebay.co.uk")
                sw.WriteLine("0.0.0.0 www.cgi.benl.ebay.be")
                sw.WriteLine("0.0.0.0 www.cgi.ebay.ca")
                sw.WriteLine("0.0.0.0 www.cgi.ebay.cn")
                sw.WriteLine("0.0.0.0 www.cgi.ebay.com.au")
                sw.WriteLine("0.0.0.0 www.cgi.ebay.com.cn")
                sw.WriteLine("0.0.0.0 www.cgi.ebay.de")
                sw.WriteLine("0.0.0.0 www.cgi.ebay.es")
                sw.WriteLine("0.0.0.0 www.cgi.ebay.fr")
                sw.WriteLine("0.0.0.0 www.cgi.ebay.ie")
                sw.WriteLine("0.0.0.0 www.cgi.ebay.in")
                sw.WriteLine("0.0.0.0 www.cgi.ebay.it")
                sw.WriteLine("0.0.0.0 www.cgi.ebay.nl")
                sw.WriteLine("0.0.0.0 www.cgi.ebay.pl")
                sw.WriteLine("0.0.0.0 www.cgi.ebay.ph")
                sw.WriteLine("0.0.0.0 www.cgi.ebay.jobs")
            End If
            If chk_failblog.Checked = True Then
                sw.WriteLine("0.0.0.0 failblog.org")
                sw.WriteLine("0.0.0.0 www.failblog.org")
            End If
            If chk_warthree.Checked = True Then
                sw.WriteLine("0.0.0.0 uswest.battle.net")
                sw.WriteLine("0.0.0.0 useast.battle.net")
                sw.WriteLine("0.0.0.0 asia.battle.net")
                sw.WriteLine("0.0.0.0 europe.battle.net")
                sw.WriteLine("0.0.0.0 classicbeta.battle.net")
                sw.WriteLine("0.0.0.0 www.uswest.battle.net")
                sw.WriteLine("0.0.0.0 www.useast.battle.net")
                sw.WriteLine("0.0.0.0 www.asia.battle.net")
                sw.WriteLine("0.0.0.0 www.europe.battle.net")
                sw.WriteLine("0.0.0.0 www.classicbeta.battle.net")
            End If
            If chk_reddit.Checked = True Then
                sw.WriteLine("0.0.0.0 reddit.com")
                sw.WriteLine("0.0.0.0 www.reddit.com")
            End If
            If chk_wikipedia.Checked = True Then
                sw.WriteLine("0.0.0.0 wikipedia.org")
                sw.WriteLine("0.0.0.0 www.wikipedia.org")
                sw.WriteLine("0.0.0.0 ab.wikipedia.org")
                sw.WriteLine("0.0.0.0 ace.wikipedia.org")
                sw.WriteLine("0.0.0.0 af.wikipedia.org")
                sw.WriteLine("0.0.0.0 als.wikipedia.org")
                sw.WriteLine("0.0.0.0 am.wikipedia.org")
                sw.WriteLine("0.0.0.0 an.wikipedia.org")
                sw.WriteLine("0.0.0.0 ang.wikipedia.org")
                sw.WriteLine("0.0.0.0 ar.wikipedia.org")
                sw.WriteLine("0.0.0.0 arc.wikipedia.org")
                sw.WriteLine("0.0.0.0 arz.wikipedia.org")
                sw.WriteLine("0.0.0.0 as.wikipedia.org")
                sw.WriteLine("0.0.0.0 ast.wikipedia.org")
                sw.WriteLine("0.0.0.0 av.wikipedia.org")
                sw.WriteLine("0.0.0.0 ay.wikipedia.org")
                sw.WriteLine("0.0.0.0 az.wikipedia.org")
                sw.WriteLine("0.0.0.0 ba.wikipedia.org")
                sw.WriteLine("0.0.0.0 bar.wikipedia.org")
                sw.WriteLine("0.0.0.0 bat-smg.wikipedia.org")
                sw.WriteLine("0.0.0.0 bcl.wikipedia.org")
                sw.WriteLine("0.0.0.0 be.wikipedia.org")
                sw.WriteLine("0.0.0.0 bg.wikipedia.org")
                sw.WriteLine("0.0.0.0 bh.wikipedia.org")
                sw.WriteLine("0.0.0.0 bi.wikipedia.org")
                sw.WriteLine("0.0.0.0 bm.wikipedia.org")
                sw.WriteLine("0.0.0.0 bn.wikipedia.org")
                sw.WriteLine("0.0.0.0 bo.wikipedia.org")
                sw.WriteLine("0.0.0.0 bpy.wikipedia.org")
                sw.WriteLine("0.0.0.0 br.wikipedia.org")
                sw.WriteLine("0.0.0.0 bs.wikipedia.org")
                sw.WriteLine("0.0.0.0 bug.wikipedia.org")
                sw.WriteLine("0.0.0.0 bxr.wikipedia.org")
                sw.WriteLine("0.0.0.0 ca.wikipedia.org")
                sw.WriteLine("0.0.0.0 cbk-zam.wikipedia.org")
                sw.WriteLine("0.0.0.0 cdo.wikipedia.org")
                sw.WriteLine("0.0.0.0 ce.wikipedia.org")
                sw.WriteLine("0.0.0.0 ceb.wikipedia.org")
                sw.WriteLine("0.0.0.0 ch.wikipedia.org")
                sw.WriteLine("0.0.0.0 chr.wikipedia.org")
                sw.WriteLine("0.0.0.0 ckb.wikipedia.org")
                sw.WriteLine("0.0.0.0 co.wikipedia.org")
                sw.WriteLine("0.0.0.0 commons.wikipedia.org")
                sw.WriteLine("0.0.0.0 cr.wikipedia.org")
                sw.WriteLine("0.0.0.0 crh.wikipedia.org")
                sw.WriteLine("0.0.0.0 cs.wikipedia.org")
                sw.WriteLine("0.0.0.0 csb.wikipedia.org")
                sw.WriteLine("0.0.0.0 cu.wikipedia.org")
                sw.WriteLine("0.0.0.0 cv.wikipedia.org")
                sw.WriteLine("0.0.0.0 cy.wikipedia.org")
                sw.WriteLine("0.0.0.0 da.wikipedia.org")
                sw.WriteLine("0.0.0.0 de.wikipedia.org")
                sw.WriteLine("0.0.0.0 diq.wikipedia.org")
                sw.WriteLine("0.0.0.0 dsb.wikipedia.org")
                sw.WriteLine("0.0.0.0 dv.wikipedia.org")
                sw.WriteLine("0.0.0.0 dz.wikipedia.org")
                sw.WriteLine("0.0.0.0 ee.wikipedia.org")
                sw.WriteLine("0.0.0.0 el.wikipedia.org")
                sw.WriteLine("0.0.0.0 eml.wikipedia.org")
                sw.WriteLine("0.0.0.0 en.wikipedia.org")
                sw.WriteLine("0.0.0.0 eo.wikipedia.org")
                sw.WriteLine("0.0.0.0 es.wikipedia.org")
                sw.WriteLine("0.0.0.0 et.wikipedia.org")
                sw.WriteLine("0.0.0.0 eu.wikipedia.org")
                sw.WriteLine("0.0.0.0 ext.wikipedia.org")
                sw.WriteLine("0.0.0.0 fa.wikipedia.org")
                sw.WriteLine("0.0.0.0 fi.wikipedia.org")
                sw.WriteLine("0.0.0.0 fiu-vro.wikipedia.org")
                sw.WriteLine("0.0.0.0 fo.wikipedia.org")
                sw.WriteLine("0.0.0.0 foundation.wikipedia.org")
                sw.WriteLine("0.0.0.0 fr.m.wikipedia.org")
                sw.WriteLine("0.0.0.0 fr.wikipedia.org")
                sw.WriteLine("0.0.0.0 frp.wikipedia.org")
                sw.WriteLine("0.0.0.0 fur.wikipedia.org")
                sw.WriteLine("0.0.0.0 fy.wikipedia.org")
                sw.WriteLine("0.0.0.0 ga.wikipedia.org")
                sw.WriteLine("0.0.0.0 gan.wikipedia.org")
                sw.WriteLine("0.0.0.0 gd.wikipedia.org")
                sw.WriteLine("0.0.0.0 gl.wikipedia.org")
                sw.WriteLine("0.0.0.0 glk.wikipedia.org")
                sw.WriteLine("0.0.0.0 gn.wikipedia.org")
                sw.WriteLine("0.0.0.0 got.wikipedia.org")
                sw.WriteLine("0.0.0.0 gu.wikipedia.org")
                sw.WriteLine("0.0.0.0 gv.wikipedia.org")
                sw.WriteLine("0.0.0.0 ha.wikipedia.org")
                sw.WriteLine("0.0.0.0 hak.wikipedia.org")
                sw.WriteLine("0.0.0.0 haw.wikipedia.org")
                sw.WriteLine("0.0.0.0 he.wikipedia.org")
                sw.WriteLine("0.0.0.0 hi.wikipedia.org")
                sw.WriteLine("0.0.0.0 hif.wikipedia.org")
                sw.WriteLine("0.0.0.0 hr.wikipedia.org")
                sw.WriteLine("0.0.0.0 hsb.wikipedia.org")
                sw.WriteLine("0.0.0.0 ht.wikipedia.org")
                sw.WriteLine("0.0.0.0 hu.wikipedia.org")
                sw.WriteLine("0.0.0.0 hy.wikipedia.org")
                sw.WriteLine("0.0.0.0 ia.wikipedia.org")
                sw.WriteLine("0.0.0.0 id.wikipedia.org")
                sw.WriteLine("0.0.0.0 ie.wikipedia.org")
                sw.WriteLine("0.0.0.0 ig.wikipedia.org")
                sw.WriteLine("0.0.0.0 ik.wikipedia.org")
                sw.WriteLine("0.0.0.0 ilo.wikipedia.org")
                sw.WriteLine("0.0.0.0 io.wikipedia.org")
                sw.WriteLine("0.0.0.0 is.wikipedia.org")
                sw.WriteLine("0.0.0.0 it.wikipedia.org")
                sw.WriteLine("0.0.0.0 iu.wikipedia.org")
                sw.WriteLine("0.0.0.0 ja.wikipedia.org")
                sw.WriteLine("0.0.0.0 jbo.wikipedia.org")
                sw.WriteLine("0.0.0.0 jv.wikipedia.org")
                sw.WriteLine("0.0.0.0 ka.wikipedia.org")
                sw.WriteLine("0.0.0.0 kaa.wikipedia.org")
                sw.WriteLine("0.0.0.0 kab.wikipedia.org")
                sw.WriteLine("0.0.0.0 kg.wikipedia.org")
                sw.WriteLine("0.0.0.0 ki.wikipedia.org")
                sw.WriteLine("0.0.0.0 kk.wikipedia.org")
                sw.WriteLine("0.0.0.0 kl.wikipedia.org")
                sw.WriteLine("0.0.0.0 km.wikipedia.org")
                sw.WriteLine("0.0.0.0 kn.wikipedia.org")
                sw.WriteLine("0.0.0.0 ko.wikipedia.org")
                sw.WriteLine("0.0.0.0 krc.wikipedia.org")
                sw.WriteLine("0.0.0.0 ks.wikipedia.org")
                sw.WriteLine("0.0.0.0 ksh.wikipedia.org")
                sw.WriteLine("0.0.0.0 ku.wikipedia.org")
                sw.WriteLine("0.0.0.0 kv.wikipedia.org")
                sw.WriteLine("0.0.0.0 kw.wikipedia.org")
                sw.WriteLine("0.0.0.0 ky.wikipedia.org")
                sw.WriteLine("0.0.0.0 la.wikipedia.org")
                sw.WriteLine("0.0.0.0 lad.wikipedia.org")
                sw.WriteLine("0.0.0.0 lb.wikipedia.org")
                sw.WriteLine("0.0.0.0 li.wikipedia.org")
                sw.WriteLine("0.0.0.0 lij.wikipedia.org")
                sw.WriteLine("0.0.0.0 lmo.wikipedia.org")
                sw.WriteLine("0.0.0.0 ln.wikipedia.org")
                sw.WriteLine("0.0.0.0 lo.wikipedia.org")
                sw.WriteLine("0.0.0.0 lt.wikipedia.org")
                sw.WriteLine("0.0.0.0 lv.wikipedia.org")
                sw.WriteLine("0.0.0.0 map-bms.wikipedia.org")
                sw.WriteLine("0.0.0.0 mdf.wikipedia.org")
                sw.WriteLine("0.0.0.0 meta.wikipedia.org")
                sw.WriteLine("0.0.0.0 mg.wikipedia.org")
                sw.WriteLine("0.0.0.0 mhr.wikipedia.org")
                sw.WriteLine("0.0.0.0 mi.wikipedia.org")
                sw.WriteLine("0.0.0.0 mk.wikipedia.org")
                sw.WriteLine("0.0.0.0 ml.wikipedia.org")
                sw.WriteLine("0.0.0.0 mn.wikipedia.org")
                sw.WriteLine("0.0.0.0 mo.wikipedia.org")
                sw.WriteLine("0.0.0.0 mr.wikipedia.org")
                sw.WriteLine("0.0.0.0 ms.wikipedia.org")
                sw.WriteLine("0.0.0.0 mt.wikipedia.org")
                sw.WriteLine("0.0.0.0 mwl.wikipedia.org")
                sw.WriteLine("0.0.0.0 my.wikipedia.org")
                sw.WriteLine("0.0.0.0 myv.wikipedia.org")
                sw.WriteLine("0.0.0.0 mzn.wikipedia.org")
                sw.WriteLine("0.0.0.0 na.wikipedia.org")
                sw.WriteLine("0.0.0.0 nah.wikipedia.org")
                sw.WriteLine("0.0.0.0 nap.wikipedia.org")
                sw.WriteLine("0.0.0.0 nds-nl.wikipedia.org")
                sw.WriteLine("0.0.0.0 nds.wikipedia.org")
                sw.WriteLine("0.0.0.0 ne.wikipedia.org")
                sw.WriteLine("0.0.0.0 new.wikipedia.org")
                sw.WriteLine("0.0.0.0 nl.wikipedia.org")
                sw.WriteLine("0.0.0.0 nn.wikipedia.org")
                sw.WriteLine("0.0.0.0 no.wikipedia.org")
                sw.WriteLine("0.0.0.0 nov.wikipedia.org")
                sw.WriteLine("0.0.0.0 nrm.wikipedia.org")
                sw.WriteLine("0.0.0.0 nv.wikipedia.org")
                sw.WriteLine("0.0.0.0 oc.wikipedia.org")
                sw.WriteLine("0.0.0.0 om.wikipedia.org")
                sw.WriteLine("0.0.0.0 or.wikipedia.org")
                sw.WriteLine("0.0.0.0 os.wikipedia.org")
                sw.WriteLine("0.0.0.0 pa.wikipedia.org")
                sw.WriteLine("0.0.0.0 pag.wikipedia.org")
                sw.WriteLine("0.0.0.0 pam.wikipedia.org")
                sw.WriteLine("0.0.0.0 pap.wikipedia.org")
                sw.WriteLine("0.0.0.0 pcd.wikipedia.org")
                sw.WriteLine("0.0.0.0 pdc.wikipedia.org")
                sw.WriteLine("0.0.0.0 pi.wikipedia.org")
                sw.WriteLine("0.0.0.0 pih.wikipedia.org")
                sw.WriteLine("0.0.0.0 pl.wikipedia.org")
                sw.WriteLine("0.0.0.0 pms.wikipedia.org")
                sw.WriteLine("0.0.0.0 pnb.wikipedia.org")
                sw.WriteLine("0.0.0.0 pnt.wikipedia.org")
                sw.WriteLine("0.0.0.0 ps.wikipedia.org")
                sw.WriteLine("0.0.0.0 pt.wikipedia.org")
                sw.WriteLine("0.0.0.0 qu.wikipedia.org")
                sw.WriteLine("0.0.0.0 rm.wikipedia.org")
                sw.WriteLine("0.0.0.0 rmy.wikipedia.org")
                sw.WriteLine("0.0.0.0 ro.wikipedia.org")
                sw.WriteLine("0.0.0.0 roa-rup.wikipedia.org")
                sw.WriteLine("0.0.0.0 roa-tara.wikipedia.org")
                sw.WriteLine("0.0.0.0 ru.wikipedia.org")
                sw.WriteLine("0.0.0.0 sa.wikipedia.org")
                sw.WriteLine("0.0.0.0 sah.wikipedia.org")
                sw.WriteLine("0.0.0.0 sc.wikipedia.org")
                sw.WriteLine("0.0.0.0 scn.wikipedia.org")
                sw.WriteLine("0.0.0.0 sco.wikipedia.org")
                sw.WriteLine("0.0.0.0 sd.wikipedia.org")
                sw.WriteLine("0.0.0.0 se.wikipedia.org")
                sw.WriteLine("0.0.0.0 sg.wikipedia.org")
                sw.WriteLine("0.0.0.0 sh.wikipedia.org")
                sw.WriteLine("0.0.0.0 si.wikipedia.org")
                sw.WriteLine("0.0.0.0 simple.wikipedia.org")
                sw.WriteLine("0.0.0.0 sk.wikipedia.org")
                sw.WriteLine("0.0.0.0 sl.wikipedia.org")
                sw.WriteLine("0.0.0.0 sm.wikipedia.org")
                sw.WriteLine("0.0.0.0 so.wikipedia.org")
                sw.WriteLine("0.0.0.0 species.wikipedia.org")
                sw.WriteLine("0.0.0.0 sq.wikipedia.org")
                sw.WriteLine("0.0.0.0 sr.wikipedia.org")
                sw.WriteLine("0.0.0.0 srn.wikipedia.org")
                sw.WriteLine("0.0.0.0 ss.wikipedia.org")
                sw.WriteLine("0.0.0.0 stq.wikipedia.org")
                sw.WriteLine("0.0.0.0 su.wikipedia.org")
                sw.WriteLine("0.0.0.0 sv.wikipedia.org")
                sw.WriteLine("0.0.0.0 sw.wikipedia.org")
                sw.WriteLine("0.0.0.0 szl.wikipedia.org")
                sw.WriteLine("0.0.0.0 ta.wikipedia.org")
                sw.WriteLine("0.0.0.0 te.wikipedia.org")
                sw.WriteLine("0.0.0.0 tet.wikipedia.org")
                sw.WriteLine("0.0.0.0 tg.wikipedia.org")
                sw.WriteLine("0.0.0.0 th.wikipedia.org")
                sw.WriteLine("0.0.0.0 ti.wikipedia.org")
                sw.WriteLine("0.0.0.0 tk.wikipedia.org")
                sw.WriteLine("0.0.0.0 tl.wikipedia.org")
                sw.WriteLine("0.0.0.0 tn.wikipedia.org")
                sw.WriteLine("0.0.0.0 to.wikipedia.org")
                sw.WriteLine("0.0.0.0 tpi.wikipedia.org")
                sw.WriteLine("0.0.0.0 tr.wikipedia.org")
                sw.WriteLine("0.0.0.0 ts.wikipedia.org")
                sw.WriteLine("0.0.0.0 tt.wikipedia.org")
                sw.WriteLine("0.0.0.0 ty.wikipedia.org")
                sw.WriteLine("0.0.0.0 udm.wikipedia.org")
                sw.WriteLine("0.0.0.0 ug.wikipedia.org")
                sw.WriteLine("0.0.0.0 uk.wikipedia.org")
                sw.WriteLine("0.0.0.0 ur.wikipedia.org")
                sw.WriteLine("0.0.0.0 uz.wikipedia.org")
                sw.WriteLine("0.0.0.0 ve.wikipedia.org")
                sw.WriteLine("0.0.0.0 vec.wikipedia.org")
                sw.WriteLine("0.0.0.0 vi.wikipedia.org")
                sw.WriteLine("0.0.0.0 vls.wikipedia.org")
                sw.WriteLine("0.0.0.0 vo.wikipedia.org")
                sw.WriteLine("0.0.0.0 wa.wikipedia.org")
                sw.WriteLine("0.0.0.0 war.wikipedia.org")
                sw.WriteLine("0.0.0.0 wo.wikipedia.org")
                sw.WriteLine("0.0.0.0 wuu.wikipedia.org")
                sw.WriteLine("0.0.0.0 xal.wikipedia.org")
                sw.WriteLine("0.0.0.0 xh.wikipedia.org")
                sw.WriteLine("0.0.0.0 yi.wikipedia.org")
                sw.WriteLine("0.0.0.0 yo.wikipedia.org")
                sw.WriteLine("0.0.0.0 za.wikipedia.org")
                sw.WriteLine("0.0.0.0 zea.wikipedia.org")
                sw.WriteLine("0.0.0.0 zh-classical.wikipedia.org")
                sw.WriteLine("0.0.0.0 zh-min-nan.wikipedia.org")
                sw.WriteLine("0.0.0.0 zh-yue.wikipedia.org")
                sw.WriteLine("0.0.0.0 zh.wikipedia.org")
                sw.WriteLine("0.0.0.0 zu.wikipedia.org")

                sw.WriteLine("0.0.0.0 www.ab.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ace.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.af.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.als.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.am.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.an.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ang.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ar.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.arc.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.arz.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.as.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ast.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.av.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ay.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.az.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ba.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.bar.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.bat-smg.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.bcl.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.be.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.bg.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.bh.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.bi.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.bm.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.bn.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.bo.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.bpy.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.br.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.bs.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.bug.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.bxr.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ca.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.cbk-zam.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.cdo.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ce.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ceb.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ch.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.chr.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ckb.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.co.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.commons.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.cr.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.crh.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.cs.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.csb.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.cu.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.cv.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.cy.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.da.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.de.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.diq.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.dsb.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.dv.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.dz.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ee.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.el.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.eml.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.en.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.eo.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.es.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.et.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.eu.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ext.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.fa.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.fi.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.fiu-vro.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.fo.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.foundation.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.fr.m.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.fr.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.frp.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.fur.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.fy.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ga.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.gan.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.gd.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.gl.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.glk.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.gn.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.got.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.gu.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.gv.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ha.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.hak.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.haw.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.he.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.hi.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.hif.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.hr.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.hsb.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ht.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.hu.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.hy.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ia.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.id.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ie.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ig.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ik.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ilo.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.io.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.is.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.it.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.iu.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ja.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.jbo.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.jv.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ka.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.kaa.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.kab.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.kg.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ki.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.kk.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.kl.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.km.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.kn.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ko.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.krc.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ks.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ksh.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ku.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.kv.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.kw.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ky.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.la.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.lad.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.lb.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.li.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.lij.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.lmo.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ln.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.lo.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.lt.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.lv.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.map-bms.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.mdf.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.meta.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.mg.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.mhr.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.mi.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.mk.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ml.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.mn.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.mo.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.mr.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ms.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.mt.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.mwl.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.my.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.myv.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.mzn.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.na.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.nah.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.nap.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.nds-nl.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.nds.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ne.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.new.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.nl.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.nn.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.no.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.nov.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.nrm.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.nv.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.oc.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.om.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.or.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.os.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.pa.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.pag.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.pam.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.pap.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.pcd.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.pdc.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.pi.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.pih.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.pl.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.pms.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.pnb.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.pnt.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ps.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.pt.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.qu.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.rm.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.rmy.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ro.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.roa-rup.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.roa-tara.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ru.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.sa.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.sah.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.sc.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.scn.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.sco.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.sd.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.se.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.sg.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.sh.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.si.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.simple.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.sk.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.sl.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.sm.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.so.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.species.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.sq.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.sr.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.srn.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ss.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.stq.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.su.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.sv.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.sw.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.szl.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ta.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.te.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.tet.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.tg.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.th.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ti.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.tk.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.tl.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.tn.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.to.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.tpi.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.tr.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ts.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.tt.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ty.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.udm.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ug.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.uk.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ur.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.uz.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.ve.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.vec.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.vi.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.vls.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.vo.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.wa.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.war.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.wo.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.wuu.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.xal.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.xh.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.yi.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.yo.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.za.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.zea.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.zh-classical.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.zh-min-nan.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.zh-yue.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.zh.wikipedia.org")
                sw.WriteLine("0.0.0.0 www.zu.wikipedia.org")
            End If
            For Each itm As String In list_cus.Items
                sw.WriteLine("0.0.0.0 " + itm)
                sw.WriteLine("0.0.0.0 www." + itm)
            Next
            sw.Close()

        Catch ex As Exception
            MsgBox("Error writing to your hosts file. Your antivirus most likely doesn't trust what I will do with it. Please don't disable your antivirus though. You have not been blocked from anything.")
            End
        End Try

        Try
            install = Registry.GetValue("HKEY_LOCAL_MACHINE\Software\KCTRP", "installpath", "C:\Program Files\ColdTurkey")
        Catch ex As Exception
            install = "C:\Program Files\ColdTurkey"
        End Try

        Dim srvTimeS As String = "0"
        Dim srvTime, expectedtime As Double
        Try
            srvTimeS = GetHTML("http://getcoldturkey.com/time.php")
        Catch ex As Exception
            MsgBox("Error contacting the time server. You need to be plugged into the Internet... " + vbNewLine + "Or make an exception in your firewall. You are not going to be blocked.")
            erroredout()
        End Try
        srvTime = Val(srvTimeS)
        expectedtime = srvTime + secondsout

        Try
            Registry.SetValue("HKEY_USERS\.DEFAULT\Software\KCTRP", "tc0", wrapper.EncryptData(expectedtime.ToString))
        Catch ex As Exception
            MsgBox("Error writing to Registry")
            End
        End Try

        If chk_24.Checked = False Then
            msgout = "You are now going Cold Turkey until " & hour.Text & ":" & minute.Text & mornin.Text & " on " & DateTimePicker1.Text & "." & vbNewLine & vbNewLine _
                        & "You might not see the block until you close and reopen your all browser windows. You can see how much time you have left and add more sites by running Cold Turkey again."
        Else
            msgout = "You are now going Cold Turkey until " & hour24.Text & ":" & minute.Text & " on " & DateTimePicker1.Text & "." & vbNewLine & vbNewLine _
                        & "You might not see the block until you close and reopen your all browser windows. You can see how much time you have left and add more sites by running Cold Turkey again."
        End If

        MsgBox(msgout, MsgBoxStyle.Information, "Cold Turkey")

        If srv_alreadyexists = False Then
            Dim install2 As String
            Try
                install2 = Registry.GetValue("HKEY_LOCAL_MACHINE\Software\KCTRP", "installpath", "C:\Program Files\ColdTurkey")
                If install2.Length() < 2 Then
                    install2 = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\KCTRP", True).ToString
                End If
            Catch ex As Exception
                install2 = "C:\Program Files\ColdTurkey"
            End Try
            ServiceInstaller.InstallAndStart("KCTRP", "KCTRP", install2 & "\KCTRP_srv.exe")
        Else
            ServiceInstaller.StartService("KCTRP")
        End If

        Try
            Dim srvcont2 As System.ServiceProcess.ServiceController
            Dim timeSpan As New TimeSpan(0, 0, 30)
            Dim install2 As String
            srvcont2 = New System.ServiceProcess.ServiceController("KCTRP")
            srvcont2.WaitForStatus(ServiceProcess.ServiceControllerStatus.Running, timeSpan)

            Try
                install2 = Registry.GetValue("HKEY_LOCAL_MACHINE\Software\KCTRP", "installpath", "C:\Program Files\ColdTurkey")
                If install2.Length() < 2 Then
                    install2 = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\KCTRP", True).ToString
                End If
            Catch ex As Exception
                install2 = "C:\Program Files\ColdTurkey"
            End Try
            Try
                Dim runMe As RegistryKey = My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
                runMe.SetValue("ColdTurkey_notify", install2 & "\ct_notify.exe")
            Catch ex As Exception
                MsgBox("There was an error writing the ct_notify.exe startup entry")
            End Try
            Try
                Process.Start(install2 & "\ct_notify.exe")
            Catch ex As Exception
                MsgBox("There was an error starting the notification program. Cold Turkey will still work, but you won't be notified when your block is over.")
            End Try
            Try
                Registry.SetValue("HKEY_CURRENT_USER\Software\KCTRP", "pop", "0")
            Catch ex As Exception
                MsgBox("There was an error applying settings for the notification program.")
            End Try

        Catch ex As TimeoutException
            MsgBox("The service could not be started. This is either caused by your antivirus software or firewall. Please don't disable your antivirus to run this program." + vbNewLine + vbNewLine + _
                   "You can try to add a firewall exception to allow internet access to both 'ColdTurkey.exe' and 'kctrp.exe' in the ColdTurkey program folder, then try again.")
            erroredout()
        Catch ex As Exception
            MsgBox("The service was not found and therefore cannot be run. This is a big boo-boo that you should e-mail me about...")
            erroredout()
        End Try

        End

    End Sub

    Private Sub erroredout()

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
        End
    End Sub

    Private Sub tab_sites_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tab_sites.Click
        pan_sites.Visible = True
        pan_custom.Visible = False
        pan_apps.Visible = False
        Dim tab_custom_u As New Font(tab_custom.Font.Name, tab_custom.Font.Size, FontStyle.Underline)
        tab_custom.Font = tab_custom_u
        Dim tab_programs_u As New Font(tab_programs.Font.Name, tab_programs.Font.Size, FontStyle.Underline)
        tab_programs.Font = tab_programs_u
        Dim tab_sites_u As New Font(tab_sites.Font.Name, tab_sites.Font.Size, FontStyle.Regular)
        tab_sites.Font = tab_sites_u

        tab_sites.Cursor = Cursors.Arrow
        tab_custom.Cursor = Cursors.Hand
        tab_programs.Cursor = Cursors.Hand

    End Sub

    Private Sub tab_programs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tab_programs.Click
        pan_apps.Visible = True
        pan_custom.Visible = False
        pan_sites.Visible = False
        Dim tab_custom_u As New Font(tab_custom.Font.Name, tab_custom.Font.Size, FontStyle.Underline)
        tab_custom.Font = tab_custom_u
        Dim tab_programs_u As New Font(tab_programs.Font.Name, tab_programs.Font.Size, FontStyle.Regular)
        tab_programs.Font = tab_programs_u
        Dim tab_sites_u As New Font(tab_sites.Font.Name, tab_sites.Font.Size, FontStyle.Underline)
        tab_sites.Font = tab_sites_u

        tab_sites.Cursor = Cursors.Hand
        tab_custom.Cursor = Cursors.Hand
        tab_programs.Cursor = Cursors.Arrow
    End Sub

    Private Sub tab_custom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tab_custom.Click
        pan_custom.Visible = True
        pan_apps.Visible = False
        pan_sites.Visible = False
        Dim tab_custom_u As New Font(tab_custom.Font.Name, tab_custom.Font.Size, FontStyle.Regular)
        tab_custom.Font = tab_custom_u
        Dim tab_programs_u As New Font(tab_programs.Font.Name, tab_programs.Font.Size, FontStyle.Underline)
        tab_programs.Font = tab_programs_u
        Dim tab_sites_u As New Font(tab_sites.Font.Name, tab_sites.Font.Size, FontStyle.Underline)
        tab_sites.Font = tab_sites_u

        tab_sites.Cursor = Cursors.Hand
        tab_custom.Cursor = Cursors.Arrow
        tab_programs.Cursor = Cursors.Hand
    End Sub

    Private Sub lbl_info_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbl_info.Click
        Dim message As String
        message = "Cold Turkey Beta 0.51" + vbCrLf + "(c) Copyright Felix Belzile, 2011" + vbCrLf + vbCrLf + "ColdTurkey will temporarily block addicting sites and games" + vbCrLf + "so that you can get some work done!"
        MsgBox(message, vbOKOnly, "About ColdTurkey")
    End Sub

    Function GetHTML(ByVal strPage As String) As String
        Dim strReply As String = "NULL"
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

    Private Sub add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles add.Click
        Dim tempString As String = txt_add.Text.ToString
        If StrComp(tempString, "") = 0 Then
        ElseIf InStr(tempString, ".") = 0 Then
            MsgBox("The domain should have an extention. (like .com or .net)")
        Else
            If InStr(tempString, "http://") = 1 Then
                tempString = Microsoft.VisualBasic.Right(tempString, tempString.Length - 7)
            End If
            If InStr(tempString, "www.") = 1 Then
                tempString = Microsoft.VisualBasic.Right(tempString, tempString.Length - 4)
            End If
            If InStr(tempString, "/") <> 0 Then
                Dim loc As Integer
                loc = InStr(tempString, "/")
                tempString = Microsoft.VisualBasic.Left(tempString, loc - 1)
            End If
            If list_cus.Items.Contains(tempString) Then
                MsgBox("This address is already in the list.")
            ElseIf StrComp(tempString, "getcoldturkey.com") = 0 Then
                MsgBox("This is a mandatory address and must not be blocked.")
            Else
                list_cus.Items.Add(tempString)
                txt_add.Text = ""
                Using sw As New IO.StreamWriter(install + "\data\custom", False)
                    For Each itm As String In list_cus.Items
                        sw.WriteLine(itm)
                    Next
                End Using
            End If
        End If

    End Sub

    Private Sub txt_add_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_add.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            add.PerformClick()
        End If
    End Sub

    Private Sub remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles remove.Click
        Dim locofitem As Integer
        locofitem = list_cus.SelectedIndex
        list_cus.Items.Remove(list_cus.SelectedItem)
        Using sw As New IO.StreamWriter(install + "\data\custom", False)
            For Each itm As String In list_cus.Items
                sw.WriteLine(itm)
            Next
        End Using
        If locofitem < list_cus.Items.Count And locofitem > 0 Then
            list_cus.SetSelected(locofitem, True)
        ElseIf locofitem = list_cus.Items.Count And locofitem > 0 Then
            list_cus.SetSelected(locofitem - 1, True)
        ElseIf locofitem = 0 And list_cus.Items.Count > 0 Then
            list_cus.SetSelected(0, True)
        End If
        If firsttimeclickcus = True Then
            firsttimeclickcus = False
            txt_add.Text = ""
            Dim normfont As New Font(txt_add.Font.Name, txt_add.Font.Size, FontStyle.Regular)
            txt_add.Font = normfont
            txt_add.ForeColor = Color.Black
            add.Enabled = True
        End If
    End Sub

    Private Sub chk_24_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chk_24.CheckedChanged
        If chk_24.Checked = True Then
            mornin.Visible = False
            hour.Visible = False
            If hour.SelectedIndex >= 0 Then
                If StrComp(mornin.Text, "PM", vbTextCompare) = 0 And Val(hour.Text) >= 1 And Val(hour.Text) <= 11 Then
                    hour24.SelectedIndex = Val(hour.Text) + 12
                ElseIf Val(hour.Text) = 12 And StrComp(mornin.Text, "AM", vbTextCompare) = 0 Then
                    hour24.SelectedIndex = 0
                Else
                    hour24.SelectedIndex = Val(hour.Text)
                End If
            End If
            hour24.Visible = True
            If (hour24.SelectedIndex >= 0 And minute.SelectedIndex >= 0) Or (hour.SelectedIndex >= 0 And minute.SelectedIndex >= 0 And mornin.SelectedIndex >= 0) Then
                calculatediff()
            End If

        Else
            mornin.Visible = True
            hour.Visible = True
            hour24.Visible = False
            If hour24.SelectedIndex >= 0 Then
                If Val(hour24.Text) > 0 And Val(hour24.Text) < 12 Then
                    hour.SelectedIndex = Val(hour24.Text) - 1
                    mornin.SelectedIndex = 0
                ElseIf Val(hour24.Text) = 0 Then
                    hour.SelectedIndex = 11
                    mornin.SelectedIndex = 0
                ElseIf Val(hour24.Text) = 12 Then
                    hour.SelectedIndex = Val(hour24.Text) - 1
                    mornin.SelectedIndex = 1
                Else
                    hour.SelectedIndex = Val(hour24.Text) - 13
                    mornin.SelectedIndex = 1
                End If
            End If
            If (hour24.SelectedIndex >= 0 And minute.SelectedIndex >= 0) Or (hour.SelectedIndex >= 0 And minute.SelectedIndex >= 0 And mornin.SelectedIndex >= 0) Then
                calculatediff()
            End If
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("http://getcoldturkey.com/contact.html")
    End Sub

    Private Sub list_cus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles list_cus.SelectedIndexChanged
        If list_cus.SelectedIndex >= 0 Then
            remove.Enabled = True
        Else
            remove.Enabled = False
        End If
    End Sub


    Private Sub txt_add_TextClicked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_add.Click
        If firsttimeclickcus = True Then
            firsttimeclickcus = False
            txt_add.Text = ""
            Dim normfont As New Font(txt_add.Font.Name, txt_add.Font.Size, FontStyle.Regular)
            txt_add.Font = normfont
            txt_add.ForeColor = Color.Black
            add.Enabled = True
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
                                If list_cus.Items.Contains(line) Then
                                    MsgBox("This address is already in the list." + vbNewLine + line)
                                ElseIf StrComp(line, "getcoldturkey.com") = 0 Then
                                    MsgBox("This is a mandatory address and must not be blocked." + vbNewLine + line)
                                Else
                                    list_cus.Items.Add(line)
                                    Using sw As New IO.StreamWriter(install + "\data\custom", False)
                                        For Each itm As String In list_cus.Items
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
            If firsttimeclickcus = True Then
                firsttimeclickcus = False
                txt_add.Text = ""
                Dim normfont As New Font(txt_add.Font.Name, txt_add.Font.Size, FontStyle.Regular)
                txt_add.Font = normfont
                txt_add.ForeColor = Color.Black
                add.Enabled = True
            End If
        End If

    End Sub

    Private Sub hour24_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles hour24.SelectedIndexChanged
        If hour24.SelectedIndex >= 0 And minute.SelectedIndex >= 0 Then
            calculatediff()
        End If

    End Sub

    Private Sub hour_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles hour.SelectedIndexChanged
        If hour.SelectedIndex >= 0 And minute.SelectedIndex >= 0 And mornin.SelectedIndex >= 0 Then
            calculatediff()
        End If
    End Sub

    Private Sub minute_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles minute.SelectedIndexChanged
        If (hour24.SelectedIndex >= 0 And minute.SelectedIndex >= 0) Or (hour.SelectedIndex >= 0 And minute.SelectedIndex >= 0 And mornin.SelectedIndex >= 0) Then
            calculatediff()
        End If
    End Sub

    Private Sub mornin_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mornin.SelectedIndexChanged
        If hour.SelectedIndex >= 0 And minute.SelectedIndex >= 0 And mornin.SelectedIndex >= 0 Then
            calculatediff()
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        If (hour24.SelectedIndex >= 0 And minute.SelectedIndex >= 0) Or (hour.SelectedIndex >= 0 And minute.SelectedIndex >= 0 And mornin.SelectedIndex >= 0) Then
            calculatediff()
        End If
    End Sub

    Private Sub calculatediff()

        Dim secondsout As Integer

        If chk_24.Checked = False Then
            If StrComp(mornin.Text, "PM", vbTextCompare) = 0 And Val(hour.Text) >= 1 And Val(hour.Text) <= 11 Then
                actualhour = Val(hour.Text) + 12
            ElseIf Val(hour.Text) = 12 And StrComp(mornin.Text, "PM", vbTextCompare) <> 0 Then
                actualhour = 0
            Else
                actualhour = Val(hour.Text)
            End If
        Else
            actualhour = Val(hour24.Text)
        End If

        If DateAndTime.Month(Now) <> DateTimePicker1.Value.Month Then
            secondsout = ((Day(DateSerial(DateTimePicker1.Value.Year, DateAndTime.Month(Now) + 1, 0)) - DateAndTime.Day(Now)) + DateTimePicker1.Value.Day) * 86400
            If TimeOfDay.Hour < actualhour Then
                secondsout = secondsout + ((actualhour - TimeOfDay.Hour) * 60 * 60)
                If TimeOfDay.Minute < Val(minute.Text) Then
                    secondsout = secondsout + ((Val(minute.Text) - TimeOfDay.Minute) * 60)
                ElseIf TimeOfDay.Minute > Val(minute.Text) Then
                    secondsout = secondsout - ((TimeOfDay.Minute - (Val(minute.Text))) * 60)
                End If
            ElseIf TimeOfDay.Hour = actualhour Then
                If TimeOfDay.Minute < Val(minute.Text) Then
                    secondsout = secondsout + ((Val(minute.Text) - TimeOfDay.Minute) * 60)
                ElseIf TimeOfDay.Minute > Val(minute.Text) Then
                    secondsout = secondsout - ((TimeOfDay.Minute - Val(minute.Text)) * 60)
                End If
            ElseIf TimeOfDay.Hour > actualhour Then
                secondsout = secondsout - ((TimeOfDay.Hour - actualhour) * 60 * 60)
                If TimeOfDay.Minute < Val(minute.Text) Then
                    secondsout = secondsout + ((Val(minute.Text) - TimeOfDay.Minute) * 60)
                ElseIf TimeOfDay.Minute > Val(minute.Text) Then
                    secondsout = secondsout - ((TimeOfDay.Minute - (Val(minute.Text))) * 60)
                End If
            End If

        ElseIf DateAndTime.Month(Now) = DateTimePicker1.Value.Month Then
            secondsout = (DateTimePicker1.Value.Day - DateAndTime.Day(Now)) * 86400
            If TimeOfDay.Hour < actualhour Then
                secondsout = secondsout + ((actualhour - TimeOfDay.Hour) * 60 * 60)
                If TimeOfDay.Minute < Val(minute.Text) Then
                    secondsout = secondsout + ((Val(minute.Text) - TimeOfDay.Minute) * 60)
                ElseIf TimeOfDay.Minute > Val(minute.Text) Then
                    secondsout = secondsout - ((TimeOfDay.Minute - (Val(minute.Text))) * 60)
                End If
            ElseIf TimeOfDay.Hour = actualhour Then
                If TimeOfDay.Minute < Val(minute.Text) Then
                    secondsout = secondsout + ((Val(minute.Text) - TimeOfDay.Minute) * 60)
                ElseIf TimeOfDay.Minute > Val(minute.Text) Then
                    secondsout = secondsout - ((TimeOfDay.Minute - Val(minute.Text)) * 60)
                End If
            ElseIf TimeOfDay.Hour > actualhour Then
                secondsout = secondsout - ((TimeOfDay.Hour - actualhour) * 60 * 60)
                If TimeOfDay.Minute < Val(minute.Text) Then
                    secondsout = secondsout + ((Val(minute.Text) - TimeOfDay.Minute) * 60)
                ElseIf TimeOfDay.Minute > Val(minute.Text) Then
                    secondsout = secondsout - ((TimeOfDay.Minute - (Val(minute.Text))) * 60)
                End If
            End If
        End If
        secondsout = secondsout - (TimeOfDay.Second)

        If secondsout > 86400 Then
            timewarning.Text = "(Nice! Thats about " + SecondsToText2(secondsout) + ")"
            timewarning.ForeColor = Color.Purple
            Button1.Enabled = True
        ElseIf secondsout < 60 And secondsout > 0 Then
            timewarning.Text = "(under a minute)"
            timewarning.ForeColor = Color.Purple
            Button1.Enabled = False
        ElseIf secondsout <= 0 Then
            timewarning.Text = "Please enter a time in the future."
            timewarning.ForeColor = Color.Gray
            Button1.Enabled = False
        Else
            timewarning.Text = "(about " + SecondsToText2(secondsout) + ")"
            timewarning.ForeColor = Color.Green
            Button1.Enabled = True
        End If
    End Sub

End Class

Public NotInheritable Class Simple3Des
    Private TripleDes As New TripleDESCryptoServiceProvider
    Private Function TruncateHash(ByVal key As String, ByVal length As Integer) As Byte()

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
    Public Function EncryptData(ByVal plaintext As String) As String

        ' Convert the plaintext string to a byte array.
        Dim plaintextBytes() As Byte = System.Text.Encoding.Unicode.GetBytes(plaintext)

        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the encoder to write to the stream.
        Dim encStream As New CryptoStream(ms, TripleDes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
        encStream.FlushFinalBlock()

        ' Convert the encrypted stream to a printable string.
        Return Convert.ToBase64String(ms.ToArray)
    End Function
    Public Function DecryptData(ByVal encryptedtext As String) As String
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
        Dim decStream As New CryptoStream(ms, TripleDes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
        decStream.FlushFinalBlock()

        ' Convert the plaintext stream to a string.
        Return System.Text.Encoding.Unicode.GetString(ms.ToArray)
    End Function

End Class
