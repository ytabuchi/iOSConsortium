using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace iOSConsortium.Models
{
    public class SpeakersModel
    {
        public bool IsBusy { get; set; }
        public ObservableCollection<Speaker> Speakers { get; set; } = new ObservableCollection<Speaker>();
        private static HttpClient client = new HttpClient();

        public SpeakersModel()
        {
        }

        public async Task GetSpeakersAsync()
        {
            if (IsBusy)
                return;

            Exception error = null;
            try
            {
                IsBusy = true;

                var json = await client.GetStringAsync("http://demo4404797.mockable.io/speakers");
                var items = JsonConvert.DeserializeObject<ObservableCollection<Speaker>>(json);

                Speakers.Clear();
                foreach (var item in items)
                    Speakers.Add(item);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex);
                error = ex;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
