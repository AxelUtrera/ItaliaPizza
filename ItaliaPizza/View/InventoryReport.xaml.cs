using Logic;
using Microsoft.Win32;
using Model;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Lógica de interacción para InventoryReport.xaml
    /// </summary>
    public partial class InventoryReport : Window
    {
        private List<Model.InventoryReport> report;
        public static Worker workerLogged;

        public InventoryReport()
        {
            InitializeComponent();
            report = new List<Model.InventoryReport>();
            SetReportToTable();
            SetDate();

        }

        private void SetDate()
        {
            Label_Date.Content = DateTime.Today.ToString("dd/MM/yyyy");
        }

        private void SetReportToTable()
        {
            report = InventoryReportLogic.getInventoryReport();
            InventoryReportTable.ItemsSource = report;
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void InventoryReportTable_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // MessageBox.Show(InventoryReportTable.CurrentItem as Model.InventoryReport +"");
        }

        private void Button_ExportPDF_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog.Title = "Guardar PDF";
            saveFileDialog.FileName = string.Format("{0}_{1}.pdf", "ReporteDeInventario", DateTime.Now.Ticks);

            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                string directoryPath = System.IO.Path.GetDirectoryName(saveFileDialog.FileName);
                string pathPDF = ItextPdf.GenerateInventoryReport(report, directoryPath);
                if (pathPDF != null)
                {
                    var response = MessageBox.Show("El reporte se ha generado exitosamente. \n ¿Desea abrir el PDF?", "Reporte Generado", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (response == MessageBoxResult.Yes)
                    {
                        Process.Start(pathPDF);
                    }
                }
                else
                {
                    MessageBox.Show("El reporte no pudo ser registrado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            List<Model.InventoryReport> updatedReport = new List<Model.InventoryReport>();

            if (InventoryReportTable.ItemsSource != null)
            {
                Model.InventoryReport reports = new Model.InventoryReport();
                reports = InventoryReportTable.SelectedItem as Model.InventoryReport;
                List<Ingredient> ingredientsList = new List<Ingredient>();
                List<Product> productsList = new List<Product>();

                report = (List<Model.InventoryReport>)InventoryReportTable.ItemsSource;
                foreach (Model.InventoryReport aux in InventoryReportTable.Items)
                {
                    var row = InventoryReportTable.ItemContainerGenerator.ContainerFromItem(aux) as DataGridRow;
                    var cellRealQuantity = InventoryReportTable.Columns[5].GetCellContent(row) as FrameworkElement;
                    var columnaRealQuantity = InventoryReportTable.Columns[5] as DataGridTemplateColumn;
                    var templateRealQuantity = columnaRealQuantity.CellTemplate as DataTemplate;
                    var integerUpDownRealQuantity = templateRealQuantity.FindName("UpDownRealQuantity", cellRealQuantity) as UpDown;
                    var realQuantity = integerUpDownRealQuantity.Value;
                    aux.RealQuantity = (int)realQuantity.Value;
                    updatedReport.Add(aux);
                    if (aux.TypeOfProduct == "Ingrediente")
                    {
                        Ingredient ingredient = new Ingredient
                        {
                            IdIngredient = Int32.Parse(aux.IdItem),
                            Quantity = aux.RealQuantity
                        };
                        ingredientsList.Add(ingredient);
                    }
                    else
                    {
                        Product product = new Product
                        {
                            ProductCode = aux.IdItem,
                            Quantity = aux.RealQuantity
                        };
                        productsList.Add(product);
                    }
                }
                var response = MessageBox.Show("¿Estas seguro de actualizar el inventario? \n una vez realizados los cambios no podran deshacerse","Confirmar cambios",MessageBoxButton.YesNo,MessageBoxImage.Question,MessageBoxResult.No);
                if (response == MessageBoxResult.Yes)
                {
                    bool isOk = true;
                    foreach(var aux in ingredientsList)
                    {
                        if (!IngredientLogic.UpdateQuantity(aux))   
                            isOk = false;
                    }
                    foreach(var aux  in productsList)
                    {
                        if (!ProductLogic.UpdateQuantity(aux))
                            isOk = false;
                    }
                    if (isOk)
                    {
                        MessageBox.Show("El inventario ha sido actualizado exitosamente", "Inventario actualizado", MessageBoxButton.OK, MessageBoxImage.Information);
                        SetReportToTable();
                    }
                    else
                    {
                        MessageBox.Show("No todos los productos pudieron ser actualizados correctamente\n por favor, intentelo nuevamente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        SetReportToTable();
                    }
                }
                MessageBox.Show("Por favor, selecciona el reporte de inventario el cual deseas aplicar la actualizacion", "Reporte de inventario", MessageBoxButton.OK, MessageBoxImage.Information);
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Seleccionar archivo PDF";
                openFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";

                bool? result = openFileDialog.ShowDialog();

                if (result == true)
                {

                    string rutaArchivo = openFileDialog.FileName;

                    string pathPDF = ItextPdf.ModifyReport(updatedReport, rutaArchivo);
                    var response2 = MessageBox.Show("El reporte se ha generado exitosamente. \n ¿Desea abrir el PDF?", "Reporte Generado", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (response2 == MessageBoxResult.Yes)
                    {
                        Process.Start(pathPDF);
                    }
                }

            }

            
        }
    }
}
