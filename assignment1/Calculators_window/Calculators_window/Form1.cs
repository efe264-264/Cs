namespace Calculators_window
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private char OPerator;
        double op1;
        double op2;
        private double Calculate(double op1, double op2, char OPerator)//���㺯��
        {
            switch (OPerator)
            {
                case '+':
                    return op1 + op2;
                case '-':
                    return op1 - op2;
                case '*':
                    return op1 * op2;
                case '/':
                    
                    return op1 / op2;
                    
                default:
                    throw new ArgumentException("error��");
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            double.TryParse(textBox1.Text, out op1);//��ȡ�����ַ���ת��Ϊdouble
            double.TryParse(textBox2.Text, out op2);
            if (OPerator == '\0')
            {
                MessageBox.Show("δѡ��operator��");
                return;
            }
            else
            {
                double result = Calculate(op1, op2, OPerator);//������
                textBox3.Text = result.ToString();//��ʾ���
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OPerator = '+';
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OPerator = '-';
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OPerator = '*';
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OPerator = '/';
        }
    }
}
