using ChartCsApp.Modules;
using ChartCsApp.Tables;
using ScottPlot;
using System.Reflection;

namespace ChartCsApp
{
    public partial class Form : System.Windows.Forms.Form
    {
        private List<Cereal> cereals = new List<Cereal>();

        public Form()
        {
            InitializeComponent();

            InitCtrl();
        }

        private void InitCtrl()
        {
            InitAxisComboBox(comboBoxX, "calories");
            InitAxisComboBox(comboBoxY, "carbo");
            InitTypeComboBox(comboBoxMfr, Cereal.GetMfrs(), 0);
            InitTypeComboBox(comboBoxType, Cereal.GetTypes(), 0);
            InitChart();
        }

        private void InitAxisComboBox(ComboBox comboBox, string initValue)
        {
            comboBox.SelectedIndex = comboBox.FindString(initValue);
        }

        private void InitTypeComboBox(ComboBox comboBox, List<string> types, int index)
        {
            comboBox.Items.Add(Cereal.AllTypeName);
            Cereal.GetMfrs().ForEach(m => { comboBox.Items.Add(m); });
            comboBox.SelectedIndex = 0;
        }

        private void InitChart()
        {
            formsPlot.Plot.Title("chart-cs-app");
            ReloadChart();
        }

        private void ReloadChart()
        {
            var cereals = Cereal.GetCereals(comboBoxMfr.Text, comboBoxType.Text);
            var xValues = cereals.ConvertAll(v => Pickup(v, comboBoxX));
            var yValues = cereals.ConvertAll(v => Pickup(v, comboBoxY));

            formsPlot.Reset();
            formsPlot.Plot.XLabel($"X:{comboBoxX.Text}");
            formsPlot.Plot.YLabel($"Y:{comboBoxY.Text}");

            if (xValues.Count() == 0 || yValues.Count() == 0)
                return;

            formsPlot.Plot.AddScatter(xValues?.ToArray(), yValues?.ToArray(), lineStyle: LineStyle.None);
            formsPlot.Refresh();
        }

        private double Pickup(Cereal cereal, ComboBox comboBox)
        {
            // プロパティ情報の取得
            var property = typeof(Cereal).GetProperty(comboBox.Text);
            if (property == null)
                return 0.0;

            // インスタンスの値を取得
            double.TryParse(property.GetValue(cereal)?.ToString(), out double value);

            return value;
        }

        private void comboBoxX_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadChart();
        }

        private void comboBoxY_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadChart();
        }

        private void comboBoxMfr_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadChart();
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadChart();
        }
    }
}