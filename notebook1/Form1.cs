using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace notebook1
{
    public partial class Mean : Form
    {
        public string filename;
        public bool isFileGhanged;

        public int fontSize = 0;
        public System.Drawing.FontStyle fs = FontStyle.Regular;

        public FontSettings fontSetts;
        public Mean()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            filename = "";
            isFileGhanged = false;
            UpdateTextWithTittle();
        }
        public void CreateNewDocument(object sender, EventArgs e)
        {
            SaveUnsavedFile();
            textBox1.Text = "";
            filename = "";
            isFileGhanged = false;
            UpdateTextWithTittle();
        }
        public void OpenFile(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                    textBox1.Text = sr.ReadToEnd();
                    sr.Close();
                    filename = openFileDialog1.FileName;
                    isFileGhanged = false;
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть файл!");
                }
            }
            UpdateTextWithTittle();
        }
        public void SaveFile(string fileName1)
        {
            if (fileName1 == "")
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName1 = saveFileDialog1.FileName;
                }
            }
            try
            {
                StreamWriter sw = new StreamWriter(fileName1);
                sw.Write(textBox1.Text);
                sw.Close();
                filename = fileName1;
                isFileGhanged = false;
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить файл!");
            }
            UpdateTextWithTittle();
        }
        public void Save(object sender, EventArgs e)
        {
            SaveFile(filename);
        }
        public void SaveAs(object sender, EventArgs e)
        {
            SaveFile(filename);
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (!isFileGhanged)
            {
                this.Text = this.Text.Replace('*', ' ');
                isFileGhanged = true;
                this.Text = "*" + this.Text;
            }
        }
        public void UpdateTextWithTittle()
        {
            if (filename != "")
                this.Text = filename + "-Notepad";
            else this.Text = "Безымянный -Notepad";
        }
        public void SaveUnsavedFile()
        {
            if (isFileGhanged)
            {
                DialogResult result = MessageBox.Show("Сохранить измeнения в файле?", "Сохранение файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    SaveFile(filename);
                }
            }
        }
        public void CopyText()
        {
            Clipboard.SetText(textBox1.SelectedText);
        }
        public void CutText()
        {
            Clipboard.SetText(textBox1.SelectedText);
            textBox1.Text = textBox1.Text.Remove(textBox1.SelectionStart, textBox1.SelectionLength);

        }
        public void PasteText()
        {
            textBox1.Text = textBox1.Text.Substring(0, textBox1.SelectionStart) + Clipboard.GetText() + textBox1.Text.Substring(textBox1.SelectionStart, textBox1.Text.Length - textBox1.SelectionStart);
        }

        private void OnCopyClick(object sender, EventArgs e)
        {
            CopyText();
        }

        private void OnCutClick(object sender, EventArgs e)
        {
            CutText();
        }

        private void OnPasteClick(object sender, EventArgs e)
        {
            PasteText();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUnsavedFile();
        }

        private void OnFontClick(object sender, EventArgs e)
        {
            fontSetts = new FontSettings();
            fontSetts.Show();
        }

        private void OnFocus(object sender, EventArgs e)
        {
            if (fontSetts != null)
            {
                fontSize = fontSetts.fontSize;
                fs = fontSetts.fs;
                textBox1.Font = new Font(textBox1.Font.FontFamily, fontSize, fs);
                fontSetts.Close();
            }
        }
    }
}
