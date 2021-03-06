using System;
using System.Diagnostics;
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
        private bool isDown;

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
            isDown = false;
        }

        private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (!isDown)
            {
                InvokeMethod(
                    redactor.InvokeHandlerFor,
                    eventConverter.ConvertFrom(e, ClickType.Down)
                ); 
                isDown = true;
            }
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

        private void OnGroup(object sender, EventArgs e) 
        {
            InvokeMethod(
                redactor.InvokeHandlerFor,
                new CompositeEventArgs() { Type = CompositeEventArgs.EventType.Group}
            );
        }

        private void OnUngroup(object sender, EventArgs e) 
        {
            InvokeMethod(
                redactor.InvokeHandlerFor,
                new CompositeEventArgs() { Type = CompositeEventArgs.EventType.Ungroup }
            );
        }

        private static int savedID = 0;
        private void OnFigureSave(object sender, EventArgs e) 
        {
            var creatorName = $"saved{savedID++}";
            
            InvokeMethod(
                redactor.InvokeHandlerFor,
                creatorName  
            );
            comboBox1.Items.Add(creatorName);
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
    }
}