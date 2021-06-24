using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using SQLite;

namespace PresionArter.Model
{
    public class Presiones
    {
        public SQLiteConnection Conexion { get; set; }

        string rutadb = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/PresionA.db3";

        public Presiones()
        {
            if (!File.Exists(rutadb))
            {
                Assembly a = Assembly.GetExecutingAssembly();
                var stream = a.GetManifestResourceStream("PresionArter.Data.PresionA.db3");
                FileStream file = File.Create(rutadb);
                stream.CopyTo(file);
                stream.Close();
                file.Close();
            }
            Conexion = new SQLiteConnection(rutadb);
        }

        public void Insert(Presion p)
        {
            if (string.IsNullOrWhiteSpace(p.Pulso.ToString()))
            {
                throw new ArgumentException("Ingrese un pulso");
            }
            if (string.IsNullOrWhiteSpace(p.Diastolica.ToString()))
            {
                throw new ArgumentException("Ingrese la diastolica");
            }
            if (string.IsNullOrWhiteSpace(p.Sistolica.ToString()))
            {
                throw new ArgumentException("Ingrese una sistolica");
            }

            Conexion.Insert(p);
        }

        public IEnumerable<Presion> GetAll()
        {
            return Conexion.Table<Presion>();
        }

        public Presion Get(int id)
        {
            return Conexion.Get<Presion>(id);
        }

        public void Update(Presion p)
        {
            if (string.IsNullOrWhiteSpace(p.Pulso.ToString()))
            {
                throw new ArgumentException("Ingrese un pulso");
            }
            if (string.IsNullOrWhiteSpace(p.Diastolica.ToString()))
            {
                throw new ArgumentException("Ingrese una diastolica");
            }
            if (string.IsNullOrWhiteSpace(p.Sistolica.ToString()))
            {
                throw new ArgumentException("Ingrese la sostolica");
            }

            var citabd = Get(p.Id);
            citabd.Pulso = p.Pulso;
            citabd.Diastolica = p.Diastolica;
            citabd.Sistolica = p.Sistolica;
            citabd.Fecha = p.Fecha;

            Conexion.Update(p);

        }

        public void Delete(Presion p)
        {
            Conexion.Delete(p);
        }
    }
}
