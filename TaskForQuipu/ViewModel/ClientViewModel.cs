using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using TaskForQuipu.DBModel;
using TaskForQuipu.Model;

namespace TaskForQuipu.ViewModel
{
    public class ClientViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string _browserPath;
        public string BrowserPath
        {
            get { return _browserPath; }
            set { _browserPath = value; OnPropertyChanged("BrowserPath"); }
        }

        private ObservableCollection<Model.Client> _ClientList;        
        public ObservableCollection<Model.Client> Clients
        {
            get { return _ClientList; }
            set { _ClientList = value; OnPropertyChanged("Clients"); }
        }

        private void OnPropertyChanged(string p)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(p));
            }
        }

        public ICommand BrowserCommand { get; set; }
        public ICommand SubmitCommand { get; set; }

        public ClientViewModel()
        {           
            BrowserCommand = new RelayCommand(o => BrowseButtonClick("MainButton"));
            SubmitCommand = new RelayCommand(o => SubmitButtonClick("MainButton"));
            Clients = new ObservableCollection<Model.Client>();
        }

        private void BrowseButtonClick(object sender)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "XML Files (*.xml)|*.xml";
                ofd.FilterIndex = 0;
                ofd.DefaultExt = "xml";
                Nullable<bool> result = ofd.ShowDialog();
                if (result == true)
                {
                    string filename = ofd.FileName;                 
                    this.BrowserPath = filename;
                    this._ClientList.Clear();
                }              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK);
            }
        }

        private void SubmitButtonClick(object sender)
        {
            try
            {
                if (this.BrowserPath != null && !string.IsNullOrEmpty(this.BrowserPath) && File.Exists(this.BrowserPath))
                {
                    string xml = File.ReadAllText(this.BrowserPath);
                    ClientXML model = ClientXML.Deserialize(xml);
                    _ClientList = new ObservableCollection<Model.Client>();
                    using (QuipuContext context = new QuipuContext())
                    {
                        foreach (var client in model.Clients)
                        {
                            this._ClientList.Add(client);

                            if (!context.Clients.Any(o => o.ID == client.Id))
                            {
                                context.Clients.Add(Mapper(client));
                            }                            
                        }
                        this.Clients = this._ClientList;
                        context.SaveChanges();                       
                    }
                }
                else
                {
                    MessageBox.Show("Something wrong. Please try again!", "Info", MessageBoxButton.OK);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK);
            }

        }        

        public DBModel.Client Mapper(Model.Client client)
        {
            DBModel.Client clientDBModel = new DBModel.Client
            {
                ID = client.Id,
                Name = client.Name,
                Address = client.Address,
            };

            return clientDBModel;

        }
    }
}
