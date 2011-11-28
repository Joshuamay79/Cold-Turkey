<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class coldturkey
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(coldturkey))
        Me.srvcont = New System.ServiceProcess.ServiceController()
        Me.facebookconnect = New System.Windows.Forms.WebBrowser()
        Me.lbl_info = New System.Windows.Forms.PictureBox()
        Me.tab_custom = New System.Windows.Forms.Label()
        Me.tab_programs = New System.Windows.Forms.Label()
        Me.tab_sites = New System.Windows.Forms.Label()
        Me.pan_sites = New System.Windows.Forms.Panel()
        Me.chk_shotmail = New System.Windows.Forms.CheckBox()
        Me.chk_stumble = New System.Windows.Forms.CheckBox()
        Me.chk_reddit = New System.Windows.Forms.CheckBox()
        Me.chk_failblog = New System.Windows.Forms.CheckBox()
        Me.chk_ebay = New System.Windows.Forms.CheckBox()
        Me.chk_collegehumor = New System.Windows.Forms.CheckBox()
        Me.chk_agames = New System.Windows.Forms.CheckBox()
        Me.chk_wikipedia = New System.Windows.Forms.CheckBox()
        Me.chk_facebook = New System.Windows.Forms.CheckBox()
        Me.chk_youtube = New System.Windows.Forms.CheckBox()
        Me.chk_twitter = New System.Windows.Forms.CheckBox()
        Me.chk_myspace = New System.Windows.Forms.CheckBox()
        Me.pan_apps = New System.Windows.Forms.Panel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.chk_warthree = New System.Windows.Forms.CheckBox()
        Me.pan_custom = New System.Windows.Forms.Panel()
        Me.import = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txt_add = New System.Windows.Forms.TextBox()
        Me.remove = New System.Windows.Forms.Button()
        Me.add = New System.Windows.Forms.Button()
        Me.list_cus = New System.Windows.Forms.ListBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.minute = New System.Windows.Forms.ComboBox()
        Me.lbl_date = New System.Windows.Forms.Label()
        Me.hour = New System.Windows.Forms.ComboBox()
        Me.mornin = New System.Windows.Forms.ComboBox()
        Me.lbl_time = New System.Windows.Forms.Label()
        Me.colon = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.updatestatus = New System.Windows.Forms.CheckBox()
        Me.chk_24 = New System.Windows.Forms.CheckBox()
        Me.hour24 = New System.Windows.Forms.ComboBox()
        Me.timewarning = New System.Windows.Forms.Label()
        Me.importfile = New System.Windows.Forms.OpenFileDialog()
        CType(Me.lbl_info, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pan_sites.SuspendLayout()
        Me.pan_apps.SuspendLayout()
        Me.pan_custom.SuspendLayout()
        Me.SuspendLayout()
        '
        'srvcont
        '
        Me.srvcont.ServiceName = "KCTRP"
        '
        'facebookconnect
        '
        Me.facebookconnect.Location = New System.Drawing.Point(388, 502)
        Me.facebookconnect.Name = "facebookconnect"
        Me.facebookconnect.ScrollBarsEnabled = False
        Me.facebookconnect.Size = New System.Drawing.Size(0, 0)
        Me.facebookconnect.TabIndex = 11
        Me.facebookconnect.Url = New System.Uri("http://antisocial.hostzi.com/", System.UriKind.Absolute)
        Me.facebookconnect.Visible = False
        '
        'lbl_info
        '
        Me.lbl_info.BackColor = System.Drawing.Color.Transparent
        Me.lbl_info.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lbl_info.Image = CType(resources.GetObject("lbl_info.Image"), System.Drawing.Image)
        Me.lbl_info.Location = New System.Drawing.Point(422, 0)
        Me.lbl_info.Name = "lbl_info"
        Me.lbl_info.Size = New System.Drawing.Size(30, 30)
        Me.lbl_info.TabIndex = 16
        Me.lbl_info.TabStop = False
        '
        'tab_custom
        '
        Me.tab_custom.BackColor = System.Drawing.Color.Transparent
        Me.tab_custom.Cursor = System.Windows.Forms.Cursors.Hand
        Me.tab_custom.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tab_custom.ForeColor = System.Drawing.Color.Black
        Me.tab_custom.Location = New System.Drawing.Point(269, 148)
        Me.tab_custom.Name = "tab_custom"
        Me.tab_custom.Size = New System.Drawing.Size(80, 18)
        Me.tab_custom.TabIndex = 24
        Me.tab_custom.Text = "Custom"
        Me.tab_custom.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'tab_programs
        '
        Me.tab_programs.BackColor = System.Drawing.Color.Transparent
        Me.tab_programs.Cursor = System.Windows.Forms.Cursors.Hand
        Me.tab_programs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tab_programs.ForeColor = System.Drawing.Color.Black
        Me.tab_programs.Location = New System.Drawing.Point(181, 148)
        Me.tab_programs.Name = "tab_programs"
        Me.tab_programs.Size = New System.Drawing.Size(80, 18)
        Me.tab_programs.TabIndex = 23
        Me.tab_programs.Text = "Games"
        Me.tab_programs.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'tab_sites
        '
        Me.tab_sites.BackColor = System.Drawing.Color.Transparent
        Me.tab_sites.Cursor = System.Windows.Forms.Cursors.Hand
        Me.tab_sites.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tab_sites.ForeColor = System.Drawing.Color.Black
        Me.tab_sites.Location = New System.Drawing.Point(99, 148)
        Me.tab_sites.Name = "tab_sites"
        Me.tab_sites.Size = New System.Drawing.Size(75, 18)
        Me.tab_sites.TabIndex = 22
        Me.tab_sites.Text = "Sites"
        Me.tab_sites.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'pan_sites
        '
        Me.pan_sites.BackColor = System.Drawing.Color.Transparent
        Me.pan_sites.Controls.Add(Me.chk_shotmail)
        Me.pan_sites.Controls.Add(Me.chk_stumble)
        Me.pan_sites.Controls.Add(Me.chk_reddit)
        Me.pan_sites.Controls.Add(Me.chk_failblog)
        Me.pan_sites.Controls.Add(Me.chk_ebay)
        Me.pan_sites.Controls.Add(Me.chk_collegehumor)
        Me.pan_sites.Controls.Add(Me.chk_agames)
        Me.pan_sites.Controls.Add(Me.chk_wikipedia)
        Me.pan_sites.Controls.Add(Me.chk_facebook)
        Me.pan_sites.Controls.Add(Me.chk_youtube)
        Me.pan_sites.Controls.Add(Me.chk_twitter)
        Me.pan_sites.Controls.Add(Me.chk_myspace)
        Me.pan_sites.Location = New System.Drawing.Point(45, 187)
        Me.pan_sites.Name = "pan_sites"
        Me.pan_sites.Size = New System.Drawing.Size(374, 108)
        Me.pan_sites.TabIndex = 20
        '
        'chk_shotmail
        '
        Me.chk_shotmail.AutoSize = True
        Me.chk_shotmail.BackColor = System.Drawing.Color.Transparent
        Me.chk_shotmail.Checked = True
        Me.chk_shotmail.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_shotmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_shotmail.Location = New System.Drawing.Point(111, 3)
        Me.chk_shotmail.Name = "chk_shotmail"
        Me.chk_shotmail.Size = New System.Drawing.Size(106, 19)
        Me.chk_shotmail.TabIndex = 11
        Me.chk_shotmail.Text = "Hotmail / MSN"
        Me.chk_shotmail.UseVisualStyleBackColor = False
        '
        'chk_stumble
        '
        Me.chk_stumble.AutoSize = True
        Me.chk_stumble.BackColor = System.Drawing.Color.Transparent
        Me.chk_stumble.Checked = True
        Me.chk_stumble.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_stumble.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_stumble.Location = New System.Drawing.Point(253, 49)
        Me.chk_stumble.Name = "chk_stumble"
        Me.chk_stumble.Size = New System.Drawing.Size(102, 19)
        Me.chk_stumble.TabIndex = 10
        Me.chk_stumble.Text = "StumbleUpon"
        Me.chk_stumble.UseVisualStyleBackColor = False
        '
        'chk_reddit
        '
        Me.chk_reddit.AutoSize = True
        Me.chk_reddit.BackColor = System.Drawing.Color.Transparent
        Me.chk_reddit.Checked = True
        Me.chk_reddit.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_reddit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_reddit.Location = New System.Drawing.Point(254, 26)
        Me.chk_reddit.Name = "chk_reddit"
        Me.chk_reddit.Size = New System.Drawing.Size(62, 19)
        Me.chk_reddit.TabIndex = 9
        Me.chk_reddit.Text = "Reddit"
        Me.chk_reddit.UseVisualStyleBackColor = False
        '
        'chk_failblog
        '
        Me.chk_failblog.AutoSize = True
        Me.chk_failblog.Checked = True
        Me.chk_failblog.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_failblog.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_failblog.Location = New System.Drawing.Point(254, 3)
        Me.chk_failblog.Name = "chk_failblog"
        Me.chk_failblog.Size = New System.Drawing.Size(71, 19)
        Me.chk_failblog.TabIndex = 8
        Me.chk_failblog.Text = "FailBlog"
        Me.chk_failblog.UseVisualStyleBackColor = True
        '
        'chk_ebay
        '
        Me.chk_ebay.AutoSize = True
        Me.chk_ebay.BackColor = System.Drawing.Color.Transparent
        Me.chk_ebay.Checked = True
        Me.chk_ebay.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_ebay.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_ebay.Location = New System.Drawing.Point(110, 72)
        Me.chk_ebay.Name = "chk_ebay"
        Me.chk_ebay.Size = New System.Drawing.Size(53, 19)
        Me.chk_ebay.TabIndex = 7
        Me.chk_ebay.Text = "Ebay"
        Me.chk_ebay.UseVisualStyleBackColor = False
        '
        'chk_collegehumor
        '
        Me.chk_collegehumor.AutoSize = True
        Me.chk_collegehumor.BackColor = System.Drawing.Color.Transparent
        Me.chk_collegehumor.Checked = True
        Me.chk_collegehumor.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_collegehumor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_collegehumor.Location = New System.Drawing.Point(110, 49)
        Me.chk_collegehumor.Name = "chk_collegehumor"
        Me.chk_collegehumor.Size = New System.Drawing.Size(106, 19)
        Me.chk_collegehumor.TabIndex = 6
        Me.chk_collegehumor.Text = "CollegeHumor"
        Me.chk_collegehumor.UseVisualStyleBackColor = False
        '
        'chk_agames
        '
        Me.chk_agames.AutoSize = True
        Me.chk_agames.BackColor = System.Drawing.Color.Transparent
        Me.chk_agames.Checked = True
        Me.chk_agames.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_agames.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_agames.Location = New System.Drawing.Point(111, 26)
        Me.chk_agames.Name = "chk_agames"
        Me.chk_agames.Size = New System.Drawing.Size(116, 19)
        Me.chk_agames.TabIndex = 5
        Me.chk_agames.Text = "AddictingGames"
        Me.chk_agames.UseVisualStyleBackColor = False
        '
        'chk_wikipedia
        '
        Me.chk_wikipedia.AutoSize = True
        Me.chk_wikipedia.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_wikipedia.Location = New System.Drawing.Point(253, 72)
        Me.chk_wikipedia.Name = "chk_wikipedia"
        Me.chk_wikipedia.Size = New System.Drawing.Size(80, 19)
        Me.chk_wikipedia.TabIndex = 4
        Me.chk_wikipedia.Text = "Wikipedia"
        Me.chk_wikipedia.UseVisualStyleBackColor = True
        '
        'chk_facebook
        '
        Me.chk_facebook.AutoSize = True
        Me.chk_facebook.BackColor = System.Drawing.Color.Transparent
        Me.chk_facebook.Checked = True
        Me.chk_facebook.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_facebook.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_facebook.Location = New System.Drawing.Point(3, 3)
        Me.chk_facebook.Name = "chk_facebook"
        Me.chk_facebook.Size = New System.Drawing.Size(80, 19)
        Me.chk_facebook.TabIndex = 0
        Me.chk_facebook.Text = "Facebook"
        Me.chk_facebook.UseVisualStyleBackColor = False
        '
        'chk_youtube
        '
        Me.chk_youtube.AutoSize = True
        Me.chk_youtube.BackColor = System.Drawing.Color.Transparent
        Me.chk_youtube.Checked = True
        Me.chk_youtube.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_youtube.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_youtube.Location = New System.Drawing.Point(3, 72)
        Me.chk_youtube.Name = "chk_youtube"
        Me.chk_youtube.Size = New System.Drawing.Size(71, 19)
        Me.chk_youtube.TabIndex = 3
        Me.chk_youtube.Text = "Youtube"
        Me.chk_youtube.UseVisualStyleBackColor = False
        '
        'chk_twitter
        '
        Me.chk_twitter.AutoSize = True
        Me.chk_twitter.BackColor = System.Drawing.Color.Transparent
        Me.chk_twitter.Checked = True
        Me.chk_twitter.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_twitter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_twitter.ForeColor = System.Drawing.Color.Black
        Me.chk_twitter.Location = New System.Drawing.Point(3, 26)
        Me.chk_twitter.Name = "chk_twitter"
        Me.chk_twitter.Size = New System.Drawing.Size(62, 19)
        Me.chk_twitter.TabIndex = 2
        Me.chk_twitter.Text = "Twitter"
        Me.chk_twitter.UseVisualStyleBackColor = False
        '
        'chk_myspace
        '
        Me.chk_myspace.AutoSize = True
        Me.chk_myspace.BackColor = System.Drawing.Color.Transparent
        Me.chk_myspace.Checked = True
        Me.chk_myspace.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_myspace.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_myspace.Location = New System.Drawing.Point(3, 49)
        Me.chk_myspace.Name = "chk_myspace"
        Me.chk_myspace.Size = New System.Drawing.Size(77, 19)
        Me.chk_myspace.TabIndex = 1
        Me.chk_myspace.Text = "MySpace"
        Me.chk_myspace.UseVisualStyleBackColor = False
        '
        'pan_apps
        '
        Me.pan_apps.BackColor = System.Drawing.Color.Transparent
        Me.pan_apps.Controls.Add(Me.LinkLabel1)
        Me.pan_apps.Controls.Add(Me.chk_warthree)
        Me.pan_apps.Location = New System.Drawing.Point(45, 187)
        Me.pan_apps.Name = "pan_apps"
        Me.pan_apps.Size = New System.Drawing.Size(374, 108)
        Me.pan_apps.TabIndex = 21
        Me.pan_apps.Visible = False
        '
        'LinkLabel1
        '
        Me.LinkLabel1.ActiveLinkColor = System.Drawing.Color.Black
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.LinkColor = System.Drawing.Color.Black
        Me.LinkLabel1.Location = New System.Drawing.Point(118, 78)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(118, 13)
        Me.LinkLabel1.TabIndex = 7
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Request more Games..."
        Me.LinkLabel1.VisitedLinkColor = System.Drawing.Color.Black
        '
        'chk_warthree
        '
        Me.chk_warthree.AutoSize = True
        Me.chk_warthree.Checked = True
        Me.chk_warthree.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_warthree.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chk_warthree.Location = New System.Drawing.Point(3, 3)
        Me.chk_warthree.Name = "chk_warthree"
        Me.chk_warthree.Size = New System.Drawing.Size(83, 19)
        Me.chk_warthree.TabIndex = 6
        Me.chk_warthree.Text = "Warcraft III"
        Me.chk_warthree.UseVisualStyleBackColor = True
        '
        'pan_custom
        '
        Me.pan_custom.BackColor = System.Drawing.Color.Transparent
        Me.pan_custom.Controls.Add(Me.import)
        Me.pan_custom.Controls.Add(Me.Label1)
        Me.pan_custom.Controls.Add(Me.Label2)
        Me.pan_custom.Controls.Add(Me.txt_add)
        Me.pan_custom.Controls.Add(Me.remove)
        Me.pan_custom.Controls.Add(Me.add)
        Me.pan_custom.Controls.Add(Me.list_cus)
        Me.pan_custom.Location = New System.Drawing.Point(45, 187)
        Me.pan_custom.Name = "pan_custom"
        Me.pan_custom.Size = New System.Drawing.Size(374, 108)
        Me.pan_custom.TabIndex = 25
        Me.pan_custom.Visible = False
        '
        'import
        '
        Me.import.Location = New System.Drawing.Point(278, 78)
        Me.import.Name = "import"
        Me.import.Size = New System.Drawing.Size(80, 20)
        Me.import.TabIndex = 27
        Me.import.Text = "Import file..."
        Me.import.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(15, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 15)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "www."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(15, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 15)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "www."
        '
        'txt_add
        '
        Me.txt_add.BackColor = System.Drawing.Color.White
        Me.txt_add.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt_add.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txt_add.ForeColor = System.Drawing.Color.DimGray
        Me.txt_add.Location = New System.Drawing.Point(57, 3)
        Me.txt_add.Name = "txt_add"
        Me.txt_add.Size = New System.Drawing.Size(215, 20)
        Me.txt_add.TabIndex = 22
        Me.txt_add.Text = "Type addresses here and hit enter..."
        '
        'remove
        '
        Me.remove.Enabled = False
        Me.remove.Location = New System.Drawing.Point(278, 34)
        Me.remove.Name = "remove"
        Me.remove.Size = New System.Drawing.Size(80, 20)
        Me.remove.TabIndex = 20
        Me.remove.Text = "- Remove"
        Me.remove.UseVisualStyleBackColor = True
        '
        'add
        '
        Me.add.Enabled = False
        Me.add.Location = New System.Drawing.Point(278, 4)
        Me.add.Name = "add"
        Me.add.Size = New System.Drawing.Size(80, 19)
        Me.add.TabIndex = 19
        Me.add.Text = "+ Add"
        Me.add.UseVisualStyleBackColor = True
        '
        'list_cus
        '
        Me.list_cus.BackColor = System.Drawing.Color.White
        Me.list_cus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.list_cus.Cursor = System.Windows.Forms.Cursors.Default
        Me.list_cus.ForeColor = System.Drawing.Color.Black
        Me.list_cus.FormattingEnabled = True
        Me.list_cus.Location = New System.Drawing.Point(57, 34)
        Me.list_cus.Name = "list_cus"
        Me.list_cus.Size = New System.Drawing.Size(215, 67)
        Me.list_cus.TabIndex = 24
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(156, 365)
        Me.DateTimePicker1.MaxDate = New Date(2011, 12, 31, 0, 0, 0, 0)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(149, 20)
        Me.DateTimePicker1.TabIndex = 26
        Me.DateTimePicker1.Value = New Date(2010, 12, 12, 0, 0, 0, 0)
        '
        'minute
        '
        Me.minute.FormattingEnabled = True
        Me.minute.Items.AddRange(New Object() {"00", "10", "20", "30", "40", "50"})
        Me.minute.Location = New System.Drawing.Point(212, 391)
        Me.minute.Name = "minute"
        Me.minute.Size = New System.Drawing.Size(36, 21)
        Me.minute.TabIndex = 32
        '
        'lbl_date
        '
        Me.lbl_date.AutoSize = True
        Me.lbl_date.BackColor = System.Drawing.Color.Transparent
        Me.lbl_date.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_date.ForeColor = System.Drawing.Color.Black
        Me.lbl_date.Location = New System.Drawing.Point(112, 365)
        Me.lbl_date.Name = "lbl_date"
        Me.lbl_date.Size = New System.Drawing.Size(36, 15)
        Me.lbl_date.TabIndex = 29
        Me.lbl_date.Text = "Date:"
        '
        'hour
        '
        Me.hour.FormattingEnabled = True
        Me.hour.Items.AddRange(New Object() {"01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"})
        Me.hour.Location = New System.Drawing.Point(156, 391)
        Me.hour.MaxDropDownItems = 12
        Me.hour.MaxLength = 2
        Me.hour.Name = "hour"
        Me.hour.Size = New System.Drawing.Size(36, 21)
        Me.hour.TabIndex = 31
        '
        'mornin
        '
        Me.mornin.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.mornin.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.mornin.DisplayMember = "PM"
        Me.mornin.FormattingEnabled = True
        Me.mornin.Items.AddRange(New Object() {"AM", "PM"})
        Me.mornin.Location = New System.Drawing.Point(259, 391)
        Me.mornin.MaxDropDownItems = 2
        Me.mornin.Name = "mornin"
        Me.mornin.Size = New System.Drawing.Size(46, 21)
        Me.mornin.TabIndex = 27
        Me.mornin.ValueMember = "PM"
        '
        'lbl_time
        '
        Me.lbl_time.AutoSize = True
        Me.lbl_time.BackColor = System.Drawing.Color.Transparent
        Me.lbl_time.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_time.ForeColor = System.Drawing.Color.Black
        Me.lbl_time.Location = New System.Drawing.Point(112, 394)
        Me.lbl_time.Name = "lbl_time"
        Me.lbl_time.Size = New System.Drawing.Size(38, 15)
        Me.lbl_time.TabIndex = 30
        Me.lbl_time.Text = "Time:"
        '
        'colon
        '
        Me.colon.AutoSize = True
        Me.colon.BackColor = System.Drawing.Color.Transparent
        Me.colon.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.colon.Location = New System.Drawing.Point(195, 393)
        Me.colon.Name = "colon"
        Me.colon.Size = New System.Drawing.Size(11, 13)
        Me.colon.TabIndex = 28
        Me.colon.Text = ":"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.Enabled = False
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Black
        Me.Button1.Location = New System.Drawing.Point(156, 538)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(149, 35)
        Me.Button1.TabIndex = 35
        Me.Button1.Text = "Go Cold Turkey!"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'updatestatus
        '
        Me.updatestatus.AutoSize = True
        Me.updatestatus.BackColor = System.Drawing.Color.Transparent
        Me.updatestatus.Checked = True
        Me.updatestatus.CheckState = System.Windows.Forms.CheckState.Checked
        Me.updatestatus.Location = New System.Drawing.Point(100, 511)
        Me.updatestatus.Name = "updatestatus"
        Me.updatestatus.Size = New System.Drawing.Size(264, 17)
        Me.updatestatus.TabIndex = 33
        Me.updatestatus.Text = "Help me update my Facebook status before I start!"
        Me.updatestatus.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.updatestatus.UseVisualStyleBackColor = False
        '
        'chk_24
        '
        Me.chk_24.AutoSize = True
        Me.chk_24.BackColor = System.Drawing.Color.Transparent
        Me.chk_24.Location = New System.Drawing.Point(317, 395)
        Me.chk_24.Name = "chk_24"
        Me.chk_24.Size = New System.Drawing.Size(82, 17)
        Me.chk_24.TabIndex = 36
        Me.chk_24.Text = "24-hr format"
        Me.chk_24.UseVisualStyleBackColor = False
        '
        'hour24
        '
        Me.hour24.FormattingEnabled = True
        Me.hour24.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"})
        Me.hour24.Location = New System.Drawing.Point(156, 391)
        Me.hour24.MaxDropDownItems = 12
        Me.hour24.MaxLength = 2
        Me.hour24.Name = "hour24"
        Me.hour24.Size = New System.Drawing.Size(36, 21)
        Me.hour24.TabIndex = 37
        Me.hour24.Visible = False
        '
        'timewarning
        '
        Me.timewarning.AutoSize = True
        Me.timewarning.BackColor = System.Drawing.Color.Transparent
        Me.timewarning.ForeColor = System.Drawing.Color.Gray
        Me.timewarning.Location = New System.Drawing.Point(153, 420)
        Me.timewarning.Name = "timewarning"
        Me.timewarning.Size = New System.Drawing.Size(150, 13)
        Me.timewarning.TabIndex = 38
        Me.timewarning.Text = "Don't be scared, select a time."
        '
        'importfile
        '
        Me.importfile.Filter = "Text files (*.txt)|*.txt"
        '
        'coldturkey
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(451, 616)
        Me.Controls.Add(Me.timewarning)
        Me.Controls.Add(Me.chk_24)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.updatestatus)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.minute)
        Me.Controls.Add(Me.lbl_date)
        Me.Controls.Add(Me.mornin)
        Me.Controls.Add(Me.lbl_time)
        Me.Controls.Add(Me.colon)
        Me.Controls.Add(Me.tab_custom)
        Me.Controls.Add(Me.tab_programs)
        Me.Controls.Add(Me.tab_sites)
        Me.Controls.Add(Me.lbl_info)
        Me.Controls.Add(Me.facebookconnect)
        Me.Controls.Add(Me.pan_custom)
        Me.Controls.Add(Me.pan_sites)
        Me.Controls.Add(Me.pan_apps)
        Me.Controls.Add(Me.hour24)
        Me.Controls.Add(Me.hour)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "coldturkey"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cold Turkey"
        CType(Me.lbl_info, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pan_sites.ResumeLayout(False)
        Me.pan_sites.PerformLayout()
        Me.pan_apps.ResumeLayout(False)
        Me.pan_apps.PerformLayout()
        Me.pan_custom.ResumeLayout(False)
        Me.pan_custom.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents srvcont As System.ServiceProcess.ServiceController
    Friend WithEvents facebookconnect As System.Windows.Forms.WebBrowser
    Friend WithEvents lbl_info As System.Windows.Forms.PictureBox
    Friend WithEvents tab_custom As System.Windows.Forms.Label
    Friend WithEvents tab_programs As System.Windows.Forms.Label
    Friend WithEvents tab_sites As System.Windows.Forms.Label
    Friend WithEvents pan_sites As System.Windows.Forms.Panel
    Friend WithEvents chk_shotmail As System.Windows.Forms.CheckBox
    Friend WithEvents chk_stumble As System.Windows.Forms.CheckBox
    Friend WithEvents chk_reddit As System.Windows.Forms.CheckBox
    Friend WithEvents chk_failblog As System.Windows.Forms.CheckBox
    Friend WithEvents chk_ebay As System.Windows.Forms.CheckBox
    Friend WithEvents chk_collegehumor As System.Windows.Forms.CheckBox
    Friend WithEvents chk_agames As System.Windows.Forms.CheckBox
    Friend WithEvents chk_wikipedia As System.Windows.Forms.CheckBox
    Friend WithEvents chk_facebook As System.Windows.Forms.CheckBox
    Friend WithEvents chk_youtube As System.Windows.Forms.CheckBox
    Friend WithEvents chk_twitter As System.Windows.Forms.CheckBox
    Friend WithEvents chk_myspace As System.Windows.Forms.CheckBox
    Friend WithEvents pan_apps As System.Windows.Forms.Panel
    Friend WithEvents chk_warthree As System.Windows.Forms.CheckBox
    Friend WithEvents pan_custom As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txt_add As System.Windows.Forms.TextBox
    Friend WithEvents remove As System.Windows.Forms.Button
    Friend WithEvents add As System.Windows.Forms.Button
    Friend WithEvents list_cus As System.Windows.Forms.ListBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents minute As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_date As System.Windows.Forms.Label
    Friend WithEvents hour As System.Windows.Forms.ComboBox
    Friend WithEvents mornin As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_time As System.Windows.Forms.Label
    Friend WithEvents colon As System.Windows.Forms.Label
    Public WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents updatestatus As System.Windows.Forms.CheckBox
    Friend WithEvents chk_24 As System.Windows.Forms.CheckBox
    Friend WithEvents hour24 As System.Windows.Forms.ComboBox
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents timewarning As System.Windows.Forms.Label
    Friend WithEvents import As System.Windows.Forms.Button
    Friend WithEvents importfile As System.Windows.Forms.OpenFileDialog

End Class
