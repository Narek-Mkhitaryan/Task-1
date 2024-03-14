using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        static int maxRecordCount = 17;
        int recordCount = 0;
        (string Name, int Age)[] Data = new (string Name, int Age)[maxRecordCount];
        (string Name, int Age)[] temp = new (string Name, int Age)[maxRecordCount];
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PrintResult()
        {
            NameInputBox.Text = AgeInputBox.Text = IDInputBox.Text = "";
            OutputBox.Text = "";
            for (int i = 0; i < recordCount; i++)
            {
                OutputBox.Text += $"ID: {i}, Name: {Data[i].Name}, Age: {Data[i].Age}\n";
            }
        }

        private void Input_Button_Click(object sender, RoutedEventArgs e)
        {
            if (recordCount < maxRecordCount)
            {
                if (int.TryParse(AgeInputBox.Text, out int age))
                {
                    Data[recordCount].Name = NameInputBox.Text;
                    Data[recordCount].Age = age;
                    OutputBox.Text += $"ID: {recordCount}, Name: {Data[recordCount].Name}, Age: {Data[recordCount].Age}\n";
                    AgeInputBox.Text = NameInputBox.Text = "";
                    recordCount += 1;
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

            for (int z = 0; z < recordCount; z++)
                for (int i = 0; i < recordCount - 1; i++)
                {
                    if (Data[i].Age > Data[i + 1].Age)
                    {
                        temp[0] = Data[i];
                        Data[i] = Data[i + 1];
                        Data[i + 1] = temp[0];
                    }
                }

            PrintResult();

        }

        private void Sort_Name_Button_Click(object sender, RoutedEventArgs e)
        {

            for (int z = 0; z < recordCount; z++)
                for (int i = 0; i < recordCount - 1; i++)
                {
                    if (Data[i].Name.CompareTo(Data[i + 1].Name) > 0)
                    {
                        temp[0] = Data[i];
                        Data[i] = Data[i + 1];
                        Data[i + 1] = temp[0];
                    }
                }

            PrintResult();

        }

        private void Search_Age_Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(AgeInputBox.Text, out int Age))
            {
                for (int i = 0; i < recordCount; i++)
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
            Name = NameInputBox.Text;
            for (int i = 0; i < recordCount; i++)
            {
                if (Name == Data[i].Name)
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

            if (int.TryParse(IDInputBox.Text, out int ID) && ID < recordCount)
            {
                for (int i = ID; i < recordCount - 1; i++)
                {
                    temp[0] = Data[i];
                    Data[i] = Data[i + 1];
                    Data[i + 1] = temp[0];
                }

                --recordCount;
                PrintResult();
            }
            else
            {
                IDInputBox.Text = "Invalid";
            }
        }

    }
}
