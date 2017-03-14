using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using iOSConsortium.Models;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace XFiOSConsortium
{
    public class SpeakersPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static HttpClient client = new HttpClient();

        public ObservableCollection<Speaker> Speakers { get; set; } 
        public Command GetSpeakersCommand { get; set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
                GetSpeakersCommand.ChangeCanExecute();
            }
        }

        public SpeakersPageViewModel()
        {
            Speakers = new ObservableCollection<Speaker>();
            GetSpeakersCommand = new Command(
                async () => await GetSpeakers(),
                () => !IsBusy);
        }

        // インターネットから speaker のデータをすべて取ってくる
        async Task GetSpeakers()
        {
            if (IsBusy)
                return;

            Exception error = null;
            try
            {
                IsBusy = true;

                //サーバーから json を取得します
                var json = await client.GetStringAsync("http://demo4404797.mockable.io/speakers");

                //json をデシリアライズします
                var items = JsonConvert.DeserializeObject<List<Speaker>>(json);

                //リストを Speakers に読み込ませます
                Speakers.Clear();
                foreach (var item in items)
                    Speakers.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
                error = ex;
            }
            finally
            {
                IsBusy = false;
            }

            if (error != null)
            {
                // ポップアップアラートを表示
                await Application.Current.MainPage.DisplayAlert("Error!", error.Message, "OK");
            }
        }

        void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
