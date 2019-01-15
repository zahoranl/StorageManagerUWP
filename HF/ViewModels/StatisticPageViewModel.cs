using HF.Models;
using HF.Services;
using HF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace HF.ViewModels
{
    public class StatisticPageViewModel : ViewModelBase
    {
        public List<ChartData> diagramData = new List<ChartData>();
        public List<ChartData> diagramData2 = new List<ChartData>();
        public List<ChartData> diagramData3 = new List<ChartData>();

        private readonly IContentProviderApiService _contentProviderApiService;
        public StatisticPageViewModel(IContentProviderApiService contentProviderApiService)
        {
            _contentProviderApiService = contentProviderApiService;
            LoadChartContents();

            diagramData = contentProviderApiService.GetChartDataAll();
            diagramData2 = contentProviderApiService.GetChartDataAll2();
        }
        private void LoadChartContents()
        {
            Random rand = new Random();
            diagramData3.Add(new ChartData() { Name = "A", Sum = rand.Next(0, 200) });
            diagramData3.Add(new ChartData() { Name = "B", Sum = rand.Next(0, 200) });
            diagramData3.Add(new ChartData() { Name = "C", Sum = rand.Next(0, 200) });
            diagramData3.Add(new ChartData() { Name = "D", Sum = rand.Next(0, 200) });
            diagramData3.Add(new ChartData() { Name = "E", Sum = rand.Next(0, 200) });
            diagramData3.Add(new ChartData() { Name = "F", Sum = rand.Next(0, 200) });
            diagramData3.Add(new ChartData() { Name = "G", Sum = rand.Next(0, 200) });
            diagramData3.Add(new ChartData() { Name = "H", Sum = rand.Next(0, 200) });

        }
        public void ButtonRefresh_Click()
        {
            LoadChartContents();
        }
    }
}
