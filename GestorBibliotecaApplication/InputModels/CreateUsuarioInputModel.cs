namespace GestorBibliotecaApplication.InputModels
{
    public class CreateUsuarioInputModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
    }
}