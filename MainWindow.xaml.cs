using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Net;
using System.ComponentModel;
using System.Threading;

namespace DataProcessor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public Dictionary<int, Threat> CurrentList = new Dictionary<int, Threat>();
        
        public List<object> DictionaryAsList = new List<object>();
        private PagingCollectionView PageCollectionList;
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < ListGrid.Columns.Count(); i++)
                ListGrid.Columns[i].Visibility = Visibility.Hidden;

        }
        public Dictionary<int, Threat> LoadDataFromExcel(string filename)
        {
            Dictionary<int, Threat> data = new Dictionary<int, Threat>();
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;


            int rw = 0;
            int cl = 0;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(filename, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;
            rw = range.Rows.Count;
            cl = range.Columns.Count;

            for (int rCnt = 3; rCnt <= rw; rCnt++)
            {

                Threat t = new Threat();
                t.ID = (int)(range.Cells[rCnt, 1]).Value2;
                t.Name = (string)(range.Cells[rCnt, 2]).Value2;
                t.Description = (string)(range.Cells[rCnt, 3]).Value2;
                t.SourceOfThreat = (string)(range.Cells[rCnt, 4]).Value2;
                t.ObjectOfImpact = (string)(range.Cells[rCnt, 5]).Value2;
                int Сonfidentiality = (int)(range.Cells[rCnt, 6]).Value2;

                if (Сonfidentiality == 1)
                {
                    t.BreachOfСonfidentiality = "Да";
                }
                else
                {
                    t.BreachOfСonfidentiality = "Нет";
                }

                int BreachOfintegrity = (int)(range.Cells[rCnt, 7]).Value2;

                if (BreachOfintegrity == 1)
                {
                    t.BreachOfintegrity = "Да";
                }
                else
                {
                    t.BreachOfintegrity = "Нет";
                }

                int BreachOfAccess = (int)(range.Cells[rCnt, 8]).Value2;
                if (BreachOfAccess == 1)
                {
                    t.BreachOfAccess = "Да";
                }
                else
                {
                    t.BreachOfAccess = "Нет";
                }
                t.DateInclude = DateTime.FromOADate((double)(range.Cells[rCnt, 9]).Value2).ToShortDateString();
                t.DateChange = DateTime.FromOADate((double)(range.Cells[rCnt, 10]).Value2).ToShortDateString();

                data[t.ID] = t;

            }
            xlWorkBook.Close();
            return data;


        }
        public void UpDateTable(Dictionary<int, Threat> list1)
        {
            try
            {
                WebClient client = new WebClient();
                client.DownloadFile(new Uri("https://bdu.fstec.ru/documents/files/thrlist.xlsx"), @"D:\thrlist.xlsx");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось скачать файл. " + ex.Message);
            }
            Dictionary<int, Threat> NewList = LoadDataFromExcel(@"D:\thrlist.xlsx");

            string str = "";

            foreach (KeyValuePair<int, Threat> dict in list1)
            {

                Threat oldValue = dict.Value;
                Threat newValue = NewList[oldValue.ID];
                if (oldValue.DateChange != newValue.DateChange)
                {
                    if (oldValue.Name != newValue.Name)
                    {
                        str += "Строка с идентификатором: " + newValue.ID + " была изменена.\nДата изменения:" + newValue.DateChange + "\nИзменилось значение поля 'Наименование УБИ'.\nБыло: " + oldValue.Name + "\nСтало: " + newValue.Name + "\n";
                    }
                    if (oldValue.Description != newValue.Description)
                    {
                        str += "Строка с идентификатором: " + newValue.ID + " была изменена.\nДата изменения:" + newValue.DateChange + "\nИзменилось значение поля 'Описание'.\nБыло: " + oldValue.Description + "\nСтало: " + newValue.Description + "\n";
                    }
                    if (oldValue.SourceOfThreat != newValue.SourceOfThreat)
                    {
                        str += "Строка с идентификатором: " + newValue.ID + " была изменена.\nДата изменения:" + newValue.DateChange + "\nИзменилось значение поля 'Источник угрозы'.\nБыло: " + oldValue.SourceOfThreat + "\nСтало: " + newValue.SourceOfThreat + "\n";
                    }
                    if (oldValue.ObjectOfImpact != newValue.ObjectOfImpact)
                    {
                        str += "Строка с идентификатором: " + newValue.ID + " была изменена.\nДата изменения:" + newValue.DateChange + "\nИзменилось значение поля 'Объект воздействия'.\nБыло: " + oldValue.ObjectOfImpact + "\nСтало: " + newValue.ObjectOfImpact + "\n";
                    }
                    if (oldValue.BreachOfСonfidentiality != newValue.BreachOfСonfidentiality)
                    {
                        str += "Строка с идентификатором: " + newValue.ID + " была изменена.\nДата изменения:" + newValue.DateChange + "\nИзменилось значение поля 'Нарушение конфиденциальности'.\nБыло: " + oldValue.BreachOfСonfidentiality + "\nСтало: " + newValue.BreachOfСonfidentiality + "\n";
                    }
                    if (oldValue.BreachOfintegrity != newValue.BreachOfintegrity)
                    {
                        str += "Строка с идентификатором: " + newValue.ID + " была изменена.\nДата изменения:" + newValue.DateChange + "\nИзменилось значение поля 'Нарушение целостности'.\nБыло: " + oldValue.BreachOfintegrity + "\nСтало: " + newValue.BreachOfintegrity + "\n";
                    }
                    if (oldValue.BreachOfAccess != newValue.BreachOfAccess)
                    {
                        str += "Строка с идентификатором: " + newValue.ID + " была изменена.\nДата изменения:" + newValue.DateChange + "\nИзменилось значение поля 'Нарушение доступа'.\nБыло: " + oldValue.BreachOfAccess + "\nСтало: " + newValue.BreachOfAccess + "\n";
                    }
                    if (oldValue.DateInclude != newValue.DateInclude)
                    {
                        str += "Строка с идентификатором: " + newValue.ID + " была изменена.\nДата изменения:" + newValue.DateChange + "\nИзменилось значение поля 'Дата включения угрозы в БиД УБИ'.\nБыло: " + oldValue.DateInclude + "\nСтало: " + newValue.DateInclude + "\n";
                    }
                }
            }

            if (str == "")
            {
                MessageBox.Show("Изменений не обнаружено");
            }
            else
            {
                MessageBox.Show(str);
                MessageBox.Show("Сейчас будет произведена повторная загрузка данных\nдля внесения изменений");
                ShowList(NewList);
            }
        }

        public void ShowList(Dictionary<int,Threat> dictionary)
        {
            List<Threat> data = new List<Threat>(); 
            foreach (KeyValuePair<int, Threat> dict in dictionary)
            {
                data.Add(dict.Value);
            }
            this.PageCollectionList = new PagingCollectionView(data, 15);
            this.DataContext = this.PageCollectionList;
            PageCollectionList.Refresh();
           
        }

        public void ShowAllColumns()
        {



            for (int i = 0; i < 10; i++)
            {
                ListGrid.Columns[i].Header = "";
                ListGrid.Columns[i].Visibility = Visibility.Visible;
            }

            ListGrid.Columns[0].Header = "Идентификатор УБИ";
            ListGrid.Columns[1].Header = "Наименование";
            ListGrid.Columns[2].Header = "Описание";
            ListGrid.Columns[3].Header = "Источник угрозы";
            ListGrid.Columns[4].Header = "Объект воздействия";
            ListGrid.Columns[5].Header = "Нарушение конфиденциальности";
            ListGrid.Columns[6].Header = "Нарушение целостности";
            ListGrid.Columns[7].Header = "Нарушение доступа";
            ListGrid.Columns[8].Header = "Дата включенияв УБИ";
            ListGrid.Columns[9].Header = "Дата изменения";
        }
        public void ShowShortColumns()
        {
            for (int i = 2; i < 10; i++)
            {
                ListGrid.Columns[i].Header = "";
                ListGrid.Columns[i].Visibility = Visibility.Hidden;
            }

        }

        internal void LoadFromDisk()
        {

            try
            {

                CurrentList = LoadDataFromExcel(@"D:\thrlist.xlsx");

                ShowAllColumns();

                ShowList(CurrentList);



                MessageBox.Show("Загрузка завершена.");
                btn_Next.IsEnabled = true;
                btn_Prev.IsEnabled = true;
                btn_Update.IsEnabled = true;
                btn_ShortList.IsEnabled = true;
                btn_AllList.IsEnabled = true;
            }
            catch (COMException ex)
            {
                MessageBoxResult result = MessageBox.Show(ex.Message + "\n\nВы хотите загрузить данные с сети?\n(Необходимо подключение к интернету!!!)",
                "Error. File not found", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        WebClient client = new WebClient();
                        client.DownloadFile(new Uri("https://bdu.fstec.ru/documents/files/thrlist.xlsx"), @"D:\thrlist.xlsx");
                        LoadFromDisk();
                    }
                    catch (Exception exconnect)
                    {
                        MessageBox.Show("Не удалось скачать файл. " + exconnect.Message);
                    }
                }


            }

        }

        public void ProgressBar()
        {
            Thread.Sleep(5000);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadFromDisk();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.PageCollectionList.MoveToPreviousPage();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.PageCollectionList.MoveToNextPage();
        }

        private void ListGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            UpDateTable(CurrentList);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ShowShortColumns();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            ShowAllColumns();
        }
    }






}



