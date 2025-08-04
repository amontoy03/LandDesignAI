using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LandDesignAIDesktop
{
    /// <summary>
    /// Provides static utility methods for performing common UI actions on Windows Forms.
    /// </summary>
    public static class LandDesignUIFunctions
    {
        /// <summary>
        /// Imports the SendMessage function from user32.dll to send messages to a window.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        /// Imports the ReleaseCapture function from user32.dll to release mouse capture.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        /// <summary>
        /// Maximizes the specified form.
        /// </summary>
        /// <param name="form">The form to maximize.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="form"/> is null.</exception>
        public static void Maximize(Form form)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            form.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Minimizes the specified form.
        /// </summary>
        /// <param name="form">The form to minimize.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="form"/> is null.</exception>
        public static void Minimize(Form form)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            form.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Restores the specified form to its normal state.
        /// </summary>
        /// <param name="form">The form to restore.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="form"/> is null.</exception>
        public static void RestoreToNormal(Form form)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            form.WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// Closes the specified form.
        /// </summary>
        /// <param name="form">The form to close.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="form"/> is null.</exception>
        public static void Close(Form form)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            form.Close();
        }

        /// <summary>
        /// Hides the specified form.
        /// </summary>
        /// <param name="form">The form to hide.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="form"/> is null.</exception>
        public static void Hide(Form form)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            form.Hide();
        }

        /// <summary>
        /// Shows the specified form.
        /// </summary>
        /// <param name="form">The form to show.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="form"/> is null.</exception>
        public static void Show(Form form)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            form.Show();
        }

        /// <summary>
        /// Brings the specified form to the front.
        /// </summary>
        /// <param name="form">The form to bring to the front.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="form"/> is null.</exception>
        public static void BringToFront(Form form)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            form.BringToFront();
        }

        /// <summary>
        /// Sets the title of the specified form.
        /// </summary>
        /// <param name="form">The form whose title to set.</param>
        /// <param name="title">The new title for the form.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="form"/> is null.</exception>
        public static void SetTitle(Form form, string title)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            form.Text = title ?? string.Empty;
        }

        /// <summary>
        /// Sets the background color of the specified form.
        /// </summary>
        /// <param name="form">The form whose background color to set.</param>
        /// <param name="color">The new background color.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="form"/> is null.</exception>
        public static void SetBackgroundColor(Form form, System.Drawing.Color color)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            form.BackColor = color;
        }

        /// <summary>
        /// Resizes the specified form to the given width and height.
        /// </summary>
        /// <param name="form">The form to resize.</param>
        /// <param name="width">The new width of the form.</param>
        /// <param name="height">The new height of the form.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="form"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="width"/> or <paramref name="height"/> is less than or equal to zero.</exception>
        public static void Resize(Form form, int width, int height)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width), "Width must be positive.");
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height), "Height must be positive.");
            form.Size = new System.Drawing.Size(width, height);
        }

        /// <summary>
        /// Moves the specified form to the given coordinates.
        /// </summary>
        /// <param name="form">The form to move.</param>
        /// <param name="x">The new x-coordinate of the form.</param>
        /// <param name="y">The new y-coordinate of the form.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="form"/> is null.</exception>
        public static void Move(Form form, int x, int y)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            form.Location = new System.Drawing.Point(x, y);
        }

        /// <summary>
        /// Enables dragging of the form using the mouse.
        /// </summary>
        /// <param name="form">The form to enable dragging for.</param>
        /// <param name="e">The mouse event arguments.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="form"/> or <paramref name="e"/> is null.</exception>
        public static void DragForm(Form form, MouseEventArgs e)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            if (e == null) throw new ArgumentNullException(nameof(e));
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(form.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
    }
}