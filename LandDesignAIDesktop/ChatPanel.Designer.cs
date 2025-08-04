using MaterialSkin.Controls;

namespace LandDesignAIDesktop
{
    partial class ChatPanel
    {
        private System.ComponentModel.IContainer components = null;

        
        private MaterialSkin.Controls.MaterialComboBox materialComboBox_Model;
        private MaterialSkin.Controls.MaterialComboBox materialComboBox_Tone;
        private MaterialSkin.Controls.MaterialComboBox materialComboBox_Role;
        private MaterialSkin.Controls.MaterialCard materialCard_BottomBar;
        private MaterialSkin.Controls.MaterialCard materialCard_Bottom;
        private MaterialSkin.Controls.MaterialSwitch materialSwitch_SpellingCorrection;
        private MaterialSkin.Controls.MaterialButton materialButton_AddFiles;
        private MaterialSkin.Controls.MaterialTextBox2 materialTextBox_PromptBox;
        

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            materialTabControl_Left = new MaterialTabControl();
            tabPage_Voice = new TabPage();
            tableLayout_ThisChat = new TableLayoutPanel();
            materialCard1 = new MaterialCard();
            materialCard2 = new MaterialCard();
            materialSwitch1 = new MaterialSwitch();
            materialButton1 = new MaterialButton();
            materialTextBox21 = new MaterialTextBox2();
            label2 = new Label();
            flowLayoutPanel2 = new FlowLayoutPanel();
            label_userVolume = new Label();
            volumeMeter_User = new NAudio.Gui.VolumeMeter();
            materialButton_Talk = new MaterialButton();
            flowLayoutPanel_Voice = new FlowLayoutPanel();
            tabPage_NewChat = new TabPage();
            tabPage_ThisChat = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            materialCard_BottomBar = new MaterialCard();
            materialCard_Bottom = new MaterialCard();
            materialSwitch_SpellingCorrection = new MaterialSwitch();
            materialButton_AddFiles = new MaterialButton();
            materialTextBox_PromptBox = new MaterialTextBox2();
            label1 = new Label();
            flowLayoutPanel_Chat = new FlowLayoutPanel();
            panel1 = new Panel();
            materialComboBox_Role = new MaterialComboBox();
            materialComboBox_Tone = new MaterialComboBox();
            materialComboBox_Model = new MaterialComboBox();
            pictureBox_Settings = new PictureBox();
            panel_Logo = new Panel();
            pictureBox1 = new PictureBox();
            imageList1 = new ImageList(components);
            materialTabControl_Left.SuspendLayout();
            tabPage_Voice.SuspendLayout();
            tableLayout_ThisChat.SuspendLayout();
            materialCard1.SuspendLayout();
            materialCard2.SuspendLayout();
            tabPage_ThisChat.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            materialCard_BottomBar.SuspendLayout();
            materialCard_Bottom.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Settings).BeginInit();
            panel_Logo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // materialTabControl_Left
            // 
            materialTabControl_Left.Controls.Add(tabPage_Voice);
            materialTabControl_Left.Controls.Add(tabPage_NewChat);
            materialTabControl_Left.Controls.Add(tabPage_ThisChat);
            materialTabControl_Left.Depth = 0;
            materialTabControl_Left.Dock = DockStyle.Fill;
            materialTabControl_Left.Location = new Point(0, 49);
            materialTabControl_Left.MouseState = MaterialSkin.MouseState.HOVER;
            materialTabControl_Left.Multiline = true;
            materialTabControl_Left.Name = "materialTabControl_Left";
            materialTabControl_Left.SelectedIndex = 0;
            materialTabControl_Left.Size = new Size(785, 854);
            materialTabControl_Left.TabIndex = 9;
            // 
            // tabPage_Voice
            // 
            tabPage_Voice.Controls.Add(tableLayout_ThisChat);
            tabPage_Voice.Controls.Add(label_userVolume);
            tabPage_Voice.Controls.Add(volumeMeter_User);
            tabPage_Voice.Controls.Add(materialButton_Talk);
            tabPage_Voice.Controls.Add(flowLayoutPanel_Voice);
            tabPage_Voice.Location = new Point(4, 24);
            tabPage_Voice.Name = "tabPage_Voice";
            tabPage_Voice.Padding = new Padding(3);
            tabPage_Voice.Size = new Size(777, 826);
            tabPage_Voice.TabIndex = 2;
            tabPage_Voice.Text = "Voice";
            tabPage_Voice.UseVisualStyleBackColor = true;
            // 
            // tableLayout_ThisChat
            // 
            tableLayout_ThisChat.AutoScroll = true;
            tableLayout_ThisChat.AutoSize = true;
            tableLayout_ThisChat.ColumnCount = 3;
            tableLayout_ThisChat.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75F));
            tableLayout_ThisChat.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayout_ThisChat.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75F));
            tableLayout_ThisChat.Controls.Add(materialCard1, 1, 1);
            tableLayout_ThisChat.Controls.Add(label2, 1, 2);
            tableLayout_ThisChat.Controls.Add(flowLayoutPanel2, 1, 0);
            tableLayout_ThisChat.Dock = DockStyle.Bottom;
            tableLayout_ThisChat.Location = new Point(3, 14);
            tableLayout_ThisChat.Margin = new Padding(0);
            tableLayout_ThisChat.Name = "tableLayout_ThisChat";
            tableLayout_ThisChat.RowCount = 3;
            tableLayout_ThisChat.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayout_ThisChat.RowStyles.Add(new RowStyle(SizeType.Absolute, 117F));
            tableLayout_ThisChat.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tableLayout_ThisChat.Size = new Size(771, 809);
            tableLayout_ThisChat.TabIndex = 12;
            // 
            // materialCard1
            // 
            materialCard1.AutoSize = true;
            materialCard1.BackColor = Color.FromArgb(255, 255, 255);
            materialCard1.Controls.Add(materialCard2);
            materialCard1.Controls.Add(materialTextBox21);
            materialCard1.Depth = 0;
            materialCard1.Dock = DockStyle.Fill;
            materialCard1.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new Point(75, 638);
            materialCard1.Margin = new Padding(0);
            materialCard1.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(14, 15, 14, 15);
            materialCard1.Size = new Size(621, 117);
            materialCard1.TabIndex = 1;
            // 
            // materialCard2
            // 
            materialCard2.BackColor = Color.FromArgb(255, 255, 255);
            materialCard2.Controls.Add(materialSwitch1);
            materialCard2.Controls.Add(materialButton1);
            materialCard2.Depth = 0;
            materialCard2.Dock = DockStyle.Fill;
            materialCard2.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard2.Location = new Point(14, 63);
            materialCard2.Margin = new Padding(0);
            materialCard2.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard2.Name = "materialCard2";
            materialCard2.Padding = new Padding(5);
            materialCard2.Size = new Size(593, 39);
            materialCard2.TabIndex = 12;
            // 
            // materialSwitch1
            // 
            materialSwitch1.AutoSize = true;
            materialSwitch1.Checked = true;
            materialSwitch1.CheckState = CheckState.Checked;
            materialSwitch1.Depth = 0;
            materialSwitch1.Dock = DockStyle.Right;
            materialSwitch1.Location = new Point(397, 5);
            materialSwitch1.Margin = new Padding(0);
            materialSwitch1.MouseLocation = new Point(-1, -1);
            materialSwitch1.MouseState = MaterialSkin.MouseState.HOVER;
            materialSwitch1.Name = "materialSwitch1";
            materialSwitch1.Ripple = true;
            materialSwitch1.Size = new Size(191, 29);
            materialSwitch1.TabIndex = 3;
            materialSwitch1.Text = "Spelling Correction";
            materialSwitch1.UseVisualStyleBackColor = true;
            materialSwitch1.CheckedChanged += materialSwitch1_CheckedChanged;
            // 
            // materialButton1
            // 
            materialButton1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton1.Density = MaterialButton.MaterialButtonDensity.Dense;
            materialButton1.Depth = 0;
            materialButton1.Dock = DockStyle.Left;
            materialButton1.HighEmphasis = true;
            materialButton1.Icon = null;
            materialButton1.Location = new Point(5, 5);
            materialButton1.Margin = new Padding(0);
            materialButton1.MouseState = MaterialSkin.MouseState.HOVER;
            materialButton1.Name = "materialButton1";
            materialButton1.NoAccentTextColor = Color.Empty;
            materialButton1.Size = new Size(92, 29);
            materialButton1.TabIndex = 0;
            materialButton1.Text = "Add Files";
            materialButton1.Type = MaterialButton.MaterialButtonType.Contained;
            materialButton1.UseAccentColor = false;
            materialButton1.UseVisualStyleBackColor = true;
            // 
            // materialTextBox21
            // 
            materialTextBox21.AnimateReadOnly = false;
            materialTextBox21.BackgroundImageLayout = ImageLayout.None;
            materialTextBox21.CharacterCasing = CharacterCasing.Normal;
            materialTextBox21.Depth = 0;
            materialTextBox21.Dock = DockStyle.Top;
            materialTextBox21.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialTextBox21.HideSelection = true;
            materialTextBox21.Hint = "Do not disclose confidential project or personal information...";
            materialTextBox21.LeadingIcon = null;
            materialTextBox21.Location = new Point(14, 15);
            materialTextBox21.MaxLength = 32767;
            materialTextBox21.MouseState = MaterialSkin.MouseState.OUT;
            materialTextBox21.Name = "materialTextBox21";
            materialTextBox21.PasswordChar = '\0';
            materialTextBox21.PrefixSuffixText = null;
            materialTextBox21.ReadOnly = false;
            materialTextBox21.RightToLeft = RightToLeft.No;
            materialTextBox21.SelectedText = "";
            materialTextBox21.SelectionLength = 0;
            materialTextBox21.SelectionStart = 0;
            materialTextBox21.ShortcutsEnabled = true;
            materialTextBox21.Size = new Size(593, 48);
            materialTextBox21.TabIndex = 11;
            materialTextBox21.TabStop = false;
            materialTextBox21.TextAlign = HorizontalAlignment.Left;
            materialTextBox21.TrailingIcon = null;
            materialTextBox21.UseSystemPasswordChar = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.Location = new Point(78, 755);
            label2.Name = "label2";
            label2.Size = new Size(615, 54);
            label2.TabIndex = 7;
            label2.Text = "AI can make mistakes. Check important info.";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoScroll = true;
            flowLayoutPanel2.BackColor = Color.White;
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.Location = new Point(78, 27);
            flowLayoutPanel2.Margin = new Padding(3, 27, 3, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Padding = new Padding(2);
            flowLayoutPanel2.Size = new Size(615, 608);
            flowLayoutPanel2.TabIndex = 6;
            flowLayoutPanel2.WrapContents = false;
            // 
            // label_userVolume
            // 
            label_userVolume.AutoSize = true;
            label_userVolume.Dock = DockStyle.Top;
            label_userVolume.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label_userVolume.Location = new Point(3, 656);
            label_userVolume.Name = "label_userVolume";
            label_userVolume.Size = new Size(57, 13);
            label_userVolume.TabIndex = 6;
            label_userVolume.Text = "Initiating...";
            label_userVolume.TextAlign = ContentAlignment.TopCenter;
            label_userVolume.Visible = false;
            // 
            // volumeMeter_User
            // 
            volumeMeter_User.Amplitude = 0F;
            volumeMeter_User.Dock = DockStyle.Top;
            volumeMeter_User.ForeColor = SystemColors.Control;
            volumeMeter_User.Location = new Point(3, 646);
            volumeMeter_User.MaxDb = 18F;
            volumeMeter_User.MinDb = -60F;
            volumeMeter_User.Name = "volumeMeter_User";
            volumeMeter_User.Orientation = Orientation.Horizontal;
            volumeMeter_User.Size = new Size(771, 10);
            volumeMeter_User.TabIndex = 5;
            volumeMeter_User.Text = "volumeMeter1";
            volumeMeter_User.Visible = false;
            // 
            // materialButton_Talk
            // 
            materialButton_Talk.AutoSize = false;
            materialButton_Talk.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton_Talk.Density = MaterialButton.MaterialButtonDensity.Default;
            materialButton_Talk.Depth = 0;
            materialButton_Talk.Dock = DockStyle.Top;
            materialButton_Talk.HighEmphasis = true;
            materialButton_Talk.Icon = null;
            materialButton_Talk.Location = new Point(3, 586);
            materialButton_Talk.Margin = new Padding(25);
            materialButton_Talk.MouseState = MaterialSkin.MouseState.HOVER;
            materialButton_Talk.Name = "materialButton_Talk";
            materialButton_Talk.NoAccentTextColor = Color.Empty;
            materialButton_Talk.Padding = new Padding(30);
            materialButton_Talk.Size = new Size(771, 60);
            materialButton_Talk.TabIndex = 4;
            materialButton_Talk.Text = "Push To Talk";
            materialButton_Talk.Type = MaterialButton.MaterialButtonType.Contained;
            materialButton_Talk.UseAccentColor = false;
            materialButton_Talk.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel_Voice
            // 
            flowLayoutPanel_Voice.Dock = DockStyle.Top;
            flowLayoutPanel_Voice.Location = new Point(3, 3);
            flowLayoutPanel_Voice.Margin = new Padding(3, 3, 3, 10);
            flowLayoutPanel_Voice.Name = "flowLayoutPanel_Voice";
            flowLayoutPanel_Voice.Size = new Size(771, 583);
            flowLayoutPanel_Voice.TabIndex = 3;
            flowLayoutPanel_Voice.Paint += flowLayoutPanel_Voice_Paint;
            // 
            // tabPage_NewChat
            // 
            tabPage_NewChat.Location = new Point(4, 24);
            tabPage_NewChat.Name = "tabPage_NewChat";
            tabPage_NewChat.Padding = new Padding(3);
            tabPage_NewChat.Size = new Size(777, 826);
            tabPage_NewChat.TabIndex = 1;
            tabPage_NewChat.Text = "New Chat";
            // 
            // tabPage_ThisChat
            // 
            tabPage_ThisChat.Controls.Add(tableLayoutPanel1);
            tabPage_ThisChat.Location = new Point(4, 24);
            tabPage_ThisChat.Name = "tabPage_ThisChat";
            tabPage_ThisChat.Padding = new Padding(3);
            tabPage_ThisChat.Size = new Size(777, 826);
            tabPage_ThisChat.TabIndex = 0;
            tabPage_ThisChat.Text = "This Chat";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75F));
            tableLayoutPanel1.Controls.Add(materialCard_BottomBar, 1, 1);
            tableLayoutPanel1.Controls.Add(label1, 1, 2);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel_Chat, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 117F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tableLayoutPanel1.Size = new Size(771, 820);
            tableLayoutPanel1.TabIndex = 8;
            // 
            // materialCard_BottomBar
            // 
            materialCard_BottomBar.AutoSize = true;
            materialCard_BottomBar.BackColor = Color.FromArgb(255, 255, 255);
            materialCard_BottomBar.Controls.Add(materialCard_Bottom);
            materialCard_BottomBar.Controls.Add(materialTextBox_PromptBox);
            materialCard_BottomBar.Depth = 0;
            materialCard_BottomBar.Dock = DockStyle.Fill;
            materialCard_BottomBar.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard_BottomBar.Location = new Point(75, 649);
            materialCard_BottomBar.Margin = new Padding(0);
            materialCard_BottomBar.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard_BottomBar.Name = "materialCard_BottomBar";
            materialCard_BottomBar.Padding = new Padding(14, 15, 14, 15);
            materialCard_BottomBar.Size = new Size(621, 117);
            materialCard_BottomBar.TabIndex = 1;
            // 
            // materialCard_Bottom
            // 
            materialCard_Bottom.BackColor = Color.FromArgb(255, 255, 255);
            materialCard_Bottom.Controls.Add(materialSwitch_SpellingCorrection);
            materialCard_Bottom.Controls.Add(materialButton_AddFiles);
            materialCard_Bottom.Depth = 0;
            materialCard_Bottom.Dock = DockStyle.Fill;
            materialCard_Bottom.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialCard_Bottom.Location = new Point(14, 63);
            materialCard_Bottom.Margin = new Padding(0);
            materialCard_Bottom.MouseState = MaterialSkin.MouseState.HOVER;
            materialCard_Bottom.Name = "materialCard_Bottom";
            materialCard_Bottom.Padding = new Padding(5);
            materialCard_Bottom.Size = new Size(593, 39);
            materialCard_Bottom.TabIndex = 12;
            // 
            // materialSwitch_SpellingCorrection
            // 
            materialSwitch_SpellingCorrection.AutoSize = true;
            materialSwitch_SpellingCorrection.Checked = true;
            materialSwitch_SpellingCorrection.CheckState = CheckState.Checked;
            materialSwitch_SpellingCorrection.Depth = 0;
            materialSwitch_SpellingCorrection.Dock = DockStyle.Right;
            materialSwitch_SpellingCorrection.Location = new Point(397, 5);
            materialSwitch_SpellingCorrection.Margin = new Padding(0);
            materialSwitch_SpellingCorrection.MouseLocation = new Point(-1, -1);
            materialSwitch_SpellingCorrection.MouseState = MaterialSkin.MouseState.HOVER;
            materialSwitch_SpellingCorrection.Name = "materialSwitch_SpellingCorrection";
            materialSwitch_SpellingCorrection.Ripple = true;
            materialSwitch_SpellingCorrection.Size = new Size(191, 29);
            materialSwitch_SpellingCorrection.TabIndex = 3;
            materialSwitch_SpellingCorrection.Text = "Spelling Correction";
            materialSwitch_SpellingCorrection.UseVisualStyleBackColor = true;
            materialSwitch_SpellingCorrection.CheckedChanged += MaterialSwitch_SpellingCorrection_CheckedChanged;
            // 
            // materialButton_AddFiles
            // 
            materialButton_AddFiles.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            materialButton_AddFiles.Density = MaterialButton.MaterialButtonDensity.Dense;
            materialButton_AddFiles.Depth = 0;
            materialButton_AddFiles.Dock = DockStyle.Left;
            materialButton_AddFiles.HighEmphasis = true;
            materialButton_AddFiles.Icon = null;
            materialButton_AddFiles.Location = new Point(5, 5);
            materialButton_AddFiles.Margin = new Padding(0);
            materialButton_AddFiles.MouseState = MaterialSkin.MouseState.HOVER;
            materialButton_AddFiles.Name = "materialButton_AddFiles";
            materialButton_AddFiles.NoAccentTextColor = Color.Empty;
            materialButton_AddFiles.Size = new Size(92, 29);
            materialButton_AddFiles.TabIndex = 0;
            materialButton_AddFiles.Text = "Add Files";
            materialButton_AddFiles.Type = MaterialButton.MaterialButtonType.Contained;
            materialButton_AddFiles.UseAccentColor = false;
            materialButton_AddFiles.UseVisualStyleBackColor = true;
            // 
            // materialTextBox_PromptBox
            // 
            materialTextBox_PromptBox.AnimateReadOnly = false;
            materialTextBox_PromptBox.BackgroundImageLayout = ImageLayout.None;
            materialTextBox_PromptBox.CharacterCasing = CharacterCasing.Normal;
            materialTextBox_PromptBox.Depth = 0;
            materialTextBox_PromptBox.Dock = DockStyle.Top;
            materialTextBox_PromptBox.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            materialTextBox_PromptBox.HideSelection = true;
            materialTextBox_PromptBox.Hint = "Do not disclose confidential project or personal information...";
            materialTextBox_PromptBox.LeadingIcon = null;
            materialTextBox_PromptBox.Location = new Point(14, 15);
            materialTextBox_PromptBox.MaxLength = 32767;
            materialTextBox_PromptBox.MouseState = MaterialSkin.MouseState.OUT;
            materialTextBox_PromptBox.Name = "materialTextBox_PromptBox";
            materialTextBox_PromptBox.PasswordChar = '\0';
            materialTextBox_PromptBox.PrefixSuffixText = null;
            materialTextBox_PromptBox.ReadOnly = false;
            materialTextBox_PromptBox.RightToLeft = RightToLeft.No;
            materialTextBox_PromptBox.SelectedText = "";
            materialTextBox_PromptBox.SelectionLength = 0;
            materialTextBox_PromptBox.SelectionStart = 0;
            materialTextBox_PromptBox.ShortcutsEnabled = true;
            materialTextBox_PromptBox.Size = new Size(593, 48);
            materialTextBox_PromptBox.TabIndex = 11;
            materialTextBox_PromptBox.TabStop = false;
            materialTextBox_PromptBox.TextAlign = HorizontalAlignment.Left;
            materialTextBox_PromptBox.TrailingIcon = null;
            materialTextBox_PromptBox.UseSystemPasswordChar = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 8.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(78, 766);
            label1.Name = "label1";
            label1.Size = new Size(615, 54);
            label1.TabIndex = 7;
            label1.Text = "AI can make mistakes. Check important info.";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // flowLayoutPanel_Chat
            // 
            flowLayoutPanel_Chat.AutoScroll = true;
            flowLayoutPanel_Chat.BackColor = Color.White;
            flowLayoutPanel_Chat.Dock = DockStyle.Fill;
            flowLayoutPanel_Chat.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel_Chat.Location = new Point(78, 27);
            flowLayoutPanel_Chat.Margin = new Padding(3, 27, 3, 3);
            flowLayoutPanel_Chat.Name = "flowLayoutPanel_Chat";
            flowLayoutPanel_Chat.Padding = new Padding(2);
            flowLayoutPanel_Chat.Size = new Size(615, 619);
            flowLayoutPanel_Chat.TabIndex = 6;
            flowLayoutPanel_Chat.WrapContents = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(materialComboBox_Role);
            panel1.Controls.Add(materialComboBox_Tone);
            panel1.Controls.Add(materialComboBox_Model);
            panel1.Controls.Add(pictureBox_Settings);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(785, 49);
            panel1.TabIndex = 10;
            // 
            // materialComboBox_Role
            // 
            materialComboBox_Role.AutoResize = false;
            materialComboBox_Role.BackColor = Color.Black;
            materialComboBox_Role.Depth = 0;
            materialComboBox_Role.DrawMode = DrawMode.OwnerDrawVariable;
            materialComboBox_Role.DropDownHeight = 174;
            materialComboBox_Role.DropDownStyle = ComboBoxStyle.DropDownList;
            materialComboBox_Role.DropDownWidth = 186;
            materialComboBox_Role.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialComboBox_Role.ForeColor = Color.White;
            materialComboBox_Role.FormattingEnabled = true;
            materialComboBox_Role.Hint = "Role";
            materialComboBox_Role.IntegralHeight = false;
            materialComboBox_Role.ItemHeight = 43;
            materialComboBox_Role.Items.AddRange(new object[] { "Civil Engineer", "Landscape Architect", "Urban Planner", "CAD Technician" });
            materialComboBox_Role.Location = new Point(294, 0);
            materialComboBox_Role.Margin = new Padding(1);
            materialComboBox_Role.MaxDropDownItems = 4;
            materialComboBox_Role.MaximumSize = new Size(141, 0);
            materialComboBox_Role.MinimumSize = new Size(141, 0);
            materialComboBox_Role.MouseState = MaterialSkin.MouseState.OUT;
            materialComboBox_Role.Name = "materialComboBox_Role";
            materialComboBox_Role.Size = new Size(141, 49);
            materialComboBox_Role.StartIndex = 0;
            materialComboBox_Role.TabIndex = 7;
            // 
            // materialComboBox_Tone
            // 
            materialComboBox_Tone.AutoResize = false;
            materialComboBox_Tone.BackColor = Color.FromArgb(255, 255, 255);
            materialComboBox_Tone.Depth = 0;
            materialComboBox_Tone.DrawMode = DrawMode.OwnerDrawVariable;
            materialComboBox_Tone.DropDownHeight = 174;
            materialComboBox_Tone.DropDownStyle = ComboBoxStyle.DropDownList;
            materialComboBox_Tone.DropDownWidth = 131;
            materialComboBox_Tone.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialComboBox_Tone.ForeColor = Color.FromArgb(222, 0, 0, 0);
            materialComboBox_Tone.FormattingEnabled = true;
            materialComboBox_Tone.Hint = "Tone";
            materialComboBox_Tone.IntegralHeight = false;
            materialComboBox_Tone.ItemHeight = 43;
            materialComboBox_Tone.Items.AddRange(new object[] { "Professional", "Concise", "Thorough", "Comical", "Sarcastic" });
            materialComboBox_Tone.Location = new Point(147, 0);
            materialComboBox_Tone.Margin = new Padding(5);
            materialComboBox_Tone.MaxDropDownItems = 4;
            materialComboBox_Tone.MaximumSize = new Size(141, 0);
            materialComboBox_Tone.MinimumSize = new Size(141, 0);
            materialComboBox_Tone.MouseState = MaterialSkin.MouseState.OUT;
            materialComboBox_Tone.Name = "materialComboBox_Tone";
            materialComboBox_Tone.Size = new Size(141, 49);
            materialComboBox_Tone.StartIndex = 0;
            materialComboBox_Tone.TabIndex = 6;
            // 
            // materialComboBox_Model
            // 
            materialComboBox_Model.AutoResize = false;
            materialComboBox_Model.BackColor = Color.Black;
            materialComboBox_Model.Depth = 0;
            materialComboBox_Model.Dock = DockStyle.Left;
            materialComboBox_Model.DrawMode = DrawMode.OwnerDrawVariable;
            materialComboBox_Model.DropDownHeight = 174;
            materialComboBox_Model.DropDownStyle = ComboBoxStyle.DropDownList;
            materialComboBox_Model.DropDownWidth = 121;
            materialComboBox_Model.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            materialComboBox_Model.ForeColor = Color.White;
            materialComboBox_Model.FormattingEnabled = true;
            materialComboBox_Model.Hint = "AI Model";
            materialComboBox_Model.IntegralHeight = false;
            materialComboBox_Model.ItemHeight = 43;
            materialComboBox_Model.Items.AddRange(new object[] { "gpt-4o", "gpt-4-turbo", "o3", "o3-mini", "grok-4-0709", "grok-3", "grok-3-mini", "grok-3-mini-fast" });
            materialComboBox_Model.Location = new Point(0, 0);
            materialComboBox_Model.Margin = new Padding(1);
            materialComboBox_Model.MaxDropDownItems = 4;
            materialComboBox_Model.MaximumSize = new Size(141, 0);
            materialComboBox_Model.MinimumSize = new Size(141, 0);
            materialComboBox_Model.MouseState = MaterialSkin.MouseState.OUT;
            materialComboBox_Model.Name = "materialComboBox_Model";
            materialComboBox_Model.Size = new Size(141, 49);
            materialComboBox_Model.StartIndex = 0;
            materialComboBox_Model.TabIndex = 5;
            // 
            // pictureBox_Settings
            // 
            pictureBox_Settings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox_Settings.BackgroundImage = Properties.Resources.settings;
            pictureBox_Settings.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox_Settings.Location = new Point(754, 3);
            pictureBox_Settings.Name = "pictureBox_Settings";
            pictureBox_Settings.Size = new Size(22, 20);
            pictureBox_Settings.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_Settings.TabIndex = 4;
            pictureBox_Settings.TabStop = false;
            // 
            // panel_Logo
            // 
            panel_Logo.BackColor = Color.Transparent;
            panel_Logo.Controls.Add(pictureBox1);
            panel_Logo.Dock = DockStyle.Bottom;
            panel_Logo.Location = new Point(0, 869);
            panel_Logo.Margin = new Padding(3, 3, 10, 3);
            panel_Logo.Name = "panel_Logo";
            panel_Logo.Padding = new Padding(500, 1, 1, 1);
            panel_Logo.Size = new Size(785, 34);
            panel_Logo.TabIndex = 10;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Dock = DockStyle.Right;
            pictureBox1.Image = Properties.Resources.LDI_logo_wordmark_white;
            pictureBox1.InitialImage = null;
            pictureBox1.Location = new Point(617, 1);
            pictureBox1.Margin = new Padding(3, 3, 10, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(167, 32);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // ChatPanel
            // 
            BackColor = Color.White;
            Controls.Add(materialTabControl_Left);
            Controls.Add(panel1);
            Name = "ChatPanel";
            Size = new Size(785, 903);
            materialTabControl_Left.ResumeLayout(false);
            tabPage_Voice.ResumeLayout(false);
            tabPage_Voice.PerformLayout();
            tableLayout_ThisChat.ResumeLayout(false);
            tableLayout_ThisChat.PerformLayout();
            materialCard1.ResumeLayout(false);
            materialCard2.ResumeLayout(false);
            materialCard2.PerformLayout();
            tabPage_ThisChat.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            materialCard_BottomBar.ResumeLayout(false);
            materialCard_Bottom.ResumeLayout(false);
            materialCard_Bottom.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox_Settings).EndInit();
            panel_Logo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }
        private MaterialSkin.Controls.MaterialTabControl materialTabControl_Left;
        private TabPage tabPage_Voice;
        private TableLayoutPanel tableLayout_ThisChat;
        private MaterialSkin.Controls.MaterialCard materialCard1;
        private MaterialSkin.Controls.MaterialCard materialCard2;
        private MaterialSkin.Controls.MaterialSwitch materialSwitch1;
        private MaterialSkin.Controls.MaterialButton materialButton1;
        private MaterialSkin.Controls.MaterialTextBox2 materialTextBox21;
        private Label label2;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label label_userVolume;
        private NAudio.Gui.VolumeMeter volumeMeter_User;
        private MaterialSkin.Controls.MaterialButton materialButton_Talk;
        private FlowLayoutPanel flowLayoutPanel_Voice;
        private TabPage tabPage_NewChat;
        private TabPage tabPage_ThisChat;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private FlowLayoutPanel flowLayoutPanel_Chat;
        private Panel panel1;
        private PictureBox pictureBox_Settings;
        private Panel panel_Logo;
        private PictureBox pictureBox1;
        private ImageList imageList1;
    }
}