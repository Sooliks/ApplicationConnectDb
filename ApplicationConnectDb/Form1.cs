using ApplicationConnectDb.Database;
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
        private static int CurrentSelectedIdStudent { get; set; } = 0;
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
                UpdateTable();
                return;
            }
            MessageBox.Show("Сначала выберите группу!");
        }
        //обновить таблицу
        private void button2_Click(object sender, EventArgs e)
        {
            Groups = GroupHandler.GetGroups();
            if (CurrentGroup == null)
            {
                MessageBox.Show("Для начала выберите группу!");
                return;
            }
            UpdateTable();
        }
        private void UpdateTable()
        {
            CurrentGroup = GroupHandler.GetGroupById(CurrentGroup.Id);
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
            if (CurrentGroup == null)
            {
                MessageBox.Show("Сначала выберите группу!");
                return;
            }
            if (CurrentSelectedIdStudent == 0)
            {
                MessageBox.Show("Сначала выберите id студента!");
                return;
            }
            GroupHandler.RemoveStudent(CurrentGroup, CurrentSelectedIdStudent);
            UpdateTable();
        }
        //обновить запись
        private void button4_Click(object sender, EventArgs e)
        {
            CurrentGroup = GroupHandler.GetGroupById(CurrentGroup.Id);
            for (int i = 0; i < CurrentGroup.Students.Count; i++)
            {             
                if(                  
                    (string)dataGridView1.Rows[i].Cells[1].Value != CurrentGroup.Students[i].LastName
                    ||
                    (string)dataGridView1.Rows[i].Cells[2].Value != CurrentGroup.Students[i].FirstName
                    ||
                    (string)dataGridView1.Rows[i].Cells[3].Value != CurrentGroup.Students[i].Patronymic
                    )
                {
                    using var db = new Context();
                    var student = db.Students.FirstOrDefault(s => s.Id == (int)dataGridView1.Rows[i].Cells[0].Value);
                    student.LastName = (string)dataGridView1.Rows[i].Cells[1].Value;
                    student.FirstName = (string)dataGridView1.Rows[i].Cells[2].Value;
                    student.Patronymic = (string)dataGridView1.Rows[i].Cells[3].Value;
                    db.Students.Update(student);
                    db.SaveChanges();
                }           
            }
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value is int)
            {
                CurrentSelectedIdStudent = (int)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }
    }
}