using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LandDesignAIDesktop.Forms
{
    public partial class ChatBubble : UserControl
    {
        public enum Sender { User, Bot }
        private readonly Label _label;
        private readonly MaterialButton _copyButton;
        private System.Windows.Forms.ToolTip _tooltip;

        public Sender Who { get; }
        public new string Text => _label.Text;

        public ChatBubble(string text, Sender who, int paneWidth)
        {
            _label = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(paneWidth - 50, 0),
                Text = text,
                Padding = new Padding(8),
                Font = new Font("Segoe UI", 11f, FontStyle.Regular),
            };

            _copyButton = new MaterialButton
            {
                Icon = Properties.Resources.copy,
                AutoSize = true,
                HighEmphasis = false,
                Density = MaterialButton.MaterialButtonDensity.Dense,
                NoAccentTextColor = Color.Transparent,
                Text = string.Empty,
                Margin = new Padding(3, 0, 0, 0),
            };
            _copyButton.Click += (s, e) =>
            {
                Clipboard.SetText(_label.Text);
            };

            _tooltip = new System.Windows.Forms.ToolTip();
            _tooltip.SetToolTip(_copyButton, "Copy");

            BackColor = who == Sender.User ? Color.FromArgb(54, 54, 54) : Color.Transparent;
            _label.ForeColor = who == Sender.User ? Color.FromArgb(129, 188, 0) : Color.FromKnownColor(KnownColor.ButtonFace);

            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            var layout = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = false
            };
            layout.Controls.Add(_label);
            layout.Controls.Add(_copyButton);

            Controls.Add(layout);

            Who = who;
            Dock = DockStyle.Top;
            ParentChanged += OnParentChanged;
        }

        public void OnParentChanged(object? sender, EventArgs e)
        {
            if (Parent != null)
            {
                Parent.Resize += (s, args) => ResizeToParentWidth();
                ResizeToParentWidth();
            }
        }

        public void ResizeToParentWidth()
        {
            if (Parent == null) return;
            _label.MaximumSize = new Size(Parent.ClientSize.Width - 50, 0);
            PerformLayout();
        }

        public void UpdateText(string newText)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(UpdateText), newText);
                return;
            }
            _label.Text = newText;
            _label.Refresh();
            PerformLayout();
        }
    }
}