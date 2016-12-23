using System;
using System.Windows.Forms;
using Timing;

namespace RegexTest
{
    internal class TimeOperation: IDisposable
    {
        private readonly Counter _counter;
        private readonly TextBox _textBox;

        public TimeOperation(TextBox textBox)
        {
            _textBox = textBox;
            _textBox.Text = "";
            _counter = new Counter();
            _counter.Start();
        }

        public void Dispose()
        {
            _counter.Stop();
            _textBox.Text = String.Format("{0:f2}", _counter.Seconds);
        }
    }
}