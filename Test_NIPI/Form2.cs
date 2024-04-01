using System;
using System.Windows.Forms;

namespace Test_NIPI
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();         
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1 form = (Form1)this.Owner;
                form.widthrec = Convert.ToInt32(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка при вводе числа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = (Form1)this.Owner;
            form.Show();
            Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Form1 form = (Form1)this.Owner;
                form.heightrec = Convert.ToInt32(textBox2.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка при вводе числа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button1.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;
        }
    }
}
