using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ApiShell;
using Core;

namespace CoralDraw_WinForms
{
    public partial class Form1 : Form
    {
        private SystemDrawingToCoreConverter converter;
        private EventArgumentsConverter eventConverter;
        private GraphicsAdapter adapter;
        private Redactor redactor;

        public Form1()
        {
            InitializeComponent();
            InitContent();
        }

        private void InitContent()
        {
            adapter = new GraphicsAdapter(this.CreateGraphics());
            eventConverter = new EventArgumentsConverter();
            converter = new SystemDrawingToCoreConverter();
            redactor = new Redactor();
            AllocConsole();
        }

        private void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) 
        {
            InvokeMethod(
                redactor.InvokeHandlerFor, 
                eventConverter.ConvertFrom(e, ClickType.Down)
            );
        }

        private void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e) 
        {
            InvokeMethod(
                redactor.InvokeHandlerFor, 
                eventConverter.ConvertFrom(e, ClickType.Hold)
            );
            OnRefesh();
        }

        private void OnMouseUp(object sender, System.Windows.Forms.MouseEventArgs e) 
        {
            InvokeMethod(
                redactor.InvokeHandlerFor, 
                eventConverter.ConvertFrom(e, ClickType.Up)
            );
        }

        private void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs e) 
        {
            InvokeMethod(
                redactor.InvokeHandlerFor, 
                eventConverter.ConvertFrom(e, ClickType.Up)
            );
        }

        private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            InvokeMethod(
                redactor.InvokeHandlerFor, 
                eventConverter.ConvertFrom(e, ClickType.Down)
            );
        }

        private void OnRefesh() 
        {
            Refresh();
            InvokeMethod(
                    redactor.InvokeHandlerFor<IDrawerAdapter>, 
                    adapter
            );
        }

        private void OnChangeFigureFactory(object sender, EventArgs e)
        {
            InvokeMethod(
                redactor.InvokeHandlerFor,
                eventConverter.CreateArgsForFigureFactory(
                    comboBox1.SelectedItem.ToString())
            );
        }

        private void OnChangeDrawerFactory(object sender, EventArgs e)
        {
            InvokeMethod(
                redactor.InvokeHandlerFor,
                eventConverter.CreateArgsForDrawerFactory(
                    comboBox2.SelectedItem.ToString())
            );
        }

        private void OnChangeState(object sender, EventArgs e)
        {
            redactor.SwapState();
            stateButton.Text = redactor.ActiveStateName();
        }

        private void OnChangeColor(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK) 
            {
                InvokeMethod(
                    redactor.InvokeHandlerFor,
                    converter.ConvertFrom(colorDialog.Color)
                );    
            }
        }

        private void InvokeMethod<T>(Action<T> action, T param) 
        {

            try
            {
                action.Invoke(param);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}