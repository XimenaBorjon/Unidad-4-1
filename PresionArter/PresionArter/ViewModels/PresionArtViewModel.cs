using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using PresionArter.Model;
using PresionArter.Views;

using Xamarin.Forms;


namespace PresionArter.ViewModels
{
    public class PresionArtViewModel : INotifyPropertyChanged
    {
        public ICommand AgregarPresionCommand { get; set; }
        public ICommand NavegarAgregarPresionCommand { get; set; }
        public ICommand NavegarPresionCommand { get; set; }
        public ICommand NavegarEditarCommand { get; set; }
        public ICommand NavegarEliminarCommand { get; set; }
        public ICommand EditarCommand { get; set; }


        Presiones presiones = new Presiones();
        public ObservableCollection<Presion> Verpresiones { get; set; }
        public ObservableCollection<Presion> PrecionesFiltradas { get; set; } = new ObservableCollection<Presion>();

        private Presion presion;

        public Presion Presion
        {
            get { return presion; }
            set
            {
                presion = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(presion)));
            }
        }
        private string errores;

        public string ErrorValidacion
        {
            get { return errores; }
            set
            {
                errores = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorValidacion)));
            }
        }
        EditarView editarview;
        AgregarView agregarregistros;
        int PresionOriginal;
        public PresionArtViewModel()
        {
            NavegarAgregarPresionCommand = new Command(NavegarAgregar);
            //NavegarRegistroReservacionCommand = new Command(NavegarRegistro);
            NavegarEditarCommand = new Command<Presion>(NavegarEditar);
            NavegarEliminarCommand = new Command<Presion>(NavegarEliminar);
            AgregarPresionCommand = new Command(Agregar);
            EditarCommand = new Command(Editar);
            editarview = new EditarView() { BindingContext = this };
            agregarregistros = new AgregarView() { BindingContext = this };

            

            Verpresiones = new ObservableCollection<Presion>();


            CargarReservaciones();
        }

        private void Eliminar()
        {
            try
            {
                presiones.Delete(Presion);
                CargarReservaciones();
                Application.Current.MainPage.Navigation.PopAsync();

            }
            catch (Exception m)
            {
                ErrorValidacion = m.Message;
            }

        }
        private void Agregar()
        {
            try
            {
                presiones.Insert(Presion);
                CargarReservaciones();
                Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception m)
            {
                ErrorValidacion = m.Message;
            }
        }
        private void CargarReservaciones()
        {
            PrecionesFiltradas.Clear();
            var a = presiones.GetAll();

            foreach (var item in a)
            {
                Verpresiones.Add(item);
            }
        }
        public void Editar()
        {

            presiones.Update(Presion);
            Application.Current.MainPage.Navigation.PopAsync();
            CargarReservaciones();

        }
        private void NavegarEditar(Presion obj)
        {


            Presion clon = new Presion()
            {
                Id = obj.Id,
                Pulso = obj.Pulso,
                Diastolica = obj.Diastolica,
                Sistolica = obj.Sistolica,
                Fecha = obj.Fecha


            };
            errores = "";
            Presion = clon;
            PresionOriginal = Verpresiones.IndexOf(obj);
            Application.Current.MainPage.Navigation.PushAsync(editarview);

        }
        private async void NavegarEliminar(Presion obj)
        {
            var opcion = await Application.Current.MainPage.DisplayActionSheet("Confirmación", "No", "Si", "¿Desea eliminar la presion seleccionada");
            if (opcion == "Si")
            {
                Presion = obj;
                Eliminar();





            }
        }
        private void NavegarAgregar()
        {
            ErrorValidacion = "";
            Presion = new Presion();
            Application.Current.MainPage.Navigation.PushAsync(agregarregistros);
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
    
}

