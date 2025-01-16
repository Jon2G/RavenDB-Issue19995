using Maui_RavenDemo.Types;
using Newtonsoft.Json;

namespace Maui_RavenDemo
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetItems();
        }

        private void RefreshView_OnRefreshing(object? sender, EventArgs e)
        {
            GetItems();
        }

        private void GetItems()
        {
            using (var session = DocumentStoreHolder.StoreTask.Result.OpenSession())
            {
               this.ListView.ItemsSource = session.Query<SampleType>().OfType<SampleType>().ToArray();
            }
        }
        private void Button_OnClicked(object? sender, EventArgs e)
        {
            //First we write some sample records
            using (var session = DocumentStoreHolder.StoreTask.Result.OpenSession())
            {
                session.Store(new SampleType()
                {
                    Id = Guid.NewGuid().ToString(),
                    Key = $"myCustomKey_{Guid.NewGuid():N}",
                    Value = $"{Guid.NewGuid():X}"
                });
                session.SaveChanges();
            }

            GetItems();
        }
    }

}
