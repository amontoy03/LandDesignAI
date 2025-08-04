using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace LandDesignAIDesktop
{
    public partial class ChatPanel : UserControl
    {
        // Events for Form1 to subscribe to
        public event EventHandler<string>? SendMessageRequested;
        public event EventHandler<bool>? SpellingCorrectionToggled;
        public event EventHandler? AddFilesRequested;



        public ChatPanel()
        {
            InitializeComponent();

            var skin = MaterialSkinManager.Instance;
            if (FindForm() is MaterialForm materialForm)
            {
                skin.AddFormToManage(materialForm);
            }

            materialTextBox_PromptBox.KeyDown += MaterialTextBox_PromptBox_KeyDown;
            materialButton_AddFiles.Click += MaterialButton_AddFiles;

            if (materialComboBox_Model.Items.Count > 0)
                materialComboBox_Model.SelectedIndex = 0;

            if (materialComboBox_Tone.Items.Count > 0)
                materialComboBox_Tone.SelectedIndex = 0;

            if (materialComboBox_Role.Items.Count > 0)
                materialComboBox_Role.SelectedIndex = 0;

        }

        private async void MaterialTextBox_PromptBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(materialTextBox_PromptBox.Text))
            {
                string message = materialTextBox_PromptBox.Text;

                // Spell correction if switch is ON
                if (materialSwitch_SpellingCorrection.Checked)
                {
                    try
                    {
                        string corrected = await SpellChecker.CorrectAsync(message);
                        materialTextBox_PromptBox.Text = corrected; // Show corrected text
                        message = corrected;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Spell check failed: " + ex.Message,
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                // Send to Form1
                SendMessageRequested?.Invoke(this, message);

                materialTextBox_PromptBox.Clear();
                e.SuppressKeyPress = true;
            }
        }



        private void MaterialSwitch_SpellingCorrection_CheckedChanged(object? sender, EventArgs e)
        {
            SpellingCorrectionToggled?.Invoke(this, materialSwitch_SpellingCorrection.Checked);
        }

        private void MaterialButton_AddFiles(object? sender, EventArgs e)
        {
            // Raise event to notify Form1
            AddFilesRequested?.Invoke(this, EventArgs.Empty);
        }

        private void materialComboBox_Model_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void materialComboBox_Model_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        public void SelectThisChatTab()
        {
            materialTabControl_Left.SelectedTab = tabPage_ThisChat;
        }

        // Helper to access FlowLayoutPanel for adding ChatBubbles
        public FlowLayoutPanel ChatFlow => flowLayoutPanel_Chat;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Get current MaterialSkin theme colors
            var skin = MaterialSkin.MaterialSkinManager.Instance;
            var backColor = skin.Theme == MaterialSkin.MaterialSkinManager.Themes.DARK
                ? Color.FromArgb(50, 50, 50) // Dark theme
                : Color.White;               // Light theme

            // Set TabControl background
            materialTabControl_Left.BackColor = backColor;

            // Set all TabPage backgrounds
            foreach (TabPage page in materialTabControl_Left.TabPages)
            {
                page.UseVisualStyleBackColor = false;  // Force BackColor to apply
                page.BackColor = backColor;
            }

            // Set all key panels and layouts
            this.BackColor = backColor;
            panel1.BackColor = backColor;
            flowLayoutPanel_Chat.BackColor = backColor;
            flowLayoutPanel2.BackColor = backColor;
            flowLayoutPanel_Voice.BackColor = backColor;
        }

        private void materialSwitch1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel_Voice_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}


