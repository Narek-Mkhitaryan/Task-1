using System;
using System.Windows;

namespace WpfApp1
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int ID { get; set; }
    }

    public partial class MainWindow : Window
    {
        static T[] InitializeArray<T>(int length) where T : new()
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
            {
                array[i] = new T();
            }

            return array;
        }

        static int MaxRecordCount = 17;
        int RecordCount = 0;
        (string Name, int Age)[] Data = new (string Name, int Age)[MaxRecordCount];
        (string Name, int Age)[] Temp = new (string Name, int Age)[MaxRecordCount];

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PrintResult()
        {
            NameInputBox.Text = AgeInputBox.Text = IDInputBox.Text = "";
            OutputBox.Text = "";
            for (int i = 0; i < RecordCount; i++)
            {
                OutputBox.Text += $"ID: {i}, Name: {Data[i].Name}, Age: {Data[i].Age}\n";
            }
        }

        private void Input_Button_Click(object sender, RoutedEventArgs e)
        {
            if (RecordCount < MaxRecordCount)
            {
                if (int.TryParse(AgeInputBox.Text, out int age))
                {
                    Data[RecordCount].Name = NameInputBox.Text;
                    Data[RecordCount].Age = age;
                    OutputBox.Text += $"ID: {RecordCount}, Name: {Data[RecordCount].Name}, Age: {Data[RecordCount].Age}\n";
                    AgeInputBox.Text = NameInputBox.Text = "";
                    RecordCount += 1;
                }
                else
                {
                    AgeInputBox.Text = "Invalid age";
                }
            }
            else
            {
                NameInputBox.Text = "No space left";
            }
        }

        private void Sort_Age_Button_Click(object sender, RoutedEventArgs e)
        {
            for (int z = 0; z < RecordCount; z++)
                for (int i = 0; i < RecordCount - 1; i++)
                {
                    if (Data[i].Age > Data[i + 1].Age)
                    {
                        Temp[0] = Data[i];
                        Data[i] = Data[i + 1];
                        Data[i + 1] = Temp[0];
                    }
                }
            PrintResult();
        }

        private void Sort_Name_Button_Click(object sender, RoutedEventArgs e)
        {
            for (int z = 0; z < RecordCount; z++)
                for (int i = 0; i < RecordCount - 1; i++)
                {
                    if (Data[i].Name.CompareTo(Data[i + 1].Name) > 0)
                    {
                        Temp[0] = Data[i];
                        Data[i] = Data[i + 1];
                        Data[i + 1] = Temp[0];
                    }
                }
            PrintResult();
        }

        private void Search_Age_Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(AgeInputBox.Text, out int Age))
            {
                for (int i = 0; i < RecordCount; i++)
                {
                    if (Data[i].Age == Age)
                    {
                        IDInputBox.Text = $"{i}";
                        NameInputBox.Text = $"{Data[i].Name}";
                        AgeInputBox.Text = $"{Data[i].Age}";
                    }
                }
            }
            else
            {
                AgeInputBox.Text = "Invalid";
            }
        }

        private void Search_Name_Button_Click(object sender, RoutedEventArgs e)
        {
            bool nameFound = false;
            string name = NameInputBox.Text;
            for (int i = 0; i < RecordCount; i++)
            {
                if (name == Data[i].Name)
                {
                    nameFound = true;
                    IDInputBox.Text = $"{i}";
                    NameInputBox.Text = $"{Data[i].Name}";
                    AgeInputBox.Text = $"{Data[i].Age}";
                }
            }
            if (!nameFound)
            {
                NameInputBox.Text = "No person";
            }
        }

        private void Delete_ID_Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IDInputBox.Text, out int ID) && ID < RecordCount)
            {
                for (int i = ID; i < RecordCount - 1; i++)
                {
                    Temp[0] = Data[i];
                    Data[i] = Data[i + 1];
                    Data[i + 1] = Temp[0];
                }
                --RecordCount;
                PrintResult();
            }
            else
            {
                IDInputBox.Text = "Invalid";
            }
        }
    }
}
