using ApplicationConnectDb.Database.Handlers;
using ApplicationConnectDb.Database.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Diagnostics;

namespace ApplicationConnectDb
{
    public partial class Form1 : Form
    {
        private static List<Group> Groups = new List<Group>();
        private static Group CurrentGroup { get; set; } = null;
        private static int GeneralCountStudents { get; set; }
        public Form1()
        {
            InitializeComponent();
            Groups = GroupHandler.GetGroups();
            UpdateCountStudents();
            comboBox1.Items.AddRange(Groups.Select(g => g.Name).ToArray());
        }
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            var nameCurrentGroup = comboBox1.SelectedItem.ToString();
            CurrentGroup = Groups.FirstOrDefault(g => g.Name == nameCurrentGroup);
            UpdateStudentsGroup();
        }
        private void UpdateCountStudents()
        {
            foreach (var group in Groups)
            {
                GeneralCountStudents += group.Students.Count;
            }
            label4.Text = $"Общее количество студентов во всех группах: {GeneralCountStudents}";
        }
        private void UpdateStudentsGroup()
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(CurrentGroup.Students.Select(g => g.LastName).ToArray());
        }

        //добавить в группу
        private void button1_Click(object sender, EventArgs e)
        {
            if (CurrentGroup != null)
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("Заполните все поля!");
                    return;
                }
                GroupHandler.AddStudent(CurrentGroup, new Student(textBox2.Text, textBox1.Text, textBox3.Text));
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                UpdateStudentsGroup();
                return;
            }
            MessageBox.Show("Сначала выберите группу!");
        }
        //обновить таблицу
        private void button2_Click(object sender, EventArgs e)
        {
            if(CurrentGroup == null)
            {
                MessageBox.Show("Для начала выберите группу!");
                return;
            }          
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            for (int i = 0; i < CurrentGroup.Students.Count; i++)
            {
                var index = this.dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = CurrentGroup.Students[i].Id;
                dataGridView1.Rows[index].Cells[1].Value = CurrentGroup.Students[i].LastName;
                dataGridView1.Rows[index].Cells[2].Value = CurrentGroup.Students[i].FirstName;
                dataGridView1.Rows[index].Cells[3].Value = CurrentGroup.Students[i].Patronymic;
            }
        }
        //удалить запись
        private void button3_Click(object sender, EventArgs e)
        {

        }
        //обновить запись
        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                
            }
        }
    }
}