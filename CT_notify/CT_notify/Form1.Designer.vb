<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.watcher = New System.IO.FileSystemWatcher()
        Me.ServiceController1 = New System.ServiceProcess.ServiceController()
        Me.timer = New System.Timers.Timer()
        CType(Me.watcher, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.timer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'watcher
        '
        Me.watcher.EnableRaisingEvents = True
        Me.watcher.Filter = "done"
        Me.watcher.NotifyFilter = System.IO.NotifyFilters.LastWrite
        Me.watcher.SynchronizingObject = Me
        '
        'ServiceController1
        '
        Me.ServiceController1.ServiceName = "KCTRP"
        '
        'timer
        '
        Me.timer.Enabled = True
        Me.timer.Interval = 5000.0R
        Me.timer.SynchronizingObject = Me
        '
        'Form1
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.Opacity = 0.0R
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        CType(Me.watcher, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.timer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents watcher As System.IO.FileSystemWatcher
    Friend WithEvents ServiceController1 As System.ServiceProcess.ServiceController
    Friend WithEvents timer As System.Timers.Timer

End Class
